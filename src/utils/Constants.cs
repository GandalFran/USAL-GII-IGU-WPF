using IniParser.Model;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace IGUWPF
{
    public static class Constants
    {
        public readonly static string ApplicationName = "WinLab";
        public readonly static string ProjectFileExtension = "winlab";
        public readonly static double NumberOfMsBeforePlotRecalculation = 3;

        public readonly static string LanguagePropertiesFilePath = "D:\\Language.properties";
        public readonly static string RepresentationParametersPropertiesFilePath = "D:\\RepresentationParameters.properties";
    }
}
