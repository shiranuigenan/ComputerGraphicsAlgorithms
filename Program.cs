using ComputerGraphicsAlgorithms;
using System.Collections;
using System.Diagnostics;

var a = new ushort[65538];
var b = new byte[65538];
var r = new Random();

for (int i = 0; i < a.Length; i++)
    a[i] = (ushort)i;
//r.NextBytes(a);

using (var fs = new FileStream("1.raw", FileMode.Create, FileAccess.Write))
{
    for (int i = 0; i < 45274; i++)
    {
        r.NextBytes(b);

        for (int j = 0; j < a.Length; j++)
        {
            fs.WriteByte((byte)a[j]);
            fs.WriteByte((byte)(a[j] >> 8));

            a[j] += b[j];
        }
    }
}
