using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerGraphicsAlgorithms;
class Program
{
    static void Main(string[] args)
    {
        var n = 2048;
        var k = 1;

        var a = MatrixArctangent2(n, k);

        for (int p = 0; p < 2; p++)
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    a[i, j] = 2.0 * Math.Abs(a[i, j] - 0.5);

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                a[i, j] = 1.0 - Math.Pow(a[i, j], 0.5);

        var b = new Bitmap(2 * n, 2 * n, PixelFormat.Format24bppRgb);
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
            {
                var p = PsuedoGreyPlus((int)(4080 * a[i, j]));

                b.SetPixel(n + i, n + j, Color.FromArgb(p[0], p[1], p[2]));
                b.SetPixel(n + i, n - j, Color.FromArgb(p[0], p[1], p[2]));
                b.SetPixel(n - i, n + j, Color.FromArgb(p[0], p[1], p[2]));
                b.SetPixel(n - i, n - j, Color.FromArgb(p[0], p[1], p[2]));
            }
        b.Save("4080_.png");
        //saveJpeg(b, 100, "1785.jpg");
    }
    public static ImageCodecInfo GetEncoder(ImageFormat format)
    {
        try
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
                if (codec.FormatID == format.Guid)
                    return codec;
        }
        catch (System.Exception) { }
        return null;
    }
    public static void saveJpeg(Bitmap bitmap, int quality, string fileName)
    {
        try
        {
            var jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            var myEncoder = Encoder.Quality;
            var myEncoderParameters = new EncoderParameters(1);

            myEncoderParameters.Param[0] = new EncoderParameter(myEncoder, quality);
            bitmap.Save(fileName, jpgEncoder, myEncoderParameters);
        }
        catch (System.Exception) { }
    }
    public static byte[] PsuedoGrey(int g)
    {
        if (g < 0) return new byte[3] { 0, 0, 0 };
        if (g > 1784) return new byte[3] { 255, 255, 255 };

        var i = g / 7;
        var j = g % 7;

        var k = new byte[7, 3] { { 0, 0, 0 }, { 0, 0, 1 }, { 1, 0, 0 }, { 1, 0, 1 }, { 0, 1, 0 }, { 0, 1, 1 }, { 1, 1, 0 } };
        return new byte[3] { (byte)(i + k[j, 0]), (byte)(i + k[j, 1]), (byte)(i + k[j, 2]) };
    }
    public static byte[] PsuedoGreyPlus(int x)
    {
        if (x < 0) return new byte[3] { 0, 0, 0 };
        if (x > 4079) return new byte[3] { 255, 255, 255 };

        var i = x / 16;
        var j = x % 16;

        var k = new byte[16, 3] { { 0, 0, 0 }, { 0, 0, 1 }, { 0, 0, 2 }, { 1, 0, 0 }, { 1, 0, 1 }, { 1, 0, 1 }, { 1, 0, 2 }, { 2, 0, 0 }, { 2, 0, 1 }, { 2, 0, 2 }, { 2, 0, 2 }, { 0, 1, 0 }, { 0, 1, 1 }, { 0, 1, 2 }, { 0, 1, 2 }, { 1, 1, 0 } };
        var r = Math.Min(i + k[j, 0], 255);
        var g = Math.Min(i + k[j, 1], 255);
        var b = Math.Min(i + k[j, 2], 255);

        return new byte[3] { (byte)r, (byte)g, (byte)b };
    }
    public static double[,] MatrixArctangent(int n, int k)
    {
        var a = new double[n, n];

        for (int i = 0; i < n * k; i++)
            for (int j = 0; j < n * k; j++)
                a[i / k, j / k] += Math.Atan((i + 1.0) / (j + 1.0));

        var max = k * k * Math.PI / 2.0;
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                a[i, j] /= max;

        return a;
    }
    public static double[,] MatrixArctangent2(int n, int k)
    {
        var a = new double[n, n];
        var max = k * k * Math.PI / 2.0;
        Parallel.For(0, n, i =>
        {
            for (int j = 0; j < n; j++)
            {
                for (int x = 0; x < k; x++)
                    for (int y = 0; y < k; y++)
                        a[i, j] += Math.Atan((i * k + x + 1.0) / (j * k + y + 1.0));

                a[i, j] /= max;
            }
        });
        return a;
    }
    public static double[,] MatrixArctangent3(int n, int k)
    {
        var a = new double[n, n];
        var max = k * k * Math.PI / 2.0;

        var n8 = n / 8;

        Parallel.For(0, 8, pi =>
        {
            Parallel.For(0, 8, pj =>
            {
                var ii = pi * n8;
                var iii = ii + n8;
                for (int i = ii; i < iii; i++)
                {
                    var jj = pj * n8;
                    var jjj = jj + n8;
                    for (int j = jj; j < jjj; j++)
                    {
                        for (int x = 0; x < k; x++)
                            for (int y = 0; y < k; y++)
                                a[i, j] += Math.Atan((i * k + x + 1.0) / (j * k + y + 1.0));

                        a[i, j] /= max;
                    }
                }
            });
        });

        return a;
    }
}


