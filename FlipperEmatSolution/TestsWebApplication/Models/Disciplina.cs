using System.ComponentModel.DataAnnotations;

namespace TestsWebApplication.Models
{
    public class Disciplina
    {
        public int DisciplinaId { get; set; }

        [Display(Name = "Disciplina")]
        public string NomeDisciplina { get; set; }

        [Display(Name = "Disciplina")]
        public string NomeDisciplinaHistorico { get; set; }

        [Display(Name = "Horário")]
        public string Horario { get; set; }
        public string Capacidade { get; set; }
        public string Ordem { get; set; }

        [Display(Name = "Atribuição Bloqueada")]
        public string BloqueioAtribuicao { get; set; }

        [Display(Name = "Tipos de Atendimento")]
        public ICollection<TipoAtendimento> TiposAtendimento { get; set; } = new List<TipoAtendimento>();

    }
}
