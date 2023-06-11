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
    class EmpregoDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Emprego Consultar(Aluno aluno)
        {
            Emprego emprego = new Emprego();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_EMPREGO_ALUNOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read() == true)
                {
                    emprego.NomeEmpresa = reader["NOME_EMPRESA"].ToString();
                    emprego.Telefone = reader["TELEFONE"].ToString();
                    emprego.Cidade = CidadeDAO.Consultar(Convert.ToInt16(reader["COD_CIDADE"].ToString()));
                    return emprego;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);//ex, "EmpregoDAO.Consultar");
                return null;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }

        public static void Gravar(Aluno aluno)
        {
            try
            {
                if (Consultar(aluno) == null)
                    Inserir(aluno);
                else
                    Atualizar(aluno);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);//ex, "EmpregoDAO.Gravar");
            }
        }

        public static void Inserir(Aluno aluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_EMPREGO_ALUNOInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
            sql_comm.Parameters.AddWithValue("@NOME_EMPRESA", aluno.Emprego.NomeEmpresa);
            sql_comm.Parameters.AddWithValue("@TELEFONE", aluno.Emprego.Telefone);
            if(aluno.Emprego.Cidade.Codigo == 0)
                sql_comm.Parameters.AddWithValue("@COD_CIDADE", DBNull.Value);
            else
                sql_comm.Parameters.AddWithValue("@COD_CIDADE", aluno.Emprego.Cidade.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;

            try
            {
                sqlHelper.ematConn.Open();
                sql_comm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);//ex, "EmpregoDAO.Inserir");
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }

        public static void Atualizar(Aluno aluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_EMPREGO_ALUNOUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
            sql_comm.Parameters.AddWithValue("@NOME_EMPRESA", aluno.Emprego.NomeEmpresa);
            sql_comm.Parameters.AddWithValue("@TELEFONE", aluno.Emprego.Telefone);
            if (aluno.Emprego.Cidade.Codigo == 0)
                sql_comm.Parameters.AddWithValue("@COD_CIDADE", DBNull.Value);
            else
                sql_comm.Parameters.AddWithValue("@COD_CIDADE", aluno.Emprego.Cidade.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                sql_comm.ExecuteScalar();

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);//ex, "EmpregoDAO.Atualizar");
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }

        public static void Excluir(Aluno aluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_EMPREGO_ALUNODelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                sql_comm.ExecuteScalar();

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);//ex, "EmpregoDAO.Excluir");
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }
    }
}
