using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032
{
    public class OleQuery
    {
         private OleDbConnection _connection;
        /// <summary>
        /// Estabelece a conexão com o banco de dados
        /// </summary>
        public OleDbConnection Connection
        {
            get { return _connection; }
            set
            {
                _connection = value;
                _command = _connection.CreateCommand();
            }
        }

        private OleDbCommand _command;
        /// <summary>
        /// Define e executa instruções Sql
        /// </summary>
        public OleDbCommand Command
        {
            get { return _command; }
            set { _command = value; }
        }

        private DataTable _table;
        /// <summary>
        /// Armazena o resultado de uma instrução SELECT
        /// </summary>
        public DataTable Table
        {
            get { return _table; }
            set { _table = value; }
        }

        private Exception _error;
        /// <summary>
        /// Armazena o erro ocorrido em uma instrução Sql
        /// </summary>
        public Exception Error
        {
            get { return _error; }
            set { _error = value; }
        }

        // construtores
        public OleQuery()
        {
            _table = new DataTable();
        }

        public OleQuery(OleDbConnection conn)
        {
            _table = new DataTable();
            Connection = conn;
        }

        // métodos
        /// <summary>
        /// Executa instuções que retornam registros
        /// </summary>
        /// <returns>Quantidade de linhas retornadas</returns>
        public int Fill(bool clearParameters = true)
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter(_command);
                _table.Clear();
                int linhas = da.Fill(_table);

                if (clearParameters) _command.Parameters.Clear();

                _error = null;
                return linhas;
            }
            catch (Exception ex)
            {
                _error = ex;
                return -1;
            }
        }
        /// <summary>
        /// Executa instruções que não retornam registros
        /// </summary>
        /// <param name="clearParameters">
        /// Se true, limpa a lista de parâmetros após a 
        /// execução do comando
        /// </param>
        /// <returns>Quantidade de linhas afetadas pelo comando</returns>
        public int ExecSql(bool clearParameters = true)
        {
            // testa se a conexão está aberta
            bool connected = _connection.State == ConnectionState.Open;
            try
            {
                // se a conexão não estiver aberta, abrir
                if (!connected) _connection.Open();
                // executar o comando
                int linhas = _command.ExecuteNonQuery();

                if (clearParameters) _command.Parameters.Clear();

                _error = null;
                return linhas;
            }
            catch (Exception ex)
            {
                _error = ex;
                return -1;
            }
            finally
            {
                // se não estava conectado, abriu a conexão. Então
                // tem que fechar
                if (!connected) _connection.Close();
            }
        }

        public bool Locate(string nomeCampo, string valor, BindingSource bs)
        {
            // variável para armazenar a posição do nome encontrado
            int pos = -1;
            // filtrar os dados do DataTable que tenham o campo
            // começando com o valor passado como parâmetro
            DataRow[] linhas = _table.Select(
                        nomeCampo + " LIKE '" + valor.Replace("'", "''") + "%'");
            if (linhas.Length > 0)
            {
                // posição onde o nome está dentro do DataTable
                pos = bs.Find(nomeCampo, linhas[0][nomeCampo]);
                // posiciona o ponteiro de registro nesta linha
                bs.Position = pos;
            }
            // retornar true se encontrou ou false caso contrário
            return pos >= 0;
        }
    }
}
