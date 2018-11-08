using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using IGUWPF.src.view.Windows;
using System.Collections.Generic;
using Microsoft.Win32;
using IGUWPF.src.services.plot;
using IGUWPF.src.models.ViewModel;
using IGUWPF.src.bean;
using IGUWPF.src.services.IO;
using System.Windows.Threading;

namespace IGUWPF
{

    public partial class MainWindow : Window
    {
        private double PlotWidth  { get => PlotPanel.ActualWidth;  }
        private double PlotHeight { get => PlotPanel.ActualHeight; }

        private Label ZoomLabel;
        private Line[] CursorAxys;
        private Label XYMouseCoordinates;

        private FunctionLitsUI FunctionListUI;
        private FunctionViewModelImpl ViewModel;
        private DispatcherTimer RefreshPlotPanelTimer;

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new FunctionViewModelImpl();

            //Timer to improve the refresh of the plot panel performance
            RefreshPlotPanelTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(Constants.NumberOfMsBeforePlotRecalculation)
            };

            //Give default values
            ViewModel.RepresentationParameters = new RepresentationParameters() {
                XMin = -10,
                XMax = 10,
                YMin = -10,
                YMax = 10
            };
            ViewModel.ZoomPonderation = 1;

            //Create the UI elements which are changed during the execution
            XYMouseCoordinates = new Label()
            {
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.DodgerBlue,
                Foreground = Brushes.DodgerBlue,
                Background = Brushes.AliceBlue,
                Visibility = Visibility.Hidden,
            };
            ZoomLabel = new Label()
            {
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.DodgerBlue,
                Foreground = Brushes.DodgerBlue,
                Background = Brushes.AliceBlue,
                Visibility = Visibility.Hidden
            };
            CursorAxys = PlotServices.GetAxys(PlotWidth,PlotHeight,ViewModel.RepresentationParameters);
                CursorAxys[0].Stroke = CursorAxys[1].Stroke = Brushes.DodgerBlue;
                CursorAxys[0].Visibility = CursorAxys[1].Visibility = Visibility.Hidden;

            //ViewModel events
            ViewModel.ElementCreated += ViewModel_ElementCreated;
            ViewModel.ElementDeleted += ViewModel_ElementDeleted;
            ViewModel.ElementUpdated += ViewModel_ElementUpdated;
            ViewModel.DeleteAll += ViewModel_DeleteAll;
            ViewModel.UpdateAll += RefreshPlotPanel;
            //Reload panel if size changes
            this.SizeChanged += ResetRefreshPlotPanelTimer;
            RefreshPlotPanelTimer.Tick += RefreshPlotPanel;
            //Mouse position events
            PlotPanel.MouseEnter += ShowCursorPositionElements;
            PlotPanel.MouseLeave += HideCursorPositionElements;
            PlotPanel.MouseMove += CalculateMousePosition;
            PlotPanel.MouseWheel += MakeZoom;
            PlotPanel.MouseWheel += CalculateMousePosition;
            //Contextual menus events
            WindowContextMenu_ExportImage.Click += ExportImage;
            //Closing event
            this.Closed += WhenClosed;

