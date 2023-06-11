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
    class EnsinoAlunoDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static EnsinoAluno Consultar(int codigo, bool carregarAgregacoes=true)
        {
            EnsinoAluno ensinoAluno;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ENSINO_ALUNOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", codigo);
            sql_comm.Parameters.AddWithValue("@N_MAT", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    ensinoAluno = new EnsinoAluno();
                    ensinoAluno.Codigo = codigo;
                    ensinoAluno.Ensino = (Enumeradores.Ensino)(Convert.ToInt32(reader["COD_ENSINO"]));
                    ensinoAluno.Atual = Convert.ToBoolean(reader["ATUAL"].ToString());
                    ensinoAluno.DtInicio =  reader["DT_INICIO"].ToString();
                    if (reader["DT_INICIO"] != DBNull.Value)
                    {
                        ensinoAluno.DtTermino = reader["DT_TERMINO"].ToString();
                    }
                    else
                    {
                        ensinoAluno.DtTermino = null;
                    }

                    if (carregarAgregacoes)
                    {
                        ensinoAluno.HistoricoEscolar = DAO.HistoricoEscolarDAO.Consultar(ensinoAluno);
                        ensinoAluno.ListaDisciplinaAluno = DAO.DisciplinaAlunoDAO.ExibirTodos(ensinoAluno, true);
                        ensinoAluno.ListaMovimentacao = null;//DAO.InativacaoAlunoDAO.ExibirTodos();
                        ensinoAluno.ListaRematricula = DAO.RematriculaDAO.ExibirTodos(ensinoAluno);
                        ensinoAluno.AtividadeExtra = AtividadeExtraDAO.Consultar(ensinoAluno);
                    }
                    

                    return ensinoAluno;
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

        public static bool Existe(int codigo)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ENSINO_ALUNOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", codigo);
            sql_comm.Parameters.AddWithValue("@N_MAT", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return false;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }

        public static List<EnsinoAluno> ExibirTodos(Aluno aluno, bool agregacoes=true) //CARREGA APENAS PELO N_MATRICULA
        {
            List<EnsinoAluno> listaEnsinoAluno = new List<EnsinoAluno>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ENSINO_ALUNOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while(reader.Read())
                {
                    EnsinoAluno ensinoAluno = new EnsinoAluno();
                    ensinoAluno.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());

                    ensinoAluno.Aluno = aluno; //Bidirecional no "ExibirTodos()"

                    ensinoAluno.Ensino = (Enumeradores.Ensino)(Convert.ToInt32(reader["COD_ENSINO"]));
                    ensinoAluno.Atual = Convert.ToBoolean(reader["ATUAL"].ToString());
                    ensinoAluno.DtInicio = reader["DT_INICIO"].ToString();
                    ensinoAluno.DtTermino = reader["DT_TERMINO"].ToString();
                    if (agregacoes)
                    {
                        ensinoAluno.ListaDisciplinaAluno = DisciplinaAlunoDAO.ExibirTodos(ensinoAluno, true);
                        ensinoAluno.AtividadeExtra = AtividadeExtraDAO.Consultar(ensinoAluno);
                        ensinoAluno.ListaRematricula = DAO.RematriculaDAO.ExibirTodos(ensinoAluno);
                        ensinoAluno.HistoricoEscolar = DAO.HistoricoEscolarDAO.Consultar(ensinoAluno);
                        ensinoAluno.ListaMovimentacao = DAO.MovimentacaoDAO.ExibirTodos(ensinoAluno);
                    }
                    listaEnsinoAluno.Add(ensinoAluno);
                }
                return listaEnsinoAluno;
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
                if (Existe(ensinoAluno.Codigo))
                {
                    return Atualizar(ensinoAluno);
                }                    
                else
                {
                    return Inserir(ensinoAluno);
                }
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
            SqlCommand sql_comm = new SqlCommand("usp_ENSINO_ALUNOInsert", sqlHelper.ematConn);            
            sql_comm.Parameters.AddWithValue("@N_MAT", ensinoAluno.Aluno.NMatricula);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO", ensinoAluno.Ensino);
            sql_comm.Parameters.AddWithValue("@ATUAL", ensinoAluno.Atual);
            sql_comm.Parameters.AddWithValue("@DT_INICIO", DateTime.Now);
            sql_comm.Parameters.AddWithValue("@DT_TERMINO", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                retorno = (int)sql_comm.ExecuteScalar();

                ensinoAluno.Codigo = retorno;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

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
            SqlCommand sql_comm = new SqlCommand("usp_ENSINO_ALUNOUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", ensinoAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@N_MAT", ensinoAluno.Aluno.NMatricula);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO", ensinoAluno.Ensino);
            sql_comm.Parameters.AddWithValue("@ATUAL", ensinoAluno.Atual);
            DateTime dataInicio;
            if (DateTime.TryParse(ensinoAluno.DtInicio, out dataInicio))
            {
                //string a = dataInicio.ToString("yyyy-MM-dd hh:mm:ss");
                sql_comm.Parameters.AddWithValue("@DT_INICIO", dataInicio);
            }
            else
            {
                sql_comm.Parameters.AddWithValue("@DT_INICIO", DBNull.Value);
            }
            DateTime dataTermino;
            if (DateTime.TryParse(ensinoAluno.DtTermino, out dataTermino) && ensinoAluno.DtTermino != "01/01/1900 00:00:00")
            {
                //string a = dataTermino.ToString("yyyy-MM-dd hh:mm:ss");
                sql_comm.Parameters.AddWithValue("@DT_TERMINO", dataTermino);
            }
            else
            {
                sql_comm.Parameters.AddWithValue("@DT_TERMINO", DBNull.Value);
            }
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
            SqlCommand sql_comm = new SqlCommand("usp_ENSINO_ALUNODelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("CODIGO", ensinoAluno.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                retorno = (int)sql_comm.ExecuteScalar();

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

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
