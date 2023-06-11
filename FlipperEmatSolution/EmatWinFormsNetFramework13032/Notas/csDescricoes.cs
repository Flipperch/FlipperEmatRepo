using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;

namespace EmatWinFormsNetFramework13032.Notas
{
    class csDescricoes
    {
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        public DataTable tab_descricoes()
        {
            DataTable tb = new DataTable();

            tb.Columns.Add();
            tb.Columns.Add();

            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT * FROM DESCRICOES";

            try
            {
                int i = 0;
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                while (reader.Read())
                {
                    tb.Rows.Add();
                    tb.Rows[i][0] = reader["ID_DESCRICAO"].ToString();
                    tb.Rows[i][1] = reader["DESCRICAO"].ToString();
                    i++;
                }
                reader.Close();
            }

            catch
            {

            }

            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }


            return tb;
        }




    }
}
