﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using IGUWPF.src.controller.calculator;
using IGUWPF.src.models.ViewModel;

namespace IGUWPF.src.controllers.ControllersImpl
{
    public class PlotServices
    {
        /*NOTE
         If you are thinking that the filter will cause the deletion of a desired line when
         two points are to far away, you are mistaken. Because the distance between two points
         is 1 px in all cases*/
        public static PointCollection[] CalculatePlot(ICalculator Calculator, double Width, double Height, PlotRepresentationSettings RepresentationValues)
        {
            double ScreenX, ScreenY, RealX,RealY;
            bool WasLastBig, WasLastSmall;
            PointCollection CurrentSegment = new PointCollection();
            List<PointCollection> PointCollectionList = new List<PointCollection>();

            WasLastBig = WasLastSmall = false;
            for (int i = 0; i < Width; i++)
            {
                ScreenX = i;
                RealX = ParseXScreenPointToRealPoint(i, Width, RepresentationValues);
                RealY = Calculator.Calculate(RealX);
                ScreenY = ParseYRealPointToScreenPoint( RealY, Height, RepresentationValues);

                //Filter to avoid unwished lines
                if (RealY <= RepresentationValues.YMin)
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
                else if (RealY >= RepresentationValues.YMax)
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

                CurrentSegment.Add(new Point(ScreenX, ScreenY));
            }
            PointCollectionList.Add(CurrentSegment);

            return PointCollectionList.ToArray();
        }

        public static Line[] GetAxys(double Width, double Height, PlotRepresentationSettings RepresentationValues)
        {
            Line[] Axys = new Line[2];

            //X Axys
            double PosY = ParseYRealPointToScreenPoint(0, Height, RepresentationValues);
            Axys[0] = new Line()
            {
                Stroke = Brushes.DarkGray,
                X1 = 0,
                X2 = Width,
                Y1 = PosY, 
                Y2 = PosY
            };

            //Y Axys
            double PosX = ParseXRealPointToScreenPoint(0, Width, RepresentationValues);
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
