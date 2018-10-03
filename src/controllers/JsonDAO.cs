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
    internal class JsonDAO: DAO <Function>
    {

        public Boolean ExportMultipleObject(string FilePath, List<Function> toExport)
        {
            try
            {
                string output = JsonConvert.SerializeObject(toExport, Formatting.Indented);
                File.WriteAllText(@FilePath, output);
            }
            catch (Exception e) {
                return false;
            }

            return true;
        }

        public Boolean ImportMultipleObject(string FilePath, List<Function> toFill)
        {
            try
            {
                string JsonSerializedFunctionArray = File.ReadAllText(FilePath, Encoding.UTF8);
                toFill.AddRange(JsonConvert.DeserializeObject<List<Function>>(@JsonSerializedFunctionArray));
            }
            catch (Exception e) {
                return false;
            }

            return true;
        }

    }
}
