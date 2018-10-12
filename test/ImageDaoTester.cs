using IGUWPF.src.IO;
using IGUWPF.src.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IGUWPF.test
{
    class ImageDaoTester : ITestable
    {
        private String LogHeader = "[ImageDaoTester] ";

        public Canvas ToExport { get; set; }
        public string FilePath { get; set; }

        public void Test()
        {
            IDAO<Panel> ImageDao = new ImageDAOImpl();

            bool result = ImageDao.ExportSingleObject(FilePath, ToExport);

            Console.WriteLine(LogHeader + "Result:(true)" + result);
        }
    }
}
