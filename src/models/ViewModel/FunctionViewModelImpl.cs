using IGUWPF.src.models.POJO;

namespace IGUWPF.src.models.ViewModel
{
    public struct RepresentationParameters
    {
        public double XMin, XMax, YMin, YMax;
    };

    public class FunctionViewModelImpl : IViewModelImpl<Function>
    {
        public event ViewModelEventHandler RepresentationParametersChanged;

        private RepresentationParameters InternPlotSettings;

        public RepresentationParameters PlotSettings
        {
            get { return InternPlotSettings; }
            set
            {
                InternPlotSettings = value;
                OnRepresentationParametersChanged();
            }
        }

        public double XMin {
            get { return InternPlotSettings.XMin; }
            set
            {
                InternPlotSettings.XMin = value;
                OnRepresentationParametersChanged();
            }
        }

        public double XMax
        {
            get { return InternPlotSettings.XMax; }
            set
            {
                InternPlotSettings.XMax = value;
                OnRepresentationParametersChanged();
            }
        }

        public double YMin
        {
            get { return InternPlotSettings.YMin; }
            set
            {
                InternPlotSettings.YMin = value;
                OnRepresentationParametersChanged();
            }
        }

        public double YMax
        {
            get { return InternPlotSettings.YMax; }
            set
            {
                InternPlotSettings.YMax = value;
                OnRepresentationParametersChanged();
            }
        }

        public void OnRepresentationParametersChanged() {
            if (null != RepresentationParametersChanged) RepresentationParametersChanged(this, new ViewModelEventArgs());
        }

    }
}
