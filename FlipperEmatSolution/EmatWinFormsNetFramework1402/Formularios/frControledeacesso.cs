using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Data.SqlClient;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frControledeacesso : Form
    {
        //TODO==>>> URGENTE Verificar utilidade deste formulário. Dependendo, agilizar implemetação ou excluir
        private readonly IEmatriculaSettings _settings;
        DataTable tbAlunos = new DataTable();
        BindingSource bsAlunos = new BindingSource();
        BindingSource bsId_users = new BindingSource();
        DataTable tab_disciplinas = new DataTable();
        List<string> lista = new List<string>();

        public frControledeacesso(IEmatriculaSettings settings)
        {
            InitializeComponent();
            _settings = settings;
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
                if (Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.ADMINISTRADOR ||
                    Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.COORDENADOR ||
                    Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.DIRETOR)
                {

                }
                else
                {

                    tabControl1.SelectedTab = tabPage1;
                    MessageBox.Show("Acesso não Autorizado!", "E-matrícula: Aba - Acesso de Funcionários");
                }
            }
            else
            {

            }
        }

        public void fill_tab_disciplina()
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("@SELECT * FROM DISCIPLINAS", sqlHelper.ematConn);

            sql_comm.Connection.Open();

            SqlDataReader reader = sql_comm.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                tab_disciplinas.Rows.Add();
                tab_disciplinas.Rows[i][0] = reader["ID_DISCIPLINA"].ToString();
                tab_disciplinas.Rows[i][1] = reader["N_DISCIPLINA"].ToString();
                i++;
            }

            reader.Close();
            sql_comm.Parameters.Clear();
            sql_comm.Connection.Close();
        }

        #region Tab Alunos

        private void btPesquisa_Click(object sender, EventArgs e)
        {
            txtNome.Text = string.Empty;
            txtRG.Text = string.Empty;
            label.Text = string.Empty;
            txtUltimoacesso.Text = string.Empty;

            if (txtPesquisa_numat.Text != string.Empty)
            {
                buscar_aluno();
                buscar_registros();
            }
            else
            {
                MessageBox.Show("Informe o número de matrícula procurado.", "E-matrícula: Pesquisar");
            }

        }

        public void buscar_aluno()
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand(@"SELECT * FROM ALUNOS WHERE N_MAT  =  @aluno", sqlHelper.ematConn);

            sql_comm.Parameters.AddWithValue("@aluno", txtPesquisa_numat.Text);

            sql_comm.Connection.Open();

            SqlDataReader reader = sql_comm.ExecuteReader();

            while (reader.Read())
            {
                txtNome.Text = reader["ALUNO"].ToString();
                txtRG.Text = reader["RG"].ToString();
                //OPA

                for (int i = 0; i < tab_disciplinas.Rows.Count; i++)
                {
                    if (tab_disciplinas.Rows[i][0].ToString() == reader["ID_DISCIPLINA_ATUAL"].ToString())
                    {
                        txtMat_atual.Text = tab_disciplinas.Rows[i][1].ToString();
                        break;
                    }

                }
            }

            reader.Close();
            sql_comm.Parameters.Clear();
            sql_comm.Connection.Close();

        }

        public void buscar_registros()
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand(@"SELECT * FROM ALUNOS WHERE N_MAT  =  @aluno", sqlHelper.ematConn);


            sql_comm.CommandText =
                @"SELECT * FROM CAT_ENT_SAI WHERE ID_CARTAO =  @aluno ORDER BY COD_ACESSO DESC";

            sql_comm.Parameters.AddWithValue("@aluno", txtPesquisa_numat.Text);

            sql_comm.Connection.Open();

            SqlDataReader reader = sql_comm.ExecuteReader();



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
            sql_comm.Parameters.Clear();
            sql_comm.Connection.Close();


        }

        private void btnAdd_n_mat_Click(object sender, EventArgs e)
        {
            if (txtN_mat_add.Text != string.Empty)
            {
                buscar_aluno_situacao();
            }


        }

        public void buscar_aluno_situacao()
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand(@"SELECT * FROM ALUNOS WHERE N_MAT  =  @aluno", sqlHelper.ematConn);

            sql_comm.Parameters.AddWithValue("@aluno", txtN_mat_add.Text);

            sql_comm.Connection.Open();

            SqlDataReader reader = sql_comm.ExecuteReader();

            //int i = 0;

            while (reader.Read())
            {
                ListViewItem ltvitem = new ListViewItem(reader["N_MAT"].ToString(), 0);

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

                if (ltvitem.SubItems.Count == 2)
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
            sql_comm.Parameters.Clear();
            sql_comm.Connection.Close();

        }

        private void btnLiberar_acesso_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand(@"UPDATE ACESSO_CATRACA SET ULTIMO_ACESSO=@ultimo, 
                                                    ATIVO=@ativo WHERE ID_CARTAO=@id_cartao", sqlHelper.ematConn);

            sql_comm.Parameters.AddWithValue("@id_cartao", System.Convert.ToInt64(txtIdcartao_selecionado.Text));
            sql_comm.Parameters.AddWithValue("@ativo", 1);
            sql_comm.Parameters.AddWithValue("@ultimo", DateTime.Now);

            try
            {
                if (sql_comm.Connection.State != ConnectionState.Closed)
                {
                    sql_comm.Connection.Close();
                }
                sql_comm.Connection.Open();

                //Executa a instrução SQL
                sql_comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_comm.Connection.Close();



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
            SqlHelper sqlHelper = new SqlHelper();


            for (int i = 0; i < ltvAlunos_situacao.Items.Count; i++)
            {
                SqlCommand sql_comm = new SqlCommand(@"UPDATE ALUNOS SET ATIVO = 0 WHERE N_MAT  =  @aluno", sqlHelper.ematConn);

                sql_comm.Parameters.AddWithValue("@aluno", ltvAlunos_situacao.Items[i].Text);
                sql_comm.Connection.Open();
                try
                {
                    if (sql_comm.Connection.State != ConnectionState.Closed)
                    {
                        sql_comm.Connection.Close();
                    }
                    sql_comm.Connection.Open();

                    //Executa a instrução SQL
                    sql_comm.ExecuteNonQuery();

                    ltvAlunos_situacao.Items[i].SubItems[3].Text = "INATIVO";
                }
                catch (Exception ex)
                {
                    ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                }
                finally
                {
                    sql_comm.Parameters.Clear();
                    sql_comm.Connection.Close();

                }
            }
            MessageBox.Show("Situação Alterada com Sucesso!", "E-matrícula: Alteração");

        }

        public void alterar_situacao_ativar()
        {
            SqlHelper sqlHelper = new SqlHelper();


            for (int i = 0; i < ltvAlunos_situacao.Items.Count; i++)
            {
                SqlCommand sql_comm = new SqlCommand(@"UPDATE ALUNOS SET ATIVO = 1 WHERE N_MAT  =  @aluno", sqlHelper.ematConn);

                sql_comm.Parameters.AddWithValue("@aluno", ltvAlunos_situacao.Items[i].Text);
                sql_comm.Connection.Open();
                try
                {
                    if (sql_comm.Connection.State != ConnectionState.Closed)
                    {
                        sql_comm.Connection.Close();
                    }
                    sql_comm.Connection.Open();

                    //Executa a instrução SQL
                    sql_comm.ExecuteNonQuery();

                    ltvAlunos_situacao.Items[i].SubItems[3].Text = "ATIVO";

                }
                catch (Exception ex)
                {
                    ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                }
                finally
                {
                    sql_comm.Parameters.Clear();
                    sql_comm.Connection.Close();

                }
            }
            MessageBox.Show("Situação Alterada com Sucesso!", "E-matrícula: Alteração");

        }

        #endregion

        #region Tab Usuários

        public void filtra_users()
        {
            ////lista.Clear();
            ////ltbResultados.DataSource = null;
            //
            //SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
            //SqlCommand sql_comm = new SqlCommand(@"SELECT * FROM ACESSO_CATRACA WHERE ID_CARTAO LIKE @id ORDER BY ID_CARTAO ", sqlHelper.ematConn);
            //
            //
            //sql_comm.Parameters.AddWithValue("@id", "%" + txtIdcartao.Text + "%");
            //
            //try
            //{
            //
            //
            //    if (consulta2.Fill() >= 0)
            //    {
            //        bsId_users.DataSource = consulta2.Table;
            //        dgvID_users.DataSource = bsId_users;
            //    }
            //    else MessageBox.Show(consulta2.Error.Message);
            //
            //
            //}
            //catch (Exception ex)
            //{
            //    string msg = ex.Message;
            //    if (msg.ToUpper().Contains("TIMEOUT EXPIRED"))
            //        msg = "Tempo de espera esgotado...";
            //    MessageBox.Show(msg);
            //}
            //finally
            //{
            //    dgvID_users.Columns[0].Width = 200;
            //    //consulta.Command.Parameters.Clear();
            //    //consulta.Connection.Close();
            //}
            ////larguracoluna();
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
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand(@"SELECT * FROM CAT_ENT_SAI WHERE ID_CARTAO =  @id ORDER BY COD_ACESSO DESC", sqlHelper.ematConn);

            sql_comm.Parameters.AddWithValue("@id", txtIdcartao_selecionado.Text);

            sql_comm.Connection.Open();

            SqlDataReader reader = sql_comm.ExecuteReader();

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
            sql_comm.Parameters.Clear();
            sql_comm.Connection.Close();


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
