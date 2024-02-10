using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
using System.Configuration;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;
using TestsWebApplication.Services;

namespace TestsWebApplication.Pages.Alunos
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IAlunoRepository _alunoRepository;

        public IndexModel(IConfiguration configuration, IAlunoRepository alunoRepository)
        {
            _configuration = configuration;
            _alunoRepository = alunoRepository;
        }

        public string MatriculaCeejaSort { get; set; }
        public string NomeSort { get; set; }
        public string RaSort { get; set; }
        public string RgSort { get; set; }
        public string CurrentSort { get; set; }

        public string CurrentFilter { get; set; }
        public SelectList SearchTypes { get; set; }
        public string SearchType { get; set; }

        public PaginatedList<Aluno> Alunos { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchType, string searchString, int? pageIndex)
        {
            SearchTypes = new SelectList(
                new Dictionary<string, object>() { { "MatriculaCeeja", "Matrícula" }, { "Nome", "Nome" }, { "Rg", "RG" }, { "Ra", "RA" } },
                "Key",
                "Value");

            CurrentSort = sortOrder;
            MatriculaCeejaSort = string.IsNullOrEmpty(sortOrder) ? "matricula_desc" : "";
            NomeSort = sortOrder == "nome" ? "nome_desc" : "nome";
            RaSort = sortOrder == "ra" ? "ra_desc" : "ra";
            RgSort = sortOrder == "rg" ? "rg_desc" : "rg";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (searchType != null)
            {
                SearchType = searchType;
            }

            CurrentFilter = searchString;

            IQueryable<Aluno> alunosIQ = (await _alunoRepository.GetAllAluno()).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                alunosIQ = searchType switch
                {
                    //"MatriculaCeeja" => int.TryParse(searchString, out int result) ? alunosIQ.Where(a => a.MatriculaCeeja == result) : alunosIQ.Where(a => a.MatriculaCeeja == 0),
                    //"MatriculaCeeja" => alunosIQ.Where(a => a.MatriculaCeeja.ToString() == searchString),
                    "MatriculaCeeja" => alunosIQ.Where(a => !string.IsNullOrEmpty(a.MatriculaCeeja.ToString()) && a.MatriculaCeeja.ToString().StartsWith(searchString, StringComparison.OrdinalIgnoreCase)),
                    "Nome" => alunosIQ.Where(a => !string.IsNullOrEmpty(a.Nome) && a.Nome.StartsWith(searchString, StringComparison.OrdinalIgnoreCase)),
                    "Ra" => alunosIQ.Where(a => !string.IsNullOrEmpty(a.Ra) && a.Ra.StartsWith(searchString, StringComparison.OrdinalIgnoreCase)),
                    //"Ra" => alunosIQ.Where(a => !string.IsNullOrEmpty(a.GetType().GetProperty($"{searchType}").GetValue(a, null).ToString()) && a.Ra.StartsWith(searchString, StringComparison.OrdinalIgnoreCase)),
                    "Rg" => alunosIQ.Where(a => !string.IsNullOrEmpty(a.Rg) && a.Rg.StartsWith(searchString, StringComparison.OrdinalIgnoreCase)),
                    _ => alunosIQ
                };
            }

            alunosIQ = sortOrder switch
            {
                "matricula_desc" => alunosIQ.OrderByDescending(a => a.MatriculaCeeja),
                "nome_desc" => alunosIQ.OrderByDescending(a => a.Nome),
                "ra_desc" => alunosIQ.OrderByDescending(a => a.Ra),
                "rg_desc" => alunosIQ.OrderByDescending(a => a.Rg),
                "nome" => alunosIQ.OrderBy(a => a.Nome),
                "ra" => alunosIQ.OrderBy(a => a.Ra),
                "rg" => alunosIQ.OrderBy(a => a.Rg),
                _ => alunosIQ.OrderBy(a => a.MatriculaCeeja),
            };
            var pageSize = _configuration.GetValue("PageSize", 10);

            Alunos = await PaginatedList<Aluno>.CreateAsync(alunosIQ, pageIndex ?? 1, pageSize);
            //alunosIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        //public string CurrentFilter { get; set; }

        //public Aluno Aluno { get; set; }

        //[BindProperty]
        //public PagedResults<Aluno> ListaAluno { get; set; }

        ////[BindProperty(SupportsGet = true)]
        ////public string SearchString { get; set; }

        //public SelectList? TiposPesquisa { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public string? TipoPesquisa { get; set; }

        //public async Task OnGetAsync(string currentFilter, string searchString, int pageNumber = 1)
        //{
        //    //if (searchString != null)
        //    //{
        //    //    pageNumber = 1;
        //    //}
        //    //else
        //    //{
        //    //    searchString = currentFilter;
        //    //}

        //    CurrentFilter = searchString;

        //    var tiposPesquisaDict = new Dictionary<string, string>();

        //    tiposPesquisaDict.Add("MatriculaCeeja", "Matrícula");
        //    tiposPesquisaDict.Add("Nome", "Nome");
        //    tiposPesquisaDict.Add("Rg", "RG");
        //    tiposPesquisaDict.Add("Ra", "RA");

        //    TiposPesquisa = new SelectList(tiposPesquisaDict, "Key", "Value");

        //    ListaAluno = await _alunoRepository.GetAsync(TipoPesquisa, searchString, pageNumber).ConfigureAwait(false);
        //}

        //public async Task OnGetAsync(int pageNumber = 1)
        //{
        //    var tiposPesquisaDict = new Dictionary<string, string>();

        //    tiposPesquisaDict.Add("MatriculaCeeja", "Matrícula");
        //    tiposPesquisaDict.Add("Nome", "Nome");
        //    tiposPesquisaDict.Add("Rg", "RG");
        //    tiposPesquisaDict.Add("Ra", "RA");

        //    TiposPesquisa = new SelectList(tiposPesquisaDict, "Key", "Value");

        //    ListaAluno = await _alunoRepository.GetAsync(TipoPesquisa, SearchString, pageNumber).ConfigureAwait(false);

        //    //if (!string.IsNullOrEmpty(SearchString))
        //    //{
        //    //    ListaAluno = await _alunoRepository.GetAsync(TipoPesquisa, SearchString, pageNumber).ConfigureAwait(false);
        //    //}
        //}
    }
}
