using IGUWPF.src.models.bean;
using System.Collections.Generic;
using System.Windows.Controls;

namespace IGUWPF.src.services.IO
{
    public class IOServices
    {

        public static bool ExportPlot(string DataPath, Panel ToExport)
        {
            IDAO<Panel> PlotDAO = new ImageDAOImpl();
            return PlotDAO.ExportSingleObject(DataPath, ToExport);
        }

    }
}
