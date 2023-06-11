using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;
using EmatWinFormsNetFramework13032.Fotos;
using System.IO;

namespace EmatWinFormsNetFramework13032.Notas
{
    public partial class frAtrib_nota : Form
    {
        SqlConnection conn = Conexoes.GetSqlConnection();
        SqlQuery sql_ = new SqlQuery(Conexoes.GetSqlConnection());
        public DataTable tab_disciplinas = new DataTable();
        public DataTable tab_PROFESSORESes = new DataTable();

        Configurações.CSconnexoes_txt conf = new Configurações.CSconnexoes_txt();
        Usuarios_Grupos.csUsuarios cs_usuarios = new Usuarios_Grupos.csUsuarios();

        public string id_prof;
        public int inativos = 0;

        public string id_mat_select = "";

        public frAtrib_nota()
        {
            InitializeComponent();
        }

        private void FRatrib_notas_Load(object sender, EventArgs e)
        {
            tab_PROFESSORESes.Columns.Add("id_prof");
            tab_PROFESSORESes.Columns.Add("nome");
            tab_PROFESSORESes.Columns.Add("rg");

            preencheform();
            preencher_cbx_materias();
            getfoto();
            determine_prof_group_id();
            preencher_PROFESSORESes();

            
            cmbMateria.SelectedIndexChanged -= cmbMateria_SelectedIndexChanged;

            cmbPROFESSORESes.Text = cs_usuarios.troca_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_usuario_logado);

            cmbMateria.Text = cs_usuarios.troca_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_disciplina_logado);

            cmbMateria.SelectedIndexChanged += cmbMateria_SelectedIndexChanged;

            if (cmbPROFESSORESes.Text != "")
            {
                cmbPROFESSORESes.Enabled = false;
            }
            if (cmbMateria.Text != "")
            {
                cmbMateria.Enabled = false;
            }

            if (txtEnsino.Text == "FUNDAMENTAL")
            {
                if (cmbMateria.Text == "FÍSICA" || cmbMateria.Text == "QUÍMICA" || cmbMateria.Text == "BIOLOGIA")
                {
                    dtpOrient_fin.Enabled = false;
                    dtpOrient_ini.Enabled = false;
                    nudMedia.Enabled = false;
                    lbAviso.Text = "Aluno do Fundamental. Não é possivel atribui notas.";
                    ckbProf_inativo.Enabled = false;
                    btSalvar.Enabled = false;
                }
            }
            

