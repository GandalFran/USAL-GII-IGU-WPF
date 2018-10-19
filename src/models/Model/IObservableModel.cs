using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IGUWPF.src.models.Model
{
    public interface IObservableModel<T> where T:IModelable<T>
    {
        int CreateElement( T Element );
        bool UpdateElement( T Element);
        bool DeleteElement( T Element );
        T GetElementByID( int ID );
        
        List<T> GetAllElements();
        ObservableCollection<T> GetAllElementsForBinding();

        void Clear();
    }
}
