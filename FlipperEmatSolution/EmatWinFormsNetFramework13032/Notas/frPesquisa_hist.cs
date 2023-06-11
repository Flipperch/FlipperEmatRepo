using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Notas
{
    public partial class frPesquisa_hist : Form
    {
        public string ReturnValue1 {get;set;}
        public string ReturnValue2 { get; set; }
        public string zaz;
        

        public frPesquisa_hist()
        {
            InitializeComponent();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FRpesquisa_hist_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtPesquisa.Text != "")
            {
                if (rbNmat.Checked)
                {
                   
                    this.ReturnValue1 = "nmat";
                    this.ReturnValue2 = txtPesquisa.Text;
                    this.Close();
                }
                else if (rbRg.Checked)
                {
                    this.ReturnValue1 = "rg";
                    this.ReturnValue2 = txtPesquisa.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Selecione RG ou Nº de Matricula.","Pesquisar Aluno.");
                    
                }
            }
            else
            {
                MessageBox.Show("Preencha o campo para pesquisar.", "Pesquisar Aluno.");
            }          

        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            if(txtPesquisa.Text != "")
            {
                btPesquisa.Enabled = true;
            }
            else
            {
                btPesquisa.Enabled = false;
            }
        }
    }
}
