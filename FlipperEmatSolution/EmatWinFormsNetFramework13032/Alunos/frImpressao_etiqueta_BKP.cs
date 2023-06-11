using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Alunos
{
    public partial class frImpressao_etiqueta_BKP : Form
    {
        Relatorios.csRelatorios et = new Relatorios.csRelatorios();

        public frImpressao_etiqueta_BKP()
        {
            InitializeComponent();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            
            //et.lista_n_mat.Add(txbN_mat1.Text);            
            //et.lista_n_mat.Add(txbN_mat2.Text);            
            //et.lista_n_mat.Add(txbN_mat3.Text);
            
            //get_data();

            //et.gera_etiqueta();

            //this.Close();
        }

        public void get_data()
        {
            for(int i =0; i < et.lista_n_mat.Count; i++)
            {
                if (et.lista_n_mat[i] != string.Empty)
                {
                    SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

                    sql_bd.Command.CommandText = @"SELECT * FROM ALUNOS WHERE N_MAT=@n_mat";

                    sql_bd.Command.Parameters.AddWithValue("@n_mat", et.lista_n_mat[i]);
                    
                    sql_bd.Connection.Open();

                    try
                    {
                        SqlDataReader reader = sql_bd.Command.ExecuteReader();

                        while (reader.Read())
                        {
                            et.lista_rg.Add(reader["RG"].ToString());
                            et.lista_aluno.Add(reader["ALUNO"].ToString());
                            et.lista_ensino.Add(reader["ENSINO"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        sql_bd.Command.Parameters.Clear();
                        sql_bd.Connection.Close();
                    }
                }
                else
                {
                    et.lista_rg.Add("");
                    et.lista_aluno.Add("");
                    et.lista_ensino.Add("");
                }
            }        
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
