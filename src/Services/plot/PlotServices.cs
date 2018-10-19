using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using IGUWPF.src.controller.calculator;
using IGUWPF.src.IO;
using IGUWPF.src.models;
using IGUWPF.src.models.Model;
using IGUWPF.src.models.ViewModel;
using IGUWPF.src.utils;

namespace IGUWPF.src.controllers.ControllersImpl
{

    public class PlotServices
    {

        /*
         NOTE FOR THE FUTURE FRAN
         If you are thinking that the filter will cause the deletion of a desired line when
         two points are to far away, you are mistaken. Because the distance between two points
         is 1 px in all cases 
        */
        public static PointCollection[] CalculatePlot(ICalculator Calculator, double Width, double Height, PlotRepresentationSettings RepresentationValues)
        {
            double x, y, realX;
            bool WasLastBig, WasLastSmall;
            PointCollection CurrentSegment = new PointCollection();
            List<PointCollection> PointCollectionList = new List<PointCollection>();

            WasLastBig = WasLastSmall = false;
            //Generate plot
            for (int i = 0; i < Width; i++)
            {
                x = i;
                realX = ParseXScreenPointToRealPoint(i, Width, RepresentationValues);
                y = ParseYRealPointToScreenPoint(Calculator.Calculate(realX), Height, RepresentationValues);

                //Filter to avoid unwished lines
                if (y < 0) //The point is smaller
                {
                    if (WasLastBig)
                    {
                        PointCollectionList.Add(CurrentSegment);
                        CurrentSegment = new PointCollection();
                    }
                    else
                    {
                        WasLastBig = false;
                        WasLastSmall = true;
                    }
                }
                else if (y >= Height) //The point is bigger
                { 
                    if (WasLastSmall)
                    {
                        PointCollectionList.Add(CurrentSegment);
                        CurrentSegment = new PointCollection();
                    }
                    else
                    {
                        WasLastBig = true;
                        WasLastSmall = false;
                    }
                }
                else
                {
                    WasLastBig = WasLastSmall = false;
                }

                CurrentSegment.Add(new Point(x, y));
            }
            PointCollectionList.Add(CurrentSegment);

            return PointCollectionList.ToArray();
        }


        public static Line[] GetAxys(double Width, double Height, PlotRepresentationSettings RepresentationValues)
        {

            Line[] Axys = new Line[2];
            //X Axys
            Axys[0] = new Line()
            {
                Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                X1 = 0,
                X2 = Width,
                Name = "FX"
            };

            if (RepresentationValues.XMin < 0 && RepresentationValues.XMax > 0)
            {
                double PosY = ParseYRealPointToScreenPoint(0, Height, RepresentationValues);
                Axys[0].Y1 = PosY;
                Axys[0].Y2 = PosY;
            }
            else
            {
                Axys[0].Visibility = Visibility.Hidden;
            }

            //Y Axys
            Axys[1] = new Line()
            {
                Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                Y1 = 0,
                Y2 = Height,
                Name = "FY"
            };

            if (RepresentationValues.YMin < 0 && RepresentationValues.YMax > 0)
            {
                double PosX = ParseXRealPointToScreenPoint(0, Width, RepresentationValues);
                Axys[1].X1 = PosX;
                Axys[1].X2 = PosX;
            }
            else
            {
                Axys[1].Visibility = Visibility.Hidden;
            }

            Axys[0].StrokeThickness = 1;
            Axys[1].StrokeThickness = 1;

            return Axys;
        }

        public static double ParseXRealPointToScreenPoint(double x, double Width, PlotRepresentationSettings RepresentationValues)
        {
            return Width * ((x - RepresentationValues.XMin) / (RepresentationValues.XMax - RepresentationValues.XMin));
        }
        public static double ParseYRealPointToScreenPoint(double y, double Height, PlotRepresentationSettings RepresentationValues)
        {
            return Height * (1 - ((y - RepresentationValues.YMin) / (RepresentationValues.YMax - RepresentationValues.YMin)));
        }
        public static double ParseXScreenPointToRealPoint(double x, double Width, PlotRepresentationSettings RepresentationValues)
        {
            return ((RepresentationValues.XMax - RepresentationValues.XMin) * x / Width) + RepresentationValues.XMin;
        }
        public static double ParseYScreenPointToRealPoint(double y, double Height, PlotRepresentationSettings RepresentationValues)
        {
            return -(((RepresentationValues.YMax - RepresentationValues.YMin) * y / Height) + RepresentationValues.YMin);
        }

        public static string GetPlotName(int ID)
        {
            return "F" + ID;
        }
    }
}
