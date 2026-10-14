namespace ComputerGraphicsAlgorithms;

public class AnimatedGif : IDisposable
{
    readonly FileStream s;
    private readonly int w;
    private readonly int h;
    readonly int bits = 8;
    readonly byte delay = 2;
    public AnimatedGif(string file, int width, int height, byte[] palette, byte frameDelay = 2, int colorBits = 8)
    {
        w = width;
        h = height;
        s = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        s.Write("GIF89a"u8);
        delay = frameDelay;
        bits = colorBits;

        s.Write([(byte)w, (byte)(w >> 8), (byte)h, (byte)(h >> 8), (byte)(0xF0 | (bits - 1)), 0, 0]);
        s.Write(palette, 0, palette.Length);
        s.Write([33, 255, 11, 78, 69, 84, 83, 67, 65, 80, 69, 50, 46, 48, 3, 1, 0, 0, 0]);
    }
    public void AddFrame(byte[] pixels)
    {
        s.Write([33, 249, 4, 0, delay, 0, 255, 0, 44, 0, 0, 0, 0, (byte)w, (byte)(w >> 8), (byte)h, (byte)(h >> 8), 0]);
        new LZWEncoder(w, h, pixels, bits).Encode(s);
    }
    public void Dispose()
    {
        s.WriteByte(0x3b);
        s.Flush();
        s.Close();
    }
}