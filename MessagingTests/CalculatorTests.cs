using MessagingAPI.Services;

namespace MessagingTests
{
    public class CalculatorTests
    {
        private readonly CalculatorService service;
        public CalculatorTests()
        {
            service = new CalculatorService();
        }

        [Fact]
        public void IsEvenTest()
        {
            int x = 1;
            int y = 2;

            var resultX = service.IsEven(x);
            var resultY = service.IsEven(y);

            Assert.False(resultX);
            Assert.True(resultY);
        }
    }
}