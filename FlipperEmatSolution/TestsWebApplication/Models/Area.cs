using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;

namespace TestsWebApplication.Models
{
    public class Area
    {
        public int AreaId { get; set; }
        public string NomeArea { get; set; }

        [Display(Name = "Disciplinas")]
        public ICollection<Disciplina> Disciplinas { get; set;} = new List<Disciplina>();
    }
}
