using System;
using System.IO;
using System.Text;

namespace Day16
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllText("input.txt");
            Console.WriteLine(Fill(272, input));
            Console.WriteLine(Fill(35651584, input));
        }

        private static string Fill(int size, string input)
        {
            var sb = new StringBuilder(input);
            while(sb.Length < size)
            {
                sb.Append("0");
                for(int i = sb.Length - 2; i >= 0; i--)
                    sb.Append(sb[i] == '0' ? '1' : '0');
            }
            sb.Remove(size, sb.Length-size);
            var sb1 = new StringBuilder();
            while(sb.Length % 2 == 0)
            {
                for(int i = 1; i < sb.Length; i+=2)
                    sb1.Append(sb[i] == sb[i-1] ? '1' : '0');
                var temp = sb;
                sb = sb1;
                sb1 = temp;
                sb1.Clear();
            }
            return sb.ToString();
        }
    }
    
}