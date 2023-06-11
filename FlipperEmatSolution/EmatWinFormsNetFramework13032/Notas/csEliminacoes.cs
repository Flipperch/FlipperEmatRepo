using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Novos Using
using System.Data;
using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Notas
{
    public class csEliminacoes
    {
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        public void adcionaEliminacoes(string n_mat, string eliminacao, int id_ensino) //verificar nota-FLOAT !!
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"INSERT INTO ELIMINACOES (N_MAT, ELIMINACAO, ID_ENSINO ) 
                                               VALUES (@n_mat, @eliminacao, @id_ensino )";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            sql_comm.Parameters.AddWithValue("@eliminacao", eliminacao);
            sql_comm.Parameters.AddWithValue("@id_ensino", id_ensino);
            

            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();
            }
            catch
            {

            }
            finally
            {

                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
        }

        public void modificaEliminacoes(string n_mat, string eliminacao, int id_ensino)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"UPDATE ELIMINACOES SET ELIMINACAO=@eliminacao, ID_ENSINO=@id_ensino WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@id_ensino", id_ensino);
            sql_comm.Parameters.AddWithValue("@eliminacao", eliminacao);
            //where
            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            try
                                    {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();

            }
            catch
            {

            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

        }

        public bool ha_Eliminacoes(string n_mat)
        {           
            bool a = false;

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ELIMINACAO FROM ELIMINACOES WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = true;
                }

                reader.Close();
            }

            catch
            {

            }

            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return a;
        }

        public string Eliminacoes(string n_mat, int id_ensino)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ELIMINACAO FROM ELIMINACOES WHERE N_MAT=@n_mat AND ID_ENSINO=@id_ensino";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            sql_comm.Parameters.AddWithValue("@id_ensino", id_ensino);

            string sequencia = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if (!(reader["ELIMINACAO"] is DBNull))
                    {
                        sequencia += reader["ELIMINACAO"].ToString();
                    }
                }
            }
            catch
            {

            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return sequencia;
        }

        public void elimina_disciplina_concluida()
        {
                    
        }

    }
}
