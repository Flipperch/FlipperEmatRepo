using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Relatorios
{

    public partial class frRelatoriosGeral : Form
    {
        Alunos.csAlunos cs_alunos = new Alunos.csAlunos();
        Notas.csNotas cs_notas = new Notas.csNotas();
        Notas.csDisciplinas cs_disciplinas = new Notas.csDisciplinas();
        Notas.csAtendimentos cs_atendimentos = new Notas.csAtendimentos();
        Usuarios_Grupos.csUsuarios cs_usuarios = new Usuarios_Grupos.csUsuarios();

        csRelatorios cs_relatorio = new csRelatorios();

        public frRelatoriosGeral()
        {
            InitializeComponent();
        }

        private void frRelatorios_Load(object sender, EventArgs e)
        {
            #region Geral

            cmbMatriculas_periodo.SelectedIndexChanged -= cmbMatriculas_periodo_SelectedIndexChanged;
            cmbMatriculas_periodo.Text = "TODOS";

            preencher_dados_gerais_e_dgvMatriculas_usuarios();

            cmbMatriculas_periodo.SelectedIndexChanged += cmbMatriculas_periodo_SelectedIndexChanged;

            #endregion



            preencher_dgvDisciplinas();
            

            
        }        

        #region Matrículas - Informações de Quantidade por data e usuários

        private void preencher_dados_gerais_e_dgvMatriculas_usuarios()
        {
            string dat_ini = dtpMatriculas_ini.Value.ToString("dd/MM/yyyy");
            string dat_fin = dtpMatriculas_fin.Value.ToString("dd/MM/yyyy");

            if (!dtpMatriculas_fin.Checked)
            {
                dat_fin = dtpMatriculas_ini.Value.ToString("dd/MM/yyyy");
            }

            //Preencher Dados 
            lblQtdMatriculas.Text = "Matrículas: " + cs_alunos.qtd_matriculas(dat_ini, dat_fin, cmbMatriculas_periodo.Text);
            //lblRematriculas.Text = "Rematrículas: " + qtd_rematriculas_dia(dat_ini);
            lblRematriculas.Visible = false;
            lblQtdAtivos.Text = "Ativos: " + cs_alunos.qtd_alunos_ativos();
            lblQtdInativos.Text = "Desistentes(Inativos): " + cs_alunos.qtd_alunos_ativos(0);
            lblQtdConcluintes.Text = "Concluintes: " + cs_alunos.qtd_alunos_ativos(0,1);

            //Preencher dgvMatriculas_usuarios
            dgvMatriculas_usuarios.Rows.Clear();

            List<int> list_ids_users = cs_alunos.list_ids_users_matriculas(dat_ini, dat_fin, cmbMatriculas_periodo.Text);

            for (int i = 0; i < list_ids_users.Count; i = i + 2)
            {
                dgvMatriculas_usuarios.Rows.Add();
                if (list_ids_users[i] != 0)
                    dgvMatriculas_usuarios.Rows[dgvMatriculas_usuarios.Rows.Count-1].Cells[0].Value = list_ids_users[i];
                else
                    dgvMatriculas_usuarios.Rows[dgvMatriculas_usuarios.Rows.Count - 1].Cells[0].Value = "---";

                dgvMatriculas_usuarios.Rows[dgvMatriculas_usuarios.Rows.Count-1].Cells[1].Value = cs_usuarios.troca_id_por_nome(list_ids_users[i]);
                dgvMatriculas_usuarios.Rows[dgvMatriculas_usuarios.Rows.Count-1].Cells[2].Value = list_ids_users[i + 1];
            }
        }

        private void dtpMatriculas_ini_ValueChanged(object sender, EventArgs e)
        {
            preencher_dados_gerais_e_dgvMatriculas_usuarios();
        }

        private void dtpMatriculas_fin_ValueChanged(object sender, EventArgs e)
        {
            preencher_dados_gerais_e_dgvMatriculas_usuarios();
        }

        private void cmbMatriculas_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            preencher_dados_gerais_e_dgvMatriculas_usuarios();
        }

        public int qtd_rematriculas_dia(DateTime dat_ini)
        {
            int qtd = 0;

            List<Alunos.csAlunos> list_ = cs_alunos.lista_alunos(false, 1, 0);

            for (int i = 0; i < list_.Count; i++)
            {
                if (list_[i].lista_rematriculas != null)
                {
                    if (list_[i].lista_rematriculas[0].ToString("dd/MM/yyyy") == dat_ini.ToString("dd/MM/yyyy"))
                    {
                        qtd++;
                    }
                }
            }

            return qtd;
        }        
       

        #endregion

        #region Disciplinas - Informações de Quantidade Atual, Iniciantes e Concluintes por Disciplina

        public void preencher_dgvDisciplinas()
        {
            dgvDisciplinas.Rows.Clear();

            List<Notas.csDisciplinas> list_ = cs_disciplinas.list_disciplinas();
            for(int i = 0; i < list_.Count; i++)
            {
                dgvDisciplinas.Rows.Add();
                dgvDisciplinas.Rows[i].Cells[0].Value = list_[i].id_disciplina;
                dgvDisciplinas.Rows[i].Cells[1].Value = list_[i].n_disciplina;
                dgvDisciplinas.Rows[i].Cells[2].Value = cs_alunos.qtd_alunos_disciplinas(list_[i].id_disciplina);

                string dt_ini = dtpIniciantes_ini.Value.ToString("dd/MM/yyyy");
                string dt_fin = dtpIniciantes_fin.Value.ToString("dd/MM/yyyy");

                if (!dtpIniciantes_fin.Checked)
                {
                    dt_fin = dtpIniciantes_ini.Value.ToString("dd/MM/yyyy");
                }

                dgvDisciplinas.Rows[i].Cells[3].Value = cs_atendimentos.qtd_atendimentos_filtro("ORIENTAÇÃO INICIAL", list_[i].id_disciplina, dt_ini, dt_fin);
                dgvDisciplinas.Rows[i].Cells[4].Value = cs_atendimentos.qtd_atendimentos_filtro("MÉDIA FINAL", list_[i].id_disciplina, dt_ini, dt_fin);
            }
        }

        private void dtpIniciantes_ini_ValueChanged(object sender, EventArgs e)
        {
            preencher_dgvDisciplinas();
        }

        private void dtpIniciantes_fin_ValueChanged(object sender, EventArgs e)
        {
            preencher_dgvDisciplinas();
        }


        #endregion

        
        

        


































    }
}
