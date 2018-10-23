using System.Collections.Generic;
using System.Windows.Media;
using Newtonsoft.Json;
using IGUWPF.src.services.calculator;
using System.ComponentModel;
using IGUWPF.src.models.model;
using IGUWPF.src.services.IO;
using System;

namespace IGUWPF.src.models.bean
{
    public class Function : IModelable, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonIgnore]
        private int InternalID;
        [JsonIgnore]
        public int ID { get => InternalID;  }

        public string Name { get; set; }
        public Calculator Calculator { get; set; }
        public bool IsHidden { get; set; }
        public Color Color { get; set; }

        public Function(string Name, Calculator Calculator)
        {
            this.Name = Name;
            this.Calculator = Calculator;
        }

        public Function(string Name, Calculator Calculator, Color Color, bool IsHidden)
        {
            this.Name = Name;
            this.Color = Color;
            this.IsHidden = IsHidden;
            this.Calculator = Calculator;
        }

        [JsonConstructor]
        public Function(string Name, SerializableCalculator Calculator, Color Color, bool IsHidden)
        {
            this.Name = Name;
            this.Color = Color;
            this.IsHidden = IsHidden;
            this.Calculator = Calculator.ToICalculator();
        }

        public override string ToString()
        {
            return "{ID=" + ID + ", Name= " + Name + ", Calculator=" + Calculator + ", Color=" + Color.ToString() + ", IsHidden=" + IsHidden.ToString() + "}";
        }

        public override bool Equals(object obj)
        {
            var function = obj as Function;
            return function != null &&
                   ID == function.ID &&
                   Name == function.Name &&
                   EqualityComparer<Calculator>.Default.Equals(Calculator, function.Calculator) &&
                   IsHidden == function.IsHidden &&
                   Color.Equals(function.Color);
        }

        public override int GetHashCode()
        {
            return InternalID;
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

        public object Clone()
        {
            Function ClonedFunction = new Function(this.Name, (Calculator) this.Calculator.Clone(), this.Color,this.IsHidden);
            ClonedFunction.InternalID = InternalID;
            return ClonedFunction;
        }

        //INotifyPropertyChanged interface
        [JsonIgnore]
        public string NameProperty
        {
            get { return Name; }
            set
            {
                Name = value;
                OnPropertyChanged("Name");
            }
        }

        [JsonIgnore]
        public string CalculatorProperty
        {
            get { return Calculator.ToString(); }
            set {
                //TODO add calculator
                OnPropertyChanged("Calculator");
            }
        }

        [JsonIgnore]
        public bool IsVissibleProperty
        {
            get { return !IsHidden; }
            set
            {
                IsHidden = !value;
                OnPropertyChanged("IsVissible");
            }
        }

        [JsonIgnore]
        public Color ColorProperty
        {
            get { return Color; }
            set
            {
                Color = value;
                OnPropertyChanged("color");
            }
        }

        protected void OnPropertyChanged(string PropertyName)
        {
            if (null != PropertyChanged) PropertyChanged(this,new PropertyChangedEventArgs(PropertyName));
        }

    }
}
