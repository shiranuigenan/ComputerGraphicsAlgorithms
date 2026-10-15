// Pink noise (1/f) piramidiyle tek kare üretimi.
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static void PinkNoise()
    {
        var r = new Random();

        var width = 28468;
        var height = 25145;
        var pixels = new Color24[height, width];

        var w = width;
        var h = height;

        var a00 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a00[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a01 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a01[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a02 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a02[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a03 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a03[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a04 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a04[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a05 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a05[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a06 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a06[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a07 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a07[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a08 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a08[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a09 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a09[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a10 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a10[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a11 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a11[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a12 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a12[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a13 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a13[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a14 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a14[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a15 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a15[i, j] = (byte)(r.Next() % 256);

        var p = Enumerable.Range(0, 4081).Select(x => PseudoGreyPlus24(x)).ToArray();

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                int a = a00[i, j]
                  + a01[i / 2, j / 2]
                  + a02[i / 4, j / 4]
                  + a03[i / 8, j / 8]
                  + a04[i / 16, j / 16]
                  + a05[i / 32, j / 32]
                  + a06[i / 64, j / 64]
                  + a07[i / 128, j / 128]
                  + a08[i / 256, j / 256]
                  + a09[i / 512, j / 512]
                  + a10[i / 1024, j / 1024]
                  + a11[i / 2048, j / 2048]
                  + a12[i / 4096, j / 4096]
                  + a13[i / 8192, j / 8192]
                  + a14[i / 16384, j / 16384]
                  + a15[i / 32768, j / 32768];

                pixels[i, j].r = p[a].r;
                pixels[i, j].g = p[a].g;
                pixels[i, j].b = p[a].b;
            }

        var b = PixelsToBitmap(pixels);

        for (int i = 0; i < 99; i++)
            SaveNetPBM(b, (i + 1).ToString("D2") + ".pm");
    }
    public static void PinkNoiseAdvanced()
    {
        var r = new Random();

        var width = 28468;
        var height = 25145;
        var ton = new ushort[height, width];
        var pixels = new Color24[height, width];

        var w = width;
        var h = height;

        var a00 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a00[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a01 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a01[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a02 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a02[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a03 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a03[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a04 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a04[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a05 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a05[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a06 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a06[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a07 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a07[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a08 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a08[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a09 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a09[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a10 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a10[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a11 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a11[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a12 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a12[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a13 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a13[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a14 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a14[i, j] = (byte)(r.Next() % 256);

        w = (w + 1) / 2; h = (h + 1) / 2;

        var a15 = new byte[h, w];
        for (int i = 0; i < h; i++)
            for (int j = 0; j < w; j++)
                a15[i, j] = (byte)(r.Next() % 256);

        var p = Enumerable.Range(0, 4081).Select(x => PseudoGreyPlus24(x)).ToArray();

        var k = new int[4081];

        int max = int.MinValue;
        int min = int.MaxValue;

        for (int i = 0; i < height * 2; i++)
            for (int j = 0; j < width * 2; j++)
            {
                int a = (r.Next() % 256)
                  + a00[i / 2, j / 2]
                  + a01[i / 4, j / 4]
                  + a02[i / 8, j / 8]
                  + a03[i / 16, j / 16]
                  + a04[i / 32, j / 32]
                  + a05[i / 64, j / 64]
                  + a06[i / 128, j / 128]
                  + a07[i / 256, j / 256]
                  + a08[i / 512, j / 512]
                  + a09[i / 1024, j / 1024]
                  + a10[i / 2048, j / 2048]
                  + a11[i / 4096, j / 4096]
                  + a12[i / 8192, j / 8192]
                  + a13[i / 16384, j / 16384]
                  + a14[i / 32768, j / 32768]
                  + a15[i / 65536, j / 65536];

                ton[i / 2, j / 2] += (ushort)a;
            }

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                if (ton[i, j] > max) max = ton[i, j];
                if (ton[i, j] < min) min = ton[i, j];
            }

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                ton[i, j] -= (ushort)min;

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                ton[i, j] = (ushort)(4080 * ton[i, j] / (0.0 + max - min));

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                pixels[i, j].r = p[ton[i, j]].r;
                pixels[i, j].g = p[ton[i, j]].g;
                pixels[i, j].b = p[ton[i, j]].b;
            }

        var b = PixelsToBitmap(pixels);

        for (int i = 0; i < 99; i++)
            SaveNetPBM(b, (i + 1).ToString("D2") + ".pm");
    }
}
