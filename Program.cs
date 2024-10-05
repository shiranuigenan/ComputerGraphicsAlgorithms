using ComputerGraphicsAlgorithms;

imageGenerate.BrownianMotion();
return;

var im = 6075;
var ia = 106;
var ic = 1283;

var result = new List<Tuple<int, double, double>>();
for (int i = 0; i < im; i++)
{
    double t = 0.0, r = i, min = double.MaxValue, max = double.MinValue;
    for (int j = 0; j < im; j++)
    {
        t += r - (im - 1.0) / 2;
        r = (r * ia + ic) % im;
        if (t > max) max = t;
        if (t < min) min = t;
    }
    result.Add(new Tuple<int, double, double>(i, min, max));
    //Console.WriteLine($"{i}\t{min}\t{max}\t{t}");
}
var minimumDistance = result.OrderBy(x => x.Item2 * x.Item2 + x.Item3 * x.Item3).First();
var mDs = result.Where(x => x.Item2 == minimumDistance.Item2 && x.Item3 == minimumDistance.Item3).ToList();
Console.WriteLine($"minimumDistance {minimumDistance}");
for (int i = 0; i < mDs.Count; i++)
    Console.WriteLine($"{mDs[i]}");

/*
(1693, -81025, 81017)
(4443, -81025, 81017) 
*/
var x = new short[im];
var y = new short[im];

{
    double t = 0.0, r = 1693;
    for (int j = 0; j < im; j++)
    {
        t += r - (im - 1.0) / 2;
        x[j] = (short)(t / 10);
        r = (r * ia + ic) % im;
    }
}
{
    double t = 0.0, r = 4443;
    for (int j = 0; j < im; j++)
    {
        t += r - (im - 1.0) / 2;
        y[j] = (short)(t / 20);
        r = (r * ia + ic) % im;
    }
}
