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
    class CidadeDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Cidade Consultar(Int16 codigo)
        {
            Cidade cidade = new Cidade();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_CIDADESelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", (Int16)codigo);
            sql_comm.Parameters.AddWithValue("@COD_UF", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    cidade.Codigo = codigo;
                    cidade.Nome = reader["NOME"].ToString();
                    cidade.Uf = UfDAO.Consultar(Convert.ToInt32(reader["COD_UF"].ToString()));
                }
                return cidade;
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
        public static List<Cidade> ExibirTodos(Uf uf)
        {
            List<Cidade> listaCidade = new List<Cidade>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_CIDADESelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
            sql_comm.Parameters.AddWithValue("@COD_UF", uf.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    Cidade cidade = new Cidade();
                    cidade.Codigo = Convert.ToInt16(reader["CODIGO"].ToString());
                    cidade.Nome = reader["NOME"].ToString();
                    cidade.Uf = uf;
                    listaCidade.Add(cidade);
                    
                }
                return listaCidade;
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
        public static int Gravar(Cidade cidade)
        {
            try
            {
                if (Consultar(cidade.Codigo) == null)
                    Inserir(cidade);
                else
                    Atualizar(cidade);

                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
        }
        public static int Inserir(Cidade cidade)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_CIDADEInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@NOME", cidade.Nome);
            sql_comm.Parameters.AddWithValue("@COD_UF", cidade.Uf.Codigo);
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
        public static int Atualizar(Cidade cidade)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_CIDADEUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", cidade.Codigo);
            sql_comm.Parameters.AddWithValue("@NOME", cidade.Nome);
            sql_comm.Parameters.AddWithValue("@COD_UF", cidade.Uf.Codigo);
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
        public static int Excluir(Cidade cidade)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_CIDADEDelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("CODIGO", cidade.Codigo);
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
    }
}
