using System;
using System.IO;
using System.Numerics;

namespace Day13
{
    public class Program
    {
        public static void Main()
        {
            int input = int.Parse(File.ReadAllText("input.txt"));
            Console.WriteLine(Search(input, (31,39)));
            Console.WriteLine(Search(input, 50));
        }

        private static int Search(int input, (int, int) destination)
        {
            var visited = new HashSet<(int,int)>();
            var queue = new Queue<((int,int), int)>();
            queue.Enqueue(((1,1), 0));
            visited.Add((1,1));
            while(queue.Count > 0)
            {
                var ((x,y), s) = queue.Dequeue();
                if((x,y) == destination)
                    return s;
                foreach(var (xx,yy) in GetNeighbours(x,y).Where(n => IsOpenSpace(n.Item1, n.Item2, input)))
                {
                    if(!visited.Contains((xx,yy)))
                    {
                        visited.Add((xx,yy));
                        queue.Enqueue(((xx,yy), s+1));
                    }
                }
            }
            return 0;
        }

        private static int Search(int input, int limit)
        {
            var visited = new HashSet<(int,int)>();
            var queue = new Queue<((int,int), int)>();
            queue.Enqueue(((1,1), 0));
            visited.Add((1,1));
            int c = 0;
            while(queue.Count > 0)
            {
                var ((x,y), s) = queue.Dequeue();
                if(s > limit)
                    break;
                c++;
                foreach(var (xx,yy) in GetNeighbours(x,y).Where(n => IsOpenSpace(n.Item1, n.Item2, input)))
                {
                    if(!visited.Contains((xx,yy)))
                    {
                        visited.Add((xx,yy));
                        queue.Enqueue(((xx,yy), s+1));
                    }
                }
            }
            return c;
        }

        private static IEnumerable<(int,int)> GetNeighbours(int x, int y)
        {
            if(x > 0)
                yield return (x-1,y);
            if(y > 0)
                yield return (x, y-1);
            yield return (x+1,y);
            yield return (x, y+1);
        }

        private static bool IsOpenSpace(int x, int y, int input)
        {
            return BitOperations.PopCount((uint)(x*x+3*x+2*x*y+y+y*y+input)) % 2 == 0;
        }
    }
}