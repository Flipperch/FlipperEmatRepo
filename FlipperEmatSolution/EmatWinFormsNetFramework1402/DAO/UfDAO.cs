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
    class UfDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Uf Consultar(int codigo)
        {
            Uf uf = new Uf();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_UFSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", codigo);
            sql_comm.Parameters.AddWithValue("@COD_PAIS", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read() == true)
                {
                    uf.Codigo = codigo;
                    uf.Nome = reader["NOME"].ToString();
                    uf.Sigla = reader["SIGLA"].ToString();
                    uf.Pais = PaisDAO.Consultar(Convert.ToInt32(reader["COD_PAIS"].ToString()));
                    return uf;
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

        public static List<Uf> ExibirTodos(Pais pais)
        {
            List<Uf> listaUf = new List<Uf>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_UFSelect", sqlHelper.ematConn);

            try
            {
                sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
                sql_comm.Parameters.AddWithValue("@COD_PAIS", pais.Codigo);
                sql_comm.CommandType = CommandType.StoredProcedure;

                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    Uf uf = new Uf();
                    uf.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());
                    uf.Nome = reader["NOME"].ToString();
                    uf.Sigla = reader["SIGLA"].ToString();
                    uf.Pais = pais;
                    listaUf.Add(uf);
                }
                return listaUf;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);//ex, "UfDAO.ExibirTodos");
                return null;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }

        public static int Gravar(Uf uf)
        {
            try
            {
                if (Consultar(uf.Codigo) == null)
                    Inserir(uf);
                else
                    Atualizar(uf);

                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
        }

        public static int Inserir(Uf uf)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_UFInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@NOME", uf.Nome);
            sql_comm.Parameters.AddWithValue("@SIGLA", uf.Sigla);
            sql_comm.Parameters.AddWithValue("@COD_PAIS", uf.Pais.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                retorno = (Int16)sql_comm.ExecuteScalar();
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

        public static int Atualizar(Uf uf)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_UFUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", uf.Codigo);
            sql_comm.Parameters.AddWithValue("@NOME", uf.Nome);
            sql_comm.Parameters.AddWithValue("@SIGLA", uf.Sigla);
            sql_comm.Parameters.AddWithValue("@COD_PAIS", uf.Pais.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                retorno = (Int16)sql_comm.ExecuteScalar();

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

        public static int Excluir(Uf uf)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_UFDelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("CODIGO", uf.Codigo);
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
