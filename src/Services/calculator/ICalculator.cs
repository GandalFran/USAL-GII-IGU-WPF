
namespace IGUWPF.src.services.calculator
{
    public abstract class ICalculator
    {
        public double a {get; set; }
        public double b { get; set; }
        public double c { get; set; }

        public static string Operation { get; }

        public abstract double Calculate(double x);
    }

    //To let other programmers add more operations easily
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
