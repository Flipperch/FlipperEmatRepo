using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace EmatWinFormsNetFramework13032.Usuarios_Grupos
{
    public static class csUsuario_logado
    {
        public static int id_usuario_logado { get; set; }
        //public static string nome_usuario_logado { get; set; }

        public static int id_disciplina_logado { get; set; }
        //public static string nome_disc_logado { get; set; }

        public static int id_grupo_logado { get; set; }
        //public static string nome_grupo_logado { get; set; }

        public static List<int> id_area_user_logado { get; set; }

        public static List<int> lista_ids_funcoes_grupo_logado { get; set; }
    }

       
}
