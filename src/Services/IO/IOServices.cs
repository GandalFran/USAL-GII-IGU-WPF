using IGUWPF.src.IO;
using IGUWPF.src.models;
using System.Collections.Generic;
using System.Windows.Controls;

namespace IGUWPF.src.controllers
{
    public class IOServices
    {
        public static bool ExportModel(string DataPath, List<Function> FunctionList)
        {
            IDAO<Function> FunctionDAO = new JsonFunctionDAOImpl();
            return FunctionDAO.ExportMultipleObject( DataPath, FunctionList );
        }

        public static bool ImportModel(string DataPath, List<Function> FunctionListToFill)
        {
            IDAO<Function> FunctionDAO = new JsonFunctionDAOImpl();
            return FunctionDAO.ImportMultipleObject( DataPath, FunctionListToFill);
        }

        public static bool ExportPlot(string DataPath, Panel ToExport)
        {
            IDAO<Panel> PlotDAO = new ImageDAOImpl();
            return PlotDAO.ExportSingleObject(DataPath, ToExport);
        }
    }
}
