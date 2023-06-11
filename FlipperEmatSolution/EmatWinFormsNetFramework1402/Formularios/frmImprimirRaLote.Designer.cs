namespace EmatWinFormsNetFramework1402.Formularios
{
    partial class frmImprimirRaLote
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvAlunos = new System.Windows.Forms.DataGridView();
            this.colAluno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colNmat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDtMatricula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblQtdAlunos = new System.Windows.Forms.Label();
            this.lblQtdFolhas = new System.Windows.Forms.Label();
            this.btnSelecionarTudo = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlunos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAlunos
            // 
            this.dgvAlunos.AllowUserToAddRows = false;
            this.dgvAlunos.AllowUserToDeleteRows = false;
            this.dgvAlunos.AllowUserToOrderColumns = true;
            this.dgvAlunos.AllowUserToResizeColumns = false;
            this.dgvAlunos.AllowUserToResizeRows = false;
            this.dgvAlunos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvAlunos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlunos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAluno,
            this.colSelect,
            this.colNmat,
            this.colNome,
            this.colDtMatricula});
            this.dgvAlunos.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgvAlunos.Location = new System.Drawing.Point(12, 41);
            this.dgvAlunos.Name = "dgvAlunos";
            this.dgvAlunos.RowHeadersVisible = false;
            this.dgvAlunos.Size = new System.Drawing.Size(514, 326);
            this.dgvAlunos.TabIndex = 23;
            // 
            // colAluno
            // 
            this.colAluno.HeaderText = "ObjAluno";
            this.colAluno.Name = "colAluno";
            this.colAluno.ReadOnly = true;
            this.colAluno.Visible = false;
            // 
            // colSelect
            // 
            this.colSelect.HeaderText = "";
            this.colSelect.Name = "colSelect";
            this.colSelect.Width = 30;
            // 
            // colNmat
            // 
            this.colNmat.HeaderText = "Nº Matrícula";
            this.colNmat.Name = "colNmat";
            this.colNmat.ReadOnly = true;
            this.colNmat.Width = 90;
            // 
            // colNome
            // 
            this.colNome.HeaderText = "Nome";
            this.colNome.Name = "colNome";
            this.colNome.ReadOnly = true;
            this.colNome.Width = 280;
            // 
            // colDtMatricula
            // 
            this.colDtMatricula.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDtMatricula.HeaderText = "Data Matrícula";
            this.colDtMatricula.Name = "colDtMatricula";
            this.colDtMatricula.ReadOnly = true;
            // 
            // lblQtdAlunos
            // 
            this.lblQtdAlunos.AutoSize = true;
            this.lblQtdAlunos.Location = new System.Drawing.Point(12, 384);
            this.lblQtdAlunos.Name = "lblQtdAlunos";
            this.lblQtdAlunos.Size = new System.Drawing.Size(115, 13);
            this.lblQtdAlunos.TabIndex = 26;
            this.lblQtdAlunos.Text = "Quantidade de Alunos:";
            // 
            // lblQtdFolhas
            // 
            this.lblQtdFolhas.AutoSize = true;
            this.lblQtdFolhas.Location = new System.Drawing.Point(151, 384);
            this.lblQtdFolhas.Name = "lblQtdFolhas";
            this.lblQtdFolhas.Size = new System.Drawing.Size(114, 13);
            this.lblQtdFolhas.TabIndex = 27;
            this.lblQtdFolhas.Text = "Quantidade de Folhas:";
            // 
            // btnSelecionarTudo
            // 
            this.btnSelecionarTudo.Location = new System.Drawing.Point(12, 12);
            this.btnSelecionarTudo.Name = "btnSelecionarTudo";
            this.btnSelecionarTudo.Size = new System.Drawing.Size(108, 23);
            this.btnSelecionarTudo.TabIndex = 28;
            this.btnSelecionarTudo.Text = "Selecionar 50";
            this.btnSelecionarTudo.UseVisualStyleBackColor = true;
            this.btnSelecionarTudo.Click += new System.EventHandler(this.btnSelecionarTudo_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(418, 379);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(108, 22);
            this.btnImprimir.TabIndex = 29;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // frmImprimirRaLote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 414);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnSelecionarTudo);
            this.Controls.Add(this.lblQtdFolhas);
            this.Controls.Add(this.lblQtdAlunos);
            this.Controls.Add(this.dgvAlunos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImprimirRaLote";
            this.ShowIcon = false;
            this.Text = "Impressão RA em Lote";
            this.Load += new System.EventHandler(this.frmImprimirRaLote_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlunos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvAlunos;
        private System.Windows.Forms.Label lblQtdAlunos;
        private System.Windows.Forms.Label lblQtdFolhas;
        private System.Windows.Forms.Button btnSelecionarTudo;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAluno;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNmat;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNome;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDtMatricula;
    }
}