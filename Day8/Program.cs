using System;
using System.IO;

namespace Day8
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
            bool[,] screen = new bool[6,50];
            foreach(var line in lines)
            {
                if(line.StartsWith("rect"))
                {
                    var s = line.Substring(5).Split('x').Select(int.Parse).ToArray();
                    for(int j = 0; j < s[0]; j++)
                        for(int i = 0; i < s[1]; i++)
                            screen[i,j] = true;
                }
                else if(line.StartsWith("rotate row"))
                {
                    var s = line.Substring(13).Split(" by ").Select(int.Parse).ToArray();
                    var row = new bool[50];
                    for(int i = 0; i < 50; i++)
                        row[(i+s[1])%50] = screen[s[0], i];
                    for(int i = 0; i < 50; i++)
                        screen[s[0], i] = row[i];
                }
                else if(line.StartsWith("rotate column"))
                {
                    var s = line.Substring(16).Split(" by ").Select(int.Parse).ToArray();
                    var column = new bool[6];
                    for(int i = 0; i < 6; i++)
                        column[(i+s[1])%6] = screen[i, s[0]];
                    for(int i = 0; i < 6; i++)
                        screen[i, s[0]] = column[i];
                }
            } 
            int c = 0;
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 50; j++)
                {
                    c += (screen[i,j] ? 1 : 0);
                    Console.Write(screen[i,j] ? '#' : '.');
                }
                Console.WriteLine();
            }
            Console.WriteLine(c);
        }
    }
}