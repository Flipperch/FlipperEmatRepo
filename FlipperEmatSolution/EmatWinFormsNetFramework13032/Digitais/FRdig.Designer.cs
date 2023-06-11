namespace EmatWinFormsNetFramework13032.Digitais
{
    partial class FRdig
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
            this.btObterdigital = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNumat = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.btnVerificardig = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.btnExcluirdig = new System.Windows.Forms.Button();
            this.labelImgQuality = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ptbDigi_status = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbDigi_status)).BeginInit();
            this.SuspendLayout();
            // 
            // btObterdigital
            // 
            this.btObterdigital.Location = new System.Drawing.Point(12, 105);
            this.btObterdigital.Name = "btObterdigital";
            this.btObterdigital.Size = new System.Drawing.Size(96, 23);
            this.btObterdigital.TabIndex = 1;
            this.btObterdigital.Text = "1 - Obter Digital";
            this.btObterdigital.UseVisualStyleBackColor = true;
            this.btObterdigital.Click += new System.EventHandler(this.btObterdigital_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 191);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(301, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl1
            // 
            this.tssl1.Name = "tssl1";
            this.tssl1.Size = new System.Drawing.Size(0, 17);
            // 
            // lblNumat
            // 
            this.lblNumat.AutoSize = true;
            this.lblNumat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumat.Location = new System.Drawing.Point(12, 31);
            this.lblNumat.Name = "lblNumat";
            this.lblNumat.Size = new System.Drawing.Size(83, 13);
            this.lblNumat.TabIndex = 3;
            this.lblNumat.Text = "Nº Matrícula:";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.Location = new System.Drawing.Point(12, 9);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(43, 13);
            this.lblNome.TabIndex = 5;
            this.lblNome.Text = "Nome:";
            // 
            // btnVerificardig
            // 
            this.btnVerificardig.Location = new System.Drawing.Point(12, 134);
            this.btnVerificardig.Name = "btnVerificardig";
            this.btnVerificardig.Size = new System.Drawing.Size(96, 23);
            this.btnVerificardig.TabIndex = 6;
            this.btnVerificardig.Text = "2 - Verificar";
            this.btnVerificardig.UseVisualStyleBackColor = true;
            this.btnVerificardig.Click += new System.EventHandler(this.btnVerificardig_Click);
            // 
            // btnGravar
            // 
            this.btnGravar.Enabled = false;
            this.btnGravar.Location = new System.Drawing.Point(12, 163);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(96, 23);
            this.btnGravar.TabIndex = 7;
            this.btnGravar.Text = "3 - Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnAceitar_Click);
            // 
            // btnExcluirdig
            // 
            this.btnExcluirdig.Enabled = false;
            this.btnExcluirdig.Location = new System.Drawing.Point(166, 163);
            this.btnExcluirdig.Name = "btnExcluirdig";
            this.btnExcluirdig.Size = new System.Drawing.Size(104, 23);
            this.btnExcluirdig.TabIndex = 10;
            this.btnExcluirdig.Text = "Excluir Digital";
            this.btnExcluirdig.UseVisualStyleBackColor = true;
            this.btnExcluirdig.Click += new System.EventHandler(this.btnExcluirdig_Click);
            // 
            // labelImgQuality
            // 
            this.labelImgQuality.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImgQuality.Location = new System.Drawing.Point(76, 54);
            this.labelImgQuality.Name = "labelImgQuality";
            this.labelImgQuality.Size = new System.Drawing.Size(35, 13);
            this.labelImgQuality.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Qualidade:";
            // 
            // ptbDigi_status
            // 
            this.ptbDigi_status.Location = new System.Drawing.Point(166, 40);
            this.ptbDigi_status.Name = "ptbDigi_status";
            this.ptbDigi_status.Size = new System.Drawing.Size(104, 116);
            this.ptbDigi_status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbDigi_status.TabIndex = 14;
            this.ptbDigi_status.TabStop = false;
            // 
            // FRdig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 213);
            this.Controls.Add(this.ptbDigi_status);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelImgQuality);
            this.Controls.Add(this.btnExcluirdig);
            this.Controls.Add(this.btnGravar);
            this.Controls.Add(this.btnVerificardig);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.lblNumat);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btObterdigital);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRdig";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Obter Digital";
            this.Load += new System.EventHandler(this.FRdig_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbDigi_status)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl1;
        private System.Windows.Forms.Label lblNumat;
        public System.Windows.Forms.Button btObterdigital;
        private System.Windows.Forms.Label lblNome;
        public System.Windows.Forms.Button btnVerificardig;
        public System.Windows.Forms.Button btnGravar;
        public System.Windows.Forms.Button btnExcluirdig;
        private System.Windows.Forms.Label labelImgQuality;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox ptbDigi_status;
    }
}