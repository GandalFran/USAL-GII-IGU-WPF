using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;
using IGUWPF.src.models.Model;

namespace IGUWPF.src.models
{
    public class Function : IModelable<Function>
    {
        private int ID;
        public String Name { get; set; }
        public Brush Color { get; set; }
        public MathematicalExpression MatheMaticalExpresion { get; set; }

        public Function(String Name, Brush Color, MathematicalExpression MatheMaticalExpresion) {
            this.Name = Name;
            this.Color = Color;
            this.MatheMaticalExpresion = MatheMaticalExpresion;
        }

        public override string ToString()
        {
            return "{ID=" + ID + ", Name= " + Name + ", Brush=" + Color + ", MatheMaticalExpresion=" + MatheMaticalExpresion + "}";
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
            } else if (!other.MatheMaticalExpresion.Equals( this.MatheMaticalExpresion)) {
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
            hashCode = hashCode * -1521134295 + EqualityComparer<Brush>.Default.GetHashCode(Color);
            hashCode = hashCode * -1521134295 + EqualityComparer<MathematicalExpression>.Default.GetHashCode(MatheMaticalExpresion);
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
            Function ClonedFunction = new Function( this.Name, this.Color, this.MatheMaticalExpresion );
            ClonedFunction.SetID(ID);
            return ClonedFunction;
        }
    }
}
