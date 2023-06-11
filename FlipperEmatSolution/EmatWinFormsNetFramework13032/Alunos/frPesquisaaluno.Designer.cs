namespace EmatWinFormsNetFramework13032.Alunos
{
    partial class frPesquisaaluno
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frPesquisaaluno));
            this.txtAluno = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtnmat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtrg = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvaluno_resultado = new System.Windows.Forms.DataGridView();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.ckbConcluentes = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvaluno_resultado)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtAluno
            // 
            this.txtAluno.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAluno.Location = new System.Drawing.Point(12, 49);
            this.txtAluno.Name = "txtAluno";
            this.txtAluno.Size = new System.Drawing.Size(371, 26);
            this.txtAluno.TabIndex = 0;
            this.txtAluno.Click += new System.EventHandler(this.txtAluno_Click);
            this.txtAluno.TextChanged += new System.EventHandler(this.txtAluno_TextChanged);
            this.txtAluno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAluno_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome";
            // 
            // txtnmat
            // 
            this.txtnmat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnmat.Location = new System.Drawing.Point(530, 48);
            this.txtnmat.Name = "txtnmat";
            this.txtnmat.Size = new System.Drawing.Size(102, 26);
            this.txtnmat.TabIndex = 2;
            this.txtnmat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtnmat_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(526, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "N. Mátricula";
            // 
            // txtrg
            // 
            this.txtrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrg.Location = new System.Drawing.Point(389, 48);
            this.txtrg.Name = "txtrg";
            this.txtrg.Size = new System.Drawing.Size(135, 26);
            this.txtrg.TabIndex = 1;
            this.txtrg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtrg_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(385, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "RG";
            // 
            // dgvaluno_resultado
            // 
            this.dgvaluno_resultado.AllowUserToAddRows = false;
            this.dgvaluno_resultado.AllowUserToDeleteRows = false;
            this.dgvaluno_resultado.AllowUserToResizeRows = false;
            this.dgvaluno_resultado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvaluno_resultado.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvaluno_resultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvaluno_resultado.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ButtonShadow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvaluno_resultado.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvaluno_resultado.Location = new System.Drawing.Point(12, 80);
            this.dgvaluno_resultado.Name = "dgvaluno_resultado";
            this.dgvaluno_resultado.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ButtonShadow;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvaluno_resultado.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvaluno_resultado.RowHeadersVisible = false;
            this.dgvaluno_resultado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvaluno_resultado.Size = new System.Drawing.Size(620, 200);
            this.dgvaluno_resultado.TabIndex = 4;
            this.dgvaluno_resultado.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvaluno_resultado_CellDoubleClick);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.Location = new System.Drawing.Point(639, 48);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(105, 27);
            this.btnFiltrar.TabIndex = 3;
            this.btnFiltrar.Text = "Pesquisar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // ckbConcluentes
            // 
            this.ckbConcluentes.AutoSize = true;
            this.ckbConcluentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbConcluentes.Location = new System.Drawing.Point(473, 286);
            this.ckbConcluentes.Name = "ckbConcluentes";
            this.ckbConcluentes.Size = new System.Drawing.Size(153, 24);
            this.ckbConcluentes.TabIndex = 5;
            this.ckbConcluentes.Text = "Exibir Concluintes";
            this.ckbConcluentes.UseVisualStyleBackColor = true;
            this.ckbConcluentes.CheckedChanged += new System.EventHandler(this.ckbConcluentes_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 314);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(756, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl1
            // 
            this.tssl1.BackColor = System.Drawing.Color.Transparent;
            this.tssl1.ForeColor = System.Drawing.Color.Black;
            this.tssl1.Name = "tssl1";
            this.tssl1.Size = new System.Drawing.Size(30, 17);
            this.tssl1.Text = "tssl1";
            // 
            // frPesquisaaluno
            // 
            this.AcceptButton = this.btnFiltrar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 336);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ckbConcluentes);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.dgvaluno_resultado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtrg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtnmat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAluno);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frPesquisaaluno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisar Aluno";
            this.Load += new System.EventHandler(this.FRpesquisaaluno_Load);
            this.Shown += new System.EventHandler(this.frPesquisaaluno_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvaluno_resultado)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAluno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtnmat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtrg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvaluno_resultado;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.CheckBox ckbConcluentes;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl1;
    }
}