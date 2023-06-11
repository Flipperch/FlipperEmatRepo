using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Principal
{
    public partial class frSuporte : Form
    {
        public frSuporte()
        {
            InitializeComponent();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {

           SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

           sql_bd.Command.CommandText = @"INSERT INTO SUPORTE (ASSUNTO, DESCRICAO, NOME, LOGIN, CONTATO, EMAIL, DATA) VALUES (@assunto, @descricao, @nome,@login,@contato,@email,@data)";

           sql_bd.Command.Parameters.AddWithValue("@assunto", txbAssunto.Text);
           sql_bd.Command.Parameters.AddWithValue("@descricao", txbDescricao.Text);
           sql_bd.Command.Parameters.AddWithValue("@nome", txbNome.Text);
           sql_bd.Command.Parameters.AddWithValue("@login", txbLogin.Text);
           sql_bd.Command.Parameters.AddWithValue("@contato", txbTelefone.Text);
           sql_bd.Command.Parameters.AddWithValue("@email", txbEmail.Text);
           sql_bd.Command.Parameters.AddWithValue("@data", DateTime.Now);

           MessageBox.Show("Enviado com Sucesso!", "Suporte");

           try
           {
               sql_bd.Connection.Open();

               sql_bd.Command.ExecuteNonQuery();
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

        private void txbDescricao_TextChanged(object sender, EventArgs e)
        {
            if(txbAssunto.Text != string.Empty || txbNome.Text != string.Empty)
            {
                btnEnviar.Enabled = true;
            }
            else
            {
                btnEnviar.Enabled = false;
            }
        }
    }
}
