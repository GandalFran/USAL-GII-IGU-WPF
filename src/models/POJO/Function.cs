using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;
using IGUWPF.src.models.Model;
using Newtonsoft.Json;
using IGUWPF.src.controller.calculator;
using IGUWPF.src.models.POJO;

namespace IGUWPF.src.models
{
    public class Function : IModelable<Function>
    {
        private int ID;
        public Plot Plot { get; set; }
        public string Name { get; set; }
        public ICalculator Calculator { get; set; }

        public Function(string Name, ICalculator Calculator)
        {
            this.Name = Name;
            this.Calculator = Calculator;
            this.Plot = new Plot();
        }

        [JsonConstructor]
        public Function(string Name, ICalculator Calculator, Plot Plot)
        {
            this.Plot = Plot;
            this.Name = Name;
            this.Calculator = Calculator;
        }

        public override string ToString()
        {
            return "{ID=" + ID + ", Name= " + Name + ", Expression=" + Calculator + ", Plot=" + Plot.ToString() + "}";
        }


        public override bool Equals(object obj)
        {
            var function = obj as Function;
            return function != null &&
                   ID == function.ID &&
                   Name == function.Name &&
                   Calculator == function.Calculator &&
                   EqualityComparer<Plot>.Default.Equals(Plot, function.Plot);
        }

        public override int GetHashCode()
        {
            var hashCode = 936340811;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICalculator>.Default.GetHashCode(Calculator);
            hashCode = hashCode * -1521134295 + EqualityComparer<Plot>.Default.GetHashCode(Plot);
            return hashCode;
        }

        public int GetID()
        {
            return ID;
        }
        public void SetID(int ID)
        {
            this.ID = ID;
        }
        public Function Clone()
        {
            Function ClonedFunction = new Function(this.Name, this.Calculator, this.Plot);
            ClonedFunction.SetID(ID);
            return ClonedFunction;
        }
    }
}
