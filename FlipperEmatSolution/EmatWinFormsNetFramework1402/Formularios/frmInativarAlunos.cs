using EmatWinFormsNetFramework1402.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frmInativarAlunos : Form
    {
        //TODO:: VERIFICAR QUESTÃO DE DISCIPLINA ATUAL Vs ULTIMO ATENDIMENTO
        private readonly IEmatriculaSettings _settings;
        List<Relatorios.Geral.DadosAlunoParaImpressao.Linha> listaDadosAlunoParaImpressao;

        public frmInativarAlunos(IEmatriculaSettings settings)
        {
            InitializeComponent();

            _settings = settings;

            cmbOrdenar.Items.Add("Nmat");
            cmbOrdenar.Items.Add("Nome");
            cmbOrdenar.Items.Add("Rg");
            cmbOrdenar.Items.Add("Ra");
            cmbOrdenar.Items.Add("Ensino");
            cmbOrdenar.Items.Add("Disciplina");
            cmbOrdenar.Items.Add("UltimoAtendimento");
            cmbOrdenar.SelectedIndex = 0;
        }

        private void Buscar()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //var context = new Modelo.Modelo();

                var query = from atendimentoAluno in _settings.Context.ATENDIMENTO_ALUNO.AsNoTracking()
                            group atendimentoAluno by atendimentoAluno.DISCIPLINA_ALUNO into agrupados
                            where (agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.ATIVO == true)
                            select new Relatorios.Geral.DadosAlunoParaImpressao.Linha
                            {
                                Nmat = agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.N_MAT,
                                Nome = agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.NOME,
                                Rg = agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.RG,
                                Ra = agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.RA,
                                EnsinoAluno = agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ENSINO_ALUNO,
                                DisciplinaAluno = agrupados.FirstOrDefault().DISCIPLINA_ALUNO,
                                UltimoAtendimentoAluno = agrupados.OrderByDescending(x => x.DT_ATENDIMENTO).FirstOrDefault(),
                            };

                var query2 = from atendimentoAluno2 in query
                             group atendimentoAluno2 by atendimentoAluno2.EnsinoAluno into agrupados
                             select new Relatorios.Geral.DadosAlunoParaImpressao.Linha
                             {
                                 Nmat = agrupados.FirstOrDefault().Nmat,
                                 Nome = agrupados.FirstOrDefault().Nome,
                                 Rg = agrupados.FirstOrDefault().Rg,
                                 Ra = agrupados.FirstOrDefault().Ra,
                                 EnsinoAluno = agrupados.FirstOrDefault().EnsinoAluno,
                                 DisciplinaAluno = agrupados.FirstOrDefault().DisciplinaAluno,
                                 UltimoAtendimentoAluno = agrupados.OrderByDescending(x => x.UltimoAtendimentoAluno.DT_ATENDIMENTO).FirstOrDefault().UltimoAtendimentoAluno
                             };

                //var query = from atendimentoAluno in context.ATENDIMENTO_ALUNO
                //            group atendimentoAluno by atendimentoAluno.DISCIPLINA_ALUNO into agrupados
                //            where (agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ATUAL == true &&
                //                   agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ENSINO_ALUNO.ATUAL == true &&
                //                   agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.ATIVO == true)
                //            select new Relatorios.Geral.DadosAlunoParaImpressao.Linha
                //            {
                //                Nmat = agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.N_MAT,
                //                Nome = agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.NOME,
                //                Rg = agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.RG,
                //                Ra = agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ENSINO_ALUNO.ALUNO.RA,
                //                EnsinoAluno = agrupados.FirstOrDefault().DISCIPLINA_ALUNO.ENSINO_ALUNO,
                //                DisciplinaAluno = agrupados.FirstOrDefault().DISCIPLINA_ALUNO,
                //                UltimoAtendimento = agrupados.Max(a => a.DT_ATENDIMENTO)
                //                
                //            };

                listaDadosAlunoParaImpressao = query2.Where(a => a.UltimoAtendimentoAluno.DT_ATENDIMENTO <= dtpData.Value).ToList();
                Ordenar();
                dgvDetalhes.DataSource = (from a in listaDadosAlunoParaImpressao
                                          select new
                                          {
                                              Nmat = a.Nmat,
                                              Nome = a.Nome,
                                              Rg = a.Rg,
                                              Ra = a.Ra,
                                              Ensino = (Enumeradores.Ensino)a.EnsinoAluno.COD_ENSINO,
                                              Disciplina = a.DisciplinaAluno.DISCIPLINA.NOME,
                                              ÚltimoAtendimento = a.UltimoAtendimentoAluno.DT_ATENDIMENTO
                                          }).ToList();



                dgvDetalhes.Columns[dgvDetalhes.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


                lblQuantidade.Text = dgvDetalhes.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void Ordenar()
        {
            try
            {
                switch (cmbOrdenar.Text)
                {
                    case "Nmat":
                        listaDadosAlunoParaImpressao = listaDadosAlunoParaImpressao.OrderBy(a => a.Nmat).ToList();
                        break;
                    case "Nome":
                        listaDadosAlunoParaImpressao = listaDadosAlunoParaImpressao.OrderBy(a => a.Nome).ToList();
                        break;
                    case "Rg":
                        listaDadosAlunoParaImpressao = listaDadosAlunoParaImpressao.OrderBy(a => a.Rg).ToList();
                        break;
                    case "Ra":
                        listaDadosAlunoParaImpressao = listaDadosAlunoParaImpressao.OrderBy(a => a.Ra).ToList();
                        break;
                    case "Ensino":
                        listaDadosAlunoParaImpressao = listaDadosAlunoParaImpressao.OrderBy(a => (Enumeradores.Ensino)a.EnsinoAluno.COD_ENSINO).ToList();
                        break;
                    case "Disciplina":
                        listaDadosAlunoParaImpressao = listaDadosAlunoParaImpressao.OrderBy(a => a.DisciplinaAluno.DISCIPLINA.NOME).ToList();
                        break;
                    case "UltimoAtendimento":
                        listaDadosAlunoParaImpressao = listaDadosAlunoParaImpressao.OrderBy(a => a.UltimoAtendimentoAluno.DT_ATENDIMENTO).ToList();
                        break;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InativarAlunos()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (MessageBox.Show("Deseja Inativar os aluno listados ?", "Inativar Aluno", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (var dados in listaDadosAlunoParaImpressao)
                    {
                        //var context = new Modelo.Modelo();

                        var query = (from aluno in _settings.Context.ALUNO
                                     where aluno.N_MAT == dados.Nmat
                                     select aluno).First();
                        query.ATIVO = false;

                        var queryAddMovimentacao = _settings.Context.MOVIMENTACAO.Add(new Modelo.MOVIMENTACAO
                        {
                            COD_SITUACAO = Convert.ToByte(Enumeradores.SituacaoAluno.NÃO_COMPARECIMENTO),
                            COD_ENSINO_ALUNO = dados.EnsinoAluno.CODIGO,
                            COD_USUARIO = (short)Utils.csUsuarioLogado.usuario.Codigo,
                            DT_MOVIMENTACAO = DateTime.Now,
                            MOTIVO = "INATIVADO POR NÃO COMPARECIMENTO"
                        });

                        _settings.Context.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Buscar();
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
                Cursor.Current = Cursors.WaitCursor;
                frCrystalReport frCrystalReport = new frCrystalReport();
                frCrystalReport.cr_report = Relatorios.Geral.Relatorios.rptListaAlunosInativacao(
                    listaDadosAlunoParaImpressao,
                    dtpData.Value.ToString("dd/MM/yyyy"));
                frCrystalReport.ShowDialog();
                Cursor.Current = Cursors.Default;

                btnInativar.Enabled = true;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

        }

        private void btnInativar_Click(object sender, EventArgs e)
        {
            try
            {
                InativarAlunos();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void btnOrdenar_Click(object sender, EventArgs e)
        {
            Ordenar();
            dgvDetalhes.DataSource = (from a in listaDadosAlunoParaImpressao
                                      select new
                                      {
                                          Nmat = a.Nmat,
                                          Nome = a.Nome,
                                          Rg = a.Rg,
                                          Ra = a.Ra,
                                          Ensino = (Enumeradores.Ensino)a.EnsinoAluno.COD_ENSINO,
                                          Disciplina = a.DisciplinaAluno.DISCIPLINA.NOME,
                                          ÚltimoAtendimento = a.UltimoAtendimentoAluno.DT_ATENDIMENTO
                                      }).ToList();
        }
    }
}