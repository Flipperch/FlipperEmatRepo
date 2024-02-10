using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;
using TestsWebApplication.Services;

namespace TestsWebApplication.Pages.DisciplinasAluno
{
    public class IndexModel : PageModel
    {
        private readonly IDisciplinaAlunoRepository _disciplinaAlunoRepository;

        public IndexModel(IDisciplinaAlunoRepository disciplinaAlunoRepository)
        {
            _disciplinaAlunoRepository = disciplinaAlunoRepository;
        }

        public DisciplinaAluno DisciplinaAluno { get; set; }

        [BindProperty]
        public PagedResults<DisciplinaAluno> ListaDisciplinaAluno { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        /// <summary>
        /// Initializes any state needed for the page, in our case Persons List
        /// </summary>
        public async Task OnGetAsync(int pageNumber = 1)
        {
            //var listaDisciplinaAluno = await _disciplinaAlunoRepository.GetAllDisciplinaAluno();

            var listaDisciplinaAluno = await _disciplinaAlunoRepository.GetAsync(SearchString, pageNumber).ConfigureAwait(false);

            ListaDisciplinaAluno = listaDisciplinaAluno;
        }
    }
}
