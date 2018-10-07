using IGUWPF.src.controllers;
using IGUWPF.src.models;
using IGUWPF.src.models.Model;
using IGUWPF.src.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IGUWPF.test
{
    public class JsonFileDaoTester : ITestable
    {
        private string DefaultTestDataSaveFile = "C:\\Users\\franp\\Desktop\\Project1.maclab";
        private string LogHeader = "[JsonFileDaoTester] ";
        public bool Test()
        {
            bool result;
            List<Function> list = new List<Function>();
            DAO<Function> JsonFileDao = new JsonDAO<Function>();

            Console.WriteLine(LogHeader + "TENER EN CUENTA QUE AQUI NO TRATAMOS IDS");
            Console.WriteLine(LogHeader + "Creating 3 functions"); 
            for (int i = 0; i < 3; i++)
            {
               list.Add(new Function("F" + i, Brushes.Red, new MathematicalExpression("h")));
            }

            Console.WriteLine(LogHeader + "Creation resuls: " + Utils.PrintFunctionList( list ) );

            Console.WriteLine(LogHeader + "Exporting functions");
            result = JsonFileDao.ExportMultipleObject( DefaultTestDataSaveFile, list );
            Console.WriteLine(LogHeader + "Exporting functions: result: " + result);

            list.RemoveAll(Element => true);

            Console.WriteLine(LogHeader + "Importing functions");
            result = JsonFileDao.ImportMultipleObject( DefaultTestDataSaveFile, list);
            Console.WriteLine(LogHeader + "Importing functions: result: " + result + " - " + Utils.PrintFunctionList(list));

            return true;
        }
    }
}
