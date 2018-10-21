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
        /*NOTE FOR FUTURE FRAN
         If you are thinking that the filter will cause the deletion of a desired line when
         two points are to far away, you are mistaken. Because the distance between two points
         is 1 px in all cases*/

        public static PointCollection[] CalculatePlot(Calculator Calculator, double Width, double Height, RepresentationParameters RepresentationValues)
        {
            double ScreenX, ScreenY, RealX, RealY;
            PointCollection Plot = new PointCollection();
            PointCollection Plot2 = new PointCollection();
            List<PointCollection> PointCollectionList = null;

            PointCollectionList = new List<PointCollection>();

            for (int i = 0; i < Width; i++)
            {
                RealX = ParseXScreenPointToRealPoint(i, Width, RepresentationValues);
                RealY = Calculator.Calculate(RealX);
                ScreenX = i;
                ScreenY = ParseYRealPointToScreenPoint(RealY, Height, RepresentationValues);

                if (RealX < 0)
                {
                    Plot.Add(new Point(ScreenX, ScreenY));
                }
                if( RealX > 0)
                {
                    Plot2.Add(new Point(ScreenX, ScreenY));
                }
             
            }

            PointCollectionList.Add(Plot);
            PointCollectionList.Add(Plot2);
            //PointCollectionList = PlotSplitter(Plot, Width, Height, RepresentationValues);


            return PointCollectionList.ToArray();
        }

        private static List<PointCollection> PlotSplitter(PointCollection Plot, double Width, double Height, RepresentationParameters RepresentationValues)
        {
            PointCollection CurrentSegment = new PointCollection();
            List<PointCollection> PointCollectionList = new List<PointCollection>();

            Console.WriteLine("-------------------------------------\n Width, Height" + Width + "   " + Height);

            CurrentSegment.Add(Plot[0]);
            for (int i = 1; i < Width; i++) {
                
                if (Math.Abs(Plot[i - 1].Y - Plot[i].Y) >=Height )
                {
                    if(Plot[i - 1].Y < 0)
                        CurrentSegment.Add( new Point(Plot[i-1].X, 0) );
                    else
                        CurrentSegment.Add(new Point(Plot[i-1].X, Height));

                    Console.WriteLine("NUEVA - BIG -> SMALL " + Plot[i - 1].X + ", " + Plot[i - 1].Y + " -- " + Plot[i].X + ", " + Plot[i].Y);
                    PointCollectionList.Add(CurrentSegment);
                    CurrentSegment = new PointCollection();

                    if (Plot[i].Y < 0)
                        CurrentSegment.Add(new Point(Plot[i].X, 0));
                    else
                        CurrentSegment.Add(new Point(Plot[i].X, Height));
                }else
                    CurrentSegment.Add(Plot[i]);
            }

            PointCollectionList.Add(CurrentSegment);
            Console.WriteLine("Number of segments " + PointCollectionList.Count );

            return PointCollectionList;
        }

        public static Line[] GetAxys(double Width, double Height, RepresentationParameters RepresentationValues)
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

        public static double ParseXRealPointToScreenPoint(double x, double Width, RepresentationParameters RepresentationValues)
        {
            return Width * ((x - RepresentationValues.XMin) / (RepresentationValues.XMax - RepresentationValues.XMin));
        }

        public static double ParseYRealPointToScreenPoint(double y, double Height, RepresentationParameters RepresentationValues)
        {
            return Height * (1 - ((y - RepresentationValues.YMin) / (RepresentationValues.YMax - RepresentationValues.YMin)));
        }

        public static double ParseXScreenPointToRealPoint(double x, double Width, RepresentationParameters RepresentationValues)
        {
            return ((RepresentationValues.XMax - RepresentationValues.XMin) * x / Width) + RepresentationValues.XMin;
        }

        public static double ParseYScreenPointToRealPoint(double y, double Height, RepresentationParameters RepresentationValues)
        {
            return -(((RepresentationValues.YMax - RepresentationValues.YMin) * y / Height) + RepresentationValues.YMin);
        }

        public static string GetPlotName(int ID)
        {
            return "F" + ID;
        }

    }
}
