namespace EmatWinFormsNetFramework13032.Notas
{
    partial class frPassaporte
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbcPassaporte = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.flpPassaporteIndividual = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flpPassaporteCompleto = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvDescricoes = new System.Windows.Forms.DataGridView();
            this.colDescricao0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescricao1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDescricao2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsOpcoes_dgv = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.excluirAtendimentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblDisciplinaAtual = new System.Windows.Forms.Label();
            this.lblNmat = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDatMat = new System.Windows.Forms.Label();
            this.lblTermo = new System.Windows.Forms.Label();
            this.lblRg = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblObs = new System.Windows.Forms.Label();
            this.txtObs = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.picFoto = new System.Windows.Forms.PictureBox();
            this.btnTroca_ensino = new System.Windows.Forms.Button();
            this.ttAjuda = new System.Windows.Forms.ToolTip(this.components);
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.tssl1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ttTipo_atendimento = new System.Windows.Forms.ToolTip(this.components);
            this.tbcPassaporte.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDescricoes)).BeginInit();
            this.cmsOpcoes_dgv.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcPassaporte
            // 
            this.tbcPassaporte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcPassaporte.Controls.Add(this.tabPage1);
            this.tbcPassaporte.Controls.Add(this.tabPage2);
            this.tbcPassaporte.Controls.Add(this.tabPage3);
            this.tbcPassaporte.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcPassaporte.Location = new System.Drawing.Point(12, 159);
            this.tbcPassaporte.Name = "tbcPassaporte";
            this.tbcPassaporte.SelectedIndex = 0;
            this.tbcPassaporte.Size = new System.Drawing.Size(812, 268);
            this.tbcPassaporte.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.flpPassaporteIndividual);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabPage1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(804, 237);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Passaporte Individual";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // flpPassaporteIndividual
            // 
            this.flpPassaporteIndividual.AutoScroll = true;
            this.flpPassaporteIndividual.BackColor = System.Drawing.Color.Transparent;
            this.flpPassaporteIndividual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPassaporteIndividual.Location = new System.Drawing.Point(3, 3);
            this.flpPassaporteIndividual.Margin = new System.Windows.Forms.Padding(0);
            this.flpPassaporteIndividual.Name = "flpPassaporteIndividual";
            this.flpPassaporteIndividual.Size = new System.Drawing.Size(798, 231);
            this.flpPassaporteIndividual.TabIndex = 14;
            this.flpPassaporteIndividual.Resize += new System.EventHandler(this.flpPassaporteIndividual_Resize);
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.flpPassaporteCompleto);
            this.tabPage2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabPage2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(804, 237);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Passaporte Completo";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flpPassaporteCompleto
            // 
            this.flpPassaporteCompleto.AutoScroll = true;
            this.flpPassaporteCompleto.BackColor = System.Drawing.Color.Transparent;
            this.flpPassaporteCompleto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPassaporteCompleto.Location = new System.Drawing.Point(3, 3);
            this.flpPassaporteCompleto.Margin = new System.Windows.Forms.Padding(0);
            this.flpPassaporteCompleto.Name = "flpPassaporteCompleto";
            this.flpPassaporteCompleto.Size = new System.Drawing.Size(798, 231);
            this.flpPassaporteCompleto.TabIndex = 15;
            this.flpPassaporteCompleto.Resize += new System.EventHandler(this.flpPassaporteCompleto_Resize);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvDescricoes);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(804, 237);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Descrições";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvDescricoes
            // 
            this.dgvDescricoes.AllowUserToAddRows = false;
            this.dgvDescricoes.AllowUserToDeleteRows = false;
            this.dgvDescricoes.AllowUserToResizeColumns = false;
            this.dgvDescricoes.AllowUserToResizeRows = false;
            this.dgvDescricoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDescricoes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDescricao0,
            this.colDescricao1,
            this.colDescricao2});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDescricoes.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDescricoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDescricoes.Location = new System.Drawing.Point(0, 0);
            this.dgvDescricoes.Name = "dgvDescricoes";
            this.dgvDescricoes.RowHeadersVisible = false;
            this.dgvDescricoes.Size = new System.Drawing.Size(804, 237);
            this.dgvDescricoes.TabIndex = 0;
            // 
            // colDescricao0
            // 
            this.colDescricao0.HeaderText = "Column1";
            this.colDescricao0.Name = "colDescricao0";
            this.colDescricao0.ReadOnly = true;
            this.colDescricao0.Visible = false;
            // 
            // colDescricao1
            // 
            this.colDescricao1.HeaderText = "Opções";
            this.colDescricao1.Name = "colDescricao1";
            this.colDescricao1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDescricao1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colDescricao1.Width = 80;
            // 
            // colDescricao2
            // 
            this.colDescricao2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescricao2.HeaderText = "Descrição";
            this.colDescricao2.Name = "colDescricao2";
            this.colDescricao2.ReadOnly = true;
            // 
            // cmsOpcoes_dgv
            // 
            this.cmsOpcoes_dgv.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excluirAtendimentoToolStripMenuItem});
            this.cmsOpcoes_dgv.Name = "cmsOpcoes_dgv";
            this.cmsOpcoes_dgv.Size = new System.Drawing.Size(182, 26);
            // 
            // excluirAtendimentoToolStripMenuItem
            // 
            this.excluirAtendimentoToolStripMenuItem.Name = "excluirAtendimentoToolStripMenuItem";
            this.excluirAtendimentoToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.excluirAtendimentoToolStripMenuItem.Text = "Excluir Atendimento";
            this.excluirAtendimentoToolStripMenuItem.Click += new System.EventHandler(this.excluirAtendimentoToolStripMenuItem_Click);
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.BackColor = System.Drawing.Color.White;
            this.lblNome.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.Location = new System.Drawing.Point(0, 31);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(63, 19);
            this.lblNome.TabIndex = 1;
            this.lblNome.Text = "NOME:";
            // 
            // lblDisciplinaAtual
            // 
            this.lblDisciplinaAtual.AutoSize = true;
            this.lblDisciplinaAtual.BackColor = System.Drawing.Color.White;
            this.lblDisciplinaAtual.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisciplinaAtual.Location = new System.Drawing.Point(1, 54);
            this.lblDisciplinaAtual.Name = "lblDisciplinaAtual";
            this.lblDisciplinaAtual.Size = new System.Drawing.Size(161, 19);
            this.lblDisciplinaAtual.TabIndex = 2;
            this.lblDisciplinaAtual.Text = "DISCIPLINA ATUAL:";
            // 
            // lblNmat
            // 
            this.lblNmat.AutoSize = true;
            this.lblNmat.BackColor = System.Drawing.Color.White;
            this.lblNmat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNmat.Location = new System.Drawing.Point(0, 0);
            this.lblNmat.Name = "lblNmat";
            this.lblNmat.Size = new System.Drawing.Size(109, 19);
            this.lblNmat.TabIndex = 0;
            this.lblNmat.Text = "MATRÍCULA:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblNmat);
            this.panel1.Controls.Add(this.lblDatMat);
            this.panel1.Controls.Add(this.lblTermo);
            this.panel1.Controls.Add(this.lblRg);
            this.panel1.Controls.Add(this.lblDisciplinaAtual);
            this.panel1.Controls.Add(this.lblNome);
            this.panel1.Location = new System.Drawing.Point(12, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 75);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblDatMat
            // 
            this.lblDatMat.AutoSize = true;
            this.lblDatMat.BackColor = System.Drawing.Color.White;
            this.lblDatMat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatMat.Location = new System.Drawing.Point(408, 31);
            this.lblDatMat.Name = "lblDatMat";
            this.lblDatMat.Size = new System.Drawing.Size(180, 19);
            this.lblDatMat.TabIndex = 25;
            this.lblDatMat.Text = "DATA DA MATRÍCULA:";
            // 
            // lblTermo
            // 
            this.lblTermo.AutoSize = true;
            this.lblTermo.BackColor = System.Drawing.Color.White;
            this.lblTermo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTermo.Location = new System.Drawing.Point(380, 0);
            this.lblTermo.Name = "lblTermo";
            this.lblTermo.Size = new System.Drawing.Size(73, 19);
            this.lblTermo.TabIndex = 28;
            this.lblTermo.Text = "TERMO:";
            // 
            // lblRg
            // 
            this.lblRg.AutoSize = true;
            this.lblRg.BackColor = System.Drawing.Color.White;
            this.lblRg.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRg.Location = new System.Drawing.Point(190, 0);
            this.lblRg.Name = "lblRg";
            this.lblRg.Size = new System.Drawing.Size(39, 19);
            this.lblRg.TabIndex = 27;
            this.lblRg.Text = "RG:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblObs);
            this.panel2.Controls.Add(this.txtObs);
            this.panel2.Location = new System.Drawing.Point(12, 127);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(704, 19);
            this.panel2.TabIndex = 30;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // lblObs
            // 
            this.lblObs.AutoSize = true;
            this.lblObs.BackColor = System.Drawing.Color.White;
            this.lblObs.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObs.Location = new System.Drawing.Point(0, 0);
            this.lblObs.Name = "lblObs";
            this.lblObs.Size = new System.Drawing.Size(129, 19);
            this.lblObs.TabIndex = 31;
            this.lblObs.Text = "OBSERVAÇÃO:";
            // 
            // txtObs
            // 
            this.txtObs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtObs.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObs.Location = new System.Drawing.Point(129, 0);
            this.txtObs.Name = "txtObs";
            this.txtObs.Size = new System.Drawing.Size(581, 19);
            this.txtObs.TabIndex = 30;
            this.txtObs.Leave += new System.EventHandler(this.txtObs_Leave);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(8, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(235, 24);
            this.lblTitulo.TabIndex = 31;
            this.lblTitulo.Text = "PASSAPORTE ENSINO";
            // 
            // picFoto
            // 
            this.picFoto.BackColor = System.Drawing.Color.White;
            this.picFoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picFoto.Location = new System.Drawing.Point(728, 41);
            this.picFoto.Name = "picFoto";
            this.picFoto.Size = new System.Drawing.Size(96, 112);
            this.picFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFoto.TabIndex = 10;
            this.picFoto.TabStop = false;
            // 
            // btnTroca_ensino
            // 
            this.btnTroca_ensino.BackColor = System.Drawing.Color.Transparent;
            this.btnTroca_ensino.BackgroundImage = global::EmatWinFormsNetFramework13032.Properties.Resources.Loop_16xLG;
            this.btnTroca_ensino.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTroca_ensino.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTroca_ensino.Location = new System.Drawing.Point(798, 12);
            this.btnTroca_ensino.Name = "btnTroca_ensino";
            this.btnTroca_ensino.Size = new System.Drawing.Size(26, 23);
            this.btnTroca_ensino.TabIndex = 29;
            this.ttAjuda.SetToolTip(this.btnTroca_ensino, "Trocar Ensino");
            this.btnTroca_ensino.UseVisualStyleBackColor = false;
            this.btnTroca_ensino.Click += new System.EventHandler(this.btnTroca_ensino_Click);
            // 
            // ttAjuda
            // 
            this.ttAjuda.IsBalloon = true;
            this.ttAjuda.Tag = "";
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisa.BackgroundImage = global::EmatWinFormsNetFramework13032.Properties.Resources.magnifier_16xLG;
            this.btnPesquisa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPesquisa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisa.Location = new System.Drawing.Point(728, 12);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(64, 23);
            this.btnPesquisa.TabIndex = 33;
            this.ttAjuda.SetToolTip(this.btnPesquisa, "Nova Pesquisa");
            this.btnPesquisa.UseVisualStyleBackColor = false;
            this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
            // 
            // tssl1
            // 
            this.tssl1.BackColor = System.Drawing.Color.Transparent;
            this.tssl1.Name = "tssl1";
            this.tssl1.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(834, 22);
            this.statusStrip1.TabIndex = 32;
            // 
            // frPassaporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(834, 461);
            this.Controls.Add(this.btnPesquisa);
            this.Controls.Add(this.btnTroca_ensino);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picFoto);
            this.Controls.Add(this.tbcPassaporte);
            this.MinimumSize = new System.Drawing.Size(850, 39);
            this.Name = "frPassaporte";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Passporte Aluno";
            this.Load += new System.EventHandler(this.FRpassaporte_Load);
            this.Shown += new System.EventHandler(this.frPassaporte_Shown);
            this.Resize += new System.EventHandler(this.frPassaporte_Resize);
            this.tbcPassaporte.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDescricoes)).EndInit();
            this.cmsOpcoes_dgv.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox picFoto;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblDisciplinaAtual;
        private System.Windows.Forms.Label lblNmat;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDatMat;
        private System.Windows.Forms.Label lblRg;
        private System.Windows.Forms.Label lblTermo;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblObs;
        private System.Windows.Forms.TextBox txtObs;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tbcPassaporte;
        private System.Windows.Forms.ContextMenuStrip cmsOpcoes_dgv;
        private System.Windows.Forms.ToolStripMenuItem excluirAtendimentoToolStripMenuItem;
        private System.Windows.Forms.Button btnTroca_ensino;
        private System.Windows.Forms.ToolTip ttAjuda;
        private System.Windows.Forms.ToolStripStatusLabel tssl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolTip ttTipo_atendimento;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvDescricoes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescricao0;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDescricao1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescricao2;
        private System.Windows.Forms.FlowLayoutPanel flpPassaporteIndividual;       
        private System.Windows.Forms.FlowLayoutPanel flpPassaporteCompleto;
    }
}