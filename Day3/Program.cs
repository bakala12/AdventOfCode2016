using System;
using System.IO;
using System.Linq;

namespace Day3
{
    public class Program
    {
        public static void Main()
        {
            var triangles = File.ReadAllLines("input.txt").Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).Select(x => (x[0], x[1], x[2])).ToArray();
            Part1(triangles);
            var triangles2 = TakeColumnTriangles(File.ReadAllLines("input.txt").SelectMany(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse))).ToArray();
            Part1(triangles2);
        }

        private static void Part1((int,int,int)[] triangles)
        {
            Console.WriteLine(triangles.Count(t => IsValid(t)));
        }

        private static bool IsValid((int a,int b, int c) triangle)
        {
            return triangle.a + triangle.b + triangle.c > 2*Math.Max(triangle.a, Math.Max(triangle.b, triangle.c));
        }

        private static IEnumerable<(int,int,int)> TakeColumnTriangles(IEnumerable<int> items)
        {
            int b = 0;
            int[] batch = new int[9];
            foreach(var i in items)
            {
                if(b < 9)
                    batch[b++] = i;
                if(b == 9)
                {
                    yield return (batch[0], batch[3], batch[6]);
                    yield return (batch[1], batch[4], batch[7]);
                    yield return (batch[2], batch[5], batch[8]);
                    b = 0;
                }
            }
        }
    }
}