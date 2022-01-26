using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day24
{
    public class Program
    {
        public static void Main()
        {
            var input = File.ReadAllLines("input.txt");
            var locations = FindLocations(input);
            Part12(input, locations);
        }

        private static void Part12(string[] input, (int,int)[] locations)
        {
            var dist = FindDistances(input, locations);
            var visited = new bool[locations.Length];
            visited[0] = true;
            var cost = FindPath(dist, 1, visited, 0, 0);
            Console.WriteLine(cost);
            visited = new bool[locations.Length];
            visited[0] = true;
            var cycle = FindCycle(dist, 1, visited, 0, 0);
            Console.WriteLine(cycle);
        }

        private static (int,int)[] FindLocations(string[] input)
        {
            var locations = new Dictionary<int, (int,int)>();
            for(int i = 0; i < input.Length; i++)
            {
                for(int j = 0; j < input[i].Length; j++)
                {
                    if(char.IsDigit(input[i][j]))
                        locations.Add(input[i][j], (i,j));
                }
            }
            return locations.OrderBy(k => k.Key).Select(k => k.Value).ToArray();
        }
    
        private static int[,] FindDistances(string[] input, (int,int)[] locations)
        {
            var dist = new int[locations.Length, locations.Length];
            for(int s = 0; s < locations.Length; s++)
            {
                var queue = new Queue<((int, int), int)>();
                var visited = new HashSet<(int, int)>();
                queue.Enqueue((locations[s], 0));
                visited.Add(locations[s]);
                int locationsToVisit = locations.Length;
                while(queue.Count > 0)
                {
                    var ((y,x), st) = queue.Dequeue();
                    for(int e = 0; e < locations.Length; e++)
                    {
                        if(locations[e] == (y,x))
                        {
                            dist[s,e] = st;
                            locationsToVisit--;
                            break;
                        }
                    }
                    if(locationsToVisit == 0)
                        break;
                    foreach(var l in GetNeighbours(input, y, x))
                    {
                        if(!visited.Contains(l))
                        {
                            visited.Add(l);
                            queue.Enqueue((l,st+1));
                        }
                    }
                }
            }
            return dist;
        }

        private static IEnumerable<(int,int)> GetNeighbours(string[] input, int y, int x)
        {
            if(y > 0 && input[y-1][x] != '#')
                yield return (y-1,x);
             if(y < input.Length - 1 && input[y+1][x] != '#')
                yield return (y+1,x);
             if(x > 0 && input[y][x-1] != '#')
                yield return (y,x-1);
             if(x < input[y].Length - 1 && input[y][x+1] != '#')
                yield return (y,x+1);   
        }
    
        private static int FindPath(int[,] dist, int v, bool[] visited, int current, int lastLoc)
        {
            if(v == visited.Length)
                return current;
            int bestCost = int.MaxValue;
            for(int i = 0; i < visited.Length; i++)
            {
                if(!visited[i])
                {
                    visited[i] = true;
                    var cost = FindPath(dist, v+1, visited, current + dist[lastLoc, i], i);
                    visited[i] = false;
                    bestCost = Math.Min(bestCost, cost);
                }
            }
            return bestCost;
        }

        private static int FindCycle(int[,] dist, int v, bool[] visited, int current, int lastLoc)
        {
            if(v == visited.Length)
                return current + dist[lastLoc, 0];
            int bestCost = int.MaxValue;
            for(int i = 0; i < visited.Length; i++)
            {
                if(!visited[i])
                {
                    visited[i] = true;
                    var cost = FindCycle(dist, v+1, visited, current + dist[lastLoc, i], i);
                    visited[i] = false;
                    bestCost = Math.Min(bestCost, cost);
                }
            }
            return bestCost;
        }
    }
}