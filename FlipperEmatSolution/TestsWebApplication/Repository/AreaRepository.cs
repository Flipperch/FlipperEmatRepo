using Dapper;
using Microsoft.Data.SqlClient;
using TestsWebApplication.Models;
using TestsWebApplication.Repository.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TestsWebApplication.Repository
{
    public class AreaRepository : IAreaRepository
    {
        IConfiguration _configuration;
        public AreaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("CeejaSorocabaDb").Value;
            return connection;
        }
        public int Add(Area area)
        {
            var connectionString = this.GetConnection();
            int count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "INSERT INTO Produtos(Nome, Estoque, Preco) VALUES(@Nome, @Estoque, @Preco); SELECT CAST(SCOPE_IDENTITY() as INT); ";
                var storedProcedureName = "spInsertArea";
                count = conn.Execute(storedProcedureName, area, commandType: System.Data.CommandType.StoredProcedure);
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
                var storedProcedureName = "spDeleteArea";
                count = conn.Execute(storedProcedureName, new { AreaId = id }, commandType: System.Data.CommandType.StoredProcedure);
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
        public int Edit(Area area)
        {
            var connectionString = this.GetConnection();
            var count = 0;
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //var query = "UPDATE Produtos SET Name = @Nome, Estoque = @Estoque, Preco = @Preco WHERE ProdutoId = " + produto.ProdutoId;
                var storedProcedureName = "spUpdateArea";
                count = conn.Execute(storedProcedureName, area, commandType: System.Data.CommandType.StoredProcedure);
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
        public Area Get(int id)
        {
            var connectionString = this.GetConnection();
            Area area = new Area();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var query = "SELECT * FROM vwArea WHERE AreaId = @AreaId";
                area = conn.Query<Area>(query, new { AreaId = id }).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return area;
        }
        public List<Area> GetAll()
        {
            var connectionString = this.GetConnection();
            List<Area> areas = new List<Area>();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var query = "SELECT * FROM vwArea";
                areas = conn.Query<Area>(query).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return areas;
        }
        public List<Area> GetAreasDisciplinas()
        {
            var connectionString = this.GetConnection();
            List<Area> areas = new List<Area>();
            var areaDict = new Dictionary<int, Area>();
            using var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                var query = @"select 
                                COD_AREA AreaId,
	                            AREA.NOME NomeArea,
                                COD_DISCIPLINA DisciplinaId,
	                            DISCIPLINA.NOME NomeDisciplina,
                                NOME_HISTORICO NomeDisciplinaHistorico,
	                            HORARIO Horario,
                                CAPACIDADE Capacidade,
	                            ORDEM Ordem,
                                BLOQ_ATRIBUICAO BloqueioAtribuicao
                               from AREA
                               JOIN AREA_DISCIPLINA ON AREA.CODIGO = AREA_DISCIPLINA.COD_AREA
                               JOIN DISCIPLINA ON AREA_DISCIPLINA.COD_DISCIPLINA = DISCIPLINA.CODIGO";

                areas = conn.Query<Area, Disciplina, Area>(query, (area, disciplina) =>
                {
                    if (!areaDict.TryGetValue(area.AreaId, out var currentArea))
                    {
                        currentArea = area;
                        areaDict.Add(currentArea.AreaId, currentArea);
                    }

                    currentArea.Disciplinas.Add(disciplina);
                    return currentArea;

                },splitOn: "DisciplinaId").ToList();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return areas;
        }
    }
}
