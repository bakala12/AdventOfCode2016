using System;
using System.IO;
using System.Linq;

namespace Day20
{
    public partial class Program
    {
        public static void Main()
        {
            var ipRanges = File.ReadAllLines("input.txt").Select(l => {
                var s = l.Split("-");
                return (uint.Parse(s[0]), uint.Parse(s[1]));
            }).ToArray();
            RangeSet? set = null;
            foreach(var (min, max) in ipRanges)
                set = RangeSet.AddRange(set, min, max);
            Part1(set);
            Part2(set);
        }

        private static void Part1(RangeSet? set)
        {
            Console.WriteLine(set?.MinimalNotIncluded());
        }

        private static void Part2(RangeSet? set)
        {
            Console.WriteLine(set?.NotIncludedCount());
        }
    }
}