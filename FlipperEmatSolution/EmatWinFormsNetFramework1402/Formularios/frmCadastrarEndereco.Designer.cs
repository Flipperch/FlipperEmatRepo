namespace EmatWinFormsNetFramework1402.Formularios
{
    partial class frmCadastrarEndereco
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
            this.btnSalvarPais = new System.Windows.Forms.Button();
            this.dgvPais = new System.Windows.Forms.DataGridView();
            this.ColCodigoPais = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNomePais = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtNomePais = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnAddPais = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmbPais = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSiglaUf = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddUf = new System.Windows.Forms.Button();
            this.txtNomeUf = new System.Windows.Forms.TextBox();
            this.dgvUf = new System.Windows.Forms.DataGridView();
            this.ColCodigoUf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNomeUf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColSiglaUf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSalvarUf = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cmbPaisCidade = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbUf = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAddCidade = new System.Windows.Forms.Button();
            this.txtNomeCidade = new System.Windows.Forms.TextBox();
            this.dgvCidade = new System.Windows.Forms.DataGridView();
            this.ColCodigoCidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNomeCidade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSalvarCidade = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPais)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUf)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCidade)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalvarPais
            // 
            this.btnSalvarPais.Location = new System.Drawing.Point(373, 234);
            this.btnSalvarPais.Name = "btnSalvarPais";
            this.btnSalvarPais.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarPais.TabIndex = 4;
            this.btnSalvarPais.Text = "Salvar";
            this.btnSalvarPais.UseVisualStyleBackColor = true;
            this.btnSalvarPais.Click += new System.EventHandler(this.btnSalvarPais_Click);
            // 
            // dgvPais
            // 
            this.dgvPais.AllowUserToAddRows = false;
            this.dgvPais.AllowUserToDeleteRows = false;
            this.dgvPais.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPais.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPais.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCodigoPais,
            this.colNomePais});
            this.dgvPais.Location = new System.Drawing.Point(8, 62);
            this.dgvPais.Name = "dgvPais";
            this.dgvPais.RowHeadersVisible = false;
            this.dgvPais.Size = new System.Drawing.Size(440, 166);
            this.dgvPais.TabIndex = 3;
            this.dgvPais.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPais_CellContentDoubleClick);
            // 
            // ColCodigoPais
            // 
            this.ColCodigoPais.HeaderText = "Código País";
            this.ColCodigoPais.Name = "ColCodigoPais";
            this.ColCodigoPais.ReadOnly = true;
            this.ColCodigoPais.Visible = false;
            // 
            // colNomePais
            // 
            this.colNomePais.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNomePais.HeaderText = "Nome País";
            this.colNomePais.Name = "colNomePais";
            this.colNomePais.ReadOnly = true;
            // 
            // txtNomePais
            // 
            this.txtNomePais.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomePais.Location = new System.Drawing.Point(8, 36);
            this.txtNomePais.Name = "txtNomePais";
            this.txtNomePais.Size = new System.Drawing.Size(414, 20);
            this.txtNomePais.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome País";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(464, 291);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnAddPais);
            this.tabPage1.Controls.Add(this.txtNomePais);
            this.tabPage1.Controls.Add(this.dgvPais);
            this.tabPage1.Controls.Add(this.btnSalvarPais);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(456, 265);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "País";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnAddPais
            // 
            this.btnAddPais.BackgroundImage = global::EmatWinFormsNetFramework1402.Properties.Resources.add;
            this.btnAddPais.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddPais.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAddPais.Location = new System.Drawing.Point(428, 36);
            this.btnAddPais.Name = "btnAddPais";
            this.btnAddPais.Size = new System.Drawing.Size(20, 20);
            this.btnAddPais.TabIndex = 2;
            this.btnAddPais.UseVisualStyleBackColor = true;
            this.btnAddPais.Click += new System.EventHandler(this.btnAddPais_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cmbPais);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtSiglaUf);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.btnAddUf);
            this.tabPage2.Controls.Add(this.txtNomeUf);
            this.tabPage2.Controls.Add(this.dgvUf);
            this.tabPage2.Controls.Add(this.btnSalvarUf);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(456, 265);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "UF";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cmbPais
            // 
            this.cmbPais.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPais.FormattingEnabled = true;
            this.cmbPais.Location = new System.Drawing.Point(8, 35);
            this.cmbPais.Name = "cmbPais";
            this.cmbPais.Size = new System.Drawing.Size(159, 21);
            this.cmbPais.TabIndex = 1;
            this.cmbPais.SelectedIndexChanged += new System.EventHandler(this.cmbPais_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "País";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(346, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sigla UF";
            // 
            // txtSiglaUf
            // 
            this.txtSiglaUf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSiglaUf.Location = new System.Drawing.Point(349, 36);
            this.txtSiglaUf.Name = "txtSiglaUf";
            this.txtSiglaUf.Size = new System.Drawing.Size(73, 20);
            this.txtSiglaUf.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nome UF";
            // 
            // btnAddUf
            // 
            this.btnAddUf.BackgroundImage = global::EmatWinFormsNetFramework1402.Properties.Resources.add;
            this.btnAddUf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddUf.Location = new System.Drawing.Point(428, 36);
            this.btnAddUf.Name = "btnAddUf";
            this.btnAddUf.Size = new System.Drawing.Size(20, 20);
            this.btnAddUf.TabIndex = 6;
            this.btnAddUf.UseVisualStyleBackColor = true;
            this.btnAddUf.Click += new System.EventHandler(this.btnAddUf_Click);
            // 
            // txtNomeUf
            // 
            this.txtNomeUf.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeUf.Location = new System.Drawing.Point(173, 36);
            this.txtNomeUf.Name = "txtNomeUf";
            this.txtNomeUf.Size = new System.Drawing.Size(170, 20);
            this.txtNomeUf.TabIndex = 3;
            // 
            // dgvUf
            // 
            this.dgvUf.AllowUserToAddRows = false;
            this.dgvUf.AllowUserToDeleteRows = false;
            this.dgvUf.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvUf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUf.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCodigoUf,
            this.ColNomeUf,
            this.ColSiglaUf});
            this.dgvUf.Location = new System.Drawing.Point(8, 62);
            this.dgvUf.Name = "dgvUf";
            this.dgvUf.RowHeadersVisible = false;
            this.dgvUf.Size = new System.Drawing.Size(440, 166);
            this.dgvUf.TabIndex = 7;
            this.dgvUf.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUf_CellContentDoubleClick);
            // 
            // ColCodigoUf
            // 
            this.ColCodigoUf.HeaderText = "Código Uf";
            this.ColCodigoUf.Name = "ColCodigoUf";
            this.ColCodigoUf.ReadOnly = true;
            this.ColCodigoUf.Visible = false;
            // 
            // ColNomeUf
            // 
            this.ColNomeUf.HeaderText = "Nome Uf";
            this.ColNomeUf.Name = "ColNomeUf";
            this.ColNomeUf.ReadOnly = true;
            this.ColNomeUf.Width = 300;
            // 
            // ColSiglaUf
            // 
            this.ColSiglaUf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColSiglaUf.HeaderText = "Sigla Uf";
            this.ColSiglaUf.Name = "ColSiglaUf";
            this.ColSiglaUf.ReadOnly = true;
            // 
            // btnSalvarUf
            // 
            this.btnSalvarUf.Location = new System.Drawing.Point(373, 234);
            this.btnSalvarUf.Name = "btnSalvarUf";
            this.btnSalvarUf.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarUf.TabIndex = 8;
            this.btnSalvarUf.Text = "Salvar";
            this.btnSalvarUf.UseVisualStyleBackColor = true;
            this.btnSalvarUf.Click += new System.EventHandler(this.btnSalvarUf_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cmbPaisCidade);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.cmbUf);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.btnAddCidade);
            this.tabPage3.Controls.Add(this.txtNomeCidade);
            this.tabPage3.Controls.Add(this.dgvCidade);
            this.tabPage3.Controls.Add(this.btnSalvarCidade);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(456, 265);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Cidade";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cmbPaisCidade
            // 
            this.cmbPaisCidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaisCidade.FormattingEnabled = true;
            this.cmbPaisCidade.Location = new System.Drawing.Point(8, 35);
            this.cmbPaisCidade.Name = "cmbPaisCidade";
            this.cmbPaisCidade.Size = new System.Drawing.Size(132, 21);
            this.cmbPaisCidade.TabIndex = 1;
            this.cmbPaisCidade.SelectedIndexChanged += new System.EventHandler(this.cmbPaisCidade_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "País";
            // 
            // cmbUf
            // 
            this.cmbUf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUf.FormattingEnabled = true;
            this.cmbUf.Location = new System.Drawing.Point(146, 35);
            this.cmbUf.Name = "cmbUf";
            this.cmbUf.Size = new System.Drawing.Size(119, 21);
            this.cmbUf.TabIndex = 3;
            this.cmbUf.SelectedIndexChanged += new System.EventHandler(this.cmbUf_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(143, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "UF";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(268, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Nome Cidade";
            // 
            // btnAddCidade
            // 
            this.btnAddCidade.BackgroundImage = global::EmatWinFormsNetFramework1402.Properties.Resources.add;
            this.btnAddCidade.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddCidade.Location = new System.Drawing.Point(428, 36);
            this.btnAddCidade.Name = "btnAddCidade";
            this.btnAddCidade.Size = new System.Drawing.Size(20, 20);
            this.btnAddCidade.TabIndex = 6;
            this.btnAddCidade.Text = "+";
            this.btnAddCidade.UseVisualStyleBackColor = true;
            this.btnAddCidade.Click += new System.EventHandler(this.btnAddCidade_Click);
            // 
            // txtNomeCidade
            // 
            this.txtNomeCidade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeCidade.Location = new System.Drawing.Point(271, 36);
            this.txtNomeCidade.Name = "txtNomeCidade";
            this.txtNomeCidade.Size = new System.Drawing.Size(151, 20);
            this.txtNomeCidade.TabIndex = 5;
            this.txtNomeCidade.TextChanged += new System.EventHandler(this.txtNomeCidade_TextChanged);
            // 
            // dgvCidade
            // 
            this.dgvCidade.AllowUserToAddRows = false;
            this.dgvCidade.AllowUserToDeleteRows = false;
            this.dgvCidade.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvCidade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCidade.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCodigoCidade,
            this.ColNomeCidade});
            this.dgvCidade.Location = new System.Drawing.Point(8, 62);
            this.dgvCidade.Name = "dgvCidade";
            this.dgvCidade.RowHeadersVisible = false;
            this.dgvCidade.Size = new System.Drawing.Size(440, 166);
            this.dgvCidade.TabIndex = 7;
            this.dgvCidade.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCidade_CellContentDoubleClick);
            // 
            // ColCodigoCidade
            // 
            this.ColCodigoCidade.HeaderText = "Código Cidade";
            this.ColCodigoCidade.Name = "ColCodigoCidade";
            this.ColCodigoCidade.ReadOnly = true;
            this.ColCodigoCidade.Visible = false;
            // 
            // ColNomeCidade
            // 
            this.ColNomeCidade.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColNomeCidade.HeaderText = "Nome Cidade";
            this.ColNomeCidade.Name = "ColNomeCidade";
            this.ColNomeCidade.ReadOnly = true;
            // 
            // btnSalvarCidade
            // 
            this.btnSalvarCidade.Location = new System.Drawing.Point(373, 234);
            this.btnSalvarCidade.Name = "btnSalvarCidade";
            this.btnSalvarCidade.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarCidade.TabIndex = 8;
            this.btnSalvarCidade.Text = "Salvar";
            this.btnSalvarCidade.UseVisualStyleBackColor = true;
            this.btnSalvarCidade.Click += new System.EventHandler(this.btnSalvarCidade_Click);
            // 
            // frmCadastrarEndereco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 291);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCadastrarEndereco";
            this.Text = "Cadastrar País/UF/Cidade";
            this.Load += new System.EventHandler(this.frmCadastrarEndereco_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPais)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUf)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCidade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSalvarPais;
        private System.Windows.Forms.DataGridView dgvPais;
        private System.Windows.Forms.TextBox txtNomePais;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddPais;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cmbPais;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSiglaUf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddUf;
        private System.Windows.Forms.TextBox txtNomeUf;
        private System.Windows.Forms.DataGridView dgvUf;
        private System.Windows.Forms.Button btnSalvarUf;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox cmbUf;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAddCidade;
        private System.Windows.Forms.TextBox txtNomeCidade;
        private System.Windows.Forms.DataGridView dgvCidade;
        private System.Windows.Forms.Button btnSalvarCidade;
        private System.Windows.Forms.ComboBox cmbPaisCidade;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCodigoPais;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNomePais;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCodigoUf;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNomeUf;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColSiglaUf;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCodigoCidade;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNomeCidade;
    }
}