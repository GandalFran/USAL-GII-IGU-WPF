using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.models.Model
{
    public class IDataModelImpl<T> : IDataModel<T> where T : IModelable<T>
    {
        private int LastAssignedID;
        private List<T> ElementList = null;

        public IDataModelImpl() {
            this.LastAssignedID = 0;
            this.ElementList = new List<T>();
        }

        public IDataModelImpl( List<T> ElementList )
        {
            this.ElementList = new List<T>();
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
            int RemovedElements = this.ElementList.RemoveAll(OldElement => (OldElement.GetID() == Element.GetID()));
            return (RemovedElements > 0);
        }

        public T GetElementByID(int ID)
        {
           return this.ElementList.SingleOrDefault(Element => (ID == Element.GetID()));
        }

        public bool UpdateElement(T NewElement)
        {
            int RemovedElements = this.ElementList.RemoveAll(Element => (NewElement.GetID() == Element.GetID()));

            if (RemovedElements > 0)
            {
                this.ElementList.Add(NewElement);
            }

            return ( RemovedElements > 0);
        }

        public List<T> GetAllElements()
        {
            return new List<T>( this.ElementList );
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
