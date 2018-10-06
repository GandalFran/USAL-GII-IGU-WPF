using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGUWPF.src.controllers
{
    public interface IController<T>
    {
        int AddAndGetID( T Element );
        bool Delete( int ID );
        bool Update( T Element );
        T GetById( int ID );
        List<T> GetAll();

        bool ExportAll(String DataPath);
        bool ImportAll(String DataPath);
    }
}
