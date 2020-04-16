namespace ComputerGraphicsAlgorithms
{
    public static class sound
    {
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
