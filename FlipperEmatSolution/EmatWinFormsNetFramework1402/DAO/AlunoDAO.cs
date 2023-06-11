using System;
using System.Collections.Generic;
using EmatWinFormsNetFramework1402.Classes;
using System.Data.SqlClient;
using System.Data;
using EmatWinFormsNetFramework1402.Utils;
using System.Configuration;

namespace EmatWinFormsNetFramework1402.DAO
{
    class AlunoDAO
    {
        public static Aluno Consultar(int nMatricula, string rg=null)
        {
            Aluno aluno;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ALUNOSelect", sqlHelper.ematConn);

            if (nMatricula != 0)
                sql_comm.Parameters.AddWithValue("@N_MAT", nMatricula);
            else
                sql_comm.Parameters.AddWithValue("@N_MAT", DBNull.Value);

            if (rg != null)
                sql_comm.Parameters.AddWithValue("@RG", rg);
            else
                sql_comm.Parameters.AddWithValue("@RG", DBNull.Value);

            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                if (reader.Read())
                {
                    aluno = new Aluno();
                    aluno.NMatricula = nMatricula;
                    aluno.Nome = reader["NOME"].ToString();
                    aluno.DtMatricula = DateTime.Parse(reader["DT_MAT"].ToString());
                    aluno.Cpf = reader["CPF"].ToString();
                    aluno.Ra = reader["RA"].ToString();
                    aluno.Rg = reader["RG"].ToString();
                    aluno.UfRg = reader["UF_RG"].ToString();
                    aluno.OrgaoRg = reader["ORGAO_RG"].ToString();
                    aluno.DtRg = reader["DT_RG"].ToString();
                    aluno.Nome = reader["NOME"].ToString();
                    aluno.DtNascimento = DateTime.Parse(reader["DT_NASCIMENTO"].ToString()).ToString("dd/MM/yyyy");
                    if (reader["SEXO"] != DBNull.Value)
                        aluno.Sexo = (Enumeradores.Sexo)reader["SEXO"];
                    aluno.NomeMae = reader["NOME_MAE"].ToString();
                    aluno.NomePai = reader["NOME_PAI"].ToString();
                    if (reader["ESTADO_CIVIL"] != DBNull.Value)
                        aluno.EstadoCivil = (Enumeradores.EstadoCivil)reader["ESTADO_CIVIL"];
                    if (reader["COR_ORIGEM_ETNICA"] != DBNull.Value)
                        aluno.CorOrigemEtnica = (Enumeradores.CorOrigemEtnica)reader["COR_ORIGEM_ETNICA"];
                    aluno.Telefone = reader["TELEFONE"].ToString();
                    aluno.Celular = reader["CELULAR"].ToString();
                    aluno.TermoMatricula = reader["TERMO_MAT"].ToString();
                    aluno.Email = reader["E_MAIL"].ToString();
                    aluno.Ativo = Convert.ToBoolean(reader["ATIVO"].ToString());
                    aluno.Concluinte = Convert.ToBoolean(reader["CONCLUINTE"].ToString());
                    aluno.Observacao = reader["OBS_PASSAPORTE"].ToString();
                    aluno.ApresentouCertificado = Convert.ToBoolean(reader["APRESENTOU_CERTIDAO"].ToString());
                    aluno.ApresentouHistorico = Convert.ToBoolean(reader["APRESENTOU_HISTORICO"]);
                    aluno.NomeSocial = reader["NOME_SOCIAL"].ToString();
                    if(reader["COD_USUARIO"]!=DBNull.Value)
                        aluno.Usuario = UsuarioDAO.Consultar(Convert.ToInt32(reader["COD_USUARIO"].ToString()));

                    //Associações
                    aluno.ListaEnsinoAluno = EnsinoAlunoDAO.ExibirTodos(aluno);
                    aluno.LocalNascimento = LocalNascimentoDAO.Consultar(aluno);
                    aluno.Endereco = EnderecoDAO.Consultar(aluno);
                    aluno.Emprego = EmpregoDAO.Consultar(aluno);
                    aluno.ListaDeficienciaPessoa = DeficienciaPessoaDAO.ExibirTodos(aluno.NMatricula);

                    reader.Close();
                    return aluno;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sqlHelper.ematConn.Close();
            }
        }

