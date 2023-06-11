using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;


using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Alunos
{
    public partial class frPesquisaaluno : Form
    {
        SqlQuery consulta = new SqlQuery(Conexoes.GetSqlConnection());
        SqlConnection conn = Conexoes.GetSqlConnection();
        DataTable tbAlunos = new DataTable();
        BindingSource bsAlunos = new BindingSource();

        csAlunos cs_alunos = new csAlunos();
        Error_Log.csControle_erros cs_erros = new Error_Log.csControle_erros();

        public string tipo_form = "nota";

        TimeSpan tempo;

        DataTable _table = new DataTable();

        List<csAlunos.n_mat_antigo> lista_n_mat_antigos = new List<csAlunos.n_mat_antigo>();

        public frPesquisaaluno()
        {
            InitializeComponent();
        }

        public void filtra()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //Concluentes
            if (!ckbConcluentes.Checked)
            {
                _table = cs_alunos.tab_alunos_nao_concluentes(txtAluno.Text, txtrg.Text, txtnmat.Text);
            }
            else
            //Não Concluentes
            {
                _table = cs_alunos.tab_alunos_concluentes(txtAluno.Text, txtrg.Text, txtnmat.Text);
            }           

            bsAlunos.DataSource = _table;

            dgvaluno_resultado.DataSource = bsAlunos;

            //Caso não seja encontrado nenhum aluno, irá iniciar a busca na lista dos antigos
            if (Configurações.csEscola.cidade.ToLower() == "americana")
            {
                if(_table.Rows.Count < 1)
                {
                    lista_n_mat_antigos = cs_alunos.lista_n_mat_antigos(txtnmat.Text);

                    if(lista_n_mat_antigos.Count > 0)
                    {
                        _table = cs_alunos.tab_alunos_nao_concluentes("","",lista_n_mat_antigos[0].n_mat);
                    }
                }
                
            }

            tssl1.Text = "Total de Alunos: " + dgvaluno_resultado.Rows.Count;

            larguracoluna();

            sw.Stop();

            tempo = sw.Elapsed;

            tssl1.Text += " - Tempo de Resposta:" + tempo.ToString();
            
        }              

        private void txtAluno_TextChanged(object sender, EventArgs e)
        {
            _table.DefaultView.RowFilter = string.Format("nome LIKE '{0}%'" , txtAluno.Text);

            bsAlunos.DataSource = _table;
            dgvaluno_resultado.DataSource = bsAlunos;     
        }

        private void FRpesquisaaluno_Load(object sender, EventArgs e)
        {
            tssl1.Text = "";

            //Cor
            dgvaluno_resultado.DefaultCellStyle.SelectionBackColor = Color.FromArgb(216, 216, 216);            

            filtra();
        }

        public void larguracoluna()
        {
            dgvaluno_resultado.Columns[0].Width = 373;
            dgvaluno_resultado.Columns[1].Width = 141;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            filtra();
        }

        //Opções de Forms
        private void dgvaluno_resultado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string n_mat_pesquisa = dgvaluno_resultado.Rows[dgvaluno_resultado.CurrentCell.RowIndex].Cells[2].Value.ToString();


            Cursor.Current = Cursors.WaitCursor;            
            CSaluno.numat = dgvaluno_resultado.Rows[dgvaluno_resultado.CurrentCell.RowIndex].Cells[2].Value.ToString();
            CSaluno.nome_aluno = dgvaluno_resultado.Rows[dgvaluno_resultado.CurrentCell.RowIndex].Cells[0].Value.ToString();
            CSaluno.rg_aluno = dgvaluno_resultado.Rows[dgvaluno_resultado.CurrentCell.RowIndex].Cells[1].Value.ToString();                       
            
            if(tipo_form == "edit")
            {
                frEditaluno form = new frEditaluno();
                form.n_mat_pesquisa = n_mat_pesquisa;
                form.MdiParent = this.ParentForm;
                form.Show();
                this.Close();
            }
            else if (tipo_form == "nota")
            {
                if (cs_alunos.ensino(CSaluno.numat) != "")
                {
                    Notas.frAtrib_nota form = new Notas.frAtrib_nota();
                    form.MdiParent = this.ParentForm;
                    form.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(cs_erros.msg_aluno_sem_ensino(), "Ensino não informado");
                }
            }
            else if (tipo_form == "historico")
            {
                if (cs_alunos.ensino(CSaluno.numat) != "")
                {
                    Notas.frHistorico form = new Notas.frHistorico();
                    form.nmat = CSaluno.numat;
                    form.MdiParent = this.ParentForm;
                    form.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(cs_erros.msg_aluno_sem_ensino(), "Ensino não informado");
                }
            }
            else if (tipo_form == "passaporte")
            {
                if(cs_alunos.ensino(CSaluno.numat)!="")
                {
                    if (cs_alunos.id_disciplina_atual__(CSaluno.numat) > 0)
                    {
                        Notas.frPassaporte form = new Notas.frPassaporte();
                        form.n_mat = dgvaluno_resultado.Rows[dgvaluno_resultado.CurrentCell.RowIndex].Cells[2].Value.ToString();
                        form.MdiParent = this.ParentForm;
                        form.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Aluno não está em uma disciplina.", "E-mat");

                        Notas.frAtribDisciplina form = new Notas.frAtribDisciplina();
                        form.n_mat = CSaluno.numat;
                        form.MdiParent = this.ParentForm;
                        form.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(cs_erros.msg_aluno_sem_ensino(), "Ensino não informado");
                }
                
            }
            else
            {
                if (cs_alunos.ensino(CSaluno.numat) != "")
                {
                    SqlCommand sql_comm1 = new SqlCommand();

                    sql_comm1.Connection = conn;
                    sql_comm1.CommandText = @"SELECT * FROM ALUNOS WHERE N_MAT=@n_mat";

                    sql_comm1.Parameters.AddWithValue("@n_mat", CSaluno.numat);

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = sql_comm1.ExecuteReader();

                        while (reader.Read())
                        {
                            CSaluno.ensino_aluno = reader["ENSINO"].ToString();
                            if (reader["ID_DISCIPLINA_ATUAL"] != DBNull.Value)
                            {
                                CSaluno.id_disciplina_atual = Convert.ToInt32(reader["ID_DISCIPLINA_ATUAL"]);
                            }

                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }

                    if (Configurações.csEscola.cidade.ToLower() == "americana")
                    {
                        Notas.frAtribDisciplina form = new Notas.frAtribDisciplina();
                        form.n_mat = CSaluno.numat;
                        form.MdiParent = this.ParentForm;
                        form.Show();
                        this.Close();

                    }
                    else
                    {
                        Notas.frAtrib_materia form = new Notas.frAtrib_materia();
                        form.MdiParent = this.ParentForm;
                        form.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(cs_erros.msg_aluno_sem_ensino(), "Ensino não informado");
                }
            }
            
            Cursor.Current = Cursors.Default;

        }        

        private void txtAluno_Click(object sender, EventArgs e)
        {            
            txtnmat.Text = "";
            txtrg.Text = "";
            filtra();
        }

        #region //Limpa txtBox

        private void txtAluno_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtnmat.Text = "";
            txtrg.Text = "";
        }

        private void txtrg_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtnmat.Text = "";
            txtAluno.Text = "";
        }

        private void txtnmat_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtAluno.Text = "";
            txtrg.Text = "";
        }

        #endregion

        private void ckbConcluentes_CheckedChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            filtra();
            Cursor.Current = Cursors.Default;
        }

        private void frPesquisaaluno_Shown(object sender, EventArgs e)
        {
            txtAluno.Focus();
        }
    }
 
}
    