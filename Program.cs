using ComputerGraphicsAlgorithms;

var a = sound.pinkNoiseFlac32bit();

using (var writer = new BinaryWriter(File.Open("1.raw", FileMode.Create)))
{
    for (int i = 0; i < a.Length; i++)
        writer.Write(a[i]);
    writer.Close();
}
