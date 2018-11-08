using IGUWPF.src.services.calculator;
using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using IGUWPF.src.services.plot;
using System.Windows.Shapes;
using IGUWPF.src.models.ViewModel;
using IGUWPF.src.bean;
using System.Windows.Controls;
using IGUWPF.src.utils;

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
            FunctionComboBox.SelectionChanged += EditFunction_ShowAndHideCValue;

            Console.WriteLine(ViewModel.XMin);

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
            SaveFileDialog SaveFileForm = new SaveFileDialog() {
                Title = "Guardar proyecto",
                FileName = "Desktop", // Default file name
                DefaultExt = ".maclab", // Default file extension
                Filter = "MacLab Project (." + Constants.ProjectFileExtension + ")|*." + Constants.ProjectFileExtension,
                AddExtension = true
            };

            bool result = (bool)SaveFileForm.ShowDialog();
            if (false == result)
                return;

            //Export the file
            result = ViewModel.ExportModel(SaveFileForm.FileName);
            if (result == false)
            {
                MessageBox.Show(LanguageProperties.IOErrorMsg, LanguageProperties.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void OpenProject(object sender, RoutedEventArgs e)
        {
            //Show dialog to choose the project to import
            OpenFileDialog OpenFileForm = new OpenFileDialog() {
                Title = "Abrir proyecto",
                FileName = "Desktop", // Default file name
                DefaultExt = ".maclab", // Default file extension
                Filter = "MacLab Project (." + Constants.ProjectFileExtension + ")|*." + Constants.ProjectFileExtension,
                Multiselect = false
            };

            bool result = (bool)OpenFileForm.ShowDialog();
            if (false == result)
                return;

            //Import the project
            result = ViewModel.ImportModel(OpenFileForm.FileName);
            if (result == false)
            {
                MessageBox.Show(LanguageProperties.IOErrorMsg, LanguageProperties.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void EditSettings(object sender, RoutedEventArgs e)
        {
            RepresentationParameters RepresentationParamters = ViewModel.RepresentationParameters;

            //Show dialog to edit de properties
            SettingsForm SettingsForm = new SettingsForm() {
                //Load older values
                Xmin = RepresentationParamters.XMin,
                Xmax = RepresentationParamters.XMax,
                Ymin = RepresentationParamters.YMin,
                Ymax = RepresentationParamters.YMax
            };

            SettingsForm.ShowDialog();
            if (false == SettingsForm.DialogResult)
                return;

            RepresentationParamters.XMin = SettingsForm.Xmin;
            RepresentationParamters.XMax = SettingsForm.Xmax;
            RepresentationParamters.YMin = SettingsForm.Ymin;
            RepresentationParamters.YMax = SettingsForm.Ymax;

            //Save new plotsettings
            ViewModel.RepresentationParameters = RepresentationParamters;
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
            if (null != Function)
            {
                MessageBoxResult Result = MessageBox.Show(LanguageProperties.DeleteConfirmationMsg, LanguageProperties.DeleteConfirmationWindowTitle,MessageBoxButton.OKCancel,MessageBoxImage.Warning);
                if(Result == MessageBoxResult.OK)
                    ViewModel.DeleteElement(Function);
            }
        }

        private void EditFunction(object sender, EventArgs e)
        {
            Function Function = (Function)FunctionDataGrid.SelectedItem;

            if (null == Function)
                return;

            //Display formulary
            ExpressionSelectorUI Form = new ExpressionSelectorUI() {
                Title = "Editar expresion",
                Calculator = Function.Calculator
            };
            Form.FunctionComboBox.ItemsSource = InitializeFunctionComboBox();

            Form.ShowDialog();
            if (false == Form.DialogResult)
                return;

            Function.Calculator = Form.Calculator;

            //Update model
            ViewModel.UpdateElement(Function);
        }

        private void EditFunction_ShowAndHideCValue(object sender, SelectionChangedEventArgs e)
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
            PointCollection[] CalculationResult = null;
            Function Function = TakeFunctionDataFromAddFunctionForm();

            if (null == Function)
                return;

            PreviewPanel.Children.Clear();

            Axys = PlotServices.GetAxys(PreviewPanel.ActualWidth, PreviewPanel.ActualHeight, ViewModel.RepresentationParameters);
            PreviewPanel.Children.Add(Axys[0]);
            PreviewPanel.Children.Add(Axys[1]);

            CalculationResult = PlotServices.CalculatePlot(Function.Calculator, PreviewPanel.ActualWidth,PreviewPanel.ActualHeight, ViewModel.RepresentationParameters);
            foreach (PointCollection Points in CalculationResult)
            {
                PreviewPanel.Children.Add( 
                    new Polyline(){
                        Stroke = new SolidColorBrush(Function.Color),
                        Points = Points
                    }
                );
            }
        }

        private Function TakeFunctionDataFromAddFunctionForm() {

            if (!double.TryParse(AValueTextBox.Text, out double toTest) ||
                !double.TryParse(BValueTextBox.Text, out toTest) ||
                !double.TryParse(CValueTextBox.Text, out toTest) ||
                FunctionNameTextBox.Text.Length == 0 ||
                ColorSelector.SelectedColor == null ||
                FunctionComboBox.SelectedIndex == -1)
            {
                MessageBox.Show(LanguageProperties.IncorrectDataMsg, LanguageProperties.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            double a, b, c;
            string FunctionName = FunctionNameTextBox.Text;
            Color FunctionColor = (Color)ColorSelector.SelectedColor;
            Calculator FunctionCalculator = null;

            a = double.Parse(AValueTextBox.Text.Replace('.',','));
            b = double.Parse(BValueTextBox.Text.Replace('.', ','));
            c = double.Parse(CValueTextBox.Text.Replace('.', ','));

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
