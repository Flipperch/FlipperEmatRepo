using System;
using System.Data;
using System.Data.SqlClient;

namespace EmatWinFormsNetFramework1402.Utils
{
    public class csDetalhesAluno
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //Classe auxiliar da classe Aluno.
        //Esta classe tem por objetivo realizar os métodos de calculo para exibição em relatorios e mecanismos de atribuição automatizada
        //utilizando os dados da tabela ALUNO.
        public static int QuantidadeDeAlunos(bool ativo = true, int codEnsino = 0, int codDisciplina = 0)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("SELECIONAR_QTD_ALUNOS", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@ativo", ativo);
            sql_comm.Parameters.AddWithValue("@codEnsino", codEnsino);
            sql_comm.Parameters.AddWithValue("@codDisciplina", codDisciplina);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                if (sqlHelper.ematConn.State != ConnectionState.Open)
                    sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while ((reader.Read()))
                {
                    retorno = Convert.ToInt32(reader["QTD"].ToString());
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sql_comm.Connection.Close();
            }
            return retorno;
        }
        public static int QuantidadeDeConcluintes(bool concluinte = true)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("SELECIONAR_QTD_CONCLUINTES", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@concluinte", concluinte);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                if (sqlHelper.ematConn.State != ConnectionState.Open)
                    sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while ((reader.Read()))
                {
                    retorno = Convert.ToInt32(reader["QTD"].ToString());
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sql_comm.Connection.Close();
            }
            return retorno;
        }
        public static int QuantidadeDeMatriculas(DateTime dtInicial, DateTime dtFinal, Periodo periodo, int codEnsino = 0)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("SELECIONAR_QTD_MATRICULAS", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@dtInicial", dtInicial);
            sql_comm.Parameters.AddWithValue("@dtFinal", dtFinal);
            sql_comm.Parameters.AddWithValue("@hrInicial", periodo.hrInicial);
            sql_comm.Parameters.AddWithValue("@hrFinal", periodo.hrFinal);
            sql_comm.Parameters.AddWithValue("@codEnsino", codEnsino);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                if (sqlHelper.ematConn.State != ConnectionState.Open)
                    sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while ((reader.Read()))
                {
                    retorno = Convert.ToInt32(reader["QTD"].ToString());
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sql_comm.Connection.Close();
            }
            return retorno;
        }
        public static int QuantidadeDeRematriculas(DateTime dtInicial, DateTime dtFinal, int codEnsino = 0)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("SELECIONAR_QTD_REMATRICULAS", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@dtInicial", dtInicial);
            sql_comm.Parameters.AddWithValue("@dtFinal", dtFinal);
            sql_comm.Parameters.AddWithValue("@codEnsino", codEnsino);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                if (sqlHelper.ematConn.State != ConnectionState.Open)
                    sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while ((reader.Read()))
                {
                    retorno = Convert.ToInt32(reader["QTD"].ToString());
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sql_comm.Connection.Close();
            }
            return retorno;
        }
        public static int QuantidadeDeHistoricoDeSituacao(string situacao, DateTime dtInicial, DateTime dtFinal, int codEnsino = 0)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            //SqlCommand sql_comm = new SqlCommand("SELECIONAR_QTD_HISTORICO_SITUACAO", sqlHelper.ematConn);
            //sql_comm.Parameters.AddWithValue("@dtInicial", dtInicial);
            //sql_comm.Parameters.AddWithValue("@dtFinal", dtFinal);
            //sql_comm.Parameters.AddWithValue("@situacao", situacao);
            //sql_comm.Parameters.AddWithValue("@codEnsino", codEnsino);

            //sql_comm.CommandType = CommandType.StoredProcedure;

