namespace EmatWinFormsNetFramework13032.Configurações
{
    partial class FRconf
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCancelar_tab2 = new System.Windows.Forms.Button();
            this.btnAplicar_tab2 = new System.Windows.Forms.Button();
            this.ckbModoserv = new System.Windows.Forms.CheckBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btLocfotos = new System.Windows.Forms.Button();
            this.btLocbd = new System.Windows.Forms.Button();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btAplicar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCamfotos = new System.Windows.Forms.TextBox();
            this.txtCambd = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnCancelar_tab2);
            this.tabPage2.Controls.Add(this.btnAplicar_tab2);
            this.tabPage2.Controls.Add(this.ckbModoserv);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(613, 380);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Funcionamento";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCancelar_tab2
            // 
            this.btnCancelar_tab2.Location = new System.Drawing.Point(449, 132);
            this.btnCancelar_tab2.Name = "btnCancelar_tab2";
            this.btnCancelar_tab2.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar_tab2.TabIndex = 7;
            this.btnCancelar_tab2.Text = "Cancelar";
            this.btnCancelar_tab2.UseVisualStyleBackColor = true;
            this.btnCancelar_tab2.Click += new System.EventHandler(this.btnCancelar_tab2_Click);
            // 
            // btnAplicar_tab2
            // 
            this.btnAplicar_tab2.Location = new System.Drawing.Point(530, 132);
            this.btnAplicar_tab2.Name = "btnAplicar_tab2";
            this.btnAplicar_tab2.Size = new System.Drawing.Size(75, 23);
            this.btnAplicar_tab2.TabIndex = 6;
            this.btnAplicar_tab2.Text = "Aplicar";
            this.btnAplicar_tab2.UseVisualStyleBackColor = true;
            this.btnAplicar_tab2.Click += new System.EventHandler(this.btnAplicar_tab2_Click);
            // 
            // ckbModoserv
            // 
            this.ckbModoserv.AutoSize = true;
            this.ckbModoserv.Location = new System.Drawing.Point(8, 6);
            this.ckbModoserv.Name = "ckbModoserv";
            this.ckbModoserv.Size = new System.Drawing.Size(254, 17);
            this.ckbModoserv.TabIndex = 0;
            this.ckbModoserv.Text = "Modo Servidor - Inicia Catraca Automaticamente";
            this.ckbModoserv.UseVisualStyleBackColor = true;
            this.ckbModoserv.CheckedChanged += new System.EventHandler(this.ckbModoserv_CheckedChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnAplicar);
            this.tabPage1.Controls.Add(this.txtValor);
            this.tabPage1.Controls.Add(this.txtItem);
            this.tabPage1.Controls.Add(this.btLocfotos);
            this.tabPage1.Controls.Add(this.btLocbd);
            this.tabPage1.Controls.Add(this.btCancelar);
            this.tabPage1.Controls.Add(this.btAplicar);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtCamfotos);
            this.tabPage1.Controls.Add(this.txtCambd);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(613, 380);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Caminhos";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btLocfotos
            // 
            this.btLocfotos.Location = new System.Drawing.Point(530, 84);
            this.btLocfotos.Name = "btLocfotos";
            this.btLocfotos.Size = new System.Drawing.Size(75, 20);
            this.btLocfotos.TabIndex = 7;
            this.btLocfotos.Text = "Localizar";
            this.btLocfotos.UseVisualStyleBackColor = true;
            this.btLocfotos.Click += new System.EventHandler(this.btLocfotos_Click);
            // 
            // btLocbd
            // 
            this.btLocbd.Enabled = false;
            this.btLocbd.Location = new System.Drawing.Point(530, 46);
            this.btLocbd.Name = "btLocbd";
            this.btLocbd.Size = new System.Drawing.Size(75, 20);
            this.btLocbd.TabIndex = 6;
            this.btLocbd.Text = "Localizar";
            this.btLocbd.UseVisualStyleBackColor = true;
            this.btLocbd.Click += new System.EventHandler(this.btLocbd_Click);
            // 
            // btCancelar
            // 
            this.btCancelar.Location = new System.Drawing.Point(449, 153);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(75, 23);
            this.btCancelar.TabIndex = 5;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // btAplicar
            // 
            this.btAplicar.Enabled = false;
            this.btAplicar.Location = new System.Drawing.Point(530, 153);
            this.btAplicar.Name = "btAplicar";
            this.btAplicar.Size = new System.Drawing.Size(75, 23);
            this.btAplicar.TabIndex = 4;
            this.btAplicar.Text = "Aplicar";
            this.btAplicar.UseVisualStyleBackColor = true;
            this.btAplicar.Click += new System.EventHandler(this.btAplicar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fotos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Banco de Dados";
            // 
            // txtCamfotos
            // 
            this.txtCamfotos.Enabled = false;
            this.txtCamfotos.Location = new System.Drawing.Point(8, 85);
            this.txtCamfotos.Name = "txtCamfotos";
            this.txtCamfotos.Size = new System.Drawing.Size(516, 20);
            this.txtCamfotos.TabIndex = 1;
            this.txtCamfotos.TextChanged += new System.EventHandler(this.txtCamfotos_TextChanged);
            // 
            // txtCambd
            // 
            this.txtCambd.Enabled = false;
            this.txtCambd.Location = new System.Drawing.Point(8, 46);
            this.txtCambd.Name = "txtCambd";
            this.txtCambd.Size = new System.Drawing.Size(516, 20);
            this.txtCambd.TabIndex = 0;
            this.txtCambd.Text = "BLOQUEADO - BUSCA NA LINHA DO CÓDIGO";
            this.txtCambd.TextChanged += new System.EventHandler(this.txtCamfotos_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(621, 406);
            this.tabControl1.TabIndex = 0;
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(8, 138);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(79, 20);
            this.txtItem.TabIndex = 8;
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(8, 164);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(79, 20);
            this.txtValor.TabIndex = 9;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(102, 159);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(85, 29);
            this.btnAplicar.TabIndex = 10;
            this.btnAplicar.Text = "button1";
            this.btnAplicar.UseVisualStyleBackColor = true;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // FRconf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 406);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRconf";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações";
            this.Load += new System.EventHandler(this.FRconf_Load);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnCancelar_tab2;
        private System.Windows.Forms.Button btnAplicar_tab2;
        private System.Windows.Forms.CheckBox ckbModoserv;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btLocfotos;
        private System.Windows.Forms.Button btLocbd;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btAplicar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCamfotos;
        private System.Windows.Forms.TextBox txtCambd;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.TextBox txtItem;
    }
}