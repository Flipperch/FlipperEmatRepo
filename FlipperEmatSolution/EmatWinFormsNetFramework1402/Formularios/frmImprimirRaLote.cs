using EmatWinFormsNetFramework1402.Classes;
using EmatWinFormsNetFramework1402.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frmImprimirRaLote : Form
    {
        private readonly IEmatriculaSettings _settings;
        decimal folhas = 0;

        public frmImprimirRaLote(IEmatriculaSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        //lblQtdEtiquetas.Text = "Etiquetas: " + listaNMatricula.Count;

        //if (listaNMatricula.Count > 0)
        //{
        //    ltbLista.DataSource = null;
        //
        //    ltbLista.DataSource = listaNMatricula;
        //    decimal listaqtd = listaNMatricula.Count;
        //    folhas = Math.Ceiling(listaqtd / 10);
        //    //lblQtdFolhas.Text = "Folhas: " + folhas;
        //}

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //Montar lista de Alunos com base nas linhas selecionadas.
                List<Aluno> listaAlunosImprimirSelecionadas = new List<Aluno>();

                var checkedRows = (from DataGridViewRow r in dgvAlunos.Rows
                                   where Convert.ToBoolean(r.Cells[1].Value) == true
                                   select r).ToList();

                foreach (var row in checkedRows)
                {
                    listaAlunosImprimirSelecionadas.Add((Aluno)row.Cells["colAluno"].Value);
                }

                var report = Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptRaVotorantimLote(listaAlunosImprimirSelecionadas, _settings);

                if (report != null)
                {
                    frCrystalReport frCrystalReport = new frCrystalReport();
                    frCrystalReport.cr_report = report;
                    frCrystalReport.ShowDialog();
                }

                //TODO:: ATUALIZAÇÃO DE IMPRESSO ESTA SENDO FEITA APÓS GERAR AS FOLHAS E MOSTRAR O FORM DE VISUALIZAÇÃO -VERIFICAR MELHOR SOLUÇÃO

                foreach (var aluno in listaAlunosImprimirSelecionadas)
                {
                    AtualizarCartaoImpresso(aluno.NMatricula);
                }

                this.Close();

                //código que abre folha por folha - modelo antigo
                /*
                folhas = listaAlunosImprimirSelecionadas.Count / 5;
                int l = 0;
                for (int i = 0; i < folhas; i++)
                {
                    for (int z = 0; z < listaAlunosImprimirSelecionadas.Count - 1; z++)
                    {
                        //Classes.Aluno aluno = DAO.AlunoDAO.Consultar(listaNMatricula[z]);
                        //if (aluno != null)
                        listaAlunosImprimirFolha.Add(listaAlunosImprimirSelecionadas[l]);
                        l++;

                        if (listaAlunosImprimirFolha.Count == 5)
                        {
                            var report = Relatorios.votorantim.RelatoriosVotorantim.rptRaVotorantimLote(listaAlunosImprimirFolha);

                            //TODO:: APRESENTAR DIALOGO DE IMPRESSORA FEITOPOR MIM MESMO 
                            if (report != null)
                            {
                                Relatorios.frCrystalReport frCrystalReport = new Relatorios.frCrystalReport();
                                frCrystalReport.cr_report = report;
                                frCrystalReport.ShowDialog();

                                listaAlunosImprimirFolha.Clear();
                                //report.PrintToPrinter(1, false, 0, 0);



                                //listaAlunosImprimirSelecionadas.RemoveAll(item => listaAlunosImprimirFolha.Select(x => x.N_MAT).Contains(item));
                            }

                            //break;
                        }
                    }
                }
                */

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void frmImprimirRaLote_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                List<int> listaNMatricula = new List<int>();
                //Carregar lista de cartoes que não tiveram Ra impresso
                //Carregar lista de alunos com base na lista de cartoes
                foreach (var cartao in CartoesNaoImpressos())
                {
                    if (File.Exists(_settings.DiretorioFotos + @"\" + cartao + ".png"))
                    {
                        listaNMatricula.Add(cartao);
                    }
                }

                //Exibir no dgv dados do aluno
                foreach (Aluno aluno in ListaAlunosParaImprimirRa(listaNMatricula))
                {
                    dgvAlunos.Rows.Add();
                    dgvAlunos.Rows[dgvAlunos.Rows.Count - 1].Cells[0].Value = aluno; //objeto aluno para ser utilizado no metodos
                    dgvAlunos.Rows[dgvAlunos.Rows.Count - 1].Cells[1].Value = 1; //checkboxcolumn
                    dgvAlunos.Rows[dgvAlunos.Rows.Count - 1].Cells[2].Value = aluno.NMatricula;
                    dgvAlunos.Rows[dgvAlunos.Rows.Count - 1].Cells[3].Value = aluno.Nome;
                    dgvAlunos.Rows[dgvAlunos.Rows.Count - 1].Cells[4].Value = aluno.DtMatricula;
                }

                //Altera Quantidade de Aluno
                lblQtdAlunos.Text = "Quantidade de Alunos: " + dgvAlunos.Rows.Count;

                //Altera Quantidade de Folhas
                folhas = dgvAlunos.Rows.Count / 5;
                lblQtdFolhas.Text = "Quantidade de Folhas:" + folhas;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

            foreach (DataGridViewRow row in dgvAlunos.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["colSelect"];
                chk.Value = false;
            }

            Cursor.Current = Cursors.Default;

        }

        public List<int> CartoesNaoImpressos()
        {
            List<int> retorno = new List<int>();
            try
            {
                SqlHelper sqlHelper = new SqlHelper();
                SqlCommand sqlComm = new SqlCommand("SELECT * FROM CARTAO_ACESSO WHERE IMPRESSO = 0", sqlHelper.ematConn);
                sqlComm.CommandType = CommandType.Text;

                sqlHelper.ematConn.Open();
                SqlDataReader reader = sqlComm.ExecuteReader();
                while (reader.Read())
                {
                    retorno.Add(Convert.ToInt32(reader["ALUNO"]));
                }
                reader.Close();
                sqlHelper.ematConn.Close();
                return retorno;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                return null;
            }
        }

        public List<Aluno> ListaAlunosParaImprimirRa(List<int> listaNmat)
        {
            List<Aluno> retorno = new List<Aluno>();
            try
            {
                SqlHelper sqlHelper = new SqlHelper();
                SqlCommand sqlComm = new SqlCommand("SELECT N_MAT,NOME,RG, DT_MAT FROM ALUNO WHERE N_MAT = @n_mat AND ATIVO = 1 AND CONCLUINTE = 0", sqlHelper.ematConn);
                sqlComm.CommandType = CommandType.Text;

                sqlHelper.ematConn.Open();

                foreach (int nmat in listaNmat)
                {
                    sqlComm.Parameters.Clear();
                    sqlComm.Parameters.AddWithValue("@n_mat", nmat);

                    SqlDataReader reader = sqlComm.ExecuteReader();
                    while (reader.Read())
                    {
                        Aluno aluno = new Aluno
                        {
                            Nome = reader["NOME"].ToString(),
                            NMatricula = Convert.ToInt32(reader["N_MAT"].ToString()),
                            DtMatricula = DateTime.Parse(reader["DT_MAT"].ToString()),
                            Rg = reader["RG"].ToString()
                        };
                        aluno.ListaEnsinoAluno = DAO.EnsinoAlunoDAO.ExibirTodos(aluno, false);

                        retorno.Add(aluno);
                    }
                    reader.Close();
                }

                sqlHelper.ematConn.Close();
                return retorno;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                return null;
            }
        }

        public void AtualizarCartaoImpresso(int aluno)
        {
            //UPDATE PARA ALTERAR CODIGO IMPRESSO DE 0 PARA 1 NA TABELA CARTAO_ACESSO
            try
            {
                SqlHelper sqlHelper = new SqlHelper();
                SqlCommand sqlComm = new SqlCommand("UPDATE CARTAO_ACESSO SET IMPRESSO = 1 WHERE ALUNO = @aluno", sqlHelper.ematConn);
                sqlComm.Parameters.AddWithValue("@aluno", aluno);
                sqlComm.CommandType = CommandType.Text;

                sqlHelper.ematConn.Open();
                if (sqlHelper.ematConn.State == ConnectionState.Open)
                {
                    sqlComm.ExecuteNonQuery();

                    sqlHelper.ematConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //public void InserirMenssagemCartaoImpresso(int aluno)
        //{
        //    //UPDATE PARA ALTERAR CODIGO IMPRESSO DE 0 PARA 1 NA TABELA CARTAO_ACESSO
        //    try
        //    {
        //        SqlConnection sqlConn = new SqlConnection(Utils.Settings.ConnectionString);
        //        SqlCommand sqlComm = new SqlCommand("UPDATE ALUNO SET OBS_PASSAPORTE = WHERE ALUNO = @aluno", sqlConn);
        //        sqlComm.Parameters.AddWithValue("@aluno", aluno);
        //        sqlComm.CommandType = CommandType.Text;
        //
        //        sqlConn.Open();
        //        if (sqlConn.State == ConnectionState.Open)
        //        {
        //            sqlComm.ExecuteNonQuery();
        //
        //            sqlConn.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //
        //
        //}

        private void btnSelecionarTudo_Click(object sender, EventArgs e)
        {
            if (btnSelecionarTudo.Text == "Selecionar 50")
            {
                int i = 0;
                foreach (DataGridViewRow row in dgvAlunos.Rows)
                {
                    if (i < 50)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["colSelect"];
                        chk.Value = true;
                        //chk.Value = !(chk.Value == null ? false : (bool)chk.Value); //because chk.Value is initialy null
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }

                btnSelecionarTudo.Text = "Desmarcar Tudo";
            }
            else
            {
                foreach (DataGridViewRow row in dgvAlunos.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["colSelect"];
                    chk.Value = false;
                }

                btnSelecionarTudo.Text = "Selecionar 50";
            }


        }
    }
}
