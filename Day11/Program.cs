using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Day11
{
    public partial class Program
    {
        public static void Main()
        {
            var lines = File.ReadAllLines("input.txt");
            var (itemFloors, itemNames) = InputParser.ParseInput(lines);
            Console.WriteLine(Find(itemFloors.ToArray(), itemNames.ToArray()));
            itemFloors.Add(0);
            itemFloors.Add(0);
            itemFloors.Add(0);
            itemFloors.Add(0);
            itemNames.Add("ElG");
            itemNames.Add("ElM");
            itemNames.Add("DiG");
            itemNames.Add("DiM");
            Console.WriteLine(Find(itemFloors.ToArray(), itemNames.ToArray()));
        }

        private static int Find(int[] itemFloors, string[] itemNames)
        {
            WorldInfo.SetItemsCount(itemFloors.Length);
            var queue = new Queue<State>();
            var consideredStates = new HashSet<long>();
            State state = State.InitialState(itemFloors);
            queue.Enqueue(state);
            consideredStates.Add(state.GetHash());
            while(queue.Count > 0)
            {
                state = queue.Dequeue();
                if(state.IsFinal())
                    return state.Steps;
                foreach(var next in state.NextStates())
                {
                    var hash = next.GetHash();
                    if(!consideredStates.Contains(hash))
                    {
                        consideredStates.Add(hash);
                        queue.Enqueue(next);
                    }
                }
            }
            return 0;
        }
    }
}