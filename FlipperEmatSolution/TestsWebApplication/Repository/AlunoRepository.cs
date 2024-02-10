using Dapper;
using TestsWebApplication.Context;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;
using TestsWebApplication.Services;

namespace TestsWebApplication.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly DapperContext _dapperContext;

        public AlunoRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Aluno>> GetAllAluno()
        {
            var query = "SELECT * FROM vwAluno";

            using var connection = _dapperContext.CreateConnection();
            var alunos = await connection.QueryAsync<Aluno>(query);

            return alunos;
        }
        public async Task<List<Aluno>> GetAllAlunoMatriculas()
        {
            var query = "SELECT * FROM vwAluno a JOIN vwMatricula m ON a.MatriculaCeeja = m.MatriculaCeeja";

            using var connection = _dapperContext.CreateConnection();
            var alunoDict = new Dictionary<int, Aluno>();

            var listAluno = await connection.QueryAsync<Aluno, Matricula, Aluno>(
                query, (aluno, matricula) =>
                {
                    if (!alunoDict.TryGetValue(aluno.MatriculaCeeja, out var currentAluno))
                    {
                        currentAluno = aluno;
                        alunoDict.Add(currentAluno.MatriculaCeeja, currentAluno);
                    }

                    currentAluno.Matriculas.Add(matricula);
                    return currentAluno;
                }
            );

            return listAluno.Distinct().ToList();
        }
        public async Task<Aluno> GetAluno(int id)
        {
            var query = "SELECT * FROM vwAluno WHERE MatriculaCeeja = @Id";

            using var connection = _dapperContext.CreateConnection();
            var aluno = await connection.QuerySingleOrDefaultAsync<Aluno>(query, new { id });

            return aluno;
        }
        public async Task<Aluno> GetAlunoMatriculas(int id)
        {
            var query = "SELECT * FROM vwAluno WHERE MatriculaCeeja = @Id;" +
                        "SELECT * FROM vwMatricula WHERE MatriculaCeeja = @Id";

            using var connection = _dapperContext.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, new { id });
            var aluno = await multi.ReadSingleOrDefaultAsync<Aluno>();
            if (aluno != null)
                aluno.Matriculas = (await multi.ReadAsync<Matricula>()).ToList();

            return aluno;
        }
        public async Task<PagedResults<Aluno>> GetAsync(string searchType = "", string searchString = "", int pageNumber = 1, int pageSize = 10)
        {
            using var connection = _dapperContext.CreateConnection();
            //await connection.OpenAsync();

            // Set first query
            var whereStatement = string.IsNullOrWhiteSpace(searchString) ? "" : $"WHERE {searchType} LIKE '{searchString}%'";
            var queries = @$"
                SELECT *
                FROM vwAluno (NOLOCK)
                {whereStatement}
                ORDER BY MatriculaCeeja
                OFFSET @PageSize * (@PageNumber - 1) ROWS
                FETCH NEXT @PageSize ROWS ONLY;";

            // Set second query, separated with semi-colon
            queries += "SELECT COUNT(*) AS TotalItems FROM vwAluno (NOLOCK);";

            // Execute multiple queries with Dapper in just one step
            using var multi = await connection.QueryMultipleAsync(queries,
                new
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });

            // Fetch Items by OFFSET-FETCH clause
            var items = await multi.ReadAsync<Aluno>().ConfigureAwait(false);

            // Fetch Total items count
            var totalItems = await multi.ReadFirstAsync<int>().ConfigureAwait(false);

            // Create paged result
            var result = new PagedResults<Aluno>(totalItems, pageNumber, pageSize)
            {
                Items = items
            };
            return result;
        }


    }
}
