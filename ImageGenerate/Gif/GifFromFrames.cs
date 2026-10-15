// Harici PNG/BMP karelerinden dithering ile GIF üretimi.
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static void GifFromFrames()
    {
        var palette = Palette555();

        int width = 270;
        int height = 480;

        using var gif = new AnimatedGif("1.gif", width, height, palette, 5, 7);
        var pixels = new byte[width * height];
        var path = @"C:\Users\Kenan\Desktop\d\ffmpeg-master-latest-win64-gpl\bin\{0:D4}.png";
        for (int f = 1; f < 379; f++)
        {
            var frame = NetPBM.Load(string.Format(path, 3 * f));
            {
                var c = new ChannelBasedImage(frame);
                c.Scale(9);
                FloydSteinberg(c.r, x => x > 8388608 ? 16777215 : 0);
                FloydSteinberg(c.g, x => x > 8388608 ? 16777215 : 0);
                FloydSteinberg(c.b, x => x > 8388608 ? 16777215 : 0);

                var k = 0;
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        byte p = 0;

                        if (c.b[2 * i + 0, 2 * j + 0] != 0) p++;
                        if (c.b[2 * i + 0, 2 * j + 1] != 0) p++;
                        if (c.b[2 * i + 1, 2 * j + 0] != 0) p++;
                        if (c.b[2 * i + 1, 2 * j + 1] != 0) p++;

                        if (c.g[2 * i + 0, 2 * j + 0] != 0) p += 5;
                        if (c.g[2 * i + 0, 2 * j + 1] != 0) p += 5;
                        if (c.g[2 * i + 1, 2 * j + 0] != 0) p += 5;
                        if (c.g[2 * i + 1, 2 * j + 1] != 0) p += 5;

                        if (c.r[2 * i + 0, 2 * j + 0] != 0) p += 25;
                        if (c.r[2 * i + 0, 2 * j + 1] != 0) p += 25;
                        if (c.r[2 * i + 1, 2 * j + 0] != 0) p += 25;
                        if (c.r[2 * i + 1, 2 * j + 1] != 0) p += 25;

                        pixels[k++] = p;
                    }

                gif.AddFrame(pixels);
            }

        }
    }
    public static void GifFromFrames2()
    {
        var palette = new byte[24] {
   0,0,0,
   0,0,255,
   0,255,0,
   0,255,255,
   255,0,0,
   255,0,255,
   255,255,0,
   255,255,255
  };

        int width = 512;
        int height = 256;

        using var gif = new AnimatedGif("1.gif", width, height, palette, 2, 3);
        var pixels = new byte[width * height];
        var path = @"C:\Users\Kenan\Desktop\d\ffmpeg-master-latest-win64-gpl\bin\{0:D4}.bmp";
        for (int f = 1; f < 501; f++)
        {
            var frame = NetPBM.Load(string.Format(path, f));
            {
                var c = new ChannelBasedImage(frame);
                SimpleDither(c.r, x => x > 8388608 ? 16777215 : 0);
                SimpleDither(c.g, x => x > 8388608 ? 16777215 : 0);
                SimpleDither(c.b, x => x > 8388608 ? 16777215 : 0);

                var k = 0;
                for (int i = 0; i < height; i++)
                    for (int j = 0; j < width; j++)
                    {
                        byte p = 0;

                        if (c.b[i, j] != 0) p++;
                        if (c.g[i, j] != 0) p += 2;
                        if (c.r[i, j] != 0) p += 4;

                        pixels[k++] = p;
                    }

                gif.AddFrame(pixels);
            }

        }
    }
}
