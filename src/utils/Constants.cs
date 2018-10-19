using System;
using System.Windows.Media.Imaging;

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

        public static string ErrorWindowTitle = "ERROR";
        public static string IncorrectDataMsg = "Los datos no son correctos";
        public static string FunctionModelErrorMsg = "Hubo un problema interno con la funcion solicitada.";
        public static string IOErrorMsg = "Hubo un problema con los ficheros.";
    }
}
