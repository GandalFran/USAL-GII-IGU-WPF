using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using IGUWPF.src.models.ViewModel;
using IGUWPF.src.services.calculator;

namespace IGUWPF.src.services.plot
{

    public class PlotServices
    {

        public static PointCollection[] CalculatePlot(Calculator Calculator, double Width, double Height, RepresentationParameters RepresentationParameters)
        {
            double ScreenX, ScreenY, RealX, RealY;
            PointCollection Plot = new PointCollection();
            List<PointCollection> PointCollectionList = null;

            for (int i = 0; i < Width; i++)
            {
                RealX = ParseXScreenPointToRealPoint(i, Width, RepresentationParameters);
                RealY = Calculator.Calculate(RealX);
                ScreenX = i;
                ScreenY = ParseYRealPointToScreenPoint(RealY, Height, RepresentationParameters);

                Plot.Add(new Point(ScreenX, ScreenY));
            }

            PointCollectionList = SlplitPlot(Plot, Width, Height, RepresentationParameters);

            return PointCollectionList.ToArray();
        }

        public static List<PointCollection> SlplitPlot(PointCollection Plot, double Width, double Height, RepresentationParameters RepresentationParameters)
        {
            PointCollection CurrentSegment = new PointCollection();
            List<PointCollection> PointCollectionList = new List<PointCollection>();

            CurrentSegment.Add(Plot[0]);
            for (int i = 1; i < Width; i++) {
                
                if (Math.Abs(Plot[i - 1].Y - Plot[i].Y) > Height )
                {
                    PointCollectionList.Add(CurrentSegment);
                    CurrentSegment = new PointCollection();
                }else
                    CurrentSegment.Add(Plot[i]);
            }

            PointCollectionList.Add(CurrentSegment);

            return PointCollectionList;
        }

        public static Line[] GetAxys(double Width, double Height, RepresentationParameters RepresentationParameters)
        {
            Line[] Axys = new Line[2];

            //X Axys
            double PosY = ParseYRealPointToScreenPoint(0, Height, RepresentationParameters);
            Axys[0] = new Line()
            {
                Stroke = Brushes.DarkGray,
                X1 = 0,
                X2 = Width,
                Y1 = PosY, 
                Y2 = PosY
            };
            //Y Axys
            double PosX = ParseXRealPointToScreenPoint(0, Width, RepresentationParameters);
            Axys[1] = new Line()
            {
                Stroke = Brushes.DarkGray,
                Y1 = 0,
                Y2 = Height,
                X1 = PosX, 
                X2 = PosX
            };

            return Axys;
        }

        public static double ParseXRealPointToScreenPoint(double x, double Width, RepresentationParameters RepresentationParameters)
        {
            return Width * ((x - RepresentationParameters.XMin) / (RepresentationParameters.XMax - RepresentationParameters.XMin));
        }

        public static double ParseYRealPointToScreenPoint(double y, double Height, RepresentationParameters RepresentationParameters)
        {
            return Height * (1 - ((y - RepresentationParameters.YMin) / (RepresentationParameters.YMax - RepresentationParameters.YMin)));
        }

        public static double ParseXScreenPointToRealPoint(double x, double Width, RepresentationParameters RepresentationParameters)
        {
            return ((RepresentationParameters.XMax - RepresentationParameters.XMin) * x / Width) + RepresentationParameters.XMin;
        }

        public static double ParseYScreenPointToRealPoint(double y, double Height, RepresentationParameters RepresentationParameters)
        {
            return RepresentationParameters.YMin - ((RepresentationParameters.YMax - RepresentationParameters.YMin) * (y - Height) / Height);
        }

        public static string GetPlotName(int ID)
        {
            return "F" + ID;
        }

    }
}
