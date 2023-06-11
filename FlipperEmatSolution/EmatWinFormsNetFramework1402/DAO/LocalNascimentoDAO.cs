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
    class LocalNascimentoDAO
    {
        public static LocalNascimento Consultar(Aluno aluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_LOCAL_NASCIMENTOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read() == true)
                {
                    LocalNascimento localNascimento = new LocalNascimento();
                    if (reader["COD_CIDADE"] != DBNull.Value)
                        localNascimento.Cidade = CidadeDAO.Consultar(Convert.ToInt16(reader["COD_CIDADE"].ToString()));
                    else
                        return null;
                    return localNascimento;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                throw ex;
            }
        }

        public static void Inserir(Aluno aluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_LOCAL_NASCIMENTOInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);


            if (aluno.LocalNascimento.Cidade.Codigo != 0)
            {
                sql_comm.Parameters.AddWithValue("@COD_CIDADE", aluno.LocalNascimento.Cidade.Codigo);
            }
            else
            {
                throw new Exception();
            }

            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                sql_comm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }

        public static void Atualizar(Aluno aluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            try
            {
                SqlCommand sql_comm = new SqlCommand("usp_LOCAL_NASCIMENTOUpdate", sqlHelper.ematConn);
                sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
                if (aluno.LocalNascimento.Cidade.Codigo == 0)
                    sql_comm.Parameters.AddWithValue("@COD_CIDADE", DBNull.Value);
                else
                    sql_comm.Parameters.AddWithValue("@COD_CIDADE", aluno.LocalNascimento.Cidade.Codigo);
                sql_comm.CommandType = CommandType.StoredProcedure;
                sqlHelper.ematConn.Open();
                sql_comm.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }

        public static void Excluir(Aluno aluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            try
            {
                SqlCommand sql_comm = new SqlCommand("usp_LOCAL_NASCIMENTODelete", sqlHelper.ematConn);
                sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
                sql_comm.CommandType = CommandType.StoredProcedure;
                sqlHelper.ematConn.Open();
                sql_comm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }
    }
}
