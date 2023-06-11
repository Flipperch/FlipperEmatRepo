using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using EmatWinFormsNetFramework13032.Properties;
using System.Threading;

namespace EmatWinFormsNetFramework13032.Notas
{
    public partial class frPassaporte : Form
    {

        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        csNotas cs_notas = new csNotas();
        Alunos.csAlunos cs_alunos = new Alunos.csAlunos();
        csDisciplinas cs_disciplinas = new csDisciplinas();
        csAtendimentos cs_atendimentos = new csAtendimentos();
        csTipo_Atendimento cs_tipoatendimento = new csTipo_Atendimento();

        Usuarios_Grupos.csUsuarios cs_usuarios = new Usuarios_Grupos.csUsuarios();

        //classe descrições
        csDescricoes cs_descriçoes = new csDescricoes();

        //Alterações no dgv
        string valor_dgv_anterior = "";

        public string n_mat;

        public int id_ensino_selecionado; //FUNDAMENTAL = 1, MÉDIO = 2;
        public string sel_ensino;

        object sender_mouse_right_dgv;

        //Parametros
        int id_disc;
        int id_ensino;

        //Data disciplina selecionada
        public DateTime orient_ini_disc_selecionada;

        public int id_ensino_atual_aluno;

        List<int> disciplinas_para_criar = new List<int>();
        List<int> disciplinas_para_criar_completo = new List<int>();        

        public frPassaporte()
        {
            InitializeComponent();

            //Campos tabela NOTAS
            //[N_MAT]
            //[MODULO]
            //[NOTA]
            //[ID_DISCIPLINA]
            //[ID_USU_LANC]
            //[ID_USU_MOD]
            //[DAT_LANC_NOTA]
            //[DAT_MOD_NOTA]
        }

        private void FRpassaporte_Load(object sender, EventArgs e)
        {
            flpPassaporteIndividual.Resize -= flpPassaporteIndividual_Resize;

            preencher_dados_aluno();
            alterar_visual_passaporte();

            //Disciplinas Individuais
            criar_nova_disciplina();
            preencher_dgvAtendimentos_dis_area();
            //Disciplinas Completas
            criar_nova_disciplina_completo();
            preencher_dgvAtendimentos_completo();

            if(Usuarios_Grupos.csUsuario_logado.id_disciplina_logado == 0)
            {
                tbcPassaporte.SelectedIndex = 1;
            }

            flpPassaporteIndividual.Resize += flpPassaporteIndividual_Resize;
        }

        private void preencher_dados_aluno()
        {
            //dados label form

            List<Alunos.csAlunos> list_ = cs_alunos.dados_aluno(n_mat);
            lblNome.Text = "NOME: " + list_[0].nome;            
            lblRg.Text = "RG: " + list_[0].rg;
            

            if (Configurações.csEscola.cidade.ToLower() == "sorocaba")
            {
                lblTermo.Text = "TERMO: " + list_[0].termo_mat;        
            }
            else if (Configurações.csEscola.cidade.ToLower() == "americana")
            {
                lblTermo.Text = "MUNICÍPIO: " + list_[0].res_cidade;
            }            
            lblNmat.Text = "MATRÍCULA: " + n_mat;
            this.Text = n_mat + " - PASSAPORTE ALUNO";
            txtObs.Text = list_[0].obs_passaporte;
            id_ensino_atual_aluno = list_[0].id_ensino_atual;
            lblDisciplinaAtual.Text = "DISC. ATUAL: " + cs_disciplinas.troca_disciplina_id_por_nome(list_[0].id_disciplina_atual);            
            lblDatMat.Text = "DATA DE MATRÍCULA: " + list_[0].dat_mat;
            id_ensino_selecionado = list_[0].id_ensino_atual;
        }

        private void alterar_visual_passaporte()
        {
            //ensino selecionado

            lblTitulo.Text = "PASSAPORTE ENSINO " + cs_disciplinas.troca_ensino_id_por_nome(id_ensino_selecionado);
            sel_ensino = cs_disciplinas.troca_ensino_id_por_nome(id_ensino_selecionado);

            if (Configurações.csEscola.cidade.ToLower() == "sorocaba")
            {
                if (id_ensino_selecionado == 1)
                {
                    this.BackColor = Color.FromArgb(48, 48, 48); //Preto - FUNDAMENTAL
                }
                else if (id_ensino_selecionado == 2)
                {
                    this.BackColor = Color.FromArgb(19, 91, 133);//azul - MÉDIO                   
                }
            }
            else if (Configurações.csEscola.cidade.ToLower() == "americana")
            {

                if (id_ensino_selecionado == 1)
                {
                    this.BackColor = Color.LightGray;
                    //txtObs.BackColor = Color.LightGray;
                }
                else if (id_ensino_selecionado == 2)
                {
                    this.BackColor = Color.FromArgb(19, 91, 133);//azul                   
                    //txtObs.BackColor = Color.FromArgb(19, 91, 133);//azul
                    
                }

                foreach (Label lbl in panel1.Controls.OfType<Label>())
                {
                    lblTitulo.ForeColor = Color.Black;
                    lbl.ForeColor = Color.Black;
                }
            }
        }

