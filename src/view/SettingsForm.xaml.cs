using System.Windows;

namespace IGUWPF.src.view.Windows
{
    public partial class SettingsForm : Window
    {
        public double Xmin { get => double.Parse(XMinTextBox.Text); set => XMinTextBox.Text = value + ""; }
        public double Xmax { get => double.Parse(XMaxTextBox.Text); set => XMaxTextBox.Text = value + ""; }
        public double Ymin { get => double.Parse(YMinTextBox.Text); set => YMinTextBox.Text = value + ""; }
        public double Ymax { get => double.Parse(YMaxTextBox.Text); set => YMaxTextBox.Text = value + ""; }

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
                !double.TryParse(YMaxTextBox.Text, out toTest) ||
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
