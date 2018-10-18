using IGUWPF.src.controller.calculator;
using IGUWPF.src.models;
using IGUWPF.src.models.ViewModel;
using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using IGUWPF.src.controllers;

namespace IGUWPF.src.view.Windows
{
    /// <summary>
    /// Lógica de interacción para FunctionListWindow.xaml
    /// </summary>
    public partial class FunctionListWindow : Window
    {

        private IViewModelImpl<Function> ViewModel;

        public object PlotSettings { get; private set; }

        public FunctionListWindow(IViewModelImpl<Function> ViewModel)
        {
            InitializeComponent();

            //Add the viewModel
            this.ViewModel = ViewModel;

            //Add handlers for the button events
            AddFuncionButton.Click += AddFunction;
            SettingsButton.Click += EdditSettings;
            SaveFileButton.Click += SaveProject;
            OpenFileButton.Click += OpenProject;
            ExportImageButton.Click += ExportImage;

            //Add Handlers for the model events
            ViewModel.CreateElementEvent += ViewModelCreateElementEvent;
            ViewModel.DeleteElementEvent += ViewModelDeleteElementEvent;
            ViewModel.UpdateElementEvent += ViewModelUpdateElementEvent;
            ViewModel.ClearEvent += ViewModelClearEvent;
        }

        private void AddFunction(object sender, RoutedEventArgs e)
        {
            //Display the formulary
            FunctionAddAndEditForm Form = new FunctionAddAndEditForm();
            Form.Title = "Anadir funcion";
            Form.A = Form.B = Form.C = 0;
            Form.Color = Color.FromRgb(0, 0, 0);
            Form.ShowDialog();
            if (false == Form.DialogResult)
                return;
            string FunctionName = Form.FunctionName;
            Color FunctionColor = Form.Color;
            ICalculator FunctionCalculator = Form.Calculator;


            Function Function = new Function(FunctionName, FunctionCalculator, FunctionColor, false);
            int ID = ViewModel.CreateElement(Function);
        }

        private void DeleteFunction(object sender, FunctionPanelEventArgs e)
        {
            bool result;
            Function Function = null;
            UIFunctionPanel FunctionPanel = (UIFunctionPanel)sender;

            //Retrieve Function from the model
            Function = ViewModel.GetElementByID(e.FunctionId);
            if (null == Function)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                //Delete function in panel because isnt in the model
                this.FuncionListPanel.Children.Remove(FunctionPanel);
                return;
            }

            //Delete funcion in model
            result = ViewModel.DeleteElement(Function);
        }

        private void EditFunction(object sender, FunctionPanelEventArgs e)
        {
            bool result;
            Function Function = null;
            UIFunctionPanel FunctionPanel = (UIFunctionPanel)sender;

            //Get Plot from model
            Function = ViewModel.GetElementByID(e.FunctionId);
            if (null == Function)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                //Delete function in panel because isnt in the model
                this.FuncionListPanel.Children.Remove(FunctionPanel);
                return;
            }

            //Display the formulary
            FunctionAddAndEditForm Form = new FunctionAddAndEditForm();
            Form.Title = "Editar funcion";
            Form.A = Function.Calculator.A;
            Form.B = Function.Calculator.B;
            Form.C = Function.Calculator.C;
            Form.Color = Function.Color;
            Form.FunctionName = Function.Name;
            Form.Calculator = Function.Calculator;

            Form.ShowDialog();
            if (false == Form.DialogResult)
                return;
            Function.Name = Form.FunctionName;
            Function.Color = Form.Color;
            Function.Calculator = Form.Calculator;

            //Update(model)
            result = ViewModel.UpdateElement(Function);
            if (result == false)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void HideButtonFunction(object sender, FunctionPanelEventArgs e)
        {
            bool result;
            Function Function = null;
            UIFunctionPanel FunctionPanel = (UIFunctionPanel)sender;

            //Retrieve the Plot from the model
            Function = ViewModel.GetElementByID(e.FunctionId);
            if (null == Function)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                //Delete function in panel because isnt in the model
                this.FuncionListPanel.Children.Remove(FunctionPanel);
                return;
            }

            //Change Plot
            Function.IsHidden = !Function.IsHidden;

            //Update Model
            result = ViewModel.UpdateElement(Function);
            if (result == false)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                //Delete function in panel because isnt in the model
                this.FuncionListPanel.Children.Remove(FunctionPanel);
                return;
            }

            //update panel
            UIFunctionPanel PanelSender = (UIFunctionPanel)sender;
            PanelSender.SwichViewButtonImage();
        }

        private void EdditSettings(object sender, RoutedEventArgs e)
        {
            /*    
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

            */
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
            result = IOServices.ExportModel(SaveFileForm.FileName, ViewModel);
            if (result == false)
            {
                MessageBox.Show(Constants.IOErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
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
            result = IOServices.ImportModel(OpenFileForm.FileName, ViewModel);
            if (result == false)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Add functions to the left table
            foreach (Function Function in ViewModel.GetAllElements())
            {
                FunctionPanel = new UIFunctionPanel(Function.ID, Function.Name, Function.IsHidden);
                FunctionPanel.ViewButtonClickHandler += HideButtonFunction;
                FunctionPanel.EditButtonClickHandler += EditFunction;
                FunctionPanel.DeleteButtonClickHandler += DeleteFunction;
                FuncionListPanel.Children.Add(FunctionPanel);
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
            /*
            //Export the image
            result = IOServices.ExportPlot(SaveFileForm.FileName, PlotPanel);
            if (result == false)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }*/
        }


        private void ViewModelCreateElementEvent(object sender, ViewModelEventArgs e)
        {
            Function Function = (Function)e.Element;

            UIFunctionPanel FunctionPanel = new UIFunctionPanel(Function.ID, Function.Name, Function.IsHidden);
                FunctionPanel.ViewButtonClickHandler += HideButtonFunction;
                FunctionPanel.EditButtonClickHandler += EditFunction;
                FunctionPanel.DeleteButtonClickHandler += DeleteFunction;
            FuncionListPanel.Children.Add(FunctionPanel);
        }

        private void ViewModelDeleteElementEvent(object sender, ViewModelEventArgs e)
        {
            UIFunctionPanel FunctionPanel = null;
            Function Function = (Function)e.Element;

            foreach (UIElement PanelElement in FuncionListPanel.Children) {
                FunctionPanel = (UIFunctionPanel)PanelElement;
                if (FunctionPanel.FunctionID == Function.ID)
                    return;  
            }

            if(null != FunctionPanel)
                FuncionListPanel.Children.Remove(FunctionPanel);
        }

        private void ViewModelUpdateElementEvent(object sender, ViewModelEventArgs e)
        {
            UIFunctionPanel FunctionPanel = null;
            Function Function = (Function)e.Element;

            foreach (UIElement PanelElement in FuncionListPanel.Children)
            {
                FunctionPanel = (UIFunctionPanel)PanelElement;
                if (FunctionPanel.FunctionID == Function.ID)
                    return;
            }

            if (null != FunctionPanel)
            {
                FunctionPanel.Name = Function.Name;
                FunctionPanel.FunctionID = Function.ID;
                FunctionPanel.isFunctionHiden = Function.IsHidden;
            }
        }

        private void ViewModelClearEvent(object sender, ViewModelEventArgs e)
        {
            FuncionListPanel.Children.Clear();
        }
    }
}
