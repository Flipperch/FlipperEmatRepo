using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace EmatWinFormsNetFramework1402.Classes
{
    public class AtendimentoAluno
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int codigo;
        private DateTime dtDoAtendimento;
        private DateTime dtDaModificaoAtendimento;
        private string modulo;
        private Atendimento atendimento;
        private Nota nota;
        private Professor professorAtribuiuAtendimento;
        private Professor professorModificouAtendimento;
        private DisciplinaAluno disciplinaAluno;

        public int Codigo { get => codigo; set => codigo = value; }
        public DateTime DtDoAtendimento { get => dtDoAtendimento; set => dtDoAtendimento = value; }
        public DateTime DtDaModificaoAtendimento { get => dtDaModificaoAtendimento; set => dtDaModificaoAtendimento = value; }
        public string Modulo { get => modulo; set => modulo = value; }
        public Atendimento Atendimento { get => atendimento; set => atendimento = value; }
        public Nota Nota { get => nota; set => nota = value; }
        public Professor ProfessorAtribuiuAtendimento { get => professorAtribuiuAtendimento; set => professorAtribuiuAtendimento = value; }
        public Professor ProfessorModificouAtendimento { get => professorModificouAtendimento; set => professorModificouAtendimento = value; }
        public DisciplinaAluno DisciplinaAluno { get => disciplinaAluno; set => disciplinaAluno = value; }

        public static AtendimentoAluno GetUltimaPresença(Aluno aluno)
        {
            try
            {
                var list = GetListaAtendimentosAluno(aluno).OrderByDescending(x => x.DtDoAtendimento).ToList();
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        return list[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
            
        }

        public static List<AtendimentoAluno> GetListaAtendimentosAluno(Aluno aluno)
        {
            try
            {
                var list = new List<AtendimentoAluno>();
                foreach(EnsinoAluno ensinoAluno in aluno.ListaEnsinoAluno)
                {
                    foreach(DisciplinaAluno disciplinaAluno in ensinoAluno.ListaDisciplinaAluno)
                    {
                        foreach(AtendimentoAluno atendimentoAluno in disciplinaAluno.ListaAtendimentoAluno)
                        {
                            list.Add(atendimentoAluno);
                        }
                    }
                }

                list = list.OrderByDescending(x => x.DtDoAtendimento).ToList();

                return list;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }
    }
}
