using Dapper;
using Microsoft.Data.SqlClient;
using TestsWebApplication.Context;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;
using TestsWebApplication.Services;

namespace TestsWebApplication.Repository
{
    public class DisciplinaAlunoRepository : IDisciplinaAlunoRepository
    {
        private readonly DapperContext _dapperContext;

        public DisciplinaAlunoRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<DisciplinaAluno>> GetAllDisciplinaAluno()
        {
            var query = "SELECT * FROM vwDisciplinaAluno";

            using var connection = _dapperContext.CreateConnection();
            var disciplinasAluno = await connection.QueryAsync<DisciplinaAluno>(query);

            return disciplinasAluno;
        }

        public async Task<List<DisciplinaAluno>> GetAllDisciplinaAlunoAtendimentos()
        {
            var query = "SELECT * FROM vwDisciplinaAluno d JOIN vwAtendimento a ON d.DisciplinaAlunoId = a.DisciplinaAlunoId";

            using var connection = _dapperContext.CreateConnection();
            var disciplinaAlunoDict = new Dictionary<int, DisciplinaAluno>();

            var listDisciplinaAluno = await connection.QueryAsync<DisciplinaAluno, Atendimento, DisciplinaAluno>(
                query, (disciplinaAluno, atendimento) =>
                {
                    if (!disciplinaAlunoDict.TryGetValue(disciplinaAluno.DisciplinaAlunoId, out var currentDisciplinaAluno))
                    {
                        currentDisciplinaAluno = disciplinaAluno;
                        disciplinaAlunoDict.Add(currentDisciplinaAluno.DisciplinaAlunoId, currentDisciplinaAluno);
                    }

                    currentDisciplinaAluno.Atendimentos.Add(atendimento);
                    return currentDisciplinaAluno;
                }
            );

            return listDisciplinaAluno.Distinct().ToList();
        }

        public async Task<DisciplinaAluno> GetDisciplinaAluno(int id)
        {
            var query = "SELECT * FROM vwDisciplinaAluno WHERE DisciplinaAlunoId = @Id";

            using var connection = _dapperContext.CreateConnection();
            var disciplinaAluno = await connection.QuerySingleOrDefaultAsync<DisciplinaAluno>(query, new { id });

            return disciplinaAluno;
        }

        public async Task<DisciplinaAluno> GetDisciplinaAlunoAtendimentos(int id)
        {
            var query = "SELECT * FROM vwDisciplinaAluno WHERE DisciplinaAlunoId = @Id;" +
                        "SELECT * FROM vwAtendimento WHERE DisciplinaAlunoId = @Id";

            using var connection = _dapperContext.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, new { id });
            var disciplinaAluno = await multi.ReadSingleOrDefaultAsync<DisciplinaAluno>();
            if (disciplinaAluno != null)
                disciplinaAluno.Atendimentos = (await multi.ReadAsync<Atendimento>()).ToList();

            return disciplinaAluno;
        }

        //TODO: Aprimorar métodos de paginação. Exemplo. Retornar todas os resultados quando filtra no campo mostrando mais que o limite da linhas neste método. Por exemplo quando pesquisa uma matricula, ele retorna apenas 10 disciplinas aluno, mantendo as restantes fora da pagina e quando clica na proxima pagina, ele esquece o filtro.
        //PagedResults
        public async Task<PagedResults<DisciplinaAluno>> GetAsync(string searchString = "", int pageNumber = 1, int pageSize = 10)
        {
            using (var connection = _dapperContext.CreateConnection())
            //using (var conn = new SqlConnection(_sqlServerOptions.SqlServerConnection))
            {
                //await connection.OpenAsync();

                // Set first query
                var whereStatement = string.IsNullOrWhiteSpace(searchString) ? "" : $"WHERE MatriculaId LIKE '{searchString}'";
                var queries = @$"
                SELECT
                    * FROM vwDisciplinaAluno (NOLOCK)
                {whereStatement}
                ORDER BY MatriculaId
                OFFSET @PageSize * (@PageNumber - 1) ROWS
                FETCH NEXT @PageSize ROWS ONLY;";

                // Set second query, separated with semi-colon
                queries += "SELECT COUNT(*) AS TotalItems FROM vwDisciplinaAluno (NOLOCK);";

                // Execute multiple queries with Dapper in just one step
                using var multi = await connection.QueryMultipleAsync(queries,
                    new
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    });

                // Fetch Items by OFFSET-FETCH clause
                var items = await multi.ReadAsync<DisciplinaAluno>().ConfigureAwait(false);

                // Fetch Total items count
                var totalItems = await multi.ReadFirstAsync<int>().ConfigureAwait(false);

                // Create paged result
                var result = new PagedResults<DisciplinaAluno>(totalItems, pageNumber, pageSize)
                {
                    Items = items
                };
                return result;
            }
        }
    }
}
