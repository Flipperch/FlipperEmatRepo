using EmatWinFormsNetFramework1402.ErrorLog;
using EmatWinFormsNetFramework1402.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Formularios
{
    //TODO:: Concluir frmExcluirAluno e botar pra rodar!!!
    public partial class frmExcluirAluno : Form
    {
        private readonly IEmatriculaSettings _settings;
        private Classes.Aluno _objAluno;
        public bool Excluido { get; set; }

        public frmExcluirAluno(Classes.Aluno objAluno, IEmatriculaSettings settings)
        {
            InitializeComponent();
            
            _settings = settings;

            _objAluno = objAluno;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //var context = new Modelo.Modelo();
                var usuariosPermitidos = _settings.Context.USUARIO.AsNoTracking().Where(
                        u => u.NIVEL_ACESSO == (byte)Enumeradores.NivelAcesso.ADMINISTRADOR ||
                        u.NIVEL_ACESSO == (byte)Enumeradores.NivelAcesso.SECRETARIOADM ||
                        u.NIVEL_ACESSO == (byte)Enumeradores.NivelAcesso.COORDENADOR ||
                        u.NIVEL_ACESSO == (byte)Enumeradores.NivelAcesso.DIRETOR).ToList();

                List<string> listaSenhasPermitidas = usuariosPermitidos.Select(u => u.SENHA).ToList();

                //Veriricar Senha
                if (listaSenhasPermitidas.Contains(txtSenha.Text))
                {
                    if (MessageBox.Show(
                        "Deseja excluir a matrícula do aluno? Não será possível recuperar os dados após a exclusão.",
                        "Excluir matrícula.",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //TODO:: Add Log no banco para

                        ExcluirAluno();
                    }
                }
                else
                {
                    MessageBox.Show("Senha incorreta ou usuário não permitido para efetuar exclusões");
                    txtSenha.Text = string.Empty;
                }

                Cursor.Current = Cursors.Default;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ematrícula - Erro");
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Excluido = false;
            this.Close();
        }

        private void ExcluirAluno()
        {
            try
            {
                //var context = new Modelo.Modelo();

                var aluno = _settings.Context.ALUNO.Where(a => a.N_MAT == _objAluno.NMatricula)
                    .Include(a => a.LOCAL_NASCIMENTO)
                    .Include(a => a.ENSINO_ALUNO)
                    .Include(a => a.ENDERECO_ALUNO)
                    .Include(a => a.EMPREGO_ALUNO)
                    .SingleOrDefault();
                var ensinos = _settings.Context.ENSINO_ALUNO.Where(en => en.ALUNO.N_MAT == _objAluno.NMatricula);
                var disciplinas = _settings.Context.DISCIPLINA_ALUNO.Where(d => d.ENSINO_ALUNO.ALUNO.N_MAT == _objAluno.NMatricula);
                var atendimentos = _settings.Context.ATENDIMENTO_ALUNO.Where(d => d.DISCIPLINA_ALUNO.ENSINO_ALUNO.N_MAT == _objAluno.NMatricula);
                var medias = _settings.Context.MEDIA.Where(d => d.DISCIPLINA_ALUNO.ENSINO_ALUNO.N_MAT == _objAluno.NMatricula);
                var notas = _settings.Context.NOTA.Where(d => d.ATENDIMENTO_ALUNO.DISCIPLINA_ALUNO.ENSINO_ALUNO.N_MAT == _objAluno.NMatricula);
                var historicosEscolar = _settings.Context.HISTORICO_ESCOLAR.Where(d => d.ENSINO_ALUNO.N_MAT == _objAluno.NMatricula);
                var rematriculas = _settings.Context.REMATRICULA.Where(d => d.ENSINO_ALUNO.N_MAT == _objAluno.NMatricula);
                var atividadesExtra = _settings.Context.ATIVIDADE_EXTRA.Where(d => d.ENSINO_ALUNO.N_MAT == _objAluno.NMatricula);
                var movimentacoes = _settings.Context.MOVIMENTACAO.Where(d => d.ENSINO_ALUNO.N_MAT == _objAluno.NMatricula);
                var cartao = _settings.Context.CARTAO_ACESSO.SingleOrDefault(d => d.ALUNO == _objAluno.NMatricula);

                ltbLog.Items.Add("Removendo Notas...");
                foreach (var nota in notas)
                {
                    _settings.Context.NOTA.Remove(nota);
                }

                ltbLog.Items.Add("Removendo Médias...");
                foreach (var media in medias)
                {
                    _settings.Context.MEDIA.Remove(media);
                }

                ltbLog.Items.Add("Removendo Atendimentos...");
                foreach (var atendimento in atendimentos)
                {
                    _settings.Context.ATENDIMENTO_ALUNO.Remove(atendimento);
                }

                ltbLog.Items.Add("Removendo Disciplinas...");
                foreach (var disciplina in disciplinas)
                {
                    _settings.Context.DISCIPLINA_ALUNO.Remove(disciplina);
                }

                ltbLog.Items.Add("Removendo Historicos Escolares...");
                foreach (var historicoEscolar in historicosEscolar)
                {
                    _settings.Context.HISTORICO_ESCOLAR.Remove(historicoEscolar);
                }

                ltbLog.Items.Add("Removendo Atividades Extra...");
                foreach (var atividadeExtra in atividadesExtra)
                {
                    _settings.Context.ATIVIDADE_EXTRA.Remove(atividadeExtra);
                }

                ltbLog.Items.Add("Removendo Rematriculas...");
                foreach (var rematricula in rematriculas)
                {
                    _settings.Context.REMATRICULA.Remove(rematricula);
                }

                ltbLog.Items.Add("Removendo Movimentaçoes...");
                foreach (var movimentacao in movimentacoes)
                {
                    _settings.Context.MOVIMENTACAO.Remove(movimentacao);
                }

                ltbLog.Items.Add("Removendo Ensinos...");
                foreach (var ensino in ensinos)
                {
                    _settings.Context.ENSINO_ALUNO.Remove(ensino);
                }

                if (aluno.LOCAL_NASCIMENTO != null)
                {
                    ltbLog.Items.Add("Removendo LocalNascimento...");
                    _settings.Context.LOCAL_NASCIMENTO.Remove(aluno.LOCAL_NASCIMENTO);
                }

                if (aluno.ENDERECO_ALUNO != null)
                {
                    ltbLog.Items.Add("Removendo Endereço...");
                    _settings.Context.ENDERECO_ALUNO.Remove(aluno.ENDERECO_ALUNO);
                }

                if (aluno.EMPREGO_ALUNO != null)
                {
                    ltbLog.Items.Add("Removendo Emprego...");
                    _settings.Context.EMPREGO_ALUNO.Remove(aluno.EMPREGO_ALUNO);
                }

                if (cartao != null)
                {
                    ltbLog.Items.Add("Removendo Cartão Acesso...");
                    _settings.Context.CARTAO_ACESSO.Remove(cartao);
                }

                ltbLog.Items.Add("Removendo Aluno...");
                _settings.Context.ALUNO.Remove(aluno);

                _settings.Context.SaveChanges();

                MessageBox.Show("Registro removido com sucesso.", "Ematricula");

                Excluido = true;

                this.Close();
            }
            catch (Exception ex)
            {
                Excluido = false;
                throw ex;
            }
        }

        private void frmExcluirAluno_Load(object sender, EventArgs e)
        {
            lblAluno.Text += " " + _objAluno.Nome + " - Nº: " + _objAluno.NMatricula;
        }
    }
}
