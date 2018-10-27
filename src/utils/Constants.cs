namespace IGUWPF
{
    public abstract class Constants
    {
        public readonly static string ProjectFileExtension = "winlab";
        public readonly static string ApplicationName = "WinLab";

        public readonly static string ErrorWindowTitle = "ERROR";
        public readonly static string IncorrectDataMsg = "Los datos introudcidos estan incompletos o no son validos. Por favor vuelva a introducirlos.";
        public readonly static string FunctionModelErrorMsg = "Hubo un problema interno con la funcion solicitada, Por favor, reinicie la aplicacion.";
        public readonly static string IOErrorMsg = "Hubo un problema con los ficheros solicitados.";

        public readonly static double NumberOfMsBeforePlotRecalculation = 1;
    }
}
