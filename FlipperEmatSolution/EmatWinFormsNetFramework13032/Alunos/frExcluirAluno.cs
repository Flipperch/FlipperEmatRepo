using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Alunos
{
    public partial class frExcluirAluno : Form
    {
        public frExcluirAluno()
        {
            InitializeComponent();
        }

        public string n_mat_p_excluir = "";
        public string id_cartao_p_excluir = "";

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            lblInforme.Text = string.Empty;

            if(txtSenha.Text != string.Empty)
            {
                btnExcluir.Enabled = true;              
            }
            else
            {
                btnExcluir.Enabled = false;
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if(txtSenha.Text == "sss")
            {
                excluir_tab_aluno();
                excluir_tab_hist_f();
                excluir_da_tab_historico();
                excluir_tab_catraca();

                this.Close();


            }
            else
            {
                lblInforme.Text = "Senha Incorreta!";
            }
        }

        private void excluir_tab_aluno()
        {
            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            sql_bd.Command.Parameters.Clear();
            sql_bd.Command.CommandText = @"DELETE FROM ALUNOS WHERE N_MAT='" + n_mat_p_excluir +"'";

            

            if (sql_bd.Connection.State != ConnectionState.Closed)
            {
                sql_bd.Connection.Close();
            }
            sql_bd.Connection.Open();

            //Executa a instrução SQL
            sql_bd.Command.ExecuteNonQuery();

            //Fecha a conexão 
            sql_bd.Command.Parameters.Clear();
            sql_bd.Connection.Close(); 

        }

        private void excluir_tab_catraca()
        {
            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            sql_bd.Command.Parameters.Clear();
            sql_bd.Command.CommandText = @"DELETE FROM ACESSO_CATRACA WHERE ID_CARTAO='" + id_cartao_p_excluir + "'";


            if (sql_bd.Connection.State != ConnectionState.Closed)
            {
                sql_bd.Connection.Close();
            }
            sql_bd.Connection.Open();

            //Executa a instrução SQL
            sql_bd.Command.ExecuteNonQuery();

            //Fecha a conexão 
            sql_bd.Command.Parameters.Clear();
            sql_bd.Connection.Close();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //sem USO
        private void excluir_tab_hist_f()
        {
            //SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            //sql_bd.Command.Parameters.Clear();
            //sql_bd.Command.CommandText = @"DELETE FROM HIST_ALUNO_F WHERE N_MAT='" + n_mat_p_excluir + "'"; 

            //if (sql_bd.Connection.State != ConnectionState.Closed)
            //{
            //    sql_bd.Connection.Close();
            //}
            //sql_bd.Connection.Open();

            ////Executa a instrução SQL
            //sql_bd.Command.ExecuteNonQuery();

            ////Fecha a conexão 
            //sql_bd.Command.Parameters.Clear();
            //sql_bd.Connection.Close();

        }

        private void excluir_da_tab_historico()
        {
            //SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            //sql_bd.Command.Parameters.Clear();
            //sql_bd.Command.CommandText = @"DELETE FROM HISTORICO WHERE N_MAT='" + n_mat_p_excluir + "'";

            //if (sql_bd.Connection.State != ConnectionState.Closed)
            //{
            //    sql_bd.Connection.Close();
            //}
            //sql_bd.Connection.Open();

            ////Executa a instrução SQL
            //sql_bd.Command.ExecuteNonQuery();

            ////Fecha a conexão 
            //sql_bd.Command.Parameters.Clear();
            //sql_bd.Connection.Close();

        }

    }
}
