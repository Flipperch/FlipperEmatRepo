using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;

namespace TestsWebApplication.Models
{
    public class Cidade
    {
        public int CidadeId { get; set; }

        [Display(Name = "Cidade")]
        public string CidadeNome { get; set; }

        public int UfId { get; set; }
    }
}
