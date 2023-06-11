using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Security.Principal;
using System.Diagnostics;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frImpressao_etiqueta : Form
    {
        private readonly IEmatriculaSettings _settings;

        decimal folhas = 0;
        List<int> listaNMatricula = new List<int>();

        public frImpressao_etiqueta(IEmatriculaSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                List<Classes.Aluno> listaAlunosImprimir = new List<Classes.Aluno>();

                for (int z = 0; z < listaNMatricula.Count; z++)
                {
                    Classes.Aluno aluno = DAO.AlunoDAO.Consultar(listaNMatricula[z]);
                    if (aluno != null)
                        listaAlunosImprimir.Add(aluno);
                }
                
                //var report = Relatorios.sorocaba.RelatoriosSorocaba.gera_crystal_etiqueta_prontuario_sorocaba(listaAlunosImprimir);
                var report = Relatorios.Escolas.Sorocaba.RelatoriosSorocaba.GerarEtiquetaProntuario50x101Carta(listaAlunosImprimir, _settings);
                //TODO:: APRESENTAR DIALOGO DE IMPRESSORA FEITOPOR MIM MESMO 
                if (report != null)
                {
                    frCrystalReport frCrystalReport = new frCrystalReport();
                    frCrystalReport.cr_report = report;
                    frCrystalReport.ShowDialog();
                    //report.PrintToPrinter(1, false, 0, 0);
                    //listaNMatricula.RemoveAll(item => listaAlunosImprimir.Select(x => x.NMatricula).Contains(item));
                }

                /*
                decimal paginas = listaNMatricula.Count / 10;

                for (int i = 0; i < paginas; i++)
                {
                    List<Classes.Aluno> listaAlunosImprimir = new List<Classes.Aluno>();
                    for( int z = 0; z < listaNMatricula.Count; z++)
                    {
                        Classes.Aluno aluno = DAO.AlunoDAO.Consultar(listaNMatricula[z]);
                        if (aluno != null)
                            listaAlunosImprimir.Add(aluno);

                        if (listaAlunosImprimir.Count == 10)
                            break;
                    }
                    
                    if(listaAlunosImprimir.Count == 10)
                    {
                        //var report = Relatorios.sorocaba.RelatoriosSorocaba.gera_crystal_etiqueta_prontuario_sorocaba(listaAlunosImprimir);
                        var report = Relatorios.sorocaba.RelatoriosSorocaba.GerarEtiquetaProntuario50x101Carta(listaAlunosImprimir);
                        //TODO:: APRESENTAR DIALOGO DE IMPRESSORA FEITOPOR MIM MESMO 
                        if (report != null)
                        {
                            report.PrintToPrinter(1, false, 0, 0);
                            listaNMatricula.RemoveAll(item => listaAlunosImprimir.Select(x => x.NMatricula).Contains(item));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Numero insuficiente de etiquetas para imprimir");


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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAddEtiqueta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAddEtiqueta.Text != string.Empty)
                {
                    btnAddEtiqueta.Enabled = true;
                }
                else btnAddEtiqueta.Enabled = false;
            }
            catch (Exception ex)
            {

                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            
        }

        private void btnAddEtiqueta_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAddEtiqueta.Text != string.Empty)
                {
                    if (txtAddEtiqueta.Text.Contains("-"))
                    {
                        string[] linha = txtAddEtiqueta.Text.Split('-');

                        if (linha.Length == 2)
                        {
                            int.TryParse(linha[0], out int a);
                            int.TryParse(linha[1], out int b);

                            if (a < b && ((b - a) < 201))
                            {
                                for (int i = a; i <= b; i++)
                                {
                                    listaNMatricula.Add(i);
                                }
                            }
                            else
                            {
                                ErrorLog.ErrorHandleService.ExibirMsgBoxError("Parâmetros de entrada incorreto.");
                            }
                        }
                        txtAddEtiqueta.TextChanged -= txtAddEtiqueta_TextChanged;
                        txtAddEtiqueta.Text = string.Empty;
                        btnAddEtiqueta.Enabled = false;
                        txtAddEtiqueta.TextChanged += txtAddEtiqueta_TextChanged;
                    }
                    else
                    {
                        if (int.TryParse(txtAddEtiqueta.Text, out int i))
                        {
                            listaNMatricula.Add(i);
                            txtAddEtiqueta.TextChanged -= txtAddEtiqueta_TextChanged;
                            txtAddEtiqueta.Text = string.Empty;
                            btnAddEtiqueta.Enabled = false;
                            txtAddEtiqueta.TextChanged += txtAddEtiqueta_TextChanged;
                        }
                    }

                    lblQtdEtiquetas.Text = "Etiquetas: " + listaNMatricula.Count;

                    if (listaNMatricula.Count > 0)
                    {
                        ltbLista.DataSource = null;

                        ltbLista.DataSource = listaNMatricula;
                        decimal listaqtd = listaNMatricula.Count;
                        folhas = Math.Ceiling(listaqtd / 10);
                        lblQtdFolhas.Text = "Folhas: " + folhas;
                    }

                }
            }
            catch (Exception ex)
            {

                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ltbLista_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
