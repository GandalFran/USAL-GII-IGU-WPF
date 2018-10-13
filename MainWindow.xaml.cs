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
using IGUWPF.src.controllers.ControllersImpl;
using static IGUWPF.src.utils.Enumerations;

namespace IGUWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private IController<Function> Controller;
        private PlotControllerImpl PlotController;

        public MainWindow()
        {
            InitializeComponent();
            InitialTasks();
        }

        private void InitialTasks()
        {
            Controller = new FunctionIControllerImpl();
            PlotController = new PlotControllerImpl(PlotPanel);

            PlotController.RealXMin = -10;
            PlotController.RealXMax = 10;
            PlotController.RealYMin = -10;
            PlotController.RealYMax = 10;

            AddFuncionButton.Click += AddFuncionButton_Click;
            SettingsButton.Click += SettingsButton_Click;
            SaveFileButton.Click += SaveFileButton_Click;
            OpenFileButton.Click += OpenFileButton_Click;
            ExportImageButton.Click += ExportImageButton_Click;

            this.SizeChanged += ReloadFunctionSize;
            this.PlotPanel.Loaded += PlotPanel_Loaded;
        }

        private void PlotPanel_Loaded(object sender, RoutedEventArgs e)
        {
            PlotController.AddAxys();
        }

        private void AddFuncionButton_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            string[] ExpressionArray = new string[] {"x","n*cos(x)","n*sin(x)", "x^n","x+n", "x*n"};

            string FunctionName = "Function";
            Color FunctionColor = Color.FromRgb((byte)r.Next(0,255), (byte)r.Next(0, 255), (byte)r.Next(0, 255));
            string Expression = ExpressionArray[(byte)r.Next(0, ExpressionArray.Length)].Replace("n", ""+r.Next(1,10));

            //ADD FORMULARY

            //Create and save function
            Function TempFunction = new Function(FunctionName,FunctionColor, Expression);
            int ID = Controller.AddAndGetID( TempFunction );
            //Draw plot
            PlotController.Add(TempFunction);
            //Add plot panel to the left plot list
            UIFunctionPanel FunctionPanel = new UIFunctionPanel(ID , FunctionName, false);

            FunctionPanel.ViewButtonClickHandler += FPanel_ViewButtonClickHandler;
            FunctionPanel.EditButtonClickHandler += FPanel_EditButtonClickHandler; ;
            FunctionPanel.DeleteButtonClickHandler += FPanel_DeleteButtonClickHandler; ;

            FuncionListPanel.Children.Add(FunctionPanel);
        }

        private void ReloadFunctionSize(object sender, EventArgs e)
        {
            PlotController.UpdateAxys();
            foreach (Function Plot in Controller.GetAll())
                PlotController.Update(Plot, PlotUpdateType.RECALCULATION);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            bool propertyChanged = true;

            //Add formulary

            //UpdateAxys
            if (propertyChanged)
            {
                PlotController.UpdateAxys();
                //Update plots
                foreach (Function Element in Controller.GetAll())
                {
                    PlotController.Update(Element, PlotUpdateType.RECALCULATION);
                }
            }
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
                    Utils.ThrowErrorWindow("No se pudo abrir el fichero");
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
                if (ImportResult == false)
                    Utils.ThrowErrorWindow("No se pudo guardar el fichero");
            }

            //Redraw the window
            PlotController.Clear();
            foreach (Function Element in Controller.GetAll()) {
                //Draw plot
                PlotController.Add(Element);
                //Draw the function panel in the left function list
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
                ExportResult = PlotController.ExportPlot(FilePath);
                if (ExportResult == false)
                    Utils.ThrowErrorWindow("No se pudo exportar la imagen");
            }
        }

        private void FPanel_DeleteButtonClickHandler(object sender, FunctionPanelEventArgs e)
        {
            bool result;
            Function ToDelete = null;
            UIFunctionPanel FunctionPanel = (UIFunctionPanel)sender;

            //Delete the plot(draw)
            ToDelete = Controller.GetById(FunctionPanel.FunctionID);
            if (null == ToDelete)
                Utils.ThrowErrorWindow("No se pudo eliminar la funcion");
            else
                PlotController.Delete(ToDelete);
            //Delete funcion (model)
            result = Controller.Delete( FunctionPanel.FunctionID );
            if (result == false)
                Utils.ThrowErrorWindow("No se pudo eliminar la funcion");

            //Delete panel
            this.FuncionListPanel.Children.Remove( FunctionPanel );
        }

        private void FPanel_EditButtonClickHandler(object sender, FunctionPanelEventArgs e)
        {
            bool result;
            Function TempFunction = null;
            PlotUpdateType [] Updates = new PlotUpdateType[2]{ PlotUpdateType.RECALCULATION , PlotUpdateType.COLOR };
            
            TempFunction = Controller.GetById( e.FunctionId );
            if (null == TempFunction)
                Utils.ThrowErrorWindow("No se pudo editar la funcion");

            //LANZAR FORMULARIO

            //Update(model)
            result = Controller.Update( TempFunction );
            if (result == false)
                Utils.ThrowErrorWindow("No se pudo editar la funcion");
            //Update(draw)
            result = PlotController.Update(TempFunction, PlotUpdateType.RECALCULATION);
            if (result == false)
                Utils.ThrowErrorWindow("No se pudo editar la funcion");
        }

        private void FPanel_ViewButtonClickHandler(object sender, FunctionPanelEventArgs e)
        {
            bool result;
            Function TempFunction = null;

            TempFunction = Controller.GetById(e.FunctionId);
            if (null == TempFunction)
                Utils.ThrowErrorWindow("No se pudo cambiar la visibilidad la funcion");

            TempFunction.IsHidden = !TempFunction.IsHidden;

            result = Controller.Update(TempFunction);
            if (result == false)
                Utils.ThrowErrorWindow("No se pudo cambiar la visibilidad la funcion");
            //Update(draw)
            result = PlotController.Update(TempFunction, PlotUpdateType.VISIBILITY);
            if (result == false)
                Utils.ThrowErrorWindow("No se pudo cambiar la visibilidad la funcion");
            //update panel
            UIFunctionPanel PanelSender = (UIFunctionPanel)sender;
            PanelSender.SwichViewButtonImage();
        }
    }
}
