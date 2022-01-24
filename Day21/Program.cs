using System;
using System.IO;

namespace Day21
{
    public class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            Part1(lines, "abcdefgh");
            Part2(lines, "fbgdceah");
        }

        private static void Part1(string[] lines, string password)
        {
            var pb = new PasswordBuilder(password);
            foreach(var l in lines)
            {
                var s = l.Split();
                if(s[0] == "swap" && s[1] == "position")
                    pb.SwapPosition(int.Parse(s[2]), int.Parse(s[5]));
                else if(s[0] == "swap" && s[1] == "letter")
                    pb.SwapLetters(s[2][0], s[5][0]);
                else if(s[0] == "rotate" && s[1] == "left")
                    pb.RotateLeft(int.Parse(s[2]));
                else if(s[0] == "rotate" && s[1] == "right")
                    pb.RotateRight(int.Parse(s[2]));
                else if(s[0] == "rotate" && s[1] == "based")
                    pb.RotateOnLetter(s[6][0]);
                else if(s[0] == "reverse")
                    pb.Reverse(int.Parse(s[2]), int.Parse(s[4]));
                else if(s[0] == "move")
                    pb.MovePositionTo(int.Parse(s[2]), int.Parse(s[5]));
            }
            Console.WriteLine(pb);
        }

        private static void Part2(string[] lines, string password)
        {
            var pb = new PasswordBuilder(password);
            for(int i = lines.Length-1; i >= 0; i--)
            {
                var s = lines[i].Split();
                if(s[0] == "swap" && s[1] == "position")
                    pb.SwapPosition(int.Parse(s[5]), int.Parse(s[2]));
                else if(s[0] == "swap" && s[1] == "letter")
                    pb.SwapLetters(s[5][0], s[2][0]);
                else if(s[0] == "rotate" && s[1] == "left")
                    pb.RotateRight(int.Parse(s[2]));
                else if(s[0] == "rotate" && s[1] == "right")
                    pb.RotateLeft(int.Parse(s[2]));
                else if(s[0] == "rotate" && s[1] == "based")
                    pb.UnrotateOnLetter(s[6][0]);
                else if(s[0] == "reverse")
                    pb.Reverse(int.Parse(s[2]), int.Parse(s[4]));
                else if(s[0] == "move")
                    pb.MovePositionTo(int.Parse(s[5]), int.Parse(s[2]));
            }
            Console.WriteLine(pb);
        }
    }
}