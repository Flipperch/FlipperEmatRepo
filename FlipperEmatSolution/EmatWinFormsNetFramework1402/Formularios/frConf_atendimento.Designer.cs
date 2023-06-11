namespace EmatWinFormsNetFramework1402.Formularios
{
    partial class frConf_atendimento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frConf_atendimento));
            this.cmbDisciplina = new System.Windows.Forms.ComboBox();
            this.ckbTem_Nota = new System.Windows.Forms.CheckBox();
            this.nudNota = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTipo_Atendimento = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblEnsino = new System.Windows.Forms.Label();
            this.cmbEnsino = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFormula = new System.Windows.Forms.TextBox();
            this.btNovo = new System.Windows.Forms.Button();
            this.btAdd_Tipodisciplina = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.ltvTipos_Atendimento = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.nudNota)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDisciplina
            // 
            this.cmbDisciplina.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisciplina.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDisciplina.FormattingEnabled = true;
            this.cmbDisciplina.Location = new System.Drawing.Point(12, 56);
            this.cmbDisciplina.Name = "cmbDisciplina";
            this.cmbDisciplina.Size = new System.Drawing.Size(235, 28);
            this.cmbDisciplina.TabIndex = 0;
            this.cmbDisciplina.DropDown += new System.EventHandler(this.cmbDisciplina_DropDown);
            this.cmbDisciplina.SelectedIndexChanged += new System.EventHandler(this.cmbDisciplina_SelectedIndexChanged);
            // 
            // ckbTem_Nota
            // 
            this.ckbTem_Nota.AutoSize = true;
            this.ckbTem_Nota.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbTem_Nota.Location = new System.Drawing.Point(12, 163);
            this.ckbTem_Nota.Name = "ckbTem_Nota";
            this.ckbTem_Nota.Size = new System.Drawing.Size(62, 24);
            this.ckbTem_Nota.TabIndex = 2;
            this.ckbTem_Nota.Text = "Nota";
            this.ckbTem_Nota.UseVisualStyleBackColor = true;
            this.ckbTem_Nota.CheckedChanged += new System.EventHandler(this.ckbTem_Nota_CheckedChanged);
            // 
            // nudNota
            // 
            this.nudNota.DecimalPlaces = 1;
            this.nudNota.Enabled = false;
            this.nudNota.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudNota.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nudNota.Location = new System.Drawing.Point(80, 163);
            this.nudNota.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudNota.Name = "nudNota";
            this.nudNota.Size = new System.Drawing.Size(55, 26);
            this.nudNota.TabIndex = 3;
            this.nudNota.ValueChanged += new System.EventHandler(this.nudNota_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Disciplina";
            // 
            // txtTipo_Atendimento
            // 
            this.txtTipo_Atendimento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTipo_Atendimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipo_Atendimento.Location = new System.Drawing.Point(12, 122);
            this.txtTipo_Atendimento.Name = "txtTipo_Atendimento";
            this.txtTipo_Atendimento.Size = new System.Drawing.Size(235, 26);
            this.txtTipo_Atendimento.TabIndex = 6;
            this.txtTipo_Atendimento.TextChanged += new System.EventHandler(this.txtTipo_Atendimento_TextChanged);
            this.txtTipo_Atendimento.Enter += new System.EventHandler(this.txtTipo_Atendimento_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tipo Atendimento";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.btNovo);
            this.splitContainer1.Panel1.Controls.Add(this.btAdd_Tipodisciplina);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.cmbDisciplina);
            this.splitContainer1.Panel1.Controls.Add(this.txtTipo_Atendimento);
            this.splitContainer1.Panel1.Controls.Add(this.ckbTem_Nota);
            this.splitContainer1.Panel1.Controls.Add(this.nudNota);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(540, 455);
            this.splitContainer1.SplitterDistance = 255;
            this.splitContainer1.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblEnsino);
            this.groupBox1.Controls.Add(this.cmbEnsino);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtFormula);
            this.groupBox1.Location = new System.Drawing.Point(12, 195);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 196);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Média";
            // 
            // lblEnsino
            // 
            this.lblEnsino.AutoSize = true;
            this.lblEnsino.Location = new System.Drawing.Point(6, 27);
            this.lblEnsino.Name = "lblEnsino";
            this.lblEnsino.Size = new System.Drawing.Size(39, 13);
            this.lblEnsino.TabIndex = 10;
            this.lblEnsino.Text = "Ensino";
            // 
            // cmbEnsino
            // 
            this.cmbEnsino.FormattingEnabled = true;
            this.cmbEnsino.Location = new System.Drawing.Point(44, 24);
            this.cmbEnsino.Name = "cmbEnsino";
            this.cmbEnsino.Size = new System.Drawing.Size(121, 21);
            this.cmbEnsino.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "exemplo: (PROVA+PROVA)/2=M";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Formula da Média";
            // 
            // txtFormula
            // 
            this.txtFormula.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFormula.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormula.Location = new System.Drawing.Point(4, 112);
            this.txtFormula.Name = "txtFormula";
            this.txtFormula.Size = new System.Drawing.Size(235, 26);
            this.txtFormula.TabIndex = 10;
            // 
            // btNovo
            // 
            this.btNovo.Enabled = false;
            this.btNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btNovo.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.action_add_16xLG;
            this.btNovo.Location = new System.Drawing.Point(214, 22);
            this.btNovo.Name = "btNovo";
            this.btNovo.Size = new System.Drawing.Size(33, 28);
            this.btNovo.TabIndex = 9;
            this.btNovo.UseVisualStyleBackColor = true;
            this.btNovo.Click += new System.EventHandler(this.btNovo_Click);
            // 
            // btAdd_Tipodisciplina
            // 
            this.btAdd_Tipodisciplina.Enabled = false;
            this.btAdd_Tipodisciplina.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAdd_Tipodisciplina.Location = new System.Drawing.Point(141, 161);
            this.btAdd_Tipodisciplina.Name = "btAdd_Tipodisciplina";
            this.btAdd_Tipodisciplina.Size = new System.Drawing.Size(106, 28);
            this.btAdd_Tipodisciplina.TabIndex = 8;
            this.btAdd_Tipodisciplina.Text = "Adicionar";
            this.btAdd_Tipodisciplina.UseVisualStyleBackColor = true;
            this.btAdd_Tipodisciplina.Click += new System.EventHandler(this.btAdd_Tipodisciplina_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ltvTipos_Atendimento);
            this.splitContainer2.Size = new System.Drawing.Size(281, 455);
            this.splitContainer2.SplitterDistance = 52;
            this.splitContainer2.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Lista de Atendimentos";
            // 
            // ltvTipos_Atendimento
            // 
            this.ltvTipos_Atendimento.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.ltvTipos_Atendimento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltvTipos_Atendimento.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ltvTipos_Atendimento.Location = new System.Drawing.Point(0, 0);
            this.ltvTipos_Atendimento.MultiSelect = false;
            this.ltvTipos_Atendimento.Name = "ltvTipos_Atendimento";
            this.ltvTipos_Atendimento.Size = new System.Drawing.Size(281, 399);
            this.ltvTipos_Atendimento.TabIndex = 9;
            this.ltvTipos_Atendimento.UseCompatibleStateImageBehavior = false;
            this.ltvTipos_Atendimento.View = System.Windows.Forms.View.Details;
            this.ltvTipos_Atendimento.ItemActivate += new System.EventHandler(this.ltvTipos_Atendimento_ItemActivate);
            this.ltvTipos_Atendimento.SelectedIndexChanged += new System.EventHandler(this.ltvTipos_Atendimento_SelectedIndexChanged);
            this.ltvTipos_Atendimento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ltvTipos_Atendimento_KeyDown);
            this.ltvTipos_Atendimento.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ltvTipos_Atendimento_MouseDoubleClick);
            this.ltvTipos_Atendimento.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ltvTipos_Atendimento_MouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tipo Atendimento";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Menção";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frConf_atendimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 455);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frConf_atendimento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração de Atendimento";
            this.Load += new System.EventHandler(this.frConf_atendimento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudNota)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDisciplina;
        private System.Windows.Forms.CheckBox ckbTem_Nota;
        private System.Windows.Forms.NumericUpDown nudNota;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTipo_Atendimento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btAdd_Tipodisciplina;
        private System.Windows.Forms.ListView ltvTipos_Atendimento;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btNovo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFormula;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblEnsino;
        private System.Windows.Forms.ComboBox cmbEnsino;
    }
}