using System;
using System.IO;

namespace Day6
{
    public class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            Part1(lines);
        }

        private static void Part1(string[] lines)
        {
            var counts = new Dictionary<char, int>[lines[0].Length];
            for(int i = 0; i < lines[0].Length; i++)
                counts[i] = new Dictionary<char, int>();
            foreach(var line in lines)
            {
                for(int i = 0; i < line.Length; i++)
                {
                    if(counts[i].TryGetValue(line[i], out int x))
                        counts[i][line[i]]++;
                    else
                        counts[i].Add(line[i], 1);
                }
            }
            foreach(var countDic in counts)
                Console.Write(countDic.OrderByDescending(k => k.Value).First().Key);
            Console.WriteLine();
            foreach(var countDic in counts)
                Console.Write(countDic.OrderBy(k => k.Value).First().Key);
            Console.WriteLine();
        }
    }
}