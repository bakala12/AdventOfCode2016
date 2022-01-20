using System;
using System.IO;
using System.Collections.Generic;

namespace Day12
{
    public class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            Run(lines, 0);
            Run(lines, 1);
        }

        private static void Run(string[] lines, int cInit)
        {
            var registers = new Dictionary<char, int>()
            {
                {'a', 0 },
                {'b', 0 },
                {'c', cInit },
                {'d', 0 }
            };
            for(int i = 0; i < lines.Length; i++)
            {
                var s = lines[i].Split(" ");
                switch(s[0])
                {
                    case "cpy":
                        var val = int.TryParse(s[1], out int v) ? v : registers[s[1][0]];
                        registers[s[2][0]] = val;
                        break;
                    case "inc":
                        registers[s[1][0]]++;
                        break;
                    case "dec":
                        registers[s[1][0]]--;
                        break;
                    case "jnz":
                        if((int.TryParse(s[1], out int v1) ? v1 : registers[s[1][0]]) != 0)
                            i += int.Parse(s[2]) - 1;
                        break;
                }
            }
            Console.WriteLine(registers['a']);
        }
    }
}