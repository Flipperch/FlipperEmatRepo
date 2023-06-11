using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EmatWinFormsNetFramework1402.Classes;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frmModificarAtendimento : Form
    {
        private readonly IEmatriculaSettings _settings;
        AtendimentoAluno AtendimentoAluno;

        bool excluirNota = false;
        bool excluirMedia = false;

        public frmModificarAtendimento(AtendimentoAluno vlrAtendimentoOrigem, IEmatriculaSettings settings)
        {
            InitializeComponent();
            _settings = settings;
            AtendimentoAluno = vlrAtendimentoOrigem;
        }

        private void frEditarAtendimento_Load(object sender, EventArgs e)
        {
            cmbAtendimentos.SelectedIndexChanged -= cmbAtendimentos_SelectedIndexChanged;
            txtModulo.TextChanged -= txtModulo_TextChanged;
            txtNota.TextChanged -= txtNota_TextChanged;

            //Preencher cmbAtendimentos e Selecionar atendimento atual
            cmbAtendimentos.DataSource = AtendimentoAluno.DisciplinaAluno.Disciplina.ListaAtendimento;
            cmbAtendimentos.DisplayMember = "Nome";
            cmbAtendimentos.ValueMember = "Codigo";

            // index = 0;
            foreach (Classes.Atendimento item in cmbAtendimentos.Items)
            {
                if (item.Codigo == AtendimentoAluno.Atendimento.Codigo)
                {
                    cmbAtendimentos.SelectedItem = item;
                }
            }

            MessageBox.Show(((Classes.Atendimento)cmbAtendimentos.SelectedItem).Nome);

            txtModulo.Text = AtendimentoAluno.Modulo;

            //Preenche vlrMédia ou vlrNota
            if (AtendimentoAluno.Modulo == "MF")
            {
                txtModulo.Enabled = false;
                //Preencher Média
                txtNota.Text = AtendimentoAluno.DisciplinaAluno.Media.Valor;
            }
            else
            {
                //Preencher Nota
                if (AtendimentoAluno.Nota != null)
                    txtNota.Text = AtendimentoAluno.Nota.Valor.ToString();
            }

            configurarTxtNota();

            dtpDtAtendimento.Value = AtendimentoAluno.DtDoAtendimento;

            cmbAtendimentos.SelectedIndexChanged += cmbAtendimentos_SelectedIndexChanged;
            txtModulo.TextChanged += txtModulo_TextChanged;
            txtNota.TextChanged += txtNota_TextChanged;

        }

        private void cmbAtendimentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtModulo.TextAlignChanged -= txtModulo_TextChanged;
            txtNota.TextAlignChanged -= txtNota_TextChanged;
            if (cmbAtendimentos.Text != string.Empty)
            {
                //Definir se irá excluir nota
                if (!((Atendimento)cmbAtendimentos.SelectedItem).Mencao)
                {
                    txtNota.Text = string.Empty;
                    excluirNota = true;
                }
                else
                {
                    excluirMedia = false;
                }

                //Desbloquia campo mod
                txtModulo.Enabled = true;
                txtModulo.Text = string.Empty;

                configurarTxtNota();

                #region TODOS - Automatizar módulo "MF"
                if (cmbAtendimentos.Text == "ENCERRADO" || cmbAtendimentos.Text == "MÉDIA FINAL")
                {
                    //desbloquia campo mod
                    txtModulo.Enabled = false;
                    txtModulo.Text = "MF";

                    //desbloqueia campo nota
                    txtNota.Enabled = true;
                    txtNota.BackColor = Color.White;
                    txtNota.Text = string.Empty;

                }
                #endregion

                #region AMERICANA - Verificar se já existe um atendimento na data de hoje 
                if (_settings.Ceeja.ToLower() == "americana")
                {
                    if (cmbAtendimentos.Text == "ORIENTAÇÃO")
                    {
                        //Atendimento objAtendimento = new Atendimento();
                        List<AtendimentoAluno> list_ = AtendimentoAluno.DisciplinaAluno.ListaAtendimentoAluno;
                        int contador = 0;
                        int i = 0;
                        foreach (var atendimento in list_)
                        {
                            if (list_[i].DtDoAtendimento.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                            {
                                if (list_[i].Atendimento.Nome == "ORIENTAÇÃO")
                                {
                                    contador += 1;
                                    break;
                                }
                            }
                        }

                        if (contador > 0)
                        {
                            MessageBox.Show("Orientação já atribuída na data de hoje.");
                            cmbAtendimentos.Focus(); //TODO: AGORA - VER O QUE FAZER
                        }
                    }
                }

                #endregion

                #region TODOS - Automatizar módulo "AF"
                if (cmbAtendimentos.Text == "AVALIAÇÃO FINAL" ||
                    cmbAtendimentos.Text == "PROVA FINAL")
                {
                    //desbloquia campo mod
                    txtModulo.Enabled = false;
                    txtModulo.Text = "AF";

                    //desbloqueia campo nota
                    txtNota.Enabled = true;
                    txtNota.BackColor = Color.White;
                    txtNota.Text = string.Empty;
                }
                #endregion

            }
            txtModulo.TextAlignChanged += txtModulo_TextChanged;
            txtNota.TextAlignChanged += txtNota_TextChanged;

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            AtendimentoAluno.Atendimento = ((Atendimento)cmbAtendimentos.SelectedItem);
            AtendimentoAluno.Modulo = txtModulo.Text;
            AtendimentoAluno.ProfessorModificouAtendimento = Utils.csUsuarioLogado.professor;
            AtendimentoAluno.DtDaModificaoAtendimento = dtpDtAtendimento.Value;

            float nota_ = 0;

            if (!String.IsNullOrEmpty(txtNota.Text))
            {
                AtendimentoAluno.Nota.Valor = nota_;
                DAO.NotaDAO.Atualizar(AtendimentoAluno);
            }
            else if (AtendimentoAluno.Modulo != "MF")
            {
                AtendimentoAluno.DisciplinaAluno.Media.Valor = txtNota.Text;
                AtendimentoAluno.DisciplinaAluno.Media.UsuarioDeModificacao = Utils.csUsuarioLogado.professor;
                AtendimentoAluno.DisciplinaAluno.Media.DtModificacao = dtpDtAtendimento.Value.ToString();
                DAO.MediaDAO.Atualizar(AtendimentoAluno.DisciplinaAluno);
            }

            if (excluirNota)
            {
                AtendimentoAluno.Nota = null;
                DAO.NotaDAO.Excluir(AtendimentoAluno);
                excluirNota = false;
            }

            if (excluirMedia)
            {
                DAO.MediaDAO.Excluir(AtendimentoAluno.DisciplinaAluno);
                excluirMedia = false;
            }

            //AtualizarAtendimento
            DAO.AtendimentoAlunoDAO.Atualizar(AtendimentoAluno);
        }

        private void txtModulo_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtModulo.Text))
            {
                if (((Atendimento)cmbAtendimentos.SelectedItem).Mencao)
                {
                    txtNota.Enabled = true;
                    txtNota.BackColor = Color.White;
                    btnSalvar.Enabled = false;
                }
                else
                {
                    txtNota.Enabled = false;
                    txtNota.BackColor = Color.DarkGray;
                    btnSalvar.Enabled = true;
                }
            }
        }

        private void txtNota_TextChanged(object sender, EventArgs e)
        {
            if (((Atendimento)cmbAtendimentos.SelectedItem).Mencao)
            {
                if (String.IsNullOrEmpty(txtNota.Text))
                {
                    btnSalvar.Enabled = false;
                }
                else
                {
                    btnSalvar.Enabled = true;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(txtNota.Text))
                {
                    btnSalvar.Enabled = true;
                }
                else
                {

                    btnSalvar.Enabled = false;
                }
            }
        }

        private void configurarTxtNota()
        {
            if (((Atendimento)cmbAtendimentos.SelectedItem).Mencao)
            {
                //LIBERA CAMPO NOTA
                txtNota.Enabled = true;
                txtNota.Text = string.Empty;
                txtNota.BackColor = Color.White;
            }
            else
            {
                //TRAVA CAMPO NOTA                      
                txtNota.Enabled = false;
                txtNota.BackColor = Color.DarkGray;

                if (txtNota.Text != string.Empty)
                {
                    //LIMPA CAMPO NOTA
                    txtNota.Text = string.Empty;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    //TODO:: Verificar código a baixo
    #region UPDATE MÓDULO
    //if (dgv_.Rows[1].Cells[indexColAtual].Value.ToString() == "ORIENTAÇÃO INICIAL") // Manter a data original case seja alterado o modulo
    //{
    //    //dtp_orient_ini.Value = DateTime.Now;
    //    //cs_notas.add_orient_ini(n_mat, cs_alunos.ensino(n_mat), cs_disciplinas.troca_disciplina_id_por_nome(id_disciplina_dgv_atual), DateTime.Now.ToString());
    //    cs_atendimentos.modificaAtendimento(Convert.ToInt32(dgv_.Rows[0].Cells[indexColAtual].Value),
    //                                                    cs_tipoatendimento.troca_nome_tipoatend_id(dgv_.Rows[1].Cells[indexColAtual].Value.ToString(), tlp.DisciplinaDaTabela.CodDaDisciplina),
    //                                                    dgv_.Rows[5].Cells[indexColAtual].Value.ToString(),
    //                                                    dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(),
    //                                                    Utils.csUsuarioLogado.usuario.CodDoUsuario,
    //                                                    n_mat,
    //                                                    ensinoSelecionado.CodDoEnsino);
    //}
    //else
    //{
    //    cs_atendimentos.modificaAtendimento(Convert.ToInt32(dgv_.Rows[0].Cells[indexColAtual].Value),
    //                                                    cs_tipoatendimento.troca_nome_tipoatend_id(dgv_.Rows[1].Cells[indexColAtual].Value.ToString(), tlp.DisciplinaDaTabela.CodDaDisciplina),
    //                                                    DateTime.Now.ToString(),
    //                                                    dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(),
    //                                                    Utils.csUsuarioLogado.usuario.CodDoUsuario,
    //                                                    n_mat,
    //                                                    ensinoSelecionado.CodDoEnsino);
    //}
    //
    //this.BeginInvoke(new MethodInvoker(() =>
    //{
    //    preencherPassaporteIndividual(ensinoSelecionado.CodDoEnsino, flpPai); //lag
    //
    //    //add_campo_branco(dgv_);
    //}));
    //repreencherPassaporteCompleto(ensinoSelecionado.CodDoEnsino, tlp.DisciplinaDaTabela.CodDaDisciplina);
    #endregion

    #region UPDATE MEDIA
    //#region Atualizar Atendimento
    //cs_atendimentos.modificaAtendimento(Convert.ToInt32(dgv_.Rows[0].Cells[indexColAtual].Value),
    //                                                cs_tipoatendimento.troca_nome_tipoatend_id(dgv_.Rows[1].Cells[indexColAtual].Value.ToString(), tlp.DisciplinaDaTabela.CodDaDisciplina),
    //                                                DateTime.Now.ToString(),
    //                                                dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(),
    //                                                Utils.csUsuarioLogado.usuario.CodDoUsuario,
    //                                                n_mat,
    //                                                ensinoSelecionado.CodDoEnsino);
    //#endregion
    //
    //if (cs_notas.existe_media(Convert.ToInt32(dgv_.Rows[0].Cells[indexColAtual].Value)))
    //{  
    //    #region Atualizar Média                                         
    //    cs_notas.upd_media_final(Configurações.csEscola.escola,
    //        Configurações.csEscola.cidade,
    //        Configurações.csEscola.sigla_estado,
    //        dgv_.Rows[3].Cells[indexColAtual].Value.ToString(),
    //        DateTime.Now.ToString("dd/MM/yyyy"),
    //        Utils.csUsuarioLogado.usuario.CodDoUsuario);
    //    #endregion
    //
    //    #region AtualizaDGV
    //    preencherPassaporteIndividual(ensinoSelecionado.CodDoEnsino, flpPai);
    //    repreencherPassaporteCompleto(ensinoSelecionado.CodDoEnsino, tlp.DisciplinaDaTabela.CodDaDisciplina);
    //    add_disciplina_flpGrade(ensinoSelecionado.CodDoEnsino);
    //    #endregion
    //
    //    #region Nova Disciplina
    //    atrib_nova_disciplina();
    //    #endregion
    //}
    //else
    //{
    //   ////Add - caso seja um sem menção para um que tenha
    //   //
    //   //#region Inserir Nova Média
    //   //Media objMedia = new Media();
    //   //objMedia.CodDoAtendimentoDaMedia = cod
    //   //objMedia.EnsinoDaMedia = ensinoSelecionado;
    //   //objMedia.DisciplinaDaMedia = tlp.DisciplinaDaTabela;
    //   //objMedia.Instituicao = Configurações.csEscola.escola.ToUpper();
    //   //objMedia.Municipio = Configurações.csEscola.cidade.ToUpper();
    //   //objMedia.Uf = Configurações.csEscola.sigla_estado.ToUpper();
    //   //objMedia.UsuarioDeCadastro = objProfessor;
    //   //objMedia.CodDoAtendimentoDaMedia = Convert.ToInt32(dgv_.Rows[0].Cells[indexColAtual].Value);
    //   //objMedia.VlrMedia = dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString();
    //   //objMedia.DtInicial = tlp.DataOrientacaoInicialDoAlunoNaDisciplina;
    //   //objMedia.UsuarioDeCadastro = objProfessor;
    //   //#endregion
    //   //
    //    //#region Inserir Nova Média
    //    //cs_notas.add_media_final_NOVO(cs_atendimentos.id_ultimo_atendimento(n_mat, ensinoSelecionado.CodDoEnsino),
    //    //    tlp.DisciplinaDaTabela.CodDaDisciplina,
    //    //    n_mat,
    //    //    cs_disciplinas.troca_ensino_nome_por_id(cs_alunos.ensino(n_mat)),
    //    //    dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(),
    //    //    tlp.DataOrientacaoInicialDoAlunoNaDisciplina.ToString("dd/MM/yyyy"),
    //    //    Utils.csUsuarioLogado.usuario.CodDoUsuario);
    //    //
    //    //#endregion
    //    //AtualizaDGV
    //    preencherPassaporteIndividual(ensinoSelecionado.CodDoEnsino, flpPai);
    //    repreencherPassaporteCompleto(ensinoSelecionado.CodDoEnsino, tlp.DisciplinaDaTabela.CodDaDisciplina);
    //
    //    //dgv_.Columns.Remove("col_btadd");
    //
    //    //Nova Disciplina
    //    atrib_nova_disciplina();
    //
    //}
    //preencherPassaporteIndividual(ensinoSelecionado.CodDoEnsino, flpPai);
    //repreencherPassaporteCompleto(ensinoSelecionado.CodDoEnsino, tlp.DisciplinaDaTabela.CodDaDisciplina);
    #endregion

    #region UPDATE NOTA
    ////Modifica
    //cs_atendimentos.modificaAtendimento(Convert.ToInt32(dgv_.Rows[0].Cells[indexColAtual].Value),
    //                                                cs_tipoatendimento.troca_nome_tipoatend_id(dgv_.Rows[1].Cells[indexColAtual].Value.ToString(), tlp.DisciplinaDaTabela.CodDaDisciplina),
    //                                                DateTime.Now.ToString(),
    //                                                dgv_.Rows[2].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString(),
    //                                                Utils.csUsuarioLogado.usuario.CodDoUsuario,
    //                                                n_mat,
    //                                                ensinoSelecionado.CodDoEnsino);

    //TODO: AGORA - TESTAR
    //if (Classes.Nota.ExisteNota(Convert.ToInt32(dgv_.Rows[0].Cells[indexColAtual].Value)))
    //{
    //    //Mod
    //    cs_notas.modificaNota(Convert.ToInt32(dgv_.Rows[0].Cells[indexColAtual].Value),
    //    float.Parse(dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString()),
    //    Utils.csUsuarioLogado.usuario.CodDoUsuario,
    //    DateTime.Now.ToString());
    //}
    //else
    //{
    //    //Grava nota usando o valor real da celula 'id_atendimento" 
    //    //Add - caso seja um sem menção para um que tenha
    //    #region Inserir Nova Nota
    //
    //    //cs_notas.adcionaNota(Convert.ToInt32(dgv_.Rows[0].Cells[indexColAtual].Value),
    //    //                      float.Parse(dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString()),
    //    //                      Utils.csUsuarioLogado.usuario.CodDoUsuario,
    //    //                      DateTime.Now.ToString());
    //
    //    objNota = new Classes.Nota();
    //    objNota.CodDoAtendimentoDaNota = Convert.ToInt32(dgv_.Rows[0].Cells[indexColAtual].Value);
    //    objNota.VlrDaNota = float.Parse(dgv_.Rows[3].Cells[dgv_.CurrentCell.ColumnIndex].Value.ToString());
    //    int codNovaNota = objNota.InserirNota(objNota);
    //
    //    #endregion                                            
    //}
    //this.BeginInvoke(new MethodInvoker(() =>
    //{
    //    preencherPassaporteIndividual(ensinoSelecionado.CodDoEnsino, flpPai); //Lags
    //    //add_campo_branco(dgv_);
    //}));
    //repreencherPassaporteCompleto(ensinoSelecionado.CodDoEnsino, tlp.DisciplinaDaTabela.CodDaDisciplina);
    #endregion
}