using System;
using System.IO;
using System.Collections.Generic;

namespace Day23
{
    public class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            Run(lines.ToList().ToArray(), 7);
            Run(lines.ToList().ToArray(), 12);
        }

        private static void Run(string[] lines, int aInit)
        {
            var registers = new Dictionary<char, long>()
            {
                {'a', aInit },
                {'b', 0 },
                {'c', 0 },
                {'d', 0 }
            };
            var instReplaces = new Dictionary<string, string>()
            {
                {"cpy", "jnz"}, 
                {"inc", "dec"}, 
                {"dec", "inc"}, 
                {"jnz", "cpy"}, 
                {"tgl", "inc"}, 
            };
            long val = 0, v = 0;
            int inst = 0;
            for(long i = 0; i < lines.Length; i++)
            {
                inst++;
                var s = lines[i].Split(" ");
                switch(s[0])
                {
                    case "cpy":
                        val = long.TryParse(s[1], out v) ? v : registers[s[1][0]];
                        registers[s[2][0]] = (long)val;
                        break;
                    case "inc":
                        registers[s[1][0]]++;
                        break;
                    case "dec":
                        registers[s[1][0]]--;
                        break;
                    case "jnz":
                        if((long.TryParse(s[1], out val) ? val : registers[s[1][0]]) != 0)
                            i += (long.TryParse(s[2], out v) ? v : registers[s[2][0]]) - 1;
                        break;
                    case "tgl":
                        val = long.TryParse(s[1], out v) ? v : registers[s[1][0]];
                        if(i+val >= 0 && i+val < lines.Length)
                        {
                            var ss = lines[i+val];
                            lines[i+val] = $"{instReplaces[ss.Substring(0,3)]}{ss.Substring(3)}";
                        }
                        break;
                }
            }
            Console.WriteLine(registers['a']);
        }
    }
}