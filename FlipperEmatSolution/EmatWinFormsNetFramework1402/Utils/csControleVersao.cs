using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.Utils
{
    public class csControleVersao
    {
        //Version versao { get; set; }
        //Código abaixo retornando apenas "1.0.0.0"
        // System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

        public static string versao_sistema()
        {

            string retorno = "";

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version vs = ApplicationDeployment.CurrentDeployment.CurrentVersion;

                retorno = vs.Major.ToString() + "."
                + vs.Minor.ToString() + "."
                + vs.Build.ToString() + "."
                + vs.Revision.ToString();
            }
            else
            {
                retorno = "Não disponível.";
            }

            return retorno;
        }

        public static void verificar_atualizacao()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();
                    
                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("A nova versão da aplicação não pode ser baixada neste momento. \n\nFavor verificar sua conexão de rede, ou tente mais tarde. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Não é possível verificar por uma nova versão da aplicação. O ClickOnce está corrompido. Favor reimplantar a aplicação e tente novamente. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("Esta aplicação no pode ser atualizada. Esta não é uma aplicação ClickOnce. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        DialogResult dr = MessageBox.Show("Uma atualização está disponível. Gostaria de atualizar agora ? Caso sim, a aplicação será reiniciada!", "Atualização Disponível", MessageBoxButtons.OKCancel);
                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        MessageBox.Show("This application has detected a mandatory update from your current " +
                            "version to version " + info.MinimumRequiredVersion.ToString() +
                            ". The application will now install the update and restart.",
                            "Atualização Disponível", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            ad.Update();
                            MessageBox.Show("A aplicação foi atualizada. Reiniciando....");
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show("Não é possível instalar a última versão da aplicação. \n\nFavor verificar sua conexão de rede, ou tente mais tarde.. Error: " + dde);
                            return;
                        }
                    }
                }
            }
        }

        bool atualizado()
        {
            bool retorno = false;

            //if()

            return retorno;
        }

        private static Version sel_ultima_versao()
        {
            //Chamar SQL
            SqlHelper sqlHelper = new SqlHelper();
            SqlCommand sql_comm = new SqlCommand();

            sql_comm.CommandText = "SELECT * FROM VERSOES";

            Version retorno = Version.Parse("");



            return retorno;
        }
    }
}
