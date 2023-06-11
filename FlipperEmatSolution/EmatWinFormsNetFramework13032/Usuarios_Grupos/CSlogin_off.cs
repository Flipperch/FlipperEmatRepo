using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.Data.SqlClient;
using System.Data;

namespace EmatWinFormsNetFramework13032.Usuarios_Grupos
{
    class CSlogin_off
    {
        Configurações.CSconnexoes_txt cami = new Configurações.CSconnexoes_txt();        
        SqlQuery consulta = new SqlQuery(Conexoes.GetSqlConnection());

        csGrupos cs_grupos = new csGrupos();

        public string psw_atual = "";
        public string psw_confirma = "";
        public string psw_new = "";

        public int ok = 0;

        public int tamanho_log;
        public int tamanho_log_sai;
        public List<DateTime> list_logs = new List<DateTime>();

        public void logar()
        {
            cami.get_configuracoes();
            consulta.Command.Parameters.Clear();
            consulta.Command.CommandText = @"SELECT * FROM ACESSO_SISTEMA WHERE ID_USUARIO=@iduser_atual";
            consulta.Command.Parameters.AddWithValue("@iduser_atual", csUsuario_logado.id_usuario_logado);
            try
            {
                if (consulta.Connection.State != ConnectionState.Closed)
                {
                    consulta.Connection.Close();
                }
                consulta.Connection.Open();
                SqlDataReader reader = consulta.Command.ExecuteReader();
                
                if(!reader.HasRows)
                {
                    primeiro_acesso();
                }
                
                registra_ent_sis();
                

            }
            catch
            {

            }
            finally
            {
                consulta.Connection.Close();
            }
        }

        public void primeiro_acesso()
        {
            cami.get_configuracoes();
            SqlQuery sql = new SqlQuery(Conexoes.GetSqlConnection());
            sql.Command.Parameters.Clear();
            sql.Command.CommandText = @"INSERT INTO ACESSO_SISTEMA (ID_USUARIO) VALUES (@iduser_atual)";
            sql.Command.Parameters.AddWithValue("@iduser_atual", csUsuario_logado.id_usuario_logado);
            try
            {
                if (sql.Connection.State != ConnectionState.Closed)
                {
                    sql.Connection.Close();
                }
                sql.Connection.Open();
                sql.Command.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sql.Connection.Close();
            } 
        }

        public void registra_ent_sis()
        {
            list_logs.Clear();
            string reg_ent = "";
            consulta.Command.Parameters.Clear();
            consulta.Command.CommandText = @"SELECT * FROM ACESSO_SISTEMA WHERE ID_USUARIO=@iduser_atual";
            consulta.Command.Parameters.AddWithValue("@iduser_atual", csUsuario_logado.id_usuario_logado);
            try
            {
                if (consulta.Connection.State != ConnectionState.Closed)
                {
                    consulta.Connection.Close();
                }
                consulta.Connection.Open();
                SqlDataReader reader = consulta.Command.ExecuteReader();

                while (reader.Read())
                {
                    reg_ent = reader["ACESSO_ENT"].ToString();
                    reg_ent += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "|";

                    if(reader["ACESSO_ENT"].ToString() != "")
                    {
                        string[] log = reg_ent.Split('|');
                        tamanho_log = reg_ent.Split('|').Length;
                        

                        for (int i = 0; i < tamanho_log-1; i++)
                        {
                            list_logs.Add(DateTime.Parse(log[i]));
                        }
                    } 
                }
                reader.Close();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                consulta.Connection.Close();
            }

            try
            {
                consulta.Command.Parameters.Clear();
                consulta.Command.CommandText =
                    @"UPDATE ACESSO_SISTEMA SET ACESSO_ENT=@acesso WHERE ID_USUARIO=@iduser_atual";
                consulta.Command.Parameters.AddWithValue("@iduser_atual", csUsuario_logado.id_usuario_logado);

                if (tamanho_log > 50)
                {
                    reg_ent = "";
                    for (int d = 0; d < 49; d++)
                    {

                        reg_ent += list_logs[d + 1].ToString();
                        reg_ent += "|";
                    }
                }

                consulta.Command.Parameters.AddWithValue("@acesso", reg_ent);
            }
            catch
            {

            }

            try
            {
                if (consulta.Connection.State != ConnectionState.Closed)
                {
                    consulta.Connection.Close();
                }
                consulta.Connection.Open();
                consulta.Command.ExecuteNonQuery();
                consulta.Command.Parameters.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                consulta.Command.Parameters.Clear();
                consulta.Connection.Close();
            }
        }

