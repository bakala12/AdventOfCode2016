using System;
using System.IO;

namespace Day15
{
    public class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            var disks = ParseInput(lines);
            Part1(disks);
            Part2(disks);
        }

        private static void Part1(Disk[] disks)
        {
            var congruences = disks.Select((d,i) => new Congruence((d.Count - ((d.Initial + i + 1) % d.Count)) % d.Count, d.Count)).ToArray();
            Console.WriteLine(ChineseRemainderTheorem.Solve(congruences));
        }

        private static void Part2(Disk[] disks)
        {
            var dd = disks.ToList();
            dd.Add(new Disk(0, 11));
            var congruences = dd.Select((d,i) => new Congruence((d.Count - ((d.Initial + i + 1) % d.Count)) % d.Count, d.Count)).ToArray();
            Console.WriteLine(ChineseRemainderTheorem.Solve(congruences));
        }

        private static Disk[] ParseInput(string[] lines)
        {
            Disk[] disks = new Disk[lines.Length];
            for(int i = 0; i < lines.Length; i++)
            {
                var s = lines[i].Split(" ");
                disks[i] = new Disk(int.Parse(s[11].Trim('.')), int.Parse(s[3]));
            }
            return disks;
        }
    }

    public record struct Disk(int Initial, int Count);
}