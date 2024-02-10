using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Pages.Usuarios
{
    public class DetalhesModel : PageModel
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public DetalhesModel(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        [BindProperty]
        public Usuario Usuario { get; set; }
        public void OnGet(int id)
        {
            Usuario = _usuarioRepository.Get(id);
        }
        public IActionResult OnPost()
        {
            var dados = Usuario;
            if (ModelState.IsValid)
            {
                var count = _usuarioRepository.Edit(dados);
                if (count > 0)
                    return RedirectToPage("/Usuarios/Index");
            }
            return Page();
        }
    }
}
