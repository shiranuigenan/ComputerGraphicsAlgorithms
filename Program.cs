using System.Drawing;
using System.Drawing.Imaging;
using static ComputerGraphicsAlgorithms.common;

namespace ComputerGraphicsAlgorithms;
class Program
{
    static void Main(string[] args)
    {
        var e = imageGenerate.EPowerXSquare(25216);
        var bitmap = pixelsToBitmap(e);
        bitmap.Save("e-power-x-square.png");
    }
}


