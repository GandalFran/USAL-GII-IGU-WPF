using System.Collections.Generic;
using System.Windows.Media;
using IGUWPF.src.models.Model;
using Newtonsoft.Json;
using IGUWPF.src.controller.calculator;
using System.Windows.Controls;

namespace IGUWPF.src.models
{
    public class Function : IModelable<Function>
    {
        private int InternalID;
        public int ID { get => InternalID;  }

        public string Name { get; internal set; }
        public ICalculator Calculator { get; set; }
        public bool IsHidden;
        public Color Color;

        public Function(string Name, ICalculator Calculator)
        {
            this.Name = Name;
            this.Calculator = Calculator;
        }

        [JsonConstructor]
        public Function(string Name, ICalculator Calculator, Color Color, bool IsHidden)
        {
            this.Name = Name;
            this.Color = Color;
            this.IsHidden = IsHidden;
            this.Calculator = Calculator;
        }

        public override string ToString()
        {
            return "{ID=" + ID + ", Name= " + Name + ", Expression=" + Calculator + ", Color=" + Color.ToString() + ", IsHidden=" + IsHidden.ToString() + "}";
        }

        public override bool Equals(object obj)
        {
            var function = obj as Function;
            return function != null &&
                   ID == function.ID &&
                   Name == function.Name &&
                   EqualityComparer<ICalculator>.Default.Equals(Calculator, function.Calculator) &&
                   IsHidden == function.IsHidden &&
                   Color.Equals(function.Color);
        }

        public override int GetHashCode()
        {
            var hashCode = -237823076;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICalculator>.Default.GetHashCode(Calculator);
            hashCode = hashCode * -1521134295 + IsHidden.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(Color);
            return hashCode;
        }

        //IModelable Interface
        public int GetID()
        {
            return InternalID;
        }
        public void SetID(int ID)
        {
            this.InternalID = ID;
        }
        public Function Clone()
        {
            Function ClonedFunction = new Function(this.Name, this.Calculator, this.Color,this.IsHidden);
            ClonedFunction.InternalID = InternalID;
            return ClonedFunction;
        }
    }
}
