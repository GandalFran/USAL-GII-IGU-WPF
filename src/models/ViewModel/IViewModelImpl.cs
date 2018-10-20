using IGUWPF.src.models.model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IGUWPF.src.models.ViewModel
{
    public class IViewModelImpl<T> : IViewModel<T> where T:IModelable<T>, INotifyPropertyChanged
    {
        private IObservableModel<T> Model;

        public event ViewModelEventHandler ElementCreated;
        public event ViewModelEventHandler ElementDeleted;
        public event ViewModelEventHandler ElementUpdated;
        public event ViewModelEventHandler ModelCleaned;

        public IViewModelImpl() {
            Model = new IObservableModelImpl<T>();
        }

        public IViewModelImpl(IObservableModel<T> Model)
        {
            this.Model = Model;
        }

        public int CreateElement(T Element)
        {
            int result = Model.CreateElement(Element);
            Element.PropertyChanged += OnPropertyChanged;
            OnElementCreated(Element);
            return result;
        }

        public bool UpdateElement(T Element)
        {
            bool result = Model.UpdateElement(Element);
            if (result)
            {
                Element.PropertyChanged += OnPropertyChanged;
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
            OnModelCleaned();
        }

        public void OnElementCreated(T Element) {
            if (null != ElementCreated) ElementCreated(this,new ViewModelEventArgs(Element));
        }

        public void OnElementDeleted(T Element)
        {
            if (null != ElementDeleted) ElementDeleted(this, new ViewModelEventArgs(Element));
        }

        public void OnElementUpdated(T Element)
        {
            if (null != ElementUpdated) ElementUpdated(this, new ViewModelEventArgs(Element));
        }

        public void OnModelCleaned()
        {
            if (null != ModelCleaned) ModelCleaned(this, new ViewModelEventArgs());
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            OnElementUpdated((T)sender);
        }
    }
}
