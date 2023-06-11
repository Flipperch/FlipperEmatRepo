using MoreLinq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmatWinFormsNetFramework1402.Utils
{
    public class SqlHelper
    {
        SqlConnection cn;
        public SqlConnection ematConn;
        public SqlHelper(string connectionString)
        {
            cn = new SqlConnection(connectionString);
        }

        public SqlHelper()
        {
            ematConn = new SqlConnection(ConfigurationManager.AppSettings["EmatriculaSettingsConnectionStringADO"].ToString());
        }

        public bool IsConnection
        {
            get
            {
                if (cn.State == System.Data.ConnectionState.Closed)
                    cn.Open();
                return true;
            }
        }
    }
}
