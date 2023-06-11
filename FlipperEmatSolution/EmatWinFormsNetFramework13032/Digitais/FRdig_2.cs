using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NITGEN.SDK.NBioBSP;

namespace EmatWinFormsNetFramework13032.Digitais
{
    public partial class FRdig_2 : Form
    {
        NBioAPI m_NBioAPI;

        //NBioAPI.Type.HFIR hCapturedFIR;

        NBioAPI.Type.HFIR hCapturedFIR2;

        NBioAPI.Type.WINDOW_OPTION m_WinOption;

        public FRdig_2()
        {
            InitializeComponent();
        }

        private void FRdig_2_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_NBioAPI = new NBioAPI();

            m_WinOption = new NBioAPI.Type.WINDOW_OPTION();

            NBioAPI.Type.VERSION version = new NBioAPI.Type.VERSION();
            m_NBioAPI.GetVersion(out version);

            NBioAPI.Type.HFIR hCapturedFIR;

            m_NBioAPI.OpenDevice(NBioAPI.Type.DEVICE_ID.AUTO);
            m_NBioAPI.Capture(out hCapturedFIR, NBioAPI.Type.TIMEOUT.DEFAULT, m_WinOption);

            hCapturedFIR2 = hCapturedFIR;

            m_NBioAPI.CloseDevice(NBioAPI.Type.DEVICE_ID.AUTO);
        }
    }
}
