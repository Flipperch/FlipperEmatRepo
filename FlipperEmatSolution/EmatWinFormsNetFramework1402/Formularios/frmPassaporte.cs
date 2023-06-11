using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using EmatWinFormsNetFramework1402.Properties;
using System.Threading;
using EmatWinFormsNetFramework1402.Classes;
using EmatWinFormsNetFramework1402.DAO;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.Formularios
{
    //TODO:: Adicionar na grade a média
    public partial class frmPassaporte : Form
    {
        private readonly IEmatriculaSettings _settings;
        public Classes.Aluno objAluno;
        Controles.dgvAtendimentosAluno dgvSelecionado;
        string valor_dgv_anterior = "";
        object sender_mouse_right_dgv;
        EnsinoAluno ensinoSelecionado;
        DisciplinaAluno disciplinaAlunoSelecionado;
        bool BoolAtribuirNovaDisciplina = false;
        bool BoolTravarCampo = false;
        #region Cores Design
        //Sorocaba
        Color corSorFund = Color.FromArgb(48, 48, 48); //FUNDAMENTAL 
        Color corSorMed = Color.FromArgb(19, 91, 133);//MÉDIO
        //Americana        
        Color corAmeFund = Color.LightGray; //FUNDAMENTAL 
        Color corAmeMed = Color.FromArgb(19, 91, 133); //MÉDIO
        #endregion

        public frmPassaporte(IEmatriculaSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void FRpassaporte_Load(object sender, EventArgs e)
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                //Desligar Eventos
                flpFunCom.Resize -= flp_Resize;
                flpMedCom.Resize -= flp_Resize;
                flpFunInd.Resize -= flp_Resize;
                flpFunInd.Resize -= flp_Resize;
                tbcHistorico.SelectedIndexChanged -= tbcHistorico_SelectedIndexChanged;

                for (int i = 0; i < EnsinoSistemaEstatica.getListaEnsinosSistema.Count; i++)
                {
                    if (EnsinoSistemaEstatica.getListaEnsinosSistema[i].Ensino == Classes.Aluno.GetEnsinoAlunoAtual(objAluno).Ensino)
                    {
                        for (int z = 0; z < EnsinoSistemaEstatica.getListaEnsinosSistema[i].ListaDisciplinas.Count; z++)
                        {
                            if (Classes.Aluno.GetEnsinoAlunoAtual(objAluno).ListaDisciplinaAluno.FindIndex(x => x.Disciplina.Codigo ==
                                 EnsinoSistemaEstatica.getListaEnsinosSistema[i].ListaDisciplinas[z].Codigo) < 0)
                            {
                                //Não encontrou, deve adicionar uma nova disciplina aluno....
                                Classes.DisciplinaAluno disciplinaAluno = new Classes.DisciplinaAluno();
                                disciplinaAluno.Codigo = 0;
                                disciplinaAluno.Atual = false;
                                disciplinaAluno.Concluida = false;
                                disciplinaAluno.Disciplina = EnsinoSistemaEstatica.getListaEnsinosSistema[i].ListaDisciplinas[z];
                                disciplinaAluno.EnsinoAluno = Classes.Aluno.GetEnsinoAlunoAtual(objAluno);
                                Classes.Aluno.GetEnsinoAlunoAtual(objAluno).ListaDisciplinaAluno.Add(disciplinaAluno);
                                DAO.DisciplinaAlunoDAO.Inserir(disciplinaAluno);
                            }
                        }
                    }
                }

                //Subprocesso
                PreencherDadosAluno();
                //Subprocesso
                ConigurarVisualPassaporte();
                //Subprocesso
                ConstruirGradeMedias();
                //Professor?            
                if (Utils.csUsuarioLogado.professor != null)
                {
                    //TODO: AGORA - Fazer tratamento para não existir professor sem disciplina
                    //Seleciona Abas Passaporte Individual
                    tbcPassaporteFundamental.SelectedIndex = 0;
                    tbcPassaporteMedio.SelectedIndex = 0;

                    //Construir Tabelas - Individual
                    ConstruirTabelasIndividual();
                }
                else
                {
                    //Seleciona Aba Passporte Completo 
                    tbcPassaporteFundamental.SelectedIndex = 1;
                    tbcPassaporteMedio.SelectedIndex = 1;
                }

                //Construir Tabelas - Completo 
                ConstruirTabelasCompleto();

                flpFunCom.Resize += flp_Resize;
                flpMedCom.Resize += flp_Resize;
                flpFunInd.Resize += flp_Resize;
                flpFunInd.Resize += flp_Resize;

                tbcHistorico.SelectedIndexChanged += tbcHistorico_SelectedIndexChanged;

                watch.Stop();
                float tempo = (float)watch.ElapsedMilliseconds;
                tempo = tempo / 1000;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void frPassaporte_Shown(object sender, EventArgs e)
        {
            try
            {
                verificaAlunoAtivo();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void verificaAlunoAtivo()
        {
            try
            {
                if (!objAluno.Ativo)
                {
                    MessageBox.Show("Aluno Inativo, Comparecer a Secretaria.", "Passaporte do Aluno: Aluno Inativo");

                    foreach (TableLayoutPanel tlp in flpFunInd.Controls.OfType<TableLayoutPanel>())
                    {
                        foreach (Controles.dgvAtendimentosAluno dgv in tlp.Controls.OfType<Controles.dgvAtendimentosAluno>())
                        {
                            dgv.Enabled = false;
                        }
                    }

                    foreach (TableLayoutPanel tlp in flpMedInd.Controls.OfType<TableLayoutPanel>())
                    {
                        foreach (Controles.dgvAtendimentosAluno dgv in tlp.Controls.OfType<Controles.dgvAtendimentosAluno>())
                        {
                            dgv.Enabled = false;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void PreencherDadosAluno()
        {
            try
            {
                //Preencher Nome
                if (objAluno.NomeSocial != string.Empty)
                {
                    lblNome.Text = "NOME: " + objAluno.NomeSocial + " (" + primeiro_nome(objAluno.Nome) + ")";
                }
                else
                {
                    lblNome.Text = "NOME: " + objAluno.Nome;
                }
                //Preencher RG                    
                lblRg.Text = "RG: " + objAluno.Rg;
                //Preencher Termo //TODO:: Definir como ficará o TERMO NO PASSAPORTE para as 3 cidades diferentes
                if (_settings.Ceeja.ToLower() == "sorocaba")
                {
                    lblTermo.Text = "TERMO: " + objAluno.TermoMatricula;
                }
                else if (_settings.Ceeja.ToLower() == "americana")
                {
                    lblTermo.Text = "MUNICÍPIO: " + objAluno.Endereco.Cidade.Nome;
                }
                else if (_settings.Ceeja.ToLower() == "votorantim")
                {
                    lblTermo.Text = "ANO: " + objAluno.TermoMatricula;
                }
                //Preencher Endereço Cidade
                //lblTermo.Text = "Município: " + objAluno.Endereco.Cidade.Nome;            
                //Preencher Nº de Matrícula
                lblNmat.Text = "MATRÍCULA: " + objAluno.NMatricula;
                //Preencher Nome da Tela
                this.Text = objAluno.NMatricula + " - PASSAPORTE ALUNO";
                //Preencher Observação
                txtObs.Text = objAluno.Observacao;
                //Preencher Nome da Disciplina Atual
                lblDisciplinaAtual.Text = "DISC. ATUAL: " + Aluno.GetDisciplinaAlunoAtual(objAluno).Disciplina.Nome;
                //Preencher Data de Matrícula
                lblDatMat.Text = "DATA DE MATRÍCULA: " + objAluno.DtMatricula.ToString("dd/MM/yyyy");
                //Selecionar Ensino Atual
                ensinoSelecionado = Aluno.GetEnsinoAlunoAtual(objAluno);
                if (ensinoSelecionado.AtividadeExtra != null)
                {
                    ckbAtvExtra.Checked = true;
                    lblDataAtvExtra.Text = ensinoSelecionado.AtividadeExtra.Data.ToString("dd/MM/yyyy");
                }
                else
                {
                    ckbAtvExtra.Checked = false;
                    lblDataAtvExtra.Text = string.Empty;
                }
                //Carregar Foto
                if (_settings.Ceeja.ToLower() == "sorocaba" ||
                    _settings.Ceeja.ToLower() == "votorantim")
                {
                    //Foto
                    picFoto.Image = objAluno.FotoDoAluno.Foto;

                    //Atv Extra - Verificar
                    verificaAtvExtra();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        public string primeiro_nome(string nome_completo)
        {
            try
            {
                string a = nome_completo.Replace("NOME: ", "");
                a = a.Remove(a.IndexOf(" "));
                return a;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtObs_Leave(object sender, EventArgs e)
        {
            try
            {
                objAluno.Observacao = txtObs.Text;
                DAO.AlunoDAO.Gravar(objAluno);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void verificaAtvExtra()
        {
            try
            {
                if (ensinoSelecionado.AtividadeExtra != null)
                {
                    ckbAtvExtra.Checked = true;
                    lblDataAtvExtra.Text = ensinoSelecionado.AtividadeExtra.Data.ToString("dd/MM/yyyy");
                }
                else
                {
                    ckbAtvExtra.Checked = false;
                    lblDataAtvExtra.Text = "";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ConigurarVisualPassaporte()
        {
            try
            {
                if (ensinoSelecionado.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    tbcHistorico.SelectedIndex = 0;
                else tbcHistorico.SelectedIndex = 1;

                if (_settings.Ceeja.ToLower() == "votorantim")
                {
                    tabFundamental.BackColor = corSorFund;
                    tabMedio.BackColor = corSorMed;
                    if (ensinoSelecionado.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    {
                        pnlForm.BackColor = corSorFund;
                        //pnlDadosAluno.BackColor = corSorFund;
                    }
                    else if (ensinoSelecionado.Ensino == Enumeradores.Ensino.MÉDIO)
                    {
                        pnlForm.BackColor = corSorMed;
                        //pnlDadosAluno.BackColor = corSorMed;
                    }
                    pAtvExtra.Visible = true;
                }
                else if (_settings.Ceeja.ToLower() == "sorocaba")
                {
                    tabFundamental.BackColor = corSorFund;
                    tabMedio.BackColor = corSorMed;
                    if (ensinoSelecionado.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    {
                        pnlForm.BackColor = corSorFund;
                        //pnlDadosAluno.BackColor = corSorFund;
                    }
                    else if (ensinoSelecionado.Ensino == Enumeradores.Ensino.MÉDIO)
                    {
                        pnlForm.BackColor = corSorMed;
                        //pnlDadosAluno.BackColor = corSorMed;
                    }
                    pAtvExtra.Visible = true;
                }
                else if (_settings.Ceeja.ToLower() == "americana")
                {
                    tabFundamental.BackColor = corAmeFund;
                    tabMedio.BackColor = corAmeMed;
                    if (ensinoSelecionado.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    {
                        pnlForm.BackColor = corAmeFund;
                        //pnlDadosAluno.BackColor = corAmeFund;
                    }
                    else if (ensinoSelecionado.Ensino == Enumeradores.Ensino.MÉDIO)
                    {
                        pnlForm.BackColor = corAmeMed;
                        //pnlDadosAluno.BackColor = corAmeMed;
                    }
                    pAtvExtra.Visible = false;

                    foreach (Label lbl in pnlDadosAluno.Controls.OfType<Label>())
                    {
                        lblTitulo.ForeColor = Color.Black;
                        lbl.ForeColor = Color.Black;
                    }
                }

                lblTitulo.Text = "PASSAPORTE ENSINO " + ensinoSelecionado.Ensino;
            }
            catch (Exception)
            {
                throw;
            }


        } //Apenas no LOAD

        private void alterar_visual_passaporte()
        {
            try
            {
                if (_settings.Ceeja.ToLower() == "votorantim")
                {
                    if (ensinoSelecionado.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    {
                        pnlForm.BackColor = corSorFund;
                    }
                    else if (ensinoSelecionado.Ensino == Enumeradores.Ensino.MÉDIO)
                    {
                        pnlForm.BackColor = corSorMed;
                    }
                    pAtvExtra.Visible = true;
                }
                else if (_settings.Ceeja.ToLower() == "sorocaba")
                {
                    if (ensinoSelecionado.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    {
                        pnlForm.BackColor = corSorFund;
                        //pnlDadosAluno.BackColor = corSorFund;             
                    }
                    else if (ensinoSelecionado.Ensino == Enumeradores.Ensino.MÉDIO)
                    {
                        pnlForm.BackColor = corSorMed;
                        //pnlDadosAluno.BackColor = corSorMed;
                    }
                    pAtvExtra.Visible = true;
                }
                else if (_settings.Ceeja.ToLower() == "americana")
                {
                    pAtvExtra.Visible = false;

                    if (ensinoSelecionado.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    {
                        pnlForm.BackColor = corAmeFund;
                        //pnlDadosAluno.BackColor = corAmeFund;
                    }
                    else if (ensinoSelecionado.Ensino == Enumeradores.Ensino.MÉDIO)
                    {
                        pnlForm.BackColor = corAmeMed;
                        //pnlDadosAluno.BackColor = corAmeMed;
                    }

                    foreach (Label lbl in pnlDadosAluno.Controls.OfType<Label>())
                    {
                        lblTitulo.ForeColor = Color.Black;
                        lbl.ForeColor = Color.Black;
                    }
                }
                lblTitulo.Text = "PASSAPORTE ENSINO " + ensinoSelecionado.Ensino;
            }
            catch (Exception)
            {
                throw;
            }


        } //Durante a mudança de Aba

        private void pnlDadosAluno_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Graphics gObject = pnlDadosAluno.CreateGraphics();

                Brush white = new SolidBrush(Color.White);
                Pen redPen = new Pen(white, 8);

                gObject.FillRectangle(white, 0, 0, 250, 19); //n-mat
                gObject.FillRectangle(white, 260, 0, 205, 19); //rg
                gObject.FillRectangle(white, 475, 0, 376, 19); //termo

                gObject.FillRectangle(white, 0, 31, 877, 100);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ConstruirGradeMedias()
        {
            try
            {
                foreach (EnsinoAluno ensinoAluno in objAluno.ListaEnsinoAluno)
                {
                    foreach (DisciplinaAluno disciplinaAluno in ensinoAluno.ListaDisciplinaAluno)
                    {
                        if (disciplinaAluno.Disciplina.Nome == "EDUCAÇÃO FÍSICA")
                            break;

                        MyPanel PanelGrade_ = new MyPanel();
                        PanelGrade_.Name = "PanelGrade_" + disciplinaAluno.Disciplina.Nome;
                        PanelGrade_.Size = new Size(87, 60);
                        //PanelGrade_.BorderStyle = BorderStyle.FixedSingle;
                        PanelGrade_.Margin = new Padding(1);
                        //PanelGrade_.Click += panel_Click;
                        //use the code

                        //Label Nome
                        Label lbl_nome_ = new Label();
                        lbl_nome_.Name = lbl_nome_ + disciplinaAluno.Disciplina.Nome;
                        lbl_nome_.Text = disciplinaAluno.Disciplina.Nome; ;
                        lbl_nome_.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                        lbl_nome_.TextAlign = ContentAlignment.MiddleCenter;
                        lbl_nome_.Size = new Size(85, 19);
                        lbl_nome_.Location = new Point(1, 1);

                        ////CheckBox
                        //CheckBox ckb_ = new CheckBox();
                        //ckb_.Name = ckb_ + disciplinaAluno.Disciplina.Nome;
                        //ckb_.Text = "";
                        //ckb_.Location = new Point(5, 21);
                        //ckb_.Checked = false;
                        //ckb_.AutoSize = true;
                        //ckb_.Enabled = false;
                        //ckb_.ForeColor = SystemColors.ControlText;

                        //Label Media
                        Label lbl_media_ = new Label();
                        lbl_media_.Name = "lbl_media_" + disciplinaAluno.Disciplina.Nome;
                        lbl_media_.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                        lbl_media_.TextAlign = ContentAlignment.MiddleCenter;
                        lbl_media_.Size = new Size(85, 19);
                        lbl_media_.Location = new Point(1, 18);
                        //lbl_media_.BackColor = Color.Red;

                        //Label Data
                        Label lbl_data_ = new Label();
                        lbl_data_.Name = "lbl_data_" + disciplinaAluno.Disciplina.Nome;
                        lbl_data_.Font = new Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                        lbl_data_.TextAlign = ContentAlignment.MiddleCenter;
                        lbl_data_.Size = new Size(85, 19);
                        lbl_data_.Location = new Point(1, 35);
                        //lbl_data_.BackColor = Color.Red;

                        //GRADE DE ENCERRADOS
                        if (disciplinaAluno.Concluida) //Testa se está concluida (mesmo que demarcado eliminado) para marcar o check
                        {
                            if (disciplinaAluno.Media != null) //Testa se possui média para exibir o nome da Instituição e data da média
                            {
                                lbl_data_.Text = DateTime.Parse(disciplinaAluno.Media.DtMedia).ToString("dd/MM/yyyy");
                                lbl_media_.Text = disciplinaAluno.Media.Valor;
                                ToolTip ttp_ = new ToolTip();
                                ttp_.SetToolTip(PanelGrade_, disciplinaAluno.Media.Instituicao);
                                ttp_.SetToolTip(lbl_media_, disciplinaAluno.Media.Instituicao);
                                ttp_.SetToolTip(lbl_data_, disciplinaAluno.Media.Instituicao);
                                ttp_.SetToolTip(lbl_nome_, disciplinaAluno.Media.Instituicao);
                                //ttp_.SetToolTip(ckb_, disciplinaAluno.Media.Instituicao);
                            }
                            //ckb_.Checked = true;
                        }
                        PanelGrade_.Controls.Add(lbl_nome_);
                        PanelGrade_.Controls.Add(lbl_media_);
                        PanelGrade_.Controls.Add(lbl_data_);
                        //PanelGrade_.Controls.Add(ckb_);

                        if (ensinoAluno.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                        {
                            flpGradeFundamental.Controls.Add(PanelGrade_);
                        }
                        else if (ensinoAluno.Ensino == Enumeradores.Ensino.MÉDIO)
                        {
                            flpGradeMedio.Controls.Add(PanelGrade_);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AtualizarMediaGrade()
        {
            try
            {
                flpGradeFundamental.Controls.Clear();
                flpGradeMedio.Controls.Clear();
                ConstruirGradeMedias();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ConstruirTabelasIndividual()
        {
            try
            {
                //Percorre a Lista de EnsinoAluno do Aluno
                foreach (EnsinoAluno ensinoAluno in objAluno.ListaEnsinoAluno)
                {
                    //Percorre a Lista de DisciplinaAluno do EnsinoAluno
                    foreach (DisciplinaAluno disciplinaAluno in ensinoAluno.ListaDisciplinaAluno)
                    {
                        //Se disciplinaAluno.Disciplina == professor.Disciplina
                        if (disciplinaAluno.Disciplina.Codigo == Utils.csUsuarioLogado.professor.Disciplina.Codigo)
                        {
                            //Construir tlpDisciplinaAluno
                            Controles.tlpDisciplinaAluno tlpDisciplinaAluno = new Controles.tlpDisciplinaAluno(disciplinaAluno, _settings);

                            //Incluir ContextMenuStrip no DGV
                            tlpDisciplinaAluno.dgvAtendimentosAluno.ContextMenuStrip = cmsOpcoes_dgv;

                            #region Data Orientação Inicial
                            foreach (DateTimePicker dtp in tlpDisciplinaAluno.flpOrientacaoInicial.Controls.OfType<DateTimePicker>())
                            {
                                dtp.Leave += dtp_orient_ini_Leave;
                            }
                            //dtp_disciplina.ValueChanged += dtp_orient_ini_ValueChanged;
                            #endregion

                            //Adicionar tlpDisciplinaAluno no flp
                            if (ensinoAluno.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                                //Adicionar no flpFunInd
                                flpFunInd.Controls.Add(tlpDisciplinaAluno);
                            else
                                //Adicionar no flpMedInd
                                flpMedInd.Controls.Add(tlpDisciplinaAluno);

                            // Adicionar Eventos
                            tlpDisciplinaAluno.dgvAtendimentosAluno.CellMouseClick += dgvAtendimentos_dis_area_CellMouseClick;
                            tlpDisciplinaAluno.dgvAtendimentosAluno.CellMouseDown += dgvAtendimentos_dis_area_CellMouseDown;
                            tlpDisciplinaAluno.dgvAtendimentosAluno.EditingControlShowing += dgvAtendimentos_dis_area_EditingControlShowing;
                            tlpDisciplinaAluno.dgvAtendimentosAluno.CellMouseEnter += dgvAtendimentos_dis_area_CellMouseEnter;
                            tlpDisciplinaAluno.dgvAtendimentosAluno.CellMouseLeave += dgvAtendimentos_dis_area_CellMouseLeave;
                            tlpDisciplinaAluno.dgvAtendimentosAluno.CellEnter += dgvAtendimentos_dis_area_CellEnter;
                            tlpDisciplinaAluno.dgvAtendimentosAluno.CellEndEdit += dgvAtendimentos_dis_area_CellEndEdit;
                        }
                        else if (Utils.csUsuarioLogado.professor.Area != null)
                        {
                            if (Utils.csUsuarioLogado.professor.Area.ListaDisciplina.Exists(x => x.Codigo == disciplinaAluno.Disciplina.Codigo))
                            {
                                //Construir tlpDisciplinaAluno
                                Controles.tlpDisciplinaAluno tlpDisciplinaAluno = new Controles.tlpDisciplinaAluno(disciplinaAluno, _settings);

                                //Incluir ContextMenuStrip no DGV
                                tlpDisciplinaAluno.dgvAtendimentosAluno.ContextMenuStrip = cmsOpcoes_dgv;

                                #region Data Orientação Inicial
                                foreach (DateTimePicker dtp in tlpDisciplinaAluno.flpOrientacaoInicial.Controls.OfType<DateTimePicker>())
                                {
                                    dtp.Leave += dtp_orient_ini_Leave;
                                }
                                //dtp_disciplina.ValueChanged += dtp_orient_ini_ValueChanged;
                                #endregion

                                //Adicionar tlpDisciplinaAluno no flp
                                if (ensinoAluno.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                                    //Adicionar no flpFunInd
                                    flpFunInd.Controls.Add(tlpDisciplinaAluno);
                                else
                                    //Adicionar no flpMedInd
                                    flpMedInd.Controls.Add(tlpDisciplinaAluno);

                                // Adicionar Eventos
                                tlpDisciplinaAluno.dgvAtendimentosAluno.CellMouseClick += dgvAtendimentos_dis_area_CellMouseClick;
                                tlpDisciplinaAluno.dgvAtendimentosAluno.CellMouseDown += dgvAtendimentos_dis_area_CellMouseDown;
                                tlpDisciplinaAluno.dgvAtendimentosAluno.EditingControlShowing += dgvAtendimentos_dis_area_EditingControlShowing;
                                tlpDisciplinaAluno.dgvAtendimentosAluno.CellMouseEnter += dgvAtendimentos_dis_area_CellMouseEnter;
                                tlpDisciplinaAluno.dgvAtendimentosAluno.CellMouseLeave += dgvAtendimentos_dis_area_CellMouseLeave;
                                tlpDisciplinaAluno.dgvAtendimentosAluno.CellEnter += dgvAtendimentos_dis_area_CellEnter;
                                tlpDisciplinaAluno.dgvAtendimentosAluno.CellEndEdit += dgvAtendimentos_dis_area_CellEndEdit;
                            }
                        }
                    }
                }
                resize_flp_(flpFunInd);
                resize_flp_(flpMedInd);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ConstruirTabelasCompleto()
        {
            try
            {
                //Percorre cada ensinoAluno da objAluno.ListaEnsinoAluno
                foreach (EnsinoAluno ensinoAluno in objAluno.ListaEnsinoAluno)
                {
                    #region Definir qual flp será populado
                    FlowLayoutPanel flp_;
                    if (ensinoAluno.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                        flp_ = flpFunCom;
                    else
                    {
                        flp_ = flpMedCom;
                    }
                    #endregion

                    //Percorre cada disciplinaAluno da ensinoAluno.ListaDisciplinaAluno
                    foreach (DisciplinaAluno disciplinaAluno in ensinoAluno.ListaDisciplinaAluno)
                    {
                        //Construir DATAGRIDVIEW
                        Controles.tlpDisciplinaAluno tlpPainelDisciplinaAluno
                            = new Controles.tlpDisciplinaAluno(disciplinaAluno, _settings);

                        #region Configurar DataGridView
                        tlpPainelDisciplinaAluno.dgvAtendimentosAluno.ReadOnly = true;
                        #endregion

                        #region Travar DatetimePicker
                        foreach (FlowLayoutPanel flp in tlpPainelDisciplinaAluno.Controls.OfType<FlowLayoutPanel>())
                        {
                            foreach (DateTimePicker dtp in tlpPainelDisciplinaAluno.flpOrientacaoInicial.Controls.OfType<DateTimePicker>())
                            {
                                dtp.Enabled = false;
                            }
                        }
                        #endregion

                        #region Adicionar Eventos
                        foreach (DataGridView dgv in tlpPainelDisciplinaAluno.Controls.OfType<DataGridView>())
                        {
                            dgv.EditingControlShowing += dgvAtendimentos_dis_area_EditingControlShowing;
                            dgv.CellMouseEnter += dgvAtendimentos_dis_area_CellMouseEnter;
                            dgv.CellMouseLeave += dgvAtendimentos_dis_area_CellMouseLeave;
                            dgv.CellEnter += dgvAtendimentos_dis_area_CellEnter;
                        }
                        #endregion

                        #region Configuração de quem pode excluir os atendimentos...

                        foreach (DataGridView dgv in tlpPainelDisciplinaAluno.Controls.OfType<DataGridView>())
                        {
                            if (Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.ADMINISTRADOR ||
                            Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.SECRETARIOADM ||
                            Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.COORDENADOR ||
                            Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.DIRETOR)
                            {
                                dgv.Enabled = true;
                                dgv.CellMouseDown += dgvAtendimentos_dis_area_CellMouseDown;
                                dgv.ContextMenuStrip = cmsOpcoes_dgv;
                            }
                        }
                        #endregion
                        flp_.Controls.Add(tlpPainelDisciplinaAluno);
                    }
                }
                resize_flp_(flpFunCom);
                resize_flp_(flpMedCom);
            }
            catch (Exception)
            {
                throw;
            }

        }

        int DropDownWidth(DataGridViewComboBoxCell myCombo)
        {
            try
            {
                int maxWidth = 0;
                int temp = 0;
                Label label1 = new Label();
                label1.AutoSize = true;
                foreach (var obj in myCombo.Items)
                {
                    label1.Text = obj.ToString();
                    temp = label1.PreferredWidth;
                    if (temp > maxWidth)
                    {
                        maxWidth = temp;
                    }
                }
                label1.Dispose();
                return maxWidth;
            }
            catch (Exception)
            {
                throw;
            }

        }
        private void dgvAtendimentos_dis_area_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Controles.dgvAtendimentosAluno dgv_ = (Controles.dgvAtendimentosAluno)sender;
                //Seleciona o DGV 
                dgvSelecionado = dgv_;

                if (dgv_.CurrentCell.Value != null)
                {
                    valor_dgv_anterior = dgv_.CurrentCell.Value.ToString();

                }
                else
                {
                    valor_dgv_anterior = "";

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void dgvAtendimentos_dis_area_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                Controles.dgvAtendimentosAluno dgv_ = (Controles.dgvAtendimentosAluno)sender;
                FlowLayoutPanel flpPai = (FlowLayoutPanel)dgv_.Parent.Parent;
                Controles.tlpDisciplinaAluno tlp = (Controles.tlpDisciplinaAluno)dgv_.Parent;
                disciplinaAlunoSelecionado = tlp.DisciplinaAluno;
                int indexColAtual = coluna_atual(dgv_);
                int indexLinAtual = linha_atual(dgv_);
                //Verifica se selecionou um Atendimento ou Módulo/Menção
                if (dgv_.CurrentCell.RowIndex == 1)
                {
                    //Selecionado um Tipo de Atendimento (ComboBox)
                    DataGridViewComboBoxCell cmbTipoAtendimentos =
                        (DataGridViewComboBoxCell)dgv_.Rows[indexLinAtual].Cells[indexColAtual];
                    if (cmbTipoAtendimentos.Value != null)
                    {
                        //Atribui o AtendimentoSelecionado do DGV a partir do que foi selecionad no cmb do campo em branco
                        dgv_.AtendimentosSelecionado = DAO.AtendimentoDAO.Consultar((int)cmbTipoAtendimentos.Value);
                        //Defini a Disciplina do AtendimentoSelecionado
                        dgv_.AtendimentosSelecionado.Disciplina = dgv_.pai.DisciplinaAluno.Disciplina;

                        if (dgv_.Rows[indexLinAtual].Cells[indexColAtual].Value != null)
                        {
                            #region TODOS - Testar o tipo de atendimento para definir se existe menção, caso não exista, bloqueia o campo.
                            if (dgv_.AtendimentosSelecionado.Mencao != false)
                            {
                                //desbloquia campo mod
                                dgv_.Rows[2].Cells[indexColAtual].ReadOnly = false;
                                dgv_.Rows[2].Cells[indexColAtual].Value = "";

                                //desbloqueia campo nota
                                dgv_.Rows[3].Cells[indexColAtual].ReadOnly = false;
                                dgv_.Rows[3].Cells[indexColAtual].Value = "";
                                dgv_.Rows[3].Cells[indexColAtual].Style.BackColor = Color.White;
                            }
                            else
                            {
                                //Bloqueia campo NOTA e pinta de Cinza                       
                                dgv_.Rows[3].Cells[indexColAtual].ReadOnly = true;
                                dgv_.Rows[3].Cells[indexColAtual].Style.BackColor = Color.DarkGray;

                                if (dgv_.Rows[3].Cells[indexColAtual].Value != null)
                                {
                                    //Excluir NOTA e apaga valor do Campo
                                    dgv_.Rows[3].Cells[indexColAtual].Value = "";
                                    //TODO: AGORA - DAO.NotaDAO.Excluir(atendimento);
                                }

                                //Desbloquia campo mod
                                dgv_.Rows[2].Cells[indexColAtual].ReadOnly = false;
                                dgv_.Rows[2].Cells[indexColAtual].Value = "";
                            }
                            #endregion

                            #region TODOS - Automatizar módulo "MF"
                            if (dgv_.AtendimentosSelecionado.Nome == "ENCERRADO" || dgv_.AtendimentosSelecionado.Nome == "MÉDIA FINAL")
                            {
                                //desbloquia campo mod
                                dgv_.Rows[2].Cells[indexColAtual].ReadOnly = true;
                                dgv_.Rows[2].Cells[indexColAtual].Value = "MF";

                                //desbloqueia campo nota
                                dgv_.Rows[3].Cells[indexColAtual].ReadOnly = false;
                                dgv_.Rows[3].Cells[indexColAtual].Value = "";

                            }
                            #endregion

                            //TODO: AGORA - VERIFICAR POIS ESTÁ COM ERRO... VERIFICAR NO SERVIDOR TBM
                            #region AMERICANA - Verificar se já existe um atendimento na data de hoje 
                            /*
                            if (Configurações.csEscola.cidade.ToLower() == "americana")
                            {
                                if (dgv_.tipoDeAtendimentoSelecionado.Nome == "ORIENTAÇÃO")
                                {
                                    for (int i = 0; i < DgvTabelaDeAtendimentos.listaDeAtendimentoDoDgv.Count; i++)
                                    {
                                        if(DgvTabelaDeAtendimentos.listaDeAtendimentoDoDgv[i].EnsinoDoAtendimento.Codigo == ensinoSelecionado.Codigo)
                                        {
                                            if (DgvTabelaDeAtendimentos.listaDeAtendimentoDoDgv[i].DtDoAtendimento.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                                            {
                                                MessageBox.Show("Orientação já atribuída na data de hoje.", "Passaporte do Aluno: Atendimento");

                                                this.BeginInvoke(new MethodInvoker(() =>
                                                {
                                                    preencherPassaporteIndividual(ensinoSelecionado.Codigo, flpPai);
                                                }));
                                                repreencherPassaporteCompleto(ensinoSelecionado.Codigo, DisciplinaDaTabela.Codigo);

                                                return;
                                            }
                                        }

                                    }
                                }
                            }
                            */
                            #endregion

                            //TODO: AGORA - VERIFICAR POIS NÃO PROCEDE
                            #region TODOS - Bloqueia campos de modulo e nota caso seja "ORIENTAÇÃO INICIAL"
                            if (dgv_.AtendimentosSelecionado.Nome == "ORIENTAÇÃO INICIAL")
                            {
                                //bloqueia campo mod
                                dgv_.Rows[2].Cells[indexColAtual].ReadOnly = false;
                                dgv_.Rows[2].Cells[indexColAtual].Value = "";

                                //bloqueia campo nota
                                dgv_.Rows[3].Cells[indexColAtual].ReadOnly = true;
                                dgv_.Rows[3].Cells[indexColAtual].Value = "";
                            }
                            #endregion

                            #region TODOS - Automatizar módulo "AF"
                            if (dgv_.AtendimentosSelecionado.Nome == "AVALIAÇÃO FINAL" ||
                                dgv_.AtendimentosSelecionado.Nome == "PROVA FINAL")
                            {
                                //desbloquia campo mod
                                dgv_.Rows[2].Cells[indexColAtual].ReadOnly = true;
                                dgv_.Rows[2].Cells[indexColAtual].Value = "AF";

                                //desbloqueia campo nota
                                dgv_.Rows[3].Cells[indexColAtual].ReadOnly = false;
                                dgv_.Rows[3].Cells[indexColAtual].Value = "";
                            }

                            //TODO: AGORA - TESTAR SE OR || ACIMA FUNCIONOU 
                            //if (dgv_.tipoDeAtendimentoSelecionado.Nome == "PROVA FINAL")
                            //{
                            //    //desbloquia campo mod
                            //    dgv_.Rows[2].Cells[indexColAtual].ReadOnly = true;
                            //    dgv_.Rows[2].Cells[indexColAtual].Value = "AF";

                            //    //desbloqueia campo nota
                            //    dgv_.Rows[3].Cells[indexColAtual].ReadOnly = false;
                            //    dgv_.Rows[3].Cells[indexColAtual].Value = "";
                            //}

                            #endregion
                        }
                    }

                }
                else
                {
                    if (dgv_.CurrentCell.RowIndex == 2) //Verifica se editou um campo de Módulo ou Notas e Médias
                    {
                        #region Editou um campo de Modulo
                        //verifica se NÃO esta vazio
                        if (dgv_.CurrentCell.Value.ToString() != string.Empty)
                        {
                            //obtem o tipo de atendimento selecionado
                            string valor_campo = dgv_.AtendimentosSelecionado.Nome;
                            //verifica se o tipo de atendimento selecionado NÃO tem mencao_padrão.
                            if (dgv_.AtendimentosSelecionado.Mencao == false)
                            {
                                if (dgv_.Rows[0].Cells[indexColAtual].Value == null) //Confirma codAtendimento em vazio
                                {
                                    //Inserir Novo AtendimentoAluno
                                    AtendimentoAluno objNovoAtendimentoAluno = new AtendimentoAluno();
                                    objNovoAtendimentoAluno.Modulo = dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(); //PODE FICAR EM BRANCO
                                    objNovoAtendimentoAluno.DtDoAtendimento = DateTime.Now;
                                    objNovoAtendimentoAluno.ProfessorAtribuiuAtendimento = Utils.csUsuarioLogado.professor;
                                    objNovoAtendimentoAluno.DisciplinaAluno = tlp.DisciplinaAluno;
                                    objNovoAtendimentoAluno.Atendimento = dgv_.AtendimentosSelecionado;
                                    int codNovoAtendimento = DAO.AtendimentoAlunoDAO.Gravar(objNovoAtendimentoAluno);
                                    //Adicionar o novo AtendimentoAluno na lista de AtendimentoAlunos
                                    dgv_.addNovoAtendimento(objNovoAtendimentoAluno);

                                    if (dgv_.AtendimentosSelecionado.Nome == "ORIENTAÇÃO INICIAL")
                                    {
                                        //dtp_orient_ini.Value = DateTime.Now;
                                        //cs_notas.add_orient_ini(n_mat, cs_alunos.ensino(n_mat), cs_disciplinas.troca_disciplina_id_por_nome(id_disciplina_dgv_atual), DateTime.Now.ToString());
                                    }
                                    //Zerar
                                    dgv_.AtendimentosSelecionado = null;
                                    //TODO: AGORA - VERIRICAR PARA ELIMININAR O REEPRENCHER
                                    //#region Reeprencher
                                    ////repreencherPassaporteIndividual(ensinoSelecionado.Codigo, DisciplinaDaTabela.Codigo);
                                    //repreencherTabela(tlp);
                                    //#endregion
                                    //Adiciona o objNovoAtendimento no campo 0 do dgv
                                    dgv_.Rows[0].Cells[indexColAtual].Value = objNovoAtendimentoAluno;
                                    //Travar campo para evitar alteração
                                    BoolTravarCampo = true;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Editou um campo de Notas e Médias
                        //verifica se campo modulo NÃO esta vazio
                        if (dgv_.Rows[2].Cells[indexColAtual].Value != null)
                        {
                            //verifica se campo nota NÃO esta vazio
                            if (dgv_.CurrentCell.Value != null)
                            {
                                if (dgv_.Rows[2].Cells[indexColAtual].Value.ToString() == "MF")
                                {
                                    #region Inserir Atendimento + Média
                                    //Verificar nota - maior que o permitido
                                    if (float.Parse(dgv_.Rows[3].Cells[indexColAtual].Value.ToString()) < 11)
                                    {
                                        if (dgv_.Rows[0].Cells[indexColAtual].Value == null) //Confirma codDoAtendimento vazio
                                        {
                                            //TODO: FUTURO 1 - IMPLEMENTAR CALCULO MÉDIA AUTOMÁTICA                                        

                                            //Inserir Novo AtendimentoAluno
                                            AtendimentoAluno objNovoAtendimentoAluno = new AtendimentoAluno();
                                            objNovoAtendimentoAluno.Modulo = dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(); //PODE FICAR EM BRANCO
                                            objNovoAtendimentoAluno.DtDoAtendimento = DateTime.Now;
                                            objNovoAtendimentoAluno.ProfessorAtribuiuAtendimento = Utils.csUsuarioLogado.professor;
                                            objNovoAtendimentoAluno.DisciplinaAluno = tlp.DisciplinaAluno;
                                            objNovoAtendimentoAluno.Atendimento = dgv_.AtendimentosSelecionado;
                                            int codNovoAtendimento = DAO.AtendimentoAlunoDAO.Gravar(objNovoAtendimentoAluno);
                                            //Adicionar o novo AtendimentoAluno na lista de AtendimentoAlunos
                                            dgv_.addNovoAtendimento(objNovoAtendimentoAluno);
                                            //Inserir Nova Média
                                            Media media = new Media();
                                            media.Valor = dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString();
                                            media.Instituicao = _settings.CeejaNome;
                                            media.Cidade = _settings.CeejaCidade;
                                            media.DtMedia = DateTime.Now.ToString("yyyy-MM-dd");
                                            media.UsuarioCadastro = Utils.csUsuarioLogado.professor;
                                            media.AtendimentoAluno = objNovoAtendimentoAluno;
                                            objNovoAtendimentoAluno.DisciplinaAluno.Media = media;
                                            DAO.MediaDAO.Gravar(objNovoAtendimentoAluno.DisciplinaAluno);
                                            //Adicionar Média na grade Atualizar  DisciplinaAluno para concluida
                                            objNovoAtendimentoAluno.DisciplinaAluno.Concluida = true;
                                            //Adiciona o objNovoAtendimento no campo 0 do dgv
                                            dgv_.Rows[0].Cells[indexColAtual].Value = objNovoAtendimentoAluno;
                                            //disciplinaAlunoSelecionado = objNovoAtendimentoAluno.DisciplinaAluno;
                                            DAO.DisciplinaAlunoDAO.Atualizar(objNovoAtendimentoAluno.DisciplinaAluno);
                                            AtualizarMediaGrade();

                                            //Travar campo de MÉDIA para evitar alteração
                                            //
                                            BoolTravarCampo = true;

                                            //Atribuir nova Disciplina
                                            BoolAtribuirNovaDisciplina = true;


                                        }
                                    }
                                    else
                                    {
                                        //Caso seja maior que permitido, zera o campo
                                        dgv_.Rows[indexLinAtual].Cells[indexColAtual].Value = "";
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region Inserir Atendimento + Nota
                                    float nota_;
                                    if (float.TryParse(dgv_.Rows[3].Cells[indexColAtual].Value.ToString(), out nota_))
                                    {
                                        if (nota_ < 11)
                                        {
                                            if (dgv_.Rows[0].Cells[indexColAtual].Value == null) //Confirma codDoAtendimento Vazio
                                            {
                                                //Inserir Novo AtendimentoAluno
                                                AtendimentoAluno objNovoAtendimentoAluno = new AtendimentoAluno();
                                                objNovoAtendimentoAluno.Modulo = dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(); //PODE FICAR EM BRANCO
                                                objNovoAtendimentoAluno.DtDoAtendimento = DateTime.Now;
                                                objNovoAtendimentoAluno.ProfessorAtribuiuAtendimento = Utils.csUsuarioLogado.professor;
                                                objNovoAtendimentoAluno.DisciplinaAluno = tlp.DisciplinaAluno;
                                                objNovoAtendimentoAluno.Atendimento = dgv_.AtendimentosSelecionado;
                                                int codNovoAtendimento = DAO.AtendimentoAlunoDAO.Gravar(objNovoAtendimentoAluno);
                                                //Adicionar o novo AtendimentoAluno na lista de AtendimentoAlunos
                                                dgv_.addNovoAtendimento(objNovoAtendimentoAluno);
                                                //Inserir Nova Nota
                                                Nota nota = new Nota();
                                                objNovoAtendimentoAluno.Nota = nota;
                                                objNovoAtendimentoAluno.Nota.Valor = float.Parse(dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString());
                                                DAO.NotaDAO.Gravar(objNovoAtendimentoAluno);
                                                //Adiciona o objNovoAtendimento no campo 0 do dgv
                                                dgv_.Rows[0].Cells[indexColAtual].Value = objNovoAtendimentoAluno;
                                                //Travar campo para evitar alteração
                                                BoolTravarCampo = true;
                                            }
                                        }
                                        else
                                        {
                                            //Caso seja maior que permitido, zera o campo
                                            dgv_.Rows[indexLinAtual].Cells[indexColAtual].Value = "";
                                        }
                                    }
                                    else
                                    {
                                        //Será A, B, C ou AC
                                        //Votorantim?
                                        if (_settings.Ceeja.ToLower() == "votorantim")
                                        {
                                            if (dgv_.AtendimentosSelecionado.Nome == "ATIVIDADE" ||
                                                dgv_.AtendimentosSelecionado.Nome == "VÍDEO")
                                            {
                                                string notaAtividade = dgv_.Rows[3].Cells[indexColAtual].Value.ToString().ToUpper();

                                                if (notaAtividade == "A")
                                                    nota_ = 3;
                                                else if (notaAtividade == "B")
                                                    nota_ = 2;
                                                else if (notaAtividade == "C")
                                                    nota_ = 1;
                                                else
                                                    nota_ = 0;

                                                //Inserir Novo AtendimentoAluno
                                                AtendimentoAluno objNovoAtendimentoAluno = new AtendimentoAluno();
                                                objNovoAtendimentoAluno.Modulo = dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(); //PODE FICAR EM BRANCO
                                                objNovoAtendimentoAluno.DtDoAtendimento = DateTime.Now;
                                                objNovoAtendimentoAluno.ProfessorAtribuiuAtendimento = Utils.csUsuarioLogado.professor;
                                                objNovoAtendimentoAluno.DisciplinaAluno = tlp.DisciplinaAluno;
                                                objNovoAtendimentoAluno.Atendimento = dgv_.AtendimentosSelecionado;
                                                int codNovoAtendimento = DAO.AtendimentoAlunoDAO.Gravar(objNovoAtendimentoAluno);
                                                //Adicionar o novo AtendimentoAluno na lista de AtendimentoAlunos
                                                dgv_.addNovoAtendimento(objNovoAtendimentoAluno);
                                                //Inserir Nova Nota
                                                Nota nota = new Nota();
                                                objNovoAtendimentoAluno.Nota = nota;
                                                objNovoAtendimentoAluno.Nota.Valor = nota_;
                                                DAO.NotaDAO.Gravar(objNovoAtendimentoAluno);
                                                //Adiciona o objNovoAtendimento no campo 0 do dgv
                                                dgv_.Rows[0].Cells[indexColAtual].Value = objNovoAtendimentoAluno;
                                                //Travar campo para evitar alteração
                                                BoolTravarCampo = true;

                                            }
                                        }

                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion

                        if (Int32.TryParse(dgv_.Rows[3].Cells[indexColAtual].Value.ToString(), out int valor_))
                        {
                            if (valor_ < 5)
                            {
                                dgv_.Rows[3].Cells[indexColAtual].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                            }
                            else
                            {
                                dgv_.Rows[3].Cells[indexColAtual].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                            }
                        }
                    }
                }

                if (BoolTravarCampo)
                {

                    TravarCampo(dgv_, indexColAtual);
                    BoolTravarCampo = false;
                }


                if (BoolAtribuirNovaDisciplina)
                {
                    AtribuirNovaDisciplina();
                    BoolAtribuirNovaDisciplina = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }
        public int linha_atual(DataGridView sender_)
        {
            return sender_.CurrentRow.Index;
        }
        public int coluna_atual(DataGridView sender_)
        {
            return sender_.CurrentCell.ColumnIndex;
        }

        public void TravarCampo(DataGridView dgv, int col)
        {
            try
            {
                dgv.Rows[1].Cells[col].ReadOnly = true;
                dgv.Rows[2].Cells[col].ReadOnly = true;
                dgv.Rows[3].Cells[col].ReadOnly = true;
            }
            catch (Exception)
            {

                throw;
            }

        }

        //Delete
        private void excluirAtendimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Controles.dgvAtendimentosAluno dgv_ = (Controles.dgvAtendimentosAluno)sender_mouse_right_dgv;
                bool excluirAtendimento = true;
                if (_settings.Ceeja.ToLower() == "americana" &&
                     dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value != null)
                {
                    if (((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value).Modulo == "MF")
                    {
                        if (ExcluirMedia(((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value)) == 0)
                        {
                            excluirAtendimento = false;
                        }

                    }
                    else if (((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value).Atendimento.Mencao)
                    {
                        if (DAO.NotaDAO.Excluir(((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value)) == 0)
                        {
                            excluirAtendimento = false;
                        }
                    }

                    if (excluirAtendimento)
                    {
                        if (DAO.AtendimentoAlunoDAO.Excluir(((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value)) > 0)
                        {
                            dgv_.Columns.RemoveAt(dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].ColumnIndex);
                            dgv_.AdicionarNovoCampo();
                        }
                    }
                }
                else if (_settings.Ceeja.ToLower() == "sorocaba" &&
                    dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value != null)
                {
                    if (((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value).Modulo == "MF")
                    {
                        if (ExcluirMedia(((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value)) == 0)
                        {
                            excluirAtendimento = false;
                        }

                    }
                    else if (((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value).Atendimento.Mencao)
                    {
                        if (DAO.NotaDAO.Excluir(((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value)) == 0)
                        {
                            excluirAtendimento = false;
                        }
                    }

                    if (excluirAtendimento)
                    {
                        if (DAO.AtendimentoAlunoDAO.Excluir(((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value)) > 0)
                        {
                            dgv_.Columns.RemoveAt(dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].ColumnIndex);
                            //verificar se ultima coluna é uma em branco ???
                            if (dgv_.Rows[0].Cells[dgv_.Columns.Count - 1].Value != null)
                                dgv_.AdicionarNovoCampo();
                        }
                    }
                }
                else if (_settings.Ceeja.ToLower() == "votorantim" &&
                    dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value != null)
                {
                    if (((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value).Modulo == "MF")
                    {
                        //Apenas Coordenador e GOE
                        if (Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.SECRETARIOADM ||
                        Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.COORDENADOR ||
                        Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.DIRETOR ||
                        Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.ADMINISTRADOR)
                        {
                            if (ExcluirMedia(((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value)) == 0)
                            {
                                excluirAtendimento = false;
                            }
                        }
                        else
                        {
                            excluirAtendimento = false;
                            MessageBox.Show("Operação não permitida para o usuário atual", "Ematrícula");
                        }
                    }
                    else if (((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value).Atendimento.Mencao)
                    {
                        if (DAO.NotaDAO.Excluir(((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value)) == 0)
                        {
                            excluirAtendimento = false;
                            //MessageBox.Show("Operação não permitida para o usuário atual", "Ematrícula");
                        }
                    }

                    if (excluirAtendimento)
                    {
                        if (DAO.AtendimentoAlunoDAO.Excluir(((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value)) > 0)
                        {
                            dgv_.Columns.RemoveAt(dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].ColumnIndex);
                            dgv_.AdicionarNovoCampo();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

        }

        private int ExcluirMedia(AtendimentoAluno atendimentoAluno)
        {
            int retorno = 0;
            try
            {
                DAO.MediaDAO.Excluir(atendimentoAluno.DisciplinaAluno);
                atendimentoAluno.DisciplinaAluno.Concluida = false;
                DAO.DisciplinaAlunoDAO.Atualizar(atendimentoAluno.DisciplinaAluno);

                if (atendimentoAluno.DisciplinaAluno.EnsinoAluno.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                {
                    foreach (var teste in flpGradeFundamental.Controls)
                    {
                        if (((MyPanel)teste).Name.Replace("PanelGrade_", string.Empty) == atendimentoAluno.DisciplinaAluno.Disciplina.Nome)
                        {
                            ClearFlp((MyPanel)teste);
                        }
                    }
                }
                else
                {
                    foreach (var teste in flpGradeMedio.Controls)
                    {
                        if (((MyPanel)teste).Name.Replace("PanelGrade_", string.Empty) == atendimentoAluno.DisciplinaAluno.Disciplina.Nome)
                        {
                            ClearFlp((MyPanel)teste);
                        }
                    }
                }
                retorno = 1;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            return retorno;
        }
        //Update
        private void modificarAtendimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dgv_ = (DataGridView)sender_mouse_right_dgv;

            if (dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value != null)
            {
                frmModificarAtendimento form = new frmModificarAtendimento((AtendimentoAluno)dgv_.Rows[0].Cells[dgv_.CurrentCell.ColumnIndex].Value, _settings);
                form.ShowDialog();

                //repreencherTabela(tlp);
                //TODO: AGORA - ATUALIZAR APENAS UM ATENDIMENTO TALVES, VERIFICAR COMPATIBILIDADE COM DATAS PARA QUE UMA NÃO VENHA ASE MANTER NA FRENTE DA A OUTRA
            }
        }
        // Ajustar tamanho do flp, tlp e dgv
        private void flp_Resize(object sender, EventArgs e)
        {
            FlowLayoutPanel flp_ = (FlowLayoutPanel)sender;
            resize_flp_(flp_);
        }
        private void resize_flp_(FlowLayoutPanel flp_)
        {
            flp_.Resize -= flp_Resize;
            foreach (TableLayoutPanel tlp in flp_.Controls.OfType<TableLayoutPanel>())
            {
                string a = Name;
                //TableLayoutPanel
                tlp.Width = flp_.Width - 20;
                //DataGridView - Largura altera por ser fill.
                foreach (DataGridView dgv in tlp.Controls.OfType<DataGridView>())
                {
                    string ee = dgv.Name;
                    foreach (var scroll in dgv.Controls.OfType<HScrollBar>())
                    {
                        if (scroll.Visible)
                        {
                            //SCROLL HORIZONTAL
                            tlp.Height = 120;
                            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                            dgv.Height = 65;
                            //Para Teste
                            //tlp.BackColor = Color.Red;
                            //dgv.BackgroundColor = Color.Green;
                        }
                        else
                        {
                            //SEM SCROLL HORIZONTAL
                            tlp.Height = 104;
                            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                            dgv.Height = 55;
                            //Para Teste
                            //tlp.BackColor = Color.Red;
                            //dgv.BackgroundColor = Color.Green;
                        }
                    }
                }
            }
            flp_.Resize += flp_Resize;
        }
        #region Data Orientação Inicial
        private void dtp_orient_ini_Leave(object sender, EventArgs e)
        {
            /*
            DateTimePicker dtp_ = (DateTimePicker)sender;
            FlowLayoutPanel flpPai = (FlowLayoutPanel)dtp_.Parent.Parent.Parent;
            Controles.tlpTabelaDeAtendimentos tlp = (Controles.tlpTabelaDeAtendimentos)dtp_.Parent.Parent;
            if (DgvTabelaDeAtendimentos.listaDeAtendimentoDoDgv.Exists(x => x.Nome == "ORIENTAÇÃO INICIAL"))
            {
                foreach (AtendimentoAluno objAtendimento in DgvTabelaDeAtendimentos.listaDeAtendimentoDoDgv)
                    if (objAtendimento.Nome == "ORIENTAÇÃO INICIAL")
                    {
                        #region Modificar Atendimento
                        objAtendimento.DtDoAtendimento = dtp_.Value;
                        objAtendimento.DtDaModificaoAtendimento = DateTime.Now;
                        objAtendimento.ProfessorModificouAtendimento = objProfessor;
                        int codNovoAtendimento = DAO.AtendimentoDAO.Atualizar(objAtendimento); //TODO:: chamar gravar para que o dao descida se grava ou atualiza
                        #endregion
                        #region Repreencher DGVs
                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            preencherPassaporteIndividual(ensinoSelecionado.Codigo, flpPai); //lags
                                                                                                  //add_campo_branco(dgv_);
                            }));
                        repreencherPassaporteCompleto(ensinoSelecionado.Codigo, DisciplinaDaTabela.Codigo);
                        #endregion
                    }
                       
            }
            else //Não Existe Orientação Incial - Criar e Atualizar DGV... 
            {
                if (DgvTabelaDeAtendimentos.listaDeAtendimentoDoDgv.Count > 0)
                {
                    foreach (AtendimentoAluno objAtendimento in DgvTabelaDeAtendimentos.listaDeAtendimentoDoDgv)
                        if (dtp_.Value < DgvTabelaDeAtendimentos.listaDeAtendimentoDoDgv[0].DtDoAtendimento)
                        {
                            #region Inserir Atendimento
                            AtendimentoAluno objNovoAtendimento = new AtendimentoAluno();
                            objNovoAtendimento.Nome = "";
                            objNovoAtendimento.Modulo = "";
                            objNovoAtendimento.ProfessorAtribuiuAtendimento = objProfessor;
                            objNovoAtendimento.DtDoAtendimento = dtp_.Value;
                            objNovoAtendimento.EnsinoDoAtendimento = ensinoSelecionado;
                            objNovoAtendimento.DisciplinaDoAtendimento = DisciplinaDaTabela;
                            objNovoAtendimento.NMatricula = objAluno.NMatricula;
                            int codNovoAtendimento = DAO.AtendimentoDAO.Gravar(objNovoAtendimento);
                            #endregion
                            #region Repreencher DGVs
                            this.BeginInvoke(new MethodInvoker(() =>
                            {
                                preencherPassaporteIndividual(ensinoSelecionado.Codigo, flpPai); //lags
                                                                                                      //add_campo_branco(dgv_);
                            }));
                            repreencherPassaporteCompleto(ensinoSelecionado.Codigo, DisciplinaDaTabela.Codigo);
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("Data informada é superior ao primeiro atendimento registrado para esta disciplina.", "Passaporte do Aluno: Atendimento");
                            break;
                        }
                }
                else
                {
                    #region Inserir Atendimento
                    AtendimentoAluno objNovoAtendimento = new AtendimentoAluno();
                    objNovoAtendimento.Nome = "";
                    objNovoAtendimento.Modulo = "";
                    objNovoAtendimento.ProfessorAtribuiuAtendimento = objProfessor;
                    objNovoAtendimento.DtDoAtendimento = dtp_.Value;
                    objNovoAtendimento.EnsinoDoAtendimento = ensinoSelecionado;
                    objNovoAtendimento.DisciplinaDoAtendimento = DisciplinaDaTabela;
                    objNovoAtendimento.NMatricula = objAluno.NMatricula;
                    int codNovoAtendimento = DAO.AtendimentoDAO.Gravar(objNovoAtendimento);
                    #endregion
                    #region Repreencher DGVs
                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        preencherPassaporteIndividual(ensinoSelecionado.Codigo, flpPai); //lags
                                                                                              //add_campo_branco(dgv_);
                    }));
                    repreencherPassaporteCompleto(ensinoSelecionado.Codigo, DisciplinaDaTabela.Codigo);
                    #endregion
                }
            }
            */
        }
        #endregion
        #region Eventos

        private void dgvAtendimentos_dis_area_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv_ = (DataGridView)sender;

            string a = dgv_.Name;

            //dtp_orient_ini.Enabled = true;
            //dtp_orient_ini.Visible = true;
            //
            //lblOrient_ini.Visible = true;
            //lblOrient_ini.Text = "Orient. Inicial - " + dgv_.Name.Replace("dgv_","") + ":";

            //if(dgv_.CurrentRow.Index == 2)
            //{
            //    //Verificar solução melhor
            //    Cursor.Hide();
            //}
        }

        private void dgvAtendimentos_dis_area_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv_ = (DataGridView)sender;

            e.Control.KeyPress -= new KeyPressEventHandler(row1_KeyPress);
            if (dgv_.CurrentCell.RowIndex == 3) //Desired row
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(row1_KeyPress);
                }
            }
        }

        private void dgvAtendimentos_dis_area_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv_ = (DataGridView)sender;

            sender_mouse_right_dgv = dgv_;

            if (e.Button == MouseButtons.Right)
            {
                dgv_.CurrentCell = dgv_.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dgv_.Rows[1].Cells[e.ColumnIndex].Selected = true;
                dgv_.Focus();

            }
        }
        #endregion
        //Alterado
        private void dgvAtendimentos_dis_area_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv_ = (DataGridView)sender;

                if (e.ColumnIndex != 0)
                {
                    //Tem id_atendimento?
                    if (dgv_.Rows[0].Cells[e.ColumnIndex].Value != null)
                    {
                        //Converter valor da célular para um AtendimentoAluno
                        AtendimentoAluno atendimentoAluno = ((AtendimentoAluno)dgv_.Rows[0].Cells[e.ColumnIndex].Value);

                        string[] atendido_por = atendimentoAluno.ProfessorAtribuiuAtendimento.Nome.Split(' ');

                        if (atendido_por.Count() > 1)
                        {
                            tssl1.Text = "Atendido por " + atendido_por[0] + " " + atendido_por[1] + " em " + atendimentoAluno.DtDoAtendimento;
                            ttTipo_atendimento.SetToolTip(dgv_, atendimentoAluno.Atendimento.Nome);
                        }
                        else
                        {
                            tssl1.Text = "Atendido por " + atendido_por[0] + " em " + atendimentoAluno.DtDoAtendimento;
                            ttTipo_atendimento.SetToolTip(dgv_, atendimentoAluno.Atendimento.Nome);
                        }

                        if (atendimentoAluno.DtDaModificaoAtendimento.ToString("dd/MM/yyyy") != "01/01/0001")
                        {
                            atendido_por = atendimentoAluno.ProfessorModificouAtendimento.Nome.Split(' ');

                            if (atendido_por.Count() > 1) //TODO: FUTURO 2 - ARRUMAR QUESTÃO NOME DUPLO 
                            {
                                tssl1.Text = "Atendido por " + atendido_por[0] + " " + atendido_por[1] + " em " + atendimentoAluno.DtDoAtendimento +
                                    " e modificado por " + atendimentoAluno.ProfessorModificouAtendimento.Nome + " em " + atendimentoAluno.DtDaModificaoAtendimento;
                                ttTipo_atendimento.SetToolTip(dgv_, atendimentoAluno.Atendimento.Nome);
                            }
                            else
                            {
                                tssl1.Text = "Atendido por " + atendido_por[0] + " em " + atendimentoAluno.DtDoAtendimento +
                                    " e modificado por " + atendimentoAluno.ProfessorModificouAtendimento.Nome + " em " + atendimentoAluno.DtDaModificaoAtendimento;
                                ttTipo_atendimento.SetToolTip(dgv_, atendimentoAluno.Atendimento.Nome);
                            }
                        }
                    }
                }
                else
                {
                    tssl1.Text = "";
                    ttTipo_atendimento.Hide(dgv_);
                }
            }
            catch (Exception ex)
            {

                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

        }
        //Alterado

        private void dgvAtendimentos_dis_area_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            tssl1.Text = "";
        }

        private void dgvAtendimentos_com_MouseLeave(object sender, EventArgs e)
        {
            tssl1.Text = "";
        }

        private void dgvAtendimentos_com_1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            string a = ((DataGridView)sender).Name;

            ((DataGridView)sender).Focus();

            foreach (DataGridView dgv in tabFundCom.Controls.OfType<DataGridView>())
            {
                if (dgv.Name == a)
                {
                    if (e.ColumnIndex != 0)
                    {
                        //Verifica quem atribuiu
                        if (dgv.Rows[4].Cells[e.ColumnIndex].Value != null)
                        {
                            //verifica quando atribuiu
                            if (dgv.Rows[5].Cells[e.ColumnIndex].Value != null)
                            {
                                //Verifica se tem NOTA e quem atribuiu
                                if (dgv.Rows[6].Cells[e.ColumnIndex].Value != null)
                                {
                                    //verifica quando atribuiu
                                    if (dgv.Rows[8].Cells[e.ColumnIndex].Value != null)
                                    {
                                        //Verifica se tem NOTA MODIFICADA e quem modificou
                                        #region MODIFICADA
                                        if (dgv.Rows[7].Cells[e.ColumnIndex].Value != null)
                                        {
                                            //verifica quando atribuiu
                                            if (dgv.Rows[9].Cells[e.ColumnIndex].Value != null)
                                            {
                                                Classes.Usuario usuario_1 = DAO.UsuarioDAO.Consultar(Convert.ToInt32(dgv.Rows[6].Cells[e.ColumnIndex].Value));
                                                string nome_1 = usuario_1.Nome;
                                                string data_lanc = dgv.Rows[8].Cells[e.ColumnIndex].Value.ToString();
                                                Classes.Usuario usuario_2 = DAO.UsuarioDAO.Consultar(Convert.ToInt32(dgv.Rows[7].Cells[e.ColumnIndex].Value));
                                                string nome_2 = usuario_2.Nome;
                                                string data_alter = dgv.Rows[9].Cells[e.ColumnIndex].Value.ToString();

                                                //Reduzir nome
                                                string[] lanc_por = nome_1.Split(' ');

                                                string[] alter_por = nome_2.Split(' ');

                                                if (lanc_por.Count() > 1)
                                                {
                                                    tssl1.Text = "Lançado por " + lanc_por[0] + " " + lanc_por[1] + " em " + data_lanc +
                                                             " - Alterado por " + alter_por[0] + " " + alter_por[1] + " em " + data_alter;
                                                }
                                                else
                                                {
                                                    tssl1.Text = "Lançado por " + lanc_por[0] + " em " + data_lanc +
                                                             " - Alterado por " + alter_por[0] + " em " + data_alter;
                                                }
                                            }
                                        }
                                        #endregion
                                        else
                                        {
                                            Classes.Usuario usuario_1 = DAO.UsuarioDAO.Consultar(Convert.ToInt32(dgv.Rows[6].Cells[e.ColumnIndex].Value));
                                            string nome_1 = usuario_1.Nome;
                                            string data_lanc = dgv.Rows[8].Cells[e.ColumnIndex].Value.ToString();

                                            string[] lanc_por = nome_1.Split(' ');

                                            if (lanc_por.Count() > 1)
                                            {
                                                tssl1.Text = "Lançado por " + lanc_por[0] + " " + lanc_por[1] + " em " + data_lanc;
                                            }
                                            else
                                            {
                                                tssl1.Text = "Lançado por " + lanc_por[0] + " em " + data_lanc;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    Classes.Usuario usuario_1 = DAO.UsuarioDAO.Consultar(Convert.ToInt32(dgv.Rows[6].Cells[e.ColumnIndex].Value));
                                    string nome_1 = usuario_1.Nome;
                                    string data_atendido = dgv.Rows[5].Cells[e.ColumnIndex].Value.ToString();

                                    string[] atendido_por = nome_1.Split(' ');

                                    if (atendido_por.Count() > 1)
                                    {
                                        tssl1.Text = "Atendido por " + atendido_por[0] + " " + atendido_por[1] + " em " + data_atendido;
                                    }
                                    else
                                    {
                                        tssl1.Text = "Atendido por " + atendido_por + " em " + data_atendido;
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        tssl1.Text = "";
                    }
                }
            }
        }

        public void AtribuirNovaDisciplina()
        {
            //TODO: AGORA - VERIFICAR COMO IRÁ SE COMPORTAR PARA CADA ESCOLA
            if (_settings.Ceeja.ToLower() != "americana")
            {
                if (MessageBox.Show("Atribuir nova disciplina ao Aluno ?", "Passaporte do Aluno: Disciplina encerrada", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Formularios.frAtribuicaoDisciplina frAtribuicaoDisciplina = new Formularios.frAtribuicaoDisciplina();
                    frAtribuicaoDisciplina.MdiParent = this.ParentForm;
                    frAtribuicaoDisciplina.objAluno = this.objAluno;
                    frAtribuicaoDisciplina.Show();
                    this.Close();
                }
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Formularios.frmProcurarAluno frmProcurarAluno = new Formularios.frmProcurarAluno("passaporte", _settings);
            frmProcurarAluno.MdiParent = ParentForm;
            //frmProcurarAluno._retornoDeFormulario = "passaporte";
            frmProcurarAluno.Show();
            Cursor.Current = Cursors.Default;

            Close();
        }

        private void row1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (_settings.Ceeja.ToLower() == "sorocaba")
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar == ','))
                        e.Handled = true;

                }
                else if (_settings.Ceeja.ToLower() == "votorantim")
                {
                    if (dgvSelecionado.AtendimentosSelecionado.Nome == "ATIVIDADE" ||
                        dgvSelecionado.AtendimentosSelecionado.Nome == "VÍDEO")
                    {
                        char tecla = e.KeyChar;
                        string tecla_ = tecla.ToString().ToUpper();

                        if ((tecla_ != "A") && (tecla_ != "B") && (tecla_ != "C") && !char.IsControl(e.KeyChar))
                        {
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar == ','))
                            e.Handled = true;
                    }
                }
                else if (_settings.Ceeja.ToLower() == "americana")
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar == ','))
                        e.Handled = true;
                }
                else
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar == ','))
                        e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void tbcHistorico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objAluno.ListaEnsinoAluno.Count > 1)
            {
                foreach (EnsinoAluno ensinoAluno in objAluno.ListaEnsinoAluno)
                {
                    if (ensinoAluno.Ensino != ensinoSelecionado.Ensino)
                    {
                        ensinoSelecionado = ensinoAluno;

                        //ensinoSelecionado = Aluno.getEnsinoAlunoAtual(objAluno);
                        if (ensinoSelecionado.AtividadeExtra != null)
                        {
                            ckbAtvExtra.Checked = true;
                            lblDataAtvExtra.Text = ensinoSelecionado.AtividadeExtra.Data.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            ckbAtvExtra.Checked = false;
                            lblDataAtvExtra.Text = string.Empty;
                        }

                        alterar_visual_passaporte();
                        break;
                    }
                }
            }
            else
            {
                //Desliga evento para não chamar novamente
                tbcHistorico.SelectedIndexChanged -= tbcHistorico_SelectedIndexChanged;
                if (ensinoSelecionado.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    tbcHistorico.SelectedIndex = 0;
                else tbcHistorico.SelectedIndex = 1;
                MessageBox.Show("Aluno não possui matrícula em outro ensino.", "E-matricula: Passaporte");
                //Ativa evento após "troca"
                tbcHistorico.SelectedIndexChanged += tbcHistorico_SelectedIndexChanged;
            }
        }
        /// <summary>
        /// Retonar para a Aba Completo caso o usuário for defeirente de professor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbcPassaporteFundamental_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Utils.csUsuarioLogado.usuario.NivelAcesso !=
                Enumeradores.NivelAcesso.PROFESSOR)
            {
                tbcPassaporteFundamental.SelectedIndex = 1;
            }
        }
        /// <summary>
        /// Retonar para a Aba Completo caso o usuário for defeirente de professor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbcPassaporteMedio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Utils.csUsuarioLogado.usuario.NivelAcesso !=
                Enumeradores.NivelAcesso.PROFESSOR)
            {
                tbcPassaporteMedio.SelectedIndex = 1;
            }
        }
        public class MyPanel : Panel
        {
            public MyPanel()
            {
                SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            }

            protected override void OnPaint(PaintEventArgs e)
            {
                using (SolidBrush brush = new SolidBrush(BackColor))
                    e.Graphics.FillRectangle(brush, ClientRectangle);
                e.Graphics.DrawRectangle(Pens.Black, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
            }
        }

        public void ClearFlp(MyPanel myPanel)
        {

            if (myPanel.Name.Contains(myPanel.Name.Replace("PanelGrade_", string.Empty)))
            {
                foreach (CheckBox ckb in myPanel.Controls.OfType<CheckBox>())
                {
                    ckb.Checked = false;
                }

                foreach (Label lbl in myPanel.Controls.OfType<Label>())
                {
                    if (lbl.Name.Contains("lbl_data_"))
                        lbl.Text = string.Empty;
                }
            }
        }
    }
}