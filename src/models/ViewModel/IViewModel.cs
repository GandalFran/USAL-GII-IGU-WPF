using IGUWPF.src.models.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IGUWPF.src.models.ViewModel
{
    public class ViewModelEventArgs : EventArgs{
        public object Element { get; set; }

        public ViewModelEventArgs()
        {
            this.Element = null;
        }
        public ViewModelEventArgs(object Element)
        {
            this.Element = Element;
        }
    }

    public delegate void ViewModelEventHandler (object sender, ViewModelEventArgs e);

    public interface IViewModel<T> where T:IModelable<T>, INotifyPropertyChanged
    {
        event ViewModelEventHandler CreateElementEvent;
        event ViewModelEventHandler DeleteElementEvent;
        event ViewModelEventHandler UpdateElementEvent;
        event ViewModelEventHandler ClearEvent;

        int CreateElement(T Element);
        bool UpdateElement(T Element);
        bool DeleteElement(T Element);
        T GetElementByID(int ID);

        List<T> GetAllElements();
        ObservableCollection<T> GetAllElementsForBinding();

        void Clear();
    }
}
