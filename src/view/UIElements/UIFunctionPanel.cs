using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IGUWPF

{

    public class UIFunctionPanel : Border
    {

        public int FunctionID { get; set; }
        public WrapPanel PanelElement { get; set; }
        public Label FunctionNameLabel { get; set; }
        public Button ViewButton { get; set; }
        public Button EditButton { get; set; }
        public Button DeleteButton { get; set; }

        public event RoutedEventHandler ViewButtonClick;
        public event RoutedEventHandler EditButtonClick;
        public event RoutedEventHandler DeleteButtonClick;


        public UIFunctionPanel(int FunctionID, String FunctionName) {
            this.FunctionID = FunctionID;

            PanelElement = new WrapPanel();
            FunctionNameLabel = new Label();

            this.BorderThickness = new Thickness(1);
            this.BorderBrush = Brushes.DodgerBlue;
            this.Margin = new Thickness(4,2,4,2);
            this.Child = PanelElement;

            FunctionNameLabel.Content = FunctionName;
            FunctionNameLabel.MinWidth = 90;

            ViewButton = getButton( Constants.ViewButtonIcon );
            EditButton = getButton( Constants.EditButtonIcon );
            DeleteButton = getButton( Constants.DeleteButtonIcon );

            PanelElement.Children.Add(FunctionNameLabel);
            PanelElement.Children.Add(ViewButton);
            PanelElement.Children.Add(EditButton);
            PanelElement.Children.Add(DeleteButton);

        }


        private Button getButton( BitmapImage image )
        {
            Image TempImage = new Image();
            Button TempButton = new Button();
            DockPanel TempPanel = new DockPanel();

            TempButton.Margin = new Thickness(3);
            TempButton.Background = Brushes.Transparent;
            TempButton.BorderThickness = new Thickness(0);
            TempButton.Content = TempPanel;

            TempImage.Source = image;
            TempImage.Width = 20;
            TempImage.Height = 20;

            TempPanel.Height = 20;
            TempPanel.Width = 20;
            TempPanel.Children.Add(TempImage);

            return TempButton;
        }



    }
}
