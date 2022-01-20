using System.Numerics;

namespace Day11
{
    public static class BitsHelper
    {
        public static List<int> GetAllSetBits(this int integer)
        {
            var list = new List<int>();
            while(integer != 0)
            {
                var lsb = BitOperations.TrailingZeroCount(integer);
                list.Add(lsb);
                integer &= (integer-1);
            }
            return list;
        }
    }
}