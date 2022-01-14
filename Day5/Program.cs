using System;
using System.IO;

namespace Day5
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllText("input.txt");
            Part1(input);
            Part2(input);
        }

        private static void Part1(string input)
        {
            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            var count = 0;
            for(int i = 1; i < int.MaxValue; i++)
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes($"{input}{i}");
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                if(hashBytes[0] == 0 && hashBytes[1] == 0 && (hashBytes[2] >> 4) == 0)
                {
                    Console.Write(hashBytes[2].ToString("X2")[1]);
                    count++;
                    if(count == 8) break;
                }
            }
            Console.WriteLine();
        }

        private static void Part2(string input)
        {
            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            var count = 0;
            char[] password = new char[8];
            bool[] visited = new bool[8];
            for(int i = 1; i < int.MaxValue; i++)
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes($"{input}{i}");
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                if(hashBytes[0] == 0 && hashBytes[1] == 0 && (hashBytes[2] >> 4) == 0)
                {
                    var pos = hashBytes[2] & 0xF;
                    var v = hashBytes[3].ToString("X2")[0];
                    if(pos >= 0 && pos < 8 && !visited[pos])
                    {
                        visited[pos] = true;
                        password[pos] = v;
                        count++;
                        if(count == 8) break;
                    }
                }
            }
            Console.WriteLine(new string(password));
        }
    }
}