namespace TestsWebApplication.Models
{
    public class Rematricula
    {
        public int RematriculaId { get; set; }
        public DateTime DataRematricula { get; set; }
        public int UsuarioId { get; set; }
        public int MatriculaId { get; set; }
        public int TipoEnsinoId { get; set; }
    }
}
