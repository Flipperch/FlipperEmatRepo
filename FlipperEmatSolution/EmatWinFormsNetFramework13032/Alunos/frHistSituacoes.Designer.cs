namespace EmatWinFormsNetFramework13032.Alunos
{
    partial class frHistSituacoes
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
            this.components = new System.ComponentModel.Container();
            this.dgvHistoricoSituacoes = new System.Windows.Forms.DataGridView();
            this.cmsOpcoes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.excluirRegistroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoricoSituacoes)).BeginInit();
            this.cmsOpcoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvHistoricoSituacoes
            // 
            this.dgvHistoricoSituacoes.AllowUserToAddRows = false;
            this.dgvHistoricoSituacoes.AllowUserToDeleteRows = false;
            this.dgvHistoricoSituacoes.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistoricoSituacoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistoricoSituacoes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dgvHistoricoSituacoes.ContextMenuStrip = this.cmsOpcoes;
            this.dgvHistoricoSituacoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistoricoSituacoes.Location = new System.Drawing.Point(0, 0);
            this.dgvHistoricoSituacoes.MultiSelect = false;
            this.dgvHistoricoSituacoes.Name = "dgvHistoricoSituacoes";
            this.dgvHistoricoSituacoes.RowHeadersVisible = false;
            this.dgvHistoricoSituacoes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistoricoSituacoes.Size = new System.Drawing.Size(784, 311);
            this.dgvHistoricoSituacoes.TabIndex = 0;
            this.dgvHistoricoSituacoes.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvHistoricoSituacoes_CellMouseDown);
            // 
            // cmsOpcoes
            // 
            this.cmsOpcoes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excluirRegistroToolStripMenuItem});
            this.cmsOpcoes.Name = "cmsOpcoes";
            this.cmsOpcoes.Size = new System.Drawing.Size(155, 26);
            // 
            // excluirRegistroToolStripMenuItem
            // 
            this.excluirRegistroToolStripMenuItem.Name = "excluirRegistroToolStripMenuItem";
            this.excluirRegistroToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.excluirRegistroToolStripMenuItem.Text = "Excluir Registro";
            this.excluirRegistroToolStripMenuItem.Click += new System.EventHandler(this.excluirRegistroToolStripMenuItem_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "id_historico_situacao";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column2.HeaderText = "Situação";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 74;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column3.HeaderText = "Data";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 55;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "Motivo";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Usuário";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // frSituacoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 311);
            this.Controls.Add(this.dgvHistoricoSituacoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frSituacoes";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Histórico de Situações";
            this.Load += new System.EventHandler(this.frSituacoes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistoricoSituacoes)).EndInit();
            this.cmsOpcoes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvHistoricoSituacoes;
        private System.Windows.Forms.ContextMenuStrip cmsOpcoes;
        private System.Windows.Forms.ToolStripMenuItem excluirRegistroToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}