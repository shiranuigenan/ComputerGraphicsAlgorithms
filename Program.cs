using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ComputerGraphicsAlgorithms;
class Program
{
    static void Main(string[] args)
    {
        SiyahBeyazTest();
    }
    static void SiyahBeyazTest()
    {
        var m1 = new int[2, 2] { { 0, 2 }, { 3, 1 } };
        var m2 = BayerMatrix(m1);
        var m4 = BayerMatrix(m2);
        var m8 = BayerMatrix(m4);

        var p1 = PreCalculatedBayerMatrix(m1);
        var p2 = PreCalculatedBayerMatrix(m2);
        var p4 = PreCalculatedBayerMatrix(m4);
        var p8 = PreCalculatedBayerMatrix(m8);

        var a = new byte[128 * 128];
        var palette = new byte[6] { 0, 0, 0, 255, 255, 255 };

        var k = 0;
        for (int i = 0; i < 128; i++)
            for (int j = 0; j < 128; j++)
            {
                var r = Math.Clamp(1 + i + j + 256 * p8[i % 16, j % 16], 0, 255);
                a[k++] = (byte)(r > 127.5 ? 1 : 0);
            }

        using (var gif = new AnimatedGif("8.gif", 128, 128, palette, 2, 1))
        {
            gif.AddFrame(a);
        }
    }
    static void RenkliTest()
    {
        var m1 = new int[2, 2] { { 0, 2 }, { 3, 1 } };
        var m2 = BayerMatrix(m1);
        var m4 = BayerMatrix(m2);
        var m8 = BayerMatrix(m4);

        var p1 = PreCalculatedBayerMatrix(m1);
        var p2 = PreCalculatedBayerMatrix(m2);
        var p4 = PreCalculatedBayerMatrix(m4);
        var p8 = PreCalculatedBayerMatrix(m8);

        var a = new byte[256 * 256];
        var palette = imageGenerate.Palette666();

        for (int f = 0; f < 256; f++)
        {
            var k = 0;
            for (int i = 0; i < 256; i++)
                for (int j = 0; j < 256; j++)
                {
                    var r = (byte)i;
                    var g = (byte)j;
                    var b = (byte)0;
                    if (i + j > 255)
                        b = (byte)(511 - i - j);
                    else
                        b = (byte)(i + j);

                    r = (byte)Math.Clamp(r + f * p8[i % 16, j % 16], 0, 255);
                    g = (byte)Math.Clamp(g + f * p8[i % 16, j % 16], 0, 255);
                    b = (byte)Math.Clamp(b + f * p8[i % 16, j % 16], 0, 255);

                    a[k++] = imageGenerate.Palette666(r, g, b);
                }


            using (var gif = new AnimatedGif(f.ToString("D3") + ".gif", 256, 256, palette, 2, 8))
            {
                gif.AddFrame(a);
            }
        }
    }
    static int[,] BayerMatrix(int[,] m)
    {
        var n = m.GetLength(0);
        var nm = new int[n * 2, n * 2];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                nm[i, j] = 4 * m[i, j];
                nm[i, j + n] = 4 * m[i, j] + 2;
                nm[i + n, j] = 4 * m[i, j] + 3;
                nm[i + n, j + n] = 4 * m[i, j] + 1;
            }
        return nm;
    }
    static double[,] PreCalculatedBayerMatrix(int[,] m)
    {
        var n = m.GetLength(0);
        double nn = n * n;
        var p = new double[n, n];

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                p[i, j] = m[i, j] / nn - 0.5;

        return p;
    }
}