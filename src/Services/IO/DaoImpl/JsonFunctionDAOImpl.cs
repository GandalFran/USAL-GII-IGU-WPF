using IGUWPF.src.controller.calculator;
using IGUWPF.src.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IGUWPF.src.controllers
{


    public class JsonFunctionDAOImpl: IDAO <Function>
    {

        public bool ImportSingleObject(string FilePath, Function toFill)
        {
            throw new NotImplementedException();
        }

        public bool ExportSingleObject(string FilePath, Function toExport)
        {
            throw new NotImplementedException();
        }

        public Boolean ImportMultipleObject(string FilePath, List<Function> ToFill)
        {
            SerializableCalculator SerializableCalculator = null;

            //Import
            try
            {
                string JsonSerializedArray = File.ReadAllText(FilePath, Encoding.UTF8);
                ToFill.AddRange(JsonConvert.DeserializeObject<List<Function>>(@JsonSerializedArray));
            }
            catch (Exception) {
                return false;
            }

            return true;
        }


        public bool ExportMultipleObject(string FilePath, List<Function> toExport)
        {

            //Parse each calculator into a serializable calculator
            foreach (Function Function in toExport)
            {
                if (null == Function.Calculator)
                    continue;
                Function.Calculator = new SerializableCalculator(Function.Calculator);
            }

            try
            {
                /*The following line has been taken from Json.net samples https://www.newtonsoft.com/json/help/html/Samples.htm*/
                string output = JsonConvert.SerializeObject(toExport, Formatting.Indented);
                File.WriteAllText(@FilePath, output);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

    }
    
    //To make easy the serialization
    public enum TypeOfCalculator { COS, SIN, TAN, XEXPN, NEXPX, NDIVEDX, X1, X2 };
    public class SerializableCalculator : ICalculator
    {
        public double[] OperationElementsArray { get; set; }
        public TypeOfCalculator Type { get; set; }

        [JsonConstructor]
        public SerializableCalculator(double[] OperationElementsArray, TypeOfCalculator Type)
        {
            this.OperationElementsArray = OperationElementsArray;
            this.Type = Type;
        }

        public SerializableCalculator(ICalculator Calculator) {
            if (Calculator is CosXCalculator)
            {
                OperationElementsArray = new double[2];
                OperationElementsArray[0] = ((CosXCalculator)Calculator).a;
                OperationElementsArray[1] = ((CosXCalculator)Calculator).b;
                Type = TypeOfCalculator.COS;
            }
            else if (Calculator is SinXCalculator)
            {
                OperationElementsArray = new double[2];
                OperationElementsArray[0] = ((SinXCalculator)Calculator).a;
                OperationElementsArray[1] = ((SinXCalculator)Calculator).b;
                Type = TypeOfCalculator.SIN;
            }
            else if (Calculator is TanXCalculator)
            {
                OperationElementsArray = new double[2];
                OperationElementsArray[0] = ((TanXCalculator)Calculator).a;
                OperationElementsArray[1] = ((TanXCalculator)Calculator).b;
                Type = TypeOfCalculator.TAN;
            }
            else if (Calculator is NDividedXCalculator)
            {
                OperationElementsArray = new double[2];
                OperationElementsArray[0] = ((NDividedXCalculator)Calculator).a;
                OperationElementsArray[1] = ((NDividedXCalculator)Calculator).b;
                Type = TypeOfCalculator.NDIVEDX;
            }
            else if (Calculator is XExpNCalculator)
            {
                OperationElementsArray = new double[2];
                OperationElementsArray[0] = ((XExpNCalculator)Calculator).a;
                OperationElementsArray[1] = ((XExpNCalculator)Calculator).b;
                Type = TypeOfCalculator.XEXPN;
            }
            else if (Calculator is NExpXCalculator)
            {
                OperationElementsArray = new double[2];
                OperationElementsArray[0] = ((NExpXCalculator)Calculator).a;
                OperationElementsArray[1] = ((NExpXCalculator)Calculator).b;
                Type = TypeOfCalculator.NEXPX;
            }
            else if (Calculator is X1Calculator)
            {
                OperationElementsArray = new double[2];
                OperationElementsArray[0] = ((X1Calculator)Calculator).a;
                OperationElementsArray[1] = ((X1Calculator)Calculator).b;
                Type = TypeOfCalculator.X1;
            }
            else if (Calculator is X2Calculator)
            {
                OperationElementsArray = new double[3];
                OperationElementsArray[0] = ((X2Calculator)Calculator).a;
                OperationElementsArray[1] = ((X2Calculator)Calculator).b;
                OperationElementsArray[2] = ((X2Calculator)Calculator).c;
                Type = TypeOfCalculator.X2;
            }
        }

        public ICalculator ToICalculator()
        {
            ICalculator Calculator = null;
            switch (Type)
            {
                case TypeOfCalculator.COS: Calculator = new CosXCalculator(OperationElementsArray[0], OperationElementsArray[1]); break;
                case TypeOfCalculator.SIN: Calculator = new SinXCalculator(OperationElementsArray[0], OperationElementsArray[1]); break;
                case TypeOfCalculator.TAN: Calculator = new TanXCalculator(OperationElementsArray[0], OperationElementsArray[1]); break;
                case TypeOfCalculator.NEXPX: Calculator = new NExpXCalculator(OperationElementsArray[0], OperationElementsArray[1]); break;
                case TypeOfCalculator.XEXPN: Calculator = new XExpNCalculator(OperationElementsArray[0], OperationElementsArray[1]); break;
                case TypeOfCalculator.NDIVEDX: Calculator = new NDividedXCalculator(OperationElementsArray[0], OperationElementsArray[1]); break;
                case TypeOfCalculator.X1: Calculator = new X1Calculator(OperationElementsArray[0], OperationElementsArray[1]); break;
                case TypeOfCalculator.X2: Calculator = new X2Calculator(OperationElementsArray[0], OperationElementsArray[1], OperationElementsArray[2]); break;
            }
            return Calculator;
        }

        public override double Calculate(double x)
        {
            return 0;
        }
    }


}
