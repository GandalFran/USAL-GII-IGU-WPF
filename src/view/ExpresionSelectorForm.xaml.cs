using IGUWPF.src.services.calculator;
using IGUWPF.src.utils;
using System.Windows;
using System.Windows.Controls;

namespace IGUWPF.src.view
{

    public partial class ExpressionSelectorUI : Window
    {
        public Calculator Calculator {

            get {
                double.TryParse(AValueTextBox.Text.Replace('.', ','), out double a);
                double.TryParse(BValueTextBox.Text.Replace('.', ','), out double b);
                double.TryParse(CValueTextBox.Text.Replace('.', ','), out double c);

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
            this.FunctionComboBox.SelectionChanged += ShowHideCValue;
        }

        private void ShowHideCValue(object sender, SelectionChangedEventArgs e)
        {
            if (FunctionComboBox.SelectedIndex == 6) //Is selected the second grade ecuation
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(AValueTextBox.Text.Replace('.', ','), out double toTest) ||
                !double.TryParse(BValueTextBox.Text.Replace('.', ','), out toTest) ||
                !double.TryParse(CValueTextBox.Text.Replace('.', ','), out toTest) ||
                FunctionComboBox.SelectedIndex == -1)
            {
                MessageBox.Show(LanguageProperties.IncorrectDataMsg, LanguageProperties.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.DialogResult = true;
            }
        }

    }
}
