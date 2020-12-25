using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerGraphicsAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            dithering.FloydSteinberg();
            return;
            var bmp = new Bitmap("1.png");

            // var bmp2 = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);
            // using (Graphics gr = Graphics.FromImage(bmp2))
            // {
            //     gr.DrawImage(bmp, new Rectangle(0, 0, bmp2.Width, bmp2.Height));
            // }
            // bmp2.Save("1.png", ImageFormat.Png);

            var byt = common.BitmapToByteArray(bmp);
            var pixels = common.bytesToPixels(byt, bmp.Height, bmp.Width);

            var scaled = common.Scale16(pixels);
            var scaledBitmap = common.pixelsToBitmap(scaled);
            scaledBitmap.Save("scaled.png", ImageFormat.Png);
        }
    }
}
