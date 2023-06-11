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
    class DeficienciaDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Deficiencia Consultar(int codigo)
        {
            Deficiencia deficiencia = new Deficiencia();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DEFICIENCIASelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read() == true)
                {
                    deficiencia.Codigo = codigo;
                    deficiencia.Nome = reader["NOME"].ToString();
                    deficiencia.TipoDeficiencia = (Enumeradores.TipoDeficiencia)reader["TIPO_DEFICIENCIA"];
                    return deficiencia;
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

        public static List<Deficiencia> ExibirTodos()
        {
            List<Deficiencia> listaDeficiencia = new List<Deficiencia>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DEFICIENCIASelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while(reader.Read())
                {
                    Deficiencia deficiencia = new Deficiencia();
                    deficiencia.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());
                    deficiencia.Nome = reader["NOME"].ToString();
                    deficiencia.TipoDeficiencia = (Enumeradores.TipoDeficiencia)Convert.ToInt32(reader["TIPO_DEFICIENCIA"]);
                    listaDeficiencia.Add(deficiencia);
                }
                return listaDeficiencia;
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

        public static int Gravar(Deficiencia deficiencia)
        {
            try
            {
                if (Consultar(deficiencia.Codigo) == null)
                    Inserir(deficiencia);
                else
                    Atualizar(deficiencia);

                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
        }
        public static int Inserir(Deficiencia deficiencia)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DEFICIENCIAInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@NOME", deficiencia.Nome);
            sql_comm.Parameters.AddWithValue("@TIPO_DEFICIENCIA", deficiencia.TipoDeficiencia);
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

        public static int Atualizar(Deficiencia deficiencia)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DEFICIENCIAUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", deficiencia.Codigo);
            sql_comm.Parameters.AddWithValue("@NOME", deficiencia.Nome);
            sql_comm.Parameters.AddWithValue("@TIPO_DEFICIENCIA", deficiencia.TipoDeficiencia);
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

        public static int Excluir(Deficiencia deficiencia)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DEFICIENCIADelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("CODIGO", deficiencia.Codigo);
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
