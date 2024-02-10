using Dapper;
using Microsoft.Data.SqlClient;
using TestsWebApplication.Context;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;
using TestsWebApplication.Services;

namespace TestsWebApplication.Repository
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly DapperContext _dapperContext;

        public MatriculaRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Matricula>> GetAllMatricula()
        {
            var query = "SELECT * FROM vwMatricula";

            using var connection = _dapperContext.CreateConnection();
            var matriculas = await connection.QueryAsync<Matricula>(query);

            return matriculas;
        }
        public async Task<Matricula> GetMatricula(int id)
        {
            var query = "SELECT * FROM vwMatricula WHERE MatriculaId = @Id";

            using var connection = _dapperContext.CreateConnection();
            var matricula = await connection.QuerySingleOrDefaultAsync<Matricula>(query, new { id });

            return matricula;
        }
        public async Task<Matricula> GetMatriculaDisciplinasAluno(int id)
        {
            var query = "SELECT * FROM vwMatricula WHERE MatriculaId = @Id;" +
                        "SELECT * FROM vwDisciplinaAluno WHERE MatriculaId = @Id";

            using var connection = _dapperContext.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, new { id });
            var matricula = await multi.ReadSingleOrDefaultAsync<Matricula>();
            if (matricula != null)
                matricula.DisciplinasAluno = (await multi.ReadAsync<DisciplinaAluno>()).ToList();

            return matricula;
        }
        public async Task<List<Matricula>> GetAllMatriculaDisciplinasAluno()
        {
            var query = "SELECT * FROM vwMatricula m JOIN vwDisciplinaAluno d ON m.MatriculaId = d.MatriculaId";

            using var connection = _dapperContext.CreateConnection();
            var matriculaDict = new Dictionary<int, Matricula>();

            var listMatricula = await connection.QueryAsync<Matricula, DisciplinaAluno, Matricula>(
                query, (matricula, disciplinaAluno) =>
                {
                    if (!matriculaDict.TryGetValue(matricula.MatriculaId, out var currentMatricula))
                    {
                        currentMatricula = matricula;
                        matriculaDict.Add(currentMatricula.MatriculaId, currentMatricula);
                    }

                    currentMatricula.DisciplinasAluno.Add(disciplinaAluno);
                    return currentMatricula;
                }
            );

            return listMatricula.Distinct().ToList();
        }
        public async Task<PagedResults<Matricula>> GetAsync(string searchString = "", int pageNumber = 1, int pageSize = 10)
        {
            using var connection = _dapperContext.CreateConnection();
            var whereStatement = string.IsNullOrWhiteSpace(searchString) ? "" : $"WHERE MatriculaCeeja LIKE '{searchString}'";
            var queries = @$"
                SELECT
                    * FROM vwMatricula (NOLOCK)
                {whereStatement}
                ORDER BY MatriculaCeeja, TipoEnsinoId
                OFFSET @PageSize * (@PageNumber - 1) ROWS
                FETCH NEXT @PageSize ROWS ONLY;";

            // Set second query, separated with semi-colon
            queries += "SELECT COUNT(*) AS TotalItems FROM vwMatricula (NOLOCK);";

            // Execute multiple queries with Dapper in just one step
            using var multi = await connection.QueryMultipleAsync(queries,
                new
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });

            // Fetch Items by OFFSET-FETCH clause
            var items = await multi.ReadAsync<Matricula>().ConfigureAwait(false);

            // Fetch Total items count
            var totalItems = await multi.ReadFirstAsync<int>().ConfigureAwait(false);

            // Create paged result
            var result = new PagedResults<Matricula>(totalItems, pageNumber, pageSize)
            {
                Items = items
            };
            return result;
        }

        public async Task<IEnumerable<Matricula>> GetMatriculasAluno(int matriculaCeeja)
        {
            var query = "SELECT * FROM vwMatricula WHERE MatriculaCeeja = @MatriculaCeeja";

            using var connection = _dapperContext.CreateConnection();
            var matriculas = await connection.QueryAsync<Matricula>(query, new { matriculaCeeja });

            return matriculas;
        }
    }
}
