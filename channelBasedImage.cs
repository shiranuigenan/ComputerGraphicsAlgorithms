using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ComputerGraphicsAlgorithms
{
    public class channelBasedImage
    {
        public int width;
        public int height;

        //renk doygunluğu 0-16777215(24bit) arası değişmektedir
        public int[,] r;
        public int[,] g;
        public int[,] b;
        public int[,] a;
        public channelBasedImage()
        {
            width = 1;
            height = 1;

            r = new int[1, 1] { { 0xFEDCBA } };
            g = new int[1, 1] { { 0xA98765 } };
            b = new int[1, 1] { { 0x543210 } };
            a = new int[1, 1] { { 0xFFFFFF } };
        }
        public channelBasedImage(Bitmap bitmap)
        {
            width = bitmap.Width;
            height = bitmap.Height;

            r = new int[height, width];
            g = new int[height, width];
            b = new int[height, width];
            a = new int[height, width];

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
                        a[i, j] = 0xFFFFFF;
                    }
            }
        }
        public Bitmap convertBitmap()
        {
            try
            {
                //blue green red alpha
                var pixels = new ushort[height * width * 4];

                int k = 0;
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        pixels[k++] = (ushort)(b[i, j]>>8);
                        pixels[k++] = (ushort)(g[i, j]>>8);
                        pixels[k++] = (ushort)(r[i, j]>>8);
                        pixels[k++] = (ushort)(a[i, j]>>8);
                    }

                var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
                var bitmap = new Bitmap(width, height, width * 8, PixelFormat.Format64bppArgb, handle.AddrOfPinnedObject());

                return bitmap;
            }
            catch (System.Exception e) { }
            return null;
        }
        public void Scale(byte ratio)
        {
            width /= ratio;
            height /= ratio;
            r = common.Scale(r, ratio);
            g = common.Scale(g, ratio);
            b = common.Scale(b, ratio);
            a = common.Scale(a, ratio);
        }
        public Bitmap convertBitmap24()
        {
            var a = new List<Tuple<byte, byte, byte>>();
            try
            {
                //blue green red alpha
                var pixels = new byte[height * width * 3];

                int k = 0;
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        pixels[k++] = (byte)(b[i, j] >> 8);
                        pixels[k++] = (byte)(g[i, j] >> 8);
                        pixels[k++] = (byte)(r[i, j] >> 8);
                        a.Add(Tuple.Create(pixels[k - 1], pixels[k - 2], pixels[k - 3]));
                    }

                var aa=a.GroupBy(x => x).OrderByDescending(x=>x.Count()).Select(x => new { x.Key, Count = x.Count() }).ToList();
                aa.ForEach(i => Console.WriteLine(i));

                var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
                var bitmap = new Bitmap(width, height, width * 3, PixelFormat.Format24bppRgb, handle.AddrOfPinnedObject());

                return bitmap;
            }
            catch (Exception) { }
            return null;
        }
    }
}
