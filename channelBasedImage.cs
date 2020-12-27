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
        public ushort[,] r;
        public ushort[,] g;
        public ushort[,] b;
        public ushort[,] a;
        public channelBasedImage()
        {
            width = 1;
            height = 1;
            r = new ushort[1, 1] { { 0xCFFF } };
            g = new ushort[1, 1] { { 0xDFFF } };
            b = new ushort[1, 1] { { 0xEFFF } };
            a = new ushort[1, 1] { { 0xFFFF } };
        }
        public channelBasedImage(Bitmap bitmap)
        {
            width = bitmap.Width;
            height = bitmap.Height;

            r = new ushort[height, width];
            g = new ushort[height, width];
            b = new ushort[height, width];
            a = new ushort[height, width];

            var bytes = common.BitmapToByteArray(bitmap);

            //bütün pixel formatları için gerekli dönüşümler yapılmalı
            if (bitmap.PixelFormat == PixelFormat.Format24bppRgb)
            {
                int k = 0;
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        b[i, j] = (ushort)(bytes[k++] * 257);
                        g[i, j] = (ushort)(bytes[k++] * 257);
                        r[i, j] = (ushort)(bytes[k++] * 257);
                        a[i, j] = 65535;
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
                        pixels[k++] = b[i, j];
                        pixels[k++] = g[i, j];
                        pixels[k++] = r[i, j];
                        pixels[k++] = a[i, j];
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
            catch (System.Exception e) { }
            return null;
        }
    }
}
