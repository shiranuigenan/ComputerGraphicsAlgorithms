// PerfectBall: yarıçap boyunca ışın sayımına dayalı küre renderı ve SVG gradient çıktıları.
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static Color24[,] PerfectBallQuarter()
    {
        var p = Enumerable.Range(0, 4081).Select(x => PseudoGreyPlus24(x)).ToArray();

        var pixels = new Color24[4080, 4080];
        Parallel.For(0, 4080, i =>
        {
            for (var j = 0; j < 4080; j++)
            {
                var b = 0;
                for (var k = 0; k < 4080; k++)
                    if (i * i + j * j + k * k < 16638241)
                        b++;

                pixels[j, i] = p[b];
            }
        });

        return pixels;
    }
    public static Color24[,] PerfectBall()
    {
        var pixels = new Color24[8192, 8192];

        var quarter = PerfectBallQuarter();

        for (int i = 0; i < 4080; i++)
            for (int j = 0; j < 4080; j++)
            {
                var c = quarter[i, j];
                pixels[4095 - i, 4095 - j].r = c.r;
                pixels[4095 - i, 4095 - j].g = c.g;
                pixels[4095 - i, 4095 - j].b = c.b;

                pixels[4095 - i, 4096 + j].r = c.r;
                pixels[4095 - i, 4096 + j].g = c.g;
                pixels[4095 - i, 4096 + j].b = c.b;

                pixels[4096 + i, 4095 - j].r = c.r;
                pixels[4096 + i, 4095 - j].g = c.g;
                pixels[4096 + i, 4095 - j].b = c.b;

                pixels[4096 + i, 4096 + j].r = c.r;
                pixels[4096 + i, 4096 + j].g = c.g;
                pixels[4096 + i, 4096 + j].b = c.b;

            }

        return pixels;
    }
    public static void PerfectBallQuarterVector()
    {
        var bb = -1;
        for (var j = 0; j < 4081; j++)
        {
            var b = 0;
            for (var k = 0; k < 4081; k++)
                if (j * j + k * k < 16646400)
                    b++;
            if (bb != b)
            {
                bb = b;
                var c = PseudoGreyPlus(b);
                Console.WriteLine(string.Format("<stop offset=\"{0:0.00}%\" style=\"stop-color:rgb({1},{2},{3})\" />", 100.0 * j / 4080.0, c.r, c.g, c.b));
            }
        }
    }
    public static Color32[,] PerfectBallQuarter2()
    {
        var pixels = new Color32[4096, 4096];
        Parallel.For(0, 4096, i =>
        {
            for (var j = 0; j < 4096; j++)
            {
                var b = 0;
                for (var k = 0; k < 4096; k++)
                    if (i * i + j * j + k * k < 16638241)
                        b++;

                pixels[j, i] = PseudoGreyPlus(b);
            }
        });

        return pixels;
    }
}
