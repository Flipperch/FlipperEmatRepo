using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestsWebApplication.Models;
using TestsWebApplication.Repository;
using TestsWebApplication.Repository.Interface;
using TestsWebApplication.Services;

namespace TestsWebApplication.Pages.Matriculas
{
    public class IndexModel : PageModel
    {
        private readonly IMatriculaRepository _matriculaRepository;

        public IndexModel(IMatriculaRepository matriculaRepository)
        {
            _matriculaRepository = matriculaRepository;
        }

        public Matricula Matricula { get; set; }

        [BindProperty]
        public PagedResults<Matricula> ListaMatricula { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            var listaMatricula = await _matriculaRepository.GetAsync(SearchString, pageNumber).ConfigureAwait(false);

            ListaMatricula = listaMatricula;
        }       
    }
}
