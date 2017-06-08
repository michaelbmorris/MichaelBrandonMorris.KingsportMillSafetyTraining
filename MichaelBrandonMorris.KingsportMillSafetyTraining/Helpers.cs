using System;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    public class Helpers
    {
        public static Tuple<int, int> CantorInverse(int z)
        {
            var w = (int) Math.Floor((Math.Sqrt(8 * z + 1) - 1) / 2);
            var t = (w * w + w) / 2;
            var y = z - t;
            var x = w - y;
            return new Tuple<int, int>(x, y);
        }

        public static int CantorPair(int x, int y)
        {
            return (x + y) * (x + y + 1) / 2 + y;
        }
    }
}