using System;
using System.IO;

namespace Day4
{
    public class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            Part12(lines);
        }

        private static void Part12(string[] lines)
        {
            var validLines = lines.Select(l => (l, IsValid(l))).Where(x => x.Item2 > 0).ToArray();
            Console.WriteLine(validLines.Sum(x => x.Item2));
            int co = 'z'-'a'+1;
            foreach(var validLine in validLines)
            {
                var sw = new StringWriter();
                var s = validLine.Item1.Split('-');
                for(int i = 0; i < s.Length - 1; i++)
                {    
                    foreach(var c in s[i])
                        sw.Write((char)((c - 'a' + validLine.Item2) % co + 'a'));
                    sw.Write(" ");
                }
                if(sw.ToString().StartsWith("northpole object storage"))
                {
                    Console.WriteLine(validLine.Item2);
                    break;
                }
            }

        }

        private static int IsValid(string line)
        {
            var split = line.Split('-');
            var counts = new Dictionary<char, int>();
            for(int i = 0; i < split.Length - 1; i++)
            {
                foreach(var c in split[i])
                {
                    if(counts.ContainsKey(c))
                        counts[c]++;
                    else
                        counts.Add(c,1);
                }
            }
            var ind = int.Parse(split[split.Length-1].Split('[')[0]);
            var controlString = split[split.Length-1].Substring(split[split.Length-1].Length-6, 5);
            var orderedCounts = counts.OrderByDescending(k => k, CountsComparer.Instance).Take(5).ToArray();
            for(int i = 0; i < 5; i++)
                if(orderedCounts[i].Key != controlString[i])
                    return 0;
            return ind;
        }

        private class CountsComparer : Comparer<KeyValuePair<char, int>>
        {
            public static readonly CountsComparer Instance = new CountsComparer();

            public override int Compare(KeyValuePair<char, int> k1, KeyValuePair<char, int> k2)
            {
                var v = k1.Value - k2.Value;
                if(v != 0)
                    return v;
                return k2.Key - k1.Key;
            }
        }
    }
}