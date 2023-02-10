using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComputerGraphicsAlgorithms
{
    public class Gif
    {
        public string Signature = "GIF";
        public string Version = "89a";
        public ushort Width;
        public ushort Height;

        //Bit 7	Local Color Table Flag
        //Bit 6	Interlace Flag
        //Bit 5	Sort Flag
        //Bits 4-3	Reserved
        //Bits 2-0	Size of Local Color Table Entry
        private byte Description;
        public int ColorBits
        {
            get { return Description & 0x7; }
            set { Description = (byte)((Description & ~0x7) | (value & 0x7)); }
        }
        public int Reserved
        {
            get { return (Description >> 3) & 0x3; }
            set { Description = (byte)((Description & ~(0x3 << 3)) | (value & 0x3) << 3); }
        }
        public bool SortFlag
        {
            get { return ((Description >> 5) & 1) == 1; }
            set { Description = (byte)((Description & ~(1 << 5)) | (value == true ? 1 : 0) << 5); }
        }
        public bool InterlaceFlag
        {
            get { return ((Description >> 6) & 1) == 1; }
            set { Description = (byte)((Description & ~(1 << 6)) | (value == true ? 1 : 0) << 6); }
        }
        public bool LocalColorTableFlag
        {
            get { return ((Description >> 7) & 1) == 1; }
            set { Description = (byte)((Description & ~(1 << 7)) | (value == true ? 1 : 0) << 7); }
        }
        public int ColorCount
        {
            get { return 1 << (ColorBits + 1); }
        }
        public byte BackgroundColorIndex;
        public byte AspectRatio;
        public byte[]? ColorTable;

        public List<byte[]>? Chunks;

        public bool LoadFile(string fileName)
        {
            try
            {
                using var b = new BinaryReader(File.OpenRead(fileName));

                Signature = System.Text.Encoding.Default.GetString(b.ReadBytes(3));
                if (Signature != "GIF")
                    throw new Exception("invalid signature");

                Version = System.Text.Encoding.Default.GetString(b.ReadBytes(3));
                if (Version != "89a")
                    throw new Exception("invalid version");

                Width = b.ReadUInt16();
                Height = b.ReadUInt16();

                Description = b.ReadByte();
                if (!LocalColorTableFlag)
                    throw new Exception("no color table");

                BackgroundColorIndex = b.ReadByte();
                AspectRatio = b.ReadByte();

                ColorTable = new byte[ColorCount * 3];
                b.Read(ColorTable);

                Chunks = new List<byte[]>();
                try
                {
                    while (true)
                    {
                        var ext = b.ReadByte();
                        if (ext == 33)
                        {
                            var label = b.ReadByte();
                            if (label == 249)
                            {
                                var str = b.ReadString();

                            }
                        }

                    }
                }
                catch (Exception) { }

                b.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
