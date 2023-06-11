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
    class NotaDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Nota Consultar(int codAtendimentoAluno)
        {
            Nota nota = new Nota();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_NOTASelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@COD_ATENDIMENTO_ALUNO", codAtendimentoAluno);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    if(reader["NOTA"]!=DBNull.Value)
                        nota.Valor = float.Parse(reader["NOTA"].ToString());
                    else
                        nota.Valor = 0;
                    return nota;
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
        public static List<Nota> ExibirTodos()
        {
            List<Nota> listaNota = new List<Nota>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_NOTASelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@COD_ATENDIMENTO_ALUNO", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    Nota nota = new Nota();
                    if (reader["NOTA"] != DBNull.Value)
                        nota.Valor = float.Parse(reader["NOTA"].ToString());
                    else
                        nota.Valor = 0;
                    listaNota.Add(nota);
                }
                return listaNota;
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
        public static int Gravar(AtendimentoAluno atendimentoAluno)
        {
            try
            {
                if (Consultar(atendimentoAluno.Codigo) == null)
                    Inserir(atendimentoAluno);
                else
                    Atualizar(atendimentoAluno);

                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
        }
        public static int Inserir(AtendimentoAluno atendimentoAluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_NOTAInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@COD_ATENDIMENTO_ALUNO", atendimentoAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@NOTA", atendimentoAluno.Nota.Valor);
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
        public static int Atualizar(AtendimentoAluno atendimentoAluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_NOTAUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@COD_ATENDIMENTO_ALUNO", atendimentoAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@NOTA", atendimentoAluno.Nota.Valor);
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
        public static int Excluir(AtendimentoAluno atendimentoAluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_NOTADelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@COD_ATENDIMENTO_ALUNO", atendimentoAluno.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                sql_comm.ExecuteNonQuery();
                retorno = 1;

            }
            catch (Exception ex)
            {
                retorno = 0;
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
