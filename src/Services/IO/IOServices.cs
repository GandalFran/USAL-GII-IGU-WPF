using IGUWPF.src.IO;
using IGUWPF.src.models;
using IGUWPF.src.models.ViewModel;
using System.Collections.Generic;
using System.Windows.Controls;

namespace IGUWPF.src.controllers
{
    public class IOServices
    {

        public static bool ExportModel(string DataPath, IViewModel<Function> FunctionModel)
        {
            IDAO<Function> FunctionDAO = new SerialDAOImpl<Function>();
            return FunctionDAO.ExportMultipleObject( DataPath, FunctionModel.GetAllElements() );
        }

        public static bool ImportModel(string DataPath, IViewModel<Function> FunctionModel)
        {
            bool result;
            List<Function> toFill = new List<Function>();
            IDAO<Function> FunctionDAO = new SerialDAOImpl<Function>();

            result = FunctionDAO.ImportMultipleObject( DataPath, toFill );

            if (result)
            {
                FunctionModel.Clear();
                foreach (Function Function in toFill)
                    FunctionModel.CreateElement(Function);
            }
            
            return result;
        }

        public static bool ExportPlot(string DataPath, Panel ToExport)
        {
            IDAO<Panel> PlotDAO = new ImageDAOImpl();
            return PlotDAO.ExportSingleObject(DataPath, ToExport);
        }

    }
}
