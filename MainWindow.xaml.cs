using IGUWPF.src.controllers;
using IGUWPF.src.models;
using IGUWPF.src.models.Model;
using IGUWPF.test;
using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();

            IController<Function> Controller = new FunctionIControllerImpl();

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

        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddFuncionButton_Click(object sender, RoutedEventArgs e)
        {
            UIFunctionPanel FPanel = new UIFunctionPanel("Function");
            FuncionListPanel.Children.Add( FPanel );
        }
    }
}
