using IGUWPF.src.models.POJO;

namespace IGUWPF.src.models.ViewModel
{
    public struct RepresentationParameters
    {
        public double XMin, XMax, YMin, YMax;
    };

    public class FunctionViewModelImpl : IViewModelImpl<Function>
    {
        public event ViewModelEventHandler UpdateRepresentationParameters;

        private RepresentationParameters InternPlotSettings;

        public RepresentationParameters PlotSettings
        {
            get { return InternPlotSettings; }
            set
            {
                InternPlotSettings = value;
                OnUpdateRepresentationParameters();
            }
        }

        public double XMin {
            get { return InternPlotSettings.XMin; }
            set
            {
                InternPlotSettings.XMin = value;
                OnUpdateRepresentationParameters();
            }
        }

        public double XMax
        {
            get { return InternPlotSettings.XMax; }
            set
            {
                InternPlotSettings.XMax = value;
                OnUpdateRepresentationParameters();
            }
        }

        public double YMin
        {
            get { return InternPlotSettings.YMin; }
            set
            {
                InternPlotSettings.YMin = value;
                OnUpdateRepresentationParameters();
            }
        }

        public double YMax
        {
            get { return InternPlotSettings.YMax; }
            set
            {
                InternPlotSettings.YMax = value;
                OnUpdateRepresentationParameters();
            }
        }

        public void OnUpdateRepresentationParameters() {
            if (null != UpdateRepresentationParameters) UpdateRepresentationParameters(this, new ViewModelEventArgs());
        }

    }
}
