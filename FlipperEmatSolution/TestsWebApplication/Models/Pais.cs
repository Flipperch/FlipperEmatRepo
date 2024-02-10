using System.ComponentModel.DataAnnotations;

namespace TestsWebApplication.Models
{
    public class Pais
    {
        public int PaisId { get; set; }

        [Display(Name = "País")]
        public string PaisNome { get; set; }

        [Display(Name = "UFs")]
        public ICollection<Uf> Ufs { get; set; } = new List<Uf>();
    }
}
