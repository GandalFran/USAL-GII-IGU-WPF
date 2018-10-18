using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.models.ViewModel
{

    public struct PlotRepresentationSettings
    {
        public double XMin, XMax, YMin, YMax;
    };


    public class FunctionViewModelImpl : IViewModelImpl<Function>
    {
        public event ViewModelEventHandler UpdatePlotSettingsEvent;

        private PlotRepresentationSettings InternPlotSettings;
        public PlotRepresentationSettings PlotSettings
        {
            get
            {
                return InternPlotSettings;
            }
            set
            {
                InternPlotSettings = value;
                OnUpdatePlotSettingsEvent();
            }
        }

        public double XMin {
            get
            {
                return InternPlotSettings.XMin;
            }
            set
            {
                InternPlotSettings.XMin = value;
                OnUpdatePlotSettingsEvent();
            }
        }
        public double XMax
        {
            get
            {
                return InternPlotSettings.XMax;
            }
            set
            {
                InternPlotSettings.XMax = value;
                OnUpdatePlotSettingsEvent();
            }
        }
        public double YMin
        {
            get
            {
                return InternPlotSettings.YMin;
            }
            set
            {
                InternPlotSettings.YMin = value;
                OnUpdatePlotSettingsEvent();
            }
        }
        public double YMax
        {
            get
            {
                return InternPlotSettings.YMax;
            }
            set
            {
                InternPlotSettings.YMax = value;
                OnUpdatePlotSettingsEvent();
            }
        }

        public void OnUpdatePlotSettingsEvent() {
            if (null != UpdatePlotSettingsEvent) UpdatePlotSettingsEvent(this, new ViewModelEventArgs());
        }
    }
}
