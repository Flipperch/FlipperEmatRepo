using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Endereços
{
    public partial class Frnovo : Form
    {
        public Frnovo()
        {
            InitializeComponent();
        }

        public string tipo = "";

        private void bt_aceitar_Click(object sender, EventArgs e)
        {
            if(txt_novo_end.Text != "")
            {
                if (tipo == "pais")
                {
                    csNovo_endereco.novo_pais = txt_novo_end.Text;
                    this.Close();
                }
                else
                {
                    csNovo_endereco.novo_estado = txt_novo_end.Text;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Infome o novo endereço.", "Novo endereço");
            }

           
        }


    }
}