        public void criar_nova_disciplina()
        {
            disciplinas_para_criar.Clear();
            flpPassaporteIndividual.Controls.Clear();
            //OBTER LISTA DE IDS_DISCIPLINAS DO ENSINO SELECIONADO
            List<int> list_ids_disciplina_ensino = cs_disciplinas.list_ensinos(id_ensino_selecionado)[0].ids_disciplinas_ensino;

            //OBTER INFORMAÇÕES DO USUÁRIO;
            List<Usuarios_Grupos.csUsuarios> list_ = cs_usuarios.lista_usuarios(Usuarios_Grupos.csUsuario_logado.id_usuario_logado);                        

            if (list_[0].id_disciplina_user != 0)
            {
                if (list_ids_disciplina_ensino.Contains(list_[0].id_disciplina_user))
                disciplinas_para_criar.Add(list_[0].id_disciplina_user);
            }
            //Alerta - Disciplina Atual
            if (!disciplinas_para_criar.Contains(cs_alunos.id_disciplina_atual__(n_mat)))
            {                
                lblDisciplinaAtual.ForeColor = Color.Red;
            }


            //VERIFICAR DISCIPLINAS DENTRO DA ÁREA
            if (list_[0].ids_areas != null)
            {
                for (int i_areas = 0; i_areas < list_[0].ids_areas.Count; i_areas++)
                {
                    //adcionar discipplinas pela qtd
                    List<int> lista_disciplinas_do_user = cs_disciplinas.list_areas(list_[0].ids_areas[i_areas])[0].ids_disciplinas_area;
                    List<int> lista_ids_disciplinas_do_ensino = cs_disciplinas.list_ensinos(id_ensino_selecionado)[0].ids_disciplinas_ensino;

                    for (int i = 0; i < lista_disciplinas_do_user.Count; i++)
                    {
                        //Verificar se id_disciplina esta na lista de ensino.
                        if (lista_ids_disciplinas_do_ensino.Contains(lista_disciplinas_do_user[i]))
                        {
                            //verificar se não esta na lista, caso não add para criar
                            if (!disciplinas_para_criar.Contains(lista_disciplinas_do_user[i]))
                            {
                                disciplinas_para_criar.Add(lista_disciplinas_do_user[i]);
                            }
                        }                        
                    }
                }
            }

            if (disciplinas_para_criar.Count > 0)
            {
                if (disciplinas_para_criar.Count > 1)
                {
                    ////Localização e tamanho do formulario
                    //this.Location = new Point(87, 20);
                    //this.Size = new Size(846, 600);
                    //
                    ////tbc
                    //tbcPassaporte.Size = new Size(806, 360);
                    //
                    ////tamanho do flp
                    //flpPassaporteIndividual.Size = new Size(764, 335);
                }

                //Limpar controles
                flpPassaporteIndividual.Controls.Clear();

                for (int i = 0; i < disciplinas_para_criar.Count; i++)
                {
                    TableLayoutPanel tlp_nova_disciplina = new TableLayoutPanel();
                    tlp_nova_disciplina.Name = "tlp_" + cs_disciplinas.troca_disciplina_id_por_nome(disciplinas_para_criar[i]);
                    tlp_nova_disciplina.RowCount = 2;
                    tlp_nova_disciplina.ColumnCount = 2;
                    //Tamanho Linhas
                    tlp_nova_disciplina.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                    tlp_nova_disciplina.RowStyles.Add(new RowStyle());
                    //Tamanho Colunas
                    tlp_nova_disciplina.ColumnStyles.Add(new ColumnStyle());
                    tlp_nova_disciplina.ColumnStyles.Add(new ColumnStyle());

                    if (disciplinas_para_criar[i] == cs_alunos.id_disciplina_atual__(n_mat))
                    {
                        tlp_nova_disciplina.BorderStyle = BorderStyle.FixedSingle;
                        //tlp_nova_disciplina.BorderColor = 
                    }

                    //tlp_nova_disciplina.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    tlp_nova_disciplina.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;

                    //LABEL
                    Label lbl_disciplina = new Label();
                    lbl_disciplina.AutoSize = true;
                    lbl_disciplina.Text = cs_disciplinas.troca_disciplina_id_por_nome(disciplinas_para_criar[i]);
                    lbl_disciplina.Font = new Font("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbl_disciplina.Dock = DockStyle.Left;
                    lbl_disciplina.TextAlign = ContentAlignment.MiddleLeft;



                    //DATAGRIDVIEW
                    DataGridView dgv_nova_disciplina = new DataGridView();
                    dgv_nova_disciplina.Name = "dgv_" + cs_disciplinas.troca_disciplina_id_por_nome(disciplinas_para_criar[i]);
                    dgv_nova_disciplina.BackgroundColor = Color.White;
                    dgv_nova_disciplina.AllowUserToAddRows = false;
                    dgv_nova_disciplina.RowHeadersVisible = false;
                    dgv_nova_disciplina.ColumnHeadersVisible = false;
                    dgv_nova_disciplina.ContextMenuStrip = cmsOpcoes_dgv;
                    dgv_nova_disciplina.Dock = DockStyle.Fill;

                    tlp_nova_disciplina.SetColumnSpan(dgv_nova_disciplina, 2);

                    #region Data Orientação Inicial

                    //FLOWLAYOUTPANEL
                    FlowLayoutPanel flp_data_ini = new FlowLayoutPanel();
                    flp_data_ini.Dock = DockStyle.Fill;
                    flp_data_ini.FlowDirection = FlowDirection.RightToLeft;
                    flp_data_ini.Padding = new Padding(0);
                    flp_data_ini.Margin = new Padding(0);
                    //flp_data_ini.BackColor = Color.Green;

                    //DATATIMEPICKER DATA
                    DateTimePicker dtp_disciplina = new DateTimePicker();
                    dtp_disciplina.Name = "dtp_" + cs_disciplinas.troca_disciplina_id_por_nome(disciplinas_para_criar[i]);
                    dtp_disciplina.Dock = DockStyle.Right;
                    dtp_disciplina.Format = DateTimePickerFormat.Short;
                    dtp_disciplina.Margin = new Padding(2);
                    dtp_disciplina.Width = 100;

                    dtp_disciplina.ValueChanged += dtp_orient_ini_ValueChanged;

                    flp_data_ini.Controls.Add(dtp_disciplina);

                    //LABEL - DATA
                    Label lbl_data = new Label();
                    lbl_data.AutoSize = true;
                    lbl_data.Text = "Orient. Inicial:";
                    lbl_data.Font = new Font("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbl_data.Dock = DockStyle.Right;
                    lbl_data.Margin = new Padding(0, 5, 0, 0);

                    flp_data_ini.Controls.Add(lbl_data);

                    #endregion


                    //Eventos
                    dgv_nova_disciplina.CellMouseClick += dgvAtendimentos_dis_area_CellMouseClick;
                    dgv_nova_disciplina.CellMouseDown += dgvAtendimentos_dis_area_CellMouseDown;
                    dgv_nova_disciplina.EditingControlShowing += dgvAtendimentos_dis_area_EditingControlShowing;
                    dgv_nova_disciplina.CellMouseEnter += dgvAtendimentos_dis_area_CellMouseEnter;
                    dgv_nova_disciplina.CellMouseLeave += dgvAtendimentos_dis_area_CellMouseLeave;
                    dgv_nova_disciplina.CellEnter += dgvAtendimentos_dis_area_CellEnter;
                    dgv_nova_disciplina.CellEndEdit += dgvAtendimentos_dis_area_CellEndEdit;

                    //dgv_nova_disciplina.Size = new Size(780, 83);
                    //dgv_nova_disciplina.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    //dgv_nova_disciplina.Dock = DockStyle.Fill;

                    //ADD LABEL NOME DISCIPLINA
                    tlp_nova_disciplina.Controls.Add(lbl_disciplina, 0, 0);
                    //ADD DATAGRIDVIEW
                    tlp_nova_disciplina.Controls.Add(dgv_nova_disciplina, 0, 1);
                    //ADD FLOW_DATA_INICIAL
                    tlp_nova_disciplina.Controls.Add(flp_data_ini, 1, 0);
                    //ADD TABLE_NOVA_DISCIPLINA
                    flpPassaporteIndividual.Controls.Add(tlp_nova_disciplina);





                }
            }
        }
        public void preencher_dgvAtendimentos_dis_area()
        {
            
            for (int i = 0; i < disciplinas_para_criar.Count; i++)
            {
                string nome_disciplina_add = cs_disciplinas.troca_disciplina_id_por_nome(disciplinas_para_criar[i]);

                foreach (TableLayoutPanel tlp in flpPassaporteIndividual.Controls.OfType<TableLayoutPanel>())
                {
                    foreach (DataGridView dgv in tlp.Controls.OfType<DataGridView>())
                    {
                        if (dgv.Name == "dgv_" + cs_disciplinas.troca_disciplina_id_por_nome(disciplinas_para_criar[i]))
                        {
                            #region Conf_linhas e colunas do DGV

                            //Desliga enventos no momento da montagem do datagridview
                            dgv.CellEnter -= dgvAtendimentos_dis_area_CellEnter;

                            //limpa dgv
                            dgv.DataSource = null;
                            dgv.Rows.Clear();
                            dgv.Columns.Clear();

                            //Titulo
                            //Columns
                            dgv.Columns.Add("titulo", "");
                            dgv.Columns[0].Width = 60;
                            dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;                            
                            dgv.Columns[0].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold); //ok
                            //Bloqueia
                            dgv.Columns[0].ReadOnly = true;

                            //Rows
                            dgv.Rows.Add(10);
                            dgv.Rows[0].Cells[0].Value = "id_atendimento";
                            dgv.Rows[0].Visible = false;

                            dgv.Rows[1].Cells[0].Value = "TIPO.";

                            if(Configurações.csEscola.cidade.ToLower() == "americana")
                            {
                                dgv.Rows[2].Cells[0].Value = "AVAL.";

                                dgv.Rows[3].Cells[0].Value = "NOTA";
                            }
                            else
                            {
                                dgv.Rows[2].Cells[0].Value = "MOD.";

                                dgv.Rows[3].Cells[0].Value = "AVAL.";
                            }
                            


                            //User lançamento atendimento
                            dgv.Rows[4].Cells[0].Value = "id_user_lanc";
                            dgv.Rows[4].Visible = false;
                            //Data atendimento
                            dgv.Rows[5].Cells[0].Value = "data_atendimento";
                            dgv.Rows[5].Visible = false;

                            //Notas
                            //Atribuido por:
                            dgv.Rows[6].Cells[0].Value = "atrib_por";
                            dgv.Rows[6].Visible = false;

                            //modificado por:
                            dgv.Rows[7].Cells[0].Value = "alter_por";
                            dgv.Rows[7].Visible = false;

                            //Data inserido:
                            dgv.Rows[8].Cells[0].Value = "data_inserido";
                            dgv.Rows[8].Visible = false;

                            //Data modificado:
                            dgv.Rows[9].Cells[0].Value = "data_modificado";
                            dgv.Rows[9].Visible = false;

                            #endregion

                            bool orient_ini_ok = true;

                            #region Preencher Orient_inicial

                            foreach (FlowLayoutPanel flp_ in tlp.Controls.OfType<FlowLayoutPanel>())
                            {
                                foreach (DateTimePicker dtp_ in flp_.Controls.OfType<DateTimePicker>())
                                {
                                    List<csTipo_Atendimento> list_tipos = cs_tipoatendimento.lista_tipos_atendimentos(disciplinas_para_criar[i], "ORIENTAÇÃO INICIAL");
                                    if(list_tipos.Count>0)
                                    {
                                        int id_tipo_atend_orient_ini_da_disc = list_tipos[0].id_tipo_atendimento;
                                        
                                        List<csAtendimentos> list_ = cs_atendimentos.lista_atendimentos(0, id_tipo_atend_orient_ini_da_disc, n_mat, id_ensino_selecionado);
                                        if (list_.Count > 0)
                                        {
                                            dtp_.Value = DateTime.Parse(list_[0].data_atendimento);
                                            dtp_.Enabled = false;
                                            orient_ini_ok = true;
                                        }
                                        else
                                        {
                                            dtp_.Enabled = true;
                                            orient_ini_ok = false;
                                        }
                                    }
                                    
                                }
                            }
                            

                            #endregion

                            #region Preencher DGV

                            DataTable table = new DataTable();
                            table = cs_atendimentos.buscarAtendimento_ind(n_mat, disciplinas_para_criar[i], id_ensino_selecionado);

                            List<int> ids_tipos_atendimentos_usuario = new List<int>();

                            for (int z = 0; z < table.Rows.Count; z++)
                            {
                                //Columns
                                dgv.Columns.Add("nota_" + z, "");
                                dgv.Columns[z + 1].Width = 50;
                                dgv.Columns[z + 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                                //Id_atendimento
                                dgv.Rows[0].Cells[z + 1].Value = table.Rows[z][0];
                                //_____________                      

                                //CONFIGURAÇÃO COMBOBOX - TIPO DE ATENDIMENTO
                                if (disciplinas_para_criar[i] != 0)
                                {
                                    DataGridViewComboBoxCell cmb = new DataGridViewComboBoxCell();
                                    cmb.DropDownWidth = 150;
                                    cmb.DataSource = cs_tipoatendimento.lista_de_tipos(disciplinas_para_criar[i]);
                                    string a = cs_tipoatendimento.troca_id_tipoatend_nome(Convert.ToInt32(table.Rows[z][1]));
                                    cmb.Value = a;
                                    dgv.Rows[1].Cells[z + 1] = cmb;
                                }

                                //_____________

                                //Modulo
                                dgv.Rows[2].Cells[z + 1].Value = table.Rows[z][3];
                                //_____________

                                //Nota
                                if (cs_notas.nota(Convert.ToInt32(table.Rows[z][0])) != 50)
                                {
                                    dgv.Rows[3].Cells[z + 1].Value = cs_notas.nota(Convert.ToInt32(table.Rows[z][0]));
                                    dgv.Rows[3].Cells[z + 1].Style.BackColor = Color.White;
                                }
                                else
                                {
                                    dgv.Rows[3].Cells[z + 1].ReadOnly = true;
                                    dgv.Rows[3].Cells[z + 1].Style.BackColor = Color.DarkGray;
                                }

                                if (table.Rows[z][3].ToString() == "MF")
                                {
                                    dgv.Rows[3].Cells[z + 1].ReadOnly = false;
                                    dgv.Rows[3].Cells[z + 1].Style.BackColor = Color.White;

                                    dgv.Rows[3].Cells[z + 1].Value = cs_notas.media_final_idatend(Convert.ToInt32(table.Rows[z][0]));
                                }

                                ///MÉDIA - HIST_ALUNO_
                                //if (table.Rows[z][3].ToString() == "MF")
                                //{
                                //    dgv.Rows[3].Cells[i + 1].ReadOnly = false;
                                //    dgv.Rows[3].Cells[i + 1].Style.BackColor = Color.White;

                                //    dgv.Rows[3].Cells[i + 1].Value = cs_notas.media_final(n_mat, cs_alunos.ensino(n_mat), Usuarios_Grupos.csUsuario_logado.mat_atual);
                                //}

                                //_____________

                                //User lançamento
                                dgv.Rows[4].Cells[z + 1].Value = table.Rows[z][4];
                                //_____________

                                //Data_atendimento
                                dgv.Rows[5].Cells[z + 1].Value = table.Rows[z][2];
                                //_____________

                                //atrib_pori
                                if (cs_notas.id_usu_lanc(Convert.ToInt32(table.Rows[z][0])) != 0)
                                {
                                    dgv.Rows[6].Cells[z + 1].Value = cs_notas.id_usu_lanc(Convert.ToInt32(table.Rows[z][0]));
                                }
                                //alter_por
                                if (cs_notas.id_usu_mod(Convert.ToInt32(table.Rows[z][0])) != 0)
                                {
                                    dgv.Rows[7].Cells[z + 1].Value = cs_notas.id_usu_mod(Convert.ToInt32(table.Rows[z][0]));
                                }

                                //data_inserido
                                if (cs_notas.dat_lanc_nota(Convert.ToInt32(table.Rows[z][0])) != "")
                                {
                                    dgv.Rows[8].Cells[z + 1].Value = cs_notas.dat_lanc_nota(Convert.ToInt32(table.Rows[z][0]));
                                }

                                //data_modificado
                                if (cs_notas.dat_mod_nota(Convert.ToInt32(table.Rows[z][0])) != "")
                                {
                                    dgv.Rows[9].Cells[z + 1].Value = cs_notas.dat_mod_nota(Convert.ToInt32(table.Rows[z][0]));
                                }
                            }

                            if ((dgv.Rows[1].Cells[dgv.ColumnCount - 1].Value.ToString() != "ENCERRADO") && (dgv.Rows[1].Cells[dgv.ColumnCount - 1].Value.ToString() != "MÉDIA FINAL"))
                            {
                                //Colls para inserir nota
                                dgv.Columns.Add("col_btadd", "");
                                dgv.Columns[dgv.Columns.Count - 1].Width = 50;

                                //Modulo
                                dgv.Rows[2].Cells[dgv.Columns.Count - 1].ReadOnly = true;

                                //Nota

                                dgv.Rows[3].Cells[dgv.Columns.Count - 1].ReadOnly = true;

                                DataGridViewComboBoxCell cmb_novo = new DataGridViewComboBoxCell();



                                List<string> valores_tipos_atendimentos = cs_tipoatendimento.lista_de_tipos(disciplinas_para_criar[i]);

                                if (orient_ini_ok)
                                    valores_tipos_atendimentos.Remove("ORIENTAÇÃO INICIAL");
                                
                                cmb_novo.DataSource = valores_tipos_atendimentos;


                                dgv.Rows[1].Cells[dgv.Columns.Count - 1] = cmb_novo;
                            }

                            //Reativa eventos depois da montagem.
                            dgv.CellEnter += dgvAtendimentos_dis_area_CellEnter;

                            //Verifica scroolbar

                            foreach (var scroll in dgv.Controls.OfType<HScrollBar>())
                            {
                                if (scroll.Visible)
                                {
                                    //SCROLL HORIZONTAL                                    
                                    dgv.Size = new Size(764, 87);

                                    dgv.Parent.Size = new Size(764, 120);

                                    //tbcPassaporte.Size = new Size(806, 150);
                                    //this.Size = new Size(850, 386);
                                }
                                else
                                {
                                    //SEM SCROLL HORIZONTAL
                                    dgv.Size = new Size(764, 70);

                                    dgv.Parent.Size = new Size(764, 104);

                                    //tbcPassaporte.Size = new Size(806, 137);
                                    //this.Size = new Size(850, 374);
                                }
                            }

                            #endregion

                        }
                    }
                }
            }
        }

        public void criar_nova_disciplina_completo()
        {
            disciplinas_para_criar_completo.Clear();
            flpPassaporteCompleto.Controls.Clear();
            //OBTER LISTA DE IDS_DISCIPLINAS DO ENSINO SELECIONADO
            List<int> list_ids_disciplina_ensino = cs_disciplinas.list_ensinos(id_ensino_selecionado)[0].ids_disciplinas_ensino;
            for (int i = 0; i < list_ids_disciplina_ensino.Count; i++)
            {
                disciplinas_para_criar_completo.Add(list_ids_disciplina_ensino[i]);
            }

            //Ordenar de acordo com 
            List<csAtendimentos> list_atend = cs_atendimentos.lista_atendimentos(0,0,n_mat,id_ensino_selecionado);
            for (int i = 0; i < list_atend.Count; i++)
            {
                if(disciplinas_para_criar_completo.Contains(list_atend[i].id_disciplina))
                {
                    disciplinas_para_criar_completo.Remove(list_atend[i].id_disciplina);

                    disciplinas_para_criar_completo.Insert(0, list_atend[i].id_disciplina);
                }
            }

            if (disciplinas_para_criar_completo.Count > 0)
            {
                if (disciplinas_para_criar_completo.Count > 1)
                {
                    ////Localização e tamanho do formulario
                    //this.Location = new Point(87, 20);
                    //this.Size = new Size(846, 600);
                    //
                    ////tbc
                    //tbcPassaporte.Size = new Size(806, 360);
                    //
                    ////tamanho do flp
                    //flpPassaporteCompleto.Size = new Size(764, 335);
                }
                for (int i = 0; i < disciplinas_para_criar_completo.Count; i++)
                {
                    TableLayoutPanel tlp_nova_disciplina = new TableLayoutPanel();
                    tlp_nova_disciplina.Name = "tlp_completo_" + cs_disciplinas.troca_disciplina_id_por_nome(disciplinas_para_criar_completo[i]);
                    tlp_nova_disciplina.RowCount = 2;
                    tlp_nova_disciplina.ColumnCount = 2;
                    //Tamanho Linhas
                    tlp_nova_disciplina.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                    tlp_nova_disciplina.RowStyles.Add(new RowStyle());
                    //Tamanho Colunas
                    tlp_nova_disciplina.ColumnStyles.Add(new ColumnStyle());
                    tlp_nova_disciplina.ColumnStyles.Add(new ColumnStyle());

                    //tlp_nova_disciplina.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    tlp_nova_disciplina.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;

                    //LABEL
                    Label lbl_disciplina = new Label();
                    lbl_disciplina.AutoSize = true;
                    lbl_disciplina.Text = cs_disciplinas.troca_disciplina_id_por_nome(disciplinas_para_criar_completo[i]);
                    lbl_disciplina.Font = new Font("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbl_disciplina.Dock = DockStyle.Left;
                    lbl_disciplina.TextAlign = ContentAlignment.MiddleLeft;

                    //DATAGRIDVIEW
                    DataGridView dgv_nova_disciplina = new DataGridView();
                    dgv_nova_disciplina.Name = "dgv_completo_" + cs_disciplinas.troca_disciplina_id_por_nome(disciplinas_para_criar_completo[i]);
                    dgv_nova_disciplina.BackgroundColor = Color.White;
                    dgv_nova_disciplina.AllowUserToAddRows = false;
                    dgv_nova_disciplina.RowHeadersVisible = false;
                    dgv_nova_disciplina.ColumnHeadersVisible = false;
                    dgv_nova_disciplina.ContextMenuStrip = cmsOpcoes_dgv;
                    dgv_nova_disciplina.Dock = DockStyle.Fill;
                    dgv_nova_disciplina.ReadOnly = true;

                    tlp_nova_disciplina.SetColumnSpan(dgv_nova_disciplina, 2);

                    //FLOWLAYOUTPANEL
                    FlowLayoutPanel flp_data_ini = new FlowLayoutPanel();
                    flp_data_ini.Dock = DockStyle.Fill;
                    flp_data_ini.FlowDirection = FlowDirection.RightToLeft;
                    flp_data_ini.Padding = new Padding(0);
                    flp_data_ini.Margin = new Padding(0);
                    //flp_data_ini.BackColor = Color.Green;

                    //DATATIMEPICKER DATA - Substituido por label, já quenão haverá alteração
                    //DateTimePicker dtp_disciplina = new DateTimePicker();
                    //dtp_disciplina.Name = "dtp_completo_" + cs_disciplinas.troca_disciplina_id_por_nome(disciplinas_para_criar_completo[i]);
                    //dtp_disciplina.Dock = DockStyle.Right;
                    //dtp_disciplina.Format = DateTimePickerFormat.Short;
                    //dtp_disciplina.Width = 70;
                    //dtp_disciplina.Enabled = false;
                    //
                    //flp_data_ini.Controls.Add(dtp_disciplina);

                    //LABEL - DATA
                    Label lbl_data = new Label();
                    lbl_data.AutoSize = true;
                    lbl_data.Name = "lbl_orient_completo_" + cs_disciplinas.troca_disciplina_id_por_nome(disciplinas_para_criar_completo[i]);
                    lbl_data.Text = "Orient. Inicial:";
                    lbl_data.Font = new Font("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    lbl_data.Dock = DockStyle.Right;
                    lbl_data.Margin = new Padding(0, 5, 0, 0);


                    flp_data_ini.Controls.Add(lbl_data);




                    //Eventos
                    //dgv_nova_disciplina.CellMouseClick += dgvAtendimentos_dis_area_CellMouseClick;
                    //dgv_nova_disciplina.CellMouseDown += dgvAtendimentos_dis_area_CellMouseDown;
                    //dgv_nova_disciplina.EditingControlShowing += dgvAtendimentos_dis_area_EditingControlShowing;
                    dgv_nova_disciplina.CellMouseEnter += dgvAtendimentos_dis_area_CellMouseEnter;
                    dgv_nova_disciplina.CellMouseLeave += dgvAtendimentos_dis_area_CellMouseLeave;
                    //dgv_nova_disciplina.CellEnter += dgvAtendimentos_dis_area_CellEnter;
                    //dgv_nova_disciplina.CellEndEdit += dgvAtendimentos_dis_area_CellEndEdit;

                    //dgv_nova_disciplina.Size = new Size(780, 83);
                    //dgv_nova_disciplina.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    //dgv_nova_disciplina.Dock = DockStyle.Fill;

                    //ADD LABEL NOME DISCIPLINA
                    tlp_nova_disciplina.Controls.Add(lbl_disciplina, 0, 0);
                    //ADD DATAGRIDVIEW
                    tlp_nova_disciplina.Controls.Add(dgv_nova_disciplina, 0, 1);
                    //ADD FLOW_DATA_INICIAL
                    tlp_nova_disciplina.Controls.Add(flp_data_ini, 1, 0);
                    //ADD TABLE_NOVA_DISCIPLINA
                    flpPassaporteCompleto.Controls.Add(tlp_nova_disciplina);





                }
            }
        }
        public void preencher_dgvAtendimentos_completo()
        {
            for (int i = 0; i < disciplinas_para_criar_completo.Count; i++)
            {
                string nome_disciplina_add = cs_disciplinas.troca_disciplina_id_por_nome(disciplinas_para_criar_completo[i]);
                foreach (TableLayoutPanel tlp in flpPassaporteCompleto.Controls.OfType<TableLayoutPanel>())
                {
                    foreach (DataGridView dgv in tlp.Controls.OfType<DataGridView>())
                    {
                        if (dgv.Name == "dgv_completo_" + cs_disciplinas.troca_disciplina_id_por_nome(disciplinas_para_criar_completo[i]))
                        {
                            #region Conf_linhas e colunas do DGV

                            //Desliga enventos no momento da montagem do datagridview
                            //dgv.CellEnter -= dgvAtendimentos_dis_area_CellEnter;

                            //limpa dgv
                            dgv.DataSource = null;
                            dgv.Rows.Clear();
                            dgv.Columns.Clear();

                            //Titulo
                            //Columns
                            dgv.Columns.Add("titulo", "");
                            dgv.Columns[0].Width = 60;
                            dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                             
                            dgv.Columns[0].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                            //Bloqueia
                            dgv.Columns[0].ReadOnly = true;

                            //Rows
                            dgv.Rows.Add(10);
                            dgv.Rows[0].Cells[0].Value = "id_atendimento";
                            dgv.Rows[0].Visible = false;

                            dgv.Rows[1].Cells[0].Value = "TIPO.";

                            if (Configurações.csEscola.cidade.ToLower() == "americana")
                            {
                                dgv.Rows[2].Cells[0].Value = "AVAL.";

                                dgv.Rows[3].Cells[0].Value = "NOTA";
                            }
                            else
                            {
                                dgv.Rows[2].Cells[0].Value = "MOD.";

                                dgv.Rows[3].Cells[0].Value = "AVAL.";
                            }


                            //User lançamento atendimento
                            dgv.Rows[4].Cells[0].Value = "id_user_lanc";
                            dgv.Rows[4].Visible = false;
                            //Data atendimento
                            dgv.Rows[5].Cells[0].Value = "data_atendimento";
                            dgv.Rows[5].Visible = false;

                            //Notas
                            //Atribuido por:
                            dgv.Rows[6].Cells[0].Value = "atrib_por";
                            dgv.Rows[6].Visible = false;

                            //modificado por:
                            dgv.Rows[7].Cells[0].Value = "alter_por";
                            dgv.Rows[7].Visible = false;

                            //Data inserido:
                            dgv.Rows[8].Cells[0].Value = "data_inserido";
                            dgv.Rows[8].Visible = false;

                            //Data modificado:
                            dgv.Rows[9].Cells[0].Value = "data_modificado";
                            dgv.Rows[9].Visible = false;

                            #endregion

                            #region Preencher Orient_inicial

                            foreach (FlowLayoutPanel flp_ in tlp.Controls.OfType<FlowLayoutPanel>())
                            {
                                foreach (Label lbl_ in flp_.Controls.OfType<Label>())
                                {
                                    string dt = cs_notas.orient_ini_por_nmat(n_mat, disciplinas_para_criar_completo[i], id_ensino_selecionado);
                                    if (dt != string.Empty)
                                    {
                                        lbl_.Text = "Orient. Inicial: " + DateTime.Parse(dt).ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        
                                        string dt_TIPO= cs_notas.dat_ini_(n_mat,cs_tipoatendimento.troca_nome_tipoatend_id("ORIENTAÇÃO INICIAL",disciplinas_para_criar_completo[i]), id_ensino_selecionado);                                        
                                        if (dt_TIPO != string.Empty)
                                        {
                                            lbl_.Text = "Orient. Inicial: " + DateTime.Parse(dt_TIPO).ToString("dd/MM/yyyy");
                                        }
                                        else
                                        {
                                            lbl_.Text = "";
                                        }
                                        
                                    }
                                }
                            }

                            #endregion

                            #region Preencher DGV

                            DataTable table = new DataTable();                           
                            table = cs_atendimentos.buscarAtendimento_ind(n_mat, disciplinas_para_criar_completo[i], id_ensino_selecionado);                            

                            for (int z = 0; z < table.Rows.Count; z++)
                            {
                                //Columns
                                dgv.Columns.Add("nota_" + z, "");
                                dgv.Columns[z + 1].Width = 50;
                                dgv.Columns[z + 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                                //Id_atendimento
                                dgv.Rows[0].Cells[z + 1].Value = table.Rows[z][0];
                                //_____________

                                //id_tipo de atendimento - trocar por nome

                                //CONFIGURAÇÃO COMBOBOX - TIPO DE ATENDIMENTO
                                if (disciplinas_para_criar_completo[i] != 0)
                                {
                                    DataGridViewComboBoxCell cmb = new DataGridViewComboBoxCell();
                                    cmb.DropDownWidth = 150;
                                    cmb.DataSource = cs_tipoatendimento.lista_de_tipos(disciplinas_para_criar_completo[i]);
                                    string a = cs_tipoatendimento.troca_id_tipoatend_nome(Convert.ToInt32(table.Rows[z][1]));
                                    cmb.Value = a;
                                    dgv.Rows[1].Cells[z + 1] = cmb;
                                }

                                //_____________

                                //Modulo
                                dgv.Rows[2].Cells[z + 1].Value = table.Rows[z][3];
                                //_____________

                                //Nota
                                if (cs_notas.nota(Convert.ToInt32(table.Rows[z][0])) != 50)
                                {
                                    dgv.Rows[3].Cells[z + 1].Value = cs_notas.nota(Convert.ToInt32(table.Rows[z][0]));
                                    dgv.Rows[3].Cells[z + 1].Style.BackColor = Color.White;
                                }
                                else
                                {
                                    dgv.Rows[3].Cells[z + 1].ReadOnly = true;
                                    dgv.Rows[3].Cells[z + 1].Style.BackColor = Color.DarkGray;
                                }

                                if (table.Rows[z][3].ToString() == "MF")
                                {
                                    dgv.Rows[3].Cells[z + 1].ReadOnly = false;
                                    dgv.Rows[3].Cells[z + 1].Style.BackColor = Color.White;

                                    dgv.Rows[3].Cells[z + 1].Value = cs_notas.media_final_idatend(Convert.ToInt32(table.Rows[z][0]));
                                }

                                ///MÉDIA - HIST_ALUNO_
                                //if (table.Rows[z][3].ToString() == "MF")
                                //{
                                //    dgv.Rows[3].Cells[i + 1].ReadOnly = false;
                                //    dgv.Rows[3].Cells[i + 1].Style.BackColor = Color.White;

                                //    dgv.Rows[3].Cells[i + 1].Value = cs_notas.media_final(n_mat, cs_alunos.ensino(n_mat), Usuarios_Grupos.csUsuario_logado.mat_atual);
                                //}

                                //_____________

                                //User lançamento
                                dgv.Rows[4].Cells[z + 1].Value = table.Rows[z][4];
                                //_____________

                                //Data_atendimento
                                dgv.Rows[5].Cells[z + 1].Value = table.Rows[z][2];
                                //_____________

                                //atrib_pori
                                if (cs_notas.id_usu_lanc(Convert.ToInt32(table.Rows[z][0])) != 0)
                                {
                                    dgv.Rows[6].Cells[z + 1].Value = cs_notas.id_usu_lanc(Convert.ToInt32(table.Rows[z][0]));
                                }
                                //alter_por
                                if (cs_notas.id_usu_mod(Convert.ToInt32(table.Rows[z][0])) != 0)
                                {
                                    dgv.Rows[7].Cells[z + 1].Value = cs_notas.id_usu_mod(Convert.ToInt32(table.Rows[z][0]));
                                }

                                //data_inserido
                                if (cs_notas.dat_lanc_nota(Convert.ToInt32(table.Rows[z][0])) != "")
                                {
                                    dgv.Rows[8].Cells[z + 1].Value = cs_notas.dat_lanc_nota(Convert.ToInt32(table.Rows[z][0]));
                                }

                                //data_modificado
                                if (cs_notas.dat_mod_nota(Convert.ToInt32(table.Rows[z][0])) != "")
                                {
                                    dgv.Rows[9].Cells[z + 1].Value = cs_notas.dat_mod_nota(Convert.ToInt32(table.Rows[z][0]));
                                }
                            }

                            if ((dgv.Rows[1].Cells[dgv.ColumnCount - 1].Value.ToString() != "ENCERRADO") && (dgv.Rows[1].Cells[dgv.ColumnCount - 1].Value.ToString() != "MÉDIA FINAL"))
                            {
                                //Colls para inserir nota
                                dgv.Columns.Add("col_btadd", "");
                                dgv.Columns[dgv.Columns.Count - 1].Width = 50;

                                //Modulo
                                dgv.Rows[2].Cells[dgv.Columns.Count - 1].ReadOnly = true;

                                //Nota

                                dgv.Rows[3].Cells[dgv.Columns.Count - 1].ReadOnly = true;

                                DataGridViewComboBoxCell cmb_novo = new DataGridViewComboBoxCell();

                                cmb_novo.DataSource = cs_tipoatendimento.lista_de_tipos(disciplinas_para_criar_completo[i]);

                                dgv.Rows[1].Cells[dgv.Columns.Count - 1] = cmb_novo;
                            }

                            //Reativa eventos depois da montagem.
                            dgv.CellEnter += dgvAtendimentos_dis_area_CellEnter;

                            //Verifica scroolbar

                            foreach (var scroll in dgv.Controls.OfType<HScrollBar>())
                            {
                                if (scroll.Visible)
                                {
                                    //SCROLL HORIZONTAL                                    
                                    dgv.Size = new Size(764, 87);
                                    dgv.Parent.Size = new Size(764, 120);
                                }
                                else
                                {
                                    //SEM SCROLL HORIZONTAL
                                    dgv.Size = new Size(764, 70);
                                    dgv.Parent.Size = new Size(764, 104);
                                }
                            }


                            #endregion
                        }
                    }
                }
            }
        }

        #region //Desenha Retangulos atras dos textbox

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gObject = panel1.CreateGraphics();

            Brush white = new SolidBrush(Color.White);
            Pen redPen = new Pen(white, 8);

            gObject.FillRectangle(white, 0, 0, 180, 19); //n-mat
            gObject.FillRectangle(white, 190, 0, 180, 19); //rg
            gObject.FillRectangle(white, 380, 0, 228, 19); //termo

            gObject.FillRectangle(white, 0, 31, 704, 50);            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics gObject = panel2.CreateGraphics();
            Brush white = new SolidBrush(Color.White);
            gObject.FillRectangle(white, 0, 0, 810, 26);            
        }

        #endregion

        #region Add, modificação e exclução no dgv.

        //Alterado
        private void dgvAtendimentos_dis_area_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv_ = (DataGridView)sender;

            if (dgv_.CurrentCell.Value != null)
            {
                valor_dgv_anterior = dgv_.CurrentCell.Value.ToString();
            }
            else valor_dgv_anterior = "";

            //Ao entrar no dgv, repassar valor do dtp_disciplina ao paramentro orient_ini_disc_selecionada.

            string nome_disciplina = dgv_.Name.Replace("dgv_", "");

            foreach (FlowLayoutPanel flp in dgv_.Parent.Controls.OfType<FlowLayoutPanel>())
            {
                foreach (DateTimePicker dtp in flp.Controls.OfType<DateTimePicker>())
                {
                    orient_ini_disc_selecionada = dtp.Value;
                }
            }

            //MessageBox.Show(orient_ini_disc_selecionada.ToString("dd/MM/yyyy"));
        }

        //Alterado ?
        private void dgvAtendimentos_dis_area_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv_ = (DataGridView)sender;

            int id_disciplina_dgv_atual = cs_disciplinas.troca_disciplina_nome_por_id(dgv_.Name.Replace("dgv_", ""));

            //verifica se esta na linha de combobox
            if (dgv_.CurrentCell.RowIndex == 1)
            {
                //
                //if(dgv_.CurrentCell.ColumnIndex == dgv_.Columns.Count-1)
                //{
                if (dgv_.Rows[linha_atual(dgv_)].Cells[coluna_atual(dgv_)].Value != null)
                {
                    string valor_campo = dgv_.Rows[1].Cells[coluna_atual(dgv_)].Value.ToString();
                    //Testar o tipo de atendimento para definir se existe menção, caso não exista, bloqueia o campo.
                    if (cs_tipoatendimento.ha_mencao(cs_tipoatendimento.troca_nome_tipoatend_id(valor_campo, id_disciplina_dgv_atual)) == true)
                    {
                        //desbloquia campo mod
                        dgv_.Rows[2].Cells[coluna_atual(dgv_)].ReadOnly = false;
                        dgv_.Rows[2].Cells[coluna_atual(dgv_)].Value = "";

                        //desbloqueia campo nota
                        dgv_.Rows[3].Cells[coluna_atual(dgv_)].ReadOnly = false;
                        dgv_.Rows[3].Cells[coluna_atual(dgv_)].Value = "";
                        dgv_.Rows[3].Cells[coluna_atual(dgv_)].Style.BackColor = Color.White;
                    }
                    else
                    {
                        //bloqueia campo nota                        
                        dgv_.Rows[3].Cells[coluna_atual(dgv_)].ReadOnly = true;
                        if (dgv_.Rows[3].Cells[coluna_atual(dgv_)].Value != null)
                        {
                            dgv_.Rows[3].Cells[coluna_atual(dgv_)].Value = "";
                            dgv_.Rows[3].Cells[coluna_atual(dgv_)].Style.BackColor = Color.DarkGray;
                            cs_notas.excluiNota(Convert.ToInt32(dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value));
                        }

                        //desbloquia campo mod
                        dgv_.Rows[2].Cells[coluna_atual(dgv_)].ReadOnly = false;
                        dgv_.Rows[2].Cells[coluna_atual(dgv_)].Value = "";
                    }

                    if (dgv_.Rows[linha_atual(dgv_)].Cells[coluna_atual(dgv_)].Value.ToString() == "ENCERRADO" || dgv_.Rows[linha_atual(dgv_)].Cells[coluna_atual(dgv_)].Value.ToString() == "MÉDIA FINAL")
                    {
                        //desbloquia campo mod
                        dgv_.Rows[2].Cells[coluna_atual(dgv_)].ReadOnly = true;
                        dgv_.Rows[2].Cells[coluna_atual(dgv_)].Value = "MF";

                        //desbloqueia campo nota
                        dgv_.Rows[3].Cells[coluna_atual(dgv_)].ReadOnly = false;
                        dgv_.Rows[3].Cells[coluna_atual(dgv_)].Value = "";

                    }

                    if(Configurações.csEscola.cidade.ToLower() == "americana")
                    {
                        if (dgv_.Rows[linha_atual(dgv_)].Cells[coluna_atual(dgv_)].Value.ToString() == "ORIENTAÇÃO")
                        {
                            //verificar se já existe um atendimento na data de hoje

                            List<csAtendimentos> list_ = cs_atendimentos.lista_atendimentos(0, cs_tipoatendimento.troca_nome_tipoatend_id("ORIENTAÇÃO", id_disciplina_dgv_atual), n_mat, id_ensino_selecionado);

                            for (int i = 0; i < list_.Count; i++)
                            {
                                if (DateTime.Parse(list_[i].data_atendimento).ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                                {
                                    MessageBox.Show("Orientação já atribuída na data de hoje.");


                                    this.BeginInvoke(new MethodInvoker(() =>
                                    {
                                        preencher_dgvAtendimentos_dis_area();
                                    }));
                                    preencher_dgvAtendimentos_completo();

                                    return;


                                }
                            }
                        }
                    }

                    

                    if (dgv_.Rows[linha_atual(dgv_)].Cells[coluna_atual(dgv_)].Value.ToString() == "ORIENTAÇÃO INICIAL")
                    {
                        //desbloquia campo mod
                        dgv_.Rows[2].Cells[coluna_atual(dgv_)].ReadOnly = false;
                        dgv_.Rows[2].Cells[coluna_atual(dgv_)].Value = "";

                        //desbloqueia campo nota
                        dgv_.Rows[3].Cells[coluna_atual(dgv_)].ReadOnly = true;
                        dgv_.Rows[3].Cells[coluna_atual(dgv_)].Value = "";
                    }

                    if (dgv_.Rows[linha_atual(dgv_)].Cells[coluna_atual(dgv_)].Value.ToString() == "AVALIAÇÃO FINAL")
                    {
                        //desbloquia campo mod
                        dgv_.Rows[2].Cells[coluna_atual(dgv_)].ReadOnly = true;
                        dgv_.Rows[2].Cells[coluna_atual(dgv_)].Value = "AF";

                        //desbloqueia campo nota
                        dgv_.Rows[3].Cells[coluna_atual(dgv_)].ReadOnly = false;
                        dgv_.Rows[3].Cells[coluna_atual(dgv_)].Value = "";
                    }

                    if (dgv_.Rows[linha_atual(dgv_)].Cells[coluna_atual(dgv_)].Value.ToString() == "PROVA FINAL")
                    {
                        //desbloquia campo mod
                        dgv_.Rows[2].Cells[coluna_atual(dgv_)].ReadOnly = true;
                        dgv_.Rows[2].Cells[coluna_atual(dgv_)].Value = "AF";

                        //desbloqueia campo nota
                        dgv_.Rows[3].Cells[coluna_atual(dgv_)].ReadOnly = false;
                        dgv_.Rows[3].Cells[coluna_atual(dgv_)].Value = "";
                    }
                }
                //}
            }
            else
            {
                //verifica se é modulo ou nota

                if (dgv_.CurrentCell.RowIndex == 2)
                {
                    #region Modulo
                    //verifica se NÃO esta vazio
                    if (dgv_.CurrentCell.Value != null)
                    {
                        //obtem o tipo de atendimento selecionado
                        string valor_campo = dgv_.Rows[1].Cells[coluna_atual(dgv_)].Value.ToString();
                        //verifica se o tipo de atendimento selecionado NÃO tem mencao_padrão.
                        if (cs_tipoatendimento.ha_mencao(cs_tipoatendimento.troca_nome_tipoatend_id(valor_campo, id_disciplina_dgv_atual)) == false)
                        {
                            //verifica se é um novo atendimento
                            if (dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value == null)
                            {
                                //Grava
                                cs_atendimentos.adcionarAtendimento(cs_tipoatendimento.troca_nome_tipoatend_id(dgv_.Rows[1].Cells[coluna_atual(dgv_)].Value.ToString(), id_disciplina_dgv_atual),
                                                                                                               DateTime.Now.ToString(),
                                                                                                               n_mat,
                                                                                                               dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(),
                                                                                                               Usuarios_Grupos.csUsuario_logado.id_usuario_logado,
                                                                                                               id_disciplina_dgv_atual,
                                                                                                               id_ensino_selecionado);

                                if (dgv_.Rows[1].Cells[coluna_atual(dgv_)].Value.ToString() == "ORIENTAÇÃO INICIAL")
                                {
                                    //dtp_orient_ini.Value = DateTime.Now;
                                    //cs_notas.add_orient_ini(n_mat, cs_alunos.ensino(n_mat), cs_disciplinas.troca_disciplina_id_por_nome(id_disciplina_dgv_atual), DateTime.Now.ToString());
                                }

                                this.BeginInvoke(new MethodInvoker(() =>
                                {
                                    preencher_dgvAtendimentos_dis_area();
                                }));
                                preencher_dgvAtendimentos_completo();
                            }
                            else
                            {
                                //Modifica
                                cs_atendimentos.modificaAtendimento(Convert.ToInt32(dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value),
                                                                                    cs_tipoatendimento.troca_nome_tipoatend_id(dgv_.Rows[1].Cells[coluna_atual(dgv_)].Value.ToString(), id_disciplina_dgv_atual),
                                                                                    DateTime.Now.ToString(),
                                                                                    dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(),
                                                                                    Usuarios_Grupos.csUsuario_logado.id_usuario_logado,
                                                                                    n_mat,
                                                                                    id_ensino_selecionado);

                                if (dgv_.Rows[1].Cells[coluna_atual(dgv_)].Value.ToString() == "ORIENTAÇÃO INICIAL")
                                {
                                    //dtp_orient_ini.Value = DateTime.Now;
                                    //cs_notas.add_orient_ini(n_mat, cs_alunos.ensino(n_mat), cs_disciplinas.troca_disciplina_id_por_nome(id_disciplina_dgv_atual), DateTime.Now.ToString());
                                }

                                this.BeginInvoke(new MethodInvoker(() =>
                                {
                                    preencher_dgvAtendimentos_dis_area();
                                }));
                                //fill_dgvAtendimentos_com();   
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region Nota
                    //verifica se campo modulo NÃO esta vazio
                    if (dgv_.Rows[2].Cells[coluna_atual(dgv_)].Value != null)
                    {
                        //verifica se campo nota NÃO esta vazio
                        if (dgv_.CurrentCell.Value != null)
                        {
                            #region Média
                            //verifica se é média final
                            if (dgv_.Rows[2].Cells[coluna_atual(dgv_)].Value.ToString() == "MF")
                            {
                                //Verificar nota - maior que o permitido
                                if (float.Parse(dgv_.Rows[3].Cells[coluna_atual(dgv_)].Value.ToString()) < 11)
                                {
                                    //verifica se NÃO há um número na linha de id_atendimento.Caso não haja, é um novo registro
                                    if (dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value == null)
                                    {
                                        ////Calular média automático
                                        //
                                        ////lista de notas
                                        //
                                        //for(int i = 0; i < dgv_.Columns.Count; i++)
                                        //{
                                        //    
                                        //    
                                        //}
                                        //
                                        //string media_auto = cs_notas.calc_media_auto(id_disciplina_dgv_atual, id_ensino_selecionado);

                                        //Grava Atendimento
                                        cs_atendimentos.adcionarAtendimento(cs_tipoatendimento.troca_nome_tipoatend_id(dgv_.Rows[1].Cells[coluna_atual(dgv_)].Value.ToString(),
                                                                                                                       id_disciplina_dgv_atual),
                                                                                                                       DateTime.Now.ToString(),
                                                                                                                       n_mat,
                                                                                                                       dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(),
                                                                                                                       Usuarios_Grupos.csUsuario_logado.id_usuario_logado,
                                                                                                                       id_disciplina_dgv_atual,
                                                                                                                       id_ensino_selecionado);
                                        //Grava Média
                                        /////////
                                        cs_notas.add_media_final_NOVO(cs_atendimentos.id_ultimo_atendimento(n_mat, id_ensino_selecionado),
                                            id_disciplina_dgv_atual,
                                            n_mat,
                                            id_ensino_selecionado,
                                            dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(),
                                            orient_ini_disc_selecionada.ToString("dd/MM/yyyy"),
                                            Usuarios_Grupos.csUsuario_logado.id_usuario_logado);


                                        //Nota
                                        //cs_notas.adcionaNota(cs_atendimentos.id_ultimo_atendimento(n_mat),
                                        //                         float.Parse(dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString()),
                                        //                         Usuarios_Grupos.csUsuario_logado.id_usuario_atual,
                                        //                         DateTime.Now.ToString());

                                        //AtualizaDGV
                                        preencher_dgvAtendimentos_dis_area();
                                        preencher_dgvAtendimentos_completo();

                                        //dgv_.Columns.Remove("col_btadd");

                                        //Atribuir nova Disciplina
                                        atrib_nova_disciplina();                                        
                                    }
                                    else
                                    {
                                        //Modifica
                                        cs_atendimentos.modificaAtendimento(Convert.ToInt32(dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value),
                                                                                        cs_tipoatendimento.troca_nome_tipoatend_id(dgv_.Rows[1].Cells[coluna_atual(dgv_)].Value.ToString(), id_disciplina_dgv_atual),
                                                                                        DateTime.Now.ToString(),
                                                                                        dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(),
                                                                                        Usuarios_Grupos.csUsuario_logado.id_usuario_logado,
                                                                                        n_mat,
                                                                                        id_ensino_selecionado);

                                        if (cs_notas.existe_media(Convert.ToInt32(dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value)))
                                        {
                                            //Mod
                                            //Média Final                                            
                                            cs_notas.upd_media_final(Configurações.csEscola.escola,
                                                Configurações.csEscola.cidade,
                                                Configurações.csEscola.sigla_estado,
                                                dgv_.Rows[3].Cells[coluna_atual(dgv_)].Value.ToString(),
                                                DateTime.Now.ToString("dd/MM/yyyy"),
                                                Usuarios_Grupos.csUsuario_logado.id_usuario_logado);

                                            //cs_notas.add_media_final(n_mat,
                                            //    cs_alunos.ensino(n_mat),
                                            //    Usuarios_Grupos.csUsuario_logado.mat_atual,
                                            //    float.Parse(dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString()),
                                            //    Usuarios_Grupos.csUsuario_logado.nome_usuario_logado);

                                            //AtualizaDGV
                                            preencher_dgvAtendimentos_dis_area();
                                            preencher_dgvAtendimentos_completo();

                                            //dgv_.Columns.Remove("col_btadd");

                                            //Nova Disciplina
                                            atrib_nova_disciplina();
                                        }
                                        else
                                        {
                                            //Add - caso seja um sem menção para um que tenha
                                            //Média Final
                                            cs_notas.add_media_final_NOVO(cs_atendimentos.id_ultimo_atendimento(n_mat, id_ensino),
                                            id_disc,
                                            n_mat,
                                            cs_disciplinas.troca_ensino_nome_por_id(cs_alunos.ensino(n_mat)),
                                            dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(),
                                            orient_ini_disc_selecionada.ToString("dd/MM/yyyy"),
                                            Usuarios_Grupos.csUsuario_logado.id_usuario_logado);

                                            //AtualizaDGV
                                            preencher_dgvAtendimentos_dis_area();
                                            preencher_dgvAtendimentos_completo();

                                            //dgv_.Columns.Remove("col_btadd");

                                            //Nova Disciplina
                                            atrib_nova_disciplina();
                                           
                                        }
                                        preencher_dgvAtendimentos_dis_area();
                                        preencher_dgvAtendimentos_completo();
                                    }
                                }
                            }

                            #endregion

                            else
                            {
                                //Verificar nota - maior que o permitido
                                if (float.Parse(dgv_.Rows[3].Cells[coluna_atual(dgv_)].Value.ToString()) < 11)
                                {
                                    //verifica se NÃO há um número na linha de id_atendimento.Caso não haja, é um novo registro
                                    if (dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value == null)
                                    {
                                        //Grava Atendimento
                                        cs_atendimentos.adcionarAtendimento(cs_tipoatendimento.troca_nome_tipoatend_id(dgv_.Rows[1].Cells[coluna_atual(dgv_)].Value.ToString(), id_disciplina_dgv_atual),
                                                                                                                       DateTime.Now.ToString(),
                                                                                                                       n_mat,
                                                                                                                       dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(),
                                                                                                                       Usuarios_Grupos.csUsuario_logado.id_usuario_logado,
                                                                                                                       id_disciplina_dgv_atual,
                                                                                                                       id_ensino_selecionado);
                                        //Grava nota usando o ultimo id_atendimento criado para o n_mat e Ensino
                                        cs_notas.adcionaNota(cs_atendimentos.id_ultimo_atendimento(n_mat, id_ensino_selecionado),
                                                                 float.Parse(dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString()),
                                                                 Usuarios_Grupos.csUsuario_logado.id_usuario_logado,
                                                                 DateTime.Now.ToString());

                                        this.BeginInvoke(new MethodInvoker(() =>
                                        {
                                            preencher_dgvAtendimentos_dis_area();
                                        }));
                                        preencher_dgvAtendimentos_completo();
                                    }
                                    else
                                    {
                                        //Modifica

                                        cs_atendimentos.modificaAtendimento(Convert.ToInt32(dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value),
                                                                                        cs_tipoatendimento.troca_nome_tipoatend_id(dgv_.Rows[1].Cells[coluna_atual(dgv_)].Value.ToString(), id_disciplina_dgv_atual),
                                                                                        DateTime.Now.ToString(),
                                                                                        dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(),
                                                                                        Usuarios_Grupos.csUsuario_logado.id_usuario_logado,
                                                                                        n_mat,
                                                                                        id_ensino_selecionado);

                                        if (cs_notas.existe_nota(Convert.ToInt32(dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value)))
                                        {
                                            //Mod
                                            cs_notas.modificaNota(Convert.ToInt32(dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value),
                                            float.Parse(dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString()),
                                            Usuarios_Grupos.csUsuario_logado.id_usuario_logado,
                                            DateTime.Now.ToString());
                                        }
                                        else
                                        {
                                            //Grava nota usando o valor real da celular 'id_atendimento" 
                                            //Add - caso seja um sem menção para um que tenha
                                            cs_notas.adcionaNota(Convert.ToInt32(dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value),
                                                                 float.Parse(dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString()),
                                                                 Usuarios_Grupos.csUsuario_logado.id_usuario_logado,
                                                                 DateTime.Now.ToString());
                                        }
                                        this.BeginInvoke(new MethodInvoker(() =>
                                        {
                                            preencher_dgvAtendimentos_dis_area();
                                        }));
                                        preencher_dgvAtendimentos_completo();
                                    }

                                }
                                //Caso seja maior que permitido, zera o campo
                                else
                                {
                                    dgv_.Rows[linha_atual(dgv_)].Cells[coluna_atual(dgv_)].Value = "";
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
        }

        public int linha_atual(DataGridView sender_)
        {
            return sender_.CurrentRow.Index;
        }

        public int coluna_atual(DataGridView sender_)
        {
            return sender_.CurrentCell.ColumnIndex;
        }

        #endregion

        #region Informaçãoes no toolstatusstrip

        //Alterado
        private void dgvAtendimentos_dis_area_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv_ = (DataGridView)sender;

            if (e.ColumnIndex != 0)
            {
                //Verifica quem atribuiu
                if (dgv_.Rows[4].Cells[e.ColumnIndex].Value != null)
                {
                    //verifica quando atribuiu
                    if (dgv_.Rows[5].Cells[e.ColumnIndex].Value != null)
                    {
                        //Verifica se tem NOTA e quem atribuiu
                        if (dgv_.Rows[6].Cells[e.ColumnIndex].Value != null)
                        {
                            //verifica quando atribuiu
                            if (dgv_.Rows[8].Cells[e.ColumnIndex].Value != null)
                            {
                                //Verifica se tem NOTA MODIFICADA e quem modificou
                                #region MODIFICADA
                                if (dgv_.Rows[7].Cells[e.ColumnIndex].Value != null)
                                {
                                    //verifica quando atribuiu
                                    if (dgv_.Rows[9].Cells[e.ColumnIndex].Value != null)
                                    {
                                        string nome_1 = cs_usuarios.troca_id_por_nome(Convert.ToInt32(dgv_.Rows[6].Cells[e.ColumnIndex].Value));
                                        string data_lanc = dgv_.Rows[8].Cells[e.ColumnIndex].Value.ToString();
                                        string nome_2 = cs_usuarios.troca_id_por_nome(Convert.ToInt32(dgv_.Rows[7].Cells[e.ColumnIndex].Value));
                                        string data_alter = dgv_.Rows[9].Cells[e.ColumnIndex].Value.ToString();
                                        string tip_atend = dgv_.Rows[1].Cells[e.ColumnIndex].Value.ToString();

                                        //Reduzir nome
                                        string[] lanc_por = nome_1.Split(' ');

                                        string[] alter_por = nome_2.Split(' ');

                                        if (lanc_por.Count() > 1)
                                        {
                                            tssl1.Text = "Lançado por " + lanc_por[0] + " " + lanc_por[1] + " em " + data_lanc +
                                                     " - Alterado por " + alter_por[0] + " " + alter_por[1] + " em " + data_alter;

                                            ttTipo_atendimento.SetToolTip(dgv_, tip_atend);
                                        }
                                        else
                                        {
                                            tssl1.Text = "Lançado por " + lanc_por[0] + " em " + data_lanc +
                                                     " - Alterado por " + alter_por[0] + " em " + data_alter;

                                            ttTipo_atendimento.SetToolTip(dgv_, tip_atend);
                                        }
                                    }
                                }
                                #endregion
                                else
                                {
                                    string nome_1 = cs_usuarios.troca_id_por_nome(Convert.ToInt32(dgv_.Rows[6].Cells[e.ColumnIndex].Value));
                                    string data_lanc = dgv_.Rows[8].Cells[e.ColumnIndex].Value.ToString();
                                    string tip_atend = dgv_.Rows[1].Cells[e.ColumnIndex].Value.ToString();

                                    string[] lanc_por = nome_1.Split(' ');

                                    if (lanc_por.Count() > 1)
                                    {
                                        tssl1.Text = "Lançado por " + lanc_por[0] + " " + lanc_por[1] + " em " + data_lanc;
                                        ttTipo_atendimento.SetToolTip(dgv_, tip_atend);
                                    }
                                    else
                                    {
                                        tssl1.Text = "Lançado por " + lanc_por[0] + " em " + data_lanc;
                                        ttTipo_atendimento.SetToolTip(dgv_, tip_atend);
                                    }
                                }

                            }
                        }
                        else
                        {
                            string nome_1 = cs_usuarios.troca_id_por_nome(Convert.ToInt32(dgv_.Rows[4].Cells[e.ColumnIndex].Value));
                            string data_atendido = dgv_.Rows[5].Cells[e.ColumnIndex].Value.ToString();
                            string tip_atend = dgv_.Rows[1].Cells[e.ColumnIndex].Value.ToString();

                            string[] atendido_por = nome_1.Split(' ');

                            if (atendido_por.Count() > 1)
                            {
                                tssl1.Text = "Atendido por " + atendido_por[0] + " " + atendido_por[1] + " em " + data_atendido;
                                ttTipo_atendimento.SetToolTip(dgv_, tip_atend);
                            }
                            else
                            {
                                tssl1.Text = "Atendido por " + atendido_por[0] + " em " + data_atendido;
                                ttTipo_atendimento.SetToolTip(dgv_, tip_atend);
                            }

                        }
                    }
                }
            }
            else
            {
                tssl1.Text = "";
                ttTipo_atendimento.Hide(dgv_);

            }
        }
        //Alterado
        private void dgvAtendimentos_dis_area_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            tssl1.Text = "";
        }


        private void dgvAtendimentos_com_MouseLeave(object sender, EventArgs e)
        {
            tssl1.Text = "";
        }

        private void dgvAtendimentos_com_1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            string a = ((DataGridView)sender).Name;

            ((DataGridView)sender).Focus();

            foreach (DataGridView dgv in tabPage2.Controls.OfType<DataGridView>())
            {
                if (dgv.Name == a)
                {
                    if (e.ColumnIndex != 0)
                    {
                        //Verifica quem atribuiu
                        if (dgv.Rows[4].Cells[e.ColumnIndex].Value != null)
                        {
                            //verifica quando atribuiu
                            if (dgv.Rows[5].Cells[e.ColumnIndex].Value != null)
                            {
                                //Verifica se tem NOTA e quem atribuiu
                                if (dgv.Rows[6].Cells[e.ColumnIndex].Value != null)
                                {
                                    //verifica quando atribuiu
                                    if (dgv.Rows[8].Cells[e.ColumnIndex].Value != null)
                                    {
                                        //Verifica se tem NOTA MODIFICADA e quem modificou
                                        #region MODIFICADA
                                        if (dgv.Rows[7].Cells[e.ColumnIndex].Value != null)
                                        {
                                            //verifica quando atribuiu
                                            if (dgv.Rows[9].Cells[e.ColumnIndex].Value != null)
                                            {
                                                string nome_1 = cs_usuarios.troca_id_por_nome(Convert.ToInt32(dgv.Rows[6].Cells[e.ColumnIndex].Value));
                                                string data_lanc = dgv.Rows[8].Cells[e.ColumnIndex].Value.ToString();
                                                string nome_2 = cs_usuarios.troca_id_por_nome(Convert.ToInt32(dgv.Rows[7].Cells[e.ColumnIndex].Value));
                                                string data_alter = dgv.Rows[9].Cells[e.ColumnIndex].Value.ToString();

                                                //Reduzir nome
                                                string[] lanc_por = nome_1.Split(' ');

                                                string[] alter_por = nome_2.Split(' ');

                                                if (lanc_por.Count() > 1)
                                                {
                                                    tssl1.Text = "Lançado por " + lanc_por[0] + " " + lanc_por[1] + " em " + data_lanc +
                                                             " - Alterado por " + alter_por[0] + " " + alter_por[1] + " em " + data_alter;
                                                }
                                                else
                                                {
                                                    tssl1.Text = "Lançado por " + lanc_por[0] + " em " + data_lanc +
                                                             " - Alterado por " + alter_por[0] + " em " + data_alter;
                                                }
                                            }
                                        }
                                        #endregion
                                        else
                                        {
                                            string nome_1 = cs_usuarios.troca_id_por_nome(Convert.ToInt32(dgv.Rows[6].Cells[e.ColumnIndex].Value));
                                            string data_lanc = dgv.Rows[8].Cells[e.ColumnIndex].Value.ToString();

                                            string[] lanc_por = nome_1.Split(' ');

                                            if (lanc_por.Count() > 1)
                                            {
                                                tssl1.Text = "Lançado por " + lanc_por[0] + " " + lanc_por[1] + " em " + data_lanc;
                                            }
                                            else
                                            {
                                                tssl1.Text = "Lançado por " + lanc_por[0] + " em " + data_lanc;
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    string nome_1 = cs_usuarios.troca_id_por_nome(Convert.ToInt32(dgv.Rows[4].Cells[e.ColumnIndex].Value));
                                    string data_atendido = dgv.Rows[5].Cells[e.ColumnIndex].Value.ToString();

                                    string[] atendido_por = nome_1.Split(' ');

                                    if (atendido_por.Count() > 1)
                                    {
                                        tssl1.Text = "Atendido por " + atendido_por[0] + " " + atendido_por[1] + " em " + data_atendido;
                                    }
                                    else
                                    {
                                        tssl1.Text = "Atendido por " + atendido_por + " em " + data_atendido;
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        tssl1.Text = "";
                    }
                }
            }
        }



        #endregion

        public void atrib_nova_disciplina()
        {
            Alunos.CSaluno.numat = n_mat;
            List<Alunos.csAlunos> list_ = cs_alunos.dados_aluno(n_mat);
            Alunos.CSaluno.id_disciplina_atual = list_[0].id_disciplina_atual;
            Alunos.CSaluno.nome_aluno = list_[0].nome;
            Alunos.CSaluno.rg_aluno = list_[0].rg;
            Alunos.CSaluno.ensino_aluno = list_[0].ensino(n_mat);

            if (MessageBox.Show("Atribuir nova disciplina ao Aluno ?", "Disciplina encerrada", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (Configurações.csEscola.cidade.ToLower() == "americana")
                {
                    frAtribDisciplina form = new frAtribDisciplina();
                    form.n_mat = n_mat;
                    form.MdiParent = this.ParentForm;
                    form.Show();
                    this.Close();

                }
                else
                {
                    Notas.frAtrib_materia form = new Notas.frAtrib_materia();
                    form.MdiParent = this.ParentForm;
                    form.Show();
                    this.Close();
                }

            }
        }

        //Alterado porem não aparenta nescessario
        private void dgvAtendimentos_dis_area_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv_ = (DataGridView)sender;

            string a = dgv_.Name;

            //dtp_orient_ini.Enabled = true;
            //dtp_orient_ini.Visible = true;
            //
            //lblOrient_ini.Visible = true;
            //lblOrient_ini.Text = "Orient. Inicial - " + dgv_.Name.Replace("dgv_","") + ":";

            //if(dgv_.CurrentRow.Index == 2)
            //{
            //    //Verificar solução melhor
            //    Cursor.Hide();
            //}
        }
        //Alterado
        private void dgvAtendimentos_dis_area_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv_ = (DataGridView)sender;

            e.Control.KeyPress -= new KeyPressEventHandler(row1_KeyPress);
            if (dgv_.CurrentCell.RowIndex == 3) //Desired row
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(row1_KeyPress);
                }
            }
        }
        //Alterado
        private void excluirAtendimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView dgv_ = (DataGridView)sender_mouse_right_dgv;

            if (dgv_.CurrentCell.ColumnIndex != 0)
            {
                if (dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value != null)
                {
                    cs_atendimentos.excluirAtendimento(Convert.ToInt32(dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value));

                    cs_notas.excluiNota(Convert.ToInt32(dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value));

                    if (dgv_.Rows[2].Cells[coluna_atual(dgv_)].Value.ToString() == "MF")
                    {
                        cs_notas.excluir_media_id_atendimento(Convert.ToInt32(dgv_.Rows[0].Cells[coluna_atual(dgv_)].Value));
                        cs_notas.excluir_media_por_id_ensino_id_disciplina(n_mat, id_ensino, cs_disciplinas.troca_area_nome_por_id(dgv_.Name.Replace("dgv_", "")));
                    }

                    preencher_dgvAtendimentos_dis_area();
                    //fill_dgvAtendimentos_com();
                }
            }


        }
        //Alterado
        private void dgvAtendimentos_dis_area_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)    
        {
            DataGridView dgv_ = (DataGridView)sender;

            sender_mouse_right_dgv = dgv_;

            if (e.Button == MouseButtons.Right)
            {
                dgv_.CurrentCell = dgv_.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dgv_.Rows[1].Cells[e.ColumnIndex].Selected = true;
                dgv_.Focus();
            }
        }
        //ok
        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Alunos.frPesquisaaluno form = new Alunos.frPesquisaaluno();
            form.MdiParent = this.ParentForm;
            //form.Owner = this;
            form.tipo_form = "passaporte";
            form.Show();
            Cursor.Current = Cursors.Default;

            this.Close();
        }
        //ok
        private void txtObs_Leave(object sender, EventArgs e)
        {
            cs_alunos.upt_obs_passporte(n_mat, txtObs.Text);
        }
        //Alterado
        private void dtp_orient_ini_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp_ = (DateTimePicker)sender;
            int id_disc = cs_disciplinas.troca_disciplina_nome_por_id(dtp_.Name.Replace("dtp_", ""));

            //Verificar se existe orient_inicial na disciplina...
            int id_tipo_atend_orient_ini_da_disc = cs_tipoatendimento.lista_tipos_atendimentos(id_disc, "ORIENTAÇÃO INICIAL")[0].id_tipo_atendimento;

            List<int> list_ids_tipos_atendimentos_do_aluno = new List<int>();

            if (cs_atendimentos.lista_atendimentos(0, id_tipo_atend_orient_ini_da_disc, n_mat, id_ensino_selecionado).Count>0)
            {
                //Existe Orientação inicial - Atualizar Atendimento ?
                dtp_.Enabled = false;
            }
            else
            {
                ///Não Existe Orientação Incial - Criar e Atualizar DGV...
                //Verificar se data dtp_é inferior ao primeiro atendimento desta disciplina...
                
                foreach(DataGridView dgv_ in dtp_.Parent.Parent.Controls.OfType<DataGridView>())
                {
                    if(dgv_.Columns.Count > 1)
                    {
                        if(dtp_.Value <  DateTime.Parse(dgv_.Rows[5].Cells[1].Value.ToString()))
                        {
                            cs_atendimentos.adcionarAtendimento(id_tipo_atend_orient_ini_da_disc,
                                dtp_.Value.ToString("dd/MM/yyyy"),
                                n_mat,
                                "",
                                Usuarios_Grupos.csUsuario_logado.id_usuario_logado,
                                id_disc,
                                id_ensino_selecionado);
                            preencher_dgvAtendimentos_dis_area();

                        }
                        else
                        {
                            MessageBox.Show("Data selecionada é inferior ao primeiro atendimento registrado para esta disciplina.");
                        }
                    }
                    
                }

                

                
            }
            

         
        }

        private void frPassaporte_Shown(object sender, EventArgs e)
        {
            if (!cs_alunos.esta_ativo(n_mat))
            {
                MessageBox.Show("Aluno Inativo, Comparecer a Secretaria.", "Inativo");

                foreach (TableLayoutPanel tlp in flpPassaporteIndividual.Controls.OfType<TableLayoutPanel>())
                {
                    foreach (DataGridView dgv in tlp.Controls.OfType<DataGridView>())
                    {
                        dgv.Enabled = false;
                    }
                }                
            }
        }

        public void cod()
        {
            if (cs_alunos.ensino(n_mat) == "")
            {

            }
            else
            {
                //Seleciona Ensino conforme atual de aluno
                sel_ensino = cs_alunos.ensino(n_mat);

                //Defini no LOAD o ensino do aluno
                if (sel_ensino == "FUNDAMENTAL")
                {
                    id_ensino_selecionado = 0;
                }
                else
                {
                    id_ensino_selecionado = 1;
                }

                this.Location = new Point(87, 20);

                this.Size = new Size(850, 374);

                tbcPassaporte.Size = new Size(806, 137);

                //dados label form
                lblNome.Text = "NOME: " + cs_alunos.troca_n_mat_nome(n_mat);

                lblRg.Text = "RG: " + cs_alunos.rg__(n_mat);

                lblTermo.Text = "TERMO: " + cs_alunos.termo(n_mat);

                lblNmat.Text = "MATRÍCULA: " + n_mat;

                this.Text = n_mat + " - Passporte Aluno";

                lblDisciplinaAtual.Text = "MATÉRIA ATUAL: " + cs_disciplinas.troca_disciplina_id_por_nome(cs_alunos.id_disciplina_atual__(n_mat));
                id_disc = Usuarios_Grupos.csUsuario_logado.id_disciplina_logado;
                id_ensino = cs_disciplinas.troca_ensino_nome_por_id(cs_alunos.ensino(n_mat));

                //Dadosdgv
                //fill_dgvAtendimentos_ind();

                preencher_dgvAtendimentos_completo();

                if (cs_usuarios.id_disciplina(Usuarios_Grupos.csUsuario_logado.id_usuario_logado) != 0)
                {
                    //ORIENT_FINAL - TAB_INDIVIDUAL
                    //if (cs_notas.orient_ini(n_mat, cs_alunos.ensino(n_mat), cs_disciplinas.troca_disciplina_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_usuario_logado)) != "")
                    //{
                    //    //dtp_orient_ini.Value = DateTime.Parse(cs_notas.orient_ini(n_mat, cs_alunos.ensino(n_mat), cs_disciplinas.troca_disciplina_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_usuario_logado)));
                    //}
                    btnTroca_ensino.Hide();
                    btnTroca_ensino.Enabled = false;
                }
                else
                {
                    tbcPassaporte.SelectedIndex = 1;
                    //dtp_orient_ini.Hide();
                    //dtp_orient_ini.Enabled = false;
                    //lblOrient_ini.Hide();

                    btnTroca_ensino.Show();
                    btnTroca_ensino.Enabled = true;
                }

                //OBS
                txtObs.Text = cs_alunos.obs_passaporte__(n_mat);

                //Foto
                picFoto.Image = cs_alunos.foto_aluno(n_mat);

                if (id_ensino_selecionado == 0)
                {
                    this.BackColor = Color.FromArgb(48, 48, 48);
                    lblTitulo.Text = "PASSAPORTE FUNDAMENTAL";

                    //BLOQUEAR TAB para professor
                    if (cs_disciplinas.troca_disciplina_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_usuario_logado) == "FÍSICA" ||
                        cs_disciplinas.troca_disciplina_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_usuario_logado) == "BIOLOGIA" ||
                        cs_disciplinas.troca_disciplina_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_usuario_logado) == "QUÍMICA")
                    {
                        //dgvAtendimentos_ind.Enabled = false;

                        MessageBox.Show("Aluno do Fundamental.", "Ensino");
                    }
                }
                else
                {
                    this.BackColor = Color.FromArgb(19, 91, 133);
                    lblTitulo.Text = "PASSAPORTE MÉDIO";

                    //BLOQUEAR TAB para professor
                    if (cs_disciplinas.troca_disciplina_id_por_nome(Usuarios_Grupos.csUsuario_logado.id_usuario_logado) == "CIÊNCIAS")
                    {
                        //dgvAtendimentos_ind.Enabled = false;

                        MessageBox.Show("Aluno do Médio.", "Ensino");
                    }
                }
            }

            fill_dgvDescricoes();
        }

        private void btnTroca_ensino_Click(object sender, EventArgs e)
        {
            if (id_ensino_selecionado == 1)
            {
                id_ensino_selecionado = 2;                
            }
            else if (id_ensino_selecionado == 2)
            {
                id_ensino_selecionado = 1;                
            }
            //Atendimentos
            this.BeginInvoke(new MethodInvoker(() =>
            {
                criar_nova_disciplina();
                preencher_dgvAtendimentos_dis_area();
                criar_nova_disciplina_completo();
                preencher_dgvAtendimentos_completo();
            }));

            //Visual Passaporte
            alterar_visual_passaporte();
        }

        private void row1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar == ','))
            {
                e.Handled = true;
            }


        }

        //Teste
        private void frPassaporte_Resize(object sender, EventArgs e)
        {
            //if(WindowState ==  FormWindowState.Minimized)
            //{
            //    foreach(Form form in Application.OpenForms)
            //    {
            //        if(form.Name == "frPrincipal")
            //        {
            //            foreach(Control control in form.Controls.OfType<FlowLayoutPanel>())
            //            {
            //                Button newButton = new Button();
            //                newButton.Name = "bt_"+n_mat;
            //                newButton.Width = 100;



            //                newButton.Text = n_mat.ToString() + " - " + primeiro_nome(lbNomeAluno.Text);



            //                control.Controls.Add(newButton);
            //            }
            //        }
            //    }
            //}
        }

        public string primeiro_nome(string nome_completo)
        {
            string a = nome_completo.Replace("NOME: ", "");

            a = a.Remove(a.IndexOf(" "));

            return a;
        }

        //Tab-Page3 - descriçoes        

        public void fill_dgvDescricoes()
        {
            DataTable tb = cs_descriçoes.tab_descricoes();

            for (int i = 0; i < tb.Rows.Count; i++)
            {
                dgvDescricoes.Rows.Add();

                dgvDescricoes.Rows[i].Cells[0].Value = tb.Rows[i][0];
                dgvDescricoes.Rows[i].Cells[2].Value = tb.Rows[i][1];
            }
        }

        private void flpPassaporteIndividual_Resize(object sender, EventArgs e)
        {
            resize_flp_individual();
        }
        public void resize_flp_individual()
        {
            flpPassaporteIndividual.Resize -= flpPassaporteIndividual_Resize;
            //dgv.Resize -=

            foreach (TableLayoutPanel tlp in flpPassaporteIndividual.Controls.OfType<TableLayoutPanel>())
            {
                string a = tlp.Name;

                //TableLayoutPanel
                tlp.Width = flpPassaporteIndividual.Width - 20;

                ///DataGridView
                //Dgv Largura altera por ser fill.

                foreach (DataGridView dgv in tlp.Controls.OfType<DataGridView>())
                {
                    string ee = dgv.Name;

                    foreach (var scroll in dgv.Controls.OfType<HScrollBar>())
                    {
                        if (scroll.Visible)
                        {
                            //SCROLL HORIZONTAL                                    
                            //dgv.Height = new Size(764, 87);   

                            tlp.Height = 120;
                            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));

                            dgv.Height = 70;

                            //tbcPassaporte.Size = new Size(806, 150);
                            //this.Size = new Size(850, 386);
                        }
                        else
                        {
                            //SEM SCROLL HORIZONTAL
                            //dgv.Size = new Size(764, 70);

                            tlp.Height = 104;
                            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));

                            dgv.Height = 60;

                            //tbcPassaporte.Size = new Size(806, 137);
                            //this.Size = new Size(850, 374);
                        }
                    }
                }
            }
            flpPassaporteIndividual.Resize += flpPassaporteIndividual_Resize;
        }
        private void flpPassaporteCompleto_Resize(object sender, EventArgs e)
        {
            resize_flp_completo();
        }
        public void resize_flp_completo()
        {
            flpPassaporteCompleto.Resize -= flpPassaporteCompleto_Resize;
            //dgv.Resize -=

            foreach (TableLayoutPanel tlp in flpPassaporteCompleto.Controls.OfType<TableLayoutPanel>())
            {
                string a = tlp.Name;

                //TableLayoutPanel
                tlp.Width = flpPassaporteCompleto.Width - 20;

                ///DataGridView
                //Dgv Largura altera por ser fill.

                foreach (DataGridView dgv in tlp.Controls.OfType<DataGridView>())
                {
                    string ee = dgv.Name;

                    foreach (var scroll in dgv.Controls.OfType<HScrollBar>())
                    {
                        if (scroll.Visible)
                        {
                            //SCROLL HORIZONTAL                                    
                            //dgv.Height = new Size(764, 87);   

                            tlp.Height = 120;
                            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));

                            dgv.Height = 70;

                            //tbcPassaporte.Size = new Size(806, 150);
                            //this.Size = new Size(850, 386);
                        }
                        else
                        {
                            //SEM SCROLL HORIZONTAL
                            //dgv.Size = new Size(764, 70);

                            tlp.Height = 104;
                            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));

                            dgv.Height = 60;

                            //tbcPassaporte.Size = new Size(806, 137);
                            //this.Size = new Size(850, 374);
                        }
                    }
                }

            }
            flpPassaporteCompleto.Resize += flpPassaporteCompleto_Resize;
        }
    }
}