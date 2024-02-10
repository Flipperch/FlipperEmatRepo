using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestsWebApplication.Models
{
    public class TipoEnsino
    {
        public int TipoEnsinoId { get; set; }

        [Required]
        [Display(Name = "Tipo de Ensino")]
        [StringLength(50, ErrorMessage = "errorMessage")]
        public string TipoEnsinoNome { get; set; }


        public ICollection<Matricula> Matriculas { get; set; }
    }
}
