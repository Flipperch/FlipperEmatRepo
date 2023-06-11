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
    /// <summary>
    /// Descrição: Constroi um TableLayoutPanel contendo
    /// Label: Informando a Disciplina
    /// DateTimePicker: Informando data da orientação Inicial
    /// /DataGridView: Contendo todos os Atendimentos
    /// </summary>
    public partial class tlpDisciplinaAluno : TableLayoutPanel
    {
        private readonly IEmatriculaSettings _settings;
        //Visual             
        public FlowLayoutPanel flpOrientacaoInicial { get; set; }        
        public DateTimePicker dtpOrientacaoInicial { get; set; }      
        public dgvAtendimentosAluno dgvAtendimentosAluno { get; set; }        
        //Não Visual
        public DisciplinaAluno DisciplinaAluno { get; set; }
        /// <summary>
        /// Construir tlpDisciplinaAluno
        /// </summary>
        /// <param name="disciplinaAluno">DisciplinaAluno que esta lincada ao controle</param>
        public tlpDisciplinaAluno(DisciplinaAluno disciplinaAluno, IEmatriculaSettings settings)
        {
            InitializeComponent();
            _settings = settings;
            //Definir DisciplinaAluno
            DisciplinaAluno = disciplinaAluno;
            //Definir Linhas e Colunas
            this.RowCount = 2;
            this.ColumnCount = 2;          
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            this.RowStyles.Add(new RowStyle());            
            this.ColumnStyles.Add(new ColumnStyle());
            this.ColumnStyles.Add(new ColumnStyle());
            this.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            //Construir lblNomeDisciplina
            Label lblNomeDisciplina = new Label();
            lblNomeDisciplina.Text = disciplinaAluno.Disciplina.Nome;
            lblNomeDisciplina.AutoSize = true;
            lblNomeDisciplina.Font = new Font("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblNomeDisciplina.Dock = DockStyle.Left;
            lblNomeDisciplina.TextAlign = ContentAlignment.MiddleLeft;
            //Construir flpOrientacaoInicial
            flpOrientacaoInicial = new FlowLayoutPanel();
            flpOrientacaoInicial.Dock = DockStyle.Fill;
            flpOrientacaoInicial.FlowDirection = FlowDirection.RightToLeft;
            flpOrientacaoInicial.Padding = new Padding(0);
            flpOrientacaoInicial.Margin = new Padding(0);
            //Construir dtpOrientacaoInicial
            DateTimePicker dtpOrientacaoInicial = new DateTimePicker();
            dtpOrientacaoInicial.Dock = DockStyle.Right;
            dtpOrientacaoInicial.Format = DateTimePickerFormat.Short;
            dtpOrientacaoInicial.Margin = new Padding(2);
            dtpOrientacaoInicial.Width = 100;
            dtpOrientacaoInicial.MaxDate = DateTime.Now;

            flpOrientacaoInicial.Controls.Add(dtpOrientacaoInicial);
            //Construir lblOrientacaoInicial
            Label lblOrientacaoInicial = new Label();
            lblOrientacaoInicial.AutoSize = true;
            lblOrientacaoInicial.Text = "Orient. Inicial:";
            lblOrientacaoInicial.Font = new Font("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            lblOrientacaoInicial.Dock = DockStyle.Right;
            lblOrientacaoInicial.Margin = new Padding(0, 5, 0, 0);
            flpOrientacaoInicial.Controls.Add(lblOrientacaoInicial);
            //Construir dgvAtendimentosAluno
            dgvAtendimentosAluno = new dgvAtendimentosAluno(_settings, this);
            dgvAtendimentosAluno.BackgroundColor = Color.White;
            dgvAtendimentosAluno.AllowUserToAddRows = false;
            dgvAtendimentosAluno.AllowUserToResizeRows = false;
            dgvAtendimentosAluno.RowHeadersVisible = false;
            dgvAtendimentosAluno.ColumnHeadersVisible = false;
            dgvAtendimentosAluno.Dock = DockStyle.Fill;
            dgvAtendimentosAluno.AllowUserToResizeRows = false;
            dgvAtendimentosAluno.AllowUserToResizeColumns = false;
            dgvAtendimentosAluno.MultiSelect = false;
            this.SetColumnSpan(dgvAtendimentosAluno, 2);
            this.Controls.Add(lblNomeDisciplina, 0, 0);        
            this.Controls.Add(dgvAtendimentosAluno, 0, 1);          
            this.Controls.Add(flpOrientacaoInicial, 1, 0);
            dgvAtendimentosAluno.pai = this;
            if (disciplinaAluno.Atual)
            {
                this.BorderStyle = BorderStyle.FixedSingle;
                lblNomeDisciplina.ForeColor = Color.Red;

            }
                

        }
        //public void setarDataOrientacaoInicial(DateTime vlrData)
        //{
        //    foreach(DateTimePicker dtp in this.flpOrientacaoInicial.Controls.OfType<DateTimePicker>())
        //    {
        //        dataOrientacaoInicialAlunoDisciplina = vlrData;
        //        dtp.Value = vlrData;
        //    }
        //}
    }
}
