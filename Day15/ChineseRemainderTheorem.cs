namespace Day15
{
    public record struct Congruence(int Remainder, int Modulo);

    public static class ChineseRemainderTheorem
    {
        public static int Solve(Congruence[] congruences)
        {
            int M = congruences.Aggregate(1, (a,c) => a * c.Modulo);
            //f[i]*modulo[i] + g[i]*(M/modulo[i]) = 1
            //x  = sum rem[i]*g[i]*(M/modulo[i])
            int x = 0;
            for(int i = 0; i < congruences.Length; i++)
            {
                EuclidAlgorithm.GCD(congruences[i].Modulo, M / congruences[i].Modulo, out int fi, out int gi);
                x += congruences[i].Remainder * gi * M / congruences[i].Modulo;
            }
            return (M + (x % M)) % M;
        }    
    }
}