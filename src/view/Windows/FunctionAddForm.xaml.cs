using IGUWPF.src.controller.calculator;
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
using System.Windows.Shapes;

namespace IGUWPF.src.view
{

    public partial class FunctionAddForm : Window
    {

        public string FunctionName{
            get {
                return this.FunctionNameTextBox.Text;
            }
        }

        public Color Color {
            get {
                //If the Selected color is returned the plot isnt drawed -- change the A value doesn't works
                Color c = (Color) this.ColorSelector.SelectedColor;
                return Color.FromArgb(255,c.R,c.G,c.B);
            }
        }

        public ICalculator Calculator {
            get {
                double a, b;

                a = double.Parse(AValueTextBox.Text);
                b = double.Parse(BValueTextBox.Text);

                switch (FunctionComboBox.SelectedIndex) {
                    case 0: return new CosXCalculator(a,b);
                    case 1: return new SinXCalculator(a,b);
                    case 2: return new TanXCalculator(a,b);
                    case 3: return new XExpNCalculator(a,b);
                    case 4: return new NExpXCalculator(a,b);
                    case 5: return new X2Calculator(a,b, double.Parse(CValueTextBox.Text));
                    default: return null;
                }
            }
        }

        public FunctionAddForm()
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
            //TODO CONTROLAR ERROR
            this.DialogResult = true;
        }
    }
}
