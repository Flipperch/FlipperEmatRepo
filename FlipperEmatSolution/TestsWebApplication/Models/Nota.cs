using System.ComponentModel.DataAnnotations;

namespace TestsWebApplication.Models
{
    public class Nota
    {
        public int AtendimentoId { get; set; }

        [Display(Name = "Nota")]
		public string NotaValor { get; set; }
    }
}
