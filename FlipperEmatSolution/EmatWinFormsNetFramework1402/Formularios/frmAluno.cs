using EmatWinFormsNetFramework1402.Utils;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace EmatWinFormsNetFramework1402.Formularios
{
    /// <summary>
    /// Formulário de Cadastro de Alunos:
    /// Criado por Felipe A. Chagas
    /// </summary>
    public partial class frmAluno : Form
    {
        private readonly IEmatriculaSettings _settings;
        private Classes.Aluno _aluno;
        bool cadastro = true;

        public frmAluno(IEmatriculaSettings settings, Classes.Aluno aluno)
        {
            InitializeComponent();
            _settings = settings;
            _aluno = aluno;
        }

        private void FrCadastroAluno_Load(object sender, EventArgs e)
        {
            try
            {
                #region Popular opções do form

                #region CorOrigemEtinica
                cmbRaca.DataSource = Enum.GetValues(typeof(Enumeradores.CorOrigemEtnica))
                    .Cast<Enum>()
                    .Select(value => new
                    {
                        (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                        value
                    }).OrderBy(item => item.value).ToList();
                cmbRaca.DisplayMember = "Description";
                cmbRaca.ValueMember = "value";
                cmbRaca.SelectedIndex = -1;
                #endregion

                #region Sexo
                cmbSexo.DataSource = Enum.GetValues(typeof(Enumeradores.Sexo));
                cmbSexo.SelectedIndex = -1;
                #endregion

                #region Estado Civil
                cmbEstadoCivil.DataSource = Enum.GetValues(typeof(Enumeradores.EstadoCivil))
                    .Cast<Enum>()
                    .Select(value => new
                    {
                        (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                        value
                    }).OrderBy(item => item.value).ToList();
                cmbEstadoCivil.DisplayMember = "Description";
                cmbEstadoCivil.ValueMember = "value";
                cmbEstadoCivil.SelectedIndex = -1;
                #endregion

                #region Ensino
                cmbEnsino.SelectedIndexChanged -= cmbEnsino_SelectedIndexChanged;
                cmbEnsino.DataSource = Enum.GetValues(typeof(Enumeradores.Ensino));
                cmbEnsino.SelectedIndex = -1;
                cmbEnsino.SelectedIndexChanged += cmbEnsino_SelectedIndexChanged;
                #endregion

                #region Carregar Opções de Países de Nascimento.
                CarregarPaises();
                #endregion

                #region Carregar Opções de UF para RESIDENCIA.    
                cmbUfEndereco.SelectedIndexChanged -= cmbUfEndereco_SelectedIndexChanged;
                BindingSource bsUfResidencia = new BindingSource();
                bsUfResidencia.DataSource = DAO.UfDAO.ExibirTodos(DAO.PaisDAO.Consultar("BRASIL")); //TODO:: MELHOR NÃO TRAVAR ISSO AQUI, OU DEFINIR A SEED DO BRASIL NO MODELO OU SEI LÁ OU ATÉ PODEMOS DEFINIR TBM NA CONFIGURAÇÃO E MOSTRA QUANDO NÃO TIVER
                cmbUfEndereco.DataSource = bsUfResidencia;
                cmbUfEndereco.DisplayMember = "Nome";
                cmbUfEndereco.ValueMember = "Codigo";
                cmbUfEndereco.SelectedIndex = -1;
                cmbUfEndereco.SelectedIndexChanged += cmbUfEndereco_SelectedIndexChanged;
                #endregion

                #region Carregar Opções de UF para TRABALHO.
                cmbUfEmprego.SelectedIndexChanged -= cmbUfEmprego_SelectedIndexChanged;
                BindingSource bsUfTrabalho = new BindingSource();
                bsUfTrabalho.DataSource = DAO.UfDAO.ExibirTodos(DAO.PaisDAO.Consultar("BRASIL")); //TODO:: MELHOR NÃO TRAVAR ISSO AQUI, OU DEFINIR A SEED DO BRASIL NO MODELO OU SEI LÁ // OU ATÉ PODEMOS DEFINIR TBM NA CONFIGURAÇÃO E MOSTRA QUANDO NÃO TIVER
                cmbUfEmprego.DataSource = bsUfTrabalho;
                cmbUfEmprego.DisplayMember = "Nome";
                cmbUfEmprego.ValueMember = "Codigo";
                cmbUfEmprego.SelectedIndex = -1;
                cmbUfEmprego.SelectedIndexChanged += cmbUfEmprego_SelectedIndexChanged;
                #endregion

                #region Deficiencia
                cmbDeficiencia.DataSource = DAO.DeficienciaDAO.ExibirTodos();
                cmbDeficiencia.DisplayMember = "Nome";
                cmbDeficiencia.ValueMember = "Codigo";
                cmbDeficiencia.SelectedIndex = -1;
                #endregion

                #region Ocultar Items Conforme Escola
                if (_settings.Ceeja.ToLower() == "sorocaba")
                {
                    gpbEmprego.Visible = false;
                }
                else if (_settings.Ceeja.ToLower() == "americana")
                {
                    gpbEmprego.Visible = true;
                }
                else if (_settings.Ceeja.ToLower() == "votorantim")
                {
                    lblTermo.Text = "Ano";
                    gpbEmprego.Visible = false;
                }
                #endregion

                #endregion

                if (_aluno == null)
                {
                    lblTítulo.Text = "Matrícula de Alunos";
                    cadastro = true;
                    txtNmat.Enabled = true;
                    txtNmat.Focus();
                    btnSalva.Enabled = true;
                    btnProcurar.Enabled = true;
                    btnImprimir.Enabled = false;
                    btnSair.Enabled = true;
                    btnFicha.Enabled = false;
                    btnPassaporte.Enabled = false;
                    btnExcluir.Enabled = false;
                    btRematricula.Enabled = false;
                    //NOVO ALUNO
                    _aluno = new Classes.Aluno();
                }
                else
                {
                    lblTítulo.Text = "Editar Matrícula de Alunos";
                    cadastro = false;
                    txtNmat.Enabled = false;
                    txtAluno.Focus();
                    btnSalva.Enabled = false;
                    btnProcurar.Enabled = true;
                    btnImprimir.Enabled = true;
                    btnSair.Enabled = true;
                    btnFicha.Enabled = true;
                    btnPassaporte.Enabled = true;
                    btnExcluir.Enabled = true;
                    btRematricula.Enabled = true;
                    //ALUNO EXISTENTE
                    PreencherDadosAlunoExistente();
                }



                //moverControles();
                //KEY DOWN PARA RESOLVER PROBLEMA DO SCROWW EU ACHO
                #region //Adcionar em todos os TextBox e Combobox evento de KeyDow - TAB
                foreach (GroupBox grb in flowLayoutPanel2.Controls.OfType<GroupBox>())
                {
                    foreach (TextBox txt in grb.Controls.OfType<TextBox>())
                    {
                        txt.KeyDown += txtNmat_KeyDown;
                    }

                    foreach (ComboBox cmb in grb.Controls.OfType<ComboBox>())
                    {
                        cmb.MouseWheel += new MouseEventHandler(comboBox1_MouseWheel);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                //TODO: implementar alguma forma de cancelar abertura do form caso haja algum erro de carregamento
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void PreencherDadosAlunoExistente()
        {
            try
            {
                txtNmat.Text = _aluno.NMatricula.ToString();
                txtRa.Text = _aluno.Ra;
                txtRg.Text = _aluno.Rg;
                txtUfRg.Text = _aluno.UfRg;
                dtpExpRg.Text = _aluno.DtRg;
                txtOrgao.Text = _aluno.OrgaoRg;
                txtAluno.Text = _aluno.Nome;
                txtNomeSocial.Text = _aluno.NomeSocial;
                txtMae.Text = _aluno.NomeMae;
                txtPai.Text = _aluno.NomePai;
                mtbCpf.Text = _aluno.Cpf;
                dtpdatnasc.Text = _aluno.DtNascimento;
                txtObs_passporte.Text = _aluno.Observacao;
                cmbSexo.SelectedItem = _aluno.Sexo;
                cmbRaca.SelectedValue = _aluno.CorOrigemEtnica;
                cmbEstadoCivil.SelectedValue = _aluno.EstadoCivil;

                cmbEnsino.SelectedIndexChanged -= cmbEnsino_SelectedIndexChanged;
                cmbEnsino.SelectedItem = _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).Ensino;
                cmbEnsino.SelectedIndexChanged += cmbEnsino_SelectedIndexChanged;

                PreencherSituacaoEnsinoAtual();

                PreencherOpcoesTermoSistema();
                cmbTermo.Text = _aluno.TermoMatricula;

                PreencherOpcoesDisciplinaSistema();
                cmbDisciplina.SelectedValue = Classes.Aluno.GetDisciplinaAlunoAtual(_aluno).Disciplina.Codigo;

                ckbCertidao.Checked = _aluno.ApresentouCertificado;
                ckbHistorico.Checked = _aluno.ApresentouHistorico;

                //Nascimento
                if (_aluno.LocalNascimento != null)
                {
                    cmbPaisNascimento.SelectedIndexChanged += cmbPaisNascimento_SelectedIndexChanged;
                    cmbPaisNascimento.SelectedValue = _aluno.LocalNascimento.Cidade.Uf.Pais.Codigo;
                    cmbUfNascimento.SelectedValue = _aluno.LocalNascimento.Cidade.Uf.Codigo;
                    cmbCidadeNascimento.SelectedValue = _aluno.LocalNascimento.Cidade.Codigo;
                }

                //Endereço
                if (_aluno.Endereco != null)
                {
                    txtLogradouro.Text = _aluno.Endereco.Logradouro;
                    txtNumero.Text = _aluno.Endereco.Numero.ToString();
                    txtBairro.Text = _aluno.Endereco.Bairro;
                    txtComplemento.Text = _aluno.Endereco.Complemento;
                    txtCep.Text = _aluno.Endereco.Cep;
                    cmbUfEndereco.SelectedIndexChanged += cmbUfEndereco_SelectedIndexChanged;
                    cmbUfEndereco.SelectedValue = _aluno.Endereco.Cidade.Uf.Codigo;
                    cmbCidadeEndereco.SelectedValue = _aluno.Endereco.Cidade.Codigo;
                }

                //Contato
                txtTelefone.Text = _aluno.Telefone;
                txtCelular.Text = _aluno.Celular;
                txtEmail.Text = _aluno.Email;

                //Deficiência
                foreach (Classes.DeficienciaPessoa deficenciaPessoa in _aluno.ListaDeficienciaPessoa)
                {
                    dgvDeficienciaAluno.Rows.Add();
                    dgvDeficienciaAluno.Rows[dgvDeficienciaAluno.Rows.Count - 1].Cells[0].Value = deficenciaPessoa;
                    dgvDeficienciaAluno.Rows[dgvDeficienciaAluno.Rows.Count - 1].Cells[1].Value = deficenciaPessoa.Deficiencia.Nome;
                    dgvDeficienciaAluno.Rows[dgvDeficienciaAluno.Rows.Count - 1].Cells[2].Value = deficenciaPessoa.Observacao;
                }

                //Emprego
                if (_aluno.Emprego != null)
                {
                    txtNomeEmpresa.Text = _aluno.Emprego.NomeEmpresa;
                    txtTelefoneEmprego.Text = _aluno.Emprego.Telefone;
                    cmbUfEmprego.SelectedIndexChanged += cmbUfEmprego_SelectedIndexChanged;
                    cmbUfEmprego.SelectedValue = _aluno.Emprego.Cidade.Uf.Codigo;
                    cmbCidadeEmprego.SelectedValue = _aluno.Emprego.Cidade.Codigo;
                }
                //Foto
                if (_aluno.FotoDoAluno != null)
                {
                    ptbFoto.Image = _aluno.FotoDoAluno.Foto;

                }
                ptbFoto.SizeMode = PictureBoxSizeMode.StretchImage;
                //Rematriculas
                ltbRematriculas.DataSource = _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).ListaRematricula.OrderByDescending(x => x.Data).ToList();
                ltbRematriculas.DisplayMember = "Data";

                //RA Impresso 
                //var context = new Modelo.Modelo();
                //var _cartao = Utils.Settings.Context.CARTAO_ACESSO.FirstOrDefault(c => c.ALUNO == objAluno.NMatricula);
                //   (from cartao in Utils.Settings.Context.CARTAO_ACESSO.AsNoTracking()
                //              where cartao.ALUNO == objAluno.NMatricula
                //              select new
                //              {
                //                  cartao.IMPRESSO
                //              });

                //if (_cartao != null)
                //{
                //    ckbRaImpresso.Checked = _cartao.IMPRESSO;
                //}
                btnSalva.Enabled = false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void cmbEnsino_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cadastro)
            {
                dtpDtMatriculaDoEnsinoAluno.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblSituacao.Text = "ATIVO";
            }
            else
            {
                if (cmbEnsino.Text == "FUNDAMENTAL") //Médio -> Fundamental - ERRO DE MATRÍCULA
                {
                    if (_aluno.ListaEnsinoAluno.Any(en => en.Ensino == Enumeradores.Ensino.FUNDAMENTAL))
                    {
                        //Utilizar Fundamental Existente

                        //Altera para atual = 0 o ensino médio
                        Classes.EnsinoAluno.AtualizarEnsinoAluno(
                            _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Ensino == Enumeradores.Ensino.MÉDIO),
                            _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Ensino == Enumeradores.Ensino.MÉDIO).DtInicio,
                            _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Ensino == Enumeradores.Ensino.MÉDIO).DtTermino,
                            false);

                        //Altera para atual = 1 o ensino fundamental
                        Classes.EnsinoAluno.AtualizarEnsinoAluno(
                            _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Ensino == Enumeradores.Ensino.FUNDAMENTAL),
                            _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Ensino == Enumeradores.Ensino.FUNDAMENTAL).DtInicio,
                            _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Ensino == Enumeradores.Ensino.FUNDAMENTAL).DtTermino,
                            true);
                        //TODO:: Verificar como o sistema deverá secomportar em casos em que o ensino fundamental esta concluído

                        //Setar data de início de matrícula no ensino
                        dtpDtMatriculaDoEnsinoAluno.Text =
                            _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                            .DtInicio;

                        lblSituacao.Text = _aluno.ListaEnsinoAluno.FirstOrDefault(
                            en => en.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                            .ListaMovimentacao.LastOrDefault().SituacaoAluno.ToString();
                        lblDataMovimentacao.Text = _aluno.ListaEnsinoAluno.FirstOrDefault(
                            en => en.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                            .ListaMovimentacao.LastOrDefault().DtMovimentacao.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        //Criar Fundamental
                        //Altera para atual = 0 o ensino médio
                        Classes.EnsinoAluno.AtualizarEnsinoAluno(
                            _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Ensino == Enumeradores.Ensino.MÉDIO),
                            _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Ensino == Enumeradores.Ensino.MÉDIO).DtInicio,
                            _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Ensino == Enumeradores.Ensino.MÉDIO).DtTermino,
                            false);

                        //Criar Ensino Fundamental
                        _aluno.InserirEnsinoAluno(Enumeradores.Ensino.FUNDAMENTAL, dtpDtMatriculaDoEnsinoAluno.Text);

                        lblSituacao.Text = "ATIVO";
                        lblDataMovimentacao.Text = "";
                    }
                }
                else //Fundamental -> Médio - CONCLUSÃO DO FUNDAMENTAL
                {
                    if (_aluno.ListaEnsinoAluno.
                        FirstOrDefault(en => en.Atual).Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    {
                        if (_aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).DtTermino != null)
                        {
                            if (MessageBox.Show("O Ensino atual do aluno esta sendo alterado. " +
                                "O Aluno já concluiu o ensino fundamental?", "E-matrícula: Alteração de Ensino", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                //Concluir ensinoFundamental
                                _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).InserirMovimentacaoNoEnsinoAluno(
                                    Enumeradores.SituacaoAluno.CONCLUINTE,
                                    "ENSINO CONCLUÍDO",
                                    DateTime.Now);

                                //Altera para atual = 0 o ensino fundamntal
                                Classes.EnsinoAluno.AtualizarEnsinoAluno(_aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual),
                                    _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).DtInicio,
                                    _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).DtTermino,
                                    false);

                                //Setar data de início de matrícula no ensino 
                                dtpDtMatriculaDoEnsinoAluno.Text = DateTime.Now.ToString("dd/MM/yyyy");

                                //Criar Ensino Médio
                                _aluno.InserirEnsinoAluno(Enumeradores.Ensino.MÉDIO, dtpDtMatriculaDoEnsinoAluno.Text);

                                lblSituacao.Text = "ATIVO";
                                lblDataMovimentacao.Text = "";

                            }
                            else
                            {

                            }
                        }
                    }
                }

                /*
                //Aqui o aluno esta no Fundamental e está sendo alterado para médio
                if (objAluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                {
                    if (cmbEnsino.Text == "MÉDIO")
                    {
                        if (objAluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).DtTermino != null)
                        {
                            if (MessageBox.Show("O Ensino atual do aluno esta sendo alterado. " +
                                "O Aluno já concluiu o ensino fundamental?", "E-matrícula: Alteração de Ensino", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                //Concluir ensinoFundamental
                                objAluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).InserirMovimentacaoNoEnsinoAluno(
                                    Enumeradores.SituacaoAluno.CONCLUINTE,
                                    "ENSINO CONCLUÍDO",
                                    DateTime.Now);

                                //Altera para atual = 0 o ensino fundamntal
                                Classes.EnsinoAluno.AtualizarEnsinoAluno(objAluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual),
                                    objAluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).DtInicio,
                                    objAluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).DtTermino,
                                    false);

                                //Setar data de 
                                dtpDtMatriculaDoEnsinoAluno.Text = DateTime.Now.ToString("dd/MM/yyyy");

                                //Criar Ensino Médio
                                objAluno.InserirEnsinoAluno(Enumeradores.Ensino.MÉDIO, dtpDtMatriculaDoEnsinoAluno.Text);

                                lblSituacao.Text = "ATIVO";
                                lblDataMovimentacao.Text = "";

                            }
                            else
                            {

                            }
                        }
                    }
                }
                else
                {
                    //Aqui o aluno esta no médio e esta sendo alterado para fundamental
                }
                */
            }

            lblEnsino.ForeColor = Color.Black;

            PreencherOpcoesTermoSistema();

            PreencherOpcoesDisciplinaSistema();

            btnSalva.Enabled = true;
        }

        private void PreencherSituacaoEnsinoAtual()
        {
            //Carrega data da matrícula no ensino atual
            dtpDtMatriculaDoEnsinoAluno.Text = _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).DtInicio;

            //Carrega última presença no ensino atual
            if (_aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual)
                        .ListaDisciplinaAluno.FirstOrDefault(di => di.Atual)
                        .ListaAtendimentoAluno.Count > 0)
            {
                lblPresenca.Text = _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual)
                        .ListaDisciplinaAluno.FirstOrDefault(di => di.Atual)
                        .ListaAtendimentoAluno.Last().DtDoAtendimento.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                lblPresenca.Text = "NENHUMA PRESENÇA";
            }

            //Carrega situação atual do ensino atual
            if (_aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).ListaMovimentacao.Count > 0)
            {
                //Situação do ensino aluno
                lblSituacao.Text = _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).ListaMovimentacao.Last().SituacaoAluno.ToString();
                //Data que ocorreu a movimentação do ensino atual
                lblDataMovimentacao.Text =
                    _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).ListaMovimentacao.Last().DtMovimentacao.ToString("dd/MM/yyyy");
                //Se ensino atual estiver concluído, trava rematrícula
                if (_aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).ListaMovimentacao.Last().SituacaoAluno == Enumeradores.SituacaoAluno.ATIVO)
                {

                    btRematricula.Enabled = true;
                }
                else if (_aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).ListaMovimentacao.Last().SituacaoAluno == Enumeradores.SituacaoAluno.CONCLUINTE)
                {
                    btRematricula.Enabled = false;
                }

            }
            else
            {
                lblSituacao.Text = "INFORMAR SITUAÇÃO";
                lblDataMovimentacao.Text = "";
            }

            btnMovimentar.Enabled = true;
            btnEliminacoes.Enabled = true;
        }

        private void PreencherOpcoesTermoSistema()
        {
            cmbTermo.Enabled = true;
            cmbTermo.Items.Clear();
            if (_settings.Ceeja.ToLower() == "sorocaba")
            {
                if (cmbEnsino.Text == "FUNDAMENTAL")
                {
                    cmbTermo.Items.Add("1º TERMO");
                    cmbTermo.Items.Add("2º TERMO");
                    cmbTermo.Items.Add("3º TERMO");
                    cmbTermo.Items.Add("4º TERMO");
                }
                else
                {
                    cmbTermo.Items.Add("1º TERMO");
                    cmbTermo.Items.Add("2º TERMO");
                    cmbTermo.Items.Add("3º TERMO");
                }
            }
            else if (_settings.Ceeja.ToLower() == "americana")
            {
                if (cmbEnsino.Text == "FUNDAMENTAL")
                {
                    cmbTermo.Items.Add("1º TERMO");
                    cmbTermo.Items.Add("2º TERMO");
                    cmbTermo.Items.Add("3º TERMO");
                    cmbTermo.Items.Add("4º TERMO");
                }
                else
                {
                    cmbTermo.Items.Add("1º TERMO");
                    cmbTermo.Items.Add("2º TERMO");
                    cmbTermo.Items.Add("3º TERMO");
                }

            }
            else if (_settings.Ceeja.ToLower() == "votorantim")
            {

                if (cmbEnsino.Text == "FUNDAMENTAL")
                {
                    cmbTermo.Items.Add("6º ANO");
                    cmbTermo.Items.Add("7º ANO");
                    cmbTermo.Items.Add("8º ANO");
                    cmbTermo.Items.Add("9º ANO");
                }
                else
                {
                    cmbTermo.Items.Add("1º ANO");
                    cmbTermo.Items.Add("2º ANO");
                    cmbTermo.Items.Add("3º ANO");
                }

            }
            cmbTermo.SelectedIndex = -1;
        }

        private void PreencherOpcoesDisciplinaSistema()
        {
            cmbDisciplina.Enabled = true;

            if (cmbEnsino.Text == "FUNDAMENTAL")
            {
                cmbDisciplina.DataSource = Classes.EnsinoSistema.ensinoFundamental().ListaDisciplinas;
                cmbDisciplina.DisplayMember = "Nome";
                cmbDisciplina.ValueMember = "Codigo";
                cmbDisciplina.SelectedIndex = -1;
            }
            else
            {
                cmbDisciplina.DataSource = Classes.EnsinoSistema.ensinoMedio().ListaDisciplinas;
                cmbDisciplina.DisplayMember = "Nome";
                cmbDisciplina.ValueMember = "Codigo";
                cmbDisciplina.SelectedIndex = -1;
            }
        }

        private void FRcadaluno_Shown(object sender, EventArgs e)
        {
            txtNmat.Focus();
            this.WindowState = FormWindowState.Maximized;
        }

        //TODO:: vERIRIFICAR SE IRÁ MANTER CÓDIGO DE MUDANÇA DE LOCILIZAÇÇA DE CONTRONELOES DO FORM DE ALUNOS
        /*
        private void moverControles()
        {
            GroupBox gb = new GroupBox();

            int linha = 25;
            int avanco = 32;

            if (Configurações.csEscola.cidade.ToLower() == "sorocaba")
            {
                gpbEmprego.Visible = false;

                lblCpf.Visible = false;
                txtCpf.Visible = false;

                lblPai.Visible = false;
                txtPai.Visible = false;

                ckbHistorico.Visible = true;
                ckbCertidao.Visible = true;

                ckbHistorico.Enabled = true;
                ckbCertidao.Enabled = true;

                #region gbAlunos
                lblNmat.Location = new Point(6, linha);
                lblRa.Location = new Point(226, linha); linha = linha + avanco;
                lblRg.Location = new Point(6, linha);
                lblExpRg.Location = new Point(252, linha);
                lblUfRg.Location = new Point(443, linha);
                lblOrgao.Location = new Point(516, linha); linha = linha + avanco;
                lblAluno.Location = new Point(6, linha); linha = linha + avanco;
                lblNomeSocial.Location = new Point(6, linha); linha = linha + avanco;
                lblMae.Location = new Point(6, linha); linha = linha + avanco;
                lblRaca.Location = new Point(6, linha); linha = linha + avanco;
                lblSexo.Location = new Point(6, linha); linha = linha + avanco;
                lblEstadoCivil.Location = new Point(6, linha); linha = linha + avanco;
                

                linha = 29;
                txtNmat.Location = new Point(122, linha);
                txtRa.Location = new Point(264, linha); linha = linha + 32;
                txtRg.Location = new Point(122, linha);
                dtpExpRg.Location = new Point(331, linha);
                txtUfRg.Location = new Point(480, linha);
                txtOrgao.Location = new Point(575, linha); linha = linha + 32;
                txtAluno.Location = new Point(122, linha); linha = linha + 32;
                txtNomeSocial.Location = new Point(122, linha); linha = linha + 32;
                txtMae.Location = new Point(122, linha); linha = linha + 32;
                cmbRaca.Location = new Point(122, linha); linha = linha + 32;
                cmbSexo.Location = new Point(122, linha); linha = linha + 32;
                cmbEstadoCivil.Location = new Point(122, linha); linha = linha + 32;
                

                gbAluno.Height = cmbEstadoCivil.Location.Y + avanco + 5; 
                //Defini o tamanho do groupbox com base na posição do ultimo controle

                #endregion

            }
            if (Configurações.csEscola.cidade.ToLower() == "americana")
            {

                ckbHistorico.Visible = false;
                ckbCertidao.Visible = false;

                ckbHistorico.Enabled = false;
                ckbCertidao.Enabled = false;

                #region gbAluno
                lblNmat.Location = new Point(6, linha);
                lblRa.Location = new Point(226, linha); linha = linha + avanco;
                lblRg.Location = new Point(6, linha);
                lblExpRg.Location = new Point(252, linha);
                lblUfRg.Location = new Point(443, linha);
                lblOrgao.Location = new Point(516, linha); linha = linha + avanco;
                lblCpf.Location = new Point(6, linha); linha = linha + avanco;
                lblAluno.Location = new Point(6, linha); linha = linha + avanco;
                lblNomeSocial.Location = new Point(6, linha); linha = linha + avanco;
                lblMae.Location = new Point(6, linha); linha = linha + avanco;
                lblPai.Location = new Point(6, linha); linha = linha + avanco;
                lblRaca.Location = new Point(6, linha); linha = linha + avanco;
                lblSexo.Location = new Point(6, linha); linha = linha + avanco;
                lblEstadoCivil.Location = new Point(6, linha); linha = linha + avanco;
                

                linha = 29;
                txtNmat.Location = new Point(122, linha);
                txtRa.Location = new Point(264, linha); linha = linha + 32;
                txtRg.Location = new Point(122, linha); 
                dtpExpRg.Location = new Point(331, linha);
                txtUfRg.Location = new Point(480, linha);
                txtOrgao.Location = new Point(579, linha); linha = linha + 32;
                txtCpf.Location = new Point(122, linha); linha = linha + 32;
                txtAluno.Location = new Point(122, linha); linha = linha + 32;
                txtNomeSocial.Location = new Point(122, linha); linha = linha + 32;
                txtMae.Location = new Point(122, linha); linha = linha + 32;
                txtPai.Location = new Point(122, linha); linha = linha + 32;
                cmbRaca.Location = new Point(122, linha); linha = linha + 32;
                cmbSexo.Location = new Point(122, linha); linha = linha + 32;
                cmbEstadoCivil.Location = new Point(122, linha); linha = linha + 32;                

                //Defini o tamanho do groupbox com base na posição do ultimo controle
                gbAluno.Height = cmbEstadoCivil.Location.Y + avanco + 5;

                #endregion

            }
        }
        */

        void comboBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private bool verificar_campos()
        {
            //Definir os campos que será obrigados para efetuar o cadastro

            DateTime dt = new DateTime();

            if (txtNmat.Text == string.Empty)
            {
                MessageBox.Show("Favor preencher o Campo de Matricula!", "E-matrícula: Falha na Matrícula");
                return false;
            }
            if (txtRg.Text == string.Empty)
            {
                MessageBox.Show("Favor preencher o Campo RG!", "E-matrícula: Falha na Matrícula");
                return false;
            }

            if (txtAluno.Text == string.Empty)
            {
                //.ForeColor = Color.Red;
                MessageBox.Show("Favor informar o nome do Aluno.", "E-matrícula: Falha na Matrícula");
                return false;
            }

            if (cmbRaca.SelectedIndex == -1)
            {
                //.ForeColor = Color.Red;
                MessageBox.Show("Favor informar a Cor / Origem Étnica do Aluno.", "E-matrícula: Falha na Matrícula");
                return false;
            }

            if (cmbSexo.SelectedIndex == -1)
            {
                //.ForeColor = Color.Red;
                MessageBox.Show("Favor informar o Sexo do Aluno.", "E-matrícula: Falha na Matrícula");
                return false;
            }

            if (cmbEstadoCivil.SelectedIndex == -1)
            {
                //.ForeColor = Color.Red;
                MessageBox.Show("Favor informar o Estado Civil do Aluno.", "E-matrícula: Falha na Matrícula");
                return false;
            }

            if (cmbDisciplina.SelectedIndex == -1)
            {
                //.ForeColor = Color.Red;
                MessageBox.Show("Favor informar uma Disciplina.", "E-matrícula: Falha na Matrícula");
                return false;
            }

            if (cmbTermo.SelectedIndex == -1)
            {
                //.ForeColor = Color.Red;
                MessageBox.Show("Favor informar o Ano.", "E-matrícula: Falha na Matrícula");
                return false;
            }

            if (!DateTime.TryParse(dtpdatnasc.Text, out dt))
            {
                MessageBox.Show("Favor informar uma data de nascimento válida.", "E-matrícula: Falha na Matrícula");
                return false;
            }

            if (!DateTime.TryParse(dtpDtMatriculaDoEnsinoAluno.Text, out dt))
            {
                MessageBox.Show("Favor informar uma data de matrícula válida.", "E-matrícula: Falha na Matrícula");
                return false;
            }

            if (cmbEnsino.Text == string.Empty)
            {
                lblEnsino.ForeColor = Color.Red;
                MessageBox.Show("Favor Selecionar o Ensino.", "E-matrícula: Falha na Matrícula");
                return false;
            }

            if (_settings.Ceeja.ToLower() == "sorocaba")
            {
                if (!ckbCertidao.Checked)
                {
                    MessageBox.Show("Falta a Certidão!", "E-matrícula: Falha na Matrícula");
                    return false;
                }
                if (!ckbHistorico.Checked)
                {
                    MessageBox.Show("Falta o Histórico!", "E-matrícula: Falha na Matrícula");
                    return false;
                }
            }
            return true;
        }

        private bool GravarAluno()
        {
            try
            {
                txtCelular.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                txtTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                txtCep.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                mtbCpf.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                #region Obter Dados Formulario

                if (cadastro)
                {
                    _aluno.DtMatricula = DateTime.Parse(dtpDtMatriculaDoEnsinoAluno.Text);
                }
                else
                {
                    if ((_aluno.DtMatricula.ToString("dd/MM/yyyy") != dtpDtMatriculaDoEnsinoAluno.Text)
                        && _aluno.ListaEnsinoAluno.Count == 1)
                    {
                        _aluno.DtMatricula = DateTime.Parse(dtpDtMatriculaDoEnsinoAluno.Text);
                    }
                }

                //Aluno
                _aluno.NMatricula = Convert.ToInt32(txtNmat.Text);
                _aluno.Ra = txtRa.Text;
                _aluno.Rg = txtRg.Text;
                _aluno.UfRg = txtUfRg.Text;
                _aluno.Cpf = mtbCpf.Text;
                _aluno.Nome = txtAluno.Text.Trim();
                _aluno.NomeSocial = txtNomeSocial.Text;
                _aluno.NomeMae = txtMae.Text;
                _aluno.NomePai = txtPai.Text;
                Enumeradores.CorOrigemEtnica corOrigemEtnica;
                Enum.TryParse<Enumeradores.CorOrigemEtnica>(cmbRaca.SelectedValue.ToString(), out corOrigemEtnica);
                _aluno.CorOrigemEtnica = corOrigemEtnica;
                Enumeradores.Sexo sexo;
                Enum.TryParse<Enumeradores.Sexo>(cmbSexo.SelectedValue.ToString(), out sexo);
                _aluno.Sexo = sexo;
                Enumeradores.EstadoCivil estadoCivil;
                Enum.TryParse<Enumeradores.EstadoCivil>(cmbEstadoCivil.SelectedValue.ToString(), out estadoCivil);
                _aluno.EstadoCivil = estadoCivil;
                _aluno.DtRg = dtpExpRg.Text;
                _aluno.OrgaoRg = txtOrgao.Text;

                //Situação
                _aluno.Ativo = true; //TODO:: REMOVER QUANDO NÃO HAVER MAIS ESSE ATRIBUTO
                _aluno.Usuario = Utils.csUsuarioLogado.usuario;
                _aluno.TermoMatricula = cmbTermo.Text;
                _aluno.Observacao = txtObs_passporte.Text;
                _aluno.ApresentouCertificado = ckbCertidao.Checked;
                _aluno.ApresentouHistorico = ckbHistorico.Checked;

                //Endereco
                if (_aluno.Endereco != null)
                {
                    if (Int32.TryParse(txtNumero.Text, out int numero))
                        _aluno.Endereco.Numero = numero;
                    _aluno.Endereco.Cep = txtCep.Text;
                    _aluno.Endereco.Logradouro = txtLogradouro.Text;
                    _aluno.Endereco.Bairro = txtBairro.Text;
                    _aluno.Endereco.Complemento = txtComplemento.Text;
                }

                //Contato
                _aluno.Telefone = txtTelefone.Text;
                _aluno.Celular = txtCelular.Text;
                _aluno.Email = txtEmail.Text;

                //Emprego
                if (_aluno.Emprego != null)
                {
                    _aluno.Emprego.NomeEmpresa = txtNomeEmpresa.Text;
                    _aluno.Emprego.Telefone = txtTelefoneEmprego.Text;
                }

                _aluno.DtNascimento = dtpdatnasc.Text;


                string msgFalha;
                if (cadastro)
                    msgFalha = "Falha na matrícula";
                else
                    msgFalha = "Falha na alteração";

                #endregion


                #region Gravação

                _aluno.GravarAluno();

                if (cadastro)
                {
                    _aluno.InserirEnsinoAluno((Enumeradores.Ensino)cmbEnsino.SelectedValue, dtpDtMatriculaDoEnsinoAluno.Text);
                }
                else
                {
                    Classes.EnsinoAluno.AtualizarEnsinoAluno(_aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual),
                        dtpDtMatriculaDoEnsinoAluno.Text, "", true);
                }

                //Verificar se irá alterar algo no ensino quando é gravação de dados do aluno
                //Disciplina
                if (!_aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).
                    ListaDisciplinaAluno.Exists(x => x.Disciplina.Codigo == ((Classes.Disciplina)cmbDisciplina.SelectedItem).Codigo))
                {
                    Classes.DisciplinaAluno disciplinaAluno = new Classes.DisciplinaAluno();
                    disciplinaAluno.Disciplina = (Classes.Disciplina)cmbDisciplina.SelectedItem;
                    disciplinaAluno.Atual = true;
                    disciplinaAluno.Codigo = 0;
                    disciplinaAluno.Concluida = false;
                    disciplinaAluno.EnsinoAluno = _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual);
                    _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).ListaDisciplinaAluno.Add(disciplinaAluno);
                    //Setar DisciplinaAluno como atual
                    Classes.Aluno.setDisciplinaAlunoAtual(_aluno, (Classes.Disciplina)cmbDisciplina.SelectedItem, cadastro);
                }
                else
                {
                    //Setar DisciplinaAluno como atual
                    Classes.Aluno.setDisciplinaAlunoAtual(_aluno, (Classes.Disciplina)cmbDisciplina.SelectedItem, cadastro);

                }

                //Disciplina
                if (DAO.DisciplinaAlunoDAO.Gravar(Classes.Aluno.GetDisciplinaAlunoAtual(_aluno)) == 0)
                {
                    MessageBox.Show(msgFalha + " - Disciplina Aluno", "E-matrícula: Matrícula!");
                }
                else
                {
                    #region Criar todas as DisciplinaAluno do Ensino Atual 
                    for (int i = 0; i < EnsinoSistemaEstatica.getListaEnsinosSistema.Count; i++)
                    {
                        if (EnsinoSistemaEstatica.getListaEnsinosSistema[i].Ensino == _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).Ensino)
                        {
                            for (int z = 0; z < EnsinoSistemaEstatica.getListaEnsinosSistema[i].ListaDisciplinas.Count; z++)
                            {
                                if (_aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).ListaDisciplinaAluno.FindIndex(x => x.Disciplina.Codigo ==
                                     EnsinoSistemaEstatica.getListaEnsinosSistema[i].ListaDisciplinas[z].Codigo) < 0)
                                {
                                    //Não encontrou, deve adicionar uma nova disciplina aluno....
                                    Classes.DisciplinaAluno disciplinaAluno = new Classes.DisciplinaAluno();
                                    disciplinaAluno.Codigo = 0;
                                    disciplinaAluno.Atual = false;
                                    disciplinaAluno.Concluida = false;
                                    disciplinaAluno.Disciplina = EnsinoSistemaEstatica.getListaEnsinosSistema[i].ListaDisciplinas[z];
                                    disciplinaAluno.EnsinoAluno = _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual);
                                    _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).ListaDisciplinaAluno.Add(disciplinaAluno);
                                    DAO.DisciplinaAlunoDAO.Inserir(disciplinaAluno);
                                }
                            }
                        }
                    }
                    #endregion
                }

                //TODO:: Melhorar validação do retorno, pois caso de algum erro quebra todo o resto do processo de gravação
                if (_aluno.Endereco != null)
                {
                    if (_aluno.Endereco.Cidade != null)
                    {
                        if (_aluno.Endereco.Cidade.Codigo != 0)
                        {
                            DAO.EnderecoDAO.Gravar(_aluno);
                        }
                    }
                }
                //TODO:: Melhorar validação do retorno, pois caso de algum erro quebra todo o resto do processo de gravação
                if (_aluno.LocalNascimento != null)
                {
                    if (_aluno.LocalNascimento.Cidade != null)
                    {
                        if (_aluno.LocalNascimento.Cidade.Codigo != 0)
                        {
                            DAO.LocalNascimentoDAO.Gravar(_aluno);
                        }
                    }
                }
                //TODO:: Melhorar validação do retorno, pois caso de algum erro quebra todo o resto do processo de gravação
                if (_aluno.Emprego != null)
                {
                    if (_aluno.Emprego.NomeEmpresa != "" && _aluno.Emprego.Cidade != null)
                    {
                        if (_aluno.Emprego.Cidade.Codigo != 0)
                        {
                            DAO.EmpregoDAO.Gravar(_aluno);
                        }
                    }
                }



                //TODO: FUTURO 2 - GRAVAR NA TABELA CARTAO_ACESSO AO CADASTRAR ALUNO
                //Alterar depois para todos os ceejas que tem catraca
                //As configurações de criação de cartão se mantem igual, porem na classe de controle da catraca
                //será diferente o mode de configuração de "modo de funcionamento" tanto online quanto offline
                if (_settings.Ceeja.ToLower() == "votorantim")
                {
                    if (cadastro)
                    {
                        GravarCartaoAcesso(_aluno.NMatricula);

                        #region Entity FrameWork isolado por erro de insert em identity
                        ////Apenas para registros novos
                        //using (var context = new Modelo.Modelo())
                        //{
                        //    var novoCodigo = context.
                        //
                        //
                        //    var cartao = new Modelo.CARTAO_ACESSO() { ALUNO = objAluno.NMatricula, ESTADO = false, PROFESSOR = null };
                        //
                        //    //var cartao = context.Set<Modelo.CARTAO_ACESSO>();
                        //    cartao(new Modelo.CARTAO_ACESSO { ALUNO = objAluno.NMatricula, ESTADO = false, PROFESSOR = null });
                        //    context.SaveChanges();
                        //}
                        #endregion
                    }
                    else
                    {
                        if (!VerificaCartaoAcesso(_aluno.NMatricula))
                        {
                            GravarCartaoAcesso(_aluno.NMatricula);
                        }
                    }
                }
                else if (_settings.Ceeja.ToLower() == "sorocaba")
                {
                    int ano = Convert.ToInt32(DateTime.Parse(_aluno.DtNascimento).ToString("yy"));
                    int cartao = int.Parse(_aluno.NMatricula.ToString() + ano.ToString());

                    if (cadastro)
                    {
                        GravarCartaoAcesso(cartao);
                        //using (var context = new Modelo.Modelo())
                        //{
                        //    var cartao = context.Set<Modelo.CARTAO_ACESSO>();
                        //    cartao.Add(new 
                        //Modelo.CARTAO_ACESSO { ALUNO = int.Parse(objAluno.NMatricula.ToString() + ano.ToString()), ESTADO = false, PROFESSOR = null });
                        //    context.SaveChanges();
                        //}
                    }
                    else
                    {
                        if (!VerificaCartaoAcesso(_aluno.NMatricula))
                        {
                            GravarCartaoAcesso(cartao);
                        }
                    }

                }

                //if(DAO.DeficienciaPessoaDAO.Gravar())
                if (cadastro)
                {
                    MessageBox.Show("Matrícula efetuada com Sucesso.", "E-matrícula: Matrícula!");
                    cadastro = false;
                }
                else
                {
                    MessageBox.Show("Alteração efetuada com Sucesso.", "E-matrícula: Matrícula!");
                }



                return true;
                #endregion
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                throw;
            }
        }

        public void GravarCartaoAcesso(int numeroCartaoAluno)
        {
            //TODO:: MARCAR NA PLANILHA DE MODIFICAÇÕES
            //ADO
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO CARTAO_ACESSO VALUES (@aluno, NULL, 0, 0)", sqlHelper.ematConn);
            sqlCommand.Parameters.AddWithValue("@aluno", numeroCartaoAluno);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            try
            {
                sqlHelper.ematConn.Open();
                sqlCommand.ExecuteNonQuery();

                sqlHelper.ematConn.Close();
            }
            catch (Exception)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError("Falha na inclusão do cartão de acesso do aluno.");
                throw;
            }
        }

        public bool VerificaCartaoAcesso(int numeroCartaoAluno)
        {
            bool retorno;
            //se for edição de cadastro, verificar se ja existe um cartão do aluno
            //TODO:: MARCAR NA PLANILHA DE MODIFICAÇÕES
            //ADO
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM CARTAO_ACESSO WHERE ALUNO = @aluno", sqlHelper.ematConn);
            sqlCommand.Parameters.AddWithValue("@aluno", numeroCartaoAluno);
            sqlCommand.CommandType = System.Data.CommandType.Text;

            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                retorno = reader.Read(); //se não existir registro irá inserir...
                sqlHelper.ematConn.Close();
                return retorno;
            }
            catch (Exception)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError("Falha na leitura do banco.");
                throw;
            }
        }

        private void travar_campos()
        {
            //Trava Controles
            foreach (GroupBox grb in flowLayoutPanel2.Controls)
            {
                foreach (Control ctr in grb.Controls.OfType<Control>())
                {
                    ctr.Enabled = false;
                }
            }
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (verificar_campos())
                if (GravarAluno())
                {
                    //Gravado
                    txtNmat.Enabled = false;
                    btnSalva.Enabled = false;
                    btnImprimir.Enabled = true;
                    btnEliminacoes.Enabled = true;
                    //TODO: FUTURO 1 - O BOTÃO DE LIBERAR ELIMINAÇÕES É LIBERADO 
                    //APENAS QUANDO O ALUNO É SALVO POIS AS DISCIPLINAS_ALUNO SÃO CRIADAS NESTE MOMENTO.
                    //TEMTAR MELHORAR



                }
                else
                {
                    //Não Gravado
                    btnSalva.Enabled = true;
                }


            Cursor.Current = Cursors.Default;
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            frmProcurarAluno frmProcurarAluno = new frmProcurarAluno("aluno", _settings);
            //frmProcurarAluno._retornoDeFormulario = "aluno";
            frmProcurarAluno.Show();
            this.Close();
        }

        private void btimprimir_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnImprimir.Enabled = false;

            frmImprimir form = new frmImprimir(_settings);
            form.cidade = cmbCidadeEndereco.Text;
            form.estaddo = cmbUfEndereco.Text;
            form.objAluno = _aluno;

            form.Show();

            btnImprimir.Enabled = true;

            Cursor.Current = Cursors.Default;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFicha_Click(object sender, EventArgs e)
        {


        }

        private void btnPassaporte_Click(object sender, EventArgs e)
        {
            frmPassaporte frmPassaporte = new frmPassaporte(_settings);
            frmPassaporte.objAluno = _aluno;
            frmPassaporte.ShowDialog();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                //Obter ultimo número
                //var context = new Modelo.Modelo();
                //var aluno = Utils.Settings.Context.ALUNO.OrderByDescending(a => a.N_MAT).FirstOrDefault();

                //MessageBox.Show(aluno.N_MAT.ToString());

                //int ultimoNMatricula = aluno.N_MAT;
                //if(objAluno.NMatricula > ultimoNMatricula)
                //{
                frmExcluirAluno frmExcluirAluno = new frmExcluirAluno(_aluno, _settings);
                frmExcluirAluno.ShowDialog();

                if (frmExcluirAluno.Excluido)
                {
                    this.Close();
                }
                //}
                //else
                //{
                //    MessageBox.Show("Exclusão do registro não permitido.","Ematrícula");
                //}
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void ptbFoto_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (txtNmat.Enabled == false)
            {
                frmObterFotos frmObterFotos = new frmObterFotos(_settings);

                frmObterFotos.nome_form_solicitante = this.Name;
                frmObterFotos.numat = txtNmat.Text;
                frmObterFotos.Owner = this;
                frmObterFotos.cad_edit = 0;
                frmObterFotos.ShowDialog();
                retorno_pos_foto();
            }
            else
            {
                MessageBox.Show("É necessario Salvar o Cadastro antes de obter a foto.", "E-matrícula: Obter Foto.");
            }

            Cursor.Current = Cursors.Default;
        }

        private void verfica_digital()
        {
            //grava.Command.CommandText =
            //    @"SELECT * FROM ACESSO_CATRACA WHERE ID_CARTAO = @n_mat";


            //grava.Command.Parameters.AddWithValue("@n_mat", txtNmat.Text);


            //bool connected = conn.State == ConnectionState.Open;
            //try
            //{
            //    string template = "";
            //    if (!connected) grava.Connection.Open();
            //    SqlDataReader reader = grava.Command.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        template = reader["TEMPLATE1"].ToString();

            //    }
            //    if (template == "")
            //    {
            //        btobterdigital.Enabled = true;
            //        txtDigital.ForeColor = Color.DarkRed;
            //        txtDigital.Text = "DIGITAL NÃO CADASTRADA";
            //        //cad_dig = 1;
            //    }
            //    else
            //    {
            //        btobterdigital.Enabled = false;
            //        txtDigital.ForeColor = Color.Black;
            //        txtDigital.Text = "DIGITAL CADASTRADA";
            //    }
            //    reader.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    grava.Command.Parameters.Clear();
            //    grava.Connection.Close();
            //    //if (cad_dig == 1)
            //    //{
            //    //    cadastrar_aluno_catraca();
            //    //}
            //    //cad_dig = 0;



            //}
        }

        private void btobterdigital_Click(object sender, EventArgs e)
        {

        }

        private void retorno_pos_foto()
        {
            //Carrega a foto após ser tirada.
            txtNmat.Enabled = false;

            _aluno.FotoDoAluno = new Classes.FotoAluno(_aluno.NMatricula, _settings);

            if (_aluno.FotoDoAluno != null)
            {
                ptbFoto.Image = _aluno.FotoDoAluno.Foto;
            }
            ptbFoto.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void txtNmat_Leave(object sender, EventArgs e)
        {
            if (txtNmat.Text != string.Empty)
            {
                if (DAO.AlunoDAO.Consultar(Convert.ToInt32(txtNmat.Text)) != null)
                {
                    //TODO: AGORA - BLOQUEAR EVENTO TEXT CHANGE
                    MessageBox.Show("Número " + txtNmat.Text + " em uso!", "E-matrícula: Falha na Matrícula");
                    //txtNmat.Text = "";
                    txtNmat.Focus();
                }
            }
        }

        private void txtRg_Leave(object sender, EventArgs e)
        {
            if (txtRg.Text != string.Empty) //Verifica se o campo rg esta vazio
            {
                string rg = "";
                if (!cadastro)
                {
                    rg = _aluno.Rg;
                }

                if (rg != txtRg.Text)// Verifica se ocorreu alteração
                {
                    if (DAO.AlunoDAO.Consultar(0, txtRg.Text) != null) //Verifica se o rg digitado esta cadastrado
                    {
                        MessageBox.Show("RG:" + txtRg.Text + " já cadastrado!", "E-matrícula: Falha na Matrícula");
                        //txtRg.Text = "";
                        txtRg.Focus();
                        return;
                    }
                }
            }
        }

        private void txtNmat_EnabledChanged(object sender, EventArgs e)
        {
            if (!txtNmat.Enabled)
            {
                btnImprimir.Enabled = true;
            }
        }

        private void nmattxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        private void estadocivilcmb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.ToString() == "TAB")
            {
                //ckbPossuiNecessidade.Focus();
            }
        }

        private void txtNmat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void btRematricula_Click(object sender, EventArgs e)
        {
            try
            {
                //Ativa aluno na tabela ALUNO
                _aluno.Ativo = true;
                DAO.AlunoDAO.Gravar(_aluno);


                //Inserir Rematrícula
                Classes.Rematricula rematricula = new Classes.Rematricula();
                rematricula.Codigo = 0;
                rematricula.Data = DateTime.Now;
                rematricula.Usuario = Utils.csUsuarioLogado.usuario;
                rematricula.EnsinoAluno = _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual);
                DAO.RematriculaDAO.Gravar(rematricula);
                _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).ListaRematricula.Add(rematricula);

                //Bind
                //Rematriculas
                ltbRematriculas.DataSource = null;
                ltbRematriculas.DataSource = _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).ListaRematricula.OrderByDescending(x => x.Data).ToList();
                ltbRematriculas.DisplayMember = "Data";

                //Inserir Movimentação
                _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).InserirMovimentacaoNoEnsinoAluno(
                    Enumeradores.SituacaoAluno.ATIVO, "REMATRÍCULA", DateTime.Now);




            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                throw;
            }
        }

        //private void InserirMovimentacao(Enumeradores.SituacaoAluno situacaoAluno, string motivo)
        //{
        //    Classes.Movimentacao movimentacao = new Classes.Movimentacao();
        //    movimentacao.SituacaoAluno = situacaoAluno;
        //    movimentacao.EnsinoAluno = objAluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual);
        //    movimentacao.Usuario = Utils.csUsuarioLogado.usuario;
        //    movimentacao.DtMovimentacao = DateTime.Now;
        //    movimentacao.Motivo = motivo;
        //    DAO.MovimentacaoDAO.InserirMovimentacao(movimentacao);
        //    objAluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).ListaMovimentacao.Add(movimentacao);
        //
        //    //Exibe alteração
        //    lblSituacao.Text = movimentacao.SituacaoAluno.ToString();
        //    lblDataMovimentacao.Text = movimentacao.DtMovimentacao.ToString("dd/MM/yyyy");
        //}

        private void btnMovimentar_Click(object sender, EventArgs e)
        {
            frmMovimentacao frMovimentacao = new frmMovimentacao();
            frMovimentacao.objAluno = _aluno;
            frMovimentacao.ensinoAluno = _aluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual);
            DialogResult result = frMovimentacao.ShowDialog();

            if (result == DialogResult.OK)
            {
                lblSituacao.Text = frMovimentacao.movimentacao.SituacaoAluno.ToString();
                lblDataMovimentacao.Text = frMovimentacao.movimentacao.DtMovimentacao.ToString("dd/MM/yyyy");
            }
        }

        private void btnEliminacoes_Click(object sender, EventArgs e)
        {
            frmEliminacoes frmEliminacoes = new frmEliminacoes(_settings);
            frmEliminacoes.aluno = _aluno;
            //TODO:: Verificar o que isso no método EliminacoesClick frmEliminacoes.csConfiguracoes = csConfiguracoes;
            frmEliminacoes.ShowDialog();

        }

        private void btnAutoAtribuicao_Click(object sender, EventArgs e)
        {
            Formularios.frAtribuicaoDisciplina form = new Formularios.frAtribuicaoDisciplina();
            form.objAluno = _aluno;
            form.ShowDialog();
        }



        private void cmbDisciplinaAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void cmbPaisNascimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaisNascimento.SelectedIndex > -1)
            {
                cmbUfNascimento.SelectedIndexChanged -= cmbUfNascimento_SelectedIndexChanged;
                BindingSource bsUfNascimento = new BindingSource();
                //List<Classes.Uf> listaUfNascimento = DAO.UfDAO.ExibirTodos(); //Da certo?                
                //bsUfNascimento.DataSource = listaUfNascimento;
                //if(listaUfNascimento.Count > 0)
                bsUfNascimento.DataSource = DAO.UfDAO.ExibirTodos((Classes.Pais)cmbPaisNascimento.SelectedItem);
                cmbUfNascimento.DataSource = bsUfNascimento;
                cmbUfNascimento.DisplayMember = "Nome";
                cmbUfNascimento.ValueMember = "Codigo";
                cmbUfNascimento.SelectedIndex = -1;
                cmbUfNascimento.SelectedIndexChanged += cmbUfNascimento_SelectedIndexChanged;
                //Zerar Cidades TODO:: VERIFICAR SE É NECESSÁRIO
                //cmbCidadeNascimento.DataSource = null;
                btnSalva.Enabled = true;
            }
        }

        private void cmbUfNascimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUfNascimento.SelectedIndex > -1)
            {
                cmbCidadeNascimento.SelectedIndexChanged -= cmbCidadeNascimento_SelectedIndexChanged;
                BindingSource bsCidadeNascimento = new BindingSource();
                bsCidadeNascimento.DataSource = DAO.CidadeDAO.ExibirTodos(((Classes.Uf)cmbUfNascimento.SelectedItem));
                cmbCidadeNascimento.DataSource = bsCidadeNascimento;
                cmbCidadeNascimento.DisplayMember = "Nome";
                cmbCidadeNascimento.ValueMember = "Codigo";
                cmbCidadeNascimento.SelectedIndex = -1;
                cmbCidadeNascimento.SelectedIndexChanged += cmbCidadeNascimento_SelectedIndexChanged;
                btnSalva.Enabled = true;
            }
        }

        private void cmbCidadeNascimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCidadeNascimento.SelectedIndex > -1)
            {
                if (_aluno.LocalNascimento == null)
                {
                    _aluno.LocalNascimento = new Classes.LocalNascimento();
                }

                _aluno.LocalNascimento.Cidade = ((Classes.Cidade)cmbCidadeNascimento.SelectedItem);
                btnSalva.Enabled = true;
            }
        }

        private void cmbUfNascimento_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbUfNascimento.Text != string.Empty)
            {
                //gravar - favorito
                //cs_enderecos.grava_end_est_favorito(cmbNascEstado.Text);
            }
        }

        private void cmbNascCidade_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbCidadeNascimento.Text != string.Empty)
            {
                //gravar - favorito
                //cs_enderecos.grava_end_cid_favorito(cmbNascCidade.Text);
            }
        }

        private void cmbCidadeNascimento_Leave(object sender, EventArgs e)
        {
            if (cmbPaisNascimento.Text == "BRASIL")
            {
                if (cmbCidadeNascimento.Text != "")
                {
                    if (cmbCidadeNascimento.FindString(cmbCidadeNascimento.Text) < 0)
                    {
                        MessageBox.Show("Cidade não cadastrada.", "E-matrícula - Cadastro");
                        cmbCidadeNascimento.Text = "";
                        cmbCidadeNascimento.Focus();
                    }
                }
            }
        }

        private void cmbUfNascimento_Leave(object sender, EventArgs e)
        {
            if (cmbPaisNascimento.Text == "BRASIL")
            {
                if (cmbUfNascimento.Text != "")
                {
                    if (cmbUfNascimento.FindString(cmbUfNascimento.Text) < 0)
                    {
                        MessageBox.Show("Estado não cadastrado.", "E-matrícula - Cadastro");
                        cmbUfNascimento.Text = "";
                        cmbUfNascimento.Focus();
                    }
                }
            }
        }

        private void cmbUfNascimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void cmbCidadeNascimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void dtpDatMat_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void cmbUfEndereco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUfEndereco.SelectedIndex > -1)
            {
                cmbCidadeEndereco.SelectedIndexChanged -= cmbCidadeEndereco_SelectedIndexChanged;
                BindingSource bsCidadeEndereco = new BindingSource();
                bsCidadeEndereco.DataSource = DAO.CidadeDAO.ExibirTodos((Classes.Uf)cmbUfEndereco.SelectedItem);
                cmbCidadeEndereco.DataSource = bsCidadeEndereco.DataSource;
                cmbCidadeEndereco.DisplayMember = "Nome";
                cmbCidadeEndereco.ValueMember = "Codigo";
                cmbCidadeEndereco.SelectedIndex = -1;
                cmbCidadeEndereco.SelectedIndexChanged += cmbCidadeEndereco_SelectedIndexChanged;
                btnSalva.Enabled = true;
            }

        }

        private void cmbCidadeEndereco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCidadeEndereco.SelectedIndex > -1)
            {
                if (_aluno.Endereco == null)
                {
                    _aluno.Endereco = new Classes.Endereco();
                }

                _aluno.Endereco.Cidade = (Classes.Cidade)cmbCidadeEndereco.SelectedItem;
                btnSalva.Enabled = true;
            }
        }

        private void cmbUfEndereco_Leave(object sender, EventArgs e)
        {
            if (cmbUfEndereco.Text != "")
            {

                if (cmbUfEndereco.FindString(cmbUfEndereco.Text) < 0)
                {
                    MessageBox.Show("Estado não cadastrado.", "E-matrícula - Cadastro");
                    cmbUfEndereco.Text = "";
                    cmbUfEndereco.Focus();
                }
            }
        }

        private void cmbCidadeEndereco_Leave(object sender, EventArgs e)
        {
            if (cmbCidadeEndereco.Text != "")
            {
                if (cmbCidadeEndereco.FindString(cmbCidadeEndereco.Text) < 0)
                {
                    MessageBox.Show("Cidade não cadastrada.", "E-matrícula - Cadastro");
                    cmbCidadeEndereco.Text = "";
                    cmbCidadeEndereco.Focus();
                }
            }
        }

        private void cmbUfEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void cmbCidadeEndreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        //Click - //TODO:: VERIIFICAR BOTÕES RESIDENCIA - BTNNOVO ENDEREÇO TBM
        private void btnNovoEndereco_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmCadastrarEndereco frmCadastrarEndereco = new frmCadastrarEndereco();
            frmCadastrarEndereco.ShowDialog();

            CarregarPaises();

            Cursor.Current = Cursors.Default;

        }

        private void CarregarPaises()
        {
            cmbPaisNascimento.SelectedIndexChanged -= cmbPaisNascimento_SelectedIndexChanged;
            BindingSource bsPaises = new BindingSource();
            bsPaises.DataSource = DAO.PaisDAO.ExibirTodos();
            cmbPaisNascimento.DataSource = bsPaises;
            cmbPaisNascimento.DisplayMember = "Nome";
            cmbPaisNascimento.ValueMember = "Codigo";
            cmbPaisNascimento.SelectedIndex = -1;
            cmbUfNascimento.SelectedIndex = -1;
            cmbCidadeNascimento.SelectedIndex = -1;
            cmbPaisNascimento.SelectedIndexChanged += cmbPaisNascimento_SelectedIndexChanged;
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            txtBairro.Text = "";
            cmbCidadeEndereco.Text = "";
            txtComplemento.Text = "";
            cmbUfEndereco.Text = "";
            txtLogradouro.Text = "";

            txtBairro.Enabled = true;
            cmbCidadeEndereco.Enabled = true;
            txtComplemento.Enabled = true;
            cmbUfEndereco.Enabled = true;
            txtLogradouro.Enabled = true;
        }

        private void btbuscacep_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtLogradouro.Enabled = true;
            txtBairro.Enabled = true;
            cmbCidadeEndereco.Enabled = true;
            cmbUfEndereco.Enabled = true;
            try
            {
                //Address address = SearchZip.GetAddress(txtCep.Text);
                var ws = new WSCorreios.AtendeClienteClient();

                var resposta = ws.consultaCEP(txtCep.Text);

                txtLogradouro.Text = resposta.end.ToUpper();
                if (txtLogradouro.Text != "")
                    txtLogradouro.Enabled = false;

                txtBairro.Text = resposta.bairro.ToUpper();

                if (txtBairro.Text != "")
                    txtBairro.Enabled = false;

                string estado = "";
                string siglaestado = resposta.uf.ToUpper();



                #region switch estados
                switch (siglaestado)
                {
                    case "AC":
                        estado = "ACRE";
                        break;
                    case "AL":
                        estado = "ALAGOAS";
                        break;
                    case "AP":
                        estado = "AMAPA";
                        break;
                    case "AM":
                        estado = "AMAZONAS";
                        break;
                    case "BA":
                        estado = "BAHIA";
                        break;
                    case "CE":
                        estado = "CEARÁ";
                        break;
                    case "DF":
                        estado = "DISTRITO FEDERAL ";
                        break;
                    case "ES":
                        estado = "ESPÍRITO SANTO";
                        break;
                    case "GO":
                        estado = "GOIÁS";
                        break;
                    case "MA":
                        estado = "MARANHÃO";
                        break;
                    case "MT":
                        estado = "MATO GROSSO";
                        break;
                    case "MS":
                        estado = "MATO GROSSO DO SUL";
                        break;
                    case "MG":
                        estado = "MINAS GERAIS";
                        break;
                    case "PA":
                        estado = "PARÁ";
                        break;
                    case "PB":
                        estado = "PARAÍBA";
                        break;
                    case "PR":
                        estado = "PARANÁ";
                        break;
                    case "PE":
                        estado = "PERNAMBUCO";
                        break;
                    case "PI":
                        estado = "PIAUÍ";
                        break;
                    case "RJ":
                        estado = "RIO DE JANEIRO";
                        break;
                    case "RN":
                        estado = "RIO GRANDE DO NORTE";
                        break;
                    case "RS":
                        estado = "RIO GRANDE DO SUL";
                        break;
                    case "RO":
                        estado = "RONDONIA";
                        break;
                    case "RR":
                        estado = "RORAIMA";
                        break;
                    case "SC":
                        estado = "SANTA CATARINA";
                        break;
                    case "SP":
                        estado = "SÃO PAULO";
                        break;
                    default:
                        estado = "TOCANTINS";
                        break;
                }
                #endregion

                cmbUfEndereco.SelectedIndex = cmbUfEndereco.FindStringExact(estado);


                if (cmbUfEndereco.Text != "")
                    cmbUfEndereco.Enabled = false;

                cmbCidadeEndereco.SelectedIndex = cmbCidadeEndereco.FindStringExact(resposta.cidade);

                if (cmbCidadeEndereco.Text != "")
                    cmbCidadeEndereco.Enabled = false;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                if (ex.Message == "Não havia um ponto de extremidade em escuta em https://apps.correios.com.br/SigepMasterJPA/AtendeClienteService/AtendeCliente capaz de aceitar a mensagem. Em geral, isso é causado por um endereço ou ação de SOAP incorreta. Consulte InnerException, se presente, para obter mais detalhes.")
                    MessageBox.Show("Falha na conexão com a Internet. Favor verificar para pesquisar o CEP.", "E-matrícula: Buscar CEP");
            }
            Cursor.Current = Cursors.Default;
        }

        private void cmbUfEmprego_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUfEmprego.SelectedIndex > -1)
            {
                cmbCidadeEmprego.SelectedIndexChanged -= cmbCidadeEmprego_SelectedIndexChanged;
                BindingSource bsCidadeEmprego = new BindingSource();
                bsCidadeEmprego.DataSource = DAO.CidadeDAO.ExibirTodos((Classes.Uf)cmbUfEmprego.SelectedItem);
                cmbCidadeEmprego.DataSource = bsCidadeEmprego;
                cmbCidadeEmprego.DisplayMember = "Nome";
                cmbCidadeEmprego.ValueMember = "Codigo";
                cmbCidadeEmprego.SelectedItem = -1;
                cmbCidadeEmprego.SelectedIndexChanged += cmbCidadeEmprego_SelectedIndexChanged;
            }
        }

        private void cmbCidadeEmprego_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCidadeEmprego.SelectedIndex > -1)
            {
                if (_aluno.Emprego == null)
                {
                    _aluno.Emprego = new Classes.Emprego();
                }

                _aluno.Emprego.Cidade = (Classes.Cidade)cmbCidadeEmprego.SelectedItem;
                btnSalva.Enabled = true;
            }
        }

        private void frmAluno_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO: FUTURO 2 - VERIFICAR SE ESTÁ CORRETO QUESTIONAMENTO SOBRE ENCERRAEMNTO DA JANELA
            if (btnSalva.Enabled)
            {
                var window = MessageBox.Show("Deseja fechar sem salvar?", "Ematrícula", MessageBoxButtons.YesNo);
                e.Cancel = (window == DialogResult.No);
            }
            else
            {
                ErrorLog.ErrorHandleService.LimparErro();
            }
        }

        private void txtNmat_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void btnIncluirDeficiencia_Click(object sender, EventArgs e)
        {
            if (cmbDeficiencia.SelectedIndex > 0)
            {
                Classes.DeficienciaPessoa deficienciaPessoa = new Classes.DeficienciaPessoa();
                deficienciaPessoa.Deficiencia = ((Classes.Deficiencia)cmbDeficiencia.SelectedItem);
                deficienciaPessoa.Observacao = txtObservacaoDeficienciaAluno.Text;
                //add na lista
                _aluno.ListaDeficienciaPessoa.Add(deficienciaPessoa);
                //add no dgv
                dgvDeficienciaAluno.Rows.Add();
                dgvDeficienciaAluno.Rows[dgvDeficienciaAluno.Rows.Count - 1].Cells[0].Value = deficienciaPessoa;
                dgvDeficienciaAluno.Rows[dgvDeficienciaAluno.Rows.Count - 1].Cells[1].Value = deficienciaPessoa.Deficiencia.Nome;
                dgvDeficienciaAluno.Rows[dgvDeficienciaAluno.Rows.Count - 1].Cells[2].Value = deficienciaPessoa.Observacao;
                //add no banco
                //DAO.DeficienciaPessoaDAO.Gravar(deficienciaPessoa);
            }
        }

        private void txtRa_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtRg_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtUfRg_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtOrgao_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void dtpExpRg_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtCpf_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtAluno_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtNomeSocial_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtMae_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtPai_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void cmbRaca_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void cmbSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void cmbEstadoCivil_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void dtpDatMat_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void cmbTermo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtObs_passporte_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void dtpdatnasc_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtCep_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtLogradouro_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtNumero_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtBairro_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtComplemento_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtTelefone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtCelular_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void cmbDeficiencia_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dtpdatnasc_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void mtbCpf_TextChanged(object sender, EventArgs e)
        {
            btnSalva.Enabled = true;
        }

        private void ckbRaImpresso_CheckedChanged(object sender, EventArgs e)
        {
            //var context = new Modelo.Modelo();
            var query = _settings.Context.CARTAO_ACESSO.First(c => c.ALUNO == _aluno.NMatricula);
            query.IMPRESSO = ((CheckBox)sender).Checked;
            _settings.Context.SaveChanges();
        }
    }
}