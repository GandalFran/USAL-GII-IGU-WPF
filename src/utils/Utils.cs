using IGUWPF.src.models;
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
                s = s + " " + f.GetID();
            }
            return s;
        }
    }
}
