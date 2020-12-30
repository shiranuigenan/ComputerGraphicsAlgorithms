using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerGraphicsAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new int[9, 9]={ };
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    a[i, j] = 1000;

            common.MinimizedAverageError(a, x => (x / 8388608) * 16777215);

            //var bmp = new Bitmap("0.png");
            //var c = new channelBasedImage(bmp);
            //c.Scale(16);

            //var b0 = c.convertBitmap();
            //b0.Save("scaled.png", ImageFormat.Png);

            //common.MinimizedAverageError(c.r, x => (x / 8388608) * 16777215);
            //common.MinimizedAverageError(c.g, x => (x / 8388608) * 16777215);
            //common.MinimizedAverageError(c.b, x => (x / 8388608) * 16777215);

            //var b1 = c.convertBitmap24();
            //b1.Save("MinimizedAverageError.png", ImageFormat.Png);
        }
    }
}
