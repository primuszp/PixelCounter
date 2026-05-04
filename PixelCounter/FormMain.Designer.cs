namespace PixelCounter
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            gBoxFolder = new System.Windows.Forms.GroupBox();
            textBoxFolders = new System.Windows.Forms.TextBox();
            btnFolder = new System.Windows.Forms.Button();
            gBoxPaper = new System.Windows.Forms.GroupBox();
            cbAutoDpi = new System.Windows.Forms.CheckBox();
            lblDpi = new System.Windows.Forms.Label();
            cmbPaperSize = new System.Windows.Forms.ComboBox();
            lblWidth = new System.Windows.Forms.Label();
            txtWidth = new System.Windows.Forms.TextBox();
            lblX = new System.Windows.Forms.Label();
            txtHeight = new System.Windows.Forms.TextBox();
            lblMm = new System.Windows.Forms.Label();
            gBoxProcess = new System.Windows.Forms.GroupBox();
            labelProgress = new System.Windows.Forms.Label();
            cbSave = new System.Windows.Forms.CheckBox();
            cbConvert = new System.Windows.Forms.CheckBox();
            cbFillHoles = new System.Windows.Forms.CheckBox();
            cbComponents = new System.Windows.Forms.CheckBox();
            lblMinSize = new System.Windows.Forms.Label();
            txtMinSize = new System.Windows.Forms.TextBox();
            btnStop = new System.Windows.Forms.Button();
            lblConvert = new System.Windows.Forms.Label();
            btnStart = new System.Windows.Forms.Button();
            progressBar = new System.Windows.Forms.ProgressBar();
            btnExit = new System.Windows.Forms.Button();
            folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            linkLabel = new System.Windows.Forms.LinkLabel();
            gBoxFolder.SuspendLayout();
            gBoxPaper.SuspendLayout();
            gBoxProcess.SuspendLayout();
            SuspendLayout();
            // 
            // gBoxFolder
            // 
            gBoxFolder.Controls.Add(textBoxFolders);
            gBoxFolder.Controls.Add(btnFolder);
            gBoxFolder.Location = new System.Drawing.Point(13, 14);
            gBoxFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gBoxFolder.Name = "gBoxFolder";
            gBoxFolder.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gBoxFolder.Size = new System.Drawing.Size(499, 88);
            gBoxFolder.TabIndex = 0;
            gBoxFolder.TabStop = false;
            gBoxFolder.Text = "Forrás";
            // 
            // textBoxFolders
            // 
            textBoxFolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            textBoxFolders.Location = new System.Drawing.Point(116, 41);
            textBoxFolders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBoxFolders.Name = "textBoxFolders";
            textBoxFolders.ReadOnly = true;
            textBoxFolders.Size = new System.Drawing.Size(375, 26);
            textBoxFolders.TabIndex = 1;
            // 
            // btnFolder
            // 
            btnFolder.Location = new System.Drawing.Point(8, 29);
            btnFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnFolder.Name = "btnFolder";
            btnFolder.Size = new System.Drawing.Size(100, 50);
            btnFolder.TabIndex = 0;
            btnFolder.Text = "Mappa";
            btnFolder.UseVisualStyleBackColor = true;
            btnFolder.Click += btnFolder_Click;
            // 
            // gBoxPaper
            // 
            gBoxPaper.Controls.Add(cbAutoDpi);
            gBoxPaper.Controls.Add(lblDpi);
            gBoxPaper.Controls.Add(cmbPaperSize);
            gBoxPaper.Controls.Add(lblWidth);
            gBoxPaper.Controls.Add(txtWidth);
            gBoxPaper.Controls.Add(lblX);
            gBoxPaper.Controls.Add(txtHeight);
            gBoxPaper.Controls.Add(lblMm);
            gBoxPaper.Location = new System.Drawing.Point(13, 112);
            gBoxPaper.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gBoxPaper.Name = "gBoxPaper";
            gBoxPaper.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gBoxPaper.Size = new System.Drawing.Size(499, 104);
            gBoxPaper.TabIndex = 1;
            gBoxPaper.TabStop = false;
            gBoxPaper.Text = "Papír mérete";
            // 
            // cbAutoDpi
            // 
            cbAutoDpi.AutoSize = true;
            cbAutoDpi.Checked = true;
            cbAutoDpi.CheckState = System.Windows.Forms.CheckState.Checked;
            cbAutoDpi.Location = new System.Drawing.Point(8, 29);
            cbAutoDpi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cbAutoDpi.Name = "cbAutoDpi";
            cbAutoDpi.Size = new System.Drawing.Size(146, 24);
            cbAutoDpi.TabIndex = 10;
            cbAutoDpi.Text = "Auto (image DPI)";
            cbAutoDpi.UseVisualStyleBackColor = true;
            cbAutoDpi.CheckedChanged += cbAutoDpi_CheckedChanged;
            // 
            // lblDpi
            // 
            lblDpi.AutoSize = true;
            lblDpi.ForeColor = System.Drawing.Color.DimGray;
            lblDpi.Location = new System.Drawing.Point(210, 30);
            lblDpi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblDpi.Name = "lblDpi";
            lblDpi.Size = new System.Drawing.Size(0, 20);
            lblDpi.TabIndex = 11;
            // 
            // cmbPaperSize
            // 
            cmbPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbPaperSize.Enabled = false;
            cmbPaperSize.Location = new System.Drawing.Point(8, 66);
            cmbPaperSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cmbPaperSize.Name = "cmbPaperSize";
            cmbPaperSize.Size = new System.Drawing.Size(99, 28);
            cmbPaperSize.TabIndex = 0;
            cmbPaperSize.SelectedIndexChanged += cmbPaperSize_SelectedIndexChanged;
            // 
            // lblWidth
            // 
            lblWidth.AutoSize = true;
            lblWidth.Enabled = false;
            lblWidth.Location = new System.Drawing.Point(116, 70);
            lblWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new System.Drawing.Size(76, 20);
            lblWidth.TabIndex = 1;
            lblWidth.Text = "Szélesség:";
            // 
            // txtWidth
            // 
            txtWidth.Enabled = false;
            txtWidth.Location = new System.Drawing.Point(210, 66);
            txtWidth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new System.Drawing.Size(65, 27);
            txtWidth.TabIndex = 1;
            // 
            // lblX
            // 
            lblX.AutoSize = true;
            lblX.Enabled = false;
            lblX.Location = new System.Drawing.Point(283, 70);
            lblX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblX.Name = "lblX";
            lblX.Size = new System.Drawing.Size(19, 20);
            lblX.TabIndex = 2;
            lblX.Text = "×";
            // 
            // txtHeight
            // 
            txtHeight.Enabled = false;
            txtHeight.Location = new System.Drawing.Point(310, 66);
            txtHeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new System.Drawing.Size(65, 27);
            txtHeight.TabIndex = 2;
            // 
            // lblMm
            // 
            lblMm.AutoSize = true;
            lblMm.Enabled = false;
            lblMm.Location = new System.Drawing.Point(383, 70);
            lblMm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblMm.Name = "lblMm";
            lblMm.Size = new System.Drawing.Size(35, 20);
            lblMm.TabIndex = 3;
            lblMm.Text = "mm";
            // 
            // gBoxProcess
            // 
            gBoxProcess.Controls.Add(labelProgress);
            gBoxProcess.Controls.Add(cbSave);
            gBoxProcess.Controls.Add(cbConvert);
            gBoxProcess.Controls.Add(cbFillHoles);
            gBoxProcess.Controls.Add(cbComponents);
            gBoxProcess.Controls.Add(lblMinSize);
            gBoxProcess.Controls.Add(txtMinSize);
            gBoxProcess.Controls.Add(btnStop);
            gBoxProcess.Controls.Add(lblConvert);
            gBoxProcess.Controls.Add(btnStart);
            gBoxProcess.Controls.Add(progressBar);
            gBoxProcess.Location = new System.Drawing.Point(13, 226);
            gBoxProcess.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gBoxProcess.Name = "gBoxProcess";
            gBoxProcess.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gBoxProcess.Size = new System.Drawing.Size(499, 198);
            gBoxProcess.TabIndex = 2;
            gBoxProcess.TabStop = false;
            gBoxProcess.Text = "Feldolgozás";
            // 
            // labelProgress
            // 
            labelProgress.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            labelProgress.Location = new System.Drawing.Point(116, 25);
            labelProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelProgress.Name = "labelProgress";
            labelProgress.Size = new System.Drawing.Size(375, 23);
            labelProgress.TabIndex = 3;
            labelProgress.Text = "Feldolgozva:";
            // 
            // cbSave
            // 
            cbSave.AutoSize = true;
            cbSave.Checked = true;
            cbSave.CheckState = System.Windows.Forms.CheckState.Checked;
            cbSave.Location = new System.Drawing.Point(337, 163);
            cbSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cbSave.Name = "cbSave";
            cbSave.Size = new System.Drawing.Size(60, 24);
            cbSave.TabIndex = 9;
            cbSave.Text = "PNG";
            cbSave.UseVisualStyleBackColor = true;
            // 
            // cbConvert
            // 
            cbConvert.AutoSize = true;
            cbConvert.Checked = true;
            cbConvert.CheckState = System.Windows.Forms.CheckState.Checked;
            cbConvert.Location = new System.Drawing.Point(213, 95);
            cbConvert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cbConvert.Name = "cbConvert";
            cbConvert.Size = new System.Drawing.Size(116, 24);
            cbConvert.TabIndex = 8;
            cbConvert.Text = "Fekete/Fehér";
            cbConvert.UseVisualStyleBackColor = true;
            // 
            // cbFillHoles
            // 
            cbFillHoles.AutoSize = true;
            cbFillHoles.Checked = true;
            cbFillHoles.CheckState = System.Windows.Forms.CheckState.Checked;
            cbFillHoles.Location = new System.Drawing.Point(337, 95);
            cbFillHoles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cbFillHoles.Name = "cbFillHoles";
            cbFillHoles.Size = new System.Drawing.Size(107, 24);
            cbFillHoles.TabIndex = 10;
            cbFillHoles.Text = "Lyukkitöltés";
            cbFillHoles.UseVisualStyleBackColor = true;
            // 
            // cbComponents
            // 
            cbComponents.AutoSize = true;
            cbComponents.Location = new System.Drawing.Point(337, 129);
            cbComponents.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            cbComponents.Name = "cbComponents";
            cbComponents.Size = new System.Drawing.Size(125, 24);
            cbComponents.TabIndex = 11;
            cbComponents.Text = "Komponensek";
            cbComponents.UseVisualStyleBackColor = true;
            // 
            // lblMinSize
            // 
            lblMinSize.AutoSize = true;
            lblMinSize.Location = new System.Drawing.Point(116, 129);
            lblMinSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblMinSize.Name = "lblMinSize";
            lblMinSize.Size = new System.Drawing.Size(113, 20);
            lblMinSize.TabIndex = 12;
            lblMinSize.Text = "Min. méret (px):";
            // 
            // txtMinSize
            // 
            txtMinSize.Location = new System.Drawing.Point(237, 126);
            txtMinSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            txtMinSize.Name = "txtMinSize";
            txtMinSize.Size = new System.Drawing.Size(65, 27);
            txtMinSize.TabIndex = 13;
            txtMinSize.Text = "500";
            // 
            // btnStop
            // 
            btnStop.Location = new System.Drawing.Point(8, 94);
            btnStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnStop.Name = "btnStop";
            btnStop.Size = new System.Drawing.Size(100, 55);
            btnStop.TabIndex = 7;
            btnStop.Text = "STOP";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // lblConvert
            // 
            lblConvert.AutoSize = true;
            lblConvert.Location = new System.Drawing.Point(116, 95);
            lblConvert.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblConvert.Name = "lblConvert";
            lblConvert.Size = new System.Drawing.Size(89, 20);
            lblConvert.TabIndex = 6;
            lblConvert.Text = "Konvertálás:";
            // 
            // btnStart
            // 
            btnStart.Location = new System.Drawing.Point(8, 25);
            btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnStart.Name = "btnStart";
            btnStart.Size = new System.Drawing.Size(100, 55);
            btnStart.TabIndex = 2;
            btnStart.Text = "START";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // progressBar
            // 
            progressBar.Location = new System.Drawing.Point(116, 53);
            progressBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(375, 21);
            progressBar.TabIndex = 1;
            // 
            // btnExit
            // 
            btnExit.Location = new System.Drawing.Point(396, 427);
            btnExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnExit.Name = "btnExit";
            btnExit.Size = new System.Drawing.Size(116, 30);
            btnExit.TabIndex = 3;
            btnExit.Text = "Kilépés";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // backgroundWorker
            // 
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            // 
            // linkLabel
            // 
            linkLabel.AutoSize = true;
            linkLabel.Location = new System.Drawing.Point(13, 432);
            linkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabel.Name = "linkLabel";
            linkLabel.Size = new System.Drawing.Size(169, 20);
            linkLabel.TabIndex = 4;
            linkLabel.TabStop = true;
            linkLabel.Text = "Készítette: Primusz Péter";
            linkLabel.LinkClicked += linkLabel_LinkClicked;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(522, 463);
            Controls.Add(linkLabel);
            Controls.Add(btnExit);
            Controls.Add(gBoxProcess);
            Controls.Add(gBoxPaper);
            Controls.Add(gBoxFolder);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "FormMain";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "PixelCounter v2.2";
            gBoxFolder.ResumeLayout(false);
            gBoxFolder.PerformLayout();
            gBoxPaper.ResumeLayout(false);
            gBoxPaper.PerformLayout();
            gBoxProcess.ResumeLayout(false);
            gBoxProcess.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox gBoxFolder;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox textBoxFolders;
        private System.Windows.Forms.GroupBox gBoxPaper;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.Label lblMm;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox gBoxProcess;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblConvert;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.CheckBox cbSave;
        private System.Windows.Forms.CheckBox cbConvert;
        private System.Windows.Forms.CheckBox cbFillHoles;
        private System.Windows.Forms.CheckBox cbComponents;
        private System.Windows.Forms.Label lblMinSize;
        private System.Windows.Forms.TextBox txtMinSize;
        private System.Windows.Forms.CheckBox cbAutoDpi;
        private System.Windows.Forms.Label lblDpi;
    }
}
