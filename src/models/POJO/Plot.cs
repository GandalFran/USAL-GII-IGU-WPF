using IGUWPF.src.controller.calculator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace IGUWPF.src.models.POJO
{
    public class Plot
    {
        private bool InternIsHidden;
        private Color InternColor;
        private Polyline InternPlotPoints;

        public Polyline PlotPoints
        {
            get
            {
                return InternPlotPoints;
            }
            set
            {
                InternPlotPoints = value;
                if (null != InternPlotPoints) {
                    if (null != InternColor)
                        InternPlotPoints.Stroke = new SolidColorBrush(InternColor);
                    if (InternIsHidden == true)
                        InternPlotPoints.Visibility = Visibility.Hidden;
                }
            }
        }
        public Color Color
        {
            get
            {
                return InternColor;
            }
            set
            {
                InternColor = value;
                if (null != PlotPoints && null != value)
                {
                    if (null != PlotPoints)
                        PlotPoints.Stroke = new SolidColorBrush(value);
                }
            }
        }
        public bool IsHidden
        {
            get
            {
                return InternIsHidden;
            }
            set
            {
                InternIsHidden = value;
                if (null != PlotPoints)
                {
                    if (value)
                        PlotPoints.Visibility = Visibility.Hidden;
                   else
                        PlotPoints.Visibility = Visibility.Visible;
                }
            }
        }

        public Plot()
        {
            this.IsHidden = false;
            this.PlotPoints = new Polyline();
        }

        public Plot(Color Color)
        {
            this.Color = Color;
            this.IsHidden = false;
            this.PlotPoints = new Polyline();
        }

        [JsonConstructor]
        public Plot(Polyline PlotPoints, Color Color, bool IsHidden)
        {
            this.PlotPoints = PlotPoints;
            this.Color = Color;
            this.IsHidden = IsHidden;
        }

        public override string ToString()
        {
            return "{Color=" + InternColor + ", PlotPoints= " + PlotPoints + ", IsHidden=" + InternIsHidden + "}";
        }

        public override bool Equals(object obj)
        {
            var plot = obj as Plot;
            return plot != null &&
                   InternIsHidden == plot.InternIsHidden &&
                   InternColor.Equals(plot.InternColor) &&
                   EqualityComparer<Polyline>.Default.Equals(PlotPoints, plot.PlotPoints) &&
                   Color.Equals(plot.Color) &&
                   IsHidden == plot.IsHidden;
        }

        public override int GetHashCode()
        {
            var hashCode = -128154857;
            hashCode = hashCode * -1521134295 + InternIsHidden.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(InternColor);
            hashCode = hashCode * -1521134295 + EqualityComparer<Polyline>.Default.GetHashCode(PlotPoints);
            hashCode = hashCode * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(Color);
            hashCode = hashCode * -1521134295 + IsHidden.GetHashCode();
            return hashCode;
        }
    }
}
