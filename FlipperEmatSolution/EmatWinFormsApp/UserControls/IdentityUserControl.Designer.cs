namespace EmatWinFormsApp.UserControls
{
    partial class IdentityUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            IdentityTableLayoutPanel = new TableLayoutPanel();
            TipoUsuarioLabel = new Label();
            NomeUsuarioLabel = new Label();
            AcoesUsuarioFlowLayoutPanel = new FlowLayoutPanel();
            PerfilButton = new Button();
            AlterarSenhaButton = new Button();
            SairButton = new Button();
            IdentityTableLayoutPanel.SuspendLayout();
            AcoesUsuarioFlowLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // IdentityTableLayoutPanel
            // 
            IdentityTableLayoutPanel.ColumnCount = 3;
            IdentityTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            IdentityTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            IdentityTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            IdentityTableLayoutPanel.Controls.Add(TipoUsuarioLabel, 0, 1);
            IdentityTableLayoutPanel.Controls.Add(NomeUsuarioLabel, 0, 0);
            IdentityTableLayoutPanel.Controls.Add(AcoesUsuarioFlowLayoutPanel, 0, 2);
            IdentityTableLayoutPanel.Dock = DockStyle.Fill;
            IdentityTableLayoutPanel.Location = new Point(0, 0);
            IdentityTableLayoutPanel.Name = "IdentityTableLayoutPanel";
            IdentityTableLayoutPanel.RowCount = 3;
            IdentityTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            IdentityTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            IdentityTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            IdentityTableLayoutPanel.Size = new Size(300, 150);
            IdentityTableLayoutPanel.TabIndex = 6;
            // 
            // TipoUsuarioLabel
            // 
            TipoUsuarioLabel.AutoSize = true;
            IdentityTableLayoutPanel.SetColumnSpan(TipoUsuarioLabel, 3);
            TipoUsuarioLabel.ImeMode = ImeMode.NoControl;
            TipoUsuarioLabel.Location = new Point(3, 50);
            TipoUsuarioLabel.Name = "TipoUsuarioLabel";
            TipoUsuarioLabel.Size = new Size(83, 15);
            TipoUsuarioLabel.TabIndex = 1;
            TipoUsuarioLabel.Text = "Administrador";
            // 
            // NomeUsuarioLabel
            // 
            NomeUsuarioLabel.AutoSize = true;
            IdentityTableLayoutPanel.SetColumnSpan(NomeUsuarioLabel, 3);
            NomeUsuarioLabel.ImeMode = ImeMode.NoControl;
            NomeUsuarioLabel.Location = new Point(3, 0);
            NomeUsuarioLabel.Name = "NomeUsuarioLabel";
            NomeUsuarioLabel.Size = new Size(123, 15);
            NomeUsuarioLabel.TabIndex = 0;
            NomeUsuarioLabel.Text = "Felipe Alencar Chagas";
            // 
            // AcoesUsuarioFlowLayoutPanel
            // 
            IdentityTableLayoutPanel.SetColumnSpan(AcoesUsuarioFlowLayoutPanel, 3);
            AcoesUsuarioFlowLayoutPanel.Controls.Add(PerfilButton);
            AcoesUsuarioFlowLayoutPanel.Controls.Add(AlterarSenhaButton);
            AcoesUsuarioFlowLayoutPanel.Controls.Add(SairButton);
            AcoesUsuarioFlowLayoutPanel.Dock = DockStyle.Fill;
            AcoesUsuarioFlowLayoutPanel.Location = new Point(3, 103);
            AcoesUsuarioFlowLayoutPanel.Name = "AcoesUsuarioFlowLayoutPanel";
            AcoesUsuarioFlowLayoutPanel.Size = new Size(294, 44);
            AcoesUsuarioFlowLayoutPanel.TabIndex = 5;
            // 
            // PerfilButton
            // 
            PerfilButton.AutoSize = true;
            PerfilButton.ImeMode = ImeMode.NoControl;
            PerfilButton.Location = new Point(3, 3);
            PerfilButton.Name = "PerfilButton";
            PerfilButton.Size = new Size(90, 25);
            PerfilButton.TabIndex = 3;
            PerfilButton.Text = "Perfil";
            PerfilButton.UseVisualStyleBackColor = true;
            PerfilButton.Click += PerfilButton_Click;
            // 
            // AlterarSenhaButton
            // 
            AlterarSenhaButton.AutoSize = true;
            AlterarSenhaButton.ImeMode = ImeMode.NoControl;
            AlterarSenhaButton.Location = new Point(99, 3);
            AlterarSenhaButton.Name = "AlterarSenhaButton";
            AlterarSenhaButton.Size = new Size(90, 25);
            AlterarSenhaButton.TabIndex = 3;
            AlterarSenhaButton.Text = "Alterar Senha";
            AlterarSenhaButton.UseVisualStyleBackColor = true;
            AlterarSenhaButton.Click += AlterarSenhaButton_Click;
            // 
            // SairButton
            // 
            SairButton.AutoSize = true;
            SairButton.ImeMode = ImeMode.NoControl;
            SairButton.Location = new Point(195, 3);
            SairButton.Name = "SairButton";
            SairButton.Size = new Size(90, 25);
            SairButton.TabIndex = 4;
            SairButton.Text = "Sair";
            SairButton.UseVisualStyleBackColor = true;
            SairButton.Click += SairButton_Click;
            // 
            // IdentityUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(IdentityTableLayoutPanel);
            Name = "IdentityUserControl";
            Size = new Size(300, 150);
            IdentityTableLayoutPanel.ResumeLayout(false);
            IdentityTableLayoutPanel.PerformLayout();
            AcoesUsuarioFlowLayoutPanel.ResumeLayout(false);
            AcoesUsuarioFlowLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel IdentityTableLayoutPanel;
        private Button SairButton;
        private Button AlterarSenhaButton;
        private Button button1;
        private Label TipoUsuarioLabel;
        private Label NomeUsuarioLabel;
        private FlowLayoutPanel AcoesUsuarioFlowLayoutPanel;
        private Button PerfilButton;
    }
}
