using System;
using System.Runtime.InteropServices;
using Should;

namespace CalcuLord.NET
{
    public class CalcuLord : IDisposable
    {
        public CalcuLord()
        {
            InitCalculator();
        }

        public void Dispose()
        {
            ReleaseCalculator();
        }

        [DllImport("Calculord.dll")]
        private static extern void InitCalculator();

        [DllImport("Calculord.dll")]
        private static extern void ReleaseCalculator();

        [DllImport("Calculord.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool Calculate(string value, out double result);

        public double Calculate(string value)
        {
            double result;
            Calculate(value, out result).ShouldBeTrue("Calculation failed.");

            return result;
        }
    }
}