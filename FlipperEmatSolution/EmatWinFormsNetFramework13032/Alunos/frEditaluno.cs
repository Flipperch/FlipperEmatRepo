using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Correios.Net;

using System.IO;

using System.Data.SqlClient;
using EmatWinFormsNetFramework13032.Fotos;

namespace EmatWinFormsNetFramework13032.Alunos
{
    public partial class frEditaluno : Form
    {
        Color cor_selecionado = Color.White;
        Color cor_descelecionado = Color.White;

        SqlConnection conn = Conexoes.GetSqlConnection();
        SqlQuery grava = new SqlQuery(Conexoes.GetSqlConnection());
        SqlQuery grava_1 = new SqlQuery(Conexoes.GetSqlConnection());
        Endereços.csEnderecos cs_enderecos = new Endereços.csEnderecos();
        BarcodeLib.Barcode b = new BarcodeLib.Barcode();

        csAlunos cs_alunos = new csAlunos();
        csRematriculas cs_rematriculas = new csRematriculas();
        csSituacoes cs_situacoes = new csSituacoes();
        csHistoricosSituacoes cs_historicos_situacoes = new csHistoricosSituacoes();
        Relatorios.csRelatorios cs_relatorio = new Relatorios.csRelatorios();
        Notas.csAtendimentos cs_atendimentos = new Notas.csAtendimentos();
        Notas.csDisciplinas cs_disciplinas = new Notas.csDisciplinas();

        Notas.csEnsinos cs_ensinos = new Notas.csEnsinos();

        Usuarios_Grupos.csUsuarios cs_usuarios = new Usuarios_Grupos.csUsuarios();

        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        DataTable tab_materias = new DataTable();

        GroupBox gb = new GroupBox();

        Configurações.CSconnexoes_txt conf = new Configurações.CSconnexoes_txt();

        string rg_load = "";

        string user_cad = "";
        int id_user_cad = 0;
        string ultima_presença = "";
        string id_cartao_atual = "";        
        public int cad_dig = 0;

        DateTime data_matricula;

        public DateTime dat_mat_registro;

        public string n_mat_pesquisa {get;set;}

        public frEditaluno()
        {
            InitializeComponent();            
        }

        private void frEditaluno_Load(object sender, EventArgs e)
        {
            //Ocultar e Mover - Itens
            moverControles();

            //Carregar Opções de Ensino 
            List<Notas.csDisciplinas> list_ensinos = cs_disciplinas.list_ensinos();
            cmbEnsino.Items.Add("");
            for (int i = 0; i < list_ensinos.Count; i++) cmbEnsino.Items.Add(list_ensinos[i].ensino);

            //TODO:: ARRUMAR CLASSE CORRETA - DISCIPLINAS PARA ENSINO
            //Carregar Opções de Países de Nascimento.
            cs_enderecos.obter_paises();
            cmbNascPais.DataSource = cs_enderecos.list_paises;

            //Carregar Opções de Estados/UF para Residencia e Trabalho.            
            cmbResEstado.SelectedIndexChanged -= cmbResEstado_SelectedIndexChanged;
            cmbResEstado.DataSource = cs_enderecos.lista_estados();;
            cmbResEstado.SelectedIndexChanged += cmbResEstado_SelectedIndexChanged;
            cmbTrabEstado.SelectedIndexChanged -= cmbTrabEstado_SelectedIndexChanged;
            cmbTrabEstado.DataSource = cs_enderecos.lista_estados();
            cmbTrabEstado.SelectedIndexChanged += cmbTrabEstado_SelectedIndexChanged;

            //Carregar Opções de Raça.
            cmbRaca.DataSource = cs_alunos.racas();

            //Adcionar em todos os TextBox e Combobox evento de KeyDow - TAB
            foreach (GroupBox grb in flowLayoutPanel1.Controls.OfType<GroupBox>())
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

            //Carregar Opções de Situações
            cmbSituacao.SelectedIndexChanged -= cmbSituacao_SelectedIndexChanged;
            List<string> list = (from row in cs_situacoes.sel_situacoes().AsEnumerable()
                                 select row.Field<string>("SITUACAO")).ToList();
            list.Insert(0, "");
            cmbSituacao.DataSource = list;

            //Preenchimento dados do Aluno
            preencheform();

            //Ativo / Situação
            preencher_info_situacoes();
            
            //Carregar foto do Aluno.
            getfoto();

            //Carregar Lista de Rematrículas
            fill_ltbRematricula();

            //Carregar Lista de Numeros Antigos.
            if (Configurações.csEscola.cidade.ToLower() == "americana")
            {
                List<csAlunos.n_mat_antigo> list_ = cs_alunos.lista_n_mat_antigos(txtNmat.Text);
                for (int i = 0; i < list_.Count; i++)
                {
                    ltbNumerosAntigos.Items.Add(list_[i].n_mat_antigo_);
                }
            }

            //Verificar presença
            verificar_presenca();
        }

        private void FRcadaluno_Shown(object sender, EventArgs e)
        {
            txtNmat.Focus();
            btnSalvar.Enabled = false;

            this.WindowState = FormWindowState.Maximized;

        }

        // --------------------------------------------------             

