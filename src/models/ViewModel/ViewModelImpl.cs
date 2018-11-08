using IGUWPF.src.models.model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IGUWPF.src.models.ViewModel
{
    public class ViewModelImpl<T> : IViewModel<T> where T:IModelable, INotifyPropertyChanged
    {
        private IObservableModel<T> Model;

        public event ViewModelEventHandler ElementCreated;
        public event ViewModelEventHandler ElementDeleted;
        public event ViewModelEventHandler ElementUpdated;
        public event ViewModelEventHandler DeleteAll;
        public event ViewModelEventHandler UpdateAll;

        public ViewModelImpl()
        {
            Model = new ObservableModelImpl<T>();
        }

        public ViewModelImpl(IObservableModel<T> Model)
        {
            this.Model = Model;
        }

        public int CreateElement(T Element)
        {
            int result = Model.CreateElement(Element);
            Element.PropertyChanged += PropertyChanged;
            OnElementCreated(Element);
            return result;
        }

        public bool UpdateElement(T Element)
        {
            bool result = Model.UpdateElement(Element);
            if (result)
            {
                Element.PropertyChanged += PropertyChanged;
                OnElementUpdated(Element);
            }
            return result;
        }

        public bool DeleteElement(T Element)
        {
            bool result = Model.DeleteElement(Element);
            if (result)
                OnElementDeleted(Element);
            return result;
        }

        public T GetElementByID(int ID)
        {
            return Model.GetElementByID(ID);
        }

        public List<T> GetAllElements()
        {
            return Model.GetAllElements();
        }

        public ObservableCollection<T> GetAllElementsForBinding()
        {
            return Model.GetAllElementsForBinding();
        }

        public void Clear()
        {
            Model.Clear();
            OnDeleteAll();
        }

        protected virtual void OnElementCreated(T Element) {
            if (null != ElementCreated) ElementCreated(this,new ViewModelEventArgs(Element));
        }

        protected virtual void OnElementDeleted(T Element)
        {
            if (null != ElementDeleted) ElementDeleted(this, new ViewModelEventArgs(Element));
        }

        protected virtual void OnElementUpdated(T Element)
        {
            if (null != ElementUpdated) ElementUpdated(this, new ViewModelEventArgs(Element));
        }

        protected virtual void OnDeleteAll()
        {
            if (null != DeleteAll) DeleteAll(this, new ViewModelEventArgs());
        }

        protected virtual void OnUpdateAll()
        {
            if (null != UpdateAll) UpdateAll(this, new ViewModelEventArgs());
        }

        protected virtual void PropertyChanged(object sender, PropertyChangedEventArgs e) {
            OnElementUpdated((T)sender);
        }
    }
}
