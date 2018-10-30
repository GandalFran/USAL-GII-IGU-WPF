using IniParser;
using IniParser.Model;

namespace IGUWPF.src.utils
{
    public static class LanguageProperties
    {
        private static IniData Properties;
        private static string PropertiesFilePath = Constants.LanguagePropertiesFilePath;

        static LanguageProperties() {
            FileIniDataParser Parser = new FileIniDataParser();
            Properties = Parser.ReadFile(PropertiesFilePath);
        }

        public static string ErrorWindowTitle {
            get => Properties["ERROR"]["ErrorWindowTitle"];
        }

        public static string IOErrorMsg
        {
            get => Properties["ERROR"]["IOErrorMsg"];
        }

        public static string FunctionModelErrorMsg
        {
            get => Properties["ERROR"]["FunctionModelErrorMsg"];
        }

        public static string IncorrectDataMsg
        {
            get => Properties["ERROR"]["IncorrectDataMsg"];
        }


        public static string DeleteConfirmationWindowTitle
        {
            get => Properties["CONFIRMATION"]["DeleteConfirmationWindowTitle"];
        }

        public static string DeleteConfirmationMsg
        {
            get => Properties["CONFIRMATION"]["DeleteConfirmationMsg"];
        }

    }
}
