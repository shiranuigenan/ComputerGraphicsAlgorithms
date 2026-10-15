// Matematiksel formüllerden (üssü, çarpım, mod, arktanjant, üstel, zigzag) türetilen desenler.
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static Color24[,] XPowerY_PLUS_YPowerX()
    {
        var p = Enumerable.Range(0, 4081).Select(x => PseudoGreyPlus24(x)).ToArray();
        var w = 7680;
        var h = 4320;

        var pixels = new Color24[h, w];

        for (int j = 0; j < h; j++)
            for (int i = 0; i < w; i++)
                pixels[j, i] = p[4080 - (((9 * i + 16 * j) / 25) % 4081)];

        return pixels;
    }
    public static Color24[,] XPowerY()
    {
        var pixels = new Color24[26752, 26752];

        for (int i = 0; i < 26752; i++)
            for (int j = 0; j < 26752; j++)
            {
                var kij = 0.0;
                kij += Math.Pow((i + 1) / 26753.0, (j + 1) / 26753.0);
                kij += Math.Pow((26752 - i) / 26753.0, (j + 1) / 26753.0);
                kij += Math.Pow((i + 1) / 26753.0, (26752 - j) / 26753.0);
                kij += Math.Pow((26752 - i) / 26753.0, (26752 - j) / 26753.0);

                var kji = 0.0;
                kji += Math.Pow((j + 1) / 26753.0, (i + 1) / 26753.0);
                kji += Math.Pow((26752 - j) / 26753.0, (i + 1) / 26753.0);
                kji += Math.Pow((j + 1) / 26753.0, (26752 - i) / 26753.0);
                kji += Math.Pow((26752 - j) / 26753.0, (26752 - i) / 26753.0);

                var k = (byte)(259 * (kij + kji - 5.0121643839212435));
                pixels[j, i].r = pixels[j, i].g = pixels[j, i].b = k;
            }
        return pixels;
    }
    public static Color24[,] EPowerXSquare(int width)
    {
        int height = 9 * width / 16;
        var pixels = new Color24[height, width];

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
    public static Color48[,] XSquarePlusYSquare()
    {
        var pixels = new Color48[10240, 10240];

        Parallel.For(0, 5120, i =>
        {
            for (var j = 0; j < 5120; j++)
            {
                var a = (i + 1) * (i + 1) + (j + 1) * (j + 1);
                pixels[5119 - j, 5119 - i] = PseudoGreyPlus48(a / 50);
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
    public static Color48[,] XMultiplyY()
    {
        var pixels = new Color48[10240, 10240];

        Parallel.For(0, 5120, i =>
        {
            for (var j = 0; j < 5120; j++)
                pixels[j, i] = PseudoGreyPlus48(((i + 1) * (j + 1) - 1) / 25);
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
    public static void XModPlusYMod()
    {
        var a = new byte[10240 * 10240 * 3];
        var k = 0;
        for (int i = 0; i < 10240; i++)
            for (int j = 0; j < 10240; j++)
            {
                var d = PseudoGreyPlus24(i % 2040 + j % 2040);
                a[k++] = d.r;
                a[k++] = d.g;
                a[k++] = d.b;
            }
        var b = BytesToBitmap24(a, 10240, 10240);
        SaveNetPBM(b, "100.pm");
    }
    public static void XPlusYMod()
    {
        var a = new byte[10240 * 10240 * 3];
        var k = 0;
        for (int i = 0; i < 10240; i++)
            for (int j = 0; j < 10240; j++)
            {
                var d = PseudoGreyPlus24((i + j) % 4080);
                a[k++] = d.r;
                a[k++] = d.g;
                a[k++] = d.b;
            }
        var b = BytesToBitmap24(a, 10240, 10240);
        SaveNetPBM(b, "100.pm");
    }
    public static void PseudoPerspective()
    {
        var a = new byte[25600 * 4096 * 3];
        var k = 0;
        for (int i = 0; i < 4096; i++)
            for (int j = 0; j < 25600; j++)
            {
                var d = PseudoGreyPlus24(((j + 1) % (i + 1)) % 4080);
                a[k++] = d.r;
                a[k++] = d.g;
                a[k++] = d.b;
            }
        var b = BytesToBitmap24(a, 25600, 4096);
        SaveNetPBM(b, "100.pm");
    }
    public static void Arctangent()
    {
        var w = new BinaryWriter(File.Create("a.raw"));

        var a = new byte[16384 * 6];
        for (int i = 0; i < 16384; i++)
        {
            var ii = i - 8191.5;
            Parallel.For(0, 16384, j =>
            {
                var c = (Math.Atan(ii / (j - 8191.5)) + Math.PI / 2) / Math.PI;
                c = 1 - Math.Abs(2 * c - 1);
                c = 1 - Math.Abs(2 * c - 1);
                c = 1 - Math.Abs(2 * c - 1);
                c = 1 - Math.Abs(2 * c - 1);
                c = 1 - Math.Abs(2 * c - 1);
                var d = PseudoGreyPlus48((int)(1048576 * c));
                a[6 * j + 0] = (byte)d.r;
                a[6 * j + 1] = (byte)(d.r >> 8);
                a[6 * j + 2] = (byte)d.g;
                a[6 * j + 3] = (byte)(d.g >> 8);
                a[6 * j + 4] = (byte)d.b;
                a[6 * j + 5] = (byte)(d.b >> 8);
            });
            w.Write(a);
        }
        w.Close();
    }
    public static void Exponential()
    {
        var w = new BinaryWriter(File.Create("a.raw"));
        for (int i = 0; i < 300000; i++)
        {
            var f = Math.Pow(1.00004621098, i);
            var d = PseudoGreyPlus48((int)f);
            w.Write(d.r);
            w.Write(d.g);
            w.Write(d.b);
        }
        w.Close();
    }
    public static void XPowerX()
    {
        var m = 0.6922;
        var n = 15;
        var a = 16384;
        var an = a * n;
        var b = new byte[a, a];
        for (int i = 0; i < an; i++)
        {
            var c = 1.0 * i / an;
            var d = (int)(an * (Math.Pow(c, c) - m));
            for (int j = 0; j < d; j++)
                b[i / n, j / n]++;
        }

        var w = new BinaryWriter(File.Create("a.raw"));
        for (int i = 0; i < a; i++)
            for (int j = 0; j < a; j++)
                w.Write((byte)(17 * b[i, j] / 15));
        w.Close();
    }
    public static void ZigZag()
    {
        var n = 32;

        var zigZag = new List<Tuple<int, int>>[2 * n - 1];
        for (int i = 0; i < zigZag.Length; i++)
            zigZag[i] = new List<Tuple<int, int>>();

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                zigZag[i + j].Add(Tuple.Create(i, j));

        var result = new NetPBM(NetPBMFormat.PM, (n * n + 1) * n, n);
        result.Fill(255, 255, 255);

        var bitmap = new NetPBM(NetPBMFormat.PM, n, n);
        bitmap.Fill(255, 255, 255);

        var t = 0;
        for (int i = 0; i < zigZag.Length; i++)
            for (int j = 0; j < zigZag[i].Count; j++)
            {
                bitmap.SetPixel(n - 1 - zigZag[i][j].Item1, n - 1 - zigZag[i][j].Item2, 0, 0, 0);
                t++;

                if (t % 2 == 1)
                {
                    bitmap.FlipX();
                    result.CopyFrom(bitmap, t * n, 0);
                    bitmap.FlipX();
                }
                else
                    result.CopyFrom(bitmap, t * n, 0);
            }

        result.Save("result.pm");
    }
}
