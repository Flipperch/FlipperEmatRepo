using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using EmatWinFormsNetFramework13032.Configurações;

namespace EmatWinFormsNetFramework13032.Alunos
{
    class csSituacoes
    {
        ///ATENÇÃO !!!
        //OS ids de NÃO FREQUENTANDO E CONCLUIU DEVEM SER FIXOS PARA O FUNCIONAMENTO CORRETO
        

        //SQL
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();
  
        //Atributos 
        public int id_situacao { get; set; }
        public string situacao { get; set; }
        public string descricao { get; set; }
        
        ///Métodos
        public void add_situacao(int id_situacao_, string n_mat_, string motivo_, int id_user_lanc_)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"INSERT INTO HISTORICOS_SITUACOES (ID_SITUACAO, DATA_ENTRADA, N_MAT, MOTIVO, ID_USER_LANC)
                                                               VALUES (@id_situacao_, @data_entrada_, @n_mat_, @motivo_, @id_user_lanc_)";

            sql_comm.Parameters.AddWithValue("@data_entrada_", DateTime.Now);
            sql_comm.Parameters.AddWithValue("@id_situacao_", id_situacao_);
            sql_comm.Parameters.AddWithValue("@n_mat_", n_mat_);
            sql_comm.Parameters.AddWithValue("@id_user_lanc_", id_user_lanc_);
            sql_comm.Parameters.AddWithValue("@motivo_", motivo_);

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
       
        public DataTable sel_situacoes(int id_situacao_pesquisa = 0)
        {
            DataTable dt = new DataTable();
            
            string str_comm = @"SELECT * FROM SITUACOES";

            if (id_situacao_pesquisa > 0)
            {
                str_comm = @"SELECT * FROM SITUACOES WHERE ID_SITUACAO=" + id_situacao_pesquisa;
            }

            SqlDataAdapter da = new SqlDataAdapter(str_comm, sql_conn);

            try
            {
                sql_conn.Open();
                da.Fill(dt);
                da.FillSchema(dt,SchemaType.Source);
            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                
                sql_conn.Close();
            }

            return dt;
        }

        //TROCA SITUACÕES
        public string troca_situacao_id_por_nome(int id_situacao_)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT SITUACAO FROM SITUACOES WHERE ID_SITUACAO=@id_situacao_";

            sql_comm.Parameters.AddWithValue("@id_situacao_", id_situacao_);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["SITUACAO"].ToString();
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

            return a;
        }
        public int troca_situacao_nome_por_id(string situacao_)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_SITUACAO FROM SITUACOES WHERE SITUACAO=@situacao_";

            sql_comm.Parameters.AddWithValue("@situacao_", situacao_);

            int a = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = (int)reader["ID_SITUACAO"];
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
    }
}
