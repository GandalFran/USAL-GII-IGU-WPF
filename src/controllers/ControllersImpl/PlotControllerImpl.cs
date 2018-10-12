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
using IGUWPF.src.models.Model;
using IGUWPF.src.utils;
using static IGUWPF.src.utils.Enumerations;
using MathParser = org.mariuszgromada.math.mxparser;

namespace IGUWPF.src.controllers.ControllersImpl
{
    public class PlotControllerImpl : IPlotController
    {

        public double RealXMin { get; set; }
        public double RealXMax { get; set; }
        public double RealYMin { get; set; }
        public double RealYMax { get; set; }

        private Line[] Axys;

        private Panel PlotPanel;
        private IDAO<Panel> PlotDAO;
        private Dictionary<Function,Polyline> PlotModel;

        public PlotControllerImpl(Panel PlotPanel)
        {
            this.PlotPanel = PlotPanel;

            this.Axys = new Line[2];
            PlotDAO = new ImageDAOImpl();
            this.PlotModel = new Dictionary<Function, Polyline>();
        }

        public int Add(Function Element)
        {
            Polyline Plot = new Polyline();

            Plot.Points = CalculatePlotPoints(Element);
            Plot.Name = "F" + Element.GetID();
            Plot.Stroke = new SolidColorBrush(Element.Color);
            if (Element.IsHidden)
                Plot.Visibility = Visibility.Hidden;

            PlotModel.Add(Element,Plot);
            PlotPanel.Children.Add(Plot);

            return Element.GetID();
        }


        public bool Delete(Function Element)
        {
            if (!this.PlotModel.Keys.Contains(Element))
            {
                return false;
            }
            else
            {
                PlotPanel.Children.Remove(PlotModel[Element]);
                PlotModel.Remove(Element);
                return true;
            }
        }

        public bool Update(Function Element, params PlotUpdateType[] Type)
        {
            Polyline PolyLineToUpdate;

            if (!this.PlotModel.Keys.Contains(Element))
                return false;
            else
                PolyLineToUpdate = this.PlotModel[Element];

            foreach (PlotUpdateType Change in Type){
                switch (Change) {
                    case PlotUpdateType.COLOR:
                        PolyLineToUpdate.Stroke = new SolidColorBrush( Element.Color );
                        break;
                    case PlotUpdateType.RECALCULATION:
                        PolyLineToUpdate.Points = CalculatePlotPoints(Element);
                        break;
                    case PlotUpdateType.VISIBILITY:
                        if (Element.IsHidden)
                            PolyLineToUpdate.Visibility = Visibility.Hidden;
                        else
                            PolyLineToUpdate.Visibility = Visibility.Visible;
                        break;
                }
            }
            return true;
        }

        public void Clear()
        {
            this.PlotPanel.Children.Clear();
            this.PlotModel.Clear();
        }

        public bool ExportPlot(string DataPath)
        {
            return PlotDAO.ExportSingleObject(DataPath, PlotPanel);
        }

        public void UpdateAxys()
        {
            //X Axys
            if (RealXMin < 0 && RealXMax > 0)
            {
                double PosY = ParseYRealPointToScreenPoint(0);
                if (null == Axys[0])
                {
                    Axys[0] = new Line()
                    {
                        Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                        X1 = 0,
                        X2 = PlotPanel.ActualWidth,
                        Y1 = PosY,
                        Y2 = PosY,
                        Name = "FX"
                    };
                }
                else
                {
                    Axys[0].Y1 = PosY;
                    Axys[0].Y2 = PosY;
                }
            }
            else if (null != Axys[0]) {
                Axys[0] = null;
            }
            //Y Axys
            if (RealYMin < 0 && RealYMax > 0)
            {
                double PosX = ParseXRealPointToScreenPoint(0);
                if (null == Axys[1])
                {
                    Axys[1] = new Line()
                    {
                        Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                        Y1 = 0,
                        Y2 = PlotPanel.ActualHeight,
                        X1 = PosX,
                        X2 = PosX,
                        Name = "FY"
                    };
                }
                else
                {
                    Axys[1].X1 = PosX;
                    Axys[1].X2 = PosX;
                }
            }
            else if (null != Axys[1])
            {
                Axys[1] = null;
            }
        }

        private PointCollection CalculatePlotPoints(Function Element)
        {
            double x, y;
            List<Point> PointDataList = new List<Point>();
            MathParser.Argument xArg = new MathParser.Argument("x");
            MathParser.Expression ParserExpression = new MathParser.Expression(Element.Expression);

            //Generate plot
            ParserExpression.addArguments(xArg);
            for (int i = 0; i < PlotPanel.ActualWidth; i++) {
                xArg.setArgumentValue( ParseXScreenPointToRealPoint(i) );
                x = i;
                y = ParseYRealPointToScreenPoint( ParserExpression.calculate() );
                //Remove if is too big
                if( y>0 && y< PlotPanel.ActualHeight)
                    PointDataList.Add( new Point(x,y));
            }
            //Generate point collection
            return new PointCollection( PointDataList );
        }

        private double ParseXRealPointToScreenPoint(double x)
        {
            return PlotPanel.ActualWidth * (1 - ((x - RealXMin) / (RealXMax - RealXMin)));
        }
        private double ParseYRealPointToScreenPoint(double y)
        {
            return PlotPanel.ActualHeight * (1 - ((y - RealYMin) / (RealYMax - RealYMin)));
        }
        private double ParseXScreenPointToRealPoint(double x)
        {
            return ((RealXMax-RealXMin) * x / PlotPanel.ActualWidth) + RealXMin;
        }
        private double ParseYScreenPointToRealPoint(double y)
        {
            return - (((RealYMax - RealYMin) * y / PlotPanel.ActualHeight) + RealYMin);
        }


    }
}
