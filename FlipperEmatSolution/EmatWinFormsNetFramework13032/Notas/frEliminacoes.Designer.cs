namespace EmatWinFormsNetFramework13032.Notas
{
    partial class frEliminacoes
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
            this.txtNmat = new System.Windows.Forms.TextBox();
            this.btPesquisar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.clbEliminacoes = new System.Windows.Forms.CheckedListBox();
            this.lbAluno = new System.Windows.Forms.Label();
            this.lbEnsino = new System.Windows.Forms.Label();
            this.lbDisciplinaAtual = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNmat
            // 
            this.txtNmat.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNmat.Location = new System.Drawing.Point(69, 31);
            this.txtNmat.Name = "txtNmat";
            this.txtNmat.Size = new System.Drawing.Size(101, 26);
            this.txtNmat.TabIndex = 0;
            this.txtNmat.TextChanged += new System.EventHandler(this.txtN_mat_TextChanged);
            // 
            // btPesquisar
            // 
            this.btPesquisar.Enabled = false;
            this.btPesquisar.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPesquisar.Location = new System.Drawing.Point(176, 30);
            this.btPesquisar.Name = "btPesquisar";
            this.btPesquisar.Size = new System.Drawing.Size(97, 28);
            this.btPesquisar.TabIndex = 1;
            this.btPesquisar.Text = "Pesquisar";
            this.btPesquisar.UseVisualStyleBackColor = true;
            this.btPesquisar.Click += new System.EventHandler(this.btPesquisar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "N. MAT.";
            // 
            // clbEliminacoes
            // 
            this.clbEliminacoes.BackColor = System.Drawing.SystemColors.Menu;
            this.clbEliminacoes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbEliminacoes.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbEliminacoes.FormattingEnabled = true;
            this.clbEliminacoes.Location = new System.Drawing.Point(6, 19);
            this.clbEliminacoes.Name = "clbEliminacoes";
            this.clbEliminacoes.Size = new System.Drawing.Size(119, 252);
            this.clbEliminacoes.TabIndex = 0;
            this.clbEliminacoes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbEliminacoes_ItemCheck);
            this.clbEliminacoes.Click += new System.EventHandler(this.clbEliminacoes_Click);
            this.clbEliminacoes.SelectedIndexChanged += new System.EventHandler(this.clbEliminacoes_SelectedIndexChanged);
            // 
            // lbAluno
            // 
            this.lbAluno.AutoSize = true;
            this.lbAluno.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAluno.Location = new System.Drawing.Point(6, 72);
            this.lbAluno.Name = "lbAluno";
            this.lbAluno.Size = new System.Drawing.Size(56, 20);
            this.lbAluno.TabIndex = 5;
            this.lbAluno.Text = "NOME: ";
            // 
            // lbEnsino
            // 
            this.lbEnsino.AutoSize = true;
            this.lbEnsino.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEnsino.Location = new System.Drawing.Point(6, 126);
            this.lbEnsino.Name = "lbEnsino";
            this.lbEnsino.Size = new System.Drawing.Size(66, 20);
            this.lbEnsino.TabIndex = 6;
            this.lbEnsino.Text = "ENSINO: ";
            // 
            // lbDisciplinaAtual
            // 
            this.lbDisciplinaAtual.AutoSize = true;
            this.lbDisciplinaAtual.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDisciplinaAtual.Location = new System.Drawing.Point(6, 163);
            this.lbDisciplinaAtual.Name = "lbDisciplinaAtual";
            this.lbDisciplinaAtual.Size = new System.Drawing.Size(92, 20);
            this.lbDisciplinaAtual.TabIndex = 7;
            this.lbDisciplinaAtual.Text = "DISC. ATUAL:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clbEliminacoes);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(364, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 288);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ELIMINAÇÕES";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtNmat);
            this.groupBox2.Controls.Add(this.lbDisciplinaAtual);
            this.groupBox2.Controls.Add(this.btPesquisar);
            this.groupBox2.Controls.Add(this.lbEnsino);
            this.groupBox2.Controls.Add(this.lbAluno);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 210);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ALUNO";
            // 
            // frEliminacoes
            // 
            this.AcceptButton = this.btPesquisar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 307);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frEliminacoes";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eliminações";
            this.Load += new System.EventHandler(this.frEliminacoes_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNmat;
        private System.Windows.Forms.Button btPesquisar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clbEliminacoes;
        private System.Windows.Forms.Label lbAluno;
        private System.Windows.Forms.Label lbEnsino;
        private System.Windows.Forms.Label lbDisciplinaAtual;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}