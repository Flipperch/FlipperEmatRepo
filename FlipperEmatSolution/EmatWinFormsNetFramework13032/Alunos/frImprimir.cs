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
    public partial class frImprimir : Form
    {
        //Dados Aluno - Vem do outro formulário
        public string n_mat;
        public string nome;
        public string sexo;
        public string termo;
        public string cel;
        public string tel;
        public string ra;
        public string rg;
        public string exp_rg;
        public int id_ensino;
        public string dat_nasc;
        public string cidade_nasc;
        public string estado_nasc;
        public string cpf;
        public string dat_mat;

        public int ini_n_mat = 0;
        public int fim_n_mat = 0;

        Relatorios.csRelatorios cs_relatorios = new Relatorios.csRelatorios();
        Notas.csNotas cs_notas = new Notas.csNotas();
        Alunos.csAlunos cs_alunos = new csAlunos();

        public frImprimir()
        {
            InitializeComponent();
        }

        private void frImprimir_Load(object sender, EventArgs e)
        {
            cmbOpcaoRelatorio.Items.Clear();
            cmbOpcaoRelatorio.DataSource = list_opcoes_impressao(Configurações.csEscola.cidade.ToLower());

            mtbDataRelatorio.Text = DateTime.Now.ToString();
        }

        public List<string> list_opcoes_impressao(string escola_)
        {
            List<string> list_ = new List<string>();

            if (escola_ == "americana")
            {
                #region Americana
                list_.Add("Declaração de Matrícula");
                list_.Add("Declaração de Comparecimento");
                list_.Add("Declaração de Conclusão");
                list_.Add("Declaração com Disciplinas Eliminadas");
                list_.Add("Requerimento de Matrícula");
                list_.Add("Etiquetas Passaporte");
                list_.Add("Carteirinha");
                #endregion

            }

            if (escola_ == "sorocaba")
            {
                #region Sorocaba
                //list_.Add("Cartão");
                //list_.Add("Declaração");
                //list_.Add("Etiqueta");
                //list_.Add("Passaporte");
                list_.Add("Requerimento de Matrícula");
                #endregion
            }

            if (escola_ == "votorantim")
            {
                #region Votorantim

                #endregion
            }

            return list_;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DateTime data_ = DateTime.Now;
            cs_relatorios.data_relatorio_ = data_;

            if (DateTime.TryParse(mtbDataRelatorio.Text, out data_))
                cs_relatorios.data_relatorio_ = data_;

            Relatorios.frCrystalReport form = new Relatorios.frCrystalReport();

            List<string> list_ = new List<string>();

            if (txtNmatInicial.Text != string.Empty)
            {
                ini_n_mat = Convert.ToInt32(txtNmatInicial.Text);
                fim_n_mat = Convert.ToInt32(txtNmatInicial.Text);

                if (txtNmatFinal.Text != string.Empty)
                {
                    fim_n_mat = Convert.ToInt32(txtNmatFinal.Text);
                }

                for (int i = ini_n_mat; i <= fim_n_mat; i++)
                {
                    list_.Add(i.ToString());
                }
            }
            else
            {
                list_.Add(n_mat);
            }

            if (Configurações.csEscola.cidade.ToLower() == "americana")
            {                
                #region Americana
                
                switch (cmbOpcaoRelatorio.Text)
                {
                    case "Declaração de Matrícula":
                        form.cr_report = cs_relatorios.gera_crystal_declaracao_matricula_americana(n_mat, nome, rg, id_ensino);
                        break;
                    case "Declaração de Comparecimento":
                        form.cr_report = cs_relatorios.gera_crystal_declaracao_comparecimento_americana(n_mat, nome, rg, id_ensino);
                        break;
                    case "Declaração de Conclusão":
                        form.cr_report = cs_relatorios.gera_crystal_declaracao_conclusao_americana(nome, rg, id_ensino);
                        break;
                    case "Declaração com Disciplinas Eliminadas":
                        form.cr_report = cs_relatorios.gera_crystal_declaracao_elimi_americana(n_mat, nome, rg, id_ensino, cs_notas.lista_notas_concluidas(n_mat, id_ensino));
                        break;
                    case "Requerimento de Matrícula":
                        form.cr_report = cs_relatorios.gera_crystal_requerimento_americana(n_mat);
                        break;
                    case "Etiquetas Passaporte":
                        form.cr_report = cs_relatorios.gera_crystal_etiqueta_passaporte_americana(list_);
                        break;
                    case "Carteirinha":
                        form.cr_report = cs_relatorios.gera_crystal_cartao_americana(list_);
                        break;
                }

                foreach (Form fr in Application.OpenForms)
                {
                    if (fr.Name == "frPrincipal")
                    {
                        form.MdiParent = fr;
                    }
                }

                this.Close();

                form.Show();

                #endregion
            }

            if (Configurações.csEscola.cidade.ToLower() == "sorocaba")
            {                
                #region Sorocaba
             
                switch (cmbOpcaoRelatorio.Text)
                {
                    case "Cartão":
                        form.cr_report = cs_relatorios.gera_crystal_ra_sorocaba(n_mat);
                        break;
                    case "Declaração de Conclusão":
                        form.cr_report = cs_relatorios.gera_crystal_declaracao_conclusao_sorocaba();
                        break;
                    case "Declaração de Matrícula":
                        form.cr_report = cs_relatorios.gera_crystal_declaracao_matricula_sorocaba();
                        break;
                    case "Etiqueta Prontuário": //Verificar com será feito aqui pois as etiquetas de prontuario são juntas
                        frImpressao_etiqueta form_ = new frImpressao_etiqueta();
                        form.ShowDialog();
                        break;
                    case "Passaporte":
                        if (id_ensino == 1) cs_relatorios.gera_pass_f_sorocaba(n_mat, nome, rg, dat_mat, termo, tel, cel);

                        else if (id_ensino == 2) cs_relatorios.gera_pass_m_sorocaba(n_mat, nome, rg, dat_mat, termo, tel, cel);

                        break;
                    case "Requerimento de Matrícula":
                        form.cr_report = cs_relatorios.gera_crystal_requerimento_sorocaba(n_mat);
                        break;
                }

                foreach (Form fr in Application.OpenForms)
                {
                    if (fr.Name == "frPrincipal")
                    {
                        form.MdiParent = fr;
                    }
                }

                this.Close();

                form.Show();

                #endregion
            }

            ini_n_mat = 0;
            fim_n_mat = 0;
            Cursor.Current = Cursors.Default;
        }

        private void cmbOpcaoRelatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOpcaoRelatorio.Text == "Etiquetas Passaporte" || cmbOpcaoRelatorio.Text == "Carteirinha")
            {
                txtNmatInicial.Enabled = true;
                txtNmatFinal.Enabled = true;
            }
            else
            {
                txtNmatInicial.Enabled = false;
                txtNmatFinal.Enabled = false;
                txtNmatInicial.Text = string.Empty;
                txtNmatFinal.Text = string.Empty;
            }
        }
    }
}
