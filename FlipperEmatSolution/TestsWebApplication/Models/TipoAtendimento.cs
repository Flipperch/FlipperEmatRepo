using System.ComponentModel.DataAnnotations;

namespace TestsWebApplication.Models
{
    public class TipoAtendimento
    {
        [Key]
        public int TipoAtendimentoId { get; set; }

        [Display(Name = "Tipo de Atendimento")]
        public string TipoAtendimentoNome { get; set; }
        public int DisciplinaId { get; set; }

        [Display(Name = "Permite Nota")]
        public bool PermiteNota { get; set; }
        public bool Ativo { get; set; }
        public int Ordem { get; set; }
    }
}
