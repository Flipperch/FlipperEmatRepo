using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

using EmatWinFormsNetFramework13032.Digitais.COM;
using EmatWinFormsNetFramework13032.Digitais.Entity;
using System.Threading;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Digitais
{
    public class Cs_digital_a_enviar
    {
        public string dig_1 = "";
        public string dig_2 = "";

        public string dig_ = "";

        public void grava()
        {
            verifica_campo();

            SqlQuery sql_ver = new SqlQuery(Conexoes.GetSqlConnection());

            sql_ver.Command.Parameters.Clear();

            if (dig_1 == "")
            {
                sql_ver.Command.CommandText =
                @"INSERT INTO DIG_A_ENVIAR (DIG_1) VALUES (@dig_)";
            }
            else
            {
                sql_ver.Command.CommandText =
                @"UPDATE DIG_A_ENVIAR SET DIG_2=@dig_ WHERE DIG_1 = @dig";

                sql_ver.Command.Parameters.AddWithValue("@dig", dig_1);
            }

            sql_ver.Command.Parameters.AddWithValue("@dig_", dig_);

            try
            {
                sql_ver.Connection.Open();
                sql_ver.Command.ExecuteNonQuery();               

            }
            catch
            {
                
            }
            finally
            {
                sql_ver.Connection.Close();

            }

        }

        public void verifica_campo()
        {
            SqlQuery sql_ver = new SqlQuery(Conexoes.GetSqlConnection());

            sql_ver.Command.Parameters.Clear();
            sql_ver.Command.CommandText =
                @"SELECT * FROM DIG_A_ENVIAR";

            sql_ver.Connection.Open();

            SqlDataReader reader = sql_ver.Command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    dig_1 = reader["DIG_1"].ToString();
                    dig_2 = reader["DIG_2"].ToString();
                }
                reader.Close();

            }
            catch
            {

            }
            finally
            {
                sql_ver.Connection.Close();

            }

        }
    }
}
