using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Pages.Disciplinas
{
    public class IndexModel : PageModel
    {
        private readonly IDisciplinaRepository _disciplinaRepository;

        public IndexModel(IDisciplinaRepository disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }

        [BindProperty]
        public IList<Disciplina> Disciplinas { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            Disciplinas = _disciplinaRepository.GetAll();
        }

        public IActionResult OnPostDelete(int id)
        {
            if (id > 0)
            {
                var count = _disciplinaRepository.Delete(id);
                if (count > 0)
                {
                    Message = "Disciplina deletada com sucesso.";
                    return RedirectToPage("/Disciplinas/Index");
                }
            }
            return Page();
        }

    }
}
