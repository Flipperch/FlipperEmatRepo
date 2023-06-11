using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Data.OleDb;

using EmatWinFormsNetFramework13032.Digitais.Entity;
using System.Threading;

using NITGEN.SDK.NBioBSP;

namespace EmatWinFormsNetFramework13032.Usuarios_Grupos
{
    public partial class frTabconf_Sistema : Form
    {
        Notas.csDisciplinas cs_disciplinas = new Notas.csDisciplinas();
        Usuarios_Grupos.csUsuarios cs_usuarios = new Usuarios_Grupos.csUsuarios();
        csGrupos cs_grupos = new csGrupos();

        Notas.csAtendimentos cs_atendimentos = new Notas.csAtendimentos();

        string id_prof_e_monitor;

        int idgruponew = 0;

        string novousuario;
        string novoid;

        int cadastro = 0;
        int a = 2;

        //string atual = "";

        string id_user_log_select = "";

        string novo_id;

        // - Tab Users
        SqlQuery users = new SqlQuery(Conexoes.GetSqlConnection());
        List<string> idsgrupo = new List<string>();
        BindingSource bsUsers = new BindingSource();
        // - Tab Groups
        SqlQuery groups = new SqlQuery(Conexoes.GetSqlConnection());
        //List<string> idsgrupo = new List<string>();
        BindingSource bsGroups = new BindingSource();
        // - Tab - professores e professores
        SqlQuery professores = new SqlQuery(Conexoes.GetSqlConnection());
        BindingSource bsprofessores = new BindingSource();
        public List<int> professores_select = new List<int>();
        public List<string> materias = new List<string>();
        public List<string> id_materias = new List<string>();
        public DataTable tab_disciplinas = new DataTable();

        public DataTable dt_prof_ment = new DataTable();


        // - Tab disciplinas
        SqlQuery disciplinas = new SqlQuery(Conexoes.GetSqlConnection());
        List<string> idsdisciplinas = new List<string>();
        BindingSource bsdisciplinas = new BindingSource();
        // - Tab - Logs
        SqlQuery logs = new SqlQuery(Conexoes.GetSqlConnection());
        BindingSource bslogs = new BindingSource();
        public List<int> fun_select = new List<int>();

        // - Tab - Horarios
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        public DateTime ultimo_horario;
        public DataTable tab_materias = new DataTable();

        public int id_mat_select;

        public int id_disciplina_selecionada_distribuicao;

        public string hor_aula_m = "m|";
        public string hor_aula_t = "t|";
        public string hor_aula_n = "n";

        public int carga_disp = 100;

        List<CheckBox> listadeckb = new List<CheckBox>();

        private void FRtabConf_acesso_Load(object sender, EventArgs e)
        {
            fill_tab_users(); //Revisar

            preencher_tab_grupos();

            fill_tab_logs(); //Revisar

            preencher_tab_disciplinas();

            fill_tab_professores();
            preencher_tab_horarios();

            preencher_tab_ensinos();
            preencher_tab_areas();
            preencher_tab_professores_mentores();



        }

        //----------------------------------------------------------------------------------------

        #region Tab Grupos -----------------

        public void preencher_tab_grupos()
        {
            preencher_ltvGrupos();

        }
        public void preencher_ltvGrupos()
        {
            ltvGrupos.Items.Clear();
            //cmbGrupo.Items.Clear();

            List<csGrupos> list_ = cs_grupos.lista_grupos();

            for (int i = 0; i < list_.Count; i++)
            {
                ltvGrupos.Items.Add(list_[i].n_grupo);


                //Prrencher cmbGrupos - Tab Usuarios
                //cmbGrupo.Items.Add(list_[i].n_grupo);
            }
        }
        //MARCAR CKB DE PERMISSOES DE ACORDO COM O GRUPO SELECIONADO
        private void ltvGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///NO SELECT INDEX CHANGED, É RODADO DUAS VEZES, UM PARA O SELECIONADO E OUTRA PARA O NOVO SELECIONADO
            //RESOLVER
            ckbAcessoSecretaria.CheckedChanged -= ckbAcessoSecretaria_CheckedChanged;
            ckbAcessoProfessores.CheckedChanged -= ckbAcessoSecretaria_CheckedChanged;
            ckbAcessoRelatorios.CheckedChanged -= ckbAcessoSecretaria_CheckedChanged;
            ckbAcessoFerramentas.CheckedChanged -= ckbAcessoSecretaria_CheckedChanged;

            foreach (CheckBox ckb in grbPermissoes.Controls.OfType<CheckBox>())
            {
                ckb.Enabled = false;
                ckb.Checked = false;
            }

            if (ltvGrupos.SelectedItems.Count > 0)
            {
                List<csGrupos> list_ = cs_grupos.lista_grupos();

                for (int i = 0; i < list_.Count; i++)
                {
                    if (list_[i].n_grupo == ltvGrupos.SelectedItems[0].Text)
                    {
                        foreach (CheckBox ckb in grbPermissoes.Controls.OfType<CheckBox>())
                        {
                            ckb.Enabled = true;

                            if (list_[i].permissoes != null)
                                if (list_[i].permissoes.Contains(cs_grupos.troca_permissao_sistema_nome_por_id(troca_ckbs_permissoes_nome_por_permissao(ckb.Name))))
                                {
                                    ckb.Checked = true;
                                }
                        }
                    }
                }
            }

            ckbAcessoSecretaria.CheckedChanged += ckbAcessoSecretaria_CheckedChanged;
            ckbAcessoProfessores.CheckedChanged += ckbAcessoSecretaria_CheckedChanged;
            ckbAcessoRelatorios.CheckedChanged += ckbAcessoSecretaria_CheckedChanged;
            ckbAcessoFerramentas.CheckedChanged += ckbAcessoSecretaria_CheckedChanged;
        }
        public string troca_ckbs_permissoes_nome_por_permissao(string nome_ckb)
        {
            string a = "";

            switch (nome_ckb)
            {
                case "ckbAcessoSecretaria":
                    a = "tsmSecretaria";
                    break;
                case "ckbAcessoProfessores":
                    a = "tsmProfessores";
                    break;
                case "ckbAcessoRelatorios":
                    a = "tsmRelatorios";
                    break;
                case "ckbAcessoFerramentas":
                    a = "tsmFerramentas";
                    break;
            }

            return a;
        }
        //ADD NOVO GRUPO
        private void txtNovoGrupo_TextChanged(object sender, EventArgs e)
        {
            if (txtNovoGrupo.Text != String.Empty) btnNovoGrupo.Enabled = true;
            else btnNovoGrupo.Enabled = false;
        }
        private void btnNovoGrupo_Click(object sender, EventArgs e)
        {
            cs_grupos.add_novo_grupo(txtNovoGrupo.Text);
            preencher_ltvGrupos();
            txtNovoGrupo.Text = "";
            btnNovoGrupo.Enabled = false;
        }
        //UPDATE CKB DE PERMISSOES
        private void ckbAcessoSecretaria_CheckedChanged(object sender, EventArgs e)
        {
            upd_permissoes();
        }
        public void upd_permissoes()
        {
            List<int> list_ = new List<int>();

            foreach (CheckBox ckb in grbPermissoes.Controls.OfType<CheckBox>())
            {
                if (ckb.Checked)
                {
                    switch (ckb.Name)
                    {
                        case "ckbAcessoSecretaria":
                            list_.Add(cs_grupos.troca_permissao_sistema_nome_por_id("tsmSecretaria"));
                            break;
                        case "ckbAcessoProfessores":
                            list_.Add(cs_grupos.troca_permissao_sistema_nome_por_id("tsmProfessores"));
                            break;
                        case "ckbAcessoRelatorios":
                            list_.Add(cs_grupos.troca_permissao_sistema_nome_por_id("tsmRelatorios"));
                            break;
                        case "ckbAcessoFerramentas":
                            list_.Add(cs_grupos.troca_permissao_sistema_nome_por_id("tsmFerramentas"));
                            break;
                    }
                }
            }

            cs_grupos.upd_permissoes_grupo(cs_grupos.troca_grupo_nome_por_id(ltvGrupos.SelectedItems[0].Text), list_);
        }

        #endregion

        //-----------------------------------------------------------------------------------------

        #region Tab Usuários ---------------

        public void fill_tab_users()
        {
            preenchercmbgrupo();
            dgvUsuarios.CellEnter -= dgvUsuarios_CellEnter;
            preencherdgv_usuarios();
            bsUsers.DataSource = users.Table;
            dgvUsuarios.DataSource = bsUsers;


            troca_id_nomegrupo();

            dgvUsuarios.Columns[0].Width = 60;
            dgvUsuarios.Columns[1].Width = 70;
            dgvUsuarios.Columns[2].Width = 300;
            dgvUsuarios.Columns[3].Width = 85;
            dgvUsuarios.Columns[4].Width = 50;
            dgvUsuarios.Columns[5].Width = 125;
            dgvUsuarios.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvUsuarios.CellEnter += dgvUsuarios_CellEnter;

        }

