using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;

using System.IO;

namespace EmatWinFormsNetFramework13032.Alunos
{
    public partial class frImportar : Form
    {
        Alunos.csAlunos cs_alunos = new Alunos.csAlunos();

        public frImportar()
        {
            InitializeComponent();
        }

        private void btExecutar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string input = txtNmats.Text;

            List<string> lista = new List<string>(
                                       input.Split(new string[] { "\r\n" },
                                       StringSplitOptions.RemoveEmptyEntries));

            if(rbFundamental.Checked)
            {
                for(int i = 1; i < 8; i ++ )
                {
                    //MessageBox.Show(txtCampo.Text+i);
                    cs_alunos.repar(lista, txtTab_1.Text, txtTab_2.Text, txtCampo.Text + i);
                }
            }
            else
            {
                for (int i = 1; i < 12; i ++ )
                {
                    //MessageBox.Show(txtCampo.Text + i);
                    cs_alunos.repar(lista, txtTab_1.Text, txtTab_2.Text, txtCampo.Text + i);
                }            
            }

            


           



            Cursor.Current = Cursors.Default;
        }

        private void txtCampo_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}

