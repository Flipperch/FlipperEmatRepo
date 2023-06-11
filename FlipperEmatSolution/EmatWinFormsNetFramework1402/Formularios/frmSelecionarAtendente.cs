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
    public partial class frmSelecionarAtendente : Form
    {
        //TODO:: Implementar janela para selecionar usuario do atendimento em casos em que user é generico
        // Criar opção de configuração na classe Settings
        public frmSelecionarAtendente()
        {
            InitializeComponent();
        }

        private void frmSelecionarAtendente_Load(object sender, EventArgs e)
        {
            cmbAtendente.DataSource = DAO.ProfessorDAO.ExibirTodos();
            cmbAtendente.DisplayMember = "Nome";
            cmbAtendente.ValueMember = "Codigo";
        }
    }
}
