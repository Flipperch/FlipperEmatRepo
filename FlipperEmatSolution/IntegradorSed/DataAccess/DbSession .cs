using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Emat.IntegracaoSedConsoleApp.DataAccess
{
	public sealed class DbSession : IDisposable
	{
		private readonly IConfiguration _configuration;
		private const string _connectionId = "EmatDatabase";

		public IDbConnection Connection { get; }
		public IDbTransaction Transaction { get; set; } = default!;

		public DbSession(IConfiguration configuration)
		{
			_configuration = configuration;
			var connectionString = _configuration.GetConnectionString(_connectionId);
			Connection = new SqlConnection(connectionString);
			Connection.Open();
		}

		public void Dispose() => Connection?.Dispose();
	}
}
