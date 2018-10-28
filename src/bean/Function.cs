using System.Collections.Generic;
using System.Windows.Media;
using Newtonsoft.Json;
using IGUWPF.src.services.calculator;
using System.ComponentModel;
using IGUWPF.src.models.model;
using IGUWPF.src.services.IO;

namespace IGUWPF.src.bean
{
    public class Function : IModelable, INotifyPropertyChanged
    {
        private string InternName;
        private Color InternColor;
        private bool InternIsHidden;
        private Calculator InternCalculator;


        public string Name
        {
            get { return InternName; }
            set
            {
                InternName = value;
                OnPropertyChanged("Name");
            }
        }

        public Calculator Calculator
        {
            get { return InternCalculator; }
            set
            {
                InternCalculator = value;
                OnPropertyChanged("Calculator");
            }
        }

        public bool IsHidden
        {
            get { return InternIsHidden; }
            set
            {
                InternIsHidden = value;
                OnPropertyChanged("IsHidden");
            }
        }
 
        public Color Color
        {
            get { return InternColor; }
            set
            {
                InternColor = value;
                OnPropertyChanged("Color");
            }
        }
        [JsonIgnore]
        public bool IsVissible
        {
            get { return !InternIsHidden; }
            set
            {
                InternIsHidden = !value;
                OnPropertyChanged("IsVissible");
            }
        }
        [JsonIgnore]
        public string CalculatorStr
        {
            get { return InternCalculator.ToString(); }
            set
            {
                //This set, is only for compatibility with the datagrid, because it's established a two way link
            }
        }

        public Function(string Name, Calculator Calculator, Color Color, bool IsHidden)
        {
            this.InternName = Name;
            this.InternColor = Color;
            this.InternIsHidden = IsHidden;
            this.InternCalculator = Calculator;
        }

        public override string ToString()
        {
            return "{ID=" + ID + ", Name= " + InternName + ", Calculator=" + CalculatorStr + ", Color=" + InternColor.ToString() + ", IsHidden=" + InternIsHidden + "}";
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
        {  //Its needed to return the ID, because if not, there is a failure adding, and delting on datagrid
            return ID;
        }

        #region JsonSerializer
        [JsonConstructor]
        public Function(string Name, SerializableCalculator Calculator, Color Color, bool IsHidden)
        {
            this.InternName = Name;
            this.InternColor = Color;
            this.InternIsHidden = IsHidden;
            this.InternCalculator = Calculator.ToICalculator();
        }
        #endregion

        #region IModelable
        [JsonIgnore]
        public int ID { get; private set; }

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
            Function ClonedFunction = new Function((string)this.Name.Clone(), (Calculator)this.Calculator.Clone(), Color.FromRgb(this.Color.R,this.Color.G,this.Color.B), this.IsHidden)
            {
                ID = this.ID
            };
            return ClonedFunction;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
