﻿using System;
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
    class FunctionPanel : Border
    {

        public int functionId { get; set; }
        public WrapPanel PanelElement { get; set; }
        public Label FunctionNameLabel { get; set; }
        public Button ViewButton { get; set; }
        public Button EditButton { get; set; }
        public Button DeleteButton { get; set; }

        public FunctionPanel(String FunctionName) {
            PanelElement = new WrapPanel();
            FunctionNameLabel = new Label();
            ViewButton = new Button();
            EditButton = new Button();
            DeleteButton = new Button();

            this.BorderThickness = new Thickness(1);
            this.CornerRadius = new CornerRadius(2);
            this.BorderBrush = Brushes.Black;
            this.Margin = new Thickness(3);
            this.Child = PanelElement;

            PanelElement.Children.Add(FunctionNameLabel);
            PanelElement.Children.Add(ViewButton);
            PanelElement.Children.Add(EditButton);
            PanelElement.Children.Add(DeleteButton);

            FunctionNameLabel.Content = FunctionName;
            FunctionNameLabel.MinWidth = 90;

            ViewButton = getButton( Constants.ViewButtonIcon );
            EditButton = getButton( Constants.EditButtonIcon );
            DeleteButton = getButton( Constants.DeleteButtonIcon );

        }


        private Button getButton( BitmapImage image )
        {
            Image TempImage = new Image();
            Button TempButton = new Button();
            DockPanel TempPanel = new DockPanel();

            TempButton.Margin = new Thickness(3);
            TempButton.Background = Brushes.Blue;
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
