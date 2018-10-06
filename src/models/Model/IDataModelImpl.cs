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
            this.LastAssignedID = 1 + GetBiggestId(ElementList);
            this.ElementList = ElementList;
        }

        public int CreateElement(T element)
        {
            int ID = GetLastAssignedIDAndIncrement();
            element.SetId( ID );
            this.ElementList.Add( element );

            return ID;
        }

        public bool DeleteElement(int ID)
        {
            int RemovedElements = this.ElementList.RemoveAll(Element => (ID == Element.GetID()));
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
            return this.ElementList;
        }

        public void DeleteAllElementsAndResetIDs()
        {
            this.LastAssignedID = 0;
            this.ElementList = new List<T>();
        }


        private int GetLastAssignedIDAndIncrement() {
            int IDtoReturn = this.LastAssignedID;
            this.LastAssignedID = this.LastAssignedID + 1;
            return IDtoReturn;
        }

        private int GetBiggestId( List<T> ElementList ) {
            int BiggestId = 0;

            foreach (T Element in ElementList) {
                if (Element.GetID() > BiggestId) {
                    BiggestId = Element.GetID();
                }
            }

            return BiggestId;
        }
    }
}
