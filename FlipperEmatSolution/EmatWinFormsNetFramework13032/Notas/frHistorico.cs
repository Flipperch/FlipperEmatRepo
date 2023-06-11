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
    public partial class frHistorico : Form
    {
        //parametros
        public int id_ensino_atual;
        public int id_ensino_selecionado;

        public int qtd_disc_ensino_selecionado;

        public string rg = "";
        public string nmat;

        public string copia;

        public string dat_mat = "";

        public string id_dir_grupo = "";
        public DataTable tab_dir = new DataTable();

        public string id_sec_grupo = "";
        public DataTable tab_sec = new DataTable();

        public DataTable tab_notas = new DataTable();

        SqlQuery consulta = new SqlQuery(Conexoes.GetSqlConnection());
        SqlQuery cad_hists = new SqlQuery(Conexoes.GetSqlConnection());

        Alunos.csAlunos cs_alunos = new Alunos.csAlunos();
        Alunos.csHistoricosSituacoes cs_historicos_situacoes = new Alunos.csHistoricosSituacoes();
        Usuarios_Grupos.csUsuarios cs_usuarios = new Usuarios_Grupos.csUsuarios();
        Notas.csDisciplinas cs_disciplinas = new Notas.csDisciplinas();
        Notas.csNotas cs_notas = new Notas.csNotas();
        Error_Log.csControle_erros cs_erros = new Error_Log.csControle_erros();
        Endereços.csEnderecos cs_enderecos = new Endereços.csEnderecos();
        Relatorios.csRelatorios cs_relatorios = new Relatorios.csRelatorios();
        Notas.csEnsinos cs_ensinos = new csEnsinos();

        public frHistorico()
        {
            InitializeComponent();
            
        }

        private void FRhistorico_Load(object sender, EventArgs e)
        {
            tab_dir.Columns.Add("diretor");
            tab_dir.Columns.Add("rg_diretor");
            get_id_grupo_diretor();

            tab_sec.Columns.Add("diretor");
            tab_sec.Columns.Add("rg_diretor");
            get_id_grupo_secretario();
            
            cmbEnsino.SelectedIndexChanged -= cmbEnsino_SelectedIndexChanged;
            cmbEnsino.DataSource = cs_disciplinas.lista_ensinos(); 
            cmbEnsino.SelectedIndexChanged += cmbEnsino_SelectedIndexChanged;
            cmbEnsino.Enabled = false;

            contextMenuStrip1.Items[0].Text = Configurações.csEscola.escola;
                        
        }
        
        private void frHistorico_Shown(object sender, EventArgs e)
        {
            cmbEnsino.SelectedIndexChanged -= cmbEnsino_SelectedIndexChanged;
            dtpEntEnsino.ValueChanged -= dtpEntEnsino_ValueChanged;
            busca_aluno();
            busca_escola_anterior();

            if (id_ensino_selecionado == 0) groupBox1.Enabled = false;
            else groupBox1.Enabled = true;

            if (txtNmat.Text != "")
            {
                cmbEnsino.Enabled = true;
                verifica_hist();
                fill_medias_hist();

                btImprimir_hist.Enabled = true;
                btImprimir_certi.Enabled = true;
                btImprimir_notas.Enabled = true;
            }
            dtpEntEnsino.ValueChanged += dtpEntEnsino_ValueChanged;
            cmbEnsino.SelectedIndexChanged += cmbEnsino_SelectedIndexChanged;
        }

        private void btPesquisa_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Alunos.frPesquisaaluno form = new Alunos.frPesquisaaluno();
            form.MdiParent = this.ParentForm;
            form.tipo_form = "historico";
            form.Show();
            Cursor.Current = Cursors.Default;

            this.Close();
        }

        public void busca_aluno()
        {
            consulta.Command.Parameters.Clear();
            if(rg != "")
            {
                consulta.Command.CommandText =
                    @"SELECT * FROM ALUNOS WHERE RG = @rg ";
                consulta.Command.Parameters.AddWithValue("@rg", rg);                
            }
            else
            {
                consulta.Command.CommandText =
                    @"SELECT * FROM ALUNOS WHERE N_MAT = @n_mat";
                consulta.Command.Parameters.AddWithValue("@n_mat", nmat);
            }

            try
            {
                consulta.Connection.Open();
                SqlDataReader reader = consulta.Command.ExecuteReader();
                while (reader.Read())
                {
                    txtAluno.Text = reader["ALUNO"].ToString();
                    txtMae.Text = reader["NOME_MAE"].ToString();
                    txtNasc_cidade.Text = reader["NASC_CIDADE"].ToString();
                    txtNasc_estado.Text = reader["NASC_ESTADO"].ToString();
                    txtNasc_pais.Text = reader["NASC_PAIS"].ToString();
                    txtNmat.Text = reader["N_MAT"].ToString();
                    txtRa.Text = reader["RA"].ToString();
                    txtRg.Text = reader["RG"].ToString();
                    //ENSINO
                    id_ensino_atual = cs_disciplinas.troca_ensino_nome_por_id(reader["ENSINO"].ToString());
                    id_ensino_selecionado = id_ensino_atual;
                    cmbEnsino.Text = reader["ENSINO"].ToString();
                    //
                    dtp_dt_nasc.Value = DateTime.Parse(reader["DAT_NASC"].ToString());
                    dat_mat = reader["DAT_MAT"].ToString();
                    dtpEntEnsino.Value = cs_ensinos.ent_ensino(txtNmat.Text, id_ensino_atual);
                }
                if(!reader.HasRows)
                {
                    MessageBox.Show("Aluno não encontrado!", "Pesquisar Aluno.");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                consulta.Command.Parameters.Clear();
                consulta.Connection.Close();
            }
        }

        //
        public void busca_escola_anterior()
        {
            if (txtNmat.Text != "")
            {
                consulta.Command.CommandText =
                    @"SELECT * FROM HISTORICOS WHERE N_MAT = @nmat AND ENSINO=@ensino ";

                consulta.Command.Parameters.AddWithValue("@nmat", txtNmat.Text);
                consulta.Command.Parameters.AddWithValue("@ensino", id_ensino_selecionado); 

                try
                {
                    consulta.Connection.Open();
                    SqlDataReader reader = consulta.Command.ExecuteReader();
                    while (reader.Read())
                    {                        
                        txtSerie_ant.Text = reader["SERIE_ANT"].ToString();
                        txtIns_ant.Text = reader["INS_ANT"].ToString();
                        txtAno_ant.Text = reader["ANO_ANT"].ToString();
                        

                        txtUfAnt.Text = reader["UF_ANT"].ToString();                       
                        txtCidadeAnt.Text = reader["MU_ANT"].ToString();


                        cmbDiretor.Text = reader["DIRETOR"].ToString();
                        txtRg_diretor.Text = reader["RG_DIRETOR"].ToString();
                        cmbSecretario.Text = reader["SECRETARIO"].ToString();
                        txtRg_secretario.Text = reader["RG_SECRETARIO"].ToString();
                        if (reader["DAT_LIVRO"] != DBNull.Value)
                            dtp_dt_liv.Value = DateTime.Parse(reader["DAT_LIVRO"].ToString());
                        txtLivro.Text = reader["LIVRO"].ToString();
                        txtPag.Text = reader["PAGINA"].ToString();
                        txtTermo.Text = reader["TERMO"].ToString();
                        if (reader["DAT_DOC"] != DBNull.Value)
                            dtp_dt_doc.Value = DateTime.Parse(reader["DAT_DOC"].ToString());
                        if (reader["ANO_CON"] != DBNull.Value)
                            txt_anocon.Text = reader["ANO_CON"].ToString();
                        txtObs.Text = reader["OBS_1"].ToString();

                        if (reader["DAT_CON"] != DBNull.Value)
                        {
                            dtp_dt_con.Value = DateTime.Parse(reader["DAT_CON"].ToString());
                            dtp_dt_con.Checked = true;
                        }
                        else
                        {
                            dtp_dt_con.Value = DateTime.Now;
                            dtp_dt_con.Checked = false;
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Error_Log.csControle_erros.exibir_erro(ex.Message);
                }
                finally
                {
                    consulta.Command.Parameters.Clear();
                    consulta.Connection.Close();

                }
            }
        }

        public void fill_medias_hist()
        {
            //Preencher Médias
            dgvHistorico.Rows.Clear();

            DataTable tb = cs_disciplinas.buscarDisciplinas_ensino(id_ensino_selecionado);

            for(int i = 0; i < tb.Rows.Count; i++)
            {
                dgvHistorico.Rows.Add();

                dgvHistorico.Rows[i].Cells[0].Value = tb.Rows[i][0].ToString(); //id_disciplina

                dgvHistorico.Rows[i].Cells[2].Value = tb.Rows[i][2].ToString(); //n_hist_disciplina

                List<string> list = cs_notas.media_final_historico(txtNmat.Text, Convert.ToInt32(tb.Rows[i][0]), id_ensino_selecionado);
                if(list.Count > 0)
                {
                    //id_media
                    dgvHistorico.Rows[i].Cells[1].Value = list[0];
                    //instituição
                    dgvHistorico.Rows[i].Cells[3].Value = list[1];
                    //municipio
                    dgvHistorico.Rows[i].Cells[4].Value = list[2];
                    //uf
                    dgvHistorico.Rows[i].Cells[5].Value = list[3];
                    //data_final
                    if (list[5].ToString() != "")
                    {
                        dgvHistorico.Rows[i].Cells[6].Value = DateTime.Parse(list[5].ToString()).ToString("dd/MM/yyyy");
                    }                    
                    //media
                    dgvHistorico.Rows[i].Cells[7].Value = list[4];                    
                }
            }
        }

        public void fill_tab_notas()
        {
            tab_notas.Columns.Clear();
            tab_notas.Rows.Clear();

            tab_notas.Columns.Add("id_disciplina"); //0
            tab_notas.Columns.Add("dat_ini");       //1
            tab_notas.Columns.Add("media");         //2
            tab_notas.Columns.Add("dat_fin");       //3
            tab_notas.Columns.Add("orientador");    //4

            int linha_tab = 0;

            for (int i = 0; i < dgvHistorico.Rows.Count; i ++)
            {
                List<string> list_dados = cs_notas.list_dados_nota(nmat, id_ensino_selecionado, Convert.ToInt32(dgvHistorico[0,i].Value));

                if(list_dados.Count > 3)
                {
                    tab_notas.Rows.Add();
                    tab_notas.Rows[linha_tab][0] = cs_disciplinas.troca_disciplina_id_por_nome(Convert.ToInt32(dgvHistorico[0, i].Value));
                    tab_notas.Rows[linha_tab][1] = list_dados[0];
                    tab_notas.Rows[linha_tab][2] = list_dados[1];
                    tab_notas.Rows[linha_tab][3] = list_dados[2];
                    tab_notas.Rows[linha_tab][4] = cs_usuarios.troca_id_por_nome(Convert.ToInt32(list_dados[3]));
                    //else tab_notas.Rows[linha_tab][4] = cs_usuarios.troca_nome_id(Convert.ToInt32(list_dados[4]));
                    linha_tab++;
                }                
            }            
        }

        public void verifica_hist()
        {
            consulta.Command.Parameters.Clear();
            
            consulta.Command.CommandText =
                    @"SELECT * FROM HISTORICOS WHERE N_MAT = @n_mat AND ENSINO=@ensino ";
            consulta.Command.Parameters.AddWithValue("@n_mat", txtNmat.Text);
            consulta.Command.Parameters.AddWithValue("@ensino", id_ensino_selecionado);
            
            try
            {
                consulta.Connection.Open();
                SqlDataReader reader = consulta.Command.ExecuteReader();

                while (reader.Read())
                {   
                }
                if (!reader.HasRows)
                {
                    cad_hist();
                }
               
                reader.Close();
            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                consulta.Command.Parameters.Clear();
                consulta.Connection.Close();
            }
        }

        public void cad_hist()
        {
            cad_hists.Command.Parameters.Clear();
                        
            cad_hists.Command.CommandText =
                  @"INSERT INTO HISTORICOS (N_MAT, ENSINO) VALUES (@n_mat, @ensino)";
            cad_hists.Command.Parameters.AddWithValue("@n_mat", txtNmat.Text);
            cad_hists.Command.Parameters.AddWithValue("@ensino", cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text));

            try
            {
                cad_hists.Connection.Open();
                cad_hists.Command.ExecuteNonQuery();         
            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }
            finally
            {
                cad_hists.Command.Parameters.Clear();
                cad_hists.Connection.Close();
            }

        }       

        // Impressão
        private void btImprimir_hist_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (Configurações.csEscola.cidade.ToLower() == "sorocaba")
            {
                btImprimir_hist.Enabled = false;

                cs_relatorios.dis_list.Clear();
                cs_relatorios.ins_list.Clear();
                cs_relatorios.mu_list.Clear();
                cs_relatorios.uf_list.Clear();
                cs_relatorios.not_list.Clear();
                cs_relatorios.dt_list.Clear();
                
                for (int i = 0; i < dgvHistorico.Rows.Count; i++)
                {
                    if (dgvHistorico.Rows[i].Cells[2].Value != null) cs_relatorios.dis_list.Add(dgvHistorico.Rows[i].Cells[2].Value.ToString());
                    else cs_relatorios.dis_list.Add("");
                    if (dgvHistorico.Rows[i].Cells[3].Value != null) cs_relatorios.ins_list.Add(dgvHistorico.Rows[i].Cells[3].Value.ToString());
                    else cs_relatorios.ins_list.Add("");
                    if (dgvHistorico.Rows[i].Cells[4].Value != null) cs_relatorios.mu_list.Add(dgvHistorico.Rows[i].Cells[4].Value.ToString());
                    else cs_relatorios.mu_list.Add("");
                    if (dgvHistorico.Rows[i].Cells[5].Value != null) cs_relatorios.uf_list.Add(dgvHistorico.Rows[i].Cells[5].Value.ToString());
                    else cs_relatorios.uf_list.Add("");
                    if (dgvHistorico.Rows[i].Cells[7].Value != null) cs_relatorios.not_list.Add(dgvHistorico.Rows[i].Cells[7].Value.ToString());
                    else cs_relatorios.not_list.Add("");
                    if (dgvHistorico.Rows[i].Cells[6].Value != null) cs_relatorios.dt_list.Add(dgvHistorico.Rows[i].Cells[6].Value.ToString());
                    else cs_relatorios.dt_list.Add("");
                }

                btsalva.PerformClick();

                Relatorios.frCrystalReport form = new Relatorios.frCrystalReport();

                //TODO: Verificar como eliminar
                if (cmbEnsino.Text == "FUNDAMENTAL") form.cr_report = cs_relatorios.gera_crystal_historico_f_sorocaba(cs_alunos.dados_aluno(txtNmat.Text), cs_notas.dados_historico(txtNmat.Text, cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text)));
                else if (cmbEnsino.Text == "MÉDIO") form.cr_report = cs_relatorios.gera_crystal_historico_m_sorocaba(cs_alunos.dados_aluno(txtNmat.Text), cs_notas.dados_historico(txtNmat.Text, cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text)));

                form.Show();

                btImprimir_hist.Enabled = true;

                btImprimir_hist.Enabled = true;
            }
            else if (Configurações.csEscola.cidade.ToLower() == "americana")
            {
                btImprimir_hist.Enabled = false;

                cs_relatorios.dis_list.Clear();
                cs_relatorios.ins_list.Clear();
                cs_relatorios.mu_list.Clear();
                cs_relatorios.uf_list.Clear();
                cs_relatorios.not_list.Clear();
                cs_relatorios.dt_list.Clear();

                for (int i = 0; i < dgvHistorico.Rows.Count; i++)
                {
                    if (dgvHistorico.Rows[i].Cells[2].Value != null) cs_relatorios.dis_list.Add(dgvHistorico.Rows[i].Cells[2].Value.ToString());
                    else cs_relatorios.dis_list.Add("");
                    if (dgvHistorico.Rows[i].Cells[3].Value != null) cs_relatorios.ins_list.Add(dgvHistorico.Rows[i].Cells[3].Value.ToString());
                    else cs_relatorios.ins_list.Add("");
                    if (dgvHistorico.Rows[i].Cells[4].Value != null) cs_relatorios.mu_list.Add(dgvHistorico.Rows[i].Cells[4].Value.ToString());
                    else cs_relatorios.mu_list.Add("");
                    if (dgvHistorico.Rows[i].Cells[5].Value != null) cs_relatorios.uf_list.Add(dgvHistorico.Rows[i].Cells[5].Value.ToString());
                    else cs_relatorios.uf_list.Add("");
                    if (dgvHistorico.Rows[i].Cells[7].Value != null) cs_relatorios.not_list.Add(dgvHistorico.Rows[i].Cells[7].Value.ToString());
                    else cs_relatorios.not_list.Add("");
                    if (dgvHistorico.Rows[i].Cells[6].Value != null) cs_relatorios.dt_list.Add(dgvHistorico.Rows[i].Cells[6].Value.ToString());
                    else cs_relatorios.dt_list.Add("");
                }

                btsalva.PerformClick();

                Relatorios.frCrystalReport form = new Relatorios.frCrystalReport();

                //TODO: Verificar como eliminar
                if (cmbEnsino.Text == "FUNDAMENTAL") form.cr_report = cs_relatorios.gera_crystal_historico_f_americana(cs_alunos.dados_aluno(txtNmat.Text), cs_notas.dados_historico(txtNmat.Text, cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text)));
                else if (cmbEnsino.Text == "MÉDIO") form.cr_report = cs_relatorios.gera_crystal_historico_m_americana(cs_alunos.dados_aluno(txtNmat.Text), cs_notas.dados_historico(txtNmat.Text, cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text)));

                form.Show();

                btImprimir_hist.Enabled = true;
            }

            Cursor.Current = Cursors.Default;
        }

        private void btImprimir_notas_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btImprimir_hist.Enabled = false;

             cs_relatorios.list_disciplina.Clear();
             cs_relatorios.list_dat_ini.Clear();
             cs_relatorios.list_media.Clear();
             cs_relatorios.list_dat_fin.Clear();
             cs_relatorios.list_orientador.Clear();
            
            fill_tab_notas();
            
            for (int i = 0; i < tab_notas.Rows.Count; i++)
            {
                cs_relatorios.list_disciplina.Add(tab_notas.Rows[i][0].ToString());
                cs_relatorios.list_dat_ini.Add(tab_notas.Rows[i][1].ToString());
                cs_relatorios.list_media.Add(tab_notas.Rows[i][2].ToString());
                cs_relatorios.list_dat_fin.Add(tab_notas.Rows[i][3].ToString());
                cs_relatorios.list_orientador.Add(tab_notas.Rows[i][4].ToString());
            }    
           
            cs_relatorios.aluno = txtAluno.Text;
            cs_relatorios.rg = txtRg.Text;
            cs_relatorios.nnmat = txtNmat.Text;

            if (cmbEnsino.Text == "MÉDIO")
            {
                cs_relatorios.gera_not_M();
            }
            else
            {
                cs_relatorios.gera_not_F();
            }

            btImprimir_hist.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btTrocaensino_Click(object sender, EventArgs e)
        {
            //Verificar forma de retirar trexo Médio e Fundamental
            if(cmbEnsino.Text ==  "MÉDIO")
            {
                cmbEnsino.Text = "FUNDAMENTAL";                
                groupBox1.Enabled = false;
            }
            else
            {
                cmbEnsino.Text = "MÉDIO";
                groupBox1.Enabled = true;
            }

            verifica_hist();
            fill_medias_hist();
            busca_escola_anterior();
            dtpEntEnsino.Value = cs_ensinos.ent_ensino(txtNmat.Text, id_ensino_selecionado);
        }
        
        //edição datagridview
        private void dgvHistorico_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvHistorico.Rows[dgvHistorico.CurrentRow.Index].Cells[dgvHistorico.CurrentCell.ColumnIndex].Value != null)
            {
                int index_linha = linha_atual();

                if (dgvHistorico.Rows[index_linha].Cells[0].Value != null && dgvHistorico.Rows[index_linha].Cells[3].Value != null &&
                        dgvHistorico.Rows[index_linha].Cells[4].Value != null && dgvHistorico.Rows[index_linha].Cells[5].Value != null &&
                        dgvHistorico.Rows[index_linha].Cells[6].Value != null && dgvHistorico.Rows[index_linha].Cells[7].Value != null)
                {
                    //UPDATE OU INSERT MÉDIA
                    if (cs_notas.ha_media_final_nmat(nmat, Convert.ToInt32(dgvHistorico.Rows[index_linha].Cells[0].Value), id_ensino_selecionado))
                    {
                        //UPDATE
                        cs_notas.upd_media_final(dgvHistorico.Rows[index_linha].Cells[3].Value.ToString(),
                            dgvHistorico.Rows[index_linha].Cells[4].Value.ToString(),
                            dgvHistorico.Rows[index_linha].Cells[5].Value.ToString(),
                            dgvHistorico.Rows[index_linha].Cells[7].Value.ToString(),
                            dgvHistorico.Rows[index_linha].Cells[6].Value.ToString(),
                            Usuarios_Grupos.csUsuario_logado.id_usuario_logado, 
                            0,
                            nmat,
                            Convert.ToInt32(dgvHistorico.Rows[index_linha].Cells[0].Value),
                            cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text));
                    }
                    else
                    {
                        //INSERT
                        cs_notas.add_media_final_pelo_hist(Convert.ToInt32(dgvHistorico.Rows[index_linha].Cells[0].Value),
                            nmat,
                            id_ensino_selecionado,
                            dgvHistorico.Rows[index_linha].Cells[3].Value.ToString(),
                            dgvHistorico.Rows[index_linha].Cells[4].Value.ToString(),
                            dgvHistorico.Rows[index_linha].Cells[5].Value.ToString(),                    
                            dgvHistorico.Rows[index_linha].Cells[7].Value.ToString(),
                            dgvHistorico.Rows[index_linha].Cells[6].Value.ToString(),
                            Usuarios_Grupos.csUsuario_logado.id_usuario_logado);
                    }
                }
            }
        }

        public int linha_atual()
        {
            return dgvHistorico.CurrentRow.Index;
        }

        public int coluna_atual()
        {
            return dgvHistorico.CurrentCell.ColumnIndex;
        }

        private void bt_edit_cad_Click(object sender, EventArgs e)
        {
            Alunos.CSaluno.numat = txtNmat.Text;

            Alunos.frEditaluno form = new Alunos.frEditaluno();
            //form.MdiParent = this.ParentForm;
            form.Show();
            this.Close();
        }

        private void txtSerie_ant_TextChanged(object sender, EventArgs e)
        {
            btsalva.Enabled = true;
        }

        //Endereços       
        
        private void dtpEntEnsino_ValueChanged(object sender, EventArgs e)
        {
            //Alterar Data de Entrada no Ensino
            cs_ensinos.salvar_troca_ensino(dtpEntEnsino.Value, id_ensino_selecionado, txtNmat.Text);
        }

        private void btsalva_Click(object sender, EventArgs e)
        {
            cs_notas.upd_historico(txtObs.Text, cmbDiretor.Text, txtRg_diretor.Text, cmbSecretario.Text,
                txtRg_secretario.Text, dtp_dt_liv.Value.ToString(), txtLivro.Text, txtPag.Text, txtTermo.Text,
                dtp_dt_doc.Value.ToString(),
                txtSerie_ant.Text, txtIns_ant.Text, txtAno_ant.Text, txtCidadeAnt.Text, txtUfAnt.Text, nmat, id_ensino_selecionado);

            btsalva.Enabled = false;

            if (cs_alunos.esta_concluido(txtNmat.Text) == false &&
                cmbEnsino.Text == "MÉDIO" &&
                dtp_dt_con.Checked &&
                txt_anocon.Text != "")
            {
                if (MessageBox.Show("O Ano de Conclusão está informado. Gostaria de Concluir o Aluno", "Concluir Aluno ?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //Altera Historico - Campos - ANO_CON e DAT_CON
                    cs_notas.updt_historico_conclusao(dtp_dt_con.Value.ToString(), txtAno_ant.Text, nmat, id_ensino_selecionado);

                    //Altera Status na tabela  para concluido = 1
                    cs_alunos.concluir_aluno(Convert.ToInt32(txtNmat.Text));

                    //Add Históricos Situação
                    cs_historicos_situacoes.add_hist_situacao(2, DateTime.Now, Usuarios_Grupos.csUsuario_logado.id_usuario_logado, "ALUNO CONCLUIU O ENSINO MÉDIO",  txtNmat.Text);
                                        
                }
                else
                {
                    txtAno_ant.Text = "";
                    dtp_dt_con.Checked = false;
                }
            }
            
            
        }

        public void get_id_grupo_diretor()
        {
            consulta.Command.CommandText =
                @"SELECT ID_GRUPO, GRUPO FROM GRUPOS WHERE GRUPO='DIRETORIA'";
            try
            {
                if (consulta.Connection.State != ConnectionState.Closed)
                {
                    consulta.Connection.Close();
                }

                consulta.Connection.Open();
                SqlDataReader reader = consulta.Command.ExecuteReader();

                while (reader.Read())
                {
                    id_dir_grupo = reader["ID_GRUPO"].ToString();
                }
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                consulta.Command.Parameters.Clear();
                consulta.Connection.Close();
                get_diretor();
            }                
        }

        public void get_diretor()
        {
            consulta.Command.CommandText =
                @"SELECT * FROM USUARIO WHERE ID_GRUPO=@idgrupo";

            consulta.Command.Parameters.AddWithValue("@idgrupo", id_dir_grupo);
            try
            {
                if (consulta.Connection.State != ConnectionState.Closed)
                {
                    consulta.Connection.Close();
                }

                consulta.Connection.Open();
                SqlDataReader reader = consulta.Command.ExecuteReader();

                int i = 0;
                while (reader.Read())
                {
                    cmbDiretor.Items.Add(reader["NOME"].ToString());

                    tab_dir.Rows.Add();
                    tab_dir.Rows[i][0] = reader["NOME"].ToString();
                    tab_dir.Rows[i][1] = reader["RG"].ToString();
                    i++;
                }
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                consulta.Command.Parameters.Clear();
                consulta.Connection.Close();
            }
        }

        public void get_id_grupo_secretario()
        {
            consulta.Command.CommandText =
                @"SELECT ID_GRUPO, GRUPO FROM GRUPOS WHERE GRUPO='SECR.ADM'";
            try
            {
                if (consulta.Connection.State != ConnectionState.Closed)
                {
                    consulta.Connection.Close();
                }

                consulta.Connection.Open();
                SqlDataReader reader = consulta.Command.ExecuteReader();

                while (reader.Read())
                {
                    id_sec_grupo = reader["ID_GRUPO"].ToString();
                }
                reader.Close();
            }
            catch
            {

            }
            finally
            {
                consulta.Command.Parameters.Clear();
                consulta.Connection.Close();
                get_secretario();
            }
        }

        public void get_secretario()
        {
            consulta.Command.CommandText =
                @"SELECT * FROM USUARIO WHERE ID_GRUPO=@idgrupo";

            consulta.Command.Parameters.AddWithValue("@idgrupo", id_sec_grupo);
            try
            {
                if (consulta.Connection.State != ConnectionState.Closed)
                {
                    consulta.Connection.Close();
                }

                consulta.Connection.Open();
                SqlDataReader reader = consulta.Command.ExecuteReader();

                int i = 0;
                while (reader.Read())
                {
                    cmbSecretario.Items.Add(reader["NOME"].ToString());

                    tab_sec.Rows.Add();
                    tab_sec.Rows[i][0] = reader["NOME"].ToString();
                    tab_sec.Rows[i][1] = reader["RG"].ToString();
                    i++;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (msg.ToUpper().Contains("TIMEOUT EXPIRED"))
                    msg = "Tempo de espera esgotado...";
                MessageBox.Show(msg);
            }
            finally
            {
                consulta.Command.Parameters.Clear();
                consulta.Connection.Close();
            }
        }

        private void cmbSecretario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSecretario.Text != "")
            {
                for (int i = 0; i < tab_sec.Rows.Count; i++)
                {
                    if (tab_sec.Rows[i][0].ToString() == cmbSecretario.Text)
                    {
                        txtRg_secretario.Text = tab_sec.Rows[i][1].ToString();
                    }
                }
            }
        }

        private void cmbDiretor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDiretor.Text != "")
            {
                for (int i = 0; i < tab_dir.Rows.Count; i++)
                {
                    if (tab_dir.Rows[i][0].ToString() == cmbDiretor.Text)
                    {
                        txtRg_diretor.Text = tab_dir.Rows[i][1].ToString();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            btImprimir_hist.Enabled = false;
            

            Relatorios.csRelatorios cert = new Relatorios.csRelatorios();

            cert.aluno = txtAluno.Text;
            cert.ra = txtRa.Text;
            cert.rg = txtRg.Text;
            cert.nnmat = txtNmat.Text;
            cert.mae = txtMae.Text;
            cert.pais = txtNasc_pais.Text;
            cert.estado = txtNasc_estado.Text;
            cert.cidade = txtNasc_cidade.Text;
            cert.ano_con = txt_anocon.Text;
            cert.Serie_ant = txtSerie_ant.Text;
            cert.Estab_ant = txtIns_ant.Text;
            cert.Ano_ant = txtAno_ant.Text;
            cert.Cidade_ant = txtCidadeAnt.Text;
            cert.Uf_ant = txtUfAnt.Text;
            cert.Diretor = cmbDiretor.Text;
            cert.Rg_diretor = txtRg_diretor.Text;
            cert.Secretario = cmbSecretario.Text;
            cert.Rg_secretario = txtRg_secretario.Text;
            cert.dt_liv = dtp_dt_liv.Text;
            cert.Livro = txtLivro.Text;
            cert.Pag = txtPag.Text;
            cert.Termo = txtTermo.Text;
            cert.dt_doc = dtp_dt_doc.Text;
            cert.Obs = txtObs.Text;
            cert.dat_nasc = dtp_dt_nasc.Text;
            cert.dat_mat = dat_mat;
            cert.dat_con = dtp_dt_con.Text;


            if (cmbEnsino.Text == "MÉDIO")
            {
                cert.gera_certi_M();
            }
            else
            {
                cert.gera_certi_F();
            }

            btImprimir_hist.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void dgvHistorico_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgvHistorico.CurrentCell = dgvHistorico.Rows[e.RowIndex].Cells[e.ColumnIndex];                
                dgvHistorico.Rows[e.RowIndex].Selected = true; 
                dgvHistorico.Focus();
            }
        }

        private void bt_certi_MouseEnter(object sender, EventArgs e)
        {
            //notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;

            //notifyIcon1.BalloonTipText = "Histórico";

            //notifyIcon1.ShowBalloonTip(1000);
            
        }
        
        #region Opções botão direito

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            copia = dgvHistorico.Rows[dgvHistorico.CurrentRow.Index].Cells[dgvHistorico.CurrentCell.ColumnIndex].Value.ToString();
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvHistorico.Rows[dgvHistorico.CurrentRow.Index].Cells[dgvHistorico.CurrentCell.ColumnIndex].Value = copia;
        }

        private void excluirNotaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MÉDIA
            cs_notas.excluir_media_id_media(Convert.ToInt32(dgvHistorico.Rows[dgvHistorico.CurrentRow.Index].Cells[1].Value));
            
            dgvHistorico.Rows[dgvHistorico.CurrentRow.Index].Cells[1].Value = "";
            dgvHistorico.Rows[dgvHistorico.CurrentRow.Index].Cells[3].Value = "";
            dgvHistorico.Rows[dgvHistorico.CurrentRow.Index].Cells[4].Value = "";
            dgvHistorico.Rows[dgvHistorico.CurrentRow.Index].Cells[5].Value = "";
            dgvHistorico.Rows[dgvHistorico.CurrentRow.Index].Cells[6].Value = "";
            dgvHistorico.Rows[dgvHistorico.CurrentRow.Index].Cells[7].Value = "";

            //ATENDIMENTO

        }

        private void cEESSOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvHistorico.Rows.Count > 0)
            {
                int index_linha = linha_atual();

                dgvHistorico.Rows[index_linha].Cells[3].Value = Configurações.csEscola.escola;
                dgvHistorico.Rows[index_linha].Cells[4].Value = Configurações.csEscola.cidade;
                dgvHistorico.Rows[index_linha].Cells[5].Value = Configurações.csEscola.sigla_estado;
                
             

                if (dgvHistorico.Rows[dgvHistorico.CurrentRow.Index].Cells[dgvHistorico.CurrentCell.ColumnIndex].Value != null)
                {
                    if (dgvHistorico.Rows[index_linha].Cells[0].Value != null && dgvHistorico.Rows[index_linha].Cells[3].Value != null &&
                        dgvHistorico.Rows[index_linha].Cells[4].Value != null && dgvHistorico.Rows[index_linha].Cells[5].Value != null &&
                        dgvHistorico.Rows[index_linha].Cells[6].Value != null && dgvHistorico.Rows[index_linha].Cells[7].Value != null)
                    {
                        //UPDATE OU INSERT MÉDIA
                        if (cs_notas.ha_media_final_nmat(nmat, Convert.ToInt32(dgvHistorico.Rows[index_linha].Cells[1].Value), id_ensino_selecionado))
                        {
                            //UPDATE
                            cs_notas.upd_media_final(dgvHistorico.Rows[index_linha].Cells[3].Value.ToString(),
                                dgvHistorico.Rows[index_linha].Cells[4].Value.ToString(),
                                dgvHistorico.Rows[index_linha].Cells[5].Value.ToString(),
                                dgvHistorico.Rows[index_linha].Cells[7].Value.ToString(),
                                dgvHistorico.Rows[index_linha].Cells[6].Value.ToString(),
                                Usuarios_Grupos.csUsuario_logado.id_usuario_logado,
                                0,
                                nmat,
                                Convert.ToInt32(dgvHistorico.Rows[index_linha].Cells[0].Value),
                                cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text));
                        }
                        else
                        {
                            //INSERT
                            cs_notas.add_media_final_pelo_hist(Convert.ToInt32(dgvHistorico.Rows[index_linha].Cells[0].Value),
                                nmat,
                                id_ensino_selecionado,
                                dgvHistorico.Rows[index_linha].Cells[3].Value.ToString(),
                                dgvHistorico.Rows[index_linha].Cells[4].Value.ToString(),
                                dgvHistorico.Rows[index_linha].Cells[5].Value.ToString(),
                                dgvHistorico.Rows[index_linha].Cells[7].Value.ToString(),
                                dgvHistorico.Rows[index_linha].Cells[6].Value.ToString(),
                                Usuarios_Grupos.csUsuario_logado.id_usuario_logado);
                        }
                    }
                }
            }
        }

        #endregion

        private void FRhistorico_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(btsalva.Enabled == true)
            {
                if(MessageBox.Show("Gostaria de salvar as alterações antes de Sair ?","Salvar Histórico", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    btsalva.PerformClick();
                }
            }
        }

        private void dtp_dt_con_ValueChanged(object sender, EventArgs e)
        {
            btsalva.Enabled = true;
        }

        private void cmbEnsino_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_ensino_selecionado = cs_disciplinas.troca_ensino_nome_por_id(cmbEnsino.Text);

            verifica_hist();
            fill_medias_hist();
            busca_escola_anterior();
        }

        
    }
}
