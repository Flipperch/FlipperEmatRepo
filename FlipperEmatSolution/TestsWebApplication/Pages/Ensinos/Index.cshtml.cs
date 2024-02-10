using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Pages.Ensinos
{
    public class IndexModel : PageModel
    {
        private readonly ITipoEnsinoRepository _tipoEnsinoRepository;

        public IndexModel(ITipoEnsinoRepository tipoEnsinoRepository)
        {
            _tipoEnsinoRepository = tipoEnsinoRepository;
        }

        [BindProperty]
        public IList<TipoEnsino> ListaTipoEnsinos { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            ListaTipoEnsinos = _tipoEnsinoRepository.GetAll();
        }

        public IActionResult OnPostDelete(int id)
        {
            if (id > 0)
            {
                var count = _tipoEnsinoRepository.Delete(id);
                if (count > 0)
                {
                    Message = "Ensino deletado com sucesso.";
                    return RedirectToPage("/Ensinos/Index");
                }
            }
            return Page();
        }
    }
}
