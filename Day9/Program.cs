using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Day9
{
    public class Program
    {
        public static void Main()
        {
            var text = File.ReadAllText("input.txt");
            Part1(text);
            Part2(text);
        }

        private static void Part1(string text)
        {
            Console.WriteLine(Decompress(text).Count(c => !char.IsWhiteSpace(c)));
        }

        private static void Part2(string text)
        {
            Console.WriteLine(DecompressRec(text));
        }

        private static string Decompress(string text)
        {
            var sb = new StringBuilder();
            for(int i = 0; i < text.Length; i++)
            {
                if(text[i] == '(')
                {
                    var marker = new string(text.Substring(i+1).TakeWhile(c => c != ')').ToArray());
                    i += marker.Length+1;
                    var s = marker.Split('x').Select(int.Parse).ToArray();
                    var str = text.Substring(i+1, s[0]);
                    i += s[0];
                    for(int c = 0; c < s[1]; c++)
                        sb.Append(str);
                }    
                else
                    sb.Append(text[i]);
            }
            return sb.ToString();
        }

        private static long DecompressRec(string text)
        {
            long l = 0;
            for(int i = 0; i < text.Length; i++)
            {
                if(text[i] == '(')
                {
                    var marker = new string(text.Substring(i+1).TakeWhile(c => c != ')').ToArray());
                    i += marker.Length+1;
                    var s = marker.Split('x').Select(int.Parse).ToArray();
                    var l1 = DecompressRec(text.Substring(i+1, s[0]));
                    i += s[0];
                    l += l1 * s[1];
                }    
                else
                    l++;
            }
            return l;
        }
    }
}