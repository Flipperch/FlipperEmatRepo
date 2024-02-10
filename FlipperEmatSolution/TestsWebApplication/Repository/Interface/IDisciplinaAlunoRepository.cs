using TestsWebApplication.Models;
using TestsWebApplication.Services;

namespace TestsWebApplication.Repository.Interface
{
    public interface IDisciplinaAlunoRepository
    {
        public Task<IEnumerable<DisciplinaAluno>> GetAllDisciplinaAluno();
        public Task<DisciplinaAluno> GetDisciplinaAluno(int id);

        //public Task<DisciplinaAluno> CreateDisciplinaAluno(DisciplinaAlunoForCreationDto disciplinaAluno);
        //public Task UpdateDisciplinaAluno(int id, DisciplinaAlunoForUpdateDto disciplinaAluno);
        //public Task DeleteDisciplinaAluno(int id);
        //public Task<Company> GetCompanyByEmployeeId(int id);

        //TODO: Verificar se este metodo GetDisciplinaAlunoFull é funcional.
        //public Task<DisciplinaAluno> GetDisciplinaAlunoFull(int id);
        //ou métodos separados como este abaixo:
        public Task<DisciplinaAluno> GetDisciplinaAlunoAtendimentos(int id);
        public Task<List<DisciplinaAluno>> GetAllDisciplinaAlunoAtendimentos();
        Task<PagedResults<DisciplinaAluno>> GetAsync(string searchString = "", int pageNumber = 1, int pageSize = 10);

        //public Task CreateMultipleCompanies(List<CompanyForCreationDto> companies);

    }
}