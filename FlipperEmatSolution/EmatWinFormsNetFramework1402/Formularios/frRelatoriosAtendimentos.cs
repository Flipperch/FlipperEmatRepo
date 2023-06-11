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
    public partial class frRelatoriosAtendimentos : Form
    {
        DateTime dtInicial;
        DateTime dtFinal;
        List<Classes.Disciplina> listaDeDisciplinas;
        string periodo_alteracoes;
        public frRelatoriosAtendimentos()
        {
            InitializeComponent();
        }
        private void frRelatoriosAtendimentos_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            filtrar();
            preencherDgvQtdAlunosDisciplina();//Não altera de acordo com a data selecionada
            Cursor.Current = Cursors.Default;
        }               
        #region Métodos
        private void filtrar()
        {
            /*
            Cursor.Current = Cursors.WaitCursor;
            #region Obter valor da data e hora dos dps
            dtInicial = DateTime.Parse(dtpMatriculas_ini.Value.ToString("dd/MM/yyyy") + " 08:00:00");
            dtFinal = DateTime.Parse(dtpMatriculas_fin.Value.ToString("dd/MM/yyyy") + " 22:00:00");
            #endregion
            #region Carregar lista de Disciplinas e struct de detalhes de cada disciplina
            listaDeDisciplinas = Classes.Disciplina.listaDeDisciplinas();
            for (int i = 0; i < listaDeDisciplinas.Count; i++)
            {
                listaDeDisciplinas[i].DetalhesDaDisciplina =
                    Utils.csDetalhesDisciplina.detalhesDaDisciplina(dtInicial, dtFinal, listaDeDisciplinas[i].CodDaDisciplina);
            }
            #endregion
            #region Exibir dados no dataGridView
            preencherDgvQtdAtendimentos();
            #endregion
            Cursor.Current = Cursors.Default;
            */
        }
        private void preencherDgvQtdAtendimentos()
        {
            /*
            dgvQtdAtendimentos.Rows.Clear();
            for (int i = 0; i < listaDeDisciplinas.Count; i++)
            {
                dgvQtdAtendimentos.Rows.Add();
                dgvQtdAtendimentos.Rows[i].Cells[0].Value = listaDeDisciplinas[i].NomeDaDisciplina;
                dgvQtdAtendimentos.Rows[i].Cells[1].Value = 
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdIniciantesFundamental +
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdRetornosFundamental +
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAtendimentosFundamental +
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAvaliacoesFundamental +
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdConcluintesFundamental; //Fun
                dgvQtdAtendimentos.Rows[i].Cells[2].Value =
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdIniciantesMedio +
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdRetornosMedio +
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAtendimentosMedio +
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAvaliacoesMedio +
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdConcluintesMedio; //Med
                dgvQtdAtendimentos.Rows[i].Cells[3].Value = listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAtendimentosFundamental +
                                                          listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAtendimentosMedio; //Total
            }
            */
        }
        private void preencherDgvQtdAlunosDisciplina()
        {
            /*
            dgvQtdAlunosDisciplina.Rows.Clear();

            for (int i = 0; i < listaDeDisciplinas.Count; i++)
            {
                dgvQtdAlunosDisciplina.Rows.Add();
                dgvQtdAlunosDisciplina.Rows[i].Cells[0].Value = listaDeDisciplinas[i].NomeDaDisciplina;
                dgvQtdAlunosDisciplina.Rows[i].Cells[1].Value = listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosFundamentalAtivo +
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosFundamentalInativo; //Fun
                dgvQtdAlunosDisciplina.Rows[i].Cells[2].Value = listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosMedioAtivo +
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosMedioInativo;
                dgvQtdAlunosDisciplina.Rows[i].Cells[3].Value = listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosFundamentalAtivo +
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosFundamentalInativo +
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosMedioAtivo +
                    listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosMedioInativo; //Total
            }
            */
        }
        private DataTable dtDisciplinasParaImpressao()
        {
            
            DataTable retorno = new DataTable();
            /*
            //DataTable            
            retorno.TableName = "QtdAlunosDisciplina";
            retorno.Columns.Add("DISCIPLINA");
            retorno.Columns.Add("FUNDAMENTAL_ATV");
            retorno.Columns.Add("FUNDAMENTAL_INA");
            retorno.Columns.Add("MEDIO_ATV");
            retorno.Columns.Add("MEDIO_INA");
            retorno.Columns.Add("TOTAL_ATV");
            retorno.Columns.Add("TOTAL_INA");

            for (int i = 0; i < listaDeDisciplinas.Count; i++)
            {
                retorno.Rows.Add();
                retorno.Rows[i][0] = listaDeDisciplinas[i].NomeDaDisciplina;
                retorno.Rows[i][1] = listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosFundamentalAtivo;// ativo
                retorno.Rows[i][2] = listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosFundamentalInativo;// inativo
                retorno.Rows[i][3] = listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosMedioAtivo; //ativo
                retorno.Rows[i][4] = listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosMedioInativo; //inativo
                retorno.Rows[i][5] = listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosFundamentalAtivo + listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosMedioAtivo;
                retorno.Rows[i][6] = listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosFundamentalInativo + listaDeDisciplinas[i].DetalhesDaDisciplina.qtdAlunosMedioInativo;
            }

            
            */
            return retorno;
        }
        
        private DataTable dtAtendimentosParaImpressao()
        {

            //string dat_ini = dtpMatriculas_ini.Value.ToString("dd/MM/yyyy");
            //string dat_fin = dtpMatriculas_fin.Value.ToString("dd/MM/yyyy");
            //periodo_alteracoes = dat_ini + " - " + dat_fin;
            //dat_fin = dtpMatriculas_fin.Value.AddDays(1).ToString("dd/MM/yyyy");

            //if (!dtpMatriculas_fin.Checked)
            //{
            //    dat_fin = dtpMatriculas_ini.Value.AddDays(1).ToString("dd/MM/yyyy");
            //    periodo_alteracoes = dat_ini;
            //}

            //dt_atend.Rows.Clear();

            //for (int i = 0; i < listaDeDisciplinas.Count; i++)
            //{
            //    dt_atend.Rows.Add();
            //    dt_atend.Rows[i][0] = listaDeDisciplinas[i].NomeDaDisciplina;
            //    int fun = Classes.Atendimento.qtd_atendimentos(listaDeDisciplinas[i].CodDaDisciplina, dat_ini, dat_fin, 1);
            //    dt_atend.Rows[i][1] = fun;
            //    int med = Classes.Atendimento.qtd_atendimentos(listaDeDisciplinas[i].CodDaDisciplina, dat_ini, dat_fin, 2);
            //    dt_atend.Rows[i][2] = med;
            //    dt_atend.Rows[i][3] = fun + med;
            //}

            DataTable retorno = new DataTable();
            //DataTable - Cria apenas uma vêz           
            retorno.TableName = "QtdAtendimentos";
            retorno.Columns.Add("DISCIPLINA");
            retorno.Columns.Add("FUNDAMENTAL");
            retorno.Columns.Add("MEDIO");
            retorno.Columns.Add("TOTAL");
            return retorno;
        }

        #endregion        
        #region Eventos
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            filtrar();
        }
        private void btnImprimirMatriculas_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DateTime data_ = DateTime.Now;

            frCrystalReport form = new frCrystalReport();

            List<bool> list_sup_secoes = new List<bool>();

            if (ckbAtendimentos.Checked) list_sup_secoes.Add(false);
            else list_sup_secoes.Add(true);
            if (ckbAlunosporDisciplina.Checked) list_sup_secoes.Add(false);
            else list_sup_secoes.Add(true);
            if (ckbGrafico.Checked) list_sup_secoes.Add(false);
            else list_sup_secoes.Add(true);

            //form.cr_report = cs_relatorios.gera_crystal_relatorio_atendimentos(list_sup_secoes, periodo_alteracoes, dt_atend, dt_disc, DateTime.Now);

            if (ckbDetalhado.Checked)
            {

            }

            form.Show();
        }

        private void dtpMatriculas_ini_ValueChanged(object sender, EventArgs e)
        {
            dtpMatriculas_fin.MinDate = dtpMatriculas_ini.Value;
        }

        private void dtpMatriculas_fin_ValueChanged(object sender, EventArgs e)
        {
        }


        #endregion

        
    }
}
