using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace IGUWPF
{
    class Constants
    {
        private static String ImagesFolderPath = "Images\\BasicIcons";
        public static String ViewButtonIconPath =    ImagesFolderPath + "\\view.png";
        public static String NotViewButtonIconPath = ImagesFolderPath + "\\no-view.png";
        public static String AddButtonIconPath =     ImagesFolderPath + "\\add.png";
        public static String EditButtonIconPath =    ImagesFolderPath + "\\edit.png";
        public static String DeleteButtonIconPath =  ImagesFolderPath + "\\delete.png";

    
        public static BitmapImage ViewButtonIcon   = new BitmapImage(new Uri(ViewButtonIconPath, UriKind.Relative));
        public static BitmapImage NotViewButton    = new BitmapImage(new Uri(NotViewButtonIconPath, UriKind.Relative));
        public static BitmapImage AddButtonIcon    = new BitmapImage(new Uri(AddButtonIconPath, UriKind.Relative));
        public static BitmapImage EditButtonIcon   = new BitmapImage(new Uri(EditButtonIconPath, UriKind.Relative));
        public static BitmapImage DeleteButtonIcon = new BitmapImage(new Uri(DeleteButtonIconPath, UriKind.Relative));



    }
}
