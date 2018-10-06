using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.models
{
    public interface DAO <T>
    {
        bool ExportMultipleObject(String FilePath, List<T> toExport);
        bool ImportMultipleObject(String FilePath, List<T> toFill);
    }
}
