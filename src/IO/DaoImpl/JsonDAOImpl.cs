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
    public class JsonDAOImpl<T>: IDAO <T>
    {

        public bool ImportSingleObject(string FilePath, T toFill)
        {
            throw new NotImplementedException();
        }

        public bool ExportSingleObject(string FilePath, T toExport)
        {
            throw new NotImplementedException();
        }

        public Boolean ImportMultipleObject(string FilePath, List<T> toFill)
        {
            try
            {
                string JsonSerializedArray = File.ReadAllText(FilePath, Encoding.UTF8);
                /*The following line has been taken from Json.net samples https://www.newtonsoft.com/json/help/html/Samples.htm*/
                toFill.AddRange(JsonConvert.DeserializeObject<List<T>>(@JsonSerializedArray));
            }
            catch (Exception) {
                return false;
            }

            return true;
        }


        public bool ExportMultipleObject(string FilePath, List<T> toExport)
        {
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
}
