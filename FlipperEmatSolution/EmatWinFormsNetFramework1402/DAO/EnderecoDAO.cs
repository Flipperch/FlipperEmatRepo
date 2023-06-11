using System;
using System.Collections.Generic;
using EmatWinFormsNetFramework1402.Classes;
using System.Data.SqlClient;
using System.Data;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.DAO
{
    class EnderecoDAO
    {
        //private readonly IEmatriculaSettings _settings;
        
        public static Endereco Consultar(Aluno aluno)
        {
            Endereco endereco = new Endereco();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ENDERECO_ALUNOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    endereco.Cidade = DAO.CidadeDAO.Consultar(Convert.ToInt16(reader["COD_CIDADE"].ToString()));
                    endereco.Cep = reader["CEP"].ToString();
                    endereco.Logradouro = reader["LOGRADOURO"].ToString();
                    endereco.Numero = Convert.ToInt32(reader["NUMERO"].ToString());
                    endereco.Bairro = reader["BAIRRO"].ToString();
                    endereco.Complemento = reader["COMPLEMENTO"].ToString();
                    return endereco;
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

        public static List<Endereco> ExibirTodos()
        {
            List<Endereco> listaEndereco = new List<Endereco>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ENDERECO_ALUNOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@CODIGO", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read() == true)
                {
                    Endereco endereco = new Endereco();
                    endereco.Cidade = DAO.CidadeDAO.Consultar(Convert.ToInt16(reader["COD_CIDADE"].ToString()));
                    endereco.Cep = reader["CEP"].ToString();
                    endereco.Logradouro = reader["LOGRADOURO"].ToString();
                    endereco.Numero = Convert.ToInt32(reader["NUMERO"].ToString());
                    endereco.Bairro = reader["BAIRRO"].ToString();
                    endereco.Complemento = reader["COMPLEMENTO"].ToString();
                    listaEndereco.Add(endereco);
                }
                return listaEndereco;
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

        public static void Gravar(Aluno aluno)
        {
            try
            {
                if (Consultar(aluno) == null)
                    Inserir(aluno);
                else
                    Atualizar(aluno);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                throw ex;
            }
        }

        public static void Inserir(Aluno aluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ENDERECO_ALUNOInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
            
            if (aluno.Endereco.Cidade.Codigo == 0)
                sql_comm.Parameters.AddWithValue("@COD_CIDADE", DBNull.Value);
            else
                sql_comm.Parameters.AddWithValue("@COD_CIDADE", aluno.Endereco.Cidade.Codigo);

            sql_comm.CommandType = CommandType.StoredProcedure;
            sql_comm.Parameters.AddWithValue("@CEP", aluno.Endereco.Cep);
            sql_comm.Parameters.AddWithValue("@LOGRADOURO", aluno.Endereco.Logradouro);
            sql_comm.Parameters.AddWithValue("@NUMERO", aluno.Endereco.Numero);
            sql_comm.Parameters.AddWithValue("@BAIRRO", aluno.Endereco.Bairro);
            sql_comm.Parameters.AddWithValue("@COMPLEMENTO", aluno.Endereco.Complemento);
            sql_comm.CommandType = CommandType.StoredProcedure;

            try
            {
                sqlHelper.ematConn.Open();
                sql_comm.ExecuteScalar();
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

        public static void Atualizar(Aluno aluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ENDERECO_ALUNOUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
            if (aluno.Endereco.Cidade.Codigo == 0)
                sql_comm.Parameters.AddWithValue("@COD_CIDADE", DBNull.Value);
            else
                sql_comm.Parameters.AddWithValue("@COD_CIDADE", aluno.Endereco.Cidade.Codigo);
            sql_comm.Parameters.AddWithValue("@CEP", aluno.Endereco.Cep);
            sql_comm.Parameters.AddWithValue("@LOGRADOURO", aluno.Endereco.Logradouro);
            sql_comm.Parameters.AddWithValue("@NUMERO", aluno.Endereco.Numero);
            sql_comm.Parameters.AddWithValue("@BAIRRO", aluno.Endereco.Bairro);
            sql_comm.Parameters.AddWithValue("@COMPLEMENTO", aluno.Endereco.Complemento);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                sql_comm.ExecuteScalar();
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

        public static void Excluir(Aluno aluno)
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ENDERECO_ALUNODelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                sql_comm.ExecuteScalar();
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
