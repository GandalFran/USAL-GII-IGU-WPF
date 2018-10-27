using System.Windows;

namespace IGUWPF.src.view.Windows
{
    public partial class SettingsForm : Window
    {
        public double Xmin { get => double.Parse(XMinTextBox.Text.Replace('.', ',')); set => XMinTextBox.Text = value + ""; }
        public double Xmax { get => double.Parse(XMaxTextBox.Text.Replace('.', ',')); set => XMaxTextBox.Text = value + ""; }
        public double Ymin { get => double.Parse(YMinTextBox.Text.Replace('.', ',')); set => YMinTextBox.Text = value + ""; }
        public double Ymax { get => double.Parse(YMaxTextBox.Text.Replace('.', ',')); set => YMaxTextBox.Text = value + ""; }

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

            if (!double.TryParse(XMinTextBox.Text.Replace('.', ','), out toTest) ||
                !double.TryParse(XMaxTextBox.Text.Replace('.', ','), out toTest) ||
                !double.TryParse(YMinTextBox.Text.Replace('.', ','), out toTest) ||
                !double.TryParse(YMaxTextBox.Text.Replace('.', ','), out toTest) ||
                Xmin >= Xmax || Ymin >= Ymax)
            {
                MessageBox.Show(Constants.IncorrectDataMsg, Constants.ErrorWindowTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.DialogResult = true;
            }
        }

    }
}
