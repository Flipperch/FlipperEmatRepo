using EmatWinFormsNetFramework1402.Classes;
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
    public partial class frmAtividadeExtra : Form
    {
        public frmAtividadeExtra()
        {
            InitializeComponent();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (Int32.TryParse(txtNMatricula.Text, out int nMatricula))
                {
                    Aluno aluno = DAO.AlunoDAO.Consultar(nMatricula);

                    if (aluno != null)
                    {
                        if (Aluno.GetEnsinoAlunoAtual(aluno) != null)
                        {

                        dgvAlunos.Rows.Add();
                        dgvAlunos.Rows[dgvAlunos.Rows.Count - 1].Cells[0].Value = aluno;
                        dgvAlunos.Rows[dgvAlunos.Rows.Count - 1].Cells[1].Value = aluno.NMatricula;
                        dgvAlunos.Rows[dgvAlunos.Rows.Count - 1].Cells[2].Value = aluno.Nome;
                        dgvAlunos.Rows[dgvAlunos.Rows.Count - 1].Cells[3].Value = Aluno.GetEnsinoAlunoAtual(aluno).Ensino;
                        dgvAlunos.Rows[dgvAlunos.Rows.Count - 1].Cells[4].Value = "Excluir";

                        txtNMatricula.Text = string.Empty;

                        txtNMatricula.Focus();
                        }
                        else
                        {
                            ErrorLog.ErrorHandleService.ExibirMsgBoxError("Aluno não possui ensino atual.");
                        }
                    }
                    else
                    {
                        ErrorLog.ErrorHandleService.ExibirMsgBoxError("Número de matrícula não encontrado.");
                    }
                }
                else
                {
                    ErrorLog.ErrorHandleService.ExibirMsgBoxError("Informe apenas números de matrícula.");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvAlunos.Rows.Count; i++)
                {
                    AtividadeExtra atividadeExtra = new AtividadeExtra();
                    atividadeExtra.Data = dtpData.Value;
                    atividadeExtra.Usuario = Utils.csUsuarioLogado.usuario;
                    Aluno.GetEnsinoAlunoAtual((Aluno)dgvAlunos.Rows[i].Cells[0].Value).AtividadeExtra = atividadeExtra;
                    DAO.AtividadeExtraDAO.Gravar(Aluno.GetEnsinoAlunoAtual((Aluno)dgvAlunos.Rows[i].Cells[0].Value));
                }
                Close();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!btnSalvar.Enabled)
                {
                    Close();
                }
                else
                {
                    if (MessageBox.Show("Deseja salvar antes de sair ?","Ematricula",MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        btnSalvar.PerformClick();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void dgvAlunos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    dgvAlunos.Rows.RemoveAt(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }
    }
}
