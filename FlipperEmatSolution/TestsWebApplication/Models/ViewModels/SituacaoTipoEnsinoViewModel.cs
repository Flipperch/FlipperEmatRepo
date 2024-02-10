using System.ComponentModel.DataAnnotations;

namespace TestsWebApplication.Models.ViewModels
{
    public class SituacaoTipoEnsinoViewModel
    {
        //TipoEnsino | Matrículas | Rematrículas | '
        public TipoEnsino TipoEnsino { get; set; }

        [Display(Name = "Matrículas")]
        public int QuantidadeMatriculas { get; set; }

        [Display(Name = "Rematrículas")]
        public int QuantidadeRematriculas { get; set; }

        [Display(Name = "Conclusões")]
        public int QuantidadeConclusoes { get; set; }


    }
}
