using System.ComponentModel.DataAnnotations;

namespace TestsWebApplication.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string? Nome { get; set; }

        [Display(Name = "Nome de Acesso")]
        public string? NomeAcesso { get; set; }
        public string? Senha { get; set; }
        public string? Rg { get; set; }

        [Display(Name = "Nível de Acesso")] //Grupo
        public int NivelAcesso { get; set; }
        public bool Ativo { get; set; }
    }
}
