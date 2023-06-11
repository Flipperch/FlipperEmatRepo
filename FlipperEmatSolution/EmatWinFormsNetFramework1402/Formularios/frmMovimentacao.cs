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
    /// <summary>
    /// Formulário de Inativação de Aluno:
    /// Criado por Felipe A. Chagas
    /// </summary>
    /// 

    public partial class frmMovimentacao : Form
    {
        public Classes.Aluno objAluno { get; set; }
        public Classes.Movimentacao movimentacao { get; set; }
        public Classes.EnsinoAluno ensinoAluno { get; set; }

        public frmMovimentacao()
        {
            InitializeComponent();
        }

        private void frInativa_select_situcao_Load(object sender, EventArgs e)
        {
            BindingSource bsSituacao = new BindingSource();
            bsSituacao.DataSource = Enum.GetValues(typeof(Enumeradores.SituacaoAluno));
            cmbSituacao.DataSource = bsSituacao;
            dtpDtMovimentacao.MaxDate = DateTime.Now;
        }
        
        private void cmbSitucao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSituacao.Text != string.Empty)
            {
                btnOK.Enabled = true;
            }
            else btnOK.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                ensinoAluno.InserirMovimentacaoNoEnsinoAluno((Enumeradores.SituacaoAluno)cmbSituacao.SelectedValue, txtMotivo.Text, dtpDtMovimentacao.Value);

                //TODO:: Irá Remover as alterações de Ativo e Concluinte (Aluno)
                //CÓDIGOS COMENTADOS POIS EDIÇÃO DOS DADOS DOALUNO NÃO SERÃO NECESSÁRIOS POR ESTE MÉTODO DE CRIAÇÃO DE MOVIMENTAÇÃO
                if ((Enumeradores.SituacaoAluno)cmbSituacao.SelectedValue == Enumeradores.SituacaoAluno.ATIVO)
                {
                    objAluno.Ativo = true;
                    objAluno.Concluinte = false;
                    DAO.AlunoDAO.Gravar(objAluno);

                }
                else if ((Enumeradores.SituacaoAluno)cmbSituacao.SelectedValue == Enumeradores.SituacaoAluno.CONCLUINTE) //Conclui o ensinoAluno
                {
                    if (ensinoAluno.Ensino == Enumeradores.Ensino.MÉDIO)
                    {
                        objAluno.Ativo = false;
                        objAluno.Concluinte = true;
                        DAO.AlunoDAO.Gravar(objAluno);
                    }
                }
                else if ((Enumeradores.SituacaoAluno)cmbSituacao.SelectedValue == Enumeradores.SituacaoAluno.FALECIDO ||
                    (Enumeradores.SituacaoAluno)cmbSituacao.SelectedValue == Enumeradores.SituacaoAluno.NÃO_COMPARECIMENTO ||
                    (Enumeradores.SituacaoAluno)cmbSituacao.SelectedValue == Enumeradores.SituacaoAluno.TRANSFERIDO)
                {
                    objAluno.Ativo = false;
                    objAluno.Concluinte = false;
                    DAO.AlunoDAO.Gravar(objAluno);
                }

                movimentacao = ensinoAluno.ListaMovimentacao.Last();

                Cursor.Current = Cursors.Default;

                this.Close();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void txtMotivo_TextChanged(object sender, EventArgs e)
        {
            if (txtMotivo.Text != string.Empty) btnOK.Enabled = true;

            else btnOK.Enabled = false;
        }
    }

}
