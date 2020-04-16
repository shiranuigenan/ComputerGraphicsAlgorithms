using System;
using System.Drawing.Imaging;

namespace ComputerGraphicsAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var pixels = imageGenerate.Gradient32();
            var pixels64 = imageGenerate.Gradient64();

            var scaled = common.Scale16(pixels);
            var scaledBitmap = common.pixelsToBitmap(scaled);
            scaledBitmap.Save("scaled.png", ImageFormat.Png);
        }
    }
}