        public static List<Aluno> ExibirTodos(bool lista=false)
        {
            List<Aluno> listaAluno = new List<Aluno>();
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ALUNOSelect", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", DBNull.Value);
            sql_comm.Parameters.AddWithValue("@RG", DBNull.Value);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while(reader.Read())
                {                    
                    Aluno aluno = new Aluno();
                    aluno.NMatricula = Convert.ToInt32(reader["N_MAT"].ToString());
                    aluno.Nome = reader["NOME"].ToString();
                    aluno.Rg = reader["RG"].ToString();
                    aluno.Ativo = Convert.ToBoolean(reader["ATIVO"].ToString());
                    aluno.Concluinte = Convert.ToBoolean(reader["CONCLUINTE"].ToString());
                    aluno.DtMatricula = DateTime.Parse(reader["DT_MAT"].ToString());
                    aluno.Cpf = reader["CPF"].ToString();
                    aluno.Ra = reader["RA"].ToString();
                    aluno.UfRg = reader["UF_RG"].ToString();
                    aluno.OrgaoRg = reader["ORGAO_RG"].ToString();
                    aluno.DtRg = reader["DT_RG"].ToString();
                    aluno.DtNascimento = reader["DT_NASCIMENTO"].ToString();
                    if(reader["SEXO"]!=DBNull.Value)
                        aluno.Sexo = (Enumeradores.Sexo)reader["SEXO"];
                    aluno.NomeMae = reader["NOME_MAE"].ToString();
                    aluno.NomePai = reader["NOME_PAI"].ToString();
                    if(reader["ESTADO_CIVIL"]!=DBNull.Value)
                        aluno.EstadoCivil = (Enumeradores.EstadoCivil)reader["ESTADO_CIVIL"];
                    if(reader["COR_ORIGEM_ETNICA"]!=DBNull.Value)
                        aluno.CorOrigemEtnica = (Enumeradores.CorOrigemEtnica)reader["COR_ORIGEM_ETNICA"];
                    aluno.Telefone = reader["TELEFONE"].ToString();
                    aluno.Celular = reader["CELULAR"].ToString();
                    aluno.TermoMatricula = reader["TERMO_MAT"].ToString();
                    aluno.Email = reader["E_MAIL"].ToString();
                    aluno.Observacao = reader["OBS_PASSAPORTE"].ToString();
                    aluno.ApresentouCertificado = Convert.ToBoolean(reader["APRESENTOU_CERTIDAO"].ToString());
                    aluno.ApresentouHistorico = Convert.ToBoolean(reader["APRESENTOU_HISTORICO"]);
                    aluno.NomeSocial = reader["NOME_SOCIAL"].ToString();
                    
                    if (!lista)
                    {                        
                        aluno.Usuario = UsuarioDAO.Consultar(Convert.ToInt32(reader["COD_USUARIO"].ToString()));
                    }
                    listaAluno.Add(aluno);
                }
                return listaAluno;
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

