using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EmatWinFormsNetFramework1402.Classes;
using EmatWinFormsNetFramework1402.DAO;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frmEliminacoes : Form
    {
        private readonly IEmatriculaSettings _settings;
        public Aluno aluno;

        public frmEliminacoes(IEmatriculaSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDisciplina.SelectedIndex > -1)
                {
                    foreach (DisciplinaAluno disciplinaAluno in Aluno.GetEnsinoAlunoAtual(aluno).ListaDisciplinaAluno)
                    {
                        if (disciplinaAluno.Disciplina.Nome == cmbDisciplina.Text)
                        {
                            disciplinaAluno.Concluida = true;
                            Media media = new Media();

                            DateTime dataEliminacao;
                            if (DateTime.TryParse(mtbDataEliminacao.Text, out dataEliminacao))
                            {
                                media.DtMedia = dataEliminacao.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                //media.DtMedia = DateTime.Now.ToString("dd/MM/yyyy");
                                media.DtMedia = DateTime.Now.ToString("yyyy-MM-dd");
                            }




                            if (txtMedia.Text != string.Empty)
                                media.Valor = txtMedia.Text;
                            else
                                media.Valor = "0";

                            media.UsuarioCadastro = Utils.csUsuarioLogado.usuario;

                            if (txtInstituicao.Text != string.Empty)
                                media.Instituicao = txtInstituicao.Text;
                            else
                                media.Instituicao = "---DISCIPLINA-ELIMINADA---";

                            if (cmbCidade.Text != string.Empty)
                                media.Cidade = (Cidade)cmbCidade.SelectedItem;
                            else
                                media.Cidade = _settings.CeejaCidade;

                            disciplinaAluno.Media = media;

                            if (DAO.MediaDAO.Gravar(disciplinaAluno) > 0)
                            {
                                if (DAO.DisciplinaAlunoDAO.Gravar(disciplinaAluno) > 0)
                                {
                                    dgvEliminacoes.Rows.Add();

                                    dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[0].Value = disciplinaAluno;
                                    dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[1].Value = disciplinaAluno.Disciplina.Nome;
                                    dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[2].Value = disciplinaAluno.Media.Instituicao;
                                    dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[3].Value = disciplinaAluno.Media.Cidade.Nome;
                                    dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[4].Value = disciplinaAluno.Media.Cidade.Uf.Sigla;
                                    dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[5].Value = disciplinaAluno.Media.Valor;
                                    dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[6].Value = "Excluir";

                                    CarregarDisciplinas();
                                }
                                else
                                {
                                    ErrorLog.ErrorHandleService.ExibirMsgBoxError("Algum problema durante a gravação da média.");
                                }
                            }
                            else
                            {
                                ErrorLog.ErrorHandleService.ExibirMsgBoxError("Algum problema durante a gravação da média.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO:: Reverter caso ocorra alguma falha na tentativa de gravar uma eliminação
                //Deverá voltar a disciplina CONCLUIDA = 0


                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);

            }
        }

        private void frEliminacoes_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarDisciplinas();




                cmbUf.SelectedIndexChanged -= cmbUf_SelectedIndexChanged;
                cmbUf.DataSource = UfDAO.ExibirTodos(PaisDAO.Consultar(_settings.CeejaCidade.Codigo)); //TODO:: VERIFICAR ISSO AQUI PELO AMOR SSE PÁ FAZER COM EF ???
                cmbUf.DisplayMember = "Nome";
                cmbUf.ValueMember = "Codigo";
                cmbUf.SelectedIndex = -1;
                cmbUf.SelectedIndexChanged += cmbUf_SelectedIndexChanged;

                foreach (DisciplinaAluno disciplinaAluno in Classes.Aluno.GetEnsinoAlunoAtual(aluno).ListaDisciplinaAluno)
                {
                    if (disciplinaAluno.Concluida)
                    {
                        dgvEliminacoes.Rows.Add();
                        dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[0].Value = disciplinaAluno;
                        dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[1].Value = disciplinaAluno.Disciplina.Nome;
                        if (disciplinaAluno.Media != null)
                        {

                            dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[2].Value = disciplinaAluno.Media.Instituicao;
                            dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[3].Value = disciplinaAluno.Media.Cidade.Nome;
                            dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[4].Value = disciplinaAluno.Media.Cidade.Uf.Sigla;
                            dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[5].Value = disciplinaAluno.Media.Valor;
                            dgvEliminacoes.Rows[dgvEliminacoes.Rows.Count - 1].Cells[6].Value = "Excluir";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void CarregarDisciplinas()
        {
            cmbDisciplina.DataSource = Classes.Aluno.GetEnsinoAlunoAtual(aluno).ListaDisciplinaAluno.
                   Where(x => x.Concluida == false).Select(r => r.Disciplina).ToList();
            cmbDisciplina.DisplayMember = "Nome";
            cmbDisciplina.ValueMember = "Codigo";
            cmbDisciplina.SelectedIndex = -1;
        }

        private void cmbUf_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUf.TextChanged -= cmbUf_TextChanged;
            try
            {
                if (cmbUf.SelectedIndex > -1)
                {
                    cmbCidade.SelectedIndexChanged -= cmbCidade_SelectedIndexChanged;
                    cmbCidade.DataSource = DAO.CidadeDAO.ExibirTodos((Classes.Uf)cmbUf.SelectedItem);
                    cmbCidade.DisplayMember = "Nome";
                    cmbCidade.ValueMember = "Codigo";
                    cmbCidade.SelectedIndex = -1;
                    cmbCidade.SelectedIndexChanged += cmbCidade_SelectedIndexChanged;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            finally
            {
                cmbUf.TextChanged += cmbUf_TextChanged;
            }
        }

        private void cmbCidade_SelectedIndexChanged(object sender, EventArgs e)
        {

            //try
            //{
            //    if (cmbUf.SelectedIndex > -1)
            //    {
            //        //cmbInstituicao.SelectedIndexChanged -= cmbInstituicao_SelectedIndexChanged;
            //        //cmbInstituicao.DataSource = DAO.InstituicaoDAO.ExibirTodos().
            //        //    Where(r => r.Cidade.Codigo == ((Classes.Cidade)cmbCidade.SelectedItem).Codigo);
            //        //cmbInstituicao.DisplayMember = "Nome";
            //        //cmbInstituicao.ValueMember = "Codigo";
            //        //cmbInstituicao.SelectedIndex = -1;
            //        //cmbInstituicao.SelectedIndexChanged += cmbInstituicao_SelectedIndexChanged;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ErrorLog.csControle_erros.ExibirErroMessBox(ex.Message);
            //}
        }

        private void dgvEliminacoes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    if (((DisciplinaAluno)dgvEliminacoes.Rows[e.RowIndex].Cells[0].Value).Media.AtendimentoAluno == null)
                    {

                        if (DAO.MediaDAO.Excluir(((DisciplinaAluno)dgvEliminacoes.Rows[e.RowIndex].Cells[0].Value)) == 1)
                        {
                            ((DisciplinaAluno)dgvEliminacoes.Rows[e.RowIndex].Cells[0].Value).Concluida = false;
                            DAO.DisciplinaAlunoDAO.Atualizar(((DisciplinaAluno)dgvEliminacoes.Rows[e.RowIndex].Cells[0].Value));
                            dgvEliminacoes.Rows.RemoveAt(e.RowIndex);

                            CarregarDisciplinas();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não é possivel excluir uma Média originária de um atendimento no passaporte.", "E-matricula");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

        }

        private void cmbUf_TextChanged(object sender, EventArgs e)
        {
            cmbUf.SelectedIndexChanged -= cmbUf_SelectedIndexChanged;
            try
            {
                if (cmbUf.SelectedIndex > -1)
                {
                    cmbCidade.SelectedIndexChanged -= cmbCidade_SelectedIndexChanged;
                    cmbCidade.DataSource = DAO.CidadeDAO.ExibirTodos((Classes.Uf)cmbUf.SelectedItem);
                    cmbCidade.DisplayMember = "Nome";
                    cmbCidade.ValueMember = "Codigo";
                    cmbCidade.SelectedIndex = -1;
                    cmbCidade.SelectedIndexChanged += cmbCidade_SelectedIndexChanged;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            finally
            {
                cmbUf.SelectedIndexChanged += cmbUf_SelectedIndexChanged;
            }
        }

        private void cmbCidade_Leave(object sender, EventArgs e)
        {
            if (cmbCidade.Text != "")
                if (cmbCidade.FindStringExact(cmbCidade.Text) <= 0)
                {
                    MessageBox.Show("Cidade não cadastrada.", "E-matrícula - Eliminação");
                    cmbCidade.Text = "";
                    cmbCidade.Focus();
                }
        }

        private void cmbUf_Leave(object sender, EventArgs e)
        {
            if (cmbUf.Text != "")
                if (cmbUf.FindStringExact(cmbUf.Text) <= 0)
                {
                    MessageBox.Show("Estado não cadastrado.", "E-matrícula - Eliminação");
                    cmbUf.Text = "";
                    cmbUf.Focus();
                }
        }

        private void dgvEliminacoes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //TODO:: IMPLEMENTAR DUPLO CLICK PARA EDITAR ELIMINAÇÃO
        }
    }
}
