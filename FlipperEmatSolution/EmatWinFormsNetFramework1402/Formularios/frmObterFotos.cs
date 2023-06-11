using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

// Reference path for the following assemblies --> C:\Program Files\Microsoft Expression\Encoder 4\SDK\
//using Microsoft.Expression.Encoder.Devices;
//using Microsoft.Expression.Encoder.Live;
//using Microsoft.Expression.Encoder;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.Formularios
{
    //TODO:: LIMPAR UI DE OBTER FOTOS E VERIFICAR MELHORIAS COMENTADAS
    //Verificar código comentado e limpar classe do form Obter Foto

    //TODO:: Melhorar auto inicialização quando á uma fonte de vídeo e 
    //Implementar padronização de fonte de vídeo, salvando em arquivo de texto na máquina
    //TODO=> SÉRIO... TÁ MUITO FEIO ESSE CÓDIGO =(
    public partial class frmObterFotos : Form
    {
        private readonly IEmatriculaSettings _settings;
        public frmObterFotos(IEmatriculaSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        public int cad_edit;

        public string numat = "";

        #region - Antigo

        //TODO:: Verificar código a baixo
        //private void FRobterfoto_Load(object sender, EventArgs e)
        //{
        //    //webcam = new WebCam();

        //    //webcam.get_path();

        //    //webcam.InitializeWebCam(ref imgVideo);

        //    //webcam.Start();           
        //}

        private void bntStart_Click(object sender, EventArgs e)
        {
            //webcam.Start();
        }

        private void bntStop_Click(object sender, EventArgs e)
        {
            //webcam.Stop();
        }

        private void bntContinue_Click(object sender, EventArgs e)
        {
            //webcam.Continue();
        }

        private void bntCapture_Click(object sender, EventArgs e)
        {
            //imgCapture.Image = imgVideo.Image;
        }

        private void bntVideoFormat_Click(object sender, EventArgs e)
        {
            //webcam.ResolutionSetting();
        }

        private void bntVideoSource_Click(object sender, EventArgs e)
        {
            //webcam.AdvanceSetting();
        }

        #endregion

        #region- Novo

        public string nome_form_solicitante;

        //private LiveJob _job;

        //private LiveDeviceSource _deviceSource;

        //private bool _bStartedRecording = false;

        private void FRobterfoto_Load(object sender, EventArgs e)
        {
            //this.Text += " - ver. " + Application.ProductVersion;

            ////lstVideoDevices.ClearSelected();
            //foreach (EncoderDevice edv in EncoderDevices.FindDevices(EncoderDeviceType.Video))
            //{
            //    cmblstVideoDevices.Items.Add(edv.Name);
            //}
            ////Selecionar primeira opção
            //if (cmblstVideoDevices.Items.Count > 0)
            //{
            //    //cmblstVideoDevices.SelectedItem = 0;
            //    //cmblstVideoDevices.SelectedItem = cs_conf.conf_video_webcam;
            //    //cmblstAudioDevices.SelectedItem = cs_conf.conf_audio_webcam;
            //    if (cmblstVideoDevices.Items.Count == 1)
            //    {
            //        cmblstVideoDevices.SelectedIndex = 0;
            //        ckbPadronizar.CheckedChanged -= ckbPadronizar_CheckedChanged;
            //        ckbPadronizar.Checked = true;
            //        ckbPadronizar.CheckedChanged += ckbPadronizar_CheckedChanged;
            //    }
            //    else
            //    {
            //        btIniciar.Enabled = true;
            //    }

            //    //btIniciar.Enabled = false;
            //}

            ////lblVideoDeviceSelectedForPreview.Text = "";

            ////lstAudioDevices.ClearSelected();
            ////foreach (EncoderDevice eda in EncoderDevices.FindDevices(EncoderDeviceType.Audio))
            ////{
            ////    cmblstAudioDevices.Items.Add(eda.Name);
            ////}
            ////lblAudioDeviceSelectedForPreview.Text = "";   
            ////TODO: AGORA - CAMERA 

            ////if(csConfiguracoes.conf_video_webcam != string.Empty)
            ////{
            ////    
            ////}
        }



        //private void iniciar()
        //{
        //    if (cmblstVideoDevices.SelectedIndex > -1)
        //    {
        //        EncoderDevice video = null;
        //        EncoderDevice audio = null;

        //        GetSelectedVideoAndAudioDevices(out video);

        //        StopJob();

        //        if (video == null)
        //        {
        //            return;
        //        }

        //        // Starts new job for preview window
        //        try
        //        {
        //            _job = new LiveJob();

        //            // Checks for a/v devices
        //            if (video != null)
        //            {
        //                // Create a new device source. We use the first audio and video devices on the system
        //                _deviceSource = _job.AddDeviceSource(video, audio);

        //                // Is it required to show the configuration dialogs ?
        //                //if (checkBoxShowConfigDialog.Checked)
        //                //{
        //                //    // Yes
        //                //    // VFW video device ?
        //                //    if (lstVideoDevices.SelectedItem.ToString().EndsWith("(VFW)", StringComparison.OrdinalIgnoreCase))
        //                //    {
        //                //        // Yes
        //                //        if (_deviceSource.IsDialogSupported(ConfigurationDialog.VfwFormatDialog))
        //                //        {
        //                //            _deviceSource.ShowConfigurationDialog(ConfigurationDialog.VfwFormatDialog, (new HandleRef(imgVideo, imgVideo.Handle)));
        //                //        }

        //                //        if (_deviceSource.IsDialogSupported(ConfigurationDialog.VfwSourceDialog))
        //                //        {
        //                //            _deviceSource.ShowConfigurationDialog(ConfigurationDialog.VfwSourceDialog, (new HandleRef(imgVideo, imgVideo.Handle)));
        //                //        }

        //                //        if (_deviceSource.IsDialogSupported(ConfigurationDialog.VfwDisplayDialog))
        //                //        {
        //                //            _deviceSource.ShowConfigurationDialog(ConfigurationDialog.VfwDisplayDialog, (new HandleRef(imgVideo, imgVideo.Handle)));
        //                //        }

        //                //    }
        //                //    else
        //                //    {
        //                //        // No
        //                //        if (_deviceSource.IsDialogSupported(ConfigurationDialog.VideoCapturePinDialog))
        //                //        {
        //                //            _deviceSource.ShowConfigurationDialog(ConfigurationDialog.VideoCapturePinDialog, (new HandleRef(imgVideo, imgVideo.Handle)));
        //                //        }                            

        //                //        if (_deviceSource.IsDialogSupported(ConfigurationDialog.VideoCaptureDialog))
        //                //        {
        //                //            _deviceSource.ShowConfigurationDialog(ConfigurationDialog.VideoCaptureDialog, (new HandleRef(imgVideo, imgVideo.Handle)));
        //                //        }

        //                //        if (_deviceSource.IsDialogSupported(ConfigurationDialog.VideoCrossbarDialog))
        //                //        {
        //                //            _deviceSource.ShowConfigurationDialog(ConfigurationDialog.VideoCrossbarDialog, (new HandleRef(imgVideo, imgVideo.Handle)));
        //                //        }

        //                //        if (_deviceSource.IsDialogSupported(ConfigurationDialog.VideoPreviewPinDialog))
        //                //        {
        //                //            _deviceSource.ShowConfigurationDialog(ConfigurationDialog.VideoPreviewPinDialog, (new HandleRef(imgVideo, imgVideo.Handle)));
        //                //        }

        //                //        if (_deviceSource.IsDialogSupported(ConfigurationDialog.VideoSecondCrossbarDialog))
        //                //        {
        //                //            _deviceSource.ShowConfigurationDialog(ConfigurationDialog.VideoSecondCrossbarDialog, (new HandleRef(imgVideo, imgVideo.Handle)));
        //                //        }
        //                //    }
        //                //}
        //                //else
        //                //{
        //                // No
        //                // Setup the video resolution and frame rate of the video device
        //                // NOTE: Of course, the resolution and frame rate you specify must be supported by the device!
        //                // NOTE2: May be not all video devices support this call, and so it just doesn't work, as if you don't call it (no error is raised)
        //                // NOTE3: As a workaround, if the .PickBestVideoFormat method doesn't work, you could force the resolution in the 
        //                //        following instructions (called few lines belows): 'imgVideo.Size=' and '_job.OutputFormat.VideoProfile.Size=' 
        //                //        to be the one you choosed (640, 480).
        //                _deviceSource.PickBestVideoFormat(new Size(320, 240), 15);
        //                //}

        //                // Get the properties of the device video
        //                SourceProperties sp = _deviceSource.SourcePropertiesSnapshot();

        //                // Resize the preview panel to match the video device resolution set
        //                //imgVideo.Size = new Size(sp.Size.Width, sp.Size.Height);
        //                imgVideo.Size = new Size(320, 240);

        //                // Setup the output video resolution file as the preview
        //                //_job.OutputFormat.VideoProfile.Size = new Size(sp.Size.Width, sp.Size.Height);
        //                _job.OutputFormat.VideoProfile.Size = new Size(320, 240);

        //                // Display the video device properties set
        //                toolStripStatusLabel1.Text = sp.Size.Width.ToString() + "x" + sp.Size.Height.ToString() + "  " + sp.FrameRate.ToString() + " fps";

        //                // Sets preview window to winform panel hosted by xaml window
        //                _deviceSource.PreviewWindow = new PreviewWindow(new HandleRef(imgVideo, imgVideo.Handle));

        //                // Make this source the active one
        //                _job.ActivateSource(_deviceSource);

        //                //btnStartStopRecording.Enabled = true;
        //                btCapture.Enabled = true;

        //                toolStripStatusLabel1.Text = "Preview activated";
        //            }
        //            else
        //            {
        //                // Gives error message as no audio and/or video devices found
        //                MessageBox.Show("No Video/Audio capture devices have been found.", "Warning");
        //                toolStripStatusLabel1.Text = "No Video/Audio capture devices have been found.";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ErrorLog.csControle_erros.ExibirMsgBoxError(ex.Message);
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Selecionar uma fonte de Video.", "Captura Webcam");
        //    }
        //}

        private void btnStartStopRecording_Click(object sender, EventArgs e)
        {
            //// Is it Recoring ?
            //if (_bStartedRecording)
            //{
            //    // Yes
            //    // Stops encoding
            //    _job.StopEncoding();
            //    //btnStartStopRecording.Text = "Start Recording";
            //    toolStripStatusLabel1.Text = "";
            //    _bStartedRecording = false;
            //}
            //else
            //{
            //    // Sets up publishing format for file archival type
            //    FileArchivePublishFormat fileOut = new FileArchivePublishFormat();

            //    // Sets file path and name
            //    fileOut.OutputFileName = String.Format("C:\\WebCam{0:yyyyMMdd_hhmmss}.wmv", DateTime.Now);

            //    // Adds the format to the job. You can add additional formats as well such as
            //    // Publishing streams or broadcasting from a port
            //    _job.PublishFormats.Add(fileOut);

            //    // Start encoding
            //    _job.StartEncoding();

            //    //btnStartStopRecording.Text = "Stop Recording";
            //    toolStripStatusLabel1.Text = fileOut.OutputFileName;
            //    _bStartedRecording = true;
            //}
        }

        private void cmdGrabImage_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    // Create a Bitmap of the same dimension of panelVideoPreview (Width x Height)
            //    using (Bitmap bitmap = new Bitmap(imgVideo.Width, imgVideo.Height))
            //    {
            //        using (Graphics g = Graphics.FromImage(bitmap))
            //        {
            //            // Get the paramters to call g.CopyFromScreen and get the image
            //            Rectangle rectanglePanelVideoPreview = imgVideo.Bounds;
            //            Point sourcePoints = imgVideo.PointToScreen(new Point(imgVideo.ClientRectangle.X, imgVideo.ClientRectangle.Y));
            //            g.CopyFromScreen(sourcePoints, Point.Empty, rectanglePanelVideoPreview.Size);
            //        }

            //        if (!File.Exists(@"C:\E-mat\"))
            //        {
            //            Directory.CreateDirectory(@"C:\E-mat\");

            //            string strGrabFileName = String.Format(@"C:\E-mat\SnapShot.png");
            //            toolStripStatusLabel1.Text = strGrabFileName;

            //            bitmap.Save(strGrabFileName, System.Drawing.Imaging.ImageFormat.Png);

            //            FileStream stream = new FileStream(@"C:\E-mat\SnapShot.png", FileMode.Open, FileAccess.Read);
            //            imgCapture.Image = Image.FromStream(stream);
            //            stream.Close();
            //        }
            //        else
            //        {
            //            string strGrabFileName = String.Format(@"C:\E-mat\SnapShot.png");
            //            toolStripStatusLabel1.Text = strGrabFileName;

            //            bitmap.Save(strGrabFileName, System.Drawing.Imaging.ImageFormat.Png);

            //            FileStream stream = new FileStream(@"C:\E-mat\SnapShot.png", FileMode.Open, FileAccess.Read);
            //            imgCapture.Image = Image.FromStream(stream);
            //            stream.Close();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.WaitCursor;
            //try
            //{
            //    if (imgCapture.Image != null)
            //    {
            //        btCancelar.Enabled = false;

            //        string a = _settings.DiretorioFotos;
            //        a += @"\";
            //        a += numat;
            //        a += @".png";

            //        string sourceFile = @"C:\E-mat\SnapShot.png";
            //        string destinationFile = a;

            //        if (System.IO.File.Exists(a))
            //        {
            //            // Use a try block to catch IOExceptions, to
            //            // handle the case of the file already being
            //            // opened by another process.
            //            try
            //            {
            //                File.Delete(a);
            //            }
            //            catch (Exception)
            //            {
            //                return;
            //            }
            //        }

            //        // To move a file or folder to a new location:
            //        System.IO.File.Move(sourceFile, destinationFile);

            //        MessageBox.Show("Foto Salva com Sucesso!");

            //        StopJob();

            //    }
            //    else
            //    {
            //        MessageBox.Show("Favor fotografar antes de Salvar.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    StopJob();
            //    ErrorLog.csControle_erros.ExibirMsgBoxError("Falha ao tentar salvar a foto. Favor verifique as configurações. " + ex.Message);
            //}
            //Close();
            //Cursor.Current = Cursors.Default;
        }

        private void Broadcast_Click(object sender, EventArgs e)
        {
            //EncoderDevice video = null;
            //EncoderDevice audio = null;

            //GetSelectedVideoAndAudioDevices(out video);
            //StopJob();

            //if (video == null)
            //{
            //    return;
            //}

            //_job = new LiveJob();

            //_deviceSource = _job.AddDeviceSource(video, audio);
            //_job.ActivateSource(_deviceSource);

            //// Finds and applys a smooth streaming preset        
            //_job.ApplyPreset(LivePresets.VC1256kDSL16x9);

            //// Creates the publishing format for the job
            //PullBroadcastPublishFormat format = new PullBroadcastPublishFormat();
            //format.BroadcastPort = 8080;
            //format.MaximumNumberOfConnections = 2;

            //// Adds the publishing format to the job
            //_job.PublishFormats.Add(format);

            //// Starts encoding
            //_job.StartEncoding();

            //toolStripStatusLabel1.Text = "Broadcast started on localhost at port 8080, run WpfShowBroadcast.exe now to see it";
        }

        //private void GetSelectedVideoAndAudioDevices(out EncoderDevice video)
        //{
        //    video = null;


        //    //lblVideoDeviceSelectedForPreview.Text = "";
        //    //lblAudioDeviceSelectedForPreview.Text = "";

        //    if (cmblstVideoDevices.Items.Count < 0)
        //    {
        //        MessageBox.Show("No Video and Audio capture devices have been selected.\nSelect an audio and video devices from the listboxes and try again.", "Warning");
        //        return;
        //    }

        //    // Get the selected video device            
        //    foreach (EncoderDevice edv in EncoderDevices.FindDevices(EncoderDeviceType.Video))
        //    {
        //        if (String.Compare(edv.Name, cmblstVideoDevices.SelectedItem.ToString()) == 0)
        //        {
        //            video = edv;
        //            //cmblstVideoDevices[1] = edv.Name;
        //            break;
        //        }
        //    }

        //    // Get the selected audio device      

        //    //foreach (EncoderDevice eda in EncoderDevices.FindDevices(EncoderDeviceType.Audio))
        //    //{
        //    //    if (String.Compare(eda.Name, cmblstAudioDevices.SelectedItem.ToString()) == 0)
        //    //    {
        //    //        audio = eda;
        //    //        //cmblstAudioDevices[1] = eda.Name;
        //    //        break;
        //    //    }
        //    //}
        //}

        //void StopJob()
        //{
        //    // Has the Job already been created ?
        //    if (_job != null)
        //    {
        //        // Yes
        //        // Is it capturing ?
        //        //if (_job.IsCapturing)
        //        if (_bStartedRecording)
        //        {
        //            // Yes
        //            // Stop Capturing
        //            //btnStartStopRecording.PerformClick();
        //        }

        //        _job.StopEncoding();

        //        // Remove the Device Source and destroy the job
        //        _job.RemoveDeviceSource(_deviceSource);

        //        // Destroy the device source
        //        _deviceSource.PreviewWindow = null;
        //        _deviceSource = null;
        //    }
        //}

        private void btCancelar_Click(object sender, EventArgs e)
        {
            //StopJob();
            //Close();
        }

        private void btIniciar_Click(object sender, EventArgs e)
        {
            //iniciar();
        }

        private void ckbPadronizar_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckbPadronizar.Checked)
            //{
            //    //TODO: AGORA - IMPLEMENTAR WEBCAM OPÇÕES
            //    //if (cmblstVideoDevices.SelectedIndex > -1)
            //    //{
            //    //    
            //    //    cs_conf.conf_video_webcam = cmblstVideoDevices.SelectedItem.ToString();
            //    //    //cs_conf.conf_audio_webcam = cmblstAudioDevices.SelectedItem.ToString();
            //    //    cs_conf.atualiza_configuracoes();
            //    //}
            //}
        }

        #endregion

        private void frmObterFotos_Shown(object sender, EventArgs e)
        {
            //iniciar();
        }
    }
}