namespace EmatWinFormsNetFramework13032.Notas
{
    partial class frAtrib_nota
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
            this.txtN_mat = new System.Windows.Forms.TextBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.txtRg = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMateria = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpOrient_ini = new System.Windows.Forms.DateTimePicker();
            this.nudMedia = new System.Windows.Forms.NumericUpDown();
            this.dtpOrient_fin = new System.Windows.Forms.DateTimePicker();
            this.cmbPROFESSORESes = new System.Windows.Forms.ComboBox();
            this.txtRg_prof = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btLocalizar = new System.Windows.Forms.Button();
            this.btSalvar = new System.Windows.Forms.Button();
            this.btCancelar = new System.Windows.Forms.Button();
            this.fotoaluno = new System.Windows.Forms.PictureBox();
            this.txtEnsino = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ckbProf_inativo = new System.Windows.Forms.CheckBox();
            this.label29 = new System.Windows.Forms.Label();
            this.lbAviso = new System.Windows.Forms.Label();
            this.lblAtivo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudMedia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fotoaluno)).BeginInit();
            this.SuspendLayout();
            // 
            // txtN_mat
            // 
            this.txtN_mat.Enabled = false;
            this.txtN_mat.Location = new System.Drawing.Point(19, 127);
            this.txtN_mat.Name = "txtN_mat";
            this.txtN_mat.Size = new System.Drawing.Size(88, 20);
            this.txtN_mat.TabIndex = 0;
            // 
            // txtNome
            // 
            this.txtNome.Enabled = false;
            this.txtNome.Location = new System.Drawing.Point(18, 172);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(354, 20);
            this.txtNome.TabIndex = 1;
            // 
            // txtRg
            // 
            this.txtRg.Enabled = false;
            this.txtRg.Location = new System.Drawing.Point(378, 172);
            this.txtRg.Name = "txtRg";
            this.txtRg.Size = new System.Drawing.Size(110, 20);
            this.txtRg.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nº de Matrícula";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nome";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(375, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "RG";
            // 
            // cmbMateria
            // 
            this.cmbMateria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMateria.FormattingEnabled = true;
            this.cmbMateria.Location = new System.Drawing.Point(18, 221);
            this.cmbMateria.Name = "cmbMateria";
            this.cmbMateria.Size = new System.Drawing.Size(148, 21);
            this.cmbMateria.TabIndex = 6;
            this.cmbMateria.SelectedIndexChanged += new System.EventHandler(this.cmbMateria_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Matéria";
            // 
            // dtpOrient_ini
            // 
            this.dtpOrient_ini.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOrient_ini.Location = new System.Drawing.Point(172, 222);
            this.dtpOrient_ini.Name = "dtpOrient_ini";
            this.dtpOrient_ini.Size = new System.Drawing.Size(95, 20);
            this.dtpOrient_ini.TabIndex = 8;
            this.dtpOrient_ini.ValueChanged += new System.EventHandler(this.dtpOrient_ini_ValueChanged);
            // 
            // nudMedia
            // 
            this.nudMedia.Location = new System.Drawing.Point(276, 222);
            this.nudMedia.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudMedia.Name = "nudMedia";
            this.nudMedia.Size = new System.Drawing.Size(92, 20);
            this.nudMedia.TabIndex = 9;
            this.nudMedia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudMedia.ValueChanged += new System.EventHandler(this.nudMedia_ValueChanged);
            // 
            // dtpOrient_fin
            // 
            this.dtpOrient_fin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOrient_fin.Location = new System.Drawing.Point(376, 222);
            this.dtpOrient_fin.Name = "dtpOrient_fin";
            this.dtpOrient_fin.Size = new System.Drawing.Size(95, 20);
            this.dtpOrient_fin.TabIndex = 10;
            this.dtpOrient_fin.ValueChanged += new System.EventHandler(this.dtpOrient_fin_ValueChanged);
            // 
            // cmbPROFESSORESes
            // 
            this.cmbPROFESSORESes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPROFESSORESes.FormattingEnabled = true;
            this.cmbPROFESSORESes.Location = new System.Drawing.Point(19, 277);
            this.cmbPROFESSORESes.Name = "cmbPROFESSORESes";
            this.cmbPROFESSORESes.Size = new System.Drawing.Size(353, 21);
            this.cmbPROFESSORESes.TabIndex = 11;
            this.cmbPROFESSORESes.SelectedIndexChanged += new System.EventHandler(this.cmbPROFESSORESes_SelectedIndexChanged);
            // 
            // txtRg_prof
            // 
            this.txtRg_prof.Enabled = false;
            this.txtRg_prof.Location = new System.Drawing.Point(378, 277);
            this.txtRg_prof.Name = "txtRg_prof";
            this.txtRg_prof.Size = new System.Drawing.Size(110, 20);
            this.txtRg_prof.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 261);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "PROFESSORES";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(378, 261);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "RG";
            // 
            // btLocalizar
            // 
            this.btLocalizar.Location = new System.Drawing.Point(19, 321);
            this.btLocalizar.Name = "btLocalizar";
            this.btLocalizar.Size = new System.Drawing.Size(75, 23);
            this.btLocalizar.TabIndex = 15;
            this.btLocalizar.Text = "Localizar";
            this.btLocalizar.UseVisualStyleBackColor = true;
            this.btLocalizar.Click += new System.EventHandler(this.btLocalizar_Click);
            // 
            // btSalvar
            // 
            this.btSalvar.Enabled = false;
            this.btSalvar.Location = new System.Drawing.Point(181, 321);
            this.btSalvar.Name = "btSalvar";
            this.btSalvar.Size = new System.Drawing.Size(75, 23);
            this.btSalvar.TabIndex = 16;
            this.btSalvar.Text = "Aceitar";
            this.btSalvar.UseVisualStyleBackColor = true;
            this.btSalvar.Click += new System.EventHandler(this.btSalvar_Click);
            // 
            // btCancelar
            // 
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.Location = new System.Drawing.Point(100, 321);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(75, 23);
            this.btCancelar.TabIndex = 17;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // fotoaluno
            // 
            this.fotoaluno.Location = new System.Drawing.Point(338, 12);
            this.fotoaluno.Name = "fotoaluno";
            this.fotoaluno.Size = new System.Drawing.Size(150, 112);
            this.fotoaluno.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fotoaluno.TabIndex = 52;
            this.fotoaluno.TabStop = false;
            // 
            // txtEnsino
            // 
            this.txtEnsino.Enabled = false;
            this.txtEnsino.Location = new System.Drawing.Point(113, 127);
            this.txtEnsino.Name = "txtEnsino";
            this.txtEnsino.Size = new System.Drawing.Size(140, 20);
            this.txtEnsino.TabIndex = 53;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(110, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 54;
            this.label7.Text = "Ensino";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(375, 206);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 55;
            this.label8.Text = "Orientação Final";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(273, 206);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 56;
            this.label9.Text = "Média Final";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(169, 206);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 57;
            this.label10.Text = "Orientação Inicial";
            // 
            // ckbProf_inativo
            // 
            this.ckbProf_inativo.AutoSize = true;
            this.ckbProf_inativo.Location = new System.Drawing.Point(270, 325);
            this.ckbProf_inativo.Name = "ckbProf_inativo";
            this.ckbProf_inativo.Size = new System.Drawing.Size(149, 17);
            this.ckbProf_inativo.TabIndex = 58;
            this.ckbProf_inativo.Text = "Exibir PROFESSORESes Inativos";
            this.ckbProf_inativo.UseVisualStyleBackColor = true;
            this.ckbProf_inativo.CheckedChanged += new System.EventHandler(this.ckbProf_inativo_CheckedChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(11, 12);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(190, 24);
            this.label29.TabIndex = 60;
            this.label29.Text = "Atribuição de notas";
            // 
            // lbAviso
            // 
            this.lbAviso.Location = new System.Drawing.Point(16, 84);
            this.lbAviso.Name = "lbAviso";
            this.lbAviso.Size = new System.Drawing.Size(91, 16);
            this.lbAviso.TabIndex = 61;
            // 
            // lblAtivo
            // 
            this.lblAtivo.Location = new System.Drawing.Point(16, 47);
            this.lblAtivo.Name = "lblAtivo";
            this.lblAtivo.Size = new System.Drawing.Size(318, 23);
            this.lblAtivo.TabIndex = 62;
            // 
            // FRatrib_notas
            // 
            this.AcceptButton = this.btSalvar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancelar;
            this.ClientSize = new System.Drawing.Size(496, 356);
            this.Controls.Add(this.lblAtivo);
            this.Controls.Add(this.lbAviso);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.ckbProf_inativo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtEnsino);
            this.Controls.Add(this.fotoaluno);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.btSalvar);
            this.Controls.Add(this.btLocalizar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRg_prof);
            this.Controls.Add(this.cmbPROFESSORESes);
            this.Controls.Add(this.dtpOrient_fin);
            this.Controls.Add(this.nudMedia);
            this.Controls.Add(this.dtpOrient_ini);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbMateria);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRg);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.txtN_mat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRatrib_notas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atribuição de Notas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FRatrib_notas_FormClosing);
            this.Load += new System.EventHandler(this.FRatrib_notas_Load);
            this.Shown += new System.EventHandler(this.FRatrib_notas_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.nudMedia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fotoaluno)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtN_mat;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.TextBox txtRg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMateria;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpOrient_ini;
        private System.Windows.Forms.NumericUpDown nudMedia;
        private System.Windows.Forms.DateTimePicker dtpOrient_fin;
        private System.Windows.Forms.ComboBox cmbPROFESSORESes;
        private System.Windows.Forms.TextBox txtRg_prof;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btLocalizar;
        private System.Windows.Forms.Button btSalvar;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.PictureBox fotoaluno;
        private System.Windows.Forms.TextBox txtEnsino;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox ckbProf_inativo;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lbAviso;
        private System.Windows.Forms.Label lblAtivo;
    }
}