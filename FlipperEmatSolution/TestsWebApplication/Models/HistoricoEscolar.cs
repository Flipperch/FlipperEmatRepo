namespace TestsWebApplication.Models
{
    public class HistoricoEscolar
    {
        public int MatriculaId { get; set; }
 	    public string Observacoes { get; set; }
 	    public int UsuarioDiretorId { get; set; }
 	    public int UsuarioSecretarioId { get; set; }
 	    public int UsuarioId { get; set; }
 	    public DateOnly DataLivro { get; set; }
 	    public string Livro { get; set; }
 	    public string Pagina { get; set; }
 	    public string Termo { get; set; }
 	    public DateOnly DataDocumento { get; set; }
 	    public DateOnly DataConclusao { get; set; }
 	    public string SerieAnterior { get; set; }
 	    public string InstituicaoAnterior { get; set; }
 	    public int AnoAnterior { get; set; }
 	    public int CidadeAnteriorId { get; set; }
 	    public string Fundamentacao { get; set; }
 	    public string Gdae { get; set; }
 	    public bool SegundaVia { get; set; }
    }
}
