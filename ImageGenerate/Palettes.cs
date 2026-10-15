// GIF çıktılerinde kullanılan indeksli paletler: gri, 4-4-4, 5-5-5, 6-6-6, 8-8-4, 5-1-7-3.
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static byte[] PalettePseudoGray()
    {
        var file = "pseudo-gray.pal";

        if (File.Exists(file))
            return File.ReadAllBytes(file);
        else
        {
            var palette = new byte[768];

            int k = 0;
            for (int i = 0; i < 256; i++)
            {
                var t = PseudoGrey(i);
                palette[k++] = t.r;
                palette[k++] = t.g;
                palette[k++] = t.b;
            }

            File.WriteAllBytes(file, palette);

            return palette;
        }
    }
    public static byte[] PaletteGray256()
    {
        var file = "gray256.pal";

        if (File.Exists(file))
            return File.ReadAllBytes(file);
        else
        {
            var palette = new byte[768];

            int k = 0;
            for (int i = 0; i < 256; i++)
            {
                var t = (byte)i;
                palette[k++] = t;
                palette[k++] = t;
                palette[k++] = t;
            }

            File.WriteAllBytes(file, palette);

            return palette;
        }
    }
    public static byte[] Palette5173()
    {
        var file = "5173.pal";

        if (File.Exists(file))
            return File.ReadAllBytes(file);
        else
        {
            var palette = new byte[768];

            var ra = new byte[] { 0, 63, 127, 191, 255 };
            var ga = new byte[] { 0, 15, 31, 47, 63, 79, 95, 111, 127, 143, 159, 175, 191, 207, 223, 239, 255 };
            var ba = new byte[] { 0, 127, 255 };

            int k = 0;
            for (int r = 0; r < ra.Length; r++)
                for (int g = 0; g < ga.Length; g++)
                    for (int b = 0; b < ba.Length; b++)
                    {
                        palette[k++] = ra[r];
                        palette[k++] = ga[g];
                        palette[k++] = ba[b];
                    }
            palette[k++] = 255;
            palette[k++] = 255;
            palette[k++] = 255;

            File.WriteAllBytes(file, palette);

            return palette;
        }
    }
    public static byte Palette5173(byte r, byte g, byte b)
    {
        var ri = (10 * r + 5) / 512;
        var gi = (34 * g + 17) / 512;
        var bi = (6 * b + 3) / 512;

        return (byte)(51 * ri + 3 * gi + bi);
    }
    public static byte[] Palette884()
    {
        var file = "884.pal";

        if (File.Exists(file))
            return File.ReadAllBytes(file);
        else
        {
            var palette = new byte[768];

            int k = 0;
            for (int r = 0; r < 8; r++)
                for (int g = 0; g < 8; g++)
                    for (int b = 0; b < 4; b++)
                    {
                        palette[k++] = (byte)(r * 255 / 7);
                        palette[k++] = (byte)(g * 255 / 7);
                        palette[k++] = (byte)(b * 255 / 3);
                    }

            File.WriteAllBytes(file, palette);

            return palette;
        }
    }
    public static byte Palette884(byte r, byte g, byte b)
    {
        var ri = (16 * r + 8) / 512;
        var gi = (16 * g + 8) / 512;
        var bi = (8 * b + 4) / 512;

        return (byte)(32 * ri + 4 * gi + bi);
    }
    public static byte[] Palette666()
    {
        var file = "666.pal";

        if (File.Exists(file))
            return File.ReadAllBytes(file);
        else
        {
            var palette = new byte[768];

            int k = 0;
            for (int r = 0; r < 6; r++)
                for (int g = 0; g < 6; g++)
                    for (int b = 0; b < 6; b++)
                    {
                        palette[k++] = (byte)(r * 255 / 5);
                        palette[k++] = (byte)(g * 255 / 5);
                        palette[k++] = (byte)(b * 255 / 5);
                    }

            File.WriteAllBytes(file, palette);

            return palette;
        }
    }
    public static byte Palette666(byte r, byte g, byte b)
    {
        var ri = (12 * r + 6) / 512;
        var gi = (12 * g + 6) / 512;
        var bi = (12 * b + 6) / 512;

        return (byte)(36 * ri + 6 * gi + bi);
    }
    public static byte[] Palette555()
    {
        var file = "555.pal";

        if (File.Exists(file))
            return File.ReadAllBytes(file);
        else
        {
            var palette = new byte[384];

            int k = 0;
            for (int r = 0; r < 5; r++)
                for (int g = 0; g < 5; g++)
                    for (int b = 0; b < 5; b++)
                    {
                        palette[k++] = (byte)(r * 255 / 4);
                        palette[k++] = (byte)(g * 255 / 4);
                        palette[k++] = (byte)(b * 255 / 4);
                    }

            File.WriteAllBytes(file, palette);

            return palette;
        }
    }
    public static byte[] Palette444()
    {
        var file = "444.pal";

        if (File.Exists(file))
            return File.ReadAllBytes(file);
        else
        {
            var palette = new byte[192];

            int k = 0;
            for (int r = 0; r < 4; r++)
                for (int g = 0; g < 4; g++)
                    for (int b = 0; b < 4; b++)
                    {
                        palette[k++] = (byte)(r * 85);
                        palette[k++] = (byte)(g * 85);
                        palette[k++] = (byte)(b * 85);
                    }

            File.WriteAllBytes(file, palette);

            return palette;
        }
    }
}
