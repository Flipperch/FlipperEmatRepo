namespace EmatWinFormsNetFramework1402.Formularios
{
    partial class frmConcluirEnsinoAluno
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
            this.lblNome = new System.Windows.Forms.Label();
            this.lblEnsinoAtual = new System.Windows.Forms.Label();
            this.lblDisciplinasEncerradas = new System.Windows.Forms.Label();
            this.lblNmat = new System.Windows.Forms.Label();
            this.btnConcluirEnsino = new System.Windows.Forms.Button();
            this.lblAtivadadeExtra = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(12, 9);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(38, 13);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome:";
            // 
            // lblEnsinoAtual
            // 
            this.lblEnsinoAtual.AutoSize = true;
            this.lblEnsinoAtual.Location = new System.Drawing.Point(12, 62);
            this.lblEnsinoAtual.Name = "lblEnsinoAtual";
            this.lblEnsinoAtual.Size = new System.Drawing.Size(69, 13);
            this.lblEnsinoAtual.TabIndex = 1;
            this.lblEnsinoAtual.Text = "Ensino Atual:";
            // 
            // lblDisciplinasEncerradas
            // 
            this.lblDisciplinasEncerradas.AutoSize = true;
            this.lblDisciplinasEncerradas.Location = new System.Drawing.Point(12, 86);
            this.lblDisciplinasEncerradas.Name = "lblDisciplinasEncerradas";
            this.lblDisciplinasEncerradas.Size = new System.Drawing.Size(117, 13);
            this.lblDisciplinasEncerradas.TabIndex = 2;
            this.lblDisciplinasEncerradas.Text = "Disciplinas Encerradas:";
            // 
            // lblNmat
            // 
            this.lblNmat.AutoSize = true;
            this.lblNmat.Location = new System.Drawing.Point(12, 37);
            this.lblNmat.Name = "lblNmat";
            this.lblNmat.Size = new System.Drawing.Size(85, 13);
            this.lblNmat.TabIndex = 3;
            this.lblNmat.Text = "Nª de Matrícula:";
            // 
            // btnConcluirEnsino
            // 
            this.btnConcluirEnsino.Location = new System.Drawing.Point(178, 205);
            this.btnConcluirEnsino.Name = "btnConcluirEnsino";
            this.btnConcluirEnsino.Size = new System.Drawing.Size(107, 23);
            this.btnConcluirEnsino.TabIndex = 4;
            this.btnConcluirEnsino.Text = "Concluir Ensino";
            this.btnConcluirEnsino.UseVisualStyleBackColor = true;
            // 
            // lblAtivadadeExtra
            // 
            this.lblAtivadadeExtra.AutoSize = true;
            this.lblAtivadadeExtra.Location = new System.Drawing.Point(12, 115);
            this.lblAtivadadeExtra.Name = "lblAtivadadeExtra";
            this.lblAtivadadeExtra.Size = new System.Drawing.Size(81, 13);
            this.lblAtivadadeExtra.TabIndex = 5;
            this.lblAtivadadeExtra.Text = "Atividade Extra:";
            // 
            // frmConcluirEnsinoAluno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 240);
            this.Controls.Add(this.lblAtivadadeExtra);
            this.Controls.Add(this.btnConcluirEnsino);
            this.Controls.Add(this.lblNmat);
            this.Controls.Add(this.lblDisciplinasEncerradas);
            this.Controls.Add(this.lblEnsinoAtual);
            this.Controls.Add(this.lblNome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConcluirEnsinoAluno";
            this.Text = "Concluir Ensino Aluno";
            this.Load += new System.EventHandler(this.frmConcluirEnsinoAluno_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblEnsinoAtual;
        private System.Windows.Forms.Label lblDisciplinasEncerradas;
        private System.Windows.Forms.Label lblNmat;
        private System.Windows.Forms.Button btnConcluirEnsino;
        private System.Windows.Forms.Label lblAtivadadeExtra;
    }
}