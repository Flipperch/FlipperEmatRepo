using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EmatWinFormsNetFramework1402.Classes;

namespace EmatWinFormsNetFramework1402.Formularios
{
    public partial class frAtribuicaoDisciplina : Form
    {
        //TODO: Revisar código de atribuição de disciplina + atribuição automatica. Tambêm implementar neste mesmo formulário, a a ordenação obrigatória de disciplinas.

        public Aluno objAluno;

        public frAtribuicaoDisciplina()
        {
            InitializeComponent();
        }

        private void frAtribDisciplina_Load(object sender, EventArgs e)
        {
            try
            {
                preencherDadosAluno();
                preencherCmbDisciplinas();
                //MessageBox.Show("Sistema em atualização. Favor atribuir manualmente a próxima disciplina.", "E-matrícula");
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
        }

        private void preencherDadosAluno()
        {
            try
            {
                lblNome.Text = "Nome: " + objAluno.Nome;
                lblRg.Text = "RG: " + objAluno.Rg;
                lblEnsino.Text = "Ensino Atual: " + Classes.Aluno.GetEnsinoAlunoAtual(objAluno).Ensino;
                lblNmat.Text = "Nº de Matrícula: " + objAluno.NMatricula;
                if (Classes.Aluno.GetDisciplinaAlunoAtual(objAluno) != null)
                    lblDisciplinaAtual.Text = "Disciplina Atual: " + Classes.Aluno.GetDisciplinaAlunoAtual(objAluno).Disciplina.Nome;
                ptbFoto.Image = objAluno.FotoDoAluno.Foto;
                ptbFoto.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void preencherCmbDisciplinas()
        {
            try
            {
                List<Disciplina> listaDisciplinasFazer =
                    Aluno.GetEnsinoAlunoAtual(objAluno).ListaDisciplinaAluno.
                    Where(x => x.Concluida == false).
                    Select(x => x.Disciplina).ToList(); ;
                cmbDisciplinas.DataSource = listaDisciplinasFazer;
                cmbDisciplinas.DisplayMember = "Nome";
                cmbDisciplinas.ValueMember = "Codigo";

                //AutoAtribuicao
                if (ckbAutoAtribuicao.Checked)
                {
                    escolher_materia(listaDisciplinasFazer);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnAtribuir_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (cmbDisciplinas.Text != string.Empty)
                {
                    Aluno.setDisciplinaAlunoAtual(objAluno, (Disciplina)cmbDisciplinas.SelectedItem, false);

                    DAO.AlunoDAO.Gravar(objAluno);
                }
                preencherDadosAluno();
               
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void cmbDisciplinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDisciplinas.Text != string.Empty)
                {
                    btnAtribuir.Enabled = true;
                }
                else
                {
                    btnAtribuir.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }

        public void escolher_materia(List<Disciplina> listaDeDisciplinasParaFazer)
        {
            try
            {
                //Excluir materia de horário indisponivel. - ATIVO
                #region Excluir Matéria pelo Horário
                List<int> list_ids_remover = new List<int>();
                if ((DateTime.Now.DayOfWeek == DayOfWeek.Saturday) || (DateTime.Now.DayOfWeek == DayOfWeek.Sunday))
                {

                }
                else
                {
                    if (ckbConsiderarHorarios.Checked)
                    {
                        DateTime hora_atual = new DateTime();
                        hora_atual = DateTime.Now;
                        string hor_aula_m = "m";
                        string hor_aula_t = "t";
                        string hor_aula_n = "n";

                        for (int i = 0; i < listaDeDisciplinasParaFazer.Count; i++)
                        {
                            if (listaDeDisciplinasParaFazer[i].Horario != string.Empty)
                            {
                                string[] todos_horarios = listaDeDisciplinasParaFazer[i].Horario.Split('|');
                                if (todos_horarios.Length > 1)
                                {
                                    hor_aula_m = todos_horarios[0];
                                    hor_aula_t = todos_horarios[1];
                                    hor_aula_n = todos_horarios[2];

                                    string b;

                                    if (DateTime.Parse(hora_atual.ToString("HH:mm")) < DateTime.Parse("12:00"))
                                    {
                                        b = hor_aula_m.Replace("|", string.Empty);
                                    }
                                    else if (DateTime.Parse(hora_atual.ToString("HH:mm")) < DateTime.Parse("18:00") && DateTime.Parse(hora_atual.ToString("HH:mm")) > DateTime.Parse("14:00"))
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
                                        horario_ini = DateTime.Parse(b.Substring(b.IndexOf(hora_atual.ToString("ddd")) + 3, 5));
                                        horario_fin = DateTime.Parse(b.Substring(b.IndexOf(hora_atual.ToString("ddd")) + 11, 5));
                                    }
                                    else
                                    {
                                        horario_ini = DateTime.Parse("00:00");
                                        horario_fin = DateTime.Parse("00:00");
                                    }

                                    if (horario_ini.ToString("HH:mm") == "00:00")
                                    {
                                        //Remover
                                        list_ids_remover.Add(listaDeDisciplinasParaFazer[i].Codigo);
                                    }
                                    else
                                    {
                                        if (DateTime.Parse(hora_atual.ToString("HH:mm")) > DateTime.Parse(horario_ini.ToString("HH:mm")) &&
                                        DateTime.Parse(hora_atual.ToString("HH:mm")) < DateTime.Parse(horario_fin.ToString("HH:mm")))
                                        {

                                        }
                                        else
                                        {
                                            //Remover
                                            list_ids_remover.Add(listaDeDisciplinasParaFazer[i].Codigo);
                                        }
                                    }
                                }
                            }
                        }

                        for (int i = 0; i < list_ids_remover.Count; i++)
                            listaDeDisciplinasParaFazer.RemoveAll(x => x.Codigo == list_ids_remover[i]);
                    }
                }
                #endregion

                //Sistema irá bloquear apenas UMA disciplina

                //Excluir disciplina bloqueada - executa se tiver mais de uma a fazer...
                #region Excluir Matérias bloqueadas

                //verificar se a disciplina bloqueada é a unica restante para o aluno...
                if (listaDeDisciplinasParaFazer.Count > 1)
                {
                    for (int i = 0; i < listaDeDisciplinasParaFazer.Count; i++)
                        if (listaDeDisciplinasParaFazer[i].BloqAtribuicao)
                        {
                            list_ids_remover.Add(listaDeDisciplinasParaFazer[i].Codigo);
                        }

                    for (int i = 0; i < list_ids_remover.Count; i++)
                        listaDeDisciplinasParaFazer.RemoveAll(x => x.Codigo == list_ids_remover[i]);
                }
                #endregion

                //Equilibrar / executa se tiver mais de uma a fazer...
                #region Equilibrar Disciplinas - Diário
                equilibrar(listaDeDisciplinasParaFazer, list_ids_remover);
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void equilibrar(List<Disciplina> listaDeDisciplinasParaFazer, List<int> list_ids_remover)
        {
            try
            {
                //aqui entra o algoritimo fudidão que fará o equilibrio perfeito entre as disciplinas.
                //neste parte, as disciplinas,eliminadas pelo aluno e fora de horário, já foram removidas.

                //O Algorítimo deverá funcionar da seguinte forma.
                //Duas etapas sendo a primeira, a distribuição confomer a quantidade de alunos em cada disciplina.
                //a segunda, o sistema deverá verificar as médias de atendimentos afim de ajustar a porcentagem que cada disciplina
                //poderá comportar.

                //Bloquear disciplina automaticamente quando atingir o limite de capacidade.

                List<DisciplinaDetalhada> lista_obj_abaixo_capacidade = new List<DisciplinaDetalhada>();
                List<DisciplinaDetalhada> lista_obj_acima_capacidade = new List<DisciplinaDetalhada>();
                List<DisciplinaDetalhada> lista_obj_ordernados = new List<DisciplinaDetalhada>();
                List<DisciplinaDetalhada> lista_disciplinas_escolhidas = new List<DisciplinaDetalhada>();


                List<DisciplinaDetalhada> listaDeDisciplinasDoEnsinoAtual = new List<DisciplinaDetalhada>();

                foreach (DisciplinaAluno disciplinaAluno in Aluno.GetEnsinoAlunoAtual(objAluno).ListaDisciplinaAluno)
                {
                    int qtdTotal = Utils.csDetalhesDisciplina.QuantidadeAlunos(Aluno.GetEnsinoAlunoAtual(objAluno), disciplinaAluno.Disciplina);
                    int qtdDia = Utils.csDetalhesDisciplina.QuantidadeAlunos(Aluno.GetEnsinoAlunoAtual(objAluno), disciplinaAluno.Disciplina);

                    listaDeDisciplinasDoEnsinoAtual.Add(new DisciplinaDetalhada(disciplinaAluno.Disciplina, qtdTotal, qtdDia));
                }

                if (listaDeDisciplinasParaFazer.Count > 1)
                {
                    DAO.AlunoDAO alunoDAO = new DAO.AlunoDAO();


                    int qtd_alunos_ativos = 0;


                    for (int i = 0; i < listaDeDisciplinasDoEnsinoAtual.Count; i++)
                    {
                        decimal a = Utils.csDetalhesDisciplina.QuantidadeAlunos(
                            Classes.Aluno.GetEnsinoAlunoAtual(objAluno),
                            listaDeDisciplinasDoEnsinoAtual[i]);

                        decimal b = qtd_alunos_ativos;
                        decimal dec = (a / b) * 100;
                        decimal porcentagem = Math.Truncate(dec);

                        if (listaDeDisciplinasParaFazer.Exists(x => (x.Codigo == listaDeDisciplinasDoEnsinoAtual[i].Codigo)))
                        {
                            if (porcentagem > listaDeDisciplinasDoEnsinoAtual[i].Capacidade) //Disciplinas com Quantidade Acima da Capacidade
                            {
                                lista_obj_acima_capacidade.Add(listaDeDisciplinasDoEnsinoAtual[i]);
                            }
                            else
                            {
                                lista_obj_abaixo_capacidade.Add(listaDeDisciplinasDoEnsinoAtual[i]);
                            }
                        }
                    }

                    lista_obj_ordernados = lista_obj_abaixo_capacidade.OrderByDescending(x => x.QtdeTotal).ToList();

                    if (lista_obj_abaixo_capacidade.Count == 0)
                    {
                        lista_obj_ordernados = lista_obj_acima_capacidade.OrderByDescending(x => x.QtdeTotal).ToList();
                    }
                    else
                    {
                        list_ids_remover.AddRange(lista_obj_acima_capacidade.Select(x => x.Codigo).ToList());
                    }


                    decimal soma_total = lista_obj_ordernados.Sum(x => x.QtdeTotal);
                    decimal media = Math.Truncate(soma_total / lista_obj_ordernados.Count);

                    //Percorrer lista e verificar quais disciplinas estão com menos
                    for (int i = 0; i < lista_obj_ordernados.Count; i++)
                    {
                        if (lista_obj_ordernados[i].QtdeTotal > media)
                        {
                            list_ids_remover.Add(lista_obj_ordernados[i].Codigo);
                        }
                    }

                    for (int i = 0; i < list_ids_remover.Count; i++)
                        listaDeDisciplinasParaFazer.RemoveAll(x => x.Codigo == list_ids_remover[i]);

                    //lista_disciplinas_fazer = lista_disciplinas_fazer.OrderBy(x => x.quantidades.qtd_total).ToList();
                    //listaDeDisciplinasParaFazer = listaDeDisciplinasParaFazer.OrderBy(x => x.QtdeDia).ThenBy(x => x.QtdeTotal).ToList();

                }
                //Escolher Disciplina

                //if (listaDeDisciplinasParaFazer.Count > 0)
                //{
                //    btnSel_mat_1.Text = listaDeDisciplinasParaFazer[0].Nome;
                //    btnSel_mat_1.Enabled = true;
                //}
                //else
                //{
                //    btnSel_mat_1.Text = "-";
                //    btnSel_mat_1.Enabled = false;
                //}
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        class DisciplinaDetalhada : Disciplina
        {
            public int QtdeTotal { get; set; }
            public int QtdeDia { get; set; }

            public DisciplinaDetalhada(Disciplina disciplina, int qtdeTotal, int qtdeDia)
            {
                Codigo = disciplina.Codigo;
                Nome = disciplina.Nome;
                NomeHistorico = disciplina.NomeHistorico;
                Ordem = disciplina.Ordem;
                Horario = disciplina.Horario;
                Capacidade = disciplina.Capacidade;
                BloqAtribuicao = disciplina.BloqAtribuicao;
                ListaAtendimento = disciplina.ListaAtendimento;
                QtdeTotal = qtdeTotal;
                QtdeDia = qtdeDia;
            }

        }

        private DataTable getsortedtable(DataTable tab_materias)
        {
            try
            {
                tab_materias.DefaultView.Sort = "entradas_hoje ASC";
                return tab_materias.DefaultView.ToTable();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }   
}
