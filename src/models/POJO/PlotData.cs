using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IGUWPF.src.models
{
    public class PlotData
    {
        public String Expression { get; set; }


        public PlotData(string Expression)
        {
            this.Expression = Expression;
        }

        public double Calculate(double x) {
            double y;

            y = x;

            return y;
        }

        public override string ToString()
        {
            return Expression;
        }

        public override bool Equals(Object obj)
        {

            if (null == obj)
            {
                return false;
            }
            else if (!obj.GetType().Equals(this.GetType()))
            {
                return false;
            }

            PlotData other = (PlotData)obj;

            if (!other.Expression.Equals(this.Expression))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override int GetHashCode()
        {
            return -1489834557 + EqualityComparer<string>.Default.GetHashCode(Expression);
        }
    }
}
