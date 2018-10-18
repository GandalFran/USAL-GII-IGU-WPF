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
            this.a = a;
            this.b = b;
            this.c = 0;
        }

        public override double Calculate(double x)
        {
            return a * Math.Cos(b * x);
        }

        public override string ToString()
        {
            return a + "*cos(" + b + "*x)";
        }

    }

    public class SinXCalculator : ICalculator
    {

        public SinXCalculator(double a, double b)
        {
            this.a = a;
            this.b = b;
            this.c = 0;
        }

        public override double Calculate(double x)
        {
            return a * Math.Sin(b * x);
        }

        public override string ToString()
        {
            return a + "*sin(" + b + "*x)";
        }
    }

    public class TanXCalculator : ICalculator
    {

        public TanXCalculator(double a, double b)
        {
            this.a = a;
            this.b = b;
            this.c = 0;
        }

        public override double Calculate(double x)
        {
            return a * Math.Tan(b * x);
        }

        public override string ToString()
        {
            return a + "*tan(" + b + "*x)";
        }
    }

    public class XExpNCalculator : ICalculator
    {

        public XExpNCalculator(double a, double b)
        {
            this.a = a;
            this.b = b;
            this.c = 0;
        }

        public override double Calculate(double x)
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

        public NExpXCalculator(double a, double b)
        {
            this.a = a;
            this.b = b;
            this.c = 0;
        }

        public override double Calculate(double x)
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

        public NDividedXCalculator(double a, double b)
        {
            this.a = a;
            this.b = b;
            this.c = 0;
        }

        public override double Calculate(double x)
        {
            return a /(b*x);
        }

        public override string ToString()
        {
            return a + "/(x*" + b + ")";
        }
    }

   public class X1Calculator : ICalculator
    {

        public X1Calculator(double a, double b)
        {
            this.a = a;
            this.b = b;
            this.c = 0;
        }

        public override double Calculate(double x)
        {
            return a + (b*x);
        }

        public override string ToString()
        {
            return a + " + x*" + b + ")";
        }
    }


    public class X2Calculator : ICalculator
    {

        public X2Calculator(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public override double Calculate(double x)
        {
            return (a*x*x) + (b*x) + c;
        }

        public override string ToString()
        {
            return a + "x^2 + " + b + "x + " + c + ")";
        }
    }

}
