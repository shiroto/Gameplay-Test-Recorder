namespace TwoGuyGames
{
    public static class Factorial
    {
        /// <summary>
        /// Most basic and dumb implementation of factorial calculation.
        /// </summary>
        public static int Calculate(int number)
        {
            if (number == 0)
            {
                return 1;
            }
            int fact = number;
            for (int i = number - 1; i > 1; i--)
            {
                fact *= i;
            }
            return fact;
        }
    }
}