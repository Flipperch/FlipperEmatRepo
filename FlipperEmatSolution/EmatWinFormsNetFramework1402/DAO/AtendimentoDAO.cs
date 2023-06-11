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
    class AtendimentoDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Carregar Atendimento pelo código - Disciplina será carregado aqui dentro
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Atendimento Consultar(int codigo)
        {
            Atendimento atendimento;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ATENDIMENTOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", codigo);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    atendimento = new Atendimento();
                    atendimento.Codigo = codigo;
                    atendimento.Nome = reader["NOME"].ToString();
                    atendimento.Mencao = Convert.ToBoolean(reader["MENCAO"].ToString());
                    atendimento.Ativo = Convert.ToBoolean(reader["ATIVO"].ToString());
                    atendimento.Ordem = Convert.ToInt32(reader["ORDEM"].ToString());
                    return atendimento;
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
        public static List<Atendimento> ExibirTodos(Disciplina disciplina)
        {
            List<Atendimento> listaAtendimento = new List<Atendimento>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ATENDIMENTOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA", disciplina.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    Atendimento atendimento = new Atendimento();
                    atendimento.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());
                    atendimento.Nome = reader["NOME"].ToString();
                    atendimento.Disciplina = disciplina;
                    atendimento.Mencao = Convert.ToBoolean(reader["MENCAO"].ToString());
                    atendimento.Ativo = Convert.ToBoolean(reader["ATIVO"].ToString());
                    listaAtendimento.Add(atendimento);
                }
                return listaAtendimento;
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
        public static int Gravar(Atendimento atendimento)
        {
            try
            {
                if (Consultar(atendimento.Codigo) == null)
                    Inserir(atendimento);
                else
                    Atualizar(atendimento);

                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
        }
        public static int Inserir(Atendimento atendimento)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ATENDIMENTOInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@NOME", atendimento.Nome);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA", atendimento.Disciplina.Codigo);
            sql_comm.Parameters.AddWithValue("@MENCAO", atendimento.Mencao);
            sql_comm.Parameters.AddWithValue("@ATIVO", atendimento.Ativo);
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
        public static int Atualizar(Atendimento atendimento)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ATENDIMENTOUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", atendimento.Codigo);
            sql_comm.Parameters.AddWithValue("@NOME", atendimento.Nome);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA", atendimento.Disciplina.Codigo);
            sql_comm.Parameters.AddWithValue("@MENCAO", atendimento.Mencao);
            sql_comm.Parameters.AddWithValue("@ATIVO", atendimento.Ativo);
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
        public static int Excluir(Atendimento atendimento)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ATENDIMENTODelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("CODIGO", atendimento.Codigo);
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
