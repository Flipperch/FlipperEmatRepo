using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Alunos
{
    public partial class frInativarSelectSituacao : Form
    {
        csAlunos cs_alunos = new csAlunos();
        csSituacoes cs_situacoes = new csSituacoes();
        csHistoricosSituacoes cs_historicosSituacoes = new csHistoricosSituacoes();

        DataTable dt_situacoes = new DataTable();

        public string n_mat { get; set; }
        public int id_situacao_selecionada = 0;
        public string situacao_selecionada = "";
        public bool inativar = false;
        //public DateTime data_entrada_situacao = DateTime.Now;     
        private string motivo;

        public frInativarSelectSituacao()
        {
            InitializeComponent();
        }

        private void frInativa_select_situcao_Load(object sender, EventArgs e)
        {
            List<string> list = (from row in cs_situacoes.sel_situacoes().AsEnumerable()
                                 select row.Field<string>("SITUACAO")).ToList();

            dt_situacoes = cs_situacoes.sel_situacoes();

            cmbSituacao.DataSource = list;

            if(situacao_selecionada!=string.Empty)
            {
                cmbSituacao.Text = situacao_selecionada;
            }

            dtpDataEntrada.MaxDate = DateTime.Now;
            
        }

        private void cmbSitucao_SelectedIndexChanged(object sender, EventArgs e)    
        {
            if (cmbSituacao.Text != string.Empty)
            {
                btnOK.Enabled = true;

                for (int i = 0; i < dt_situacoes.Rows.Count; i++)
                {
                    if (dt_situacoes.Rows[i][1].ToString() == cmbSituacao.Text)
                    {
                        id_situacao_selecionada = Convert.ToInt32(dt_situacoes.Rows[i][0]);
                        motivo = dt_situacoes.Rows[i][2].ToString();
                        lblDescricao.Text = "Descrição da situação:" + motivo;
                    }
                }                
            }
            else btnOK.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            inativar = false;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(cmbSituacao.Text != string.Empty)
            {
                if (txtMotivo.Text != string.Empty) motivo = txtMotivo.Text;             

                cs_historicosSituacoes.add_hist_situacao(id_situacao_selecionada,
                        dtpDataEntrada.Value,
                        Usuarios_Grupos.csUsuario_logado.id_usuario_logado,
                        motivo,
                        n_mat);

                inativar = true;

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                inativar = false;
            }

            this.Close();
        }

        private void txtMotivo_TextChanged(object sender, EventArgs e)
        {
            if (txtMotivo.Text != string.Empty) btnOK.Enabled = true;

            else btnOK.Enabled = false;
        }
    }
}
