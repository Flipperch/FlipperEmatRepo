using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Principal
{
    public partial class frSplash : Form
    {
        Configurações.csConfiguracoes cs_conf = new Configurações.csConfiguracoes();
        Alunos.csAlunos cs_alunos = new Alunos.csAlunos();
        Notas.csNotas cs_notas = new Notas.csNotas();
        Notas.csAtendimentos cs_atendimentos = new Notas.csAtendimentos();

        public frSplash()
        {
            InitializeComponent();
        }

        private void frSplash_Load(object sender, EventArgs e)
        {
            
        }

        private void frSplash_Shown(object sender, EventArgs e)
        {
            if (cs_conf.conexao_bd_ok())
            {
                cs_conf.buscar_escola_ativa();

                //cs_atendimentos.rotina_inativar();

                //TODO: ELIMININAR ESTAS FUNÇÕES DA CLASSE CS_NOTAS
                //MessageBox.Show("Rodar o script de conversão Nome>id PROFESSORES. Antes de Continuar");
                //cs_notas.convert_hist_to_medias_tabs(0);
                //cs_notas.convert_hist_to_medias_tabs(1);
                //
                //cs_notas.troca_id_disciplina();

                //string a = Application.ProductVersion;            

                Thread.Sleep(1000);

                this.Hide();

                frPrincipal form = new frPrincipal();

                form.ShowDialog();

                this.Close();
            }
            else
            {
                MessageBox.Show("Não foi possível conectar ao Banco de Dados. Por favor, Verifique a conexão de rede.","E-matrícula");

                Close();
            }
            
        }

       
    }
}


