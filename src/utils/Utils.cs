using IGUWPF.src.models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.utils
{
    public class Utils
    {

        public static string PrintFunctionList(List<Function> list)
        {
            string s = "";
            foreach (Function f in list)
            {
                s = s + " " + f.ToString();
            }
            return s;
        }

        public static void ThrowErrorWindow(string msg)
        {
            //Lanzar ventana de error
            Console.WriteLine("Error: " + msg );

        }

        public static void CreateFileTypeAssociation() {
           /* /*Snipet taken from https://www.codeproject.com/Articles/17023/System-File-Association 

            FileAssociationInfo fai = new FileAssociationInfo("." + Constants.ProjectFileExtension);
            if (!fai.Exists)
            {
                fai.Create(Constants.ApplicationName);

                //Specify MIME type (optional)
                fai.ContentType = "application/myfile";

                //Programs automatically displayed in open with list
                fai.OpenWithList = new string[]
               { Constants.ApplicationName + ".exe", "sublime_text.exe.exe"};
            }

            ProgramAssociationInfo pai = new ProgramAssociationInfo(fai.ProgID);
            if (!pai.Exists)
            {
                pai.Create
                (
                //Description of program/file type
                "MacLab Project",

                new ProgramVerb
                     (null,
                     //Path and arguments to use
                     @"C:\SomePath\MyApp.exe %1"
                     )
                   );

                //optional
                pai.DefaultIcon = new ProgramIcon(@"Images\\ApplicationIcon.png");
            }*/

        }

    }
}
