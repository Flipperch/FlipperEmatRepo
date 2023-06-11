using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.Formularios
{
    //TODO:: Verificar a necessidade de definir propriedade form.MdiParent = this;
    public partial class frmEmatricula : Form
    {
        private readonly IEmatriculaSettings _settings;
        private frmSplash splash;
        private bool done;

        public frmEmatricula()
        {
            InitializeComponent();

            _settings = new EmatriculaConfigurationSettings();

            if (_settings.IsComplete)
            {
                //TODO:: Arrumar Splash
                //this.splash = new frmSplash();

                //TODO:: Verificar uma forma mais eficiente de realizar estas modificações

                //Menu Atividade Extra
                atribuirAtividadeExtraToolStripMenuItem.Enabled = _settings.UIHabilitaMenuImpressaoRaLote;
                atribuirAtividadeExtraToolStripMenuItem.Visible = _settings.UIHabilitaMenuImpressaoRaLote;
                //Menu Impressões Ra Lote
                impressõesToolStripMenuItem.Enabled = _settings.UIHabilitaMenuAtividadeExtra;
                impressõesToolStripMenuItem.Visible = _settings.UIHabilitaMenuAtividadeExtra;

                //TODO:: Configurações a seguir devem sair daqui ?
                //Ver quanto tempo demora, mas colocar no carregamento
                EnsinoSistemaEstatica.getListaEnsinosSistema = new List<Classes.EnsinoSistema>();
                //Adiciona o Ensino Fundamental
                EnsinoSistemaEstatica.getListaEnsinosSistema.Add(Classes.EnsinoSistema.ensinoFundamental());
                //Adiciona o Ensino Médio
                EnsinoSistemaEstatica.getListaEnsinosSistema.Add(Classes.EnsinoSistema.ensinoMedio());

                //COR
                mspPrincipal.BackColor = Color.FromArgb(216, 216, 216);
                tssErrors.Text = "";
            }
            else
            {
                //ChamarTeladeConfiguração
                MessageBox.Show("O Ematrícula não está configurado corretamente. Revise as configurações.", "Erro de inicialização", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                frmConfiguracoes frmConfiguracoes = new frmConfiguracoes();
                if (frmConfiguracoes.ShowDialog() == DialogResult.OK)
                {
                    _settings = new EmatriculaConfigurationSettings();
                }
            }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void frmPrincipal_Shown(object sender, EventArgs e)
        {
            if (_settings.IsComplete)
            {
                tsmLogin_logout.PerformClick();

                if (csUsuarioLogado.usuario != null)
                {
                    if (csUsuarioLogado.usuario.Codigo > 0)
                    {
                        tsmAlterar_senha.Enabled = true;
                    }
                }
            }
            else
            {
                //Aviso de configuração incompleta
                MessageBox.Show("O Ematrícula não está configurado corretamente. Revise as configurações.", "Erro de inicialização", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                frmConfiguracoes frmConfiguracoes = new frmConfiguracoes();
                frmConfiguracoes.ShowDialog();
            }
        }

        private void showSplash()
        {
            splash.Show();
            while (!done)
            {
                Application.DoEvents();
            }
            splash.Close();
            this.splash.Dispose();
        }

        public void logoff()
        {

            //TODO:: melhorar esta etapa de logof de moto a tornar mais dinamico e talvez sercontrolado por outra classe (Login)

            //Usuarios_Grupos.CSlogin_off log = new Usuarios_Grupos.CSlogin_off();
            //log.registra_sai_sis();
            tsslUsuario_Grupo.Text = "Não Logado";

            tsmAlterar_senha.Enabled = false;

            tsmSecretaria.Enabled = false;
            tsmProfessores.Enabled = false;
            tsmRelatorios.Enabled = false;
            tsmFerramentas.Enabled = false;
            tsmItemConfUsuarios.Enabled = false;


            csUsuarioLogado.usuario = null;
            csUsuarioLogado.professor = null;
            csUsuarioLogado.modeloProfessor = null;

            tssErrors.Text = "";
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (csUsuarioLogado.usuario != null)
                if (csUsuarioLogado.usuario.Codigo > 0)
                {
                    //Usuarios_Grupos.CSlogin_off log = new Usuarios_Grupos.CSlogin_off();
                    //log.registra_sai_sis(); 
                    //TODO: FUTURO 2 - verificar se irá manter
                }
        }


        #region Usuarios

        private void loginLogoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (csUsuarioLogado.usuario == null)
                {
                    frmLogin fr1 = new frmLogin(_settings);
                    fr1.ShowDialog();

                    if (csUsuarioLogado.usuario != null)
                        if (csUsuarioLogado.usuario.Codigo > 0)
                        {
                            tsmAlterar_senha.Enabled = true;
                        }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Deseja fazer Logoff do Sistema ? ", "E-matrícula: Logoff", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        foreach (Form fr in Application.OpenForms)
                        {
                            if (fr.Name != "frmEmatricula")
                                if (fr.Name != "frmSplash")
                                    MessageBox.Show("É necessario fechar as janelas abertas antes de sair.", "E-matrícula: Logoff");



                        }

                        logoff();
                        frmLogin fr1 = new frmLogin(_settings);


                        fr1.ShowDialog();

                        if (fr1.mostrarcsDetalhesDisciplina)
                        {
                            frmcsDetalhesDisciplina frmcsDetalhesDisciplina = new frmcsDetalhesDisciplina(_settings);
                            frmcsDetalhesDisciplina.MdiParent = this;
                            frmcsDetalhesDisciplina.Show();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void tsmAlterar_senha_Click(object sender, EventArgs e)
        {
            try
            {
                frmTrocaSenha fr_troca_psw = new frmTrocaSenha();

                fr_troca_psw.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        #endregion


        #region Secretaria

        private void novaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                frmAluno fr1 = new frmAluno(_settings, null);
                fr1.MdiParent = this;
                fr1.Show();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                frmProcurarAluno form = new frmProcurarAluno("aluno", _settings);
                form.MdiParent = this;
                //form.tipo_form = "aluno";
                form.Show();

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void inativarAlunosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                frmInativarAlunos frmInativarAlunos = new frmInativarAlunos(_settings);
                frmInativarAlunos.MdiParent = this;
                frmInativarAlunos.Show();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion


        #region Professores

        private void tsmiPassporte_prof_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                frmProcurarAluno form = new frmProcurarAluno("passaporte", _settings);
                form.MdiParent = this;
                //form.tipo_form = "passaporte";
                form.Show();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

            Cursor.Current = Cursors.Default;
        }

        private void tsmiPassporte_secr_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                frmProcurarAluno form = new frmProcurarAluno("passaporte", _settings);
                form.MdiParent = this;
                //form.tipo_form = "passaporte";
                form.Show();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void atribuirMatériaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void históricoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                frmProcurarAluno form = new frmProcurarAluno("historico", _settings);
                form.MdiParent = this;
                //form.Owner = this;
                //form.tipo_form = "historico";
                form.Show();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

        }



        private void tiposDeAtendimentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                frConf_atendimento form = new frConf_atendimento();
                //form.csConfiguracoes = csConfiguracoes;
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion


        #region Relatórios

        private void relatóriosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                frRelatoriosSecretaria form = new frRelatoriosSecretaria(_settings);
                //form.csConfiguracoes = csConfiguracoes;
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        #endregion


        #region Ferramentas

        private void endereçosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                frmCadastrarEndereco frmCadastrarEndereco = new frmCadastrarEndereco();
                frmCadastrarEndereco.ShowDialog();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void tsmConfiguracoes_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                frmConfiguracoes frmConfiguracoes = new frmConfiguracoes();
                frmConfiguracoes.ShowDialog();
                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        #endregion


        #region Ajuda

        private void localDaAplicaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Cursor.Current = Cursors.Default;
                if (DialogResult.OK == MessageBox.Show("Pasta da Aplicação: " + AppDomain.CurrentDomain.BaseDirectory + " - Deseja acessa-la ?", "E-matrícula: Pasta da Aplicação", MessageBoxButtons.OKCancel))
                {
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Cursor.Current = Cursors.Default;
                frmSobre form = new frmSobre();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

        }

        #endregion


        private void disciplinasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                frRelatoriosAtendimentos form = new frRelatoriosAtendimentos();
                form.MdiParent = this;
                form.Show();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void listaDeAtualizaçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                frmAtualizacoes fr = new frmAtualizacoes();
                fr.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;

        }

        private void detalhesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                frmcsDetalhesDisciplina frmcsDetalhesDisciplina = new frmcsDetalhesDisciplina(_settings);
                frmcsDetalhesDisciplina.MdiParent = this;
                frmcsDetalhesDisciplina.Show();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;

        }



        private void tsmAjuda_Click(object sender, EventArgs e)
        {

        }

        private void atribuirAtividadeExtraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAtividadeExtra frmAtividadeExtra = new frmAtividadeExtra();
            frmAtividadeExtra.ShowDialog();
        }

        private void tsmProfessores_Click(object sender, EventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.E))
            {
                editarToolStripMenuItem.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.N))
            {
                novaToolStripMenuItem.PerformClick();
                return true;
            }

            if (keyData == (Keys.Control | Keys.H))
            {
                históricoToolStripMenuItem.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void eliminaçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProcurarAluno frmProcurarAluno = new frmProcurarAluno("eliminacoes", _settings);
            //frmProcurarAluno.tipo_form = "eliminacoes";
            frmProcurarAluno.ShowDialog();
        }

        private void rAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImprimirRaLote frmImprimirRaLote = new frmImprimirRaLote(_settings);
            //TODO:: frmImprimirRaLote.MdiParent = this;?
            frmImprimirRaLote.ShowDialog();
        }

        private void usuáriosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmUsuario frmUsuario = new frmUsuario(_settings);
            frmUsuario.ShowDialog();
        }
    }
}
