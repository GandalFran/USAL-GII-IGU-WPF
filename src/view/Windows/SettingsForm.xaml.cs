using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IGUWPF.src.view.Windows
{

    public partial class SettingsForm : Window
    {

            public double Xmin { get => double.Parse(XMinTextBox.Text); set => XMinTextBox.Text = value.ToString(); }
            public double Xmax { get => double.Parse(XMaxTextBox.Text); set => XMaxTextBox.Text = value.ToString(); }
            public double Ymin { get => double.Parse(YMinTextBox.Text); set => YMinTextBox.Text = value.ToString(); }
            public double Ymax { get => double.Parse(YMaxTextBox.Text); set => YMaxTextBox.Text = value.ToString(); }


        public SettingsForm()
        {
            InitializeComponent();

            this.SaveButton.Click += SaveButton_Click;
            this.CancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            double toTest;

            if (!double.TryParse(XMinTextBox.Text, out toTest) ||
                !double.TryParse(XMaxTextBox.Text, out toTest) ||
                !double.TryParse(YMinTextBox.Text, out toTest) ||
                !double.TryParse(YMaxTextBox.Text, out toTest)) {
                //TODO Lanzar error

                return;
            }

            this.DialogResult = true;
        }
    }
}
