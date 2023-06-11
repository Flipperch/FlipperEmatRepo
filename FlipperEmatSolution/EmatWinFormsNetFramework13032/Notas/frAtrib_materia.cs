using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Notas
{
    public partial class frAtrib_materia : Form
    {
        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();
        csEliminacoes cs_eliminacoes = new csEliminacoes();
        csDisciplinas cs_disciplinas = new csDisciplinas();
        csNotas cs_notas = new csNotas();

        bool alterado = false;

        int id_ensino;

        Alunos.csAlunos cs_alunos = new Alunos.csAlunos();

        public DataTable tab_disciplinas = new DataTable();

        public List<string> lista_notas = new List<string>();

        public List<int> lista_ids_disc_concluidas = new List<int>();

        public List<string> lista_ids_mats = new List<string>();

        List<string> materia_a_fazer = new List<string>();
        List<string> materia_a_fazer_nome = new List<string>();
        List<string> materia_a_fazer_nome_horario = new List<string>();
        List<string> materia_a_fazer_nome_distribuicao = new List<string>();

        public string nova_materia;

        public frAtrib_materia()
        {
            InitializeComponent();
        }

        private void frAtribuicao_materia_Load(object sender, EventArgs e)
        {
            //verificar aluno Inativo
   
            lblNmat.Text = "Nº de Matrícula: " + Alunos.CSaluno.numat;
            lblNome.Text = "Nome: " + Alunos.CSaluno.nome_aluno;
            lblRg.Text = "RG: " + Alunos.CSaluno.rg_aluno;
            lblEnsino.Text = "Ensino Atual: " + Alunos.CSaluno.ensino_aluno;

            //FOTO
            ptbFotoAluno.Image = cs_alunos.foto_aluno(Alunos.CSaluno.numat);

            //GRB nome
            string a = Alunos.CSaluno.ensino_aluno[0].ToString().ToUpper();
            grbEliminacoes.Text = "Eliminações " + a + Alunos.CSaluno.ensino_aluno.ToLower().Remove(0, 1);

            clbEliminacoes.Items.Clear();

            id_ensino = cs_disciplinas.troca_ensino_nome_por_id(cs_alunos.ensino(Alunos.CSaluno.numat));

            tab_disciplinas = cs_disciplinas.buscarDisciplinas_ensino(id_ensino);            

            for (int i = 0; i < tab_disciplinas.Rows.Count; i++)
            {
                clbEliminacoes.Items.Add(tab_disciplinas.Rows[i][1]);
            }

            clbEliminacoes.ItemCheck -= clbEliminacoes_ItemCheck;

            fill_clbEliminacoes();

            clbEliminacoes.ItemCheck += clbEliminacoes_ItemCheck;

            //tab_materias.Columns[6].DataType = Type.GetType("System.Int32");
       
            escolher_materia();

            if (cs_alunos.esta_ativo(Alunos.CSaluno.numat) == false)
            {
                MessageBox.Show("Aluno Inativo. Comparecer à Secretaria.");
                clbEliminacoes.Enabled = false;
                btnSel_mat_1.Enabled = false;
                btnSel_mat_2.Enabled = false;
                btnSel_mat_3.Enabled = false;
            }
        }

        public void escolher_materia()
        {
            lista_ids_disc_concluidas = cs_disciplinas.disciplinas_conluidas(Convert.ToInt32(Alunos.CSaluno.numat), id_ensino);

            materia_a_fazer_nome.Clear();
            materia_a_fazer_nome_horario.Clear();
            materia_a_fazer_nome_distribuicao.Clear();

            for (int i = 0; i < (tab_disciplinas.Rows.Count); i++)
            {
                if (!lista_ids_disc_concluidas.Contains(Convert.ToInt32(tab_disciplinas.Rows[i][0])))
                {
                    //fazer
                    materia_a_fazer_nome.Add(tab_disciplinas.Rows[i][1].ToString());
                }
            }

            buscar_qtd_alunos(tab_disciplinas);
            busca_nome_mat(tab_disciplinas);

            //Classificar por quantidade de alunos NA MATÉRIA. - INATIVO

            #region Selecionar por Quandtidade            

            //for (int i = 0; i < tab_materias.Rows.Count; i++)
            //{
            //    for (int z = 0; z < materia_a_fazer_nome.Count; z++)
            //    {
            //        if (materia_a_fazer_nome[z] == getsortedtable(tab_materias).Rows[i][1].ToString())
            //        {
            //            materia_a_fazer_nome_1.Add(getsortedtable(tab_materias).Rows[i][1].ToString());
            //            break;
            //        }                   
            //    }

            //    if(materia_a_fazer_nome_1.Count == 3)
            //    {
            //        break;
            //    }
            //}

            #endregion

            // Excluir materia Eliminações - ATIVO

            #region Excluir Matéria Eliminações

            if(clbEliminacoes.CheckedItems.Count>0)
            {
                List<string> list_remov = new List<string>();

                List<string> checkedItems = new List<string>();

                foreach (var item in clbEliminacoes.CheckedItems)
                    checkedItems.Add(item.ToString());

                foreach (string item in checkedItems)
                {
                    for (int z = 0; z < materia_a_fazer_nome.Count; z++)
                    {
                        if (item == materia_a_fazer_nome[z])
                        {
                            list_remov.Add(item);
                        }
                    }
                    
                }

                for (int i = 0; i < list_remov.Count; i++)
                {
                    materia_a_fazer_nome.Remove(list_remov[i]);
                    
                }
            }

            #endregion

            // Excluir materia de horário indisponivel. - ATIVO

            #region Excluir Matéria pelo Horário

            DateTime hoje = new DateTime();
            hoje = DateTime.Now;

            string hor_aula_m = "m";
            string hor_aula_t = "t";
            string hor_aula_n = "n";

            for (int z = 0; z < materia_a_fazer_nome.Count; z++)
            {
                for (int i = 0; i < tab_disciplinas.Rows.Count; i++)
                {
                    if (tab_disciplinas.Rows[i][1].ToString() == materia_a_fazer_nome[z])
                    {
                        if (tab_disciplinas.Rows[i][3].ToString() != "")
                        {

                            string[] todos_horarios = tab_disciplinas.Rows[i][3].ToString().Split('|');
                            if (todos_horarios.Length > 1)
                            {
                                hor_aula_m = todos_horarios[0];
                                hor_aula_t = todos_horarios[1];
                                hor_aula_n = todos_horarios[2];

                                string b;

                                if (DateTime.Parse(hoje.ToString("HH:mm")) < DateTime.Parse("12:00"))
                                {
                                    b = hor_aula_m.Replace("|", string.Empty);
                                }
                                else if (DateTime.Parse(hoje.ToString("HH:mm")) < DateTime.Parse("18:00") && DateTime.Parse(hoje.ToString("HH:mm")) > DateTime.Parse("14:00"))
                                {
                                    b = hor_aula_t.Replace("|", string.Empty);
                                }
                                else
                                {
                                    b = hor_aula_n.Replace("|", string.Empty);
                                }

                                DateTime horario_ini;
                                DateTime horario_fin;

                                if (b.Length > 1)
                                {
                                    horario_ini = DateTime.Parse(b.Substring(b.IndexOf(hoje.ToString("ddd")) + 3, 5));
                                    horario_fin = DateTime.Parse(b.Substring(b.IndexOf(hoje.ToString("ddd")) + 11, 5));
                                }
                                else
                                {
                                    horario_ini = DateTime.Parse("00:00");
                                    horario_fin = DateTime.Parse("00:00");
                                }

                                if (DateTime.Parse(hoje.ToString("HH:mm")) > DateTime.Parse(horario_ini.ToString("HH:mm")) &&
                                    DateTime.Parse(hoje.ToString("HH:mm")) < DateTime.Parse(horario_fin.ToString("HH:mm")))
                                {
                                    if (materia_a_fazer_nome[z] != Alunos.CSaluno.numat)
                                    {
                                        materia_a_fazer_nome_horario.Add(materia_a_fazer_nome[z]);
                                        break;
                                    }
                                }
                            }
                        }
                    }                   
                }
            }        

            #endregion

            //Classificar por quantidade de alunos ATRIBUIDOS NO DIA. - ATIVO

            #region Selecionar por Distribuição

            if(ckbCons_horarios.Checked)
            {
                string a = "";
                for (int i = 0; i < tab_disciplinas.Rows.Count; i++)
                {
                    for (int z = 0; z < materia_a_fazer_nome_horario.Count; z++)
                    {
                        a = getsortedtable(tab_disciplinas).Rows[i][1].ToString();
                        if (materia_a_fazer_nome_horario[z] == a)
                        {
                            materia_a_fazer_nome_distribuicao.Add(a);
                            break;
                        }
                    }

                    if (materia_a_fazer_nome_distribuicao.Count == 3)
                    {
                        break;
                    }
                }
            }
            else
            {
                string a = "";
                for (int i = 0; i < tab_disciplinas.Rows.Count; i++)
                {
                    for (int z = 0; z < materia_a_fazer_nome.Count; z++)
                    {
                        a = getsortedtable(tab_disciplinas).Rows[i][1].ToString();
                        if (materia_a_fazer_nome[z] == a)
                        {
                            materia_a_fazer_nome_distribuicao.Add(a);
                            break;
                        }
                    }

                    if (materia_a_fazer_nome_distribuicao.Count == 3)
                    {
                        break;
                    }
                }
            }

            

            #endregion

            //Escolher três matérias Aleatórias. - ATIVO

            #region Selecionar Aleatório

            Random rnd = new Random();

            if (materia_a_fazer_nome_distribuicao.Count > 0)
            {
                //btnSel_mat_1.Text = materia_a_fazer_nome_distribuicao[rnd.Next(0, materia_a_fazer_nome_distribuicao.Count)];
                btnSel_mat_1.Text = materia_a_fazer_nome_distribuicao[0];
                btnSel_mat_1.Enabled = true;
            }
            else
            {
                btnSel_mat_1.Text = "-";
                btnSel_mat_1.Enabled = false;
            }

            if (materia_a_fazer_nome_distribuicao.Count > 1)
            {    
                do
                {
                    //btnSel_mat_2.Text = materia_a_fazer_nome_distribuicao[rnd.Next(0, materia_a_fazer_nome_distribuicao.Count)];
                    btnSel_mat_2.Text = materia_a_fazer_nome_distribuicao[1];
                }
                while(btnSel_mat_1.Text == btnSel_mat_2.Text);
                btnSel_mat_2.Enabled = true;
            }
            else
            {
                btnSel_mat_2.Text = "-";
                btnSel_mat_2.Enabled = false;
            }

            if (materia_a_fazer_nome_distribuicao.Count > 2)
            {
                do
                {
                    //btnSel_mat_3.Text = materia_a_fazer_nome_distribuicao[rnd.Next(0, materia_a_fazer_nome_distribuicao.Count)];
                    btnSel_mat_3.Text = materia_a_fazer_nome_distribuicao[2];
                }
                while (btnSel_mat_2.Text == btnSel_mat_3.Text || btnSel_mat_1.Text == btnSel_mat_3.Text);
                btnSel_mat_3.Enabled = true;
            }
            else
            {
                btnSel_mat_3.Text = "-";
                btnSel_mat_3.Enabled = false;
            }

            #endregion
        }

        public void buscar_qtd_alunos(DataTable tab_materias)
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"SELECT ID_DISCIPLINA_ATUAL, DT_ENT_DISCIPLINA FROM ALUNOS WHERE ATIVO = 1";

            try
            {
                sql_conn.Open();
                SqlDataReader reader = sql_comm.ExecuteReader();
                int registro = 0;
                while (reader.Read())
                {
                    for (int i = 0; i < tab_materias.Rows.Count; i++)
                    {
                        if (tab_materias.Rows[i][0].ToString() == reader["ID_DISCIPLINA_ATUAL"].ToString())
                        {
                            tab_materias.Rows[i][5] = (Convert.ToInt32(tab_materias.Rows[i][5]) + 1);

                            if (reader["DT_ENT_DISCIPLINA"] is DBNull)
                            {
                                break;
                            }
                            else
                            {
                                if (DateTime.Now.ToString("dd/MM/yyyy") == DateTime.Parse(reader["DT_ENT_DISCIPLINA"].ToString()).ToString("dd/MM/yyyy"))
                                {
                                    tab_materias.Rows[i][6] = (Convert.ToInt32(tab_materias.Rows[i][6]) + 1);
                                    break;
                                }
                            }
                        }
                    }
                    registro++;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
        }

        public void busca_nome_mat(DataTable tab_materias)
        {
            if (Alunos.CSaluno.id_disciplina_atual > 0)
            {
                for (int i = 0; i < tab_materias.Rows.Count; i++)
                {
                    if (Alunos.CSaluno.id_disciplina_atual == Convert.ToInt32(tab_materias.Rows[i][0]))
                    {
                        lblDisciplinaAtual.Text = "Disciplina Atual: " + tab_materias.Rows[i][1].ToString();
                        break;
                    }
                }
            }
            else
            {
                lblDisciplinaAtual.Text = "Disciplina Atual: Nenhuma";
            }
        }

        private DataTable getsortedtable(DataTable tab_materias)
        {            

            tab_materias.DefaultView.Sort = "entradas_hoje ASC";
            return tab_materias.DefaultView.ToTable();
        }

        //Atribução

        public void atrib_nova_mat()
        {
            sql_comm.Connection = sql_conn;
            sql_comm.CommandText = @"UPDATE ALUNOS SET ID_DISCIPLINA_ATUAL=@id_disciplina_atual, DT_ENT_DISCIPLINA=@dt_ent_disciplina WHERE N_MAT=@n_mat";

            sql_comm.Parameters.AddWithValue("@n_mat", Alunos.CSaluno.numat);

            for (int i = 0; i < tab_disciplinas.Rows.Count; i++)
            {
                if (tab_disciplinas.Rows[i][1].ToString() == nova_materia)
                {
                    sql_comm.Parameters.AddWithValue("@id_disciplina_atual", tab_disciplinas.Rows[i][0].ToString());
                    sql_comm.Parameters.AddWithValue("@dt_ent_disciplina", DateTime.Now);
                    break;
                }
            }

            try
            {
                sql_conn.Open();
                sql_comm.ExecuteNonQuery();

                MessageBox.Show("Matéria Atribuída", "Atribuição");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {                
                sql_comm.Parameters.Clear();
                sql_conn.Close();
            }
        }

        private void btnSel_mat_1_Click(object sender, EventArgs e)
        {
            nova_materia =btnSel_mat_1.Text;

            atrib_nova_mat();

            lblDisciplinaAtual.Text = "Disciplina Atual: " + cs_disciplinas.troca_disciplina_id_por_nome(cs_alunos.id_disciplina_atual__(Alunos.CSaluno.numat));
        }

        private void btnSel_mat_2_Click(object sender, EventArgs e)
        {
            nova_materia = btnSel_mat_2.Text;

            atrib_nova_mat();

            lblDisciplinaAtual.Text = "Disciplina Atual: " + cs_alunos.id_disciplina_atual__(Alunos.CSaluno.numat).ToString();
        }

        private void btnSel_mat_3_Click(object sender, EventArgs e)
        {
            nova_materia = btnSel_mat_3.Text;

            atrib_nova_mat();

            lblDisciplinaAtual.Text = "Disciplina Atual: " + cs_alunos.id_disciplina_atual__(Alunos.CSaluno.numat).ToString();
        }

        private void clbEliminacoes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            List<string> checkedItems = new List<string>();

            string eliminacoes = "";

            foreach (var item in clbEliminacoes.CheckedItems)
                checkedItems.Add(item.ToString());
            
            if (e.NewValue == CheckState.Checked)
                checkedItems.Add(clbEliminacoes.Items[e.Index].ToString());
            else
                checkedItems.Remove(clbEliminacoes.Items[e.Index].ToString());
            
            foreach (string item in checkedItems)
            {
                eliminacoes += cs_disciplinas.troca_disciplina_nome_por_id(item) + "|";
            }

            if (cs_eliminacoes.ha_Eliminacoes(Alunos.CSaluno.numat) == true)
            {
                cs_eliminacoes.modificaEliminacoes(Alunos.CSaluno.numat, eliminacoes, cs_disciplinas.troca_ensino_nome_por_id(Alunos.CSaluno.ensino_aluno));
                alterado = true;
            }
            else
            {
                cs_eliminacoes.adcionaEliminacoes(Alunos.CSaluno.numat, eliminacoes, cs_disciplinas.troca_ensino_nome_por_id(Alunos.CSaluno.ensino_aluno));
                alterado = true;
            }
            
            Cursor.Current = Cursors.Default;
        }

        public void fill_clbEliminacoes()
        {
            //Lista de Index 
            List<int> list_index = new List<int>();

            //Lista de Eliminações em ID
            List<string> lista = new List<string>();

            lista = cs_eliminacoes.Eliminacoes(Alunos.CSaluno.numat, cs_disciplinas.troca_ensino_nome_por_id(Alunos.CSaluno.ensino_aluno)).Split('|').ToList();
            lista.RemoveAt(lista.Count - 1);
            lista.Remove("");

            if(lista.Count > 0)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    string a = cs_disciplinas.troca_disciplina_id_por_nome(Convert.ToInt32(lista[i]));
                    foreach(string item in clbEliminacoes.Items)
                    {
                        if(item == a)
                        {
                            list_index.Add(clbEliminacoes.Items.IndexOf(a));
                            break;
                        }
                    }
                }

                for(int i = 0; i < list_index.Count; i++)
                {
                    clbEliminacoes.SetItemChecked(list_index[i], true);
                }                
            }

            preencher_ckbs_por_medias();
        }

        public void preencher_ckbs_por_medias()
        {
            List<int> list_i = new List<int>();


            foreach(string ckb in clbEliminacoes.Items)
            {                
                if (cs_notas.ha_media_final_nmat(Alunos.CSaluno.numat, cs_disciplinas.troca_disciplina_nome_por_id(ckb), cs_disciplinas.troca_ensino_nome_por_id(Alunos.CSaluno.ensino_aluno)))
                {
                    list_i.Add(clbEliminacoes.Items.IndexOf(ckb));
                }
            }

            for (int i = 0; i < list_i.Count; i++)
            {
                clbEliminacoes.SetItemChecked(list_i[i], true);
            }
        }

        private void clbEliminacoes_MouseUp(object sender, MouseEventArgs e)
        {
            if(alterado == true)
            {
                escolher_materia();
            }
        }

        private void ckbCons_horarios_CheckedChanged(object sender, EventArgs e)
        {
            escolher_materia();
        }

        private void clbEliminacoes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //---------
    }
}
