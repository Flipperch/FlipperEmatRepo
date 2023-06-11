using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework13032.Alunos
{
    class csRematriculas
    {
        //SQL
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        public int id_rematricula { get; set; }
        public DateTime data_rematricula { get; set; }
        public int id_user_lanc { get; set; }
        public string n_mat { get; set; }

        //INSERT
        public void add_rematricula(DateTime data_rematricula, int id_user_lanc, string n_mat)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"INSERT INTO REMATRICULAS VALUES (@data_rematricula, @id_user_lanc, @n_mat)";
            sql_comm.Parameters.AddWithValue("@data_rematricula", data_rematricula);
            sql_comm.Parameters.AddWithValue("@id_user_lanc", id_user_lanc);
            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            
            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
        }

        //DELETE
        public void del_rematricula(int id_rematricula)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"DELETE FROM REMATRICULAS WHERE ID_REMATRICULA=@id_rematricula";
            sql_comm.Parameters.AddWithValue("@id_rematricula", id_rematricula);

            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
        }

        //SELECT 
        public DataTable sel_rematriculas(string n_mat_pesquisa="")
        {
            DataTable dt = new DataTable();

            sql_comm.Connection = sql_conn;
            string sql_text = @"SELECT * FROM REMATRICULAS";
            if (n_mat_pesquisa != string.Empty) sql_text = @"SELECT * FROM REMATRICULAS WHERE N_MAT='" + n_mat_pesquisa + "'";
            
            SqlDataAdapter da = new SqlDataAdapter(sql_text, sql_conn);

            try
            {
                sql_conn.Open();
                da.Fill(dt);
                da.FillSchema(dt, SchemaType.Source);
            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return dt;
        }

        public DataTable sel_rematriculas_para_exibir(string n_mat_pesquisa = "")
        {
            DataTable dt = new DataTable();

            sql_comm.Connection = sql_conn;
            string sql_text = @"SELECT ID_REMATRICULA, DATA_REMATRICULA, NOME, N_MAT
                                FROM REMATRICULAS JOIN USUARIOS ON REMATRICULAS.ID_USER_LANC = USUARIOS.ID_USUARIO ORDER BY DATA_REMATRICULA";
            if (n_mat_pesquisa != string.Empty) 
                sql_text = @"SELECT ID_REMATRICULA, DATA_REMATRICULA, NOME, N_MAT 
                             FROM REMATRICULAS JOIN USUARIO ON REMATRICULAS.ID_USER_LANC = USUARIO.ID_USUARIO
                             WHERE N_MAT='" + n_mat_pesquisa + "' ORDER BY DATA_REMATRICULA";

            SqlDataAdapter da = new SqlDataAdapter(sql_text, sql_conn);

            try
            {
                sql_conn.Open();
                da.Fill(dt);
                da.FillSchema(dt, SchemaType.Source);
            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return dt;
        }
    }
}
