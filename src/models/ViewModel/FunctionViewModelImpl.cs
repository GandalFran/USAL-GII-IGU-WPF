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

        private double InternZoomPonderation;
        private RepresentationParameters InternRepresentationParameters;

        public RepresentationParameters RepresentationParameters
        {
            get
            {
                RepresentationParameters ToReturn;
                ToReturn.XMin = InternRepresentationParameters.XMin;
                ToReturn.XMax = InternRepresentationParameters.XMax;
                ToReturn.YMin = InternRepresentationParameters.YMin;
                ToReturn.YMax = InternRepresentationParameters.YMax;
                return ToReturn;
            }
            set
            {
                InternRepresentationParameters = value;
                OnRepresentationParametersChanged();
            }
        }

        public RepresentationParameters PonderedPlotSettings
        {
            get {
                RepresentationParameters ToReturn;
                ToReturn.XMin = InternRepresentationParameters.XMin * InternZoomPonderation;
                ToReturn.XMax = InternRepresentationParameters.XMax * InternZoomPonderation;
                ToReturn.YMin = InternRepresentationParameters.YMin * InternZoomPonderation;
                ToReturn.YMax = InternRepresentationParameters.YMax * InternZoomPonderation;
                return ToReturn;
            }
        }

        public double XMin {
            get { return InternRepresentationParameters.XMin * ZoomPonderation; }
        }

        public double XMax
        {
            get { return InternRepresentationParameters.XMax * ZoomPonderation; }
        }

        public double YMin
        {
            get { return InternRepresentationParameters.YMin * ZoomPonderation; }
        }

        public double YMax
        {
            get { return InternRepresentationParameters.YMax * ZoomPonderation; }
        }

        public double ZoomPonderation {
            get { return InternZoomPonderation; }
            set
            {
                this.InternZoomPonderation = value;
                OnRepresentationParametersChanged();
            }
        }

        protected void OnRepresentationParametersChanged() {
            if (null != RepresentationParametersChanged) RepresentationParametersChanged(this, new ViewModelEventArgs());
        }

    }
}
