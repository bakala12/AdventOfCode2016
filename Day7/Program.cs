using System;
using System.IO;
using System.Linq;

namespace Day7
{
    public class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            Part1(lines);
            Part2(lines);
        }

        private static void Part1(string[] lines)
        {
            Console.WriteLine(lines.Count(l => IsSupportingTls(l)));
        }

        private static void Part2(string[] lines)
        {
            Console.WriteLine(lines.Count(l => IsSupportingSsl(l)));
        }

        private static bool IsSupportingTls(string line)
        {
            bool inside = false;
            bool contains = false;
            for(int i = 1; i < line.Length-2; i++)
            {
                if(line[i] == '[')
                    inside = true;
                else if(line[i] == ']')
                    inside = false;
                else if(!inside && contains) continue;
                else if(line[i] == line[i+1] && line[i-1] == line[i+2] && line[i] != line[i-1])
                {
                    if(inside) return false;
                    contains = true;
                }
            }
            return contains;
        }

        private static bool IsSupportingSsl(string line)
        {
            var outsideSet = new HashSet<(char, char, char)>();
            var insideSet = new HashSet<(char, char, char)>();
            bool inside = false;
            for(int i = 1; i < line.Length - 1; i++)
            {
                if(line[i] == '[')
                    inside = true;
                else if(line[i] == ']')
                    inside = false;
                else if(line[i-1] == line[i+1] && line[i-1] != line[i])
                    (inside ?insideSet : outsideSet).Add((line[i-1], line[i], line[i+1]));
            }
            return insideSet.Any(i => outsideSet.Contains((i.Item2, i.Item1, i.Item2)));
        }
    }
}