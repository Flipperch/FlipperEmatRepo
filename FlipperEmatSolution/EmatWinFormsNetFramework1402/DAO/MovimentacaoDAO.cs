using EmatWinFormsNetFramework1402.Classes;
using EmatWinFormsNetFramework1402.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.DAO
{
    class MovimentacaoDAO
    {
        public static List<Movimentacao> ExibirTodos(EnsinoAluno ensinoAluno)
        {
            List<Movimentacao> listaMovimentacoes = new List<Movimentacao>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("SELECT * FROM MOVIMENTACAO WHERE COD_ENSINO_ALUNO = @cod_ensino_aluno", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@cod_ensino_aluno", ensinoAluno.Codigo);
            sql_comm.CommandType = CommandType.Text;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read() == true)
                {
                    Movimentacao movimentacao = new Movimentacao();
                    movimentacao.Codigo = Convert.ToInt32(reader["CODIGO"].ToString());
                    movimentacao.SituacaoAluno = (Enumeradores.SituacaoAluno)Convert.ToInt32(reader["COD_SITUACAO"]);
                    movimentacao.EnsinoAluno = ensinoAluno;
                    movimentacao.Usuario = UsuarioDAO.Consultar(Convert.ToInt32(reader["COD_USUARIO"].ToString()));
                    movimentacao.DtMovimentacao = DateTime.Parse(reader["DT_MOVIMENTACAO"].ToString());
                    movimentacao.Motivo = reader["MOTIVO"].ToString();
                    listaMovimentacoes.Add(movimentacao);
                }
                return listaMovimentacoes;
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

        public static void InserirMovimentacao(Movimentacao movimentacao)
        {
            //TODO:VERIFICAR POR QUE AO CRIAR ESTA ENTRANDO DUAS VEZES NESTE MÉTODO

            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("INSERT INTO MOVIMENTACAO VALUES (@codSituacao, @codEnsinoAluno, @codUsuario, " +
                "@dtMovimentacao, @motivo)", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@codSituacao", Convert.ToInt32(movimentacao.SituacaoAluno));
            sql_comm.Parameters.AddWithValue("@codEnsinoAluno", movimentacao.EnsinoAluno.Codigo);
            sql_comm.Parameters.AddWithValue("@codUsuario", movimentacao.Usuario.Codigo);
            sql_comm.Parameters.AddWithValue("@dtMovimentacao", movimentacao.DtMovimentacao);
            sql_comm.Parameters.AddWithValue("@motivo", movimentacao.Motivo);
            sql_comm.CommandType = CommandType.Text;
            try
            {
                sqlHelper.ematConn.Open();
                sql_comm.ExecuteNonQuery();


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
    }
}
