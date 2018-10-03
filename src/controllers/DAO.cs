using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.models
{
    interface DAO <T>
    {
        Boolean ExportMultipleObject(String FilePath, List<T> toExport);
        Boolean ImportMultipleObject(String FilePath, List<T> toFill);
    }
}
