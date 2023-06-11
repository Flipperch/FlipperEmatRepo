using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Notas
{
    public partial class frAtribDisciplina : Form
    {
        public string n_mat { get; set; }
        public int id_ensino_atual_ { get; set; }
        

        Alunos.csAlunos cs_alunos = new Alunos.csAlunos();
        csDisciplinas cs_disciplinas = new csDisciplinas();
        csNotas cs_notas = new csNotas();

        public frAtribDisciplina()
        {
            InitializeComponent();
        }

        private void frAtribDisciplina_Load(object sender, EventArgs e)
        {
            preencher_dados_aluno();
            preencher_cmbDisciplinas();
        }

        private void preencher_dados_aluno()
        {
            List<Alunos.csAlunos> list_ = cs_alunos.dados_aluno(n_mat);
            lblNome.Text = "Nome: " + list_[0].nome;
            lblRg.Text = "RG: " + list_[0].rg;
            lblEnsino.Text = "Ensino Atual: " + cs_disciplinas.troca_ensino_id_por_nome(list_[0].id_ensino_atual);
            lblNmat.Text = "Nº de Matrícula: " + n_mat;
            lblDisciplinaAtual.Text = "Disciplina Atual: " + cs_disciplinas.troca_disciplina_id_por_nome(list_[0].id_disciplina_atual);
            id_ensino_atual_ = list_[0].id_ensino_atual;
        }

        private void preencher_cmbDisciplinas()
        {
            List<csDisciplinas> list_disciplinas_fazer = cs_disciplinas.lista_disciplinas_fazer(n_mat, id_ensino_atual_);

            for(int i = 0; i < list_disciplinas_fazer.Count; i++)
            {
                cmbDisciplinas.Items.Add(list_disciplinas_fazer[i].n_disciplina);
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(cmbDisciplinas.Text != string.Empty)
            {
                cs_alunos.atrib_disciplina_aluno(n_mat, cs_disciplinas.troca_disciplina_nome_por_id(cmbDisciplinas.Text));
            }

            preencher_dados_aluno();
        }

        private void btnPassaporte_Click(object sender, EventArgs e)
        {
            if (cs_alunos.id_disciplina_atual__(n_mat) > 0)
            {
                Notas.frPassaporte form = new Notas.frPassaporte();
                form.n_mat = n_mat;
                form.MdiParent = this.ParentForm;
                form.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Aluno não está em uma disciplina.", "E-mat");
            }
        }

        private void cmbDisciplinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbDisciplinas.Text != string.Empty)
            {
                btnPassaporte.Enabled = true;
            }
            else
            {
                btnPassaporte.Enabled = false;
            }
        }
    }
}
