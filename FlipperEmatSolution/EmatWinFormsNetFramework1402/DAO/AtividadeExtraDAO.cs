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
    class AtividadeExtraDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static AtividadeExtra Consultar(EnsinoAluno EnsinoAluno)
        {
            AtividadeExtra atividadeExtra = new AtividadeExtra();

            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ATIVIDADE_EXTRASelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", EnsinoAluno.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read() == true)
                {
                    atividadeExtra.Usuario = UsuarioDAO.Consultar(Convert.ToInt32(reader["COD_USUARIO"].ToString()));
                    atividadeExtra.Data = DateTime.Parse(reader["DT_ATIVIDADE_EXTRA"].ToString());
                    return atividadeExtra;
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
       
        public static int Gravar(EnsinoAluno ensinoAluno)
        {
            try
            {
                if (Consultar(ensinoAluno) == null)
                    Inserir(ensinoAluno);
                else
                    Atualizar(ensinoAluno);

                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
        }
        public static int Inserir(EnsinoAluno ensinoAluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ATIVIDADE_EXTRAInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", ensinoAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_USUARIO", ensinoAluno.AtividadeExtra.Usuario.Codigo);
            sql_comm.Parameters.AddWithValue("@DT_ATIVIDADE_EXTRA", ensinoAluno.AtividadeExtra.Data);
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
        public static int Atualizar(EnsinoAluno ensinoAluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ATIVIDADE_EXTRAUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", ensinoAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_USUARIO", ensinoAluno.AtividadeExtra.Usuario.Codigo);
            sql_comm.Parameters.AddWithValue("@DT_ATIVIDADE_EXTRA", ensinoAluno.AtividadeExtra.Data);
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
        public static int Excluir(EnsinoAluno ensinoAluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ATIVIDADE_EXTRADelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", ensinoAluno.Codigo);
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
