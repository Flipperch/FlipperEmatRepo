using System;
using System.IO;
using System.Linq;
using System.Text;
using WebCam_Capture;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace EmatWinFormsNetFramework13032.Fotos
{
    
    class WebCam
    {
        public static string numat { get; set; }

        public static string path = @"\\SERVMAT\SERVER\DADOS-MATRICULA\FOTOS\";

        private WebCamCapture webcam;
        private System.Windows.Forms.PictureBox _FrameImage;
        private int FrameNumber = 30;

        public void InitializeWebCam(ref System.Windows.Forms.PictureBox ImageControl)
        {
            webcam = new WebCamCapture();
            webcam.FrameNumber = ((ulong)(0ul));
            webcam.TimeToCapture_milliseconds = FrameNumber;
            webcam.ImageCaptured += new WebCamCapture.WebCamEventHandler(webcam_ImageCaptured);
            _FrameImage = ImageControl;
        }

        public void get_path()
        {
            //string caminho_app = AppDomain.CurrentDomain.BaseDirectory + "configuracoes.txt";
            //if (File.Exists(caminho_app))
            //{
            //    TextReader tr = new StreamReader(caminho_app, true);
            //    path = tr.ReadLine();
            //    path = @"\\servmat\SERVER\DADOS-MATRICULA\FOTOS\";
            //    tr.Close();
            //}
            
        }

        void webcam_ImageCaptured(object source, WebcamEventArgs e)
        {
            _FrameImage.Image = e.WebCamImage;
        }

        public void Start()
        {
            webcam.TimeToCapture_milliseconds = FrameNumber;
            webcam.Start(0);
        }

        public void Stop()
        {
            webcam.Stop();
        }

        public void Continue()
        {
            // change the capture time frame
            webcam.TimeToCapture_milliseconds = FrameNumber;

            // resume the video capture from the stop
            webcam.Start(this.webcam.FrameNumber);
        }

        public void ResolutionSetting()
        {
            webcam.Config();
        }

        public void AdvanceSetting()
        {
            webcam.Config2();
        }        

        public static void SaveImageCapture(System.Drawing.Image image)
        {
            string a = path;
            //a += @"\";
            a += numat;
            a += @".png";

            FileStream fstream = new FileStream(a, FileMode.Create);
            image.Save(fstream, System.Drawing.Imaging.ImageFormat.Jpeg);

            fstream.Close();

           
        }

    }
}
