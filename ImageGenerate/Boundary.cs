// Rastgele yığınlamayla oluşan sınır/doku bantları (Boundary ailesi) ve iç içe gradient kenarlık.
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static Color24[,] Boundary4080()
    {
        var p = Enumerable.Range(0, 4081).Select(x => PseudoGreyPlus24(x)).ToArray();

        var w = 30720;
        var h = 17280;

        var r = new Random();

        var pixels = new Color24[h, w];

        Parallel.For(0, w, j =>
        {
            var grey = new short[h];

            for (int i = 0; i < 4080; i++)
            {
                var hh = r.Next(h);
                for (int k = 0; k < hh; k++)
                    grey[k]++;
            }

            for (int n = 0; n < h; n++)
            {
                pixels[n, j].r += p[grey[n]].r;
                pixels[n, j].g += p[grey[n]].g;
                pixels[n, j].b += p[grey[n]].b;
            }
        });



        return pixels;
    }
    public static Color24[,] Boundary(byte a, int w, int h)
    {

        var b = 255 / a;
        var r = new Random();

        var pixels = new Color24[h, w];

        for (int j = 0; j < w; j++)
        {
            for (int i = 0; i < b; i++)
            {
                var hh = r.Next(h);
                for (int k = 0; k < hh; k++)
                {
                    pixels[k, j].r += a;
                    pixels[k, j].g += a;
                    pixels[k, j].b += a;
                }
            }
        }

        return pixels;
    }
    public static Color24[,] BoundaryColored(byte a, int w, int h)
    {

        var b = 255 / a;
        var r = new Random();

        var pixels = new Color24[h, w];

        for (int j = 0; j < w; j++)
        {
            for (int i = 0; i < b; i++)
            {
                var hh = r.Next(h);
                for (int k = 0; k < hh; k++)
                    pixels[k, j].r += a;

                hh = r.Next(h);
                for (int k = 0; k < hh; k++)
                    pixels[k, j].g += a;

                hh = r.Next(h);
                for (int k = 0; k < hh; k++)
                    pixels[k, j].b += a;
            }
        }

        return pixels;
    }
    public static void GradientBorder()
    {
        int a = 46340;
        var b = new byte[a, a];
        for (int i = 0; i < a; i++)
            for (int j = 0; j < a; j++)
                b[i, j] = 255;

        for (int j = 0; j < 256; j++)
        {
            var k = (byte)j;
            for (int i = j; i < a - j; i++)
            {
                b[i, j] = k;
                b[j, i] = k;
                b[i, a - 1 - j] = k;
                b[a - 1 - j, i] = k;
            }
        }

        var c = PixelsToBitmap(b);
        c.Save("1.pgm");
    }
}
