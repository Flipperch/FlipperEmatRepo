namespace EmatWinFormsNetFramework13032.Alunos
{
    partial class frImprimir
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
            this.grbOpcoes = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.txtNmatFinal = new System.Windows.Forms.TextBox();
            this.txtNmatInicial = new System.Windows.Forms.TextBox();
            this.cmbOpcaoRelatorio = new System.Windows.Forms.ComboBox();
            this.mtbDataRelatorio = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grbOpcoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbOpcoes
            // 
            this.grbOpcoes.AutoSize = true;
            this.grbOpcoes.Controls.Add(this.mtbDataRelatorio);
            this.grbOpcoes.Controls.Add(this.label2);
            this.grbOpcoes.Controls.Add(this.label1);
            this.grbOpcoes.Controls.Add(this.btnImprimir);
            this.grbOpcoes.Controls.Add(this.txtNmatFinal);
            this.grbOpcoes.Controls.Add(this.txtNmatInicial);
            this.grbOpcoes.Controls.Add(this.cmbOpcaoRelatorio);
            this.grbOpcoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbOpcoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbOpcoes.Location = new System.Drawing.Point(0, 0);
            this.grbOpcoes.Name = "grbOpcoes";
            this.grbOpcoes.Size = new System.Drawing.Size(444, 140);
            this.grbOpcoes.TabIndex = 1;
            this.grbOpcoes.TabStop = false;
            this.grbOpcoes.Text = "Selecione o Relatório";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Intervalo Nº de Matrícula";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Location = new System.Drawing.Point(335, 60);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(97, 35);
            this.btnImprimir.TabIndex = 3;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // txtNmatFinal
            // 
            this.txtNmatFinal.Enabled = false;
            this.txtNmatFinal.Location = new System.Drawing.Point(265, 66);
            this.txtNmatFinal.Name = "txtNmatFinal";
            this.txtNmatFinal.Size = new System.Drawing.Size(64, 26);
            this.txtNmatFinal.TabIndex = 2;
            // 
            // txtNmatInicial
            // 
            this.txtNmatInicial.Enabled = false;
            this.txtNmatInicial.Location = new System.Drawing.Point(195, 66);
            this.txtNmatInicial.Name = "txtNmatInicial";
            this.txtNmatInicial.Size = new System.Drawing.Size(64, 26);
            this.txtNmatInicial.TabIndex = 1;
            this.txtNmatInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbOpcaoRelatorio
            // 
            this.cmbOpcaoRelatorio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpcaoRelatorio.FormattingEnabled = true;
            this.cmbOpcaoRelatorio.Location = new System.Drawing.Point(12, 25);
            this.cmbOpcaoRelatorio.Name = "cmbOpcaoRelatorio";
            this.cmbOpcaoRelatorio.Size = new System.Drawing.Size(420, 28);
            this.cmbOpcaoRelatorio.TabIndex = 0;
            this.cmbOpcaoRelatorio.SelectedIndexChanged += new System.EventHandler(this.cmbOpcaoRelatorio_SelectedIndexChanged);
            // 
            // mtbDataRelatorio
            // 
            this.mtbDataRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtbDataRelatorio.Location = new System.Drawing.Point(128, 102);
            this.mtbDataRelatorio.Mask = "00/00/0000";
            this.mtbDataRelatorio.Name = "mtbDataRelatorio";
            this.mtbDataRelatorio.Size = new System.Drawing.Size(87, 26);
            this.mtbDataRelatorio.TabIndex = 5;
            this.mtbDataRelatorio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Data Relatório:";
            // 
            // frImprimir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 140);
            this.Controls.Add(this.grbOpcoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frImprimir";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Imprimir";
            this.Load += new System.EventHandler(this.frImprimir_Load);
            this.grbOpcoes.ResumeLayout(false);
            this.grbOpcoes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbOpcoes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.TextBox txtNmatFinal;
        private System.Windows.Forms.TextBox txtNmatInicial;
        private System.Windows.Forms.ComboBox cmbOpcaoRelatorio;
        private System.Windows.Forms.MaskedTextBox mtbDataRelatorio;
        private System.Windows.Forms.Label label2;

    }
}