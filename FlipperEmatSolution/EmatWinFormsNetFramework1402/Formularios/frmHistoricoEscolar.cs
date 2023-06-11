using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EmatWinFormsNetFramework1402.Classes;


using System.Data.SqlClient;
using System.Linq;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.Formularios
{
    //TODO:: ADICIONAR AVISO QUANDO UMA MEDIA FOR ALTERADA 
    public partial class frmHistoricoEscolar : Form
    {
        private readonly IEmatriculaSettings _settings;
        public Aluno objAluno;
        public string copia;
        public DataTable tab_notas = new DataTable();
        BindingSource bs_ensinos = new BindingSource();
        private object oldValue;

        public frmHistoricoEscolar(IEmatriculaSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void frmHistoricoEscolar_Load(object sender, EventArgs e)
        {
            try
            {
                #region Popular cmbDiretor e cmbSecretario
                List<Usuario> listaUsuarios = DAO.UsuarioDAO.ExibirTodos();
                //Diretor
                cmbDiretor.SelectedIndexChanged -= cmbDiretor_SelectedIndexChanged;
                BindingSource bsDiretor = new BindingSource();
                bsDiretor.DataSource = listaUsuarios.Where(x => x.NivelAcesso == Enumeradores.NivelAcesso.DIRETOR && x.Ativo == true).ToList();
                cmbDiretor.DataSource = bsDiretor;
                cmbDiretor.DisplayMember = "Nome";
                cmbDiretor.ValueMember = "Codigo";
                cmbDiretor.SelectedIndex = -1;
                cmbDiretor.SelectedIndexChanged += cmbDiretor_SelectedIndexChanged;
                //Secretário
                cmbSecretario.SelectedIndexChanged -= cmbSecretario_SelectedIndexChanged;
                BindingSource bsSecretario = new BindingSource();
                bsSecretario.DataSource = listaUsuarios.Where(x => x.NivelAcesso == Enumeradores.NivelAcesso.SECRETARIOADM && x.Ativo == true).ToList();
                cmbSecretario.DataSource = bsSecretario;
                cmbSecretario.DisplayMember = "Nome";
                cmbSecretario.ValueMember = "Codigo";
                cmbSecretario.SelectedIndex = -1;
                cmbSecretario.SelectedIndexChanged += cmbSecretario_SelectedIndexChanged;
                #endregion

                #region Popular Ensinos Aluno
                //Ensino
                cmbEnsinoAluno.SelectedIndexChanged -= cmbEnsino_SelectedIndexChanged;
                cmbEnsinoAluno.DataSource = objAluno.ListaEnsinoAluno.Select(x => x).ToList();
                cmbEnsinoAluno.DisplayMember = "Ensino";
                cmbEnsinoAluno.ValueMember = "Codigo";
                cmbEnsinoAluno.SelectedIndex = -1;
                cmbEnsinoAluno.Enabled = true;
                cmbEnsinoAluno.SelectedIndexChanged += cmbEnsino_SelectedIndexChanged;
                #endregion

                #region Carregar Opções de Países Anterior.
                CarregarPaisAnterior();
                #endregion

                #region Configura opção do botão esquerdo
                contextMenuStrip1.Items[0].Text = _settings.CeejaNome;
                #endregion

                #region Preencher Dados Aluno
                if (objAluno.NMatricula > 0)
                {
                    //Aluno
                    txtNmat.Text = objAluno.NMatricula.ToString();
                    txtRa.Text = objAluno.Ra;
                    txtRg.Text = objAluno.Rg + "/" + objAluno.UfRg;
                    txtAluno.Text = objAluno.Nome;
                    txtMae.Text = objAluno.NomeMae;
                    if (objAluno.LocalNascimento != null)
                    {
                        txtNasc_cidade.Text = objAluno.LocalNascimento.Cidade.Nome;
                        txtNasc_estado.Text = objAluno.LocalNascimento.Cidade.Uf.Nome;
                        txtNasc_pais.Text = objAluno.LocalNascimento.Cidade.Uf.Pais.Nome;
                    }
                    //Ensino - Irá chamar o evento selectIndexCnange e consequentemente montar
                    cmbEnsinoAluno.SelectedItem = Classes.Aluno.GetEnsinoAlunoAtual(objAluno);

                    //Nascimento
                    dtpDataNascimento.Text = objAluno.DtNascimento;
                }
                #endregion

                btnImprimirHistorico.Enabled = true;
                btImprimir_certi.Enabled = true;

                //TODO: FUTURO 2 - MELHORAR BOTÃO DE IMPRESÃO DE NOTA
                //Teste if parar desabilitar botão Impressão de Médias
                //AQUI O PROBLEMA É AS ESCOLAS QUE NÃO TEM PROFESSOR INDIVIDUAL
                if (_settings.Ceeja.ToLower() != "americana")
                {
                    btImprimir_notas.Enabled = true;
                }

                btnSalvarHistorico.Enabled = false;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void fill_medias_extras()
        {
            //  //Função preencher médias extras no histórico
            //  csMediasExtra cs_mediaextra = new csMediasExtra();
            //
            //  List<csMediasExtra> list_medias_extras = cs_mediaextra.sel_list_mediaextra(objAluno.NDeMatricula.ToString(), ensino_selecionado.CodDoEnsino);
            //
            //  for(int i = 0; i < list_medias_extras.Count; i++)
            //  {
            //      dgvMedias.Rows.Add();
            //
            //      int ultimalinha = dgvMedias.Rows.Count - 1;
            //      
            //      dgvMedias.Rows[ultimalinha].Cells[9].Value = list_medias_extras[i].id_media_extra;
            //      dgvMedias.Rows[ultimalinha].Cells[2].Value = list_medias_extras[i].disciplina;
            //      dgvMedias.Rows[ultimalinha].Cells[3].Value = list_medias_extras[i].instituicao;
            //      dgvMedias.Rows[ultimalinha].Cells[4].Value = list_medias_extras[i].cidade;
            //      dgvMedias.Rows[ultimalinha].Cells[5].Value = list_medias_extras[i].uf;
            //      if(list_medias_extras[i].data.ToString("dd/MM/yyyy") != "01/01/0001")
            //          dgvMedias.Rows[ultimalinha].Cells[6].Value = list_medias_extras[i].data.ToString("dd/MM/yyyy");
            //      dgvMedias.Rows[ultimalinha].Cells[7].Value = list_medias_extras[i].media;
            //  }

        }

        private void btImprimir_certi_Click(object sender, EventArgs e)
        {
            //  Cursor.Current = Cursors.WaitCursor;
            //
            //  btImprimir_hist.Enabled = false;            
            //
            //  if (Configurações.csEscola.cidade.ToLower() == "americana")
            //  {
            //      Relatorios.frCrystalReport form = new Relatorios.frCrystalReport();
            //
            //      
            //      /*
            //      form.cr_report = cs_relatorios.gera_crystal_certif_americana(
            //          objAluno, cs_notas.dados_historico(txtNmat.Text, (Int32)cmbEnsino.SelectedValue),
            //          ensino_selecionado, dtp_dt_doc.Value);
            //      */
            //      form.Show();
            //  }
            //  else
            //  {
            //      Relatorios.csRelatorios cert = new Relatorios.csRelatorios();
            //                      
            //      //cert.ano_con = txtAnoCon.Text;
            //      //cert.Serie_ant = txtSerie_ant.Text;
            //      //cert.Estab_ant = txtIns_ant.Text;
            //      //cert.Ano_ant = txtAno_ant.Text;
            //      //cert.Cidade_ant = txtCidadeAnt.Text;
            //      //cert.Uf_ant = txtUfAnt.Text;
            //      //cert.Diretor = cmbDiretor.Text;
            //      //cert.Rg_diretor = txtRg_diretor.Text;
            //      //cert.Secretario = cmbSecretario.Text;
            //      //cert.Rg_secretario = txtRg_secretario.Text;
            //      //cert.dt_liv = dtp_dt_liv.Text;
            //      //cert.Livro = txtLivro.Text;
            //      //cert.Pag = txtPag.Text;
            //      //cert.Termo = txtTermo.Text;
            //      //cert.dt_doc = dtp_dt_doc.Text;
            //      //cert.Obs = txtObs.Text;
            //      //cert.dat_nasc = dtp_dt_nasc.Text;
            //      //cert.dat_mat = objAluno.SituacaoDoAluno.DtDeMatricula.ToString();
            //      //cert.dat_con = dtp_dt_con.Text;
            //
            //
            //      if (cmbEnsino.Text == "MÉDIO")
            //      {
            //          cert.gera_certi_M(objAluno);
            //      }
            //      else
            //      {
            //          cert.gera_certi_F(objAluno);
            //      }
            //
            //      
            //  }
            //  btImprimir_hist.Enabled = true;
            //  Cursor.Current = Cursors.Default;

        }

        private void btnImprimirHistorico_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (_settings.Ceeja.ToLower() == "sorocaba")
            {
                btnImprimirHistorico.Enabled = false;

                btnSalvarHistorico.PerformClick();

                frCrystalReport form = new frCrystalReport();

                if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    form.cr_report = Relatorios.Escolas.Sorocaba.RelatoriosSorocaba.gera_crystal_historico_f_sorocaba(objAluno);
                else if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).Ensino == Enumeradores.Ensino.MÉDIO)
                    form.cr_report = Relatorios.Escolas.Sorocaba.RelatoriosSorocaba.gera_crystal_historico_m_sorocaba(objAluno);

                form.Show();

                btnImprimirHistorico.Enabled = true;
            }
            else if (_settings.Ceeja.ToLower() == "americana")
            {
                //for (int i = 0; i < dgvMedias.Rows.Count; i++)
                //{
                //    DataGridViewCheckBoxCell cbxCell = (DataGridViewCheckBoxCell)dgvMedias.Rows[i].Cells[8];
                //
                //    if ((bool)cbxCell.Value)
                //    {
                //        if (dgvMedias.Rows[i].Cells[2].Value != null) cs_relatorios.dis_list.Add(dgvMedias.Rows[i].Cells[2].Value.ToString());
                //        else cs_relatorios.dis_list.Add("-----");
                //        if (dgvMedias.Rows[i].Cells[3].Value != null) cs_relatorios.ins_list.Add(dgvMedias.Rows[i].Cells[3].Value.ToString());
                //        else cs_relatorios.ins_list.Add("-----");
                //        if (dgvMedias.Rows[i].Cells[4].Value != null) cs_relatorios.mu_list.Add(dgvMedias.Rows[i].Cells[4].Value.ToString());
                //        else cs_relatorios.mu_list.Add("-----");
                //        if (dgvMedias.Rows[i].Cells[5].Value != null) cs_relatorios.uf_list.Add(dgvMedias.Rows[i].Cells[5].Value.ToString());
                //        else cs_relatorios.uf_list.Add("-----");
                //        if (dgvMedias.Rows[i].Cells[7].Value != null) cs_relatorios.not_list.Add(dgvMedias.Rows[i].Cells[7].Value.ToString());
                //        else cs_relatorios.not_list.Add("-----");
                //        if (dgvMedias.Rows[i].Cells[6].Value != null) cs_relatorios.dt_list.Add(dgvMedias.Rows[i].Cells[6].Value.ToString());
                //        else cs_relatorios.dt_list.Add("-----");
                //    }
                //}
                //
                //btsalva.PerformClick();
                //
                //Relatorios.frCrystalReport form = new Relatorios.frCrystalReport();
                //
                //if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                //    form.cr_report = Relatorios.americana.RelatoriosAmericana.gera_crystal_historico_f_americana(objAluno);
                //else if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).Ensino == Enumeradores.Ensino.MÉDIO)
                //    form.cr_report = Relatorios.americana.RelatoriosAmericana.gera_crystal_historico_m_americana(objAluno);
                //
                //form.Show();
                //
                //btImprimir_hist.Enabled = true;
            }
            else if (_settings.Ceeja.ToLower() == "votorantim")
            {
                if (string.IsNullOrEmpty(txtNasc_cidade.Text) || string.IsNullOrEmpty(txtNasc_pais.Text) ||
                    string.IsNullOrEmpty(txtNasc_estado.Text))
                {
                    MessageBox.Show("Dados Incompletos.");
                    return;
                }

                btnImprimirHistorico.Enabled = false;

                btnSalvarHistorico.PerformClick();

                frCrystalReport form = new frCrystalReport();

                if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    form.cr_report = Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptHistoricoEscolarFundamental(objAluno);
                else if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).Ensino == Enumeradores.Ensino.MÉDIO)
                    form.cr_report = Relatorios.Escolas.Votorantim.RelatoriosVotorantim.rptHistoricoEscolarMedio(objAluno);

                form.Show();

                btnImprimirHistorico.Enabled = true;
            }
            Cursor.Current = Cursors.Default;
        }

        private void btImprimir_notas_Click(object sender, EventArgs e)
        {
            // Cursor.Current = Cursors.WaitCursor;
            // btImprimir_hist.Enabled = false;
            //
            //  cs_relatorios.list_disciplina.Clear();
            //  cs_relatorios.list_dat_ini.Clear();
            //  cs_relatorios.list_media.Clear();
            //  cs_relatorios.list_dat_fin.Clear();
            //  cs_relatorios.list_orientador.Clear();
            // 
            // fill_tab_notas();
            // 
            // for (int i = 0; i < tab_notas.Rows.Count; i++)
            // {
            //     cs_relatorios.list_disciplina.Add(tab_notas.Rows[i][0].ToString());
            //     cs_relatorios.list_dat_ini.Add(tab_notas.Rows[i][1].ToString());
            //     cs_relatorios.list_media.Add(tab_notas.Rows[i][2].ToString());
            //     cs_relatorios.list_dat_fin.Add(tab_notas.Rows[i][3].ToString());
            //     cs_relatorios.list_orientador.Add(tab_notas.Rows[i][4].ToString());
            // }
            //
            // if (cmbEnsino.Text == "MÉDIO")
            // {
            //     cs_relatorios.gera_not_M(objAluno);
            // }
            // else
            // {
            //     cs_relatorios.gera_not_F(objAluno);
            // }
            //
            // btImprimir_hist.Enabled = true;
            // Cursor.Current = Cursors.Default;
        }

        private void dgvMedias_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            salvar_media();
        }

        private void btnEditarMatricula_Click(object sender, EventArgs e)
        {
            frmAluno form = new frmAluno(_settings, objAluno);
            form.MdiParent = this.ParentForm;
            form.Show();
            this.Close();
        }

        private void btnSalvarHistorico_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //Fazer Verificação de Campos 
                if (cmbSecretario.SelectedIndex > -1)
                {
                    if (cmbDiretor.SelectedIndex > -1)
                    {
                        if ((EnsinoAluno)cmbEnsinoAluno.SelectedItem != null)
                        {
                            //Testar se Histórico esta nulo (Nunca teve histórico)
                            if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar == null)
                            {
                                //Novo Histórico
                                ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar = new HistoricoEscolar();
                            }
                            //Obter Dados do Formulário
                            ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Observacao = txtObs.Text;
                            ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Diretor = (Usuario)cmbDiretor.SelectedItem;
                            ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Secretario = (Usuario)cmbSecretario.SelectedItem;
                            ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.DtDocumento = dtp_dt_doc.Value.ToString("yyyy-MM-dd");

                            ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.SerieAnterior = txtSerieAnterior.Text;
                            ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.InstituicaoAnterior = txtInstituicaoAnterior.Text;
                            int anoAnterior;
                            if (Int32.TryParse(txtAnoAnterior.Text, out anoAnterior))
                                ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.AnoAnterior = anoAnterior;
                            ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.CidadeAnterior = (Cidade)cmbCidadeAnterior.SelectedItem;
                            ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Gdae = txtGdae.Text;
                            ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Fundamentacao = txtFundamentacao.Text;
                            ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Usuario = Utils.csUsuarioLogado.usuario;
                            ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.SegundaVia = ckbSegundaVia.Checked;
                            //Término
                            ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).DtTermino = dtpTerminoEnsino.Value.ToString();
                            ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.DtConclusao = dtpTerminoEnsino.Value.ToString("yyyy-MM-dd");

                            //Gravar Histórico
                            DAO.HistoricoEscolarDAO.Gravar(((EnsinoAluno)cmbEnsinoAluno.SelectedItem));
                            DAO.EnsinoAlunoDAO.Gravar((EnsinoAluno)cmbEnsinoAluno.SelectedItem);

                            if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).Ensino == Enumeradores.Ensino.MÉDIO)
                            {
                                objAluno.Concluinte = true;
                                DAO.AlunoDAO.Gravar(objAluno);

                                //var context = new Modelo.Modelo();

                                var query = (from aluno in _settings.Context.ALUNO
                                             where aluno.N_MAT == objAluno.NMatricula
                                             select aluno).First();
                                query.ATIVO = false;

                                var queryAddMovimentacao = _settings.Context.MOVIMENTACAO.Add(new Modelo.MOVIMENTACAO
                                {
                                    COD_SITUACAO = Convert.ToByte(Enumeradores.SituacaoAluno.CONCLUINTE),
                                    COD_ENSINO_ALUNO = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).Codigo,
                                    COD_USUARIO = (short)Utils.csUsuarioLogado.usuario.Codigo,
                                    DT_MOVIMENTACAO = DateTime.Now,
                                    MOTIVO = "ALUNO CONCLUINTE"
                                });

                                _settings.Context.SaveChanges();

                            }
                        }
                        btnSalvarHistorico.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Informar o Diretor.");
                    }
                }
                else
                {
                    MessageBox.Show("Informar o Secretário.");
                }

                //TODO:: Verificar código comentado a baixo. Método SalvarHistórico
                //agilizar implementação ou excluir

                //Verificar se critérios para conclusão do ensino médio estão preenchidos
                //if (objAluno.Concluiente == false &&
                //    cmbEnsino.Text == "MÉDIO" &&
                //    dtp_dt_con.Checked &&
                //    txtAnoCon.Text != "")
                //{
                //    if (MessageBox.Show("O Ano de Conclusão está informado. Gostaria de Concluir o Aluno", "Concluir Aluno ?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //    {
                //        //Altera Historico - Campos - ANO_CON e DAT_CON
                //        if(ensino_selecionado.CodDoEnsino == 1)
                //        {
                //         objAluno.DtConclusao = dtp_dt_con.Value;
                //            //txtAnoCon.Text
                //        }
                //        else
                //        {
                //            objAluno.HistoricoEscolarMedio.DtConclusao = dtp_dt_con.Value;
                //            //txtAnoCon.Text
                //        }
                //
                //
                //
                //        //Altera Status na tabela  para concluido = 1 e ativo = 0
                //        objAluno.SituacaoDoAluno.Concluinte = true;
                //        DAO.AlunoDAO alunoDAO = new DAO.AlunoDAO();
                //        alunoDAO.Gravar(objAluno);
                //
                //        //Add Históricos Situação - 3 = Concluído
                //       // inativacao.add_hist_situacao(3, DateTime.Now, Utils.csUsuarioLogado.usuario.CodDoUsuario, "ALUNO CONCLUIU O ENSINO MÉDIO", txtNmat.Text);
                //
                //    }
                //    else
                //    {
                //        txtAno_ant.Text = "";
                //        dtp_dt_con.Checked = false;
                //    }
                //}

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void frmHistoricoEscolar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnSalvarHistorico.Enabled)
                if (MessageBox.Show("Gostaria de salvar as alterações antes de Sair ?", "Salvar Histórico", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    btnSalvarHistorico.PerformClick();
        }

        private void salvar_media()
        {
            try
            {
                if (dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[dgvMedias.CurrentCell.ColumnIndex].Value != null)
                {
                    if (dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[0].Value != null)
                    {
                        if (dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells["colNomeInstituicao"].Value != null &&
                                dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells["colCidadeInstituicao"].Value != null &&
                                dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells["colUfInstituicao"].Value != null &&
                                dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells["colDataMedia"].Value != null &&
                                dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells["colValorMedia"].Value != null)
                        {
                            DateTime data;
                            if (DateTime.TryParse(dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells["colDataMedia"].Value.ToString(), out data) && data < DateTime.Now)
                            {
                                Media media = new Media();
                                media.Instituicao = dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells["colNomeInstituicao"].Value.ToString();


                                List<Uf> listaUf = DAO.UfDAO.ExibirTodos(DAO.PaisDAO.Consultar("BRASIL"));
                                Uf uf_ = listaUf.FirstOrDefault(x => x.Sigla == dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells["colUfInstituicao"].Value.ToString());

                                media.Cidade = DAO.CidadeDAO.ExibirTodos(uf_).FirstOrDefault(x => x.Nome == dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells["colCidadeInstituicao"].Value.ToString());

                                media.Valor = dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells["colValorMedia"].Value.ToString();
                                media.DtMedia = data.ToString("yyyy-MM-dd");

                                if (((DisciplinaAluno)dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[0].Value).Media != null)
                                {
                                    media.UsuarioCadastro = ((DisciplinaAluno)dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[0].Value).Media.UsuarioCadastro;
                                    media.UsuarioDeModificacao = Utils.csUsuarioLogado.usuario;
                                    media.DtModificacao = DateTime.Now.ToString();

                                }
                                else
                                {
                                    media.UsuarioCadastro = Utils.csUsuarioLogado.usuario;
                                    media.UsuarioDeModificacao = null;
                                    media.DtModificacao = null;
                                }


                                DisciplinaAluno disciplinaAluno = new DisciplinaAluno();
                                disciplinaAluno = (DisciplinaAluno)dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[0].Value;
                                disciplinaAluno.Media = media;
                                dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[0].Value = disciplinaAluno;


                                if (DAO.MediaDAO.Consultar((DisciplinaAluno)dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[0].Value) != null)
                                {
                                    if (MessageBox.Show("Deseja Alterar a Média?", "Histórico Escolar", MessageBoxButtons.YesNo) == DialogResult.No)
                                    {
                                        dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells["colNomeInstituicao"].Value = oldValue.ToString();
                                        return;
                                    }
                                }

                                DAO.MediaDAO.Gravar((DisciplinaAluno)dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[0].Value);
                                disciplinaAluno.Concluida = true;
                                DAO.DisciplinaAlunoDAO.Gravar(disciplinaAluno);


                            }
                            else
                            {
                                MessageBox.Show("Data em formato incorreto ou superior a data atual.", "Histórico");
                            }
                        }
                    }
                }
                else
                {
                    #region Disciplinas Extra
                    /*
                    if (dgvMedias.Rows[index_linha].Cells[3].Value != null &&
                            dgvMedias.Rows[index_linha].Cells[4].Value != null && dgvMedias.Rows[index_linha].Cells[5].Value != null &&
                            dgvMedias.Rows[index_linha].Cells[6].Value != null && dgvMedias.Rows[index_linha].Cells[7].Value != null)
                    {
                        DateTime data = DateTime.Parse("01/01/0001");

                        if (DateTime.TryParse(dgvMedias.Rows[linha_atual()].Cells[6].Value.ToString(), out data))
                        {
                            csMediasExtra cs_mediaextra = new csMediasExtra();

                            cs_mediaextra.n_mat = objAluno.NDeMatricula.ToString();

                            if (dgvMedias.Rows[linha_atual()].Cells[9].Value != null)
                                cs_mediaextra.id_media_extra = Convert.ToInt32(dgvMedias.Rows[linha_atual()].Cells[9].Value);

                            if (dgvMedias.Rows[linha_atual()].Cells[2].Value != null)
                                cs_mediaextra.disciplina = dgvMedias.Rows[linha_atual()].Cells[2].Value.ToString();

                            if (dgvMedias.Rows[linha_atual()].Cells[3].Value != null)
                                cs_mediaextra.instituicao = dgvMedias.Rows[linha_atual()].Cells[3].Value.ToString();

                            if (dgvMedias.Rows[linha_atual()].Cells[4].Value != null)
                                cs_mediaextra.cidade = dgvMedias.Rows[linha_atual()].Cells[4].Value.ToString();

                            if (dgvMedias.Rows[linha_atual()].Cells[5].Value != null)
                                cs_mediaextra.uf = dgvMedias.Rows[linha_atual()].Cells[5].Value.ToString();

                            if (dgvMedias.Rows[linha_atual()].Cells[6].Value != null)
                                if (DateTime.TryParse(dgvMedias.Rows[linha_atual()].Cells[6].Value.ToString(), out data))
                                    cs_mediaextra.data = data;

                            if (dgvMedias.Rows[linha_atual()].Cells[7].Value != null)
                                cs_mediaextra.media = dgvMedias.Rows[linha_atual()].Cells[7].Value.ToString();

                            cs_mediaextra.id_ensino = ensino_selecionado.CodDoEnsino;

                            //Retorna o numero gerado da id_media_extra após salvar tanto para insert quanto para update
                            dgvMedias.Rows[linha_atual()].Cells[9].Value = cs_mediaextra.salvar_media_extra(cs_mediaextra);
                        }
                        else
                        {
                            MessageBox.Show("A data final não está em um formato correto.", "Emat-Histórico");
                        }                        
                    }
                    */

                    #endregion
                }

                //Atualiza
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void dgvMedias_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //TODO: AGORA - TESTAR ERRO AO CLICAR COM BOTÃO DIRETOR NO HEADER
            if (e.Button == MouseButtons.Right)
            {
                dgvMedias.CurrentCell = dgvMedias.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dgvMedias.Rows[e.RowIndex].Selected = true;
                dgvMedias.Focus();
            }
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copia = dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[dgvMedias.CurrentCell.ColumnIndex].Value.ToString();
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[dgvMedias.CurrentCell.ColumnIndex].Value = copia;
        }

        private void excluirNotaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
             if(dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[0].Value == null)
             {
                 //EXTRA 
                 if (dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[9].Value != null)
                 {
                     csMediasExtra cs_media_extra = new csMediasExtra();

                     int id_media_extra = Convert.ToInt32(dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[9].Value.ToString());
                     if (cs_media_extra.del_media_extra(id_media_extra) > 0)
                     {
                         dgvMedias.Rows.Remove(dgvMedias.Rows[dgvMedias.CurrentRow.Index]);
                     }
                 }
                 else
                 {
                     dgvMedias.Rows.Remove(dgvMedias.Rows[dgvMedias.CurrentRow.Index]);
                 }
             }
             else
             {
                 //MÉDIA

                 //Excluir Atendimento se houver
                 //id_atendimento
                 int id_atendimento = 0;
                 if (Int32.TryParse(dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[10].Value.ToString(), out id_atendimento))
                 {
                     DAO.AtendimentoDAO.Excluir(id_atendimento);
                 }

                 //Excluir Média

                 DAO.MediaDAO.Excluir(Convert.ToInt32(dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[1].Value));

                 dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[1].Value = null;
                 dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[3].Value = null;
                 dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[4].Value = null;
                 dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[5].Value = null;
                 dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[6].Value = null;
                 dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[7].Value = null;
             }

             */

        }

        private void dtp_dt_con_ValueChanged(object sender, EventArgs e)
        {
            /*
            if (dtp_dt_con.Enabled && objAluno.SituacaoDoAluno.Concluinte)
            {
                if (ensino_selecionado.CodDoEnsino == 1)
                {
                    objAluno.HistoricoEscolarFundamental.DtConclusao = dtp_dt_con.Value;
                    //TODO: AGORA - txtAnoCon.Text utilizar para verificar ?
                }
                else
                {
                    objAluno.HistoricoEscolarMedio.DtConclusao = dtp_dt_con.Value;
                    //TODO: AGORA - txtAnoCon.Text utilizar para verificar ?
                }
            }*/
            btnSalvarHistorico.Enabled = true;

        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmProcurarAluno form = new frmProcurarAluno("historico", _settings);
            form.MdiParent = this.ParentForm;
            form.Show();
            Cursor.Current = Cursors.Default;
            this.Close();
        }

        private void btnAddLinha_Click(object sender, EventArgs e)
        {
            dgvMedias.Rows.Add();

            int ultimalinha = dgvMedias.Rows.Count - 1;

            DataGridViewCell cell = dgvMedias.Rows[ultimalinha].Cells[2];

            dgvMedias.CurrentCell = cell;

            dgvMedias.BeginEdit(true);


        }

        private void dgvMedias_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //Média Extra
            dgvMedias.Rows[dgvMedias.Rows.Count - 1].Cells[8].Value = true;
        }

        private void cmbEnsino_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbEnsinoAluno.Text == "FUNDAMENTAL")
                {
                    cmbPaisAnterior.Enabled = false;
                    cmbUfAnterior.Enabled = false;
                    cmbCidadeAnterior.Enabled = false;
                    txtInstituicaoAnterior.Enabled = false;
                    txtAnoAnterior.Enabled = false;

                }
                else
                {
                    cmbPaisAnterior.Enabled = true;
                    cmbUfAnterior.Enabled = true;
                    cmbCidadeAnterior.Enabled = true;
                    txtInstituicaoAnterior.Enabled = true;
                    txtAnoAnterior.Enabled = true;
                }


                //TODO:: Verificar para remover
                #region Montar Lista de Disciplinas do Ensino
                for (int i = 0; i < EnsinoSistemaEstatica.getListaEnsinosSistema.Count; i++)
                {
                    if (EnsinoSistemaEstatica.getListaEnsinosSistema[i].Ensino == ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).Ensino)
                    {
                        for (int z = 0; z < EnsinoSistemaEstatica.getListaEnsinosSistema[i].ListaDisciplinas.Count; z++)
                        {
                            if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).ListaDisciplinaAluno.FindIndex(x => x.Disciplina.Codigo ==
                                 EnsinoSistemaEstatica.getListaEnsinosSistema[i].ListaDisciplinas[z].Codigo) < 0)
                            {
                                //Não encontrou, deve adicionar uma nova disciplina aluno....
                                DisciplinaAluno disciplinaAluno = new DisciplinaAluno();
                                disciplinaAluno.Codigo = 0;
                                disciplinaAluno.Atual = false;
                                disciplinaAluno.Concluida = false;
                                disciplinaAluno.Disciplina = EnsinoSistemaEstatica.getListaEnsinosSistema[i].ListaDisciplinas[z];
                                disciplinaAluno.EnsinoAluno = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem);
                                ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).ListaDisciplinaAluno.Add(disciplinaAluno);
                                DAO.DisciplinaAlunoDAO.Inserir(disciplinaAluno);
                            }
                        }
                    }
                }
                #endregion

                #region Preencher Medias 

                dgvMedias.Rows.Clear();
                foreach (DisciplinaAluno disciplinaAluno in ((Classes.EnsinoAluno)cmbEnsinoAluno.SelectedItem).ListaDisciplinaAluno.OrderBy(x => x.Disciplina.Ordem))
                {
                    dgvMedias.Rows.Add();
                    dgvMedias.Rows[dgvMedias.Rows.Count - 1].Cells[0].Value = disciplinaAluno;
                    dgvMedias.Rows[dgvMedias.Rows.Count - 1].Cells[1].Value = disciplinaAluno.Disciplina.NomeHistorico;
                    if (disciplinaAluno.Media != null)
                    {
                        dgvMedias.Rows[dgvMedias.Rows.Count - 1].Cells[2].Value = disciplinaAluno.Media.Instituicao;
                        dgvMedias.Rows[dgvMedias.Rows.Count - 1].Cells[3].Value = disciplinaAluno.Media.Cidade.Nome;
                        dgvMedias.Rows[dgvMedias.Rows.Count - 1].Cells[4].Value = disciplinaAluno.Media.Cidade.Uf.Sigla;
                        dgvMedias.Rows[dgvMedias.Rows.Count - 1].Cells[5].Value = DateTime.Parse(disciplinaAluno.Media.DtMedia).ToString("dd/MM/yyyy");
                        dgvMedias.Rows[dgvMedias.Rows.Count - 1].Cells[6].Value = disciplinaAluno.Media.Valor;
                    }
                }
                #endregion

                #region Dados Histórico

                if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar != null)
                {
                    #region Escola Anterior
                    txtSerieAnterior.Text = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.SerieAnterior;
                    txtInstituicaoAnterior.Text = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.InstituicaoAnterior;
                    txtAnoAnterior.Text = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.AnoAnterior.ToString();
                    //Selecionar como no frmAluno
                    cmbPaisAnterior.SelectedValue = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.CidadeAnterior.Uf.Pais.Codigo;
                    cmbUfAnterior.SelectedValue = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.CidadeAnterior.Uf.Codigo;
                    cmbCidadeAnterior.SelectedValue = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.CidadeAnterior.Codigo;
                    #endregion

                    #region Dados Certificado e Histórico
                    if (!String.IsNullOrEmpty(((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.DtDocumento))
                        dtp_dt_doc.Value = DateTime.Parse(((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.DtDocumento);

                    txtObs.Text = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Observacao;

                    DateTime dtInicioEnsino = DateTime.Now;
                    if (DateTime.TryParse(((EnsinoAluno)cmbEnsinoAluno.SelectedItem).DtInicio, out dtInicioEnsino))
                        dtpInicioEnsino.Value = dtInicioEnsino;

                    DateTime dtTerminoEnsino = DateTime.Now;
                    if (DateTime.TryParse(((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.DtConclusao, out dtTerminoEnsino))
                        dtpTerminoEnsino.Value = dtTerminoEnsino;

                    cmbSecretario.SelectedValue = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Secretario.Codigo;

                    cmbDiretor.SelectedValue = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Diretor.Codigo;

                    txtGdae.Text = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Gdae;

                    ckbSegundaVia.Checked = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.SegundaVia;

                    if (_settings.Ceeja.ToLower() == "americana")
                    {
                        if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Fundamentacao != string.Empty)
                            txtFundamentacao.Text = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Fundamentacao;
                        else
                        {
                            txtFundamentacao.Text = "Fundamento Legal: Lei Federal 9394 / 96, Res.SE 77 / 11, Res.SE 81 / 11.";

                            btnSalvarHistorico.PerformClick();
                        }
                    }
                    else if (_settings.Ceeja.ToLower() == "votorantim")
                    {
                        if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Fundamentacao != string.Empty)
                            txtFundamentacao.Text = ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Fundamentacao;
                        else
                        {
                            txtFundamentacao.Text = "Fundamento Legal: Lei Federal 9394/96, Artigos 37 e 38; Resolução CEB n° 03/98; " +
                                " Deliberação CEE 82/09 e Res.SE 3 de 13/01/2010, Res. SE 75/2018, Res. SEED 4817 de 25/10/2013.";
                            btnSalvarHistorico.PerformClick();
                        }
                    }

                    #endregion
                }
                else
                {
                    //Histórico Escolar Novo
                    //Irá criar um histórico e preencher os os dados para ao aluno que ainda não fez o histórico

                    ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar = new HistoricoEscolar();

                    DateTime dtInicioEnsino = DateTime.Now;
                    if (DateTime.TryParse(((EnsinoAluno)cmbEnsinoAluno.SelectedItem).DtInicio, out dtInicioEnsino))
                        dtpInicioEnsino.Value = dtInicioEnsino;

                    //Limpar todos os Campos ja que vai criar um novo historico entende-se que sera tudo limpo
                    txtSerieAnterior.Text = "";
                    txtInstituicaoAnterior.Text = "";
                    txtAnoAnterior.Text = "";
                    cmbPaisAnterior.SelectedValue = -1;
                    cmbUfAnterior.SelectedValue = -1;
                    cmbCidadeAnterior.SelectedValue = -1;
                    dtp_dt_doc.Value = DateTime.Now;
                    txtObs.Text = "";
                    dtpInicioEnsino.Value = DateTime.Now;
                    dtpTerminoEnsino.Value = DateTime.Now;
                    cmbSecretario.SelectedValue = -1;
                    cmbDiretor.SelectedValue = -1;
                    txtGdae.Text = "";
                    ckbSegundaVia.Checked = false;

                    if (_settings.Ceeja.ToLower() == "americana")
                    {
                        if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Fundamentacao != string.Empty)
                            txtFundamentacao.Text = "";
                        else
                        {
                            txtFundamentacao.Text = "Fundamento Legal: Lei Federal 9394 / 96, Res.SE 77 / 11, Res.SE 81 / 11.";
                        }
                    }
                    else if (_settings.Ceeja.ToLower() == "votorantim")
                    {
                        if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.Fundamentacao != string.Empty)
                            txtFundamentacao.Text = "";
                        else
                        {
                            txtFundamentacao.Text = "Fundamento Legal: Lei Federal 9394/96, Artigos 37 e 38; Resolução CEB n° 03/98;  " +
                                "Deliberação CEE 82/09 e Res.SE 3 de 13/01/2010, Res. SE 75/2018, Res. SEED 4817 de 25/10/2013.";
                        }
                    }
                }

                #endregion

                btnSalvarHistorico.Enabled = false;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void cmbSecretario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSecretario.SelectedIndex > -1)
            {
                txtRg_secretario.Text = ((Classes.Usuario)cmbSecretario.SelectedItem).Rg;
                btnSalvarHistorico.Enabled = true;
            }
        }

        private void cmbDiretor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDiretor.SelectedIndex > -1)
            {
                txtRg_diretor.Text = ((Classes.Usuario)cmbDiretor.SelectedItem).Rg;
                btnSalvarHistorico.Enabled = true;
            }
        }

        private void cmbPaisAnterior_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaisAnterior.SelectedIndex > -1)
            {
                cmbUfAnterior.SelectedIndexChanged -= cmbUfAnterior_SelectedIndexChanged;
                BindingSource bsUf = new BindingSource();
                bsUf.DataSource = DAO.UfDAO.ExibirTodos((Classes.Pais)cmbPaisAnterior.SelectedItem);
                cmbUfAnterior.DataSource = bsUf;
                cmbUfAnterior.DisplayMember = "Nome";
                cmbUfAnterior.ValueMember = "Codigo";
                cmbUfAnterior.SelectedIndex = -1;
                cmbUfAnterior.SelectedIndexChanged += cmbUfAnterior_SelectedIndexChanged;

                //Zerar Cidades TODO:: VERIFICAR SE É NECESSÁRIO
                //cmbCidadeAnterior.DataSource = null;

                btnSalvarHistorico.Enabled = true;
            }
        }

        private void CarregarPaisAnterior()
        {
            cmbPaisAnterior.SelectedIndexChanged -= cmbPaisAnterior_SelectedIndexChanged;
            BindingSource bsPaises = new BindingSource();
            bsPaises.DataSource = DAO.PaisDAO.ExibirTodos();
            cmbPaisAnterior.DataSource = bsPaises;
            cmbPaisAnterior.DisplayMember = "Nome";
            cmbPaisAnterior.ValueMember = "Codigo";
            cmbPaisAnterior.SelectedIndex = -1;
            cmbUfAnterior.SelectedIndex = -1;
            cmbCidadeAnterior.SelectedIndex = -1;
            cmbPaisAnterior.SelectedIndexChanged += cmbPaisAnterior_SelectedIndexChanged;
        }

        private void cmbUfAnterior_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUfAnterior.SelectedIndex > -1)
            {
                cmbCidadeAnterior.SelectedIndexChanged -= cmbCidadeAnterior_SelectedIndexChanged;
                BindingSource bsCidade = new BindingSource();
                bsCidade.DataSource = DAO.CidadeDAO.ExibirTodos(((Classes.Uf)cmbUfAnterior.SelectedItem));
                cmbCidadeAnterior.DataSource = bsCidade;
                cmbCidadeAnterior.DisplayMember = "Nome";
                cmbCidadeAnterior.ValueMember = "Codigo";
                cmbCidadeAnterior.SelectedIndex = -1;
                cmbCidadeAnterior.SelectedIndexChanged += cmbCidadeAnterior_SelectedIndexChanged;

                btnSalvarHistorico.Enabled = true;
            }
        }

        private void cmbCidadeAnterior_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCidadeAnterior.SelectedIndex > -1)
            {
                if (((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.CidadeAnterior != null)
                {
                    ((EnsinoAluno)cmbEnsinoAluno.SelectedItem).HistoricoEscolar.CidadeAnterior = ((Classes.Cidade)cmbCidadeAnterior.SelectedItem);
                }
                btnSalvarHistorico.Enabled = true;
            }
        }

        private void cmbCidadeAnterior_Leave(object sender, EventArgs e)
        {
            if (cmbPaisAnterior.Text == "BRASIL")
            {
                if (cmbCidadeAnterior.Text != "")
                {
                    if (cmbCidadeAnterior.FindString(cmbCidadeAnterior.Text) < 0)
                    {
                        MessageBox.Show("Cidade não cadastrada.", "E-matrícula - Histórico Escolar");
                        cmbCidadeAnterior.Text = "";
                        cmbCidadeAnterior.Focus();
                    }
                }
            }
        }

        private void cmbUfAnterior_Leave(object sender, EventArgs e)
        {
            if (cmbPaisAnterior.Text == "BRASIL")
            {
                if (cmbUfAnterior.Text != "")
                {
                    if (cmbUfAnterior.FindString(cmbUfAnterior.Text) < 0)
                    {
                        MessageBox.Show("Estado não cadastrado.", "E-matrícula - Histórico Escolar");
                        cmbUfAnterior.Text = "";
                        cmbUfAnterior.Focus();
                    }
                }
            }
        }

        private void cmbUfAnterior_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void cmbCidadeAnterior_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
                e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private int linha_atual()
        {
            return dgvMedias.CurrentRow.Index;
        }

        private int coluna_atual()
        {
            return dgvMedias.CurrentCell.ColumnIndex;
        }

        private void txtObs_TextChanged(object sender, EventArgs e)
        {
            btnSalvarHistorico.Enabled = true;
        }

        private void txtFundamentacao_TextChanged(object sender, EventArgs e)
        {
            btnSalvarHistorico.Enabled = true;
        }

        private void txtGdae_TextChanged(object sender, EventArgs e)
        {
            btnSalvarHistorico.Enabled = true;
        }

        private void txtAnoCon_TextChanged(object sender, EventArgs e)
        {
            btnSalvarHistorico.Enabled = true;
        }

        private void txtSerie_ant_TextChanged(object sender, EventArgs e)
        {
            btnSalvarHistorico.Enabled = true;
        }

        private void txtInstituicaoAnterior_TextChanged(object sender, EventArgs e)
        {
            btnSalvarHistorico.Enabled = true;
        }

        private void txtAnoAnterior_TextChanged(object sender, EventArgs e)
        {
            btnSalvarHistorico.Enabled = true;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void ckbSegundaVia_CheckedChanged(object sender, EventArgs e)
        {
            btnSalvarHistorico.Enabled = true;
        }

        private void dtp_dt_doc_ValueChanged(object sender, EventArgs e)
        {
            btnSalvarHistorico.Enabled = true;
        }

        private void dtpInicioEnsino_ValueChanged(object sender, EventArgs e)
        {
            //veriricar como será feito a conclusão do aluno
            btnSalvarHistorico.Enabled = true;
        }

        private void cEESSOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:: Verificar Anotação Histórico Incluir Escola na média
            dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[2].Value = _settings.CeejaNome;
            dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[3].Value = _settings.Ceeja;
            dgvMedias.Rows[dgvMedias.CurrentRow.Index].Cells[4].Value = _settings.CeejaCidade;

            salvar_media();
        }

        private void cmbFundamentacaoExtra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFundamentacaoExtra.Text != string.Empty)
            {
                txtObs.Text += " " + cmbFundamentacaoExtra.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmCadastrarEndereco frmCadastrarEndereco = new frmCadastrarEndereco();
            frmCadastrarEndereco.ShowDialog();
            CarregarPaisAnterior();
        }

        private void dgvMedias_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            oldValue = dgvMedias[e.ColumnIndex, e.RowIndex].Value;
        }

        private void btnPadrao_Click(object sender, EventArgs e)
        {
            if (_settings.Ceeja.ToLower() == "americana")
            {
                txtFundamentacao.Text = "Fundamento Legal: Lei Federal 9394 / 96, Res.SE 77 / 11, Res.SE 81 / 11.";
            }
            else if (_settings.Ceeja.ToLower() == "votorantim")
            {
                txtFundamentacao.Text = "Fundamento Legal: Lei Federal 9394/96, Artigos 37 e 38; Resolução CEB n° 03/98;  " +
                        "Deliberação CEE 82/09 e Res.SE 3 de 13/01/2010, Res. SE 75/2018, Res. SEED 4817 de 25/10/2013.";
            }
        }
    }
}
