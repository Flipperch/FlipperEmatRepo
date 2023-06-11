using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Notas
{
    public partial class frEliminacoes : Form
    {
        Alunos.csAlunos cs_alunos = new Alunos.csAlunos();
        Notas.csDisciplinas cs_disciplinas = new Notas.csDisciplinas();
        Notas.csEliminacoes cs_eliminacoes = new Notas.csEliminacoes();
        Notas.csNotas cs_notas = new Notas.csNotas();
        Error_Log.csControle_erros cs_erros = new Error_Log.csControle_erros();

        int id_ensino;
        public DataTable tab_disciplinas = new DataTable();

        public frEliminacoes()
        {
            InitializeComponent();
        }

        private void clbEliminacoes_MouseEnter(object sender, EventArgs e)
        {
            clbEliminacoes.Focus();
        }

        private void clbEliminacoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = clbEliminacoes.SelectedIndex;

            var a = clbEliminacoes.GetItemChecked(i);
            
            if (!a)
            {
                clbEliminacoes.SetItemChecked(i, true);

            }
            else
            {
                clbEliminacoes.SetItemChecked(i, false);
            }
        }

        private void clbEliminacoes_Click(object sender, EventArgs e)
        {
            
        }

        public void fill_form(string n_mat)
        {
            
            lbAluno.Text = "ALUNO: " + cs_alunos.troca_n_mat_nome(n_mat);
            lbEnsino.Text = "ENSINO " + cs_alunos.ensino(n_mat);
            lbDisciplinaAtual.Text = "DISCIPLINA ATUAL: " + cs_disciplinas.troca_disciplina_id_por_nome(cs_alunos.id_disciplina_atual__(n_mat));
        }

        public void fill_clb(int n_mat)
        {
            //List<string> lista = new List<string>();

            //lista = cs_eliminacoes.Eliminacoes(Convert.ToInt32(n_mat)).Split('|').ToList();

            //lista.Remove("");

            //int index = 0;

            //if (lista.Count > 0)
            //{
            //    if (lista[0] != "")
            //    {
            //        for (int i = 0; i < lista.Count; i++)
            //        {
            //            int z = Convert.ToInt32(lista[i]);
            //            string disci = cs_disciplinas.troca_id_disc_nome(z);

            //            foreach (Object item in clbEliminacoes.Items)
            //            {
            //                if(item.ToString() == disci)
            //                {
            //                    index = clbEliminacoes.Items.IndexOf(item);
            //                    clbEliminacoes.SetItemChecked(index, true);
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        for (int i = 1; i < lista.Count; i++)
            //        {
            //            int z = Convert.ToInt32(lista[i]);
            //            string disci = cs_disciplinas.troca_id_disc_nome(z);

            //            foreach (Object item in clbEliminacoes.Items)
            //            {
            //                if (item.ToString() == disci)
            //                {
            //                    index = clbEliminacoes.Items.IndexOf(item);
            //                    clbEliminacoes.SetItemChecked(index, true);
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}
        }

        public void elimina_disciplina_concluida(string n_mat, string ensino)
        {
            //rotina onde verifica materias encerradas e elimina no checkboxlist
            
            List<string> list_con = new List<string>();

            List<string> list_disc = new List<string>();

            if(ensino == "MÉDIO")
            {
                list_disc.Add("PORTUGUÊS");
                list_disc.Add("MATEMÁTICA");
                list_disc.Add("QUÍMICA");
                list_disc.Add("HISTÓRIA");
                list_disc.Add("FÍSICA");
                list_disc.Add("BIOLOGIA");
                list_disc.Add("INGLÊS");
                list_disc.Add("ARTE");
                list_disc.Add("FILOSOFIA");
                list_disc.Add("SOCIOLOGIA");                
                list_disc.Add("GEOGRAFIA");

                for (int i = 0; i < 11; i++)
                {
                    //if (cs_notas.ha_media_final(n_mat, ensino, list_disc[i]))
                    //{
                    //    list_con.Add(list_disc[i]);

                    //    clbEliminacoes.SelectedItem = list_disc[i];
                    //}                   
                }
           }
           else
           {
                list_disc.Add("CIÊNCIAS");
                list_disc.Add("PORTUGUÊS");
                list_disc.Add("MATEMÁTICA");
                list_disc.Add("HISTÓRIA");
                list_disc.Add("INGLÊS");
                list_disc.Add("ARTE");
                list_disc.Add("GEOGRAFIA");
                
                //for (int i = 0; i < 7; i++)
                //{
                //    if (cs_notas.ha_media_final(n_mat, ensino, list_disc[i]))
                //    {
                //        list_con.Add(list_disc[i]);

                        

                //        clbEliminacoes.SelectedItem = list_disc[i];

                //    }
                //}
            }
        }

        private void btPesquisar_Click(object sender, EventArgs e)
        {
            string n_mat = txtNmat.Text;

            if (cs_alunos.existe_nmat(n_mat))
            {
                if (cs_alunos.ensino(n_mat) != "")
                {                
                    fill_form(n_mat);

                    clbEliminacoes.Items.Clear();

                    id_ensino = cs_disciplinas.troca_ensino_nome_por_id(cs_alunos.ensino(n_mat));

                    tab_disciplinas = cs_disciplinas.buscarDisciplinas_ensino(id_ensino);

                    for (int i = 0; i < tab_disciplinas.Rows.Count; i++)
                    {
                        clbEliminacoes.Items.Add(tab_disciplinas.Rows[i][1]);
                    }

                    clbEliminacoes.ItemCheck -= clbEliminacoes_ItemCheck;

                    fill_clbEliminacoes();

                    clbEliminacoes.ItemCheck += clbEliminacoes_ItemCheck;
                }
                else
                {
                    MessageBox.Show(cs_erros.msg_aluno_sem_ensino(), "Ensino não informado");                    
                }                
            }
            else
            {
                MessageBox.Show(cs_erros.msg_n_mat_n_existe(), "Número não existe");
            }
        }

        private void txtN_mat_TextChanged(object sender, EventArgs e)
        {
            if(txtNmat.Text != "")
            {
                btPesquisar.Enabled = true;
            }
            else
            {
                btPesquisar.Enabled = false;
            }
        }

        private void clbEliminacoes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            clbEliminacoes.ItemCheck -= clbEliminacoes_ItemCheck;

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

            if (cs_eliminacoes.ha_Eliminacoes(txtNmat.Text) == true)
            {
                cs_eliminacoes.modificaEliminacoes(txtNmat.Text, eliminacoes, cs_disciplinas.troca_ensino_nome_por_id(Alunos.CSaluno.ensino_aluno));
               
            }
            else
            {
                cs_eliminacoes.adcionaEliminacoes(txtNmat.Text, eliminacoes, cs_disciplinas.troca_ensino_nome_por_id(Alunos.CSaluno.ensino_aluno));
           
            }

            clbEliminacoes.ItemCheck += clbEliminacoes_ItemCheck;

            Cursor.Current = Cursors.Default;
        }

        private void frEliminacoes_Load(object sender, EventArgs e)
        {
        }

        public void fill_clbEliminacoes()
        {
            //Lista de Index 
            List<int> list_index = new List<int>();

            //Lista de Eliminações em ID
            List<string> lista = new List<string>();

            lista = cs_eliminacoes.Eliminacoes(txtNmat.Text, cs_disciplinas.troca_ensino_nome_por_id(Alunos.CSaluno.ensino_aluno)).Split('|').ToList();
            lista.RemoveAt(lista.Count - 1);
            lista.Remove("");


            if (lista.Count > 0)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    string a = cs_disciplinas.troca_disciplina_id_por_nome(Convert.ToInt32(lista[i]));
                    foreach (string item in clbEliminacoes.Items)
                    {
                        if (item == a)
                        {
                            list_index.Add(clbEliminacoes.Items.IndexOf(a));
                            break;
                        }
                    }
                }

                for (int i = 0; i < list_index.Count; i++)
                {
                    clbEliminacoes.SetItemChecked(list_index[i], true);
                }

            }
        }
    }
}
