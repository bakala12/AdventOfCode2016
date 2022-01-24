namespace Day20
{
    public class RangeSet
    {
        public uint Min, Max;
        public RangeSet? Lower, Upper;

        public RangeSet(uint min, uint max)
        {
            Min = min;
            Max = max;
        }

        public static RangeSet AddRange(RangeSet? set, uint min, uint max)
        {
            if(set == null)
                return new RangeSet(min, max);
            if(min < set.Min)
                set.Lower = AddRange(set.Lower, min, Math.Min(set.Min-1, max));
            if(max > set.Max)
                set.Upper = AddRange(set.Upper, Math.Max(min, set.Max+1), max);
            return set;
        }

        private void AddRanges(List<(uint, uint)> ranges)
        {
            Lower?.AddRanges(ranges);
            ranges.Add((Min,Max));
            Upper?.AddRanges(ranges);
        }

        public uint MinimalNotIncluded()
        {
            var ranges = new List<(uint, uint)>();
            AddRanges(ranges);
            if(ranges[0].Item1 > 0)
                return 0;
            uint min = ranges[0].Item2+1;
            for(int i = 1; i < ranges.Count; i++)
            {
                if(min < ranges[i].Item1)
                    return min;
                min = ranges[i].Item2+1;
            }
            return uint.MaxValue;
        }

        public uint NotIncludedCount()
        {
            var ranges = new List<(uint, uint)>();
            AddRanges(ranges);
            uint lastMax = 0;
            uint count = 0;
            foreach(var (min, max) in ranges)
            {
                if(lastMax < min)
                    count += (uint)(min - lastMax);
                lastMax = max+1;
            }
            return count;
        }
    }
}