            verifica_mat();
            traz_mat();
        }

        public void preencheform()
        {
            sql_.Command.CommandText = @"SELECT * FROM ALUNOS WHERE N_MAT=@n_mat";

            sql_.Command.Parameters.AddWithValue("@n_mat", Alunos.CSaluno.numat);

            try
            {
                sql_.Connection.Open();
                SqlDataReader reader = sql_.Command.ExecuteReader();
                while (reader.Read())
                {
                    txtN_mat.Text = reader["N_MAT"].ToString();
                    txtRg.Text = reader["RG"].ToString();
                    txtEnsino.Text = reader["ENSINO"].ToString();
                    txtNome.Text = reader["ALUNO"].ToString();

                    
                    if (reader["ATIVO"].ToString() == "0")
                    {
                        lblAtivo.Text = "Aluno Inativo. Comparecer à Secretaria.";
                        lblAtivo.ForeColor = Color.Red;

                        btSalvar.Enabled = false;
                        dtpOrient_fin.Enabled = false;
                        dtpOrient_ini.Enabled = false;
                        nudMedia.Enabled = false;
                        ckbProf_inativo.Enabled = false;
                    }

                    sql_.Command.Parameters.Clear();


                }
                reader.Close();

                
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.ToUpper().Contains("TIMEOUT EXPIRED"))
                    msg = "Tempo de espera esgotado...";
                MessageBox.Show(msg);
            }
            finally
            {
                sql_.Connection.Close();
            }
        }

        public void preencher_cbx_materias()
        {
            sql_.Command.CommandText =
                @"SELECT * FROM DISCIPLINAS ORDER BY N_DISCIPLINA ";

            sql_.Connection.Open();

            SqlDataReader reader = sql_.Command.ExecuteReader();
            try
            {
                tab_disciplinas.Columns.Add("id_mat");
                tab_disciplinas.Columns.Add("n_mat");

                while (reader.Read())
                {
                    DataRow dr = tab_disciplinas.NewRow();
                    dr[0] = reader["ID_DISCIPLINA"].ToString();
                    dr[1] = reader["N_DISCIPLINA"].ToString();
                    cmbMateria.Items.Add(reader["N_DISCIPLINA"].ToString());

                }

                reader.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.ToUpper().Contains("TIMEOUT EXPIRED"))
                    msg = "Tempo de espera esgotado...";
                MessageBox.Show(msg);
            }
            finally
            {
                sql_.Connection.Close();
            }
        }

        public void getfoto()
        {
            conf.get_configuracoes();

            if (txtN_mat.Text != "")
            {

                if (File.Exists(conf.caminho_fotos + txtN_mat.Text + ".png"))
                {
                    System.Threading.Thread.Sleep(1 * 1000);
                    fotoaluno.Image = Image.FromFile(conf.caminho_fotos + txtN_mat.Text + ".png");
                }
                else if (File.Exists(Fotos.WebCam.path + txtN_mat.Text + ".jpg"))
                {
                    System.Threading.Thread.Sleep(1 * 1000);
                    fotoaluno.Image = Image.FromFile(conf.caminho_fotos + txtN_mat.Text + ".jpg");
                }
                else
                {

                }

            }

        }

        public void determine_prof_group_id()
        {
            sql_.Command.CommandText =
                @"SELECT ID_GRUPO, GRUPO FROM GRUPOS ORDER BY ID_GRUPO ";

            sql_.Connection.Open();

            SqlDataReader reader = sql_.Command.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader["GRUPO"].ToString() == "PROFESSORES")
                    {
                        id_prof = reader["ID_GRUPO"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.ToUpper().Contains("TIMEOUT EXPIRED"))
                    msg = "Tempo de espera esgotado...";
                MessageBox.Show(msg);
            }
            finally
            {
                sql_.Connection.Close();
            }

        }

        public void preencher_PROFESSORESes()
        {

            if (inativos == 0)
            {
                sql_.Command.CommandText =
                    @"SELECT ID_USUARIO, NOME, RG  FROM USUARIO WHERE ID_GRUPO=" + id_prof + " AND INATIVO=0 ORDER BY ID_USUARIO ";
            }
            else
            {
                sql_.Command.CommandText =
                    @"SELECT ID_USUARIO, NOME, RG  FROM USUARIO WHERE ID_GRUPO=" + id_prof + " ORDER BY ID_USUARIO ";
            }
            sql_.Connection.Open();
            SqlDataReader reader = sql_.Command.ExecuteReader();
            try
            {
                cmbPROFESSORESes.Items.Add("");

                int i = 0;
                while (reader.Read())
                {
                    tab_PROFESSORESes.Rows.Add();
                    tab_PROFESSORESes.Rows[i][0] = reader["ID_USUARIO"].ToString();
                    tab_PROFESSORESes.Rows[i][1] = reader["NOME"].ToString();
                    tab_PROFESSORESes.Rows[i][2] = reader["RG"].ToString();
                    cmbPROFESSORESes.Items.Add(reader["NOME"].ToString());
                    i++;
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.ToUpper().Contains("TIMEOUT EXPIRED"))
                    msg = "Tempo de espera esgotado...";
                MessageBox.Show(msg);
            }
            finally
            {

                sql_.Command.Parameters.Clear();
                sql_.Connection.Close();
            }

        }

        public void obter_rg_prof()
        {
            for (int i = 0; i < tab_PROFESSORESes.Rows.Count; i++)
            {
                if (cmbPROFESSORESes.Text == tab_PROFESSORESes.Rows[i][1].ToString())
                {
                    txtRg_prof.Text = tab_PROFESSORESes.Rows[i][2].ToString();
                }
            }
        }

        private void btLocalizar_Click(object sender, EventArgs e)
        {
            Principal.frPrincipal fr = (Principal.frPrincipal)this.MdiParent;

            this.Close();

            fr.tsmEliminacoes.PerformClick();
        }

        private void cmbPROFESSORESes_SelectedIndexChanged(object sender, EventArgs e)
        {
            obter_rg_prof();

            if (cmbMateria.Text != "")
            {
                btSalvar.Enabled = true;
            }
            else
            {
                btSalvar.Enabled = false;
            
            }
        }

        private void ckbProf_inativo_CheckedChanged(object sender, EventArgs e)
        {
            tab_PROFESSORESes.Clear();
            cmbPROFESSORESes.Items.Clear();

            if (ckbProf_inativo.Checked)
            {
                inativos = 1;
            }
            else
            {
                inativos = 0;
            }
            preencher_PROFESSORESes();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSalvar_Click(object sender, EventArgs e)
        {
            if (txtRg_prof.Text != "")
            {
                if (cmbPROFESSORESes.Text != "")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //"PORT. e LEIT. e PROD. TEXTOS = ORIENT_INI_1
                    //"HISTÓRIA = ORIENT_INI_2
                    //"GEOGRAFIA = ORIENT_INI_3
                    //"FÍSICA = ORIENT_INI_4
                    //"QUÍMICA = ORIENT_INI_5
                    //"BIOLOGIA = ORIENT_INI_6
                    //"MATEMÁTICA = ORIENT_INI_7
                    //"INGLÊS = ORIENT_INI_8
                    //"ARTE = ORIENT_INI_9
                    //"FILOSOFIA = ORIENT_INI_10
                    //"SOCIOLOGIA = ORIENT_INI_11

                    //LÍNGUA PORTUGUESA E LIT. = ORIENT_INI_1
                    //HISTÓRIA = ORIENT_INI_2
                    //GEOGRAFIA = ORIENT_INI_3
                    //CIÊNCIAS = ORIENT_INI_4
                    //MATEMÁTICA = ORIENT_INI_5
                    //INGLÊS = ORIENT_INI_6
                    //ARTE = ORIENT_INI_7

                    #region instrução
                    string mat;

                    #region FUNDAMENTAL
                    if (txtEnsino.Text == "FUNDAMENTAL")
                    {
                        switch (cmbMateria.Text)
                        {
                            case "PORTUGUÊS":
                                mat = "1";
                                break;
                            case "HISTÓRIA":
                                mat = "2";
                                break;
                            case "GEOGRAFIA":
                                mat = "3";
                                break;
                            case "CIÊNCIAS":
                                mat = "4";
                                break;
                            case "MATEMÁTICA":
                                mat = "5";
                                break;
                            case "INGLÊS":
                                mat = "6";
                                break;
                            default:
                                mat = "7";
                                break;
                        }
                    }
                    #endregion FUNDAMENTAL

                    else

                    #region MÉDIO
                    {
                        switch (cmbMateria.Text)
                        {
                            case "PORTUGUÊS":
                                mat = "1";
                                break;
                            case "HISTÓRIA":
                                mat = "2";
                                break;
                            case "GEOGRAFIA":
                                mat = "3";
                                break;
                            case "FÍSICA":
                                mat = "4";
                                break;
                            case "QUÍMICA":
                                mat = "5";
                                break;
                            case "BIOLOGIA":
                                mat = "6";
                                break;
                            case "MATEMÁTICA":
                                mat = "7";
                                break;
                            case "INGLÊS":
                                mat = "8";
                                break;
                            case "ARTE":
                                mat = "9";
                                break;
                            case "FILOSOFIA":
                                mat = "10";
                                break;
                            default:
                                mat = "11";
                                break;
                        }

                    }
                    #endregion

                    string A = "INS_" + mat;
                    string B = "MU_" + mat;
                    string C = "UF_" + mat;
                    string D = "DT_" + mat;
                    string E = "NOT_FIN_" + mat;
                    string F = "ORIENT_INI_" + mat;
                    string G = "ORIENTADOR_" + mat;

                    if (txtEnsino.Text == "FUNDAMENTAL")
                    {
                        sql_.Command.CommandText =
                            @"UPDATE HIST_ALUNO_F SET " + A + "=@ins," + B + "=@mu," + C + "=@uf," + D + "=@orint_fin," + E + "=@media," + F + "=@orint_ini," + G + "=@orientador WHERE N_MAT = @n_mat";
                    }
                    else
                    {
                        sql_.Command.CommandText =
                            @"UPDATE HIST_ALUNO_M SET " + A + "=@ins," + B + "=@mu," + C + "=@uf," + D + "=@orint_fin," + E + "=@media," + F + "=@orint_ini," + G + "=@orientador WHERE N_MAT = @n_mat";
                    }
                    sql_.Command.Parameters.AddWithValue("@n_mat", txtN_mat.Text);
                    sql_.Command.Parameters.AddWithValue("@ins", "CEEJA PROFº NORBERTO SOARES RAMOS");
                    sql_.Command.Parameters.AddWithValue("@mu", "SOROCABA");
                    sql_.Command.Parameters.AddWithValue("@uf", "SP");
                    sql_.Command.Parameters.AddWithValue("@orint_fin", dtpOrient_fin.Value.ToString("dd/MM/yyyy"));
                    sql_.Command.Parameters.AddWithValue("@media", nudMedia.Value);
                    sql_.Command.Parameters.AddWithValue("@orint_ini", dtpOrient_ini.Value);
                    sql_.Command.Parameters.AddWithValue("@orientador", cmbPROFESSORESes.Text);
                }

                    #endregion

                try
                {
                    if (sql_.Connection.State != ConnectionState.Closed)
                    {
                        sql_.Connection.Close();
                    }
                    sql_.Connection.Open();
                    sql_.Command.ExecuteNonQuery();
                    btSalvar.Enabled = false;
                    btCancelar.Text = "Fechar";
                }
                catch (Exception ex)
                {
                    Error_Log.csControle_erros.exibir_erro(ex.Message);
                }
                finally
                {
                    sql_.Command.Parameters.Clear();
                    sql_.Connection.Close();
                }
            }

            if (MessageBox.Show("Atribuir nova matéria ao Aluno ?", "Notas", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                SqlCommand sql_comm1 = new SqlCommand();

                sql_comm1.Connection = conn;
                sql_comm1.CommandText = @"SELECT * FROM ALUNOS WHERE N_MAT=@n_mat";

                sql_comm1.Parameters.AddWithValue("@n_mat", txtN_mat.Text);

                try
                {
                    conn.Open();
                    SqlDataReader reader = sql_comm1.ExecuteReader();

                    while (reader.Read())
                    {
                        Alunos.CSaluno.ensino_aluno = reader["ENSINO"].ToString();
                        if (reader["ID_DISCIPLINA_ATUAL"] != DBNull.Value)
                        {
                            Alunos.CSaluno.id_disciplina_atual = Convert.ToInt32(reader["ID_DISCIPLINA_ATUAL"]);
                        }
                        
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }


                Notas.frAtrib_materia form = new Notas.frAtrib_materia();
                form.MdiParent = this.ParentForm;
                form.Show();
                this.Close();
            }
            else
            {
                this.Close();
            }

            Close();

            
        }

        public void verifica_mat()
        {

            if (cmbMateria.Text != "")
            {
                sql_.Command.CommandText =
                    @"SELECT * FROM DISCIPLINAS WHERE N_DISCIPLINA='" + cmbMateria.Text + "'";


                sql_.Connection.Open();
                SqlDataReader reader = sql_.Command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        id_mat_select = reader["ID_DISCIPLINA"].ToString();
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    if (msg.ToUpper().Contains("TIMEOUT EXPIRED"))
                        msg = "Tempo de espera esgotado...";
                    MessageBox.Show(msg);
                }
                finally
                {

                    sql_.Command.Parameters.Clear();
                    sql_.Connection.Close();
                }
            }
        }

        public void traz_mat()
        {
            if (cmbMateria.Text != "")
            {
                string mat;

                #region FUNDAMENTAL
                if (txtEnsino.Text == "FUNDAMENTAL")
                {
                    switch (cmbMateria.Text)
                    {
                        case "PORTUGUÊS":
                            mat = "1";
                            break;
                        case "HISTÓRIA":
                            mat = "2";
                            break;
                        case "GEOGRAFIA":
                            mat = "3";
                            break;
                        case "CIÊNCIAS":
                            mat = "4";
                            break;
                        case "MATEMÁTICA":
                            mat = "5";
                            break;
                        case "INGLÊS":
                            mat = "6";
                            break;
                        default:
                            mat = "7";
                            break;
                    }
                }
                #endregion FUNDAMENTAL

                else

                #region MÉDIO
                {
                    switch (cmbMateria.Text)
                    {
                        case "PORTUGUÊS":
                            mat = "1";
                            break;
                        case "HISTÓRIA":
                            mat = "2";
                            break;
                        case "GEOGRAFIA":
                            mat = "3";
                            break;
                        case "FÍSICA":
                            mat = "4";
                            break;
                        case "QUÍMICA":
                            mat = "5";
                            break;
                        case "BIOLOGIA":
                            mat = "6";
                            break;
                        case "MATEMÁTICA":
                            mat = "7";
                            break;
                        case "INGLÊS":
                            mat = "8";
                            break;
                        case "ARTE":
                            mat = "9";
                            break;
                        case "FILOSOFIA":
                            mat = "10";
                            break;
                        default:
                            mat = "11";
                            break;
                    }

                }
                #endregion

                string D = "DT_" + mat;
                string E = "NOT_FIN_" + mat;
                string F = "ORIENT_INI_" + mat;
                string G = "ORIENTADOR_" + mat;

                if (txtEnsino.Text == "FUNDAMENTAL")
                {
                    sql_.Command.CommandText =
                        @"SELECT * FROM HIST_ALUNO_F WHERE N_MAT=" + txtN_mat.Text;
                }
                else
                {
                    sql_.Command.CommandText =
                           @"SELECT * FROM HIST_ALUNO_M WHERE N_MAT=" + txtN_mat.Text;

                }


                sql_.Connection.Open();
                SqlDataReader reader = sql_.Command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        dtpOrient_fin.Text = reader[D].ToString();
                        if (reader[E] != DBNull.Value)
                        {
                            nudMedia.Value = Convert.ToDecimal(reader[E]);
                        }

                        dtpOrient_ini.Text = reader[F].ToString();
                        if(reader[G].ToString() != "")
                        {
                            cmbPROFESSORESes.Text = reader[G].ToString();
                        }
                        

                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    if (msg.ToUpper().Contains("TIMEOUT EXPIRED"))
                        msg = "Tempo de espera esgotado...";
                    MessageBox.Show(msg);
                }
                finally
                {

                    sql_.Command.Parameters.Clear();
                    sql_.Connection.Close();
                }
            }

        }

        private void cmbMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPROFESSORESes.Text = "";
            nudMedia.Value = 0;
            dtpOrient_fin.Text = "";
            dtpOrient_ini.Text = "";
            txtRg_prof.Text = "";

            verifica_mat();
            traz_mat();

            if(cmbPROFESSORESes.Text != "")
            {
                btSalvar.Enabled = true;
            }
            else
            {
                btSalvar.Enabled = false;

            }
            
        }

        private void FRatrib_notas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btSalvar.Enabled == true)
            {
                if (MessageBox.Show("A nota atribuida não foi salva. Tem certeza que deseja Sair ?", "Atribuição de notas",MessageBoxButtons.YesNo) == DialogResult.Yes)                
                {
                    
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void nudMedia_ValueChanged(object sender, EventArgs e)
        {
            btSalvar.Enabled = true;
        }

        private void dtpOrient_ini_ValueChanged(object sender, EventArgs e)
        {
            btSalvar.Enabled = true;
        }

        private void dtpOrient_fin_ValueChanged(object sender, EventArgs e)
        {
            btSalvar.Enabled = true;
        }

        private void FRatrib_notas_Shown(object sender, EventArgs e)
        {
            
            if (txtEnsino.Text == string.Empty)
            {
                MessageBox.Show("Ensino não Informado. Solicitar à Secretaria, a revisão do cadastro.", "Fundamental ou Médio ?");
                dtpOrient_fin.Enabled = false;
                dtpOrient_ini.Enabled = false;
                nudMedia.Enabled = false;
                cmbMateria.Enabled = false;
                cmbPROFESSORESes.Enabled = false;

            }
            
            btSalvar.Enabled = false;
        }
    }
}