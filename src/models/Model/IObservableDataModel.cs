using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.models.Model
{
    public interface IObservableDataModel<T> where T:IModelable<T>
    {
        int CreateElement( T Element );
        bool UpdateElement( T Element);
        bool DeleteElement( T Element );
        T GetElementByID( int ID );
        
        ObservableCollection<T> GetAllElements();
        void Clear();
    }
}
