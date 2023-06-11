namespace EmatWinFormsNetFramework1402.Formularios
{
    partial class frmEmatricula
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmatricula));
            this.mspPrincipal = new System.Windows.Forms.MenuStrip();
            this.usuáriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAlterar_senha = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLogin_logout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSair = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSecretaria = new System.Windows.Forms.ToolStripMenuItem();
            this.matrícularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.novaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.históricoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPassporte_secr = new System.Windows.Forms.ToolStripMenuItem();
            this.inativarAlunoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.impressõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmProfessores = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPassporte_prof = new System.Windows.Forms.ToolStripMenuItem();
            this.atribuirAtividadeExtraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiposDeAtendimentosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atribuirMatériaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.detalhesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminaçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRelatorios = new System.Windows.Forms.ToolStripMenuItem();
            this.relatóriosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disciplinasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFerramentas = new System.Windows.Forms.ToolStripMenuItem();
            this.endereçosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpcoes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemConfUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAjuda = new System.Windows.Forms.ToolStripMenuItem();
            this.localDaAplicaçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listaDeAtualizaçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslUsuario_Grupo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblVersao_soft = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssErrors = new System.Windows.Forms.ToolStripStatusLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.mspPrincipal.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mspPrincipal
            // 
            this.mspPrincipal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mspPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuáriosToolStripMenuItem,
            this.tsmSecretaria,
            this.tsmProfessores,
            this.tsmRelatorios,
            this.tsmFerramentas,
            this.tsmAjuda});
            this.mspPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mspPrincipal.Name = "mspPrincipal";
            this.mspPrincipal.Size = new System.Drawing.Size(1008, 29);
            this.mspPrincipal.TabIndex = 1;
            this.mspPrincipal.Text = "menuStrip1";
            // 
            // usuáriosToolStripMenuItem
            // 
            this.usuáriosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAlterar_senha,
            this.tsmLogin_logout,
            this.tsmSair});
            this.usuáriosToolStripMenuItem.Name = "usuáriosToolStripMenuItem";
            this.usuáriosToolStripMenuItem.Size = new System.Drawing.Size(83, 25);
            this.usuáriosToolStripMenuItem.Text = "Usuários";
            // 
            // tsmAlterar_senha
            // 
            this.tsmAlterar_senha.Enabled = false;
            this.tsmAlterar_senha.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.PencilAngled_16xLG;
            this.tsmAlterar_senha.Name = "tsmAlterar_senha";
            this.tsmAlterar_senha.Size = new System.Drawing.Size(174, 26);
            this.tsmAlterar_senha.Text = "Alterar Senha";
            this.tsmAlterar_senha.Click += new System.EventHandler(this.tsmAlterar_senha_Click);
            // 
            // tsmLogin_logout
            // 
            this.tsmLogin_logout.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.Team_16xLG;
            this.tsmLogin_logout.Name = "tsmLogin_logout";
            this.tsmLogin_logout.Size = new System.Drawing.Size(174, 26);
            this.tsmLogin_logout.Text = "Entrar/Sair";
            this.tsmLogin_logout.Click += new System.EventHandler(this.loginLogoutToolStripMenuItem1_Click);
            // 
            // tsmSair
            // 
            this.tsmSair.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.action_Cancel_16xLG;
            this.tsmSair.Name = "tsmSair";
            this.tsmSair.Size = new System.Drawing.Size(174, 26);
            this.tsmSair.Text = "Sair";
            this.tsmSair.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // tsmSecretaria
            // 
            this.tsmSecretaria.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.matrícularToolStripMenuItem,
            this.históricoToolStripMenuItem,
            this.tsmiPassporte_secr,
            this.inativarAlunoToolStripMenuItem,
            this.impressõesToolStripMenuItem});
            this.tsmSecretaria.Enabled = false;
            this.tsmSecretaria.Name = "tsmSecretaria";
            this.tsmSecretaria.Size = new System.Drawing.Size(91, 25);
            this.tsmSecretaria.Text = "Secretaria";
            // 
            // matrícularToolStripMenuItem
            // 
            this.matrícularToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.novaToolStripMenuItem,
            this.editarToolStripMenuItem});
            this.matrícularToolStripMenuItem.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.PencilAngled_16xLG;
            this.matrícularToolStripMenuItem.Name = "matrícularToolStripMenuItem";
            this.matrícularToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.matrícularToolStripMenuItem.Text = "Matrícula";
            // 
            // novaToolStripMenuItem
            // 
            this.novaToolStripMenuItem.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.action_add_16xLG;
            this.novaToolStripMenuItem.Name = "novaToolStripMenuItem";
            this.novaToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.novaToolStripMenuItem.Text = "Nova Matrícula";
            this.novaToolStripMenuItem.Click += new System.EventHandler(this.novaToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.magnifier_16xLG;
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.editarToolStripMenuItem.Text = "Pesquisar/Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // históricoToolStripMenuItem
            // 
            this.históricoToolStripMenuItem.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.certificate_16xLG;
            this.históricoToolStripMenuItem.Name = "históricoToolStripMenuItem";
            this.históricoToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.históricoToolStripMenuItem.Text = "Histórico Escolar";
            this.históricoToolStripMenuItem.Click += new System.EventHandler(this.históricoToolStripMenuItem_Click);
            // 
            // tsmiPassporte_secr
            // 
            this.tsmiPassporte_secr.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.Table_748;
            this.tsmiPassporte_secr.Name = "tsmiPassporte_secr";
            this.tsmiPassporte_secr.Size = new System.Drawing.Size(222, 26);
            this.tsmiPassporte_secr.Text = "Passaporte do Aluno";
            this.tsmiPassporte_secr.Click += new System.EventHandler(this.tsmiPassporte_secr_Click);
            // 
            // inativarAlunoToolStripMenuItem
            // 
            this.inativarAlunoToolStripMenuItem.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.lock_16xLG;
            this.inativarAlunoToolStripMenuItem.Name = "inativarAlunoToolStripMenuItem";
            this.inativarAlunoToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.inativarAlunoToolStripMenuItem.Text = "Inativar Alunos";
            this.inativarAlunoToolStripMenuItem.Click += new System.EventHandler(this.inativarAlunosToolStripMenuItem_Click);
            // 
            // impressõesToolStripMenuItem
            // 
            this.impressõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rAToolStripMenuItem});
            this.impressõesToolStripMenuItem.Name = "impressõesToolStripMenuItem";
            this.impressõesToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.impressõesToolStripMenuItem.Text = "Impressões";
            // 
            // rAToolStripMenuItem
            // 
            this.rAToolStripMenuItem.Name = "rAToolStripMenuItem";
            this.rAToolStripMenuItem.Size = new System.Drawing.Size(100, 26);
            this.rAToolStripMenuItem.Text = "RA";
            this.rAToolStripMenuItem.Click += new System.EventHandler(this.rAToolStripMenuItem_Click);
            // 
            // tsmProfessores
            // 
            this.tsmProfessores.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPassporte_prof,
            this.atribuirAtividadeExtraToolStripMenuItem,
            this.tiposDeAtendimentosToolStripMenuItem,
            this.atribuirMatériaToolStripMenuItem1,
            this.detalhesToolStripMenuItem,
            this.eliminaçõesToolStripMenuItem});
            this.tsmProfessores.Enabled = false;
            this.tsmProfessores.Name = "tsmProfessores";
            this.tsmProfessores.Size = new System.Drawing.Size(103, 25);
            this.tsmProfessores.Text = "Professores";
            this.tsmProfessores.Click += new System.EventHandler(this.tsmProfessores_Click);
            // 
            // tsmiPassporte_prof
            // 
            this.tsmiPassporte_prof.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.Table_748;
            this.tsmiPassporte_prof.Name = "tsmiPassporte_prof";
            this.tsmiPassporte_prof.Size = new System.Drawing.Size(245, 26);
            this.tsmiPassporte_prof.Text = "Passaporte Aluno";
            this.tsmiPassporte_prof.Click += new System.EventHandler(this.tsmiPassporte_prof_Click);
            // 
            // atribuirAtividadeExtraToolStripMenuItem
            // 
            this.atribuirAtividadeExtraToolStripMenuItem.Name = "atribuirAtividadeExtraToolStripMenuItem";
            this.atribuirAtividadeExtraToolStripMenuItem.Size = new System.Drawing.Size(245, 26);
            this.atribuirAtividadeExtraToolStripMenuItem.Text = "Atribuir Atividade Extra";
            this.atribuirAtividadeExtraToolStripMenuItem.Click += new System.EventHandler(this.atribuirAtividadeExtraToolStripMenuItem_Click);
            // 
            // tiposDeAtendimentosToolStripMenuItem
            // 
            this.tiposDeAtendimentosToolStripMenuItem.Enabled = false;
            this.tiposDeAtendimentosToolStripMenuItem.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.properties_16xLG;
            this.tiposDeAtendimentosToolStripMenuItem.Name = "tiposDeAtendimentosToolStripMenuItem";
            this.tiposDeAtendimentosToolStripMenuItem.Size = new System.Drawing.Size(245, 26);
            this.tiposDeAtendimentosToolStripMenuItem.Text = "Tipos de Atendimentos";
            this.tiposDeAtendimentosToolStripMenuItem.Click += new System.EventHandler(this.tiposDeAtendimentosToolStripMenuItem_Click);
            // 
            // atribuirMatériaToolStripMenuItem1
            // 
            this.atribuirMatériaToolStripMenuItem1.Enabled = false;
            this.atribuirMatériaToolStripMenuItem1.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.AddInheritedControl_372;
            this.atribuirMatériaToolStripMenuItem1.Name = "atribuirMatériaToolStripMenuItem1";
            this.atribuirMatériaToolStripMenuItem1.Size = new System.Drawing.Size(245, 26);
            this.atribuirMatériaToolStripMenuItem1.Text = "Atribuir Nova Disciplina";
            this.atribuirMatériaToolStripMenuItem1.Click += new System.EventHandler(this.atribuirMatériaToolStripMenuItem_Click);
            // 
            // detalhesToolStripMenuItem
            // 
            this.detalhesToolStripMenuItem.Name = "detalhesToolStripMenuItem";
            this.detalhesToolStripMenuItem.Size = new System.Drawing.Size(245, 26);
            this.detalhesToolStripMenuItem.Text = "Detalhes";
            this.detalhesToolStripMenuItem.Click += new System.EventHandler(this.detalhesToolStripMenuItem_Click);
            // 
            // eliminaçõesToolStripMenuItem
            // 
            this.eliminaçõesToolStripMenuItem.Name = "eliminaçõesToolStripMenuItem";
            this.eliminaçõesToolStripMenuItem.Size = new System.Drawing.Size(245, 26);
            this.eliminaçõesToolStripMenuItem.Text = "Eliminações";
            this.eliminaçõesToolStripMenuItem.Click += new System.EventHandler(this.eliminaçõesToolStripMenuItem_Click);
            // 
            // tsmRelatorios
            // 
            this.tsmRelatorios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.relatóriosToolStripMenuItem,
            this.disciplinasToolStripMenuItem});
            this.tsmRelatorios.Enabled = false;
            this.tsmRelatorios.Name = "tsmRelatorios";
            this.tsmRelatorios.Size = new System.Drawing.Size(92, 25);
            this.tsmRelatorios.Text = "Relatórios";
            // 
            // relatóriosToolStripMenuItem
            // 
            this.relatóriosToolStripMenuItem.Name = "relatóriosToolStripMenuItem";
            this.relatóriosToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.relatóriosToolStripMenuItem.Text = "Secretaria";
            this.relatóriosToolStripMenuItem.Click += new System.EventHandler(this.relatóriosToolStripMenuItem_Click_1);
            // 
            // disciplinasToolStripMenuItem
            // 
            this.disciplinasToolStripMenuItem.Name = "disciplinasToolStripMenuItem";
            this.disciplinasToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.disciplinasToolStripMenuItem.Text = "Disciplinas";
            this.disciplinasToolStripMenuItem.Click += new System.EventHandler(this.disciplinasToolStripMenuItem_Click);
            // 
            // tsmFerramentas
            // 
            this.tsmFerramentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.endereçosToolStripMenuItem,
            this.tsmOpcoes,
            this.tsmItemConfUsuarios});
            this.tsmFerramentas.Enabled = false;
            this.tsmFerramentas.Name = "tsmFerramentas";
            this.tsmFerramentas.Size = new System.Drawing.Size(109, 25);
            this.tsmFerramentas.Text = "Ferramentas";
            // 
            // endereçosToolStripMenuItem
            // 
            this.endereçosToolStripMenuItem.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.globe_16xLG;
            this.endereçosToolStripMenuItem.Name = "endereçosToolStripMenuItem";
            this.endereçosToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.endereçosToolStripMenuItem.Text = "Novo Endereço";
            this.endereçosToolStripMenuItem.Click += new System.EventHandler(this.endereçosToolStripMenuItem_Click_1);
            // 
            // tsmOpcoes
            // 
            this.tsmOpcoes.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.properties_16xLG;
            this.tsmOpcoes.Name = "tsmOpcoes";
            this.tsmOpcoes.Size = new System.Drawing.Size(186, 26);
            this.tsmOpcoes.Text = "Configurações";
            this.tsmOpcoes.Click += new System.EventHandler(this.tsmConfiguracoes_Click);
            // 
            // tsmItemConfUsuarios
            // 
            this.tsmItemConfUsuarios.Enabled = false;
            this.tsmItemConfUsuarios.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.Team_16xLG;
            this.tsmItemConfUsuarios.Name = "tsmItemConfUsuarios";
            this.tsmItemConfUsuarios.Size = new System.Drawing.Size(186, 26);
            this.tsmItemConfUsuarios.Text = "Usuários";
            this.tsmItemConfUsuarios.Click += new System.EventHandler(this.usuáriosToolStripMenuItem1_Click);
            // 
            // tsmAjuda
            // 
            this.tsmAjuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localDaAplicaçãoToolStripMenuItem,
            this.listaDeAtualizaçõesToolStripMenuItem,
            this.sobreToolStripMenuItem});
            this.tsmAjuda.Name = "tsmAjuda";
            this.tsmAjuda.Size = new System.Drawing.Size(62, 25);
            this.tsmAjuda.Text = "Ajuda";
            this.tsmAjuda.Click += new System.EventHandler(this.tsmAjuda_Click);
            // 
            // localDaAplicaçãoToolStripMenuItem
            // 
            this.localDaAplicaçãoToolStripMenuItem.Name = "localDaAplicaçãoToolStripMenuItem";
            this.localDaAplicaçãoToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.localDaAplicaçãoToolStripMenuItem.Text = "Local da Aplicação";
            this.localDaAplicaçãoToolStripMenuItem.Click += new System.EventHandler(this.localDaAplicaçãoToolStripMenuItem_Click);
            // 
            // listaDeAtualizaçõesToolStripMenuItem
            // 
            this.listaDeAtualizaçõesToolStripMenuItem.Name = "listaDeAtualizaçõesToolStripMenuItem";
            this.listaDeAtualizaçõesToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.listaDeAtualizaçõesToolStripMenuItem.Text = "Lista de Atualizações";
            this.listaDeAtualizaçõesToolStripMenuItem.Click += new System.EventHandler(this.listaDeAtualizaçõesToolStripMenuItem_Click);
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.Image = global::EmatWinFormsNetFramework1402.Properties.Resources.Symbols_Help_and_inclusive_16xLG;
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.sobreToolStripMenuItem.Text = "Sobre";
            this.sobreToolStripMenuItem.Click += new System.EventHandler(this.sobreToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslUsuario_Grupo,
            this.lblVersao_soft,
            this.tssErrors});
            this.statusStrip1.Location = new System.Drawing.Point(0, 660);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslUsuario_Grupo
            // 
            this.tsslUsuario_Grupo.Name = "tsslUsuario_Grupo";
            this.tsslUsuario_Grupo.Size = new System.Drawing.Size(72, 17);
            this.tsslUsuario_Grupo.Text = "Não Logado";
            // 
            // lblVersao_soft
            // 
            this.lblVersao_soft.Name = "lblVersao_soft";
            this.lblVersao_soft.Size = new System.Drawing.Size(0, 17);
            this.lblVersao_soft.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tssErrors
            // 
            this.tssErrors.Name = "tssErrors";
            this.tssErrors.Size = new System.Drawing.Size(98, 17);
            this.tssErrors.Text = "errors_admin_log";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Enabled = false;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(898, 27);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(110, 630);
            this.flowLayoutPanel1.TabIndex = 5;
            this.flowLayoutPanel1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Recentes";
            // 
            // frmEmatricula
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 682);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mspPrincipal);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mspPrincipal;
            this.Name = "frmEmatricula";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "E-Matrícula - 1.3";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.Shown += new System.EventHandler(this.frmPrincipal_Shown);
            this.mspPrincipal.ResumeLayout(false);
            this.mspPrincipal.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mspPrincipal;
        private System.Windows.Forms.ToolStripMenuItem usuáriosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmAlterar_senha;
        private System.Windows.Forms.ToolStripMenuItem tsmSecretaria;
        private System.Windows.Forms.ToolStripMenuItem matrícularToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem novaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmProfessores;
        private System.Windows.Forms.ToolStripMenuItem tsmFerramentas;
        private System.Windows.Forms.ToolStripMenuItem tsmOpcoes;
        private System.Windows.Forms.ToolStripMenuItem tsmAjuda;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmLogin_logout;
        private System.Windows.Forms.ToolStripMenuItem tsmSair;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslUsuario_Grupo;
        public System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem históricoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localDaAplicaçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblVersao_soft;
        private System.Windows.Forms.ToolStripMenuItem atribuirMatériaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem inativarAlunoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiPassporte_secr;
        private System.Windows.Forms.ToolStripMenuItem tsmRelatorios;
        private System.Windows.Forms.ToolStripStatusLabel tssErrors;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem relatóriosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem endereçosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiPassporte_prof;
        private System.Windows.Forms.ToolStripMenuItem tiposDeAtendimentosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disciplinasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listaDeAtualizaçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detalhesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atribuirAtividadeExtraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminaçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem impressõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmItemConfUsuarios;
    }
}