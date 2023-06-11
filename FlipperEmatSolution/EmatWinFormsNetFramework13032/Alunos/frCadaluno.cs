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

using System.Data.SqlClient;
using System.Data.Odbc;
using System.IO;

namespace EmatWinFormsNetFramework13032.Alunos
{
    public partial class frCadaluno : Form
    {
        Alunos.csAlunos cs_alunos = new Alunos.csAlunos();
        Relatorios.csRelatorios cs_relatorio = new Relatorios.csRelatorios();
        Notas.csDisciplinas cs_disciplinas = new Notas.csDisciplinas();
        Notas.csEnsinos cs_ensinos = new Notas.csEnsinos();
        Configurações.CSconnexoes_txt conf = new Configurações.CSconnexoes_txt();
        Endereços.csEnderecos cs_enderecos = new Endereços.csEnderecos();

        DateTime data_matricula = DateTime.Now;

        public frCadaluno()
        {
            InitializeComponent();            
        }

        private void FRcadaluno_Load(object sender, EventArgs e)
        {
            //Ocultar Items Conforme Escola.
            moverControles();

            //Data de Matrícula Recebe Hoje.
            dtpDatMat.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //TODO:: ARRUMAR CLASSE CORRETA - DISCIPLINAS PARA ENSINO
            //Carregar Opções de Ensino 
            List<Notas.csDisciplinas> list_ensinos = cs_disciplinas.list_ensinos();
            cmbEnsino.Items.Add("");
            for (int i = 0; i < list_ensinos.Count; i++) cmbEnsino.Items.Add(list_ensinos[i].ensino);

            //Carregar Opções de Países de Nascimento.
            cs_enderecos.obter_paises();
            cmbNascPais.DataSource = cs_enderecos.list_paises;

            //Carregar Opções de Estados/UF para Residencia e Trabalho.
            List<string> list_estados = cs_enderecos.lista_estados();
            cmbResEstado.SelectedIndexChanged -= cmbResCidade_SelectedIndexChanged;
            cmbResEstado.DataSource = list_estados;
            cmbResEstado.SelectedIndexChanged += cmbResCidade_SelectedIndexChanged;
            cmbTrabEstado.SelectedIndexChanged -= cmbTrabEstado_SelectedIndexChanged;
            cmbTrabEstado.DataSource = list_estados;
            cmbTrabEstado.SelectedIndexChanged += cmbTrabEstado_SelectedIndexChanged;

            //Carregar Opções de Raça.
            cmbRaca.DataSource = cs_alunos.racas();

            //Adcionar em todos os TextBox e Combobox evento de KeyDow - TAB
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
        }

        private void moverControles()
        {
            GroupBox gb = new GroupBox();

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
                ckbPossuiNecessidade.Location = new Point(10, linha + 5); linha = linha + avanco;
                lblNec.Location = new Point(6, linha);

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

                #region gbAluno
                lblNmat.Location = new Point(6, linha);
                lblRa.Location = new Point(226, linha); linha = linha + avanco;
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
                txtNmat.Location = new Point(122, linha);
                txtRa.Location = new Point(264, linha); linha = linha + 32;
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

            }
        }

        private void FRcadaluno_Shown(object sender, EventArgs e)
        {
            txtNmat.Focus();
            this.WindowState = FormWindowState.Maximized;
        }

        void comboBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        
        #region Lateral Esquerda

