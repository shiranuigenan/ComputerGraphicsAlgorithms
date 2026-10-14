namespace ComputerGraphicsAlgorithms;

public class channelBasedImage
{
    public int width;
    public int height;

    public int[,] r;
    public int[,] g;
    public int[,] b;
    public int[,] a;
    public channelBasedImage()
    {
        width = 1;
        height = 1;

        r = new int[1, 1] { { 0xFEDCBA } };
        g = new int[1, 1] { { 0xA98765 } };
        b = new int[1, 1] { { 0x543210 } };
        a = new int[1, 1] { { 0xFFFFFF } };
    }
    public channelBasedImage(int width, int height)
    {
        this.width = width;
        this.height = height;

        r = new int[height, width];
        g = new int[height, width];
        b = new int[height, width];
        a = new int[height, width];
    }
    public channelBasedImage(NetPBM netPBM)
    {
        width = netPBM.Width;
        height = netPBM.Height;

        r = new int[height, width];
        g = new int[height, width];
        b = new int[height, width];
        a = new int[height, width];

        var bytes = netPBM.Pixels;

        if (netPBM.Format == NetPBMFormat.PM)
        {
            int k = 0;
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    b[i, j] = bytes[k++] * 0x10101;
                    g[i, j] = bytes[k++] * 0x10101;
                    r[i, j] = bytes[k++] * 0x10101;
                    a[i, j] = 0xFFFFFF;
                }
        }
    }
    public NetPBM convertBitmap()
    {
        var pixels = new byte[height * width * 3];

        int k = 0;
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                pixels[k++] = (byte)(b[i, j] >> 8);
                pixels[k++] = (byte)(g[i, j] >> 8);
                pixels[k++] = (byte)(r[i, j] >> 8);
            }

        return new NetPBM(NetPBMFormat.PM32, pixels, width, height);
    }
    public void Scale(byte ratio)
    {
        width /= ratio;
        height /= ratio;
        r = Common.Scale(r, ratio);
        g = Common.Scale(g, ratio);
        b = Common.Scale(b, ratio);
        a = Common.Scale(a, ratio);
    }
    public NetPBM convertBitmap24()
    {
        var pixels = new byte[height * width * 3];

        int k = 0;
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                pixels[k++] = (byte)(b[i, j] >> 16);
                pixels[k++] = (byte)(g[i, j] >> 16);
                pixels[k++] = (byte)(r[i, j] >> 16);
            }

        return new NetPBM(NetPBMFormat.PM, pixels, width, height);
    }
}
