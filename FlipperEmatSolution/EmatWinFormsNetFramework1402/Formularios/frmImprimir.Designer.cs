namespace EmatWinFormsNetFramework1402.Formularios
{
    partial class frmImprimir
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
            this.grbIntervaloNMatricula = new System.Windows.Forms.GroupBox();
            this.txtNmatFinal = new System.Windows.Forms.TextBox();
            this.txtNmatInicial = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grbParametrosData = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mtbDataRelatorio = new System.Windows.Forms.MaskedTextBox();
            this.mtbDatFim = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mtbDatIni = new System.Windows.Forms.MaskedTextBox();
            this.grbEnsino = new System.Windows.Forms.GroupBox();
            this.rdbMedio = new System.Windows.Forms.RadioButton();
            this.rdbFundamental = new System.Windows.Forms.RadioButton();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.cmbOpcaoRelatorio = new System.Windows.Forms.ComboBox();
            this.grbOpcoes.SuspendLayout();
            this.grbIntervaloNMatricula.SuspendLayout();
            this.grbParametrosData.SuspendLayout();
            this.grbEnsino.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbOpcoes
            // 
            this.grbOpcoes.AutoSize = true;
            this.grbOpcoes.Controls.Add(this.grbIntervaloNMatricula);
            this.grbOpcoes.Controls.Add(this.grbParametrosData);
            this.grbOpcoes.Controls.Add(this.grbEnsino);
            this.grbOpcoes.Controls.Add(this.btnImprimir);
            this.grbOpcoes.Controls.Add(this.cmbOpcaoRelatorio);
            this.grbOpcoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbOpcoes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbOpcoes.Location = new System.Drawing.Point(0, 0);
            this.grbOpcoes.Name = "grbOpcoes";
            this.grbOpcoes.Size = new System.Drawing.Size(422, 217);
            this.grbOpcoes.TabIndex = 0;
            this.grbOpcoes.TabStop = false;
            this.grbOpcoes.Text = "Selecione o Relatório";
            // 
            // grbIntervaloNMatricula
            // 
            this.grbIntervaloNMatricula.Controls.Add(this.txtNmatFinal);
            this.grbIntervaloNMatricula.Controls.Add(this.txtNmatInicial);
            this.grbIntervaloNMatricula.Controls.Add(this.label1);
            this.grbIntervaloNMatricula.Location = new System.Drawing.Point(10, 51);
            this.grbIntervaloNMatricula.Name = "grbIntervaloNMatricula";
            this.grbIntervaloNMatricula.Size = new System.Drawing.Size(308, 44);
            this.grbIntervaloNMatricula.TabIndex = 15;
            this.grbIntervaloNMatricula.TabStop = false;
            this.grbIntervaloNMatricula.Text = "Etiquetas Prontuário";
            // 
            // txtNmatFinal
            // 
            this.txtNmatFinal.Enabled = false;
            this.txtNmatFinal.Location = new System.Drawing.Point(235, 15);
            this.txtNmatFinal.Name = "txtNmatFinal";
            this.txtNmatFinal.Size = new System.Drawing.Size(64, 22);
            this.txtNmatFinal.TabIndex = 3;
            // 
            // txtNmatInicial
            // 
            this.txtNmatInicial.Enabled = false;
            this.txtNmatInicial.Location = new System.Drawing.Point(165, 15);
            this.txtNmatInicial.Name = "txtNmatInicial";
            this.txtNmatInicial.Size = new System.Drawing.Size(64, 22);
            this.txtNmatInicial.TabIndex = 2;
            this.txtNmatInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Intervalo Nº de Matrícula";
            // 
            // grbParametrosData
            // 
            this.grbParametrosData.Controls.Add(this.label2);
            this.grbParametrosData.Controls.Add(this.mtbDataRelatorio);
            this.grbParametrosData.Controls.Add(this.mtbDatFim);
            this.grbParametrosData.Controls.Add(this.label3);
            this.grbParametrosData.Controls.Add(this.label4);
            this.grbParametrosData.Controls.Add(this.mtbDatIni);
            this.grbParametrosData.Location = new System.Drawing.Point(10, 101);
            this.grbParametrosData.Name = "grbParametrosData";
            this.grbParametrosData.Size = new System.Drawing.Size(308, 106);
            this.grbParametrosData.TabIndex = 14;
            this.grbParametrosData.TabStop = false;
            this.grbParametrosData.Text = "Parâmetros";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(42, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Data Relatório:";
            // 
            // mtbDataRelatorio
            // 
            this.mtbDataRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtbDataRelatorio.Location = new System.Drawing.Point(146, 15);
            this.mtbDataRelatorio.Mask = "00/00/0000";
            this.mtbDataRelatorio.Name = "mtbDataRelatorio";
            this.mtbDataRelatorio.Size = new System.Drawing.Size(87, 22);
            this.mtbDataRelatorio.TabIndex = 6;
            this.mtbDataRelatorio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mtbDatFim
            // 
            this.mtbDatFim.Enabled = false;
            this.mtbDatFim.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtbDatFim.Location = new System.Drawing.Point(146, 71);
            this.mtbDatFim.Mask = "00/00/0000";
            this.mtbDatFim.Name = "mtbDatFim";
            this.mtbDatFim.Size = new System.Drawing.Size(87, 22);
            this.mtbDatFim.TabIndex = 10;
            this.mtbDatFim.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(63, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Data Inicial:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(68, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Data Final:";
            // 
            // mtbDatIni
            // 
            this.mtbDatIni.Enabled = false;
            this.mtbDatIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtbDatIni.Location = new System.Drawing.Point(146, 43);
            this.mtbDatIni.Mask = "00/00/0000";
            this.mtbDatIni.Name = "mtbDatIni";
            this.mtbDatIni.Size = new System.Drawing.Size(87, 22);
            this.mtbDatIni.TabIndex = 8;
            this.mtbDatIni.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grbEnsino
            // 
            this.grbEnsino.Controls.Add(this.rdbMedio);
            this.grbEnsino.Controls.Add(this.rdbFundamental);
            this.grbEnsino.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbEnsino.Location = new System.Drawing.Point(324, 51);
            this.grbEnsino.Name = "grbEnsino";
            this.grbEnsino.Size = new System.Drawing.Size(89, 44);
            this.grbEnsino.TabIndex = 13;
            this.grbEnsino.TabStop = false;
            this.grbEnsino.Text = "Ensino";
            // 
            // rdbMedio
            // 
            this.rdbMedio.AutoSize = true;
            this.rdbMedio.Location = new System.Drawing.Point(46, 21);
            this.rdbMedio.Name = "rdbMedio";
            this.rdbMedio.Size = new System.Drawing.Size(37, 20);
            this.rdbMedio.TabIndex = 12;
            this.rdbMedio.TabStop = true;
            this.rdbMedio.Text = "M";
            this.rdbMedio.UseVisualStyleBackColor = true;
            // 
            // rdbFundamental
            // 
            this.rdbFundamental.AutoSize = true;
            this.rdbFundamental.Location = new System.Drawing.Point(6, 21);
            this.rdbFundamental.Name = "rdbFundamental";
            this.rdbFundamental.Size = new System.Drawing.Size(34, 20);
            this.rdbFundamental.TabIndex = 11;
            this.rdbFundamental.TabStop = true;
            this.rdbFundamental.Text = "F";
            this.rdbFundamental.UseVisualStyleBackColor = true;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Location = new System.Drawing.Point(324, 175);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(89, 32);
            this.btnImprimir.TabIndex = 4;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // cmbOpcaoRelatorio
            // 
            this.cmbOpcaoRelatorio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpcaoRelatorio.FormattingEnabled = true;
            this.cmbOpcaoRelatorio.Location = new System.Drawing.Point(10, 21);
            this.cmbOpcaoRelatorio.Name = "cmbOpcaoRelatorio";
            this.cmbOpcaoRelatorio.Size = new System.Drawing.Size(403, 24);
            this.cmbOpcaoRelatorio.TabIndex = 0;
            this.cmbOpcaoRelatorio.SelectedIndexChanged += new System.EventHandler(this.cmbOpcaoRelatorio_SelectedIndexChanged);
            // 
            // frmImprimir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 217);
            this.Controls.Add(this.grbOpcoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImprimir";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Imprimir";
            this.Load += new System.EventHandler(this.frImprimir_Load);
            this.grbOpcoes.ResumeLayout(false);
            this.grbIntervaloNMatricula.ResumeLayout(false);
            this.grbIntervaloNMatricula.PerformLayout();
            this.grbParametrosData.ResumeLayout(false);
            this.grbParametrosData.PerformLayout();
            this.grbEnsino.ResumeLayout(false);
            this.grbEnsino.PerformLayout();
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
        private System.Windows.Forms.MaskedTextBox mtbDatFim;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox mtbDatIni;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grbIntervaloNMatricula;
        private System.Windows.Forms.GroupBox grbParametrosData;
        private System.Windows.Forms.GroupBox grbEnsino;
        private System.Windows.Forms.RadioButton rdbMedio;
        private System.Windows.Forms.RadioButton rdbFundamental;
    }
}