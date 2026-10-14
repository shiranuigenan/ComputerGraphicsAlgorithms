namespace ComputerGraphicsAlgorithms;

public class LZWEncoder
{

    private static readonly int EOF = -1;

    private int imgW, imgH;
    private byte[] pixAry;
    private int initCodeSize;
    private int remaining;
    private int curPixel;

    static readonly int BITS = 12;

    static readonly int HSIZE = 5003; 

    int n_bits; 
    int maxbits = BITS; 
    int maxcode; 
    int maxmaxcode = 1 << BITS; 

    int[] htab = new int[HSIZE];
    int[] codetab = new int[HSIZE];

    int hsize = HSIZE; 

    int free_ent = 0; 

    bool clear_flg = false;

    int g_init_bits;

    int ClearCode;
    int EOFCode;

    int cur_accum = 0;
    int cur_bits = 0;

    int[] masks =
    {
            0x0000,
            0x0001,
            0x0003,
            0x0007,
            0x000F,
            0x001F,
            0x003F,
            0x007F,
            0x00FF,
            0x01FF,
            0x03FF,
            0x07FF,
            0x0FFF,
            0x1FFF,
            0x3FFF,
            0x7FFF,
            0xFFFF };

    int a_count;

    byte[] accum = new byte[256];

    public LZWEncoder(int width, int height, byte[] pixels, int color_depth)
    {
        imgW = width;
        imgH = height;
        pixAry = pixels;
        initCodeSize = Math.Max(2, color_depth);
    }

    void Add(byte c, Stream outs)
    {
        accum[a_count++] = c;
        if (a_count >= 254)
            Flush(outs);
    }

    void ClearTable(Stream outs)
    {
        ResetCodeTable(hsize);
        free_ent = ClearCode + 2;
        clear_flg = true;

        Output(ClearCode, outs);
    }

    void ResetCodeTable(int hsize)
    {
        for (int i = 0; i < hsize; ++i)
            htab[i] = -1;
    }

    void Compress(int init_bits, Stream outs)
    {
        int fcode;
        int i ;
        int c;
        int ent;
        int disp;
        int hsize_reg;
        int hshift;

        g_init_bits = init_bits;

        clear_flg = false;
        n_bits = g_init_bits;
        maxcode = MaxCode(n_bits);

        ClearCode = 1 << (init_bits - 1);
        EOFCode = ClearCode + 1;
        free_ent = ClearCode + 2;

        a_count = 0; 

        ent = NextPixel();

        hshift = 0;
        for (fcode = hsize; fcode < 65536; fcode *= 2)
            ++hshift;
        hshift = 8 - hshift; 

        hsize_reg = hsize;
        ResetCodeTable(hsize_reg); 

        Output(ClearCode, outs);

    outer_loop: while ((c = NextPixel()) != EOF)
        {
            fcode = (c << maxbits) + ent;
            i = (c << hshift) ^ ent; 

            if (htab[i] == fcode)
            {
                ent = codetab[i];
                continue;
            }
            else if (htab[i] >= 0) 
            {
                disp = hsize_reg - i; 
                if (i == 0)
                    disp = 1;
                do
                {
                    if ((i -= disp) < 0)
                        i += hsize_reg;

                    if (htab[i] == fcode)
                    {
                        ent = codetab[i];
                        goto outer_loop;
                    }
                } while (htab[i] >= 0);
            }
            Output(ent, outs);
            ent = c;
            if (free_ent < maxmaxcode)
            {
                codetab[i] = free_ent++; 
                htab[i] = fcode;
            }
            else
                ClearTable(outs);
        }
        Output(ent, outs);
        Output(EOFCode, outs);
    }
    public void Encode(Stream os)
    {
        os.WriteByte(Convert.ToByte(initCodeSize)); 

        remaining = imgW * imgH; 
        curPixel = 0;

        Compress(initCodeSize + 1, os); 

        os.WriteByte(0); 
    }
    void Flush(Stream outs)
    {
        if (a_count > 0)
        {
            outs.WriteByte(Convert.ToByte(a_count));
            outs.Write(accum, 0, a_count);
            a_count = 0;
        }
    }

    int MaxCode(int n_bits)
    {
        return (1 << n_bits) - 1;
    }

    private int NextPixel()
    {
        if (remaining == 0)
            return EOF;

        --remaining;

        byte pix = pixAry[curPixel++];

        return pix & 0xff;
    }

    void Output(int code, Stream outs)
    {
        cur_accum &= masks[cur_bits];

        if (cur_bits > 0)
            cur_accum |= (code << cur_bits);
        else
            cur_accum = code;

        cur_bits += n_bits;

        while (cur_bits >= 8)
        {
            Add((byte)(cur_accum & 0xff), outs);
            cur_accum >>= 8;
            cur_bits -= 8;
        }

        if (free_ent > maxcode || clear_flg)
        {
            if (clear_flg)
            {
                maxcode = MaxCode(n_bits = g_init_bits);
                clear_flg = false;
            }
            else
            {
                ++n_bits;
                if (n_bits == maxbits)
                    maxcode = maxmaxcode;
                else
                    maxcode = MaxCode(n_bits);
            }
        }

        if (code == EOFCode)
        {
            while (cur_bits > 0)
            {
                Add((byte)(cur_accum & 0xff), outs);
                cur_accum >>= 8;
                cur_bits -= 8;
            }

            Flush(outs);
        }
    }
}