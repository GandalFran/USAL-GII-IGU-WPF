using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.models.Model
{
    public interface IModelable<T>
    {
        int GetID();
        void SetId(int ID);
        T Clone();
    }
}
