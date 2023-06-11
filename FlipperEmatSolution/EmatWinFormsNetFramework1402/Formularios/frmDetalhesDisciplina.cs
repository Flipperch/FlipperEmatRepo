using EmatWinFormsNetFramework1402.Classes;
using EmatWinFormsNetFramework1402.Utils;
using MoreLinq;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace EmatWinFormsNetFramework1402.Formularios
{
    //TODO=> rEVISAR ESTE FORM frmcsDetalhesDisciplina
    public partial class frmcsDetalhesDisciplina : Form
    {
        private readonly IEmatriculaSettings _settings;
        List<Relatorios.Geral.DadosAlunoParaImpressao.Linha> ListaDadosAluno;
        List<Relatorios.Geral.DadosAlunoParaImpressao.Linha> ListaDadoFiltradosOrdenados;

        public frmcsDetalhesDisciplina(IEmatriculaSettings settings)
        {
            InitializeComponent();

            _settings = settings;

            cmbEnsino.SelectedIndex = 0;

            cmbDisciplina.SelectedIndexChanged -= cmbDisciplina_SelectedIndexChanged;
            //var context = new Modelo.Modelo();
            var query = _settings.Context.DISCIPLINA.AsNoTracking().ToList();
            cmbDisciplina.DataSource = query;
            cmbDisciplina.ValueMember = "CODIGO";
            cmbDisciplina.DisplayMember = "NOME";

            cmbDisciplina.SelectedIndexChanged += cmbDisciplina_SelectedIndexChanged;

            cmbOrdenar.Items.Add("Nmat");
            cmbOrdenar.Items.Add("Nome");
            cmbOrdenar.Items.Add("Ra");
            cmbOrdenar.Items.Add("Telefone");
            cmbOrdenar.Items.Add("Celular");
            cmbOrdenar.Items.Add("Ensino");
            cmbOrdenar.Items.Add("UltimoAtendimento");
            cmbOrdenar.SelectedIndex = 0;
        }

        private void frmcsDetalhesDisciplina_Load(object sender, EventArgs e)
        {
            try
            {
                if (Utils.csUsuarioLogado.modeloProfessor != null)
                {
                    cmbDisciplina.SelectedValue = Utils.csUsuarioLogado.modeloProfessor.DISCIPLINA.CODIGO;
                }
                else
                {
                    cmbDisciplina.SelectedIndex = 0;
                    cmbDisciplina.Enabled = true;
                }

                Buscar();
                Filtrar();
                Ordenar();

                dgvAlunos.DataSource = (from a in ListaDadoFiltradosOrdenados
                                        select new
                                        {
                                            a.Nmat,
                                            a.Nome,
                                            a.Ra,
                                            Ensino = (Enumeradores.Ensino)a.EnsinoAluno.COD_ENSINO,
                                            a.Celular,
                                            a.Telefone,
                                            a.UltimoAtendimentoAluno.DT_ATENDIMENTO
                                        }).ToList();
                lblQtdDgvAlunos.Text = "Quantidade: " + dgvAlunos.RowCount;

                BuscarDetalhesDisciplina();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        public void Buscar()
        {
            try
            {
                //var context = new Modelo.Modelo();

                var query = from atendimentoAluno in _settings.Context.ATENDIMENTO_ALUNO.AsNoTracking()//.Include("DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO")
                            where atendimentoAluno.DISCIPLINA_ALUNO.ATUAL == true && atendimentoAluno.DISCIPLINA_ALUNO.ENSINO_ALUNO.ATUAL == true
                            && atendimentoAluno.DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.ATIVO == ckbAtivos.Checked
                            && atendimentoAluno.DISCIPLINA_ALUNO.DISCIPLINA.CODIGO == ((Modelo.DISCIPLINA)cmbDisciplina.SelectedItem).CODIGO
                            select new Relatorios.Geral.DadosAlunoParaImpressao.Linha
                            {
                                Nmat = atendimentoAluno.DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.N_MAT,
                                Nome = atendimentoAluno.DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.NOME,
                                Ra = atendimentoAluno.DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.RA,
                                Celular = atendimentoAluno.DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.CELULAR,
                                Telefone = atendimentoAluno.DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.TELEFONE,
                                EnsinoAluno = atendimentoAluno.DISCIPLINA_ALUNO.ENSINO_ALUNO,
                                UltimoAtendimentoAluno = atendimentoAluno //(Modelo.ATENDIMENTO_ALUNO)agrupados.MaxBy(a => a.DT_ATENDIMENTO)
                            };

                //var query = from a in context.ATENDIMENTO_ALUNO

                ListaDadosAluno = query.ToList();

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        public void Filtrar()
        {
            try
            {
                if (cmbEnsino.Text == "Todos")
                {
                    ListaDadoFiltradosOrdenados = ListaDadosAluno;
                }
                else
                {
                    if (cmbEnsino.Text == "Fundamental")
                    {
                        ListaDadoFiltradosOrdenados = ListaDadosAluno
                            .Where(a => a.EnsinoAluno.COD_ENSINO == (byte)Enumeradores.Ensino.FUNDAMENTAL).ToList();
                    }
                    else
                    {
                        ListaDadoFiltradosOrdenados = ListaDadosAluno
                           .Where(a => a.EnsinoAluno.COD_ENSINO == (byte)Enumeradores.Ensino.MÉDIO).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void Ordenar()
        {
            try
            {
                switch (cmbOrdenar.Text)
                {
                    case "Nmat":
                        ListaDadoFiltradosOrdenados = ListaDadoFiltradosOrdenados.OrderBy(a => a.Nmat).ToList();
                        break;
                    case "Nome":
                        ListaDadoFiltradosOrdenados = ListaDadoFiltradosOrdenados.OrderBy(a => a.Nome).ToList();
                        break;
                    case "Ra":
                        ListaDadoFiltradosOrdenados = ListaDadoFiltradosOrdenados.OrderBy(a => a.Ra).ToList();
                        break;
                    case "Celular":
                        ListaDadoFiltradosOrdenados = ListaDadoFiltradosOrdenados.OrderBy(a => a.Celular).ToList();
                        break;
                    case "Telefone":
                        ListaDadoFiltradosOrdenados = ListaDadoFiltradosOrdenados.OrderBy(a => a.Telefone).ToList();
                        break;
                    case "Ensino":
                        ListaDadoFiltradosOrdenados = ListaDadoFiltradosOrdenados.OrderBy(a => (Enumeradores.Ensino)a.EnsinoAluno.COD_ENSINO).ToList();
                        break;
                    case "UltimoAtendimento":
                        ListaDadoFiltradosOrdenados = ListaDadoFiltradosOrdenados.OrderBy(a => a.UltimoAtendimentoAluno.DT_ATENDIMENTO).ToList();
                        break;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BuscarDetalhesDisciplina()
        {
            int qtdAtivosFundamental = QuantidadeAlunos((Modelo.DISCIPLINA)cmbDisciplina.SelectedItem, 1, true);
            int qtdInativosFundamental = QuantidadeAlunos((Modelo.DISCIPLINA)cmbDisciplina.SelectedItem, 1, false);
            int qtdConcluintesFundamental = 0;

            int qtdAtivosMedio = QuantidadeAlunos((Modelo.DISCIPLINA)cmbDisciplina.SelectedItem, 2, true); ;
            int qtdInativosMedio = QuantidadeAlunos((Modelo.DISCIPLINA)cmbDisciplina.SelectedItem, 2, false); ;
            int qtdConcluintesMedio = 0;

            int qtdAtivosTotal = qtdAtivosFundamental + qtdAtivosMedio;
            int qtdInativosTotal = qtdInativosFundamental + qtdInativosMedio;
            int qtdConcluintesTotal = 0;

            //Grade
            dgvDetalhes.Rows.Clear();

            dgvDetalhes.Rows.Add();
            dgvDetalhes.Rows[0].Cells[0].Value = "Ativos";
            dgvDetalhes.Rows[0].Cells[1].Value = qtdAtivosFundamental;
            dgvDetalhes.Rows[0].Cells[2].Value = qtdAtivosMedio;
            dgvDetalhes.Rows[0].Cells[3].Value = qtdAtivosTotal;

            dgvDetalhes.Rows.Add();
            dgvDetalhes.Rows[1].Cells[0].Value = "Inativos";
            dgvDetalhes.Rows[1].Cells[1].Value = qtdInativosFundamental;
            dgvDetalhes.Rows[1].Cells[2].Value = qtdInativosMedio;
            dgvDetalhes.Rows[1].Cells[3].Value = qtdInativosTotal;

            //dgvDetalhes.Rows.Add();
            //dgvDetalhes.Rows[2].Cells[0].Value = "Concluintes";
            //dgvDetalhes.Rows[2].Cells[1].Value = qtdConcluintesFundamental;
            //dgvDetalhes.Rows[2].Cells[2].Value = qtdConcluintesMedio;
            //dgvDetalhes.Rows[2].Cells[3].Value = qtdConcluintesTotal;
        }

        public int QuantidadeAlunos(Modelo.DISCIPLINA disciplina, int ensino, bool ativo = true)
        {
            try
            {
                //Dados
                //var context = new Modelo.Modelo();

                int qtd = (from disciplinaAluno in _settings.Context.DISCIPLINA_ALUNO.AsNoTracking()
                           where (disciplinaAluno.COD_DISCIPLINA == disciplina.CODIGO)
                           && (disciplinaAluno.ENSINO_ALUNO.ALUNO.ATIVO == ativo)
                           && (disciplinaAluno.ENSINO_ALUNO.ALUNO.CONCLUINTE == false)
                           && (disciplinaAluno.ATUAL == true)
                           && (disciplinaAluno.CONCLUIDA == false)
                           && (disciplinaAluno.ENSINO_ALUNO.COD_ENSINO == ensino)
                           && (disciplinaAluno.ENSINO_ALUNO.ATUAL == true)
                           select disciplinaAluno).Count();

                return qtd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cmbDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Buscar();
                Filtrar();
                Ordenar();

                dgvAlunos.DataSource = (from a in ListaDadoFiltradosOrdenados
                                        select new
                                        {
                                            a.Nmat,
                                            a.Nome,
                                            a.Ra,
                                            Ensino = (Enumeradores.Ensino)a.EnsinoAluno.COD_ENSINO,
                                            a.Celular,
                                            a.Telefone,
                                            a.UltimoAtendimentoAluno.DT_ATENDIMENTO
                                        }).ToList();
                lblQtdDgvAlunos.Text = "Quantidade: " + dgvAlunos.RowCount;
                BuscarDetalhesDisciplina();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                Buscar();
                Filtrar();
                Ordenar();

                dgvAlunos.DataSource = (from a in ListaDadoFiltradosOrdenados
                                        select new
                                        {
                                            a.Nmat,
                                            a.Nome,
                                            a.Ra,
                                            Ensino = (Enumeradores.Ensino)a.EnsinoAluno.COD_ENSINO,
                                            a.Celular,
                                            a.Telefone,
                                            a.UltimoAtendimentoAluno.DT_ATENDIMENTO
                                        }).ToList();
                lblQtdDgvAlunos.Text = "Quantidade: " + dgvAlunos.RowCount;
                BuscarDetalhesDisciplina();

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            try
            {
                Ordenar();

                dgvAlunos.DataSource = (from a in ListaDadoFiltradosOrdenados
                                        select new
                                        {
                                            a.Nmat,
                                            a.Nome,
                                            a.Ra,
                                            Ensino = (Enumeradores.Ensino)a.EnsinoAluno.COD_ENSINO,
                                            a.Celular,
                                            a.Telefone,
                                            a.UltimoAtendimentoAluno.DT_ATENDIMENTO
                                        }).ToList();

                lblQtdDgvAlunos.Text = "Quantidade: " + dgvAlunos.RowCount;
                BuscarDetalhesDisciplina();

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                frCrystalReport frCrystalReport = new frCrystalReport();
                frCrystalReport.cr_report = Relatorios.Geral.Relatorios.RptListaAlunosDisciplina(ListaDadoFiltradosOrdenados, ((Modelo.DISCIPLINA)cmbDisciplina.SelectedItem).NOME);
                frCrystalReport.ShowDialog();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }
    }
}
