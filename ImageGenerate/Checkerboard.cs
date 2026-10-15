// Damalı desen üretimi, ölçekleme ve bu desenin video çıktıları.
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static byte[,] Checker(byte[,] a)
    {
        var b = a.GetLength(0);
        var c = b * 2;
        var d = new byte[c, c];

        for (var i = 0; i < c; i++)
            for (var j = 0; j < c; j++)
                d[i, j] = a[i / 2, j / 2];

        for (var i = 0; i < c; i++)
            d[i, i] = (byte)(255 - d[i, i]);

        return d;
    }
    public static byte[,] Checker169(byte[,] a)
    {
        var b = a.GetLength(0);
        var c = b * 2;
        var d = new byte[c, c];

        for (var i = 0; i < c; i++)
            for (var j = 0; j < c; j++)
                d[i, j] = a[i / 2, j / 2];

        for (var i = 0; i < c; i++)
            d[i, i]++;

        return d;
    }
    public static void CheckerVideo()
    {
        var n = 14;
        var a = 1 << n;
        var b = new byte[1, 1] { { 0 } };

        for (int i = 0; i < n; i++)
            b = Checker(b);

        var inputArgs = $"-y -f rawvideo -pix_fmt gray -s:v 4608x8192 -r 60 -i -";
        var outputArgs = $"-c:v libx265 -preset ultrafast -x265-params lossless=1 1.mp4";

        var p = Ffmpeg.Start(inputArgs, outputArgs);

        var ffmpegIn = p.StandardInput.BaseStream;

        for (int f = 0; f < 3600; f++)
        {
            for (int i = 0; i < 9216; i++)
                for (int j = 0; j < 16384; j++)
                    ffmpegIn.WriteByte(b[(i + f) % 16384, (j + f) % 16384]);

            ffmpegIn.Flush();
        }
        Ffmpeg.Finish(p);
    }
    public static void CheckerVideo169()
    {
        var n = 7;
        var a = 1 << n;
        var b = new byte[1, 1] { { 0 } };

        for (int i = 0; i < n; i++)
            b = Checker169(b);

        var r = new byte[8, 3] {
       { 0, 0, 0 },
       { 0, 0, 255 },
       { 0, 255, 0 },
       { 0, 255, 255 },
       { 255, 0, 0 },
       { 255, 0, 255 },
       { 255, 255, 0 },
       { 255, 255, 255 }
     };

        var c = new byte[8192, 8192, 3];
        for (int i = 0; i < 8192; i++)
            for (int j = 0; j < 8192; j++)
            {
                var k = b[i / 64, j / 64];
                c[i, j, 0] = r[k % 8, 0];
                c[i, j, 1] = r[k % 8, 1];
                c[i, j, 2] = r[k % 8, 2];
            }

        var inputArgs = $"-y -f rawvideo -pix_fmt rgb24 -s:v 8192x4608 -r 60 -i -";
        var outputArgs = $"-c:v libx265 -preset ultrafast -x265-params lossless=1 lossless.mp4";

        var p = Ffmpeg.Start(inputArgs, outputArgs);

        var ffmpegIn = p.StandardInput.BaseStream;

        for (int f = 0; f < 4096; f++)
        {
            for (int i = 0; i < 4608; i++)
                for (int j = 0; j < 8192; j++)
                {
                    var ii = (i + f) % 8192;
                    var jj = (j + f) % 8192;

                    ffmpegIn.WriteByte(c[ii, jj, 2]);
                    ffmpegIn.WriteByte(c[ii, jj, 1]);
                    ffmpegIn.WriteByte(c[ii, jj, 0]);
                }

            ffmpegIn.Flush();
        }
        Ffmpeg.Finish(p);
    }
    public static void Izgara()
    {
        int w = 512;
        int h = 512;
        var width = 8192;
        var height = 4608;
        var pixels = new Color24[height, width];
        var pix = new Color24[1 + height / h, 1 + width / w];

        var r = new Random();

        for (int y = 0; y < 1 + height / h; y++)
            for (int x = 0; x < 1 + width / w; x++)
            {
                pix[y, x].r = (byte)(r.Next(86) * 3);
                pix[y, x].g = (byte)(r.Next(86) * 3);
                pix[y, x].b = (byte)(r.Next(86) * 3);
            }

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                pixels[y, x] = pix[y / h, x / w];

        var b = PixelsToBitmap(pixels);

        for (int i = 0; i < 101; i += 5)
            SaveNetPBM(b, i.ToString("D3") + ".pm");
    }
}
