namespace EmatWinFormsApp.UserControls
{
    partial class AlterarSenhaUserControl
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
            AlterarSenhaTableLayoutPanel = new TableLayoutPanel();
            label1 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            NovaSenhaTextBox = new TextBox();
            SenhaAtualTextBox = new TextBox();
            ConfirmacaoSenhaTextBox = new TextBox();
            NomeUsuarioLabel = new Label();
            ConfirmarButton = new Button();
            CancelarButton = new Button();
            AlterarSenhaTableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // AlterarSenhaTableLayoutPanel
            // 
            AlterarSenhaTableLayoutPanel.ColumnCount = 2;
            AlterarSenhaTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            AlterarSenhaTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            AlterarSenhaTableLayoutPanel.Controls.Add(label1, 0, 0);
            AlterarSenhaTableLayoutPanel.Controls.Add(label4, 0, 4);
            AlterarSenhaTableLayoutPanel.Controls.Add(label3, 0, 3);
            AlterarSenhaTableLayoutPanel.Controls.Add(label2, 0, 2);
            AlterarSenhaTableLayoutPanel.Controls.Add(NovaSenhaTextBox, 1, 3);
            AlterarSenhaTableLayoutPanel.Controls.Add(SenhaAtualTextBox, 1, 2);
            AlterarSenhaTableLayoutPanel.Controls.Add(ConfirmacaoSenhaTextBox, 1, 4);
            AlterarSenhaTableLayoutPanel.Controls.Add(NomeUsuarioLabel, 0, 1);
            AlterarSenhaTableLayoutPanel.Controls.Add(ConfirmarButton, 0, 5);
            AlterarSenhaTableLayoutPanel.Controls.Add(CancelarButton, 1, 5);
            AlterarSenhaTableLayoutPanel.Dock = DockStyle.Fill;
            AlterarSenhaTableLayoutPanel.Location = new Point(0, 0);
            AlterarSenhaTableLayoutPanel.Name = "AlterarSenhaTableLayoutPanel";
            AlterarSenhaTableLayoutPanel.RowCount = 6;
            AlterarSenhaTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            AlterarSenhaTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            AlterarSenhaTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            AlterarSenhaTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            AlterarSenhaTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            AlterarSenhaTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            AlterarSenhaTableLayoutPanel.Size = new Size(450, 300);
            AlterarSenhaTableLayoutPanel.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(107, 15);
            label1.TabIndex = 0;
            label1.Text = "Alteração de senha";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 200);
            label4.Name = "label4";
            label4.Size = new Size(100, 15);
            label4.TabIndex = 5;
            label4.Text = "Confirme a senha";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 150);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 3;
            label3.Text = "Nova senha";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 100);
            label2.Name = "label2";
            label2.Size = new Size(110, 15);
            label2.TabIndex = 1;
            label2.Text = "Digite a senha atual";
            // 
            // NovaSenhaTextBox
            // 
            NovaSenhaTextBox.Location = new Point(228, 153);
            NovaSenhaTextBox.Name = "NovaSenhaTextBox";
            NovaSenhaTextBox.Size = new Size(100, 23);
            NovaSenhaTextBox.TabIndex = 4;
            // 
            // SenhaAtualTextBox
            // 
            SenhaAtualTextBox.Location = new Point(228, 103);
            SenhaAtualTextBox.Name = "SenhaAtualTextBox";
            SenhaAtualTextBox.Size = new Size(100, 23);
            SenhaAtualTextBox.TabIndex = 2;
            // 
            // ConfirmacaoSenhaTextBox
            // 
            ConfirmacaoSenhaTextBox.Location = new Point(228, 203);
            ConfirmacaoSenhaTextBox.Name = "ConfirmacaoSenhaTextBox";
            ConfirmacaoSenhaTextBox.Size = new Size(100, 23);
            ConfirmacaoSenhaTextBox.TabIndex = 6;
            // 
            // NomeUsuarioLabel
            // 
            NomeUsuarioLabel.AutoSize = true;
            NomeUsuarioLabel.Location = new Point(3, 50);
            NomeUsuarioLabel.Name = "NomeUsuarioLabel";
            NomeUsuarioLabel.Size = new Size(47, 15);
            NomeUsuarioLabel.TabIndex = 7;
            NomeUsuarioLabel.Text = "Usuário";
            // 
            // ConfirmarButton
            // 
            ConfirmarButton.Location = new Point(3, 253);
            ConfirmarButton.Name = "ConfirmarButton";
            ConfirmarButton.Size = new Size(75, 23);
            ConfirmarButton.TabIndex = 8;
            ConfirmarButton.Text = "Confirmar";
            ConfirmarButton.UseVisualStyleBackColor = true;
            ConfirmarButton.Click += ConfirmarButton_Click;
            // 
            // CancelarButton
            // 
            CancelarButton.Location = new Point(228, 253);
            CancelarButton.Name = "CancelarButton";
            CancelarButton.Size = new Size(75, 23);
            CancelarButton.TabIndex = 9;
            CancelarButton.Text = "Cancelar";
            CancelarButton.UseVisualStyleBackColor = true;
            CancelarButton.Click += CancelarButton_Click;
            // 
            // AlterarSenhaUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(AlterarSenhaTableLayoutPanel);
            Name = "AlterarSenhaUserControl";
            Size = new Size(450, 300);
            AlterarSenhaTableLayoutPanel.ResumeLayout(false);
            AlterarSenhaTableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel AlterarSenhaTableLayoutPanel;
        private Label label1;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox NovaSenhaTextBox;
        private TextBox SenhaAtualTextBox;
        private TextBox ConfirmacaoSenhaTextBox;
        private Label NomeUsuarioLabel;
        private Button ConfirmarButton;
        private Button CancelarButton;
    }
}
