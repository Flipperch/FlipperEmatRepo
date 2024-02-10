using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Pages.Matriculas
{
    public class DetalhesModel : PageModel
    {
        private readonly IMatriculaRepository _matriculaRepository;

        public DetalhesModel(IMatriculaRepository matriculaRepository)
        {
            _matriculaRepository = matriculaRepository;
        }

        public Matricula Matricula { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //DisciplinaAluno = await _disciplinaAlunoRepository.GetDisciplinaAluno((int)id);
            Matricula = await _matriculaRepository.GetMatriculaDisciplinasAluno((int)id);

            if (Matricula == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
