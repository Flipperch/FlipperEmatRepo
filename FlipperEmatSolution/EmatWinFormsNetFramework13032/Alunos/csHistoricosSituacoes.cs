using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using EmatWinFormsNetFramework13032.Configurações;

namespace EmatWinFormsNetFramework13032.Alunos
{
    class csHistoricosSituacoes
    {
        //SQL
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();
        
        //
        csSituacoes cs_situacoes = new csSituacoes();

        //Atributos 
        public int id_hist_situacao { get; set; }
        public int id_situacao { get; set; }
        public string dat_entrada { get; set; }
        public int id_user_lanc { get; set; }
        public string motivo { get; set; }
        public string n_mat { get; set; }

        ///Métodos
        //INSERT
        public void add_hist_situacao(int id_situacao_, DateTime data_entrada_, int id_user_lanc_, string motivo_, string n_mat_)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"INSERT INTO HISTORICOS_SITUACOES VALUES (@id_situacao_, @data_entrada_, @id_user_lanc_, @motivo_, @n_mat_)";
                        
            sql_comm.Parameters.AddWithValue("@id_situacao_", id_situacao_);
            sql_comm.Parameters.AddWithValue("@data_entrada_", data_entrada_);            
            sql_comm.Parameters.AddWithValue("@id_user_lanc_", id_user_lanc_);
            sql_comm.Parameters.AddWithValue("@motivo_", motivo_);
            sql_comm.Parameters.AddWithValue("@n_mat_", n_mat_);

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
        public void del_hist_situacao(int id_hist_situacao)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"DELETE FROM HISTORICOS_SITUACOES WHERE ID_HIST_SITUACAO=@id_hist_situacao";

            sql_comm.Parameters.AddWithValue("@id_hist_situacao", id_hist_situacao);

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

        ///UPDATE - Nunca irá existir um update, pois são inseridas novas entradas na tabela conforme.
        
        //SELECT
        public DataTable sel_historicos_situacacao(string n_mat_pesquisa = "")
        {
            DataTable dt = new DataTable();

            string str_comm = @"SELECT * FROM HISTORICOS_SITUACOES ORDER BY DATA_ENTRADA";

            if (n_mat != "")
            {
                str_comm = @"SELECT * FROM HISTORICOS_SITUACOES WHERE N_MAT='" + n_mat_pesquisa + "' ORDER BY DATA_ENTRADA ";
            }
            SqlDataAdapter da = new SqlDataAdapter(str_comm, sql_conn);

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
                sql_conn.Close();
            }

            return dt;
        }

        public DataTable sel_historicos_para_exibir(string n_mat_pesquisa = "")
        {
            DataTable dt = new DataTable();

            string str_comm = @"SELECT ID_HIST_SITUACAO, SITUACAO, DATA_ENTRADA, MOTIVO, NOME
                             FROM HISTORICOS_SITUACOES JOIN SITUACOES
                             ON HISTORICOS_SITUACOES.ID_SITUACAO = SITUACOES.ID_SITUACAO
                             JOIN USUARIO ON HISTORICOS_SITUACOES.ID_USER_LANC = USUARIO.ID_USUARIO
                             ORDER BY DATA_ENTRADA ";

            if (n_mat != "")
            {
                str_comm = @"SELECT ID_HIST_SITUACAO, SITUACAO, DATA_ENTRADA, MOTIVO, NOME
                             FROM HISTORICOS_SITUACOES JOIN SITUACOES
                             ON HISTORICOS_SITUACOES.ID_SITUACAO = SITUACOES.ID_SITUACAO
                             JOIN USUARIO ON HISTORICOS_SITUACOES.ID_USER_LANC = USUARIO.ID_USUARIO
                             WHERE N_MAT='" + n_mat_pesquisa + "' ORDER BY DATA_ENTRADA ";
            }
            SqlDataAdapter da = new SqlDataAdapter(str_comm, sql_conn);

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
                sql_conn.Close();
            }

            return dt;
        }

        public string situacao_atual(string n_mat)
        {
            string retorno = "";

            List<int> list = (from row in sel_historicos_situacacao(n_mat).AsEnumerable()
                              select row.Field<int>("ID_SITUACAO")).ToList<int>();

            if(list.Count > 0)
            {
                retorno = cs_situacoes.sel_situacoes(list[list.Count - 1]).Rows[0][1].ToString();
            }

            return retorno;
        }

        public DateTime data_situacao_atual(string n_mat)
        {
            DateTime retorno = DateTime.Now;

            List<DateTime> list = (from row in sel_historicos_situacacao(n_mat).AsEnumerable()
                                   select row.Field<DateTime>("DATA_ENTRADA")).ToList<DateTime>();

            if (list.Count > 0)
            {
                DateTime.TryParse(list[list.Count - 1].ToString(), out retorno);
            }

            return retorno;
        }

    }
}
