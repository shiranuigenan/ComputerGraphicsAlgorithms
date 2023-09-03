using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ComputerGraphicsAlgorithms
{
    public class channelBasedImage96
    {
        public int width;
        public int height;

        //renk doygunluğu 0-16777215(24bit) arası değişmektedir
        public int[,] r;
        public int[,] g;
        public int[,] b;
        public channelBasedImage96()
        {
            width = 1;
            height = 1;

            r = new int[1, 1] { { 0xFEDCBA } };
            g = new int[1, 1] { { 0xA98765 } };
            b = new int[1, 1] { { 0x543210 } };
        }
        public channelBasedImage96(int width, int height)
        {
            this.width = width;
            this.height = height;

            r = new int[height, width];
            g = new int[height, width];
            b = new int[height, width];
        }
        public channelBasedImage96(Bitmap bitmap)
        {
            width = bitmap.Width;
            height = bitmap.Height;

            r = new int[height, width];
            g = new int[height, width];
            b = new int[height, width];

            var bytes = common.BitmapToByteArray(bitmap);

            //bütün pixel formatları için gerekli dönüşümler yapılmalı
            if (bitmap.PixelFormat == PixelFormat.Format24bppRgb)
            {
                int k = 0;
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        b[i, j] = bytes[k++] * 0x10101;
                        g[i, j] = bytes[k++] * 0x10101;
                        r[i, j] = bytes[k++] * 0x10101;
                    }
            }
        }
        public void Scale(byte ratio)
        {
            width /= ratio;
            height /= ratio;
            r = common.Scale(r, ratio);
            g = common.Scale(g, ratio);
            b = common.Scale(b, ratio);
        }
        public Bitmap convertBitmap()
        {
            try
            {
                //blue green red
                var pixels = new byte[height * width * 3];

                int k = 0;
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        pixels[k++] = (byte)b[i, j];
                        pixels[k++] = (byte)g[i, j];
                        pixels[k++] = (byte)r[i, j];
                    }

                var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
                var bitmap = new Bitmap(width, height, width * 3, PixelFormat.Format24bppRgb, handle.AddrOfPinnedObject());

                return bitmap;
            }
            catch (Exception) { }
            return null;
        }
    }
}
