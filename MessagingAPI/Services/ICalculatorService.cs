namespace MessagingAPI.Services
{
  public interface ICalculatorService
    {
        int Add(int x, int y);
        double Average(params int[] values);
        int Diff(int x, int y);
        bool IsEven(int number);
        int Sum(params int[] values);
    }
}