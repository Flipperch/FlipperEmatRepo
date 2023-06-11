using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Novos Using
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace EmatWinFormsNetFramework13032.Notas
{
    public class csAtendimentos
    {
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        public int id_atendimento { get; set; }
        public int id_tipo_atendimento { get; set; }
        public string data_atendimento { get; set; }
        public string n_mat { get; set; }
        public string modulo { get; set; }
        public int id_user_lanc { get; set; }
        public int id_disciplina { get; set; }
        public int id_ensino {get;set;}      

        //classes

        Alunos.csAlunos cs_alunos = new Alunos.csAlunos();
        Alunos.csHistoricosSituacoes cs_historicos_situacoes = new Alunos.csHistoricosSituacoes();
        Error_Log.csControle_erros cs_erros = new Error_Log.csControle_erros();

        public List<csAtendimentos> lista_atendimentos(int id_atendimento_pesquisa=0, int id_tipo_atendimento_pesquisa=0,string n_mat_pesquisa="",int id_ensino_pesquisa=0)
        {
            List<csAtendimentos> list_ = new List<csAtendimentos>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM ATENDIMENTOS ";

            #region Filtros Where

            if (id_atendimento_pesquisa>0)
            {
                if (!sql_comm.CommandText.Contains("WHERE"))
                    sql_comm.CommandText += "WHERE ID_ATENDIMENTO=@id_atendimento_pesquisa ";
                else
                    sql_comm.CommandText += "AND ID_ATENDIMENTO=@id_atendimento_pesquisa ";
                
                sql_comm.Parameters.AddWithValue("@id_atendimento_pesquisa", id_atendimento_pesquisa);
            }

            if (id_tipo_atendimento_pesquisa>0)
            {
                if (!sql_comm.CommandText.Contains("WHERE"))
                    sql_comm.CommandText += "WHERE ID_TIPO_ATENDIMENTO=@id_tipo_atendimento_pesquisa ";
                else
                    sql_comm.CommandText += "AND ID_TIPO_ATENDIMENTO=@id_tipo_atendimento_pesquisa ";
              
                sql_comm.Parameters.AddWithValue("@id_tipo_atendimento_pesquisa", id_tipo_atendimento_pesquisa);
            }

            if (n_mat_pesquisa!=string.Empty)
            {
                if (!sql_comm.CommandText.Contains("WHERE"))
                    sql_comm.CommandText += "WHERE N_MAT=@n_mat_pesquisa ";
                else
                    sql_comm.CommandText += "AND N_MAT=@n_mat_pesquisa ";

                sql_comm.Parameters.AddWithValue("@n_mat_pesquisa", n_mat_pesquisa);
            }

            if (id_ensino_pesquisa>0)
            {
                if (!sql_comm.CommandText.Contains("WHERE"))
                    sql_comm.CommandText += "WHERE ENSINO=@id_ensino_pesquisa ";
                else
                    sql_comm.CommandText += "AND ENSINO=@id_ensino_pesquisa ";

                sql_comm.Parameters.AddWithValue("@id_ensino_pesquisa", id_ensino_pesquisa);
            }

            #endregion

            sql_comm.CommandText += "ORDER BY DATA_ATENDIMENTO ";
            
            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    csAtendimentos cs_atendimentos_ = new csAtendimentos();
                    cs_atendimentos_.id_atendimento = Convert.ToInt32(reader["ID_ATENDIMENTO"]);
                    cs_atendimentos_.id_tipo_atendimento = Convert.ToInt32(reader["ID_TIPO_ATENDIMENTO"]);
                    cs_atendimentos_.data_atendimento = reader["DATA_ATENDIMENTO"].ToString();
                    cs_atendimentos_.n_mat = reader["N_MAT"].ToString();
                    cs_atendimentos_.modulo = reader["MODULO"].ToString();
                    cs_atendimentos_.id_user_lanc = Convert.ToInt32(reader["ID_USER_LANC"]);
                    cs_atendimentos_.id_disciplina = Convert.ToInt32(reader["ID_DISCIPLINA"]);
                    cs_atendimentos_.id_ensino = Convert.ToInt32(reader["ENSINO"]);
                    list_.Add(cs_atendimentos_);
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


        public DataTable buscarAtendimento_ind(string n_mat, int id_disciplina, int ensino)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM ATENDIMENTOS WHERE N_MAT=@n_mat AND ID_DISCIPLINA=@id_disciplina AND ENSINO=@ensino ORDER BY DATA_ATENDIMENTO";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
            sql_comm.Parameters.AddWithValue("@ensino", ensino);



            DataTable table = new DataTable();
            table.Columns.Add("id_atendimento");             //0
            table.Columns.Add("id_tipo_atendimento");        //1
            table.Columns.Add("data_atendimento");           //2 
            table.Columns.Add("modulo");                     //3
            table.Columns.Add("id_user_lanc");               //4
              

            int i = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add();
                    table.Rows[i][0] = reader["ID_ATENDIMENTO"].ToString();
                    table.Rows[i][1] = reader["ID_TIPO_ATENDIMENTO"].ToString();
                    table.Rows[i][2] = reader["DATA_ATENDIMENTO"].ToString();
                    table.Rows[i][3] = reader["MODULO"].ToString();
                    table.Rows[i][4] = reader["ID_USER_LANC"].ToString();


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

        public DataTable buscarAtendimento_com(string n_mat, int ensino)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM ATENDIMENTOS WHERE N_MAT=@n_mat AND ENSINO=@ensino ORDER BY DATA_ATENDIMENTO ASC";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            sql_comm.Parameters.AddWithValue("@ensino", ensino);


            DataTable table = new DataTable();
            table.Columns.Add("id_atendimento");             //0
            table.Columns.Add("id_tipo_atendimento");        //1
            table.Columns.Add("data_atendimento");           //2 
            table.Columns.Add("modulo");                     //3
            table.Columns.Add("id_user_lanc");               //4
            table.Columns.Add("id_disciplina");              //5


            int i = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add();
                    table.Rows[i][0] = reader["ID_ATENDIMENTO"].ToString();
                    table.Rows[i][1] = reader["ID_TIPO_ATENDIMENTO"].ToString();
                    table.Rows[i][2] = reader["DATA_ATENDIMENTO"].ToString();
                    table.Rows[i][3] = reader["MODULO"].ToString();
                    table.Rows[i][4] = reader["ID_USER_LANC"].ToString();
                    table.Rows[i][5] = reader["ID_DISCIPLINA"].ToString();


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

        //Tabela - CommandText editavel
        public DataTable buscarAtendimentos(string cmd_text)
        {
            //@"SELECT * FROM ATENDIMENTOS WHERE N_MAT=@n_mat AND ID_DISCIPLINA=@id_disciplina AND ENSINO=@ensino
            //                       ORDER BY ID_ATENDIMENTO";

            sql_comm.Connection = sql_conn;
            
            sql_comm.CommandText = cmd_text;

            //Determinar campos

            int part_A = cmd_text.IndexOf("SELECT") + "SELECT".Length + 1;

            int part_B = cmd_text.IndexOf("FROM");

            string a = cmd_text.Substring(part_A,part_B-part_A);
            a = a.Replace(" ","");
            a = a.Replace("ATENDIMENTOS.","");
            a = a.Replace("NOTAS.", "");
            a = a.Replace("ALUNOS.", "");
            a = a.Replace("USUARIO.", "");

            List<string> lista_campos = a.Split(',').ToList();


            DataTable table = new DataTable();

            for (int z = 0; z < lista_campos.Count; z++)
            {
                table.Columns.Add(lista_campos[z]);
            }
            
            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    table.Rows.Add();
                    for (int z = 0; z < lista_campos.Count; z++)
                    {
                        table.Rows[i][z] = reader[z].ToString();
                    }
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

        public void adcionarAtendimento(int id_tipo_atendimento, string data_atendimento, string n_mat, string modulo, int id_user_lanc, int id_disciplina, int ensino) //verificar nota-FLOAT !!
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"INSERT INTO ATENDIMENTOS (ID_TIPO_ATENDIMENTO, DATA_ATENDIMENTO, N_MAT, MODULO, ID_USER_LANC, ID_DISCIPLINA, ENSINO)
                                               VALUES (@id_tipo_atendimento,@data_atendimento, @n_mat, @modulo, @id_user_lanc,@id_disciplina, @ensino)";

            sql_comm.Parameters.AddWithValue("@id_tipo_atendimento", id_tipo_atendimento);
            sql_comm.Parameters.AddWithValue("@data_atendimento", data_atendimento);
            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            sql_comm.Parameters.AddWithValue("@modulo", modulo);
            sql_comm.Parameters.AddWithValue("@id_user_lanc", id_user_lanc);
            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
            sql_comm.Parameters.AddWithValue("@ensino", ensino);

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

        public void excluirAtendimento(int id_atendimento)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"DELETE FROM ATENDIMENTOS WHERE ID_ATENDIMENTO=@id_atendimento";

            sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);

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

        public void modificaAtendimento(int id_atendimento, int id_tipo_atendimento, string data_atendimento, string modulo, int id_user_lanc, string n_mat, int ensino)
        {
            sql_comm.Connection = sql_conn;

            sql_comm.CommandText = @"UPDATE ATENDIMENTOS SET ID_TIPO_ATENDIMENTO=@id_tipo_atendimento,
                                                             DATA_ATENDIMENTO=@data_atendimento,
                                                             MODULO=@modulo,
                                                             ID_USER_LANC=@id_user_lanc,
                                                             N_MAT=@n_mat,
                                                             ENSINO=@ensino
                                                             WHERE ID_ATENDIMENTO=@id_atendimento";
            
            sql_comm.Parameters.AddWithValue("@id_tipo_atendimento", id_tipo_atendimento);
            sql_comm.Parameters.AddWithValue("@data_atendimento", data_atendimento);
            sql_comm.Parameters.AddWithValue("@modulo", modulo);
            sql_comm.Parameters.AddWithValue("@id_user_lanc", id_user_lanc);
            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            sql_comm.Parameters.AddWithValue("@ensino", ensino);
            //where
            sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);

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

        public int id_ultimo_atendimento(string n_mat, int ensino)
        {          

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_ATENDIMENTO FROM ATENDIMENTOS WHERE N_MAT=@n_mat AND ENSINO=@ensino ORDER BY ID_ATENDIMENTO";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            sql_comm.Parameters.AddWithValue("@ensino", ensino);

            int a = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = (int)reader["ID_ATENDIMENTO"];
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

        public int qtd_alunos(string ensino, int id_disciplina_atual)
        {
            sql_comm.Connection = sql_conn;
            if(ensino=="AMBOS" && id_disciplina_atual==0)
            {
                sql_comm.CommandText = @"SELECT count(ID_DISCIPLINA_ATUAL) FROM ALUNOS WHERE ATIVO=1";
            }
            else if (ensino == "AMBOS" && id_disciplina_atual != 0)
            {
                sql_comm.CommandText = @"SELECT count(ID_DISCIPLINA_ATUAL) FROM ALUNOS WHERE ATIVO=1 AND ID_DISCIPLINA_ATUAL=@id_disciplina_atual";
            }
            else 
            {
                sql_comm.CommandText = @"SELECT count(ID_DISCIPLINA_ATUAL) FROM ALUNOS WHERE ATIVO=1 AND ID_DISCIPLINA_ATUAL=@id_disciplina_atual AND ENSINO=@ensino";
            }
            

            sql_comm.Parameters.AddWithValue("@id_disciplina_atual", id_disciplina_atual);
            sql_comm.Parameters.AddWithValue("@ensino", ensino);

            int a = 0;
            
            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = (int)reader[0];
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

        public int qtd_alunos_ini_ret_con(string tipo_atend, string data, bool total, int id_disciplina = 0, int ensino = 2)
        {

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT COUNT(ID_ATENDIMENTO)
                                     FROM ATENDIMENTOS JOIN TIPO_ATENDIMENTO
                                     ON ATENDIMENTOS.ID_TIPO_ATENDIMENTO = TIPO_ATENDIMENTO.ID_TIPO_ATENDIMENTO";


            if(ensino!=2)
            {
                sql_comm.CommandText += " WHERE ENSINO = @ensino";
            }
            
            if (!total)
            {
                if(sql_comm.CommandText.Contains("WHERE"))
                {
                    sql_comm.CommandText += " AND DATA_ATENDIMENTO between '" + data + " 08:00:00' AND '" + data + " 22:00:00'";
                }
                else
                {
                    sql_comm.CommandText += " WHERE DATA_ATENDIMENTO between '" + data + " 08:00:00' AND '" + data + " 22:00:00'";
                }
            }           

            if (sql_comm.CommandText.Contains("WHERE"))
            {
                sql_comm.CommandText += " AND TIPO_ATENDIMENTO = @tipo_atend";
            }
            else
            {
                sql_comm.CommandText += " WHERE TIPO_ATENDIMENTO = @tipo_atend";
            }
 
            if(id_disciplina!=0)
            {
                if (sql_comm.CommandText.Contains("WHERE"))
                {
                    sql_comm.CommandText += " AND ATENDIMENTOS.ID_DISCIPLINA = @id_disciplina";
                }
                else
                {
                    sql_comm.CommandText += " WHERE ATENDIMENTOS.ID_DISCIPLINA = @id_disciplina";
                }
            }

            
       
 
             

            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
            sql_comm.Parameters.AddWithValue("@ensino", ensino);
            sql_comm.Parameters.AddWithValue("@tipo_atend", tipo_atend);

            int a = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = (int)reader[0];
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

        public int qtd_atendimentos_filtro(string tipo_atendimento_pesquisa, int id_disciplina_pesquisa, string dat_ini, string dat_fin)
        {
            int i = 0;

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT COUNT(*) AS QTD 
                                     FROM ATENDIMENTOS 
                                     WHERE ID_TIPO_ATENDIMENTO IN (SELECT ID_TIPO_ATENDIMENTO AS ID 
                                                                  FROM TIPO_ATENDIMENTO 
                                                                  WHERE ID_DISCIPLINA=@id_disciplina_pesquisa 
                                                                  AND TIPO_ATENDIMENTO=@tipo_atendimento_pesquisa) 
                                                                  AND DATA_ATENDIMENTO BETWEEN '" + dat_ini + " 07:00:00' AND '" + dat_fin + " 23:00:00'";




            sql_comm.Parameters.AddWithValue("@tipo_atendimento_pesquisa", tipo_atendimento_pesquisa);
            sql_comm.Parameters.AddWithValue("@id_disciplina_pesquisa", id_disciplina_pesquisa);

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["QTD"] != DBNull.Value)
                    {
                        i = Convert.ToInt32(reader["QTD"]);
                    }
                }
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

            return i;
        }

        public int qtd_atendimentos( int id_disciplina, string data, bool total, int ensino = 2)
        {
            sql_comm.Connection = sql_conn;
            
                sql_comm.CommandText = @"SELECT COUNT(ID_ATENDIMENTO)
                                         FROM ATENDIMENTOS";                                

            if (ensino != 2)
            {
                sql_comm.CommandText += " WHERE ENSINO = @ensino";
            }

            if (!total)
            {
                if (sql_comm.CommandText.Contains("WHERE"))
                {
                    sql_comm.CommandText += " AND DATA_ATENDIMENTO between '" + data + " 21:59:59'";
                }
                else
                {
                    sql_comm.CommandText += " WHERE DATA_ATENDIMENTO between '" + data + " 21:59:59'";
                }
            }
                        
            if (sql_comm.CommandText.Contains("WHERE"))
            {
                sql_comm.CommandText += " AND ID_DISCIPLINA = @id_disciplina";
            }
            else
            {
                sql_comm.CommandText += " WHERE ID_DISCIPLINA = @id_disciplina";
            }

            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
            sql_comm.Parameters.AddWithValue("@ensino", ensino);
            
            int a = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = (int)reader[0];
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

        public string troca_id_nome_tipo_atendimento(int id_tipo_atendimento)
        {
            string a = "";

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT TIPO_ATENDIMENTO FROM TIPOS_ATENDIMENTOS WHERE ID_TIPO_ATENDIMENTO=@id_tipo_atendimento";

            sql_comm.Parameters.AddWithValue("id_tipo_atendimento", id_tipo_atendimento);

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();

                while(reader.Read())
                {
                    a = reader["TIPO_ATENDIMENTO"].ToString();
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

        #region Verifica presença dos alunos   

        public void rotina_inativar()
        {
            //Diagnostico
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //verificar ultima presença de cada aluno;
            //Cria tabela com n_mat
            DataTable table = tab_ultimas_presencas();
            
            DataTable table_2 = new DataTable();
            table_2.Columns.Add("n_mat");

            DateTime hoje = DateTime.Now;

            int z = 0;

            //comparar com data atual e preenche table_2 para inativar, + de 50 dias;
            for(int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][1].ToString() != "")
                {
                    DateTime Ultima = DateTime.Parse(table.Rows[i][1].ToString());                    

                    TimeSpan TSDiferenca = hoje - Ultima;
                    int DiferencaEmDias = TSDiferenca.Days;

                    if (Ultima.Month < 8)
                    {
                        if(hoje.Month > 6)
                        {
                            //eliminar qtd dias referente a Julho.
                            DiferencaEmDias = DiferencaEmDias - 30;
                        }
                    }

                    if (DiferencaEmDias > 51)
                    {
                        string[] b = cs_alunos.sequencia_rematriculas(table.Rows[i][0].ToString()).Split('|');
                        
                        //Teste Rematricula
                        if(b[0].ToString() != "")
                        {
                            //Data rematricula
                            DateTime ultima_rematricula = DateTime.Parse(b[0]);

                            TimeSpan tsdife_2 = hoje - ultima_rematricula;

                            int diferenca_dias_rematricula = tsdife_2.Days;

                            if (ultima_rematricula.Month < 6)
                            {
                                if (diferenca_dias_rematricula > 5)
                                {
                                    ///ALUNO COM REMATRICULA ATRASADA E SEM PRESENCA
                                    //Adcionar na tabela para inativar
                                    table_2.Rows.Add();
                                    table_2.Rows[z][0] = table.Rows[i][0];
                                    z++;
                                }      
                            }

                                                  
                        }
                        else
                        {
                            ///ALUNO SEM NEMHUMA REMATRICULA E SEM PRESENÇA
                            //Adcionar na tabela para inativar
                            table_2.Rows.Add();
                            table_2.Rows[z][0] = table.Rows[i][0];
                            z++;
                        }
                    }                   
                }
                else
                {
                    DateTime dat_mat_ = cs_alunos.dat_mat__(table.Rows[i][0].ToString());

                    TimeSpan diferenca_mat = hoje - dat_mat_;
                    int diferenca_mat_ = diferenca_mat.Days;

                    //teste de data de matricula
                    if (diferenca_mat_ > 30)
                    {
                        ///ALUNO SEM NENHUMA PRESENÇA
                        //Adcionar na tabela para inativar
                        table_2.Rows.Add();
                        table_2.Rows[z][0] = table.Rows[i][0];
                        z++;
                    }                   
                }
            }


            //inativar itens da table_2;
            for (int i = 0; i < table_2.Rows.Count; i++ )
            {
                if(table_2.Rows[i][0].ToString() != "")
                {  
                    cs_alunos.ativa_inativa_aluno(table_2.Rows[i][0].ToString(), 0, 0);

                    cs_historicos_situacoes.add_hist_situacao(1, DateTime.Now, Usuarios_Grupos.csUsuario_logado.id_usuario_logado, "MAIS DE 30 DIAS", table_2.Rows[i][0].ToString());

                    cs_alunos.add_mod_lista_ativa_inativa(table_2.Rows[i][0].ToString(), 0, 0, "MAIS DE 30 DIAS");
                }
            }
            sw.Stop();

            //Fim diagnostico
            TimeSpan tm = sw.Elapsed;
            string a = tm.ToString();
            //cs_erros.mostra_msgbox_erro(a);
        }

        public string data_ultimo_atendimento(int n_mat)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT DATA_ATENDIMENTO FROM ATENDIMENTOS WHERE N_MAT=@n_mat ORDER BY DATA_ATENDIMENTO DESC";

            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
   

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["DATA_ATENDIMENTO"].ToString();
                    break;
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

        public string data_ultimo_atendimento_WHERE(string where)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT DATA_ATENDIMENTO FROM ATENDIMENTOS WHERE N_MAT IN ("+ where +")";
            
            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["DATA_ATENDIMENTO"].ToString();
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

        public DataTable tab_ultimas_presencas()
        {
            sql_comm.Connection = sql_conn;
            //sql_comm.CommandText = @"SELECT N_MAT FROM ALUNOS WHERE ATIVO=1";

            sql_comm.CommandText = @"SELECT ALUNOS.N_MAT, MAX(DATA_ATENDIMENTO) as ULTIMA_PRESENCA
                                     FROM ALUNOS LEFT JOIN ATENDIMENTOS
                                     ON ALUNOS.N_MAT = ATENDIMENTOS.N_MAT
                                     WHERE ATIVO = 1
                                     GROUP BY ALUNOS.N_MAT
                                     ORDER BY N_MAT";


            DataTable table = new DataTable();

            table.Columns.Add("n_mat");
            table.Columns.Add("ultima_presenca");

            int i =0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add();
                    table.Rows[i][0] = reader["N_MAT"].ToString();
                    table.Rows[i][1] = reader["ULTIMA_PRESENCA"].ToString();
                    i++;
                }

                sql_conn.Close();

                

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

        //Aprender a usar array 2d
        //public Array lista_array()
        //{
        //    string[][] a;

        //    string b = "teste1|teste2";

        //    Array c = b.Split('|');



        //    return a;
        //}

        #endregion

    }
}
