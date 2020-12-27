using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerGraphicsAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var bmp = new Bitmap("0.png");
            var c = new channelBasedImage(bmp);
            c.Scale(16);
            
            var b0 = c.convertBitmap();
            b0.Save("scaled.png", ImageFormat.Png);

            common.Dithering(c.r, x => (x / 32768) * 65535);
            //common.Dithering(c.g, x => (x / 21846) * 32767);
            common.Dithering(c.g, x => (x / 32768) * 65535);
            common.Dithering(c.b, x => (x / 32768) * 65535);
            
            var b1 = c.convertBitmap24();
            b1.Save("dithered.png", ImageFormat.Png);
        }
    }
}
