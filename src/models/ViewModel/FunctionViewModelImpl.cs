using IGUWPF.src.bean;
using IGUWPF.src.services.IO;
using System.Collections.Generic;

namespace IGUWPF.src.models.ViewModel
{
    public struct RepresentationParameters
    {
        public double XMin, XMax, YMin, YMax;
    };

    public class FunctionViewModelImpl : ViewModelImpl<Function>
    {
        private double InternZoomPonderation;
        private RepresentationParameters InternRepresentationParameters;

        public double ZoomPonderation
        {
            get { return InternZoomPonderation; }
            set
            {
                this.InternZoomPonderation = value;
                OnUpdateAll();
            }
        }

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
                OnUpdateAll();
            }
        }

        public RepresentationParameters PonderedRepresentationParameters
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

        public bool ExportModel(string DataPath)
        {
            IDAO<Function> FunctionDAO = new JsonFunctionDAOImpl();
            return FunctionDAO.ExportMultipleObject(DataPath, this.GetAllElements());
        }

        public bool ImportModel(string DataPath)
        {
            bool result;
            IDAO<Function> FunctionDAO = new JsonFunctionDAOImpl();
            List<Function> FunctionListToFill = new List<Function>();

            result = FunctionDAO.ImportMultipleObject(DataPath, FunctionListToFill);

            if (result) {
                this.Clear();
                foreach (Function Function in FunctionListToFill)
                    this.CreateElement(Function);
            }

            return result;
        }

    }
}
