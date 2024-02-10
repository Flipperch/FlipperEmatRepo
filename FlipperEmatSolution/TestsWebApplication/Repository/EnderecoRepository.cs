using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Drawing;
using TestsWebApplication.Context;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly DapperContext _dapperContext;

        public EnderecoRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Pais>> GetAllPais()
        {
            var query = "SELECT * FROM vwPais";

            using var connection = _dapperContext.CreateConnection();
            var paises = await connection.QueryAsync<Pais>(query);

            return paises;
        }
        public async Task<IEnumerable<Uf>> GetAllUf()
        {
            var query = "SELECT * FROM vwUf";

            using var connection = _dapperContext.CreateConnection();
            var ufs = await connection.QueryAsync<Uf>(query);

            return ufs;
        }
        public async Task<IEnumerable<Cidade>> GetAllCidade()
        {
            var query = "SELECT * FROM vwCidade";

            using var connection = _dapperContext.CreateConnection();
            var cidades = await connection.QueryAsync<Cidade>(query);

            return cidades;
        }

        public async Task<Pais> GetPais(int id)
        {
            var query = "SELECT * FROM vwPais WHERE PaisId = @Id";

            using var connection = _dapperContext.CreateConnection();
            var pais = await connection.QuerySingleOrDefaultAsync<Pais>(query, new { id });

            return pais;
        }
        public async Task<Uf> GetUf(int id)
        {
            var query = "SELECT * FROM vwUf WHERE UfId = @Id";

            using var connection = _dapperContext.CreateConnection();
            var uf = await connection.QuerySingleOrDefaultAsync<Uf>(query, new { id });

            return uf;
        }
        public async Task<Cidade> GetCidade(int id)
        {
            var query = "SELECT * FROM vwCidade WHERE CidadeId = @Id";

            using var connection = _dapperContext.CreateConnection();
            var cidade = await connection.QuerySingleOrDefaultAsync<Cidade>(query, new { id });

            return cidade;
        }

        public async Task<Pais> GetPaisUfs(int id)
        {
            var query = "SELECT * FROM vwPais WHERE PaisId = @Id;" +
                        "SELECT * FROM vwUf WHERE PaisId = @Id";

            using var connection = _dapperContext.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, new { id });
            var pais = await multi.ReadSingleOrDefaultAsync<Pais>();
            if (pais != null)
                pais.Ufs = (await multi.ReadAsync<Uf>()).ToList();

            return pais;
        }
        public async Task<Uf> GetUfCidades(int id)
        {
            var query = "SELECT * FROM vwUf WHERE UfId = @Id;" +
                        "SELECT * FROM vwCidade WHERE UfId = @Id";

            using var connection = _dapperContext.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(query, new { id });
            var uf = await multi.ReadSingleOrDefaultAsync<Uf>();
            if (uf != null)
                uf.Cidades = (await multi.ReadAsync<Cidade>()).ToList();

            return uf;
        }

        public async Task<EnderecoAluno> GetEnderecoAluno(int id)
        {
            var query = "SELECT * FROM vwEnderecoAluno WHERE MatriculaCeeja = @Id";

            using var connection = _dapperContext.CreateConnection();
            var enderecoAluno = await connection.QuerySingleOrDefaultAsync<EnderecoAluno>(query, new { id });

            return enderecoAluno;
        }

        //public async Task<EnderecoAluno> GetEnderecoAlunoCidade(int id)
        //{
        //    var query = "SELECT * FROM vwUf WHERE UfId = @Id;" +
        //                "SELECT * FROM vwCidade WHERE UfId = @Id";

        //    using var connection = _dapperContext.CreateConnection();
        //    using var multi = await connection.QueryMultipleAsync(query, new { id });
        //    var uf = await multi.ReadSingleOrDefaultAsync<Uf>();
        //    if (uf != null)
        //        uf.Cidades = (await multi.ReadAsync<Cidade>()).ToList();

        //    return uf;
        //}

        //public IEnumerable<EnderecoAluno> Get([FromServices] IConfiguration config)
        //{
        //    using (SqlConnection conexao = new SqlConnection(
        //        config.GetConnectionString("ExemplosDapper")))
        //    {
        //        return conexao.Query<EnderecoAluno, Cidade, EnderecoAluno>(
        //            "SELECT * " +
        //            "FROM vwEnderecoAluno E " +
        //            "INNER JOIN vwCidade C ON C.CidadeId = E.CidadeId " +
        //            "ORDER BY E.MatriculaCeeja",
        //            map: (enderecoAluno, cidade) =>
        //            {
        //                enderecoAluno.Cidade = cidade;
        //                return enderecoAluno;
        //            },
        //            splitOn: "SiglaEstado,IdRegiao");
        //    }
        //}



    }
}