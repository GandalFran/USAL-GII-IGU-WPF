using IGUWPF.src.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace IGUWPF.src.controllers.ControllersImpl
{
    public interface IPlotController
    {
        Line[] GetAxys();
        PointCollection CalculatePlotPoints( PlotData Plot);
        void ConfigurePolyLine(Function DataSource, Polyline ToConfigure, PointCollection Points);

        bool ExportPlot( string FilePath, Panel ToExport);
    }
}
