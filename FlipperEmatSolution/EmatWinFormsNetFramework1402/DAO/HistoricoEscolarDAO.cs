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
    class HistoricoEscolarDAO
    {
        public static HistoricoEscolar Consultar(EnsinoAluno ensinoAluno)
        {
            HistoricoEscolar historicoEscolar;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_HISTORICO_ESCOLARSelect", sqlHelper.ematConn);            
            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", ensinoAluno.Codigo);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read() == true)
                {
                    historicoEscolar = new HistoricoEscolar();
                    historicoEscolar.Observacao = reader["OBS"].ToString();
                    historicoEscolar.Diretor = UsuarioDAO.Consultar(Convert.ToInt32(reader["COD_USUARIO_DIRETOR"].ToString()));
                    historicoEscolar.Secretario = UsuarioDAO.Consultar(Convert.ToInt32(reader["COD_USUARIO_SECRETARIO"].ToString()));
                    historicoEscolar.DtDocumento = reader["DT_DOCUMENTO"].ToString();
                    historicoEscolar.DtConclusao = reader["DT_CONCLUSAO"].ToString();
                    historicoEscolar.SerieAnterior = reader["SERIE_ANTERIOR"].ToString();
                    historicoEscolar.InstituicaoAnterior = reader["INSTITUICAO_ANTERIOR"].ToString();
                    if (reader["ANO_ANTERIOR"] != DBNull.Value)
                        historicoEscolar.AnoAnterior = Convert.ToInt32(reader["ANO_ANTERIOR"].ToString());
                    else
                        historicoEscolar.AnoAnterior = 0;
                    if (reader["COD_CIDADE_ANTERIOR"] != DBNull.Value)
                        historicoEscolar.CidadeAnterior = CidadeDAO.Consultar(Convert.ToInt16(reader["COD_CIDADE_ANTERIOR"].ToString()));
                    historicoEscolar.Fundamentacao = reader["FUNDAMENTACAO"].ToString();
                    historicoEscolar.Gdae = reader["GDAE"].ToString();
                    historicoEscolar.Usuario = UsuarioDAO.Consultar(Convert.ToInt32(reader["COD_USUARIO"].ToString()));
                    //historicoEscolar.SegundaVia = Boolean.Parse(reader["SEGUNDA_VIA"].ToString());
                    return historicoEscolar;
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

        public static int Gravar(EnsinoAluno ensinoAluno)
        {
            try
            {
                if (Consultar(ensinoAluno) == null)
                    Inserir(ensinoAluno);
                else
                    Atualizar(ensinoAluno);

                return 1;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public static int Inserir(EnsinoAluno ensinoAluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_HISTORICO_ESCOLARInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", ensinoAluno.Codigo); //WHERE
            sql_comm.CommandType = CommandType.StoredProcedure;

            if (!String.IsNullOrEmpty(ensinoAluno.HistoricoEscolar.Observacao))
                sql_comm.Parameters.AddWithValue("@OBS", ensinoAluno.HistoricoEscolar.Observacao);
            else
                sql_comm.Parameters.AddWithValue("@OBS", DBNull.Value);

            if (ensinoAluno.HistoricoEscolar.Diretor != null)
                sql_comm.Parameters.AddWithValue("@COD_USUARIO_DIRETOR", ensinoAluno.HistoricoEscolar.Diretor.Codigo);
            else
                sql_comm.Parameters.AddWithValue("@COD_USUARIO_DIRETOR", DBNull.Value);

            if (ensinoAluno.HistoricoEscolar.Secretario != null)
                sql_comm.Parameters.AddWithValue("@COD_USUARIO_SECRETARIO", ensinoAluno.HistoricoEscolar.Secretario.Codigo);
            else
                sql_comm.Parameters.AddWithValue("@COD_USUARIO_SECRETARIO", DBNull.Value);

            if (!String.IsNullOrEmpty(ensinoAluno.HistoricoEscolar.DtDocumento))
                sql_comm.Parameters.AddWithValue("@DT_DOCUMENTO", ensinoAluno.HistoricoEscolar.DtDocumento);
            else
                sql_comm.Parameters.AddWithValue("@DT_DOCUMENTO", DBNull.Value);

            if (!String.IsNullOrEmpty(ensinoAluno.HistoricoEscolar.DtConclusao))
                sql_comm.Parameters.AddWithValue("@DT_CONCLUSAO", ensinoAluno.HistoricoEscolar.DtConclusao);
            else
                sql_comm.Parameters.AddWithValue("@DT_CONCLUSAO", DBNull.Value);

            if (!String.IsNullOrEmpty(ensinoAluno.HistoricoEscolar.SerieAnterior))
                sql_comm.Parameters.AddWithValue("@SERIE_ANTERIOR", ensinoAluno.HistoricoEscolar.SerieAnterior);
            else
                sql_comm.Parameters.AddWithValue("@SERIE_ANTERIOR", DBNull.Value);

            if (!String.IsNullOrEmpty(ensinoAluno.HistoricoEscolar.InstituicaoAnterior))
                sql_comm.Parameters.AddWithValue("@INSTITUICAO_ANTERIOR", ensinoAluno.HistoricoEscolar.InstituicaoAnterior);
            else
                sql_comm.Parameters.AddWithValue("@INSTITUICAO_ANTERIOR", DBNull.Value);

            if (ensinoAluno.HistoricoEscolar.AnoAnterior != 0)
                sql_comm.Parameters.AddWithValue("@ANO_ANTERIOR", ensinoAluno.HistoricoEscolar.AnoAnterior);
            else
                sql_comm.Parameters.AddWithValue("@ANO_ANTERIOR", DBNull.Value);

            if (ensinoAluno.HistoricoEscolar.CidadeAnterior != null)
                sql_comm.Parameters.AddWithValue("@COD_CIDADE_ANTERIOR", ensinoAluno.HistoricoEscolar.CidadeAnterior.Codigo);
            else
                sql_comm.Parameters.AddWithValue("@COD_CIDADE_ANTERIOR", DBNull.Value);

            if (!String.IsNullOrEmpty(ensinoAluno.HistoricoEscolar.Fundamentacao))
                sql_comm.Parameters.AddWithValue("@FUNDAMENTACAO", ensinoAluno.HistoricoEscolar.Fundamentacao);
            else
                sql_comm.Parameters.AddWithValue("@FUNDAMENTACAO", DBNull.Value);

            if (ensinoAluno.HistoricoEscolar.Gdae != null)
                sql_comm.Parameters.AddWithValue("@GDAE", ensinoAluno.HistoricoEscolar.Gdae);
            else 
                sql_comm.Parameters.AddWithValue("@GDAE", DBNull.Value);
            
            if (ensinoAluno.HistoricoEscolar.Usuario != null)
                sql_comm.Parameters.AddWithValue("@COD_USUARIO", ensinoAluno.HistoricoEscolar.Usuario.Codigo);
            else
                sql_comm.Parameters.AddWithValue("@COD_USUARIO", DBNull.Value);

            sql_comm.Parameters.AddWithValue("@SEGUNDA_VIA", ensinoAluno.HistoricoEscolar.SegundaVia);
           
            sql_comm.Parameters.AddWithValue("@DT_LIVRO", DBNull.Value);
            sql_comm.Parameters.AddWithValue("@LIVRO", DBNull.Value);
            sql_comm.Parameters.AddWithValue("@PAGINA", DBNull.Value);
            sql_comm.Parameters.AddWithValue("@TERMO", DBNull.Value);
            
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

        public static int Atualizar(EnsinoAluno ensinoAluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_HISTORICO_ESCOLARUpdate", sqlHelper.ematConn);
            sql_comm.CommandType = CommandType.StoredProcedure;

            sql_comm.Parameters.AddWithValue("@COD_ENSINO_ALUNO", ensinoAluno.Codigo);

            if (!String.IsNullOrEmpty(ensinoAluno.HistoricoEscolar.Observacao))
                sql_comm.Parameters.AddWithValue("@OBS", ensinoAluno.HistoricoEscolar.Observacao);
            else
                sql_comm.Parameters.AddWithValue("@OBS", DBNull.Value);

            if (ensinoAluno.HistoricoEscolar.Diretor != null)
                sql_comm.Parameters.AddWithValue("@COD_USUARIO_DIRETOR", ensinoAluno.HistoricoEscolar.Diretor.Codigo);
            else
                sql_comm.Parameters.AddWithValue("@COD_USUARIO_DIRETOR", DBNull.Value);

            if (ensinoAluno.HistoricoEscolar.Secretario != null)
                sql_comm.Parameters.AddWithValue("@COD_USUARIO_SECRETARIO", ensinoAluno.HistoricoEscolar.Secretario.Codigo);
            else
                sql_comm.Parameters.AddWithValue("@COD_USUARIO_SECRETARIO", DBNull.Value);

            if (!String.IsNullOrEmpty(ensinoAluno.HistoricoEscolar.DtDocumento))
                sql_comm.Parameters.AddWithValue("@DT_DOCUMENTO", ensinoAluno.HistoricoEscolar.DtDocumento);
            else
                sql_comm.Parameters.AddWithValue("@DT_DOCUMENTO", DBNull.Value);

            if (!String.IsNullOrEmpty(ensinoAluno.HistoricoEscolar.DtConclusao))
                sql_comm.Parameters.AddWithValue("@DT_CONCLUSAO", ensinoAluno.HistoricoEscolar.DtConclusao);
            else
                sql_comm.Parameters.AddWithValue("@DT_CONCLUSAO", DBNull.Value);

            if (!String.IsNullOrEmpty(ensinoAluno.HistoricoEscolar.SerieAnterior))
                sql_comm.Parameters.AddWithValue("@SERIE_ANTERIOR", ensinoAluno.HistoricoEscolar.SerieAnterior);
            else
                sql_comm.Parameters.AddWithValue("@SERIE_ANTERIOR", DBNull.Value);

            if (!String.IsNullOrEmpty(ensinoAluno.HistoricoEscolar.InstituicaoAnterior))
                sql_comm.Parameters.AddWithValue("@INSTITUICAO_ANTERIOR", ensinoAluno.HistoricoEscolar.InstituicaoAnterior);
            else
                sql_comm.Parameters.AddWithValue("@INSTITUICAO_ANTERIOR", DBNull.Value);

            if (ensinoAluno.HistoricoEscolar.AnoAnterior != 0)
                sql_comm.Parameters.AddWithValue("@ANO_ANTERIOR", ensinoAluno.HistoricoEscolar.AnoAnterior);
            else
                sql_comm.Parameters.AddWithValue("@ANO_ANTERIOR", DBNull.Value);

            if (ensinoAluno.HistoricoEscolar.CidadeAnterior != null)
                sql_comm.Parameters.AddWithValue("@COD_CIDADE_ANTERIOR", ensinoAluno.HistoricoEscolar.CidadeAnterior.Codigo);
            else
                sql_comm.Parameters.AddWithValue("@COD_CIDADE_ANTERIOR", DBNull.Value);

            if (!String.IsNullOrEmpty(ensinoAluno.HistoricoEscolar.Fundamentacao))
                sql_comm.Parameters.AddWithValue("@FUNDAMENTACAO", ensinoAluno.HistoricoEscolar.Fundamentacao);
            else
                sql_comm.Parameters.AddWithValue("@FUNDAMENTACAO", DBNull.Value);

            if (ensinoAluno.HistoricoEscolar.Usuario != null)
                sql_comm.Parameters.AddWithValue("@GDAE", ensinoAluno.HistoricoEscolar.Gdae);
            else
                sql_comm.Parameters.AddWithValue("@GDAE", DBNull.Value);

            if (ensinoAluno.HistoricoEscolar.Usuario != null)
                sql_comm.Parameters.AddWithValue("@COD_USUARIO", ensinoAluno.HistoricoEscolar.Usuario.Codigo);
            else
                sql_comm.Parameters.AddWithValue("@COD_USUARIO", DBNull.Value);

            sql_comm.Parameters.AddWithValue("@SEGUNDA_VIA", ensinoAluno.HistoricoEscolar.SegundaVia);

            sql_comm.Parameters.AddWithValue("@DT_LIVRO", DBNull.Value);
            sql_comm.Parameters.AddWithValue("@LIVRO", DBNull.Value);
            sql_comm.Parameters.AddWithValue("@PAGINA", DBNull.Value);
            sql_comm.Parameters.AddWithValue("@TERMO", DBNull.Value);
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

        public static int Excluir(EnsinoAluno ensinoAluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_HISTORICO_ESCOLARDelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("COD_ENSINO_ALUNO", ensinoAluno.Codigo);
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
