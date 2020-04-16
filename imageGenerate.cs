using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace ComputerGraphicsAlgorithms
{
    public static class imageGenerate
    {
        public static common.Color32[,] EPowerXSquare(int width)
        {
            int height = 9 * width / 16;
            var pixels = new common.Color32[height, width];

            for (int i = 0; i < width; i++)
            {
                double x = (i - width / 2) / (width / 6.0);
                int y = (int)((width / 6.0) / Math.Pow(Math.E, x * x));

                for (int j = (9 * width / 32) - y; j < (9 * width / 32) + y; j++)
                {
                    pixels[j, i].r = 255;
                    pixels[j, i].g = 255;
                    pixels[j, i].b = 255;
                }
            }

            return pixels;
        }
        public static common.Color32[,] PerfectBallQuarter()
        {
            var pixels = new common.Color32[1786, 1786];
            Parallel.For(0, 1786, i =>
            {
                for (var j = 0; j < 1786; j++)
                {
                    var b = 0;
                    for (var k = 0; k < 1786; k++)
                        if (i * i + j * j + k * k < 3186225)
                            b++;

                    pixels[j, i] = common.ExtendedGrayTones[b];
                }
            });

            return pixels;
        }
        public static common.Color32[,] Gradient32()
        {
            var pixels = new common.Color32[256, 256];

            Parallel.For(0, 256, i =>
            {
                for (var j = 0; j < 256; j++)
                {
                    pixels[j, i].r = pixels[j, i].g = pixels[j, i].b = (byte)(((i + 1) * (j + 1) - 1) >> 8);
                    pixels[j, i].a = 255;
                }
            });

            return pixels;
        }
        public static common.Color64[,] Gradient64()
        {
            var pixels = new common.Color64[256, 256];

            Parallel.For(0, 256, i =>
            {
                for (var j = 0; j < 256; j++)
                {
                    pixels[j, i].r = pixels[j, i].g = pixels[j, i].b = (ushort)((i + 1) * (j + 1) - 1);
                    pixels[j, i].a = 65535;
                }
            });

            return pixels;
        }
    }
}
