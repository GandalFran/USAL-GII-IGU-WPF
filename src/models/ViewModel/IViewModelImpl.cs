﻿using IGUWPF.src.models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.models.ViewModel
{
    public class IViewModelImpl<T> : IViewModel<T> where T:IModelable<T>
    {
        private IObservableDataModel<T> Model;

        public event ViewModelEventHandler CreateElementEvent;
        public event ViewModelEventHandler DeleteElementEvent;
        public event ViewModelEventHandler UpdateElementEvent;
        public event ViewModelEventHandler ClearEvent;

        public IViewModelImpl() {
            Model = new IObservableDataModelImpl<T>();
        }

        public IViewModelImpl(IObservableDataModel<T> Model)
        {
            this.Model = Model;
        }

        public int CreateElement(T Element)
        {
            int result = Model.CreateElement(Element);
            OnCreateElementEvent(Element);
            return result;
        }

        public bool DeleteElement(T Element)
        {
            bool result = Model.DeleteElement(Element);
            if (result)
                OnDeleteElementEvent(Element);
            return result;
        }

        public bool UpdateElement(T Element)
        {
            bool result = Model.UpdateElement(Element);
            if (result)
                OnUpdateElementEvent(Element);
            return result;
        }

        public List<T> GetAllElements()
        {
            return Model.GetAllElements();
        }

        public T GetElementByID(int ID)
        {
            return Model.GetElementByID(ID);
        }

        public void Clear()
        {
            Model.Clear();
            OnClearEvent();
        }

        public void OnCreateElementEvent(T Element) {
            if (null != CreateElementEvent) CreateElementEvent(this,new ViewModelEventArgs(Element));
        }
        public void OnDeleteElementEvent(T Element)
        {
            if (null != DeleteElementEvent) DeleteElementEvent(this, new ViewModelEventArgs(Element));
        }
        public void OnUpdateElementEvent(T Element)
        {
            if (null != UpdateElementEvent) UpdateElementEvent(this, new ViewModelEventArgs(Element));
        }
        public void OnClearEvent()
        {
            if (null != ClearEvent) ClearEvent(this, new ViewModelEventArgs());
        }
    }
}