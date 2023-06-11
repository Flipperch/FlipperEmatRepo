using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Novos Using...
using System.Data;
using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Usuarios_Grupos
{
    public class csUsuarios
    {
        //SQL
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        public int id_usuario { get; set; }
        public string usuario { get; set; }
        public int id_grupo_usuario { get; set; }
        public string psw { get; set; }
        public string nome { get; set; }
        public int inativo { get; set; }
        public string rg { get; set; }
        public int id_cartao { get; set; }
        public int id_disciplina_user { get; set; }
        public List<int> ids_areas { get; set; }    
        
        //ID => NOME
        public string troca_id_por_nome(int id_usuario)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT NOME FROM USUARIO WHERE ID_USUARIO=@id_usuario";

            sql_comm.Parameters.AddWithValue("@id_usuario", id_usuario);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["NOME"].ToString();
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
        //NOME => ID
        public int troca_nome_por_id(string nome)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_USUARIO FROM USUARIO WHERE NOME=@nome";

            sql_comm.Parameters.AddWithValue("@nome", nome);

            int a =0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = Int32.Parse(reader["ID_USUARIO"].ToString());
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
        //NOME => ID
        public int troca_usuario_por_id(string usuario)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_USUARIO FROM USUARIO WHERE USUARIO=@usuario";

            sql_comm.Parameters.AddWithValue("@usuario", usuario);

            int a = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = Int32.Parse(reader["ID_USUARIO"].ToString());
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
        //ID => DISCIPLINA
        public int id_disciplina(int id_usuario)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_DISCIPLINA FROM USUARIO WHERE ID_USUARIO=@id_usuario";

            sql_comm.Parameters.AddWithValue("@id_usuario", id_usuario);

            int a = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = Convert.ToInt32(reader["ID_DISCIPLINA"]);
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

        public List<string> list_nome_usuarios()
        {
            List<string> a = new List<string>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT NOME FROM USUARIO WHERE INATIVO = 0 AND ID_DISCIPLINA IS NULL AND NOME <> 'admin' ORDER BY NOME";
            
            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a.Add(reader["NOME"].ToString());
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

        public List<int> list_id_usuarios()
        {
            List<int> a = new List<int>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_USUARIO FROM USUARIO WHERE INATIVO = 0 AND ID_DISCIPLINA IS NULL AND NOME <> 'admin' ORDER BY NOME";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a.Add(Convert.ToInt32(reader["ID_USUARIO"]));
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

        public void upd_id_disciplina_usuario(int id_usuario, int id_disciplina)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE USUARIO SET ID_DISCIPLINA=@id_disciplina WHERE ID_USUARIO=@id_usuario";

            sql_comm.Parameters.AddWithValue("@id_usuario", id_usuario);
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

        public void limpa_id_disciplina_usuario(int id_usuario)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE USUARIO SET ID_DISCIPLINA=@id_disciplina WHERE ID_USUARIO=@id_usuario";

            sql_comm.Parameters.AddWithValue("@id_usuario", id_usuario);
            sql_comm.Parameters.AddWithValue("@id_disciplina", DBNull.Value);

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

        //INATIVO
        public bool esta_inativo(int id_usuario)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT INATIVO FROM USUARIO WHERE ID_USUARIO=@id_usuario";

            sql_comm.Parameters.AddWithValue("@id_usuario", id_usuario);

            bool a = false;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if(Convert.ToInt32(reader["INATIVO"]) == 1)
                    {
                        a = true;
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

        public void ativa_ou_inativar(int id_usuario, int situacao)
        {
            //0 : Ativo
            //1 : Inativo


            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE USUARIO SET INATIVO=@situacao WHERE ID_USUARIO=@id_usuario";

            sql_comm.Parameters.AddWithValue("@id_usuario", id_usuario);
            sql_comm.Parameters.AddWithValue("@situacao", situacao);

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

        //GRUPO
        public string troca_n_grupo_id_grupo(int id_grupo)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT GRUPO FROM GRUPOS WHERE ID_GRUPO=@id_grupo";

            sql_comm.Parameters.AddWithValue("@id_grupo", id_grupo);

            string a = "";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["GRUPO"].ToString();
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

        public int id_grupo(int id_usuario)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_GRUPO FROM USUARIO WHERE ID_USUARIO=@id_usuario";

            sql_comm.Parameters.AddWithValue("@id_usuario", id_usuario);

            int a = 0;

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = Convert.ToInt32(reader["ID_GRUPO"]);
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


        //ID_AREA
        public void upd_ids_areas_user(int id_usuario, List<int> list_ids_areas)
        {
            string a = "";

            for (int i = 0; i < list_ids_areas.Count; i++)
            {
                a = a + list_ids_areas[i] + "|";
            }

            if (list_ids_areas.Count > 0)
            {
                a = a.Remove(a.Length - 1);
                sql_comm.Parameters.AddWithValue("ids_areas", a);
            }
            else
            {
                sql_comm.Parameters.AddWithValue("ids_areas", DBNull.Value);
            }
            

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE USUARIO SET IDS_AREAS=@ids_areas WHERE ID_USUARIO=@id_usuario";

            sql_comm.Parameters.AddWithValue("id_usuario", id_usuario);
            


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

        public List<int> ids_areas_user(int id_usuario)
        {
            List<int> list_ = new List<int>();
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT IDS_AREAS FROM USUARIO WHERE ID_USUARIO=@id_usuario";
            sql_comm.Parameters.AddWithValue("id_usuario", id_usuario);
            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if(reader[0] != DBNull.Value) list_ = reader[0].ToString().Split('|').Select(Int32.Parse).ToList();                    
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

        //EXISTE ?
        public bool usuario_existe(string usuario)
        {
            bool a = false;
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT USUARIO FROM USUARIO WHERE USUARIO=@usuario";

            sql_comm.Parameters.AddWithValue("@usuario", usuario);

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = true;
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
            return a;
        }

        public bool psw_confirma(string usuario, string psw)
        {
            bool a = false;
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT PSW FROM USUARIO WHERE USUARIO=@usuario";

            sql_comm.Parameters.AddWithValue("@usuario", usuario);

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    if(reader[0].ToString() == psw)
                    {
                        a = true;
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
            return a;
        }

        //LISTA USUARIOS
        public List<csUsuarios> lista_usuarios(int id_usuario_pesquisa=0, string ordem ="NENHUMA", bool incluir_inativos=false)
        {
            List<csUsuarios> list_ = new List<csUsuarios>();

            sql_comm.Connection = sql_conn;

            if (incluir_inativos)
            {
                sql_comm.CommandText = @"SELECT * FROM USUARIO WHERE INATIVO = 0";
            }
            else
            {
                sql_comm.CommandText = @"SELECT * FROM USUARIO";
            }

            //add WHERE ID_USUARIO        
            if (id_usuario_pesquisa != 0)
            {
                if(sql_comm.CommandText.Contains("WHERE"))
                {
                    sql_comm.CommandText += " AND ID_USUARIO=@id_usuario_pesquisa ";
                    sql_comm.Parameters.AddWithValue("@id_usuario_pesquisa", id_usuario_pesquisa);
                }
                else
                {
                    sql_comm.CommandText += " WHERE ID_USUARIO=@id_usuario_pesquisa ";
                    sql_comm.Parameters.AddWithValue("@id_usuario_pesquisa", id_usuario_pesquisa);
                }
                
            }

            if (ordem != "NENHUMA")
            {
                sql_comm.CommandText += " ORDER BY " + ordem;
            }  

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    csUsuarios usuario_ = new csUsuarios();
                    usuario_.id_usuario = Convert.ToInt32(reader["ID_USUARIO"]);
                    usuario_.usuario = reader["USUARIO"].ToString();
                    usuario_.id_grupo_usuario = Convert.ToInt32(reader["ID_GRUPO"]);
                    usuario_.psw = reader["PSW"].ToString();
                    usuario_.nome = reader["NOME"].ToString();
                    usuario_.inativo = Convert.ToInt32(reader["INATIVO"]);

                    if (reader["RG"] != DBNull.Value) usuario_.rg = reader["RG"].ToString();
                    if (reader["ID_CARTAO"] != DBNull.Value) usuario_.id_cartao = Convert.ToInt32(reader["ID_CARTAO"]);

                    if (reader["ID_DISCIPLINA"] != DBNull.Value)
                    {
                        usuario_.id_disciplina_user = Convert.ToInt32(reader["ID_DISCIPLINA"]);
                    }
                    if (reader["IDS_AREAS"] != DBNull.Value)
                    {
                        usuario_.ids_areas = reader["IDS_AREAS"].ToString().Split('|').Select(Int32.Parse).ToList();   
                    }
                     
                    list_.Add(usuario_);
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
    }
}
