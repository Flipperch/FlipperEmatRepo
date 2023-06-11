namespace EmatWinFormsNetFramework13032.Usuarios_Grupos
{
    partial class frTroca_senha
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
            this.txtNova_senha = new System.Windows.Forms.TextBox();
            this.txtConfirma_senha = new System.Windows.Forms.TextBox();
            this.btAlterarsenha = new System.Windows.Forms.Button();
            this.btCancelar = new System.Windows.Forms.Button();
            this.txtSenha_atual = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbErro = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtNova_senha
            // 
            this.txtNova_senha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNova_senha.Location = new System.Drawing.Point(183, 49);
            this.txtNova_senha.Name = "txtNova_senha";
            this.txtNova_senha.PasswordChar = '*';
            this.txtNova_senha.Size = new System.Drawing.Size(100, 26);
            this.txtNova_senha.TabIndex = 1;
            this.txtNova_senha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNova_senha.TextChanged += new System.EventHandler(this.txtSenha_atual_TextChanged);
            // 
            // txtConfirma_senha
            // 
            this.txtConfirma_senha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirma_senha.Location = new System.Drawing.Point(183, 81);
            this.txtConfirma_senha.Name = "txtConfirma_senha";
            this.txtConfirma_senha.PasswordChar = '*';
            this.txtConfirma_senha.Size = new System.Drawing.Size(100, 26);
            this.txtConfirma_senha.TabIndex = 2;
            this.txtConfirma_senha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtConfirma_senha.TextChanged += new System.EventHandler(this.txtSenha_atual_TextChanged);
            // 
            // btAlterarsenha
            // 
            this.btAlterarsenha.Enabled = false;
            this.btAlterarsenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAlterarsenha.Location = new System.Drawing.Point(183, 129);
            this.btAlterarsenha.Name = "btAlterarsenha";
            this.btAlterarsenha.Size = new System.Drawing.Size(80, 28);
            this.btAlterarsenha.TabIndex = 3;
            this.btAlterarsenha.Text = "Alterar";
            this.btAlterarsenha.UseVisualStyleBackColor = true;
            this.btAlterarsenha.Click += new System.EventHandler(this.btAlterarsenha_Click);
            // 
            // btCancelar
            // 
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCancelar.Location = new System.Drawing.Point(96, 129);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(80, 28);
            this.btCancelar.TabIndex = 4;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            // 
            // txtSenha_atual
            // 
            this.txtSenha_atual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha_atual.Location = new System.Drawing.Point(183, 17);
            this.txtSenha_atual.Name = "txtSenha_atual";
            this.txtSenha_atual.PasswordChar = '*';
            this.txtSenha_atual.Size = new System.Drawing.Size(100, 26);
            this.txtSenha_atual.TabIndex = 0;
            this.txtSenha_atual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSenha_atual.TextChanged += new System.EventHandler(this.txtSenha_atual_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(79, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Senha Atual";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(81, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nova Senha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Confirma Nova Senha";
            // 
            // lbErro
            // 
            this.lbErro.AutoSize = true;
            this.lbErro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbErro.Location = new System.Drawing.Point(45, 106);
            this.lbErro.Name = "lbErro";
            this.lbErro.Size = new System.Drawing.Size(0, 20);
            this.lbErro.TabIndex = 8;
            // 
            // frTroca_senha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancelar;
            this.ClientSize = new System.Drawing.Size(350, 166);
            this.Controls.Add(this.lbErro);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSenha_atual);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.btAlterarsenha);
            this.Controls.Add(this.txtConfirma_senha);
            this.Controls.Add(this.txtNova_senha);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frTroca_senha";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alterar Senha";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNova_senha;
        private System.Windows.Forms.TextBox txtConfirma_senha;
        private System.Windows.Forms.Button btAlterarsenha;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.TextBox txtSenha_atual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbErro;
    }
}