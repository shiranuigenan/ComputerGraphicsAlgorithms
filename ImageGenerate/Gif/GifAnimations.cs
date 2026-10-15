// Prosedürel üretilen GIF animasyonları: sinüzoidal, ivme, artı ve pseudo.
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static void GifSinusoidal()
    {
        var palette = PaletteGray256();

        int width = 1024;
        int height = 256;

        using var gif = new AnimatedGif("sinusoidal-gray.gif", width, height, palette);
        var pixels = new byte[width * height];
        var sins = new byte[width];
        for (int f = 0; f < width / 4; f++)
        {
            var k = 0;
            for (int j = 0; j < width; j++)
                sins[j] = (byte)(255 * (Math.Sin(Math.PI * (j + 8 * f) / width) / 2 + 0.5));

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    pixels[k++] = (byte)(sins[j] - i);
                }
            gif.AddFrame(pixels);

        }
    }
    public static void GifAcceleration()
    {
        var palette = new byte[2 * 3] { 0, 0, 0, 255, 255, 255 };

        int width = 1000;
        int height = 64;

        using var gif = new AnimatedGif("acceleration.gif", width, height, palette, 2, 1);
        var pixels = new byte[width * height];
        for (int f = 0; f < 1000; f++)
        {
            for (int i = 0; i < height; i++)
                pixels[i * width + f] = 1;
            gif.AddFrame(pixels);

        }
    }
    public static void GifPlus()
    {
        var palette = Palette5173();

        int width = 256;
        int height = 256;

        using var gif = new AnimatedGif("plus.gif", width, height, palette);
        var pixels = new byte[width * height];

        for (int f = 0; f < 254; f++)
        {
            var k = 0;
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    pixels[k++] = (byte)((k + f - 1) % 255);
            gif.AddFrame(pixels);

        }
    }
    public static void GifPseudo()
    {
        var palette = new byte[768];
        var pseudoPalette = new byte[315];
        var k = 0;
        for (int i = 0; i < 315; i++)
        {
            var p = PseudoGreyPlus24(i + 3766);

            var newColor = true;
            for (int j = 0; j < k; j++)
                if (p.r == palette[3 * j + 0] && p.g == palette[3 * j + 1] && p.b == palette[3 * j + 2])
                {
                    newColor = false;
                    break;
                }

            if (newColor)
            {
                palette[3 * k + 0] = p.r;
                palette[3 * k + 1] = p.g;
                palette[3 * k + 2] = p.b;
                k++;
            }

            pseudoPalette[i] = (byte)(k - 1);
        }

        int width = 512;
        int height = 16;

        using var gif = new AnimatedGif("pseudo.gif", width, height, palette);
        var pixels = new byte[width * height];

        for (int f = 0; f < 254; f++)
        {
            k = 0;
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    pixels[k++] = pseudoPalette[(i + j + f) % 315];
            gif.AddFrame(pixels);

        }
    }
}
