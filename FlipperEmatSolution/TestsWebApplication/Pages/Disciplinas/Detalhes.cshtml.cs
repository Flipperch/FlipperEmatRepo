using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Pages.Disciplinas
{
    public class DetalhesModel : PageModel
    {
        private readonly IDisciplinaRepository _disciplinaRepository;

        public DetalhesModel(IDisciplinaRepository disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }

        [BindProperty]
        public Disciplina Disciplina { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet(int? id)
        {
            Disciplina = _disciplinaRepository.GetDisciplinasTiposAtendimento().FirstOrDefault(d => d.DisciplinaId == id);
        }
    }
}
