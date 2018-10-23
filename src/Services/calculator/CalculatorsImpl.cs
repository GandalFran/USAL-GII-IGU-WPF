using System;

namespace IGUWPF.src.services.calculator
{
    public class CosXCalculator : Calculator
    {
        public new static string Operation { get { return "a*cos(b*x)"; } }

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

        public override object Clone()
        {
            return new CosXCalculator(this.a, this.b);
        }
    }

    public class SinXCalculator : Calculator
    {
        public new static string Operation { get { return "a*sin(b*x)"; } }

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

        public override object Clone()
        {
            return new SinXCalculator(this.a, this.b);
        }
    }

    public class XExpNCalculator : Calculator
    {
        public new static string Operation { get { return "a*x^b"; } }

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

        public override object Clone()
        {
            return new XExpNCalculator(this.a, this.b);
        }
    }

    public class NExpXCalculator : Calculator
    {
        public new static string Operation { get { return "a*b^x"; } }

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

        public override object Clone()
        {
            return new NExpXCalculator(this.a, this.b);
        }
    }

    public class NDividedXCalculator : Calculator
    {
        public new static string Operation { get { return "a/(b*x)"; } }

        public NDividedXCalculator(double a, double b)
        {
            this.a = a;
            this.b = b;
            this.c = 0;
        }

        public override double Calculate(double x)
        {
            double toReturn;
            try
            {
                toReturn = a / (b * x);
            }
            catch (Exception) {
                return double.MaxValue * ( ((a*b)>0) ? (1) : (-1)  );
            }
            return toReturn;
        }

        public override string ToString()
        {
            return a + "/(x*" + b + ")";
        }

        public override object Clone()
        {
            return new NDividedXCalculator(this.a, this.b);
        }
    }

   public class X1Calculator : Calculator
    {
        public new static string Operation { get { return "a + b*x"; } }

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
            return a + " + (x*" + b + ")";
        }

        public override object Clone()
        {
            return new X1Calculator(this.a, this.b);
        }
    }

    public class X2Calculator : Calculator
    {
        public new static string Operation { get { return "a*x^2 + bx + c"; } }

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
            return a + "x^2 + " + b + "x + " + c;
        }

        public override object Clone()
        {
            return new X2Calculator(this.a, this.b, this.c);
        }
    }

}
