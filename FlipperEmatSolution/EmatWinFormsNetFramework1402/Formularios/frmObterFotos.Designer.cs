namespace EmatWinFormsNetFramework1402.Formularios
{
    partial class frmObterFotos
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
            this.imgVideo = new System.Windows.Forms.PictureBox();
            this.btCapture = new System.Windows.Forms.Button();
            this.btSalvar = new System.Windows.Forms.Button();
            this.imgCapture = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btCancelar = new System.Windows.Forms.Button();
            this.cmblstVideoDevices = new System.Windows.Forms.ComboBox();
            this.btIniciar = new System.Windows.Forms.Button();
            this.ckbPadronizar = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgVideo
            // 
            this.imgVideo.Location = new System.Drawing.Point(5, 7);
            this.imgVideo.Name = "imgVideo";
            this.imgVideo.Size = new System.Drawing.Size(320, 240);
            this.imgVideo.TabIndex = 1;
            this.imgVideo.TabStop = false;
            // 
            // btCapture
            // 
            this.btCapture.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCapture.Location = new System.Drawing.Point(106, 261);
            this.btCapture.Name = "btCapture";
            this.btCapture.Size = new System.Drawing.Size(108, 29);
            this.btCapture.TabIndex = 3;
            this.btCapture.Text = "Fotografar";
            this.btCapture.UseVisualStyleBackColor = true;
            this.btCapture.Click += new System.EventHandler(this.cmdGrabImage_Click);
            // 
            // btSalvar
            // 
            this.btSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSalvar.Location = new System.Drawing.Point(220, 261);
            this.btSalvar.Name = "btSalvar";
            this.btSalvar.Size = new System.Drawing.Size(108, 29);
            this.btSalvar.TabIndex = 4;
            this.btSalvar.Text = "Salvar";
            this.btSalvar.UseVisualStyleBackColor = true;
            this.btSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // imgCapture
            // 
            this.imgCapture.Location = new System.Drawing.Point(333, 7);
            this.imgCapture.Name = "imgCapture";
            this.imgCapture.Size = new System.Drawing.Size(320, 240);
            this.imgCapture.TabIndex = 0;
            this.imgCapture.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 304);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(658, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // btCancelar
            // 
            this.btCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCancelar.Location = new System.Drawing.Point(334, 261);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(108, 29);
            this.btCancelar.TabIndex = 9;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // cmblstVideoDevices
            // 
            this.cmblstVideoDevices.FormattingEnabled = true;
            this.cmblstVideoDevices.Location = new System.Drawing.Point(64, 19);
            this.cmblstVideoDevices.Name = "cmblstVideoDevices";
            this.cmblstVideoDevices.Size = new System.Drawing.Size(129, 21);
            this.cmblstVideoDevices.TabIndex = 10;
            // 
            // btIniciar
            // 
            this.btIniciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btIniciar.Location = new System.Drawing.Point(5, 261);
            this.btIniciar.Name = "btIniciar";
            this.btIniciar.Size = new System.Drawing.Size(95, 29);
            this.btIniciar.TabIndex = 13;
            this.btIniciar.Text = "Iniciar";
            this.btIniciar.UseVisualStyleBackColor = true;
            this.btIniciar.Click += new System.EventHandler(this.btIniciar_Click);
            // 
            // ckbPadronizar
            // 
            this.ckbPadronizar.AutoSize = true;
            this.ckbPadronizar.Location = new System.Drawing.Point(6, 21);
            this.ckbPadronizar.Name = "ckbPadronizar";
            this.ckbPadronizar.Size = new System.Drawing.Size(60, 17);
            this.ckbPadronizar.TabIndex = 14;
            this.ckbPadronizar.Text = "Padrão";
            this.ckbPadronizar.UseVisualStyleBackColor = true;
            this.ckbPadronizar.CheckedChanged += new System.EventHandler(this.ckbPadronizar_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmblstVideoDevices);
            this.groupBox1.Controls.Add(this.ckbPadronizar);
            this.groupBox1.Location = new System.Drawing.Point(448, 251);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 48);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selecione a Webcam";
            // 
            // frmObterFotos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(658, 326);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btIniciar);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btSalvar);
            this.Controls.Add(this.btCapture);
            this.Controls.Add(this.imgVideo);
            this.Controls.Add(this.imgCapture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmObterFotos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Obter Foto";
            this.Load += new System.EventHandler(this.FRobterfoto_Load);
            this.Shown += new System.EventHandler(this.frmObterFotos_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgVideo;
        private System.Windows.Forms.Button btCapture;
        private System.Windows.Forms.Button btSalvar;
        private System.Windows.Forms.PictureBox imgCapture;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.ComboBox cmblstVideoDevices;
        private System.Windows.Forms.Button btIniciar;
        private System.Windows.Forms.CheckBox ckbPadronizar;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}