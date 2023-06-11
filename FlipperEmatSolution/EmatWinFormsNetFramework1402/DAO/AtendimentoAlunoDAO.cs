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
    class AtendimentoAlunoDAO
    {
        public static AtendimentoAluno Consultar(int codigo)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ATENDIMENTO_ALUNOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", codigo);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA_ALUNO", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    AtendimentoAluno atendimentoAluno = new AtendimentoAluno();
                    atendimentoAluno.Codigo = codigo;
                    atendimentoAluno.Atendimento = AtendimentoDAO.Consultar(Convert.ToInt32(reader["COD_ATENDIMENTO"].ToString()));
                    atendimentoAluno.ProfessorAtribuiuAtendimento = ProfessorDAO.Consultar(Convert.ToInt32(reader["COD_PROFESSOR"].ToString()));
                    atendimentoAluno.DtDoAtendimento = DateTime.Parse(reader["DT_ATENDIMENTO"].ToString());
                    if (reader["COD_PROFESSOR_MODIFICACAO"] != DBNull.Value)
                        atendimentoAluno.ProfessorModificouAtendimento = ProfessorDAO.Consultar(Convert.ToInt32(reader["COD_PROFESSOR_MODIFICACAO"].ToString()));
                    if (reader["DT_MODIFICACAO"] != DBNull.Value)
                        atendimentoAluno.DtDaModificaoAtendimento = DateTime.Parse(reader["DT_MODIFICACAO"].ToString());
                    atendimentoAluno.Modulo = reader["MODULO"].ToString();
                    return atendimentoAluno;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                return null;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }
        public static List<AtendimentoAluno> ExibirTodos(DisciplinaAluno disciplinaAluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ATENDIMENTO_ALUNOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA_ALUNO", disciplinaAluno.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                List<AtendimentoAluno> listaAtendimentoAluno = new List<AtendimentoAluno>();
                while (reader.Read())
                {
                    AtendimentoAluno atendimentoAluno = new AtendimentoAluno();
                    atendimentoAluno.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());
                    atendimentoAluno.DisciplinaAluno = disciplinaAluno;
                    atendimentoAluno.Atendimento = AtendimentoDAO.Consultar(Convert.ToInt32(reader["COD_ATENDIMENTO"].ToString()));
                    atendimentoAluno.ProfessorAtribuiuAtendimento = ProfessorDAO.Consultar(Convert.ToInt32(reader["COD_PROFESSOR"].ToString()));
                    atendimentoAluno.DtDoAtendimento = DateTime.Parse(reader["DT_ATENDIMENTO"].ToString());
                    if(reader["COD_PROFESSOR_MODIFICACAO"]!=DBNull.Value)
                        atendimentoAluno.ProfessorModificouAtendimento = ProfessorDAO.Consultar(Convert.ToInt32(reader["COD_PROFESSOR_MODIFICACAO"].ToString()));
                    if(reader["DT_MODIFICACAO"]!=DBNull.Value)
                        atendimentoAluno.DtDaModificaoAtendimento = DateTime.Parse(reader["DT_MODIFICACAO"].ToString());
                    atendimentoAluno.Modulo = reader["MODULO"].ToString();
                    atendimentoAluno.Nota = DAO.NotaDAO.Consultar(atendimentoAluno.Codigo);
                    listaAtendimentoAluno.Add(atendimentoAluno);
                }
                return listaAtendimentoAluno;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
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
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                return 0;
            }
        }
        public static int Inserir(AtendimentoAluno atendimentoAluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ATENDIMENTO_ALUNOInsert", sqlHelper.ematConn);            
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA_ALUNO", atendimentoAluno.DisciplinaAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_ATENDIMENTO", atendimentoAluno.Atendimento.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_PROFESSOR", atendimentoAluno.ProfessorAtribuiuAtendimento.Codigo);
            sql_comm.Parameters.AddWithValue("@DT_ATENDIMENTO", atendimentoAluno.DtDoAtendimento);
            if(atendimentoAluno.ProfessorModificouAtendimento != null)
                sql_comm.Parameters.AddWithValue("@COD_PROFESSOR_MODIFICACAO", atendimentoAluno.ProfessorModificouAtendimento.Codigo);
            else
                sql_comm.Parameters.AddWithValue("@COD_PROFESSOR_MODIFICACAO", DBNull.Value);

            if(atendimentoAluno.DtDaModificaoAtendimento.ToString("dd/MM/yyyy") != "01/01/0001")
                sql_comm.Parameters.AddWithValue("@DT_MODIFICACAO", atendimentoAluno.DtDaModificaoAtendimento);
            else
                sql_comm.Parameters.AddWithValue("@DT_MODIFICACAO", DBNull.Value);
            sql_comm.Parameters.AddWithValue("@MODULO", atendimentoAluno.Modulo);
            sql_comm.CommandType = CommandType.StoredProcedure;

            try
            {
                sqlHelper.ematConn.Open();
                retorno = (int)sql_comm.ExecuteScalar();
                atendimentoAluno.Codigo = retorno;

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
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
            SqlCommand sql_comm = new SqlCommand("usp_ATENDIMENTO_ALUNOUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", atendimentoAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA_ALUNO", atendimentoAluno.DisciplinaAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_ATENDIMENTO", atendimentoAluno.Atendimento.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_PROFESSOR", atendimentoAluno.ProfessorAtribuiuAtendimento.Codigo);
            sql_comm.Parameters.AddWithValue("@DT_ATENDIMENTO", atendimentoAluno.DtDoAtendimento);
            sql_comm.Parameters.AddWithValue("@COD_PROFESSOR_MODIFICACAO", atendimentoAluno.ProfessorModificouAtendimento.Codigo);
            sql_comm.Parameters.AddWithValue("@DT_MODIFICACAO", atendimentoAluno.DtDaModificaoAtendimento);
            sql_comm.Parameters.AddWithValue("@MODULO", atendimentoAluno.Modulo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                retorno = (int)sql_comm.ExecuteScalar();
                

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
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
            SqlCommand sql_comm = new SqlCommand("usp_ATENDIMENTO_ALUNODelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("CODIGO", atendimentoAluno.Codigo);
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
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
            return retorno;
        }
    }
}