        private void btcancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btsalva_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btsalva.Enabled = false;
            if(verificar_campos())
            {
                if (cadastrar_aluno())
                {
                    //Bloquear Campos
                    travar_campos();
                    //Registro Ativação Aluno
                    cs_alunos.ativa_inativa_aluno(txtNmat.Text, 1, Usuarios_Grupos.csUsuario_logado.id_usuario_logado);
                    cs_alunos.add_mod_lista_ativa_inativa(txtNmat.Text, 1, Usuarios_Grupos.csUsuario_logado.id_usuario_logado, "MATRÍCULA");
                    //Registra Entrada no Ensino
                    cs_ensinos.salvar_troca_ensino(data_matricula, cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text), txtNmat.Text);                    
                    //Cadastro de aluno na Catraca
                    cadastrar_aluno_catraca();
                }                              
            }   
            else
            {
                btsalva.Enabled = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btimprimir_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btimprimir.Enabled = false;

            frImprimir form = new frImprimir();

          
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

        #endregion

        #region Lateral Direita

        private void ptbFoto_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (btsalva.Enabled == false)
            {
                Fotos.frObterfotos formfoto = new Fotos.frObterfotos();
                formfoto.nome_form_solicitante = this.Name;
                formfoto.numat = txtNmat.Text;
                formfoto.Owner = this;
                formfoto.cad_edit = 0;
                formfoto.ShowDialog();
                retorno_pos_foto();
            }
            else
            {
                MessageBox.Show("É necessario Salvar o Cadastro antes de obter a foto.", "Obter Foto.");
            }

            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Foto e Digital

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
            Digitais.CScad_digital.cad_card = txtNmat.Text;
            Digitais.CScad_digital.nome_cad_card = txtAluno.Text;

            using (Digitais.FRdig fr = new Digitais.FRdig())
            {
                fr.ShowDialog();

                verfica_digital();
            }

        }

        private void btobterfoto_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (btsalva.Enabled == false)
            {   
                Fotos.frObterfotos formfoto = new Fotos.frObterfotos();
                formfoto.nome_form_solicitante = this.Name;
                formfoto.numat = txtNmat.Text;
                formfoto.Owner = this;
                formfoto.cad_edit = 0;
                formfoto.ShowDialog();
                retorno_pos_foto();
            }
            else
            {
                MessageBox.Show("É necessario Salvar o Cadastro antes de obter a foto.", "Obter Foto.");
            }

            Cursor.Current = Cursors.Default;
        }        

        private void retorno_pos_foto()
        {
            txtNmat.Enabled = false;
            ptbFoto.Image = cs_alunos.foto_aluno(txtAluno.Text);
        }
      
        #endregion
     
        private bool verificar_campos()
        {            
            DateTime dt = new DateTime();

            if (txtNmat.Text == string.Empty)
            {
                MessageBox.Show("Falta preencher o Campo de Matricula!");
                return false;
            }
            if (txtRg.Text == string.Empty)
            {
                MessageBox.Show("Falta preencher o Campo RG!", "Falha na Matrícula");
                return false;
            }

            if (!DateTime.TryParse(dtpdatnasc.Text, out dt))
            {
                MessageBox.Show("Favor informar uma data de nascimento válida.", "Falha na Matrícula");
                return false;
            }

            if (!DateTime.TryParse(dtpDatMat.Text, out dt))
            {
                MessageBox.Show("Favor informar uma data de matrícula válida.", "Falha na Matrícula");
                return false;
            }

            if (cmbEnsino.Text == string.Empty)
            {
                lblEnsino.ForeColor = Color.Red;
                MessageBox.Show("Favor Selecionar o Ensino.", "Falha na Matrícula");
                return false;
            }

            if (txtAluno.Text == string.Empty)
            {
                //.ForeColor = Color.Red;
                MessageBox.Show("Falta informar o nome do Aluno.", "Falha na Matrícula");
                return false;
            }

            if (Configurações.csEscola.cidade.ToLower() == "sorocaba")
            {
                if (!ckbCertidao.Checked)
                {
                    MessageBox.Show("Falta a Certidão!", "Falha na Matrícula");
                    return false;
                }
                if (!ckbHistorico.Checked)
                {
                    MessageBox.Show("Falta o Histórico!", "Falha na Matrícula");
                    return false;
                }
            }

            return true;
        }
        
        private bool cadastrar_aluno()
        {            
            txtCelular.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            txtCep.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            Alunos.csAlunos novo_aluno = new Alunos.csAlunos();

            #region Obter Dados Formulario

            //Aluno         
            novo_aluno.n_mat = txtNmat.Text;
            novo_aluno.ra = txtRa.Text;
            novo_aluno.rg = txtRg.Text;
            novo_aluno.dat_rg = dtpExpRg.Text;
            novo_aluno.uf_rg = txtUfRg.Text;
            novo_aluno.orgao = txtOrgao.Text;
            novo_aluno.cpf = txtCpf.Text;
            novo_aluno.nome = txtAluno.Text.Trim();
            novo_aluno.nome_social = txtNomeSocial.Text;
            novo_aluno.nome_mae = txtMae.Text;
            novo_aluno.nome_pai = txtPai.Text;
            novo_aluno.id_raca = cs_alunos.troca_id_raca(cmbRaca.Text);
            novo_aluno.sexo = cmbSexo.Text;
            novo_aluno.estado_civil = cmbEstadoCivil.Text;
            if (ckbPossuiNecessidade.Checked) novo_aluno.port_nec = true;
            else novo_aluno.port_nec = false;
            novo_aluno.nec = cmbNec.Text;
            //Situação
            string dat_time_str = DateTime.Parse(dtpDatMat.Text).ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("HH:mm:ss");
            data_matricula = DateTime.Parse(dat_time_str);
            novo_aluno.dat_mat = data_matricula;
            novo_aluno.certidao = 1;
            novo_aluno.historico = 1;
            novo_aluno.nome_ensino_atual = cmbEnsino.Text;
            novo_aluno.termo_mat = cmbTermo.Text;
            novo_aluno.obs_passaporte = txtObs_passporte.Text;
            //Nascimento
            novo_aluno.dat_nasc = dtpdatnasc.Text; //TRY
            novo_aluno.nasc_pais = cmbNascPais.Text;
            novo_aluno.nasc_uf = cmbNascEstado.Text;
            novo_aluno.nasc_cidade = cmbNascCidade.Text;
            //Endereço
            novo_aluno.res_cep = txtCep.Text;
            novo_aluno.res_endereco = txtEndereco.Text;
            novo_aluno.res_numero = txtNumero.Text;
            novo_aluno.res_bairro = txtResBairro.Text;
            novo_aluno.res_complemento = txtResComplemento.Text;
            novo_aluno.res_uf = cmbResEstado.Text;
            novo_aluno.res_cidade = cmbResCidade.Text;
            //Contato
            novo_aluno.res_telefone = txtTelefone.Text;
            novo_aluno.res_celular = txtCelular.Text;
            novo_aluno.e_mail = txtEmail.Text;
            //Trabalho
            novo_aluno.trabalho = txtTrabalho.Text;
            novo_aluno.trab_estado = cmbTrabEstado.Text;
            novo_aluno.trab_cidade = cmbTrabCidade.Text;
            novo_aluno.trab_telefone = txtTrabTel.Text;
            
#endregion

            //Salvar
            cs_alunos.salvar_aluno(novo_aluno);

            if(cs_alunos.sel_list_alunos(txtNmat.Text).Count > 0)
            {
                MessageBox.Show("Matrícula efetuada com Sucesso.", "Matrícula!");                
                return true;
            }
            else
            {                
                MessageBox.Show("Falha na matrícula.", "Matrícula!");
                return false;
            }
        }

        private void cadastrar_aluno_catraca()
        {
            string cartao = txtNmat.Text + DateTime.Parse(dtpdatnasc.Text).ToString("yy");
            cs_alunos.add_aluno_catraca(cartao);
        }        

        private void travar_campos()
        {
            //Trava Controles
            foreach (GroupBox grb in flowLayoutPanel2.Controls)
            {
                foreach(Control ctr in grb.Controls.OfType<Control>())
                {
                    ctr.Enabled = false;
                }                
            }
        }
        
        #region Aluno

        private void txtNmat_EnabledChanged(object sender, EventArgs e)
        {
            if (!txtNmat.Enabled)
            {
                btimprimir.Enabled = true;
            }
        }

        private void txtNmat_Leave(object sender, EventArgs e)
        {
            if(txtNmat.Text != string.Empty)
            {
                if (cs_alunos.sel_list_alunos(txtNmat.Text).Count > 0)
                {
                    MessageBox.Show("Número " + txtNmat.Text + " em uso!", "Falha na Matrícula");

                    txtNmat.Text = "";
                    txtNmat.Focus();
                }
            }
        }

       

        private void nmattxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        private void txtNmat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void estadocivilcmb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.ToString() == "TAB")
            {
                ckbPossuiNecessidade.Focus();
            }
        }

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
        }
        
        #endregion

        #region Situação

        private void termomatcmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            btsalva.Enabled = true;
        }

        private void cmbEnsino_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblEnsino.ForeColor = Color.Black;
        }
        
        #endregion

        #region Nascimento

        private void nascestadocmb_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbNascEstado.Text != string.Empty)
            {
                //gravar - favorito
                cs_enderecos.grava_end_est_favorito(cmbNascEstado.Text);
            }
        }

        private void nasccidadecmb_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbNascCidade.Text != string.Empty)
            {
                //gravar - favorito
                cs_enderecos.grava_end_cid_favorito(cmbNascCidade.Text);
            }
        }

        private void nascpaiscmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            cs_enderecos.list_estados.Clear();

            if (cmbNascPais.Text != "")
            {
                cmbNascEstado.DataSource = null;
                cs_enderecos.pais_selec = cmbNascPais.SelectedItem.ToString();
                cs_enderecos.obter_estados();
                cmbNascEstado.SelectedIndexChanged -= nascestadocmb_SelectedIndexChanged;
                cmbNascEstado.DataSource = cs_enderecos.list_estados;
                //"Descelecionar" estados
                cmbNascEstado.SelectedIndex = -1;


                //Zerar Cidades
                cmbNascCidade.DataSource = null;

                cmbNascEstado.SelectedIndexChanged += nascestadocmb_SelectedIndexChanged;
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
                //cmbNascCidade.SelectedIndexChanged -= nasccidadecmb_SelectedIndexChanged;
                cmbNascCidade.DataSource = cs_enderecos.bscidades;
                //cmbNascCidade.SelectedIndexChanged += nasccidadecmb_SelectedIndexChanged;

            }
            else
            {
                cmbNascCidade.DataSource = null;
            }
        }
        
        #endregion

        #region Endereço

        private void btNovoendere_Click(object sender, EventArgs e)
        {
            using (Endereços.frEndereco fr = new Endereços.frEndereco())
            {
                cmbNascPais.Text = "";
                cmbNascEstado.Text = "";
                cmbNascCidade.Text = "";

                if (fr.ShowDialog() == DialogResult.OK)
                {

                }

                cs_enderecos.list_paises.Clear();
                cmbNascPais.DataSource = null;
                cs_enderecos.obter_paises();
                cmbNascPais.DataSource = cs_enderecos.list_paises;



            }
        }

        private void btLimpar_Click(object sender, EventArgs e)
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

        private void cmbResCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbResCidade.DataSource = null;

            if (cmbResEstado.Text != "")
            {
                cmbResCidade.DataSource = cs_enderecos.lista_cidades(cs_enderecos.troca_estado_nome_sigla(cmbResEstado.Text));
            }
            else
            {
                cmbResCidade.DataSource = null;
            }
        }

        #endregion

        #region Trabalho

        private void cmbTrabEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTrabCidade.DataSource = cs_enderecos.lista_cidades(cs_enderecos.troca_estado_nome_sigla(cmbTrabCidade.Text));
        }

        #endregion      


     
    }
}
