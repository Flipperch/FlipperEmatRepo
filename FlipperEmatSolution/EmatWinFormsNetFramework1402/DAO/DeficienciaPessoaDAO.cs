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
    class DeficienciaPessoaDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static DeficienciaPessoa Consultar(int codigo)
        {
            DeficienciaPessoa deficienciaPessoa = new DeficienciaPessoa();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DEFICIENCIA_PESSOASelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read() == true)
                {
                    sql_comm.Parameters.AddWithValue("@COD_DEFICIENCIA", deficienciaPessoa.Deficiencia.Codigo);
                    sql_comm.Parameters.AddWithValue("@OBS", deficienciaPessoa.Observacao);
                    return deficienciaPessoa;
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
        public static List<DeficienciaPessoa> ExibirTodos(int n_mat)
        {
            List<DeficienciaPessoa> listaDeficienciaPessoa = new List<DeficienciaPessoa>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DEFICIENCIA_ALUNOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", n_mat);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    DeficienciaPessoa deficienciaPessoa = new DeficienciaPessoa();
                    sql_comm.Parameters.AddWithValue("@COD_DEFICIENCIA", deficienciaPessoa.Deficiencia.Codigo);
                    sql_comm.Parameters.AddWithValue("@OBS", deficienciaPessoa.Observacao);
                    listaDeficienciaPessoa.Add(deficienciaPessoa);
                }
                return listaDeficienciaPessoa;
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
        public static int Gravar(Aluno aluno, DeficienciaPessoa deficienciaPessoa)
        {
            try
            {
                if (Consultar(1) == null)
                    Inserir(aluno, deficienciaPessoa);
                else
                    Atualizar(aluno, deficienciaPessoa);

                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
        }
        public static int Inserir(Aluno aluno, DeficienciaPessoa deficienciaPessoa)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DEFICIENCIA_ALUNOInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
            sql_comm.Parameters.AddWithValue("@COD_DEFICIENCIA", deficienciaPessoa.Deficiencia.Codigo);
            sql_comm.Parameters.AddWithValue("@OBS", deficienciaPessoa.Observacao);
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
        public static int Atualizar(Aluno aluno, DeficienciaPessoa deficienciaPessoa)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DEFICIENCIA_PESSOAUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", aluno.NMatricula);
            sql_comm.Parameters.AddWithValue("@COD_DEFICIENCIA", deficienciaPessoa.Deficiencia.Codigo);
            sql_comm.Parameters.AddWithValue("@OBS", deficienciaPessoa.Observacao);
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
        public static int Excluir(Aluno aluno, DeficienciaPessoa deficienciaPessoa)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DEFICIENCIA_ALUNODelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
            sql_comm.Parameters.AddWithValue("@COD_DEFICIENCIA", deficienciaPessoa.Deficiencia.Codigo);
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