        public static int Gravar(Aluno aluno)
        {
            try
            {
                if (Consultar(aluno.NMatricula) == null)
                    return Inserir(aluno);
                else
                    return Atualizar(aluno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int Inserir(Aluno aluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ALUNOInsert", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("N_MAT", aluno.NMatricula);
            sql_comm.Parameters.AddWithValue("@DT_MAT", aluno.DtMatricula);
            sql_comm.Parameters.AddWithValue("@CPF", aluno.Cpf);
            //sql_comm.Parameters.AddWithValue("@RA", aluno.Ra);
            sql_comm.Parameters.AddWithValue("@RA", aluno.Ra == string.Empty ? (object)DBNull.Value : aluno.Ra);
            sql_comm.Parameters.AddWithValue("@RG", aluno.Rg);
            sql_comm.Parameters.AddWithValue("@UF_RG", aluno.UfRg);
            sql_comm.Parameters.AddWithValue("@ORGAO_RG", aluno.OrgaoRg);
            sql_comm.Parameters.AddWithValue("@NOME", aluno.Nome);
            DateTime dataRg;
            if(DateTime.TryParse(aluno.DtRg, out dataRg)){
                sql_comm.Parameters.AddWithValue("@DT_RG", dataRg.ToString("yyyy-MM-dd"));
            }
            else
            {
                sql_comm.Parameters.AddWithValue("@DT_RG", DBNull.Value);
            }
            DateTime dataNascimento;
            if(DateTime.TryParse(aluno.DtNascimento, out dataNascimento))
            {
                sql_comm.Parameters.AddWithValue("@DT_NASCIMENTO", dataNascimento.ToString("yyyy-MM-dd"));
            }                
            else
            {
                sql_comm.Parameters.AddWithValue("@DT_NASCIMENTO", DBNull.Value);
            }                
            sql_comm.Parameters.AddWithValue("@SEXO", aluno.Sexo);
            sql_comm.Parameters.AddWithValue("@NOME_MAE", aluno.NomeMae);
            sql_comm.Parameters.AddWithValue("@NOME_PAI", aluno.NomePai);
            sql_comm.Parameters.AddWithValue("@ESTADO_CIVIL", aluno.EstadoCivil);
            sql_comm.Parameters.AddWithValue("@COR_ORIGEM_ETNICA", aluno.CorOrigemEtnica);
            sql_comm.Parameters.AddWithValue("@TELEFONE", aluno.Telefone);
            sql_comm.Parameters.AddWithValue("@CELULAR", aluno.Celular);
            sql_comm.Parameters.AddWithValue("@TERMO_MAT", aluno.TermoMatricula);
            sql_comm.Parameters.AddWithValue("@E_MAIL", aluno.Email);
            sql_comm.Parameters.AddWithValue("@ATIVO", aluno.Ativo);
            sql_comm.Parameters.AddWithValue("@CONCLUINTE", aluno.Concluinte);
            sql_comm.Parameters.AddWithValue("@OBS_PASSAPORTE", aluno.Observacao);
            sql_comm.Parameters.AddWithValue("@APRESENTOU_CERTIDAO", aluno.ApresentouCertificado);
            sql_comm.Parameters.AddWithValue("@APRESENTOU_HISTORICO", aluno.ApresentouHistorico);
            sql_comm.Parameters.AddWithValue("@NOME_SOCIAL", aluno.NomeSocial);
            sql_comm.Parameters.AddWithValue("@COD_USUARIO", aluno.Usuario.Codigo);
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

        public static int Atualizar(Aluno aluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ALUNOUpdate", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@N_MAT", aluno.NMatricula);           
            sql_comm.Parameters.AddWithValue("@DT_MAT", aluno.DtMatricula);
            sql_comm.Parameters.AddWithValue("@CPF", aluno.Cpf);
            sql_comm.Parameters.AddWithValue("@RA", aluno.Ra == string.Empty ? (object)DBNull.Value : aluno.Ra);
            sql_comm.Parameters.AddWithValue("@RG", aluno.Rg);
            sql_comm.Parameters.AddWithValue("@UF_RG", aluno.UfRg);
            sql_comm.Parameters.AddWithValue("@ORGAO_RG", aluno.OrgaoRg);
            sql_comm.Parameters.AddWithValue("@NOME", aluno.Nome);
            DateTime dataRg;
            if (DateTime.TryParse(aluno.DtRg, out dataRg))
            {
                sql_comm.Parameters.AddWithValue("@DT_RG", dataRg.ToString("yyyy-MM-dd"));
            }
            else
            {
                sql_comm.Parameters.AddWithValue("@DT_RG", DBNull.Value);
            }
            DateTime dataNascimento;
            if (DateTime.TryParse(aluno.DtNascimento, out dataNascimento))
            {
                sql_comm.Parameters.AddWithValue("@DT_NASCIMENTO", dataNascimento.ToString("yyyy-MM-dd"));
            }
            else
            {
                sql_comm.Parameters.AddWithValue("@DT_NASCIMENTO", DBNull.Value);
            }
            sql_comm.Parameters.AddWithValue("@SEXO", aluno.Sexo);
            sql_comm.Parameters.AddWithValue("@NOME_MAE", aluno.NomeMae);
            sql_comm.Parameters.AddWithValue("@NOME_PAI", aluno.NomePai);
            sql_comm.Parameters.AddWithValue("@ESTADO_CIVIL", aluno.EstadoCivil);
            sql_comm.Parameters.AddWithValue("@COR_ORIGEM_ETNICA", aluno.CorOrigemEtnica);
            sql_comm.Parameters.AddWithValue("@TELEFONE", aluno.Telefone);
            sql_comm.Parameters.AddWithValue("@CELULAR", aluno.Celular);
            sql_comm.Parameters.AddWithValue("@TERMO_MAT", aluno.TermoMatricula);
            sql_comm.Parameters.AddWithValue("@E_MAIL", aluno.Email);
            sql_comm.Parameters.AddWithValue("@ATIVO", aluno.Ativo);
            sql_comm.Parameters.AddWithValue("@CONCLUINTE", aluno.Concluinte);
            sql_comm.Parameters.AddWithValue("@OBS_PASSAPORTE", aluno.Observacao);
            sql_comm.Parameters.AddWithValue("@APRESENTOU_CERTIDAO", aluno.ApresentouCertificado);
            sql_comm.Parameters.AddWithValue("@APRESENTOU_HISTORICO", aluno.ApresentouHistorico);
            sql_comm.Parameters.AddWithValue("@NOME_SOCIAL", aluno.NomeSocial);
            sql_comm.Parameters.AddWithValue("@COD_USUARIO", aluno.Usuario.Codigo);
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

        public static int Excluir(Aluno aluno)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("usp_ALUNODelete", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("N_MAT", aluno.NMatricula);
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
