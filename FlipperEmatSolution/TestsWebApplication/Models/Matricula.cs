namespace TestsWebApplication.Models
{
    public class Matricula
    {
        public int MatriculaId { get; set; } //ENSINO_ALUNO.CODIGO
        public int MatriculaCeeja { get; set; } //N_MAT
        public int TipoEnsinoId { get; set; } //ENSINO.CODIGO
        public bool Atual { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
            
        public TipoEnsino? TipoEnsino { get; set; }
        public ICollection<Rematricula>? Rematriculas { get; set; }
        public ICollection<DisciplinaAluno> DisciplinasAluno { get; set; } = new List<DisciplinaAluno>();
    }
}
