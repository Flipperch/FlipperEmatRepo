namespace EmatWinFormsNetFramework13032.Digitais.FrmOnline
{
    partial class FRdigital
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
            this.btIniciar = new System.Windows.Forms.Button();
            this.btCapturar = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.cmdEnviarInner = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblManutencao = new System.Windows.Forms.ToolStripStatusLabel();
            this.edCartao = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btIniciar
            // 
            this.btIniciar.Location = new System.Drawing.Point(159, 167);
            this.btIniciar.Name = "btIniciar";
            this.btIniciar.Size = new System.Drawing.Size(68, 24);
            this.btIniciar.TabIndex = 31;
            this.btIniciar.Text = "Iniciar";
            this.btIniciar.Click += new System.EventHandler(this.btIniciar_Click);
            // 
            // btCapturar
            // 
            this.btCapturar.Location = new System.Drawing.Point(12, 42);
            this.btCapturar.Name = "btCapturar";
            this.btCapturar.Size = new System.Drawing.Size(100, 24);
            this.btCapturar.TabIndex = 36;
            this.btCapturar.Text = "Capturar";
            this.btCapturar.Click += new System.EventHandler(this.btCapturar_Click);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(9, 13);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 16);
            this.label17.TabIndex = 35;
            this.label17.Text = "Nº de Matrícula";
            // 
            // cmdEnviarInner
            // 
            this.cmdEnviarInner.Location = new System.Drawing.Point(127, 42);
            this.cmdEnviarInner.Name = "cmdEnviarInner";
            this.cmdEnviarInner.Size = new System.Drawing.Size(100, 24);
            this.cmdEnviarInner.TabIndex = 38;
            this.cmdEnviarInner.Text = "Enviar p/ catraca";
            this.cmdEnviarInner.Click += new System.EventHandler(this.cmdEnviarInner_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblManutencao});
            this.statusStrip1.Location = new System.Drawing.Point(0, 197);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(238, 22);
            this.statusStrip1.TabIndex = 39;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblManutencao
            // 
            this.lblManutencao.Name = "lblManutencao";
            this.lblManutencao.Size = new System.Drawing.Size(0, 17);
            // 
            // edCartao
            // 
            this.edCartao.AutoSize = true;
            this.edCartao.Location = new System.Drawing.Point(93, 16);
            this.edCartao.Name = "edCartao";
            this.edCartao.Size = new System.Drawing.Size(0, 13);
            this.edCartao.TabIndex = 40;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 170);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 41;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // FRdigital
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 219);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.edCartao);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdEnviarInner);
            this.Controls.Add(this.btCapturar);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.btIniciar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRdigital";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Obter Digital";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmBIO_FormClosed);
            this.Load += new System.EventHandler(this.FRdigital_Load);
            this.Shown += new System.EventHandler(this.MainBIO_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        public System.Windows.Forms.Button btIniciar;
        public System.Windows.Forms.Button btCapturar;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button cmdEnviarInner;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel lblManutencao;
        public System.Windows.Forms.Label edCartao;
        private System.Windows.Forms.TextBox textBox1;
    }
}