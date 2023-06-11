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
    class DisciplinaDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Disciplina Consultar(int codigo)
        {
            Disciplina disciplina = new Disciplina();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DISCIPLINASelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read() == true)
                {
                    disciplina.Codigo = codigo;
                    disciplina.Nome = reader["NOME"].ToString();
                    disciplina.NomeHistorico = reader["NOME_HISTORICO"].ToString();
                    disciplina.Horario = reader["HORARIO"].ToString();
                    disciplina.Capacidade = Convert.ToInt32(reader["CAPACIDADE"].ToString());
                    disciplina.Ordem = Convert.ToInt32(reader["ORDEM"].ToString());
                    disciplina.BloqAtribuicao = Convert.ToBoolean(reader["BLOQ_ATRIBUICAO"].ToString());
                    disciplina.ListaAtendimento = AtendimentoDAO.ExibirTodos(disciplina); 
                    return disciplina;
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
        public static List<Disciplina> ExibirTodos()
        {
            List<Disciplina> listaDisciplina = new List<Disciplina>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DISCIPLINASelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read() == true)
                {
                    Disciplina disciplina = new Disciplina();
                    disciplina.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());
                    disciplina.Nome = reader["NOME"].ToString();
                    disciplina.NomeHistorico = reader["NOME_HISTORICO"].ToString();
                    disciplina.Horario = reader["HORARIO"].ToString();
                    disciplina.Capacidade = Convert.ToInt32(reader["CAPACIDADE"].ToString());
                    disciplina.Ordem = Convert.ToInt32(reader["ORDEM"].ToString());
                    disciplina.BloqAtribuicao = Convert.ToBoolean(reader["BLOQ_ATRIBUICAO"].ToString());
                    listaDisciplina.Add(disciplina);
                }
                return listaDisciplina;
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
        public static int Gravar(Disciplina disciplina)
        {
            try
            {
                if (Consultar(disciplina.Codigo) == null)
                    Inserir(disciplina);
                else
                    Atualizar(disciplina);

                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
        }
        public static int Inserir(Disciplina disciplina)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DISCIPLINAInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@NOME", disciplina.Nome);
            sql_comm.Parameters.AddWithValue("@NOME_HISTORICO", disciplina.NomeHistorico);
            sql_comm.Parameters.AddWithValue("@HORARIO", disciplina.Horario);
            sql_comm.Parameters.AddWithValue("@CAPACIDADE", disciplina.Capacidade);
            sql_comm.Parameters.AddWithValue("@ORDEM", disciplina.Ordem);
            sql_comm.Parameters.AddWithValue("@BLOQ_ATRIBUICAO", disciplina.BloqAtribuicao);
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
        public static int Atualizar(Disciplina disciplina)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DISCIPLINAUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", disciplina.Codigo);
            sql_comm.Parameters.AddWithValue("@NOME", disciplina.Nome);
            sql_comm.Parameters.AddWithValue("@NOME_HISTORICO", disciplina.NomeHistorico);
            sql_comm.Parameters.AddWithValue("@HORARIO", disciplina.Horario);
            sql_comm.Parameters.AddWithValue("@CAPACIDADE", disciplina.Capacidade);
            sql_comm.Parameters.AddWithValue("@ORDEM", disciplina.Ordem);
            sql_comm.Parameters.AddWithValue("@BLOQ_ATRIBUICAO", disciplina.BloqAtribuicao);
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
        public static int Excluir(Disciplina disciplina)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DISCIPLINADelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("CODIGO", disciplina.Codigo);
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
        public static List<Disciplina> ExibirTodos(int codigoArea)
        {
            List<Disciplina> listaDisciplina = new List<Disciplina>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("SELECT DISCIPLINA.CODIGO, DISCIPLINA.NOME, DISCIPLINA.NOME_HISTORICO, DISCIPLINA.HORARIO, DISCIPLINA.CAPACIDADE, DISCIPLINA.ORDEM, DISCIPLINA.BLOQ_ATRIBUICAO " +
                "FROM DISCIPLINA JOIN AREA_DISCIPLINA " +
                "ON DISCIPLINA.CODIGO = AREA_DISCIPLINA.COD_DISCIPLINA " +
                "WHERE COD_AREA = @codigoArea", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@codigoArea", codigoArea);
            sql_comm.CommandType = CommandType.Text;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read() == true)
                {
                    Disciplina disciplina = new Disciplina();
                    disciplina.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());
                    disciplina.Nome = reader["NOME"].ToString();
                    disciplina.NomeHistorico = reader["NOME_HISTORICO"].ToString();
                    disciplina.Horario = reader["HORARIO"].ToString();
                    disciplina.Capacidade = Convert.ToInt32(reader["CAPACIDADE"].ToString());
                    disciplina.Ordem = Convert.ToInt32(reader["ORDEM"].ToString());
                    disciplina.BloqAtribuicao = Convert.ToBoolean(reader["BLOQ_ATRIBUICAO"].ToString());
                    listaDisciplina.Add(disciplina);
                }
                return listaDisciplina;
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
    }
}
