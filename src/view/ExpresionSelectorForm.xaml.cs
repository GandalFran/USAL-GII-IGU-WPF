using IGUWPF.src.services.calculator;
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

    public partial class ExpressionSelectorUI : Window
    {

        public ICalculator Calculator {

            get {
                double a, b, c;

                double.TryParse(AValueTextBox.Text, out a);
                double.TryParse(BValueTextBox.Text, out b);
                double.TryParse(CValueTextBox.Text, out c);

                switch (FunctionComboBox.SelectedIndex) {
                    case 0: return new CosXCalculator(a,b);
                    case 1: return new SinXCalculator(a,b);
                    case 2: return new NDividedXCalculator(a, b);
                    case 3: return new XExpNCalculator(a,b);
                    case 4: return new NExpXCalculator(a,b);
                    case 5: return new X1Calculator(a,b);
                    case 6: return new X2Calculator(a,b,c);
                    default: return null;
                }
            }

            set {
                if (null == value)
                    return;

                if (value is CosXCalculator)
                {
                    FunctionComboBox.SelectedIndex = 0;
                }
                else if (value is SinXCalculator)
                {
                    FunctionComboBox.SelectedIndex = 1;
                }
                else if (value is NDividedXCalculator)
                {
                    FunctionComboBox.SelectedIndex = 2;
                }
                else if (value is XExpNCalculator)
                {
                    FunctionComboBox.SelectedIndex = 3;
                }
                else if (value is NExpXCalculator)
                {
                    FunctionComboBox.SelectedIndex = 4;
                }
                else if (value is X1Calculator)
                {
                    FunctionComboBox.SelectedIndex = 5;
                }
                else if (value is X2Calculator)
                {
                    FunctionComboBox.SelectedIndex = 6;
                }

                AValueTextBox.Text = value.a + "";
                BValueTextBox.Text = value.b + "";
                CValueTextBox.Text = value.c + "";
            }

        }

        public ExpressionSelectorUI()
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

            if (!double.TryParse(AValueTextBox.Text, out toTest) ||
                !double.TryParse(BValueTextBox.Text, out toTest) ||
                !double.TryParse(CValueTextBox.Text, out toTest) ||
                FunctionComboBox.SelectedIndex == -1)
            {
                MessageBox.Show(Constants.IncorrectDataMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
                this.DialogResult = true;
        }

    }
}
