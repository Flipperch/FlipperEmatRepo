using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

using System.Reflection;

namespace EmatWinFormsNetFramework13032.Principal
{
    public partial class frPrincipal : Form
    {        
        Configurações.CSconnexoes_txt conf = new Configurações.CSconnexoes_txt();

        public string sql = "";
        
        public frPrincipal()
        {
            InitializeComponent();
        }
        
        private void frPrincipal_Load(object sender, EventArgs e)
        {
            //OCULTAR BOTÕES
            if (Configurações.csEscola.cidade.ToLower() == "americana")
            {
                tsmCatraca.Enabled = false;
                tsmCatraca.Visible = false;

                tsmEliminacoes.Enabled = false;
                tsmEliminacoes.Visible = false;
            }

            DateTime data = DateTime.Today;
                        
            //COR
            mspPrincipal.BackColor = Color.FromArgb(216,216, 216);
            tssErrors.Text = "";
        }

        private void frPrincipal_Shown(object sender, EventArgs e)
        {
            tsmLogin_logout.PerformClick();

            if (Usuarios_Grupos.csUsuario_logado.id_usuario_logado > 0)
            {
                tsmAlterar_senha.Enabled = true;
            }

            //Verifica se esta configurado como servidor,
            //caso afirmativo, inicia rotina automatica para ligar catraca.
            Configurações.CSconnexoes_txt conf = new Configurações.CSconnexoes_txt();
            conf.get_configuracoes();

            if (conf.modo == "servidor")
            {
                Digitais.FrmOnline.FrmOnline fr = new Digitais.FrmOnline.FrmOnline(this);
            }
        }

        public void logoff()
        {
            Usuarios_Grupos.CSlogin_off log = new Usuarios_Grupos.CSlogin_off();
            log.registra_sai_sis();
            tsslUsuario_Grupo.Text = "Não Logado";

            tsmAlterar_senha.Enabled = false;

            tsmSecretaria.Enabled = false;
            tsmProfessores.Enabled = false;
            tsmRelatorios.Enabled = false;
            tsmFerramentas.Enabled = false;


            //Limpar tsddbAreas
            tsddbAreas.DropDownItems.Clear();

            Usuarios_Grupos.csUsuario_logado.id_disciplina_logado = 0;
            Usuarios_Grupos.csUsuario_logado.id_grupo_logado = 0;
            Usuarios_Grupos.csUsuario_logado.id_usuario_logado = 0;

            tssErrors.Text = "";
        }