            SqlCommand sql_comm = new SqlCommand(@"SELECT COUNT(*) as QTD FROM HISTORICOS_SITUACOES JOIN SITUACOES ON HISTORICOS_SITUACOES.ID_SITUACAO = SITUACOES.ID_SITUACAO
                INNER JOIN ALUNOS ON HISTORICOS_SITUACOES.N_MAT = ALUNOS.N_MAT 
                WHERE DATA_ENTRADA BETWEEN @dtInicial and @dtFinal AND SITUACAO = @situacao AND COD_ENSINO = @codEnsino GROUP BY SITUACAO", sqlHelper.ematConn);

            sql_comm.Parameters.AddWithValue("@dtInicial", dtInicial);
            sql_comm.Parameters.AddWithValue("@dtFinal", dtFinal);
            sql_comm.Parameters.AddWithValue("@situacao", situacao);
            sql_comm.Parameters.AddWithValue("@codEnsino", codEnsino);


            try
            {
                if (sqlHelper.ematConn.State != ConnectionState.Open)
                    sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while ((reader.Read()))
                {
                    retorno = Convert.ToInt32(reader["QTD"].ToString());
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_comm.Connection.Close();
            }
            return retorno;
        }
        public static int QuantidadeDeConclusoesDeEnino(int codEnsino, DateTime dtInicial, DateTime dtFinal)
        {
            int retorno = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand("SELECIONAR_QTD_CONCLUINTE_ENSINO", sqlHelper.ematConn);
            sql_comm.Parameters.AddWithValue("@codEnsino", codEnsino);
            sql_comm.Parameters.AddWithValue("@dtInicial", dtInicial);
            sql_comm.Parameters.AddWithValue("@dtFinal", dtFinal);
            sql_comm.CommandType = CommandType.StoredProcedure;
            try
            {
                if (sqlHelper.ematConn.State != ConnectionState.Open)
                    sqlHelper.ematConn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while ((reader.Read()))
                {
                    retorno = Convert.ToInt32(reader["QTD"].ToString());
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                sql_comm.Connection.Close();
            }
            return retorno;

        }
        public static int QuantidadeDeInativacoes()
        {
            int retorno = 0;

            return retorno;
        }

        #region Alunos no Sistema - Struct e Construtor
        public struct AlunosNoSistema
        {
            public int qtd_ativos;
            public int atv_fun;
            public int atv_med;
            public int qtd_inativos;
            public int ina_fun;
            public int ina_med;
            public int qtd_n_frequentando;
            public int qtd_cancelado;
            public int qtd_transferidos;
            public int qtd_concluintes;
        }

        public static AlunosNoSistema alunosNoSistema()
        {
            AlunosNoSistema retorno = new AlunosNoSistema();
            //Ativo            
            retorno.atv_fun = QuantidadeDeAlunos(true, 1);
            retorno.atv_med = QuantidadeDeAlunos(true, 2);
            retorno.qtd_ativos = retorno.atv_fun + retorno.atv_med;
            //Inativos            
            retorno.ina_fun = QuantidadeDeAlunos(false, 1);
            retorno.ina_med = QuantidadeDeAlunos(false, 2);
            retorno.qtd_inativos = retorno.ina_fun + retorno.ina_med;
            //concluintes
            retorno.qtd_concluintes = QuantidadeDeConcluintes();
            //historicoDeSituações
            retorno.qtd_n_frequentando = 0; // quantidadeDeHistoricoDeSituacao("NÃO FREQUENTANDO", dtInicial, dtFinal);
            retorno.qtd_cancelado = 0; // quantidadeDeHistoricoDeSituacao("CANCELADO", dtInicial, dtFinal);
            retorno.qtd_transferidos = 0; // quantidadeDeHistoricoDeSituacao("TRANSFERIDO", dtInicial, dtFinal);

            return retorno;
        }
        #endregion

        #region Matriculas - Struct e Construtor
        public struct Matriculas
        {
            public int fundamentalManha;
            public int fundamentalTarde;
            public int fundamentalNoite;

            public int fundamentalTotal;

            public int medioManha;
            public int medioTarde;
            public int medioNoite;

            public int medioTotal;

            public int total;
        }

        public static Matriculas matriculas(DateTime dtInicial, DateTime dtFinal)
        {
            Matriculas retorno = new Matriculas();

            retorno.fundamentalManha = QuantidadeDeMatriculas(dtInicial, dtFinal, periodo("MANHÃ"), 1);
            retorno.fundamentalTarde = QuantidadeDeMatriculas(dtInicial, dtFinal, periodo("TARDE"), 1);
            retorno.fundamentalNoite = QuantidadeDeMatriculas(dtInicial, dtFinal, periodo("NOITE"), 1);

            retorno.medioManha = QuantidadeDeMatriculas(dtInicial, dtFinal, periodo("MANHÃ"), 2);
            retorno.medioTarde = QuantidadeDeMatriculas(dtInicial, dtFinal, periodo("TARDE"), 2);
            retorno.medioNoite = QuantidadeDeMatriculas(dtInicial, dtFinal, periodo("NOITE"), 2);

            retorno.fundamentalTotal = retorno.fundamentalManha + retorno.fundamentalTarde + retorno.fundamentalNoite;
            retorno.medioTotal = retorno.medioManha + retorno.medioTarde + retorno.medioNoite;

            retorno.total = retorno.fundamentalTotal + retorno.medioTotal;

            return retorno;
        }
        #endregion

        #region Rematriculas - Struct e Construtor
        public struct Rematriculas
        {
            public int fundamental;
            public int medio;
            public int total;
        }
        public static Rematriculas rematriculas(DateTime dtInicial, DateTime dtFinal)
        {
            Rematriculas retorno = new Rematriculas();
            retorno.fundamental = QuantidadeDeRematriculas(dtInicial, dtFinal, 1);
            retorno.medio = QuantidadeDeRematriculas(dtInicial, dtFinal, 2);
            retorno.total = retorno.fundamental + retorno.medio;
            return retorno;
        }
        #endregion

        #region HistoricoDeSituacao - Struct e Construtor

        public struct HistoricoDeSituacoes
        {
            public int n_frequetando_fundamental;
            public int n_frequetando_medio;
            public int n_frequetando_total;

            public int cancelamento_fundamental;
            public int cancelamento_medio;
            public int cancelamento_total;

            public int transferencia_fundamental;
            public int transferencia_medio;
            public int transferencia_total;


            public int total;
        }

        public static HistoricoDeSituacoes historicoDeSituacoes(DateTime dtInicial, DateTime dtFinal)
        {
            HistoricoDeSituacoes retorno = new HistoricoDeSituacoes();
            retorno.n_frequetando_fundamental = QuantidadeDeHistoricoDeSituacao("NÃO FREQUENTANDO", dtInicial, dtFinal, 1);
            retorno.n_frequetando_medio = QuantidadeDeHistoricoDeSituacao("NÃO FREQUENTANDO", dtInicial, dtFinal, 2);
            retorno.n_frequetando_total = retorno.n_frequetando_fundamental + retorno.n_frequetando_medio;
            retorno.cancelamento_fundamental = QuantidadeDeHistoricoDeSituacao("CANCELADO", dtInicial, dtFinal, 1);
            retorno.cancelamento_medio = QuantidadeDeHistoricoDeSituacao("CANCELADO", dtInicial, dtFinal, 2);
            retorno.cancelamento_total = retorno.cancelamento_fundamental + retorno.cancelamento_medio;
            retorno.transferencia_fundamental = QuantidadeDeHistoricoDeSituacao("TRANSFERIDO", dtInicial, dtFinal, 1);
            retorno.transferencia_medio = QuantidadeDeHistoricoDeSituacao("TRANSFERIDO", dtInicial, dtFinal, 2);
            retorno.transferencia_total = retorno.transferencia_fundamental + retorno.transferencia_medio;
            retorno.total = retorno.n_frequetando_total + retorno.cancelamento_total + retorno.cancelamento_total;
            return retorno;
        }

        #endregion

        #region ConclusoesDeEnsino
        public struct ConclusoesDeEnsino
        {
            public int fundamental;
            public int medio;
            public int total;
        }
        public static ConclusoesDeEnsino conclusoesDeEnsino(DateTime dtInicial, DateTime dtFinal)
        {
            ConclusoesDeEnsino retorno = new ConclusoesDeEnsino();
            retorno.fundamental = QuantidadeDeConclusoesDeEnino(1, dtInicial, dtFinal);
            retorno.medio = QuantidadeDeConclusoesDeEnino(2, dtInicial, dtFinal);
            retorno.total = retorno.fundamental + retorno.medio;
            return retorno;
        }
        #endregion

        #region Periodo - Struct e Construtor
        public struct Periodo
        {
            public string nome;
            public int hrInicial;
            public int hrFinal;

        }

        public static Periodo periodo(string nome = "TODO")
        {
            Periodo retorno = new Periodo();
            retorno.nome = nome;
            switch (nome)
            {
                case "MANHÃ":
                    retorno.hrInicial = 7;
                    retorno.hrFinal = 13;
                    break;
                case "TARDE":
                    retorno.hrInicial = 13;
                    retorno.hrFinal = 19;
                    break;
                case "NOITE":
                    retorno.hrInicial = 19;
                    retorno.hrFinal = 23;
                    break;
                default:
                    retorno.hrInicial = 7;
                    retorno.hrFinal = 23;
                    break;
            }
            return retorno;
        }
        #endregion
    }


}
