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
    public class JsonDAO<T>: DAO <T>
    {

        public bool ExportMultipleObject(string FilePath, List<T> toExport)
        {
            try
            {
                string output = JsonConvert.SerializeObject(toExport, Formatting.Indented);
                File.WriteAllText(@FilePath, output);
            }
            catch (Exception) {
                return false;
            }

            return true;
        }

        public Boolean ImportMultipleObject(string FilePath, List<T> toFill)
        {
            try
            {
                string JsonSerializedArray = File.ReadAllText(FilePath, Encoding.UTF8);
                toFill.AddRange(JsonConvert.DeserializeObject<List<T>>(@JsonSerializedArray));
            }
            catch (Exception) {
                return false;
            }

            return true;
        }

    }
}