        void comboBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        
        public void moverControles()
        {
            int linha = 25;
            int avanco = 32;

            if (Configurações.csEscola.cidade.ToLower() == "sorocaba")
            {
                gbTrabalho.Visible = false;

                lblCpf.Visible = false;
                txtCpf.Visible = false;

                lblPai.Visible = false;
                txtPai.Visible = false;

                ckbHistorico.Visible = true;
                ckbCertidao.Visible = true;

                ckbHistorico.Enabled = true;
                ckbCertidao.Enabled = true;

                lblNAntigos.Visible = false;
                ltbNumerosAntigos.Visible = false;

                #region gbAlunos
                lblNmat.Location = new Point(6, linha); linha = linha + avanco;
                lblRg.Location = new Point(6, linha); linha = linha + avanco;
                lblAluno.Location = new Point(6, linha); linha = linha + avanco;
                lblNomeSocial.Location = new Point(6, linha); linha = linha + avanco;
                lblMae.Location = new Point(6, linha); linha = linha + avanco;
                lblRaca.Location = new Point(6, linha); linha = linha + avanco;
                lblSexo.Location = new Point(6, linha); linha = linha + avanco;
                lblEstadoCivil.Location = new Point(6, linha); linha = linha + avanco;
                ckbPossuiNecessidade.Location = new Point(10, linha+5); linha = linha + avanco;
                lblNec.Location = new Point(6, linha);               

                linha = 29;
                txtNmat.Location = new Point(122, linha); linha = linha + 32;
                txtRg.Location = new Point(122, linha); linha = linha + 32;
                txtAluno.Location = new Point(122, linha); linha = linha + 32;
                txtNomeSocial.Location = new Point(122, linha); linha = linha + 32;
                txtMae.Location = new Point(122, linha); linha = linha + 32;
                cmbRaca.Location = new Point(122, linha); linha = linha + 32;
                cmbSexo.Location = new Point(122, linha); linha = linha + 32;
                cmbEstadoCivil.Location = new Point(122, linha); linha = linha + 32;
                cmbNec.Location = new Point(122, linha + 32);

                gbAluno.Height = cmbNec.Location.Y + avanco + 5;

                #endregion

            }
            if (Configurações.csEscola.cidade.ToLower() == "americana")
            {
                ckbHistorico.Visible = false;
                ckbCertidao.Visible = false;

                ckbHistorico.Enabled = false;
                ckbCertidao.Enabled = false;

                lblDatMat.Location = new Point(86, 32);
                dtpDatMat.Location = new Point(204, 26);

                #region gbAlunos
                lblNmat.Location = new Point(6, linha); linha = linha + avanco;
                lblRg.Location = new Point(6, linha); linha = linha + avanco;
                lblCpf.Location = new Point(6, linha); linha = linha + avanco;
                lblAluno.Location = new Point(6, linha); linha = linha + avanco;
                lblNomeSocial.Location = new Point(6, linha); linha = linha + avanco;
                lblMae.Location = new Point(6, linha); linha = linha + avanco;
                lblPai.Location = new Point(6, linha); linha = linha + avanco;                
                lblRaca.Location = new Point(6, linha); linha = linha + avanco;
                lblSexo.Location = new Point(6, linha); linha = linha + avanco;
                lblEstadoCivil.Location = new Point(6, linha); linha = linha + avanco;
                ckbPossuiNecessidade.Location = new Point(10, linha + 5); linha = linha + avanco;
                lblNec.Location = new Point(6, linha);

                linha = 29;
                txtNmat.Location = new Point(122, linha); linha = linha + 32;
                txtRg.Location = new Point(122, linha); linha = linha + 32;
                txtCpf.Location = new Point(122, linha); linha = linha + 32;
                txtAluno.Location = new Point(122, linha); linha = linha + 32;
                txtNomeSocial.Location = new Point(122, linha); linha = linha + 32;
                txtMae.Location = new Point(122, linha); linha = linha + 32;
                txtPai.Location = new Point(122, linha); linha = linha + 32;
                cmbRaca.Location = new Point(122, linha); linha = linha + 32;
                cmbSexo.Location = new Point(122, linha); linha = linha + 32;
                cmbEstadoCivil.Location = new Point(122, linha); linha = linha + 32;
                cmbNec.Location = new Point(122, linha + 32);

                gbAluno.Height = cmbNec.Location.Y + avanco + 5;

                #endregion

                lblNAntigos.Visible = true;
                ltbNumerosAntigos.Visible = true;

            }
            

    

           
            
        }

        public List<Label> list_lbl_gbAluno()
        {
            List<Label> list_ = new List<Label>();

            List<string> list_nome_campos = new List<string>();
            list_nome_campos.Add("lblN_mat");
            list_nome_campos.Add("lblRg");
            list_nome_campos.Add("lblRa");
            list_nome_campos.Add("lblDatExp");
            list_nome_campos.Add("lblUf");
            list_nome_campos.Add("lblOrgao");
            list_nome_campos.Add("lblCpf");
            list_nome_campos.Add("lblNome");
            list_nome_campos.Add("lblNomeSocial");
            list_nome_campos.Add("lblNomeMae");
            list_nome_campos.Add("lblNomePai");
            list_nome_campos.Add("lblSexo");
            list_nome_campos.Add("lblRaca");
            list_nome_campos.Add("lblEstadoCivil");

            List<string> list_texto_campos = new List<string>();
            list_texto_campos.Add("Nº de Matrícula");
            list_texto_campos.Add("RG");
            list_texto_campos.Add("RA");
            list_texto_campos.Add("Data Exp.");
            list_texto_campos.Add("UF");
            list_texto_campos.Add("Orgão");
            list_texto_campos.Add("CPF");
            list_texto_campos.Add("Nome");
            list_texto_campos.Add("Nome Social");
            list_texto_campos.Add("Nome Mae");
            list_texto_campos.Add("Nome Pai");
            list_texto_campos.Add("Sexo");
            list_texto_campos.Add("Raça");
            list_texto_campos.Add("Estado Civil");
            

            for (int i = 0; i < list_nome_campos.Count; i++)
            {
                Label lbl = new Label();
                lbl.Name = list_nome_campos[i];
                lbl.Text = list_texto_campos[i];
                list_.Add(lbl);
            }

            return list_;
        }      
        