        public void registra_sai_sis()
        {
            list_logs.Clear();
            string reg_sai = "";
            consulta.Command.Parameters.Clear();
            consulta.Command.CommandText = @"SELECT * FROM ACESSO_SISTEMA WHERE ID_USUARIO=@iduser_atual";
            consulta.Command.Parameters.AddWithValue("@iduser_atual", csUsuario_logado.id_usuario_logado);
            try
            {
                if (consulta.Connection.State != ConnectionState.Closed)
                {
                    consulta.Connection.Close();
                }
                consulta.Connection.Open();
                SqlDataReader reader = consulta.Command.ExecuteReader();

                while (reader.Read())
                {
                    reg_sai = reader["ACESSO_SAI"].ToString();
                    reg_sai += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "|";

                    if (reader["ACESSO_SAI"].ToString() != "")
                    {
                        string[] log = reg_sai.Split('|');
                        tamanho_log = reg_sai.Split('|').Length;


                        for (int i = 0; i < tamanho_log - 1; i++)
                        {
                            list_logs.Add(DateTime.Parse(log[i]));
                        }
                    }
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                consulta.Connection.Close();
            }

            try
            {
                consulta.Command.Parameters.Clear();
                consulta.Command.CommandText =
                    @"UPDATE ACESSO_SISTEMA SET ACESSO_SAI=@acesso WHERE ID_USUARIO=@iduser_atual";
                consulta.Command.Parameters.AddWithValue("@iduser_atual", csUsuario_logado.id_usuario_logado);

                if (tamanho_log > 50)
                {
                    reg_sai = "";
                    for (int d = 0; d < 49; d++)
                    {

                        reg_sai += list_logs[d + 1].ToString();
                        reg_sai += "|";
                    }
                }

                consulta.Command.Parameters.AddWithValue("@acesso", reg_sai);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            try
            {
                if (consulta.Connection.State != ConnectionState.Closed)
                {
                    consulta.Connection.Close();
                }
                consulta.Connection.Open();
                consulta.Command.ExecuteNonQuery();
                consulta.Command.Parameters.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            finally
            {

                consulta.Command.Parameters.Clear();
                consulta.Connection.Close();
            }
        }

        //public void get_n_user()
        //{
        //    cami.get_configuracoes();
        //    consulta.Command.Parameters.Clear();
        //    consulta.Command.CommandText =
        //        @"SELECT * FROM USUARIO WHERE ID_USUARIO=@iduser_atual";

        //    consulta.Command.Parameters.AddWithValue("@iduser_atual", csUsuario_logado.id_usuario_logado);

        //    try
        //    {
        //        if (consulta.Connection.State != ConnectionState.Closed)
        //        {
        //            consulta.Connection.Close();
        //        }

        //        consulta.Connection.Open();
        //        SqlDataReader reader = consulta.Command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Usuarios_Grupos.csUsuario_logado.nome_usuario_logado = reader["NOME"].ToString();
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        consulta.Connection.Close();

        //    }
        //}

        //public void get_n_grupo()
        //{
        //    cami.get_configuracoes();
        //    consulta.Command.Parameters.Clear();
        //    consulta.Command.CommandText =
        //        @"SELECT * FROM grupos WHERE ID_GRUPO=@idgrupo_atual";

        //    consulta.Command.Parameters.AddWithValue("@idgrupo_atual", csUsuario_logado.id_grupo_logado);

        //    try
        //    {
        //        if (consulta.Connection.State != ConnectionState.Closed)
        //        {
        //            consulta.Connection.Close();
        //        }

        //        consulta.Connection.Open();
        //        SqlDataReader reader = consulta.Command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Usuarios_Grupos.csUsuario_logado.id_grupo_logado = reader["GRUPO"].ToString();
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        consulta.Connection.Close();

        //    }
        //}

        public void alterar_senha()
        {
            cami.get_configuracoes();
            consulta.Command.Parameters.Clear();
            consulta.Command.CommandText =
                @"SELECT * FROM USUARIO WHERE ID_USUARIO=@iduser_atual";

            consulta.Command.Parameters.AddWithValue("@iduser_atual", csUsuario_logado.id_usuario_logado);

            try
            {
                if (consulta.Connection.State != ConnectionState.Closed)
                {
                    consulta.Connection.Close();
                }

                consulta.Connection.Open();
                SqlDataReader reader = consulta.Command.ExecuteReader();
                while (reader.Read())
                {
                    psw_atual = reader["PSW"].ToString();
                }
                reader.Close();
            }

            catch (Exception ex)
            {
                string erro = ex.Message;
            }
            finally
            {
                consulta.Connection.Close();
            }

            if (psw_confirma == psw_atual)
            {
                consulta.Command.Parameters.Clear();
                consulta.Command.CommandText =
                    @"UPDATE USUARIO SET
                PSW=@psw               
                WHERE ID_USUARIO=@iduser_atual";

                consulta.Command.Parameters.AddWithValue("@iduser_atual", csUsuario_logado.id_usuario_logado);
                consulta.Command.Parameters.AddWithValue("@psw", psw_new);

                try
                {
                    if (consulta.Connection.State != ConnectionState.Closed)
                    {
                        consulta.Connection.Close();
                    }
                    consulta.Connection.Open();
                    consulta.Command.ExecuteNonQuery();
                    MessageBox.Show("Senha Alterada com Sucesso!", "Alteração de Senha");
                    ok = 1;
                    consulta.Command.Parameters.Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    consulta.Command.Parameters.Clear();
                    consulta.Connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Senha atual Incorreta", "Erro ao Alterar Senha");
            }

        }

    }
}
