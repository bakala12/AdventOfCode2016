namespace Day15
{
    public static class EuclidAlgorithm
    {
        public static int GCD(int a, int b, out int x, out int y)
        {
            int s = x = 1;
            int r = y = 0;
            if(b > a)
            {
                int t = b;
                b = a;
                a = t;
                s = x = 0;
                r = y = 1;
            }
            while(b != 0)
            {
                int c = a % b;
                int q = a / b;
                a = b;
                b = c;
                int r1 = r;
                int s1 = s;
                r = x - q * r;
                s = y - q * s;
                x = r1;
                y = s1;
            }
            return a;
        }
    }
}