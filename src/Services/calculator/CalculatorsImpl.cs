using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.controller.calculator
{
    public class CosXCalculator : ICalculator
    {
        public CosXCalculator(double a, double b) 
        {
            this.A = a;
            this.B = b;
            this.C = 0;
        }

        public override double Calculate(double x)
        {
            return A * Math.Cos(B*x);
        }

        public override string ToString()
        {
            return A + "*cos(" + B + "*x)";
        }

    }

    public class SinXCalculator : ICalculator
    {
        public SinXCalculator(double a, double b)
        {
            this.A = a;
            this.B = b;
            this.C = 0;
        }

        public override double Calculate(double x)
        {
            return A * Math.Sin(B * x);
        }

        public override string ToString()
        {
            return A + "*sin(" + B + "*x)";
        }
    }

    public class TanXCalculator : ICalculator
    {
        public TanXCalculator(double a, double b)
        {
            this.A = a;
            this.B = b;
            this.C = 0;
        }

        public override double Calculate(double x)
        {
            return A * Math.Tan(B * x);
        }

        public override string ToString()
        {
            return A + "*tan(" + B + "*x)";
        }
    }

    public class XExpNCalculator : ICalculator
    {
        public XExpNCalculator(double a, double b)
        {
            this.A = a;
            this.B = b;
            this.C = 0;
        }

        public override double Calculate(double x)
        {
            return A * Math.Pow(x,B);
        }

        public override string ToString()
        {
            return A + "*x^" + B;
        }
    }

    public class NExpXCalculator : ICalculator
    {
        public NExpXCalculator(double a, double b)
        {
            this.A = a;
            this.B = b;
            this.C = 0;
        }

        public override double Calculate(double x)
        {
            return A * Math.Pow(B,x);
        }

        public override string ToString()
        {
            return A + "*" + B + "^x";
        }
    }

    public class NDividedXCalculator : ICalculator
    {
        public NDividedXCalculator(double a, double b)
        {
            this.A = a;
            this.B = b;
            this.C = 0;
        }

        public override double Calculate(double x)
        {
            return A /(B*x);
        }

        public override string ToString()
        {
            return A + "/(x*" + B + ")";
        }
    }

   public class X1Calculator : ICalculator
    {
        public X1Calculator(double a, double b)
        {
            this.A = a;
            this.B = b;
            this.C = 0;
        }

        public override double Calculate(double x)
        {
            return A + (B*x);
        }

        public override string ToString()
        {
            return A + " + x*" + B + ")";
        }
    }


    public class X2Calculator : ICalculator
    {
        public X2Calculator(double a, double b, double c)
        {
            this.A = a;
            this.B = b;
            this.C = C;
        }

        public override double Calculate(double x)
        {
            return (A*x*x) + (B*x) + C;
        }

        public override string ToString()
        {
            return A + "x^2 + " + B + "x + " + C + ")";
        }
    }


    public class MultipleOperationCalculator : ICalculator
    {
        public ICalculator[] OperationArray { get; set; }

        public MultipleOperationCalculator(params ICalculator[] OperationArray)
        {
            this.OperationArray = OperationArray;
        }

        public override double Calculate(double x)
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
