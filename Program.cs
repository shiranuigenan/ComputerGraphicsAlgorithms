using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using static ComputerGraphicsAlgorithms.common;

namespace ComputerGraphicsAlgorithms;
class Program
{
    static void Main(string[] args)
    {
        var x = 4096;
        var p = new byte[x * x * 72];
        var r = new Random();
        r.NextBytes(p);
        var b = bytesToBitmap4bit(p, x * 16, x * 9);
        b.Save("1.png");
    }
}
