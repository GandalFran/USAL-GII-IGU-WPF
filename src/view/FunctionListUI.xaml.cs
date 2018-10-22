using IGUWPF.src.services.calculator;
using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using System.Collections.Generic;
using IGUWPF.src.services.plot;
using System.Windows.Shapes;
using IGUWPF.src.models.ViewModel;
using IGUWPF.src.models.POJO;
using IGUWPF.src.services.IO;
using System.Windows.Controls;

namespace IGUWPF.src.view.Windows
{

    public partial class FunctionLitsUI : Window
    {

        private FunctionViewModelImpl ViewModel;

        public FunctionLitsUI(FunctionViewModelImpl ViewModel)
        {
            InitializeComponent();

            this.ViewModel = ViewModel;
            //Link the model to the datagrid
            FunctionDataGrid.ItemsSource = ViewModel.GetAllElementsForBinding();

            //Add function selection combobox items
            FunctionComboBox.ItemsSource = InitializeFunctionComboBox();
            FunctionComboBox.SelectionChanged += EditFunctionShowHideCValue;

            //Add handlers 
            //Handlers for button events
            SaveFileButton.Click += SaveProject;
            OpenFileButton.Click += OpenProject;
            SettingsButton.Click += EditSettings;
            //Tab 1
            DataGridContextMenu_AddFunction.Click += ChangeToAddFunctionTab;
            DataGridContextMenu_DeleteSelectedFunction.Click += DeleteFunction;
            DataGridContextMenu_EditSelectedFunctionExpression.Click += EditFunction;
            //Tab 2
            PreviewButton.Click += GeneratePreview;
            AddFunctionButton.Click += AddFunction;
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
                MessageBox.Show(Constants.IOErrorMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ViewModel.Clear();
            foreach (Function Function in ToImport)
                ViewModel.CreateElement(Function);
        }

        private void EditSettings(object sender, RoutedEventArgs e)
        {
            RepresentationParameters Settings = ViewModel.RepresentationParameters;

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
            ViewModel.RepresentationParameters = Settings;
        }

        private void ChangeToAddFunctionTab(object sender, RoutedEventArgs e)
        {
            /*Snipet taken from https://stackoverflow.com/questions/7929646/how-to-programmatically-select-a-tabitem-in-wpf-tabcontrol*/
            Dispatcher.BeginInvoke((Action)(() => MainTabControl.SelectedIndex = 1));
        }

        private void AddFunction(object sender, RoutedEventArgs e)
        {
            Function Function = TakeFunctionDataFromAddFunctionForm();
            if (null != Function)
                ViewModel.CreateElement(Function);
        }

        private void DeleteFunction(object sender, RoutedEventArgs e)
        {
            Function Function = (Function)FunctionDataGrid.SelectedItem;
            if(null != Function)
                ViewModel.DeleteElement(Function);
        }

        private void EditFunction(object sender, EventArgs e)
        {
            Function Function = (Function)FunctionDataGrid.SelectedItem;

            if (null == Function)
                return;

            //Display formulary
            ExpressionSelectorUI Form = new ExpressionSelectorUI();
            Form.Title = "Editar expresion";
            Form.FunctionComboBox.ItemsSource = InitializeFunctionComboBox();
            Form.Calculator = Function.Calculator;

            Form.ShowDialog();
            if (false == Form.DialogResult)
                return;

            Function.Calculator = Form.Calculator;

            //Update model
            ViewModel.UpdateElement(Function);
        }

        private void EditFunctionShowHideCValue(object sender, SelectionChangedEventArgs e)
        {
            if (FunctionComboBox.SelectedIndex == 6)
            {
                CValueTextBox.Visibility = Visibility.Visible;
                CLabel.Visibility = Visibility.Visible;
            }
            else
            {
                CValueTextBox.Visibility = Visibility.Hidden;
                CLabel.Visibility = Visibility.Hidden;
            }
        }

        private void GeneratePreview(object sender, RoutedEventArgs e)
        {
            Line[] Axys = null;
            Polyline Segment = null;
            PointCollection[] CalculationResult = null;
            Function Function = TakeFunctionDataFromAddFunctionForm();

            if (null == Function)
                return;

            PreviewPanel.Children.Clear();
            Axys = PlotServices.GetAxys(PreviewPanel.ActualWidth, PreviewPanel.ActualHeight, ViewModel.RepresentationParameters);
            PreviewPanel.Children.Add(Axys[0]);
            PreviewPanel.Children.Add(Axys[1]);
            CalculationResult = PlotServices.CalculatePlot(Function.Calculator, PreviewPanel.ActualWidth,PreviewPanel.ActualHeight, ViewModel.RepresentationParameters);
            foreach (PointCollection Points in CalculationResult) {
                Segment = new Polyline();
                Segment.Stroke = new SolidColorBrush(Function.Color);
                Segment.Points = Points;
                PreviewPanel.Children.Add(Segment);
            }
        }

        private Function TakeFunctionDataFromAddFunctionForm() {
            double toTest;

            if (!double.TryParse(AValueTextBox.Text, out toTest) ||
                !double.TryParse(BValueTextBox.Text, out toTest) ||
                !double.TryParse(CValueTextBox.Text, out toTest) ||
                FunctionNameTextBox.Text.Length == 0 ||
                ColorSelector.SelectedColor == null ||
                FunctionComboBox.SelectedIndex == -1)
            {
                MessageBox.Show(Constants.IncorrectDataMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            double a, b, c;
            string FunctionName = FunctionNameTextBox.Text;
            Color FunctionColor = (Color)ColorSelector.SelectedColor;
            Calculator FunctionCalculator = null;

            a = double.Parse(AValueTextBox.Text);
            b = double.Parse(BValueTextBox.Text);
            c = double.Parse(CValueTextBox.Text);

            switch (FunctionComboBox.SelectedIndex)
            {
                case 0: FunctionCalculator = new CosXCalculator(a, b); break;
                case 1: FunctionCalculator = new SinXCalculator(a, b); break;
                case 2: FunctionCalculator = new NDividedXCalculator(a, b); break;
                case 3: FunctionCalculator = new XExpNCalculator(a, b); break;
                case 4: FunctionCalculator = new NExpXCalculator(a, b); break;
                case 5: FunctionCalculator = new X1Calculator(a, b); break;
                case 6: FunctionCalculator = new X2Calculator(a, b, c); break;
            }

            return new Function(FunctionName, FunctionCalculator, FunctionColor, false);
        }

        private String[] InitializeFunctionComboBox() {
            String[] ItemArray = {
                    CosXCalculator.Operation
                    , SinXCalculator.Operation
                    , NDividedXCalculator.Operation
                    , XExpNCalculator.Operation
                    , NExpXCalculator.Operation
                    , X1Calculator.Operation
                    , X2Calculator.Operation};
            return ItemArray;
        }

    }
}
