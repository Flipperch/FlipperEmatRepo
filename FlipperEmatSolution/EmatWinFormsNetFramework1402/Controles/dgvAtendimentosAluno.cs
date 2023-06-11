using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EmatWinFormsNetFramework1402.Classes;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.Controles
{
    public partial class dgvAtendimentosAluno : DataGridView
    {
        private readonly IEmatriculaSettings _settings;
        public tlpDisciplinaAluno pai { get; set; }

        public Atendimento AtendimentosSelecionado { get; set; } //Por ser apenas um campo em branco...
        
        /// <summary>
        /// Construir dgvAtendimentosAluno
        /// </summary>
        /// <param name="listaAtendimentosAluno"></param>
        /// <param name="pai"></param>
        /// <param name="individual"></param>
        public dgvAtendimentosAluno(IEmatriculaSettings settings, tlpDisciplinaAluno pai, bool individual = true)
        {
            InitializeComponent();
            _settings = settings;
            this.pai = pai;
            
            // Definir linhas e Colunas do dgvAtendimentosAluno
            //Titulo
            //Columns
            this.Columns.Add("titulo", "");
            this.Columns[0].Width = 60;
            this.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.Columns[0].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            //Bloqueia
            this.Columns[0].ReadOnly = true;
            //Rows
            this.Rows.Add(4);
            this.Rows[0].Cells[0].Value = "objAtendimento";
            this.Rows[0].Visible = false;
            this.Rows[1].Cells[0].Value = "TIPO.";

            //TODO: TESTAR Tornar dinâmico nome da linha MOD para alterar no settings, pois as unidades chamam de diferentes formas.

            this.Rows[2].Cells[0].Value = _settings.UINomenclaturaModuloPassaporte;
            
            this.Rows[3].Cells[0].Value = "NOTA";
                        
            //Preencher AtendimentosAluno
            preencherAtendimentosAluno(individual);
            //Adicionar campo em branco
            if (individual)
                AdicionarNovoCampo();

        }
        
        /// <summary>
        ///  Preenche os atendimentos com base na lista de atendimentos do aluno 
        ///  Mesmo que não tenha atendimento, esse método irá adcionar um campo vazio para o professor selecionar
        /// </summary>
        /// <param name="listaAtendimentosAluno"></param>
        /// <param name="individual"></param>
        public void preencherAtendimentosAluno(bool individual=true)
        {

            pai.DisciplinaAluno.ListaAtendimentoAluno = pai.DisciplinaAluno.ListaAtendimentoAluno.OrderBy(x => x.DtDoAtendimento).ToList();

            //Percore a lista de AtendimentosAluno
            for (int i = 0; i < pai.DisciplinaAluno.ListaAtendimentoAluno.Count; i++)
            {
                // Adcionar nova coluna no dgvAtendimentosAluno
                this.Columns.Add("nota_" + i, "");
                this.Columns[i + 1].Width = 60;
                this.Columns[i + 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //Preencher: linha 0 = objeto AtendimentoAluno
                this.Rows[0].Cells[i + 1].Value = pai.DisciplinaAluno.ListaAtendimentoAluno[i];
                //Preencher: linha 1 = nome AtendimentoAluno
                this.Rows[1].Cells[i + 1].ReadOnly = true;
                this.Rows[1].Cells[i + 1].Value = pai.DisciplinaAluno.ListaAtendimentoAluno[i].Atendimento.Nome;
                //Preencher: linha 2 = modulo AtendimentoAluno
                this.Rows[2].Cells[i + 1].ReadOnly = true;
                this.Rows[2].Cells[i + 1].Value = pai.DisciplinaAluno.ListaAtendimentoAluno[i].Modulo;
                //Menção?                       
                if (pai.DisciplinaAluno.ListaAtendimentoAluno[i].Nota != null)
                {
                    //Preencher: linha 3 = nota AtendimentoAluno
                   if (_settings.Ceeja.ToLower() == "votorantim")
                    {
                        if (pai.DisciplinaAluno.ListaAtendimentoAluno[i].Atendimento.Nome == "ATIVIDADE" ||
                            pai.DisciplinaAluno.ListaAtendimentoAluno[i].Atendimento.Nome == "VÍDEO")
                        {
                            string notaAtividade;

                            if (pai.DisciplinaAluno.ListaAtendimentoAluno[i].Nota.Valor == 3)
                                notaAtividade = "A";
                            else if(pai.DisciplinaAluno.ListaAtendimentoAluno[i].Nota.Valor == 2)
                                notaAtividade = "B";
                            else if (pai.DisciplinaAluno.ListaAtendimentoAluno[i].Nota.Valor == 1)
                                notaAtividade = "C";
                            else
                                notaAtividade = "AC";

                            this.Rows[3].Cells[i + 1].Value = notaAtividade;
                        }
                        else
                        {
                            this.Rows[3].Cells[i + 1].Value = pai.DisciplinaAluno.ListaAtendimentoAluno[i].Nota.Valor;
                        }
                    }
                    else
                    {
                        this.Rows[3].Cells[i + 1].Value = pai.DisciplinaAluno.ListaAtendimentoAluno[i].Nota.Valor;

                        
                    }
                    //Define tipo de campo: Com Menção
                    this.Rows[3].Cells[i + 1].ReadOnly = true;
                    this.Rows[3].Cells[i + 1].Style.BackColor = Color.White;

                    if (Int32.TryParse(this.Rows[3].Cells[i + 1].Value.ToString(), out int valor_))
                    {
                        if (valor_ < 5)
                        {
                            this.Rows[3].Cells[i + 1].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                        }
                        else
                        {
                            this.Rows[3].Cells[i + 1].Style = new DataGridViewCellStyle { ForeColor = Color.Black };
                        }
                    }                    
                }
                else
                {
                    //Define tipo de campo: Sem Menção
                    this.Rows[3].Cells[i + 1].Value = string.Empty;
                    this.Rows[3].Cells[i + 1].ReadOnly = true;
                    this.Rows[3].Cells[i + 1].Style.BackColor = Color.DarkGray;
                }                
                // Média?                            
                if (pai.DisciplinaAluno.ListaAtendimentoAluno[i].Modulo == "MF")
                {
                    //Preencher: linha 3 = média AtendimentoAluno
                    if (pai.DisciplinaAluno.Media != null)
                    {
                        this.Rows[3].Cells[i + 1].Value = pai.DisciplinaAluno.Media.Valor;
                    }
                    
                    //Define tipo de campo: Com Menção //TODO:: VERIRICAR SE PODE TIRAR DEVIDO AO IF ACIMA CONSIDERAR NOTA/MENÇÃO
                    this.Rows[3].Cells[i + 1].ReadOnly = false;
                    this.Rows[3].Cells[i + 1].Style.BackColor = Color.White;                    
                    
                }
            }
        }
        
        /// <summary>
        /// Adiciona novo campo em branco
        /// </summary>
        public void AdicionarNovoCampo()
        {
            if ((this.Rows[1].Cells[this.ColumnCount - 1].Value.ToString() != "ENCERRADO") && 
                    (this.Rows[1].Cells[this.ColumnCount - 1].Value.ToString() != "MÉDIA FINAL"))
            {
                //Colls para inserir nota
                this.Columns.Add("col_btadd", "");
                this.Columns[this.Columns.Count - 1].Width = 60;
                #region  Travar Campos Módulo e Nota
                this.Rows[2].Cells[this.Columns.Count - 1].ReadOnly = true;
                this.Rows[3].Cells[this.Columns.Count - 1].ReadOnly = true;
                #endregion

                Atendimento objTipoAtendimento = new Atendimento();

                List<Atendimento> opcoesAtendimento = pai.DisciplinaAluno.Disciplina.ListaAtendimento;
                
                //Remove a Orientação Inicial das opções caso já tenha sido lançado.
                if (pai.DisciplinaAluno.ListaAtendimentoAluno.Exists(x => x.Atendimento.Nome == "ORIENTAÇÃO INICIAL"))
                    opcoesAtendimento.RemoveAll(x => x.Nome == "ORIENTAÇÃO INICIAL");
                
                //Criar ComboBox de Atendimentos
                DataGridViewComboBoxCell cmb_novo = new DataGridViewComboBoxCell();
                cmb_novo.DataSource = pai.DisciplinaAluno.Disciplina.ListaAtendimento;
                cmb_novo.DisplayMember = "Nome";
                cmb_novo.ValueMember = "Codigo";
                cmb_novo.DropDownWidth = 150;
                this.Rows[1].Cells[this.Columns.Count - 1] = cmb_novo;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="novoAtendimento"></param>
        public void addNovoAtendimento(AtendimentoAluno novoAtendimento)
        {
            if (!pai.DisciplinaAluno.ListaAtendimentoAluno.Exists(x => x.Codigo == novoAtendimento.Codigo))
                pai.DisciplinaAluno.ListaAtendimentoAluno.Add(novoAtendimento);
            if(novoAtendimento.Modulo != "MF")
                AdicionarNovoCampo();
        }
        
        /// <summary>
        /// Excluir AtendimentoAluno da ListaAtendimentoAluno e do DGV
        /// </summary>
        /// <param name="novoAtendimento"></param>
        public void excluirAtendimentoAluno(int columnIndex, AtendimentoAluno atendimentoAluno)
        {
            //Remover Coluna
            this.Columns.RemoveAt(columnIndex);
            //Remover Atendimento da Lista
            pai.DisciplinaAluno.ListaAtendimentoAluno.RemoveAll(x => x.Codigo == atendimentoAluno.Codigo);
        }

        public void updAtendimento(AtendimentoAluno novoAtendimento)
        {

        }

        public void bloqueiaCelula()
        {
            
        }
    }
}
