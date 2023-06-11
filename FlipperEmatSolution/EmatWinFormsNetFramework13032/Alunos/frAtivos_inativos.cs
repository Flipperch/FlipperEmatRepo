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
    public partial class frAtivos_inativos : Form
    {
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();
        Alunos.csAlunos cs_alunos = new Alunos.csAlunos();
        Notas.csAtendimentos cs_atendimentos = new Notas.csAtendimentos();
        Usuarios_Grupos.csUsuarios cs_usuarios = new Usuarios_Grupos.csUsuarios();
        Notas.csDisciplinas cs_disciplinas = new Notas.csDisciplinas();

        Relatorios.csRelatorios cs_relatorio = new Relatorios.csRelatorios();

        DataTable dt = new DataTable();

        public frAtivos_inativos()
        {
            InitializeComponent();
        }

        private void frAtivos_inativos_Load(object sender, EventArgs e)
        {

        }
        
        public void filtrar(DateTime dat_ini, DateTime dat_fim)
        {
            Cursor.Current = Cursors.WaitCursor;
            dt.Rows.Clear();
            dt.Columns.Clear();

            dt.Columns.Add("ORDEM");              //0
            dt.Columns.Add("N_MAT");             //1
            dt.Columns.Add("NOME");              //2
            dt.Columns.Add("RG");                //3
            dt.Columns.Add("MAE");               //4
            //dt.Columns.Add("ENSINO");            //5
            dt.Columns.Add("DISCIPLINA_ATUAL");        //6
            dt.Columns.Add("ULTIMA_PRESENCA");            //7
            
            List<csAlunos> list_ = cs_alunos.lista_alunos_com_ultima_presenca(dat_ini,dat_fim);

            dgvFiltro.Rows.Clear();

            for (int i = 0; i < list_.Count; i++)
            {
                dgvFiltro.Rows.Add();
                dgvFiltro.Rows[i].Cells[1].Value = list_[i].n_mat;
                dgvFiltro.Rows[i].Cells[2].Value = list_[i].nome;
                dgvFiltro.Rows[i].Cells[3].Value = list_[i].rg;
                dgvFiltro.Rows[i].Cells[4].Value = cs_disciplinas.troca_ensino_id_por_nome(list_[i].id_ensino_atual);
                dgvFiltro.Rows[i].Cells[5].Value = cs_disciplinas.troca_disciplina_id_por_nome(list_[i].id_disciplina_atual);
                dgvFiltro.Rows[i].Cells[6].Value = list_[i].ultima_presenca;

                dt.Rows.Add();
                dt.Rows[i][0] = i + 1;
                dt.Rows[i][1] = list_[i].n_mat;
                dt.Rows[i][2] = list_[i].nome;
                dt.Rows[i][3] = list_[i].rg;
                dt.Rows[i][4] = list_[i].nome_mae; //mae
                //dt.Rows[i][5] = cs_disciplinas.troca_ensino_id_por_nome(list_[i].id_ensino_atual);
                dt.Rows[i][5] = cs_disciplinas.troca_disciplina_id_por_nome(list_[i].id_disciplina_atual);
                dt.Rows[i][6] = list_[i].ultima_presenca;
                
            }

            lblQtd_Inat.Text = "Quantidade Lista: " + dgvFiltro.Rows.Count;

            Cursor.Current = Cursors.Default;
        }

        //Filtro
        private void dtpMat_ini_ValueChanged(object sender, EventArgs e)
        {
            chama_filtro();
        }

        private void dtpMat_fin_ValueChanged(object sender, EventArgs e)
        {
            chama_filtro();
        }

        public void chama_filtro()
        {
            if(dtpFim.Checked)
                filtrar(dtpIni.Value, dtpFim.Value);
            else
                filtrar(dtpIni.Value, dtpIni.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if(btnSelecionarTudo.Text == "Selecionar Tudo")
            {
                for (int i = 0; i < dgvFiltro.Rows.Count; i++)
                {
                    dgvFiltro.Rows[i].Cells[0].Value = true;
                }

                btnSelecionarTudo.Text = "Limpar Seleção";
            }
            else
            {
                for (int i = 0; i < dgvFiltro.Rows.Count; i++)
                {
                    dgvFiltro.Rows[i].Cells[0].Value = false;
                }

                btnSelecionarTudo.Text = "Selecionar Tudo";
            }

            Cursor.Current = Cursors.Default;
        }

        private void dgvFiltro_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvFiltro.CurrentCell.ColumnIndex == 0)
            {
                for (int i = 0; i < dgvFiltro.Rows.Count; i++)
                {
                    if (dgvFiltro.Rows[i].Cells[0].Value == "true")
                    {

                    }
                }
            }
        }

        private void btnInativar_Click(object sender, EventArgs e)
        {        
            List<string> list_ = new List<string>();

            for(int i = 0; i < dgvFiltro.Rows.Count; i ++)
            {
                DataGridViewCheckBoxCell checkcell = (DataGridViewCheckBoxCell)dgvFiltro.Rows[i].Cells[0];
                bool bchecked = (null != checkcell && null != checkcell.Value && true == (bool)checkcell.Value);
                if (bchecked==true)
                {
                    list_.Add(dgvFiltro.Rows[i].Cells[1].Value.ToString());
                }
            }

            if (list_.Count>0)
            {
                //Inativar e add no historico de situações
                //for(int i = 0;i < list_.Count;i++)
                //{
                //    cs_alunos.ativa_inativa_aluno(list_[i], 0, Usuarios_Grupos.csUsuario_logado.id_usuario_logado);
                //
                //    cs_alunos.add_situacao(1, list_[i], "INATIVADO POR AUSENCIA", Usuarios_Grupos.csUsuario_logado.id_usuario_logado);
                //}
                 
                //Após inativar a lista, atualizar dgv
                chama_filtro();
                
                //Imprimir lista dos alunos inativados
                cs_relatorio.gera_lista_inativacao(dt);

                //Texto botão
                btnSelecionarTudo.Text = "Selecionar Tudo";
            }        
        }
    }
}
