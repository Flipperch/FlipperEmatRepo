using EmatWinFormsNetFramework1402.Utils;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Formularios
{
    //TODO==> FORM frRelatoriosSecretaria INCOMPLETO... Muito trabalhoso... Verificar alternativas e Eliminar... Passar para Asp.Net ?
    public partial class frRelatoriosSecretaria : Form
    {
        private readonly IEmatriculaSettings _settings;

        //TODO:: VERIFICAR E REMOVER CASO DESNECESSÁRIO
        //private Utils.csDetalhesAluno.AlunosNoSistema alunosNoSistema;
        //private Utils.csDetalhesAluno.Matriculas matriculas;
        //private Utils.csDetalhesAluno.Rematriculas rematriculas;
        //private Utils.csDetalhesAluno.HistoricoDeSituacoes historicoDeSituacoes;
        //private Utils.csDetalhesAluno.ConclusoesDeEnsino conclusoesDeEnsino;

        int ativoFundamental = 0;
        int ativoMedio = 0;
        int concluinteFundamental = 0;
        int concluinteMedio = 0;
        int naoComparecimentoFundamental = 0;
        int naoComparecimentoMedio = 0;
        int transferidoFundamental = 0;
        int transferidoMedio = 0;
        int falecidoFundamental = 0;
        int falecidoMedio = 0;

        int comMovimentacao = 0;
        int semMovimentacao = 0;

        public frRelatoriosSecretaria(IEmatriculaSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void frRelatorios_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            #region Define data limite para dtps
            dtpPeriodoInicial.MaxDate = DateTime.Now;
            dtpPeriodoFinal.MaxDate = DateTime.Now;
            dtpPeriodoFinal.MinDate = dtpPeriodoInicial.Value;
            #endregion
            try
            {
                lblTempo.Text = "Tempo: 0";
                Stopwatch sw = new Stopwatch();
                sw.Start();

                //Fixo - Chamado apenas no Load
                CarregarEstadoAtualSistema();
                //Dinâmico
                Consulta();

                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                lblTempo.Text = "Tempo: " + String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

            Cursor.Current = Cursors.Default;
        }

        #region Métodos

        private void Consulta()
        {
            try
            {
                //TODO:: Organizar -> Declaração de variáveis Matrículas, Rematrículas, Ativações, Conclusões e Inativações
                int matriculasFundamental = 0;
                int matriculasMedio = 0;
                int rematriculasFundamental = 0;
                int rematriculasMedio = 0;
                int ativacoesFundamental = 0;
                int ativacoesMedio = 0;
                int conclusoesFundamental = 0;
                int conclusoesMedio = 0;
                int inativacoesFundamental = 0;
                int inativacoesMedio = 0;
                int transferenciasFundamental = 0;
                int transferenciasMedio = 0;

                //TODO:: VERIFICAR MELHORIA DE DESEMPENHO !!!---ACESSO AO BANCO---!!!
                //_ensinosAluno Acessado duas vezes. Dois métodos diferentes. Verificar se tem como melhorar

                DateTime? _dtFinal;

                //Define PorDia ou PorPeríodo
                if (rdbPorDia.Checked)
                {
                    _dtFinal = null;
                }
                else
                {
                    _dtFinal = dtpPeriodoFinal.Value;
                }

                //Consultas de Números de Matriculas, Rematrículas, Ativações, Conclusões e Inativações(NcomApenas) (PorDia ou PorPeríodo definido no método)

                //Matrículas
                matriculasFundamental = QuantidadeMatriculas(Enumeradores.Ensino.FUNDAMENTAL, dtpPeriodoInicial.Value, _dtFinal);
                matriculasMedio = QuantidadeMatriculas(Enumeradores.Ensino.MÉDIO, dtpPeriodoInicial.Value, _dtFinal);

                //Rematrículas
                rematriculasFundamental = QuantidadeRematriculas(Enumeradores.Ensino.FUNDAMENTAL, dtpPeriodoInicial.Value, _dtFinal);
                rematriculasMedio = QuantidadeRematriculas(Enumeradores.Ensino.MÉDIO, dtpPeriodoInicial.Value, _dtFinal);

                //Ativações
                ativacoesFundamental = QuantidadeMovimentacoes(Enumeradores.SituacaoAluno.ATIVO,
                    Enumeradores.Ensino.FUNDAMENTAL,
                    dtpPeriodoInicial.Value, _dtFinal);
                ativacoesMedio = QuantidadeMovimentacoes(Enumeradores.SituacaoAluno.ATIVO,
                    Enumeradores.Ensino.MÉDIO,
                    dtpPeriodoInicial.Value, _dtFinal);

                //Conclusões
                conclusoesFundamental = QuantidadeMovimentacoes(Enumeradores.SituacaoAluno.CONCLUINTE,
                    Enumeradores.Ensino.FUNDAMENTAL,
                    dtpPeriodoInicial.Value, _dtFinal);
                conclusoesMedio = QuantidadeMovimentacoes(Enumeradores.SituacaoAluno.CONCLUINTE,
                    Enumeradores.Ensino.MÉDIO,
                    dtpPeriodoInicial.Value, _dtFinal);

                //Inativacões
                inativacoesFundamental = QuantidadeMovimentacoes(Enumeradores.SituacaoAluno.NÃO_COMPARECIMENTO,
                    Enumeradores.Ensino.FUNDAMENTAL,
                    dtpPeriodoInicial.Value, _dtFinal);
                inativacoesMedio = QuantidadeMovimentacoes(Enumeradores.SituacaoAluno.NÃO_COMPARECIMENTO,
                    Enumeradores.Ensino.MÉDIO,
                    dtpPeriodoInicial.Value, _dtFinal);

                //Transferências
                transferenciasFundamental = QuantidadeMovimentacoes(Enumeradores.SituacaoAluno.TRANSFERIDO,
                    Enumeradores.Ensino.FUNDAMENTAL,
                    dtpPeriodoInicial.Value, _dtFinal);
                transferenciasMedio = QuantidadeMovimentacoes(Enumeradores.SituacaoAluno.TRANSFERIDO,
                    Enumeradores.Ensino.MÉDIO,
                    dtpPeriodoInicial.Value, _dtFinal);

                //Preenchimento de Operações (No Load e a cada Consulta - Dinâmico)
                lblMatriculasFundamental.Text = matriculasFundamental.ToString();
                lblMatriculasMedio.Text = matriculasMedio.ToString();
                lblMatriculasTotal.Text = (matriculasFundamental + matriculasMedio).ToString();

                lblRematriculasFundamental.Text = rematriculasFundamental.ToString();
                lblRematriculasMedio.Text = rematriculasMedio.ToString();
                lblRematriculasTotal.Text = (rematriculasFundamental + rematriculasMedio).ToString();

                lblAtivacoesFundamental.Text = ativacoesFundamental.ToString();
                lblAtivacoesMedio.Text = ativacoesMedio.ToString();
                lblAtivacoesTotal.Text = (ativacoesFundamental + ativacoesMedio).ToString();

                lblConclusoesFundamental.Text = conclusoesFundamental.ToString();
                lblConclusoesMedio.Text = conclusoesMedio.ToString();
                lblConclusoesTotal.Text = (conclusoesFundamental + conclusoesMedio).ToString();

                lblInativacoesFundamental.Text = inativacoesFundamental.ToString();
                lblInativacoesMedio.Text = inativacoesMedio.ToString();
                lblInativacoesTotal.Text = (inativacoesFundamental + inativacoesMedio).ToString();

                lblTransferenciasFundamental.Text = transferenciasFundamental.ToString();
                lblTransferenciasMedio.Text = transferenciasMedio.ToString();
                lblTransferenciasTotal.Text = (transferenciasFundamental + transferenciasMedio).ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CarregarEstadoAtualSistema()
        {
            try
            {
                var _ensinosAluno = _settings.Context.ENSINO_ALUNO.Include("MOVIMENTACAO").Include("ALUNO").Where(en => en.ATUAL == true);

                //Preenchimento do Estado Atual do Sistema (FIXO)
                foreach (Modelo.ENSINO_ALUNO ensino in _ensinosAluno)
                {
                    if (ensino.MOVIMENTACAO.Count > 0)
                    {
                        if (ensino.MOVIMENTACAO.LastOrDefault().COD_SITUACAO == (byte)Enumeradores.SituacaoAluno.ATIVO)
                        {
                            if (ensino.COD_ENSINO == 1)
                            {
                                ativoFundamental++;
                            }
                            else
                            {
                                ativoMedio++;
                            }
                        }
                        else if (ensino.MOVIMENTACAO.LastOrDefault().COD_SITUACAO == (byte)Enumeradores.SituacaoAluno.CONCLUINTE)
                        {
                            if (ensino.COD_ENSINO == 1)
                            {
                                concluinteFundamental++;
                            }
                            else
                            {
                                concluinteMedio++;
                            }
                        }
                        else if (ensino.MOVIMENTACAO.LastOrDefault().COD_SITUACAO == (byte)Enumeradores.SituacaoAluno.NÃO_COMPARECIMENTO)
                        {
                            if (ensino.COD_ENSINO == 1)
                            {
                                naoComparecimentoFundamental++;
                            }
                            else
                            {
                                naoComparecimentoMedio++;
                            }
                        }
                        else if (ensino.MOVIMENTACAO.LastOrDefault().COD_SITUACAO == (byte)Enumeradores.SituacaoAluno.TRANSFERIDO)
                        {
                            if (ensino.COD_ENSINO == 1)
                            {
                                transferidoFundamental++;
                            }
                            else
                            {
                                transferidoMedio++;
                            }
                        }
                        else if (ensino.MOVIMENTACAO.LastOrDefault().COD_SITUACAO == (byte)Enumeradores.SituacaoAluno.FALECIDO)
                        {
                            if (ensino.COD_ENSINO == 1)
                            {
                                falecidoFundamental++;
                            }
                            else
                            {
                                falecidoMedio++;
                            }
                        }

                        comMovimentacao++;
                    }
                    else
                    {
                        if (ensino.ALUNO.ATIVO == true)
                        {
                            if (ensino.COD_ENSINO == 1)
                            {
                                ativoFundamental++;
                            }
                            else
                            {
                                ativoMedio++;
                            }
                        }
                        else if (ensino.ALUNO.ATIVO == false && ensino.ALUNO.CONCLUINTE == false)
                        {
                            if (ensino.COD_ENSINO == 1)
                            {
                                naoComparecimentoFundamental++;
                            }
                            else
                            {
                                naoComparecimentoMedio++;
                            }
                        }
                        else if (ensino.ALUNO.ATIVO == false && ensino.ALUNO.CONCLUINTE == true)
                        {
                            if (ensino.COD_ENSINO == 1)
                            {
                                concluinteFundamental++;
                            }
                            else
                            {
                                concluinteMedio++;
                            }
                        }

                        semMovimentacao++;
                    }
                }

                //Preenchimento Estado Atual do Sistema (Apenas no Load - Fixo)
                lblAtivoFundamental.Text = ativoFundamental.ToString();
                lblAtivoMedio.Text = ativoMedio.ToString();
                lblAtivoTotal.Text = (ativoFundamental + ativoMedio).ToString();

                lblConcluinteFundamental.Text = concluinteFundamental.ToString();
                lblConcluinteMedio.Text = concluinteMedio.ToString();
                lblConcluinteTotal.Text = (concluinteFundamental + concluinteMedio).ToString();

                lblNcomFundamental.Text = naoComparecimentoFundamental.ToString();
                lblNcomMedio.Text = naoComparecimentoMedio.ToString();
                lblNcomTotal.Text = (naoComparecimentoFundamental + naoComparecimentoMedio).ToString();

                lblTransferidoFundamental.Text = transferidoFundamental.ToString();
                lblTransferidoMedio.Text = transferidoMedio.ToString();
                lblTransferidoTotal.Text = (transferidoFundamental + transferidoMedio).ToString();

                lblFalecidoFundamental.Text = falecidoFundamental.ToString();
                lblFalecidoMedio.Text = falecidoMedio.ToString();
                lblFalecidoTotal.Text = (falecidoFundamental + falecidoMedio).ToString();

                int totalFundamental = ativoFundamental + concluinteFundamental + naoComparecimentoFundamental
                    + transferidoFundamental + falecidoFundamental;
                lblTotalFundamental.Text = totalFundamental.ToString();

                int totalMedio = ativoMedio + concluinteMedio + naoComparecimentoMedio
                    + transferidoMedio + falecidoMedio;
                lblTotalMedio.Text = totalMedio.ToString();

                lblTotal.Text = (totalFundamental + totalMedio).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int QuantidadeMatriculas(Enumeradores.Ensino ensino, DateTime dtInicial, DateTime? dtFinal = null)
        {
            try
            {
                int qtd = 0;

                var _ensinosAluno = _settings.Context.ENSINO_ALUNO;

                if (dtFinal == null)
                {
                    //Matrículas Por Dia
                    qtd = _ensinosAluno.Where(en =>
                        DbFunctions.TruncateTime(en.DT_INICIO) == DbFunctions.TruncateTime(dtInicial) &&
                        en.COD_ENSINO == (byte)ensino).Count();
                }
                else
                {
                    //Matrículas Por Período
                    qtd = _ensinosAluno.Where(en =>
                        DbFunctions.TruncateTime(en.DT_INICIO) >= DbFunctions.TruncateTime(dtInicial) &&
                        DbFunctions.TruncateTime(en.DT_INICIO) <= DbFunctions.TruncateTime(dtFinal) &&
                        en.COD_ENSINO == (byte)ensino).Count();
                }

                return qtd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int QuantidadeRematriculas(Enumeradores.Ensino ensino, DateTime dtInicial, DateTime? dtFinal = null)
        {
            try
            {
                int qtd = 0;

                var _rematriculas = _settings.Context.REMATRICULA.Include("ENSINO_ALUNO");

                if (dtFinal == null)
                {
                    //Rematrículas Por Dia
                    qtd = _rematriculas.Where(re =>
                        DbFunctions.TruncateTime(re.DT_REMATRICULA) == DbFunctions.TruncateTime(dtInicial) &&
                        re.ENSINO_ALUNO.COD_ENSINO == (byte)ensino).Count();
                }
                else
                {
                    //Rematrículas Por Período
                    qtd = _rematriculas.Where(re =>
                        DbFunctions.TruncateTime(re.DT_REMATRICULA) >= DbFunctions.TruncateTime(dtInicial) &&
                        DbFunctions.TruncateTime(re.DT_REMATRICULA) <= DbFunctions.TruncateTime(dtFinal) &&
                        re.ENSINO_ALUNO.COD_ENSINO == (byte)ensino).Count();
                }

                return qtd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int QuantidadeMovimentacoes(Enumeradores.SituacaoAluno situacaoAluno, Enumeradores.Ensino ensino, DateTime dtInicial, DateTime? dtFinal = null)
        {
            try
            {
                int qtd = 0;

                var _movimentacoes = _settings.Context.MOVIMENTACAO;

                if (dtFinal == null)
                {
                    qtd = _movimentacoes.Where(mo =>
                    DbFunctions.TruncateTime(mo.DT_MOVIMENTACAO) == DbFunctions.TruncateTime(dtInicial) &&
                    mo.ENSINO_ALUNO.COD_ENSINO == (byte)ensino && mo.COD_SITUACAO == (byte)situacaoAluno).Count();
                }
                else
                {
                    qtd = _movimentacoes.Where(mo =>
                    DbFunctions.TruncateTime(mo.DT_MOVIMENTACAO) >= DbFunctions.TruncateTime(dtInicial) &&
                    DbFunctions.TruncateTime(mo.DT_MOVIMENTACAO) <= DbFunctions.TruncateTime(dtFinal) &&
                    mo.ENSINO_ALUNO.COD_ENSINO == (byte)ensino && mo.COD_SITUACAO == (byte)situacaoAluno).Count();
                }

                return qtd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void filtrar()
        {
            //Cursor.Current = Cursors.WaitCursor;
            //#region Obter valor da data e hora dos dps
            //dtInicial = DateTime.Parse(dtpMatriculas_ini.Value.ToString("dd/MM/yyyy") + " 08:00:00");
            //dtFinal = DateTime.Parse(dtpMatriculas_fin.Value.ToString("dd/MM/yyyy") + " 22:00:00");
            //#endregion
            //#region Carregar os structs
            ////TODO: AGORA - VERIFICAR PERIODOS SE IRÃO DAR NAS MATRÍCULAS CERTO COM OS MÉTODOS DE DE STRUCT
            //matriculas = Utils.csDetalhesAluno.matriculas(dtInicial, dtFinal);
            //rematriculas = Utils.csDetalhesAluno.rematriculas(dtInicial, dtFinal);
            //historicoDeSituacoes = Utils.csDetalhesAluno.historicoDeSituacoes(dtInicial, dtFinal);
            //conclusoesDeEnsino = Utils.csDetalhesAluno.conclusoesDeEnsino(dtInicial, dtFinal);
            //#endregion            
            //#region Exibir na Tela
            //lblQtdMatriculas.Text = "Matrículas: " + matriculas.total;
            //lblQtdRematriculas.Text = "Rematrículas: " + rematriculas.total;
            //lblQtdCancelamentos.Text = "Cancelamentos: " + historicoDeSituacoes.cancelamento_total;
            //lblQtdNaoFrequentando.Text = "Não Frequentes: " + historicoDeSituacoes.n_frequetando_total;
            //lblQtdTransferencias.Text = "Transferências: " + historicoDeSituacoes.transferencia_total;
            //lblQtdConclusoesFundamental.Text = "Conclusões do Fundamental: " + conclusoesDeEnsino.fundamental;
            //lblQtdConclusoesMedio.Text = "Conclusões do Médio: " + conclusoesDeEnsino.medio;
            //#endregion
            //Cursor.Current = Cursors.Default;
        }

        private void imprimir()
        {
            ////Seleciona as opções de relatório
            //
            ////emiti o relatorio
            ////Relatorios.csRelatorios.gera_crystal_relatorio_secretaria();
            ////exibe na janela frCrystal
            //frCrystalReport form = new frCrystalReport();
        }
        #endregion

        #region Eventos
        private void dtpMatriculas_ini_ValueChanged(object sender, EventArgs e)
        {
            dtpPeriodoFinal.MinDate = dtpPeriodoInicial.Value;
        }

        private void dtpMatriculas_fin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            imprimir();
        }

        #endregion

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                lblTempo.Text = "Tempo: 0";
                Stopwatch sw = new Stopwatch();
                sw.Start();

                //Dinâmico
                Consulta();

                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                lblTempo.Text = "Tempo: " + String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void rdbPorDia_CheckedChanged(object sender, EventArgs e)
        {
            VerificarFiltroPor();
        }

        private void rdbPorPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            VerificarFiltroPor();
        }

        private void VerificarFiltroPor()
        {
            if (rdbPorPeriodo.Checked)
            {
                dtpPeriodoFinal.Enabled = true;
            }
            else
            {
                //TODO:: dtpPeriodoFinal.Value = DateTime.Now; Verificar o porque do erro entre min e max
                dtpPeriodoFinal.Enabled = false;
            }
        }
    }
}
