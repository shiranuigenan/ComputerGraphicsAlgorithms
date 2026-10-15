// Pink noise piramitinin büyük ölçekli ve animasyonlu varyantları (ffmpeg ham video akışı).
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static void PinkNoise2()
    {
        var w = 524288;
        var h = 294912;

        var a = new List<List<byte[]>>();

        var bit = 20;
        var ww = w;
        var hh = h;
        for (int i = 1; i < bit; i++)
        {
            ww = (ww + 1) >> 1;
            hh = (hh + 1) >> 1;

            var b = new List<byte[]>();
            for (int j = 0; j < hh; j++)
                b.Add(new byte[ww]);

            a.Add(b);
        }

        var p = new List<long[]>();
        for (int j = 0; j < h >> 4; j++)
            p.Add(new long[w >> 4]);

        var r = new Random();
        a.ForEach(x => x.ForEach(y => r.NextBytes(y)));

        for (int y = 0; y < h; y++)
        {
            if (y % 29492 == 0)
                Console.WriteLine(DateTime.Now.ToShortTimeString() + " " + y / 29492);

            for (int x = 0; x < w; x++)
            {
                int sum = r.Next(256);
                for (int j = 1; j < bit; j++)
                    sum += a[j - 1][y >> j][x >> j];
                p[y >> 4][x >> 4] += sum;
            }
        }

        var max = long.MinValue;
        var min = long.MaxValue;

        for (int y = 0; y < h >> 4; y++)
            for (int x = 0; x < w >> 4; x++)
            {
                if (p[y][x] > max) max = p[y][x];
                if (p[y][x] < min) min = p[y][x];
            }

        for (int y = 0; y < h >> 4; y++)
            for (int x = 0; x < w >> 4; x++)
                p[y][x] -= min;

        for (int y = 0; y < h >> 4; y++)
            for (int x = 0; x < w >> 4; x++)
                p[y][x] = (long)(4080 * p[y][x] / (0.0 + max - min));

        var pixels = new Color24[h >> 4, w >> 4];
        var t = Enumerable.Range(0, 4081).Select(x => PseudoGreyPlus24(x)).ToArray();

        for (int y = 0; y < h >> 4; y++)
            for (int x = 0; x < w >> 4; x++)
            {
                pixels[y, x].r = t[p[y][x]].r;
                pixels[y, x].g = t[p[y][x]].g;
                pixels[y, x].b = t[p[y][x]].b;
            }

        var bm = PixelsToBitmap(pixels);

        for (int i = 0; i < 99; i++)
            SaveNetPBM(bm, (i + 1).ToString("D2") + ".pm");

    }
    public static void PinkNoiseAnimated()
    {
        var w = 16384;
        var h = 9216;

        var a = new List<List<byte[]>>();

        var bit = 15;
        var ww = w;
        var hh = h;
        for (int i = 1; i < bit; i++)
        {
            ww = (ww + 1) >> 1;
            hh = (hh + 1) >> 1;

            var b = new List<byte[]>();
            for (int j = 0; j < hh; j++)
                b.Add(new byte[ww]);

            a.Add(b);
        }

        var inputArgs = $"-y -f rawvideo -pix_fmt rgb24 -s:v {w}x{h} -r 60 -i -";
        var outputArgs = $"-c:v libx265 -preset veryslow -crf 0 4.mp4";
        var p = Ffmpeg.Start(inputArgs, outputArgs);

        var ffmpegIn = p.StandardInput.BaseStream;
        var d = Enumerable.Range(0, 4081).Select(x => PseudoGreyPlus24(x)).ToArray();

        var r = new Random();
        var frameCount = 65536;
        for (int i = 0, diff, k, x, y, sum; i < frameCount; i++)
        {
            diff = (i - 1) ^ i;
            for (k = 1; k < bit; k++)
                if ((diff & (1 << k)) > 0)
                    a[k - 1].ForEach(x => r.NextBytes(x));

            for (y = 0; y < h; y++)
                for (x = 0; x < w; x++)
                {
                    sum = r.Next(256);
                    for (int j = 1; j < bit; j++)
                        sum += a[j - 1][y >> j][x >> j];

                    ffmpegIn.WriteByte(d[sum].r);
                    ffmpegIn.WriteByte(d[sum].g);
                    ffmpegIn.WriteByte(d[sum].b);
                }
            ffmpegIn.Flush();
        }
        Ffmpeg.Finish(p);
    }
}
