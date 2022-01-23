using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace Day17
{
    public class Program
    {
        private static readonly MD5 md5 = MD5.Create();

        public static void Main()
        {
            var input = File.ReadAllText("input.txt");
            Part1(input);
            Part2(input);
        }

        private static void Part1(string input)
        {
            Console.WriteLine(FindPath(input));
        }

        private static void Part2(string input)
        {
            Console.WriteLine(FindPath2(input));
        }

        private static string GetHash(string input)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
                sb.Append(hashBytes[i].ToString("x2"));
            return sb.ToString();
        }

        private static bool IsDoorOpen(char c)
        {
            return c == 'b' || c == 'c' || c == 'd' || c == 'e' || c == 'f';
        }

        private static IEnumerable<(int, int, char)> GetNeighbours(int x, int y, string pathHash)
        {
            //up, down, left, right
            if(y < 3 && IsDoorOpen(pathHash[1]))
                yield return (x, y + 1, 'D');
            if(x < 3 && IsDoorOpen(pathHash[3]))
                yield return (x+1, y, 'R');
            if(x > 0 && IsDoorOpen(pathHash[2]))
                yield return (x-1, y, 'L');
            if(y > 0 && IsDoorOpen(pathHash[0]))
                yield return (x, y-1, 'U');
        }

        private static string FindPath(string input)
        {
            var queue = new Queue<(int, int, string)>();
            queue.Enqueue((0,0,string.Empty));
            while(queue.Count > 0)
            {
                var (x,y,path) = queue.Dequeue();
                if(x == 3 && y == 3)
                    return path;
                foreach(var (nextX, nextY, nextChar) in GetNeighbours(x,y, GetHash(input+path)))
                    queue.Enqueue((nextX, nextY, path + nextChar));
            }
            return string.Empty;
        }

        private static int FindPath2(string input)
        {
            var stack = new Stack<(int, int, string, int)>();
            stack.Push((0,0,string.Empty, 0));
            int best = -1;
            while(stack.Count > 0)
            {
                var (x,y,path, steps) = stack.Pop();
                if(x == 3 && y == 3)
                    best = Math.Max(best, steps);
                else
                {
                    foreach(var (nextX, nextY, nextChar) in GetNeighbours(x,y, GetHash(input+path)))
                        stack.Push((nextX, nextY, path + nextChar, steps+1));
                }
            }
            return best;
        }
    }
}