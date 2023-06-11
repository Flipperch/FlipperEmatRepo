using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Usuarios_Grupos
{
    public partial class frLogin : Form
    {
        //SQL
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();
        //CLASSES
        csUsuarios cs_usuarios = new csUsuarios();
        csGrupos cs_grupos = new csGrupos();
        Notas.csDisciplinas cs_disciplinas = new Notas.csDisciplinas();


        Usuarios_Grupos.CSlogin_off cslogin = new Usuarios_Grupos.CSlogin_off();

        public frLogin()
        {
            InitializeComponent();
        }
        
        private void btLogar_Click(object sender, EventArgs e)
        {
            verificar_campos();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Campos Preenchidos = > Logar
        public void verificar_campos()
        {
            if (txtusuario.Text != "")
            {
                if(txtsenha.Text != "")
                {
                    verificar_user_senha(txtusuario.Text, txtsenha.Text);
                }
                else
                {
                    lblMessage_erro.Text = "Favor informar a senha.";
                }
            }
            else
            {
                lblMessage_erro.Text = "Favor informar o usuário.";
            }
        }

        public void senha_padrao()
        {
            if(txtsenha.Text =="123456")
            {
                MessageBox.Show("Senha padrão ainda em uso. Favor alterar.", "Login");
            }
        }

        public void verificar_user_senha(string usuario, string psw)
        {
            if (cs_usuarios.usuario_existe(usuario))
            {
                if (cs_usuarios.psw_confirma(usuario, psw))
                {
                    logar(txtusuario.Text);
                }
                else
                {
                    lblMessage_erro.Text = "Senha Incorreta.";
                }
            }
            else
            {
                lblMessage_erro.Text = "Usuário não Existe.";
            }
        }

        public void logar(string usuario)
        {
            Usuarios_Grupos.csUsuario_logado.id_usuario_logado = cs_usuarios.troca_usuario_por_id(usuario);
            Usuarios_Grupos.csUsuario_logado.id_grupo_logado = cs_usuarios.id_grupo(Usuarios_Grupos.csUsuario_logado.id_usuario_logado);;            
            Usuarios_Grupos.csUsuario_logado.id_disciplina_logado = cs_usuarios.id_disciplina(Usuarios_Grupos.csUsuario_logado.id_usuario_logado);
            Usuarios_Grupos.csUsuario_logado.id_area_user_logado = cs_usuarios.ids_areas_user(Usuarios_Grupos.csUsuario_logado.id_usuario_logado);
            
            //Habilitar Funções
            Usuarios_Grupos.csUsuario_logado.lista_ids_funcoes_grupo_logado = cs_grupos.lista_grupos(Usuarios_Grupos.csUsuario_logado.id_grupo_logado)[0].permissoes;

            habilitarbotoes(Usuarios_Grupos.csUsuario_logado.lista_ids_funcoes_grupo_logado);
            //Adcionar no tssl as informações de login
            fill_tssl(cs_usuarios.troca_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_usuario_logado),
                      cs_disciplinas.troca_disciplina_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_disciplina_logado),
                      Usuarios_Grupos.csUsuario_logado.id_area_user_logado);

            cslogin.logar();
            senha_padrao();
            get_id_mat();
            //get_mat_prof();            
            

            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "frPrincipal")
                {
                    foreach (MenuStrip menus in form.Controls.OfType<MenuStrip>())
                    {
                        if (menus.Name == "mspPrincipal")
                        {
                            foreach (ToolStripMenuItem menu_item in menus.Items)
                            {
                                if (menu_item.Name == "tsmAlterar_senha")
                                {
                                    menu_item.Enabled = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            this.Close();
        }

        //LIMPAR lblMessage_erro
        private void txtusuario_Enter(object sender, EventArgs e)
        {
            lblMessage_erro.Text = "";
        }

        public void habilitarbotoes(List<int> ids_permissoes_para_liberar)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "frPrincipal")
                {
                    foreach (MenuStrip menus in form.Controls.OfType<MenuStrip>())
                    {
                        if (menus.Name == "mspPrincipal")
                        {
                            foreach (ToolStripMenuItem menu_item in menus.Items)
                            {
                                if(ids_permissoes_para_liberar.Contains(cs_grupos.troca_permissao_sistema_nome_por_id(menu_item.Name)))
                                {
                                    menu_item.Enabled = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void fill_tssl(string nome_usuario_logado, string nome_disc_logado, List<int> id_areas_user_logado)
        {
            //add parametro , List<string> area_logado 

            //cslogin.get_n_grupo();
            //cslogin.get_n_user();

            string user_atual = "Usuário Atual: " + nome_usuario_logado;
            user_atual += " - ";
            user_atual += nome_disc_logado;

            
            
            //Adcionar área_loagdo
            
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "frPrincipal")
                {
                    foreach (MenuStrip menus in form.Controls.OfType<MenuStrip>())
                    {
                        if (menus.Name == "mspPrincipal")
                        {
                            foreach (ToolStripMenuItem menu_item in menus.Items)
                            {
                                //TODO:: ARRUMAR FUNÇÕES DE GRUPOS - COMPLETAMENTE

                                if (menu_item.Name == "tsmRelatorios")
                                {
                                    
                                        menu_item.Enabled = true;
                                        break;
                                    
                                }
                            }
                        }
                    }
                }
            }

            foreach (Form forms in Application.OpenForms)
            {
                if (forms.Name == "frPrincipal")
                {
                    foreach (StatusStrip statusstrip in forms.Controls.OfType<StatusStrip>())
                    {
                        foreach (ToolStripStatusLabel tssl in statusstrip.Items.OfType<ToolStripStatusLabel>())
                        {
                            if (tssl.Name == "tsslUsuario_Grupo")
                            {
                                tssl.Text = user_atual;
                            }

                            if (cs_grupos.troca_grupo_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_grupo_logado) == "ADMINISTRADOR")

                            if (tssl.Name == "tssErrors")
                            {
                                tssl.Text = "errors_admin_log";
                            }

                        }

                        if (id_areas_user_logado.Count > 0)
                        {
                            foreach (ToolStripDropDownButton tsddb in statusstrip.Items.OfType<ToolStripDropDownButton>())
                            {
                                if (tsddb.Name == "tsddbAreas")
                                {
                                    for (int i = 0; i < id_areas_user_logado.Count; i++ )
                                    {
                                        tsddb.DropDownItems.Add(cs_disciplinas.troca_area_id_por_nome(id_areas_user_logado[i]));
                                    }
                                        
                                }
                            }
                        }                       
                    }
                }


            }
        }

        private void FRlogin_Shown(object sender, EventArgs e)
        {
            txtusuario.Focus();
        }

        private void FRlogin_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            //Verifica se esta configurado com servidor,
            //caso afirmativo, inicia rotina automatica para ligar catraca.
            Configurações.CSconnexoes_txt conf = new Configurações.CSconnexoes_txt();
            conf.get_configuracoes();

            if(conf.modo == "servidor")
            {
                btCancelar.PerformClick();
            }

            lblMessage_erro.Text = "";

            Cursor.Current = Cursors.Default;
        }

        public void get_id_mat()
        {
            if (cs_grupos.troca_grupo_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_grupo_logado) == "PROFESSORES")
            {
                sql_comm.Connection = sql_conn;
                sql_comm.CommandText =
                @"SELECT * FROM PROFESSORES WHERE ID_USUARIO=@idusuarios_atual";

                sql_comm.Parameters.AddWithValue("@idusuarios_atual", csUsuario_logado.id_usuario_logado);

                try
                {
                    sql_conn.Open();
                    SqlDataReader reader = sql_comm.ExecuteReader();

                    Usuarios_Grupos.csUsuario_logado.lista_ids_funcoes_grupo_logado.Clear();

                    while (reader.Read())
                    {
                        csUsuario_logado.id_disciplina_logado = (int)reader["ID_DISCIPLINA"];
                    }
                    reader.Close();                    
                }
                catch
                {

                }
                finally
                {
                    sql_conn.Close();
                }
            }
            
                


        }

        //public void get_mat_prof()
        //{
        //    if (Usuarios_Grupos.csUsuario_logado.nome_grupo_logado == "PROFESSORES")
        //    {
        //        sql_comm.Connection = sql_conn;
        //        sql_comm.CommandText =
        //        @"SELECT * FROM DISCIPLINAS WHERE ID_DISCIPLINA=@id_disciplina_atual";

        //        sql_comm.Parameters.AddWithValue("@id_disciplina_atual", csUsuario_logado.id_disciplina_logado);

        //        try
        //        {
        //            sql_conn.Open();
        //            SqlDataReader reader = sql_comm.ExecuteReader();

        //            Usuarios_Grupos.csUsuario_logado.lista_ids_funcoes_grupo_logado.Clear();

        //            while (reader.Read())
        //            {
        //                csUsuario_logado.nome_disc_logado = reader["N_DISCIPLINA"].ToString();
        //            }
        //            reader.Close();
        //        }
        //        catch
        //        {

        //        }
        //        finally
        //        {
        //            sql_conn.Close();
        //        }
        //    }
        //}
    }
}
