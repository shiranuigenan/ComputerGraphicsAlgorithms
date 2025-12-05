using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ComputerGraphicsAlgorithms
{
    public static class common
    {
        public struct Color24
        {
            public byte b;
            public byte g;
            public byte r;
        }
        public struct Color32
        {
            public byte b;
            public byte g;
            public byte r;
            public byte a;
        }
        public struct Color48
        {
            public ushort b;
            public ushort g;
            public ushort r;
        }
        public struct Color64
        {
            public ushort b;
            public ushort g;
            public ushort r;
            public ushort a;
        }
        public static Color32 PsuedoGrey(int g)
        {
            if (g < 1) return new Color32 { r = 0, g = 0, b = 0, a = 0 };
            if (g > 1784) return new Color32 { r = 255, g = 255, b = 255, a = 255 };

            var i = g / 7;
            var j = g % 7;

            var k = new byte[7, 3] { { 0, 0, 0 }, { 0, 0, 1 }, { 1, 0, 0 }, { 1, 0, 1 }, { 0, 1, 0 }, { 0, 1, 1 }, { 1, 1, 0 } };
            return new Color32 { r = (byte)(i + k[j, 0]), g = (byte)(i + k[j, 1]), b = (byte)(i + k[j, 2]), a = 255 };
        }
        public static Color32 PsuedoGreyPlus(int x)
        {
            if (x < 1) return new Color32 { r = 0, g = 0, b = 0, a = 0 };
            if (x > 4079) return new Color32 { r = 255, g = 255, b = 255, a = 255 };

            var i = x / 16;
            var j = x % 16;

            var k = new byte[16, 3] { { 0, 0, 0 }, { 0, 0, 1 }, { 0, 0, 2 }, { 1, 0, 0 }, { 1, 0, 1 }, { 1, 0, 1 }, { 1, 0, 2 }, { 2, 0, 0 }, { 2, 0, 1 }, { 2, 0, 2 }, { 2, 0, 2 }, { 0, 1, 0 }, { 0, 1, 1 }, { 0, 1, 2 }, { 0, 1, 2 }, { 1, 1, 0 } };
            var r = Math.Min(i + k[j, 0], 255);
            var g = Math.Min(i + k[j, 1], 255);
            var b = Math.Min(i + k[j, 2], 255);

            return new Color32 { r = (byte)r, g = (byte)g, b = (byte)b, a = 255 };
        }
        public static Color24 PsuedoGreyPlus24(int x)
        {
            if (x < 1) return new Color24 { r = 0, g = 0, b = 0 };
            if (x > 4079) return new Color24 { r = 255, g = 255, b = 255 };

            var i = x / 16;
            var j = x % 16;

            var k = new byte[16, 3] { { 0, 0, 0 }, { 0, 0, 1 }, { 0, 0, 2 }, { 1, 0, 0 }, { 1, 0, 1 }, { 1, 0, 1 }, { 1, 0, 2 }, { 2, 0, 0 }, { 2, 0, 1 }, { 2, 0, 2 }, { 2, 0, 2 }, { 0, 1, 0 }, { 0, 1, 1 }, { 0, 1, 2 }, { 0, 1, 2 }, { 1, 1, 0 } };
            var r = Math.Min(i + k[j, 0], 255);
            var g = Math.Min(i + k[j, 1], 255);
            var b = Math.Min(i + k[j, 2], 255);

            return new Color24 { r = (byte)r, g = (byte)g, b = (byte)b };
        }
        public static Color48 PsuedoGreyPlus48(int x)
        {
            if (x < 1) return new Color48 { r = 0, g = 0, b = 0 };
            if (x > 1048559) return new Color48 { r = 65535, g = 65535, b = 65535 };

            var i = x / 16;
            var j = x % 16;

            var k = new byte[16, 3] { { 0, 0, 0 }, { 0, 0, 1 }, { 0, 0, 2 }, { 1, 0, 0 }, { 1, 0, 1 }, { 1, 0, 1 }, { 1, 0, 2 }, { 2, 0, 0 }, { 2, 0, 1 }, { 2, 0, 2 }, { 2, 0, 2 }, { 0, 1, 0 }, { 0, 1, 1 }, { 0, 1, 2 }, { 0, 1, 2 }, { 1, 1, 0 } };
            var r = Math.Min(i + k[j, 0], 65535);
            var g = Math.Min(i + k[j, 1], 65535);
            var b = Math.Min(i + k[j, 2], 65535);

            return new Color48 { r = (ushort)r, g = (ushort)g, b = (ushort)b };
        }
        public static Color24[,] bytesToPixels24(byte[] bytes, int width, int height)
        {
            try
            {
                var byteCount = width * height * 3;
                var pixels = new Color24[width, height];
                var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
                var pointer = handle.AddrOfPinnedObject();
                Marshal.Copy(bytes, 0, pointer, byteCount);
                handle.Free();

                return pixels;
            }
            catch (System.Exception) { }
            return null;
        }
        public static Color32[,] bytesToPixels(byte[] bytes, int width, int height)
        {
            try
            {
                var byteCount = width * height * 4;
                var pixels = new Color32[width, height];
                var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
                var pointer = handle.AddrOfPinnedObject();
                Marshal.Copy(bytes, 0, pointer, byteCount);
                handle.Free();

                return pixels;
            }
            catch (System.Exception) { }
            return null;
        }
        public static Color48[,] bytesToPixels48(byte[] bytes, int width, int height)
        {
            try
            {
                var byteCount = width * height * 6;
                var pixels = new Color48[width, height];
                var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
                var pointer = handle.AddrOfPinnedObject();
                Marshal.Copy(bytes, 0, pointer, byteCount);
                handle.Free();

                return pixels;
            }
            catch (System.Exception) { }
            return null;
        }
        public static Bitmap pixelsToBitmap(byte[,] pixels)
        {
            try
            {
                var width = pixels.GetLength(1);
                var height = pixels.GetLength(0);
                var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
                var bitmap = new Bitmap(width, height, width, PixelFormat.Format8bppIndexed, handle.AddrOfPinnedObject());

                return bitmap;
            }
            catch (System.Exception) { }
            return null;
        }
        public static Bitmap pixelsToBitmap(Color24[,] pixels)
        {
            try
            {
                var width = pixels.GetLength(1);
                var height = pixels.GetLength(0);
                var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
                var bitmap = new Bitmap(width, height, width * 3, PixelFormat.Format24bppRgb, handle.AddrOfPinnedObject());

                return bitmap;
            }
            catch (System.Exception) { }
            return null;
        }
        public static Bitmap pixelsToBitmap(Color32[,] pixels)
        {
            try
            {
                var width = pixels.GetLength(1);
                var height = pixels.GetLength(0);
                var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
                var bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, handle.AddrOfPinnedObject());

                return bitmap;
            }
            catch (System.Exception) { }
            return null;
        }
        public static Bitmap pixelsToBitmap(Color48[,] pixels)
        {
            try
            {
                var width = pixels.GetLength(1);
                var height = pixels.GetLength(0);
                var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
                var bitmap = new Bitmap(width, height, width * 6, PixelFormat.Format48bppRgb, handle.AddrOfPinnedObject());

                return bitmap;
            }
            catch (System.Exception e) { }
            return null;
        }
        public static Bitmap pixelsToBitmap(Color64[,] pixels)
        {
            try
            {
                var width = pixels.GetLength(1);
                var height = pixels.GetLength(0);
                var handle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
                var bitmap = new Bitmap(width, height, width * 8, PixelFormat.Format64bppArgb, handle.AddrOfPinnedObject());

                return bitmap;
            }
            catch (System.Exception e) { }
            return null;
        }
        public static Bitmap bitsToBitmap(byte[] bytes, int width, int height)
        {
            try
            {
                var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                var bitmap = new Bitmap(width, height, width / 8, PixelFormat.Format1bppIndexed, handle.AddrOfPinnedObject());

                return bitmap;
            }
            catch (System.Exception e) { }
            return null;
        }
        public static Bitmap bytesToBitmap24(byte[] bytes, int width, int height)
        {
            try
            {
                var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                var bitmap = new Bitmap(width, height, width * 3, PixelFormat.Format24bppRgb, handle.AddrOfPinnedObject());

                return bitmap;
            }
            catch (System.Exception e) { }
            return null;
        }
        public static Bitmap bytesToBitmap4bit(byte[] bytes, int width, int height)
        {
            try
            {
                var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                var bitmap = new Bitmap(width, height, width / 2, PixelFormat.Format4bppIndexed, handle.AddrOfPinnedObject());

                return bitmap;
            }
            catch (System.Exception e) { }
            return null;
        }
        public static byte[] randomBytes(int byteCount)
        {
            try
            {
                var bytes = new byte[byteCount];
                var random = new Random();
                random.NextBytes(bytes);

                return bytes;
            }
            catch (System.Exception) { }
            return null;
        }
        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            try
            {
                var codecs = ImageCodecInfo.GetImageDecoders();
                foreach (var codec in codecs)
                    if (codec.FormatID == format.Guid)
                        return codec;
            }
            catch (System.Exception) { }
            return null;
        }
        public static void saveJpeg(Bitmap bitmap, int quality, string fileName)
        {
            try
            {
                var jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                var myEncoder = Encoder.Quality;
                var myEncoderParameters = new EncoderParameters(1);

                myEncoderParameters.Param[0] = new EncoderParameter(myEncoder, quality);
                bitmap.Save(fileName, jpgEncoder, myEncoderParameters);
            }
            catch (System.Exception) { }
        }
        public static Color48[,] Scale16(Color24[,] sourcePixels)
        {
            var sourceWidth = sourcePixels.GetLength(1);
            var sourceHeight = sourcePixels.GetLength(0);

            var destinationWidth = (int)Math.Floor(sourceWidth / 16.0);
            var destinationHeight = (int)Math.Floor(sourceHeight / 16.0);

            var destinationPixels = new Color48[destinationHeight, destinationWidth];

            Parallel.For(0, destinationWidth, i =>
            {
                var ii = i * 16;
                Parallel.For(0, destinationHeight, j =>
                {
                    var sum = new int[4];
                    var jj = j * 16;
                    for (var x = 0; x < 16; x++)
                    {
                        var iii = ii + x;
                        for (var y = 0; y < 16; y++)
                        {
                            var jjj = jj + y;
                            sum[0] += sourcePixels[jjj, iii].r;
                            sum[1] += sourcePixels[jjj, iii].g;
                            sum[2] += sourcePixels[jjj, iii].b;
                        }
                    }
                    destinationPixels[j, i].r = (ushort)(257 * sum[0] / 256);
                    destinationPixels[j, i].g = (ushort)(257 * sum[1] / 256);
                    destinationPixels[j, i].b = (ushort)(257 * sum[2] / 256);
                });
            });


            return destinationPixels;
        }
        public static Color64[,] Scale16(Color32[,] sourcePixels)
        {
            var sourceWidth = sourcePixels.GetLength(1);
            var sourceHeight = sourcePixels.GetLength(0);

            var destinationWidth = (int)Math.Floor(sourceWidth / 16.0);
            var destinationHeight = (int)Math.Floor(sourceHeight / 16.0);

            var destinationPixels = new Color64[destinationHeight, destinationWidth];

            Parallel.For(0, destinationWidth, i =>
            {
                var ii = i * 16;
                Parallel.For(0, destinationHeight, j =>
                {
                    var sum = new int[4];
                    var jj = j * 16;
                    for (var x = 0; x < 16; x++)
                    {
                        var iii = ii + x;
                        for (var y = 0; y < 16; y++)
                        {
                            var jjj = jj + y;
                            sum[0] += sourcePixels[jjj, iii].r;
                            sum[1] += sourcePixels[jjj, iii].g;
                            sum[2] += sourcePixels[jjj, iii].b;
                            sum[3] += sourcePixels[jjj, iii].a;
                        }
                    }
                    destinationPixels[j, i].r = (ushort)(257 * sum[0] / 256);
                    destinationPixels[j, i].g = (ushort)(257 * sum[1] / 256);
                    destinationPixels[j, i].b = (ushort)(257 * sum[2] / 256);
                    destinationPixels[j, i].a = (ushort)(257 * sum[3] / 256);
                });
            });


            return destinationPixels;
        }
        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {

            BitmapData bmpdata = null;

            try
            {
                bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                int numbytes = bmpdata.Stride * bitmap.Height;
                byte[] bytedata = new byte[numbytes];
                IntPtr ptr = bmpdata.Scan0;

                Marshal.Copy(ptr, bytedata, 0, numbytes);

                return bytedata;
            }
            finally
            {
                if (bmpdata != null)
                    bitmap.UnlockBits(bmpdata);
            }
        }

        // scale 1/ratio
        public static int[,] Scale(int[,] source, byte ratio)
        {
            var sourceWidth = source.GetLength(1);
            var sourceHeight = source.GetLength(0);

            var destinationWidth = sourceWidth / ratio;
            var destinationHeight = sourceHeight / ratio;

            var destination = new int[destinationHeight, destinationWidth];

            Parallel.For(0, destinationWidth, i =>
            {
                var ii = i * ratio;
                Parallel.For(0, destinationHeight, j =>
                {
                    var sum = 0L;
                    var jj = j * ratio;
                    for (var x = 0; x < ratio; x++)
                    {
                        var iii = ii + x;
                        for (var y = 0; y < ratio; y++)
                        {
                            var jjj = jj + y;
                            sum += source[jjj, iii];
                        }
                    }
                    destination[j, i] = (int)(sum / (ratio * ratio));
                });
            });

            return destination;
        }
        public static void SimpleDither(int[,] target, Func<int, int> lambda)
        {
            var width = target.GetLength(1);
            var height = target.GetLength(0);

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    var c = lambda(target[i, j]);
                    var d = target[i, j] - c;
                    target[i, j] = c;

                    if (j + 1 < width)
                    {
                        target[i, j + 1] += d / 2;
                    }
                    if (i + 1 < height)
                    {
                        target[i + 1, j] += d / 2;
                    }
                }
        }
        public static void FloydSteinberg(int[,] target, Func<int, int> lambda)
        {
            var width = target.GetLength(1);
            var height = target.GetLength(0);

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    var c = lambda(target[i, j]);
                    var d = target[i, j] - c;
                    target[i, j] = c;

                    if (j + 1 < width)
                    {
                        target[i, j + 1] += d * 7 / 16;
                        if (i + 1 < height)
                            target[i + 1, j + 1] += d * 1 / 16;
                    }
                    if (i + 1 < height)
                    {
                        target[i + 1, j] += d * 5 / 16;
                        if (j > 0)
                            target[i + 1, j - 1] += d * 3 / 16;
                    }
                }
        }
        public static void MinimizedAverageError(int[,] target, Func<int, int> lambda)
        {
            var width = target.GetLength(1);
            var height = target.GetLength(0);

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    var c = lambda(target[i, j]);
                    var d = target[i, j] - c;
                    target[i, j] = c;

                    if (j + 1 < width)
                    {
                        target[i, j + 1] += d * 7 / 48;
                        if (i + 1 < height)
                            target[i + 1, j + 1] += d * 5 / 48;
                        if (i + 2 < height)
                            target[i + 2, j + 1] += d * 3 / 48;
                    }
                    if (j + 2 < width)
                    {
                        target[i, j + 2] += d * 5 / 48;
                        if (i + 1 < height)
                            target[i + 1, j + 2] += d * 3 / 48;
                        if (i + 2 < height)
                            target[i + 2, j + 2] += d * 1 / 48;
                    }
                    if (i + 1 < height)
                    {
                        target[i + 1, j] += d * 7 / 48;
                        if (j > 0)
                            target[i + 1, j - 1] += d * 5 / 48;
                        if (j > 1)
                            target[i + 1, j - 2] += d * 3 / 48;
                    }
                    if (i + 2 < height)
                    {
                        target[i + 2, j] += d * 5 / 48;
                        if (j > 0)
                            target[i + 2, j - 1] += d * 3 / 48;
                        if (j > 1)
                            target[i + 2, j - 2] += d * 1 / 48;
                    }
                }
        }
        public static byte[] ConvertPaletted(Color24[,] pixels)
        {
            var width = pixels.GetLength(1);
            var height = pixels.GetLength(0);

            var result = new byte[width * height];

            int k = 0;
            for (int i = 0; i < height; i++)
                for (int j = 0; j < height; j++)
                    result[k++] = imageGenerate.Palette666(pixels[i, j].r, pixels[i, j].g, pixels[i, j].b);

            return result;
        }

        public static void SiyahBeyazTest()
        {
            var m1 = new int[2, 2] { { 0, 2 }, { 3, 1 } };
            var m2 = BayerMatrix(m1);
            var m4 = BayerMatrix(m2);
            var m8 = BayerMatrix(m4);

            var p1 = PreCalculatedBayerMatrix(m1);
            var p2 = PreCalculatedBayerMatrix(m2);
            var p4 = PreCalculatedBayerMatrix(m4);
            var p8 = PreCalculatedBayerMatrix(m8);

            var a = new byte[128 * 128];
            var palette = new byte[6] { 0, 0, 0, 255, 255, 255 };

            var k = 0;
            for (int i = 0; i < 128; i++)
                for (int j = 0; j < 128; j++)
                {
                    var r = Math.Clamp(1 + i + j + 256 * p8[i % 16, j % 16], 0, 255);
                    a[k++] = (byte)(r > 127.5 ? 1 : 0);
                }

            using (var gif = new AnimatedGif("8.gif", 128, 128, palette, 2, 1))
            {
                gif.AddFrame(a);
            }
        }
        public static void RenkliTest()
        {
            var m1 = new int[2, 2] { { 0, 2 }, { 3, 1 } };
            var m2 = BayerMatrix(m1);
            var m4 = BayerMatrix(m2);
            var m8 = BayerMatrix(m4);

            var p1 = PreCalculatedBayerMatrix(m1);
            var p2 = PreCalculatedBayerMatrix(m2);
            var p4 = PreCalculatedBayerMatrix(m4);
            var p8 = PreCalculatedBayerMatrix(m8);

            var a = new byte[256 * 256];
            var palette = imageGenerate.Palette666();

            for (int f = 0; f < 256; f++)
            {
                var k = 0;
                for (int i = 0; i < 256; i++)
                    for (int j = 0; j < 256; j++)
                    {
                        var r = (byte)i;
                        var g = (byte)j;
                        var b = (byte)0;
                        if (i + j > 255)
                            b = (byte)(511 - i - j);
                        else
                            b = (byte)(i + j);

                        r = (byte)Math.Clamp(r + f * p8[i % 16, j % 16], 0, 255);
                        g = (byte)Math.Clamp(g + f * p8[i % 16, j % 16], 0, 255);
                        b = (byte)Math.Clamp(b + f * p8[i % 16, j % 16], 0, 255);

                        a[k++] = imageGenerate.Palette666(r, g, b);
                    }


                using (var gif = new AnimatedGif(f.ToString("D3") + ".gif", 256, 256, palette, 2, 8))
                {
                    gif.AddFrame(a);
                }
            }
        }
        static int[,] BayerMatrix(int[,] m)
        {
            var n = m.GetLength(0);
            var nm = new int[n * 2, n * 2];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    nm[i, j] = 4 * m[i, j];
                    nm[i, j + n] = 4 * m[i, j] + 2;
                    nm[i + n, j] = 4 * m[i, j] + 3;
                    nm[i + n, j + n] = 4 * m[i, j] + 1;
                }
            return nm;
        }
        static double[,] PreCalculatedBayerMatrix(int[,] m)
        {
            var n = m.GetLength(0);
            double nn = n * n;
            var p = new double[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    p[i, j] = m[i, j] / nn - 0.5;

            return p;
        }

    }
}
