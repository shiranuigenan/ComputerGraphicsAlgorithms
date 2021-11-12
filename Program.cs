using System.Drawing.Imaging;
using ComputerGraphicsAlgorithms;
using static ComputerGraphicsAlgorithms.common;

var h = 2048;
var w = h * 45;

var a = new short[h, h];
for (int i = 0; i < w; i++)
    for (int j = 0; j < w; j++)
    {
        var x = i - (w - 1) / 2.0;
        var y = j - (w - 1) / 2.0;
        var z = (int)(9 + 18 * Math.Atan(x / y) / Math.PI);
        a[i / 45, j / 45] += (short)(z % 2);
    }

var b = new Color32[h, h];
for (int i = 0; i < h; i++)
    for (int j = 0; j < h; j++)
    {
        b[i, j].r = b[i, j].g = b[i, j].b = (byte)(1 + (a[i, j] >> 3));
        b[i, j].a = 255;
    }

var bitmap = common.pixelsToBitmap(b);
bitmap.Save("a.png", ImageFormat.Png);