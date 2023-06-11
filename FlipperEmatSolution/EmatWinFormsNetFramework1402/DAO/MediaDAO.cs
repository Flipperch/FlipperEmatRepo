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
    class MediaDAO
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Media Consultar(DisciplinaAluno disciplinaAluno)
        {
            Media media;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_MEDIASelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA_ALUNO", disciplinaAluno.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    media = new Media();

                    media.Instituicao = reader["INSTITUICAO"].ToString();
                    media.Cidade = CidadeDAO.Consultar(Convert.ToInt16(reader["COD_CIDADE"].ToString()));
                    media.Valor = reader["VALOR"].ToString();
                    media.DtMedia = reader["DT_MEDIA"].ToString();
                    media.UsuarioCadastro = UsuarioDAO.Consultar(Convert.ToInt32(reader["COD_USUARIO"].ToString()));

                    if (reader["COD_USUARIO_MODIFICACAO"] != DBNull.Value) media.UsuarioDeModificacao = UsuarioDAO.Consultar(Convert.ToInt32(reader["COD_USUARIO_MODIFICACAO"].ToString()));
                    else media.UsuarioDeModificacao = null;

                    if (reader["DT_MODIFICACAO"] != DBNull.Value) media.DtModificacao = reader["DT_MODIFICACAO"].ToString();
                    else media.DtModificacao = null;

                    if (reader["COD_ATENDIMENTO_ALUNO"] != DBNull.Value)
                    {
                        media.AtendimentoAluno = AtendimentoAlunoDAO.Consultar(Convert.ToInt32(reader["COD_ATENDIMENTO_ALUNO"].ToString()));                        
                    }
                    else media.AtendimentoAluno = null;

                    return media;
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
        public static int Gravar(DisciplinaAluno disciplinaAluno)
        {
            try
            {
                if (Consultar(disciplinaAluno) == null)
                    return Inserir(disciplinaAluno);
                else
                    return Atualizar(disciplinaAluno);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
        }
        public static int Inserir(DisciplinaAluno disciplinaAluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_MEDIAInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@INSTITUICAO", disciplinaAluno.Media.Instituicao.ToString());
            sql_comm.Parameters.AddWithValue("@COD_CIDADE", disciplinaAluno.Media.Cidade.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA_ALUNO", disciplinaAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@VALOR", disciplinaAluno.Media.Valor);
            sql_comm.Parameters.AddWithValue("@DT_MEDIA", disciplinaAluno.Media.DtMedia);
            sql_comm.Parameters.AddWithValue("@COD_USUARIO", disciplinaAluno.Media.UsuarioCadastro.Codigo);

            sql_comm.Parameters.AddWithValue("@COD_USUARIO_MODIFICACAO", DBNull.Value);

            sql_comm.Parameters.AddWithValue("@DT_MODIFICACAO", DBNull.Value);

            if (disciplinaAluno.Media.AtendimentoAluno != null) sql_comm.Parameters.AddWithValue("@COD_ATENDIMENTO_ALUNO", disciplinaAluno.Media.AtendimentoAluno.Codigo);
            else sql_comm.Parameters.AddWithValue("@COD_ATENDIMENTO_ALUNO", DBNull.Value);

            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                sql_comm.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                return 0;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }
        public static int Atualizar(DisciplinaAluno disciplinaAluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_MEDIAUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@INSTITUICAO", disciplinaAluno.Media.Instituicao);
            sql_comm.Parameters.AddWithValue("@COD_CIDADE", disciplinaAluno.Media.Cidade.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_DISCIPLINA_ALUNO", disciplinaAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@VALOR", disciplinaAluno.Media.Valor);
            sql_comm.Parameters.AddWithValue("@DT_MEDIA", disciplinaAluno.Media.DtMedia);
            sql_comm.Parameters.AddWithValue("@COD_USUARIO", disciplinaAluno.Media.UsuarioCadastro.Codigo);
            sql_comm.Parameters.AddWithValue("@COD_USUARIO_MODIFICACAO", disciplinaAluno.Media.UsuarioDeModificacao.Codigo);
            sql_comm.Parameters.AddWithValue("@DT_MODIFICACAO", disciplinaAluno.Media.DtModificacao);
            sql_comm.Parameters.AddWithValue("@COD_ATENDIMENTO_ALUNO", disciplinaAluno.Media.AtendimentoAluno);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                return (int)sql_comm.ExecuteScalar();

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }
        public static int Excluir(DisciplinaAluno disciplinaAluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_MEDIADelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("COD_DISCIPLINA_ALUNO", disciplinaAluno.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                sql_comm.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return 0;
            }
            finally
            {
                sqlHelper.ematConn.Close();
            }
        }
    }
}
