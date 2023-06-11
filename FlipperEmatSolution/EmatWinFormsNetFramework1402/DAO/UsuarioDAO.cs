using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using EmatWinFormsNetFramework1402.Classes;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.DAO
{
    class UsuarioDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Usuario Consultar(int codigo)
        {
            Usuario usuario;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_USUARIOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                if(sqlHelper.ematConn.State == ConnectionState.Closed)
                    sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    usuario = new Usuario();
                    usuario.Codigo = codigo;
                    usuario.Nome = reader["NOME"].ToString();
                    usuario.Login = reader["NOME_ACESSO"].ToString();
                    usuario.Senha = reader["SENHA"].ToString();
                    usuario.Rg = reader["RG"].ToString();
                    usuario.NivelAcesso = 
                        (Enumeradores.NivelAcesso)Enum.Parse(typeof(Enumeradores.NivelAcesso), 
                        reader["NIVEL_ACESSO"].ToString());
                    usuario.Ativo = Convert.ToBoolean(reader["ATIVO"].ToString());
                    return usuario;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }
        public static List<Usuario> ExibirTodos()
        {
            List<Usuario> listaUsuario = new List<Usuario>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_USUARIOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while(reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());
                    usuario.Nome = reader["NOME"].ToString();
                    usuario.Login = reader["NOME_ACESSO"].ToString();
                    usuario.Senha = reader["SENHA"].ToString();
                    usuario.Rg = reader["RG"].ToString();
                    //usuario.NivelAcesso = (Enumeradores.NivelAcesso)reader["NIVEL_ACESSO"];
                    usuario.NivelAcesso = 
                        (Enumeradores.NivelAcesso)Enum.Parse(typeof(Enumeradores.NivelAcesso),
                        reader["NIVEL_ACESSO"].ToString());
                    usuario.Ativo = Convert.ToBoolean(reader["ATIVO"].ToString());
                    listaUsuario.Add(usuario);
                }
                return listaUsuario;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }
        public static int Gravar(Usuario usuario)
        {
            try
            {
                if (Consultar(usuario.Codigo) == null)
                    Inserir(usuario);
                else
                    Atualizar(usuario);

                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
        }
        public static int Inserir(Usuario usuario)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_USUARIOInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", usuario.Codigo);
            sql_comm.Parameters.AddWithValue("@NOME", usuario.Nome);
            sql_comm.Parameters.AddWithValue("@NOME_ACESSO", usuario.Login);
            sql_comm.Parameters.AddWithValue("@SENHA", usuario.Senha);
            sql_comm.Parameters.AddWithValue("@RG", usuario.Rg);
            sql_comm.Parameters.AddWithValue("@NIVEL_ACESSO", usuario.NivelAcesso);
            sql_comm.Parameters.AddWithValue("@ATIVO", usuario.Ativo);
            sql_comm.CommandType = CommandType.StoredProcedure;

            try
            {
                sqlHelper.ematConn.Open();
                retorno = (int)sql_comm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
            return retorno;
        }
        public static int Atualizar(Usuario usuario)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_USUARIOUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", usuario.Codigo);
            sql_comm.Parameters.AddWithValue("@NOME", usuario.Nome);
            sql_comm.Parameters.AddWithValue("@NOME_ACESSO", usuario.Login);
            sql_comm.Parameters.AddWithValue("@SENHA", usuario.Senha);
            sql_comm.Parameters.AddWithValue("@RG", usuario.Rg);
            sql_comm.Parameters.AddWithValue("@NIVEL_ACESSO", usuario.NivelAcesso);
            sql_comm.Parameters.AddWithValue("@ATIVO", usuario.Ativo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                retorno = (int)sql_comm.ExecuteScalar();

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
            return retorno;
        }
        public static int Excluir(Usuario usuario)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_USUARIODelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", usuario.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                retorno = (int)sql_comm.ExecuteScalar();

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
            return retorno;
        }
    }
}