        public void preencherdgv_usuarios()
        {

            if (!ckbUsersinativos.Checked)
            {
                users.Command.CommandText =
                @"SELECT ID_USUARIO, USUARIO, ID_GRUPO, NOME, RG, ID_CARTAO FROM USUARIO WHERE INATIVO=0 ORDER BY USUARIO ";
            }
            else
            {
                users.Command.CommandText =
                @"SELECT ID_USUARIO, USUARIO, ID_GRUPO, NOME, RG, ID_CARTAO FROM USUARIO ORDER BY USUARIO ";
            }

            try
            {
                if (users.Fill() >= 0)
                {

                }
                else MessageBox.Show(users.Error.Message);

                for (int i = 0; i < users.Table.Rows.Count; i++)
                {
                    idsgrupo.Add(users.Table.Rows[i][a].ToString());
                }
                users.Table.Columns.RemoveAt(a);

                if (cadastro == 0)
                {
                    users.Table.Columns.Add("GRUPO", System.Type.GetType("System.String"));
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

            }



        }

        private void dgvUsuarios_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            btExcluir_usuario.Enabled = true;

            int id_user_select = Convert.ToInt32(dgvUsuarios.Rows[dgvUsuarios.CurrentRow.Index].Cells[0].Value.ToString());

            if (cs_usuarios.esta_inativo(id_user_select))
            {
                btExcluir_usuario.Text = "Ativar";
            }
            else
            {
                btExcluir_usuario.Text = "Inativar";
            }


            verfica_digital();
        }

        public void verfica_digital()
        {
            users.Command.CommandText =
                @"SELECT * FROM ACESSO_CATRACA WHERE ID_CARTAO = @n_mat";


            users.Command.Parameters.AddWithValue("@n_mat", dgvUsuarios.Rows[dgvUsuarios.CurrentRow.Index].Cells[0].Value);

            users.Connection.Open();
            try
            {
                string template = "";

                SqlDataReader reader = users.Command.ExecuteReader();

                while (reader.Read())
                {
                    template = reader["TEMPLATE1"].ToString();

                }
                if (template == "")
                {
                    //btObterdigital.Enabled = true;
                    //lbDigital.ForeColor = Color.DarkRed;
                    //lbDigital.Text = "DIGITAL NÃO CADASTRADA";
                    //btObterdigital.Text = "Obter Digital"; 

                }
                else
                {
                    //btObterdigital.Enabled = true;
                    //lbDigital.ForeColor = Color.Black;
                    //lbDigital.Text = "DIGITAL CADASTRADA";
                    //btObterdigital.Text = "Editar Digital"; 
                }


                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                users.Command.Parameters.Clear();
                users.Connection.Close();

            }
        }

        public frTabconf_Sistema()
        {
            InitializeComponent();
        }

        public void troca_id_nomegrupo()
        {

            for (int i = 0; i < dgvUsuarios.RowCount; i++)
            {
                users.Command.CommandText = @"SELECT * FROM grupos where ID_grupo=@id_grupo";

                users.Command.Parameters.AddWithValue("@id_grupo", idsgrupo[i].ToString());

                try
                {
                    if (users.Connection.State != ConnectionState.Closed)
                    {
                        users.Connection.Close();
                    }
                    users.Connection.Open();

                    SqlDataReader reader = users.Command.ExecuteReader();

                    while (reader.Read())
                    {
                        dgvUsuarios.Rows[i].Cells[5].Value = reader["GRUPO"].ToString();
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
                users.Command.Parameters.Clear();
                users.Connection.Close();



            }



        }

        public void excluir_usuario()
        {


        }

        private void cmbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btCadastrarUsuario.Enabled = true;
        }

        public void preenchercmbgrupo()
        {
            users.Command.CommandText =
                @"SELECT * FROM grupos";
            try
            {
                List<string> grupos = new List<string>();
                grupos.Add("");
                if (users.Connection.State != ConnectionState.Closed)
                {
                    users.Connection.Close();
                }
                users.Connection.Open();

                SqlDataReader reader = users.Command.ExecuteReader();

                while (reader.Read())
                {
                    grupos.Add(reader["GRUPO"].ToString());
                }
                reader.Close();
                cmbGrupo.DataSource = grupos;
                btCadastrarUsuario.Enabled = false;

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.ToUpper().Contains("TIMEOUT EXPIRED"))
                    msg = "Tempo de espera esgotado...";

                MessageBox.Show(msg);
            }
            users.Connection.Close();
        }

        public void getidgrupo()
        {
            users.Command.CommandText =
                @"SELECT * FROM grupos WHERE grupo=@grupo";

            users.Command.Parameters.AddWithValue("@grupo", cmbGrupo.Text);
            try
            {
                if (users.Connection.State != ConnectionState.Closed)
                {
                    users.Connection.Close();
                }

                users.Connection.Open();

                SqlDataReader reader = users.Command.ExecuteReader();

                while (reader.Read())
                {
                    idgruponew = Convert.ToInt32(reader["ID_GRUPO"]);
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
            users.Connection.Close();
        }

        public void atualizadgv()
        {
            cadastro = 1;
            a = 6;
            idsgrupo.Clear();

            bsUsers.DataSource = null;
            dgvUsuarios.Rows.Clear();
            preencherdgv_usuarios();
            bsUsers.DataSource = users.Table;
            dgvUsuarios.DataSource = bsUsers;
            troca_id_nomegrupo();
            cadastro = 0;

            dgvUsuarios.Columns[0].Width = 60;
            dgvUsuarios.Columns[1].Width = 70;
            dgvUsuarios.Columns[2].Width = 300;
            dgvUsuarios.Columns[3].Width = 85;
            dgvUsuarios.Columns[4].Width = 50;
            dgvUsuarios.Columns[5].Width = 125;

        }

        public void cadastrar_usuario()
        {
            users.Command.CommandText =
                @"INSERT INTO USUARIO (USUARIO, ID_GRUPO, PSW, NOME, RG, INATIVO)
                VALUES ( @usuario, @idgrupo, @psw, @nome, @rg, @inativo)";

            users.Command.Parameters.AddWithValue("@usuario", txtLogin.Text);
            users.Command.Parameters.AddWithValue("@idgrupo", idgruponew);
            users.Command.Parameters.AddWithValue("@psw", "123456");
            users.Command.Parameters.AddWithValue("@nome", txtNome_usuario.Text);
            users.Command.Parameters.AddWithValue("@rg", txtRG.Text);
            users.Command.Parameters.AddWithValue("@inativo", 0);



            try
            {
                if (users.Connection.State != ConnectionState.Closed)
                {
                    users.Connection.Close();
                }
                users.Connection.Open();
                users.Command.ExecuteNonQuery();
                users.Command.Parameters.Clear();
                //nmattxt.Enabled = false;
                //MessageBox.Show("Cadastrado com Sucesso", "Cadastro!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                users.Command.Parameters.Clear();
                users.Connection.Close();


                atualizadgv();


            }
        }

        public void cadastrar_usuario_catraca()
        {
            users.Command.CommandText =
                @"INSERT INTO ACESSO_CATRACA (ID_CARTAO, ESTADO)
                VALUES ( @idcartao, @estado )";

            //ID  cartão + digito rg ?

            string string_rg = txtRG.Text;

            if (string_rg.Contains('.'))
            {
                string_rg = string_rg.Remove(string_rg.IndexOf('.'));

                if (string_rg.Count() > 1)
                {
                    novo_id = novoid + string_rg;
                }
                else
                {
                    novo_id = novoid + "0" + string_rg;

                }
            }
            else
            {
                novo_id = novoid + string_rg.Remove(2);
            }

            users.Command.Parameters.AddWithValue("@idcartao", novo_id);
            users.Command.Parameters.AddWithValue("@estado", 0);

            try
            {
                if (users.Connection.State != ConnectionState.Closed)
                {
                    users.Connection.Close();
                }
                users.Connection.Open();
                users.Command.ExecuteNonQuery();
                users.Command.Parameters.Clear();
                //nmattxt.Enabled = false;
                //MessageBox.Show("Cadastrado com Sucesso", "Cadastro!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                users.Command.Parameters.Clear();
                users.Connection.Close();


                atualizadgv();


            }
        }

        public void atualiza_usuario_id_cartao()
        {
            users.Command.CommandText = @"UPDATE USUARIO SET ID_CARTAO = @id                    
                                                             
                                                             where id_usuario=@iduser";


            users.Command.Parameters.AddWithValue("@id", novo_id);
            users.Command.Parameters.AddWithValue("@iduser", novoid);


            try
            {
                if (users.Connection.State != ConnectionState.Closed)
                {
                    users.Connection.Close();
                }
                users.Connection.Open();
                users.Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.ToUpper().Contains("timeout expired"))
                    msg = "tempo de espera esgotado...";

                MessageBox.Show(msg);
            }
            users.Command.Parameters.Clear();
            users.Connection.Close();
            atualizadgv();

        }

        public void atualiza_usuario()
        {
            users.Command.CommandText = @"UPDATE USUARIO SET USUARIO = @usuario,
                                                             NOME = @nome,
                                                             RG = @rg
                                                             where id_usuario=@iduser";

            users.Command.Parameters.AddWithValue("@usuario", dgvUsuarios.Rows[dgvUsuarios.CurrentCell.RowIndex].Cells[1].Value);
            users.Command.Parameters.AddWithValue("@nome", dgvUsuarios.Rows[dgvUsuarios.CurrentCell.RowIndex].Cells[2].Value);
            users.Command.Parameters.AddWithValue("@rg", dgvUsuarios.Rows[dgvUsuarios.CurrentCell.RowIndex].Cells[3].Value);
            users.Command.Parameters.AddWithValue("@iduser", dgvUsuarios.Rows[dgvUsuarios.CurrentCell.RowIndex].Cells[0].Value);

            try
            {
                if (users.Connection.State != ConnectionState.Closed)
                {
                    users.Connection.Close();
                }
                users.Connection.Open();
                users.Command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.ToUpper().Contains("timeout expired"))
                    msg = "tempo de espera esgotado...";

                MessageBox.Show(msg);
            }
            users.Command.Parameters.Clear();
            users.Connection.Close();
            atualizadgv();

        }

