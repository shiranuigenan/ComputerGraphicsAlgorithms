using ComputerGraphicsAlgorithms;
using System.Drawing;
using System.Drawing.Imaging;

var r = new Random();
var pixels = new common.Color24[17280, 30720];
var k = 0;
Parallel.For(0, 30720, i =>
{
    for (var x = 0; x < 255; x++)
    {
        k = r.Next(17281);
        for (var j = 0; j < k; j++)
            pixels[j, i].r++;

        k = r.Next(17281);
        for (var j = 0; j < k; j++)
            pixels[j, i].g++;

        k = r.Next(17281);
        for (var j = 0; j < k; j++)
            pixels[j, i].b++;
    }
});

var bitmap = common.pixelsToBitmap(pixels);
for (int i = 0; i < 10; i++)
    common.saveJpeg(bitmap, 10 + i * 10, i + ".jpg");