using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using TestsWebApplication.Models;
using TestsWebApplication.Models.ViewModels;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Pages.Relatorios
{
    public class RelatorioGeralModel : PageModel
    {

        private readonly ITipoEnsinoRepository _tipoEnsinoRepository;
        private readonly IMatriculaRepository _matriculaRepository;
        private readonly IRematriculaRepository _rematriculaRepository;

        public RelatorioGeralModel(ITipoEnsinoRepository tipoEnsinoRepository, IMatriculaRepository matriculaRepository, IRematriculaRepository rematriculaRepository)
        {
            _tipoEnsinoRepository = tipoEnsinoRepository;
            _matriculaRepository = matriculaRepository;
            _rematriculaRepository = rematriculaRepository;
        }

        public IList<SituacaoTipoEnsinoViewModel> ListaSituacaoTipoEnsinoViewModel { get; set; }


        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime PeriodoInicial { get; set; } = DateTime.Today;


        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime PeriodoFinal { get; set; } = DateTime.Today;

        public void OnGet()
        {
            var listaTipoEnsino = _tipoEnsinoRepository.GetAll();
            var listaMatricula = _matriculaRepository.GetAllMatricula().Result.Where(m => m.DataInicio >= PeriodoInicial && m.DataInicio <= PeriodoFinal);
            var listaRematricula = _rematriculaRepository.GetAll().Where(m => m.DataRematricula >= PeriodoInicial && m.DataRematricula <= PeriodoFinal);

            var listaConclusoes = _matriculaRepository.GetAllMatricula().Result.Where(m => m.DataFinal >= PeriodoInicial && m.DataFinal <= PeriodoFinal);

            ListaSituacaoTipoEnsinoViewModel = new List<SituacaoTipoEnsinoViewModel>();

            foreach (var item in listaTipoEnsino)
            {
                ListaSituacaoTipoEnsinoViewModel.Add(
                    new SituacaoTipoEnsinoViewModel
                    {
                        TipoEnsino = item,
                        QuantidadeMatriculas = listaMatricula.Count(m => m.TipoEnsinoId == item.TipoEnsinoId),
                        QuantidadeRematriculas = listaRematricula.Count(r => r.TipoEnsinoId == item.TipoEnsinoId),
                        QuantidadeConclusoes = listaConclusoes.Count(m => m.TipoEnsinoId == item.TipoEnsinoId),
                    }); 
            }

        }
    }
}
