namespace TestsWebApplication.Models
{
    public class Media
    {
        public int DisciplinaAlunoId { get; set; }
 	    public string MediaValor { get; set; }
 	    public DateOnly MediaData { get; set; }
 	    public int MediaUsuarioId { get; set; }
 	    public int ModificacaoMediaUsuarioId { get; set; }
 	    public DateTime ModificacaoMediaData { get; set; }
 	    public int? AtendimentoId { get; set; }
 	    public int CidadeId { get; set; }
 	    public string Instituicao { get; set; }
    }
}
