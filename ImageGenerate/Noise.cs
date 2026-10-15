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

        var layers = NoisePyramid(r, width, height, 16);

        var p = Enumerable.Range(0, 4081).Select(x => PseudoGreyPlus24(x)).ToArray();

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                var a = NoiseSumAt(layers, i, j);

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

        var layers = NoisePyramid(r, width, height, 16);

        var p = Enumerable.Range(0, 4081).Select(x => PseudoGreyPlus24(x)).ToArray();

        int max = int.MinValue;
        int min = int.MaxValue;

        for (int i = 0; i < height * 2; i++)
            for (int j = 0; j < width * 2; j++)
            {
                var a = (r.Next() % 256) + NoiseSumAt(layers, i / 2, j / 2);

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
    /// <summary>
    /// Pink noise için kademeli küçülen rastgele gri seviye katmanları üretir.
    /// katman[0] tam çözünürlüktür; her sonraki katman bir öncekinin yaklaşık yarısıdır ((boyut+1)/2).
    /// Rastgelelik akısının sırası, katmanların elle doldurulduğu eski kodla birebir aynıdır.
    /// </summary>
    /// <param name="r">Kullanılacak rastgelelik üreteci (paylaşılan örnekte akı devam eder).</param>
    /// <param name="width">İlk katmanın genişliği.</param>
    /// <param name="height">İlk katmanın yüksekliği.</param>
    /// <param name="levels">Katman sayısı (1/f deseni için 16 kullanılır).</param>
    static byte[][,] NoisePyramid(Random r, int width, int height, int levels)
    {
        var pyramid = new byte[levels][,];

        var w = width;
        var h = height;

        for (var level = 0; level < levels; level++)
        {
            var layer = new byte[h, w];

            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    layer[i, j] = (byte)(r.Next() % 256);

            pyramid[level] = layer;

            w = (w + 1) / 2;
            h = (h + 1) / 2;
        }

        return pyramid;
    }
    /// <summary>
    /// 1/f deseni: her katkının kendi katmanından, pikselin o katmandaki yerine karşılık gelen değeri gelerek toplanır
    /// (katman k için y >> k, x >> k).
    /// </summary>
    static int NoiseSumAt(byte[][,] pyramid, int y, int x)
    {
        var sum = 0;

        for (var k = 0; k < pyramid.Length; k++)
            sum += pyramid[k][y >> k, x >> k];

        return sum;
    }
}
