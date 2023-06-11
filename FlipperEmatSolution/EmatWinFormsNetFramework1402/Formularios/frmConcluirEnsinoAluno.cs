using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frmConcluirEnsinoAluno : Form
    {
        public Classes.Aluno objAluno { get; set; }

        public frmConcluirEnsinoAluno()
        {
            InitializeComponent();
        }

        private void frmConcluirEnsinoAluno_Load(object sender, EventArgs e)
        {
            lblNome.Text = "Nome: " + objAluno.Nome;
            lblNmat.Text = "Nº de Matrícula: " + objAluno.NMatricula;
            lblEnsinoAtual.Text = "Ensino Atual: " + Classes.Aluno.GetEnsinoAlunoAtual(objAluno);

            
           //foreach(Classes.DisciplinaAluno disciplinaAluno in Classes.Aluno.GetEnsinoAlunoAtual(objAluno).ListaDisciplinaAluno)
           //{
           //    if ()
           //    disciplinaAluno.Disciplina.Nome;
           //}
           //
           //lblDisciplinasEncerradas.Text;
        }
    }
}
