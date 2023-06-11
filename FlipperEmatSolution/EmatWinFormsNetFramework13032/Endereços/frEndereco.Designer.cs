namespace EmatWinFormsNetFramework13032.Endereços
{
    partial class frEndereco
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
            this.btsalvar = new System.Windows.Forms.Button();
            this.txtcidade = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_novoestado = new System.Windows.Forms.Button();
            this.bt_novopais = new System.Windows.Forms.Button();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.cmbPais = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btsalvar
            // 
            this.btsalvar.Location = new System.Drawing.Point(205, 138);
            this.btsalvar.Name = "btsalvar";
            this.btsalvar.Size = new System.Drawing.Size(57, 23);
            this.btsalvar.TabIndex = 4;
            this.btsalvar.Text = "Incluir";
            this.btsalvar.UseVisualStyleBackColor = true;
            this.btsalvar.Click += new System.EventHandler(this.btsalvar_Click);
            // 
            // txtcidade
            // 
            this.txtcidade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcidade.Enabled = false;
            this.txtcidade.Location = new System.Drawing.Point(6, 112);
            this.txtcidade.Name = "txtcidade";
            this.txtcidade.Size = new System.Drawing.Size(256, 20);
            this.txtcidade.TabIndex = 3;
            this.txtcidade.TextChanged += new System.EventHandler(this.txtcidade_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Pais";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Estado/Região";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Cidade";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_novoestado);
            this.groupBox1.Controls.Add(this.bt_novopais);
            this.groupBox1.Controls.Add(this.cmbEstado);
            this.groupBox1.Controls.Add(this.cmbPais);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btsalvar);
            this.groupBox1.Controls.Add(this.txtcidade);
            this.groupBox1.Location = new System.Drawing.Point(3, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 168);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inclusão";
            // 
            // bt_novoestado
            // 
            this.bt_novoestado.BackgroundImage = global::EmatWinFormsNetFramework13032.Properties.Resources.action_add_16xLG;
            this.bt_novoestado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bt_novoestado.Enabled = false;
            this.bt_novoestado.Location = new System.Drawing.Point(231, 72);
            this.bt_novoestado.Name = "bt_novoestado";
            this.bt_novoestado.Size = new System.Drawing.Size(31, 21);
            this.bt_novoestado.TabIndex = 14;
            this.bt_novoestado.UseVisualStyleBackColor = true;
            this.bt_novoestado.Click += new System.EventHandler(this.bt_novoestado_Click);
            // 
            // bt_novopais
            // 
            this.bt_novopais.BackgroundImage = global::EmatWinFormsNetFramework13032.Properties.Resources.action_add_16xLG;
            this.bt_novopais.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bt_novopais.Location = new System.Drawing.Point(231, 32);
            this.bt_novopais.Name = "bt_novopais";
            this.bt_novopais.Size = new System.Drawing.Size(31, 21);
            this.bt_novopais.TabIndex = 12;
            this.bt_novopais.UseVisualStyleBackColor = true;
            this.bt_novopais.Click += new System.EventHandler(this.bt_novopais_Click);
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.Enabled = false;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(6, 72);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(222, 21);
            this.cmbEstado.TabIndex = 13;
            this.cmbEstado.SelectedIndexChanged += new System.EventHandler(this.cmbEstado_SelectedIndexChanged);
            // 
            // cmbPais
            // 
            this.cmbPais.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPais.FormattingEnabled = true;
            this.cmbPais.Location = new System.Drawing.Point(6, 32);
            this.cmbPais.Name = "cmbPais";
            this.cmbPais.Size = new System.Drawing.Size(222, 21);
            this.cmbPais.TabIndex = 12;
            this.cmbPais.SelectedIndexChanged += new System.EventHandler(this.cmbPais_SelectedIndexChanged);
            // 
            // FRenderecos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 189);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FRenderecos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Endereços";
            this.Load += new System.EventHandler(this.FRenderecos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btsalvar;
        private System.Windows.Forms.TextBox txtcidade;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.ComboBox cmbPais;
        private System.Windows.Forms.Button bt_novoestado;
        private System.Windows.Forms.Button bt_novopais;
    }
}