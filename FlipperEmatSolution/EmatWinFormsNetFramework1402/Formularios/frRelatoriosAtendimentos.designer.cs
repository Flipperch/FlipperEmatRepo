namespace EmatWinFormsNetFramework1402.Formularios
{
    partial class frRelatoriosAtendimentos
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
            this.dgvQtdAlunosDisciplina = new System.Windows.Forms.DataGridView();
            this.disciplina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fundamental = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.medio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpMatriculas_fin = new System.Windows.Forms.DateTimePicker();
            this.dtpMatriculas_ini = new System.Windows.Forms.DateTimePicker();
            this.dgvQtdAtendimentos = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ckbDetalhado = new System.Windows.Forms.CheckBox();
            this.ckbGrafico = new System.Windows.Forms.CheckBox();
            this.ckbAtendimentos = new System.Windows.Forms.CheckBox();
            this.btnImprimirMatriculas = new System.Windows.Forms.Button();
            this.ckbAlunosporDisciplina = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQtdAlunosDisciplina)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQtdAtendimentos)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvQtdAlunosDisciplina
            // 
            this.dgvQtdAlunosDisciplina.AllowUserToAddRows = false;
            this.dgvQtdAlunosDisciplina.BackgroundColor = System.Drawing.Color.White;
            this.dgvQtdAlunosDisciplina.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQtdAlunosDisciplina.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.disciplina,
            this.fundamental,
            this.medio,
            this.totol});
            this.dgvQtdAlunosDisciplina.Location = new System.Drawing.Point(10, 55);
            this.dgvQtdAlunosDisciplina.Name = "dgvQtdAlunosDisciplina";
            this.dgvQtdAlunosDisciplina.RowHeadersVisible = false;
            this.dgvQtdAlunosDisciplina.Size = new System.Drawing.Size(405, 353);
            this.dgvQtdAlunosDisciplina.TabIndex = 0;
            // 
            // disciplina
            // 
            this.disciplina.HeaderText = "Disciplina";
            this.disciplina.Name = "disciplina";
            this.disciplina.ReadOnly = true;
            // 
            // fundamental
            // 
            this.fundamental.HeaderText = "Fundamental";
            this.fundamental.Name = "fundamental";
            this.fundamental.ReadOnly = true;
            // 
            // medio
            // 
            this.medio.HeaderText = "Médio";
            this.medio.Name = "medio";
            this.medio.ReadOnly = true;
            // 
            // totol
            // 
            this.totol.HeaderText = "Total";
            this.totol.Name = "totol";
            this.totol.ReadOnly = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvQtdAlunosDisciplina);
            this.groupBox3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(443, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(425, 414);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Alunos por Disciplina";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFiltrar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dtpMatriculas_fin);
            this.groupBox1.Controls.Add(this.dtpMatriculas_ini);
            this.groupBox1.Controls.Add(this.dgvQtdAtendimentos);
            this.groupBox1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 414);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Atendimentos";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.Location = new System.Drawing.Point(336, 23);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(51, 27);
            this.btnFiltrar.TabIndex = 14;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(164, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Final";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Inicial";
            // 
            // dtpMatriculas_fin
            // 
            this.dtpMatriculas_fin.Checked = false;
            this.dtpMatriculas_fin.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpMatriculas_fin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpMatriculas_fin.Location = new System.Drawing.Point(210, 22);
            this.dtpMatriculas_fin.MaxDate = new System.DateTime(2080, 12, 31, 0, 0, 0, 0);
            this.dtpMatriculas_fin.Name = "dtpMatriculas_fin";
            this.dtpMatriculas_fin.Size = new System.Drawing.Size(88, 26);
            this.dtpMatriculas_fin.TabIndex = 11;
            this.dtpMatriculas_fin.ValueChanged += new System.EventHandler(this.dtpMatriculas_fin_ValueChanged);
            // 
            // dtpMatriculas_ini
            // 
            this.dtpMatriculas_ini.Checked = false;
            this.dtpMatriculas_ini.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpMatriculas_ini.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpMatriculas_ini.Location = new System.Drawing.Point(58, 23);
            this.dtpMatriculas_ini.MaxDate = new System.DateTime(2080, 12, 31, 0, 0, 0, 0);
            this.dtpMatriculas_ini.Name = "dtpMatriculas_ini";
            this.dtpMatriculas_ini.Size = new System.Drawing.Size(88, 26);
            this.dtpMatriculas_ini.TabIndex = 10;
            this.dtpMatriculas_ini.ValueChanged += new System.EventHandler(this.dtpMatriculas_ini_ValueChanged);
            // 
            // dgvQtdAtendimentos
            // 
            this.dgvQtdAtendimentos.AllowUserToAddRows = false;
            this.dgvQtdAtendimentos.BackgroundColor = System.Drawing.Color.White;
            this.dgvQtdAtendimentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQtdAtendimentos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.dgvQtdAtendimentos.Location = new System.Drawing.Point(10, 55);
            this.dgvQtdAtendimentos.Name = "dgvQtdAtendimentos";
            this.dgvQtdAtendimentos.RowHeadersVisible = false;
            this.dgvQtdAtendimentos.Size = new System.Drawing.Size(405, 353);
            this.dgvQtdAtendimentos.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Disciplina";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Fundamental";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Médio";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Total";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ckbDetalhado);
            this.groupBox2.Controls.Add(this.ckbGrafico);
            this.groupBox2.Controls.Add(this.ckbAtendimentos);
            this.groupBox2.Controls.Add(this.btnImprimirMatriculas);
            this.groupBox2.Controls.Add(this.ckbAlunosporDisciplina);
            this.groupBox2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 432);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(855, 48);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opções Relatório";
            // 
            // ckbDetalhado
            // 
            this.ckbDetalhado.AutoSize = true;
            this.ckbDetalhado.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbDetalhado.Location = new System.Drawing.Point(370, 22);
            this.ckbDetalhado.Name = "ckbDetalhado";
            this.ckbDetalhado.Size = new System.Drawing.Size(88, 24);
            this.ckbDetalhado.TabIndex = 25;
            this.ckbDetalhado.Text = "Detalhado";
            this.ckbDetalhado.UseVisualStyleBackColor = true;
            // 
            // ckbGrafico
            // 
            this.ckbGrafico.AutoSize = true;
            this.ckbGrafico.Enabled = false;
            this.ckbGrafico.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbGrafico.Location = new System.Drawing.Point(294, 22);
            this.ckbGrafico.Name = "ckbGrafico";
            this.ckbGrafico.Size = new System.Drawing.Size(70, 24);
            this.ckbGrafico.TabIndex = 3;
            this.ckbGrafico.Text = "Gráfico";
            this.ckbGrafico.UseVisualStyleBackColor = true;
            // 
            // ckbAtendimentos
            // 
            this.ckbAtendimentos.AutoSize = true;
            this.ckbAtendimentos.Checked = true;
            this.ckbAtendimentos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAtendimentos.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbAtendimentos.Location = new System.Drawing.Point(18, 22);
            this.ckbAtendimentos.Name = "ckbAtendimentos";
            this.ckbAtendimentos.Size = new System.Drawing.Size(110, 24);
            this.ckbAtendimentos.TabIndex = 1;
            this.ckbAtendimentos.Text = "Atendimentos";
            this.ckbAtendimentos.UseVisualStyleBackColor = true;
            // 
            // btnImprimirMatriculas
            // 
            this.btnImprimirMatriculas.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirMatriculas.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.printer_16xLG;
            this.btnImprimirMatriculas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimirMatriculas.Location = new System.Drawing.Point(765, 14);
            this.btnImprimirMatriculas.Name = "btnImprimirMatriculas";
            this.btnImprimirMatriculas.Size = new System.Drawing.Size(81, 28);
            this.btnImprimirMatriculas.TabIndex = 24;
            this.btnImprimirMatriculas.Text = "Imprimir";
            this.btnImprimirMatriculas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimirMatriculas.UseVisualStyleBackColor = true;
            this.btnImprimirMatriculas.Click += new System.EventHandler(this.btnImprimirMatriculas_Click);
            // 
            // ckbAlunosporDisciplina
            // 
            this.ckbAlunosporDisciplina.AutoSize = true;
            this.ckbAlunosporDisciplina.Checked = true;
            this.ckbAlunosporDisciplina.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAlunosporDisciplina.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbAlunosporDisciplina.Location = new System.Drawing.Point(134, 22);
            this.ckbAlunosporDisciplina.Name = "ckbAlunosporDisciplina";
            this.ckbAlunosporDisciplina.Size = new System.Drawing.Size(154, 24);
            this.ckbAlunosporDisciplina.TabIndex = 0;
            this.ckbAlunosporDisciplina.Text = "Alunos por Disciplina";
            this.ckbAlunosporDisciplina.UseVisualStyleBackColor = true;
            // 
            // frRelatoriosAtendimentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 492);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "frRelatoriosAtendimentos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatórios de Atendimentos";
            this.Load += new System.EventHandler(this.frRelatoriosAtendimentos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQtdAlunosDisciplina)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQtdAtendimentos)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvQtdAlunosDisciplina;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvQtdAtendimentos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpMatriculas_fin;
        private System.Windows.Forms.DateTimePicker dtpMatriculas_ini;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckbGrafico;
        private System.Windows.Forms.CheckBox ckbAtendimentos;
        private System.Windows.Forms.Button btnImprimirMatriculas;
        private System.Windows.Forms.CheckBox ckbAlunosporDisciplina;
        private System.Windows.Forms.DataGridViewTextBoxColumn disciplina;
        private System.Windows.Forms.DataGridViewTextBoxColumn fundamental;
        private System.Windows.Forms.DataGridViewTextBoxColumn medio;
        private System.Windows.Forms.DataGridViewTextBoxColumn totol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.CheckBox ckbDetalhado;
        private System.Windows.Forms.Button btnFiltrar;
    }
}