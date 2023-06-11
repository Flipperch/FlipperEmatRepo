namespace EmatWinFormsNetFramework13032.Endereços
{
    partial class Frnovo
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
            this.txt_novo_end = new System.Windows.Forms.TextBox();
            this.bt_aceitar = new System.Windows.Forms.Button();
            this.bt_cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_novo_end
            // 
            this.txt_novo_end.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_novo_end.Location = new System.Drawing.Point(3, 12);
            this.txt_novo_end.Name = "txt_novo_end";
            this.txt_novo_end.Size = new System.Drawing.Size(258, 20);
            this.txt_novo_end.TabIndex = 0;
            // 
            // bt_aceitar
            // 
            this.bt_aceitar.Location = new System.Drawing.Point(267, 12);
            this.bt_aceitar.Name = "bt_aceitar";
            this.bt_aceitar.Size = new System.Drawing.Size(75, 23);
            this.bt_aceitar.TabIndex = 1;
            this.bt_aceitar.Text = "Aceitar";
            this.bt_aceitar.UseVisualStyleBackColor = true;
            this.bt_aceitar.Click += new System.EventHandler(this.bt_aceitar_Click);
            // 
            // bt_cancelar
            // 
            this.bt_cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_cancelar.Location = new System.Drawing.Point(348, 12);
            this.bt_cancelar.Name = "bt_cancelar";
            this.bt_cancelar.Size = new System.Drawing.Size(75, 23);
            this.bt_cancelar.TabIndex = 2;
            this.bt_cancelar.Text = "Cancelar";
            this.bt_cancelar.UseVisualStyleBackColor = true;
            // 
            // Frnovo
            // 
            this.AcceptButton = this.bt_aceitar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bt_cancelar;
            this.ClientSize = new System.Drawing.Size(435, 43);
            this.Controls.Add(this.bt_cancelar);
            this.Controls.Add(this.bt_aceitar);
            this.Controls.Add(this.txt_novo_end);
            this.Name = "Frnovo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Novo Endereço";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_novo_end;
        private System.Windows.Forms.Button bt_aceitar;
        private System.Windows.Forms.Button bt_cancelar;
    }
}