using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Pages.Usuarios
{
    public class IndexModel : PageModel
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public IndexModel(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [BindProperty]
        public IList<Usuario> ListaUsuarios { get; set; }

        [BindProperty]
        public Usuario Usuario { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            ListaUsuarios = _usuarioRepository.GetAll();
        }

        public IActionResult OnPostDelete(int id)
        {
            if (id > 0)
            {
                var count = _usuarioRepository.Delete(id);
                if (count > 0)
                {
                    Message = "Usuário deletado com sucesso.";
                    return RedirectToPage("/Usuarios/Index");
                }
            }
            return Page();
        }
    }
}
