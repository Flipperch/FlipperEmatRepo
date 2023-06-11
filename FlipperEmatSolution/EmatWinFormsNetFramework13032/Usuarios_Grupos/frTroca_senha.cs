using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Usuarios_Grupos
{
    public partial class frTroca_senha : Form
    {
        Usuarios_Grupos.CSlogin_off alterar = new Usuarios_Grupos.CSlogin_off();

        public frTroca_senha()
        {
            InitializeComponent();
        }

        private void btAlterarsenha_Click(object sender, EventArgs e)
        {
            verifica_campos();

            if(alterar.ok == 1)
            {
                this.Close();
            }

        }

        public void verifica_campos()
        {
            if(txtSenha_atual.Text != "")
            {
                if(txtNova_senha.Text != "")
                {
                    if(txtConfirma_senha.Text == txtNova_senha.Text)
                    {
                        compara_senha_atual();                        
                    }
                    else MessageBox.Show("Senha de Confirmação não corresponde a nova Senha!", "Erro ao Alterar Senha"); 
                }
                else MessageBox.Show("Favor Informar a Nova Senha!", "Erro ao Alterar Senha");                
            }
            else MessageBox.Show("Favor Informar a Senha Atual!", "Erro ao Alterar Senha");
        }
 
        public void compara_senha_atual()
        {
            alterar.psw_confirma = txtSenha_atual.Text;
            alterar.psw_new = txtNova_senha.Text;            
            alterar.alterar_senha();
        }

        private void txtSenha_atual_TextChanged(object sender, EventArgs e)
        {
            if (txtSenha_atual.Text != "")
            {
                if (txtNova_senha.Text != "")
                {
                    if (txtConfirma_senha.Text != "")
                    {
                        btAlterarsenha.Enabled = true;
                    }
                    else btAlterarsenha.Enabled = false;
                }
                else btAlterarsenha.Enabled = false;
            }
            else btAlterarsenha.Enabled = false;
        }
    }
}
