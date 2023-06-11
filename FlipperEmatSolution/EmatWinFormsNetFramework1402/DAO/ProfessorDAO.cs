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
    class ProfessorDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Professor Consultar(int codigo)
        {            
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_PROFESSORSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    Usuario usuario = DAO.UsuarioDAO.Consultar(codigo);
                    Professor professor = new Professor();
                    professor.Nome = usuario.Nome.ToString();
                    professor.Senha = usuario.Senha.ToString();
                    professor.Rg = usuario.Rg.ToString();
                    professor.NivelAcesso = usuario.NivelAcesso;
                    professor.Login = usuario.Login.ToString();
                    professor.Ativo = usuario.Ativo;
                    professor.DtNascimento = usuario.DtNascimento;
                    professor.Codigo = codigo;
                    professor.ListaDeficienciaPessoa = usuario.ListaDeficienciaPessoa;
                    if (reader["COD_AREA"] != DBNull.Value)
                        professor.Area = AreaDAO.Consultar(Convert.ToInt32(reader["COD_AREA"].ToString()));
                    professor.Disciplina = DisciplinaDAO.Consultar(Convert.ToInt32(reader["COD_DISCIPLINA"].ToString()));       
                    return professor;
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
        public static List<Professor> ExibirTodos()
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_PROFESSORSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                List<Professor> listaProfessor = new List<Professor>();
                while (reader.Read())
                {
                    Usuario usuario = DAO.UsuarioDAO.Consultar(Convert.ToInt32(reader["CODIGO"].ToString()));
                    Professor professor = new Professor();
                    professor.Nome = usuario.Nome.ToString();
                    professor.Senha = usuario.Senha.ToString();
                    professor.Rg = usuario.Rg.ToString();
                    professor.NivelAcesso = usuario.NivelAcesso;
                    professor.Login = usuario.Login.ToString();
                    professor.Ativo = usuario.Ativo;
                    professor.DtNascimento = usuario.DtNascimento;
                    professor.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());
                    professor.ListaDeficienciaPessoa = usuario.ListaDeficienciaPessoa;
                    if (reader["COD_AREA"] != DBNull.Value)
                        professor.Area = AreaDAO.Consultar(Convert.ToInt32(reader["COD_AREA"].ToString()));
                    professor.Disciplina = DisciplinaDAO.Consultar(Convert.ToInt32(reader["COD_DISCIPLINA"].ToString()));
                    listaProfessor.Add(professor);
                }
                return listaProfessor;
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
        public static int Gravar(Professor professor)
        {
            try
            {
                if (Consultar(professor.Codigo) == null)
                    Inserir(professor);
                else
                    Atualizar(professor);

                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
        }
        public static int Inserir(Professor professor)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_PROFESSORInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", professor.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA", professor.Codigo);
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
        public static int Atualizar(Professor professor)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_PROFESSORUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", professor.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA", professor.Codigo);
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
        public static int Excluir(Professor professor)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_PROFESSORDelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("CODIGO", professor.Codigo);
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
