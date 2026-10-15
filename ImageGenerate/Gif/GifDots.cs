// Nokta animasyonları: noktasal hareket ve yarım piksel taşıma GIFleri.
using static ComputerGraphicsAlgorithms.Common;

namespace ComputerGraphicsAlgorithms;

public static partial class ImageGenerate
{
    public static void GifDot1()
    {
        var palette = new byte[2 * 3] { 0, 0, 0, 255, 255, 255 };

        using var gif = new AnimatedGif($"dot1.gif", 4, 4, palette, 30, 1);
        var pixels = new byte[16] { 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        gif.AddFrame(pixels);
        pixels[5] = 1; pixels[6] = 0;
        gif.AddFrame(pixels);
        pixels[6] = 1; pixels[10] = 0;
        gif.AddFrame(pixels);
        pixels[10] = 1; pixels[9] = 0;
        gif.AddFrame(pixels);
    }
    public static void GifDot2()
    {
        var palette = new byte[4 * 3] { 0, 0, 0, 85, 85, 85, 170, 170, 170, 255, 255, 255 };

        using var gif = new AnimatedGif($"dot2.gif", 4, 4, palette, 10, 2);
        var pixels = new byte[16] { 3, 3, 3, 3, 3, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 };
        gif.AddFrame(pixels);
        pixels[5] = 1; pixels[6] = 2;
        gif.AddFrame(pixels);
        pixels[5] = 2; pixels[6] = 1;
        gif.AddFrame(pixels);
        pixels[5] = 3; pixels[6] = 0;
        gif.AddFrame(pixels);
        pixels[6] = 1; pixels[10] = 2;
        gif.AddFrame(pixels);
        pixels[6] = 2; pixels[10] = 1;
        gif.AddFrame(pixels);
        pixels[6] = 3; pixels[10] = 0;
        gif.AddFrame(pixels);
        pixels[10] = 1; pixels[9] = 2;
        gif.AddFrame(pixels);
        pixels[10] = 2; pixels[9] = 1;
        gif.AddFrame(pixels);
        pixels[10] = 3; pixels[9] = 0;
        gif.AddFrame(pixels);
        pixels[9] = 1; pixels[5] = 2;
        gif.AddFrame(pixels);
        pixels[9] = 2; pixels[5] = 1;
        gif.AddFrame(pixels);
    }
    public static void GifDot4()
    {
        var palette = new byte[16 * 3] { 0, 0, 0, 17, 17, 17, 34, 34, 34, 51, 51, 51, 68, 68, 68, 85, 85, 85, 102, 102, 102, 119, 119, 119, 136, 136, 136, 153, 153, 153, 170, 170, 170, 187, 187, 187, 204, 204, 204, 221, 221, 221, 238, 238, 238, 255, 255, 255 };

        using var gif = new AnimatedGif($"dot4.gif", 4, 4, palette, 2, 4);
        var pixels = new byte[16] { 15, 15, 15, 15, 15, 0, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
        gif.AddFrame(pixels);
        pixels[5] = 1; pixels[6] = 14;
        gif.AddFrame(pixels);
        pixels[5] = 2; pixels[6] = 13;
        gif.AddFrame(pixels);
        pixels[5] = 3; pixels[6] = 12;
        gif.AddFrame(pixels);
        pixels[5] = 4; pixels[6] = 11;
        gif.AddFrame(pixels);
        pixels[5] = 5; pixels[6] = 10;
        gif.AddFrame(pixels);
        pixels[5] = 6; pixels[6] = 9;
        gif.AddFrame(pixels);
        pixels[5] = 7; pixels[6] = 8;
        gif.AddFrame(pixels);
        pixels[5] = 8; pixels[6] = 7;
        gif.AddFrame(pixels);
        pixels[5] = 9; pixels[6] = 6;
        gif.AddFrame(pixels);
        pixels[5] = 10; pixels[6] = 5;
        gif.AddFrame(pixels);
        pixels[5] = 11; pixels[6] = 4;
        gif.AddFrame(pixels);
        pixels[5] = 12; pixels[6] = 3;
        gif.AddFrame(pixels);
        pixels[5] = 13; pixels[6] = 2;
        gif.AddFrame(pixels);
        pixels[5] = 14; pixels[6] = 1;
        gif.AddFrame(pixels);
        pixels[5] = 15; pixels[6] = 0;
        gif.AddFrame(pixels);

        pixels[6] = 1; pixels[10] = 14;
        gif.AddFrame(pixels);
        pixels[6] = 2; pixels[10] = 13;
        gif.AddFrame(pixels);
        pixels[6] = 3; pixels[10] = 12;
        gif.AddFrame(pixels);
        pixels[6] = 4; pixels[10] = 11;
        gif.AddFrame(pixels);
        pixels[6] = 5; pixels[10] = 10;
        gif.AddFrame(pixels);
        pixels[6] = 6; pixels[10] = 9;
        gif.AddFrame(pixels);
        pixels[6] = 7; pixels[10] = 8;
        gif.AddFrame(pixels);
        pixels[6] = 8; pixels[10] = 7;
        gif.AddFrame(pixels);
        pixels[6] = 9; pixels[10] = 6;
        gif.AddFrame(pixels);
        pixels[6] = 10; pixels[10] = 5;
        gif.AddFrame(pixels);
        pixels[6] = 11; pixels[10] = 4;
        gif.AddFrame(pixels);
        pixels[6] = 12; pixels[10] = 3;
        gif.AddFrame(pixels);
        pixels[6] = 13; pixels[10] = 2;
        gif.AddFrame(pixels);
        pixels[6] = 14; pixels[10] = 1;
        gif.AddFrame(pixels);
        pixels[6] = 15; pixels[10] = 0;
        gif.AddFrame(pixels);

        pixels[10] = 1; pixels[9] = 14;
        gif.AddFrame(pixels);
        pixels[10] = 2; pixels[9] = 13;
        gif.AddFrame(pixels);
        pixels[10] = 3; pixels[9] = 12;
        gif.AddFrame(pixels);
        pixels[10] = 4; pixels[9] = 11;
        gif.AddFrame(pixels);
        pixels[10] = 5; pixels[9] = 10;
        gif.AddFrame(pixels);
        pixels[10] = 6; pixels[9] = 9;
        gif.AddFrame(pixels);
        pixels[10] = 7; pixels[9] = 8;
        gif.AddFrame(pixels);
        pixels[10] = 8; pixels[9] = 7;
        gif.AddFrame(pixels);
        pixels[10] = 9; pixels[9] = 6;
        gif.AddFrame(pixels);
        pixels[10] = 10; pixels[9] = 5;
        gif.AddFrame(pixels);
        pixels[10] = 11; pixels[9] = 4;
        gif.AddFrame(pixels);
        pixels[10] = 12; pixels[9] = 3;
        gif.AddFrame(pixels);
        pixels[10] = 13; pixels[9] = 2;
        gif.AddFrame(pixels);
        pixels[10] = 14; pixels[9] = 1;
        gif.AddFrame(pixels);
        pixels[10] = 15; pixels[9] = 0;
        gif.AddFrame(pixels);

        pixels[9] = 1; pixels[5] = 14;
        gif.AddFrame(pixels);
        pixels[9] = 2; pixels[5] = 13;
        gif.AddFrame(pixels);
        pixels[9] = 3; pixels[5] = 12;
        gif.AddFrame(pixels);
        pixels[9] = 4; pixels[5] = 11;
        gif.AddFrame(pixels);
        pixels[9] = 5; pixels[5] = 10;
        gif.AddFrame(pixels);
        pixels[9] = 6; pixels[5] = 9;
        gif.AddFrame(pixels);
        pixels[9] = 7; pixels[5] = 8;
        gif.AddFrame(pixels);
        pixels[9] = 8; pixels[5] = 7;
        gif.AddFrame(pixels);
        pixels[9] = 9; pixels[5] = 6;
        gif.AddFrame(pixels);
        pixels[9] = 10; pixels[5] = 5;
        gif.AddFrame(pixels);
        pixels[9] = 11; pixels[5] = 4;
        gif.AddFrame(pixels);
        pixels[9] = 12; pixels[5] = 3;
        gif.AddFrame(pixels);
        pixels[9] = 13; pixels[5] = 2;
        gif.AddFrame(pixels);
        pixels[9] = 14; pixels[5] = 1;
        gif.AddFrame(pixels);
    }
    public static void GifDot2R()
    {
        var palette = Palette444();

        List<byte[]> frames = [
         [3, 3, 3, 3, 3, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3],
   [3, 3, 3, 3, 3, 1, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3],
   [3, 3, 3, 3, 3, 2, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3],
   [3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3],
   [3, 3, 3, 3, 3, 3, 1, 3, 3, 3, 2, 3, 3, 3, 3, 3],
   [3, 3, 3, 3, 3, 3, 2, 3, 3, 3, 1, 3, 3, 3, 3, 3],
   [3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 3, 3, 3, 3, 3],
   [3, 3, 3, 3, 3, 3, 3, 3, 3, 2, 0, 3, 3, 3, 3, 3],
   [3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 3, 3, 3, 3, 3],
   [3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 2, 3, 3, 3, 3, 3],
   [3, 3, 3, 3, 3, 2, 3, 3, 3, 1, 3, 3, 3, 3, 3, 3],
   [3, 3, 3, 3, 3, 1, 3, 3, 3, 2, 3, 3, 3, 3, 3, 3]];

        using var gif = new AnimatedGif($"dot2r.gif", 4, 4, palette, 10, 6);
        for (int f = 0; f < frames.Count; f++)
        {
            var r = frames[f];
            var g = frames[(f + 3) % 12];
            var b = frames[(f + 6) % 12];
            var pixels = new byte[16];

            for (int i = 0; i < 16; i++)
                pixels[i] = (byte)(r[i] * 16 + g[i] * 4 + b[i]);

            gif.AddFrame(pixels);
        }
    }
}
