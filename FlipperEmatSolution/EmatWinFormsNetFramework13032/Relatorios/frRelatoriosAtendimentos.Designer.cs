namespace EmatWinFormsNetFramework13032.Relatorios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btLimpar = new System.Windows.Forms.Button();
            this.gbParametros = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSel_disciplina = new System.Windows.Forms.ComboBox();
            this.ckbMencao = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ckbCon_disciplina = new System.Windows.Forms.CheckBox();
            this.cmbSel_periodo = new System.Windows.Forms.ComboBox();
            this.ckbInativos_disciplina = new System.Windows.Forms.CheckBox();
            this.cmbSel_ensino = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtInstrucao = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpAtend_fin = new System.Windows.Forms.DateTimePicker();
            this.dtpAtend_ini = new System.Windows.Forms.DateTimePicker();
            this.lblQtd_atendimentos = new System.Windows.Forms.Label();
            this.dgvAlunos_disciplina = new System.Windows.Forms.DataGridView();
            this.btPrint_atend = new System.Windows.Forms.Button();
            this.gbParametros.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlunos_disciplina)).BeginInit();
            this.SuspendLayout();
            // 
            // btLimpar
            // 
            this.btLimpar.Location = new System.Drawing.Point(749, 21);
            this.btLimpar.Name = "btLimpar";
            this.btLimpar.Size = new System.Drawing.Size(60, 31);
            this.btLimpar.TabIndex = 23;
            this.btLimpar.Text = "Limpar";
            this.btLimpar.UseVisualStyleBackColor = true;
            // 
            // gbParametros
            // 
            this.gbParametros.Controls.Add(this.label3);
            this.gbParametros.Controls.Add(this.cmbSel_disciplina);
            this.gbParametros.Controls.Add(this.ckbMencao);
            this.gbParametros.Controls.Add(this.label9);
            this.gbParametros.Controls.Add(this.ckbCon_disciplina);
            this.gbParametros.Controls.Add(this.cmbSel_periodo);
            this.gbParametros.Controls.Add(this.ckbInativos_disciplina);
            this.gbParametros.Controls.Add(this.cmbSel_ensino);
            this.gbParametros.Controls.Add(this.label8);
            this.gbParametros.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbParametros.Location = new System.Drawing.Point(9, 10);
            this.gbParametros.Name = "gbParametros";
            this.gbParametros.Size = new System.Drawing.Size(461, 130);
            this.gbParametros.TabIndex = 27;
            this.gbParametros.TabStop = false;
            this.gbParametros.Text = "Parâmetros";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Disciplina";
            // 
            // cmbSel_disciplina
            // 
            this.cmbSel_disciplina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSel_disciplina.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSel_disciplina.FormattingEnabled = true;
            this.cmbSel_disciplina.Location = new System.Drawing.Point(83, 20);
            this.cmbSel_disciplina.Name = "cmbSel_disciplina";
            this.cmbSel_disciplina.Size = new System.Drawing.Size(199, 28);
            this.cmbSel_disciplina.TabIndex = 0;
            // 
            // ckbMencao
            // 
            this.ckbMencao.AutoSize = true;
            this.ckbMencao.Enabled = false;
            this.ckbMencao.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbMencao.Location = new System.Drawing.Point(311, 88);
            this.ckbMencao.Name = "ckbMencao";
            this.ckbMencao.Size = new System.Drawing.Size(126, 24);
            this.ckbMencao.TabIndex = 5;
            this.ckbMencao.Text = "Apenas Menção";
            this.ckbMencao.UseVisualStyleBackColor = true;
            this.ckbMencao.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 20);
            this.label9.TabIndex = 21;
            this.label9.Text = "Ensino";
            // 
            // ckbCon_disciplina
            // 
            this.ckbCon_disciplina.AutoSize = true;
            this.ckbCon_disciplina.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbCon_disciplina.Location = new System.Drawing.Point(311, 20);
            this.ckbCon_disciplina.Name = "ckbCon_disciplina";
            this.ckbCon_disciplina.Size = new System.Drawing.Size(149, 24);
            this.ckbCon_disciplina.TabIndex = 3;
            this.ckbCon_disciplina.Text = "Apenas Concluidos";
            this.ckbCon_disciplina.UseVisualStyleBackColor = true;
            // 
            // cmbSel_periodo
            // 
            this.cmbSel_periodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSel_periodo.Enabled = false;
            this.cmbSel_periodo.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSel_periodo.FormattingEnabled = true;
            this.cmbSel_periodo.Items.AddRange(new object[] {
            "",
            "MANHÃ",
            "TARDE",
            "NOITE"});
            this.cmbSel_periodo.Location = new System.Drawing.Point(83, 54);
            this.cmbSel_periodo.Name = "cmbSel_periodo";
            this.cmbSel_periodo.Size = new System.Drawing.Size(199, 28);
            this.cmbSel_periodo.TabIndex = 1;
            // 
            // ckbInativos_disciplina
            // 
            this.ckbInativos_disciplina.AutoSize = true;
            this.ckbInativos_disciplina.Enabled = false;
            this.ckbInativos_disciplina.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbInativos_disciplina.Location = new System.Drawing.Point(311, 54);
            this.ckbInativos_disciplina.Name = "ckbInativos_disciplina";
            this.ckbInativos_disciplina.Size = new System.Drawing.Size(121, 24);
            this.ckbInativos_disciplina.TabIndex = 4;
            this.ckbInativos_disciplina.Text = "Incluir Inativos";
            this.ckbInativos_disciplina.UseVisualStyleBackColor = true;
            this.ckbInativos_disciplina.Visible = false;
            // 
            // cmbSel_ensino
            // 
            this.cmbSel_ensino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSel_ensino.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSel_ensino.FormattingEnabled = true;
            this.cmbSel_ensino.Items.AddRange(new object[] {
            "",
            "FUNDAMENTAL",
            "MÉDIO"});
            this.cmbSel_ensino.Location = new System.Drawing.Point(82, 88);
            this.cmbSel_ensino.Name = "cmbSel_ensino";
            this.cmbSel_ensino.Size = new System.Drawing.Size(199, 28);
            this.cmbSel_ensino.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Período";
            // 
            // txtInstrucao
            // 
            this.txtInstrucao.Location = new System.Drawing.Point(476, 114);
            this.txtInstrucao.Name = "txtInstrucao";
            this.txtInstrucao.Size = new System.Drawing.Size(247, 20);
            this.txtInstrucao.TabIndex = 28;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.dtpAtend_fin);
            this.groupBox3.Controls.Add(this.dtpAtend_ini);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(476, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(247, 85);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data de Atendimento";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(122, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Final";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Diário / Inicial";
            // 
            // dtpAtend_fin
            // 
            this.dtpAtend_fin.Checked = false;
            this.dtpAtend_fin.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpAtend_fin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAtend_fin.Location = new System.Drawing.Point(126, 45);
            this.dtpAtend_fin.Name = "dtpAtend_fin";
            this.dtpAtend_fin.ShowCheckBox = true;
            this.dtpAtend_fin.Size = new System.Drawing.Size(110, 26);
            this.dtpAtend_fin.TabIndex = 1;
            // 
            // dtpAtend_ini
            // 
            this.dtpAtend_ini.Checked = false;
            this.dtpAtend_ini.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpAtend_ini.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAtend_ini.Location = new System.Drawing.Point(10, 45);
            this.dtpAtend_ini.Name = "dtpAtend_ini";
            this.dtpAtend_ini.ShowCheckBox = true;
            this.dtpAtend_ini.Size = new System.Drawing.Size(110, 26);
            this.dtpAtend_ini.TabIndex = 0;
            // 
            // lblQtd_atendimentos
            // 
            this.lblQtd_atendimentos.AutoSize = true;
            this.lblQtd_atendimentos.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtd_atendimentos.Location = new System.Drawing.Point(745, 123);
            this.lblQtd_atendimentos.Name = "lblQtd_atendimentos";
            this.lblQtd_atendimentos.Size = new System.Drawing.Size(120, 20);
            this.lblQtd_atendimentos.TabIndex = 25;
            this.lblQtd_atendimentos.Text = "qtd_atendimentos";
            // 
            // dgvAlunos_disciplina
            // 
            this.dgvAlunos_disciplina.AllowUserToAddRows = false;
            this.dgvAlunos_disciplina.AllowUserToDeleteRows = false;
            this.dgvAlunos_disciplina.AllowUserToResizeColumns = false;
            this.dgvAlunos_disciplina.AllowUserToResizeRows = false;
            this.dgvAlunos_disciplina.ColumnHeadersHeight = 25;
            this.dgvAlunos_disciplina.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlunos_disciplina.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAlunos_disciplina.Location = new System.Drawing.Point(9, 146);
            this.dgvAlunos_disciplina.Name = "dgvAlunos_disciplina";
            this.dgvAlunos_disciplina.ReadOnly = true;
            this.dgvAlunos_disciplina.RowHeadersVisible = false;
            this.dgvAlunos_disciplina.Size = new System.Drawing.Size(868, 277);
            this.dgvAlunos_disciplina.TabIndex = 24;
            // 
            // btPrint_atend
            // 
            this.btPrint_atend.Enabled = false;
            this.btPrint_atend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPrint_atend.Image = global::EmatWinFormsNetFramework13032.Properties.Resources.printer_16xLG;
            this.btPrint_atend.Location = new System.Drawing.Point(846, 429);
            this.btPrint_atend.Name = "btPrint_atend";
            this.btPrint_atend.Size = new System.Drawing.Size(35, 28);
            this.btPrint_atend.TabIndex = 29;
            this.btPrint_atend.UseVisualStyleBackColor = true;
            // 
            // frRelatoriosAtendimentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 483);
            this.Controls.Add(this.btPrint_atend);
            this.Controls.Add(this.btLimpar);
            this.Controls.Add(this.gbParametros);
            this.Controls.Add(this.txtInstrucao);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lblQtd_atendimentos);
            this.Controls.Add(this.dgvAlunos_disciplina);
            this.Name = "frRelatoriosAtendimentos";
            this.Text = "frRelatoriosAtendimentos";
            this.Load += new System.EventHandler(this.frRelatoriosAtendimentos_Load);
            this.gbParametros.ResumeLayout(false);
            this.gbParametros.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlunos_disciplina)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btLimpar;
        private System.Windows.Forms.GroupBox gbParametros;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSel_disciplina;
        private System.Windows.Forms.CheckBox ckbMencao;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox ckbCon_disciplina;
        private System.Windows.Forms.ComboBox cmbSel_periodo;
        private System.Windows.Forms.CheckBox ckbInativos_disciplina;
        private System.Windows.Forms.ComboBox cmbSel_ensino;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtInstrucao;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpAtend_fin;
        private System.Windows.Forms.DateTimePicker dtpAtend_ini;
        private System.Windows.Forms.Label lblQtd_atendimentos;
        private System.Windows.Forms.DataGridView dgvAlunos_disciplina;
        private System.Windows.Forms.Button btPrint_atend;

    }
}