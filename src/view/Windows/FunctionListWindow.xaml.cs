using IGUWPF.src.controller.calculator;
using IGUWPF.src.models;
using IGUWPF.src.models.ViewModel;
using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using IGUWPF.src.controllers;
using System.Collections.Generic;

namespace IGUWPF.src.view.Windows
{

    public partial class FunctionListWindow : Window
    {

        private FunctionViewModelImpl ViewModel;
        

        public FunctionListWindow(FunctionViewModelImpl ViewModel)
        {
            InitializeComponent();

            //Add the viewModel
            this.ViewModel = ViewModel;

            //Add the datagrid the collection
            FunctionListPanel.ItemsSource = ViewModel.GetAllElements();

            //Add handlers for the button events
            SettingsButton.Click += EditSettings;
            SaveFileButton.Click += SaveProject;
            OpenFileButton.Click += OpenProject;
            //Add handlers for the context menu
            WindowContextMenu_AddFunction.Click += AddFunction;
            DataGridContextMenu_AddFunction.Click += AddFunction;
            DataGridContextMenu_DeleteSelectedFunction.Click += DeleteFunction;
            DataGridContextMenu_EditSelectedFunction.Click += EditFunction;
            //WindowContextMenu_DeleteSelectedFunction.Click += DeleteFunction;

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

            //Add function to model
            Function Function = new Function(FunctionName, FunctionCalculator, FunctionColor, false);
            ViewModel.CreateElement(Function);
        }

        private void DeleteFunction(object sender, EventArgs e)
        {
            Function Function = (Function)FunctionListPanel.SelectedItem;
            ViewModel.DeleteElement(Function);
        }

        private void EditFunction(object sender, EventArgs e)
        {
            Function Function = null; //TODO obtener funcion

            //Display the formulary
            FunctionAddAndEditForm Form = new FunctionAddAndEditForm();
            Form.Title = "Editar funcion";
            Form.A = Function.Calculator.a;
            Form.B = Function.Calculator.b;
            Form.C = Function.Calculator.c;
            Form.Color = Function.Color;
            Form.FunctionName = Function.Name;
            Form.Calculator = Function.Calculator;

            Form.ShowDialog();
            if (false == Form.DialogResult)
                return;
            Function.Name = Form.FunctionName;
            Function.Color = Form.Color;
            Function.Calculator = Form.Calculator;

            //Update model
            ViewModel.UpdateElement(Function);
        }


        private void EditSettings(object sender, RoutedEventArgs e)
        {
            PlotRepresentationSettings Settings = ViewModel.PlotSettings;

            //Show dialog to edit de properties
            SettingsForm SettingsForm = new SettingsForm();
            //Load older values
            SettingsForm.Xmin = Settings.XMin;
            SettingsForm.Xmax = Settings.XMax;
            SettingsForm.Ymin = Settings.YMin;
            SettingsForm.Ymax = Settings.YMax;

            SettingsForm.ShowDialog();
            if (false == SettingsForm.DialogResult)
                return;

            Settings.XMin = SettingsForm.Xmin;
            Settings.XMax = SettingsForm.Xmax;
            Settings.YMin = SettingsForm.Ymin;
            Settings.YMax = SettingsForm.Ymax;

            //Save new plotsettings
            ViewModel.PlotSettings = Settings;
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
            //Its neccesary to create a new list from the model list because the calculators are replaced in the parse process
            result = IOServices.ExportModel(SaveFileForm.FileName, new List<Function>(ViewModel.GetAllElements()));
            if (result == false)
            {
                MessageBox.Show(Constants.IOErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void OpenProject(object sender, RoutedEventArgs e)
        {
            List<Function> ToImport = new List<Function>();
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
            result = IOServices.ImportModel(OpenFileForm.FileName, ToImport);
            if (result == false)
            {
                MessageBox.Show(Constants.FunctionModelErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (Function Function in ToImport)
                ViewModel.CreateElement(Function);
        }

    }
}
