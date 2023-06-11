using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Usuarios_Grupos
{
    class csGrupos
    {
        //SQL
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        //
        public int id_grupo { get; set; }
        public string n_grupo { get; set; }
        public List<int> permissoes { get; set; }


        ///PERMISSÕES DO SISTEMA - lista de permissoes e seus ids são configurados aqui,
        //Caso surja uma nova permissao, addcionar nesta lista
        public int id_permissao_sistema { get; set; }
        public string nome_permissao_sistema { get; set; }


        public List<csGrupos> lista_grupos(int id_grupo_pesquisa=0)
        {
            List<csGrupos> list_ = new List<csGrupos>();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM GRUPOS ORDER BY ID_GRUPO";

            if (id_grupo_pesquisa>0)
            {
                sql_comm.CommandText = @"SELECT * FROM GRUPOS WHERE ID_GRUPO=@id_grupo_pesquisa ORDER BY ID_GRUPO";
                sql_comm.Parameters.AddWithValue("@id_grupo_pesquisa", id_grupo_pesquisa);
            }

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();

                while(reader.Read())
                {
                    csGrupos cs_grupo_ = new csGrupos();
                    cs_grupo_.id_grupo = Convert.ToInt32(reader["ID_GRUPO"]);
                    cs_grupo_.n_grupo = reader["GRUPO"].ToString();
                    if (reader["PERMISSOES"] != DBNull.Value) cs_grupo_.permissoes = reader["PERMISSOES"].ToString().Split('|').Select(Int32.Parse).ToList();
                    list_.Add(cs_grupo_);
                
                }

                reader.Close();
                

            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                sql_conn.Close();
            }

            return list_; 
        }

        public void add_novo_grupo(string grupo)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"INSERT INTO GRUPOS (GRUPO) VALUES (@grupo)";

            sql_comm.Parameters.AddWithValue("@grupo", grupo);

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
                sql_conn.Close();
            }
        }
        public void upd_nome_grupo (int id_grupo, string grupo)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE GRUPOS SET (GRUPO=@grupo) WHERE ID_GRUPO=@id_grupo";

            sql_comm.Parameters.AddWithValue("@id_grupo", id_grupo);
            sql_comm.Parameters.AddWithValue("@grupo", grupo);

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
                sql_conn.Close();
            }
        }
        public void upd_permissoes_grupo (int id_grupo, List<int> list_ids_permissoes)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE GRUPOS SET PERMISSOES=@permissoes WHERE ID_GRUPO=@id_grupo";

            sql_comm.Parameters.AddWithValue("@id_grupo", id_grupo);

            string a = "";

            if(list_ids_permissoes.Count > 0)
            {
                for (int i = 0; i < list_ids_permissoes.Count; i++)
                {
                    a = a + list_ids_permissoes[i] + "|";
                }
                a = a.Remove(a.Length - 1);
                sql_comm.Parameters.AddWithValue("@permissoes", a);
            }
            else sql_comm.Parameters.AddWithValue("@permissoes", DBNull.Value);    

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
                sql_conn.Close();
            }
        }

        //metódo para traduzir ids de permissoes do grupo em nomes de permissões (ex. nome de um campo)
        
        public List<csGrupos> lista_permissoes_do_sistema()
        {
            List<csGrupos> list_ = new List<csGrupos>();

            csGrupos csgrupo_1 = new csGrupos();
            csgrupo_1.id_permissao_sistema = 1;
            csgrupo_1.nome_permissao_sistema = "tsmSecretaria";
            list_.Add(csgrupo_1);
            csGrupos csgrupo_2 = new csGrupos();
            csgrupo_2.id_permissao_sistema = 2;
            csgrupo_2.nome_permissao_sistema = "tsmProfessores";
            list_.Add(csgrupo_2);
            csGrupos csgrupo_3 = new csGrupos();
            csgrupo_3.id_permissao_sistema = 3;
            csgrupo_3.nome_permissao_sistema = "tsmRelatorios";
            list_.Add(csgrupo_3);
            csGrupos csgrupo_4 = new csGrupos();
            csgrupo_4.id_permissao_sistema = 4;
            csgrupo_4.nome_permissao_sistema = "tsmFerramentas";
            list_.Add(csgrupo_4);
            csGrupos csgrupo_5 = new csGrupos();
            csgrupo_5.id_permissao_sistema = 5;
            csgrupo_5.nome_permissao_sistema = "tsmAjuda";
            list_.Add(csgrupo_5);
            csGrupos csgrupo_6 = new csGrupos();
            csgrupo_6.id_permissao_sistema = 6;
            csgrupo_6.nome_permissao_sistema = "tsmCatraca";
            list_.Add(csgrupo_6);

            return list_;
        }

        //METODO PARA TROCAR ID_NOME_PERMISSOES_SISTEMA

        public int troca_permissao_sistema_nome_por_id(string nome_permissao)
        {
            int a = 0;

            List<csGrupos> list_ = lista_permissoes_do_sistema();

            for (int i = 0; i < list_.Count; i++)
            {
                if(list_[i].nome_permissao_sistema==nome_permissao)
                {
                    a = list_[i].id_permissao_sistema;
                }
            }
            
            return a;
        }

        public int troca_grupo_nome_por_id(string grupo)
        {
            int a = 0;

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_GRUPO FROM GRUPOS WHERE GRUPO=@grupo";

            sql_comm.Parameters.AddWithValue("@grupo", grupo);

            
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

        public string troca_grupo_id_por_nome(int id_grupo)
        {
            string a = "";

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT GRUPO FROM GRUPOS WHERE ID_GRUPO=@id_grupo";

            sql_comm.Parameters.AddWithValue("@id_grupo", id_grupo);


            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    a = reader["GRUPO"].ToString();
                }
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

    }
}
