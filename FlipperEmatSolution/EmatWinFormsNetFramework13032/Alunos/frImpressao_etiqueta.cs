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
    public partial class frImpressao_etiqueta : Form
    {
        Relatorios.csRelatorios et = new Relatorios.csRelatorios();
        csAlunos cs_alunos = new csAlunos();

        public frImpressao_etiqueta()
        {
            InitializeComponent();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            List<csAlunos> list_ = new List<csAlunos>();

            foreach(TextBox txt in this.Controls.OfType<TextBox>())
            {
                if(txt.Text != string.Empty)
                {
                    List<csAlunos> list = cs_alunos.dados_aluno(txt.Text);
                    if (list[0].n_mat != string.Empty)
                    {
                        csAlunos cs_aluno_ = new csAlunos();
                        cs_aluno_.n_mat = list[0].n_mat;
                        cs_aluno_.nome = list[0].nome;
                        cs_aluno_.rg = list[0].rg;
                        cs_aluno_.id_disciplina_atual = list[0].id_disciplina_atual;
                        list_.Add(cs_aluno_);
                    }
                }
                
            }
            
            et.gera_etiqueta_sorocaba(list_);

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
