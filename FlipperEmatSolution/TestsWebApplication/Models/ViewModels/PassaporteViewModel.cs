namespace TestsWebApplication.Models.ViewModels
{
    public class PassaporteViewModel
    {
        public string PassaporteTipoEnsinoNome { get; set; }
        public int MatriculaCeeja { get; set; }
        public string AlunoRg { get; set; }
        
        //TODO: Verificar o campo AlunoTermoMatricula no passaporte. Tabela ALUNO.
        public string AlunoTermoMatricula { get; set; } 
        public string AlunoNome { get; set; }
        
        //TODO: Data de Matrícula no passaporte condiz ao DataInicio da matricula.
        public string DataMatricula { get; set; } 
        public string DisciplinaAlunoAtualNome { get; set; }
        
        //TODO: Verificar o campo AlunoObsPassaporte no passaporte. Tabela ALUNO.
        public string AlunoObsPassaporte { get; set; }
        
        //TODO: Verificar AtividadeExtra marcada no passaporte.Tabela ATIVIDADE_EXTRA JOIN ENSINO_ALUNO.
        public bool AtividadeExtra { get; set; }

        public ICollection<DisciplinaAlunoViewModel>? DisciplinaAlunoViewModels { get; set; }
    }

    public class DisciplinaAlunoViewModel
    {
        public int DisciplinaAlunoId { get; set; }
        public string DisciplinaAlunoNome { get; set; }
        public string DisciplinaAlunoOrientacaoInicial { get; set; }

        public ICollection<AtendimentoViewModel>? AtendimentoViewModels { get; set; }
    }

    public class AtendimentoViewModel
    {
        public int AtendimentoId { get; set; }
        public string TipoAtendimentoNome { get; set; }
        public string AtendimentoModulo { get; set; }
        public string AtendimentoValor { get; set; } //TODO: Definir melhor este campo pois pode ser Nota ou Média
        public string AtendimentoData { get; set; } //TODO: Definir melhor campo AtendimentoData pois pode ter alteracao aparecendo as duas datas. Exemplo: Data Atendimento: 01/01/2023 10:00 | Data Alteração: 02/01/2023 08:00.
    }
}
