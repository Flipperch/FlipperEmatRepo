using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Alunos
{
    public partial class frControledeacesso : Form
    {
        Usuarios_Grupos.csGrupos cs_grupos = new Usuarios_Grupos.csGrupos();

        SqlQuery consulta = new SqlQuery(Conexoes.GetSqlConnection());
        SqlQuery consulta2 = new SqlQuery(Conexoes.GetSqlConnection());
        SqlConnection conn = Conexoes.GetSqlConnection();
        DataTable tbAlunos = new DataTable();
        BindingSource bsAlunos = new BindingSource();
        BindingSource bsId_users = new BindingSource();

        DataTable tab_disciplinas = new DataTable();


        List<string> lista = new List<string>();       

        public frControledeacesso()
        {
            InitializeComponent();
        }

        private void FRcontroledeacesso_Load(object sender, EventArgs e)
        {
            filtra_users();

            tab_disciplinas.Columns.Add("id");
            tab_disciplinas.Columns.Add("nome");
            fill_tab_disciplina();

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabUsuarios")
            {
                if (cs_grupos.troca_grupo_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_grupo_logado) == "ADMINISTRADOR" ||
                    cs_grupos.troca_grupo_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_grupo_logado) == "COORDENADORIA" ||
                    cs_grupos.troca_grupo_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_grupo_logado) == "DIRETORIA")
                {

                }
                else
                {

                    tabControl1.SelectedTab = tabPage1;
                    MessageBox.Show("Acesso não Autorizado!", "Aba - Acesso de Funcionários");
                }
            }
            else
            {

            }
        }

        public void fill_tab_disciplina()
        {
            consulta.Command.CommandText =
                   @"SELECT * FROM DISCIPLINAS";

            consulta.Connection.Open();

            SqlDataReader reader = consulta.Command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                tab_disciplinas.Rows.Add();
                tab_disciplinas.Rows[i][0] = reader["ID_DISCIPLINA"].ToString();
                tab_disciplinas.Rows[i][1] = reader["N_DISCIPLINA"].ToString();
                i++;
            }

            reader.Close();
            consulta.Command.Parameters.Clear();
            consulta.Connection.Close();
        }

        #region Tab Alunos

        //Grupo_box - Registros

        private void btPesquisa_Click(object sender, EventArgs e)
        {
            txtNome.Text = string.Empty;
            txtRG.Text = string.Empty;
            label.Text = string.Empty;
            txtUltimoacesso.Text = string.Empty;

            if(txtPesquisa_numat.Text != string.Empty)
            {
                buscar_aluno();
                buscar_registros();
            }
            else
            {
                MessageBox.Show("Informeo número de matrícula procurado.", "Pesquisar");
            }
            
        }

        public void buscar_aluno()
        {
            consulta.Command.CommandText =
                   @"SELECT * FROM ALUNOS WHERE N_MAT  =  @aluno";

            consulta.Command.Parameters.AddWithValue("@aluno", txtPesquisa_numat.Text);

            consulta.Connection.Open();

            SqlDataReader reader = consulta.Command.ExecuteReader();       

            while (reader.Read())
            {
                txtNome.Text = reader["ALUNO"].ToString();
                txtRG.Text = reader["RG"].ToString();
                //OPA

                for (int i = 0; i < tab_disciplinas.Rows.Count; i++ )
                {
                    if(tab_disciplinas.Rows[i][0].ToString() == reader["ID_DISCIPLINA_ATUAL"].ToString())
                    {
                        txtMat_atual.Text = tab_disciplinas.Rows[i][1].ToString();
                        break;
                    }
                      
                }                  
            }

            reader.Close();
            consulta.Command.Parameters.Clear();
            consulta.Connection.Close();

        }

        public void buscar_registros()
        {
            consulta.Command.CommandText =
                @"SELECT * FROM CAT_ENT_SAI WHERE ID_CARTAO =  @aluno ORDER BY COD_ACESSO DESC";

            consulta.Command.Parameters.AddWithValue("@aluno", txtPesquisa_numat.Text);

            consulta.Connection.Open();

            SqlDataReader reader = consulta.Command.ExecuteReader();

          

            while (reader.Read())
            {
                if (reader["ENT_SAI"].ToString() == "1")
                {
                    ltbRegistros.Items.Add(reader["DATA"].ToString() + " - Entrada");
                    
                }
                else
                {
                    ltbRegistros.Items.Add(reader["DATA"].ToString() + " - Saída");
                    
                }
         
            }

            txtUltimoacesso.Text = ltbRegistros.Items[0].ToString();

            reader.Close();
            consulta.Command.Parameters.Clear();
            consulta.Connection.Close();


        }

        

        //Grupo_box - Situação

        private void btnAdd_n_mat_Click(object sender, EventArgs e)
        {
            if (txtN_mat_add.Text != string.Empty)
            {
                buscar_aluno_situacao();
            }

           
        }

        public void buscar_aluno_situacao()
        {
            consulta.Command.CommandText =
                   @"SELECT * FROM ALUNOS WHERE N_MAT  =  @aluno";

            consulta.Command.Parameters.AddWithValue("@aluno", txtN_mat_add.Text);

            consulta.Connection.Open();

            SqlDataReader reader = consulta.Command.ExecuteReader();

            //int i = 0;

            while (reader.Read())
            {
                ListViewItem ltvitem = new ListViewItem(reader["N_MAT"].ToString(),0);

                ltvitem.SubItems.Add(reader["ALUNO"].ToString());
                
                //OPA

                for (int z = 0; z < tab_disciplinas.Rows.Count; z++)
                {
                    if (tab_disciplinas.Rows[z][0].ToString() == reader["ID_DISCIPLINA_ATUAL"].ToString())
                    {
                        ltvitem.SubItems.Add(tab_disciplinas.Rows[z][1].ToString());
                        break;
                    }                   
                }

                if(ltvitem.SubItems.Count == 2)
                {
                    ltvitem.SubItems.Add("NENHUMA");
                }

                if (reader["ATIVO"].ToString() == "1")
                {
                    ltvitem.SubItems.Add("ATIVO");
                }
                else
                {
                    ltvitem.SubItems.Add("INATIVO");
                }                
                

                ltvAlunos_situacao.Items.Add(ltvitem);
            }

            reader.Close();
            consulta.Command.Parameters.Clear();
            consulta.Connection.Close();

        }

        private void btnLiberar_acesso_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            sql_bd.Command.CommandText = @"UPDATE ACESSO_CATRACA SET ULTIMO_ACESSO=@ultimo, ATIVO=@ativo WHERE ID_CARTAO=@id_cartao";

            sql_bd.Command.Parameters.AddWithValue("@id_cartao", System.Convert.ToInt64(txtIdcartao_selecionado.Text));
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



                Cursor.Current = Cursors.Default;
            }
        }

        private void txtN_mat_add_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_n_mat.PerformClick();
                txtN_mat_add.Text = string.Empty;
                txtN_mat_add.Focus();
            }
        }

        private void ltvAlunos_situacao_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (ltvAlunos_situacao.SelectedItems.Count > 0)
                {
                    var selectedItems = ltvAlunos_situacao.SelectedItems;
                    foreach (ListViewItem selectedItem in selectedItems)
                    {
                        ltvAlunos_situacao.Items.RemoveAt(selectedItem.Index);
                    }
                }

            }
        }

        private void btnApagar_lista_Click(object sender, EventArgs e)
        {
            ltvAlunos_situacao.Items.Clear();
        }
        
        private void btnInativar_Click(object sender, EventArgs e)
        {
            alterar_situacao_inativar();
        }

        private void btnAtivar_Click(object sender, EventArgs e)
        {
            alterar_situacao_ativar();
        }

        public void alterar_situacao_inativar()
        {
            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            for (int i = 0; i < ltvAlunos_situacao.Items.Count; i++)
            {
                sql_bd.Command.CommandText =
                   @"UPDATE ALUNOS SET ATIVO = 0 WHERE N_MAT  =  @aluno";
                sql_bd.Command.Parameters.AddWithValue("@aluno", ltvAlunos_situacao.Items[i].Text);
                sql_bd.Connection.Open();
                try
                {
                    if (sql_bd.Connection.State != ConnectionState.Closed)
                    {
                        sql_bd.Connection.Close();
                    }
                    sql_bd.Connection.Open();

                    //Executa a instrução SQL
                    sql_bd.Command.ExecuteNonQuery();

                    ltvAlunos_situacao.Items[i].SubItems[3].Text = "INATIVO";
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    sql_bd.Command.Parameters.Clear();
                    sql_bd.Connection.Close();
                    
                }
            }
            MessageBox.Show("Situação Alterada com Sucesso!", "Alteração");

        }

        public void alterar_situacao_ativar()
        {
            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            for (int i = 0; i < ltvAlunos_situacao.Items.Count; i++)
            {
                sql_bd.Command.CommandText =
                   @"UPDATE ALUNOS SET ATIVO = 1 WHERE N_MAT  =  @aluno";
                sql_bd.Command.Parameters.AddWithValue("@aluno", ltvAlunos_situacao.Items[i].Text);
                sql_bd.Connection.Open();
                try
                {
                    if (sql_bd.Connection.State != ConnectionState.Closed)
                    {
                        sql_bd.Connection.Close();
                    }
                    sql_bd.Connection.Open();

                    //Executa a instrução SQL
                    sql_bd.Command.ExecuteNonQuery();

                    ltvAlunos_situacao.Items[i].SubItems[3].Text = "ATIVO";
                    
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    sql_bd.Command.Parameters.Clear();
                    sql_bd.Connection.Close();
                    
                }
            }
            MessageBox.Show("Situação Alterada com Sucesso!", "Alteração");

        }

        #endregion
        
        #region Tab Usuários

        public void filtra_users()
        {
            //lista.Clear();
            //ltbResultados.DataSource = null;

            consulta2.Command.CommandText =
                @"SELECT * FROM ACESSO_CATRACA WHERE ID_CARTAO LIKE @id ORDER BY ID_CARTAO ";

            consulta2.Command.Parameters.AddWithValue("@id", "%" + txtIdcartao.Text + "%");

            try
            {


                if (consulta2.Fill() >= 0)
                {
                    bsId_users.DataSource = consulta2.Table;
                    dgvID_users.DataSource = bsId_users;
                }
                else MessageBox.Show(consulta2.Error.Message);


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
                dgvID_users.Columns[0].Width = 200;
                //consulta.Command.Parameters.Clear();
                //consulta.Connection.Close();
            }
            //larguracoluna();
        }

        private void dgvID_users_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //txtNome.Text = dgvResultados.Rows[dgvResultados.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtIdcartao_selecionado.Text = dgvID_users.Rows[dgvID_users.CurrentCell.RowIndex].Cells[0].Value.ToString();
            //txtRG.Text = dgvResultados.Rows[dgvResultados.CurrentCell.RowIndex].Cells[1].Value.ToString();
            ltbRegistros_users.Items.Clear();
            buscar_registros_users();

            if (ltbRegistros_users.Items.Count != 0)
            {
                txtUltimoacesso_users.Text = ltbRegistros_users.Items[0].ToString().Remove(10);
                verificar_data_users();
            }
        }

        public void buscar_registros_users()
        {
            consulta2.Command.CommandText =
                @"SELECT * FROM CAT_ENT_SAI WHERE ID_CARTAO =  @id ORDER BY COD_ACESSO DESC";

            consulta2.Command.Parameters.AddWithValue("@id", txtIdcartao_selecionado.Text);

            consulta2.Connection.Open();

            SqlDataReader reader = consulta2.Command.ExecuteReader();

            //int i = 0;

            while (reader.Read())
            {
                if (reader["ENT_SAI"].ToString() == "1")
                {
                    ltbRegistros_users.Items.Add(reader["DATA"].ToString() + " - Entrada");
                }
                else
                {
                    ltbRegistros_users.Items.Add(reader["DATA"].ToString() + " - Saída");
                }
                //i++;
            }

            reader.Close();
            consulta2.Command.Parameters.Clear();
            consulta2.Connection.Close();


        }

        public void verificar_data_users()
        {
            DateTime Ultima = DateTime.Parse(txtUltimoacesso_users.Text);
            DateTime hoje = DateTime.Now;

            TimeSpan TSDiferenca = hoje - Ultima;
            int DiferencaEmDias = TSDiferenca.Days;


            //if (DiferencaEmDias > 61)
            //{
            //    lblSituacao.Text = "Ultima presença há mais de 60 dias.";
            //    lblSituacao.ForeColor = Color.Red;
            //}
            //else
            //{

            //}
        }

        #endregion

       

        

        

        

        

        

        
    }
}
