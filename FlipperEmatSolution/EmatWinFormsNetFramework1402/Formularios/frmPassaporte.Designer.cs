namespace EmatWinFormsNetFramework1402.Formularios
{
    partial class frmPassaporte
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmsOpcoes_dgv = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modificarAtendimentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excluirAtendimentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ttAjuda = new System.Windows.Forms.ToolTip(this.components);
            this.btnPesquisa = new System.Windows.Forms.Button();
            this.tssl1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ttTipo_atendimento = new System.Windows.Forms.ToolTip(this.components);
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvDescricoes = new System.Windows.Forms.DataGridView();
            this.colDescricao2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescricao1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDescricao0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabFundCom = new System.Windows.Forms.TabPage();
            this.flpFunCom = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tbcHistorico = new System.Windows.Forms.TabControl();
            this.tabFundamental = new System.Windows.Forms.TabPage();
            this.tbcPassaporteFundamental = new System.Windows.Forms.TabControl();
            this.tabFundInd = new System.Windows.Forms.TabPage();
            this.flpFunInd = new System.Windows.Forms.FlowLayoutPanel();
            this.flpGradeFundamental = new System.Windows.Forms.FlowLayoutPanel();
            this.tabMedio = new System.Windows.Forms.TabPage();
            this.tbcPassaporteMedio = new System.Windows.Forms.TabControl();
            this.tabMedInd = new System.Windows.Forms.TabPage();
            this.flpMedInd = new System.Windows.Forms.FlowLayoutPanel();
            this.tabMedCom = new System.Windows.Forms.TabPage();
            this.flpMedCom = new System.Windows.Forms.FlowLayoutPanel();
            this.flpGradeMedio = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlDadosAluno = new System.Windows.Forms.Panel();
            this.lblNmat = new System.Windows.Forms.Label();
            this.lblDatMat = new System.Windows.Forms.Label();
            this.lblTermo = new System.Windows.Forms.Label();
            this.lblRg = new System.Windows.Forms.Label();
            this.lblDisciplinaAtual = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.picFoto = new System.Windows.Forms.PictureBox();
            this.pAtvExtra = new System.Windows.Forms.Panel();
            this.lblDataAtvExtra = new System.Windows.Forms.Label();
            this.ckbAtvExtra = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlObs = new System.Windows.Forms.Panel();
            this.lblObs = new System.Windows.Forms.Label();
            this.txtObs = new System.Windows.Forms.TextBox();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.cmsOpcoes_dgv.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDescricoes)).BeginInit();
            this.tabFundCom.SuspendLayout();
            this.tbcHistorico.SuspendLayout();
            this.tabFundamental.SuspendLayout();
            this.tbcPassaporteFundamental.SuspendLayout();
            this.tabFundInd.SuspendLayout();
            this.tabMedio.SuspendLayout();
            this.tbcPassaporteMedio.SuspendLayout();
            this.tabMedInd.SuspendLayout();
            this.tabMedCom.SuspendLayout();
            this.pnlDadosAluno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            this.pAtvExtra.SuspendLayout();
            this.pnlObs.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsOpcoes_dgv
            // 
            this.cmsOpcoes_dgv.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarAtendimentoToolStripMenuItem,
            this.excluirAtendimentoToolStripMenuItem});
            this.cmsOpcoes_dgv.Name = "cmsOpcoes_dgv";
            this.cmsOpcoes_dgv.Size = new System.Drawing.Size(199, 48);
            // 
            // modificarAtendimentoToolStripMenuItem
            // 
            this.modificarAtendimentoToolStripMenuItem.Name = "modificarAtendimentoToolStripMenuItem";
            this.modificarAtendimentoToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.modificarAtendimentoToolStripMenuItem.Text = "Modificar Atendimento";
            this.modificarAtendimentoToolStripMenuItem.Visible = false;
            this.modificarAtendimentoToolStripMenuItem.Click += new System.EventHandler(this.modificarAtendimentoToolStripMenuItem_Click);
            // 
            // excluirAtendimentoToolStripMenuItem
            // 
            this.excluirAtendimentoToolStripMenuItem.Name = "excluirAtendimentoToolStripMenuItem";
            this.excluirAtendimentoToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.excluirAtendimentoToolStripMenuItem.Text = "Excluir Atendimento";
            this.excluirAtendimentoToolStripMenuItem.Click += new System.EventHandler(this.excluirAtendimentoToolStripMenuItem_Click);
            // 
            // ttAjuda
            // 
            this.ttAjuda.IsBalloon = true;
            this.ttAjuda.Tag = "";
            // 
            // btnPesquisa
            // 
            this.btnPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPesquisa.BackColor = System.Drawing.Color.Transparent;
            this.btnPesquisa.BackgroundImage = global::EmatWinFormsNetFramework1402.Properties.Resources.magnifier_16xLG;
            this.btnPesquisa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnPesquisa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisa.Location = new System.Drawing.Point(903, 14);
            this.btnPesquisa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPesquisa.Name = "btnPesquisa";
            this.btnPesquisa.Size = new System.Drawing.Size(94, 23);
            this.btnPesquisa.TabIndex = 37;
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1010, 22);
            this.statusStrip1.TabIndex = 32;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(1, 5);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(1, 5, 1, 5);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(885, 95);
            this.flowLayoutPanel2.TabIndex = 15;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // dgvDescricoes
            // 
            this.dgvDescricoes.AllowUserToAddRows = false;
            this.dgvDescricoes.AllowUserToDeleteRows = false;
            this.dgvDescricoes.AllowUserToResizeColumns = false;
            this.dgvDescricoes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDescricoes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDescricoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDescricoes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDescricoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDescricoes.Location = new System.Drawing.Point(0, 0);
            this.dgvDescricoes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvDescricoes.Name = "dgvDescricoes";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDescricoes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDescricoes.RowHeadersVisible = false;
            this.dgvDescricoes.Size = new System.Drawing.Size(1823, 641);
            this.dgvDescricoes.TabIndex = 0;
            // 
            // colDescricao2
            // 
            this.colDescricao2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescricao2.HeaderText = "Descrição";
            this.colDescricao2.Name = "colDescricao2";
            this.colDescricao2.ReadOnly = true;
            // 
            // colDescricao1
            // 
            this.colDescricao1.HeaderText = "Opções";
            this.colDescricao1.Name = "colDescricao1";
            this.colDescricao1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDescricao1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colDescricao1.Width = 80;
            // 
            // colDescricao0
            // 
            this.colDescricao0.HeaderText = "Column1";
            this.colDescricao0.Name = "colDescricao0";
            this.colDescricao0.ReadOnly = true;
            this.colDescricao0.Visible = false;
            // 
            // tabFundCom
            // 
            this.tabFundCom.AutoScroll = true;
            this.tabFundCom.Controls.Add(this.flpFunCom);
            this.tabFundCom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabFundCom.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabFundCom.Location = new System.Drawing.Point(4, 29);
            this.tabFundCom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabFundCom.Name = "tabFundCom";
            this.tabFundCom.Size = new System.Drawing.Size(973, 208);
            this.tabFundCom.TabIndex = 1;
            this.tabFundCom.Text = "Passaporte Completo";
            this.tabFundCom.UseVisualStyleBackColor = true;
            // 
            // flpFunCom
            // 
            this.flpFunCom.AutoScroll = true;
            this.flpFunCom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFunCom.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flpFunCom.Location = new System.Drawing.Point(0, 0);
            this.flpFunCom.Name = "flpFunCom";
            this.flpFunCom.Size = new System.Drawing.Size(973, 208);
            this.flpFunCom.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(16, 14);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(235, 24);
            this.lblTitulo.TabIndex = 31;
            this.lblTitulo.Text = "PASSAPORTE ENSINO";
            // 
            // tbcHistorico
            // 
            this.tbcHistorico.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcHistorico.Controls.Add(this.tabFundamental);
            this.tbcHistorico.Controls.Add(this.tabMedio);
            this.tbcHistorico.Location = new System.Drawing.Point(0, 182);
            this.tbcHistorico.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbcHistorico.Name = "tbcHistorico";
            this.tbcHistorico.SelectedIndex = 0;
            this.tbcHistorico.Size = new System.Drawing.Size(1006, 379);
            this.tbcHistorico.TabIndex = 34;
            this.tbcHistorico.SelectedIndexChanged += new System.EventHandler(this.tbcHistorico_SelectedIndexChanged);
            // 
            // tabFundamental
            // 
            this.tabFundamental.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.tabFundamental.Controls.Add(this.tbcPassaporteFundamental);
            this.tabFundamental.Controls.Add(this.flpGradeFundamental);
            this.tabFundamental.Location = new System.Drawing.Point(4, 29);
            this.tabFundamental.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabFundamental.Name = "tabFundamental";
            this.tabFundamental.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabFundamental.Size = new System.Drawing.Size(998, 346);
            this.tabFundamental.TabIndex = 0;
            this.tabFundamental.Text = "Fundamental";
            // 
            // tbcPassaporteFundamental
            // 
            this.tbcPassaporteFundamental.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcPassaporteFundamental.Controls.Add(this.tabFundInd);
            this.tbcPassaporteFundamental.Controls.Add(this.tabFundCom);
            this.tbcPassaporteFundamental.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcPassaporteFundamental.Location = new System.Drawing.Point(11, 78);
            this.tbcPassaporteFundamental.Margin = new System.Windows.Forms.Padding(0);
            this.tbcPassaporteFundamental.Name = "tbcPassaporteFundamental";
            this.tbcPassaporteFundamental.Padding = new System.Drawing.Point(0, 0);
            this.tbcPassaporteFundamental.SelectedIndex = 0;
            this.tbcPassaporteFundamental.Size = new System.Drawing.Size(981, 241);
            this.tbcPassaporteFundamental.TabIndex = 6;
            this.tbcPassaporteFundamental.SelectedIndexChanged += new System.EventHandler(this.tbcPassaporteFundamental_SelectedIndexChanged);
            // 
            // tabFundInd
            // 
            this.tabFundInd.Controls.Add(this.flpFunInd);
            this.tabFundInd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabFundInd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabFundInd.Location = new System.Drawing.Point(4, 29);
            this.tabFundInd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabFundInd.Name = "tabFundInd";
            this.tabFundInd.Size = new System.Drawing.Size(973, 208);
            this.tabFundInd.TabIndex = 0;
            this.tabFundInd.Text = "Passaporte Individual";
            this.tabFundInd.UseVisualStyleBackColor = true;
            // 
            // flpFunInd
            // 
            this.flpFunInd.AutoScroll = true;
            this.flpFunInd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFunInd.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flpFunInd.Location = new System.Drawing.Point(0, 0);
            this.flpFunInd.Name = "flpFunInd";
            this.flpFunInd.Size = new System.Drawing.Size(973, 208);
            this.flpFunInd.TabIndex = 1;
            // 
            // flpGradeFundamental
            // 
            this.flpGradeFundamental.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpGradeFundamental.BackColor = System.Drawing.Color.White;
            this.flpGradeFundamental.Location = new System.Drawing.Point(11, 11);
            this.flpGradeFundamental.Margin = new System.Windows.Forms.Padding(1, 5, 1, 5);
            this.flpGradeFundamental.Name = "flpGradeFundamental";
            this.flpGradeFundamental.Size = new System.Drawing.Size(981, 62);
            this.flpGradeFundamental.TabIndex = 5;
            this.flpGradeFundamental.WrapContents = false;
            // 
            // tabMedio
            // 
            this.tabMedio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(91)))), ((int)(((byte)(133)))));
            this.tabMedio.Controls.Add(this.tbcPassaporteMedio);
            this.tabMedio.Controls.Add(this.flpGradeMedio);
            this.tabMedio.Location = new System.Drawing.Point(4, 29);
            this.tabMedio.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabMedio.Name = "tabMedio";
            this.tabMedio.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabMedio.Size = new System.Drawing.Size(998, 346);
            this.tabMedio.TabIndex = 0;
            this.tabMedio.Text = "Médio";
            // 
            // tbcPassaporteMedio
            // 
            this.tbcPassaporteMedio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbcPassaporteMedio.Controls.Add(this.tabMedInd);
            this.tbcPassaporteMedio.Controls.Add(this.tabMedCom);
            this.tbcPassaporteMedio.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcPassaporteMedio.Location = new System.Drawing.Point(11, 78);
            this.tbcPassaporteMedio.Margin = new System.Windows.Forms.Padding(0);
            this.tbcPassaporteMedio.Name = "tbcPassaporteMedio";
            this.tbcPassaporteMedio.Padding = new System.Drawing.Point(0, 0);
            this.tbcPassaporteMedio.SelectedIndex = 0;
            this.tbcPassaporteMedio.Size = new System.Drawing.Size(979, 238);
            this.tbcPassaporteMedio.TabIndex = 7;
            this.tbcPassaporteMedio.SelectedIndexChanged += new System.EventHandler(this.tbcPassaporteMedio_SelectedIndexChanged);
            // 
            // tabMedInd
            // 
            this.tabMedInd.Controls.Add(this.flpMedInd);
            this.tabMedInd.Location = new System.Drawing.Point(4, 29);
            this.tabMedInd.Name = "tabMedInd";
            this.tabMedInd.Size = new System.Drawing.Size(971, 205);
            this.tabMedInd.TabIndex = 0;
            this.tabMedInd.Text = "Passaporte Individual";
            this.tabMedInd.UseVisualStyleBackColor = true;
            // 
            // flpMedInd
            // 
            this.flpMedInd.AutoScroll = true;
            this.flpMedInd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMedInd.Location = new System.Drawing.Point(0, 0);
            this.flpMedInd.Name = "flpMedInd";
            this.flpMedInd.Size = new System.Drawing.Size(971, 205);
            this.flpMedInd.TabIndex = 0;
            // 
            // tabMedCom
            // 
            this.tabMedCom.Controls.Add(this.flpMedCom);
            this.tabMedCom.Location = new System.Drawing.Point(4, 29);
            this.tabMedCom.Name = "tabMedCom";
            this.tabMedCom.Size = new System.Drawing.Size(971, 205);
            this.tabMedCom.TabIndex = 1;
            this.tabMedCom.Text = "Passaporte Completo";
            this.tabMedCom.UseVisualStyleBackColor = true;
            // 
            // flpMedCom
            // 
            this.flpMedCom.AutoScroll = true;
            this.flpMedCom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpMedCom.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flpMedCom.Location = new System.Drawing.Point(0, 0);
            this.flpMedCom.Name = "flpMedCom";
            this.flpMedCom.Size = new System.Drawing.Size(971, 205);
            this.flpMedCom.TabIndex = 1;
            // 
            // flpGradeMedio
            // 
            this.flpGradeMedio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpGradeMedio.BackColor = System.Drawing.Color.White;
            this.flpGradeMedio.Location = new System.Drawing.Point(11, 11);
            this.flpGradeMedio.Margin = new System.Windows.Forms.Padding(1, 5, 1, 5);
            this.flpGradeMedio.Name = "flpGradeMedio";
            this.flpGradeMedio.Size = new System.Drawing.Size(979, 62);
            this.flpGradeMedio.TabIndex = 6;
            this.flpGradeMedio.WrapContents = false;
            // 
            // pnlDadosAluno
            // 
            this.pnlDadosAluno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDadosAluno.BackColor = System.Drawing.Color.White;
            this.pnlDadosAluno.Controls.Add(this.lblNmat);
            this.pnlDadosAluno.Controls.Add(this.lblDatMat);
            this.pnlDadosAluno.Controls.Add(this.lblTermo);
            this.pnlDadosAluno.Controls.Add(this.lblRg);
            this.pnlDadosAluno.Controls.Add(this.lblDisciplinaAtual);
            this.pnlDadosAluno.Controls.Add(this.lblNome);
            this.pnlDadosAluno.Location = new System.Drawing.Point(15, 49);
            this.pnlDadosAluno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlDadosAluno.Name = "pnlDadosAluno";
            this.pnlDadosAluno.Size = new System.Drawing.Size(880, 94);
            this.pnlDadosAluno.TabIndex = 35;
            this.pnlDadosAluno.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDadosAluno_Paint);
            // 
            // lblNmat
            // 
            this.lblNmat.AutoSize = true;
            this.lblNmat.BackColor = System.Drawing.Color.White;
            this.lblNmat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNmat.Location = new System.Drawing.Point(0, 1);
            this.lblNmat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNmat.Name = "lblNmat";
            this.lblNmat.Size = new System.Drawing.Size(109, 19);
            this.lblNmat.TabIndex = 0;
            this.lblNmat.Text = "MATRÍCULA:";
            // 
            // lblDatMat
            // 
            this.lblDatMat.AutoSize = true;
            this.lblDatMat.BackColor = System.Drawing.Color.White;
            this.lblDatMat.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatMat.Location = new System.Drawing.Point(475, 37);
            this.lblDatMat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            this.lblTermo.Location = new System.Drawing.Point(475, 1);
            this.lblTermo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            this.lblRg.Location = new System.Drawing.Point(260, 1);
            this.lblRg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRg.Name = "lblRg";
            this.lblRg.Size = new System.Drawing.Size(39, 19);
            this.lblRg.TabIndex = 27;
            this.lblRg.Text = "RG:";
            // 
            // lblDisciplinaAtual
            // 
            this.lblDisciplinaAtual.AutoSize = true;
            this.lblDisciplinaAtual.BackColor = System.Drawing.Color.White;
            this.lblDisciplinaAtual.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisciplinaAtual.Location = new System.Drawing.Point(0, 73);
            this.lblDisciplinaAtual.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDisciplinaAtual.Name = "lblDisciplinaAtual";
            this.lblDisciplinaAtual.Size = new System.Drawing.Size(161, 19);
            this.lblDisciplinaAtual.TabIndex = 2;
            this.lblDisciplinaAtual.Text = "DISCIPLINA ATUAL:";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.BackColor = System.Drawing.Color.White;
            this.lblNome.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.Location = new System.Drawing.Point(0, 37);
            this.lblNome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(63, 19);
            this.lblNome.TabIndex = 1;
            this.lblNome.Text = "NOME:";
            // 
            // picFoto
            // 
            this.picFoto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picFoto.BackColor = System.Drawing.Color.White;
            this.picFoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picFoto.Location = new System.Drawing.Point(903, 48);
            this.picFoto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picFoto.Name = "picFoto";
            this.picFoto.Size = new System.Drawing.Size(94, 95);
            this.picFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFoto.TabIndex = 36;
            this.picFoto.TabStop = false;
            // 
            // pAtvExtra
            // 
            this.pAtvExtra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pAtvExtra.BackColor = System.Drawing.Color.White;
            this.pAtvExtra.Controls.Add(this.lblDataAtvExtra);
            this.pAtvExtra.Controls.Add(this.ckbAtvExtra);
            this.pAtvExtra.Controls.Add(this.label1);
            this.pAtvExtra.Location = new System.Drawing.Point(836, 182);
            this.pAtvExtra.Margin = new System.Windows.Forms.Padding(0);
            this.pAtvExtra.Name = "pAtvExtra";
            this.pAtvExtra.Size = new System.Drawing.Size(161, 20);
            this.pAtvExtra.TabIndex = 38;
            // 
            // lblDataAtvExtra
            // 
            this.lblDataAtvExtra.AutoSize = true;
            this.lblDataAtvExtra.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataAtvExtra.Location = new System.Drawing.Point(87, 0);
            this.lblDataAtvExtra.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDataAtvExtra.Name = "lblDataAtvExtra";
            this.lblDataAtvExtra.Size = new System.Drawing.Size(73, 20);
            this.lblDataAtvExtra.TabIndex = 2;
            this.lblDataAtvExtra.Text = "22/04/2017";
            // 
            // ckbAtvExtra
            // 
            this.ckbAtvExtra.AutoSize = true;
            this.ckbAtvExtra.Enabled = false;
            this.ckbAtvExtra.Location = new System.Drawing.Point(69, 4);
            this.ckbAtvExtra.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ckbAtvExtra.Name = "ckbAtvExtra";
            this.ckbAtvExtra.Size = new System.Drawing.Size(15, 14);
            this.ckbAtvExtra.TabIndex = 1;
            this.ckbAtvExtra.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Atv. Extra";
            // 
            // pnlObs
            // 
            this.pnlObs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlObs.BackColor = System.Drawing.Color.Transparent;
            this.pnlObs.Controls.Add(this.lblObs);
            this.pnlObs.Controls.Add(this.txtObs);
            this.pnlObs.Location = new System.Drawing.Point(15, 153);
            this.pnlObs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlObs.Name = "pnlObs";
            this.pnlObs.Size = new System.Drawing.Size(982, 19);
            this.pnlObs.TabIndex = 39;
            // 
            // lblObs
            // 
            this.lblObs.AutoSize = true;
            this.lblObs.BackColor = System.Drawing.Color.White;
            this.lblObs.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObs.Location = new System.Drawing.Point(0, 0);
            this.lblObs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObs.Name = "lblObs";
            this.lblObs.Size = new System.Drawing.Size(129, 19);
            this.lblObs.TabIndex = 31;
            this.lblObs.Text = "OBSERVAÇÃO:";
            // 
            // txtObs
            // 
            this.txtObs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtObs.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObs.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObs.ForeColor = System.Drawing.Color.Red;
            this.txtObs.Location = new System.Drawing.Point(129, 0);
            this.txtObs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtObs.Name = "txtObs";
            this.txtObs.Size = new System.Drawing.Size(853, 19);
            this.txtObs.TabIndex = 30;
            this.txtObs.Leave += new System.EventHandler(this.txtObs_Leave);
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.pnlForm.Controls.Add(this.pnlObs);
            this.pnlForm.Controls.Add(this.pAtvExtra);
            this.pnlForm.Controls.Add(this.btnPesquisa);
            this.pnlForm.Controls.Add(this.picFoto);
            this.pnlForm.Controls.Add(this.pnlDadosAluno);
            this.pnlForm.Controls.Add(this.tbcHistorico);
            this.pnlForm.Controls.Add(this.lblTitulo);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Location = new System.Drawing.Point(0, 0);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(1010, 561);
            this.pnlForm.TabIndex = 35;
            // 
            // frmPassaporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1010, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pnlForm);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1026, 600);
            this.Name = "frmPassaporte";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Passporte Aluno";
            this.Load += new System.EventHandler(this.FRpassaporte_Load);
            this.Shown += new System.EventHandler(this.frPassaporte_Shown);
            this.cmsOpcoes_dgv.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDescricoes)).EndInit();
            this.tabFundCom.ResumeLayout(false);
            this.tbcHistorico.ResumeLayout(false);
            this.tabFundamental.ResumeLayout(false);
            this.tbcPassaporteFundamental.ResumeLayout(false);
            this.tabFundInd.ResumeLayout(false);
            this.tabMedio.ResumeLayout(false);
            this.tbcPassaporteMedio.ResumeLayout(false);
            this.tabMedInd.ResumeLayout(false);
            this.tabMedCom.ResumeLayout(false);
            this.pnlDadosAluno.ResumeLayout(false);
            this.pnlDadosAluno.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            this.pAtvExtra.ResumeLayout(false);
            this.pAtvExtra.PerformLayout();
            this.pnlObs.ResumeLayout(false);
            this.pnlObs.PerformLayout();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmsOpcoes_dgv;
        private System.Windows.Forms.ToolStripMenuItem excluirAtendimentoToolStripMenuItem;
        private System.Windows.Forms.ToolTip ttAjuda;
        private System.Windows.Forms.ToolStripStatusLabel tssl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolTip ttTipo_atendimento;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.DataGridView dgvDescricoes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescricao2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDescricao1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescricao0;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tbcHistorico;
        private System.Windows.Forms.TabPage tabFundamental;
        private System.Windows.Forms.FlowLayoutPanel flpGradeFundamental;
        private System.Windows.Forms.TabPage tabMedio;
        private System.Windows.Forms.TabControl tbcPassaporteMedio;
        private System.Windows.Forms.FlowLayoutPanel flpGradeMedio;
        private System.Windows.Forms.Panel pnlDadosAluno;
        private System.Windows.Forms.Label lblNmat;
        private System.Windows.Forms.Label lblDatMat;
        private System.Windows.Forms.Label lblTermo;
        private System.Windows.Forms.Label lblRg;
        private System.Windows.Forms.Label lblDisciplinaAtual;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.PictureBox picFoto;
        private System.Windows.Forms.Button btnPesquisa;
        private System.Windows.Forms.Panel pAtvExtra;
        private System.Windows.Forms.Label lblDataAtvExtra;
        private System.Windows.Forms.CheckBox ckbAtvExtra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlObs;
        private System.Windows.Forms.Label lblObs;
        private System.Windows.Forms.TextBox txtObs;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.TabPage tabMedInd;
        private System.Windows.Forms.TabPage tabMedCom;
        private System.Windows.Forms.TabControl tbcPassaporteFundamental;
        private System.Windows.Forms.TabPage tabFundInd;
        private System.Windows.Forms.TabPage tabFundCom;
        private System.Windows.Forms.FlowLayoutPanel flpFunCom;
        private System.Windows.Forms.FlowLayoutPanel flpFunInd;
        private System.Windows.Forms.FlowLayoutPanel flpMedInd;
        private System.Windows.Forms.FlowLayoutPanel flpMedCom;
        private System.Windows.Forms.ToolStripMenuItem modificarAtendimentoToolStripMenuItem;
    }
}