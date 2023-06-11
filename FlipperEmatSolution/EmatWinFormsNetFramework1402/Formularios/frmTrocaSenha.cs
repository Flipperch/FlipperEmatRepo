using System;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frmTrocaSenha : Form
    {
        public frmTrocaSenha()
        {
            InitializeComponent();
        }

        private void btAlterarsenha_Click(object sender, EventArgs e)
        {
            verifica_campos();
        }

        public void verifica_campos()
        {
            if (txtSenha_atual.Text != "")
            {
                if (txtNova_senha.Text != "")
                {
                    if (txtConfirma_senha.Text == txtNova_senha.Text)
                    {
                        if (Utils.csUsuarioLogado.usuario.Senha == txtSenha_atual.Text)
                        {
                            //alterar senha aqui
                            Utils.csUsuarioLogado.usuario.Senha = txtNova_senha.Text;

                            DAO.UsuarioDAO.Gravar(Utils.csUsuarioLogado.usuario);

                            MessageBox.Show("Senha alterada com sucesso!", "Senha Alterada");
                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("Favor Informar a Senha Atual!", "Erro ao Alterar Senha");
                        }
                    }
                    else MessageBox.Show("Senha de Confirmação não corresponde a nova Senha!", "Erro ao Alterar Senha");
                }
                else MessageBox.Show("Favor Informar a Nova Senha!", "Erro ao Alterar Senha");
            }
            else MessageBox.Show("Favor Informar a Senha Atual!", "Erro ao Alterar Senha");
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
