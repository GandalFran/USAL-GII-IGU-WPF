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

namespace IGUWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int PrivateI=0;
        private IFunctionController Controller;

        public MainWindow()
        {
            InitialTasks();
            InitializeComponent();

            AddFuncionButton.Click += AddFuncionButton_Click;
            ReloadPanelFunction.Click += ReloadPanelFunction_Click;
            SettingsButton.Click += SettingsButton_Click;
            SaveFileButton.Click += SaveFileButton_Click;
            OpenFileButton.Click += OpenFileButton_Click;
            ExportImageButton.Click += ExportImageButton_Click;
            
        }

        public void InitialTasks()
        {
            Controller = new FunctionIControllerImpl();
        }

        private void AddFuncionButton_Click(object sender, RoutedEventArgs e)
        {
            string FunctionName = "Function" + PrivateI++;
            Brush FunctionColor = Brushes.Red;
            MathematicalExpression FunctionMathematicalExpression = new MathematicalExpression("h");

            //ADD FORMULARY

            Function TempFunction = new Function(FunctionName,FunctionColor, FunctionMathematicalExpression);
            int ID = Controller.AddAndGetID( TempFunction );

            UIFunctionPanel FunctionPanel = new UIFunctionPanel(ID , FunctionName, false);

            FunctionPanel.ViewButtonClickHandler += FPanel_ViewButtonClickHandler;
            FunctionPanel.EditButtonClickHandler += FPanel_EditButtonClickHandler; ;
            FunctionPanel.DeleteButtonClickHandler += FPanel_DeleteButtonClickHandler; ;

            FuncionListPanel.Children.Add(FunctionPanel);
        }

        private void ReloadPanelFunction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            bool ExportResult;

            /*Part of this snipet is taken from: https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.openfiledialog?view=netframework-4.7.2*/
            string FilePath = null;
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Save project";
            sfd.FileName = "Desktop"; // Default file name
            sfd.DefaultExt = ".maclab"; // Default file extension
            sfd.Filter = "MacLab Project (." + Constants.ProjectFileExtension + ")|*." + Constants.ProjectFileExtension;
            sfd.AddExtension = true;

            Nullable<bool> result = sfd.ShowDialog();

            if (result == true)
            {
                FilePath = sfd.FileName;
            }
            /*End of snipet*/

            if (null != FilePath)
            {
                ExportResult = Controller.ExportAll(FilePath);
                if (ExportResult == false)
                {
                    Console.WriteLine("Error Exportando");
                    //LANZAR VENTANA DE ERORR
                }
            }

        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            bool ImportResult;
            UIFunctionPanel FunctionPanel;

            /*Part of this snipet is taken from: https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.openfiledialog?view=netframework-4.7.2*/
            string FilePath = null;
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.FileName = "Open project";
            ofd.FileName = "Desktop"; // Default file name
            ofd.DefaultExt = ".maclab"; // Default file extension
            ofd.Filter = "MacLab Project (." + Constants.ProjectFileExtension + ")|*." + Constants.ProjectFileExtension;
            ofd.Multiselect = false;

            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                FilePath = ofd.FileName;
            }
            /*End of snipet*/

            if (null != FilePath) {
                ImportResult = Controller.ImportAll( FilePath );
                if (ImportResult == false) {
                    Console.WriteLine("Error Importando");
                    //LANZAR VENTANA DE ERORR
                }
            }

            //Refresh window
            FuncionListPanel.Children.Clear();
            foreach (Function Element in Controller.GetAll()) {
                FunctionPanel = new UIFunctionPanel(Element.GetID(), Element.Name, Element.IsHidden);
                FunctionPanel.ViewButtonClickHandler += FPanel_ViewButtonClickHandler;
                FunctionPanel.EditButtonClickHandler += FPanel_EditButtonClickHandler; ;
                FunctionPanel.DeleteButtonClickHandler += FPanel_DeleteButtonClickHandler;
                FuncionListPanel.Children.Add( FunctionPanel );
            }

        }

        private void ExportImageButton_Click(object sender, RoutedEventArgs e)
        {
            bool ExportResult;
            /*Part of this snipet is taken from: https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.openfiledialog?view=netframework-4.7.2*/
            string FilePath = null;
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Export plot";
            sfd.FileName = "Desktop"; // Default file name
            sfd.DefaultExt = ".png"; // Default file extension
            sfd.Filter = "PNG image (.png)|*.png";
            sfd.AddExtension = true;

            Nullable<bool> result = sfd.ShowDialog();

            if (result == true)
            {
                FilePath = sfd.FileName;
            }
            /*End of snipet*/

            if (null != FilePath)
            {
                ExportResult = Controller.ExportPlot(FilePath, PlotPanel);
                if (ExportResult == false)
                {
                    Console.WriteLine("Error Exportando");
                    //LANZAR VENTANA DE ERORR
                }
            }
        }

        private void FPanel_DeleteButtonClickHandler(object sender, FunctionPanelEventArgs e)
        {
            bool result;
            UIFunctionPanel FunctionPanel = (UIFunctionPanel)sender;

            //Delete funcion
            result = Controller.Delete( FunctionPanel.FunctionID );
            if (result == false) {
                Console.WriteLine("ERROR: no se pudo borrar");
                //LANZAR ERROR
            }

            //Delete panel
            this.FuncionListPanel.Children.Remove( FunctionPanel );
        }

        private void FPanel_EditButtonClickHandler(object sender, FunctionPanelEventArgs e)
        {
            bool result;
            Function TempFunction = null;

            TempFunction = Controller.GetById( e.FunctionId );
            if (null == TempFunction)
            {
                Console.WriteLine("ERROR: No se pudo obtener la funcion");
                //LANZAR VENTANA DE ERROR
                return;
            }

            //LANZAR FORMULARIO

            result = Controller.Update( TempFunction );
            if (result == false)
            {
                Console.WriteLine("ERROR: no se pudo actualizar");
                //LANZAR VENTANA DE ERROR
            }
        }

        private void FPanel_ViewButtonClickHandler(object sender, FunctionPanelEventArgs e)
        {
            bool result;
            Function TempFunction = null;

            TempFunction = Controller.GetById(e.FunctionId);
            if (null == TempFunction)
            {
                Console.WriteLine("ERROR: no se pudo obtener");
                //LANZAR VENTANA DE ERROR
                return;
            }

            TempFunction.IsHidden = !TempFunction.IsHidden;

            result = Controller.Update(TempFunction);
            if (result == false)
            {
                Console.WriteLine("ERROR: no se pudo actualizar");
                //LANZAR VENTANA DE ERROR
            }

            UIFunctionPanel PanelSender = (UIFunctionPanel)sender;
            PanelSender.SwichViewButtonImage();

        }
    }
}
