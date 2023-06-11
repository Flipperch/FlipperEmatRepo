using EmatWinFormsNetFramework1402.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frmLogin : Form
    {
        private readonly IEmatriculaSettings _settings;
        public bool mostrarcsDetalhesDisciplina = false;

        public frmLogin(IEmatriculaSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void btLogar_Click(object sender, EventArgs e)
        {
            verificar_campos();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void verificar_campos()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (txtusuario.Text != "")
            {
                if (txtsenha.Text != "")
                {
                    verificar_user_senha(txtusuario.Text, txtsenha.Text);
                }
                else
                {
                    lblMessage_erro.Text = "Favor informar a senha.";
                }
            }
            else
            {
                lblMessage_erro.Text = "Favor informar o usuário.";
            }
            Cursor.Current = Cursors.Default;

        }

        public void verificar_user_senha(string usuario, string psw)
        {
            List<Classes.Usuario> listaUsuarios = DAO.UsuarioDAO.ExibirTodos();
            bool resultado = listaUsuarios.Any(u => u.Login == usuario);
            if (resultado)
            {
                Classes.Usuario objUsuario = (Classes.Usuario)listaUsuarios.Single(u => u.Login == usuario);

                if (objUsuario.Senha == txtsenha.Text)
                    logar(objUsuario);
                else
                    lblMessage_erro.Text = "Senha Incorreta.";
            }
            else
                lblMessage_erro.Text = "Usuário não Existe.";
        }

        public void logar(Classes.Usuario objUsuario)
        {
            #region Dados de Usuário
            //Atribuir dados de usuário ao usuario_logado
            Utils.csUsuarioLogado.usuario = objUsuario;
            #endregion

            //Adcionar no tssl as informações de login
            fill_tssl(Utils.csUsuarioLogado.usuario.Nome, string.Empty);

            #region Dados de Professor
            Utils.csUsuarioLogado.professor = null;
            if (objUsuario.NivelAcesso == Enumeradores.NivelAcesso.PROFESSOR)
            {
                //Construindo o professor e atribuindo os dados do objeto usuário ao objeto professor.
                Utils.csUsuarioLogado.professor = DAO.ProfessorDAO.Consultar(objUsuario.Codigo);
                if (Utils.csUsuarioLogado.professor != null)
                {
                    Utils.csUsuarioLogado.professor.Codigo = objUsuario.Codigo;
                    Utils.csUsuarioLogado.professor.Nome = objUsuario.Nome;
                    Utils.csUsuarioLogado.professor.Rg = objUsuario.Rg;
                    Utils.csUsuarioLogado.professor.NivelAcesso = objUsuario.NivelAcesso;
                    Utils.csUsuarioLogado.professor.Senha = objUsuario.Senha;
                    Utils.csUsuarioLogado.professor.ListaDeficienciaPessoa = objUsuario.ListaDeficienciaPessoa;
                    Utils.csUsuarioLogado.professor.DtNascimento = objUsuario.DtNascimento;

                    if (Utils.csUsuarioLogado.professor.Area != null)
                    {
                        fill_tssl(Utils.csUsuarioLogado.usuario.Nome + " - " +
                            Utils.csUsuarioLogado.professor.Disciplina.Nome + " - " +
                            Utils.csUsuarioLogado.professor.Area.Nome);
                    }
                    else
                        fill_tssl(Utils.csUsuarioLogado.usuario.Nome, Utils.csUsuarioLogado.professor.Disciplina.Nome);
                }

                //Utils.Settings.Context = new Modelo.Modelo();
                var query = _settings.Context.PROFESSOR.AsNoTracking().SingleOrDefault(p => p.CODIGO == objUsuario.Codigo);
                Utils.csUsuarioLogado.modeloProfessor = query;
            }
            #endregion



            habilitarMenus();


            if (txtsenha.Text == "123456")
            {
                MessageBox.Show("Senha padrão ainda em uso. Favor alterar.", "Login");
            }

            this.Close();
        }

        private void txtusuario_Enter(object sender, EventArgs e)
        {
            lblMessage_erro.Text = "";
        }

        public void habilitarMenus()
        {
            try
            {
                List<string> listaItemsHaLiberar = new List<string>();

                if (Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.ADMINISTRADOR)
                {
                    listaItemsHaLiberar =
                        new List<string> { "tsmAlterar_senha", "tsmSecretaria", "tsmProfessores", "tsmRelatorios", "tsmFerramentas", "tsmItemConfUsuarios" };
                }
                else if (Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.SECRETARIO)
                {
                    listaItemsHaLiberar = new List<string> { "tsmAlterar_senha", "tsmSecretaria" };
                }
                else if (Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.SECRETARIOADM)
                {
                    listaItemsHaLiberar = new List<string> { "tsmAlterar_senha", "tsmSecretaria", "tsmProfessores", "tsmRelatorios", "tsmFerramentas" };
                }
                else if (Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.PROFESSOR)
                {
                    listaItemsHaLiberar = new List<string> { "tsmAlterar_senha", "tsmProfessores" };
                }
                else if (Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.COORDENADOR)
                {
                    listaItemsHaLiberar = new List<string> { "tsmAlterar_senha", "tsmSecretaria", "tsmProfessores", "tsmRelatorios", "tsmFerramentas" };
                }
                else if (Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.DIRETOR)
                {
                    listaItemsHaLiberar = new List<string> { "tsmAlterar_senha", "tsmSecretaria", "tsmProfessores", "tsmRelatorios", "tsmFerramentas" };
                }
                else
                {
                    MessageBox.Show("Nenhum nível para este usuário");
                }

                //Percorre items para liberar
                foreach (Form form in Application.OpenForms)
                {
                    if (form.Name == "frmEmatricula")
                    {
                        foreach (MenuStrip menus in form.Controls.OfType<MenuStrip>())
                        {
                            if (menus.Name == "mspPrincipal")
                            {
                                foreach (ToolStripMenuItem item in menus.Items)
                                {
                                    if (listaItemsHaLiberar.Contains(item.Name))
                                    {
                                        item.Enabled = true;
                                    }

                                    foreach (ToolStripDropDownItem subItem in item.DropDownItems)
                                    {
                                        if (listaItemsHaLiberar.Contains(subItem.Name))
                                        {
                                            subItem.Enabled = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void fill_tssl(string nome, string disciplina = "", string area = "")
        {
            //Montar String para colocar no toolstripitem
            string user_atual = "Usuário Atual: " + nome;

            if (disciplina != string.Empty)
                user_atual += " - " + disciplina;

            if (area != string.Empty)
                user_atual += " - " + area;

            //selecionar o toolstriitem
            foreach (Form forms in Application.OpenForms)
            {
                if (forms.Name == "frmEmatricula")
                {
                    foreach (StatusStrip statusstrip in forms.Controls.OfType<StatusStrip>())
                    {
                        foreach (ToolStripStatusLabel tssl in statusstrip.Items.OfType<ToolStripStatusLabel>())
                        {
                            if (tssl.Name == "tsslUsuario_Grupo")
                            {
                                tssl.Text = user_atual;
                            }

                            if (Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.ADMINISTRADOR)

                                if (tssl.Name == "tssErrors")
                                {
                                    tssl.Text = "errors_admin_log";
                                }

                        }
                    }
                }
            }
        }

        private void FRlogin_Shown(object sender, EventArgs e)
        {
            txtusuario.Focus();
        }

        private void FRlogin_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Verifica se esta configurado com servidor,
            //caso afirmativo, inicia rotina automatica para ligar catraca.

            lblMessage_erro.Text = "";

            Cursor.Current = Cursors.Default;
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (Configurações.csEscola.cidade.ToLower() == "sorocaba")
            //    if(Usuarios_Grupos.Utils.csUsuarioLogado.usuario != null)
            //        if (Usuarios_Grupos.Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.PROFESSOR)
            //        {
            //            mostrarcsDetalhesDisciplina = true;
            //        }
        }
    }
}