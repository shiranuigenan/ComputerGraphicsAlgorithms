using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerGraphicsAlgorithms
{
    public static class dithering
    {
        public static void FloydSteinberg()
        {
            var a = 1920;
            var b = new int[a, a];
            var x = new int[a, a];

            for (var i = 0; i < a; i++)
                for (var j = 0; j < a; j++)
                {
                    b[i, j] = (int)(i * 34.151120375190203230849400729547);
                    x[i, j] = b[i, j];
                }

            using (var bitmap = new Bitmap(a, a, PixelFormat.Format24bppRgb))
            {
                for (var i = 0; i < a; i++)
                    for (var j = 0; j < a; j++)
                    {
                        var t = (int)Math.Round(b[i, j] / 257.0);
                        x[i, j] = t;
                        bitmap.SetPixel(i, j, Color.FromArgb(t, t, t));
                    }

                bitmap.Save("a.png", ImageFormat.Png);
            }

            for (var j = 0; j < a; j++)
                for (var i = 0; i < a; i++)
                {
                    var c = ((int)Math.Round(b[i, j] / 65535.0)) * 65535;
                    var d = b[i, j] - c;
                    b[i, j] = c;

                    if (i + 1 < a)
                    {
                        b[i + 1, j] += d * 7 / 16;
                        if (j + 1 < a)
                            b[i + 1, j + 1] += d * 1 / 16;
                    }
                    if (j + 1 < a)
                    {
                        b[i, j + 1] += d * 5 / 16;
                        if (i > 0)
                            b[i - 1, j + 1] += d * 3 / 16;
                    }
                }

            using (var bitmap = new Bitmap(a, a, PixelFormat.Format24bppRgb))
            {
                for (var i = 0; i < a; i++)
                    for (var j = 0; j < a; j++)
                    {
                        var t = b[i, j] >> 8;
                        bitmap.SetPixel(i, j, Color.FromArgb(t, t, t));
                    }

                bitmap.Save("b.png", ImageFormat.Png);
            }
        }
    }
}
