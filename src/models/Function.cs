using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;

namespace IGUWPF.src.models
{
    class Function
    {
        public String Name { get; set; }
        public Brush Color { get; set; }
        public String MatheMaticalExpresion { get; set; }

        public Function(String Name, Brush Color, String MatheMaticalExpresion) {
            this.Name = Name;
            this.Color = Color;
            this.MatheMaticalExpresion = MatheMaticalExpresion;
        }
    }
}
