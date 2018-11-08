using IGUWPF.src.bean;
using IGUWPF.src.services.IO;
using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

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
            get => new RepresentationParameters()
            {
                XMin = PropertiesDAO.XMin,
                XMax = PropertiesDAO.XMax,
                YMin = PropertiesDAO.YMin,
                YMax = PropertiesDAO.YMax
            };
            set
            {
                PropertiesDAO.XMin = value.XMin;
                PropertiesDAO.XMax = value.XMax;
                PropertiesDAO.YMin = value.YMin;
                PropertiesDAO.YMax = value.YMax;
                OnUpdateAll();
            }
        }

        public double ZoomPonderation
        {
            get { return InternZoomPonderation; }
            set
            {
                InternRepresentationParameters = value;
                OnUpdateAll();
            }
        }

        public RepresentationParameters PonderedRepresentationParameters
        {
            get  => new RepresentationParameters() {
                        XMin = PropertiesDAO.XMin * InternZoomPonderation,
                        XMax = PropertiesDAO.XMax * InternZoomPonderation,
                        YMin = PropertiesDAO.YMin * InternZoomPonderation,
                        YMax = PropertiesDAO.YMax * InternZoomPonderation
            };
        }

        public double XMin {
            get { return PropertiesDAO.XMin * ZoomPonderation; }
        }

        public double XMax
        {
            get { return PropertiesDAO.XMax * ZoomPonderation; }
        }

        public double YMin
        {
            get { return PropertiesDAO.YMin * ZoomPonderation; }
        }

        public double YMax
        {
            get { return PropertiesDAO.YMax * ZoomPonderation; }
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
