using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _calc.Calculate("0/1").ShouldEqual(0);
            _calc.Calculate("0/-1").ShouldEqual(0);
            _calc.Calculate("0/1*1+213-123456+8+7*7").ShouldEqual(-123186);
            _calc.Calculate("0/1*1+213-123456+8+7*7+10").ShouldEqual(-123176);
            _calc.Calculate("0/-1*10000+123456").ShouldEqual(123456);
            _calc.Calculate("1*8*7+100-123").ShouldEqual(33);
            _calc.Calculate("1/8/7/100/123/0.000012").ShouldEqual(0.120983353);
            _calc.Calculate("0.5*0.001").ShouldEqual(0.0005);
            _calc.Calculate("0.5*0.001/0.5*0.001/0.5*0.001").ShouldEqual(0.000000002);
            _calc.Calculate("1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1").ShouldEqual(16);
            _calc.Calculate("1+1+0/1+1+1+1+1+1+0/1+1+1+1+0/1+1+1+1").ShouldEqual(13);
            _calc.Calculate("0/1+1+0/1+(1+1+1+1)*3+1+0/1+1+1+1+0/1+1+1+1").ShouldEqual(20);
            _calc.Calculate("(7+2)*2").ShouldEqual(18);
            _calc.Calculate("7+2*2").ShouldEqual(11);
            _calc.Calculate("7000+200*2/(1+1+1+1)*1").ShouldEqual(7100);
            _calc.Calculate("(7+2+1)*2").ShouldEqual(20);
        }
    }
}
