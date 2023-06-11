namespace EmatWinFormsNetFramework1402.Formularios
{
    partial class frmcsDetalhesDisciplina
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
            this.cmbDisciplina = new System.Windows.Forms.ComboBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.lblQtdDgvAlunos = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbEnsino = new System.Windows.Forms.ComboBox();
            this.dgvAlunos = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOrdenar = new System.Windows.Forms.Button();
            this.cmbOrdenar = new System.Windows.Forms.ComboBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgvDetalhes = new System.Windows.Forms.DataGridView();
            this.colQtd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFundamental = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMedio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ckbAtivos = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlunos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalhes)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDisciplina
            // 
            this.cmbDisciplina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisciplina.FormattingEnabled = true;
            this.cmbDisciplina.Location = new System.Drawing.Point(6, 19);
            this.cmbDisciplina.Name = "cmbDisciplina";
            this.cmbDisciplina.Size = new System.Drawing.Size(189, 21);
            this.cmbDisciplina.TabIndex = 16;
            this.cmbDisciplina.SelectedIndexChanged += new System.EventHandler(this.cmbDisciplina_SelectedIndexChanged);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(235, 19);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 15;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // lblQtdDgvAlunos
            // 
            this.lblQtdDgvAlunos.AutoSize = true;
            this.lblQtdDgvAlunos.Location = new System.Drawing.Point(6, 456);
            this.lblQtdDgvAlunos.Name = "lblQtdDgvAlunos";
            this.lblQtdDgvAlunos.Size = new System.Drawing.Size(65, 13);
            this.lblQtdDgvAlunos.TabIndex = 14;
            this.lblQtdDgvAlunos.Text = "Quantidade:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Ensino";
            // 
            // cmbEnsino
            // 
            this.cmbEnsino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnsino.FormattingEnabled = true;
            this.cmbEnsino.Items.AddRange(new object[] {
            "Todos",
            "Fundamental",
            "Médio"});
            this.cmbEnsino.Location = new System.Drawing.Point(108, 20);
            this.cmbEnsino.Name = "cmbEnsino";
            this.cmbEnsino.Size = new System.Drawing.Size(121, 21);
            this.cmbEnsino.TabIndex = 12;
            // 
            // dgvAlunos
            // 
            this.dgvAlunos.AllowUserToAddRows = false;
            this.dgvAlunos.AllowUserToDeleteRows = false;
            this.dgvAlunos.AllowUserToOrderColumns = true;
            this.dgvAlunos.AllowUserToResizeRows = false;
            this.dgvAlunos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAlunos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvAlunos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlunos.Location = new System.Drawing.Point(6, 47);
            this.dgvAlunos.MultiSelect = false;
            this.dgvAlunos.Name = "dgvAlunos";
            this.dgvAlunos.RowHeadersVisible = false;
            this.dgvAlunos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlunos.Size = new System.Drawing.Size(771, 406);
            this.dgvAlunos.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbAtivos);
            this.groupBox1.Controls.Add(this.btnOrdenar);
            this.groupBox1.Controls.Add(this.cmbOrdenar);
            this.groupBox1.Controls.Add(this.btnImprimir);
            this.groupBox1.Controls.Add(this.dgvAlunos);
            this.groupBox1.Controls.Add(this.btnConsultar);
            this.groupBox1.Controls.Add(this.cmbEnsino);
            this.groupBox1.Controls.Add(this.lblQtdDgvAlunos);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(341, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(783, 472);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Alunos";
            // 
            // btnOrdenar
            // 
            this.btnOrdenar.Location = new System.Drawing.Point(600, 19);
            this.btnOrdenar.Name = "btnOrdenar";
            this.btnOrdenar.Size = new System.Drawing.Size(75, 23);
            this.btnOrdenar.TabIndex = 18;
            this.btnOrdenar.Text = "Ordenar";
            this.btnOrdenar.UseVisualStyleBackColor = true;
            this.btnOrdenar.Click += new System.EventHandler(this.btnOrdenar_Click);
            // 
            // cmbOrdenar
            // 
            this.cmbOrdenar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrdenar.FormattingEnabled = true;
            this.cmbOrdenar.Location = new System.Drawing.Point(450, 20);
            this.cmbOrdenar.Name = "cmbOrdenar";
            this.cmbOrdenar.Size = new System.Drawing.Size(144, 21);
            this.cmbOrdenar.TabIndex = 17;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(702, 18);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 16;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.cmbDisciplina);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(323, 472);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Disciplina";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgvDetalhes);
            this.groupBox6.Location = new System.Drawing.Point(6, 47);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(311, 210);
            this.groupBox6.TabIndex = 25;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Detalhes";
            // 
            // dgvDetalhes
            // 
            this.dgvDetalhes.AllowUserToAddRows = false;
            this.dgvDetalhes.AllowUserToDeleteRows = false;
            this.dgvDetalhes.AllowUserToResizeColumns = false;
            this.dgvDetalhes.AllowUserToResizeRows = false;
            this.dgvDetalhes.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDetalhes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalhes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colQtd,
            this.colFundamental,
            this.colMedio,
            this.colTotal});
            this.dgvDetalhes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalhes.Location = new System.Drawing.Point(3, 16);
            this.dgvDetalhes.Name = "dgvDetalhes";
            this.dgvDetalhes.RowHeadersVisible = false;
            this.dgvDetalhes.Size = new System.Drawing.Size(305, 191);
            this.dgvDetalhes.TabIndex = 0;
            // 
            // colQtd
            // 
            this.colQtd.HeaderText = "Qtd";
            this.colQtd.Name = "colQtd";
            this.colQtd.ReadOnly = true;
            this.colQtd.Width = 70;
            // 
            // colFundamental
            // 
            this.colFundamental.HeaderText = "Fundamental";
            this.colFundamental.Name = "colFundamental";
            this.colFundamental.ReadOnly = true;
            this.colFundamental.Width = 70;
            // 
            // colMedio
            // 
            this.colMedio.HeaderText = "Médio";
            this.colMedio.Name = "colMedio";
            this.colMedio.ReadOnly = true;
            this.colMedio.Width = 70;
            // 
            // colTotal
            // 
            this.colTotal.HeaderText = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            this.colTotal.Width = 70;
            // 
            // ckbAtivos
            // 
            this.ckbAtivos.AutoSize = true;
            this.ckbAtivos.Checked = true;
            this.ckbAtivos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAtivos.Location = new System.Drawing.Point(9, 21);
            this.ckbAtivos.Name = "ckbAtivos";
            this.ckbAtivos.Size = new System.Drawing.Size(55, 17);
            this.ckbAtivos.TabIndex = 19;
            this.ckbAtivos.Text = "Ativos";
            this.ckbAtivos.UseVisualStyleBackColor = true;
            // 
            // frmcsDetalhesDisciplina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 496);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmcsDetalhesDisciplina";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalhes da Disciplina";
            this.Load += new System.EventHandler(this.frmcsDetalhesDisciplina_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlunos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalhes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbDisciplina;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Label lblQtdDgvAlunos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbEnsino;
        private System.Windows.Forms.DataGridView dgvAlunos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgvDetalhes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQtd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFundamental;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMedio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnOrdenar;
        private System.Windows.Forms.ComboBox cmbOrdenar;
        private System.Windows.Forms.CheckBox ckbAtivos;
    }
}