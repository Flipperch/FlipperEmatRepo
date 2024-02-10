using System.ComponentModel.DataAnnotations;

namespace TestsWebApplication.Models
{
    public class Uf
    {
        public int UfId { get; set; }

        [Display(Name = "UF")]
        public string UfNome { get; set; }

        public string UfSigla { get; set; }

        public int PaisId { get; set; }

        public ICollection<Cidade> Cidades { get; set; } = new List<Cidade>();

    }
}
