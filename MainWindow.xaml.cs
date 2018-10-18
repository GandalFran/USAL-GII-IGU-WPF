using IGUWPF.src.controllers;
using IGUWPF.src.models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using IGUWPF.src.view.Windows;
using IGUWPF.src.controllers.ControllersImpl;
using IGUWPF.src.models.ViewModel;
using System.Collections.Generic;
using Microsoft.Win32;

namespace IGUWPF
{

    public partial class MainWindow : Window
    {
        private double PlotWidth { get => PlotPanel.ActualWidth; }
        private double PlotHeight { get => PlotPanel.ActualHeight; }

        private Label XYMouseCoordinates;

        private IDAO<Function> FunctionDAO;
        private FunctionViewModelImpl ViewModel;

        private FunctionListWindow FunctionListWindow;

        public MainWindow()
        {
            InitializeComponent();
            //Instance components
            ViewModel = new FunctionViewModelImpl();
            FunctionDAO = new SerialDAOImpl<Function>();

            //Create the label to know the Cursor position
            XYMouseCoordinates = new Label()
            {
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.DodgerBlue,
                Foreground = Brushes.DodgerBlue,
                Background = Brushes.AliceBlue,
                Visibility = Visibility.Hidden
            };

            //Give default values
            ViewModel.XMin = ViewModel.YMin = -10;
            ViewModel.XMax = ViewModel.YMax = 10;

            //Add event handlers
            //Reload panel if size changes
            PlotPanel.SizeChanged += RefreshPlotPanel;
            //Mouse position events
            PlotPanel.MouseEnter += SetMousePositionLabelVissible;
            PlotPanel.MouseLeave += SetMousePositionLabelHidden;
            PlotPanel.MouseMove += CalculateMousePosition;
            //ViewModel events
            ViewModel.ClearEvent += ViewModelClearEvent;
            ViewModel.CreateElementEvent += ViewModelCreateElementEvent;
            ViewModel.DeleteElementEvent += ViewModelDeleteElementEvent;
            ViewModel.UpdateElementEvent += ViewModelUpdateElementEvent;
            ViewModel.UpdatePlotSettingsEvent += RefreshPlotPanel;
            //Contextual menus events
            WindowContextMenu_ExportImage.Click += ExportImage;
            //Closing event
            this.Closed += WhenClosed;

            //FunctionListWindow processing
            FunctionListWindow = new FunctionListWindow(ViewModel);
            FunctionListWindow.Closed += WhenClosed;
            FunctionListWindow.Show();
        }

        private void ViewModelCreateElementEvent(object sender, ViewModelEventArgs e) {
            Polyline Polyline = null;
            PointCollection [] Segments = null;
            Function Function = (Function)e.Element;

            Segments = PlotServices.CalculatePlot(Function.Calculator, this.PlotWidth, this.PlotHeight, ViewModel.PlotSettings);

            for (int i = 0; i < Segments.Length; i++)
            {
                Polyline = new Polyline();

                Polyline.Points = Segments[i];
                Polyline.Name = PlotServices.GetPlotName(Function.ID) + i;
                Polyline.Stroke = new SolidColorBrush(Function.Color);
                PlotPanel.Children.Add(Polyline);
            }
        }

        private void ViewModelDeleteElementEvent(object sender, ViewModelEventArgs e)
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

        private void ViewModelUpdateElementEvent(object sender, ViewModelEventArgs e)
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

            //Get new plot
            Segments = PlotServices.CalculatePlot(Function.Calculator, this.PlotWidth, this.PlotHeight, ViewModel.PlotSettings);

            for (int i = 0; i < Segments.Length; i++)
            {
                Polyline = new Polyline();

                Polyline.Points = Segments[i];
                Polyline.Name = PlotServices.GetPlotName(Function.ID) + i;
                Polyline.Stroke = new SolidColorBrush(Function.Color);
                PlotPanel.Children.Add(Polyline);
            }
        }

        private void ViewModelClearEvent(object sender, ViewModelEventArgs e)
        {
            //Clear the panel
            PlotPanel.Children.Clear();

            //Add the Label to know the plot position
            PlotPanel.Children.Add(XYMouseCoordinates);
            Canvas.SetRight(XYMouseCoordinates, 0);
            Canvas.SetBottom(XYMouseCoordinates, 0);

            //Add axys
            Line[] Axys = PlotServices.GetAxys(this.PlotWidth, this.PlotHeight, ViewModel.PlotSettings);
            PlotPanel.Children.Add(Axys[0]);
            PlotPanel.Children.Add(Axys[1]);
        }

        private void SetMousePositionLabelVissible(object sender, MouseEventArgs e)
        {
            XYMouseCoordinates.Visibility = Visibility.Visible;
        }
        private void SetMousePositionLabelHidden(object sender, MouseEventArgs e)
        {
            XYMouseCoordinates.Visibility = Visibility.Hidden;
        }
        private void CalculateMousePosition(object sender, MouseEventArgs e)
        {
            double realX, realY;

            //Obtain the mouse pointer coordinates
            Panel MousePanel = (Panel)sender;
            Point p = e.GetPosition(MousePanel);

            //Calculate real points
            realX = Math.Truncate(PlotServices.ParseXScreenPointToRealPoint(p.X, MousePanel.ActualWidth, ViewModel.PlotSettings));
            realY = Math.Truncate(PlotServices.ParseYScreenPointToRealPoint(p.Y, MousePanel.ActualHeight, ViewModel.PlotSettings));

            //Update label
            if (null != XYMouseCoordinates)
                XYMouseCoordinates.Content = "X: " + realX + " Y: " + realY;
        }

        private void ExportImage(object sender, RoutedEventArgs e)
        {
            //Show dialog to choose the path to export
            SaveFileDialog SaveFileForm = new SaveFileDialog();
            SaveFileForm.Title = "Export plot";
            SaveFileForm.FileName = "Desktop";
            SaveFileForm.DefaultExt = ".png";
            SaveFileForm.Filter = "PNG image (.png)|*.png";
            SaveFileForm.AddExtension = true;

            Nullable<bool> result = SaveFileForm.ShowDialog();
            if (null == result)
                return;
            
            //Export the image
            result = IOServices.ExportPlot(SaveFileForm.FileName, PlotPanel);
            if (result == false)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void RefreshPlotPanel(object sender, EventArgs e)
        {
            Polyline Polyline = null;
            PointCollection[] Segments = null;
            //Clear the panel
            PlotPanel.Children.Clear();

            //Add the Label to know the plot position
            PlotPanel.Children.Add(XYMouseCoordinates);
            Canvas.SetRight(XYMouseCoordinates, 0);
            Canvas.SetBottom(XYMouseCoordinates, 0);

            //Add axys
            Line[] Axys = PlotServices.GetAxys(this.PlotWidth, this.PlotHeight, ViewModel.PlotSettings);
            PlotPanel.Children.Add(Axys[0]);
            PlotPanel.Children.Add(Axys[1]);

            //Redraw all functions
            foreach (Function Function in ViewModel.GetAllElements()) {
                if (Function.IsHidden)
                {
                    Segments = PlotServices.CalculatePlot(Function.Calculator, this.PlotWidth, this.PlotHeight, ViewModel.PlotSettings);

                    for (int i = 0; i < Segments.Length; i++)
                    {
                        Polyline = new Polyline();

                        Polyline.Points = Segments[i];
                        Polyline.Name = PlotServices.GetPlotName(Function.ID) + i;
                        Polyline.Stroke = new SolidColorBrush(Function.Color);
                        PlotPanel.Children.Add(Polyline);
                    }
                }
            }
        }

        private void WhenClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
