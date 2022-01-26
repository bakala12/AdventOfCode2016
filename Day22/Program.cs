using System;
using System.IO;
using System.Linq;

namespace Day22
{
    public class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            var nodes = ParseInput(lines);
            Part1(nodes);
            Part2(nodes);
        }

        public readonly record struct Node(string Name, int Size, int Used, int Available, int X, int Y);

        private static Node[,] ParseInput(string[] lines)
        {
            var nodes = new List<Node>();
            for(int i = 2; i < lines.Length; i++)
            {
                var s = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var c = s[0].Split('-');
                var n = new Node(s[0], int.Parse(s[1].Trim('T')), int.Parse(s[2].Trim('T')), int.Parse(s[3].Trim('T')), int.Parse(c[1].Trim('x')), int.Parse(c[2].Trim('y')));
                nodes.Add(n);
            }
            int height = nodes.Max(n => n.Y) + 1;
            int width = nodes.Max(n => n.X) + 1;
            var nodeTab = new Node[height, width];
            foreach(var n in nodes)
                nodeTab[n.Y, n.X] = n;
            return nodeTab;
        }

        private static void Part1(Node[,] nodes)
        {
            int c = 0;
            foreach(var n1 in nodes)
                foreach(var n2 in nodes)
                    if(n1 != n2 && n1.Used != 0 && n1.Used <= n2.Available)
                        c++;
            Console.WriteLine(c);
        }
    
        private static void Part2(Node[,] nodes)
        {
            var hole = (-1, -1);
            foreach(var n in nodes)
                if(n.Used == 0)
                {
                    hole = (n.Y, n.X);
                    break;
                }
            var data = (0, nodes.GetLength(1)-1);
            var visited = new HashSet<((int, int), (int, int))>();
            var queue = new Queue<((int, int), (int, int), int)>();
            queue.Enqueue((hole, data, 0));
            visited.Add((hole, data));
            while(queue.Count > 0)
            {
                var (h, d, s) = queue.Dequeue();
                if(d == (0,0))
                {
                    Console.WriteLine(s);
                    return;
                }
                foreach(var (nh, nd) in GetHoleMove(h, d, nodes))
                {
                    if(!visited.Contains((nh, nd)))
                    {
                        visited.Add((nh, nd));    
                        queue.Enqueue((nh, nd, s+1));
                    }
                }
            }
        }

        private static void Print(Node[,] nodes, (int,int) hole, (int,int) data)
        {
            for(int y = 0; y < nodes.GetLength(0); y++)
            {
                for(int x = 0; x < nodes.GetLength(1); x++)
                {
                    if((y,x) == hole)
                        Console.Write('_');
                    else if((y,x) == data)
                        Console.Write('D');
                    else if(nodes[y,x].Used > 100)
                        Console.Write('X');
                    else
                        Console.Write('.');
                }
                Console.WriteLine();
            }
        }

        private static IEnumerable<((int, int), (int, int))> GetHoleMove((int, int) hole, (int, int) data, Node[,] nodes)
        {
            var (y,x) = hole;
            var move = ((0,0), (0,0));
            if(y > 0 && IsMovePossible(nodes, hole, (y-1, x), data, out move))
                yield return move;
            if(y < nodes.GetLength(0) - 1 && IsMovePossible(nodes, hole, (y+1, x), data, out move))
                yield return move;
            if(x > 0 && IsMovePossible(nodes, hole, (y, x-1), data, out move))
                yield return move;
            if(x < nodes.GetLength(1) - 1 && IsMovePossible(nodes, hole, (y, x+1), data, out move))
                yield return move;
        }

        private static bool IsMovePossible(Node[,] nodes, (int, int) hole, (int, int) to, (int,int) data, out ((int,int),(int,int)) move)
        {
            if(to == data)
                move = (to, hole);
            else
                move = (to, data);
            return nodes[to.Item1, to.Item2].Used < nodes[hole.Item1, hole.Item2].Size;
        }
    }
}