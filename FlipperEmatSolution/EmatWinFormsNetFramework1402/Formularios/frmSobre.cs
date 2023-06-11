using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frmSobre : Form
    {
        public frmSobre()
        {
            InitializeComponent();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSobre_Load(object sender, EventArgs e)
        {
            label1.Text = "E-Matricula: " + Utils.csControleVersao.versao_sistema();

            Utils.csControleVersao.verificar_atualizacao();
        }
    }
}