        //------------------------------------------------------

        #region Verificação de Campos

        private void verificar_txtNmat()
        {
            if (txtNmat.Text == "")
            {
                MessageBox.Show("Falta preencher o Campo de Matricula!", "Falha na Matrícula");
            }
            else
            {
                verificar_ckbCertidao();
            }

        }
                
        private void verificar_ckbCertidao()
        {
            if (Configurações.csEscola.cidade.ToLower() == "sorocaba")
            {
                if (ckbCertidao.Checked)
                {
                    verificar_ckbHistorico();
                }
                else
                {
                    MessageBox.Show("Falta a Certidão!", "Falha na Matrícula");
                }
            }
            else
            {
                verificar_ckbHistorico();
            }

        }

        private void verificar_ckbHistorico()
        {
            if (Configurações.csEscola.cidade.ToLower() == "sorocaba")
            {
                if (ckbHistorico.Checked)
                {
                    verificar_txtRg();
                }
                else
                {
                    MessageBox.Show("Falta o Histórico!", "Falha na Matrícula");
                }
            }
            else
            {
                verificar_txtRg();
            }
        }

        private void verificar_txtRg()
        {
            if (txtRg.Text != string.Empty)
            {
                verifica_dtpdatnasc();
            }
            else
            {
                MessageBox.Show("Falta preencher o RG!", "Falha na Matrícula");
            }

        }    

        private void verifica_dtpdatnasc()
        {
            DateTime dt = new DateTime();

            if (DateTime.TryParse(dtpdatnasc.Text, out dt))
            {
                verificar_dtpmat();
            }
            else
            {
                MessageBox.Show("Favor informar uma data de nascimento válida.", "Falha na Matrícula");
            }            
        }

        public void verificar_dtpmat()
        {
            DateTime dt = new DateTime();

            if (DateTime.TryParse(dtpDatMat.Text, out dt))
            {
                verificar_cmbensino();
            }
            else
            {
                MessageBox.Show("Favor informar uma data de matrícula válida.", "Falha na Matrícula");
            }
        }

        public void verificar_cmbensino()
        {
            if (cmbEnsino.Text == "")
            {
                lblEnsino.ForeColor = Color.Red;
                MessageBox.Show("Favor Selecionar o Ensino.", "Falha na Matrícula");
            }
            else
            {
                verificar_txtNome();
            }
        }        

        public void verificar_txtNome()
        {
            if (txtAluno.Text == "")
            {                
                //.ForeColor = Color.Red;
                MessageBox.Show("Falta informar o nome do Aluno.", "Falha na Matrícula");
            }
            else
            {
                if (txtAluno.Text.Substring(0,1) == " ")
                {
                    MessageBox.Show("Nome do Aluno Incorreto.", "Falha na Matrícula");
                }
                else
                {
                    atualizarcadastro();
                    atualiza_digital();
                }                
            }
        }

        #endregion
        
        public void atualizarcadastro()
        {
            txtCelular.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtCep.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtTrabTel.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //dtpdatnasc.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            //dtpExpRg.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            #region Obter Dados Formulario
            //Aluno         
            cs_alunos.n_mat = txtNmat.Text;
            cs_alunos.ra = txtRa.Text;
            cs_alunos.rg = txtRg.Text;
            cs_alunos.dat_rg = dtpExpRg.Text;
            cs_alunos.uf_rg = txtUfRg.Text;
            cs_alunos.orgao = txtOrgao.Text;
            cs_alunos.cpf = txtCpf.Text;
            cs_alunos.nome = txtAluno.Text.Trim();
            cs_alunos.nome_social = txtNomeSocial.Text;
            cs_alunos.nome_mae = txtMae.Text;
            cs_alunos.nome_pai = txtPai.Text;
            cs_alunos.id_raca = cs_alunos.troca_id_raca(cmbRaca.Text);
            cs_alunos.estado_civil = cmbEstadoCivil.Text;
            if (ckbPossuiNecessidade.Checked) cs_alunos.port_nec = true;
            else cs_alunos.port_nec = false;
            cs_alunos.nec = cmbNec.Text;
            //Situação
            string dat_time_str = DateTime.Parse(dtpDatMat.Text).ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("HH:mm:ss");
            data_matricula = DateTime.Parse(dat_time_str);
            cs_alunos.dat_mat = data_matricula;
            cs_alunos.certidao = 1;
            cs_alunos.historico = 1;
            cs_alunos.nome_ensino_atual = cmbEnsino.Text;
            cs_alunos.id_disciplina_atual = cs_disciplinas.troca_disciplina_nome_por_id(cmbEnsino.Text);
            cs_alunos.dt_ent_disciplina = dtpEntDisciplina.Value.ToString();            
            cs_alunos.termo_mat = cmbTermo.Text;
            cs_alunos.obs_passaporte = txtObs_passporte.Text;
            //Nascimento
            cs_alunos.dat_nasc = dtpdatnasc.Text; //TRY
            cs_alunos.nasc_pais = cmbNascPais.Text;
            cs_alunos.nasc_uf = cmbNascEstado.Text;
            cs_alunos.nasc_cidade = cmbNascCidade.Text;
            //Endereço
            cs_alunos.res_cep = txtCep.Text;
            cs_alunos.res_endereco = txtEndereco.Text;
            cs_alunos.res_numero = txtNumero.Text;
            cs_alunos.res_bairro = txtResBairro.Text;
            cs_alunos.res_complemento = txtResComplemento.Text;
            cs_alunos.res_uf = cmbResEstado.Text;
            cs_alunos.res_cidade = cmbResCidade.Text;
            //Contato
            cs_alunos.res_telefone = txtTelefone.Text;
            cs_alunos.res_celular = txtCelular.Text;
            cs_alunos.e_mail = txtEmail.Text;
            //Trabalho
            cs_alunos.trabalho = txtTrabalho.Text;
            cs_alunos.trab_estado = cmbTrabEstado.Text;
            cs_alunos.trab_cidade = cmbTrabCidade.Text;
            cs_alunos.trab_telefone = txtTrabTel.Text;


            #endregion

            cs_alunos.salvar_aluno(cs_alunos);

            //Verificar Atualização

            txtNmat.Enabled = false;
            MessageBox.Show("Cadastro Atualizado com Sucesso!", "Atualização");
            btnSalvar.Enabled = false;

            #region Registra Historico de Trocas de Ensino

            cs_ensinos.salvar_troca_ensino(dtpEntEnsino.Value, cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text), txtNmat.Text);

