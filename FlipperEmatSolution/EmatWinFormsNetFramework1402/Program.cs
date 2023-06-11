using System;
using System.Windows.Forms;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace EmatWinFormsNetFramework1402
{
    static class Program
    {
        //TODO:: LIMPAR - DATASETS FORA DA PASTA RELATORIOS

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Formularios.frmEmatricula());
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.RegistrarErroBanco("Erro no Ematrícula" + ex.StackTrace);
                ErrorLog.ErrorHandleService.ExibirMsgBoxError("Erro no Ematrícula - Mensagem: " + ex.Message);
            }
        }
    }
}
