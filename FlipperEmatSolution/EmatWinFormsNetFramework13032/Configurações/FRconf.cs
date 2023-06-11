using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;



namespace EmatWinFormsNetFramework13032.Configurações
{
    public partial class FRconf : Form
    {
        Configurações.CSconnexoes_txt conf = new Configurações.CSconnexoes_txt();

        public FRconf()
        {
            InitializeComponent();
        }

        private void btLocfotos_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCamfotos.Text = folderBrowserDialog1.SelectedPath;                
            }
        }

        private void btLocbd_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCambd.Text = openFileDialog1.FileName;              
            }

        }

        private void btAplicar_Click(object sender, EventArgs e)
        {
            if (txtCamfotos.Text != "" && txtCambd.Text != "")
            {
                conf.caminho_bd = txtCambd.Text;
                conf.caminho_fotos = txtCamfotos.Text;
                

                MessageBox.Show("Configurações Aplicadas com Sucesso", "Configurações");
            }            
        }

        private void btnAplicar_tab2_Click(object sender, EventArgs e)
        {
            if (ckbModoserv.Checked)
            {
                conf.modo = "servidor";
            }
            else
            {
                conf.modo = "normal";
            }
            conf.atualiza_configuracoes();

            MessageBox.Show("Configurações Aplicadas com Sucesso", "Configurações");
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancelar_tab2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void txtCamfotos_TextChanged(object sender, EventArgs e)
        {
            btAplicar.Enabled = true;

        }

        private void FRconf_Load(object sender, EventArgs e)
        {
            conf.get_configuracoes();
            txtCamfotos.Text = conf.caminho_fotos;

            if(conf.modo == "servidor")
            {
                ckbModoserv.Checked = true;
            }
            else
            {
                ckbModoserv.Checked = false;
            }

            //txtCambd.Text = cam.caminho_bd;
        }

        private void ckbModoserv_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbModoserv.Checked)
            {

            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            csConfiguracoes cs_configuracoes = new csConfiguracoes();

            cs_configuracoes.update_arquivo_conf(txtItem.Text, txtValor.Text);

        }

        

       
    }
}
