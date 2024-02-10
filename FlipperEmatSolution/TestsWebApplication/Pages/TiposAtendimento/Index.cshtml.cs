using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Pages.TiposAtendimento
{
    public class IndexModel : PageModel
    {
        private readonly ITipoAtendimentoRepository _tipoAtendimentoRepository;

        public IndexModel(ITipoAtendimentoRepository tipoAtendimentoRepository)
        {
            _tipoAtendimentoRepository = tipoAtendimentoRepository;
        }

        [BindProperty]
        public IList<TipoAtendimento> TiposAtendimento { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            TiposAtendimento = _tipoAtendimentoRepository.GetAll();
        }

        public IActionResult OnPostDelete(int id)
        {
            if (id > 0)
            {
                var count = _tipoAtendimentoRepository.Delete(id);
                if (count > 0)
                {
                    Message = "Tipo de Atendimento deletado com sucesso.";
                    return RedirectToPage("/Disciplinas/Index");
                }
            }
            return Page();
        }
    }
}
