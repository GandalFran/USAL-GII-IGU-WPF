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

        [JsonIgnore]
        public int ID { get; private set; }

        public string Name { get; private set; }
        public Calculator Calculator { get; set; }
        public bool IsHidden { get; private set; }
        public Color Color { get; private set; }

        public Function(string Name, Calculator Calculator, Color Color, bool IsHidden)
        {
            this.Name = Name;
            this.Color = Color;
            this.IsHidden = IsHidden;
            this.Calculator = Calculator;
        }

        public override string ToString()
        {
            return "{ID=" + ID + ", Name= " + Name + ", Calculator=" + Calculator + ", Color=" + Color.ToString() + ", IsHidden=" + IsHidden.ToString() + "}";
        }

        public override bool Equals(object obj)
        {
            return obj is Function function &&
                   ID == function.ID &&
                   Name == function.Name &&
                   EqualityComparer<Calculator>.Default.Equals(Calculator, function.Calculator) &&
                   IsHidden == function.IsHidden &&
                   Color.Equals(function.Color);
        }

        public override int GetHashCode()
        {
            var hashCode = -878180465;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Calculator>.Default.GetHashCode(Calculator);
            hashCode = hashCode * -1521134295 + IsHidden.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(Color);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NameProperty);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CalculatorProperty);
            hashCode = hashCode * -1521134295 + IsVissibleProperty.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(ColorProperty);
            return hashCode;
        }

        #region JsonSerializer
        [JsonConstructor]
        public Function(string Name, SerializableCalculator Calculator, Color Color, bool IsHidden)
        {
            this.Name = Name;
            this.Color = Color;
            this.IsHidden = IsHidden;
            this.Calculator = Calculator.ToICalculator();
        }
        #endregion

        #region IModelable
        public int GetID()
        {
            return ID;
        }

        public void SetID(int ID)
        {
            this.ID = ID;
        }

        public object Clone()
        {
            Function ClonedFunction = new Function(this.Name, (Calculator)this.Calculator.Clone(), this.Color, this.IsHidden)
            {
                ID = ID
            };
            return ClonedFunction;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string PropertyName)
        {
            if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

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
        #endregion
    }
}
