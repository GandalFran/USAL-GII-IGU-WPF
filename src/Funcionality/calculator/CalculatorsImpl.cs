using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.controller.calculator
{
    public class CosXCalculator : ICalculator
    {
        private double a, b;

        public CosXCalculator(double a, double b) {
            this.a = a;
            this.b = b;
        }

        public double Calculate(double x)
        {
            return a * Math.Cos(b*x);
        }

        public override string ToString()
        {
            return a + "cos(" + b + "*x)";
        }
    }

    public class SinXCalculator : ICalculator
    {
        private double a, b;

        public SinXCalculator(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public double Calculate(double x)
        {
            return a * Math.Sin(b * x);
        }

        public override string ToString()
        {
            return a + "sin(" + b + "*x)";
        }
    }

    public class TanXCalculator : ICalculator
    {
        private double a, b;

        public TanXCalculator(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public double Calculate(double x)
        {
            return a * Math.Tan(b * x);
        }

        public override string ToString()
        {
            return a + "tan(" + b + "*x)";
        }
    }

    public class XExpNCalculator : ICalculator
    {
        private double a, b;

        public XExpNCalculator(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public double Calculate(double x)
        {
            return a * Math.Pow(x,b);
        }

        public override string ToString()
        {
            return a + "*x^" + b;
        }
    }

    public class NExpXCalculator : ICalculator
    {
        private double a, b;

        public NExpXCalculator(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public double Calculate(double x)
        {
            return a * Math.Pow(b,x);
        }

        public override string ToString()
        {
            return a + "*" + b + "^x";
        }
    }

    public class NDividedXCalculator : ICalculator
    {
        private double a, b;

        public NDividedXCalculator(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public double Calculate(double x)
        {
            return a /b*x;
        }

        public override string ToString()
        {
            return a + "/(x*" + b + ")";
        }
    }

    public class X2Calculator : ICalculator
    {
        private double a, b,c;

        public X2Calculator(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public double Calculate(double x)
        {
            return (a*x*x) + (b*x) + c;
        }

        public override string ToString()
        {
            return a + "x^2 + " + b + "x + " + c + ")";
        }
    }


    public class MultipleOperationCalculator : ICalculator
    {
        private ICalculator[] OperationArray;

        public MultipleOperationCalculator(params ICalculator[] OperationArray)
        {
            this.OperationArray = OperationArray;
        }

        public double Calculate(double x)
        {
            double result = x;

            foreach (ICalculator calc in OperationArray)
                result = calc.Calculate(result);

            return result;
        }

        public override string ToString()
        {
            string ToReturn = "";
            foreach (ICalculator calc in OperationArray)
                ToReturn = "(" + calc.ToString() + ")";
            return ToReturn;
        }
    }
}
