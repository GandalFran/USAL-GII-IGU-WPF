using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using IGUWPF.src.IO;
using IGUWPF.src.models;
using MathParser=org.mariuszgromada.math.mxparser;

namespace IGUWPF.src.controllers.ControllersImpl
{
    public class PlotControllerImpl : IPlotController
    {

        public double RealXMin {get; set; }
        public double RealXMax { get; set; }
        public double RealYMin { get; set; }
        public double RealYMax { get; set; }
        public double PanelWidth { get; set; }
        public double PanelHeight { get; set; }

        private IDAO<Panel> PlotDAO;

        public PlotControllerImpl()
        {
            PlotDAO = new ImageDAOImpl();
        }


        public Line[] GetAxys()
        {
            Line[] Axys = new Line[2] { null, null };

            //X Axys
            if (RealXMin < 0 && RealXMax > 0) {
                double PosY = RealYMax * (RealYMax - RealYMin) / PanelHeight;
                Axys[0] = new Line() {
                    Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    X1 = 0,
                    X2 = PanelWidth,
                    Y1 = PosY,
                    Y2 = PosY
                };

                Console.WriteLine(PosY);
            }

            //Y Axys
            if (RealYMin < 0 && RealYMax > 0)
            {
                double PosX = RealXMax * (RealXMax - RealXMin) / PanelWidth;
                Axys[1] = new Line()
                {
                    Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    Y1 = 0,
                    Y2 = PanelHeight,
                    X1 = PosX,
                    X2 = PosX
                };
            }
            
            return Axys;
        }

        public PointCollection CalculatePlotPoints(PlotData Plot)
        {
            double x, y, Increment;
            PointCollection ToReturn = null;
            List<Point> PointDataList = new List<Point>();

            MathParser.Argument xArg = new MathParser.Argument("x");
            MathParser.Expression ParserExpression = new MathParser.Expression(Plot.Expression);
            ParserExpression.addArguments( xArg );

            Increment = (RealXMax - RealXMin) / PanelWidth;

            for (int i = 0; i < PanelWidth; i++) {
                xArg.setArgumentValue(i * Increment + RealXMin);

                x = i;
                y = ParserExpression.calculate();
                PointDataList.Add( new Point(x,y));
            }

            DeletePointsDontFit(PointDataList);
            ParsePointsToScreenPoints( PointDataList );

            ToReturn = new PointCollection( PointDataList );

            return ToReturn;
        }

        public void ConfigurePolyLine(Function DataSource, Polyline ToConfigure, PointCollection Points)
        {
            ToConfigure.Name = DataSource.Name;
            ToConfigure.Stroke = new SolidColorBrush( DataSource.Color );
            ToConfigure.Points = Points;

        }

        public bool ExportPlot(string FilePath, Panel ToExport)
        {
            return PlotDAO.ExportSingleObject(FilePath, ToExport);
        }


        private void ParsePointsToScreenPoints(List<Point> ToParse)
        {
            ToParse.ForEach((Point p) =>
            {
                p.Y = PanelHeight*(1 - ((p.Y - RealYMin) / (RealYMax - RealYMin)));
            });
        }


        private void DeletePointsDontFit(List<Point> ToParse)
        {
            ToParse.RemoveAll((Point p) => p.Y > PanelHeight);
        }

    }
}
