using TestsWebApplication.Models;
using TestsWebApplication.Services;

namespace TestsWebApplication.Repository.Interface
{
    public interface IMatriculaRepository
    {
        public Task<IEnumerable<Matricula>> GetAllMatricula();
        public Task<Matricula> GetMatricula(int id);

        //public Task<Matricula> CreateMatricula(MatriculaForCreationDto matricula);
        //public Task UpdateMatricula(int id, MatriculaForUpdateDto matricula);
        //public Task DeleteMatricula(int id);
        //public Task<Company> GetCompanyByEmployeeId(int id);

        //TODO: Verificar se este metodo GetMatriculaFull é funcional.
        //public Task<Matricula> GetMatriculaFull(int id);
        //ou métodos separados como este abaixo:

        public Task<Matricula> GetMatriculaDisciplinasAluno(int id);
        public Task<List<Matricula>> GetAllMatriculaDisciplinasAluno();
        Task<PagedResults<Matricula>> GetAsync(string searchString = "", int pageNumber = 1, int pageSize = 10);
        public Task<IEnumerable<Matricula>> GetMatriculasAluno(int matriculaCeeja);

        //public Task CreateMultipleCompanies(List<CompanyForCreationDto> companies);
    }
}