using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Day14
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllText("input.txt");
            using MD5 md5 = MD5.Create();
            FindHashes(input, md5, 1);           
            FindHashes(input, md5, 2017);           
        }

        private static void FindHashes(string input, MD5 md5, int partCount)
        {
            List<(string, int)> keyCandidates = new List<(string, int)>();
            List<(string, int)> keys = new List<(string, int)>();
            int index = 0;
            while(true)
            {
                var hash = ProduceHash(md5, input+index, partCount);
                if(HasSameCharacterNumberOfTimes(hash, 3))
                {
                    var candidatesToRemove = new List<(string, int)>();
                    foreach(var candidate in keyCandidates)
                    {
                        if(candidate.Item2 + 1000 <= index)
                            candidatesToRemove.Add(candidate);
                        if(HasSameCharacterNumberOfTimes(candidate.Item1, 3, hash, 5))
                        {
                            keys.Add(candidate);
                            candidatesToRemove.Add(candidate);
                        }
                    }
                    foreach(var remove in candidatesToRemove)
                        keyCandidates.Remove(remove);
                    keyCandidates.Add((hash, index));
                    if(keys.Count >= 64)
                        break;
                }
                index++;
            }
            Console.WriteLine(keys[63].Item2);
        }

        private static bool HasSameCharacterNumberOfTimes(string str, int times)
        {
            for(int i = times - 1; i < str.Length; i++)
            {
                bool repetition = true;
                for(int j = i-1; j >= i - times + 1; j--)
                    if(str[j] != str[i])
                    {
                        repetition = false;
                        break;
                    }
                if(repetition)
                    return true;
            }
            return false;
        }

        private static bool HasSameCharacterNumberOfTimes(string input1, int times1, string input2, int times2)
        {
            char toTest = 'x';
            for(int i = times1 - 1; i < input1.Length; i++)
            {
                bool repetition = true;
                for(int j = i-1; j >= i - times1 + 1; j--)
                    if(input1[j] != input1[i])
                    {
                        repetition = false;
                        break;
                    }
                if(repetition)
                {
                    toTest = input1[i];
                    break;
                }
            }
            for(int i = times2 - 1; i < input2.Length; i++)
            {
                if(input2[i] == toTest)
                {
                    bool repetition = true;
                    for(int j = i-1; j >= i - times2 + 1; j--)
                    {
                        if(input2[j] != input2[i])
                        {
                            repetition = false;
                            break;
                        }
                    }
                    if(repetition)
                        return true;
                }
            }
            return false;
        }

        private static string ProduceHash(MD5 md5, string input, int count)
        {
            if(count == 0)
                return input;
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
                sb.Append(hashBytes[i].ToString("x2"));
            return ProduceHash(md5, sb.ToString(), count-1);
        }
    }
}