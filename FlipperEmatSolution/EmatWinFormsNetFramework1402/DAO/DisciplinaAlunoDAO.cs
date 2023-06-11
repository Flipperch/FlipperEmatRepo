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
    class DisciplinaAlunoDAO
    {
        public static DisciplinaAluno Consultar(int codigo)
        {
            DisciplinaAluno disciplinaAluno;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DISCIPLINA_ALUNOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", codigo);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    disciplinaAluno = new DisciplinaAluno();
                    disciplinaAluno.Codigo = codigo;
                    disciplinaAluno.Atual = Convert.ToBoolean(reader["ATUAL"].ToString());
                    disciplinaAluno.Concluida = Convert.ToBoolean(reader["CONCLUIDA"].ToString());
                    disciplinaAluno.ListaAtendimentoAluno = AtendimentoAlunoDAO.ExibirTodos(disciplinaAluno);                     
                    disciplinaAluno.Disciplina = DisciplinaDAO.Consultar(Convert.ToInt32(reader["COD_DISCIPLINA"].ToString()));
                    disciplinaAluno.Media = MediaDAO.Consultar(disciplinaAluno);
                    return disciplinaAluno;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }

        public static List<DisciplinaAluno> ExibirTodos(EnsinoAluno ensinoAluno, bool ordenado=false)
        {
            List<DisciplinaAluno> listaDisciplinaAluno = new List<DisciplinaAluno>();
            SqlHelper sqlHelper = new SqlHelper();
            string command = "";
            if (ordenado)
            {
                command = "usp_DISCIPLINA_ALUNOSelectOrdenadoPeloUltimoAtendimento";
            }
            else
            {
                command = "usp_DISCIPLINA_ALUNOSelect";
            }

            SqlCommand sql_comm = new SqlCommand(command, sqlHelper.ematConn);

            if (ensinoAluno != null)
            {
                sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
                sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", ensinoAluno.Codigo);
            }
            else
            {
                sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
                sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", DBNull.Value);

            }
             
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    DisciplinaAluno disciplinaAluno = new DisciplinaAluno();
                    disciplinaAluno.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());
                    disciplinaAluno.Atual = Convert.ToBoolean(reader["ATUAL"].ToString());
                    disciplinaAluno.Concluida = Convert.ToBoolean(reader["CONCLUIDA"].ToString());
                    disciplinaAluno.ListaAtendimentoAluno = AtendimentoAlunoDAO.ExibirTodos(disciplinaAluno);
                    disciplinaAluno.Disciplina = DisciplinaDAO.Consultar(Convert.ToInt32(reader["COD_DISCIPLINA"].ToString()));
                    disciplinaAluno.Media = MediaDAO.Consultar(disciplinaAluno);
                    disciplinaAluno.EnsinoAluno = ensinoAluno;
                    listaDisciplinaAluno.Add(disciplinaAluno);
                }
                return listaDisciplinaAluno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }

        public static int Gravar(DisciplinaAluno disciplinaAluno)
        {
            try
            {
                if (Consultar(disciplinaAluno.Codigo) == null)
                    return Inserir(disciplinaAluno);
                else
                    return Atualizar(disciplinaAluno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int Inserir(DisciplinaAluno disciplinaAluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DISCIPLINA_ALUNOInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", disciplinaAluno.EnsinoAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA", disciplinaAluno.Disciplina.Codigo);
            sql_comm.Parameters.AddWithValue("@ATUAL", disciplinaAluno.Atual);
            sql_comm.Parameters.AddWithValue("@CONCLUIDA", disciplinaAluno.Concluida);
            sql_comm.CommandType = CommandType.StoredProcedure;

            try
            {
                sqlHelper.ematConn.Open();
                retorno = (int)sql_comm.ExecuteScalar();
                disciplinaAluno.Codigo = retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
            return retorno;
        }

        public static int Atualizar(DisciplinaAluno disciplinaAluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DISCIPLINA_ALUNOUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", disciplinaAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", disciplinaAluno.EnsinoAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA", disciplinaAluno.Disciplina.Codigo);
            sql_comm.Parameters.AddWithValue("@ATUAL", disciplinaAluno.Atual);
            sql_comm.Parameters.AddWithValue("@CONCLUIDA", disciplinaAluno.Concluida);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                retorno = (int)sql_comm.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
            return retorno;
        }

        public static int Excluir(DisciplinaAluno disciplinaAluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_DISCIPLINA_ALUNODelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("CODIGO", disciplinaAluno.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                retorno = (int)sql_comm.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
            return retorno;
        }
    }
}
