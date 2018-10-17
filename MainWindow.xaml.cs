using IGUWPF.src.controllers;
using IGUWPF.src.models;
using IGUWPF.src.models.Model;
using IGUWPF.src.utils;
using IGUWPF.test;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IGUWPF.src.view;
using IGUWPF.src.view.Windows;
using IGUWPF.src.models.POJO;
using IGUWPF.src.controllers.ControllersImpl;
using static IGUWPF.src.utils.Enumerations;
using IGUWPF.src.controller.calculator;

namespace IGUWPF
{

    public partial class MainWindow : Window
    {
        private double PlotWidth { get => PlotPanel.ActualWidth; }
        private double PlotHeight { get => PlotPanel.ActualHeight; }

        private Label XYMouseCoordinates;

        private IDataModel<Function> Model;
        private IDAO<Function> FunctionDAO;
        private PlotRepresentationSettings PlotSettings;

        public MainWindow()
        {
            InitializeComponent();

            Model = new IDataModelImpl<Function>();
            FunctionDAO = new SerialDAOImpl<Function>();

            //Give defect values
            PlotSettings.XMin = PlotSettings.YMin = -10;
            PlotSettings.XMax = PlotSettings.YMax = 10;

            //Add event handlers
            AddFuncionButton.Click += AddFunction;
            SettingsButton.Click += EdditSettings;
            SaveFileButton.Click += SaveProject;
            OpenFileButton.Click += OpenProject;
            ExportImageButton.Click += ExportImage;

            PlotPanel.SizeChanged += ReloadPlotPanel;
            PlotPanel.MouseEnter += SetMousePositionLabelVissible;
            PlotPanel.MouseLeave += SetMousePositionLabelHidden;
            PlotPanel.MouseMove += CalculateMousePosition;

            //Create the label to know the Cursor position
            XYMouseCoordinates = new Label()
            {
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.DodgerBlue,
                Foreground = Brushes.DodgerBlue,
                Background = Brushes.AliceBlue,
                Visibility = Visibility.Hidden
            };
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
            realX = Math.Truncate( PlotServices.ParseXScreenPointToRealPoint(p.X, MousePanel.ActualWidth, PlotSettings) );
            realY = Math.Truncate( PlotServices.ParseYScreenPointToRealPoint(p.Y, MousePanel.ActualHeight, PlotSettings) );

            //Update label
            if(null != XYMouseCoordinates)
                XYMouseCoordinates.Content = "X: " + realX + " Y: " + realY;
        }

        private void AddFunction(object sender, RoutedEventArgs e)
        {
            //Display the formulary
            FunctionAddAndEditForm Form = new FunctionAddAndEditForm();
            Form.Title = "Anadir funcion";
            Form.A = Form.B = Form.C = 0;
            Form.Color = Color.FromRgb(0,0,0);
            Form.ShowDialog();
            if (false == Form.DialogResult)
                return;
            string FunctionName = Form.FunctionName;
            Color FunctionColor = Form.Color;
            ICalculator FunctionCalculator = Form.Calculator;

            //Create and save function
            Plot Plot = new Plot(FunctionColor);
            Function Function = new Function(FunctionName, FunctionCalculator, Plot);
            int ID = Model.CreateElement(Function);

            //Draw plot
            PlotServices.CalculatePlot(Function, this.PlotWidth, this.PlotHeight, PlotSettings);
            PlotPanel.Children.Add(Function.Plot.PlotPoints);

            //Add plot panel to the plot list
            UIFunctionPanel FunctionPanel = new UIFunctionPanel(ID, FunctionName, false);
                FunctionPanel.ViewButtonClickHandler += HideButtonFunction;
                FunctionPanel.EditButtonClickHandler += EditFunction;
                FunctionPanel.DeleteButtonClickHandler += DeleteFunction;
            FuncionListPanel.Children.Add(FunctionPanel);
        }

        private void ReloadPlotPanel(object sender, EventArgs e)
        {
            //Clear the panel
            PlotPanel.Children.Clear();
            //Add the Label to know the plot position
            PlotPanel.Children.Add(XYMouseCoordinates);
            Canvas.SetRight(XYMouseCoordinates, 0);
            Canvas.SetBottom(XYMouseCoordinates,0);
            //Add axys
            Line[] Axys = PlotServices.GetAxys(this.PlotWidth, this.PlotHeight, PlotSettings);
            PlotPanel.Children.Add( Axys[0] );
            PlotPanel.Children.Add( Axys[1] );

            //Add functions
            foreach (Function Function in Model.GetAllElements()) {
                PlotServices.CalculatePlot(Function, this.PlotWidth, this.PlotHeight, PlotSettings);
                PlotPanel.Children.Add(Function.Plot.PlotPoints);
            }
        }

        private void EdditSettings(object sender, RoutedEventArgs e)
        {
            double OlderXmin, OlderXmax, OlderYmin, OlderYmax;

            //Show dialog to edit de properties
            SettingsForm SettingsForm = new SettingsForm();
            //Load older values
            SettingsForm.Xmin = OlderXmin = PlotSettings.XMin;
            SettingsForm.Xmax = OlderXmax = PlotSettings.XMax;
            SettingsForm.Ymin = OlderYmin = PlotSettings.YMin;
            SettingsForm.Ymax = OlderYmax = PlotSettings.YMax;

            SettingsForm.ShowDialog();
            if (false == SettingsForm.DialogResult)
                return;

            PlotSettings.XMin = SettingsForm.Xmin;
            PlotSettings.XMax = SettingsForm.Xmax;
            PlotSettings.YMin = SettingsForm.Ymin;
            PlotSettings.YMax = SettingsForm.Ymax;

            //Check if the values has changed 
            if (PlotSettings.XMin == OlderXmin
                && PlotSettings.XMax == OlderXmax
                && PlotSettings.YMin == OlderYmin
                && PlotSettings.YMax == OlderYmax)
                return;

            //Reload plot panel
            ReloadPlotPanel(null,null);
        }

