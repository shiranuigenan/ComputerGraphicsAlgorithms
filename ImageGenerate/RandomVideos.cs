// ffmpeg'e ham piksel akıtarak üretilen rastgelelik, renk yağmuru ve Brownian hareket videoları.
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static void PerfectRandom48()
    {
        var w = 9;
        var h = 5;

        var inputArgs = $"-y -f rawvideo -pix_fmt rgb48 -s:v {w}x{h} -r 60 -i -";
        var outputArgs = $"-c:v flashsv2 -vf scale=3840:2160 -sws_flags neighbor flashsv2.mkv";

        var p = Ffmpeg.Start(inputArgs, outputArgs);

        var ffmpegIn = p.StandardInput.BaseStream;

        var a = new ushort[w * h * 3];
        var b = new byte[w * h * 3];
        var r = new Random();

        for (int i = 0; i < a.Length; i++)
            a[i] = (ushort)r.Next(65536);

        for (int i = 0; i < 360; i++)
        {
            r.NextBytes(b);
            for (int j = 0; j < a.Length; j++)
            {
                a[j] += b[j];

                ffmpegIn.WriteByte((byte)a[j]);
                ffmpegIn.WriteByte((byte)(a[j] >> 8));
            }
            ffmpegIn.Flush();
        }
        Ffmpeg.Finish(p);
    }
    public static void PerfectRandom16Gray()
    {
        var inputArgs = $"-y -f rawvideo -pix_fmt rgb48 -s:v 9x5 -r 60 -i -";
        var outputArgs = $"-sws_flags gauss -c:v libx265 -preset ultrafast -vf scale=16384:9216 -crf 0 gauss_crf0_16k.mp4";
        var p = Ffmpeg.Start(inputArgs, outputArgs);

        var ffmpegIn = p.StandardInput.BaseStream;

        var a = new ushort[135];
        var b = new byte[135];
        var r = new Random();

        for (int i = 0; i < a.Length; i++)
            a[i] = (ushort)r.Next(65536);

        for (int i = 0; i < 4859; i++)
        {
            r.NextBytes(b);
            for (int j = 0; j < a.Length; j++)
            {
                a[j] += b[j];

                ffmpegIn.WriteByte((byte)a[j]);
                ffmpegIn.WriteByte((byte)(a[j] >> 8));
            }
            ffmpegIn.Flush();
        }
        Ffmpeg.Finish(p);
    }
    public static void PerfectRandom8()
    {
        var w = 16;
        var h = 9 * w / 16;

        var inputArgs = $"-y -f rawvideo -pix_fmt rgb24 -s:v {w}x{h} -r 60 -i -";
        var outputArgs = $"-c:v libsvtav1 -preset 12 -vf scale=7680:4320 -crf 31 crf31_8k.mp4";
        var p = Ffmpeg.Start(inputArgs, outputArgs);

        var ffmpegIn = p.StandardInput.BaseStream;

        var a = new int[w * h];
        var b = new byte[w * h];
        var r = new Random();

        for (int j = 0; j < a.Length; j++)
            a[j] = r.Next() % 16777216;

        for (int i = 0; i < 5184; i++)
        {
            r.NextBytes(b);
            for (int j = 0; j < a.Length; j++)
            {
                a[j] = (a[j] + (b[j] >> 7)) % 16777216;
                ffmpegIn.WriteByte((byte)a[j]);
                ffmpegIn.WriteByte((byte)(a[j] >> 8));
                ffmpegIn.WriteByte((byte)(a[j] >> 16));
            }
            ffmpegIn.Flush();
        }
        Ffmpeg.Finish(p);
    }
    public static void PerfectRandom24()
    {
        var w = 9;
        var h = 5;

        var inputArgs = $"-y -f rawvideo -pix_fmt rgb24 -s:v 9x5 -r 60 -i -";
        var outputArgs = $"-c:v libx265 -preset ultrafast -x265-params lossless=1 -sws_flags neighbor -vf scale=240:135 1.mp4";
        var p = Ffmpeg.Start(inputArgs, outputArgs);

        var ffmpegIn = p.StandardInput.BaseStream;

        var a = new byte[w * h * 3];
        var b = new byte[w * h * 3];
        var r = new Random();
        r.NextBytes(a);

        for (int i = 0; i < 3600; i++)
        {
            r.NextBytes(b);
            for (int j = 0; j < a.Length; j++)
                a[j] += (byte)(b[j] >> 7);

            ffmpegIn.Write(a);
            ffmpegIn.Flush();
        }
        Ffmpeg.Finish(p);
    }
    public static void ColorRain()
    {
        var inputArgs = $"-y -f rawvideo -pix_fmt rgb48 -s:v 9x5 -r 60 -i -";
        var outputArgs = $"-c:v libx265 -preset ultrafast -vf scale=2430:1350 -x265-params lossless=1 9.mp4";

        var p = Ffmpeg.Start(inputArgs, outputArgs);

        var ffmpegIn = p.StandardInput.BaseStream;
        var a = new ushort[84];
        var b = new byte[84];
        var r = new Random();

        for (int i = 0; i < 84; i++)
            a[i] = (ushort)r.Next(65536);

        for (int i = 0; i < 4859; i++)
        {

            r.NextBytes(b);
            for (var j = 0; j < 84; j++)
            {
                a[j] += b[j];
                ffmpegIn.WriteByte((byte)a[j]);
                ffmpegIn.WriteByte((byte)(a[j] >> 8));
            }

            ffmpegIn.Flush();
        }
        Ffmpeg.Finish(p);
    }
    public static void ColorRainHDR()
    {
        var inputArgs = $"-y -f rawvideo -pix_fmt rgb48 -s:v 9x5 -r 60 -i -";
        var outputArgs = $"-c:v libaom-av1 -colorspace bt2020nc -color_trc smpte2084 -color_primaries bt2020 -preset ultrafast -vf scale=144:80 -pix_fmt yuv420p10le -crf 0 -sws_flags gauss 8.mp4";

        var p = Ffmpeg.Start(inputArgs, outputArgs);

        var ffmpegIn = p.StandardInput.BaseStream;
        var a = new ushort[135];
        var b = new byte[135];
        var r = new Random();

        for (int i = 0; i < 135; i++)
            a[i] = (ushort)r.Next(65536);

        for (int i = 0; i < 4859; i++)
        {
            r.NextBytes(b);
            for (var j = 0; j < 135; j++)
            {
                a[j] += b[j];
                ffmpegIn.WriteByte((byte)a[j]);
                ffmpegIn.WriteByte((byte)(a[j] >> 8));
            }

            ffmpegIn.Flush();
        }
        Ffmpeg.Finish(p);
    }
    public static void BrownianMotion()
    {
        var inputArgs = $"-y -f rawvideo -pix_fmt gray -s:v 36x64 -r 60 -i -";
        var outputArgs = "-c:v libx265 -sws_flags neighbor -preset ultrafast -vf scale=4320:7680 -x265-params lossless=1 1.mp4";
        var p = Ffmpeg.Start(inputArgs, outputArgs);

        var ffmpegIn = p.StandardInput.BaseStream;
        var a = new byte[2304];
        var x = 18;
        var y = 32;
        var r = new Random();
        for (int i = 0; i < 3600; i++)
        {
            var z = y * 36 + x;
            a[z] += 51;
            x += (r.Next() % 3) - 1;
            y += (r.Next() % 5) - 2;

            if (x < 0) x += 36;
            if (x > 35) x -= 36;
            if (y < 0) y += 64;
            if (y > 63) y -= 64;

            ffmpegIn.Write(a);
            ffmpegIn.Flush();
        }
        Ffmpeg.Finish(p);
    }
    public static void Bound()
    {
        var inputArgs = $"-y -f rawvideo -pix_fmt rgb48 -s:v 9x5 -r 60 -i -";
        var outputArgs = $"-c:v libaom-av1 -colorspace bt2020nc -color_trc smpte2084 -color_primaries bt2020 -preset ultrafast -vf scale=1280:720 -pix_fmt yuv420p10le -crf 0 -sws_flags gauss 8.mp4";

        var p = Ffmpeg.Start(inputArgs, outputArgs);

        var ffmpegIn = p.StandardInput.BaseStream;
        var a = new ushort[135];
        var b = new byte[135];
        var r = new Random();

        for (int i = 0; i < 135; i++)
            a[i] = (ushort)r.Next(65536);

        for (int i = 0; i < 1530; i++)
        {
            r.NextBytes(b);
            for (var j = 0; j < 135; j++)
            {
                a[j] += (byte)(b[j] % (2 + i / 60));
                ffmpegIn.WriteByte((byte)a[j]);
                ffmpegIn.WriteByte((byte)(a[j] >> 8));
            }

            ffmpegIn.Flush();
        }
        Ffmpeg.Finish(p);
    }
}
