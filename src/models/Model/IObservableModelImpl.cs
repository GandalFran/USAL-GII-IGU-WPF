using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IGUWPF.src.models.model
{
    public class IObservableModelImpl<T> : IObservableModel<T> where T : IModelable
    {

        private int LastAssignedID;
        private ObservableCollection<T> ElementList;

        public IObservableModelImpl() {
            this.LastAssignedID = 0;
            this.ElementList = new ObservableCollection<T>();
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
            List<T> ElementListToReturn = new List<T>();
            foreach (T Element in ElementList)
                ElementListToReturn.Add((T)Element.Clone());
            return ElementListToReturn;
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

        protected int GetLastAssignedIDAndIncrement() {
            int IDtoReturn = this.LastAssignedID;
            this.LastAssignedID = this.LastAssignedID + 1;
            return IDtoReturn;
        }

    }
}
