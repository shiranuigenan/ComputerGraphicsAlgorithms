using System.Text;

namespace ComputerGraphicsAlgorithms;

public enum NetPBMFormat
{
    PBM,    // 1-bit
    PGM,    // 8-bit gray
    PM,     // 8-bit RGB
    PGM32,  // 16-bit gray
    PM32    // 16-bit RGB
}

public class NetPBM
{
    public int Width;
    public int Height;
    public byte[] Pixels;
    public byte[]? Palette;
    public NetPBMFormat Format;
    public int MaxValue;

    public NetPBM(NetPBMFormat format, int width, int height)
    {
        Format = format;
        Width = width;
        Height = height;
        MaxValue = MaxValueForFormat(format);
        Pixels = new byte[PixelDataSize(format, width, height)];
    }

    static int MaxValueForFormat(NetPBMFormat format)
    {
        if (format == NetPBMFormat.PBM) return 1;
        if (format == NetPBMFormat.PGM32 || format == NetPBMFormat.PM32) return 65535;
        return 255;
    }

    static int PixelDataSize(NetPBMFormat format, int width, int height)
    {
        switch (format)
        {
            case NetPBMFormat.PBM: return width * height / 8;
            case NetPBMFormat.PGM: return width * height;
            case NetPBMFormat.PM: return width * height * 3;
            case NetPBMFormat.PGM32: return width * height * 2;
            case NetPBMFormat.PM32: return width * height * 3;
        }
        return 0;
    }

    public NetPBM(NetPBMFormat format, byte[] pixels, int width, int height, byte[]? palette = null)
    {
        Format = format;
        Width = width;
        Height = height;
        MaxValue = MaxValueForFormat(format);
        Pixels = pixels;
        Palette = palette;
    }

    public NetPBM(byte[,] pixels)
    {
        Width = pixels.GetLength(1);
        Height = pixels.GetLength(0);
        Format = NetPBMFormat.PGM;
        MaxValue = 255;
        Pixels = new byte[Width * Height];
        int k = 0;
        for (int i = 0; i < Height; i++)
            for (int j = 0; j < Width; j++)
                Pixels[k++] = pixels[i, j];
    }

    public NetPBM(Common.Color24[,] pixels)
    {
        Width = pixels.GetLength(1);
        Height = pixels.GetLength(0);
        Format = NetPBMFormat.PM;
        MaxValue = 255;
        Pixels = new byte[Width * Height * 3];
        int k = 0;
        for (int i = 0; i < Height; i++)
            for (int j = 0; j < Width; j++)
            {
                Pixels[k++] = pixels[i, j].r;
                Pixels[k++] = pixels[i, j].g;
                Pixels[k++] = pixels[i, j].b;
            }
    }

    public NetPBM(Common.Color32[,] pixels)
    {
        Width = pixels.GetLength(1);
        Height = pixels.GetLength(0);
        Format = NetPBMFormat.PM;
        MaxValue = 255;
        Pixels = new byte[Width * Height * 3];
        int k = 0;
        for (int i = 0; i < Height; i++)
            for (int j = 0; j < Width; j++)
            {
                Pixels[k++] = pixels[i, j].r;
                Pixels[k++] = pixels[i, j].g;
                Pixels[k++] = pixels[i, j].b;
            }
    }

    public NetPBM(Common.Color48[,] pixels)
    {
        Width = pixels.GetLength(1);
        Height = pixels.GetLength(0);
        Format = NetPBMFormat.PM32;
        MaxValue = 65535;
        Pixels = new byte[Width * Height * 3];
        int k = 0;
        for (int i = 0; i < Height; i++)
            for (int j = 0; j < Width; j++)
            {
                Pixels[k++] = (byte)(pixels[i, j].r & 0xFF);
                Pixels[k++] = (byte)(pixels[i, j].r >> 8);
                Pixels[k++] = (byte)(pixels[i, j].g & 0xFF);
                Pixels[k++] = (byte)(pixels[i, j].g >> 8);
                Pixels[k++] = (byte)(pixels[i, j].b & 0xFF);
                Pixels[k++] = (byte)(pixels[i, j].b >> 8);
            }
    }

    public NetPBM(Common.Color64[,] pixels)
    {
        Width = pixels.GetLength(1);
        Height = pixels.GetLength(0);
        Format = NetPBMFormat.PM32;
        MaxValue = 65535;
        Pixels = new byte[Width * Height * 3];
        int k = 0;
        for (int i = 0; i < Height; i++)
            for (int j = 0; j < Width; j++)
            {
                Pixels[k++] = (byte)(pixels[i, j].r & 0xFF);
                Pixels[k++] = (byte)(pixels[i, j].r >> 8);
                Pixels[k++] = (byte)(pixels[i, j].g & 0xFF);
                Pixels[k++] = (byte)(pixels[i, j].g >> 8);
                Pixels[k++] = (byte)(pixels[i, j].b & 0xFF);
                Pixels[k++] = (byte)(pixels[i, j].b >> 8);
            }
    }

