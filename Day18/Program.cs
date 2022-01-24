using System;
using System.IO;
using System.Linq;

namespace Day18
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllText("input.txt");
            Part1(input, 40);
            Part1(input, 400000);
        }

        private static void Part1(string input, int rows)
        {
            char[] prevRow = input.ToCharArray();
            long c = prevRow.Count(c => c == '.');
            int s;
            for(int r = 0; r < rows-1; r++)
            {
                (prevRow, s) = GenerateRow(prevRow);
                c += s;
            }
            Console.WriteLine(c);
        }

        private static char NextSquare(char left, char center, char right)
        {
            return ((left == '^') ^ (right == '^')) ? '^' : '.';
        }

        private static (char[], int) GenerateRow(char[] previousRow)
        {
            int safeCount = 0;
            char[] next = new char[previousRow.Length];
            for(int i = 0; i < previousRow.Length; i++)
            {
                char left = i > 0 ? previousRow[i-1] : '.';
                char right = i < previousRow.Length - 1 ? previousRow[i+1] : '.';
                next[i] = NextSquare(left, previousRow[i], right);
                if(next[i] == '.')
                    safeCount++;
            }
            return (next, safeCount);
        }
    }
}