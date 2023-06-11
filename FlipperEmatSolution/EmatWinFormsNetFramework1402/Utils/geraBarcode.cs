using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Utils
{
    public static class geraBarcode
    {
        //TODO - Feature removida - geraBarcode
        //Motivo - Nuget Package deprecated - Verificar.
        /// <summary>
        /// Feature Removida
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="settings"></param>
        public static void SalvarBarcode(int codigo, IEmatriculaSettings settings)
        {
            //BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
            //GroupBox gb = new GroupBox();
            //int W = Convert.ToInt32("300");
            //int H = Convert.ToInt32("150");
            //barcode.Alignment = BarcodeLib.AlignmentPositions.CENTER;
            //BarcodeLib.TYPE type = BarcodeLib.TYPE.Interleaved2of5;
            //try
            //{

            //    barcode.IncludeLabel = true;

            //    barcode.RotateFlipType = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), "rotatenoneflipnone", true);
                
            //    barcode.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;
                
            //    int z = codigo;
            //    string num = z.ToString("D8");

            //    gb.BackgroundImage = barcode.Encode(type, num.Trim(), Color.Black, Color.White, W, H);

            //    BarcodeLib.SaveTypes savetype = BarcodeLib.SaveTypes.PNG;

            //    string path_barcode = settings.DiretorioBarcode;

            //    barcode.SaveImage(path_barcode + @"\" + codigo + ".png", savetype);

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
    }
}