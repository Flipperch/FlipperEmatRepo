using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace EmatWinFormsNetFramework13032
{
    // por default a classe é internal, que é visível em todo o assembly
    // (.EXE ou .DLL)

    //Data Source=(LocalDB)\v11.0;
    //Data Source=.\SQLEXPRESS;

    static class Conexoes
    {
        #region SQL

        /// 1 - ceeja Sorocaba - escola
        // 2 - ceeja Sorocaba - note
        // 3 - ceeja Americana - escola
        // 4 - ceeja Americana - note2

        public static string SqlConnectionString =
            ConfigurationManager.ConnectionStrings["connSql2"].ConnectionString;
        
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(SqlConnectionString);
        }

        #endregion

        #region OLE
        // notas
        //public static string OleConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\SERVER\DADOS-MATRICULA\BANCOS\Importar\NOTAS MÉDIO.xlsx;Extended Properties=""Excel 12.0 Xml;HDR=YES;""";
        // alunos
        public static string OleConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Bancos\Importar\CADASTRO.xls;Extended Properties=""Excel 12.0 Xml;HDR=YES;""";

        //public static string OleConnectionString = "";
            //ConfigurationManager.ConnectionStrings[].ConnectionString;

        
        public static OleDbConnection GetOleDbConnection()
        {
            return new OleDbConnection(OleConnectionString);
        }

        #endregion
    }
}
