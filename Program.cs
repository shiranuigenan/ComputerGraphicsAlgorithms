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
        var b = Enumerable.Range(0, 4081).Select(x => PsuedoGreyPlus24(x)).ToArray();
        var w = new BinaryWriter(File.Create("a.raw"));
        for (int i = 0; i < 4082; i++)
        {
            if (i % 408 == 0)
                Console.WriteLine((i / 408) + " " + DateTime.Now);

            for (int j = 0; j < 2304; j++)
                for (int k = 0; k < 4096; k++)
                {
                    var p = (i + j + k) % 4081;
                    w.Write(b[p].r);
                    w.Write(b[p].g);
                    w.Write(b[p].b);
                }
        }
        w.Close();

        //var a = frekans(26291, 4294967295);
        //var aa = a.Take(345600000).Select(x => (int)(x - 2147483648l)).ToArray();
        //var w = new BinaryWriter(File.Create("a.raw"));
        //Array.ForEach(aa, x => w.Write(x));
        //w.Close();

        //var a = frekans(351, 1048560);
        //var b = a.Select(x => common.PsuedoGreyPlus48((int)x)).ToArray();

        //var w = new BinaryWriter(File.Create("a.raw"));
        //for (int i = 0; i < 54000; i++)
        //{
        //    if (i % 5400 == 0)
        //        Console.WriteLine((i / 5400) + " " + DateTime.Now);

        //    for (int j = 0; j < 7680; j++)
        //    {
        //        w.Write(b[i + j].r);
        //        w.Write(b[i + j].g);
        //        w.Write(b[i + j].b);
        //    }

        //    w.Flush();
        //}
        //w.Close();
    }
    //en fazla 47 bit'lik genlikte, frekansı düşen dalga üretir
    static long[] frekans(int dalga = 46340, long genlik = 140737488355327)
    {
        if (dalga > 46340)
            dalga = 46340;

        var result = new long[dalga * (dalga + 1) / 2];
        var k = 0;
        for (int i = 0, t = 0; i < dalga; i++, t = 1 - t)
        {
            result[k++] = t * genlik;
            for (int j = 0; j < i; j++)
                result[k++] = genlik * (t * (i - 2 * j - 1) + j + 1) / (i + 1);
        }
        return result;
    }
    static void lineToPng(ushort[] line, string fileName)
    {
        var a = new ushort[1024];
        for (int i = 0; i < 1024; i++)
        {
            var ii = i * 16;
            var sum = 0;
            for (int j = 0; j < 16; j++)
                sum += line[ii + j];
            a[i] = (ushort)(sum >> 4);
        }

        var b = new Color48[256, 1024];
        for (int i = 0; i < 1024; i++)
            for (int j = 0; j < 256; j++)
            {
                b[j, i].r = a[i];
                b[j, i].g = a[i];
                b[j, i].b = a[i];
            }

        using var bitmap = common.pixelsToBitmap(b);
        bitmap.Save(fileName);
    }
}
