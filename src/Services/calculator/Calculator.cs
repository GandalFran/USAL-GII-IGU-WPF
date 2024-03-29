﻿using System;

namespace IGUWPF.src.services.calculator
{
    public abstract class Calculator : ICloneable
    {
        public double a {get; set; }
        public double b { get; set; }
        public double c { get; set; }

        public static string Operation { get; }

        public abstract double Calculate(double x);
        public abstract object Clone();
    }

    //To let other programmers add complex operations easily
    public class MultipleOperationCalculator : Calculator
    {
        public Calculator[] OperationArray { get; set; }

        public MultipleOperationCalculator(params Calculator[] OperationArray)
        {
            this.OperationArray = OperationArray;
        }

        public override double Calculate(double x)
        {
            double result = x;

            foreach (Calculator calc in OperationArray)
                result = calc.Calculate(result);

            return result;
        }

        public override string ToString()
        {
            string ToReturn = "";
            foreach (Calculator calc in OperationArray)
                ToReturn = "(" + calc.ToString() + ")";
            return ToReturn;
        }

        public override object Clone()
        {
            MultipleOperationCalculator Cloned = new MultipleOperationCalculator();
            Cloned.OperationArray = (Calculator[])this.OperationArray.Clone();
            return Cloned;
        }
    }

}
