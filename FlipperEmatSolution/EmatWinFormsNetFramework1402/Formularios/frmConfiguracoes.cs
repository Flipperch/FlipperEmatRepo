using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using EmatWinFormsNetFramework1402.Utils;
/// <summary>
/// Tela de Configurações do Sistema.
/// Criado por Felipe Alencar Chagas
/// </summary>
namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frmConfiguracoes : Form
    {
        public frmConfiguracoes()
        {
            InitializeComponent();
        }

        private void frmConfiguracoes_Load(object sender, EventArgs e)
        {
            try
            {
                cmbServer.Items.Add(".");
                cmbServer.Items.Add("(local)");
                cmbServer.Items.Add(@".\SQLEXPRESS");
                cmbServer.Items.Add(string.Format(@"{0}\SQLEXPRESS", Environment.MachineName));
                cmbServer.SelectedIndex = 3;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            string connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", cmbServer.Text, txtDatabase.Text, txtUsername.Text, txtPassword.Text);
            try
            {
                SqlHelper helper = new SqlHelper(connectionString);
                if (helper.IsConnection)
                    MessageBox.Show("Teste de conexão efetuada com sucesso.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", cmbServer.Text, txtDatabase.Text, txtUsername.Text, txtPassword.Text);
            try
            {
                SqlHelper helper = new SqlHelper(connectionString);
                if (helper.IsConnection)
                {
                    //TODO => Concluir UI p/ configurar settings
                    //EmatriculaConfigurationSettings setting = new EmatriculaConfigurationSettings();
                    //setting.SaveEmatriculaSettingBase("connectionStringADO", connectionString);
                    //MessageBox.Show("A configuração do banco de dados foi salva com sucesso.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }
    }
}
