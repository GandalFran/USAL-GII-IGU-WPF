using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.models.Model
{
    public interface IDataModel<T> where T:IModelable<T>
    {
        int CreateElement( T Element );
        bool UpdateElement( T NewElement );
        bool DeleteElement( int ID );
        T GetElementByID( int ID );
        
        List<T> GetAllElements();
        void DeleteAllElementsAndResetIDs();
        
    }
}
