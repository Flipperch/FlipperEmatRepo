﻿namespace EmatWinFormsNetFramework1402.Formularios
{
    partial class frAtribuicaoDisciplina
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAtribuir = new System.Windows.Forms.Button();
            this.ckbConsiderarHorarios = new System.Windows.Forms.CheckBox();
            this.ckbAutoAtribuicao = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDisciplinas = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblDisciplinaAtual = new System.Windows.Forms.Label();
            this.lblEnsino = new System.Windows.Forms.Label();
            this.lblRg = new System.Windows.Forms.Label();
            this.lblNmat = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.ptbFoto = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFoto)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAtribuir);
            this.groupBox1.Controls.Add(this.ckbConsiderarHorarios);
            this.groupBox1.Controls.Add(this.ckbAutoAtribuicao);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbDisciplinas);
            this.groupBox1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 176);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Size = new System.Drawing.Size(598, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Próxima Disciplina";
            // 
            // btnAtribuir
            // 
            this.btnAtribuir.Enabled = false;
            this.btnAtribuir.Font = new System.Drawing.Font("Arial Narrow", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtribuir.Location = new System.Drawing.Point(354, 34);
            this.btnAtribuir.Name = "btnAtribuir";
            this.btnAtribuir.Size = new System.Drawing.Size(89, 28);
            this.btnAtribuir.TabIndex = 2;
            this.btnAtribuir.Text = "Atribuir";
            this.btnAtribuir.UseVisualStyleBackColor = true;
            this.btnAtribuir.Click += new System.EventHandler(this.btnAtribuir_Click);
            // 
            // ckbConsiderarHorarios
            // 
            this.ckbConsiderarHorarios.AutoSize = true;
            this.ckbConsiderarHorarios.Checked = true;
            this.ckbConsiderarHorarios.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbConsiderarHorarios.Enabled = false;
            this.ckbConsiderarHorarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbConsiderarHorarios.Location = new System.Drawing.Point(190, 68);
            this.ckbConsiderarHorarios.Name = "ckbConsiderarHorarios";
            this.ckbConsiderarHorarios.Size = new System.Drawing.Size(227, 24);
            this.ckbConsiderarHorarios.TabIndex = 8;
            this.ckbConsiderarHorarios.Text = "Considerar Horários de Aula";
            this.ckbConsiderarHorarios.UseVisualStyleBackColor = true;
            // 
            // ckbAutoAtribuicao
            // 
            this.ckbAutoAtribuicao.AutoSize = true;
            this.ckbAutoAtribuicao.Enabled = false;
            this.ckbAutoAtribuicao.Location = new System.Drawing.Point(91, 68);
            this.ckbAutoAtribuicao.Name = "ckbAutoAtribuicao";
            this.ckbAutoAtribuicao.Size = new System.Drawing.Size(93, 24);
            this.ckbAutoAtribuicao.TabIndex = 3;
            this.ckbAutoAtribuicao.Text = "Automática";
            this.ckbAutoAtribuicao.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Disciplinas:";
            // 
            // cmbDisciplinas
            // 
            this.cmbDisciplinas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisciplinas.FormattingEnabled = true;
            this.cmbDisciplinas.Location = new System.Drawing.Point(91, 34);
            this.cmbDisciplinas.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cmbDisciplinas.Name = "cmbDisciplinas";
            this.cmbDisciplinas.Size = new System.Drawing.Size(256, 28);
            this.cmbDisciplinas.TabIndex = 0;
            this.cmbDisciplinas.SelectedIndexChanged += new System.EventHandler(this.cmbDisciplinas_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblDisciplinaAtual);
            this.groupBox2.Controls.Add(this.lblEnsino);
            this.groupBox2.Controls.Add(this.lblRg);
            this.groupBox2.Controls.Add(this.lblNmat);
            this.groupBox2.Controls.Add(this.lblNome);
            this.groupBox2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(10, 14);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox2.Size = new System.Drawing.Size(443, 156);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Aluno";
            // 
            // lblDisciplinaAtual
            // 
            this.lblDisciplinaAtual.AutoSize = true;
            this.lblDisciplinaAtual.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisciplinaAtual.Location = new System.Drawing.Point(5, 123);
            this.lblDisciplinaAtual.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDisciplinaAtual.Name = "lblDisciplinaAtual";
            this.lblDisciplinaAtual.Size = new System.Drawing.Size(111, 20);
            this.lblDisciplinaAtual.TabIndex = 8;
            this.lblDisciplinaAtual.Text = "Disciplina Atual:";
            // 
            // lblEnsino
            // 
            this.lblEnsino.AutoSize = true;
            this.lblEnsino.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnsino.Location = new System.Drawing.Point(4, 77);
            this.lblEnsino.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEnsino.Name = "lblEnsino";
            this.lblEnsino.Size = new System.Drawing.Size(93, 20);
            this.lblEnsino.TabIndex = 6;
            this.lblEnsino.Text = "Ensino Atual:";
            // 
            // lblRg
            // 
            this.lblRg.AutoSize = true;
            this.lblRg.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRg.Location = new System.Drawing.Point(5, 54);
            this.lblRg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRg.Name = "lblRg";
            this.lblRg.Size = new System.Drawing.Size(32, 20);
            this.lblRg.TabIndex = 4;
            this.lblRg.Text = "RG:";
            // 
            // lblNmat
            // 
            this.lblNmat.AutoSize = true;
            this.lblNmat.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNmat.Location = new System.Drawing.Point(5, 100);
            this.lblNmat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNmat.Name = "lblNmat";
            this.lblNmat.Size = new System.Drawing.Size(107, 20);
            this.lblNmat.TabIndex = 1;
            this.lblNmat.Text = "Nº de Matrícula:";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.Location = new System.Drawing.Point(5, 31);
            this.lblNome.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(49, 20);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome:";
            // 
            // ptbFoto
            // 
            this.ptbFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ptbFoto.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.slr_camera_application_slr_3037;
            this.ptbFoto.Location = new System.Drawing.Point(458, 14);
            this.ptbFoto.Name = "ptbFoto";
            this.ptbFoto.Size = new System.Drawing.Size(152, 109);
            this.ptbFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ptbFoto.TabIndex = 52;
            this.ptbFoto.TabStop = false;
            // 
            // frAtribuicaoDisciplina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 286);
            this.Controls.Add(this.ptbFoto);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frAtribuicaoDisciplina";
            this.ShowIcon = false;
            this.Text = "Atribuição de Disciplinas";
            this.Load += new System.EventHandler(this.frAtribDisciplina_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFoto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDisciplinas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblDisciplinaAtual;
        private System.Windows.Forms.Label lblEnsino;
        private System.Windows.Forms.Label lblRg;
        private System.Windows.Forms.Label lblNmat;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Button btnAtribuir;
        private System.Windows.Forms.CheckBox ckbAutoAtribuicao;
        private System.Windows.Forms.CheckBox ckbConsiderarHorarios;
        private System.Windows.Forms.PictureBox ptbFoto;
    }
}