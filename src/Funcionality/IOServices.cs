using IGUWPF.src.IO;
using IGUWPF.src.models;
using IGUWPF.src.models.Model;
using IGUWPF.src.models.POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IGUWPF.src.controllers
{
    public class IOServices
    {

        public static bool ExportModel(string DataPath, IDataModel<Function> FunctionModel)
        {
            IDAO<Function> FunctionDAO = new SerialDAOImpl<Function>();
            return FunctionDAO.ExportMultipleObject( DataPath, FunctionModel.GetAllElements() );
        }

        public static bool ImportModel(string DataPath, IDataModel<Function> FunctionModel)
        {
            bool result;
            List<Function> toFill = new List<Function>();
            IDAO<Function> FunctionDAO = new SerialDAOImpl<Function>();

            result = FunctionDAO.ImportMultipleObject( DataPath, toFill );

            if (result)
            {
                FunctionModel = new IDataModelImpl<Function>( toFill );
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
