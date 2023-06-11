using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmatWinFormsNetFramework13032.Relatorios
{
    public partial class frRelatoriosAtendimentos : Form
    {
        Alunos.csAlunos cs_alunos = new Alunos.csAlunos();
        Notas.csDisciplinas cs_disciplinas = new Notas.csDisciplinas();
        Notas.csAtendimentos cs_atendimentos = new Notas.csAtendimentos();
        Usuarios_Grupos.csUsuarios cs_usuarios = new Usuarios_Grupos.csUsuarios();

        public frRelatoriosAtendimentos()
        {
            InitializeComponent();
        }

        private void frRelatoriosAtendimentos_Load(object sender, EventArgs e)
        {
            #region Atendimentos / Disciplinas

            //Fill comboBox
            cs_disciplinas.buscarDisciplinas();
            cmbSel_disciplina.Items.Add("");
            for (int i = 0; i < cs_disciplinas.tab_disciplinas.Rows.Count; i++)
            {
                cmbSel_disciplina.Items.Add(cs_disciplinas.tab_disciplinas.Rows[i][1].ToString());
            }

            filtro_dtp_atend();

            lblQtd_atendimentos.Text = "Atendimentos: " + dgvAlunos_disciplina.Rows.Count;

            if (cs_usuarios.troca_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_usuario_logado) != "admin")
            {
                txtInstrucao.Visible = false;
                txtInstrucao.Enabled = false;
            }

            #endregion
        }

        #region Atendimentos / Disciplinas

        private void cmbSel_disciplina_SelectedIndexChanged(object sender, EventArgs e)
        {

            filtro_dtp_atend();

        }

        private void ckbInativos_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbInativos_disciplina.Checked)
            {
                filtra_Alunos_disciplinas(true);
            }
            else
            {
                filtra_Alunos_disciplinas(false);
            }

        }

        public void filtra_Alunos_disciplinas(bool inativo)
        {
            string id_disc = cs_disciplinas.troca_disciplina_nome_por_id(cmbSel_disciplina.Text).ToString();

            if (inativo)
            {
                dgvAlunos_disciplina.DataSource = cs_alunos.tab_alunos(@"SELECT N_MAT, ALUNO FROM ALUNOS WHERE ID_DISCIPLINA_ATUAL="
                    + id_disc + " ORDER BY DAT_MAT DESC");
            }
            else
            {
                dgvAlunos_disciplina.DataSource = cs_alunos.tab_alunos(@"SELECT N_MAT, ALUNO FROM ALUNOS WHERE ID_DISCIPLINA_ATUAL="
                    + id_disc + " AND ATIVO = 1 ORDER BY DAT_MAT DESC");
            }

            lblQtd_atendimentos.Text = "Atendimentos: " + dgvAlunos_disciplina.Rows.Count;
        }

        public List<string> definir_data(bool lista)
        {
            //bool
            //lista - true 

            List<string> data_where = new List<string>();

            //Periodo
            string data_filtro_ini = "";
            string data_filtro_fin = "";

            data_filtro_ini = dtpAtend_ini.Value.ToString("dd/MM/yyyy");
            data_filtro_fin = dtpAtend_fin.Value.ToString("dd/MM/yyyy");

            if (!lista)
            {
                if (!dtpAtend_fin.Checked)
                {
                    data_where.Add(data_filtro_ini + "' AND '" + data_filtro_ini);
                }
                else
                {
                    data_where.Add(data_filtro_ini + "' AND '" + data_filtro_fin);
                }
            }
            else
            {
                int data_count = (DateTime.Parse(data_filtro_fin).Day - DateTime.Parse(data_filtro_ini).Day) + 1;

                DateTime data = DateTime.Parse(data_filtro_ini);


                for (int i = 0; i < data_count; i++)
                {
                    data_where.Add(data.ToString("dd/MM/yyyy") + "' AND '" + data.ToString("dd/MM/yyyy"));
                    data = data.AddDays(1);
                }
            }
            return data_where;
        }

        public List<string> definir_periodo(bool lista, List<string> data)
        {
            string a = "";
            List<string> periodo_where = new List<string>();

            //Periodo            
            if (cmbSel_periodo.Text == "MANHÃ")
            {
                //MANHÃ 
                for (int i = 0; i < data.Count; i++)
                {
                    a = data[i].Replace("' AND", " 08:00:00' AND");
                    periodo_where.Add("DATA_ATENDIMENTO between '" + a + " 11:59:59'");
                }
            }
            else if (cmbSel_periodo.Text == "TARDE")
            {
                //TARDE 
                for (int i = 0; i < data.Count; i++)
                {
                    a = data[i].Replace("' AND", " 12:00:00' AND");
                    periodo_where.Add("DATA_ATENDIMENTO between '" + a + " 17:59:59'");
                }
            }
            else if (cmbSel_periodo.Text == "NOITE")
            {
                //NOITE 
                for (int i = 0; i < data.Count; i++)
                {
                    a = data[i].Replace("' AND", " 18:00:00' AND");
                    periodo_where.Add("DATA_ATENDIMENTO between '" + a + " 21:59:59'");
                }
            }
            else
            {
                //TODOS
                periodo_where.Add("DATA_ATENDIMENTO between '" + data[0] + " 21:59:59'");
            }

            return periodo_where;

        }

        private void ckbCon_disciplina_CheckedChanged(object sender, EventArgs e)
        {
            filtro_dtp_atend();
        }

        private void dtpAtend_ini_ValueChanged(object sender, EventArgs e)
        {
            if (dtpAtend_ini.Checked)
            {
                cmbSel_periodo.Enabled = true;
            }
            else
            {
                cmbSel_periodo.Enabled = false;
                dtpAtend_fin.Checked = false;
                dtpAtend_fin.Value = DateTime.Now;
            }

            filtro_dtp_atend();
        }

        public void filtro_dtp_atend()
        {
            DataTable tab_atendimentos = new DataTable();

            //DataTable table = new DataTable();
            //table.Columns.Add("id_atendimento");
            //table.Columns.Add("n_mat");
            //table.Columns.Add("aluno");
            //table.Columns.Add("ensino");
            //table.Columns.Add("id_tipo_atendimento");
            //table.Columns.Add("mencao");
            //table.Columns.Add("modulo");
            //table.Columns.Add("id_user_lanc");
            //table.Columns.Add("data_atendimento");

            //@"SELECT ID_ATENDIMENTO, ID_TIPO_ATENDIMENTO, DATA_ATENDIMENTO, N_MAT, MODULO, ID_USER_LANC, ID_DISCIPLINA, ENSINO 
            //FROM ATENDIMENTOS 
            //WHERE ID_DISCIPLINA=___ AND ENSINO=___ ORDER BY ___";

            string command_text = "";

            command_text = @"SELECT ID_ATENDIMENTO, ID_TIPO_ATENDIMENTO, DATA_ATENDIMENTO, N_MAT, MODULO, ID_USER_LANC, ID_DISCIPLINA, ENSINO FROM ATENDIMENTOS";

            command_text = @"SELECT ATENDIMENTOS.N_MAT  
                            ,ALUNO     
  	                        ,ALUNOS.ENSINO
                            ,TIPO_ATENDIMENTO
                            ,DATA_ATENDIMENTO
  	                        ,N_DISCIPLINA
  	                        ,USUARIO.NOME
                            FROM ATENDIMENTOS 
                            JOIN TIPO_ATENDIMENTO
                            ON ATENDIMENTOS.ID_TIPO_ATENDIMENTO = TIPO_ATENDIMENTO.ID_TIPO_ATENDIMENTO
                            LEFT JOIN NOTAS
                            ON ATENDIMENTOS.ID_ATENDIMENTO = NOTAS.ID_ATENDIMENTO
                            LEFT JOIN ALUNOS
                            ON ATENDIMENTOS.N_MAT = ALUNOS.N_MAT
                            LEFT JOIN DISCIPLINAS
                            ON ATENDIMENTOS.ID_DISCIPLINA = DISCIPLINAS.ID_DISCIPLINA
                            LEFT JOIN USUARIO
                            ON ATENDIMENTOS.ID_USER_LANC = USUARIO.ID_USUARIO";
            //CAMPO REMOVIDO
            //,NOTAS.NOTA
            //,MODULO

            //where id_disciplina
            if (cmbSel_disciplina.Text != "")
            {
                //id_disciplina
                //command_text += " WHERE ID_DISCIPLINA=" + cs_disciplinas.troca_nome_disc_id(cmbSel_disciplina.Text);
                //nome_disciplina
                command_text += " WHERE N_DISCIPLINA='" + cmbSel_disciplina.Text + "'";
            }

            //where ensino
            if (cmbSel_ensino.Text != "")
            {
                if (command_text.Contains("WHERE"))
                {
                    command_text += " AND ALUNOS.ENSINO='" + cmbSel_ensino.Text + "'";
                }
                else
                {
                    command_text += " AND ALUNOS.ENSINO='" + cmbSel_ensino.Text + "'";
                }

                //int i = 1;

                //if (cmbSel_ensino.Text == "FUNDAMENTAL")
                //{
                //    i = 0;
                //}

                //if (command_text.Contains("WHERE"))
                //{
                //    command_text += " AND ENSINO=" + i;
                //}
                //else
                //{
                //    command_text += " WHERE ENSINO=" + i;
                //}
            }

            //where concluido
            if (ckbCon_disciplina.Checked)
            {
                if (command_text.Contains("WHERE"))
                {
                    command_text += " AND TIPO_ATENDIMENTO='ENCERRADO'";
                }
                else
                {
                    command_text += " WHERE TIPO_ATENDIMENTO='ENCERRADO'";
                }
            }
            //else
            //{
            //    if (command_text.Contains("WHERE"))
            //    {
            //        command_text += " AND CONCLUIDO=0";
            //    }
            //    else
            //    {
            //        command_text += " WHERE CONCLUIDO=0";
            //    }
            //}

            ////where ativo
            //if (!ckbInativos_disciplina.Checked)
            //{
            //    if (command_text.Contains("WHERE"))
            //    {
            //        command_text += " AND ATIVO=1";
            //    }
            //    else
            //    {
            //        command_text += " WHERE ATIVO=1";
            //    }
            //}

            //where data
            if (dtpAtend_ini.Checked)
            {
                if (command_text.Contains("WHERE"))
                {
                    command_text += " AND DATA_ATENDIMENTO between '" + definir_data(false)[0] + " 21:59:59'";
                }
                else
                {
                    command_text += " WHERE DATA_ATENDIMENTO between '" + definir_data(false)[0] + " 21:59:59'";
                }
            }

            //where periodo ou executa
            if (cmbSel_periodo.Text != "")
            {
                if (command_text.Contains("WHERE"))
                {
                    if (command_text.Contains("AND DATA_ATENDIMENTO between"))
                    {
                        int part_a = command_text.IndexOf("AND DATA_ATENDIMENTO between");
                        int part_b = command_text.IndexOf("59'") + 3;

                        command_text = command_text.Remove(part_a, part_b - part_a);

                        command_text += " AND ";
                    }
                    else if (command_text.Contains("WHERE DATA_ATENDIMENTO between"))
                    {
                        int part_a = command_text.IndexOf("WHERE DATA_ATENDIMENTO between");
                        int part_b = command_text.IndexOf("59'") + 3;

                        command_text = command_text.Remove(part_a, part_b - part_a);

                        command_text += " WHERE ";
                    }
                }
                else
                {
                    command_text += "WHERE ";
                }

                if (dtpAtend_fin.Checked)
                {

                    List<string> list_data = new List<string>();
                    list_data = definir_data(true);

                    for (int i = 0; i < list_data.Count; i++)
                    {
                        //fazer loop
                        //criar lista de datas
                        string a = "";

                        a += command_text + definir_periodo(true, list_data)[i];

                        a += " ORDER BY DATA_ATENDIMENTO DESC";

                        foreach (DataRow row in cs_atendimentos.buscarAtendimentos(a).Rows)
                        {
                            if (tab_atendimentos.Rows.Count > 0)
                            {
                                tab_atendimentos.ImportRow(row);
                            }
                            else
                            {
                                tab_atendimentos = cs_atendimentos.buscarAtendimentos(a);
                                //Teste -______________________________________________________________
                                txtInstrucao.Text = a;
                                break;
                            }

                        }
                    }
                }
                else
                {
                    //criar lsta de um dia 
                    List<string> list_data = new List<string>();
                    list_data = definir_data(false);

                    for (int i = 0; i < list_data.Count; i++)
                    {
                        string a = "";

                        a += command_text + definir_periodo(false, list_data)[i];

                        a += " ORDER BY DATA_ATENDIMENTO DESC";

                        tab_atendimentos = cs_atendimentos.buscarAtendimentos(a);
                        //Teste -______________________________________________________________
                        txtInstrucao.Text = a;
                    }
                }
            }
            else
            {
                //Executa
                command_text += " ORDER BY DATA_ATENDIMENTO DESC";
                tab_atendimentos = cs_atendimentos.buscarAtendimentos(command_text);
                //Teste -______________________________________________________________
                txtInstrucao.Text = command_text;
            }

            dgvAlunos_disciplina.DataSource = tab_atendimentos;
            lblQtd_atendimentos.Text = "Atendimentos: " + dgvAlunos_disciplina.Rows.Count;
            //Teste -______________________________________________________________
            txtInstrucao.Text = command_text;
        }

        private void dtpAtend_fin_ValueChanged(object sender, EventArgs e)
        {
            filtro_dtp_atend();
        }

        private void cmbSel_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtro_dtp_atend();
        }

        private void cmbSel_ensino_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtro_dtp_atend();
        }

        private void gbParametros_Paint(object sender, PaintEventArgs e)
        {
            Graphics gObject = gbParametros.CreateGraphics();

            Brush gray = new SolidBrush(Color.LightGray);
            Pen pen = new Pen(gray, 1);

            gObject.DrawLine(pen, 295, 20, 295, 116);
        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            cmbSel_disciplina.Text = "";
            cmbSel_periodo.Text = "";
            cmbSel_ensino.Text = "";
            ckbCon_disciplina.Checked = false;
            ckbInativos_disciplina.Checked = false;
            ckbMencao.Checked = false;
            dtpAtend_ini.Value = DateTime.Now;
            dtpAtend_fin.Value = DateTime.Now;
            dtpAtend_ini.Checked = false;
            dtpAtend_fin.Checked = false;
            filtro_dtp_atend();
        }

        #endregion

        
    }

}
