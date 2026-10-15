// Çizgisel hareket, ivme ve çok seviyeli (multi-level) video akışları.
using System.Diagnostics;
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static void Acceleration()
    {
        var b = Enumerable.Range(0, 4081).Select(x => PseudoGreyPlus24(x)).ToArray();
        var w = 1044480;
        var h = 1;

        var inputArgs = $"-y -f rawvideo -pix_fmt rgb24 -s:v {w}x{h} -r 60 -i -";
        var outputArgs = "-c:v libx265 -preset ultrafast -crf 0 -vf scale=16384:9216 1.mp4";
        var p = new Process
        {
            StartInfo =
{
 FileName = "ffmpeg.exe",
 Arguments = $"{inputArgs} {outputArgs}",
 UseShellExecute = false,
 CreateNoWindow = false,
 RedirectStandardInput = true
}
        };

        p.Start();

        var t = 0;
        var a = new int[54000];
        for (int i = 0; i < 27000; i++)
        {
            t += i;
            a[i] = t;
        }
        for (int i = 0; i < 27000; i++)
        {
            t += 27000 - i;
            a[i + 27000] = t;
        }

        var ffmpegIn = p.StandardInput.BaseStream;
        var Data = new byte[w * h * 3];
        for (int i = 0; i < 54000; i++)
        {
            if (i % 5400 == 0)
                Console.WriteLine(i / 5400);

            for (int j = 0; j < w * h; j++)
            {
                var k = (a[i] + j) % 522240;
                var c = b[k / 128];
                Data[3 * j + 0] = c.r;
                Data[3 * j + 1] = c.g;
                Data[3 * j + 2] = c.b;
            }

            ffmpegIn.Write(Data);
            ffmpegIn.Flush();
        }
        ffmpegIn.Close();
        p.WaitForExit();
    }
    public static void AccelerationLine()
    {
        var w = 1044480;
        var h = 4;

        var inputArgs = $"-y -f rawvideo -pix_fmt rgb24 -s:v {w}x{h} -r 60 -i -";
        var outputArgs = "-c:v libx265 -preset ultrafast -crf 0 -vf scale=16384:9216 2.mp4";
        var p = new Process
        {
            StartInfo =
{
 FileName = "ffmpeg.exe",
 Arguments = $"{inputArgs} {outputArgs}",
 UseShellExecute = false,
 CreateNoWindow = false,
 RedirectStandardInput = true
}
        };

        p.Start();

        var a = new byte[8400 * 3];
        Array.Fill(a, (byte)255);
        var c = new byte[8400 * 3];
        for (int i = 0; i < 4200; i++)
        {
            var b = PseudoGreyPlus24(i);
            c[3 * i + 0] = b.r;
            c[3 * i + 1] = b.g;
            c[3 * i + 2] = b.b;

            c[3 * (8399 - i) + 0] = b.r;
            c[3 * (8399 - i) + 1] = b.g;
            c[3 * (8399 - i) + 2] = b.b;
        }

        var ffmpegIn = p.StandardInput.BaseStream;
        var Data = new byte[w * 3];
        var k = 0;
        for (int i = 0; i < 1440; i++)
        {
            k += i;
            if (i % 145 == 0)
                Console.WriteLine(i / 145);

            ffmpegIn.Write(Data, 0, k * 3);
            ffmpegIn.Write(c);
            ffmpegIn.Write(Data, 0, 1036080 * 3 - k * 3);

            ffmpegIn.Write(Data, 0, k * 3);
            ffmpegIn.Write(c);
            ffmpegIn.Write(Data, 0, 1036080 * 3 - k * 3);

            ffmpegIn.Write(Data, 0, k * 3);
            ffmpegIn.Write(a);
            ffmpegIn.Write(Data, 0, 1036080 * 3 - k * 3);

            ffmpegIn.Write(Data, 0, k * 3);
            ffmpegIn.Write(a);
            ffmpegIn.Write(Data, 0, 1036080 * 3 - k * 3);

            ffmpegIn.Flush();
        }
        ffmpegIn.Close();
        p.WaitForExit();
    }
    public static void LinearMovementX()
    {
        var inputArgs = $"-y -f rawvideo -pix_fmt gray -s:v 131072x1 -r 60 -i -";
        var outputArgs = "-c:v libx265 -preset ultrafast -vf scale=16384:9216 -x265-params lossless=1 lossless_16k_ultrafast.mp4";

        var p = new Process
        {
            StartInfo =
  {
   FileName = "ffmpeg.exe",
   Arguments = $"{inputArgs} {outputArgs}",
   UseShellExecute = false,
   CreateNoWindow = false,
   RedirectStandardInput = true
  }
        };

        p.Start();

        var ffmpegIn = p.StandardInput.BaseStream;
        var a = new byte[131072];

        for (int i = 0; i < 131072; i++)
        {
            a[i] = 255;
            ffmpegIn.Write(a);
            ffmpegIn.Flush();
        }
        ffmpegIn.Close();
        p.WaitForExit();
    }
    public static void LinearMovementVertical()
    {
        var inputArgs = $"-y -f rawvideo -pix_fmt gray -s:v 16384x9216 -r 60 -i -";
        var outputArgs = "-c:v libx265 -preset ultrafast -x265-params lossless=1 lossless_16k_ultrafast.mp4";

        var p = new Process
        {
            StartInfo =
  {
   FileName = "ffmpeg.exe",
   Arguments = $"{inputArgs} {outputArgs}",
   UseShellExecute = false,
   CreateNoWindow = false,
   RedirectStandardInput = true
  }
        };

        p.Start();

        var ffmpegIn = p.StandardInput.BaseStream;
        var a = new byte[16384 * 9216];

        for (int i = 0; i < 4859; i++)
        {
            var t = 0;
            for (int j = 0; j < 9216; j++)
            {
                var h = 16 * j;
                for (int k = 0; k < 16384; k++)
                {
                    if (h + 9 * k < i * 60.7015)
                        a[t++] = 255;
                    else
                        a[t++] = 0;
                }
            }
            ffmpegIn.Write(a);
            ffmpegIn.Flush();
        }
        ffmpegIn.Close();
        p.WaitForExit();
    }
    public static void MultiLevel()
    {
        var n = 7;
        var w = 747;
        var h = 420;

        var inputArgs = $"-y -f rawvideo -pix_fmt rgb48 -s:v {w}x{h} -r 60 -i -";
        var outputArgs = "-c:v libx265 -preset ultrafast -crf 0 crf0.mp4";
        var p = new Process
        {
            StartInfo =
{
 FileName = "ffmpeg.exe",
 Arguments = $"{inputArgs} {outputArgs}",
 UseShellExecute = false,
 CreateNoWindow = false,
 RedirectStandardInput = true
}
        };

        p.Start();

        var rnd = new Random();
        var a = new List<ushort[,,]>();

        for (int i = 0; i < n; i++)
        {
            var x = (int)Math.Ceiling(1.0 * w / (h / (i + 1)));
            var y = (int)Math.Ceiling(1.0 * h / (h / (i + 1)));

            var u = new ushort[y, x, 3];

            for (int j = 0; j < u.GetLength(0); j++)
                for (int k = 0; k < u.GetLength(1); k++)
                {
                    u[j, k, 0] = (ushort)rnd.Next(65536);
                    u[j, k, 1] = (ushort)rnd.Next(65536);
                    u[j, k, 2] = (ushort)rnd.Next(65536);
                }

            a.Add(u);
        }

        var ffmpegIn = p.StandardInput.BaseStream;

        for (int i = 1; i < n + 1; i++)
        {
            for (int j = 0; j < 180; j++)
            {
                for (int s = 0; s < i; s++)
                {
                    var e = 0;
                    var b = new byte[a[s].GetLength(0) * a[s].GetLength(1) * 3];
                    rnd.NextBytes(b);
                    for (int t = 0; t < a[s].GetLength(0); t++)
                        for (int k = 0; k < a[s].GetLength(1); k++)
                        {
                            a[s][t, k, 0] += b[e++];
                            a[s][t, k, 1] += b[e++];
                            a[s][t, k, 2] += b[e++];
                        }
                }

                for (int y = 0; y < h; y++)
                    for (int x = 0; x < w; x++)
                    {
                        int r = 0, g = 0, b = 0;
                        for (int k = 0; k < i; k++)
                        {
                            var xx = x / (h / (k + 1));
                            var yy = y / (h / (k + 1));

                            r += a[k][yy, xx, 0];
                            g += a[k][yy, xx, 1];
                            b += a[k][yy, xx, 2];
                        }
                        var ur = r / i;
                        var ug = g / i;
                        var ub = b / i;

                        ffmpegIn.WriteByte((byte)ur); ffmpegIn.WriteByte((byte)(ur >> 8));
                        ffmpegIn.WriteByte((byte)ug); ffmpegIn.WriteByte((byte)(ug >> 8));
                        ffmpegIn.WriteByte((byte)ub); ffmpegIn.WriteByte((byte)(ub >> 8));
                    }
                ffmpegIn.Flush();
            }
        }
        ffmpegIn.Close();
        p.WaitForExit();
    }
    public static void MultiLevelLine()
    {
        var w = 1044480;
        var inputArgs = $"-y -f rawvideo -pix_fmt gray16 -s:v {w}x1 -r 60 -i -";
        var outputArgs = $"-c:v libx265 -preset veryfast -x265-params lossless=1 -vf scale=16384:9216 vertfast_lossless.mp4";
        var p = new Process
        {
            StartInfo =
  {
   FileName = "ffmpeg.exe",
   Arguments = $"{inputArgs} {outputArgs}",
   UseShellExecute = false,
   CreateNoWindow = false,
   RedirectStandardInput = true
  }
        };

        p.Start();

        var ffmpegIn = p.StandardInput.BaseStream;

        for (int i = 0; i < 65537; i++)
        {
            for (int j = 0; j < w; j++)
            {
                var g = (ushort)(i + j / 16);
                ffmpegIn.WriteByte((byte)g);
                ffmpegIn.WriteByte((byte)(g >> 8));
            }

            ffmpegIn.Flush();
        }
        ffmpegIn.Close();
        p.WaitForExit();

    }
}
