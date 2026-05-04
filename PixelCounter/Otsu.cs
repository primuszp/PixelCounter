using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace PixelCounter
{
    [SupportedOSPlatform("windows")]
    class Otsu
    {
        // function is used to compute the q values in the equation
        private float Px(int init, int end, int[] hist)
        {
            int sum = 0;
            int i;
            for (i = init; i <= end; i++)
                sum += hist[i];

            return (float)sum;
        }

        // function is used to compute the mean values in the equation (mu)
        private float Mx(int init, int end, int[] hist)
        {
            int sum = 0;
            int i;
            for (i = init; i <= end; i++)
                sum += i * hist[i];

            return (float)sum;
        }

        // finds the maximum element in a vector
        private int FindMax(float[] vec, int n)
        {
            float maxVec = 0;
            int idx = 0;
            int i;

            for (i = 1; i < n - 1; i++)
            {
                if (vec[i] > maxVec)
                {
                    maxVec = vec[i];
                    idx = i;
                }
            }
            return idx;
        }

        // simply computes the image histogram
        unsafe private void GetHistogram(byte* p, int w, int h, int ws, int[] hist)
        {
            hist.Initialize();
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w * 3; j += 3)
                {
                    int index = i * ws + j;
                    hist[p[index]]++;
                }
            }
        }

        // find otsu threshold
        public int GetOtsuThreshold(Bitmap bmp)
        {
            byte t = 0;
            float[] vet = new float[256];
            int[] hist = new int[256];
            vet.Initialize();

            float p1, p2, p12;
            int k;

            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* p = (byte*)(void*)bmData.Scan0.ToPointer();

                GetHistogram(p, bmp.Width, bmp.Height, bmData.Stride, hist);

                // loop through all possible t values and maximize between class variance
                for (k = 1; k != 255; k++)
                {
                    p1 = Px(0, k, hist);
                    p2 = Px(k + 1, 255, hist);
                    p12 = p1 * p2;
                    if (p12 == 0)
                        p12 = 1;
                    float diff = (Mx(0, k, hist) * p2) - (Mx(k + 1, 255, hist) * p1);
                    vet[k] = (float)diff * diff / p12;
                    //vet[k] = (float)Math.Pow((Mx(0, k, hist) * p2) - (Mx(k + 1, 255, hist) * p1), 2) / p12;
                }
            }
            bmp.UnlockBits(bmData);

            t = (byte)FindMax(vet, 256);

            return t;
        }

        // Simple routine to convert to gray scale, not work fine
        //public void Convert2GrayScaleFast(Bitmap bmp)
        //{
        //    BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
        //            ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
        //    unsafe
        //    {
        //        byte* p = (byte*)(void*)bmData.Scan0.ToPointer();
        //        int stopAddress = (int)p + bmData.Stride * bmData.Height;
        //        while ((int)p != stopAddress)
        //        {
        //            p[0] = (byte)(.299 * p[2] + .587 * p[1] + .114 * p[0]);
        //            p[1] = p[0];
        //            p[2] = p[0];
        //            p += 3;
        //        }
        //    }
        //    bmp.UnlockBits(bmData);
        //}

        // http://www.liensberger.it/web/blog/?p=155
        public void ConvertToGrayScaleFast(Bitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentNullException("Bitmap Error!");

            // lock the bitmap.
            var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            try
            {
                unsafe
                {
                    // get a pointer to the data.
                    byte* ptr = (byte*)data.Scan0;

                    // loop over all the data.
                    for (int i = 0; i < data.Height; i++)
                    {
                        for (int j = 0; j < data.Width; j++)
                        {
                            // calculate the gray value.
                            byte y = (byte)((0.299 * ptr[2]) + (0.587 * ptr[1]) + (0.114 * ptr[0]));

                            // set the gray value.
                            ptr[0] = ptr[1] = ptr[2] = y;

                            // increment the pointer.
                            ptr += 3;
                        }
                        // move on to the next line.
                        ptr += data.Stride - data.Width * 3;
                    }
                }
            }
            finally
            {
                // unlock the bits when done or when
                // an exception has been thrown.
                bmp.UnlockBits(data);
            }
        }

        // simple routine for thresholding
        public void Threshold(Bitmap bmp, int thresh)
        {
            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* p = (byte*)(void*)bmData.Scan0.ToPointer();

                int h = bmp.Height;
                int w = bmp.Width;
                int ws = bmData.Stride;

                for (int i = 0; i < h; i++)
                {
                    byte* row = &p[i * ws];
                    for (int j = 0; j < w * 3; j += 3)
                    {
                        row[j] = (byte)((row[j] > (byte)thresh) ? 255 : 0);
                        row[j + 1] = (byte)((row[j + 1] > (byte)thresh) ? 255 : 0);
                        row[j + 2] = (byte)((row[j + 2] > (byte)thresh) ? 255 : 0);
                    }
                }
            }
            bmp.UnlockBits(bmData);
        }

        // Fills white holes inside dark objects (e.g. leaf veins).
        // BFS flood fill from all border pixels marks background white;
        // any remaining white pixel is interior and gets set to black.
        public void FillInteriorHoles(Bitmap bmp)
        {
            int w = bmp.Width;
            int h = bmp.Height;

            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, w, h),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* p = (byte*)(void*)bmData.Scan0.ToPointer();
                int stride = bmData.Stride;

                // background marker: 128 (not 0 or 255)
                const byte BG = 128;

                var queue = new Queue<int>(w * 2 + h * 2);

                // seed all border pixels that are white
                for (int x = 0; x < w; x++)
                {
                    TryEnqueue(p, stride, x, 0,     w, h, BG, queue);
                    TryEnqueue(p, stride, x, h - 1, w, h, BG, queue);
                }
                for (int y = 1; y < h - 1; y++)
                {
                    TryEnqueue(p, stride, 0,     y, w, h, BG, queue);
                    TryEnqueue(p, stride, w - 1, y, w, h, BG, queue);
                }

                // BFS: spread BG marker to all connected white pixels
                while (queue.Count > 0)
                {
                    int idx = queue.Dequeue();
                    int x = idx % w;
                    int y = idx / w;
                    TryEnqueue(p, stride, x - 1, y,     w, h, BG, queue);
                    TryEnqueue(p, stride, x + 1, y,     w, h, BG, queue);
                    TryEnqueue(p, stride, x,     y - 1, w, h, BG, queue);
                    TryEnqueue(p, stride, x,     y + 1, w, h, BG, queue);
                }

                // restore BG→white, interior white→black
                for (int y = 0; y < h; y++)
                {
                    byte* row = &p[y * stride];
                    for (int x = 0; x < w; x++)
                    {
                        byte v = row[x * 3];
                        if (v == BG)
                            row[x * 3] = row[x * 3 + 1] = row[x * 3 + 2] = 255;
                        else if (v == 255)
                            row[x * 3] = row[x * 3 + 1] = row[x * 3 + 2] = 0;
                    }
                }
            }

            bmp.UnlockBits(bmData);
        }

        unsafe private static void TryEnqueue(byte* p, int stride, int x, int y,
            int w, int h, byte marker, Queue<int> queue)
        {
            if (x < 0 || y < 0 || x >= w || y >= h) return;
            byte* px = p + y * stride + x * 3;
            if (px[0] != 255) return;   // not white → skip
            px[0] = px[1] = px[2] = marker;
            queue.Enqueue(y * w + x);
        }

        // Returns (pixelCount, centroid) for each connected black component >= minPixels.
        public List<(int Count, Point Centroid)> FindComponents(Bitmap bmp, int minPixels)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            bool[] visited = new bool[w * h];
            var results = new List<(int, Point)>();
            var queue = new Queue<int>();

            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* p = (byte*)(void*)bmData.Scan0.ToPointer();
                int stride = bmData.Stride;

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        int idx = y * w + x;
                        if (visited[idx]) continue;

                        byte v = p[y * stride + x * 3];
                        if (v != 0) { visited[idx] = true; continue; }  // not black

                        // BFS from this unvisited black pixel
                        int count = 0;
                        long sumX = 0, sumY = 0;
                        queue.Clear();
                        queue.Enqueue(idx);
                        visited[idx] = true;

                        while (queue.Count > 0)
                        {
                            int cur = queue.Dequeue();
                            int cx = cur % w;
                            int cy = cur / w;
                            count++;
                            sumX += cx;
                            sumY += cy;

                            TryEnqueueComponent(p, stride, cx - 1, cy,     w, h, visited, queue);
                            TryEnqueueComponent(p, stride, cx + 1, cy,     w, h, visited, queue);
                            TryEnqueueComponent(p, stride, cx,     cy - 1, w, h, visited, queue);
                            TryEnqueueComponent(p, stride, cx,     cy + 1, w, h, visited, queue);
                        }

                        if (count >= minPixels)
                        {
                            var centroid = new Point((int)(sumX / count), (int)(sumY / count));
                            results.Add((count, centroid));
                        }
                    }
                }
            }

            bmp.UnlockBits(bmData);
            return results;
        }

        unsafe private static void TryEnqueueComponent(byte* p, int stride, int x, int y,
            int w, int h, bool[] visited, Queue<int> queue)
        {
            if (x < 0 || y < 0 || x >= w || y >= h) return;
            int idx = y * w + x;
            if (visited[idx]) return;
            visited[idx] = true;
            if (p[y * stride + x * 3] != 0) return;  // not black
            queue.Enqueue(idx);
        }
    }
}
