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
    public partial class frHistoricoAluno : Form
    {
        public string n_mat_pesquisa_ { get;set; }
        public DateTime dat_matricula { get; set; }
        public string user_matricula { get; set; }

        public frHistoricoAluno()
        {
            InitializeComponent();
        }

        private void frSituacoes_Load(object sender, EventArgs e)
        {
            preencher_dgvHistoricoSituacoes();
            qtd_situacao_sem_rematricula();
        }

        private void preencher_dgvHistoricoSituacoes()
        {
            dgvHistoricoSituacoes.Rows.Clear();

            DataTable dt_situacoes = null;// cs_historicosituacoes.sel_historicos_para_exibir(n_mat_pesquisa_);

            DataTable dt_rematriculas = null;//cs_rematriculas.sel_rematriculas_para_exibir(n_mat_pesquisa_);

            DataTable dt = new DataTable();

            dt = dt_situacoes;

            DataRow r_row_matricula = dt.NewRow();
            r_row_matricula[0] = 0;
            r_row_matricula[1] = "MATRÍCULA";
            r_row_matricula[2] = dat_matricula;
            r_row_matricula[3] = "-";
            r_row_matricula[4] = user_matricula;
            dt.Rows.Add(r_row_matricula);


            foreach (DataRow row in dt_rematriculas.Rows)
            {
                DataRow r_row = dt.NewRow();
                r_row[0] = row[0];
                r_row[1] = "REMATRÍCULA";
                r_row[2] = DateTime.Parse(row[1].ToString());
                r_row[3] = "-";
                r_row[4] = row[2];
                dt.Rows.Add(r_row);
            }

            DataView dv = dt.DefaultView;
            dv.Sort = "DATA_ENTRADA desc";
            DataTable dt_sorted = dv.ToTable();


            for (int i = 0; i < dt_sorted.Rows.Count; i++)
            {
                dgvHistoricoSituacoes.Rows.Add();
                dgvHistoricoSituacoes.Rows[i].Cells[0].Value = dt_sorted.Rows[i][0].ToString();
                dgvHistoricoSituacoes.Rows[i].Cells[1].Value = dt_sorted.Rows[i][1].ToString();
                dgvHistoricoSituacoes.Rows[i].Cells[2].Value = DateTime.Parse(dt_sorted.Rows[i][2].ToString()).ToString("dd/MM/yyyy HH:mm");
                dgvHistoricoSituacoes.Rows[i].Cells[3].Value = dt_sorted.Rows[i][3].ToString();
                dgvHistoricoSituacoes.Rows[i].Cells[4].Value = dt_sorted.Rows[i][4].ToString();                
            }

            if(dgvHistoricoSituacoes.Rows.Count > 1)
            {
                dgvHistoricoSituacoes.ContextMenuStrip = cmsOpcoes;
            }
            else dgvHistoricoSituacoes.ContextMenuStrip = null;
            
        }

        private void dgvHistoricoSituacoes_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    dgvHistoricoSituacoes.CurrentCell = dgvHistoricoSituacoes.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvHistoricoSituacoes.Rows[e.RowIndex].Cells[1].Selected = true;
                    dgvHistoricoSituacoes.Focus();
                }
            }
                      
        }

        private void excluirRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvHistoricoSituacoes.SelectedRows != null && dgvHistoricoSituacoes.Rows[dgvHistoricoSituacoes.CurrentRow.Index].Cells[1].Value.ToString() != "REMATRÍCULA" && dgvHistoricoSituacoes.Rows[dgvHistoricoSituacoes.CurrentRow.Index].Cells[1].Value.ToString() != "MATRÍCULA")
            {
                int a = dgvHistoricoSituacoes.CurrentRow.Index;

                //cs_historicosituacoes.del_hist_situacao(Convert.ToInt32(dgvHistoricoSituacoes.Rows[a].Cells[0].Value));

                dgvHistoricoSituacoes.Rows.RemoveAt(a);
            }

            qtd_situacao_sem_rematricula();

            
        }

        public void qtd_situacao_sem_rematricula()
        {
            int a = 0;

            for(int i = 0; i < dgvHistoricoSituacoes.Rows.Count; i++)
            {
                if((dgvHistoricoSituacoes.Rows[i].Cells[1].Value.ToString() != "REMATRÍCULA") &&
                    (dgvHistoricoSituacoes.Rows[i].Cells[1].Value.ToString() != "MATRÍCULA"))
                a++;
            }

            if (a > 1)
            {
                dgvHistoricoSituacoes.ContextMenuStrip = cmsOpcoes;
            }
            else dgvHistoricoSituacoes.ContextMenuStrip = null;

        }
    }
}