        private void SaveProject(object sender, RoutedEventArgs e)
        {
            //Show dialog to choose the path to save the project
            SaveFileDialog SaveFileForm = new SaveFileDialog();
            SaveFileForm.Title = "Save project";
            SaveFileForm.FileName = "Desktop"; // Default file name
            SaveFileForm.DefaultExt = ".maclab"; // Default file extension
            SaveFileForm.Filter = "MacLab Project (." + Constants.ProjectFileExtension + ")|*." + Constants.ProjectFileExtension;
            SaveFileForm.AddExtension = true;

            Nullable<bool> result = SaveFileForm.ShowDialog();
            if (false == result)
                return;

            //Export the file
            result = IOServices.ExportModel(SaveFileForm.FileName,Model);
            if (result == false)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void OpenProject(object sender, RoutedEventArgs e)
        {
            UIFunctionPanel FunctionPanel;

            //Show dialog to choose the project to import
            OpenFileDialog OpenFileForm = new OpenFileDialog();
            OpenFileForm.FileName = "Open project";
            OpenFileForm.FileName = "Desktop"; // Default file name
            OpenFileForm.DefaultExt = ".maclab"; // Default file extension
            OpenFileForm.Filter = "MacLab Project (." + Constants.ProjectFileExtension + ")|*." + Constants.ProjectFileExtension;
            OpenFileForm.Multiselect = false;

            Nullable<bool> result = OpenFileForm.ShowDialog();
            if (false == result)
                return;

            //Import the project
            result = IOServices.ImportModel(OpenFileForm.FileName, Model);
            if (result == false)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Redraw the window

            //Clear plot representation and plot list
            FuncionListPanel.Children.Clear();
            PlotPanel.Children.Clear();
            
            //Add axys
            Line[] Axys = PlotServices.GetAxys(this.PlotWidth, this.PlotHeight, PlotSettings);
            PlotPanel.Children.Add(Axys[0]);
            PlotPanel.Children.Add(Axys[1]);
            
            //Add functions to the left table
            foreach (Function Function in Model.GetAllElements()) {
                FunctionPanel = new UIFunctionPanel(Function.GetID(), Function.Name, Function.Plot.IsHidden);
                    FunctionPanel.ViewButtonClickHandler += HideButtonFunction;
                    FunctionPanel.EditButtonClickHandler += EditFunction; ;
                    FunctionPanel.DeleteButtonClickHandler += DeleteFunction;
                FuncionListPanel.Children.Add( FunctionPanel );
            }

            //Reload panel
            ReloadPlotPanel(null, null);
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

        
        private void DeleteFunction(object sender, FunctionPanelEventArgs e)
        {
            bool result;
            Function Function = null;
            UIFunctionPanel FunctionPanel = (UIFunctionPanel)sender;

            //Delete the plot in draw
            Function = Model.GetElementByID(e.FunctionId);
            if (null == Function)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                PlotPanel.Children.Remove(Function.Plot.PlotPoints);
            }

            //Delete funcion in model
            result = Model.DeleteElement(Function);

            //Delete panel
            this.FuncionListPanel.Children.Remove( FunctionPanel );
        }

        private void EditFunction(object sender, FunctionPanelEventArgs e)
        {
            bool result;
            Function Function = null;

            //Get Plot from model
            Function = Model.GetElementByID( e.FunctionId );
            if (null == Function)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Display the formulary
            FunctionAddAndEditForm Form = new FunctionAddAndEditForm();
            Form.Title = "Editar funcion";
            Form.A = Function.Calculator.A;
            Form.B = Function.Calculator.B;
            Form.C = Function.Calculator.C;
            Form.Color = Function.Plot.Color;
            Form.FunctionName = Function.Name;
            //TODO -- METER CALCULATOR Form.Calculator = ;

            Form.ShowDialog();
            if (false == Form.DialogResult)
                return;
            Function.Name = Form.FunctionName;
            Function.Plot.Color = Form.Color;
            Function.Calculator = Form.Calculator;

            //Update(model)
            result = Model.UpdateElement(Function);
            if (result == false)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Update(draw)
            PlotPanel.Children.Remove(Function.Plot.PlotPoints);
            PlotServices.CalculatePlot(Function, this.PlotWidth, this.PlotHeight, PlotSettings);
            PlotPanel.Children.Add(Function.Plot.PlotPoints);

            //Update(panel)
            UIFunctionPanel FunctionPanel = (UIFunctionPanel)sender;
            FunctionPanel.FunctionNameLabel.Content = Function.Name;
        }

        private void HideButtonFunction(object sender, FunctionPanelEventArgs e)
        {
           bool result;
            Function Function = null;

            //Retrieve the Plot from the model
            Function = Model.GetElementByID(e.FunctionId);
            if (null == Function)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Change Plot
            Function.Plot.IsHidden = !Function.Plot.IsHidden;

            //Update Model -- Its not necesay because the Plot isn't encapsuled but is made to mantain coherence
            result = Model.UpdateElement(Function);
            if (result == false)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //update panel
            UIFunctionPanel PanelSender = (UIFunctionPanel)sender;
            PanelSender.SwichViewButtonImage();
        }
    }
}
