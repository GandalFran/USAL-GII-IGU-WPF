using IGUWPF.src.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IGUWPF.src.controllers
{
    interface IFunctionController : IController<Function>
    {
        bool ExportPlot(String FilePath, Canvas PlotPanel);
    }
}
