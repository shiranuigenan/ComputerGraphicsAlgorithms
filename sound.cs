namespace ComputerGraphicsAlgorithms
{
    public static class sound
    {
        ///flac 1.raw -f --endian=little --channels=1 --bps=32 --sample-rate=655350 --sign=signed
        public static void Su32bit()
        {
            var r = new Random();
            using (var writer = new BinaryWriter(File.Open("1.raw", FileMode.Create)))
            {
                var max = 655350 * 30;
                for (long i = 0, k = 1, p = 0; i < max; i++, k *= -1, p = p + k * (r.Next() % (20000 - (i >> 10))))
                    writer.Write((int)(p));
                writer.Close();
            }
        }
        public static void bit8()
        {
            using (var writer = new BinaryWriter(File.Open("1.raw", FileMode.Create)))
            {
                var k = 1;
                for (int i = 0; i < 1073741824; i++)
                {
                    var p = ((i & (1 << 0)) >> 0) + 1;
                    p *= ((i & (1 << 01)) >> 01) + 1;
                    p *= ((i & (1 << 02)) >> 02) + 1;
                    p *= ((i & (1 << 03)) >> 03) + 1;
                    p *= ((i & (1 << 04)) >> 04) + 1;
                    p *= ((i & (1 << 05)) >> 05) + 1;
                    p *= ((i & (1 << 06)) >> 06) + 1;
                    p *= ((i & (1 << 07)) >> 07) + 1;
                    p *= ((i & (1 << 09)) >> 09) + 1;
                    p *= ((i & (1 << 10)) >> 10) + 1;
                    p *= ((i & (1 << 11)) >> 11) + 1;
                    p *= ((i & (1 << 12)) >> 12) + 1;
                    p *= ((i & (1 << 13)) >> 13) + 1;
                    p *= ((i & (1 << 14)) >> 14) + 1;
                    p *= ((i & (1 << 15)) >> 15) + 1;
                    p *= ((i & (1 << 16)) >> 16) + 1;
                    p *= ((i & (1 << 17)) >> 17) + 1;
                    p *= ((i & (1 << 18)) >> 18) + 1;
                    p *= ((i & (1 << 19)) >> 19) + 1;
                    p *= ((i & (1 << 20)) >> 20) + 1;
                    p *= ((i & (1 << 21)) >> 21) + 1;
                    p *= ((i & (1 << 22)) >> 22) + 1;
                    p *= ((i & (1 << 23)) >> 23) + 1;
                    p *= ((i & (1 << 24)) >> 24) + 1;
                    p *= ((i & (1 << 25)) >> 25) + 1;
                    p *= ((i & (1 << 26)) >> 26) + 1;
                    p *= ((i & (1 << 27)) >> 27) + 1;
                    p *= ((i & (1 << 28)) >> 28) + 1;
                    p *= ((i & (1 << 29)) >> 29) + 1;
                    writer.Write(k * p);
                    k *= -1;
                }
                writer.Close();
            }
        }
        public static void ChiuvFlac32bit()
        {
            using (var writer = new BinaryWriter(File.Open("1.raw", FileMode.Create)))
            {
                var p = 1;
                for (int i = 1; i < 2000; i++)
                {
                    var k = int.MinValue;
                    var a = uint.MaxValue / i;
                    for (int j = 0; j < i + 1; j++)
                    {
                        writer.Write(k / p++);
                        k = (int)(k + a);
                    }
                }
                writer.Close();
            }
        }
        public static byte[] whiteNoiseFlac24bit()
        {
            return intTo24bit(pseudoRandomNumberGenerator24bit());
        }
        public static byte[] pinkNoiseFlac24bit()
        {
            int bit = 23;
            var prn = pseudoRandomNumberGenerator24bit();
            var whiteNoise = new int[bit];
            var sampleCount = 1 << bit;
            var pinkNoise = new int[sampleCount];
            for (int i = 0, j = 0, k = 0, sum = 0, diff = 0, key = sampleCount - 1; i < sampleCount; i++, key++, sum = 0)
            {
                diff = (key + 1) ^ key;
                for (k = 0; k < bit; k++)
                {
                    if ((diff & (1 << k)) > 0)
                        whiteNoise[k] = prn[j++];
                    sum += whiteNoise[k];
                }
                pinkNoise[i] = (int)((sum - 77243565) / 13.114748f);
            }
            return intTo24bit(pinkNoise);
        }
        public static int[] pinkNoiseFlac32bit()
        {
            int bit = 23;
            var prn = pseudoRandomNumberGenerator24bit();
            var whiteNoise = new int[bit];
            var sampleCount = 1 << bit;
            var pinkNoise = new int[sampleCount];
            for (int i = 0, j = 0, k = 0, sum = 0, diff = 0, key = sampleCount - 1; i < sampleCount; i++, key++, sum = 0)
            {
                diff = (key + 1) ^ key;
                for (k = 0; k < bit; k++)
                {
                    if ((diff & (1 << k)) > 0)
                        whiteNoise[k] = prn[j++];
                    sum += whiteNoise[k];
                }
                pinkNoise[i] = (int)((sum - 77243565) / 13.114748f);
            }
            return pinkNoise;
        }
        public static int[] pinkNoiseF()
        {
            var r = new Random();
            int bit = 23;
            var prn = new byte[2147483591];
            r.NextBytes(prn);
            var whiteNoise = new int[bit];
            var sampleCount = 1 << bit;
            var pinkNoise = new int[sampleCount];
            for (int i = 0, j = 0, k = 0, sum = 0, diff = 0, key = sampleCount - 1; i < sampleCount; i++, key++, sum = 0)
            {
                diff = (key + 1) ^ key;
                for (k = 0; k < bit; k++)
                {
                    if ((diff & (1 << k)) > 0)
                        whiteNoise[k] = prn[j++];
                    sum += whiteNoise[k];
                }
                pinkNoise[i] = (int)((sum - 77243565) / 13.114748f);
            }
            return pinkNoise;
        }
        public static byte[] pinkNoiseFlac24bit2()
        {
            int bit = 23;
            var whiteNoise = new int[bit];
            var sampleCount = 1 << bit;
            var pinkNoise = new int[sampleCount];
            long rnd = 0;
            for (int i = 0, k = 0, sum = 0, diff = 0, key = sampleCount - 1; i < sampleCount; i++, key++, sum = 0)
            {
                diff = (key + 1) ^ key;
                for (k = 0; k < bit; k++)
                {
                    if ((diff & (1 << k)) > 0)
                    {
                        whiteNoise[k] = (int)rnd;
                        rnd = (1140671485 * rnd + 12820163) % 16777216;
                    }
                    sum += whiteNoise[k];
                }
                pinkNoise[i] = (int)((sum - 77243565) / 13.114748f);
            }
            return intTo24bit(pinkNoise);
        }
        public static int[] pseudoRandomNumberGenerator24bit()
        {
            var a = new int[16777216];
            long x = 0;
            for (int i = 0; i < 16777216; i++, x = (1140671485 * x + 12820163) % 16777216)
                a[i] = (int)x;
            return a;
        }
        public static uint[] pseudoRandomNumberGenerator32bit(uint seed)
        {
            int size = 1 << 27;
            var a = new uint[size];
            ulong x = seed;
            for (int i = 0; i < size; i++, x = (1664525 * x + 1013904223) % 4294967296)
                a[i] = (uint)x;
            return a;
        }
        public static byte[] intTo24bit(int[] a)
        {
            if (a == null || a.Length < 1)
                return null;

            var b = new byte[a.Length * 3];
            for (int i = 0, j = 0; i < a.Length; i++)
            {
                b[j++] = (byte)(a[i] >> 16);
                b[j++] = (byte)(a[i] >> 8);
                b[j++] = (byte)(a[i] >> 0);
            }
            return b;
        }
    }
}
