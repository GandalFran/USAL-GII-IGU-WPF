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

    public class FunctionPanelEventArgs : EventArgs
    {
        public int FunctionId { get; set;  }

        public FunctionPanelEventArgs(int FunctionId) {
            this.FunctionId = FunctionId;
        }
    }

    public delegate void FunctionPanelButtonClick(Object sender, FunctionPanelEventArgs e);

    public class UIFunctionPanel : Border
    {
        public int FunctionID { get; set; }
        public bool isFunctionHiden { get; set; }
        public WrapPanel PanelElement { get; set; }
        public Label FunctionNameLabel { get; set; }
        public Button ViewButton { get; set; }
        public Button EditButton { get; set; }
        public Button DeleteButton { get; set; }

        public event FunctionPanelButtonClick ViewButtonClickHandler;
        public event FunctionPanelButtonClick EditButtonClickHandler;
        public event FunctionPanelButtonClick DeleteButtonClickHandler;


        public UIFunctionPanel(int FunctionID, String FunctionName, bool isFunctionHiden) {
            this.FunctionID = FunctionID;
            this.isFunctionHiden = isFunctionHiden;

            PanelElement = new WrapPanel();
            FunctionNameLabel = new Label();

            this.BorderThickness = new Thickness(1);
            this.BorderBrush = Brushes.DodgerBlue;
            this.Margin = new Thickness(4,2,4,2);
            this.Child = PanelElement;

            FunctionNameLabel.Content = FunctionName;
            FunctionNameLabel.MaxWidth = 90;

            if ( isFunctionHiden)
                ViewButton = getButton( Constants.NotViewButtonIcon);
            else
                ViewButton = getButton(Constants.ViewButtonIcon);

            EditButton = getButton( Constants.EditButtonIcon );
            DeleteButton = getButton( Constants.DeleteButtonIcon );

            ViewButton.Click += ViewButton_Click;
            EditButton.Click += EditButton_Click;
            DeleteButton.Click += DeleteButton_Click;

            PanelElement.Children.Add(FunctionNameLabel);
            PanelElement.Children.Add(ViewButton);
            PanelElement.Children.Add(EditButton);
            PanelElement.Children.Add(DeleteButton);

        }

        public void SwichViewButtonImage() {
            Image TempImage = new Image();
            DockPanel ButtonContent = (DockPanel) this.ViewButton.Content;

            if (isFunctionHiden)
            {
                isFunctionHiden = false;
                TempImage.Source = Constants.ViewButtonIcon;
            }
            else
            {
                isFunctionHiden = true;
                TempImage.Source = Constants.NotViewButtonIcon;
            }

            TempImage.Width = 20;
            TempImage.Height = 20;

            ButtonContent.Children.Clear();
            ButtonContent.Children.Add(TempImage);
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

        public void onViewButtonClick()
        {
            if (null != ViewButtonClickHandler) {
                ViewButtonClickHandler(this, new FunctionPanelEventArgs(this.FunctionID));
            }
        }

        public void onEditButtonClick()
        {
            if (null != EditButtonClickHandler)
            {
                EditButtonClickHandler(this, new FunctionPanelEventArgs(this.FunctionID));
            }
        }

        public void onDeleteButtonClick()
        {
            if (null != DeleteButtonClickHandler)
            {
                DeleteButtonClickHandler(this, new FunctionPanelEventArgs(this.FunctionID));
            }
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            onViewButtonClick();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            onEditButtonClick();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            onDeleteButtonClick();
        }


    }
}
