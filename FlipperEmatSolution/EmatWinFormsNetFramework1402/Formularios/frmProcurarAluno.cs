using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.Formularios
{
    //TODO=> FORM IMPORTANTE - frmProcurarAluno, Revisar Urgente! Dar atenção aos comentários.
    public partial class frmProcurarAluno : Form
    {
        private readonly IEmatriculaSettings _settings;
        DataTable tbAlunos = new DataTable();
        BindingSource bsAlunos = new BindingSource();
        BindingList<Classes.Aluno> blAluno = new BindingList<Classes.Aluno>();

        //List<Modelo.ALUNO> list = new List<Modelo.ALUNO>();

        Classes.Aluno aluno = new Classes.Aluno();
        ErrorLog.ErrorHandleService cs_erros = new ErrorLog.ErrorHandleService();
        string n_mat_selecionado = "";
        string _retornoDeFormulario = "nota";
        TimeSpan tempo;

        //TODO:: Lista de Melhorias na tela de Consulta de Alunos
        //IMPLEMENTAR NUMERO ANTIGO ALUNOS (AMERICANA)
        //Para Americana Caso não seja encontrado nenhum aluno, irá iniciar a busca na lista dos antigos

        //TODO:: Verificar forma de checar atualizações fora do load...
        //deste form pois quando esse método é chamado muitas vezes 
        //e isso atrapalha quando a versão esta com erro de atualização
        //Utils.csControleVersao.verificar_atualizacao();

        //TODO: REPENSAR MODO DE PESQUISAR
        //Mudar forma de consulta para que as pesquisas sejam mais ageis
        //Mudar exibição mostrando informações mais relevantes na tela (exemplo: Situação do Aluno, Ensino Atual ou Disciplina Atual)

        /// <summary>
        /// Tela de Consulta de Alunos
        /// </summary>
        /// <param name="retornoDeFormulario"></param>
        public frmProcurarAluno(string retornoDeFormulario, IEmatriculaSettings settings)
        {
            InitializeComponent();

            _settings = settings;

            dgvaluno_resultado.DefaultCellStyle.SelectionBackColor = Color.FromArgb(216, 216, 216);
            _retornoDeFormulario = retornoDeFormulario;
        }

        private void FRpesquisaaluno_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DefinirDgvAlunos();

                BuscarAlunos();

                AjustarLarguraColuna();
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void frPesquisaaluno_Shown(object sender, EventArgs e)
        {
            try
            {
                txtNmat.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DefinirDgvAlunos()
        {
            try
            {
                DataGridViewCell cell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxColumn colNDeMatricula = new DataGridViewTextBoxColumn()
                {
                    CellTemplate = cell,
                    Name = "nMatricula",
                    HeaderText = "Nº de Matrícula",
                    DataPropertyName = "nMatricula" // Tell the column which property of FileName it should use
                };
                DataGridViewCell cell2 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxColumn colRg = new DataGridViewTextBoxColumn()
                {
                    CellTemplate = cell2,
                    Name = "rg",
                    HeaderText = "RG",
                    DataPropertyName = "rg" // Tell the column which property of FileName it should use
                };
                DataGridViewCell cell3 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxColumn colNome = new DataGridViewTextBoxColumn()
                {
                    CellTemplate = cell3,
                    Name = "nome",
                    HeaderText = "Nome",
                    DataPropertyName = "nome" // Tell the column which property of FileName it should use
                };

                dgvaluno_resultado.AutoGenerateColumns = false;
                dgvaluno_resultado.Columns.Add(colNDeMatricula);
                dgvaluno_resultado.Columns.Add(colNome);
                dgvaluno_resultado.Columns.Add(colRg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BuscarAlunos()
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                ////ADO
                blAluno = new BindingList<Classes.Aluno>(DAO.AlunoDAO.ExibirTodos(true).ToList());
                dgvaluno_resultado.DataSource = blAluno;

                //EF
                //var db = new Modelo.Modelo();
                //var query = (from a in db.ALUNO
                //             select new {a.N_MAT, a.NOME, a.RG });
                //dgvaluno_resultado.DataSource = query.ToList();

                tssl1.Text = "Total de Alunos: " + dgvaluno_resultado.Rows.Count;

                sw.Stop();
                tempo = sw.Elapsed;
                tssl1.Text += " - Tempo de Resposta:" + tempo.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AjustarLarguraColuna()
        {
            try
            {
                dgvaluno_resultado.Columns[0].Width = 77;
                dgvaluno_resultado.Columns[1].Width = 355;
                dgvaluno_resultado.Columns[2].Width = 137;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dgvaluno_resultado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                n_mat_selecionado = dgvaluno_resultado.Rows[dgvaluno_resultado.CurrentCell.RowIndex].Cells[0].Value.ToString();
                Classes.Aluno objAluno = DAO.AlunoDAO.Consultar(Convert.ToInt32(n_mat_selecionado));
                objAluno.FotoDoAluno = new Classes.FotoAluno(objAluno.NMatricula, _settings);

                if (_retornoDeFormulario == "aluno")
                {
                    frmAluno frmAluno = new frmAluno(_settings, objAluno);
                    frmAluno.MdiParent = this.ParentForm;
                    frmAluno.Show();
                    this.Close();
                }
                else if (_retornoDeFormulario == "historico")
                {
                    frmHistoricoEscolar frmHistoricoEscolar = new frmHistoricoEscolar(_settings);
                    frmHistoricoEscolar.objAluno = objAluno;
                    frmHistoricoEscolar.MdiParent = this.ParentForm;
                    frmHistoricoEscolar.Show();
                    this.Close();
                }
                else if (_retornoDeFormulario == "eliminacoes")
                {
                    frmEliminacoes frmEliminacoes = new frmEliminacoes(_settings);
                    frmEliminacoes.aluno = objAluno;
                    frmEliminacoes.MdiParent = this.ParentForm;
                    frmEliminacoes.ShowDialog();
                    //this.Close();
                }
                else if (_retornoDeFormulario == "passaporte")
                {
                    if (Classes.Aluno.GetDisciplinaAlunoAtual(objAluno) != null)
                    {
                        if (_settings.Ceeja.ToLower() == "americana")
                        {
                            ////TODO: AGORA - RESOLVER A PARADA
                            ////Em Americana, quando o aluno está em uma disciplina concluída, a janela de seleção de disciplina será exibida.
                            //
                            //foreach(ENSINO_ALUNO ensinoAluno in aluno.lis)
                            //
                            //List<Classes.Disciplina> list_ = Notas.AtribuicaoDeDisciplinas.listaDeDisciplinasParaFazer_Encerradas(aluno, false);
                            //if(list_.Exists(x => x.CodDaDisciplina == aluno.SituacaoDoAluno.DisciplinaAtual.CodDaDisciplina))
                            //{
                            //    //Atribuir Disciplina nova por atual estar encerrada
                            //    Formularios.frAtribuicaoDisciplina form = new Formularios.frAtribuicaoDisciplina();
                            //    form.objAluno = aluno;
                            //    form.MdiParent = this.ParentForm;
                            //    form.Show();
                            //    this.Close();
                            //}
                            //else
                            //{
                            //    frmPassaporte form = new frmPassaporte();
                            //    form.objAluno = aluno;
                            //    form.MdiParent = this.ParentForm;
                            //    form.Show();
                            //    this.Close();
                            //}
                        }
                        else
                        {
                            frmPassaporte form = new frmPassaporte(_settings);
                            form.objAluno = objAluno;
                            form.MdiParent = this.ParentForm;
                            form.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Aluno não está em uma disciplina.", "E-mat");

                        frAtribuicaoDisciplina frAtribuicaoDisciplina = new frAtribuicaoDisciplina();
                        frAtribuicaoDisciplina.objAluno = objAluno;
                        frAtribuicaoDisciplina.MdiParent = this.ParentForm;
                        frAtribuicaoDisciplina.Show();
                        this.Close();
                    }
                }
                else
                {
                    //TODO: AGORA - ELIMINAR TALVEZ ESTE TREXO
                    //sql_comm = new SqlCommand(@"SELECT * FROM ALUNOS WHERE N_MAT=@n_mat", sqlHelper.ematConn);
                    //sql_comm.Parameters.AddWithValue("@n_mat", aluno.NMatricula.ToString());
                    //
                    //try
                    //{
                    //    sqlHelper.ematConn.Open();
                    //    SqlDataReader reader = sql_comm.ExecuteReader();
                    //
                    //    while (reader.Read())
                    //    {
                    //        //CSaluno.ensino_aluno = reader["ENSINO"].ToString();
                    //        //if (reader["ID_DISCIPLINA_ATUAL"] != DBNull.Value)
                    //        //{
                    //        //    CSaluno.id_disciplina_atual = Convert.ToInt32(reader["ID_DISCIPLINA_ATUAL"]);
                    //        //}
                    //
                    //    }
                    //    reader.Close();
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message);
                    //}
                    //finally
                    //{
                    //    sqlHelper.ematConn.Close();
                    //}

                    if (_settings.Ceeja.ToLower() == "americana")
                    {
                        Formularios.frAtribuicaoDisciplina form = new Formularios.frAtribuicaoDisciplina();
                        form.objAluno = objAluno;
                        form.MdiParent = this.ParentForm;
                        form.Show();
                        this.Close();
                    }

                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void txtAluno_Click(object sender, EventArgs e)
        {
            try
            {
                txtNmat.Text = "";
                txtRg.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void txtAluno_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                txtNmat.Text = "";
                txtRg.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void txtrg_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                txtNmat.Text = "";
                txtNome.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void txtnmat_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                txtNome.Text = "";
                txtRg.Text = "";
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void txtNmat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Filter = txtNmat.Text.Trim().Replace("'", "''");
                dgvaluno_resultado.DataSource =
                    new BindingList<Classes.Aluno>(blAluno.Where(m => m.NMatricula.ToString().StartsWith(Filter)).ToList<Classes.Aluno>());

                dgvaluno_resultado.DataSource =
                    new BindingList<Classes.Aluno>(blAluno.Where(m => m.NMatricula.ToString().StartsWith(Filter)).ToList<Classes.Aluno>());
            }
            catch (Exception ex)
            {
                new ToolTip().SetToolTip(txtNmat, ex.Message);
            }
        }

        private void txtAluno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Filter = txtNome.Text.Trim().Replace("'", "''");
                dgvaluno_resultado.DataSource =
                    new BindingList<Classes.Aluno>(blAluno.Where(m => m.Nome.StartsWith(Filter)).ToList<Classes.Aluno>());
            }
            catch (Exception ex)
            {
                new ToolTip().SetToolTip(txtNome, ex.Message);
            }
        }

        private void txtRg_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Filter = txtRg.Text.Trim().Replace("'", "''");
                dgvaluno_resultado.DataSource =
                    new BindingList<Classes.Aluno>(blAluno.Where(m => m.Rg.ToString().StartsWith(Filter)).ToList<Classes.Aluno>());
            }
            catch (Exception ex)
            {
                new ToolTip().SetToolTip(txtRg, ex.Message);
            }
        }
    }
}
