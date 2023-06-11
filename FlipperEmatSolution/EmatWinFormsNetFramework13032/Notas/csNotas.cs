using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Novos Using
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace EmatWinFormsNetFramework13032.Notas
{
   public class csNotas
   {
       SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
       SqlCommand sql_comm = new SqlCommand();

       Error_Log.csControle_erros cs_erros = new Error_Log.csControle_erros();
       csDisciplinas cs_disciplinas = new csDisciplinas();

       //HISTÓRICO
       public int id_historico { get; set; }
       public string n_mat { get; set; }
       public int id_ensino { get; set; }
       public string obs_1 { get; set; }
       public string diretor { get; set; }
       public string rg_diretor { get; set; }
       public string secretario { get; set; }
       public string rg_secretario { get; set; }
       public DateTime dat_livro { get; set; }
       public string livro { get; set; }
       public string pagina { get; set; }
       public string termo { get; set; }
       public DateTime dat_doc { get; set; }
       public int ano_con { get; set; }
       public DateTime dat_con_ { get; set; }
       public string serie_ant { get; set; }
       public string ins_ant { get; set; }
       public int ano_ant { get; set; }
       public string mu_ant { get; set; }
       public string uf_ant { get; set; }

       //MÉDIAS
       public int id_media { get; set; }
       public int id_atendimento { get; set; }
       public int id_disciplina { get; set; }      
       //public string n_mat { get; set; }
       public int ensino_media { get; set; }
       public string instituicao { get; set; }
       public string municipio { get; set; }
       public string uf { get; set; }
       public string media { get; set; }
       public string dat_ini { get; set; }
       public string dat_fin { get; set; }
       public int usu_lanc_media { get; set; }
       public int usu_mod_media { get; set; }
       public int dat_mod_media { get; set; }

       public class nota_atendimento
       {
           //public int id_nota { get; set; }
           public int nota {get;set;}
           public string tipo_atendimento{get;set;}
       }

       public List<csNotas> lista_notas_concluidas(string n_mat, int id_ensino)
       {
           List<csNotas> list_ = new List<csNotas>();

           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"SELECT ID_DISCIPLINA, MEDIA, DAT_FIN FROM MEDIAS WHERE N_MAT=@n_mat AND ENSINO=@id_ensino";

           sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
           sql_comm.Parameters.AddWithValue("@id_ensino", id_ensino);

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   csNotas cs_notas_ = new csNotas();

                   cs_notas_.id_disciplina = Convert.ToInt32(reader["ID_DISCIPLINA"]);

                   if (reader["DAT_FIN"] != DBNull.Value)
                       cs_notas_.dat_fin = DateTime.Parse(reader["DAT_FIN"].ToString()).ToString("dd/MM/yyyy");
                   else                 
                       cs_notas_.dat_fin = "";
                   
                   cs_notas_.media = reader["MEDIA"].ToString();

                   list_.Add(cs_notas_);
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

       
       
       //HISTORICO

       public List<csNotas> dados_historico(string n_mat_, int id_ensino_)
       {
           List<csNotas> a = new List<csNotas>();

           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"SELECT * FROM HISTORICOS WHERE N_MAT=@n_mat_ AND ENSINO=@id_ensino_";

           sql_comm.Parameters.AddWithValue(@"n_mat_", n_mat_);
           sql_comm.Parameters.AddWithValue(@"id_ensino_", id_ensino_);

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   csNotas cs_hist_ = new csNotas();
                   if (reader["ID_HISTORICO"] != DBNull.Value) cs_hist_.id_historico = Convert.ToInt32(reader["ID_HISTORICO"].ToString());
                   cs_hist_.n_mat = reader["N_MAT"].ToString();
                   if (reader["ENSINO"] != DBNull.Value) cs_hist_.id_ensino = Convert.ToInt32(reader["ENSINO"].ToString());
                   cs_hist_.obs_1 = reader["OBS_1"].ToString();
                   cs_hist_.diretor = reader["DIRETOR"].ToString();
                   cs_hist_.rg_diretor = reader["RG_DIRETOR"].ToString();
                   cs_hist_.secretario = reader["SECRETARIO"].ToString();
                   cs_hist_.rg_secretario = reader["RG_SECRETARIO"].ToString();
                   if (reader["DAT_LIVRO"] != DBNull.Value) cs_hist_.dat_livro = DateTime.Parse(reader["DAT_LIVRO"].ToString());
                   cs_hist_.livro = reader["LIVRO"].ToString();
                   cs_hist_.pagina = reader["PAGINA"].ToString();
                   cs_hist_.termo = reader["TERMO"].ToString();
                   if (reader["DAT_DOC"] != DBNull.Value) cs_hist_.dat_doc = DateTime.Parse(reader["DAT_DOC"].ToString());
                   if (reader["ANO_CON"] != DBNull.Value) cs_hist_.ano_con = Convert.ToInt32(reader["ANO_CON"].ToString());
                   if (reader["DAT_CON"] != DBNull.Value) cs_hist_.dat_con_ = DateTime.Parse(reader["DAT_CON"].ToString());
                   cs_hist_.serie_ant = reader["SERIE_ANT"].ToString();
                   cs_hist_.ins_ant = reader["INS_ANT"].ToString();
                   if (reader["ANO_ANT"] != DBNull.Value) cs_hist_.ano_ant = Convert.ToInt32(reader["ANO_ANT"].ToString());
                   cs_hist_.mu_ant = reader["MU_ANT"].ToString();
                   cs_hist_.uf_ant = reader["UF_ANT"].ToString();

                   a.Add(cs_hist_);

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

           return a;
       }

       //NOTAS
       public float nota(int id_atendimento)
       {
           float a = 0;
           bool b = false;

           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"SELECT NOTA FROM NOTAS WHERE ID_ATENDIMENTO=@id_atendimento";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);          

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   a = float.Parse(reader["NOTA"].ToString());
                   b = true;
               }

               if(b==false)
               {
                   a = 50;
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

           return a;
       }

       public int id_usu_lanc(int id_atendimento)
       {
           int a = 0;

           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"SELECT ID_USU_LANC FROM NOTAS WHERE ID_ATENDIMENTO=@id_atendimento";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   a = (int)reader["ID_USU_LANC"];
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

           return a;
       }

       public string dat_lanc_nota(int id_atendimento)
       {
           string a = "";

           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"SELECT DAT_LANC_NOTA FROM NOTAS WHERE ID_ATENDIMENTO=@id_atendimento";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   a = reader["DAT_LANC_NOTA"].ToString();
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

           return a;
       }

       public int id_usu_mod(int id_atendimento)
       {
           int a = 0;

           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"SELECT ID_USU_MOD FROM NOTAS WHERE ID_ATENDIMENTO=@id_atendimento";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   if (reader["ID_USU_MOD"] != DBNull.Value)
                   {
                       a = Convert.ToInt32(reader["ID_USU_MOD"]);
                   }
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

           return a;
       }

       public string dat_mod_nota(int id_atendimento)
       {
           string a = "";

           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"SELECT DAT_MOD_NOTA FROM NOTAS WHERE ID_ATENDIMENTO=@id_atendimento";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   a = reader["DAT_MOD_NOTA"].ToString();
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

           return a;
       }

       public void adcionaNota(int id_atendimento, float nota, int id_usu_lanc, string dat_lanc_nota) //verificar nota-FLOAT !!
       {
           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"INSERT INTO NOTAS (ID_ATENDIMENTO, NOTA, ID_USU_LANC, DAT_LANC_NOTA ) 
                                               VALUES (@id_atendimento, @nota, @id_usu_lanc, @dat_lanc_nota )";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);          
           sql_comm.Parameters.AddWithValue("@nota", nota);           
           sql_comm.Parameters.AddWithValue("@id_usu_lanc", id_usu_lanc);
           sql_comm.Parameters.AddWithValue("@dat_lanc_nota", dat_lanc_nota);

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

       public void excluiNota(int id_atendimento)
       {
           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"DELETE FROM NOTAS WHERE ID_ATENDIMENTO=@id_atendimento";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);

           try
           {
               sql_conn.Open();
               sql_comm.ExecuteNonQuery();
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

       }

       public void modificaNota(int id_atendimento, float nota, int id_usu_mod, string dat_mod_nota)
       {
           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"UPDATE NOTAS SET NOTA=@nota, ID_USU_MOD=@id_usu_mod, DAT_MOD_NOTA=@dat_mod_nota WHERE ID_ATENDIMENTO=@id_atendimento";

           
           sql_comm.Parameters.AddWithValue("@nota", nota);
           sql_comm.Parameters.AddWithValue("@id_usu_mod", id_usu_mod);
           sql_comm.Parameters.AddWithValue("@dat_mod_nota", dat_mod_nota);
           //where
           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);

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

       public bool existe_nota(int id_atendimento)
       {
           bool a = false;

           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"SELECT ID_ATENDIMENTO FROM NOTAS WHERE ID_ATENDIMENTO=@id_atendimento";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);

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

           catch (Exception ex)
           {
               
               Error_Log.csControle_erros.exibir_erro(ex.Message);
           }

           finally
           {
               sql_comm.Parameters.Clear();
               sql_conn.Close();
           }

           return a;
       }

       public bool existe_media(int id_atendimento)
       {
           bool a = false;

           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"SELECT ID_ATENDIMENTO FROM MEDIAS WHERE ID_ATENDIMENTO=@id_atendimento";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);

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

           catch (Exception ex)
           {

               Error_Log.csControle_erros.exibir_erro(ex.Message);
           }

           finally
           {
               sql_comm.Parameters.Clear();
               sql_conn.Close();
           }

           return a;
       }
       
       //MEDIAS - NOVO MODELO DE BANCO       
       
       public void add_media_final_NOVO(int id_atendimento, int id_disciplina, string nmat, int ensino,string media, string dat_ini, int user_lanc_media)
       {
           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"INSERT INTO MEDIAS (ID_ATENDIMENTO,
                                                        ID_DISCIPLINA,
                                                        N_MAT,
                                                        ENSINO,
                                                        INSTITUICAO,
                                                        MUNICIPIO,
                                                        UF,
                                                        MEDIA,
                                                        DAT_INI,
                                                        DAT_FIN,
                                                        USU_LANC_MEDIA) 
                                                VALUES (@id_atendimento,
                                                        @id_disciplina,
                                                        @n_mat,
                                                        @ensino,
                                                        @instituicao,
                                                        @municipio,
                                                        @uf,
                                                        @media,
                                                        @dat_ini,
                                                        @dat_fin,
                                                        @usu_lanc_media)";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);
           sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
           sql_comm.Parameters.AddWithValue("@n_mat", nmat);
           sql_comm.Parameters.AddWithValue("@ensino", ensino);
           sql_comm.Parameters.AddWithValue("@instituicao", Configurações.csEscola.escola.ToUpper());
           sql_comm.Parameters.AddWithValue("@municipio", Configurações.csEscola.cidade.ToUpper());
           sql_comm.Parameters.AddWithValue("@uf", Configurações.csEscola.sigla_estado.ToUpper());
           sql_comm.Parameters.AddWithValue("@media", media);
           sql_comm.Parameters.AddWithValue("@dat_ini", dat_ini);
           sql_comm.Parameters.AddWithValue("@dat_fin", DateTime.Now.ToString("dd/MM/yyyy"));
           sql_comm.Parameters.AddWithValue("@usu_lanc_media", user_lanc_media);
 
           try
           {
               if (sql_comm.Connection.State != ConnectionState.Closed)
               {
                   sql_comm.Connection.Close();
               }
               sql_comm.Connection.Open();
               sql_comm.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               
               Error_Log.csControle_erros.exibir_erro(ex.Message);
           }
           finally
           {
               sql_comm.Parameters.Clear();
               sql_comm.Connection.Close();
           }
       }

       public void add_media_final_pelo_hist(int id_disciplina, string nmat, int ensino, string ins, string mu, string uf, string media, string dat_fin, int user_lanc_media)
       {
           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"INSERT INTO MEDIAS (ID_DISCIPLINA,
                                                        N_MAT,
                                                        ENSINO,
                                                        INSTITUICAO,
                                                        MUNICIPIO,
                                                        UF,
                                                        MEDIA,                                                        
                                                        DAT_FIN,
                                                        USU_LANC_MEDIA) 
                                                VALUES (@id_disciplina,
                                                        @n_mat,
                                                        @ensino,
                                                        @instituicao,
                                                        @municipio,
                                                        @uf,
                                                        @media,                                                        
                                                        @dat_fin,
                                                        @usu_lanc_media)";

           sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
           sql_comm.Parameters.AddWithValue("@n_mat", nmat);
           sql_comm.Parameters.AddWithValue("@ensino", ensino);
           sql_comm.Parameters.AddWithValue("@instituicao", ins);
           sql_comm.Parameters.AddWithValue("@municipio", mu);
           sql_comm.Parameters.AddWithValue("@uf", uf);
           sql_comm.Parameters.AddWithValue("@media", media);
           sql_comm.Parameters.AddWithValue("@dat_fin", dat_fin);
           sql_comm.Parameters.AddWithValue("@usu_lanc_media", user_lanc_media);

           try
           {
               if (sql_comm.Connection.State != ConnectionState.Closed)
               {
                   sql_comm.Connection.Close();
               }
               sql_comm.Connection.Open();
               sql_comm.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               
               Error_Log.csControle_erros.exibir_erro(ex.Message);
           }
           finally
           {
               sql_comm.Parameters.Clear();
               sql_comm.Connection.Close();
           }
       }
       
       //UPDATE
       public void upd_media_final(string instituicao, string municipio, string uf, string media, string dat_fin, int id_user_mod, int id_atendimento = 0, string n_mat_="", int id_disc_=0, int id_ensino_=0)
       {
           sql_comm.Connection = sql_conn;

           if (id_atendimento != 0)
           {
               sql_comm.CommandText = @"UPDATE MEDIAS SET   INSTITUICAO=@instituicao, 
                                                        MUNICIPIO=@municipio, 
                                                        UF=@uf, 
                                                        MEDIA=@media,                                                        
                                                        DAT_FIN=@dat_fin,
                                                        USU_MOD_MEDIA=@id_user_mod,
                                                        DAT_MOD_MEDIA=@dat_mod_media 
                                                        WHERE ID_ATENDIMENTO=@id_atendimento";
               sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);
           }
           else
           {
               sql_comm.CommandText = @"UPDATE MEDIAS SET   INSTITUICAO=@instituicao, 
                                                        MUNICIPIO=@municipio, 
                                                        UF=@uf, 
                                                        MEDIA=@media,                                                        
                                                        DAT_FIN=@dat_fin,
                                                        USU_MOD_MEDIA=@id_user_mod,
                                                        DAT_MOD_MEDIA=@dat_mod_media 
                                                        WHERE N_MAT=@n_mat_ AND ENSINO=@id_ensino_ AND ID_DISCIPLINA=@id_disc_";

               sql_comm.Parameters.AddWithValue("@n_mat_", n_mat_);
               sql_comm.Parameters.AddWithValue("@id_ensino_", id_ensino_);
               sql_comm.Parameters.AddWithValue("@id_disc_", id_disc_);
           }
           

           

           sql_comm.Parameters.AddWithValue("@instituicao", instituicao);
           sql_comm.Parameters.AddWithValue("@municipio", municipio);
           sql_comm.Parameters.AddWithValue("@uf", uf);           
           sql_comm.Parameters.AddWithValue("@media", media);
           sql_comm.Parameters.AddWithValue("@dat_fin", dat_fin);
           sql_comm.Parameters.AddWithValue("@id_user_mod", id_user_mod);
           sql_comm.Parameters.AddWithValue("@dat_mod_media", DateTime.Now.ToString());

           try
           {
               if (sql_comm.Connection.State != ConnectionState.Closed)
               {
                   sql_comm.Connection.Close();
               }
               sql_comm.Connection.Open();
               sql_comm.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               
               Error_Log.csControle_erros.exibir_erro(ex.Message);
           }
           finally
           {
               sql_comm.Parameters.Clear();
               sql_comm.Connection.Close();
           }
       }

       public void upd_historico(string obs,string diretor,string rg_diretor, string secretario, string rg_secretario,
           string dat_livro, string livro, string pagina, string termo, string dat_doc, string serie_ant, string ins_ant,
           string ano_ant,string mu_ant,string uf_ant,string n_mat,int ensino)
       {
           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"UPDATE HISTORICOS SET OBS_1=@obs,
                                                          DIRETOR=@diretor,
                                                          RG_DIRETOR=@rg_diretor,
                                                          SECRETARIO=@secretario,
                                                          RG_SECRETARIO=@rg_secretario,
                                                          DAT_LIVRO=@dat_livro,
                                                          LIVRO=@livro,
                                                          PAGINA=@pagina,
                                                          TERMO=@termo,
                                                          DAT_DOC=@dat_doc,                                                   
                                                          SERIE_ANT=@serie_ant,
                                                          INS_ANT=@ins_ant,
                                                          ANO_ANT=@ano_ant,
                                                          MU_ANT=@mu_ant,
                                                          UF_ANT=@uf_ant
                                                          WHERE N_MAT=@n_mat AND ENSINO=@ensino";

           sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
           sql_comm.Parameters.AddWithValue("@ensino", ensino);
         

           if (obs != "") sql_comm.Parameters.AddWithValue("@obs", obs);
           else sql_comm.Parameters.AddWithValue("obs", DBNull.Value);

           if (diretor != "") sql_comm.Parameters.AddWithValue("@diretor", diretor);
           else sql_comm.Parameters.AddWithValue("@diretor", DBNull.Value);

           if (rg_diretor != "") sql_comm.Parameters.AddWithValue("@rg_diretor", rg_diretor);
           else sql_comm.Parameters.AddWithValue("@rg_diretor", DBNull.Value);

           if (secretario != "") sql_comm.Parameters.AddWithValue("@secretario", secretario);
           else sql_comm.Parameters.AddWithValue("@secretario", DBNull.Value);

           if (rg_secretario != "") sql_comm.Parameters.AddWithValue("@rg_secretario", rg_secretario);
           else sql_comm.Parameters.AddWithValue("@rg_secretario", DBNull.Value);

           if (dat_livro != "") sql_comm.Parameters.AddWithValue("@dat_livro", dat_livro);
           else sql_comm.Parameters.AddWithValue("@dat_livro", DBNull.Value);

           if (livro != "") sql_comm.Parameters.AddWithValue("@livro", livro);
           else sql_comm.Parameters.AddWithValue("@livro", DBNull.Value);

           if (pagina != "") sql_comm.Parameters.AddWithValue("@pagina", pagina);
           else sql_comm.Parameters.AddWithValue("@pagina", DBNull.Value);

           if (termo != "") sql_comm.Parameters.AddWithValue("@termo", termo);
           else sql_comm.Parameters.AddWithValue("@termo", DBNull.Value);

           if (dat_doc != "") sql_comm.Parameters.AddWithValue("@dat_doc", dat_doc);
           else sql_comm.Parameters.AddWithValue("@dat_doc", DBNull.Value);           

           if (serie_ant != "") sql_comm.Parameters.AddWithValue("@serie_ant", serie_ant);
           else sql_comm.Parameters.AddWithValue("@serie_ant", DBNull.Value);

           if (ins_ant != "") sql_comm.Parameters.AddWithValue("@ins_ant", ins_ant);
           else sql_comm.Parameters.AddWithValue("@ins_ant", DBNull.Value);

           if (ano_ant != "") sql_comm.Parameters.AddWithValue("@ano_ant", ano_ant);
           else sql_comm.Parameters.AddWithValue("@ano_ant", DBNull.Value);

           if (mu_ant != "") sql_comm.Parameters.AddWithValue("@mu_ant", mu_ant);
           else sql_comm.Parameters.AddWithValue("@mu_ant", DBNull.Value);

           if (uf_ant != "") sql_comm.Parameters.AddWithValue("@uf_ant", uf_ant);
           else sql_comm.Parameters.AddWithValue("@uf_ant", DBNull.Value);     

           try
           {
               if (sql_comm.Connection.State != ConnectionState.Closed)
               {
                   sql_comm.Connection.Close();
               }
               sql_comm.Connection.Open();
               sql_comm.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               
               Error_Log.csControle_erros.exibir_erro(ex.Message);
           }
           finally
           {
               sql_comm.Parameters.Clear();
               sql_comm.Connection.Close();
           }
       }

       public void updt_historico_conclusao(string dat_con, string ano_con, string n_mat, int ensino)
       {
           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"UPDATE HISTORICOS SET DAT_CON=@dat_con,                                                        
                                                          ANO_CON=@ano_con                                                          
                                                          WHERE N_MAT=@n_mat AND ENSINO=@ensino";

           sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
           sql_comm.Parameters.AddWithValue("@ensino", ensino);

           if (ano_con != "") sql_comm.Parameters.AddWithValue("@ano_con", ano_con);
           else sql_comm.Parameters.AddWithValue("@ano_con", DBNull.Value);

           if (dat_con != "") sql_comm.Parameters.AddWithValue("@dat_con", dat_con);
           else sql_comm.Parameters.AddWithValue("@dat_con", DBNull.Value);

           try
           {
               if (sql_comm.Connection.State != ConnectionState.Closed)
               {
                   sql_comm.Connection.Close();
               }
               sql_comm.Connection.Open();
               sql_comm.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               
               Error_Log.csControle_erros.exibir_erro(ex.Message);
           }
           finally
           {
               sql_comm.Parameters.Clear();
               sql_comm.Connection.Close();
           }

           
       }

       //SELECT DAT_CON
       public string dat_con(string n_mat, int ensino)
       {
           string a = "";

           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"SELECT DAT_CON FROM HISTORICO WHERE N_MAT=@nmat AND ENSINO=@ensino";

           sql_comm.Parameters.AddWithValue("@nmat", n_mat);
           sql_comm.Parameters.AddWithValue("@ensino", ensino);

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   a = reader["DAT_CON"].ToString();
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


           return a;
       }

       //SELECT MÉDIA
       public float media_final_nmat(string nmat, int id_disciplina, int ensino)
       {
           float a = 0;

           sql_comm.Connection = sql_conn;
           
           sql_comm.CommandText = @"SELECT MEDIA FROM MEDIAS WHERE N_MAT=@nmat AND ID_DISCIPLINA=@id_disciplina and ENSINO=@ensino";
           
           sql_comm.Parameters.AddWithValue("@nmat", nmat);
           sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
           sql_comm.Parameters.AddWithValue("@ensino", ensino);

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   a = float.Parse(reader["MEDIA"].ToString());
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

           return a;

       }

       public float media_final_idatend(int id_atendimento)
       {
           float a = 0;

           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"SELECT MEDIA FROM MEDIAS WHERE ID_ATENDIMENTO=@id_atendimento";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);
           //sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
           //sql_comm.Parameters.AddWithValue("@ensino", ensino);

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   a = float.Parse(reader["MEDIA"].ToString());
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

           return a;

       }

       //SELECT MÉDIA_HISTORICO
       public List<string> media_final_historico(string nmat, int id_disciplina, int id_ensino)
       {
           List<string> a = new List<string>();

           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"SELECT * FROM MEDIAS WHERE N_MAT=@nmat AND ID_DISCIPLINA=@id_disciplina AND ENSINO=@id_ensino";

           sql_comm.Parameters.AddWithValue(@"nmat", nmat);
           sql_comm.Parameters.AddWithValue(@"id_disciplina", id_disciplina);
           sql_comm.Parameters.AddWithValue(@"id_ensino", id_ensino);

           

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while(reader.Read())
               {                   
                   a.Add(reader["ID_MEDIA"].ToString());         //0
                   a.Add(reader["INSTITUICAO"].ToString());      //1
                   a.Add(reader["MUNICIPIO"].ToString());        //2
                   a.Add(reader["UF"].ToString());               //3
                   a.Add(reader["MEDIA"].ToString());            //4
                   a.Add(reader["DAT_FIN"].ToString());     //5
                                                    
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

           return a;
       }

       //SELECT MEDIAS - PARA HISTORICO
       public List<string> list_dados_nota(string nmat, int id_ensino, int id_disciplina)
       {
           List<string> a = new List<string>();

           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"SELECT * FROM MEDIAS WHERE N_MAT=@nmat AND ID_DISCIPLINA=@id_disciplina AND ENSINO=@id_ensino";

           sql_comm.Parameters.AddWithValue(@"nmat", nmat);
           sql_comm.Parameters.AddWithValue(@"id_disciplina", id_disciplina);
           sql_comm.Parameters.AddWithValue(@"id_ensino", id_ensino);

           try
           {               
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   // ___ 0 ___
                   if (reader["DAT_INI"] != DBNull.Value) a.Add(DateTime.Parse(reader["DAT_INI"].ToString()).ToString("dd/MM/yyyy")); 
                   else a.Add("");
                   // ___ 1 ___
                   a.Add(reader["MEDIA"].ToString());
                   // ___ 2 ___
                   if (reader["DAT_FIN"] != DBNull.Value) a.Add(DateTime.Parse(reader["DAT_FIN"].ToString()).ToString("dd/MM/yyyy")); 
                   else a.Add("");
                   // ___ 3 ___
                   if (reader["USU_MOD_MEDIA"] == DBNull.Value)
                   {
                       a.Add(reader["USU_LANC_MEDIA"].ToString()); 
                   }
                   else
                   {
                       a.Add(reader["USU_MOD_MEDIA"].ToString()); 
                   }
                   // ___ 4 ___
                   if (reader["DAT_MOD_MEDIA"] != DBNull.Value)
                   {
                       a.Add(DateTime.Parse(reader["DAT_MOD_MEDIA"].ToString()).ToString("dd/MM/yyyy"));  //4
                   }
                   else
                   {
                       a.Add("");
                   }

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

           return a;
       }

       //SELECT DAT_INI
       public string orient_ini_por_nmat(string nmat, int id_disciplina, int ensino_)
       {
           string a = "";
           
           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"SELECT DAT_INI FROM MEDIAS WHERE N_MAT=@nmat AND ID_DISCIPLINA=@id_disciplina AND ENSINO=@ensino";

           sql_comm.Parameters.AddWithValue("@nmat", nmat);
           sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
           sql_comm.Parameters.AddWithValue("@ensino", ensino_);

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   a = reader["DAT_INI"].ToString();
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

           return a;
       }

       public string busca_dat_ini_idatend(int id_atendimento, int id_disciplina, int ensino_)
       {
           string a = "";

           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"SELECT DAT_INI FROM MEDIAS WHERE ID_ATENDIMENTO=@id_atendimento AND ID_DISCIPLINA=@id_disciplina AND ENSINO=@ensino";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);
           sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
           sql_comm.Parameters.AddWithValue("@ensino", ensino_);

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   a = reader["DAT_INI"].ToString();
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

           return a;
       }

       public string dat_ini_(string n_mat, int id_tipo_atendimento, int id_ensino)
       {
           string a = "";

           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"SELECT N_MAT, ID_TIPO_ATENDIMENTO, DATA_ATENDIMENTO 
                                    FROM ATENDIMENTOS
                                    WHERE ID_TIPO_ATENDIMENTO=@id_tipo_atendimento AND N_MAT=@n_mat AND ENSINO=@ensino ";

           sql_comm.Parameters.AddWithValue("@id_tipo_atendimento", id_tipo_atendimento);
           sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
           sql_comm.Parameters.AddWithValue("@ensino", id_ensino);

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   a = reader["DATA_ATENDIMENTO"].ToString();
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


           return a;
       }

       //ATUALIZA DATA ORIENTAÇÃO INICIAL
       public void atualiza_dat_ini_nmat(string nmat, int id_disciplina, int ensino_, string dat_ini)
       {
           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"UPDATE MEDIAS SET DAT_INI=@dat_ini WHERE N_MAT=@n_mat AND ID_DISCIPLINA=@id_disciplina AND ENSINO=@ensino";

           sql_comm.Parameters.AddWithValue("@dat_ini", dat_ini);

           sql_comm.Parameters.AddWithValue("@n_mat", nmat);
           sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
           sql_comm.Parameters.AddWithValue("@ensino", ensino_);

           try
           {
               if (sql_comm.Connection.State != ConnectionState.Closed)
               {
                   sql_comm.Connection.Close();
               }
               sql_comm.Connection.Open();
               sql_comm.ExecuteNonQuery();
           }
           catch (Exception ex)
           {

               Error_Log.csControle_erros.exibir_erro(ex.Message);
           }
           finally
           {
               sql_comm.Parameters.Clear();
               sql_comm.Connection.Close();
           }
       }

       public void atualiza_dat_ini_idatend(int id_atendimento, int id_disciplina, int ensino_, string dat_ini)
       {
           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"UPDATE MEDIAS SET DAT_INI=@dat_ini WHERE ID_ATENDIMENTO=@id_atendimento AND ID_DISCIPLINA=@id_disciplina AND ENSINO=@ensino";

           sql_comm.Parameters.AddWithValue("@dat_ini", dat_ini);

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);
           sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
           sql_comm.Parameters.AddWithValue("@ensino", ensino_);

           try
           {
               if (sql_comm.Connection.State != ConnectionState.Closed)
               {
                   sql_comm.Connection.Close();
               }
               sql_comm.Connection.Open();
               sql_comm.ExecuteNonQuery();
           }
           catch (Exception ex)
           {

               Error_Log.csControle_erros.exibir_erro(ex.Message);
           }
           finally
           {
               sql_comm.Parameters.Clear();
               sql_comm.Connection.Close();
           }
       }

       //HÁ MÉDIA ?
       public bool ha_media_final_nmat(string nmat, int id_disciplina, int ensino_)
       {
           bool a = false;

           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"SELECT MEDIA FROM MEDIAS WHERE N_MAT=@nmat AND ID_DISCIPLINA=@id_disciplina and ENSINO=@ensino";

           sql_comm.Parameters.AddWithValue("@nmat", nmat);
           sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
           sql_comm.Parameters.AddWithValue("@ensino", ensino_);
           
           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   if (reader["MEDIA"] != DBNull.Value)
                   {
                       a = true;
                   }
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

           return a;

       }

       public bool ha_media_final_idatend(int id_atendimento, int id_disciplina, int ensino)
       {
           bool a = false;

           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"SELECT MEDIA FROM MEDIAS WHERE ID_ATENDIMENTO=@id_atendimento AND ID_DISCIPLINA=@id_disciplina and ENSINO=@ensino";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);
           sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
           sql_comm.Parameters.AddWithValue("@ensino", ensino);

           try
           {
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   if (reader["MEDIA"] != DBNull.Value)
                   {
                       a = true;
                   }
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

           return a;

       }       

       //EXCLUIR MÉDIA
       public void excluir_media_id_media(int id_media)
       {
           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"DELETE MEDIAS WHERE ID_MEDIA=@id_media";

           sql_comm.Parameters.AddWithValue("@id_media", id_media);

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

       public void excluir_media_id_atendimento(int id_atendimento)
       {
           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"DELETE MEDIAS WHERE ID_ATENDIMENTO=@id_atendimento";

           sql_comm.Parameters.AddWithValue("@id_atendimento", id_atendimento);

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

       public void excluir_media_por_id_ensino_id_disciplina(string n_mat, int id_ensino, int id_disciplina)
       {
           sql_comm.Connection = sql_conn;
           sql_comm.CommandText = @"DELETE MEDIAS WHERE N_MAT=@n_mat and ENSINO=@id_ensino AND ID_DISCIPLINA=@id_disciplina";

           sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
           sql_comm.Parameters.AddWithValue("@id_ensino", id_ensino);
           sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);

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

       //MÉDIA AUTOMÁTICA
       public string calc_media_auto(int id_disciplina, int id_ensino)
       {
           //Puxar a formula de média da disciplina
           //string formula = cs_disciplinas.sel_formula_media(id_disciplina,id_ensino);
           ///verificar se os atendimentos que estão na disciplina formam a média automatica
           ///

           string formula = "(AVALIAÇÃO+AVALIAÇÃO)/2=M";

           csExpressaoMatematica cs_expre_math = new csExpressaoMatematica();
           double db = cs_expre_math.calcular(formula);

           //Quebrar a formula 
           string formula_split = formula.Replace('+', '|');
           formula_split = formula.Replace('-', '|');
           formula_split = formula.Replace('*', '|');
           formula_split = formula.Replace('/', '|');
           formula_split = formula.Replace('(', '|');
           formula_split = formula.Replace(')', '|');
           formula_split = formula.Replace("=M", "|");

           //, List<nota_atendimento> notas_atendimentos
           List<string> list_tipos_atendimentos_na_formula = formula_split.Split('|').ToList();

           List<nota_atendimento> list_notas_no_passaporte = new List<nota_atendimento>();

           nota_atendimento notas_ = new nota_atendimento();
           notas_.nota = 10;
           notas_.tipo_atendimento = "AVALIAÇÃO";
           list_notas_no_passaporte.Add(notas_);
           nota_atendimento notas_1 = new nota_atendimento();
           notas_1.nota = 10;
           notas_1.tipo_atendimento = "TRABALHO";
           list_notas_no_passaporte.Add(notas_1);
           nota_atendimento notas_2 = new nota_atendimento();
           notas_2.nota = 8;
           notas_2.tipo_atendimento = "AVALIAÇÃO";
           list_notas_no_passaporte.Add(notas_2);

           for (int i = 0; i < list_notas_no_passaporte.Count; i++)
           {
               var regex = new Regex(Regex.Escape(list_notas_no_passaporte[i].tipo_atendimento));
               formula = regex.Replace(formula, list_notas_no_passaporte[i].nota.ToString(), 1);
           }

           

           //calcular(formula.Replace("=M", "|"));

               //efetuar o calculo da média

               //arrendondar

               return formula;
       }
       
       //MANUTENÇÃO
       
       public void convert_hist_to_medias_tabs(int id_ensino)
       {
           //TODO: Verificar novamente
           #region Disciplinas - Ids
           //"PORT. e LEIT. e PROD. TEXTOS = ORIENT_INI_1
           //"HISTÓRIA = ORIENT_INI_2
           //"GEOGRAFIA = ORIENT_INI_3
           //"FÍSICA = ORIENT_INI_4
           //"QUÍMICA = ORIENT_INI_5
           //"BIOLOGIA = ORIENT_INI_6
           //"MATEMÁTICA = ORIENT_INI_7
           //"INGLÊS = ORIENT_INI_8
           //"ARTE = ORIENT_INI_9
           //"FILOSOFIA = ORIENT_INI_10
           //"SOCIOLOGIA = ORIENT_INI_11

           //FUNDAMENTAL

           //LÍNGUA PORTUGUESA E LIT. = ORIENT_INI_1
           //HISTÓRIA = ORIENT_INI_2
           //GEOGRAFIA = ORIENT_INI_3
           //CIÊNCIAS = ORIENT_INI_4
           //MATEMÁTICA = ORIENT_INI_5
           //INGLÊS = ORIENT_INI_6
           //ARTE = ORIENT_INI_7           
 
           //1	PORTUGUÊS
           //2	MATEMÁTICA
           //3	QUÍMICA
           //4	HISTÓRIA
           //5	FÍSICA
           //6	BIOLOGIA
           //7	INGLÊS
           //8	ARTE
           //9	FILOSOFIA
           //10	SOCIOLOGIA
           //11	CIÊNCIAS
           //12	GEOGRAFIA
           #endregion

           #region Tabela

           DataTable dt = new DataTable();
           dt.Columns.Add("ID_MEDIA");
           dt.Columns.Add("ID_ATENDIMENTO");
           dt.Columns.Add("ID_DISCIPLINA");
           dt.Columns.Add("N_MAT");
           dt.Columns.Add("ENSINO");
           dt.Columns.Add("INSTITUICAO");
           dt.Columns.Add("MUNICIPIO");
           dt.Columns.Add("UF");
           dt.Columns.Add("MEDIA");
           dt.Columns.Add("DAT_INI");
           dt.Columns.Add("DAT_FIN");
           dt.Columns.Add("USU_LANC_MEDIA");
           dt.Columns.Add("USU_MOD_MEDIA");
           dt.Columns.Add("DAT_MOD_MEDIA");

           #endregion

           int qtd_dis;

           sql_comm.Connection = sql_conn;

           if (id_ensino == 0)
           {
               sql_comm.CommandText = @"SELECT * FROM HIST_ALUNO_F";
               qtd_dis = 8;
           }
           else
           {
               sql_comm.CommandText = @"SELECT * FROM HIST_ALUNO_M";
               qtd_dis = 12;
           }
           
           try
           {
               int linhas = 0;
               
               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())               
               {
                   for (int i = 0; i < qtd_dis; i++)
                   {
                       if (reader["INS_" + (i + 1)] != DBNull.Value)
                       {
                           if (reader["INS_" + (i + 1)].ToString() != "")
                           {
                               dt.Rows.Add();
                               dt.Rows[linhas][2] = i + 1; //id_disciplina
                               dt.Rows[linhas][3] = reader["N_MAT"]; 
                               dt.Rows[linhas][4] = id_ensino; //Id_ensino
                               if (reader["INS_" + (i + 1)] != DBNull.Value)
                               {
                                   dt.Rows[linhas][5] = reader["INS_" + (i + 1)];
                               }
                               else dt.Rows[linhas][10] = DBNull.Value;
                               if (reader["MU_" + (i + 1)] != DBNull.Value)
                               {
                                   dt.Rows[linhas][6] = reader["MU_" + (i + 1)];
                               }
                               else dt.Rows[linhas][10] = DBNull.Value;

                               if (reader["UF_" + (i + 1)] != DBNull.Value)
                                   dt.Rows[linhas][7] = reader["UF_" + (i + 1)];
                               else dt.Rows[linhas][10] = DBNull.Value;

                               if (reader["NOT_FIN_" + (i + 1)] != DBNull.Value)
                                   dt.Rows[linhas][8] = reader["NOT_FIN_" + (i + 1)];                               
                               else dt.Rows[linhas][8] = DBNull.Value;

                               if (reader["ORIENT_INI_" + (i + 1)] != DBNull.Value)
                               dt.Rows[linhas][9] = reader["ORIENT_INI_" + (i + 1)];
                               else dt.Rows[linhas][9] = DBNull.Value;
                   
                               if (reader["DT_" + (i + 1)] != DBNull.Value)
                               {
                                   if (reader["DT_" + (i + 1)].ToString() != "")
                                   {
                                       if (Regex.IsMatch(reader["DT_" + (i + 1)].ToString(), "[a-z]"))
                                       {
                                           dt.Rows[linhas][10] = DateTime.Parse(reader["DT_" + (i + 1)].ToString()).ToString("dd/MM/yyyy");                                           
                                       }
                                       else
                                       {
                                           dt.Rows[linhas][10] = reader["DT_" + (i + 1)];                                           
                                       }
                                   }
                               }
                               else
                               {
                                   dt.Rows[linhas][10] = DBNull.Value;
                               }

                               if (reader["ORIENTADOR_" + (i + 1)] != DBNull.Value)
                               {
                                   if (reader["ORIENTADOR_" + (i + 1)].ToString().All(char.IsDigit))
                                   {
                                       dt.Rows[linhas][11] = reader["ORIENTADOR_" + (i + 1)];
                                   }
                                   else
                                   {
                                       dt.Rows[linhas][11] = DBNull.Value;
                                   }
                                   
                               }
                               else
                               {
                                   dt.Rows[linhas][11] = DBNull.Value;
                               }
                   
                               linhas++;          
                           }                   
                       }                     
                   }
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

           //Insert

           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = @"INSERT INTO MEDIAS (ID_DISCIPLINA,
                                                        N_MAT,
                                                        ENSINO,
                                                        INSTITUICAO,
                                                        MUNICIPIO,
                                                        UF,
                                                        MEDIA,
                                                        DAT_INI,
                                                        DAT_FIN,
                                                        USU_LANC_MEDIA) 
                                                VALUES (@id_disciplina,
                                                        @n_mat,
                                                        @ensino,
                                                        @instituicao,
                                                        @municipio,
                                                        @uf,
                                                        @media,
                                                        @dat_ini,
                                                        @dat_fin,
                                                        @usu_lanc_media)";
           

           for (int i = 0; i < dt.Rows.Count; i++)
           {
               sql_comm.Parameters.AddWithValue("@id_disciplina", dt.Rows[i][2]);
               sql_comm.Parameters.AddWithValue("@n_mat", dt.Rows[i][3]);
               sql_comm.Parameters.AddWithValue("@ensino", dt.Rows[i][4]);
               sql_comm.Parameters.AddWithValue("@instituicao", dt.Rows[i][5]);
               sql_comm.Parameters.AddWithValue("@municipio", dt.Rows[i][6]);
               sql_comm.Parameters.AddWithValue("@uf", dt.Rows[i][7]);
               sql_comm.Parameters.AddWithValue("@media", dt.Rows[i][8]);
               sql_comm.Parameters.AddWithValue("@dat_ini", dt.Rows[i][9]);
               sql_comm.Parameters.AddWithValue("@dat_fin", dt.Rows[i][10]);
               sql_comm.Parameters.AddWithValue("@usu_lanc_media", dt.Rows[i][11]);
              

               

               try
               {
                   sql_conn.Open();
                   sql_comm.ExecuteNonQuery();
               }
               catch (Exception ex)
               {
                   //Colocar na lista de erros
                                      
                   cs_erros.registra_erro(dt.Rows[i][3] + " - Id_disciplina: " + dt.Rows[i][2] + " - Erro: " + ex.Message,"error_log");
               }
               finally
               {

                   sql_comm.Parameters.Clear();
                   sql_conn.Close();
               }
           }
               
       }

       public void troca_id_disciplina()
       {
           DataTable dt = new DataTable();
           dt.Columns.Add("id_media");
           dt.Columns.Add("id_disciplina");
           dt.Columns.Add("id_ensino");

           sql_comm.Connection = sql_conn;

           sql_comm.CommandText = "SELECT * FROM MEDIAS";

           try
           {
               int linhas = 0;

               sql_conn.Open();
               SqlDataReader reader = sql_comm.ExecuteReader();
               while (reader.Read())
               {
                   dt.Rows.Add();
                   dt.Rows[linhas][0] = reader["ID_MEDIA"].ToString();
                   dt.Rows[linhas][1] = reader["ID_DISCIPLINA"].ToString();
                   dt.Rows[linhas][2] = reader["ENSINO"].ToString();
                   linhas++;
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

           //UPDATE

           string id_disc;

           for (int i = 0; i < dt.Rows.Count; i++ )
           {

               sql_comm.Connection = sql_conn;
               sql_comm.CommandText = @"UPDATE MEDIAS SET ID_DISCIPLINA=@id_disc WHERE ID_MEDIA=@id_media";

               if(dt.Rows[i][2].ToString() == "0")
               {
                   //Fundamental

                   //Tabela Update Fundamental
                   // 1 - 1 Por
                   // 2 - 4 His
                   // 3 - 12 Geo
                   // 4 - 11 Cie
                   // 5 - 2 Mat
                   // 6 - 7 Ing
                   // 7 - 8 Art
                   switch (dt.Rows[i][1].ToString())
                   {
                       case "1":
                           id_disc = "1";
                           break;
                       case "2":
                           id_disc = "4";
                           break;
                       case "3":
                           id_disc = "12";
                           break;
                       case "4":
                           id_disc = "11";
                           break;
                       case "5":
                           id_disc = "2";
                           break;
                       case "6":
                           id_disc = "7";
                           break;
                       default:
                           id_disc = "8";
                           break;
                   }
               }
               else
               {
                   //Médio

                   //Tabela Update Médio
                   // 1 - 1 Por
                   // 2 - 4 hist
                   // 3 - 12 geo
                   // 4 - 5 fis
                   // 5 - 3 qui
                   // 6 - 6 bio
                   // 7 - 2 mat
                   // 8 - 7 ing
                   // 9 - 8 art
                   // 10 - 9 fil
                   // 11 - 10 soc
                   switch (dt.Rows[i][1].ToString())
                   {
                       case "1":
                           id_disc = "1";
                           break;
                       case "2":
                           id_disc = "4";
                           break;
                       case "3":
                           id_disc = "12";
                           break;
                       case "4":
                           id_disc = "5";
                           break;
                       case "5":
                           id_disc = "3";
                           break;
                       case "6":
                           id_disc = "6";
                           break;
                       case "7":
                           id_disc = "2";
                           break;
                       case "8":
                           id_disc = "7";
                           break;
                       case "9":
                           id_disc = "8";
                           break;
                       case "10":
                           id_disc = "9";
                           break;
                       default:
                           id_disc = "10";
                           break;
                   }
               }               

               sql_comm.Parameters.AddWithValue("@id_media", dt.Rows[i][0]);
               sql_comm.Parameters.AddWithValue("@id_disc", id_disc);

               try
               {
                   sql_conn.Open();
                   sql_comm.ExecuteNonQuery();
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
           }

               
       }
       
    }
}
