using System;
using System.IO;

namespace Day10
{
    public class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            Bot[] bots = ParseBots(lines);
            Part12(bots);
        }

        private static void Part12(Bot[] bots)
        {
            var res = Process(bots, b => 
            {
                if((b.First == 61 && b.Second == 17) || (b.First == 17 && b.Second == 61))
                    Console.WriteLine(b.Id);
            });
            Console.WriteLine(res);
        }

        private static int Process(Bot[] bots, Action<Bot> botAction)
        {
            var outputs = new int[25];
            bool process = true;
            while(process)
            {
                process = false;
                foreach(var b in bots)
                {
                    if(b != null && !b.Processed && b.Count == 2)
                    {
                        botAction(b);
                        var high = Math.Max(b.First, b.Second);
                        var low = Math.Min(b.First, b.Second);
                        if(b.OutputLow.Item1)
                            outputs[b.OutputLow.Item2] = low;
                        else
                            bots[b.OutputLow.Item2].Give(low);
                        if(b.OutputHigh.Item1)
                            outputs[b.OutputHigh.Item2] = high;
                        else
                            bots[b.OutputHigh.Item2].Give(high);
                        b.Processed = true;
                        process = true;
                    }
                }
            }
            return outputs[0] * outputs[1] * outputs[2];
        }

        private static Bot[] ParseBots(string[] lines)
        {
            var bots = new Bot[300];
            foreach(var line in lines)
            {
                if(line.StartsWith("value"))
                {
                    var s = line.Split(' ');
                    var val = int.Parse(s[1]);
                    var botId = int.Parse(s[5]);
                    if(bots[botId] == null)
                        bots[botId] = new Bot() { Id = botId };
                    var bot = bots[botId];
                    bot.Give(val);
                }
                else if(line.StartsWith("bot"))
                {
                    var s = line.Split(' ');
                    var botId = int.Parse(s[1]);
                    if(bots[botId] == null)
                        bots[botId] = new Bot() { Id = botId };
                    var bot = bots[botId];
                    var lowOut = int.Parse(s[6]);
                    if(s[5] == "output")
                        bot.OutputLow = (true, lowOut);
                    else if(s[5] == "bot")
                        bot.OutputLow = (false, lowOut);
                    var highOut = int.Parse(s[11]);
                    if(s[10] == "output")
                        bot.OutputHigh = (true, highOut);
                    else if(s[10] == "bot")
                        bot.OutputHigh = (false, highOut);
                }
            }
            return bots;
        }

        public class Bot
        {
            public int Id;
            public int Count;
            public int First;
            public int Second;
            public (bool, int) OutputHigh;
            public (bool, int) OutputLow;
            public bool Processed;

            public void Give(int value)
            {
                if(Count == 0)
                    First = value;
                else
                    Second = value;
                Count++;
            }
        }
    }
}