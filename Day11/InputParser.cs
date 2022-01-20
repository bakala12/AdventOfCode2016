namespace Day11
{
    public class InputParser
    {
        public static (List<int>, List<string>) ParseInput(string[] lines)
        {
            int items = 0;
            var itemList = new List<(string, int)>();
            for(int f = 0; f < lines.Length; f++)
            {
                var s = lines[f].Split(' ');
                for(int i = 4; i < s.Length-1; i++)
                {
                    if(s[i] == "a" || s[i] == "and" || s[i] == "nothing" || s[i] == "relevant") continue;
                    var itemName = $"{char.ToUpper(s[i][0])}{s[i][1]}{(s[i+1].Trim(',', '.') == "generator" ? "G" : "M")}";
                    itemList.Add((itemName, f));
                    items++;
                    i++;
                }    
            }
            var itemFloors = new List<int>();
            var itemNames = new List<string>();
            foreach(var (name, floor) in itemList.OrderBy(x => x.Item1))
            {
                itemFloors.Add(floor);
                itemNames.Add(name);
            }
            return (itemFloors, itemNames);
        }
    }
}