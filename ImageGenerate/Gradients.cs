// Çözünürlük ve alt piksel düzenine bağlı gradyanlar, Viewfinity S9 desenleri ve rasgele gradyanlar.
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static Color24[,] W2560H1440()
    {
        var pixels = new Color24[1440, 2560];
        Parallel.For(0, 2560, i =>
        {
            for (var j = 0; j < 1440; j++)
            {
                var k = (byte)((9 * i + 16 * j) >> 9);
                pixels[j, i].r = k;
                pixels[j, i].g = k;
                pixels[j, i].b = k;
            }
        });

        return pixels;
    }
    public static void RealGradient()
    {
        int w = 34 * 2560, h = 34 * 1440;

        var a = new int[h, w];

        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a[i, j] = (i * 16 + j * 9) << 1;

        MinimizedAverageError(a, x => x > 8388608 ? 16777215 : 0);
        var a2 = Scale(a, 34);

        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a[i, j] = (i * 16 + j * 9) << 2;

        MinimizedAverageError(a, x => x > 8388608 ? 16777215 : 0);
        var a3 = Scale(a, 34);

        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a[i, j] = (i * 16 + j * 9) << 3;

        MinimizedAverageError(a, x => x > 8388608 ? 16777215 : 0);
        var a4 = Scale(a, 34);

        var c = new ChannelBasedImage96(w / 34, h / 34);

        c.r = a2; c.g = a3; c.b = a4; var bgr = c.ConvertBitmap(); bgr.Save("bgr.pm");
        c.r = a2; c.g = a4; c.b = a3; var gbr = c.ConvertBitmap(); gbr.Save("gbr.pm");
        c.r = a3; c.g = a2; c.b = a4; var brg = c.ConvertBitmap(); brg.Save("brg.pm");
        c.r = a3; c.g = a4; c.b = a2; var grb = c.ConvertBitmap(); grb.Save("grb.pm");
        c.r = a4; c.g = a2; c.b = a3; var rbg = c.ConvertBitmap(); rbg.Save("rbg.pm");
        c.r = a4; c.g = a3; c.b = a2; var rgb = c.ConvertBitmap(); rgb.Save("rgb.pm");
    }
    public static void Gigapixel17()
    {
        int w = 10922 * 16, h = 10922 * 9;
        var a = new int[h, w];

        for (int i = 0; i < h; i++)
        {
            if (i % 9830 == 0)
                Console.WriteLine(i / 9830);

            for (int j = 0; j < w; j++)
                a[i, j] += i * 16 + j * 9;
        }

        var max = a[h - 1, w - 1];

        MinimizedAverageError(a, x => x > (max >> 1) ? max : 0);
        var w8 = w / 8;
        var b = new byte[h * w8];
        int k = 0, p = 0;
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
            {
                if (a[i, j] != 0)
                    b[k] += (byte)(1 << (7 - p));
                p++;
                if (p == 8)
                {
                    p = 0;
                    k++;
                }
            }

        var bitmap = BitsToBitmap(b, w, h);
        bitmap.Save("3.pbm");
    }
    public static Color24[,] ViewfinityS9()
    {
        var p = Enumerable.Range(0, 4081).Select(x => PseudoGreyPlus24(x)).ToArray();
        var w = 5120;
        var h = 2880;
        var n = 8;
        var a = new short[h * n, w * n];

        for (int j = 0; j < h * n; j++)
            for (int i = 0; i < w * n; i++)
                a[j, i] = (short)((j * 16 + i * 9) / 90.34);

        var pixels = new Color24[h, w];

        for (int j = 0; j < h; j++)
        {
            var jj = j * 8;
            for (int i = 0; i < w; i++)
            {
                var ii = i * 8;
                int r = 0, g = 0, b = 0;
                for (int x = 0; x < 8; x++)
                    for (int y = 0; y < 8; y++)
                    {
                        var k = a[jj + x, ii + y] % 4081;
                        r += p[k].r;
                        g += p[k].g;
                        b += p[k].b;
                    }
                pixels[j, i].r = (byte)(r / 64);
                pixels[j, i].g = (byte)(g / 64);
                pixels[j, i].b = (byte)(b / 64);
            }
        }
        return pixels;
    }
    public static Color24[,] ViewfinityS9Simple()
    {
        var n = 13;
        var w = 5120;
        var h = 2880;
        var a = new int[h, w];

        var c = 1 + ((h * n - 1) * 16 + (w * n - 1) * 9) / 2;
        var d = c / 4;

        for (int j = 0; j < h * n; j++)
            for (int i = 0; i < w * n; i++)
                a[j / n, i / n] += ((j * 16 + i * 9) / d) % 4;

        var pixels = new Color24[h, w];

        for (int j = 0; j < h; j++)
        {
            for (int i = 0; i < w; i++)
            {
                var k = (byte)(1 + (a[j, i] >> 1));

                pixels[j, i].r = k;
                pixels[j, i].g = k;
                pixels[j, i].b = k;
            }
        }
        return pixels;
    }
    public static Color24[,] ViewfinityS9Simple2()
    {
        var p = Enumerable.Range(0, 4081).Select(x => PseudoGreyPlus24(x)).ToArray();

        var w = 5120;
        var h = 2880;

        var pixels = new Color24[h, w];

        for (int j = 0; j < h; j++)
        {
            for (int i = 0; i < w; i++)
            {
                var a = (j * 16 + i * 9) / 25;

                pixels[j, i].r = p[a].r;
                pixels[j, i].g = p[a].g;
                pixels[j, i].b = p[a].b;
            }
        }
        return pixels;
    }
    public static Color24[,]? RandomGradient24(int n = 26752)
    {
        var r = new Random();
        var m = n / 256.0;
        var k = 0;
        var a = new byte[n * n * 3];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                var p = i + j;
                if (p < n)
                {
                    a[k++] = (byte)r.Next((int)(p / m));
                    a[k++] = (byte)r.Next((int)(p / m));
                    a[k++] = (byte)r.Next((int)(p / m));
                }
                else
                {
                    a[k++] = (byte)r.Next((int)((p - n) / m), 256);
                    a[k++] = (byte)r.Next((int)((p - n) / m), 256);
                    a[k++] = (byte)r.Next((int)((p - n) / m), 256);
                }
            }

        return BytesToPixels24(a, n, n);
    }
    public static Color48[,] RandomGradient48(int n = 12800)
    {
        var r = new Random();
        var m = n / 65536.0;
        var a = new Color48[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                var p = i + j;
                if (p < n)
                {
                    a[i, j].r = (ushort)r.Next((int)(p / m));
                    a[i, j].g = (ushort)r.Next((int)(p / m));
                    a[i, j].b = (ushort)r.Next((int)(p / m));
                }
                else
                {
                    a[i, j].r = (ushort)r.Next((int)((p - n) / m), 65536);
                    a[i, j].g = (ushort)r.Next((int)((p - n) / m), 65536);
                    a[i, j].b = (ushort)r.Next((int)((p - n) / m), 65536);
                }
            }

        return a;
    }
    public static Color24[,] Hat()
    {
        var p = Enumerable.Range(0, 4081).Select(x => PseudoGreyPlus24(x)).ToArray();

        var w = 5248;
        var h = 2880;

        var pixels = new Color24[h, w];

        var k = 0;
        for (int i = 0; i < 100; i++)
        {
            var a = 4079 / (i + 1.0);
            var b = 1.0;
            for (int j = 0; j < i + 2; j++)
            {
                pixels[i, k].r = p[(int)b].r;
                pixels[i, k].g = p[(int)b].g;
                pixels[i, k].b = p[(int)b].b;
                k++;
                b += a;
            }
        }

        return pixels;
    }
}
