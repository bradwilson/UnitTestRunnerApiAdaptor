namespace SampleUnderTest
{
    using System;

    public class MathService
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public int DoSomething(bool a, int b, int c)
        {
            return a ? (b * c) / 2 : 10;
        }

        public int DoSomethingElse(int x, int y)
        {
            return (x * y) / 2;
        }

        public int NestedLeftAndRight(int x, int y)
        {
            return (x * y) / (x + y);
        }

        public double DewPoint(double RelativeHumidity, double Temperature)
        {
            double VapourPressureValue = RelativeHumidity * 0.01 * 6.112 * Math.Exp((17.62 * Temperature) / (Temperature + 243.12));
            double Numerator = 243.12 * Math.Log(VapourPressureValue) - 440.1;
            double Denominator = 19.43 - (Math.Log(VapourPressureValue));
            double DewPoint = Numerator / Denominator;

            return DewPoint;
        }

        public int ChainedOperators(int w, int x, int y, int z)
        {
            return w * x * y;
        }

        public string ConcatThese(string s, string t)
        {
            // expect this not to be mutated
            return s + t;
        }
    }
}
