using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Principal.FRprincipal());

            //no splashscreen, verifica-se alunos com mais de 30 dias !!
            Application.Run(new Principal.frSplash());

        }
    }
}
