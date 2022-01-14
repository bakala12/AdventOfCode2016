using System.IO;

namespace Day1
{
    public class Program
    {
        public static void Main()
        {
            var instructions = File.ReadAllText("input.txt").Split(", ").ToArray();
            Part12(instructions);
        }

        private static void Part12(string[] instructions)
        {
            var (x,y) = (0,0);
            var dir = Direction.North;
            var visited = new HashSet<(int,int)>();
            bool firstDoubleVisit = false;
            var (dx,dy) = (0,0);
            visited.Add((0,0));
            foreach(var instr in instructions)
            {
                var (px,py) = (x,y);
                (x,y,dir) = Move((x,y,dir), instr);
                if(px == x)
                {
                    for(int yy = Math.Min(y,py); yy <= Math.Max(y,py); yy++)
                    {
                        if(yy == py) continue;
                        if(!visited.Contains((x,yy)))
                           visited.Add((x,yy));
                        else if(!firstDoubleVisit)
                        {
                            firstDoubleVisit = true;
                            (dx,dy) = (x,yy);
                        }
                    }
                }
                else
                {
                    for(int xx = Math.Min(x,px); xx <= Math.Max(x,px); xx++)
                    {
                        if(xx == px) continue;
                        if(!visited.Contains((xx,y)))
                           visited.Add((xx,y));
                        else if(!firstDoubleVisit)
                        {
                            firstDoubleVisit = true;
                            (dx,dy) = (xx,y);
                        }
                    }
                }
            }
            Console.WriteLine(Math.Abs(x)+Math.Abs(y));
            Console.WriteLine(Math.Abs(dx)+Math.Abs(dy));
        }

        public enum Direction
        {
            North,
            South,
            East,
            West
        }

        private static (int, int, Direction) Move((int, int, Direction) state, string instruction)
        {
            var (x,y,dir) = state;
            var left = instruction[0] == 'L';
            var d = int.Parse(instruction.Substring(1));
            switch(dir)
            {
                case Direction.North:
                    dir = left ? Direction.West : Direction.East;
                    x += (left ? -1 : 1) * d;
                    break;
                case Direction.South:
                    dir = left ? Direction.East : Direction.West;
                    x += (left ? 1 : -1) * d;
                    break;
                case Direction.East:
                    dir = left ? Direction.North : Direction.South;
                    y += (left ? 1 : -1) * d;
                    break;
                case Direction.West:
                    dir = left ? Direction.South : Direction.North;
                    y += (left ? -1 : 1) * d;
                    break;
            }
            return (x,y,dir);
        }
    }
}