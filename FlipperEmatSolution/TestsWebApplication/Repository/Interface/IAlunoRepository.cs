using TestsWebApplication.Models;
using TestsWebApplication.Services;

namespace TestsWebApplication.Repository.Interface
{
    public interface IAlunoRepository
    {
        public Task<IEnumerable<Aluno>> GetAllAluno();
        public Task<Aluno> GetAluno(int id);

        //public Task<Aluno> CreateAluno(AlunoForCreationDto aluno);
        //public Task UpdateAluno(int id, AlunoForUpdateDto aluno);
        //public Task DeleteAluno(int id);
        //public Task<Company> GetCompanyByEmployeeId(int id);

        //TODO: Verificar se este metodo GetAlunoFull é funcional.
        //public Task<Aluno> GetAlunoFull(int id);
        //ou métodos separados como este abaixo:

        public Task<Aluno> GetAlunoMatriculas(int id);
        public Task<List<Aluno>> GetAllAlunoMatriculas();
        Task<PagedResults<Aluno>> GetAsync(string searchType = "", string searchString = "", int pageNumber = 1, int pageSize = 10);

        //public Task CreateMultipleCompanies(List<CompanyForCreationDto> companies);

    }
}