    public void SetPixel(int x, int y, byte value)
    {
        if (Format == NetPBMFormat.PBM)
        {
            int idx = y * Width + x;
            int byteIdx = idx / 8;
            int bitIdx = 7 - (idx % 8);
            if (value != 0)
                Pixels[byteIdx] |= (byte)(1 << bitIdx);
            else
                Pixels[byteIdx] &= (byte)~(1 << bitIdx);
        }
        else if (Format == NetPBMFormat.PGM)
            Pixels[y * Width + x] = value;
    }

    public void SetPixel(int x, int y, byte r, byte g, byte b)
    {
        int idx = y * Width * 3 + x * 3;
        Pixels[idx] = r;
        Pixels[idx + 1] = g;
        Pixels[idx + 2] = b;
    }

    public void SetPixel(int x, int y, ushort value)
    {
        if (Format == NetPBMFormat.PGM32)
        {
            int idx = y * Width * 2 + x * 2;
            Pixels[idx] = (byte)(value & 0xFF);
            Pixels[idx + 1] = (byte)(value >> 8);
        }
    }

    public void SetPixel(int x, int y, ushort r, ushort g, ushort b)
    {
        int idx = y * Width * 3 + x * 3;
        Pixels[idx] = (byte)(r & 0xFF);
        Pixels[idx + 1] = (byte)(r >> 8);
        Pixels[idx + 2] = (byte)(g & 0xFF);
        Pixels[idx + 3] = (byte)(g >> 8);
        Pixels[idx + 4] = (byte)(b & 0xFF);
        Pixels[idx + 5] = (byte)(b >> 8);
    }

    public void Fill(byte r, byte g, byte b)
    {
        if (Format == NetPBMFormat.PM)
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                {
                    int idx = i * Width * 3 + j * 3;
                    Pixels[idx] = r;
                    Pixels[idx + 1] = g;
                    Pixels[idx + 2] = b;
                }
        }
    }

    public void FlipX()
    {
        if (Format == NetPBMFormat.PM)
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width / 2; j++)
                {
                    int idx1 = i * Width * 3 + j * 3;
                    int idx2 = i * Width * 3 + (Width - 1 - j) * 3;
                    for (int c = 0; c < 3; c++)
                    {
                        var tmp = Pixels[idx1 + c];
                        Pixels[idx1 + c] = Pixels[idx2 + c];
                        Pixels[idx2 + c] = tmp;
                    }
                }
        }
    }

    public void CopyFrom(NetPBM src, int x, int y)
    {
        if (Format == NetPBMFormat.PM && src.Format == NetPBMFormat.PM)
        {
            for (int i = 0; i < src.Height; i++)
                for (int j = 0; j < src.Width; j++)
                {
                    int srcIdx = i * src.Width * 3 + j * 3;
                    int dstIdx = (y + i) * Width * 3 + (x + j) * 3;
                    Pixels[dstIdx] = src.Pixels[srcIdx];
                    Pixels[dstIdx + 1] = src.Pixels[srcIdx + 1];
                    Pixels[dstIdx + 2] = src.Pixels[srcIdx + 2];
                }
        }
    }

    public void Save(string path)
    {
        using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
        string magic = Format == NetPBMFormat.PM || Format == NetPBMFormat.PM32 ? "P3" : "P2";
        var header = Encoding.ASCII.GetBytes($"{magic}\n{Width} {Height}\n{MaxValue}\n");
        fs.Write(header);
        fs.Write(Pixels);
    }

    public static NetPBM Load(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        var magic = new byte[2];
        fs.ReadExactly(magic, 0, 2);
        int type = magic[1];
        int c;
        while ((c = fs.ReadByte()) != '\n' && c != '\r' && c != -1) ;
        var w = ReadNumber(fs);
        var h = ReadNumber(fs);
        var maxval = ReadNumber(fs);
        NetPBMFormat format;
        int bpp;
        if (type == '2')
        {
            if (maxval <= 255) { format = NetPBMFormat.PGM; bpp = 1; }
            else { format = NetPBMFormat.PGM32; bpp = 2; }
        }
        else if (type == '3')
        {
            if (maxval <= 255) { format = NetPBMFormat.PM; bpp = 3; }
            else { format = NetPBMFormat.PM32; bpp = 3; }
        }
        else
        {
            throw new NotSupportedException("P1 (ASCII) not supported");
        }
        var pixels = new byte[w * h * bpp];
        fs.ReadExactly(pixels, 0, pixels.Length);
        return new NetPBM(format, pixels, w, h);
    }

    static int ReadNumber(Stream s)
    {
        int n = 0;
        int c;
        while ((c = s.ReadByte()) != -1 && (c == ' ' || c == '\n' || c == '\r' || c == '\t')) ;
        while (c != -1 && c >= '0' && c <= '9')
        {
            n = n * 10 + (c - '0');
            c = s.ReadByte();
        }
        return n;
    }
}
