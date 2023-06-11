namespace EmatWinFormsNetFramework1402.Formularios
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
            this.btnImprimir = new System.Windows.Forms.Button();
            this.txtAddEtiqueta = new System.Windows.Forms.TextBox();
            this.btnAddEtiqueta = new System.Windows.Forms.Button();
            this.lblQtdEtiquetas = new System.Windows.Forms.Label();
            this.lblQtdFolhas = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ltbLista = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnImprimir
            // 
            this.btnImprimir.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Location = new System.Drawing.Point(156, 410);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(100, 29);
            this.btnImprimir.TabIndex = 10;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // txtAddEtiqueta
            // 
            this.txtAddEtiqueta.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddEtiqueta.Location = new System.Drawing.Point(13, 39);
            this.txtAddEtiqueta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAddEtiqueta.Name = "txtAddEtiqueta";
            this.txtAddEtiqueta.Size = new System.Drawing.Size(135, 26);
            this.txtAddEtiqueta.TabIndex = 13;
            this.txtAddEtiqueta.TextChanged += new System.EventHandler(this.txtAddEtiqueta_TextChanged);
            // 
            // btnAddEtiqueta
            // 
            this.btnAddEtiqueta.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddEtiqueta.Location = new System.Drawing.Point(160, 39);
            this.btnAddEtiqueta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddEtiqueta.Name = "btnAddEtiqueta";
            this.btnAddEtiqueta.Size = new System.Drawing.Size(98, 26);
            this.btnAddEtiqueta.TabIndex = 14;
            this.btnAddEtiqueta.Text = "Adicionar";
            this.btnAddEtiqueta.UseVisualStyleBackColor = true;
            this.btnAddEtiqueta.Click += new System.EventHandler(this.btnAddEtiqueta_Click);
            // 
            // lblQtdEtiquetas
            // 
            this.lblQtdEtiquetas.AutoSize = true;
            this.lblQtdEtiquetas.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtdEtiquetas.Location = new System.Drawing.Point(156, 85);
            this.lblQtdEtiquetas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQtdEtiquetas.Name = "lblQtdEtiquetas";
            this.lblQtdEtiquetas.Size = new System.Drawing.Size(79, 20);
            this.lblQtdEtiquetas.TabIndex = 15;
            this.lblQtdEtiquetas.Text = "Etiquetas: 0";
            // 
            // lblQtdFolhas
            // 
            this.lblQtdFolhas.AutoSize = true;
            this.lblQtdFolhas.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtdFolhas.Location = new System.Drawing.Point(156, 119);
            this.lblQtdFolhas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQtdFolhas.Name = "lblQtdFolhas";
            this.lblQtdFolhas.Size = new System.Drawing.Size(64, 20);
            this.lblQtdFolhas.TabIndex = 16;
            this.lblQtdFolhas.Text = "Folhas: 0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Nº Matrícula";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // ltbLista
            // 
            this.ltbLista.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ltbLista.FormattingEnabled = true;
            this.ltbLista.ItemHeight = 20;
            this.ltbLista.Location = new System.Drawing.Point(13, 75);
            this.ltbLista.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ltbLista.Name = "ltbLista";
            this.ltbLista.Size = new System.Drawing.Size(135, 364);
            this.ltbLista.TabIndex = 12;
            this.ltbLista.SelectedIndexChanged += new System.EventHandler(this.ltbLista_SelectedIndexChanged);
            // 
            // frImpressao_etiqueta
            // 
            this.AcceptButton = this.btnAddEtiqueta;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblQtdFolhas);
            this.Controls.Add(this.lblQtdEtiquetas);
            this.Controls.Add(this.btnAddEtiqueta);
            this.Controls.Add(this.txtAddEtiqueta);
            this.Controls.Add(this.ltbLista);
            this.Controls.Add(this.btnImprimir);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frImpressao_etiqueta";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.TextBox txtAddEtiqueta;
        private System.Windows.Forms.Button btnAddEtiqueta;
        private System.Windows.Forms.Label lblQtdEtiquetas;
        private System.Windows.Forms.Label lblQtdFolhas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox ltbLista;
    }
}