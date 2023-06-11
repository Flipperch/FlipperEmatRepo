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
    class AreaDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Area Consultar(int codigo)
        {
            Area area = new Area();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_AREASelect", sqlHelper.ematConn);
            
            try
            {
                sql_comm.Parameters.AddWithValue("@CODIGO", codigo);
                sql_comm.CommandType = CommandType.StoredProcedure;
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if(reader.Read() == true)
                {
                    area.Codigo = codigo;
                    area.ListaDisciplina = DisciplinaDAO.ExibirTodos(codigo);
                    area.Nome = reader["NOME"].ToString();
                    return area;
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
        public static List<Area> ExibirTodos()
        {
            List<Area> listaArea = new List<Area>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_AREASelect", sqlHelper.ematConn);
            
            try
            {
                sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
                sql_comm.CommandType = CommandType.StoredProcedure;
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read() == true)
                {
                    Area area = new Area();
                    area.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());
                    area.Nome = reader["NOME"].ToString();
                    //area.ListaDisciplina = reader[""].ToString();
                    listaArea.Add(area);
                }
                return listaArea;
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
        public static int Gravar(Area area)
        {
            try
            {
                if (Consultar(area.Codigo) == null)
                    Inserir(area);
                else
                    Atualizar(area);

                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }            
        }
        public static int Inserir(Area area)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_AREAInsert", sqlHelper.ematConn);            
            try
            {
                sql_comm.Parameters.AddWithValue("@NOME", area.Nome);
                sql_comm.CommandType = CommandType.StoredProcedure;
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
        public static int Atualizar(Area area)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_AREAUpdate", sqlHelper.ematConn);
            try
            {
                sql_comm.Parameters.AddWithValue("@CODIGO", area.Codigo);
                sql_comm.Parameters.AddWithValue("@NOME", area.Nome);
                sql_comm.CommandType = CommandType.StoredProcedure;
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
        public static int Excluir(Area area)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_AREADelete", sqlHelper.ematConn);
            try
            {
                sql_comm.Parameters.AddWithValue("CODIGO", area.Codigo);
                sql_comm.CommandType = CommandType.StoredProcedure;
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
