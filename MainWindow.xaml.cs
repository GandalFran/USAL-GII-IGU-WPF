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

        IController<Function> Controller;

        public MainWindow()
        {
            Controller = new FunctionIControllerImpl();

            InitializeComponent();

            AddFuncionButton.Click += AddFuncionButton_Click;
            SaveFileButton.Click += SaveFileButton_Click;
            OpenFileButton.Click += OpenFileButton_Click;
            SettingsButton.Click += SettingsButton_Click;

        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            bool ImportResult;
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

        private void AddFuncionButton_Click(object sender, RoutedEventArgs e)
        {
            UIFunctionPanel FPanel = new UIFunctionPanel("Function");
            FuncionListPanel.Children.Add( FPanel );
        }
    }
}