            dtpEntEnsino.Value = cs_ensinos.ent_ensino(txtNmat.Text, cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text));

            #endregion
        }        
        
        public void preencheform()
        {
            cs_alunos.sel_aluno(n_mat_pesquisa);            

            //Aluno
            txtNmat.Text = cs_alunos.n_mat;
            txtRa.Text= cs_alunos.ra;
            txtRg.Text = cs_alunos.rg;
            txtUfRg.Text = cs_alunos.uf_rg;
            dtpExpRg.Text = cs_alunos.dat_rg;
            txtOrgao.Text= cs_alunos.orgao;            
            txtAluno.Text= cs_alunos.nome;            
            cmbSexo.Text = cs_alunos.sexo;
            txtMae.Text = cs_alunos.nome_mae;
            ckbPossuiNecessidade.Checked = cs_alunos.port_nec;
            cmbNec.Text = cs_alunos.nec;            
            cmbEstadoCivil.Text = cs_alunos.estado_civil;
            txtNomeSocial.Text = cs_alunos.nome_social;
            txtPai.Text = cs_alunos.nome_pai;
            txtCpf.Text = cs_alunos.cpf;
            cmbRaca.Text = cs_alunos.troca_raca_id(cs_alunos.id_raca);
            //Situação
            //dat_mat_registro = cs_alunos.dat; 
            if (cs_alunos.ativo == 0) ckbAtivo.Checked = false;
            else ckbAtivo.Checked = true;
            dtpDatMat.Text = cs_alunos.dat_mat.ToString("dd/MM/yyyy");
            cmbEnsino.Text= cs_alunos.nome_ensino_atual;
            cmbDiscAtual.Text = cs_disciplinas.troca_disciplina_id_por_nome(cs_alunos.id_disciplina_atual);
            dtpEntEnsino.Value = cs_ensinos.ent_ensino(txtNmat.Text, cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text));
            cmbTermo.Text = cs_alunos.termo_mat;
            txtObs_passporte.Text = cs_alunos.obs_passaporte;
            //Nascimento
            dtpdatnasc.Text = cs_alunos.dat_nasc;
            cmbNascPais.Text = cs_alunos.nasc_pais;
            cmbNascEstado.Text = cs_alunos.nasc_uf;
            cmbNascCidade.Text = cs_alunos.nasc_cidade; 
            //Endereço
            txtEndereco.Text = cs_alunos.res_endereco;
            txtNumero.Text = cs_alunos.res_numero;
            txtResBairro.Text = cs_alunos.res_bairro;                
            cmbResEstado.Text = cs_alunos.res_uf;
            cmbResCidade.Text = cs_alunos.res_cidade;
            txtResComplemento.Text = cs_alunos.res_complemento;
            txtCep.Text = cs_alunos.res_cep;
            
            //Contato
            txtTelefone.Text = cs_alunos.res_telefone;
            txtCelular.Text = cs_alunos.res_celular;
            txtEmail.Text = cs_alunos.e_mail;
            //Trabalho
            txtTrabalho.Text = cs_alunos.trabalho;
            cmbTrabEstado.Text = cs_alunos.trab_estado;
            cmbTrabCidade.Text = cs_alunos.trab_cidade;
            txtTrabTel.Text = cs_alunos.trab_telefone;

            id_user_cad = cs_alunos.id_usuario_cad;
            
            if(cs_alunos.ativo==1) ckbAtivo.Checked = true;
            else ckbAtivo.Checked = false;
            
            if (cs_alunos.certidao == 1) ckbCertidao.Checked = true;
            else ckbCertidao.Checked = false;

            if (cs_alunos.historico == 1) ckbHistorico.Checked = true;
            else ckbHistorico.Checked = false;
        }

        //------------------------------------------------------
        
        public void get_id_user_cad()
        {

            grava.Command.Parameters.Clear();
            grava.Command.CommandText =
                @"SELECT * FROM USUARIO WHERE ID_USUARIO=@id_user_cad";

            grava.Command.Parameters.AddWithValue("@id_user_cad", id_user_cad);

            try
            {
                if (grava.Connection.State != ConnectionState.Closed)
                {
                    grava.Connection.Close();
                }

                grava.Connection.Open();
                SqlDataReader reader = grava.Command.ExecuteReader();
                while (reader.Read())
                {
                    user_cad = reader["NOME"].ToString();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                grava.Connection.Close();

            }
        }

        private void FReditaluno_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnSalvar.Enabled)
            {
                if (MessageBox.Show("Cadastro não esta Salvo. Deseja sair sem Salvar ?", "Atualização", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                   
                }
                else
                {
                    e.Cancel = true;
                }
            }

            cs_relatorio.limpar_temp(cs_relatorio.nome_arquivo);
        }       

        #region Rematricula

        private void btRematricula_Click(object sender, EventArgs e)
        {
            if (ckbAtivo.Checked)
            {
                //Aluno ATIVO fazendo rematricula - NÃO entra na tab LISTA_ALTERACOES
                cs_alunos.ativa_inativa_aluno(txtNmat.Text, 1, Usuarios_Grupos.csUsuario_logado.id_usuario_logado);                
            }
            else
            {
                //Aluno INATIVO fazendo rematricula - Entra na tab LISTA_ALTERACOES 
                cs_alunos.ativa_inativa_aluno(txtNmat.Text, 1, Usuarios_Grupos.csUsuario_logado.id_usuario_logado);
                cs_alunos.add_mod_lista_ativa_inativa(txtNmat.Text, 1, Usuarios_Grupos.csUsuario_logado.id_usuario_logado, "REMATRÍCULA");                
            }

            //Visual - Form
            btRematricula.Enabled = false;            
            ckbAtivo.Checked = true;
            ckbAtivo.Enabled = true;
            
            //Rematricula
            cs_rematriculas.add_rematricula(DateTime.Now, Usuarios_Grupos.csUsuario_logado.id_usuario_logado, txtNmat.Text);

            fill_ltbRematricula();

            preencher_info_situacoes();

            //Salvar
            btnSalvar.PerformClick();
        }

        public void fill_ltbRematricula()
        {
            ltbRematriculas.DataSource = (from row in cs_rematriculas.sel_rematriculas(txtNmat.Text).AsEnumerable()
                                          select row.Field<DateTime>("DATA_REMATRICULA").ToString("dd/MM/yyyy")).ToList();
            
        }

        #endregion

        #region Lateral Esquerda

        private void btPassaporte_Click(object sender, EventArgs e)
        {
            Notas.frPassaporte form = new Notas.frPassaporte();
            form.n_mat = txtNmat.Text;

            form.ShowDialog();


        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            frExcluirAluno fr_excluir = new frExcluirAluno();

            fr_excluir.n_mat_p_excluir = txtNmat.Text;
            fr_excluir.id_cartao_p_excluir = txtNmat.Text + DateTime.Parse(dtpdatnasc.Text).ToString("yy");

            fr_excluir.ShowDialog();

            grava.Command.CommandText = @"SELECT * FROM ALUNOS WHERE N_MAT=@n_mat";

            grava.Command.Parameters.AddWithValue("@n_mat", CSaluno.numat);

            grava.Connection.Open();

            SqlDataReader reader = grava.Command.ExecuteReader();

            while (reader.Read())
            {

            }

            if (!reader.HasRows)
            {
                this.Close();
            }

            grava.Connection.Close();

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Principal.frPrincipal fr = (Principal.frPrincipal)this.MdiParent;

            this.Close();

            fr.editarToolStripMenuItem.PerformClick();

        }

        private void btimprimir_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btimprimir.Enabled = false;

            frImprimir form = new frImprimir();

            if (Configurações.csEscola.cidade.ToLower() == "sorocaba")
            {
                gera_barcode();
            }

            form.n_mat = txtNmat.Text;
            form.nome = txtAluno.Text;
            form.sexo = cmbSexo.Text;
            form.termo = cmbTermo.Text;
            form.cel = txtCelular.Text;
            form.tel = txtTelefone.Text;
            form.ra = txtRa.Text;
            form.rg = txtRg.Text;
            form.exp_rg = "";
            form.id_ensino = cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text);
            form.dat_nasc = dtpdatnasc.Text;
            form.cidade_nasc = cmbNascCidade.Text;
            form.estado_nasc = cmbNascEstado.Text;
            form.cpf = "";
            form.dat_mat = dtpDatMat.Text;

            form.Show();

            btimprimir.Enabled = true;

            Cursor.Current = Cursors.Default;

        }

        private void altera(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void btcancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btsalva_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            verificar_txtNmat();

        }

        public void gera_barcode()
        {
            errorProvider1.Clear();
            int W = Convert.ToInt32("300");
            int H = Convert.ToInt32("150");
            b.Alignment = BarcodeLib.AlignmentPositions.CENTER;

            BarcodeLib.TYPE type = BarcodeLib.TYPE.Interleaved2of5;

            try
            {

                b.IncludeLabel = true;

                b.RotateFlipType = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), "rotatenoneflipnone", true);


                b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;


                int z = Convert.ToInt32(txtNmat.Text);
                string num = z.ToString("D8");

                gb.BackgroundImage = b.Encode(type, num.Trim(), Color.Black, Color.White, W, H);

                BarcodeLib.SaveTypes savetype = BarcodeLib.SaveTypes.PNG;

                conf.get_configuracoes();

                string patch_barcode = conf.caminho_barcodes + "\\bc_";

                b.SaveImage(patch_barcode + txtNmat.Text + ".png", savetype);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }//catch

        }

        #endregion

        #region Lateral Direita

        public void getfoto()
        {
            //Foto


            ptbFoto.Image = cs_alunos.foto_aluno(txtNmat.Text);

            cs_alunos.foto_aluno(txtNmat.Text).Dispose();

        }

        public void verfica_digital()
        {
            grava.Command.CommandText =
                @"SELECT * FROM ACESSO_CATRACA WHERE ID_CARTAO = @idcartao";

            grava.Command.Parameters.AddWithValue("@idcartao", txtNmat.Text + DateTime.Parse(dtpdatnasc.Text).ToString("yy"));


            bool connected = conn.State == ConnectionState.Open;
            try
            {
                string template = "";
                if (!connected) grava.Connection.Open();
                SqlDataReader reader = grava.Command.ExecuteReader();

                while (reader.Read())
                {
                    template = reader["TEMPLATE1"].ToString();

                    if (reader["ULTIMO_ACESSO"].ToString() != string.Empty)
                    {
                        ultima_presença = DateTime.Parse(reader["ULTIMO_ACESSO"].ToString()).Date.ToString();
                    }

                }
                //if(template == "")
                //{
                //    btobterdigital.Text = "Obter Digital";
                //    txtDigital.ForeColor = Color.DarkRed;
                //    txtDigital.Text = "DIGITAL NÃO CADASTRADA";
                //    //cad_dig = 1;
                //}
                //else
                //{
                //    btobterdigital.Text = "Editar Digital";
                //    txtDigital.ForeColor = Color.Black;
                //    txtDigital.Text = "DIGITAL CADASTRADA";
                //}
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                grava.Command.Parameters.Clear();
                grava.Connection.Close();
            }
        }

        private void btobterfoto_Click(object sender, EventArgs e)
        {

        }

        private void btobterdigital_Click(object sender, EventArgs e)
        {
            //Digitais.CScad_digital.cad_card = txtNmat.Text;
            //Digitais.CScad_digital.nome_cad_card = alunotxt.Text;

            //if(btobterdigital.Text == "Editar Digital")
            //{
            //    Digitais.CScad_digital.existente = "sim";
            //}
            //else
            //{
            //    Digitais.CScad_digital.existente = "não";
            //}

            //using (Digitais.FRdig fr = new Digitais.FRdig())
            //{
            //    fr.ShowDialog();
            //
            //    verfica_digital();
            //}
        }

        public void retorno_pos_foto()
        {
            txtNmat.Enabled = false;
            getfoto();
        }

        public void cadastrar_aluno_catraca()
        {
            grava.Command.CommandText =
                @"INSERT INTO ACESSO_CATRACA (ID_CARTAO, ESTADO, ENVIADO)
                VALUES (@idcartao, @estado, @enviado)";

            grava.Command.Parameters.AddWithValue("@idcartao", txtNmat.Text + DateTime.Parse(dtpdatnasc.Text).ToString("yy"));
            grava.Command.Parameters.AddWithValue("@estado", 0);
            grava.Command.Parameters.AddWithValue("@enviado", 0);


            bool connected = conn.State == ConnectionState.Open;
            try
            {
                if (!connected) grava.Connection.Open();
                grava.Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                grava.Command.Parameters.Clear();
                grava.Connection.Close();
            }

        }

        public void verificar_presenca()
        {
            string ultimo_atendimento = cs_atendimentos.data_ultimo_atendimento(Convert.ToInt32(txtNmat.Text));

            if (ultimo_atendimento != string.Empty)
            {

                DateTime Ultima = DateTime.Parse(ultimo_atendimento);
                DateTime hoje = DateTime.Now;

                TimeSpan TSDiferenca = hoje - Ultima;
                int DiferencaEmDias = TSDiferenca.Days;


                if (DiferencaEmDias > 51)
                {
                    //lblPresenca.Text = DateTime.Parse(ultima_presença).ToString("dd/MM/yyyy");
                    lblPresenca.Text = ultimo_atendimento;
                    lblPresenca.ForeColor = Color.Red;
                    //btnLiberar_entrada.Enabled = true;
                }
                else
                {
                    //lblPresenca.Text = DateTime.Parse(ultima_presença).ToString("dd/MM/yyyy");
                    lblPresenca.Text = ultimo_atendimento;
                    lblPresenca.ForeColor = Color.Black;
                    //btnLiberar_entrada.Enabled = false;

                }
            }
        }

        public void atualiza_digital()
        {
            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            sql_bd.Command.CommandText = @"UPDATE ACESSO_CATRACA SET ID_CARTAO=@idcartao_novo WHERE ID_CARTAO=@idcartao_antigo";

            sql_bd.Command.Parameters.AddWithValue("@idcartao_antigo", id_cartao_atual);
            sql_bd.Command.Parameters.AddWithValue("@idcartao_novo", txtNmat.Text + DateTime.Parse(dtpdatnasc.Text).ToString("yy"));


            try
            {
                if (sql_bd.Connection.State != ConnectionState.Closed)
                {
                    sql_bd.Connection.Close();
                }
                sql_bd.Connection.Open();

                //Executa a instrução SQL
                sql_bd.Command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnLiberar_entrada_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            sql_bd.Command.CommandText = @"UPDATE ACESSO_CATRACA SET ULTIMO_ACESSO=@ultimo, ATIVO=@ativo WHERE ID_CARTAO=@id_cartao";

            sql_bd.Command.Parameters.AddWithValue("@idcartao", txtNmat.Text + DateTime.Parse(dtpdatnasc.Text).ToString("yy"));
            sql_bd.Command.Parameters.AddWithValue("@ativo", 1);
            sql_bd.Command.Parameters.AddWithValue("@ultimo", DateTime.Now);

            try
            {
                if (sql_bd.Connection.State != ConnectionState.Closed)
                {
                    sql_bd.Connection.Close();
                }
                sql_bd.Connection.Open();

                //Executa a instrução SQL
                sql_bd.Command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                sql_bd.Command.Parameters.Clear();
                sql_bd.Connection.Close();

                //btnLiberar_entrada.Enabled = false;

                verfica_digital();

                verificar_presenca();

                Cursor.Current = Cursors.Default;
            }
        }

        private void fotoaluno_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            ptbFoto.Enabled = false;

            if (txtNmat.Text != "")
            {

                ptbFoto.Image.Dispose();


                Fotos.frObterfotos formfoto = new Fotos.frObterfotos();
                formfoto.nome_form_solicitante = this.Name;
                formfoto.numat = txtNmat.Text;
                formfoto.Owner = this;
                formfoto.cad_edit = 1;
                Cursor.Current = Cursors.WaitCursor;
                formfoto.ShowDialog();

                retorno_pos_foto();
            }
            else
            {
                MessageBox.Show("É necessario informar o numero de matrícula.");
            }


            ptbFoto.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Aluno

        private void posnecckb_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbPossuiNecessidade.Checked == true)
            {
                cmbNec.Enabled = true;
            }
            else
            {
                cmbNec.Text = "";
                cmbNec.Enabled = false;
            }

            btnSalvar.Enabled = true;
        }      

        private void sexocmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void dtpdatnasc_ValueChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void posnecckb_CheckedChanged_1(object sender, EventArgs e)
        {
        }

        private void estadocivilcmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void racacmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void txtMae_TextChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void cmbSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void cmbRaca_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void cmbEstadoCivil_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void ckbPossuiNecessidade_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbPossuiNecessidade.Checked) cmbNec.Enabled = true;
            else cmbNec.Enabled = false;

            btnSalvar.Enabled = true;            
        }

        private void cmbNec_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }  

        private void txtNmat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        

        private void nmattxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        private void nmattxt_TextChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void neccmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        #endregion 

        #region Situações

        private void preencher_info_situacoes()
        {
            if (ckbAtivo.Checked)
            {
                cmbSituacao.Text = "";
                cmbSituacao.Enabled = false;
                dtpSituacao.Value = DateTime.Now;
                
            }
            else
            {
                cmbSituacao.Enabled = true;
                cmbSituacao.Text = cs_historicos_situacoes.situacao_atual(txtNmat.Text);
                dtpSituacao.Value = cs_historicos_situacoes.data_situacao_atual(txtNmat.Text);
              
            }
        }

        private void ckbAtivo_CheckedChanged(object sender, EventArgs e)
        {
            //Ocorre quando um aluno Ativo, é inativado pelo usuário,podendo ser ativado apenas por rematrícula
            if (ckbAtivo.Checked == false)
            {
                frInativarSelectSituacao form = new frInativarSelectSituacao();

                form.n_mat = txtNmat.Text;

                ckbAtivo.CheckedChanged -= ckbAtivo_CheckedChanged;

                form.ShowDialog();

                if (form.inativar)
                {
                    //Inativar
                    ckbAtivo.Checked = false;
                    ckbAtivo.Enabled = false;

                    //INATIVA ALUNO
                    cs_alunos.ativa_inativa_aluno(txtNmat.Text, 0, Usuarios_Grupos.csUsuario_logado.id_usuario_logado);
                    //ADD NA LISTA_ALTERACOES COMO INATIVO - PELO USUARIO
                    cs_alunos.add_mod_lista_ativa_inativa(txtNmat.Text, 0, Usuarios_Grupos.csUsuario_logado.id_usuario_logado, "PELO USUÁRIO");
                }
                else
                {
                    //Ativar
                    ckbAtivo.Checked = true;
                }

                cmbSituacao.SelectedIndexChanged -= cmbSituacao_SelectedIndexChanged;
                preencher_info_situacoes();
                cmbSituacao.SelectedIndexChanged += cmbSituacao_SelectedIndexChanged;

                ckbAtivo.CheckedChanged += ckbAtivo_CheckedChanged;
            }
        }

        private void cmbSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {            

            ///Aqui, aluno já esta inativo, porem está sendo alterado situação de inatividade
            if (cmbSituacao.Text != string.Empty)
            {
                frInativarSelectSituacao form = new frInativarSelectSituacao();

                form.n_mat = txtNmat.Text;
                form.situacao_selecionada = cmbSituacao.Text;
                form.ShowDialog();
            }

            cmbSituacao.SelectedIndexChanged -= cmbSituacao_SelectedIndexChanged;
            preencher_info_situacoes();
            cmbSituacao.SelectedIndexChanged += cmbSituacao_SelectedIndexChanged;
        }

        private void cmbEnsino_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;

            if (cmbEnsino.Text != "")
            {
                //Busca Opções de Disciplinas
                cmbDiscAtual.DataSource = cs_disciplinas.lista_disciplinas_por_ensino(cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text));

                //Buscar data da troca de Ensino
                dtpEntEnsino.Value = cs_ensinos.ent_ensino(txtNmat.Text, cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text));
            }
        }

        private void cmbDiscAtual_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void ckbCertidao_CheckedChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void ckbHistorico_CheckedChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void cmbMat_atual_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void termomatcmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }        

        #endregion

        #region Nascimento

        private void nasccidadecmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }



        private void cmbNascPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            cs_enderecos.list_estados.Clear();

            if (cmbNascPais.Text != "")
            {
                cmbNascEstado.DataSource = null;
                cs_enderecos.pais_selec = cmbNascPais.SelectedItem.ToString();
                cs_enderecos.obter_estados();
                cmbNascEstado.DataSource = cs_enderecos.list_estados;
                btnSalvar.Enabled = true;
            }
            else
            {
                cmbNascEstado.DataSource = null;
            }
        }

        private void nascestadocmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            cs_enderecos.cidades.Clear();
            if (cmbNascEstado.Text != "")
            {
                cmbNascCidade.DataSource = null;
                cs_enderecos.estado_sel = cmbNascEstado.SelectedItem.ToString();
                cs_enderecos.buscarcidades();
                cmbNascCidade.DataSource = cs_enderecos.bscidades;
                btnSalvar.Enabled = true;
            }
            else
            {
                cmbNascCidade.DataSource = null;
            }
        }

        #endregion
        
        #region Endereço

        private void btLimparEndereco_Click(object sender, EventArgs e)
        {
            txtResBairro.Text = "";
            cmbResCidade.Text = "";
            txtResComplemento.Text = "";
            cmbResEstado.Text = "";
            txtEndereco.Text = "";

            txtResBairro.Enabled = true;
            cmbResCidade.Enabled = true;
            txtResComplemento.Enabled = true;
            cmbResEstado.Enabled = true;
            txtEndereco.Enabled = true;
        }

        private void btbuscacep_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtEndereco.Enabled = true;
            txtResBairro.Enabled = true;
            cmbResCidade.Enabled = true;
            cmbResEstado.Enabled = true;
            try
            {
                Address address = BuscaCep.GetAddress(txtCep.Text);

                txtEndereco.Text = address.Street.ToUpper();
                if (txtEndereco.Text != "")
                {
                    txtEndereco.Enabled = false;
                }
                txtResBairro.Text = address.District.ToUpper();
                if (txtResBairro.Text != "")
                {
                    txtResBairro.Enabled = false;
                }
                string estado = "";
                string siglaestado = address.State.ToUpper();

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

                cmbResEstado.Text = estado;
                if (cmbResEstado.Text != "")
                {
                    cmbResEstado.Enabled = false;
                }

                cmbResCidade.Text = address.City.ToUpper();

                if (address.City == "Santa Bárbara D&#39;Oeste")
                {
                    cmbResCidade.Text = "SANTA BÁRBARA D'OESTE";
                }


                if (cmbResCidade.Text != "")
                {
                    cmbResCidade.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message == "O nome remoto não pôde ser resolvido: 'm.correios.com.br'")
                {
                    MessageBox.Show("Falha na conexão com a Internet. Favor verificar para pesquisar o CEP.", "Buscar CEP");
                }
            }
            Cursor.Current = Cursors.Default;

        }

        private void cmbResEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbResEstado.Text != "")
            {
                cmbResCidade.DataSource = null;
                cmbResCidade.DataSource = cs_enderecos.lista_cidades(cs_enderecos.troca_estado_nome_sigla(cmbResEstado.Text));
                btnSalvar.Enabled = true;
            }
            else
            {
                cmbResCidade.DataSource = null;
            }
        }
        

            

        private void rescomplementotxt_TextChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void btNovoendere_Click(object sender, EventArgs e)
        {
            using (Endereços.frEndereco fr = new Endereços.frEndereco())
            {
                cmbNascPais.Text = "";
                cmbNascEstado.Text = "";
                cmbNascCidade.Text = "";



                cs_enderecos.list_paises.Clear();
                cmbNascPais.DataSource = null;
                cs_enderecos.obter_paises();
                cmbNascPais.DataSource = cs_enderecos.list_paises;

            }
        }

        

        private void rescidadecmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void resenderecotxt_TextChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void resnumerotxt_TextChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void resbairrotxt_TextChanged(object sender, EventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void restelefonetxt_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        private void rescelulartxt_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            btnSalvar.Enabled = true;
        }

        #endregion

        #region Contato

        #endregion

        #region Trabalho

        private void cmbTrabEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTrabEstado.Text != "")
            {
                cmbTrabCidade.DataSource = null;
                cmbTrabCidade.DataSource = cs_enderecos.lista_cidades(cs_enderecos.troca_estado_nome_sigla(cmbTrabEstado.Text));
                btnSalvar.Enabled = true;
            }
            else
            {
                cmbTrabCidade.DataSource = null;
            }
        }

        #endregion

        private void btnHistoricoSituacoes_Click(object sender, EventArgs e)
        {
            frHistSituacoes form = new frHistSituacoes();
            form.n_mat_pesquisa_ = txtNmat.Text;
            form.dat_matricula = cs_alunos.dat_mat;
            form.user_matricula = user_cad;
            form.ShowDialog();

            cmbSituacao.SelectedIndexChanged -= cmbSituacao_SelectedIndexChanged;
            preencher_info_situacoes();
            cmbSituacao.SelectedIndexChanged += cmbSituacao_SelectedIndexChanged;
        }

        private void dtpEntEnsino_ValueChanged(object sender, EventArgs e)
        {
            cs_ensinos.salvar_troca_ensino(dtpEntEnsino.Value, cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text), txtNmat.Text);
        }

        private void txtRg_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRg_Leave(object sender, EventArgs e)
        {
            if (txtRg.Text != string.Empty)
            {
                if (cs_alunos.sel_list_alunos("",txtRg.Text).Count > 0)
                {
                    MessageBox.Show("O RG " + txtRg.Text + "já está cadastrado!", "Falha na Matrícula");

                    txtRg.Text = "";
                    txtRg.Focus();
                }
            }
        }

       
    }
}
