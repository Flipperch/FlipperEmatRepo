using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Pages.Areas
{
    public class DetalhesModel : PageModel
    {
        private readonly IAreaRepository _areaRepository;

        public DetalhesModel(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        [BindProperty]
        public Area Area { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet(int? id)
        {
            Area = _areaRepository.GetAreasDisciplinas().FirstOrDefault(a => a.AreaId == id);
        }      
    }
}
