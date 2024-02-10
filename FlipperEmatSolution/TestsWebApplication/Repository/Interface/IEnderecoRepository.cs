using Microsoft.AspNetCore.Mvc;
using TestsWebApplication.Models;

namespace TestsWebApplication.Repository.Interface
{
    public interface IEnderecoRepository
    {
        //IEnumerable<EnderecoAluno> Get([FromServices] IConfiguration config);
        Task<IEnumerable<Cidade>> GetAllCidade();
        Task<IEnumerable<Pais>> GetAllPais();
        Task<IEnumerable<Uf>> GetAllUf();
        Task<Cidade> GetCidade(int id);
        Task<EnderecoAluno> GetEnderecoAluno(int id);
        Task<Pais> GetPais(int id);
        Task<Pais> GetPaisUfs(int id);
        Task<Uf> GetUf(int id);
        Task<Uf> GetUfCidades(int id);
    }
}