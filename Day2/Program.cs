using System;
using System.IO;

namespace Day2
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            Part1(input);
            Part2(input);
        }

        private static void Part1(string[] input)
        {
            var (i,j) = (1,1);
            foreach(var line in input)
            {
                foreach(var c in line)
                {
                    switch(c)
                    {
                        case 'U':
                            i = Math.Max(i-1, 0);
                            break;
                        case 'D':
                            i = Math.Min(i+1, 2);
                            break;
                        case 'L':
                            j = Math.Max(i-1, 0);
                            break;
                        case 'R':
                            j = Math.Min(i+1, 2);
                            break;
                    }
                }
                Console.Write(3*i+j+1);
            }
            Console.WriteLine();
        }

        private static void Part2(string[] input)
        {
            var b1 = new Button('1');
            var b2 = new Button('2');
            var b3 = new Button('3');
            var b4 = new Button('4');
            var b5 = new Button('5');
            var b6 = new Button('6');
            var b7 = new Button('7');
            var b8 = new Button('8');
            var b9 = new Button('9');
            var ba = new Button('A');
            var bb = new Button('B');
            var bc = new Button('C');
            var bd = new Button('D');
            b1.Moves.Add('D', b3);
            b2.Moves.Add('D', b6);
            b2.Moves.Add('R', b3);
            b3.Moves.Add('U', b1);
            b3.Moves.Add('L', b2);
            b3.Moves.Add('R', b4);
            b3.Moves.Add('D', b7);
            b4.Moves.Add('L', b3);
            b4.Moves.Add('D', b8);
            b5.Moves.Add('R', b6);
            b6.Moves.Add('L', b5);
            b6.Moves.Add('R', b7);
            b6.Moves.Add('U', b2);
            b6.Moves.Add('D', ba);
            b7.Moves.Add('L', b6);
            b7.Moves.Add('R', b8);
            b7.Moves.Add('U', b3);
            b7.Moves.Add('D', bb);
            b8.Moves.Add('L', b7);
            b8.Moves.Add('R', b9);
            b8.Moves.Add('D', bc);
            b8.Moves.Add('U', b4);
            b9.Moves.Add('L', b8);
            ba.Moves.Add('U', b6);
            ba.Moves.Add('R', bb);
            bb.Moves.Add('L', ba);
            bb.Moves.Add('R', bc);
            bb.Moves.Add('U', b7);
            bb.Moves.Add('D', bd);
            bc.Moves.Add('L', bb);
            bc.Moves.Add('U', b8);
            bd.Moves.Add('U', bb);
            Button current = b5;
            foreach(var line in input)
            {
                foreach(var c in line)
                {
                    if(current.Moves.TryGetValue(c, out Button next))
                        current = next;
                }
                Console.Write(current.Label);
            }
            Console.WriteLine();
        }

        private struct Button
        {
            public char Label;
            public IDictionary<char,Button> Moves;

            public Button(char label)
            {
                Label = label;
                Moves = new Dictionary<char, Button>();
            }
        }
    }
}