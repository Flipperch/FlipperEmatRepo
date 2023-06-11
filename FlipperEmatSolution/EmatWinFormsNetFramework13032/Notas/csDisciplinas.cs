using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Novos Using
using System.Data;
using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Notas
{
    public class csDisciplinas
    {
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        public DataTable tab_disciplinas;

        //Disciplinas
        public int id_disciplina { get; set; }
        public string n_disciplina { get; set; }
        public string n_hist_disciplina { get; set; }
        public string horarios_aulas { get; set; }
        public int capacidade { get; set; }
        public int ordem { get; set; }
        public string formula_media { get; set; }

        //Area
        public int id_area { get; set; }
        public string area_ { get; set; }
        public List<int> ids_disciplinas_area { get; set; }   

        //Ensinos
        public int id_ensino { get; set; }
        public string ensino { get; set; }
        public List<int> ids_disciplinas_ensino { get; set; }
        
        //TROCAS DISCIPLINAS
        public string troca_disciplina_id_por_nome(int id_disciplina)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT N_DISCIPLINA FROM DISCIPLINAS WHERE ID_DISCIPLINA=@id_disciplina";

            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["N_DISCIPLINA"].ToString();
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
        public string troca_disciplina_id_por_n_historico(int id_disciplina)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT N_HIST_DISCIPLINA FROM DISCIPLINAS WHERE ID_DISCIPLINA=@id_disciplina";

            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["N_HIST_DISCIPLINA"].ToString();
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
        public int troca_disciplina_nome_por_id(string n_disciplina)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_DISCIPLINA FROM DISCIPLINAS WHERE N_DISCIPLINA=@n_disciplina";

            sql_comm.Parameters.AddWithValue("@n_disciplina", n_disciplina);

            int a = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = (int)reader["ID_DISCIPLINA"];
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
        //TROCAS ENSINOS
        public string troca_ensino_id_por_nome(int id_ensino)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ENSINO FROM ENSINOS WHERE ID_ENSINO=@id_ensino";

            sql_comm.Parameters.AddWithValue("@id_ensino", id_ensino);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["ENSINO"].ToString();
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
        public int troca_ensino_nome_por_id(string ensino)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_ENSINO FROM ENSINOS WHERE ENSINO=@ensino";

            sql_comm.Parameters.AddWithValue("@ensino", ensino);

            int a = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = (int)reader["ID_ENSINO"];
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
        //TROCA AREAS
        public string troca_area_id_por_nome(int id_area)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT AREA FROM AREAS WHERE ID_AREA=@id_area";

            sql_comm.Parameters.AddWithValue("@id_area", id_area);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["AREA"].ToString();
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
        public int troca_area_nome_por_id(string area)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_AREA FROM AREAS WHERE AREA=@area";

            sql_comm.Parameters.AddWithValue("@area", area);

            int a = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = (int)reader["ID_AREA"];
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
        //LISTA AREAS
        public List<csDisciplinas> list_areas(int id_area_pesquisa=0)
        {
            List<csDisciplinas> list_ = new List<csDisciplinas>();
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM AREAS";

            if (id_area_pesquisa != 0)
            {
                sql_comm.CommandText += " WHERE ID_AREA=@id_area_pesquisa";
                sql_comm.Parameters.AddWithValue("@id_area_pesquisa", id_area_pesquisa);
            }

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    csDisciplinas area_ = new csDisciplinas();
                    area_.id_area = Convert.ToInt32(reader[0]);
                    area_.area_ = reader[1].ToString();
                    if(reader[2] != DBNull.Value) area_.ids_disciplinas_area = reader[2].ToString().Split('|').Select(Int32.Parse).ToList();
                    list_.Add(area_);
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

            return list_;
        }
        //LISTA DISCIPLINAS
        public List<csDisciplinas> list_disciplinas(int id_disciplina_pesquisa = 0)
        {
            List<csDisciplinas> list_ = new List<csDisciplinas>();

            sql_comm.Connection = sql_conn;

            if (id_disciplina_pesquisa > 0)
            {
                sql_comm.CommandText = @"SELECT * FROM DISCIPLINAS WHERE ID_DISCIPLINA=@id_disciplina_pesquisa ORDER BY ORDEM";
                sql_comm.Parameters.AddWithValue("@id_disciplina_pesquisa", id_disciplina_pesquisa);
            }
            else sql_comm.CommandText = @"SELECT * FROM DISCIPLINAS ORDER BY ORDEM";
                       

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    csDisciplinas disciplina_ = new csDisciplinas();
                    disciplina_.id_disciplina = Convert.ToInt32(reader["ID_DISCIPLINA"]);
                    disciplina_.n_disciplina = reader["N_DISCIPLINA"].ToString();
                    disciplina_.n_hist_disciplina = reader["N_HIST_DISCIPLINA"].ToString();
                    disciplina_.horarios_aulas = reader["HORARIOS_AULAS"].ToString();
                    if (reader["CAPACIDADE"] != DBNull.Value) disciplina_.capacidade = Convert.ToInt32(reader["CAPACIDADE"].ToString());
                    else disciplina_.capacidade = 0;
                    if (reader["ORDEM"] != DBNull.Value) disciplina_.ordem = Convert.ToInt32(reader["ORDEM"].ToString());
                    else disciplina_.ordem = 0;
                    list_.Add(disciplina_);
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
        //LISTA ENSINOS
        public List<csDisciplinas> list_ensinos(int id_ensino_pesquisa=0)
        {
            List<csDisciplinas> list_ = new List<csDisciplinas>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM ENSINOS";

            if (id_ensino_pesquisa>0)
            {
                sql_comm.CommandText = @"SELECT * FROM ENSINOS WHERE ID_ENSINO=@id_ensino_pesquisa";
                sql_comm.Parameters.AddWithValue("@id_ensino_pesquisa", id_ensino_pesquisa);
            }
            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    csDisciplinas ensino_ = new csDisciplinas();
                    ensino_.id_ensino = Convert.ToInt32(reader["ID_ENSINO"]);
                    ensino_.ensino = reader["ENSINO"].ToString();
                    ensino_.ids_disciplinas_ensino = reader["IDS_DISCIPLINAS"].ToString().Split('|').Select(Int32.Parse).ToList();
                    list_.Add(ensino_);
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
        //METODOS PARA INSERT AREAS
        public void add_area(string area)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"INSERT INTO AREAS (AREA) VALUES (@area)";

            sql_comm.Parameters.AddWithValue("area", area);

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
        //METODOS PARA INSERT ENSINOS
        public void add_ensino(string ensino)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"INSERT INTO ENSINOS (ENSINO) VALUES (@ensino)";

            sql_comm.Parameters.AddWithValue("ensino", ensino);

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
        //METODOS PARA INSERT DISCIPLINAS
        public void add_disciplina(string disciplina)
        {
            List<csDisciplinas> list_ = list_disciplinas();
            int ordem_nova = 0;

            for (int i = 0; i < list_.Count; i++)
            {
                if(list_[i].ordem > ordem_nova)
                {
                    ordem_nova = list_[i].ordem;
                }
            }

            ordem_nova = ordem_nova + 1;
            
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"INSERT INTO DISCIPLINAS (N_DISCIPLINA, ORDEM) VALUES (@disciplina, @ordem)";

            sql_comm.Parameters.AddWithValue("disciplina", disciplina);
            sql_comm.Parameters.AddWithValue("ordem", ordem_nova);

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
        //METODOS PARA UPDATE AREAS
        public void upd_ids_disciplinas_area(int id_area, List<int> list_ids_disciplinas )
        {
            string a = "";

            for (int i = 0; i < list_ids_disciplinas.Count; i++)
            {
                a = a + list_ids_disciplinas[i] + "|";
            }
            if (a != string.Empty)
            {
                //Remover Ultimo Pipe "|"
                a = a.Remove(a.Length - 1);
                sql_comm.Parameters.AddWithValue("ids_disciplinas", a);
            }
            else sql_comm.Parameters.AddWithValue("ids_disciplinas", DBNull.Value);

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE AREAS SET IDS_DISCIPLINAS=@ids_disciplinas WHERE ID_AREA=@id_area";

            sql_comm.Parameters.AddWithValue("id_area", id_area);
            
            

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
        //METODOS PARA UPDATE ENSINOS
        public void upd_ids_disciplinas_ensino(int id_ensino, List<int> list_ids_disciplinas)
        {
            string a = "";

            for (int i = 0; i < list_ids_disciplinas.Count; i++)
            {
                a = a + list_ids_disciplinas[i] + "|";
            }
            a = a.Remove(a.Length - 1);

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE ENSINOS SET IDS_DISCIPLINAS=@ids_disciplinas WHERE ID_ENSINO=@id_ensino";

            sql_comm.Parameters.AddWithValue("id_ensino", id_ensino);
            sql_comm.Parameters.AddWithValue("ids_disciplinas", a);


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
        //METODOS PARA UPDATE DISCIPLINAS
        public void upd_nome_hist_disciplina(int id_disciplina, string nome_hist_disciplina)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE DISCIPLINAS SET N_HIST_DISCIPLINA=@nome_hist_disciplina WHERE ID_DISCIPLINA=@id_disciplina";

            sql_comm.Parameters.AddWithValue("id_disciplina", id_disciplina);
            sql_comm.Parameters.AddWithValue("nome_hist_disciplina", nome_hist_disciplina);

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
        public void upd_ordem_disciplina(int id_disciplina, int ordem)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE DISCIPLINAS SET ORDEM=@ordem WHERE ID_DISCIPLINA=@id_disciplina";

            sql_comm.Parameters.AddWithValue("id_disciplina", id_disciplina);
            sql_comm.Parameters.AddWithValue("ordem", ordem);

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
        public void upd_cap_disciplina(int id_disciplina, int capacidade_upd)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE DISCIPLINAS SET CAPACIDADE=@capacidade_upd WHERE ID_DISCIPLINA=@id_disciplina";

            sql_comm.Parameters.AddWithValue("id_disciplina", id_disciplina);
            sql_comm.Parameters.AddWithValue("capacidade_upd", capacidade_upd);

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
        public void alterar_ordem_disciplina(int id_disciplina, int subir=1)
        {
            List<csDisciplinas> list_ = list_disciplinas();
            int ordem_atual = 0;
            int id_disciplina_troca = 0;
            int ordem_troca = 0;

            for(int i = 0; i < list_.Count; i++)
            {
                if(list_[i].id_disciplina == id_disciplina)
                {
                    if(subir==1)
                    {
                        ordem_atual = list_[i].ordem;
                        id_disciplina_troca = list_[i - 1].id_disciplina;
                        ordem_troca = list_[i - 1].ordem;
                        
                        //alterar ordem solicitado
                        upd_ordem_disciplina(id_disciplina, ordem_atual-1);
                        //alterar ordem troca
                        upd_ordem_disciplina(id_disciplina_troca, ordem_troca + 1);

                    }
                        else
                    {
                        ordem_atual = list_[i].ordem;
                        id_disciplina_troca = list_[i + 1].id_disciplina;
                        ordem_troca = list_[i + 1].ordem;

                        //alterar ordem solicitado
                        upd_ordem_disciplina(id_disciplina, ordem_atual + 1);
                        //alterar ordem troca
                        upd_ordem_disciplina(id_disciplina_troca, ordem_troca - 1);
                    }
                    
                }
            }
        }
        public int qtd_alunos_disciplina(int id_disciplina_pesquisa, string hoje="")
        {
            int a = 0;

            sql_comm.Connection = sql_conn;

            if(hoje == string.Empty)
            {
                sql_comm.CommandText = @"SELECT ID_DISCIPLINA_ATUAL FROM ALUNOS WHERE ID_DISCIPLINA_ATUAL=@id_disciplina_pesquisa AND ATIVO=1";

                sql_comm.Parameters.AddWithValue("@id_disciplina_pesquisa", id_disciplina_pesquisa);
            }
            else
            {
                sql_comm.CommandText = @"SELECT ID_DISCIPLINA_ATUAL FROM ALUNOS 
                                         WHERE ID_DISCIPLINA_ATUAL=@id_disciplina_pesquisa AND ATIVO=1 AND DT_ENT_DISCIPLINA between @hoje AND @amanha";

                sql_comm.Parameters.AddWithValue("@id_disciplina_pesquisa", id_disciplina_pesquisa);
                sql_comm.Parameters.AddWithValue("@hoje", hoje);
                string b = DateTime.Parse(hoje).AddDays(1).ToString("dd/MM/yyyy");
                sql_comm.Parameters.AddWithValue("@amanha", DateTime.Parse(hoje).AddDays(1));
            }
            

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a++;
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
        
        public DataTable buscarDisciplinas()
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM DISCIPLINAS";

           

            DataTable table = new DataTable();
            table.Columns.Add("id_disciplina");       //0
            table.Columns.Add("n_disciplina");        //1

            int i = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add();
                    table.Rows[i][0] = reader["ID_DISCIPLINA"].ToString();
                    table.Rows[i][1] = reader["N_DISCIPLINA"].ToString();

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

            tab_disciplinas = table;

            return table;

            
        }

        public DataTable buscarDisciplinas_ensino(int id_ensino)
        {
            List<int> list_ = new List<int>();
            list_ = lista_ids_disciplina_por_ensino(id_ensino);
            string a = "";
            for (int i = 0; i < list_.Count; i++)
            {
                if (i == list_.Count - 1) a += list_[i].ToString();
                else a += list_[i].ToString() + ", ";
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("id_disciplina");       //0
            dt.Columns.Add("n_disciplina");        //1             
            dt.Columns.Add("n_hist_mat");          //2
            dt.Columns.Add("horarios_aula");       //3
            dt.Columns.Add("capacidade");          //4
            dt.Columns.Add("qtd_alunos");          //5 - 0
            dt.Columns.Add("entradas_hoje");       //6 - 0            
            
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM DISCIPLINAS WHERE ID_DISCIPLINA IN (" + a + ") ORDER BY ORDEM";

            try
            {            
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1][0] = reader["ID_DISCIPLINA"].ToString();
                    dt.Rows[dt.Rows.Count - 1][1] = reader["N_DISCIPLINA"].ToString();
                    dt.Rows[dt.Rows.Count - 1][2] = reader["N_HIST_DISCIPLINA"].ToString();
                    dt.Rows[dt.Rows.Count - 1][3] = reader["HORARIOS_AULAS"];
                    dt.Rows[dt.Rows.Count - 1][4] = reader["CAPACIDADE"];
                    dt.Rows[dt.Rows.Count - 1][5] = 0;
                    dt.Rows[dt.Rows.Count - 1][6] = 0;                                        
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
            

            return dt;
        }

        public List<int> disciplinas_conluidas(int n_mat, int id_ensino)
        {
            List<int> a = new List<int>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT MEDIA, ID_DISCIPLINA FROM MEDIAS WHERE N_MAT=@n_mat AND ENSINO=@id_ensino ORDER BY ID_DISCIPLINA";


            sql_comm.Parameters.AddWithValue("@n_mat", n_mat);
            sql_comm.Parameters.AddWithValue("@id_ensino", id_ensino);

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();

                while (reader.Read())
                {
                    a.Add((int)reader["ID_DISCIPLINA"]);
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

        public List<int> lista_ids_disciplina_por_ensino(int id_ensino)
        {
            List<int> list_ = new List<int>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM ENSINOS WHERE ID_ENSINO=@id_ensino";

            sql_comm.Parameters.AddWithValue("@id_ensino", id_ensino);            

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                     list_ = reader["IDS_DISCIPLINAS"].ToString().Split('|').Select(Int32.Parse).ToList();            

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

        public List<string> lista_disciplinas_por_ensino(int id_ensino)
        {
            DataTable tb = buscarDisciplinas_ensino(id_ensino);
            List<string> a = new List<string>();

            a.Add("");

            for(int i = 0; i < tb.Rows.Count; i++ )
            {
                a.Add(tb.Rows[i][1].ToString());
            }

            return a;
        }

        public List<string> lista_ensinos()
        {
            List<string> a = new List<string>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ENSINO FROM ENSINOS";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();

                while(reader.Read())
                {
                    a.Add(reader["ENSINO"].ToString());
                }
                reader.Close();

            }
            catch
            {

            }
            finally
            {
                sql_conn.Close();
            }

            return a;
        }

        public List<int> lista_ids_ensinos()
        {
            List<int> a = new List<int>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_ENSINO FROM ENSINOS";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();

                while (reader.Read())
                {
                    a.Add(Convert.ToInt32(reader["ID_ENSINO"]));
                }
                reader.Close();

            }
            catch
            {

            }
            finally
            {
                sql_conn.Close();
            }

            return a;
        }

        private List<int> list_ids_disciplinas_concluidas(string n_mat_, int id_ensino_)
        {
            List<int> list_ = new List<int>();
            csNotas cs_notas = new csNotas();
            List<csNotas> list_notas = cs_notas.lista_notas_concluidas(n_mat_, id_ensino_);

            for (int i = 0; i < list_notas.Count; i++)
            {
                list_.Add(list_notas[i].id_disciplina);
            }
            return list_;
        }

        private List<csDisciplinas> list_disciplinas_(int id_ensino_)
        {
            List<csDisciplinas> list_ = new List<csDisciplinas>();
            List<int> list_ids_disciplinasss = lista_ids_disciplina_por_ensino(id_ensino_);

            for (int i = 0; i < list_ids_disciplinasss.Count; i++)
            {
                sql_comm.Connection = sql_conn;
                sql_comm.CommandText = @"SELECT * FROM DISCIPLINAS WHERE ID_DISCIPLINA=@id_disciplina ORDER BY ORDEM";
                sql_comm.Parameters.AddWithValue("@id_disciplina", list_ids_disciplinasss[i]);
                try
                {
                    sql_conn.Open();
                    SqlDataReader reader = sql_comm.ExecuteReader();
                    while (reader.Read())
                    {
                        csDisciplinas disciplina_ = new csDisciplinas();
                        disciplina_.id_disciplina = Convert.ToInt32(reader["ID_DISCIPLINA"]);
                        disciplina_.n_disciplina = reader["N_DISCIPLINA"].ToString();
                        disciplina_.n_hist_disciplina = reader["N_HIST_DISCIPLINA"].ToString();
                        disciplina_.horarios_aulas = reader["HORARIOS_AULAS"].ToString();
                        if (reader["CAPACIDADE"] != DBNull.Value) disciplina_.capacidade = Convert.ToInt32(reader["CAPACIDADE"].ToString());
                        else disciplina_.capacidade = 0;
                        if (reader["ORDEM"] != DBNull.Value) disciplina_.ordem = Convert.ToInt32(reader["ORDEM"].ToString());
                        else disciplina_.ordem = 0;
                        list_.Add(disciplina_);
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
            }            
            return list_;
        }

        private List<csDisciplinas> lista_disciplinas_concluidas(List<int> list_ids_)
        {
            List<csDisciplinas> list_ = new List<csDisciplinas>();
            List<int> list_ids_disciplinas_con_ = list_ids_;

            for(int i = 0; i < list_ids_disciplinas_con_.Count; i++)
            {
                list_.Add(list_disciplinas(list_ids_disciplinas_con_[i])[0]);
            }

            return list_;
        }

        public List<csDisciplinas> lista_disciplinas_fazer(string n_mat_, int id_ensino_)
        {
            List<csDisciplinas> list_ = new List<csDisciplinas>();

            //Lista de Ids de Disciplinas Concluidas
            List<int> list_ids_con_ = list_ids_disciplinas_concluidas(n_mat_, id_ensino_);

            //Lista de Disciplinas do Ensino
            List<csDisciplinas> list_disc = list_disciplinas_(id_ensino_);

            for (int i = 0; i < list_disc.Count; i++)
            {
                if(!list_ids_con_.Contains(list_disc[i].id_disciplina))
                {
                    list_.Add(list_disc[i]);
                }
            }
            
            return list_;
        }        

        public void upd_area(int id_area, string area_, string ids_disciplinas_areas)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE AREAS SET AREA=@area, IDS_DISCIPLINAS_AREAS=@ids_disciplinas_areas WHERE ID_AREA=@id_area";

            sql_comm.Parameters.AddWithValue("id_area", id_area);
            sql_comm.Parameters.AddWithValue("area", area_);
            sql_comm.Parameters.AddWithValue("ids_disciplinas_areas", ids_disciplinas_areas);

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

        //FORMULA DE MÉDIAS
        public void gravar_formula(int id_disciplina, int id_ensino, string formula)
        {
            if (sel_formula_media(id_disciplina, id_ensino) != string.Empty)
            {
                upd_formula_media(id_disciplina, id_ensino, formula);
            }
            else
            {
                add_formula_media(id_disciplina, id_ensino, formula);
            }
        }

        private void add_formula_media(int id_disciplina, int id_ensino, string formula)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"INSERT INTO FORMULAS_MEDIAS VALUES(@id_disciplina, @id_ensino, @formula";

            sql_comm.Parameters.AddWithValue("id_disciplina", id_disciplina);
            sql_comm.Parameters.AddWithValue("id_ensino", id_ensino);
            sql_comm.Parameters.AddWithValue("formula", formula);

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

        private void upd_formula_media(int id_disciplina, int id_ensino, string formula)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE FORMULAS_MEDIAS SET FORMULA_MEDIA=@formula WHERE ID_DISCIPLINA=@id_disciplina AND ID_ENSINO=@id_ensino";

            sql_comm.Parameters.AddWithValue("id_disciplina", id_disciplina);
            sql_comm.Parameters.AddWithValue("id_ensino", id_ensino);
            sql_comm.Parameters.AddWithValue("formula", formula);

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

        public string sel_formula_media(int id_disciplina, int id_ensino)
        {
            string a = "";

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = "SELECT FORMULA_MEDIA FROM FORMULAS_MEDIAS WHERE ID_DISCIPLINA=@id_disciplina AND ID_ENSINO=@id_ensino";

            sql_comm.Parameters.AddWithValue("@id_disciplina", id_disciplina);
            sql_comm.Parameters.AddWithValue("@id_ensino", id_ensino);

            try
            {
                sql_comm.Connection.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();

                while(reader.Read())
                {
                    a = reader["FORMULA_MEDIA"].ToString();
                }
                
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
            return a;
        }
    
    }
}