        private void frPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Usuarios_Grupos.csUsuario_logado.id_usuario_logado > 0)
            {
                Usuarios_Grupos.CSlogin_off log = new Usuarios_Grupos.CSlogin_off();
                log.registra_sai_sis();
            }

        }

        //Menu Strips

        #region Usuarios

        private void loginLogoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (Usuarios_Grupos.csUsuario_logado.id_usuario_logado == 0)
            {
                Usuarios_Grupos.frLogin fr1 = new Usuarios_Grupos.frLogin();
                fr1.ShowDialog();

                if (Usuarios_Grupos.csUsuario_logado.id_usuario_logado > 0)
                {
                    tsmAlterar_senha.Enabled = true;
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Deseja fazer Logoff do Sistema ? ", "Logoff", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    logoff();
                    Usuarios_Grupos.frLogin fr1 = new Usuarios_Grupos.frLogin();
                    fr1.ShowDialog();
                }
            }
            Cursor.Current = Cursors.Default;
        }



        private void tsmAlterar_senha_Click(object sender, EventArgs e)
        {
            Usuarios_Grupos.frTroca_senha fr_troca_psw = new Usuarios_Grupos.frTroca_senha();

            fr_troca_psw.ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Secretaria

        private void novaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Alunos.frCadaluno fr1 = new Alunos.frCadaluno();
            fr1.MdiParent = this;
            fr1.Show();
            Cursor.Current = Cursors.Default;
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Alunos.frPesquisaaluno form = new Alunos.frPesquisaaluno();
            form.MdiParent = this;
            //form.Owner = this;
            form.tipo_form = "edit";
            form.Show();
            Cursor.Current = Cursors.Default;
        }

        public void abrir_form_filho()
        {
            Cursor.Current = Cursors.WaitCursor;
            Alunos.frEditaluno fr1 = new Alunos.frEditaluno();
            fr1.MdiParent = this;
            fr1.Show();
            Cursor.Current = Cursors.Default;

        }

        #endregion

        #region Professores

        private void tsmiPassporte_prof_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Alunos.frPesquisaaluno form = new Alunos.frPesquisaaluno();
            form.MdiParent = this;
            //form.Owner = this;
            form.tipo_form = "passaporte";
            form.Show();
            Cursor.Current = Cursors.Default;
        }

        private void tsmiPassporte_secr_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Alunos.frPesquisaaluno form = new Alunos.frPesquisaaluno();
            form.MdiParent = this;
            //form.Owner = this;
            form.tipo_form = "passaporte";
            form.Show();
            Cursor.Current = Cursors.Default;
        }

        private void atribuirMatériaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Alunos.frPesquisaaluno form = new Alunos.frPesquisaaluno();
            form.MdiParent = this;
            //form.Owner = this;
            form.tipo_form = "atrib_mat";
            form.Show();
            Cursor.Current = Cursors.Default;
        }

        private void históricoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Alunos.frPesquisaaluno form = new Alunos.frPesquisaaluno();
            form.MdiParent = this;
            //form.Owner = this;
            form.tipo_form = "historico";
            form.Show();
            Cursor.Current = Cursors.Default;


            //Notas.frHistorico fr = new Notas.frHistorico();
            //fr.MdiParent = this;
            //fr.Show();
        }

        private void listaAtivosEInativosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Alunos.frAtivos_inativos form = new Alunos.frAtivos_inativos();
            form.MdiParent = this;
            form.Show();
            Cursor.Current = Cursors.Default;
        }

        private void tiposDeAtendimentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Notas.frConf_atendimento form = new Notas.frConf_atendimento();
            form.MdiParent = this;
            form.Show();
            Cursor.Current = Cursors.Default;
        }

        private void tsmEliminacoes_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Notas.frEliminacoes form = new Notas.frEliminacoes();
            form.MdiParent = this;
            //form.Owner = this;            
            form.Show();
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Relatórios

        private void relatóriosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Relatorios.frRelatoriosGeral form = new Relatorios.frRelatoriosGeral();
            form.MdiParent = this;
            form.Show();
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Ferramentas

        private void endereçosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Endereços.frEndereco frenderecos = new Endereços.frEndereco();
            //frenderecos.MdiParent = this;
            frenderecos.ShowDialog();
            Cursor.Current = Cursors.Default;
        }

        private void tsmImportar_Click(object sender, EventArgs e)
        {
            Alunos.frImportar fr = new Alunos.frImportar();
            fr.MdiParent = this;
            fr.Show();
        }

        private void tsmCaminhos_Click(object sender, EventArgs e)
        {
            Configurações.FRconf form = new Configurações.FRconf();
            form.ShowDialog();
        }

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Usuarios_Grupos.frTabconf_Sistema fr1 = new Usuarios_Grupos.frTabconf_Sistema();
            fr1.MdiParent = this;
            fr1.Show();
            Cursor.Current = Cursors.Default;
        }

        private void controleDeAcessoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Alunos.frControledeacesso form = new Alunos.frControledeacesso();
            form.MdiParent = this;
            //form.Owner = this;            
            form.Show();
            Cursor.Current = Cursors.Default;
        }

             

        #endregion

        #region Ajuda

        private void suporteTécnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Principal.frSuporte form_suporte = new Principal.frSuporte();
            form_suporte.ShowDialog();
        }

        private void localDaAplicaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Pasta da Aplicação: " + AppDomain.CurrentDomain.BaseDirectory + " - Deseja acessa-la ?", "Pasta da Aplicação", MessageBoxButtons.OKCancel))
            {
                Process.Start(AppDomain.CurrentDomain.BaseDirectory);
            }
        }


        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }  

        #endregion

        #region Catraca

        //Controles Catraca
        private void onLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Digitais.FrmOnline.FrmOnline fr = new Digitais.FrmOnline.FrmOnline(this);
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Digitais.Controle_Catraca.frConfCatraca fr = new Digitais.Controle_Catraca.frConfCatraca(this);
            fr.Show();

        }

        #endregion
    }
}
