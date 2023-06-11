using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EmatWinFormsNetFramework1402.Utils
{
    /// <summary>
    /// Classe Auxiliar para buscar Detalhes de uma Disciplina
    /// </summary>
    public class csDetalhesDisciplina
    {
        //TODO:: Remover referencias do log, mantendo apenas a chamado pelo metodo estático... OU....
        //...seria correto chamar desta forma mesmo ??? Seria DI ? Poderia construir uma classe que herda esse logmanager ?
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static List<Modelo.DISCIPLINA_ALUNO> ListaDisciplinaAluno(Modelo.DISCIPLINA disciplina, IEmatriculaSettings settings)
        {
            try
            {
                using (var context = new Modelo.Modelo(settings))
                {
                    var query = context.DISCIPLINA_ALUNO.Where(d => d.DISCIPLINA == disciplina).ToList();
                    return query;
                };
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                return null;
            }
            
        }

        //public static  ListaAlunosNaDisciplina(Modelo.DISCIPLINA disciplina)
        //{
        //    try
        //    {
        //        List<Modelo.ALUNO> listaAlunos = new List<Modelo.ALUNO>();
        //        using (var context = new Modelo.Modelo())
        //        {

        //            var entryPoint = (from disciplinaAluno in context.DISCIPLINA_ALUNO
        //                              join ensinoAluno in context.ENSINO_ALUNO on disciplinaAluno.COD_ENSINO_ALUNO equals ensinoAluno.CODIGO
        //                              join aluno in context.ALUNO on ensinoAluno.N_MAT equals aluno.N_MAT
        //                              where disciplinaAluno.COD_DISCIPLINA == disciplina.CODIGO
        //                              select new
        //                              {
        //                                  aluno.N_MAT,
        //                                  aluno.NOME,
        //                                  aluno.RG
        //                              }).ToList();

        //            listaAlunos = entryPoint

        //            return listaAlunos;

        //            //var query = context.DISCIPLINA_ALUNO.Where(d => d.DISCIPLINA == disciplina).ToList();
        //        };




        //        //using (var context = new Modelo.Modelo())
        //        //{
        //        //    var query = context.ALUNO.Where(d => d.ENSINO_ALUNO == disciplina).ToList();
        //        //    return query;
        //        //};
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message);
        //        ErrorLog.csControle_erros.ExibirErroMessBox(ex.Message);
        //        return null;
        //    }
        //}


        public static int QuantidadeAlunos(Classes.EnsinoAluno ensinoAluno, Classes.Disciplina disciplina)
        {
            int i = 0;
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sqlCommand = new SqlCommand(
                "SELECT COUNT(*) AS QTD FROM DISCIPLINA_ALUNO JOIN ENSINO_ALUNO" +
                "WHERE DISCIPLINA_ALUNO.ATUAL = 1 AND DISCIPLINA = " + disciplina.Codigo +
                " AND ENSINO_ALUNO.ENSINO = " + ensinoAluno.Ensino, sqlHelper.ematConn);

            sqlHelper.ematConn.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                return Int32.Parse(reader["QTD"].ToString());
            }
            reader.Close();
            sqlHelper.ematConn.Close();

            //i = DAO.DisciplinaAlunoDAO.ExibirTodos(null).Where(x => x.Atual == true && x.Codigo == codigoDisciplina).Count();

            return i;
        }

        //public static int QuantidadeAtendimentos(string atendimento)
        //{
        //    try
        //    {
        //        int i = 0;
        //        SqlConnection sqlConnection = new SqlConnection(Utils.Settings.ConnectionString);
        //        SqlCommand sqlCommand = new SqlCommand(
        //            "SELECT COUNT(*) AS QTD FROM DISCIPLINA_ALUNO JOIN ENSINO_ALUNO" +
        //            "WHERE DISCIPLINA_ALUNO.ATUAL = 1 AND DISCIPLINA = " + codigoDisciplina +
        //            " AND ENSINO_ALUNO.ENSINO = " + ensino, sqlConnection);
        //
        //        sqlConnection.Open();
        //        SqlDataReader reader = sqlCommand.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            return Int32.Parse(reader["QTD"].ToString());
        //        }
        //        reader.Close();
        //        sqlConnection.Close();
        //
        //        //i = DAO.DisciplinaAlunoDAO.ExibirTodos(null).Where(x => x.Atual == true && x.Codigo == codigoDisciplina).Count();
        //
        //        return i;
        //    }
        //    catch (Exception ex)
        //    {
        //        
        //        ErrorLog.csControle_erros.ExibirErroMessBox(ex.Message);
        //        return 0;
        //
        //    }
        //}

        //Opções de Atendimentos "Padrões"
        //"ORIENTAÇÃO INICIAL"
        //"RETORNO"
        //"AVALIAÇÃO"
        //"ENCERRADO"
    }
}
