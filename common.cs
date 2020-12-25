using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ComputerGraphicsAlgorithms
{
    public static class common
    {
        public struct Color32
        {
            public byte b;
            public byte g;
            public byte r;
            public byte a;
        }
        public struct Color64
        {
            public ushort b;
            public ushort g;
            public ushort r;
            public ushort a;
        }
        public static readonly Color32[] ExtendedGrayTones = new Color32[1786];
        static common()
        {
            for (int i = 0; i < 255; i++)
            {
                ExtendedGrayTones[7 * i + 0].r = (byte)(i + 0); ExtendedGrayTones[7 * i + 0].g = (byte)(i + 0); ExtendedGrayTones[7 * i + 0].b = (byte)(i + 0);
                ExtendedGrayTones[7 * i + 1].r = (byte)(i + 0); ExtendedGrayTones[7 * i + 1].g = (byte)(i + 0); ExtendedGrayTones[7 * i + 1].b = (byte)(i + 1);
                ExtendedGrayTones[7 * i + 2].r = (byte)(i + 1); ExtendedGrayTones[7 * i + 2].g = (byte)(i + 0); ExtendedGrayTones[7 * i + 2].b = (byte)(i + 0);
                ExtendedGrayTones[7 * i + 3].r = (byte)(i + 1); ExtendedGrayTones[7 * i + 3].g = (byte)(i + 0); ExtendedGrayTones[7 * i + 3].b = (byte)(i + 1);
                ExtendedGrayTones[7 * i + 4].r = (byte)(i + 0); ExtendedGrayTones[7 * i + 4].g = (byte)(i + 1); ExtendedGrayTones[7 * i + 4].b = (byte)(i + 0);
                ExtendedGrayTones[7 * i + 5].r = (byte)(i + 0); ExtendedGrayTones[7 * i + 5].g = (byte)(i + 1); ExtendedGrayTones[7 * i + 5].b = (byte)(i + 1);
                ExtendedGrayTones[7 * i + 6].r = (byte)(i + 1); ExtendedGrayTones[7 * i + 6].g = (byte)(i + 1); ExtendedGrayTones[7 * i + 6].b = (byte)(i + 0);
            }
            ExtendedGrayTones[1785].r = 255; ExtendedGrayTones[1785].g = 255; ExtendedGrayTones[1785].b = 255;
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
    }
}
