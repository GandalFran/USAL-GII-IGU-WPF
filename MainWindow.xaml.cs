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
        
        private IDataModel<Function> Model;
        private IDAO<Function> FunctionDAO;
        private PlotRepresentationSettings PlotSettings;

        public MainWindow()
        {
            InitializeComponent();

            Model = new IDataModelImpl<Function>();
            FunctionDAO = new JsonDAOImpl<Function>();

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
        }

        private void AddFunction(object sender, RoutedEventArgs e)
        {
            //Display the formulary
            FunctionAddForm Form = new FunctionAddForm();
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
            PlotUtils.CalculatePlot(Function, this.PlotWidth, this.PlotHeight, PlotSettings);
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
            //Add axys
            Line[] Axys = PlotUtils.GetAxys(this.PlotWidth, this.PlotHeight, PlotSettings);
            PlotPanel.Children.Add( Axys[0] );
            PlotPanel.Children.Add( Axys[1] );
            //Add functions
            foreach (Function Function in Model.GetAllElements()) {
                PlotUtils.CalculatePlot(Function, this.PlotWidth, this.PlotHeight, PlotSettings);
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
                Utils.ThrowErrorWindow("No se pudo abrir el fichero");
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
                Utils.ThrowErrorWindow("No se pudo guardar el fichero");

            //Redraw the window

            //Clear plot representation and plot list
            FuncionListPanel.Children.Clear();
            PlotPanel.Children.Clear();
            
            //Add axys
            Line[] Axys = PlotUtils.GetAxys(this.PlotWidth, this.PlotHeight, PlotSettings);
            PlotPanel.Children.Add(Axys[0]);
            PlotPanel.Children.Add(Axys[1]);
            
            //Add functions
            foreach (Function Function in Model.GetAllElements()) {
                //Draw plot
                PlotUtils.CalculatePlot(Function, this.PlotWidth, this.PlotHeight, PlotSettings);
                PlotPanel.Children.Add(Function.Plot.PlotPoints);
                
                //Draw the function panel in the left function list
                FunctionPanel = new UIFunctionPanel(Function.GetID(), Function.Name, Function.Plot.IsHidden);
                    FunctionPanel.ViewButtonClickHandler += HideButtonFunction;
                    FunctionPanel.EditButtonClickHandler += EditFunction; ;
                    FunctionPanel.DeleteButtonClickHandler += DeleteFunction;
                FuncionListPanel.Children.Add( FunctionPanel );
            }

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
                Utils.ThrowErrorWindow("No se pudo exportar la imagen");
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
                Utils.ThrowErrorWindow("No se pudo eliminar la funcion");
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
                Utils.ThrowErrorWindow("No se pudo editar la funcion");
                return;
            }

            //LANZAR FORMULARIO

            //Update(model)
            result = Model.UpdateElement(Function);
            if (result == false)
            {
                Utils.ThrowErrorWindow("No se pudo editar la funcion");
            }

            //Update(draw)
            PlotPanel.Children.Remove(Function.Plot.PlotPoints);
            PlotUtils.CalculatePlot(Function, this.PlotWidth, this.PlotHeight, PlotSettings);
            PlotPanel.Children.Add(Function.Plot.PlotPoints);
        }

        private void HideButtonFunction(object sender, FunctionPanelEventArgs e)
        {
           bool result;
            Function Function = null;

            //Retrieve the Plot from the model
            Function = Model.GetElementByID(e.FunctionId);
            if (null == Function)
            {
                Utils.ThrowErrorWindow("No se pudo cambiar la visibilidad la funcion");
                return;
            }

            //Change Plot
            Function.Plot.IsHidden = !Function.Plot.IsHidden;

            //Update Model -- Its not necesay because the Plot isn't encapsuled but is made to mantain coherence
            result = Model.UpdateElement(Function);
            if (result == false)
            {
                Utils.ThrowErrorWindow("No se pudo cambiar la visibilidad la funcion");
                return;
            }

            //update panel
            UIFunctionPanel PanelSender = (UIFunctionPanel)sender;
            PanelSender.SwichViewButtonImage();
        }
    }
}
