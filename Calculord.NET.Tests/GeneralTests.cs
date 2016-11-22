using NUnit.Framework;
using Should;

namespace Calculord.NET.Tests
{
    [TestFixture]
    public class GeneralTests
    {
        private readonly CalcuLord.NET.CalcuLord _calc;

        public GeneralTests()
        {
            _calc = new CalcuLord.NET.CalcuLord();
        }

        [Test]
        public void basic_operations_should_work()
        {
            _calc.Calculate("1+2").ShouldEqual(3);
            _calc.Calculate("1-2").ShouldEqual(-1);
            _calc.Calculate("1*2").ShouldEqual(2);
            _calc.Calculate("1/2").ShouldEqual(0.5);
        }
    }
}