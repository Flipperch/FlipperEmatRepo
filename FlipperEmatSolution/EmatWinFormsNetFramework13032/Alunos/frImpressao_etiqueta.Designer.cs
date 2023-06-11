namespace EmatWinFormsNetFramework13032.Alunos
{
    partial class frImpressao_etiqueta
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
            this.txbN_mat1 = new System.Windows.Forms.TextBox();
            this.txbN_mat2 = new System.Windows.Forms.TextBox();
            this.txbN_mat3 = new System.Windows.Forms.TextBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txbN_mat5 = new System.Windows.Forms.TextBox();
            this.txbN_mat4 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txbN_mat1
            // 
            this.txbN_mat1.Location = new System.Drawing.Point(12, 12);
            this.txbN_mat1.Name = "txbN_mat1";
            this.txbN_mat1.Size = new System.Drawing.Size(100, 20);
            this.txbN_mat1.TabIndex = 0;
            // 
            // txbN_mat2
            // 
            this.txbN_mat2.Location = new System.Drawing.Point(12, 38);
            this.txbN_mat2.Name = "txbN_mat2";
            this.txbN_mat2.Size = new System.Drawing.Size(100, 20);
            this.txbN_mat2.TabIndex = 1;
            // 
            // txbN_mat3
            // 
            this.txbN_mat3.Location = new System.Drawing.Point(12, 64);
            this.txbN_mat3.Name = "txbN_mat3";
            this.txbN_mat3.Size = new System.Drawing.Size(100, 20);
            this.txbN_mat3.TabIndex = 2;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(25, 146);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 5;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(25, 175);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txbN_mat5
            // 
            this.txbN_mat5.Location = new System.Drawing.Point(12, 116);
            this.txbN_mat5.Name = "txbN_mat5";
            this.txbN_mat5.Size = new System.Drawing.Size(100, 20);
            this.txbN_mat5.TabIndex = 4;
            // 
            // txbN_mat4
            // 
            this.txbN_mat4.Location = new System.Drawing.Point(12, 90);
            this.txbN_mat4.Name = "txbN_mat4";
            this.txbN_mat4.Size = new System.Drawing.Size(100, 20);
            this.txbN_mat4.TabIndex = 3;
            // 
            // frImpressao_etiqueta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(124, 214);
            this.Controls.Add(this.txbN_mat5);
            this.Controls.Add(this.txbN_mat4);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.txbN_mat3);
            this.Controls.Add(this.txbN_mat2);
            this.Controls.Add(this.txbN_mat1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frImpressao_etiqueta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbN_mat1;
        private System.Windows.Forms.TextBox txbN_mat2;
        private System.Windows.Forms.TextBox txbN_mat3;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txbN_mat5;
        private System.Windows.Forms.TextBox txbN_mat4;
    }
}