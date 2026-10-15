// Türkiye bayrağı: antialiasingli dairesel ay ve yıldız.
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static Color24[,] FlagTurkiye(int yukseklik)
    {

        var genislik = 3 * yukseklik / 2;

        var n = 8;
        var Y = yukseklik * n;
        var A = Y / 2;
        var B = Y / 4;
        var C = Y / 16;
        var D = Y / 5;
        var E = Y / 3;
        var F = Y / 8;
        var L = 3 * Y / 2;
        var YILDIZ = 33 * Y / 40;

        var alan = new int[yukseklik, genislik];
        for (int i = 0; i < Y; i++)
            for (int j = 0; j < L; j++)
            {
                if (Math.Sqrt((i - A) * (i - A) + (j - A) * (j - A)) < B && Math.Sqrt((i - A) * (i - A) + (j - A - C) * (j - A - C)) > D)
                    alan[i / n, j / n] += 1;

                if (Math.Sqrt((i - A) * (i - A) + (j - YILDIZ) * (j - YILDIZ)) < F && Math.Sqrt((i - A) * (i - A) + (j - YILDIZ) * (j - YILDIZ)) > 0.4 * F)
                    alan[i / n, j / n] += 1;

            }

        var pixels = new Color24[yukseklik, genislik];

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
}
