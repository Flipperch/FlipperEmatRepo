namespace EmatWinFormsNetFramework13032.Alunos
{
    partial class frImportar
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtCampo = new System.Windows.Forms.TextBox();
            this.btExecutar = new System.Windows.Forms.Button();
            this.txtTab_1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTab_2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNmats = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rbFundamental = new System.Windows.Forms.RadioButton();
            this.rbMedio = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Campo";
            // 
            // txtCampo
            // 
            this.txtCampo.Location = new System.Drawing.Point(67, 65);
            this.txtCampo.Name = "txtCampo";
            this.txtCampo.Size = new System.Drawing.Size(100, 20);
            this.txtCampo.TabIndex = 0;
            this.txtCampo.TextChanged += new System.EventHandler(this.txtCampo_TextChanged);
            // 
            // btExecutar
            // 
            this.btExecutar.Location = new System.Drawing.Point(173, 10);
            this.btExecutar.Name = "btExecutar";
            this.btExecutar.Size = new System.Drawing.Size(86, 25);
            this.btExecutar.TabIndex = 1;
            this.btExecutar.Text = "Executar";
            this.btExecutar.UseVisualStyleBackColor = true;
            this.btExecutar.Click += new System.EventHandler(this.btExecutar_Click);
            // 
            // txtTab_1
            // 
            this.txtTab_1.Location = new System.Drawing.Point(67, 13);
            this.txtTab_1.Name = "txtTab_1";
            this.txtTab_1.Size = new System.Drawing.Size(100, 20);
            this.txtTab_1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tabela_1";
            // 
            // txtTab_2
            // 
            this.txtTab_2.Location = new System.Drawing.Point(67, 39);
            this.txtTab_2.Name = "txtTab_2";
            this.txtTab_2.Size = new System.Drawing.Size(100, 20);
            this.txtTab_2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tabela_2";
            // 
            // txtNmats
            // 
            this.txtNmats.Location = new System.Drawing.Point(67, 91);
            this.txtNmats.Multiline = true;
            this.txtNmats.Name = "txtNmats";
            this.txtNmats.Size = new System.Drawing.Size(100, 134);
            this.txtNmats.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Números";
            // 
            // rbFundamental
            // 
            this.rbFundamental.AutoSize = true;
            this.rbFundamental.Checked = true;
            this.rbFundamental.Location = new System.Drawing.Point(173, 42);
            this.rbFundamental.Name = "rbFundamental";
            this.rbFundamental.Size = new System.Drawing.Size(86, 17);
            this.rbFundamental.TabIndex = 8;
            this.rbFundamental.TabStop = true;
            this.rbFundamental.Text = "Fundamental";
            this.rbFundamental.UseVisualStyleBackColor = true;
            // 
            // rbMedio
            // 
            this.rbMedio.AutoSize = true;
            this.rbMedio.Location = new System.Drawing.Point(173, 65);
            this.rbMedio.Name = "rbMedio";
            this.rbMedio.Size = new System.Drawing.Size(54, 17);
            this.rbMedio.TabIndex = 9;
            this.rbMedio.Text = "Médio";
            this.rbMedio.UseVisualStyleBackColor = true;
            // 
            // frImportar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 237);
            this.Controls.Add(this.rbMedio);
            this.Controls.Add(this.rbFundamental);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNmats);
            this.Controls.Add(this.txtTab_2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTab_1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btExecutar);
            this.Controls.Add(this.txtCampo);
            this.Controls.Add(this.label1);
            this.Name = "frImportar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reparar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCampo;
        private System.Windows.Forms.Button btExecutar;
        private System.Windows.Forms.TextBox txtTab_1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTab_2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNmats;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbFundamental;
        private System.Windows.Forms.RadioButton rbMedio;




    }
}