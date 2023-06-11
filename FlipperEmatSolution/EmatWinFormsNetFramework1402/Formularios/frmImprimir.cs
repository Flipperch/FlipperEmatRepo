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
    public partial class frmImprimir : Form
    {
        private readonly IEmatriculaSettings _settings;
        public Classes.Aluno objAluno;
        public string cidade;
        public string estaddo;
        public int ini_n_mat = 0;
        public int fim_n_mat = 0;

        Relatorios.Geral.csRelatorios cs_relatorios = new Relatorios.Geral.csRelatorios();

        public frmImprimir(IEmatriculaSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void frImprimir_Load(object sender, EventArgs e)
        {
            try
            {
                cmbOpcaoRelatorio.Items.Clear();
                cmbOpcaoRelatorio.DataSource = list_opcoes_impressao(_settings.Ceeja.ToLower());
                mtbDataRelatorio.Text = DateTime.Now.ToString();


                if (Classes.Aluno.GetEnsinoAlunoAtual(objAluno).Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                {
                    rdbFundamental.Checked = true;
                }
                else
                {
                    rdbMedio.Checked = true;

                }

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        public List<string> list_opcoes_impressao(string escola_)
        {

            List<string> list_ = new List<string>();

            if (escola_ == "americana")
            {
                #region Americana
                list_.Add("Declaração de Matrícula");
                list_.Add("Declaração de Comparecimento");
                list_.Add("Declaração de Conclusão");
                list_.Add("Declaração com Disciplinas Eliminadas");
                list_.Add("Requerimento de Matrícula");
                list_.Add("Etiquetas Passaporte");
                list_.Add("Carteirinha");
                #endregion

            }

            if (escola_ == "sorocaba")
            {
                #region Sorocaba    
                list_.Add("Requerimento de Matrícula");
                list_.Add("Declaração de Matrícula");
                list_.Add("Declaração de Conclusão");
                list_.Add("Etiquetas de Prontuário");
                list_.Add("RA (Carteirinha)");
                list_.Add("Declaração de Inss");
                list_.Add("Atestado de Eliminação");
                list_.Add("Declaração de Etec");

                #endregion
            }

            if (escola_ == "votorantim")
            {
                #region Votorantim
                list_.Add("Passaporte");
                list_.Add("Requerimento de Matrícula");
                list_.Add("RA (Carteirinha)");
                list_.Add("Declaração de Matrícula");
                list_.Add("Declaração de Conclusão");
                list_.Add("Declaração de Eliminação");
                list_.Add("Atestado de Eliminação");
                list_.Add("Declaração de Inss");
                //.Add("Declaração de Etec");
                #endregion
            }

            return list_;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string reportName = "";
            Cursor.Current = Cursors.WaitCursor;
            DateTime data_ = DateTime.Now;
            DateTime.TryParse(mtbDataRelatorio.Text, out data_);
            try
            {
                frCrystalReport form = new frCrystalReport();

                List<string> list_ = new List<string>();

                if (txtNmatInicial.Text != string.Empty)
                {
                    ini_n_mat = Convert.ToInt32(txtNmatInicial.Text);
                    fim_n_mat = Convert.ToInt32(txtNmatInicial.Text);
                    if (txtNmatFinal.Text != string.Empty)
                    {
                        fim_n_mat = Convert.ToInt32(txtNmatFinal.Text);
                    }
                    for (int i = ini_n_mat; i <= fim_n_mat; i++)
                    {
                        list_.Add(i.ToString());
                    }
                }
                else
                {
                    list_.Add(objAluno.NMatricula.ToString());
                }

                #region Americana
                /*
                if (Configurações.csEscola.cidade.ToLower() == "americana")
                {
                    switch (cmbOpcaoRelatorio.Text)
                    {
                        case "Declaração de Matrícula":
                            form.cr_report = cs_relatorios.gera_crystal_declaracao_matricula_americana(objAluno, data_);
                            break;
                        case "Declaração de Comparecimento":
                            form.cr_report = cs_relatorios.gera_crystal_declaracao_comparecimento_americana(objAluno,data_);
                            break;
                        case "Declaração de Conclusão":
                            form.cr_report = cs_relatorios.gera_crystal_declaracao_conclusao_americana(objAluno, data_);
                            break;
                        //TODO: FUTURO 1 - Implementar lista de médias para relatorio ou alterar relatorio
                        //case "Declaração com Disciplinas Eliminadas":                        
                        //    var lista_medias_concluidas = cs_notas.lista_medias_concluidas(objAluno.NMatricula.ToString();
                        //    form.cr_report = cs_relatorios.gera_crystal_declaracao_elimi_americana(objAluno,
                        //        lista_medias_concluidas,
                        //        Classes.Aluno.getEnsinoAlunoAtual(objAluno), data_));
                        //        
                        //    break;
                        case "Requerimento de Matrícula":
                            form.cr_report = cs_relatorios.gera_crystal_requerimento_americana(objAluno.NMatricula.ToString(),data_);
                            break;
                        case "Etiquetas Passaporte":
                            form.cr_report = cs_relatorios.gera_crystal_etiqueta_passaporte_americana(list_);
                            break;
                        case "Carteirinha":
                            form.cr_report = cs_relatorios.gera_crystal_cartao_americana(list_);
                            break;
                    }

                    foreach (Form fr in Application.OpenForms)
                    {
                        if (fr.Name == "frmEmatricula")
                        {
                            form.MdiParent = fr;
                        }
                    }
                    this.Close();
                    form.Show();                
                }
                */
                #endregion

                #region Sorocaba

                if (_settings.Ceeja.ToLower() == "sorocaba")
                {
                    DateTime dat_ini = DateTime.Now;
                    DateTime dat_fim = DateTime.Now;
                    if (DateTime.TryParse(mtbDatIni.Text + " 00:00:00", out data_))
                        dat_ini = data_;
                    if (DateTime.TryParse(mtbDatFim.Text + " 23:00:00", out data_))
                        dat_fim = data_;
                    switch (cmbOpcaoRelatorio.Text)
                    {
                        case "Atestado de Eliminação":
                            reportName = "Atestado de Eliminação";

                            form.cr_report = Relatorios.Escolas.Sorocaba.RelatoriosSorocaba.gera_crystal_atestado_eliminacao_sorocaba(objAluno, data_);
                            break;
                        case "RA (Carteirinha)":
                            reportName = "RA (Carteirinha)";

                            form.cr_report = Relatorios.Escolas.Sorocaba.RelatoriosSorocaba.gera_crystal_ra_sorocaba(objAluno);
                            break;
                        case "Declaração de Conclusão":
                            reportName = "Declaração de Conclusão";

                            foreach (Classes.EnsinoAluno ensinoAluno in objAluno.ListaEnsinoAluno)
                            {
                                if (rdbFundamental.Checked)
                                {
                                    if (ensinoAluno.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                                    {
                                        if (ensinoAluno.DtTermino != null && ensinoAluno.DtTermino != String.Empty)
                                        {
                                            form.cr_report = Relatorios.Escolas.Sorocaba.RelatoriosSorocaba.gera_crystal_declaracao_conclusao_sorocaba(objAluno, data_, ensinoAluno);
                                            break;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Aluno ainda não possui histórico de conclusão no ensino selecionado.", "Ematrícula - Impressão");
                                            form.cr_report = Relatorios.Escolas.Sorocaba.RelatoriosSorocaba.gera_crystal_declaracao_conclusao_sorocaba(objAluno, data_, ensinoAluno);
                                            break;
                                        }
                                    }

                                }
                                else
                                {
                                    if (ensinoAluno.Ensino == Enumeradores.Ensino.MÉDIO)
                                    {
                                        if (ensinoAluno.DtTermino != null && ensinoAluno.DtTermino != string.Empty)
                                        {
                                            form.cr_report = Relatorios.Escolas.Sorocaba.RelatoriosSorocaba.gera_crystal_declaracao_conclusao_sorocaba(objAluno, data_, ensinoAluno);
                                            break;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Aluno ainda não possui histórico de conclusão no ensino selecionado.", "Ematrícula - Impressão");
                                            form.cr_report = Relatorios.Escolas.Sorocaba.RelatoriosSorocaba.gera_crystal_declaracao_conclusao_sorocaba(objAluno, data_, ensinoAluno);
                                            break;
                                        }
                                    }

                                }
                            }


                            break;
                        case "Declaração de Etec":
                            reportName = "Declaração de Etec";
                            form.cr_report = Relatorios.Escolas.Sorocaba.RelatoriosSorocaba.gera_crystal_declaracao_etec_sorocaba(objAluno, data_);
                            break;
                        case "Declaração de Inss":
                            reportName = "Declaração de Inss";
                            form.cr_report = Relatorios.Escolas.Sorocaba.RelatoriosSorocaba.gera_crystal_declaracao_inss_sorocaba(objAluno, dat_ini, dat_fim, data_);
                            break;
                        case "Declaração de Matrícula":
                            reportName = "Declaração de Matrícula";
                            form.cr_report = Relatorios.Escolas.Sorocaba.RelatoriosSorocaba.gera_crystal_declaracao_matricula_sorocaba(objAluno, data_);
                            break;
                        case "Etiquetas de Prontuário":
                            reportName = "Etiquetas de Prontuário";
                            frImpressao_etiqueta form_ = new frImpressao_etiqueta(_settings);
                            form_.ShowDialog();
                            break;
                        case "Requerimento de Matrícula":
                            reportName = "Requerimento de Matrícula";
                            form.cr_report = Relatorios.Escolas.Sorocaba.RelatoriosSorocaba.rptRequerimentoMatricula(objAluno, data_, cidade, estaddo);
                            break;
                    }
                    foreach (Form fr in Application.OpenForms)
                    {
                        if (fr.Name == "frmEmatricula")
                        {
                            form.MdiParent = fr;
                        }
                    }

                    //TODO:: Verificar se esta salvando o log de impressão de relatórios no banco 
                    //Verificar necessidade desta funcionalidade e caso seja interessante, implementar classe para tal funcão e 
                    //melhorar implementação para todas as impressões em todas as escolas 
                    log4net.GlobalContext.Properties["n_mat"] = objAluno.NMatricula;
                    log4net.GlobalContext.Properties["report_name"] = reportName;
                    log4net.GlobalContext.Properties["id_user"] = Utils.csUsuarioLogado.usuario.Codigo;
                    log4net.GlobalContext.Properties["parameters"] = "";
                    log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    log.Info("Report:");

                    this.Close();
                    form.Show();
                }
                #endregion

                #region Votorantim
                if (_settings.Ceeja.ToLower() == "votorantim")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    DateTime dat_ini = DateTime.Now;
                    DateTime dat_fim = DateTime.Now;
                    if (DateTime.TryParse(mtbDatIni.Text + " 00:00:00", out data_))
                        dat_ini = data_;
                    if (DateTime.TryParse(mtbDatFim.Text + " 23:00:00", out data_))
                        dat_fim = data_;
                    //csAlunos obj_aluno = new csAlunos(n_mat);
                    string teste = cmbOpcaoRelatorio.Text;
                    switch (cmbOpcaoRelatorio.Text)
                    {
                        case "Atestado de Eliminação":

                            Enumeradores.Ensino ensino = Enumeradores.Ensino.FUNDAMENTAL;
                            bool encontrado = true;

                            if (rdbMedio.Checked)
                                ensino = Enumeradores.Ensino.MÉDIO;

                            foreach (var ensinoAluno in objAluno.ListaEnsinoAluno)
                            {
                                if (ensinoAluno.Ensino == ensino)
                                {
                                    form.cr_report =
                                        Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptAtestadoEliminacao(
                                            objAluno, ensinoAluno, DataRelatorio());
                                    break;
                                }
                            }

                            if (!encontrado)
                                MessageBox.Show("Aluno não possui eliminações no ensino atual.", "Ematrícula - Impressão");

                            break;                            
                        case "Declaração de Conclusão":

                            foreach (Classes.EnsinoAluno ensinoAluno in objAluno.ListaEnsinoAluno)
                            {
                                if (rdbFundamental.Checked)
                                {
                                    if (ensinoAluno.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                                    {
                                        if (ensinoAluno.DtTermino != null && ensinoAluno.DtTermino != String.Empty)
                                        {
                                            form.cr_report = Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptDeclaracaoConclusao(objAluno, ensinoAluno, DataRelatorio());
                                            break;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Aluno ainda não possui histórico de conclusão no ensino selecionado.", "Ematrícula - Impressão");
                                            form.cr_report = Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptDeclaracaoConclusao(objAluno, ensinoAluno, DataRelatorio());
                                            break;
                                        }
                                    }

                                }
                                else
                                {
                                    if (ensinoAluno.Ensino == Enumeradores.Ensino.MÉDIO)
                                    {
                                        if (ensinoAluno.DtTermino != null && ensinoAluno.DtTermino != String.Empty)
                                        {
                                            form.cr_report = Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptDeclaracaoConclusao(objAluno, ensinoAluno, DataRelatorio());
                                            break;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Aluno ainda não possui histórico de conclusão no ensino selecionado.", "Ematrícula - Impressão");
                                            form.cr_report = Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptDeclaracaoConclusao(objAluno, ensinoAluno, DataRelatorio());
                                            break;
                                        }
                                    }
                                }
                            }

                            break;
                        case "Declaração de Eliminação":
                            if (Classes.Aluno.GetEnsinoAlunoAtual(objAluno).ListaDisciplinaAluno.Count(x => x.Concluida) > 0)
                            {
                                form.cr_report = Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptDeclaracaoEliminacao(objAluno, DataRelatorio());

                            }
                            else
                            {
                                MessageBox.Show("Aluno não possui eliminações no ensino atual.", "Ematrícula - Impressão");
                                return;
                            }
                            break;
                        case "Declaração de Inss":
                            form.cr_report = Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptDeclaracaoInss(objAluno);
                            break;
                        case "Declaração de Matrícula":
                            form.cr_report = Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptDeclaracaoMatricula(objAluno);
                            break;
                        case "Passaporte":
                            form.cr_report = Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptPassaporte(objAluno);
                            break;
                        case "Requerimento de Matrícula":
                            form.cr_report = Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptRequerimentoMatricula(objAluno, _settings);
                            break;
                        case "RA (Carteirinha)":
                            MessageBox.Show("CARTEIRINHA");
                            //Utils.geraBarcode.SalvarBarcode(objAluno.NMatricula);
                            List<Classes.Aluno> alunos = new List<Classes.Aluno>() {objAluno};
                            form.cr_report = Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptRaVotorantimLote(alunos, _settings);

                            break;
                    }

                    foreach (Form fr in Application.OpenForms)
                    {
                        if (fr.Name == "frmEmatricula")
                        {
                            form.MdiParent = fr;
                        }
                    }
                    form.Show();
                    this.Close();
                    Cursor.Current = Cursors.Default;
                }
                #endregion

                ini_n_mat = 0;
                fim_n_mat = 0;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void cmbOpcaoRelatorio_SelectedIndexChanged(object sender, EventArgs e)
        {

            #region Americana
            /*
            if(Configurações.csEscola.cidade.ToLower() == "americana")
            {
                if (cmbOpcaoRelatorio.Text == "Etiquetas Passaporte" || cmbOpcaoRelatorio.Text == "Carteirinha")
                {
                    txtNmatInicial.Enabled = true;
                    txtNmatFinal.Enabled = true;
                }
                else
                {
                    txtNmatInicial.Enabled = false;
                    txtNmatFinal.Enabled = false;
                    txtNmatInicial.Text = string.Empty;
                    txtNmatFinal.Text = string.Empty;
                }
            }
            */
            #endregion

            #region Sorocaba
            if (_settings.Ceeja.ToLower() == "sorocaba")
            {
                mtbDatIni.Text = string.Empty;
                mtbDatFim.Text = string.Empty;

                if (cmbOpcaoRelatorio.Text == "Declaração de Inss")
                {
                    mtbDatIni.Enabled = true;
                    mtbDatFim.Enabled = true;

                    Classes.EnsinoAluno ensinoAluno = Classes.Aluno.GetEnsinoAlunoAtual(objAluno);
                    List<Classes.DisciplinaAluno> ListaDisciplinaAlunosensinoAluno = ensinoAluno.ListaDisciplinaAluno;
                    List<Classes.AtendimentoAluno> ListaAtendimentosAluno = ListaDisciplinaAlunosensinoAluno.SelectMany(x => x.ListaAtendimentoAluno).ToList();

                    ListaAtendimentosAluno = ListaAtendimentosAluno.OrderBy(x => x.DtDoAtendimento).ToList();

                    if (ListaAtendimentosAluno.Count > 0)
                    {
                        mtbDatIni.Text = ListaAtendimentosAluno[0].DtDoAtendimento.ToString("dd/MM/yyyy");
                        mtbDatFim.Text = ListaAtendimentosAluno[ListaAtendimentosAluno.Count - 1].DtDoAtendimento.ToString("dd/MM/yyyy");
                    }

                }
                else
                {
                    mtbDatIni.Enabled = false;
                    mtbDatFim.Enabled = false;
                }
            }

            #endregion

            #region
            if (_settings.Ceeja.ToLower() == "votorantim")
            {
                if (cmbOpcaoRelatorio.Text == "Passaporte" ||
                    cmbOpcaoRelatorio.Text == "Requerimento de Matrícula")
                {
                    grbIntervaloNMatricula.Enabled = false;
                    grbEnsino.Enabled = false;
                    grbParametrosData.Enabled = false;
                }
                else
                {
                    grbIntervaloNMatricula.Enabled = true;
                    grbEnsino.Enabled = true;
                    grbParametrosData.Enabled = true;
                }
            }
            #endregion
        }

        private DateTime DataRelatorio()
        {
            if (DateTime.TryParse(mtbDataRelatorio.Text, out DateTime data))
            {
                return data;
            }
            else
            {
                return DateTime.Now;
            }
        }
    }
}
