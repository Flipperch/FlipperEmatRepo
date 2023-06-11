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
    class RematriculaDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Rematricula Consultar(int codigo)
        {
            Rematricula rematricula = new Rematricula();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_REMATRICULASelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", codigo);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    rematricula.Codigo = codigo;
                    rematricula.Data = DateTime.Parse(reader["DT_REMATRICULA"].ToString());
                    rematricula.Usuario = UsuarioDAO.Consultar(Convert.ToInt32(reader["COD_USUARIO"].ToString()));
                    rematricula.EnsinoAluno = null;
                    return rematricula;
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
        public static List<Rematricula> ExibirTodos(EnsinoAluno ensinoAluno)
        {
            List<Rematricula> listaRematricula = new List<Rematricula>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_REMATRICULASelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", ensinoAluno.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    Rematricula rematricula = new Rematricula();
                    rematricula.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());                   
                    rematricula.Data = DateTime.Parse(reader["DT_REMATRICULA"].ToString());
                    rematricula.Usuario = UsuarioDAO.Consultar(Convert.ToInt32(reader["COD_USUARIO"].ToString()));
                    rematricula.EnsinoAluno = ensinoAluno; 
                    listaRematricula.Add(rematricula);
                }
                return listaRematricula;
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
        public static int Gravar(Rematricula rematricula)
        {
            try
            {
                if (Consultar(rematricula.Codigo) == null)
                    Inserir(rematricula);
                else
                    Atualizar(rematricula);

                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
        }
        public static int Inserir(Rematricula rematricula)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_REMATRICULAInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@DT_REMATRICULA", rematricula.Data);
            sql_comm.Parameters.AddWithValue("@COD_USUARIO", rematricula.Usuario.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", rematricula.EnsinoAluno.Codigo);
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
        public static int Atualizar(Rematricula rematricula)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_REMATRICULAUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", rematricula.Codigo);
            sql_comm.Parameters.AddWithValue("@DT_REMATRICULA", rematricula.Data);
            sql_comm.Parameters.AddWithValue("@COD_USUARIO", rematricula.Usuario.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", rematricula.EnsinoAluno.Codigo);
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
        public static int Excluir(Rematricula rematricula)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_REMATRICULADelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("CODIGO", rematricula.Codigo);
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
