using EmatWinFormsNetFramework13032.Usuarios_Grupos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Error_Log
{
    class csControle_erros
    {
        csGrupos cs_grupos = new csGrupos();

        List<string> list_ = new List<string>();

        public static void exibir_erro(string erro)
        {
            foreach (Form forms in Application.OpenForms)
            {
                if (forms.Name == "frPrincipal")
                {
                    foreach (StatusStrip statusstrip in forms.Controls.OfType<StatusStrip>())
                    {
                        foreach (ToolStripStatusLabel tssl in statusstrip.Items.OfType<ToolStripStatusLabel>())
                        {
                            // 1 - Admin
                            if (Usuarios_Grupos.csUsuario_logado.id_grupo_logado == 1)

                                if (tssl.Name == "tssErrors")
                                {
                                    tssl.Text = erro;
                                }
                        }
                    }
                }
            }
        }

        public void registra_erro(string erro, string nome_arquivo)
        {
            string local_error_log = @"C:\E-mat\";
            local_error_log += nome_arquivo + ".txt";

            list_.Clear();

            if(File.Exists(local_error_log))
            {                
                string line;

                StreamReader read = new StreamReader(local_error_log);
                while((line = read.ReadLine()) != null)
                {
                    list_.Add(line);
                }

                read.Close();
            }

            if (File.Exists(local_error_log))
            {               
                list_.Add(erro);

                File.WriteAllLines(local_error_log, list_);
            }
            else
            {
                Directory.CreateDirectory(Path.GetDirectoryName(local_error_log));

                using (FileStream fs = new FileStream(local_error_log, FileMode.Create))
                {
                    fs.Close();
                                        
                    list_.Add(erro);

                    File.WriteAllLines(local_error_log, list_);
                }
            }
        }

        //Menssagens de erro 
        public string msg_aluno_sem_ensino()
        {
            string a = "Nível atual do aluno, não Informado. Favor Comunicar a Secretaria.";

            return a;
        }

        public string msg_n_mat_n_existe()
        {
            string a = "Número de Matrícula não existe. Favor Comunicar a Secretaria.";

            return a;
        }

        //messagebox
        public void mostra_msgbox_erro(string erro)
        {
            MessageBox.Show(erro);
        }
    }
}
