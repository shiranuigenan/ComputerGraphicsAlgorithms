﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static ComputerGraphicsAlgorithms.common;

namespace ComputerGraphicsAlgorithms
{
    public static class imageGenerate
    {
        public static common.Color24[,] EPowerXSquare(int width)
        {
            int height = 9 * width / 16;
            var pixels = new common.Color24[height, width];

            for (int i = 0; i < width; i++)
            {
                double x = (i - width / 2) / (width / 6.0);
                int y = (int)((width / 6.0) / Math.Pow(Math.E, x * x));

                for (int j = (9 * width / 32) - y; j < (9 * width / 32) + y; j++)
                {
                    pixels[j, i].r = 255;
                    pixels[j, i].g = 255;
                    pixels[j, i].b = 255;
                }
            }

            return pixels;
        }
        public static common.Color24[,] FlagTurkiye(int yukseklik)
        {
            //Y	Yükseklik 1
            //A	Dış ay merkezinin uçkurluktan mesafesi	1/2 Y
            //B	Ayın dış dairesinin kutru (çapı)	1/2 Y
            //C	Ayın iç ve dış merkezleri arası	1/16 Y
            //D	Ayın iç dairesinin kutru (çapı)	0.4 Y
            //E	Yıldız dairesinin, ayın iç dairesinden olan mesafesi	1/3 Y
            //F	Yıldız dairesinin kutru (çapı)	1/4 Y
            //L	Genişlik 1.5 Y

            var genislik = 3 * yukseklik / 2;

            var n = 2; //super resolution katsayısı
            var Y = yukseklik * n;
            var A = Y / 2;
            var B = Y / 4;
            var C = Y / 16;
            var D = Y / 5;
            var E = Y / 3;
            var F = Y / 8;
            var L = 3 * Y / 2;
            var YILDIZ = 33 * Y / 40;


            var rnd = new Random();
            var alan = new int[yukseklik, genislik];
            for (int i = 0; i < Y; i++)
                for (int j = 0; j < L; j++)
                {
                    if (Math.Sqrt((i - A) * (i - A) + (j - A) * (j - A)) < B && Math.Sqrt((i - A) * (i - A) + (j - A - C) * (j - A - C)) > D)
                        alan[i / n, j / n] += 1;

                    if (Math.Sqrt((i - A) * (i - A) + (j - YILDIZ) * (j - YILDIZ)) < F && Math.Sqrt((i - A) * (i - A) + (j - YILDIZ) * (j - YILDIZ)) > 0.4 * F)
                        alan[i / n, j / n] += 1;

                }

            var pixels = new common.Color24[yukseklik, genislik];

            for (int i = 0; i < yukseklik; i++)
            {
                for (int j = 0; j < genislik; j++)
                {
                    var oran = 1.0 * alan[i, j] / (n * n);
                    pixels[i, j].r = (byte)(227 * (1.0 - oran) + oran * 255);
                    pixels[i, j].g = (byte)(10 * (1.0 - oran) + oran * 255);
                    pixels[i, j].b = (byte)(23 * (1.0 - oran) + oran * 255);
                }
            }

            return pixels;
        }
        public static common.Color24[,] PerfectBallQuarter()
        {
            var pixels = new common.Color24[4080, 4080];
            Parallel.For(0, 4080, i =>
            {
                for (var j = 0; j < 4080; j++)
                {
                    var b = 0;
                    for (var k = 0; k < 4080; k++)
                        if (i * i + j * j + k * k < 16638241)
                            b++;

                    pixels[j, i] = common.PsuedoGreyPlus24(b);
                }
            });

            return pixels;
        }
        public static void PerfectBallQuarterVector()
        {
            var bb = -1;
            for (var j = 0; j < 4081; j++)
            {
                var b = 0;
                for (var k = 0; k < 4081; k++)
                    if (j * j + k * k < 16646400)
                        b++;
                if (bb != b)
                {
                    bb = b;
                    var c = common.PsuedoGreyPlus(b);
                    Console.WriteLine(string.Format("<stop offset=\"{0:0.00}%\" style=\"stop-color:rgb({1},{2},{3})\" />", 100.0 * j / 4080.0, c.r, c.g, c.b));
                }
            }
        }
        public static common.Color32[,] PerfectBallQuarter2()
        {
            var pixels = new common.Color32[4096, 4096];
            Parallel.For(0, 4096, i =>
            {
                for (var j = 0; j < 4096; j++)
                {
                    var b = 0;
                    for (var k = 0; k < 4096; k++)
                        if (i * i + j * j + k * k < 16638241)
                            b++;

                    pixels[j, i] = common.PsuedoGreyPlus(b);
                }
            });

            return pixels;
        }
        public static common.Color48[,] XSquarePlusYSquare()
        {
            var pixels = new common.Color48[10240, 10240];

            Parallel.For(0, 5120, i =>
            {
                for (var j = 0; j < 5120; j++)
                {
                    var a = (i + 1) * (i + 1) + (j + 1) * (j + 1);
                    pixels[5119 - j, 5119 - i] = common.PsuedoGreyPlus48(a / 50);
                }
            });

            for (var i = 0; i < 5120; i++)
                for (var j = 0; j < 5120; j++)
                {
                    pixels[10239 - j, i] = pixels[j, i];
                    pixels[10239 - j, 10239 - i] = pixels[j, i];
                    pixels[j, 10239 - i] = pixels[j, i];
                }

            return pixels;
        }
        public static common.Color48[,] XMultiplyY()
        {
            var pixels = new common.Color48[10240, 10240];

            Parallel.For(0, 5120, i =>
            {
                for (var j = 0; j < 5120; j++)
                    pixels[j, i] = common.PsuedoGreyPlus48(((i + 1) * (j + 1) - 1) / 25);
            });

            for (var i = 0; i < 5120; i++)
                for (var j = 0; j < 5120; j++)
                {
                    pixels[10239 - j, i] = pixels[j, i];
                    pixels[10239 - j, 10239 - i] = pixels[j, i];
                    pixels[j, 10239 - i] = pixels[j, i];
                }

            return pixels;
        }
        public static void XModPlusYMod()
        {
            var a = new byte[10240 * 10240 * 3];
            var k = 0;
            for (int i = 0; i < 10240; i++)
                for (int j = 0; j < 10240; j++)
                {
                    var d = common.PsuedoGreyPlus24(i % 2040 + j % 2040);
                    a[k++] = d.r;
                    a[k++] = d.g;
                    a[k++] = d.b;
                }
            var b = common.bytesToBitmap24(a, 10240, 10240);
            common.saveJpeg(b, 100, "100.jpg");
        }
        public static void XPlusYMod()
        {
            var a = new byte[10240 * 10240 * 3];
            var k = 0;
            for (int i = 0; i < 10240; i++)
                for (int j = 0; j < 10240; j++)
                {
                    var d = common.PsuedoGreyPlus24((i + j) % 4080);
                    a[k++] = d.r;
                    a[k++] = d.g;
                    a[k++] = d.b;
                }
            var b = common.bytesToBitmap24(a, 10240, 10240);
            common.saveJpeg(b, 100, "100.jpg");
        }
        public static void PsuedoPerspective()
        {
            var a = new byte[25600 * 4096 * 3];
            var k = 0;
            for (int i = 0; i < 4096; i++)
                for (int j = 0; j < 25600; j++)
                {
                    var d = common.PsuedoGreyPlus24(((j + 1) % (i + 1)) % 4080);
                    a[k++] = d.r;
                    a[k++] = d.g;
                    a[k++] = d.b;
                }
            var b = common.bytesToBitmap24(a, 25600, 4096);
            common.saveJpeg(b, 100, "100.jpg");
        }
        public static void Arctangent()
        {
            var w = new BinaryWriter(File.Create("a.raw"));

            var a = new byte[16384 * 6];
            for (int i = 0; i < 16384; i++)
            {
                var ii = i - 8191.5;
                Parallel.For(0, 16384, j =>
                {
                    var c = (Math.Atan(ii / (j - 8191.5)) + Math.PI / 2) / Math.PI;
                    c = 1 - Math.Abs(2 * c - 1);
                    c = 1 - Math.Abs(2 * c - 1);
                    c = 1 - Math.Abs(2 * c - 1);
                    c = 1 - Math.Abs(2 * c - 1);
                    c = 1 - Math.Abs(2 * c - 1);
                    var d = common.PsuedoGreyPlus48((int)(1048576 * c));
                    a[6 * j + 0] = (byte)d.r;
                    a[6 * j + 1] = (byte)(d.r >> 8);
                    a[6 * j + 2] = (byte)d.g;
                    a[6 * j + 3] = (byte)(d.g >> 8);
                    a[6 * j + 4] = (byte)d.b;
                    a[6 * j + 5] = (byte)(d.b >> 8);
                });
                w.Write(a);
            }
            w.Close();
        }
        public static void Exponential()
        {
            var w = new BinaryWriter(File.Create("a.raw"));
            for (int i = 0; i < 300000; i++)
            {
                var f = Math.Pow(1.00004621098, i);
                var d = common.PsuedoGreyPlus48((int)f);
                w.Write(d.r);
                w.Write(d.g);
                w.Write(d.b);
            }
            w.Close();
        }
        public static void PinkNoise()
        {
            var r = new Random();

            var width = 28468;
            var height = 25145;
            var pixels = new Color24[height, width];

            var w = width;
            var h = height;

            var a00 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a00[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a01 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a01[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a02 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a02[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a03 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a03[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a04 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a04[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a05 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a05[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a06 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a06[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a07 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a07[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a08 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a08[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a09 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a09[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a10 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a10[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a11 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a11[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a12 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a12[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a13 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a13[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a14 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a14[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a15 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a15[i, j] = (byte)(r.Next() % 256);

            var p = Enumerable.Range(0, 4081).Select(x => PsuedoGreyPlus24(x)).ToArray();

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    int a = a00[i, j]
                          + a01[i / 2, j / 2]
                          + a02[i / 4, j / 4]
                          + a03[i / 8, j / 8]
                          + a04[i / 16, j / 16]
                          + a05[i / 32, j / 32]
                          + a06[i / 64, j / 64]
                          + a07[i / 128, j / 128]
                          + a08[i / 256, j / 256]
                          + a09[i / 512, j / 512]
                          + a10[i / 1024, j / 1024]
                          + a11[i / 2048, j / 2048]
                          + a12[i / 4096, j / 4096]
                          + a13[i / 8192, j / 8192]
                          + a14[i / 16384, j / 16384]
                          + a15[i / 32768, j / 32768];

                    pixels[i, j].r = p[a].r;
                    pixels[i, j].g = p[a].g;
                    pixels[i, j].b = p[a].b;
                }

            var b = pixelsToBitmap(pixels);

            for (int i = 0; i < 99; i++)
                saveJpeg(b, i + 2, (i + 1).ToString("D2") + ".jpg");
        }
        public static void PinkNoiseAdvanced()
        {
            var r = new Random();

            var width = 28468;
            var height = 25145;
            var ton = new ushort[height, width];
            var pixels = new Color24[height, width];

            var w = width;
            var h = height;

            var a00 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a00[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a01 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a01[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a02 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a02[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a03 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a03[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a04 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a04[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a05 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a05[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a06 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a06[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a07 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a07[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a08 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a08[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a09 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a09[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a10 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a10[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a11 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a11[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a12 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a12[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a13 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a13[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a14 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a14[i, j] = (byte)(r.Next() % 256);

            w = (w + 1) / 2; h = (h + 1) / 2;

            var a15 = new byte[h, w];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a15[i, j] = (byte)(r.Next() % 256);

            var p = Enumerable.Range(0, 4081).Select(x => PsuedoGreyPlus24(x)).ToArray();

            var k = new int[4081];

            int max = int.MinValue;
            int min = int.MaxValue;

            for (int i = 0; i < height * 2; i++)
                for (int j = 0; j < width * 2; j++)
                {
                    int a = (r.Next() % 256)
                          + a00[i / 2, j / 2]
                          + a01[i / 4, j / 4]
                          + a02[i / 8, j / 8]
                          + a03[i / 16, j / 16]
                          + a04[i / 32, j / 32]
                          + a05[i / 64, j / 64]
                          + a06[i / 128, j / 128]
                          + a07[i / 256, j / 256]
                          + a08[i / 512, j / 512]
                          + a09[i / 1024, j / 1024]
                          + a10[i / 2048, j / 2048]
                          + a11[i / 4096, j / 4096]
                          + a12[i / 8192, j / 8192]
                          + a13[i / 16384, j / 16384]
                          + a14[i / 32768, j / 32768]
                          + a15[i / 65536, j / 65536];

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

            var b = pixelsToBitmap(pixels);

            for (int i = 0; i < 99; i++)
                saveJpeg(b, i + 2, (i + 1).ToString("D2") + ".jpg");
        }
        public static void ZigZag()
        {
            var n = 32;

            var zigZag = new List<Tuple<int, int>>[2 * n - 1];
            for (int i = 0; i < zigZag.Length; i++)
                zigZag[i] = new List<Tuple<int, int>>();

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    zigZag[i + j].Add(Tuple.Create(i, j));

            var result = new Bitmap((n * n + 1) * n, n, PixelFormat.Format24bppRgb);
            var rg = Graphics.FromImage(result);
            rg.Clear(Color.White);

            var bitmap = new Bitmap(n, n, PixelFormat.Format24bppRgb);

            using (var g = Graphics.FromImage(bitmap))
                g.Clear(Color.White);

            var t = 0;
            for (int i = 0; i < zigZag.Length; i++)
                for (int j = 0; j < zigZag[i].Count; j++)
                {
                    bitmap.SetPixel(n - 1 - zigZag[i][j].Item1, n - 1 - zigZag[i][j].Item2, Color.Black);
                    t++;

                    if (t % 2 == 1)
                    {
                        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        rg.DrawImage(bitmap, t * n, 0);
                        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }
                    else
                        rg.DrawImage(bitmap, t * n, 0);
                }

            result.Save("result.png");
        }
        public static void XPowerX()
        {
            var m = 0.6922;
            var n = 15;
            var a = 16384;
            var an = a * n;
            var b = new byte[a, a];
            for (int i = 0; i < an; i++)
            {
                var c = 1.0 * i / an;
                var d = (int)(an * (Math.Pow(c, c) - m));
                for (int j = 0; j < d; j++)
                    b[i / n, j / n]++;
            }

            var w = new BinaryWriter(File.Create("a.raw"));
            for (int i = 0; i < a; i++)
                for (int j = 0; j < a; j++)
                    w.Write((byte)(17 * b[i, j] / 15));
            w.Close();
        }
        public static void Acceleration()
        {
            var b = Enumerable.Range(0, 4081).Select(x => PsuedoGreyPlus24(x)).ToArray();
            var w = 1044480;
            var h = 1;

            var inputArgs = $"-y -f rawvideo -pix_fmt rgb24 -s:v {w}x{h} -r 60 -i -";
            var outputArgs = "-c:v libx265 -preset ultrafast -crf 0 -vf scale=16384:9216 1.mp4";
            var p = new Process
            {
                StartInfo =
    {
        FileName = "ffmpeg.exe",
        Arguments = $"{inputArgs} {outputArgs}",
        UseShellExecute = false,
        CreateNoWindow = false,
        RedirectStandardInput = true
    }
            };

            p.Start();

            var t = 0;
            var a = new int[54000];
            for (int i = 0; i < 27000; i++)
            {
                t += i;
                a[i] = t;
            }
            for (int i = 0; i < 27000; i++)
            {
                t += 27000 - i;
                a[i + 27000] = t;
            }

            var ffmpegIn = p.StandardInput.BaseStream;
            var Data = new byte[w * h * 3];
            for (int i = 0; i < 54000; i++)
            {
                if (i % 5400 == 0)
                    Console.WriteLine(i / 5400);

                for (int j = 0; j < w * h; j++)
                {
                    var k = (a[i] + j) % 522240;
                    var c = b[k / 128];
                    Data[3 * j + 0] = c.r;
                    Data[3 * j + 1] = c.g;
                    Data[3 * j + 2] = c.b;
                }

                ffmpegIn.Write(Data);
                ffmpegIn.Flush();
            }
            ffmpegIn.Close();
            p.WaitForExit();
        }
        public static void AccelerationLine()
        {
            var w = 1044480;
            var h = 4;

            var inputArgs = $"-y -f rawvideo -pix_fmt rgb24 -s:v {w}x{h} -r 60 -i -";
            var outputArgs = "-c:v libx265 -preset ultrafast -crf 0 -vf scale=16384:9216 2.mp4";
            var p = new Process
            {
                StartInfo =
    {
        FileName = "ffmpeg.exe",
        Arguments = $"{inputArgs} {outputArgs}",
        UseShellExecute = false,
        CreateNoWindow = false,
        RedirectStandardInput = true
    }
            };

            p.Start();

            var a = new byte[8400 * 3];
            Array.Fill(a, (byte)255);
            var c = new byte[8400 * 3];
            for (int i = 0; i < 4200; i++)
            {
                var b = PsuedoGreyPlus24(i);
                c[3 * i + 0] = b.r;
                c[3 * i + 1] = b.g;
                c[3 * i + 2] = b.b;

                c[3 * (8399 - i) + 0] = b.r;
                c[3 * (8399 - i) + 1] = b.g;
                c[3 * (8399 - i) + 2] = b.b;
            }

            var ffmpegIn = p.StandardInput.BaseStream;
            var Data = new byte[w * 3];
            var k = 0;
            for (int i = 0; i < 1440; i++)
            {
                k += i;
                if (i % 145 == 0)
                    Console.WriteLine(i / 145);

                ffmpegIn.Write(Data, 0, k * 3);
                ffmpegIn.Write(c);
                ffmpegIn.Write(Data, 0, 1036080 * 3 - k * 3);

                ffmpegIn.Write(Data, 0, k * 3);
                ffmpegIn.Write(c);
                ffmpegIn.Write(Data, 0, 1036080 * 3 - k * 3);

                ffmpegIn.Write(Data, 0, k * 3);
                ffmpegIn.Write(a);
                ffmpegIn.Write(Data, 0, 1036080 * 3 - k * 3);

                ffmpegIn.Write(Data, 0, k * 3);
                ffmpegIn.Write(a);
                ffmpegIn.Write(Data, 0, 1036080 * 3 - k * 3);

                ffmpegIn.Flush();
            }
            ffmpegIn.Close();
            p.WaitForExit();
        }
        public static void LinearMovementX()
        {
            //pixabay max duration 4859 frame 60fps
            var inputArgs = $"-y -f rawvideo -pix_fmt gray -s:v 131072x1 -r 60 -i -";
            var outputArgs = "-c:v libx265 -preset ultrafast -vf scale=16384:9216 -x265-params lossless=1 lossless_16k_ultrafast.mp4";

            //var outputArgs = $"-c:v libsvtav1 -preset 12 -vf scale=7680:4320 -crf 1 libsvtav1_crf1_8k.mp4";
            //-sws_flags neighbor
            var p = new Process
            {
                StartInfo =
            {
                FileName = "ffmpeg.exe",
                Arguments = $"{inputArgs} {outputArgs}",
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardInput = true
            }
            };

            p.Start();

            var ffmpegIn = p.StandardInput.BaseStream;
            var a = new byte[131072];

            for (int i = 0; i < 131072; i++)
            {
                a[i] = 255;
                ffmpegIn.Write(a);
                ffmpegIn.Flush();
            }
            ffmpegIn.Close();
            p.WaitForExit();
        }
        public static void ColorRain()
        {
            //pixabay max duration 4859 frame 60fps
            var inputArgs = $"-y -f rawvideo -pix_fmt rgb48 -s:v 9x5 -r 60 -i -";
            var outputArgs = $"-c:v libx265 -preset ultrafast -vf scale=2430:1350 -x265-params lossless=1 9.mp4";

            //var outputArgs = $"-c:v libsvtav1 -preset 12 -vf scale=7680:4320 -crf 1 libsvtav1_crf1_8k.mp4";
            //-sws_flags neighbor
            var p = new Process
            {
                StartInfo =
            {
                FileName = "ffmpeg.exe",
                Arguments = $"{inputArgs} {outputArgs}",
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardInput = true
            }
            };

            p.Start();

            var ffmpegIn = p.StandardInput.BaseStream;
            var a = new ushort[84];
            var b = new byte[84];
            var r = new Random();

            for (int i = 0; i < 84; i++)
                a[i] = (ushort)r.Next(65536);

            for (int i = 0; i < 4859; i++)
            {
                //a[i] = 255;
                //a[i / 255] = (byte)(1 + (i % 255));

                r.NextBytes(b);
                for (var j = 0; j < 84; j++)
                {
                    a[j] += b[j];
                    ffmpegIn.WriteByte((byte)a[j]);
                    ffmpegIn.WriteByte((byte)(a[j] >> 8));
                }

                ffmpegIn.Flush();
            }
            ffmpegIn.Close();
            p.WaitForExit();
        }
        public static void PerfectRandom48()
        {
            var w = 9;
            var h = 5;

            var inputArgs = $"-y -f rawvideo -pix_fmt rgb48 -s:v {w}x{h} -r 60 -i -";
            //var outputArgs = $"-c:v libaom-av1 -colorspace bt2020nc -color_trc smpte2084 -color_primaries bt2020 -preset ultrafast -vf scale=1280:720 -pix_fmt yuv420p10le -crf 0 -sws_flags neighbor h.mp4";
            var outputArgs = $"-c:v flashsv2 -vf scale=3840:2160 -sws_flags neighbor flashsv2.mkv";
            //libx265
/*
ffmpeg -i <infile> \
-c:a copy \
-c:v libx265 \
-tag:v hvc1 \
-crf 22 \
-pix_fmt yuv420p10le \
-x265-params "colorprim=bt2020:transfer=smpte2084:colormatrix=bt2020nc" \
<outfile>.mkv
*/            
            
            var p = new Process
            {
                StartInfo =
            {
                FileName = "ffmpeg.exe",
                Arguments = $"{inputArgs} {outputArgs}",
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardInput = true
            }
            };

            p.Start();

            var ffmpegIn = p.StandardInput.BaseStream;

            var a = new ushort[w * h * 3];
            var b = new byte[w * h * 3];
            var r = new Random();

            for (int i = 0; i < a.Length; i++)
                a[i] = (ushort)r.Next(65536);

            for (int i = 0; i < 360; i++)
            {
                r.NextBytes(b);
                for (int j = 0; j < a.Length; j++)
                {
                    a[j] += b[j];

                    ffmpegIn.WriteByte((byte)a[j]);
                    ffmpegIn.WriteByte((byte)(a[j] >> 8));
                }
                ffmpegIn.Flush();
            }
            ffmpegIn.Close();
            p.WaitForExit();
        }
        public static void PerfectRandom16Gray()
        {
            var inputArgs = $"-y -f rawvideo -pix_fmt rgb48 -s:v 9x5 -r 60 -i -";
            var outputArgs = $"-sws_flags gauss -c:v libx265 -preset ultrafast -vf scale=16384:9216 -crf 0 gauss_crf0_16k.mp4";
            var p = new Process
            {
                StartInfo =
            {
                FileName = "ffmpeg.exe",
                Arguments = $"{inputArgs} {outputArgs}",
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardInput = true
            }
            };

            p.Start();

            var ffmpegIn = p.StandardInput.BaseStream;

            var a = new ushort[135];
            var b = new byte[135];
            var r = new Random();

            for (int i = 0; i < a.Length; i++)
                a[i] = (ushort)r.Next(65536);

            for (int i = 0; i < 4859; i++)
            {
                r.NextBytes(b);
                for (int j = 0; j < a.Length; j++)
                {
                    a[j] += b[j];

                    ffmpegIn.WriteByte((byte)a[j]);
                    ffmpegIn.WriteByte((byte)(a[j] >> 8));
                }
                ffmpegIn.Flush();
            }
            ffmpegIn.Close();
            p.WaitForExit();
        }
        public static void PerfectRandom8()
        {
            var w = 16;
            var h = 9 * w / 16;

            var inputArgs = $"-y -f rawvideo -pix_fmt rgb24 -s:v {w}x{h} -r 60 -i -";
            //var outputArgs = $"-c:v libx265 -preset ultrafast -vf scale=3840:2160 -crf 51 crf51_4k.mp4";
            var outputArgs = $"-c:v libsvtav1 -preset 12 -vf scale=7680:4320 -crf 31 crf31_8k.mp4";
            var p = new Process
            {
                StartInfo =
            {
                FileName = "ffmpeg.exe",
                Arguments = $"{inputArgs} {outputArgs}",
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardInput = true
            }
            };

            p.Start();

            var ffmpegIn = p.StandardInput.BaseStream;

            var a = new int[w * h];
            var b = new byte[w * h];
            var r = new Random();

            for (int j = 0; j < a.Length; j++)
                a[j] = r.Next() % 16777216;

            for (int i = 0; i < 5184; i++)
            {
                r.NextBytes(b);
                for (int j = 0; j < a.Length; j++)
                {
                    a[j] = (a[j] + (b[j] >> 7)) % 16777216;
                    ffmpegIn.WriteByte((byte)a[j]);
                    ffmpegIn.WriteByte((byte)(a[j] >> 8));
                    ffmpegIn.WriteByte((byte)(a[j] >> 16));
                }
                ffmpegIn.Flush();
            }
            ffmpegIn.Close();
            p.WaitForExit();
        }
        public static void PerfectRandom24()
        {
            var w = 9;
            var h = 5;

            var inputArgs = $"-y -f rawvideo -pix_fmt rgb24 -s:v 9x5 -r 60 -i -";
            var outputArgs = $"-c:v libx265 -preset ultrafast -x265-params lossless=1 -sws_flags neighbor -vf scale=240:135 1.mp4";
            var p = new Process
            {
                StartInfo =
            {
                FileName = "ffmpeg.exe",
                Arguments = $"{inputArgs} {outputArgs}",
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardInput = true
            }
            };

            p.Start();

            var ffmpegIn = p.StandardInput.BaseStream;

            var a = new byte[w * h * 3];
            var b = new byte[w * h * 3];
            var r = new Random();
            r.NextBytes(a);

            for (int i = 0; i < 3600; i++)
            {
                r.NextBytes(b);
                for (int j = 0; j < a.Length; j++)
                    a[j] += (byte)(b[j] >> 7);

                ffmpegIn.Write(a);
                ffmpegIn.Flush();
            }
            ffmpegIn.Close();
            p.WaitForExit();
        }
        public static void PinkNoise2()
        {
            var w = 524288;//524288
            var h = 294912;//294912

            var a = new List<List<byte[]>>();

            var bit = 20;
            var ww = w;
            var hh = h;
            for (int i = 1; i < bit; i++)
            {
                ww = (ww + 1) >> 1;
                hh = (hh + 1) >> 1;

                var b = new List<byte[]>();
                for (int j = 0; j < hh; j++)
                    b.Add(new byte[ww]);

                a.Add(b);
            }

            var p = new List<long[]>();
            for (int j = 0; j < h >> 4; j++)
                p.Add(new long[w >> 4]);

            var r = new Random();
            a.ForEach(x => x.ForEach(y => r.NextBytes(y)));

            for (int y = 0; y < h; y++)
            {
                if (y % 29492 == 0)
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + " " + y / 29492);

                for (int x = 0; x < w; x++)
                {
                    int sum = r.Next(256);
                    for (int j = 1; j < bit; j++)
                        sum += a[j - 1][y >> j][x >> j];
                    p[y >> 4][x >> 4] += sum;
                }
            }

            var max = long.MinValue;
            var min = long.MaxValue;

            for (int y = 0; y < h >> 4; y++)
                for (int x = 0; x < w >> 4; x++)
                {
                    if (p[y][x] > max) max = p[y][x];
                    if (p[y][x] < min) min = p[y][x];
                }

            for (int y = 0; y < h >> 4; y++)
                for (int x = 0; x < w >> 4; x++)
                    p[y][x] -= min;

            for (int y = 0; y < h >> 4; y++)
                for (int x = 0; x < w >> 4; x++)
                    p[y][x] = (long)(4080 * p[y][x] / (0.0 + max - min));

            var pixels = new Color24[h >> 4, w >> 4];
            var t = Enumerable.Range(0, 4081).Select(x => PsuedoGreyPlus24(x)).ToArray();

            for (int y = 0; y < h >> 4; y++)
                for (int x = 0; x < w >> 4; x++)
                {
                    pixels[y, x].r = t[p[y][x]].r;
                    pixels[y, x].g = t[p[y][x]].g;
                    pixels[y, x].b = t[p[y][x]].b;
                }

            var bm = pixelsToBitmap(pixels);

            for (int i = 0; i < 99; i++)
                saveJpeg(bm, i + 2, (i + 1).ToString("D2") + ".jpg");

        }
        public static void PinkNoiseAnimated()
        {
            var w = 16384;//524288
            var h = 9216;//294912

            var a = new List<List<byte[]>>();

            var bit = 15;
            var ww = w;
            var hh = h;
            for (int i = 1; i < bit; i++)
            {
                ww = (ww + 1) >> 1;
                hh = (hh + 1) >> 1;

                var b = new List<byte[]>();
                for (int j = 0; j < hh; j++)
                    b.Add(new byte[ww]);

                a.Add(b);
            }

            var inputArgs = $"-y -f rawvideo -pix_fmt rgb24 -s:v {w}x{h} -r 60 -i -";
            var outputArgs = $"-c:v libx265 -preset veryslow -crf 0 4.mp4";
            var p = new Process
            {
                StartInfo =
            {
                FileName = "ffmpeg.exe",
                Arguments = $"{inputArgs} {outputArgs}",
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardInput = true
            }
            };
            p.Start();

            var ffmpegIn = p.StandardInput.BaseStream;
            var d = Enumerable.Range(0, 4081).Select(x => PsuedoGreyPlus24(x)).ToArray();

            var r = new Random();
            var frameCount = 65536;
            for (int i = 0, diff, k, x, y, sum; i < frameCount; i++)
            {
                diff = (i - 1) ^ i;
                for (k = 1; k < bit; k++)
                    if ((diff & (1 << k)) > 0)
                        a[k - 1].ForEach(x => r.NextBytes(x));

                for (y = 0; y < h; y++)
                    for (x = 0; x < w; x++)
                    {
                        sum = r.Next(256);
                        for (int j = 1; j < bit; j++)
                            sum += a[j - 1][y >> j][x >> j];

                        ffmpegIn.WriteByte(d[sum].r);
                        ffmpegIn.WriteByte(d[sum].g);
                        ffmpegIn.WriteByte(d[sum].b);
                    }
                ffmpegIn.Flush();
            }
            ffmpegIn.Close();
            p.WaitForExit();
        }
        public static void LinearMovementVertical()
        {
            var inputArgs = $"-y -f rawvideo -pix_fmt gray -s:v 16384x9216 -r 60 -i -";
            var outputArgs = "-c:v libx265 -preset ultrafast -x265-params lossless=1 lossless_16k_ultrafast.mp4";

            var p = new Process
            {
                StartInfo =
            {
                FileName = "ffmpeg.exe",
                Arguments = $"{inputArgs} {outputArgs}",
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardInput = true
            }
            };

            p.Start();

            var ffmpegIn = p.StandardInput.BaseStream;
            var a = new byte[16384 * 9216];

            for (int i = 0; i < 4859; i++)
            {
                var t = 0;
                for (int j = 0; j < 9216; j++)
                {
                    var h = 16 * j;
                    for (int k = 0; k < 16384; k++)
                    {
                        if (h + 9 * k < i * 60.7015)
                            a[t++] = 255;
                        else
                            a[t++] = 0;
                    }
                }
                ffmpegIn.Write(a);
                ffmpegIn.Flush();
            }
            ffmpegIn.Close();
            p.WaitForExit();
        }
        public static Bitmap TestRGB()
        {
            var file = "full-rgb-test.png";

            if (File.Exists(file))
                return new Bitmap(file);
            else
            {
                var pixels = new byte[256 * 256 * 256 * 3];
                var k = 0;

                for (int i = 0; i < 4096; i++)
                    for (int j = 0; j < 4096; j++)
                    {
                        pixels[k++] = (byte)(16 * (i / 256) + j / 256);
                        pixels[k++] = (byte)i;
                        pixels[k++] = (byte)j;
                    }
                var bitmap = bytesToBitmap24(pixels, 4096, 4096);
                bitmap.Save(file);
                return bitmap;
            }
        }
        public static Bitmap Palette8BitToBitmap(byte[] palette)
        {
            var b = new Bitmap(16, 16, PixelFormat.Format24bppRgb);

            var k = 0;
            for (int i = 0; i < 16; i++)
                for (int j = 0; j < 16; j++)
                {
                    b.SetPixel(j, i, Color.FromArgb(palette[k + 0], palette[k + 1], palette[k + 2]));
                    k += 3;
                }

            return b;
        }
        public static byte[] PalettePsuedoGray()
        {
            var file = "psuedo-gray.pal";

            if (File.Exists(file))
                return File.ReadAllBytes(file);
            else
            {
                var palette = new byte[768];

                int k = 0;
                for (int i = 0; i < 256; i++)
                {
                    var t = PsuedoGrey(i);
                    palette[k++] = t.r;
                    palette[k++] = t.g;
                    palette[k++] = t.b;
                }

                File.WriteAllBytes(file, palette);

                return palette;
            }
        }
        public static byte[] PaletteGray256()
        {
            var file = "gray256.pal";

            if (File.Exists(file))
                return File.ReadAllBytes(file);
            else
            {
                var palette = new byte[768];

                int k = 0;
                for (int i = 0; i < 256; i++)
                {
                    var t = (byte)i;
                    palette[k++] = t;
                    palette[k++] = t;
                    palette[k++] = t;
                }

                File.WriteAllBytes(file, palette);

                return palette;
            }
        }
        public static byte[] Palette5173()
        {
            var file = "5173.pal";

            if (File.Exists(file))
                return File.ReadAllBytes(file);
            else
            {
                var palette = new byte[768];

                var ra = new byte[] { 0, 63, 127, 191, 255 };
                var ga = new byte[] { 0, 15, 31, 47, 63, 79, 95, 111, 127, 143, 159, 175, 191, 207, 223, 239, 255 };
                var ba = new byte[] { 0, 127, 255 };

                int k = 0;
                for (int r = 0; r < ra.Length; r++)
                    for (int g = 0; g < ga.Length; g++)
                        for (int b = 0; b < ba.Length; b++)
                        {
                            palette[k++] = ra[r];
                            palette[k++] = ga[g];
                            palette[k++] = ba[b];
                        }
                palette[k++] = 255;
                palette[k++] = 255;
                palette[k++] = 255;

                File.WriteAllBytes(file, palette);

                return palette;
            }
        }
        public static byte Palette5173(byte r, byte g, byte b)
        {
            var ri = (10 * r + 5) / 512;
            var gi = (34 * g + 17) / 512;
            var bi = (6 * b + 3) / 512;

            return (byte)(51 * ri + 3 * gi + bi);
        }
        public static byte[] Palette884()
        {
            var file = "884.pal";

            if (File.Exists(file))
                return File.ReadAllBytes(file);
            else
            {
                var palette = new byte[768];

                int k = 0;
                for (int r = 0; r < 8; r++)
                    for (int g = 0; g < 8; g++)
                        for (int b = 0; b < 4; b++)
                        {
                            palette[k++] = (byte)(r * 255 / 7);
                            palette[k++] = (byte)(g * 255 / 7);
                            palette[k++] = (byte)(b * 255 / 3);
                        }

                File.WriteAllBytes(file, palette);

                return palette;
            }
        }
        public static byte Palette884(byte r, byte g, byte b)
        {
            var ri = (16 * r + 8) / 512;
            var gi = (16 * g + 8) / 512;
            var bi = (8 * b + 4) / 512;

            return (byte)(32 * ri + 4 * gi + bi);
        }
        public static byte[] Palette666()
        {
            var file = "666.pal";

            if (File.Exists(file))
                return File.ReadAllBytes(file);
            else
            {
                var palette = new byte[768];

                int k = 0;
                for (int r = 0; r < 6; r++)
                    for (int g = 0; g < 6; g++)
                        for (int b = 0; b < 6; b++)
                        {
                            palette[k++] = (byte)(r * 255 / 5);
                            palette[k++] = (byte)(g * 255 / 5);
                            palette[k++] = (byte)(b * 255 / 5);
                        }

                File.WriteAllBytes(file, palette);

                return palette;
            }
        }
        public static byte Palette666(byte r, byte g, byte b)
        {
            var ri = (12 * r + 6) / 512;
            var gi = (12 * g + 6) / 512;
            var bi = (12 * b + 6) / 512;

            return (byte)(36 * ri + 6 * gi + bi);
        }
        public static byte[] Palette555()
        {
            var file = "555.pal";

            if (File.Exists(file))
                return File.ReadAllBytes(file);
            else
            {
                var palette = new byte[384];

                int k = 0;
                for (int r = 0; r < 5; r++)
                    for (int g = 0; g < 5; g++)
                        for (int b = 0; b < 5; b++)
                        {
                            palette[k++] = (byte)(r * 255 / 4);
                            palette[k++] = (byte)(g * 255 / 4);
                            palette[k++] = (byte)(b * 255 / 4);
                        }

                File.WriteAllBytes(file, palette);

                return palette;
            }
        }
        public static void GifSinusoidal()
        {
            //pixabay max 1000 frame
            var palette = PaletteGray256();

            int width = 1024;
            int height = 256;

            using (var gif = new AnimatedGif("sinusoidal-gray.gif", width, height, palette))
            {
                var pixels = new byte[width * height];
                var sins = new byte[width];
                for (int f = 0; f < width / 4; f++)
                {
                    var k = 0;
                    for (int j = 0; j < width; j++)
                        sins[j] = (byte)(255 * (Math.Sin(Math.PI * (j + 8 * f) / width) / 2 + 0.5));

                    for (int i = 0; i < height; i++)
                        for (int j = 0; j < width; j++)
                        {
                            pixels[k++] = (byte)(sins[j] - i);
                        }
                    gif.AddFrame(pixels);
                }
            }
        }
        public static void GifAcceleration()
        {
            //pixabay max 1000 frame
            var palette = new byte[2 * 3] { 0, 0, 0, 255, 255, 255 };
            //for (int i = 0; i < 256; i++)
            //{
            //    palette[3 * i + 0] = (byte)i;
            //    palette[3 * i + 1] = (byte)i;
            //    palette[3 * i + 2] = (byte)i;
            //}

            int width = 1000;
            int height = 64;

            using (var gif = new AnimatedGif("acceleration.gif", width, height, palette, 2, 1))
            {
                var pixels = new byte[width * height];
                for (int f = 0; f < 1000; f++)
                {
                    for (int i = 0; i < height; i++)
                        pixels[i * width + f] = 1;
                    gif.AddFrame(pixels);
                }
            }
        }
        public static void GifPlus()
        {
            var palette = Palette5173();

            int width = 256;
            int height = 256;

            using (var gif = new AnimatedGif("plus.gif", width, height, palette))
            {
                var pixels = new byte[width * height];

                for (int f = 0; f < 254; f++)
                {
                    var k = 0;
                    for (int i = 0; i < height; i++)
                        for (int j = 0; j < width; j++)
                            pixels[k++] = (byte)((k + f - 1) % 255);
                    gif.AddFrame(pixels);
                }
            }
        }
        public static void GifPsuedo()
        {
            var palette = new byte[768];
            var psuedoPalette = new byte[315];
            var k = 0;
            for (int i = 0; i < 315; i++)
            {
                var p = common.PsuedoGreyPlus24(i + 3766);

                var newColor = true;
                for (int j = 0; j < k; j++)
                    if (p.r == palette[3 * j + 0] && p.g == palette[3 * j + 1] && p.b == palette[3 * j + 2])
                    {
                        newColor = false;
                        break;
                    }

                if (newColor)
                {
                    palette[3 * k + 0] = p.r;
                    palette[3 * k + 1] = p.g;
                    palette[3 * k + 2] = p.b;
                    k++;
                }

                psuedoPalette[i] = (byte)(k - 1);
            }

            int width = 512;
            int height = 16;

            using (var gif = new AnimatedGif("psuedo.gif", width, height, palette))
            {
                var pixels = new byte[width * height];

                for (int f = 0; f < 254; f++)
                {
                    k = 0;
                    for (int i = 0; i < height; i++)
                        for (int j = 0; j < width; j++)
                            pixels[k++] = psuedoPalette[(i + j + f) % 315];
                    gif.AddFrame(pixels);
                }
            }
        }
        public static void GifFromFrames()
        {
            var palette = Palette555();

            int width = 270;
            int height = 480;

            using (var gif = new AnimatedGif("1.gif", width, height, palette, 5, 7))
            {
                var pixels = new byte[width * height];
                var path = @"C:\Users\Kenan\Desktop\d\ffmpeg-master-latest-win64-gpl\bin\{0:D4}.png";
                //var path = @"C:\Users\Kenan\Desktop\frames3\{0:D4}.bmp";
                for (int f = 1; f < 379; f++)
                {
                    using (var frame = new Bitmap(string.Format(path, 3 * f)))
                    {
                        var c = new channelBasedImage(frame);
                        c.Scale(9);
                        common.FloydSteinberg(c.r, x => x > 8388608 ? 16777215 : 0);
                        common.FloydSteinberg(c.g, x => x > 8388608 ? 16777215 : 0);
                        common.FloydSteinberg(c.b, x => x > 8388608 ? 16777215 : 0);

                        var k = 0;
                        for (int i = 0; i < height; i++)
                            for (int j = 0; j < width; j++)
                            {
                                byte p = 0;

                                if (c.b[2 * i + 0, 2 * j + 0] != 0) p++;
                                if (c.b[2 * i + 0, 2 * j + 1] != 0) p++;
                                if (c.b[2 * i + 1, 2 * j + 0] != 0) p++;
                                if (c.b[2 * i + 1, 2 * j + 1] != 0) p++;

                                if (c.g[2 * i + 0, 2 * j + 0] != 0) p += 5;
                                if (c.g[2 * i + 0, 2 * j + 1] != 0) p += 5;
                                if (c.g[2 * i + 1, 2 * j + 0] != 0) p += 5;
                                if (c.g[2 * i + 1, 2 * j + 1] != 0) p += 5;

                                if (c.r[2 * i + 0, 2 * j + 0] != 0) p += 25;
                                if (c.r[2 * i + 0, 2 * j + 1] != 0) p += 25;
                                if (c.r[2 * i + 1, 2 * j + 0] != 0) p += 25;
                                if (c.r[2 * i + 1, 2 * j + 1] != 0) p += 25;

                                pixels[k++] = p;
                            }

                        gif.AddFrame(pixels);
                    }
                }
            }
        }
        public static void GifFromFrames2()
        {
            var palette = new byte[24] {
                0,0,0,
                0,0,255,
                0,255,0,
                0,255,255,
                255,0,0,
                255,0,255,
                255,255,0,
                255,255,255
            };

            int width = 512;
            int height = 256;

            using (var gif = new AnimatedGif("1.gif", width, height, palette, 2, 3))
            {
                var pixels = new byte[width * height];
                var path = @"C:\Users\Kenan\Desktop\d\ffmpeg-master-latest-win64-gpl\bin\{0:D4}.bmp";
                //var path = @"C:\Users\Kenan\Desktop\frames3\{0:D4}.bmp";
                for (int f = 1; f < 501; f++)
                {
                    using (var frame = new Bitmap(string.Format(path, f)))
                    {
                        var c = new channelBasedImage(frame);
                        //c.Scale(3);
                        common.SimpleDither(c.r, x => x > 8388608 ? 16777215 : 0);
                        common.SimpleDither(c.g, x => x > 8388608 ? 16777215 : 0);
                        common.SimpleDither(c.b, x => x > 8388608 ? 16777215 : 0);

                        var k = 0;
                        for (int i = 0; i < height; i++)
                            for (int j = 0; j < width; j++)
                            {
                                byte p = 0;

                                if (c.b[i, j] != 0) p++;
                                if (c.g[i, j] != 0) p += 2;
                                if (c.r[i, j] != 0) p += 4;

                                pixels[k++] = p;
                            }

                        gif.AddFrame(pixels);
                    }
                }
            }
        }
        //public static void GifDiagonal()
        //{
        //    using var gif = AnimatedGif.AnimatedGif.Create("diagonal.gif", 20);

        //    int width = 512;
        //    int height = 512;
        //    var pixels = new Color24[width, height];

        //    for (int f = 0; f < 255; f++)
        //    {
        //        for (int i = 0; i < width / 2; i++)
        //            for (int j = 0; j < height / 2; j++)
        //            {
        //                var a = (byte)(i + j + f);

        //                pixels[i, j].r = a;
        //                pixels[i, j].g = a;
        //                pixels[i, j].b = a;

        //                pixels[width - i - 1, j].r = a;
        //                pixels[width - i - 1, j].g = a;
        //                pixels[width - i - 1, j].b = a;

        //                pixels[i, height - j - 1].r = a;
        //                pixels[i, height - j - 1].g = a;
        //                pixels[i, height - j - 1].b = a;

        //                pixels[width - i - 1, height - j - 1].r = a;
        //                pixels[width - i - 1, height - j - 1].g = a;
        //                pixels[width - i - 1, height - j - 1].b = a;
        //            }
        //        gif.AddFrame(pixelsToBitmap(pixels), quality: GifQuality.Grayscale);
        //    }
        //}
        public static common.Color24[,] W2560H1440()
        {
            var pixels = new common.Color24[1440, 2560];
            Parallel.For(0, 2560, i =>
            {
                for (var j = 0; j < 1440; j++)
                {
                    var k = (byte)((9 * i + 16 * j) >> 9);
                    pixels[j, i].r = k;
                    pixels[j, i].g = k;
                    pixels[j, i].b = k;
                    //pixels[j, i] = common.PsuedoGreyPlus24((9*i + 16*j) / 32);
                }
            });

            return pixels;
        }
        public static void RealGradient()
        {
            int w = 34 * 2560, h = 34 * 1440;

            var a = new int[h, w];

            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a[i, j] = (i * 16 + j * 9) << 1;

            common.MinimizedAverageError(a, x => x > 8388608 ? 16777215 : 0);
            var a2 = common.Scale(a, 34);

            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a[i, j] = (i * 16 + j * 9) << 2;

            common.MinimizedAverageError(a, x => x > 8388608 ? 16777215 : 0);
            var a3 = common.Scale(a, 34);

            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                    a[i, j] = (i * 16 + j * 9) << 3;

            common.MinimizedAverageError(a, x => x > 8388608 ? 16777215 : 0);
            var a4 = common.Scale(a, 34);

            var c = new channelBasedImage96(w / 34, h / 34);

            c.r = a2; c.g = a3; c.b = a4; var bgr = c.convertBitmap(); bgr.Save("bgr.png");//common.saveJpeg(bgr, 100, "bgr.jpg");
            c.r = a2; c.g = a4; c.b = a3; var gbr = c.convertBitmap(); gbr.Save("gbr.png");//common.saveJpeg(gbr, 100, "gbr.jpg");
            c.r = a3; c.g = a2; c.b = a4; var brg = c.convertBitmap(); brg.Save("brg.png");//common.saveJpeg(brg, 100, "brg.jpg");
            c.r = a3; c.g = a4; c.b = a2; var grb = c.convertBitmap(); grb.Save("grb.png");//common.saveJpeg(grb, 100, "grb.jpg");
            c.r = a4; c.g = a2; c.b = a3; var rbg = c.convertBitmap(); rbg.Save("rbg.png");//common.saveJpeg(rbg, 100, "rbg.jpg");
            c.r = a4; c.g = a3; c.b = a2; var rgb = c.convertBitmap(); rgb.Save("rgb.png");//common.saveJpeg(rgb, 100, "rgb.jpg");
        }
        public static void Gigapixel17()
        {
            int w = 10922 * 16, h = 10922 * 9;
            var a = new int[h, w];

            for (int i = 0; i < h; i++)
            {
                if (i % 9830 == 0)
                    Console.WriteLine(i / 9830);

                for (int j = 0; j < w; j++)
                    a[i, j] += i * 16 + j * 9;
            }

            var max = a[h - 1, w - 1];

            common.MinimizedAverageError(a, x => x > (max >> 1) ? max : 0);
            var w8 = w / 8;
            var b = new byte[h * w8];
            int k = 0, p = 0;
            for (int i = 0; i < h; i++)
                for (int j = 0; j < w; j++)
                {
                    if (a[i, j] != 0)
                        b[k] += (byte)(1 << (7 - p));
                    p++;
                    if (p == 8)
                    {
                        p = 0;
                        k++;
                    }
                }

            var bitmap = common.bitsToBitmap(b, w, h);
            bitmap.Save("3.tiff", ImageFormat.Tiff);
        }
    }
}
