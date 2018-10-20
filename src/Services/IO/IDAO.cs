using System;
using System.Collections.Generic;

namespace IGUWPF.src.services.IO
{
    public interface IDAO <T>
    {
        bool ImportSingleObject(String FilePath, T toFill);
        bool ExportSingleObject(String FilePath, T toExport);
        bool ImportMultipleObject(String FilePath, List<T> toFill);
        bool ExportMultipleObject(String FilePath, List<T> toExport);
    }
}
