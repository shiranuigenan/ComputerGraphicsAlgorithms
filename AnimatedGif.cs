using System;
using System.Drawing;
using System.IO;

namespace ComputerGraphicsAlgorithms
{
    public class AnimatedGif : IDisposable
    {
        FileStream s;
        int w, h;
        int bits = 8;
        byte delay = 2;
        public AnimatedGif(string file, int width, int height, byte[] palette, byte frameDelay = 2, int colorBits = 8)
        {
            w = width;
            h = height;
            s = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            s.Write(new byte[] { 71, 73, 70, 56, 57, 97 });
            delay = frameDelay;
            bits = colorBits;

            s.Write(new byte[] { (byte)w, (byte)(w >> 8), (byte)h, (byte)(h >> 8), (byte)(0xF0 | (bits - 1)), 0, 0 });
            s.Write(palette, 0, palette.Length);
            s.Write(new byte[] { 33, 255, 11, 78, 69, 84, 83, 67, 65, 80, 69, 50, 46, 48, 3, 1, 0, 0, 0 });
        }
        public void AddFrame(byte[] pixels)
        {
            s.Write(new byte[] { 33, 249, 4, 0, delay, 0, 255, 0, 44, 0, 0, 0, 0, (byte)w, (byte)(w >> 8), (byte)h, (byte)(h >> 8), 0 });
            (new LZWEncoder(w, h, pixels, bits)).Encode(s);
        }
        public void Dispose()
        {
            s.WriteByte(0x3b);
            s.Flush();
            s.Close();
        }
    }
}