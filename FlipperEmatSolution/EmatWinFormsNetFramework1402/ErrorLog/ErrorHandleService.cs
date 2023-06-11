using log4net;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework1402.ErrorLog
{
    class ErrorHandleService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// Método que exibe erro no rodapé caso o Administrador esteja logado
        /// </summary>
        /// <param name="erro"></param>
        public static void ExibirErroToolStripMenu(string erro)
        {
            foreach (Form forms in Application.OpenForms)
            {
                if (forms.Name == "frmEmatricula")
                {
                    foreach (StatusStrip statusstrip in forms.Controls.OfType<StatusStrip>())
                    {
                        foreach (ToolStripStatusLabel tssl in statusstrip.Items.OfType<ToolStripStatusLabel>())
                        {
                            // 1 - Admin
                            if(Utils.csUsuarioLogado.usuario != null)
                                if (Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.ADMINISTRADOR)
                                    if (tssl.Name == "tssErrors")
                                    {
                                        tssl.Text = erro;
                                    }
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Método que exibe um messagebox na tela e registra na tabela ERROR_LOG
        /// </summary>
        /// <param name="erro"></param>
        public static void ExibirMsgBoxError(string erro)
        {
            try
            {
                log.Error(erro);
                MessageBox.Show(erro, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// Método que exibe um messagebox na tela e registra na tabela ERROR_LOG
        /// </summary>
        /// <param name="erro"></param>
        public static void RegistrarErroBanco(string erro)
        {
            try
            {
                log.Error(erro);
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// Método para limpar erro exibido no rodapé caso o Administrador esteja logado
        /// </summary>
        public static void LimparErro()
        {
            foreach (Form forms in Application.OpenForms)
            {
                if (forms.Name == "frmEmatricula")
                {
                    foreach (StatusStrip statusstrip in forms.Controls.OfType<StatusStrip>())
                    {
                        foreach (ToolStripStatusLabel tssl in statusstrip.Items.OfType<ToolStripStatusLabel>())
                        {
                            // 1 - Admin
                            if (Utils.csUsuarioLogado.usuario != null)
                                if (Utils.csUsuarioLogado.usuario.NivelAcesso == Enumeradores.NivelAcesso.ADMINISTRADOR)
                                    if (tssl.Name == "tssErrors")
                                    {
                                        tssl.Text = string.Empty;
                                    }
                        }
                    }
                }
            }
        }
    }
}