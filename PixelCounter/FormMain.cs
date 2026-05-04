using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace PixelCounter
{
    public struct Pixel
    {
        public double Black;
        public double White;
        public double Other;
        public double Ratio;
        public double Area;

        // pixelAreaCm2: physical area of one pixel in cm²
        public Pixel(double black, double white, double other, double pixelAreaCm2)
        {
            Black = black;
            White = white;
            Other = other;

            double total = black + white;
            if (total == 0)
            {
                Ratio = 0;
                Area = 0;
            }
            else
            {
                Ratio = Math.Round((black / total) * 100, 2);
                Area = Math.Round(black * pixelAreaCm2, 4);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}\t{4}", Black, White, Other, Ratio, Area);
        }
    }

    public partial class FormMain : Form
    {
        private string path = string.Empty;
        private Bitmap? bmp;
        private BitmapData bd = default;
        private IntPtr ptrToBd;
        private string currentFile = string.Empty;
        private StreamWriter? writeFile;

        private static readonly (string Name, int Width, int Height)[] PaperPresets =
        {
            ("A4",  210, 297),
            ("A3",  297, 420),
            ("A2",  420, 594),
            ("A1",  594, 841),
            ("A0",  841, 1189),
        };

        public FormMain()
        {
            InitializeComponent();

            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            using var stream = asm.GetManifestResourceStream("PixelCounter.pixelcounter.ico");
            if (stream != null)
                Icon = new Icon(stream);

            linkLabel.Links.Add(12, 13, "http://primuszpeter.blogspot.com/");

            foreach (var p in PaperPresets)
                cmbPaperSize.Items.Add(p.Name);
            cmbPaperSize.Items.Add(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "hu" ? "Egyéni" : "Custom");
            cmbPaperSize.SelectedIndex = 0;

            ApplyStrings();
        }

        private void ApplyStrings()
        {
            gBoxFolder.Text    = Strings.FolderGroupTitle;
            btnFolder.Text     = Strings.FolderButton;
            gBoxPaper.Text     = Strings.PaperGroupTitle;
            lblWidth.Text      = Strings.WidthLabel;
            cbAutoDpi.Text     = Strings.AutoDpiCheck;
            gBoxProcess.Text   = Strings.ProcessGroupTitle;
            lblConvert.Text    = Strings.ConvertLabel;
            cbConvert.Text     = Strings.ConvertCheck;
            cbFillHoles.Text   = Strings.FillHolesCheck;
            cbComponents.Text  = Strings.ComponentsCheck;
            lblMinSize.Text    = Strings.MinSizeLabel;
            btnExit.Text           = Strings.ExitButton;
            lblDpi.Text            = string.Empty;
            labelProgress.AutoSize = false;
            labelProgress.Text     = Strings.Ready;
        }

        // Returns area per pixel in cm² using the chosen method.
        // DPI mode: derived from image metadata.
        // Paper mode: derived from (paper area / total pixels).
        private double GetPixelAreaCm2(Bitmap bitmap)
        {
            if (cbAutoDpi.Checked)
            {
                double dpiX = bitmap.HorizontalResolution;
                double dpiY = bitmap.VerticalResolution;
                if (dpiX > 10 && dpiY > 10)
                {
                    double cmPerPxX = 2.54 / dpiX;
                    double cmPerPxY = 2.54 / dpiY;
                    string dpiText = string.Format(Strings.DpiDetected, dpiX, dpiY);
                    Invoke(new Action(() => lblDpi.Text = dpiText));
                    return cmPerPxX * cmPerPxY;
                }
                // fallthrough to paper size if DPI metadata is missing
                Invoke(new Action(() => lblDpi.Text = Strings.DpiNotFound));
            }

            // Paper size fallback
            double paperArea = PaperAreaCm2();
            if (paperArea <= 0) return 0;
            return paperArea / (bitmap.Width * bitmap.Height);
        }

        private double PaperAreaCm2()
        {
            if (!double.TryParse(txtWidth.Text, out double w) || w <= 0) return 0;
            if (!double.TryParse(txtHeight.Text, out double h) || h <= 0) return 0;
            return (w / 10.0) * (h / 10.0);
        }

        private void cbAutoDpi_CheckedChanged(object sender, EventArgs e)
        {
            bool manual = !cbAutoDpi.Checked;
            cmbPaperSize.Enabled = manual;
            lblWidth.Enabled     = manual;
            txtWidth.Enabled     = manual;
            lblX.Enabled         = manual;
            txtHeight.Enabled    = manual;
            lblMm.Enabled        = manual;
            if (!manual) lblDpi.Text = string.Empty;
        }

        private void cmbPaperSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = cmbPaperSize.SelectedIndex;
            if (idx < PaperPresets.Length)
            {
                txtWidth.Text = PaperPresets[idx].Width.ToString();
                txtHeight.Text = PaperPresets[idx].Height.ToString();
                txtWidth.ReadOnly = true;
                txtHeight.ReadOnly = true;
            }
            else
            {
                txtWidth.ReadOnly = false;
                txtHeight.ReadOnly = false;
            }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                path = folderBrowserDialog.SelectedPath;
            textBoxFolders.Text = path;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            backgroundWorker?.Dispose();
            writeFile?.Dispose();
            bmp?.Dispose();
            Close();
        }

        public void ProcessDirectory(string targetDirectory, bool componentMode, int minPixels)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);

            CreateFile(targetDirectory + "\\" + "report", componentMode);

            foreach (string fileName in fileEntries)
            {
                if (backgroundWorker.CancellationPending)
                {
                    CloseFile();
                    return;
                }

                if (!CheckExtension(fileName)) continue;

                currentFile = Path.GetFileName(fileName);
                backgroundWorker.ReportProgress(0);

                if (cbConvert.CheckState == CheckState.Checked)
                    bmp = ConvertBitmap(fileName);
                else
                    bmp = CheckPixelFormat(new Bitmap(fileName));

                double pixelAreaCm2 = GetPixelAreaCm2(bmp);

                if (componentMode)
                {
                    Otsu otsu = new Otsu();
                    var components = otsu.FindComponents(bmp, minPixels);

                    if (cbSave.Checked)
                        SaveLabeledBitmap(fileName, bmp, components);

                    for (int i = 0; i < components.Count; i++)
                    {
                        double area = Math.Round(components[i].Count * pixelAreaCm2, 4);
                        WriteText(string.Format("{0}\t{1}\t{2}\t{3:0.0000}",
                            currentFile, i + 1, components[i].Count, area));
                    }
                }
                else
                {
                    if (cbSave.Checked)
                        SaveBitmap(fileName, bmp);

                    Pixel pixel = ProcessBitmap(bmp, pixelAreaCm2);
                    WriteText(currentFile + '\t' + pixel.ToString());
                }
            }

            CloseFile();
        }

        private Bitmap CheckPixelFormat(Bitmap source)
        {
            if (source.PixelFormat == PixelFormat.Format24bppRgb)
                return source;

            Bitmap newBmp = new Bitmap(source.Width, source.Height, PixelFormat.Format24bppRgb);
            using Graphics g = Graphics.FromImage(newBmp);
            g.DrawImage(source, new Rectangle(Point.Empty, source.Size));
            return newBmp;
        }

        private bool CheckExtension(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            string[] extensions = { ".bmp", ".png", ".jpg", ".gif", ".tiff", ".tif" };
            return Array.Exists(extensions, s => s == ext);
        }

        private void CreateFile(string fileName, bool componentMode)
        {
            writeFile = new StreamWriter(fileName + ".txt", append: false);
            WriteText(componentMode ? Strings.ReportHeaderComponents : Strings.ReportHeaderNormal);
        }

        private void WriteText(string text)
        {
            writeFile?.WriteLine(text);
        }

        private void CloseFile()
        {
            writeFile?.Close();
        }

        private Bitmap ConvertBitmap(string filePath)
        {
            Otsu otsu = new Otsu();
            Bitmap converted = CheckPixelFormat(new Bitmap(filePath));

            otsu.ConvertToGrayScaleFast(converted);
            int threshold = otsu.GetOtsuThreshold(converted);
            otsu.Threshold(converted, threshold);

            if (cbFillHoles.Checked)
                otsu.FillInteriorHoles(converted);

            return converted;
        }

        private string GetOutputPath(string filePath)
        {
            string folder = Path.Combine(Path.GetDirectoryName(filePath)!, "Black&White");
            Directory.CreateDirectory(folder);
            return Path.Combine(folder, Path.ChangeExtension(Path.GetFileName(filePath), ".png"));
        }

        private void SaveBitmap(string filePath, Bitmap bitmap)
        {
            bitmap.Save(GetOutputPath(filePath), ImageFormat.Png);
        }

        private void SaveLabeledBitmap(string filePath, Bitmap bitmap, List<(int Count, Point Centroid)> components)
        {
            using Bitmap labeled = new Bitmap(bitmap);
            using Graphics g = Graphics.FromImage(labeled);

            int fontSize = Math.Max(12, Math.Min(bitmap.Width, bitmap.Height) / 40);
            using Font font = new Font("Arial", fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
            using SolidBrush textBrush = new SolidBrush(Color.Red);
            using SolidBrush bgBrush = new SolidBrush(Color.FromArgb(180, Color.White));

            for (int i = 0; i < components.Count; i++)
            {
                string label = (i + 1).ToString();
                Point c = components[i].Centroid;
                SizeF sz = g.MeasureString(label, font);
                float lx = c.X - sz.Width / 2;
                float ly = c.Y - sz.Height / 2;
                g.FillRectangle(bgBrush, lx - 1, ly - 1, sz.Width + 2, sz.Height + 2);
                g.DrawString(label, font, textBrush, lx, ly);
            }

            labeled.Save(GetOutputPath(filePath), ImageFormat.Png);
        }

        private Pixel ProcessBitmap(Bitmap bitmap, double pixelAreaCm2)
        {
            int blackPx = 0;
            int whitePx = 0;
            int otherPx = 0;
            int totalPx = bitmap.Width * bitmap.Height;
            int processed = 0;

            int black = Color.Black.ToArgb();
            int white = Color.White.ToArgb();

            BitmapLock();

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    int pixel = GetPixelInt(x, y);
                    if (pixel == black)
                        blackPx++;
                    else if (pixel == white)
                        whitePx++;
                    else
                        otherPx++;

                    processed++;
                    if (processed % (totalPx / 10 + 1) == 0)
                        backgroundWorker.ReportProgress((int)((double)processed / totalPx * 100));
                }
            }

            BitmapUnlock();

            if (blackPx + whitePx + otherPx == totalPx)
                return new Pixel(blackPx, whitePx, otherPx, pixelAreaCm2);

            return new Pixel(0, 0, 0, 0);
        }

        #region Unsafe Bitmap Methods

        private void BitmapLock()
        {
            bd = bmp!.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            ptrToBd = bd.Scan0;
        }

        private void BitmapUnlock()
        {
            bmp!.UnlockBits(bd);
        }

        private int GetPixelInt(int x, int y)
        {
            return GetPixelRGB(ptrToBd, bd.Stride, x, y);
        }

        unsafe private int GetPixelRGB(IntPtr ptr, int stride, int x, int y)
        {
            int* pixels = (int*)ptr;
            return pixels[x + y * stride / 4];
        }

        #endregion

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool componentMode = (bool)Invoke(new Func<bool>(() => cbComponents.Checked));
            int minPixels = (int)Invoke(new Func<int>(() => {
                int.TryParse(txtMinSize.Text, out int v);
                return v > 0 ? v : 100;
            }));
            ProcessDirectory(path, componentMode, minPixels);
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            int pct = Math.Min(e.ProgressPercentage, 100);
            labelProgress.Text = pct > 0
                ? string.Format("{0} {1} ({2}%)", Strings.Ready, currentFile, pct)
                : string.Format("{0} {1}", Strings.Ready, currentFile);
            progressBar.Value = pct;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            labelProgress.Text = Strings.ProcessingDone;
            progressBar.Value = 0;
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Link.LinkData!.ToString()!) { UseShellExecute = true });
        }
    }
}
