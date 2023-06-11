namespace EmatWinFormsNetFramework13032.Notas
{
    partial class frPesquisa_hist
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
            this.txtPesquisa = new System.Windows.Forms.TextBox();
            this.rbRg = new System.Windows.Forms.RadioButton();
            this.rbNmat = new System.Windows.Forms.RadioButton();
            this.btPesquisa = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Location = new System.Drawing.Point(12, 38);
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Size = new System.Drawing.Size(370, 20);
            this.txtPesquisa.TabIndex = 0;
            this.txtPesquisa.TextChanged += new System.EventHandler(this.txtPesquisa_TextChanged);
            // 
            // rbRg
            // 
            this.rbRg.AutoSize = true;
            this.rbRg.Location = new System.Drawing.Point(341, 15);
            this.rbRg.Name = "rbRg";
            this.rbRg.Size = new System.Drawing.Size(41, 17);
            this.rbRg.TabIndex = 1;
            this.rbRg.TabStop = true;
            this.rbRg.Text = "RG";
            this.rbRg.UseVisualStyleBackColor = true;
            // 
            // rbNmat
            // 
            this.rbNmat.AutoSize = true;
            this.rbNmat.Checked = true;
            this.rbNmat.Location = new System.Drawing.Point(252, 15);
            this.rbNmat.Name = "rbNmat";
            this.rbNmat.Size = new System.Drawing.Size(83, 17);
            this.rbNmat.TabIndex = 2;
            this.rbNmat.TabStop = true;
            this.rbNmat.Text = "Nº Matricula";
            this.rbNmat.UseVisualStyleBackColor = true;
            // 
            // btPesquisa
            // 
            this.btPesquisa.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btPesquisa.Enabled = false;
            this.btPesquisa.Location = new System.Drawing.Point(206, 64);
            this.btPesquisa.Name = "btPesquisa";
            this.btPesquisa.Size = new System.Drawing.Size(75, 23);
            this.btPesquisa.TabIndex = 3;
            this.btPesquisa.Text = "Pesquisar";
            this.btPesquisa.UseVisualStyleBackColor = true;
            this.btPesquisa.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Selecione a pesquisa e digite no campo abaixo.";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(116, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frPesquisa_hist
            // 
            this.AcceptButton = this.btPesquisa;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 94);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btPesquisa);
            this.Controls.Add(this.rbNmat);
            this.Controls.Add(this.rbRg);
            this.Controls.Add(this.txtPesquisa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frPesquisa_hist";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisar Aluno";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FRpesquisa_hist_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPesquisa;
        private System.Windows.Forms.RadioButton rbRg;
        private System.Windows.Forms.RadioButton rbNmat;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btPesquisa;
        public System.Windows.Forms.Button button2;
    }
}