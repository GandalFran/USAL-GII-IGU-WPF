using IGUWPF.src.models;
using IGUWPF.src.models.Model;
using IGUWPF.src.utils;
using System;
using IGUWPF.src.models.POJO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using IGUWPF.src.controller.calculator;

namespace IGUWPF.test
{
    public class DataModelTester: ITestable
    {

        private String LogHeader = "[DataModelTester] ";

        public void Test() {
            bool result;
            Function TempFunction = null;
            List<Function> TempFunctionList = null;

            IDataModelImpl<Function> FunctionModel = new IDataModelImpl<Function>();
            Console.WriteLine(LogHeader + "Instanced");


            Console.WriteLine(LogHeader + "Creating test: Create 3 functions");
            for (int i = 0; i < 3; i++)
            {
                Plot OtherPlot = new Plot(Color.FromRgb(255, 0, 0));
                int ID = FunctionModel.CreateElement(new Function("Fi", new CosXCalculator(1, 1), OtherPlot));
                if (ID != i)
                {
                    Console.WriteLine("\t" + LogHeader + "ERROR Creating test: ID" + ID);
                }
            }
            Console.WriteLine(LogHeader + "Creating test: Results: " + Utils.PrintFunctionList(FunctionModel.GetAllElements()) );
            /*
            Console.WriteLine(LogHeader + "Deleting test:");
            result = FunctionModel.DeleteElement(0);
            Console.WriteLine(LogHeader + "Deleting test: Results:" + "Deleted Funcion0 true(" + result + ") : " + Utils.PrintFunctionList(FunctionModel.GetAllElements()));
            result = FunctionModel.DeleteElement(0);
            Console.WriteLine(LogHeader + "Deleting test: Results:" + "Trying to delete Funcion0 false(" + result + ") : " + Utils.PrintFunctionList(FunctionModel.GetAllElements()));
            result = FunctionModel.DeleteElement(20);
            Console.WriteLine(LogHeader + "Deleting test: Results:" + "Trying to delete Function 20 false(" + result + ") : " + Utils.PrintFunctionList(FunctionModel.GetAllElements()));
            */
            Console.WriteLine(LogHeader + "Searching test:");
            TempFunction = FunctionModel.GetElementByID( 0 );
            Console.WriteLine(LogHeader + "Searching test: Trying to search ID 0 Results: null - " + TempFunction );
            TempFunction = FunctionModel.GetElementByID( 1 );
            Console.WriteLine(LogHeader + "Searching test: Trying to search ID 1 Results: F1 - " + TempFunction);

            Console.WriteLine(LogHeader + "Updating test:");
            result = FunctionModel.UpdateElement(TempFunction);
            Console.WriteLine(LogHeader + "Updating test: Updating ID 1 Results true(" + result + "): ");
            TempFunction.SetID(0);
            result = FunctionModel.UpdateElement(TempFunction);
            Console.WriteLine(LogHeader + "Updating test: Trying to update ID 0 Results false(" + result + "): " + Utils.PrintFunctionList(FunctionModel.GetAllElements()));

            TempFunctionList = FunctionModel.GetAllElements();
      
            Console.WriteLine(LogHeader + "Deleting all test");
            FunctionModel.Clear();
            Console.WriteLine(LogHeader + "Deleting all test: Restults: " + Utils.PrintFunctionList(FunctionModel.GetAllElements()));

            Console.WriteLine(LogHeader + "Instancing new Model since the last functions");
            FunctionModel = new IDataModelImpl<Function>( TempFunctionList );
            Plot Plot = new Plot(Color.FromRgb(255, 0, 0));
            int InternalID = FunctionModel.CreateElement(new Function("Fi",new CosXCalculator(1,1),Plot));
            Console.WriteLine(LogHeader + "Instancing new Model since the last functions: 3- New internal ID=" + (InternalID+1) + " Results: " + Utils.PrintFunctionList(FunctionModel.GetAllElements()) );
        }
    }
}
