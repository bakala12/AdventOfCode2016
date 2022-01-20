namespace Day11
{
    public static class WorldInfo
    {
        public static int ItemsCount { get; private set; }
        public static int Generators { get; private set; }
        public static int Microchips { get; private set; }

        public static void SetItemsCount(int itemsCount)
        {
            ItemsCount = itemsCount;
            Generators = Microchips = 0;
            for(int i = 0; i < ItemsCount; i++)
            {
                Generators |= 0x1 << (2*i);
                Microchips |= 0x2 << (2*i);
            }
        }
    }
}