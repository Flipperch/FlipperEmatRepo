using Dapper;
using Microsoft.Data.SqlClient;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;

namespace TestsWebApplication.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        IConfiguration _configuration;

        public UsuarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("CeejaSorocabaDb").Value;
            return connection;
        }

        public int Add(Usuario usuario)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "INSERT INTO Produtos(Nome, Estoque, Preco) VALUES(@Nome, @Estoque, @Preco); SELECT CAST(SCOPE_IDENTITY() as INT); ";
                var storedProcedureName = "spInsertUsuario";
                count = conn.Execute(storedProcedureName, usuario, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        public int Delete(int id)
        {
            var connectionString = this.GetConnection();
            var count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "DELETE FROM Produtos WHERE ProdutoId =" + id;
                var storedProcedureName = "spDeleteUsuario";
                count = conn.Execute(storedProcedureName, new { UsuarioId = id }, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        public int Edit(Usuario usuario)
        {
            var connectionString = this.GetConnection();
            var count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "UPDATE Produtos SET Name = @Nome, Estoque = @Estoque, Preco = @Preco WHERE ProdutoId = " + produto.ProdutoId;
                var storedProcedureName = "spUpdateUsuario";
                count = conn.Execute(storedProcedureName, usuario, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        public Usuario Get(int id)
        {
            var connectionString = this.GetConnection();
            Usuario usuario = new Usuario();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "SELECT * FROM Produtos WHERE ProdutoId =" + id;
                var query = "SELECT * FROM vwUsuario WHERE UsuarioId = @UsuarioId";
                usuario = conn.Query<Usuario>(query, new { UsuarioId = id }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return usuario;
        }
        public List<Usuario> GetAll()
        {
            var connectionString = this.GetConnection();
            List<Usuario> usuarios = new List<Usuario>();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "SELECT * FROM Produtos";
                var query = "SELECT * FROM vwUsuario";
                usuarios = conn.Query<Usuario>(query).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return usuarios;
        }
    }
}
