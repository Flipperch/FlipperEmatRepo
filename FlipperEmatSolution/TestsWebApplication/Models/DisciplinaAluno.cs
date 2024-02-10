using System.ComponentModel.DataAnnotations;

namespace TestsWebApplication.Models
{
    public class DisciplinaAluno
    {
        public int DisciplinaAlunoId { get; set; }
 	    public int MatriculaId { get; set; }
 	    public int DisciplinaId { get; set; }

        [Display(Name = "Atual")]
 	    public bool Atual { get; set; }

        [Display(Name = "Concluída")]
 	    public bool Concluida { get; set; }

        //TODO: Implementar DataInicio(DataOrientacaoInicial) e DataFinal(DataConclusao). DISCIPLINA_ALUNO JOIN ATENDIMENTO_ALUNO.

        [Display(Name = "Atendimentos")]
        public ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();
        public Media? Media { get; set; }
    }
}
