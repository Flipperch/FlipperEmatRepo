using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework13032.Notas
{
    class csEnsinos
    {
        //SQL
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        public int id_troca_ensino { get; set; }
        public DateTime data_troca { get; set; }
        public int id_ensino { get; set; }
        public string n_mat { get; set; }
        public int id_user_troca { get; set; }

        //Métodos

        public void salvar_troca_ensino(DateTime data_troca, int id_ensino, string n_mat)
        {
            List<csEnsinos> list_ = sel_troca_ensino(n_mat, id_ensino);
            if(list_.Count > 0)
            {
                //UPDATE
                upd_troca_ensino(data_troca, id_ensino, n_mat, list_[0].id_troca_ensino);
            }
            else
            {
                //INSERT
                add_troca_ensino(data_troca, id_ensino, n_mat);
            }
        }

        public List<csEnsinos> sel_troca_ensino(string n_mat, int id_ensino=0)
        {
            List<csEnsinos> list_ = new List<csEnsinos>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM HISTORICO_TROCAS_ENSINO WHERE N_MAT=@n_mat ";
            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);

            if (id_ensino > 0)
            {
                sql_comm.CommandText += " AND ID_ENSINO=@id_ensino";
                sql_comm.Parameters.AddWithValue("@id_ensino", id_ensino);
            }

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while(reader.Read())
                {
                    csEnsinos cs_ensino = new csEnsinos();
                    cs_ensino.id_troca_ensino = Convert.ToInt32(reader["ID_TROCA_ENSINO"].ToString());
                    cs_ensino.id_ensino = Convert.ToInt32(reader["ID_ENSINO"].ToString());
                    cs_ensino.data_troca = DateTime.Parse(reader["DATA_TROCA"].ToString());
                    cs_ensino.id_user_troca = Convert.ToInt32(reader["ID_USER_LANC"].ToString());
                    list_.Add(cs_ensino);
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }

            return list_;
        }

        private void add_troca_ensino(DateTime data_troca, int id_ensino, string n_mat)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"INSERT INTO HISTORICO_TROCAS_ENSINO VALUES (@data_troca, @id_ensino, @n_mat, @id_user_lanc)";

            sql_comm.Parameters.AddWithValue("@data_troca", data_troca);
            sql_comm.Parameters.AddWithValue("@id_ensino", id_ensino);
            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            sql_comm.Parameters.AddWithValue("@id_user_lanc", Usuarios_Grupos.csUsuario_logado.id_usuario_logado);

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

        private void upd_troca_ensino(DateTime data_troca, int id_ensino, string n_mat, int id_troca_ensino)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE HISTORICO_TROCAS_ENSINO SET DATA_TROCA=@data_troca, ID_ENSINO=@id_ensino, N_MAT=@n_mat, ID_USER_LANC=@id_user_lanc WHERE ID_TROCA_ENSINO=@id_troca_ensino";

            sql_comm.Parameters.AddWithValue("@id_troca_ensino", id_troca_ensino);
            sql_comm.Parameters.AddWithValue("@data_troca", data_troca);
            sql_comm.Parameters.AddWithValue("@id_ensino", id_ensino);
            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            sql_comm.Parameters.AddWithValue("@id_user_lanc", Usuarios_Grupos.csUsuario_logado.id_usuario_logado);

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

        public DateTime ent_ensino(string n_mat, int id_ensino)
        {
            List<csEnsinos> list_ = sel_troca_ensino(n_mat, id_ensino);

            DateTime dt = DateTime.Now;

            for (int i = 0; i < list_.Count; i++)
            {
                dt = list_[i].data_troca;
            }
            
            return dt;
        }
    }
}
