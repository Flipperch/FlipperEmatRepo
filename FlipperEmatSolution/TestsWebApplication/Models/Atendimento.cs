namespace TestsWebApplication.Models
{
    public class Atendimento
    {
        public int AtendimentoId { get; set; }
 	    public int DisciplinaAlunoId { get; set; }
 	    public int TipoAtendimentoId { get; set; }
 	    public int ProfessorId { get; set; }
 	    public DateTime DataAtendimento { get; set; }
 	    public int ProfessorAlteracaoId { get; set; }
 	    public DateTime DataAlteracao { get; set; }
 	    public string Modulo { get; set; }
 	    public int Ordem { get; set; }
    }
}
