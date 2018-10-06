using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.models
{
    public class MathematicalExpression
    {
        public String Expression { get; set; }

        public MathematicalExpression(string Expression)
        {
            this.Expression = Expression;
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

            MathematicalExpression other = (MathematicalExpression)obj;

            if (other.Expression.Equals(this.Expression))
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
