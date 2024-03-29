﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IGUWPF.src.models.model
{
    public interface IObservableModel<T> where T:IModelable
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
