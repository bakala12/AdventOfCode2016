using System;
using System.IO;
using System.Collections.Generic;

namespace Day19
{
    public class Program
    {
        public static void Main()
        {
            var elvesCount = int.Parse(File.ReadAllText("input.txt"));
            Part1(elvesCount);
            Part2(elvesCount);
        }

        private static void Part1(int elvesCount)
        {
            Console.WriteLine(FlawiusProblem(elvesCount));
        }

        private static int FlawiusProblem(int elvesCount)
        {
            int[] remaining = new int[elvesCount+1];
            remaining[1] = remaining[2] = 1;
            for(int n = 3; n <= elvesCount; n++)
            {
                //J(2n) = 2*J(n)-1
                //J(2n+1) = 2*J(n)+1
                if(n % 2 == 0)
                    remaining[n] = 2*remaining[n/2] - 1;
                else
                    remaining[n] = 2*remaining[n/2] + 1;
            }
            return remaining[elvesCount];
        }

        private static void Part2(int elvesCount)
        {
            Console.WriteLine(ExtendedFlawius(elvesCount));
        }

        private static int ExtendedFlawius(int elvesCount)
        {
            int[] extendedRemaining = new int[elvesCount+1];
            extendedRemaining[1] = 1;
            for(int n = 2; n <= elvesCount; n++)
            {
                int rem = n / 2;
                extendedRemaining[n] = (extendedRemaining[n-1] < rem ? extendedRemaining[n-1] : extendedRemaining[n-1]+1) % n + 1;
            }
            return extendedRemaining[elvesCount];
        }
    }
}