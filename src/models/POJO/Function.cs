using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;
using IGUWPF.src.models.Model;
using Newtonsoft.Json;

namespace IGUWPF.src.models
{
    public class Function : IModelable<Function>
    {
        private int ID;
        public bool IsHidden { get; set; }
        public String Name { get; set; }
        public Color Color { get; set; }
        public PlotData PlotData { get; set; }

        public Function(String Name, Color Color, PlotData PlotData)
        {
            this.Name = Name;
            this.Color = Color;
            this.PlotData = PlotData;
            this.IsHidden = false;
        }

        [JsonConstructor]
        public Function(String Name, Color Color, PlotData PlotData, bool IsHidden) {
            this.Name = Name;
            this.Color = Color;
            this.PlotData = PlotData;
            this.IsHidden = IsHidden;
        }

        public override string ToString()
        {
            return "{ID=" + ID + ", Name= " + Name + ", IsHidden=" + IsHidden + ", Brush=" + Color + ", PlotData=" + PlotData + "}";
        }

        public override bool Equals(object obj)
        {

            if( null == obj)
            {
                return false;
            }else if (!obj.GetType().Equals( this.GetType()))
            {
                return false;
            }

            Function other = (Function)obj;

            if (other.ID != this.ID)
            {
                return false;
            } else if (!other.Name.Equals(this.Name))
            {
                return false;
            } else if (!other.Color.Equals(this.Color))
            {
                return false;
            } else if (!other.PlotData.Equals( this.PlotData)) {
                return false;
            }else
            {
                return true;
            } 
        }

        public override int GetHashCode()
        {
            var hashCode = 783144234;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(Color);
            hashCode = hashCode * -1521134295 + EqualityComparer<PlotData>.Default.GetHashCode(PlotData);
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
            Function ClonedFunction = new Function( this.Name, this.Color, this.PlotData);
            ClonedFunction.SetID(ID);
            return ClonedFunction;
        }
    }
}
