using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace ComputerGraphicsAlgorithms
{
    public static class imageGenerate
    {
        public static common.Color24[,] EPowerXSquare(int width)
        {
            int height = 9 * width / 16;
            var pixels = new common.Color24[height, width];

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
        public static common.Color24[,] FlagTurkiye(int yukseklik)
        {
            //Y	Yükseklik 1
            //A	Dış ay merkezinin uçkurluktan mesafesi	1/2 Y
            //B	Ayın dış dairesinin kutru (çapı)	1/2 Y
            //C	Ayın iç ve dış merkezleri arası	1/16 Y
            //D	Ayın iç dairesinin kutru (çapı)	0.4 Y
            //E	Yıldız dairesinin, ayın iç dairesinden olan mesafesi	1/3 Y
            //F	Yıldız dairesinin kutru (çapı)	1/4 Y
            //L	Genişlik 1.5 Y

            var genislik = 3 * yukseklik / 2;

            var n = 2; //super resolution katsayısı
            var Y = yukseklik * n;
            var A = Y / 2;
            var B = Y / 4;
            var C = Y / 16;
            var D = Y / 5;
            var E = Y / 3;
            var F = Y / 8;
            var L = 3 * Y / 2;
            var YILDIZ = 33 * Y / 40;


            var rnd = new Random();
            var alan = new int[yukseklik, genislik];
            for (int i = 0; i < Y; i++)
                for (int j = 0; j < L; j++)
                {
                    if (Math.Sqrt((i - A) * (i - A) + (j - A) * (j - A)) < B && Math.Sqrt((i - A) * (i - A) + (j - A - C) * (j - A - C)) > D)
                        alan[i / n, j / n] += 1;

                    if (Math.Sqrt((i - A) * (i - A) + (j - YILDIZ) * (j - YILDIZ)) < F && Math.Sqrt((i - A) * (i - A) + (j - YILDIZ) * (j - YILDIZ)) > 0.4 * F)
                        alan[i / n, j / n] += 1;

                }

            var pixels = new common.Color24[yukseklik, genislik];

            for (int i = 0; i < yukseklik; i++)
            {
                for (int j = 0; j < genislik; j++)
                {
                    var oran = 1.0 * alan[i, j] / (n * n);
                    pixels[i, j].r = (byte)(227 * (1.0 - oran) + oran * 255);
                    pixels[i, j].g = (byte)(10 * (1.0 - oran) + oran * 255);
                    pixels[i, j].b = (byte)(23 * (1.0 - oran) + oran * 255);
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

                    pixels[j, i] = common.PsuedoGrey(b);
                }
            });

            return pixels;
        }
        public static common.Color32[,] PerfectBallQuarter2()
        {
            var pixels = new common.Color32[4096, 4096];
            Parallel.For(0, 4096, i =>
            {
                for (var j = 0; j < 4096; j++)
                {
                    var b = 0;
                    for (var k = 0; k < 4096; k++)
                        if (i * i + j * j + k * k < 16638241)
                            b++;

                    pixels[j, i] = common.PsuedoGreyPlus(b);
                }
            });

            return pixels;
        }
        public static common.Color48[,] XSquarePlusYSquare()
        {
            var pixels = new common.Color48[10240, 10240];

            Parallel.For(0, 5120, i =>
            {
                for (var j = 0; j < 5120; j++)
                {
                    var a = (i + 1) * (i + 1) + (j + 1) * (j + 1);
                    pixels[5119-j, 5119-i] = common.PsuedoGreyPlus48(a / 50);
                }
            });

            for (var i = 0; i < 5120; i++)
                for (var j = 0; j < 5120; j++)
                {
                    pixels[10239 - j, i] = pixels[j, i];
                    pixels[10239 - j, 10239 - i] = pixels[j, i];
                    pixels[j, 10239 - i] = pixels[j, i];
                }

            return pixels;
        }
        public static common.Color48[,] XMultiplyY()
        {
            var pixels = new common.Color48[10240, 10240];

            Parallel.For(0, 5120, i =>
            {
                for (var j = 0; j < 5120; j++)
                    pixels[j, i] = common.PsuedoGreyPlus48(((i + 1) * (j + 1) - 1) / 25);
            });

            for (var i = 0; i < 5120; i++)
                for (var j = 0; j < 5120; j++)
                {
                    pixels[10239 - j, i] = pixels[j, i];
                    pixels[10239 - j, 10239 - i] = pixels[j, i];
                    pixels[j, 10239 - i] = pixels[j, i];
                }

            return pixels;
        }
        public static common.Color48[,] Arctangent()
        {
            var pixels = new common.Color48[10240, 10240];

            Parallel.For(0, 10240, i =>
            {
                for (var j = 0; j < 10240; j++)
                    pixels[j, i] = common.PsuedoGreyPlus48(((i + 1) * (j + 1) - 1) / 25);
            });

            return pixels;
        }

    }
}
