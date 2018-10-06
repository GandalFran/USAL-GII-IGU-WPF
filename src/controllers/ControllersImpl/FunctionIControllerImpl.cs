using IGUWPF.src.models;
using IGUWPF.src.models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.controllers
{
    public class FunctionIControllerImpl : IController<Function>
    {
        private IDataModel<Function> FunctionModel;
        private DAO<Function> FunctionDAO;

        public FunctionIControllerImpl() {
            FunctionDAO = new JsonDAO<Function>();
            FunctionModel = new IDataModelImpl<Function>();
        }

        public int AddAndGetID(Function Element)
        {
            return FunctionModel.CreateElement( Element );
        }

        public bool Delete(int ID)
        {
            return FunctionModel.DeleteElement( ID );
        }

        public Function GetById(int ID)
        {
            return FunctionModel.GetElementByID( ID );
        }

        public bool Update(Function Element)
        {
            return FunctionModel.UpdateElement( Element );
        }

        public List<Function> GetAll()
        {
            return FunctionModel.GetAllElements();
        }


        public bool ExportAll(string DataPath)
        {
            return FunctionDAO.ExportMultipleObject( DataPath, FunctionModel.GetAllElements() );
        }

        public bool ImportAll(string DataPath)
        {
            bool result;
            List<Function> toFill = new List<Function>();

            result = FunctionDAO.ImportMultipleObject( DataPath, toFill );

            if (result)
            {
                FunctionModel = new IDataModelImpl<Function>( toFill );
            }

            return result;
        }
    }
}
