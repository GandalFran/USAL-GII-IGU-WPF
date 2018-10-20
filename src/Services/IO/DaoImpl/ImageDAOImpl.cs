
using IGUWPF.src.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IGUWPF.src.services.IO
{
    public class ImageDAOImpl : IDAO<Panel>
    {

        public bool ImportSingleObject(string FilePath, Panel toFill)
        {
            throw new NotImplementedException();
        }

        public bool ExportSingleObject(string FilePath, Panel toExport)
        {
            try
            {
                /*Snipet taken from: https://social.msdn.microsoft.com/Forums/vstudio/en-US/7bd0c71b-8f2c-4638-adec-5ff2416bd90e/save-canvas-to-image?forum=wpf */

                BitmapEncoder encoder = new TiffBitmapEncoder();
                FileStream fs = new FileStream(FilePath, FileMode.Create);
                RenderTargetBitmap bmp = new RenderTargetBitmap((int)toExport.ActualWidth, (int)toExport.ActualHeight, 1 / 96, 1 / 96, PixelFormats.Pbgra32);

                bmp.Render(toExport);
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(fs);
                fs.Close();

                /*End of snipet*/
            }
            catch (Exception) {
                return false;
            }

            return true;
        }

        public bool ImportMultipleObject(string FilePath, List<Panel> toFill)
        {
            throw new NotImplementedException();
        }

        public bool ExportMultipleObject(string FilePath, List<Panel> toExport)
        {
            throw new NotImplementedException();
        }
    }
}
