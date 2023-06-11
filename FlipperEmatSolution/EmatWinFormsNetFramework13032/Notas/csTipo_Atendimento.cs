using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Novos Using
using System.Data;
using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Notas
{
    public class csTipo_Atendimento
    {
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();


        public int id_tipo_atendimento { get; set; }
        public string tipo_atendimento { get; set; }
        public string mencao_padrao { get; set; }
        public int id_disciplina { get; set; }

        //LISTA PRINCIPAL- TIPOS DE ATENDIMENTOS
        public List<csTipo_Atendimento> lista_tipos_atendimentos(int id_disciplina_pesquisa = 0, string tipo_atendimento_pesquisa = "")
        {
            List<csTipo_Atendimento> list_ = new List<csTipo_Atendimento>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM TIPO_ATENDIMENTO ";
            

            if(id_disciplina_pesquisa>0)
            {
                sql_comm.CommandText = @"SELECT * FROM TIPO_ATENDIMENTO WHERE ID_DISCIPLINA=@id_disciplina_pesquisa ";
                sql_comm.Parameters.AddWithValue("@id_disciplina_pesquisa", id_disciplina_pesquisa);
            }

            if(tipo_atendimento_pesquisa!=string.Empty)
            {
                if (!sql_comm.CommandText.Contains("WHERE"))
                {
                    sql_comm.CommandText += @"WHERE TIPO_ATENDIMENTO=@tipo_atendimento_pesquisa ";
                }
                else
                {
                    sql_comm.CommandText += @"AND TIPO_ATENDIMENTO=@tipo_atendimento_pesquisa ";
                }
               
                sql_comm.Parameters.AddWithValue("@tipo_atendimento_pesquisa", tipo_atendimento_pesquisa);
            }

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    csTipo_Atendimento cs_tipos_atendimentos_ = new csTipo_Atendimento();
                    cs_tipos_atendimentos_.id_tipo_atendimento = Convert.ToInt32(reader["ID_TIPO_ATENDIMENTO"]);
                    cs_tipos_atendimentos_.tipo_atendimento = reader["TIPO_ATENDIMENTO"].ToString();
                    cs_tipos_atendimentos_.mencao_padrao = reader["MENCAO_PADRAO"].ToString();
                    cs_tipos_atendimentos_.id_disciplina = Convert.ToInt32(reader["ID_DISCIPLINA"]);
                    list_.Add(cs_tipos_atendimentos_);
                }
                reader.Close();
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

            return list_;
        }                
        
        public DataTable buscarTipo_Atendimento_ind(int id_disciplina)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM TIPO_ATENDIMENTO WHERE ID_DISCIPLINA=@id_disciplina";

            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);

            DataTable table = new DataTable();
            table.Columns.Add("id_tipo_atendimento");       //0
            table.Columns.Add("tipo_atendimento");        //1
            table.Columns.Add("mencao_padrao");          //2 

            int i = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add();
                    table.Rows[i][0] = reader["ID_TIPO_ATENDIMENTO"].ToString();
                    table.Rows[i][1] = reader["TIPO_ATENDIMENTO"].ToString();
                    table.Rows[i][2] = reader["MENCAO_PADRAO"].ToString();                    
                    
                    
                    i++;
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

            return table;
        }

        public int troca_nome_tipoatend_id(string tipo_atendimento, int id_disciplina)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_TIPO_ATENDIMENTO FROM TIPO_ATENDIMENTO WHERE TIPO_ATENDIMENTO=@tipo_atendimento AND ID_DISCIPLINA=@id_disciplina";

            sql_comm.Parameters.AddWithValue("@tipo_atendimento", tipo_atendimento);
            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);

            int a = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = (int)reader["ID_TIPO_ATENDIMENTO"];
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

        public string troca_id_tipoatend_nome(int id_tipo_atendimento)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT TIPO_ATENDIMENTO FROM TIPO_ATENDIMENTO WHERE ID_TIPO_ATENDIMENTO=@id_tipo_atendimento";

            sql_comm.Parameters.AddWithValue("@id_tipo_atendimento", id_tipo_atendimento);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = (string)reader["TIPO_ATENDIMENTO"];
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

        public void adicionarTipo_Atendimento(string tipo_atendimento, float mencao_padrao, int id_disciplina) //verificar nota-FLOAT !!
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"INSERT INTO TIPO_ATENDIMENTO (TIPO_ATENDIMENTO, MENCAO_PADRAO, ID_DISCIPLINA) 
                                               VALUES (@tipo_atendimento,@mencao_padrao, @id_disciplina)";

            sql_comm.Parameters.AddWithValue("@tipo_atendimento", tipo_atendimento);
            sql_comm.Parameters.AddWithValue("@mencao_padrao", mencao_padrao);
            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
            
            
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

        public void adicionarTipo_Atendimento_semnota(string tipo_atendimento, int id_disciplina) //verificar nota-FLOAT !!
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"INSERT INTO TIPO_ATENDIMENTO (TIPO_ATENDIMENTO,  ID_DISCIPLINA) 
                                               VALUES (@tipo_atendimento, @id_disciplina)";

            sql_comm.Parameters.AddWithValue("@tipo_atendimento", tipo_atendimento);

            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);


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

        public void excluirTipo_Atendimento(int id_tipo_atendimento)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"DELETE FROM TIPO_ATENDIMENTO WHERE ID_TIPO_ATENDIMENTO=@id_tipo_atendimento";

            sql_comm.Parameters.AddWithValue("@id_tipo_atendimento", id_tipo_atendimento);

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

        public void modificaTipo_Atendimento(int id_tipo_atendimento, string tipo_atendimento, float mencao_padrao, int id_disciplina)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"UPDATE TIPO_ATENDIMENTO SET TIPO_ATENDIMENTO=@tipo_atendimento, MENCAO_PADRAO=@mencao_padrao, ID_DISCIPLINA=@id_disciplina WHERE ID_TIPO_ATENDIMENTO=@id_tipo_atendimento";

            sql_comm.Parameters.AddWithValue("@id_tipo_atendimento", id_tipo_atendimento);
            sql_comm.Parameters.AddWithValue("@tipo_atendimento", tipo_atendimento);
            sql_comm.Parameters.AddWithValue("@mencao_padrao", mencao_padrao);
            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);

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

        public void modificaTipo_Atendimento_semnota(int id_tipo_atendimento, string tipo_atendimento,  int id_disciplina)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"UPDATE TIPO_ATENDIMENTO SET TIPO_ATENDIMENTO=@tipo_atendimento,  ID_DISCIPLINA=@id_disciplina WHERE ID_TIPO_ATENDIMENTO=@id_tipo_atendimento";

            sql_comm.Parameters.AddWithValue("@id_tipo_atendimento", id_tipo_atendimento);
            sql_comm.Parameters.AddWithValue("@tipo_atendimento", tipo_atendimento);
   
            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);

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

        public List<string> lista_de_tipos(int id_disciplina)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM TIPO_ATENDIMENTO WHERE ID_DISCIPLINA=@id_disciplina";

            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);

            List<string> list = new List<string>();           

            int i = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    list.Add((string)reader["TIPO_ATENDIMENTO"]);
                    i++;
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

            return list;
        }

        public bool ha_mencao(int id_tipo_atendimento)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT MENCAO_PADRAO FROM TIPO_ATENDIMENTO WHERE ID_TIPO_ATENDIMENTO=@id_tipo_atendimento";

            sql_comm.Parameters.AddWithValue("@id_tipo_atendimento", id_tipo_atendimento);

            bool a = true;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if(reader["MENCAO_PADRAO"] == DBNull.Value)
                    {
                        a = false;
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

            return a;
        }

        
    }
}
