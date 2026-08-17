using System.Collections;

namespace MessagingAPI.Services
{
    public class CalculatorService : ICalculatorService
    {
        public bool IsEven(int number) => number % 2 == 0;

        public int Diff(int x, int y) => y - x;

        public int Add(int x, int y) => x + y;

        public int Sum(params int[] values)
        {
            int sum = 0;

            foreach (var value in values)
            {
                sum += value;
            }
            return sum;
        }

        public double Average(params int[] values)
        {
            int sum = 0;
            foreach (var value in values)
            {
                sum += value;
            }

            return sum / values.Length;
        }
    }
}