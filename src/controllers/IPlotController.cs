using IGUWPF.src.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static IGUWPF.src.utils.Enumerations;

namespace IGUWPF.src.controllers
{
    interface IPlotController
    {
        int Add(Function Element);
        bool Delete(Function Element);
        bool Update(Function Element, params PlotUpdateType[] Type);
        void Clear();

        void UpdateAxys();
        bool ExportPlot(string DataPath);
    }
}
