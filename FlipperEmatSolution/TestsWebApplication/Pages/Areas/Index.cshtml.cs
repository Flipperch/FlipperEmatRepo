using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Pages.Areas
{
    public class IndexModel : PageModel
    {
        
        private readonly IAreaRepository _areaRepository;

        public IndexModel(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        [BindProperty]
        public IList<Area> Areas { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            Areas = _areaRepository.GetAll();
        }

        public IActionResult OnPostDelete(int id) 
        {
            if (id > 0)
            {
                var count = _areaRepository.Delete(id);
                if (count > 0)
                {
                    Message = "Area deletada com sucesso.";
                    return RedirectToPage("/Areas/Index");
                }
            }
            return Page();
        }
    }
}
