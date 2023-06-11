using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmatWinFormsNetFramework1402.Classes;
using System.Data.SqlClient;
using System.Data;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.DAO
{
    class PaisDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Pais Consultar(int codigo)
        {
            Pais pais = new Pais();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_PAISSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    pais.Codigo = codigo;
                    pais.Nome = reader["NOME"].ToString();
                    return pais;
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

        public static Pais Consultar(string nome)
        {
            Pais pais = new Pais();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("SELECT * FROM PAIS WHERE NOME = @NOME", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@NOME", nome);
            sql_comm.CommandType = CommandType.Text;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    pais.Codigo = Convert.ToInt32(reader["CODIGO"]);
                    pais.Nome = reader["NOME"].ToString();
                    return pais;
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

        public static List<Pais> ExibirTodos()
        {
            List<Pais> listaPais = new List<Pais>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_PAISSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    Pais pais = new Pais();
                    pais.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());
                    pais.Nome = reader["NOME"].ToString();
                    listaPais.Add(pais);
                }
                return listaPais;

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
        public static int Gravar(Pais pais)
        {
            try
            {
                if (Consultar(pais.Codigo) != null)
                    Inserir(pais);
                else
                    Atualizar(pais);

                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }

        }
        public static int Inserir(Pais pais)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_PAISInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@NOME", pais.Nome);
            sql_comm.CommandType = CommandType.StoredProcedure;

            try
            {
                sqlHelper.ematConn.Open();
                retorno = (Byte)sql_comm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                //ErrorLog.csControle_erros.ExibirErroMessBox(ex.Message);
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
            return retorno;
        }
        public static int Atualizar(Pais pais)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_PAISUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", pais.Codigo);
            sql_comm.Parameters.AddWithValue("@NOME", pais.Nome);
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
        public static int Excluir(Pais pais)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_PAISDelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("CODIGO", pais.Codigo);
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
