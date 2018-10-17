﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.controller.calculator
{
    public abstract class ICalculator
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        abstract public double Calculate(double x);
    }
}
