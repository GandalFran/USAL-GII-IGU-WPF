using IGUWPF.src.controller.calculator;
using IGUWPF.src.controllers;
using IGUWPF.src.models;
using IGUWPF.src.models.Model;
using IGUWPF.src.models.POJO;
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
        public void Test()
        {
            bool result;
            List<Function> list = new List<Function>();
            IDAO<Function> JsonFileDao = new JsonDAOImpl<Function>();

            Console.WriteLine(LogHeader + "TENER EN CUENTA QUE AQUI NO TRATAMOS IDS");
            Console.WriteLine(LogHeader + "Creating 3 functions"); 
            for (int i = 0; i < 3; i++)
            {
                Plot Plot = new Plot(Color.FromRgb(255, 0, 0));
                list.Add(new Function("Fi", new CosXCalculator(1, 1), Plot));
            }

            Console.WriteLine(LogHeader + "Creation resuls: " + Utils.PrintFunctionList( list ) );

            Console.WriteLine(LogHeader + "Exporting functions");
            result = JsonFileDao.ExportMultipleObject( DefaultTestDataSaveFile, list );
            Console.WriteLine(LogHeader + "Exporting functions: result: " + result);

            list.RemoveAll(Element => true);

            Console.WriteLine(LogHeader + "Importing functions");
            result = JsonFileDao.ImportMultipleObject( DefaultTestDataSaveFile, list);
            Console.WriteLine(LogHeader + "Importing functions: result: " + result + " - " + Utils.PrintFunctionList(list));
        }
    }
}
