using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Pages.DisciplinasAluno
{
    public class DetalhesModel : PageModel
    {
        private readonly IDisciplinaAlunoRepository _disciplinaAlunoRepository;

        public DetalhesModel(IDisciplinaAlunoRepository disciplinaAlunoRepository)
        {
            _disciplinaAlunoRepository = disciplinaAlunoRepository;
        }

        public DisciplinaAluno DisciplinaAluno { get; set; }

        //public async Task OnGetAsync(int id)
        //{
        //    DisciplinaAluno = await _disciplinaAlunoRepository.GetDisciplinaAluno(id);
        //}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //DisciplinaAluno = await _disciplinaAlunoRepository.GetDisciplinaAluno((int)id);
            DisciplinaAluno = await _disciplinaAlunoRepository.GetDisciplinaAlunoAtendimentos((int)id);

            if (DisciplinaAluno == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
