using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IGUWPF.src.models.Model
{
    public class IObservableModelImpl<T> : IObservableModel<T> where T : IModelable<T>
    {
        private int LastAssignedID;
        private ObservableCollection<T> ElementList = null;

        public IObservableModelImpl() {
            this.LastAssignedID = 0;
            this.ElementList = new ObservableCollection<T>();
        }

        public IObservableModelImpl( List<T> ElementList )
        {
            this.ElementList = new ObservableCollection<T>();
            foreach (T Element in ElementList) {
                CreateElement(Element);
            }
        }

        public int CreateElement(T Element)
        {
            int ID = GetLastAssignedIDAndIncrement();
            Element.SetID( ID );
            this.ElementList.Add(Element);

            return ID;
        }

        public bool DeleteElement(T Element)
        {
            bool found = false;
            T ElementToRemove = default(T);

            foreach (T Oldelement in ElementList) {
                if (Oldelement.GetID() == Element.GetID())
                {
                    found = true;
                    ElementToRemove = Oldelement;
                    break;
                }
            }

            if (found)
            {
                ElementList.Remove(ElementToRemove);
                return true;
            }
            else
            {
                return false;
            }
        }

        public T GetElementByID(int ID)
        {
           return this.ElementList.SingleOrDefault(Element => (ID == Element.GetID()));
        }

        public bool UpdateElement(T NewElement)
        {
            bool found = false;
            T ElementToRemove = default(T);

            foreach (T Oldelement in ElementList)
            {
                if (Oldelement.GetID() == NewElement.GetID())
                {
                    found = true;
                    ElementToRemove = Oldelement;
                    break;
                }
            }

            if (found)
            {
                this.ElementList.Add(NewElement);
                ElementList.Remove(ElementToRemove);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<T> GetAllElements()
        {
            return new List<T>(this.ElementList);
        }

        public ObservableCollection<T> GetAllElementsForBinding()
        {
            return this.ElementList;
        }

        public void Clear()
        {
            this.LastAssignedID = 0;
            this.ElementList.Clear();
        }

        private int GetLastAssignedIDAndIncrement() {
            int IDtoReturn = this.LastAssignedID;
            this.LastAssignedID = this.LastAssignedID + 1;
            return IDtoReturn;
        }
    }
}
