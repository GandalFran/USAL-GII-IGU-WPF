using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace IGUWPF
{
    public class Constants
    {
        private static String IconsFolderPath = "Images\\Icons";
    
        public static BitmapImage ViewButtonIcon    = new BitmapImage(new Uri(IconsFolderPath + "\\view.png", UriKind.Relative));
        public static BitmapImage NotViewButtonIcon = new BitmapImage(new Uri(IconsFolderPath + "\\not-view.png", UriKind.Relative));
        public static BitmapImage AddButtonIcon     = new BitmapImage(new Uri(IconsFolderPath + "\\add.png", UriKind.Relative));
        public static BitmapImage EditButtonIcon    = new BitmapImage(new Uri(IconsFolderPath + "\\edit.png", UriKind.Relative));
        public static BitmapImage DeleteButtonIcon  = new BitmapImage(new Uri(IconsFolderPath + "\\delete.png", UriKind.Relative));

        public static string ProjectFileExtension = "maclab";
        public static string ApplicationName = "MacLab";
    }
}