            FunctionListUI = new FunctionLitsUI(ViewModel);
            FunctionListUI.Closed += WhenClosed;
            FunctionListUI.Show();
        }

        #region ViewModelEvents
        private void ViewModel_ElementCreated(object sender, ViewModelEventArgs e) {
            PointCollection [] Segments = null;
            Function Function = (Function)e.Element;

            if (!Function.IsHidden)
            {
                Segments = PlotServices.CalculatePlot(Function.Calculator, this.PlotWidth, this.PlotHeight, ViewModel.PonderedRepresentationParameters);

                int i = 0;
                foreach (PointCollection Points in Segments)
                {
                    PlotPanel.Children.Add(new Polyline()
                    {
                        Points = Points,
                        Stroke = new SolidColorBrush(Function.Color),
                        Name = PlotServices.GetPlotName(Function.ID) + "S" + (i++)
                    });
                }

            }
        }

        private void ViewModel_ElementDeleted(object sender, ViewModelEventArgs e)
        {
            string PlotName = null;
            Polyline Polyline = null;
            Function Function = (Function)e.Element;
            List<Polyline> PolylineList = new List<Polyline>();

            PlotName = PlotServices.GetPlotName(Function.ID);

            foreach (UIElement Element in PlotPanel.Children) {
                if (Element is Polyline)
                {
                    Polyline = (Polyline)Element;
                    if (Polyline.Name.Contains(PlotName))
                        PolylineList.Add((Polyline)Element);
                }
            }

            foreach (Polyline Element in PolylineList)
                PlotPanel.Children.Remove(Element);
        }

        private void ViewModel_ElementUpdated(object sender, ViewModelEventArgs e)
        {
            string PlotName = null;
            Polyline Polyline = null;
            PointCollection[] Segments = null;
            Function Function = (Function)e.Element;
            List<Polyline> PolylineList = new List<Polyline>();

            //Delete older plot
            PlotName = PlotServices.GetPlotName(Function.ID);

            foreach (UIElement Element in PlotPanel.Children)
            {
                if (Element is Polyline)
                {
                    Polyline = (Polyline)Element;
                    if (Polyline.Name.Contains(PlotName))
                        PolylineList.Add((Polyline)Element);
                }
            }

            foreach (Polyline Element in PolylineList)
                PlotPanel.Children.Remove(Element);

            if (!Function.IsHidden)
            {
                //Get new plot
                Segments = PlotServices.CalculatePlot(Function.Calculator, this.PlotWidth, this.PlotHeight, ViewModel.PonderedRepresentationParameters);

                int i = 0;
                foreach(PointCollection Points in Segments)
                {
                    PlotPanel.Children.Add(new Polyline()
                    {
                        Points = Points,
                        Stroke = new SolidColorBrush(Function.Color),
                        Name = PlotServices.GetPlotName(Function.ID) + "S" + (i++),
                    });
                }
            }
        }

        private void ViewModel_DeleteAll(object sender, ViewModelEventArgs e)
        {
            PlotPanel.Children.Clear();
            AddPlotPanelBasics();
        }
        #endregion

        private void ShowCursorPositionElements(object sender, MouseEventArgs e)
        {
            double ZoomValue;

            ZoomValue = Math.Truncate(ViewModel.ZoomPonderation * 100);
            ZoomLabel.Content = "Zoom: " + ZoomValue + "%";

            XYMouseCoordinates.Visibility = Visibility.Visible;
            ZoomLabel.Visibility = Visibility.Visible;
            CursorAxys[0].Visibility = Visibility.Visible;
            CursorAxys[1].Visibility = Visibility.Visible;
        }

        private void HideCursorPositionElements(object sender, MouseEventArgs e)
        {
            ZoomLabel.Visibility = Visibility.Hidden;
            XYMouseCoordinates.Visibility = Visibility.Hidden;
            CursorAxys[0].Visibility = Visibility.Hidden;
            CursorAxys[1].Visibility = Visibility.Hidden;
        }

        private void CalculateMousePosition(object sender, MouseEventArgs e)
        {
            double RealX, RealY, ScreenX, ScreenY;

            //Obtain the mouse pointer coordinates
            Panel MousePanel = (Panel)sender;
            Point p = e.GetPosition(MousePanel);

            //Calculate real points
            ScreenX = p.X;
            ScreenY = p.Y;
            RealX = double.Parse(string.Format("{0:n2}", (PlotServices.ParseXScreenPointToRealPoint(ScreenX, MousePanel.ActualWidth, ViewModel.PonderedRepresentationParameters) * 100) / 100));
            RealY = double.Parse(string.Format("{0:n2}", (PlotServices.ParseYScreenPointToRealPoint(ScreenY, MousePanel.ActualHeight, ViewModel.PonderedRepresentationParameters) * 100) / 100));

            //Update label
            if (null != XYMouseCoordinates)
                XYMouseCoordinates.Content = "X: " + RealX + " Y: " + RealY;

            //Update cursor axys
            CursorAxys[0].X1 = 0;
            CursorAxys[0].X2 = PlotWidth;
            CursorAxys[0].Y1 = CursorAxys[0].Y2 = ScreenY;
            CursorAxys[1].Y1 = 0;
            CursorAxys[1].Y2 = PlotHeight;
            CursorAxys[1].X1 = CursorAxys[1].X2 = ScreenX;
        }

        private void MakeZoom(object sender, MouseWheelEventArgs e)
        {
            double ZoomValue, ToAddToZoomValue;

            ToAddToZoomValue = (e.Delta > 0) ? (-0.05) : (0.05);
            if (!((ToAddToZoomValue < 0 && ViewModel.ZoomPonderation <= 0.1) || (ToAddToZoomValue > 0 && ViewModel.ZoomPonderation >= 5)))
                ViewModel.ZoomPonderation += ToAddToZoomValue;

            ZoomValue = Math.Truncate(ViewModel.ZoomPonderation * 100); ;
            ZoomLabel.Content = "Zoom: " + ZoomValue + "%";
        }

        private void ResetRefreshPlotPanelTimer(object sender, EventArgs e) {
            RefreshPlotPanelTimer.Stop();
            RefreshPlotPanelTimer.Start();
        }

        private void RefreshPlotPanel(object sender, EventArgs e)
        {
            Polyline Polyline = null;
            PointCollection[] Segments = null;

            //Stop the timer
            RefreshPlotPanelTimer.Stop();

            //Clear the panel
            PlotPanel.Children.Clear();
            AddPlotPanelBasics();

            //Redraw all functions
            foreach (Function Function in ViewModel.GetAllElements())
            {
                if (!Function.IsHidden)
                {
                    Segments = PlotServices.CalculatePlot(Function.Calculator, this.PlotWidth, this.PlotHeight, ViewModel.PonderedRepresentationParameters);

                    for (int i = 0; i < Segments.Length; i++)
                    {
                        Polyline = new Polyline() {
                            Points = Segments[i],
                            Name = PlotServices.GetPlotName(Function.ID) + i,
                            Stroke = new SolidColorBrush(Function.Color)
                        };
                        PlotPanel.Children.Add(Polyline);
                    }
                }
            }
        }

        private void WhenClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ExportImage(object sender, RoutedEventArgs e)
        {
            //Show dialog to choose the path to export
            SaveFileDialog SaveFileForm = new SaveFileDialog() {
                Title = "Exportar representacion",
                FileName = "Desktop",
                DefaultExt = ".png",
                Filter = "PNG image (.png)|*.png",
                AddExtension = true
            };

            bool result = (bool)SaveFileForm.ShowDialog();
            if (false == result)
                return;
            
            //Export the image
            result = IOServices.ExportPlot(SaveFileForm.FileName, PlotPanel);
            if (result == false)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void AddPlotPanelBasics() {
            PlotPanel.Children.Add(CursorAxys[0]);
            PlotPanel.Children.Add(CursorAxys[1]);

            PlotPanel.Children.Add(XYMouseCoordinates);
                Canvas.SetRight(XYMouseCoordinates, 0);
                Canvas.SetBottom(XYMouseCoordinates, 0);
            //Give a high zIndex to ensure that is over all functions
            Panel.SetZIndex(XYMouseCoordinates, 300); 

            PlotPanel.Children.Add(ZoomLabel);
                Canvas.SetLeft(ZoomLabel, 0);
                Canvas.SetTop(ZoomLabel, 0);
            //Give a high zIndex to ensure that is over all functions
            Panel.SetZIndex(ZoomLabel, 300); 

            Line[] Axys = PlotServices.GetAxys(this.PlotWidth, this.PlotHeight, ViewModel.PonderedRepresentationParameters);
                PlotPanel.Children.Add(Axys[0]);
                PlotPanel.Children.Add(Axys[1]);
        }

    }
}