        private void dgvUsuarios_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            atualiza_usuario();
        }

        public void get_new_id()
        {
            users.Command.CommandText =
                @"SELECT ID_USUARIO FROM USUARIO WHERE RG = @rg";

            users.Command.Parameters.AddWithValue("@rg", novousuario);
            users.Connection.Open();

            SqlDataReader reader = users.Command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    novoid = reader["ID_USUARIO"].ToString();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                users.Command.Parameters.Clear();
                users.Connection.Close();
            }
        }

        public void cad_tabela_prof()
        {
            users.Command.CommandText =
                @"INSERT INTO PROFESSORES (ID_USUARIO)
                VALUES ( @id_usuario)";

            users.Command.Parameters.AddWithValue("@id_usuario", novoid);

            try
            {
                if (users.Connection.State != ConnectionState.Closed)
                {
                    users.Connection.Close();
                }
                users.Connection.Open();
                users.Command.ExecuteNonQuery();
                users.Command.Parameters.Clear();
                //nmattxt.Enabled = false;
                //MessageBox.Show("Cadastrado com Sucesso", "Cadastro!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                users.Command.Parameters.Clear();
                users.Connection.Close();
                txtNome_usuario.Text = "";
                txtRG.Text = "";
                txtLogin.Text = "";
                cmbGrupo.Text = "";

                atualizadgv();


            }
        }

        private void btAtribsenhapadrao_Click(object sender, EventArgs e)
        {
            DialogResult s_n = MessageBox.Show("Tem certeza que deseja atribuir a senha padrão 123456 ao usuario Selecionado ?", "Senha Padrão", MessageBoxButtons.YesNo);
            if (s_n == DialogResult.Yes)
            {
                users.Command.CommandText = @"UPDATE USUARIO SET PSW = 123456 where id_usuario=@iduser";

                users.Command.Parameters.AddWithValue("@iduser", dgvUsuarios.Rows[dgvUsuarios.CurrentCell.RowIndex].Cells[0].Value);

                try
                {
                    if (users.Connection.State != ConnectionState.Closed)
                    {
                        users.Connection.Close();
                    }
                    users.Connection.Open();
                    users.Command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    if (msg.ToUpper().Contains("timeout expired"))
                        msg = "tempo de espera esgotado...";

                    MessageBox.Show(msg);
                }
                users.Command.Parameters.Clear();
                users.Connection.Close();
                atualizadgv();
            }
            else if (s_n == DialogResult.No)
            {

            }

        }

        private void dgvUsuarios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btExcluir_usuario.PerformClick();
            }
        }

        private void btCadastrarUsuario_Click(object sender, EventArgs e)
        {

            bsUsers.RemoveSort();

            List<string> lista_usuarios_nome = new List<string>();
            List<string> lista_usuarios_login = new List<string>();


            for (int i = 0; i < dgvUsuarios.Rows.Count; i++)
            {
                lista_usuarios_nome.Add(dgvUsuarios.Rows[i].Cells[2].Value.ToString());
                lista_usuarios_login.Add(dgvUsuarios.Rows[i].Cells[1].Value.ToString());
            }


            novousuario = txtRG.Text;

            if (txtNome_usuario.Text != "")
            {
                if (txtLogin.Text != "")
                {
                    if (!lista_usuarios_login.Contains(txtLogin.Text))
                    {
                        if (cmbGrupo.Text != "")
                        {
                            if (txtRG.Text != "")
                            {
                                getidgrupo();
                                cadastrar_usuario();
                                get_new_id();
                                cadastrar_usuario_catraca();

                                atualiza_usuario_id_cartao();

                                if (cmbGrupo.Text == "PROFESSORES")
                                {
                                    cad_tabela_prof();
                                }

                                txtNome_usuario.Text = "";
                                txtRG.Text = "";
                                txtLogin.Text = "";
                                cmbGrupo.Text = "";

                                fill_tab_professores();
                            }
                            else
                            {
                                MessageBox.Show("Falta informaro RG.", "Cadastrar Usuário");
                            }


                        }
                        else
                        {
                            MessageBox.Show("Falta selecionar o Grupo.", "Cadastrar Usuário");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Login já esta em uso!", "Cadastrar Usuário");

                    }
                }
                else
                {
                    MessageBox.Show("Falta informar o Login.", "Cadastrar Usuário");
                }
            }
            else
            {
                MessageBox.Show("Falta informar o Nome.", "Cadastrar Usuário");
            }



        }

        private void btExcluir_usuario_Click(object sender, EventArgs e)
        {
            int id_user_select = Convert.ToInt32(dgvUsuarios.Rows[dgvUsuarios.CurrentRow.Index].Cells[0].Value.ToString());

            if (btExcluir_usuario.Text == "Inativar")
            {
                DialogResult s_n = MessageBox.Show("Tem certeza que deseja desabilitar o usuario Selecionado ?", "Desabilitar Usuário", MessageBoxButtons.YesNo);
                if (s_n == DialogResult.Yes)
                {
                    //Inativar
                    cs_usuarios.ativa_ou_inativar(id_user_select, 1);
                    atualizadgv();
                }
                else if (s_n == DialogResult.No)
                {

                }
            }
            else
            {
                DialogResult s_n = MessageBox.Show("Tem certeza que deseja ativar o usuario Selecionado ?", "Ativar Usuário", MessageBoxButtons.YesNo);
                if (s_n == DialogResult.Yes)
                {
                    //Ativar
                    cs_usuarios.ativa_ou_inativar(id_user_select, 0);
                    atualizadgv();
                }
                else if (s_n == DialogResult.No)
                {

                }
            }
        }

        private void ckbUsersinativos_CheckedChanged(object sender, EventArgs e)
        {
            atualizadgv();

        }

        private void btObterdigital_Click(object sender, EventArgs e)
        {

        }

        #endregion

        //-----------------------------------------------------------------------------------------        

        #region Tab Log Acesso_sistema

        public void fill_tab_logs()
        {
            preencherdgv_log_acesso();
            preencher_cmb_users();

        }

        public void preencherdgv_log_acesso()
        {
            if (id_user_log_select != "")
            {
                logs.Command.CommandText =
                    @"SELECT * FROM ACESSO_SISTEMA WHERE ID_USUARIO LIKE @id_usuario";

                logs.Command.Parameters.AddWithValue("@id_usuario", id_user_log_select);

                logs.Connection.Open();

                try
                {
                    DataTable dt = new DataTable();

                    dt.Columns.Add("Entrada", typeof(DateTime));

                    dt.Columns.Add("Saida", typeof(DateTime));

                    SqlDataReader reader = logs.Command.ExecuteReader();
                    while (reader.Read())
                    {

                        int i = 0;

                        string[] ents = (reader["ACESSO_ENT"].ToString()).Split('|');
                        while (i < ents.Length)
                        {
                            if (ents[i] != null)
                            {

                                if (ents[i] != "")
                                {
                                    DataRow dr = dt.NewRow();
                                    dr[0] = DateTime.Parse(ents[i]);
                                    dt.Rows.Add(dr);
                                }
                            }

                            i++;
                        }

                        i = 0;

                        if (reader["ACESSO_SAI"].ToString() != "")
                        {
                            string[] saids = ((reader["ACESSO_SAI"].ToString()).Remove(0, 1)).Split('|');
                            while (i < saids.Length)
                            {
                                if (saids[i] != "")
                                {
                                    dt.Rows[i][1] = DateTime.Parse(saids[i]);
                                }
                                i++;
                            }
                        }
                    }
                    dgvLog_acesso.DataSource = dt;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    if (msg.ToUpper().Contains("TIMEOUT EXPIRED"))
                        msg = "Tempo de espera esgotado...";
                    MessageBox.Show(msg);
                }
            }
        }

        public void preencher_cmb_users()
        {
            logs.Command.CommandText =
                @"SELECT * FROM USUARIO";
            try
            {
                List<string> list_users = new List<string>();
                list_users.Add("");
                if (logs.Connection.State != ConnectionState.Closed)
                {
                    logs.Connection.Close();
                }
                logs.Connection.Open();

                SqlDataReader reader = logs.Command.ExecuteReader();

                while (reader.Read())
                {
                    list_users.Add(reader["NOME"].ToString());
                }
                reader.Close();
                cmbUsuarios.DataSource = list_users;

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.ToUpper().Contains("TIMEOUT EXPIRED"))
                    msg = "Tempo de espera esgotado...";

                MessageBox.Show(msg);
            }
            logs.Connection.Close();
        }

        public void troca_nome_id_user()
        {
            logs.Command.CommandText =
                   @"SELECT * FROM USUARIO WHERE NOME = @nome_select";

            logs.Command.Parameters.AddWithValue("@nome_select", cmbUsuarios.Text);
            try
            {
                if (logs.Connection.State != ConnectionState.Closed)
                {
                    logs.Connection.Close();
                }
                logs.Connection.Open();

                SqlDataReader reader = logs.Command.ExecuteReader();

                while (reader.Read())
                {
                    id_user_log_select = reader["ID_USUARIO"].ToString();
                    txtLogin_log.Text = reader["USUARIO"].ToString();
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
                logs.Command.Parameters.Clear();
                logs.Connection.Close();
            }



        }

        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            troca_nome_id_user();
            preencherdgv_log_acesso();
        }

        #endregion

        //------------------------------------------------------------------------------------------

        #region Tab Disciplinas

        public void preencher_tab_disciplinas()
        {
            preencher_ltvDisciplinas();
        }
        public void preencher_ltvDisciplinas()
        {
            List<Notas.csDisciplinas> list_ = cs_disciplinas.list_disciplinas();

            ltvDisciplinas.Items.Clear();
            for (int i = 0; i < list_.Count; i++)
            {
                ltvDisciplinas.Items.Add(list_[i].n_disciplina);
                ltvDisciplinas.Items[i].SubItems.Add(list_[i].n_hist_disciplina);
            }
        }

        private void btNovaDisciplina_Click(object sender, EventArgs e)
        {
            txtNovaDisciplina.Text = "";
            txtNovoHistDisciplina.Text = "";
            btAddNovaDisciplina.Text = "Adicionar";
            btAddNovaDisciplina.Enabled = false;
            btNovaDisciplina.Enabled = true;
        }

        private void btAddNovaDisciplina_Click(object sender, EventArgs e)
        {
            if (btAddNovaDisciplina.Text == "Adicionar")
            {
                #region Cod-adicionar
                if (txtNovaDisciplina.Text != "")
                {
                    //Verifica se tem nome igual na listview
                    bool add = true;
                    foreach (ListViewItem item in ltvDisciplinas.Items)
                    {
                        if (item.Text == txtNovaDisciplina.Text)
                        {
                            add = false;
                        }
                    }
                    //Grava se não houver
                    if (add == true)
                    {
                        //INSERT NOVA DISCIPLINA
                        cs_disciplinas.add_disciplina(txtNovaDisciplina.Text);
                        //VERIFICA SE HIST_DISCIPLINA ESTA PREENCHIDO - CASO NÃO, Ñ DA UPDATE - Ñ APARECE NO HISTORICO
                        if (txtNovoHistDisciplina.Text != "")
                        {
                            //UPDATE NOME_HIST DISCIPLINA
                            cs_disciplinas.upd_nome_hist_disciplina(cs_disciplinas.troca_disciplina_nome_por_id(txtNovaDisciplina.Text), txtNovoHistDisciplina.Text);
                        }
                        //Limpar campos e volta botão para adicionar
                        btAddNovaDisciplina.Text = "Adicionar";
                        txtNovaDisciplina.Text = "";
                    }
                    else
                    {
                        txtNovaDisciplina.BackColor = Color.IndianRed;
                        MessageBox.Show("Disciplina já existe. Favor usar um nome diferente.");
                    }
                }
                else
                {
                    txtNovaDisciplina.BackColor = Color.IndianRed;
                    MessageBox.Show("Favor informar a disciplina.");
                }
                #endregion Cod-adicionar
            }
            else
            {
                #region Cod-modificar
                MessageBox.Show("MODIFICAR");

                //if (cmbDisciplina.Text != "")
                //{
                //    if (txtTipo_Atendimento.Text != "")
                //    {
                //
                //
                //        if (ckbTem_Nota.Checked)
                //        {
                //            cs_tipodisciplina.modificaTipo_Atendimento(cs_tipodisciplina.troca_nome_tipoatend_id(item_selecionado, cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina.Text)), txtTipo_Atendimento.Text, (float)nudNota.Value, cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina.Text));
                //
                //            update_ltb();
                //            //Limpar campos e volta botão para adicionar
                //            btAdd_Tipodisciplina.Text = "Adicionar";
                //            txtTipo_Atendimento.Text = "";
                //            nudNota.Value = 0;
                //            ckbTem_Nota.Checked = false;
                //        }
                //        else
                //        {
                //            cs_tipodisciplina.modificaTipo_Atendimento_semnota(cs_tipodisciplina.troca_nome_tipoatend_id(item_selecionado, cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina.Text)), txtTipo_Atendimento.Text, cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina.Text));
                //
                //            update_ltb();
                //            //Limpar campos e volta botão para adicionar
                //            btAdd_Tipodisciplina.Text = "Adicionar";
                //            txtTipo_Atendimento.Text = "";
                //            nudNota.Value = 0;
                //            ckbTem_Nota.Checked = false;
                //        }
                //
                //
                //    }
                //    else
                //    {
                //        txtTipo_Atendimento.BackColor = Color.IndianRed;
                //        MessageBox.Show("Favor informar o novo tipo de atendimento.");
                //    }
                //}
                //else
                //{
                //    cmbDisciplina.BackColor = Color.IndianRed;
                //    MessageBox.Show("Favor selecionar a disciplina.");
                //}
                #endregion Cod-adicionar
            }

            preencher_ltvDisciplinas();
        }

        public void cadastrar_disciplina()
        {
            disciplinas.Command.CommandText =
                @"INSERT INTO DISCIPLINAS (N_DISCIPLINA, N_HIST_DISCIPLINA )
                VALUES (@n_disciplina, @n_hist_disciplina )";


            disciplinas.Command.Parameters.AddWithValue("@n_disciplina", txtNovaDisciplina.Text);
            disciplinas.Command.Parameters.AddWithValue("@n_hist_disciplina", txtNovoHistDisciplina.Text);

            try
            {
                if (disciplinas.Connection.State != ConnectionState.Closed)
                {
                    disciplinas.Connection.Close();
                }
                disciplinas.Connection.Open();
                disciplinas.Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                disciplinas.Command.Parameters.Clear();
                disciplinas.Connection.Close();
            }
        }

        private void dgvDisciplinas_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //dgvDisciplinas.Columns[0].ReadOnly = true;
            //
            //if (cadastro != 1)
            //{
            //    atual = dgvDisciplinas.CurrentCell.Value.ToString();
            //}
        }

        private void dgvDisciplinas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvDisciplinas.CurrentCell.Value.ToString() != "")
            //{
            //    atualizar_disciplina();
            //}
            //else
            //{
            //    dgvDisciplinas.CurrentCell.Value = atual;
            //}

        }

        public void atualizar_disciplina()
        {
            //disciplinas.Command.CommandText =
            //    @"UPDATE DISCIPLINAS SET
            //    N_DISCIPLINA=@n_disciplina,
            //    N_HIST_DISCIPLINA=@n_hist_disciplina     
            //    WHERE ID_DISCIPLINA=@iddiciplina_selecionada";
            //
            //disciplinas.Command.Parameters.AddWithValue("@iddiciplina_selecionada", dgvDisciplinas.Rows[dgvDisciplinas.CurrentCell.RowIndex].Cells[0].Value);
            //disciplinas.Command.Parameters.AddWithValue("@n_disciplina", dgvDisciplinas.Rows[dgvDisciplinas.CurrentCell.RowIndex].Cells[1].Value);
            //disciplinas.Command.Parameters.AddWithValue("@n_hist_disciplina", dgvDisciplinas.Rows[dgvDisciplinas.CurrentCell.RowIndex].Cells[2].Value);
            //
            //bool connected = disciplinas.Connection.State == ConnectionState.Open;
            //
            //try
            //{
            //    if (!connected) disciplinas.Connection.Open();
            //    disciplinas.Command.ExecuteNonQuery();
            //    disciplinas.Command.Parameters.Clear();
            //    int z = dgvDisciplinas.CurrentCell.RowIndex;
            //    btAddNovaDisciplina.Enabled = false;               
            //
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //
            //    disciplinas.Command.Parameters.Clear();
            //    disciplinas.Connection.Close();
            //}
        }

        private void ltvDisciplinas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem item in ltvDisciplinas.SelectedItems)
            {
                txtNovaDisciplina.Text = item.Text;

                txtNovoHistDisciplina.Text = item.SubItems[1].Text;

                btAddNovaDisciplina.Text = "Atualizar";
                btNovaDisciplina.Enabled = true;


            }
        }

        private void txtNovadisciplina_TextChanged(object sender, EventArgs e)
        {
            btAddNovaDisciplina.Enabled = true;

            txtNovaDisciplina.BackColor = Color.White;

        }

        private void btnUpOrdemHist_Click(object sender, EventArgs e)
        {
            //Subir
            if (ltvDisciplinas.SelectedItems.Count > 0)
            {
                cs_disciplinas.alterar_ordem_disciplina(cs_disciplinas.troca_disciplina_nome_por_id(ltvDisciplinas.SelectedItems[0].Text));
                preencher_ltvDisciplinas();
            }
        }

        private void btnDownOrdemHist_Click(object sender, EventArgs e)
        {
            //Descer
            if (ltvDisciplinas.SelectedItems.Count > 0)
            {
                cs_disciplinas.alterar_ordem_disciplina(cs_disciplinas.troca_disciplina_nome_por_id(ltvDisciplinas.SelectedItems[0].Text), 0);
                preencher_ltvDisciplinas();

            }
        }

        private void ltvDisciplinas_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (ltvDisciplinas.SelectedItems.Count > 0)
            {
                int i = ltvDisciplinas.Items.IndexOf(ltvDisciplinas.SelectedItems[0]);

                if (i > 0) btnUpOrdemHist.Enabled = true;
                else btnUpOrdemHist.Enabled = false;

                if (i < ltvDisciplinas.Items.Count - 1) btnDownOrdemHist.Enabled = true;
                else btnDownOrdemHist.Enabled = false;
            }
            else
            {
                btnUpOrdemHist.Enabled = false;
                btnDownOrdemHist.Enabled = false;
            }

        }

        #endregion

        //------------------------------------------------------------------------------------------

        #region Tab Professores e Monitores

        public void preencher_tab_professores_mentores()
        {
            preencher_clbAreas();
        }
        public void preencherltvProfessore_mentores()
        {
            ltvProfessore_mentores.Items.Clear();

            List<csUsuarios> list_ = cs_usuarios.lista_usuarios(0, "NOME");

            for (int i = 0; i < list_.Count; i++)
            {
                if (list_[i].id_disciplina_user > 0)
                {
                    ltvProfessore_mentores.Items.Add(cs_usuarios.troca_id_por_nome(list_[i].id_usuario));
                    ltvProfessore_mentores.Items[ltvProfessore_mentores.Items.Count - 1].SubItems.Add(cs_usuarios.troca_n_grupo_id_grupo(list_[i].id_grupo_usuario));
                    ltvProfessore_mentores.Items[ltvProfessore_mentores.Items.Count - 1].SubItems.Add(cs_disciplinas.troca_disciplina_id_por_nome(list_[i].id_disciplina_user));
                }
            }
        }
        public void preencher_clbAreas()
        {
            List<Notas.csDisciplinas> list_ = cs_disciplinas.list_areas();
            clbAreas.Items.Clear();
            for (int i = 0; i < list_.Count; i++)
            {
                clbAreas.Items.Add(list_[i].area_);
            }
        }

        private void clbAreas_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            clbAreas.ItemCheck -= clbAreas_ItemCheck;

            List<string> checkedItems = new List<string>();

            foreach (var item in clbAreas.CheckedItems)
                checkedItems.Add(item.ToString());

            if (e.NewValue == CheckState.Checked)
                checkedItems.Add(clbAreas.Items[e.Index].ToString());
            else
                checkedItems.Remove(clbAreas.Items[e.Index].ToString());

            //CRIAR LISTA PARA ADD AREAS
            List<int> list_ids_areas_ = new List<int>();

            foreach (string item in checkedItems)
            {
                list_ids_areas_.Add(cs_disciplinas.troca_area_nome_por_id(item));
            }

            //CHAMA METODO PARA UPDATE
            cs_usuarios.upd_ids_areas_user(cs_usuarios.troca_nome_por_id(ltvProfessore_mentores.SelectedItems[0].Text), list_ids_areas_);

            clbAreas.ItemCheck += clbAreas_ItemCheck;

            Cursor.Current = Cursors.Default;
        }

        public void fill_tab_professores()
        {
            //limpeza
            cmbUsuarios_tabprof.Items.Clear();
            cmbDisciplina_tabprof.Items.Clear();

            //popula cmbUsuarios_tabprof
            List<int> list = cs_usuarios.list_id_usuarios();
            cmbUsuarios_tabprof.Items.Add("USUÁRIO | GRUPO");
            for (int i = 0; i < list.Count; i++)
            {
                cmbUsuarios_tabprof.Items.Add(cs_usuarios.troca_id_por_nome(Int32.Parse(list[i].ToString())) + " | " + cs_usuarios.troca_n_grupo_id_grupo(cs_usuarios.id_grupo(Int32.Parse(list[i].ToString()))));
            }

            cmbUsuarios_tabprof.Text = "USUÁRIO | GRUPO";

            //popula cmbDisciplina_tabprof
            DataTable dt = cs_disciplinas.buscarDisciplinas();
            cmbDisciplina_tabprof.Items.Add("DISCIPLINA");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbDisciplina_tabprof.Items.Add(cs_disciplinas.tab_disciplinas.Rows[i][1].ToString());
            }
            cmbDisciplina_tabprof.Text = "DISCIPLINA";

            //Limpeza
            dt_prof_ment.Rows.Clear();
            dt_prof_ment.Columns.Clear();

            dt_prof_ment.Columns.Add("id_usuario");
            dt_prof_ment.Columns.Add("nome_usuario");
            dt_prof_ment.Columns.Add("id_grupo");
            dt_prof_ment.Columns.Add("id_disciplina");

            //preencher listbox usuarios com id_disciplina
            preencherltvProfessore_mentores();
            preencherltvDisciplinas_prof_mont();


            //preencher_cbx_materias();
            //selecionar_materias();
            ////dgvProfessores.CellEnter -= dgvDisciplinas_CellEnter;            
            ////bsprofessores.DataSource = professores.Table;
            ////dgvProfessores.DataSource = bsprofessores;
            ////busca_materias();

            //btAdd_disciplina.Enabled = false;
            //dgvDisciplinas.CellEnter += dgvDisciplinas_CellEnter;
        }



        public void preencherltvDisciplinas_prof_mont()
        {
            //for(int i = 0; i < dt_prof_ment.Rows.Count; i++)
            //{
            //    ltvProfessore_mentores.Items[i].SubItems.Add(cs_usuarios.troca_n_grupo_id_grupo(Convert.ToInt32(dt_prof_ment.Rows[i][2])));
            //    ltvProfessore_mentores.Items[i].SubItems.Add(cs_disciplinas.troca_disciplina_id_por_nome(Convert.ToInt32(dt_prof_ment.Rows[i][3])));                
            //}

        }

        private void ltvProfessore_mentores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ltvProfessore_mentores.SelectedItems.Count > 0)
            {
                marcar_clbAreaPorUser(Convert.ToInt32(cs_usuarios.troca_nome_por_id(ltvProfessore_mentores.SelectedItems[0].Text)));
            }
        }

        public void marcar_clbAreaPorUser(int id_usuario)
        {
            //Desabilitar evento ItemCheck
            clbAreas.ItemCheck -= clbAreas_ItemCheck;

            //Desmarcar tudo
            for (int i = 0; i < clbAreas.Items.Count; i++)
            {
                clbAreas.SetItemChecked(i, false);
            }

            //Marcar as areas correspondentes ao id_usuario
            List<int> list_ = cs_usuarios.ids_areas_user(id_usuario);
            for (int i = 0; i < list_.Count; i++)
            {
                clbAreas.SetItemChecked(clbAreas.Items.IndexOf(cs_disciplinas.troca_area_id_por_nome(list_[i])), true);
            }

            //Habilitar evento ItemCheck
            clbAreas.ItemCheck += clbAreas_ItemCheck;
        }

        private void btAddNovaRelacao_Click(object sender, EventArgs e)
        {
            string nome = cmbUsuarios_tabprof.Text;

            nome = nome.Remove(nome.IndexOf("|"));

            cs_usuarios.upd_id_disciplina_usuario(cs_usuarios.troca_nome_por_id(nome), cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplina_tabprof.Text));

            fill_tab_professores();

        }

        private void cmbUsuarios_tabprof_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios_tabprof.Text != "USUÁRIO | GRUPO")
            {
                if (cmbDisciplina_tabprof.Text != "DISCIPLINA")
                {
                    btAddNovaRelacao.Enabled = true;
                }
                else
                {
                    btAddNovaRelacao.Enabled = false;
                }
            }
            else
            {
                btAddNovaRelacao.Enabled = false;
            }
        }

        private void cmbDisciplina_tabprof_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios_tabprof.Text != "USUÁRIO | GRUPO")
            {
                if (cmbDisciplina_tabprof.Text != "DISCIPLINA")
                {
                    btAddNovaRelacao.Enabled = true;
                }
                else
                {
                    btAddNovaRelacao.Enabled = false;
                }
            }
            else
            {
                btAddNovaRelacao.Enabled = false;
            }
        }

        private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UPDATE - 

            if (ltvProfessore_mentores.SelectedItems.Count > 0)
            {
                int id_user_select = Convert.ToInt32(cs_usuarios.troca_nome_por_id(ltvProfessore_mentores.SelectedItems[0].Text));

                cs_usuarios.limpa_id_disciplina_usuario(id_user_select);
            }

            fill_tab_professores();

        }


        #endregion

        //------------------------------------------------------------------------------------------               


        #region Tab Ensinos

        public void preencher_tab_ensinos()
        {
            //Preencher ltvEnsinos.
            preencher_ltvEnsinos();

            //Preencher ckbList.
            preencher_clbDisciplinasPorEnsino();

            //Marcar disciplinas 
            if (ltvEnsinos.SelectedItems.Count > 0)
            {
                marcar_clbDisciplinasPorEnsino(cs_disciplinas.troca_ensino_nome_por_id(ltvEnsinos.SelectedItems[0].ToString()));
            }



        }
        public void preencher_ltvEnsinos()
        {
            //Preencher ltvEnsinos.
            ltvEnsinos.Items.Clear();
            List<int> list_ = cs_disciplinas.lista_ids_ensinos();
            for (int i = 0; i < list_.Count; i++)
            {
                ltvEnsinos.Items.Add(cs_disciplinas.troca_ensino_id_por_nome(list_[i]));
            }
        }
        public void preencher_clbDisciplinasPorEnsino()
        {
            //Trazer todas as disciplinas cadastradas.
            clbDisciplinasPorEnsino.Items.Clear();
            List<Notas.csDisciplinas> list_ = cs_disciplinas.list_disciplinas();
            for (int i = 0; i < list_.Count; i++)
            {
                clbDisciplinasPorEnsino.Items.Add(list_[i].n_disciplina);
            }
        }
        public void marcar_clbDisciplinasPorEnsino(int id_ensino)
        {
            clbDisciplinasPorEnsino.ItemCheck -= clbDisciplinasPorEnsino_ItemCheck;

            for (int i = 0; i < clbDisciplinasPorEnsino.Items.Count; i++)
            {
                clbDisciplinasPorEnsino.SetItemChecked(i, false);
            }
            //Marcar as disciplinas correspondentes ao id_ensino
            List<Notas.csDisciplinas> list_ = cs_disciplinas.list_ensinos();
            for (int i = 0; i < list_.Count; i++)
            {
                if (list_[i].id_ensino == id_ensino)
                {
                    for (int z = 0; z < list_[i].ids_disciplinas_ensino.Count; z++)
                    {
                        string a = cs_disciplinas.troca_disciplina_id_por_nome(list_[i].ids_disciplinas_ensino[z]);
                        clbDisciplinasPorEnsino.SetItemChecked(clbDisciplinasPorEnsino.Items.IndexOf(a), true);
                    }
                }
            }

            clbDisciplinasPorEnsino.ItemCheck += clbDisciplinasPorEnsino_ItemCheck;
        }
        //LTV ITEM SELECIONADO
        private void ltvEnsinos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Marcar disciplinas 
            if (ltvEnsinos.SelectedItems.Count > 0)
            {
                marcar_clbDisciplinasPorEnsino(cs_disciplinas.troca_ensino_nome_por_id(ltvEnsinos.SelectedItems[0].Text));
            }
        }
        //ITEM CLB ALTERADO
        private void clbDisciplinasPorEnsino_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            clbDisciplinasPorEnsino.ItemCheck -= clbDisciplinasPorEnsino_ItemCheck;

            List<string> checkedItems = new List<string>();

            foreach (var item in clbDisciplinasPorEnsino.CheckedItems)
                checkedItems.Add(item.ToString());

            if (e.NewValue == CheckState.Checked)
                checkedItems.Add(clbDisciplinasPorEnsino.Items[e.Index].ToString());
            else
                checkedItems.Remove(clbDisciplinasPorEnsino.Items[e.Index].ToString());

            //CRIAR LISTA PARA ADD IDS_DISCIPLINAS
            List<int> list_ids_disc_ = new List<int>();

            foreach (string item in checkedItems)
            {
                list_ids_disc_.Add(cs_disciplinas.troca_disciplina_nome_por_id(item));
            }

            //CHAMA METODO PARA UPDATE
            cs_disciplinas.upd_ids_disciplinas_ensino(cs_disciplinas.troca_ensino_nome_por_id(ltvEnsinos.SelectedItems[0].Text), list_ids_disc_);

            clbDisciplinasPorEnsino.ItemCheck += clbDisciplinasPorEnsino_ItemCheck;

            Cursor.Current = Cursors.Default;
        }
        //TXT ALTERADO
        private void txtNovoEnsino_TextChanged(object sender, EventArgs e)
        {
            if (txtNovoEnsino.Text != string.Empty)
                btnAddNovoEnsino.Enabled = true;
            else btnAddNovoEnsino.Enabled = false;
        }
        //ADD NOVO ENSINO
        private void btnAddNovoEnsino_Click(object sender, EventArgs e)
        {
            List<string> list_ensinos = new List<string>();
            foreach (ListViewItem item in ltvEnsinos.Items)
            {
                list_ensinos.Add(item.Text);
            }
            if (!list_ensinos.Contains(txtNovoEnsino.Text))
            {
                //Add no Banco
                cs_disciplinas.add_ensino(txtNovoEnsino.Text);
                //Add no listview
                ltvEnsinos.Items.Add(txtNovoEnsino.Text);
                //limpa textBox
                txtNovoEnsino.Text = string.Empty;
            }
            else
            {
                //Ensino já existe
            }
        }


        #endregion

        //------------------------------------------------------------------------------------------

        #region Tab Areas

        public void preencher_tab_areas()
        {
            //Preencher ltvAreas.
            preencher_ltvAreas();

            //Preencher ckbList.
            preencher_clbDisciplinasPorArea();

            //Marcar disciplinas 
            if (ltvAreas.SelectedItems.Count > 0)
            {
                marcar_clbDisciplinasPorArea(cs_disciplinas.troca_area_nome_por_id(ltvAreas.SelectedItems[0].ToString()));
            }
        }
        public void preencher_ltvAreas()
        {
            //Preencher ltvAreas.
            ltvAreas.Items.Clear();
            List<Notas.csDisciplinas> list_ = cs_disciplinas.list_areas();
            for (int i = 0; i < list_.Count; i++)
            {
                ltvAreas.Items.Add(cs_disciplinas.troca_area_id_por_nome(list_[i].id_area));
            }
        }
        public void preencher_clbDisciplinasPorArea()
        {
            //Trazer todas as disciplinas cadastradas.
            clbDisciplinasPorArea.Items.Clear();
            List<Notas.csDisciplinas> list_ = cs_disciplinas.list_disciplinas();
            for (int i = 0; i < list_.Count; i++)
            {
                clbDisciplinasPorArea.Items.Add(list_[i].n_disciplina);
            }
        }
        public void marcar_clbDisciplinasPorArea(int id_area)
        {
            //Desabilitar evento ItemCheck
            clbDisciplinasPorArea.ItemCheck -= clbDisciplinasPorArea_ItemCheck;

            for (int i = 0; i < clbDisciplinasPorArea.Items.Count; i++)
            {
                clbDisciplinasPorArea.SetItemChecked(i, false);
            }
            //Marcar as disciplinas correspondentes ao id_area
            List<Notas.csDisciplinas> list_ = cs_disciplinas.list_areas();
            for (int i = 0; i < list_.Count; i++)
            {
                if (list_[i].id_area == id_area)
                {
                    if (list_[i].ids_disciplinas_area != null)
                    {
                        for (int z = 0; z < list_[i].ids_disciplinas_area.Count; z++)
                        {
                            string a = cs_disciplinas.troca_disciplina_id_por_nome(list_[i].ids_disciplinas_area[z]);
                            clbDisciplinasPorArea.SetItemChecked(clbDisciplinasPorArea.Items.IndexOf(a), true);
                        }
                    }

                }
            }

            //Habilitar evento ItemCheck
            clbDisciplinasPorArea.ItemCheck += clbDisciplinasPorArea_ItemCheck;
        }
        //CLB ITEM SELECIONADO
        private void ltvAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ltvAreas.SelectedItems.Count > 0)
            {
                clbDisciplinasPorArea.Enabled = true;
                marcar_clbDisciplinasPorArea(cs_disciplinas.troca_area_nome_por_id(ltvAreas.SelectedItems[0].Text));

            }
            else
            {
                clbDisciplinasPorArea.ItemCheck -= clbDisciplinasPorArea_ItemCheck;

                for (int i = 0; i < clbDisciplinasPorArea.Items.Count; i++)
                {
                    clbDisciplinasPorArea.SetItemChecked(i, false);
                }

                clbDisciplinasPorArea.Enabled = false;

                clbDisciplinasPorArea.ItemCheck += clbDisciplinasPorArea_ItemCheck;
            }

        }
        //ITEM CLB ALTERADO
        private void clbDisciplinasPorArea_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            clbDisciplinasPorArea.ItemCheck -= clbDisciplinasPorArea_ItemCheck;

            List<string> checkedItems = new List<string>();

            foreach (var item in clbDisciplinasPorArea.CheckedItems)
                checkedItems.Add(item.ToString());

            if (e.NewValue == CheckState.Checked)
                checkedItems.Add(clbDisciplinasPorArea.Items[e.Index].ToString());
            else
                checkedItems.Remove(clbDisciplinasPorArea.Items[e.Index].ToString());

            //CRIAR LISTA PARA ADD IDS_DISCIPLINAS
            List<int> list_ids_disc_ = new List<int>();

            foreach (string item in checkedItems)
            {
                list_ids_disc_.Add(cs_disciplinas.troca_disciplina_nome_por_id(item));
            }

            //CHAMA METODO PARA UPDATE
            cs_disciplinas.upd_ids_disciplinas_area(cs_disciplinas.troca_area_nome_por_id(ltvAreas.SelectedItems[0].Text), list_ids_disc_);

            clbDisciplinasPorArea.ItemCheck += clbDisciplinasPorArea_ItemCheck;

            Cursor.Current = Cursors.Default;
        }
        //TXT ALTERADO
        private void txtNovaArea_TextChanged(object sender, EventArgs e)
        {
            if (txtNovaArea.Text != string.Empty)
                btnNovaArea.Enabled = true;
            else btnNovaArea.Enabled = false;
        }
        //ADD NOVA AREA
        private void btnNovaArea_Click(object sender, EventArgs e)
        {
            List<string> list_areas = new List<string>();
            foreach (ListViewItem item in ltvAreas.Items)
            {
                list_areas.Add(item.Text);
            }
            if (!list_areas.Contains(txtNovaArea.Text))
            {
                //Add no Banco
                cs_disciplinas.add_area(txtNovaArea.Text);
                //Add no listview
                ltvAreas.Items.Add(txtNovaArea.Text);
                //limpa textBox
                txtNovaArea.Text = string.Empty;
            }
            else
            {
                //Ensino já existe
            }

        }

        #endregion

        //------------------------------------------------------------------------------------------

        #region Tab Horarios

        //Capacidades

        public void preencher_tab_horarios()
        {
            ltvDistribucao.ItemSelectionChanged -= ltvDistribucao_ItemSelectionChanged;
            ///Passos
            //Definir horario inicial(quando abre o form)
            cmbPeriodos.Text = "Manhã";
            //preecnher ltv
            preencher_ltvDistribucao();
            //
            ltvDistribucao.ItemSelectionChanged += ltvDistribucao_ItemSelectionChanged;

        }

        public void preencher_ltvDistribucao()
        {
            carga_disp = 100;

            List<Notas.csDisciplinas> list_ = cs_disciplinas.list_disciplinas();

            ltvDistribucao.Items.Clear();

            int qtdAtivosDisciplinas = 0;
            int tota_capacidade_utilizada = 0;
            int entradas_dia_select = 0;

            for (int i = 0; i < list_.Count; i++)
            {
                //Disciplinas
                ltvDistribucao.Items.Add(list_[i].n_disciplina);
                //Quantidade
                int total_disciplina = cs_disciplinas.qtd_alunos_disciplina(list_[i].id_disciplina);
                ltvDistribucao.Items[i].SubItems.Add(total_disciplina.ToString());
                qtdAtivosDisciplinas = qtdAtivosDisciplinas + total_disciplina;
                //Diário

                
                if(dtpDataDistribuicao.Checked)
                {                   
                    int total_alunos_diario = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL",
                        dtpDataDistribuicao.Value.ToString("dd/MM/yyyy"),
                        false,
                        list_[i].id_disciplina);

                    entradas_dia_select = entradas_dia_select + total_alunos_diario;
                       
                    ltvDistribucao.Items[i].SubItems.Add(total_alunos_diario.ToString());
                }
                else
                {                    
                    int total_alunos_diario = cs_disciplinas.qtd_alunos_disciplina(list_[i].id_disciplina, DateTime.Now.ToString("dd/MM/yyyy"));
                    entradas_dia_select = entradas_dia_select + total_alunos_diario;
                    ltvDistribucao.Items[i].SubItems.Add(total_alunos_diario.ToString());
                }
                
                //Capacidade
                int capacidadeDisciplina = list_[i].capacidade;
                ltvDistribucao.Items[i].SubItems.Add(capacidadeDisciplina.ToString()+"%");
                tota_capacidade_utilizada = tota_capacidade_utilizada + capacidadeDisciplina;
            }

            lblQtdAtivosDisciplinas.Text = "Total Alunos em Disciplinas: " + qtdAtivosDisciplinas;

            carga_disp = carga_disp - tota_capacidade_utilizada;

            lblCarga_disponivel.Text = "Disponível: " + carga_disp +"%";
            lblQtdAlunosEntrada.Text = "Total Alunos Entrada:" + entradas_dia_select;

        }

        private void btnAlmentarCapacidade_Click(object sender, EventArgs e)
        {
            if (ltvDistribucao.SelectedItems.Count > 0 && carga_disp > 0)
            {
                //Atualiza capacidade da disciplina selecionada
                cs_disciplinas.upd_cap_disciplina(cs_disciplinas.troca_disciplina_nome_por_id(ltvDistribucao.SelectedItems[0].Text),
                                                  Convert.ToInt32(ltvDistribucao.SelectedItems[0].SubItems[3].Text.Replace("%", "")) + 1);

                ltvDistribucao.SelectedItems[0].SubItems[3].Text = ((Convert.ToInt32(ltvDistribucao.SelectedItems[0].SubItems[3].Text.Replace("%", "")) + 1) + "%").ToString();

                carga_disp = carga_disp - 1;
                lblCarga_disponivel.Text = "Disponível: " + carga_disp + "%";
            }
        }

        private void btnDiminuirCapacidade_Click(object sender, EventArgs e)
        {
            if (ltvDistribucao.SelectedItems.Count > 0 && Convert.ToInt32(ltvDistribucao.SelectedItems[0].SubItems[3].Text.Replace("%", "")) > 0)
            {
                //Atualiza capacidade da disciplina selecionada
                cs_disciplinas.upd_cap_disciplina(cs_disciplinas.troca_disciplina_nome_por_id(ltvDistribucao.SelectedItems[0].Text),
                                                  Convert.ToInt32(ltvDistribucao.SelectedItems[0].SubItems[3].Text.Replace("%", "")) - 1);

                ltvDistribucao.SelectedItems[0].SubItems[3].Text = ((Convert.ToInt32(ltvDistribucao.SelectedItems[0].SubItems[3].Text.Replace("%", "")) - 1) + "%").ToString();

                carga_disp = carga_disp + 1;
                lblCarga_disponivel.Text = "Disponível: " + carga_disp + "%";
            }
        }

        private void ltvDistribucao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "+")
            {
                btnAlmentarCapacidade.PerformClick();
            }
            else if (e.KeyChar.ToString() == "-")
            {
                btnDiminuirCapacidade.PerformClick();
            }
        }

        private void btnLimparCapacidade_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ltvDistribucao.Items.Count; i++)
            {
                //Atualiza capacidade de todas as disciplinas
                cs_disciplinas.upd_cap_disciplina(cs_disciplinas.troca_disciplina_nome_por_id(ltvDistribucao.Items[i].Text), 0);

                ltvDistribucao.Items[i].SubItems[3].Text = "0";

                carga_disp = 100;
                lblCarga_disponivel.Text = "Disponível: " + carga_disp + "%";
            }
        }

        //Horários

        private void ltvDistribucao_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (ltvDistribucao.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in ltvDistribucao.Items)
                {
                    item.BackColor = Color.White;
                }

                foreach(ListViewItem item in ltvDistribucao.SelectedItems)
                {
                    item.BackColor = Color.LightGray;
                }
                
                id_disciplina_selecionada_distribuicao = cs_disciplinas.troca_disciplina_nome_por_id(ltvDistribucao.SelectedItems[0].Text);

                preencher_dtps();

                foreach(DateTimePicker dtp in grbDistribuicao.Controls.OfType<DateTimePicker>())
                {
                    dtp.Enabled = true;
                }

            }
            else
            {
                foreach (DateTimePicker dtp in grbDistribuicao.Controls.OfType<DateTimePicker>())
                {
                    dtp.Enabled = false;
                }
            }
        }       

        public void preencher_dtps()
        {
            hor_aula_m = "m";
            hor_aula_t = "t";
            hor_aula_n = "n";

            List<Notas.csDisciplinas> list_ = cs_disciplinas.list_disciplinas(id_disciplina_selecionada_distribuicao);
          
            if (list_[0].horarios_aulas != string.Empty)
            {
                string[] todos_horarios = list_[0].horarios_aulas.Split('|');
                if (todos_horarios.Length > 1)
                {
                        hor_aula_m = todos_horarios[0];
                        hor_aula_t = todos_horarios[1];
                        hor_aula_n = todos_horarios[2];
                }
            }
            else
            {
                dtpSeg_ini.Value = DateTime.Parse("00:00");
                dtpSeg_fim.Value = DateTime.Parse("00:00");

                dtpTer_ini.Value = DateTime.Parse("00:00");
                dtpTer_fim.Value = DateTime.Parse("00:00");

                dtpQua_ini.Value = DateTime.Parse("00:00");
                dtpQua_fim.Value = DateTime.Parse("00:00");

                dtpQui_ini.Value = DateTime.Parse("00:00");
                dtpQui_fim.Value = DateTime.Parse("00:00");

                dtpSex_ini.Value = DateTime.Parse("00:00");
                dtpSex_fim.Value = DateTime.Parse("00:00");
            }
                
            


            string a;

            if (cmbPeriodos.Text == "Manhã")
            {
                a = hor_aula_m.Replace("|", string.Empty);
            }
            else if (cmbPeriodos.Text == "Tarde")
            {
                a = hor_aula_t.Replace("|", string.Empty);
            }
            else
            {
                a = hor_aula_n.Replace("|", string.Empty);
            }

            if (a.Length > 1)
            {

                dtpSeg_ini.Value = DateTime.Parse(a.Substring(a.IndexOf("seg") + 3, 5));
                dtpSeg_fim.Value = DateTime.Parse(a.Substring(a.IndexOf("seg") + 11, 5));

                dtpTer_ini.Value = DateTime.Parse(a.Substring(a.IndexOf("ter") + 3, 5));
                dtpTer_fim.Value = DateTime.Parse(a.Substring(a.IndexOf("ter") + 11, 5));

                dtpQua_ini.Value = DateTime.Parse(a.Substring(a.IndexOf("qua") + 3, 5));
                dtpQua_fim.Value = DateTime.Parse(a.Substring(a.IndexOf("qua") + 11, 5));

                dtpQui_ini.Value = DateTime.Parse(a.Substring(a.IndexOf("qui") + 3, 5));
                dtpQui_fim.Value = DateTime.Parse(a.Substring(a.IndexOf("qui") + 11, 5));

                dtpSex_ini.Value = DateTime.Parse(a.Substring(a.IndexOf("sex") + 3, 5));
                dtpSex_fim.Value = DateTime.Parse(a.Substring(a.IndexOf("sex") + 11, 5));
            }
            else
            {
                dtpSeg_ini.Value = DateTime.Parse("00:00");
                dtpSeg_fim.Value = DateTime.Parse("00:00");

                dtpTer_ini.Value = DateTime.Parse("00:00");
                dtpTer_fim.Value = DateTime.Parse("00:00");

                dtpQua_ini.Value = DateTime.Parse("00:00");
                dtpQua_fim.Value = DateTime.Parse("00:00");

                dtpQui_ini.Value = DateTime.Parse("00:00");
                dtpQui_fim.Value = DateTime.Parse("00:00");

                dtpSex_ini.Value = DateTime.Parse("00:00");
                dtpSex_fim.Value = DateTime.Parse("00:00");
            }

            btnAceitar_horarios.Enabled = false;
        }

        private void cmbPeriodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbPeriodos.Text == "Manhã")
            {
                definir_limite_horario("12:59:00", "13:00:00");

            }
            else if (cmbPeriodos.Text == "Tarde")
            {
                definir_limite_horario("17:59:00", "18:00:00");
                
            }
            else
            {
                definir_limite_horario("21:59:00", "22:00:00");
            }

            preencher_dtps();
        }

        private void btnAceitar_horarios_Click(object sender, EventArgs e)
        {
            atualizar_horarios();
        }

        public void atualizar_horarios()
        {
            if (cmbPeriodos.Text == "Manhã")
            {
                hor_aula_m = "mseg" + dtpSeg_ini.Value.ToString("HH:mm") + " - " + dtpSeg_fim.Value.ToString("HH:mm");
                hor_aula_m += "mter" + dtpTer_ini.Value.ToString("HH:mm") + " - " + dtpTer_fim.Value.ToString("HH:mm");
                hor_aula_m += "mqua" + dtpQua_ini.Value.ToString("HH:mm") + " - " + dtpQua_fim.Value.ToString("HH:mm");
                hor_aula_m += "mqui" + dtpQui_ini.Value.ToString("HH:mm") + " - " + dtpQui_fim.Value.ToString("HH:mm");
                hor_aula_m += "msex" + dtpSex_ini.Value.ToString("HH:mm") + " - " + dtpSex_fim.Value.ToString("HH:mm");
            }
            else if (cmbPeriodos.Text == "Tarde")
            {
                hor_aula_t = "tseg" + dtpSeg_ini.Value.ToString("HH:mm") + " - " + dtpSeg_fim.Value.ToString("HH:mm");
                hor_aula_t += "tter" + dtpTer_ini.Value.ToString("HH:mm") + " - " + dtpTer_fim.Value.ToString("HH:mm");
                hor_aula_t += "tqua" + dtpQua_ini.Value.ToString("HH:mm") + " - " + dtpQua_fim.Value.ToString("HH:mm");
                hor_aula_t += "tqui" + dtpQui_ini.Value.ToString("HH:mm") + " - " + dtpQui_fim.Value.ToString("HH:mm");
                hor_aula_t += "tsex" + dtpSex_ini.Value.ToString("HH:mm") + " - " + dtpSex_fim.Value.ToString("HH:mm");
            }
            else
            {
                hor_aula_n = "nseg" + dtpSeg_ini.Value.ToString("HH:mm") + " - " + dtpSeg_fim.Value.ToString("HH:mm");
                hor_aula_n += "nter" + dtpTer_ini.Value.ToString("HH:mm") + " - " + dtpTer_fim.Value.ToString("HH:mm");
                hor_aula_n += "nqua" + dtpQua_ini.Value.ToString("HH:mm") + " - " + dtpQua_fim.Value.ToString("HH:mm");
                hor_aula_n += "nqui" + dtpQui_ini.Value.ToString("HH:mm") + " - " + dtpQui_fim.Value.ToString("HH:mm");
                hor_aula_n += "nsex" + dtpSex_ini.Value.ToString("HH:mm") + " - " + dtpSex_fim.Value.ToString("HH:mm");
            }

            string hor_aula_todos = hor_aula_m + "|" + hor_aula_t + "|" + hor_aula_n;

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE DISCIPLINAS SET HORARIOS_AULAS=@horario WHERE ID_DISCIPLINA=@id_disciplina ";
            sql_comm.Parameters.AddWithValue("@horario", hor_aula_todos);
            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina_selecionada_distribuicao);

            try
            {
                sql_conn.Open();

                sql_comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
            
            btnAceitar_horarios.Enabled = false;
        }

        public void definir_limite_horario(string maxini, string maxfim)
        {
            dtpSeg_ini.MaxDate = DateTime.Parse(maxini);
            dtpTer_ini.MaxDate = DateTime.Parse(maxini);
            dtpQua_ini.MaxDate = DateTime.Parse(maxini);
            dtpQui_ini.MaxDate = DateTime.Parse(maxini);
            dtpSex_ini.MaxDate = DateTime.Parse(maxini);

            dtpSeg_fim.MaxDate = DateTime.Parse(maxfim);
            dtpTer_fim.MaxDate = DateTime.Parse(maxfim);
            dtpQua_fim.MaxDate = DateTime.Parse(maxfim);
            dtpQui_fim.MaxDate = DateTime.Parse(maxfim);
            dtpSex_fim.MaxDate = DateTime.Parse(maxfim);
        }

        private void dtpSeg_ini_ValueChanged(object sender, EventArgs e)
        {
            btnAceitar_horarios.Enabled = true;
        }

        private void dtpDataDistribuicao_ValueChanged(object sender, EventArgs e)
        {
            preencher_ltvDistribucao();
        }


        #endregion

        //------------------------------------------------------------------------------------------


        private void ltvAreas_Click(object sender, EventArgs e)
        {
            
        }

        
       

        

        
    }
}