using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Principal
{
    public partial class frAtalhos : Form
    {
        Usuarios_Grupos.csUsuarios cs_usuarios = new Usuarios_Grupos.csUsuarios();
        Usuarios_Grupos.csGrupos cs_grupos = new Usuarios_Grupos.csGrupos();

        public frAtalhos()
        {
            InitializeComponent();
        }

        private void frAtalhos_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = "TAREFAS - " + cs_grupos.troca_grupo_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_grupo_logado);
        }

        //Definir Atalho por grupos

        


    }
}
