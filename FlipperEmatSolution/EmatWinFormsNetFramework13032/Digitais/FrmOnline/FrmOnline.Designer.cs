namespace EmatWinFormsNetFramework13032.Digitais.FrmOnline
{
    partial class FrmOnline
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
            this.ofgBase = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblEmExec1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblDados = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lstUsuarios = new System.Windows.Forms.ListBox();
            this.gbCadastro = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdLimpar = new System.Windows.Forms.Button();
            this.lstBilhetes = new System.Windows.Forms.ListBox();
            this.gbMonitoracao = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmdEntrada = new System.Windows.Forms.Button();
            this.cmdSair = new System.Windows.Forms.Button();
            this.gbCadastrados = new System.Windows.Forms.GroupBox();
            this.lstInnersCadastrados = new System.Windows.Forms.ListBox();
            this.btnPararMaquina = new System.Windows.Forms.Button();
            this.btnIniciarMaquina = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblManu2 = new System.Windows.Forms.Label();
            this.lblEstimado = new System.Windows.Forms.Label();
            this.btnParar = new System.Windows.Forms.Button();
            this.lblQtdcatraca = new System.Windows.Forms.Label();
            this.lblQtdenviar = new System.Windows.Forms.Label();
            this.lblQtderros = new System.Windows.Forms.Label();
            this.lblQtdenviados = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblUsuarioscatraca = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ltbUsuarioscatraca = new System.Windows.Forms.ListBox();
            this.ltbEnviar = new System.Windows.Forms.ListBox();
            this.lblManutencao = new System.Windows.Forms.Label();
            this.btnEnviar_dig = new System.Windows.Forms.Button();
            this.btnReceber_dig = new System.Windows.Forms.Button();
            this.ltbErros = new System.Windows.Forms.ListBox();
            this.lblErros = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbEnviando = new System.Windows.Forms.Label();
            this.ltbEnviados = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFora_digitais = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnManutenção = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbCadastro.SuspendLayout();
            this.gbMonitoracao.SuspendLayout();
            this.gbCadastrados.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofgBase
            // 
            this.ofgBase.FileName = "*.mdb";
            this.ofgBase.Filter = "*.mdb|Base de Dados";
            this.ofgBase.FilterIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEmExec1,
            this.lblDados});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 422);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(727, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 34;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblEmExec1
            // 
            this.lblEmExec1.Name = "lblEmExec1";
            this.lblEmExec1.Size = new System.Drawing.Size(10, 17);
            this.lblEmExec1.Text = " ";
            // 
            // lblDados
            // 
            this.lblDados.Name = "lblDados";
            this.lblDados.Size = new System.Drawing.Size(0, 17);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(727, 422);
            this.tabControl1.TabIndex = 44;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblEstado);
            this.tabPage1.Controls.Add(this.lstUsuarios);
            this.tabPage1.Controls.Add(this.gbCadastro);
            this.tabPage1.Controls.Add(this.btnPararMaquina);
            this.tabPage1.Controls.Add(this.btnIniciarMaquina);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(719, 396);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Catraca";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.Location = new System.Drawing.Point(317, 21);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(0, 25);
            this.lblEstado.TabIndex = 51;
            // 
            // lstUsuarios
            // 
            this.lstUsuarios.FormattingEnabled = true;
            this.lstUsuarios.Location = new System.Drawing.Point(-131, -22);
            this.lstUsuarios.Name = "lstUsuarios";
            this.lstUsuarios.Size = new System.Drawing.Size(122, 108);
            this.lstUsuarios.TabIndex = 50;
            // 
            // gbCadastro
            // 
            this.gbCadastro.Controls.Add(this.label3);
            this.gbCadastro.Controls.Add(this.cmdLimpar);
            this.gbCadastro.Controls.Add(this.lstBilhetes);
            this.gbCadastro.Controls.Add(this.gbMonitoracao);
            this.gbCadastro.Controls.Add(this.cmdEntrada);
            this.gbCadastro.Controls.Add(this.cmdSair);
            this.gbCadastro.Controls.Add(this.gbCadastrados);
            this.gbCadastro.Location = new System.Drawing.Point(8, 89);
            this.gbCadastro.Name = "gbCadastro";
            this.gbCadastro.Size = new System.Drawing.Size(705, 301);
            this.gbCadastro.TabIndex = 44;
            this.gbCadastro.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Bilhetes coletados";
            // 
            // cmdLimpar
            // 
            this.cmdLimpar.Location = new System.Drawing.Point(151, 278);
            this.cmdLimpar.Name = "cmdLimpar";
            this.cmdLimpar.Size = new System.Drawing.Size(66, 23);
            this.cmdLimpar.TabIndex = 4;
            this.cmdLimpar.Text = "Limpar";
            this.cmdLimpar.UseVisualStyleBackColor = true;
            // 
            // lstBilhetes
            // 
            this.lstBilhetes.FormattingEnabled = true;
            this.lstBilhetes.Location = new System.Drawing.Point(14, 137);
            this.lstBilhetes.Name = "lstBilhetes";
            this.lstBilhetes.Size = new System.Drawing.Size(673, 134);
            this.lstBilhetes.TabIndex = 0;
            // 
            // gbMonitoracao
            // 
            this.gbMonitoracao.Controls.Add(this.lblStatus);
            this.gbMonitoracao.Location = new System.Drawing.Point(16, 71);
            this.gbMonitoracao.Name = "gbMonitoracao";
            this.gbMonitoracao.Size = new System.Drawing.Size(671, 47);
            this.gbMonitoracao.TabIndex = 5;
            this.gbMonitoracao.TabStop = false;
            this.gbMonitoracao.Text = "Status comunicação";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(6, 22);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 1;
            // 
            // cmdEntrada
            // 
            this.cmdEntrada.Enabled = false;
            this.cmdEntrada.Location = new System.Drawing.Point(14, 278);
            this.cmdEntrada.Name = "cmdEntrada";
            this.cmdEntrada.Size = new System.Drawing.Size(64, 23);
            this.cmdEntrada.TabIndex = 3;
            this.cmdEntrada.Text = "Entrada";
            this.cmdEntrada.UseVisualStyleBackColor = true;
            // 
            // cmdSair
            // 
            this.cmdSair.Enabled = false;
            this.cmdSair.Location = new System.Drawing.Point(84, 278);
            this.cmdSair.Name = "cmdSair";
            this.cmdSair.Size = new System.Drawing.Size(61, 23);
            this.cmdSair.TabIndex = 2;
            this.cmdSair.Text = "Saida";
            this.cmdSair.UseVisualStyleBackColor = true;
            // 
            // gbCadastrados
            // 
            this.gbCadastrados.Controls.Add(this.lstInnersCadastrados);
            this.gbCadastrados.Location = new System.Drawing.Point(15, 19);
            this.gbCadastrados.Name = "gbCadastrados";
            this.gbCadastrados.Size = new System.Drawing.Size(672, 46);
            this.gbCadastrados.TabIndex = 4;
            this.gbCadastrados.TabStop = false;
            this.gbCadastrados.Text = "Dispositivos Cadastrados em Memória";
            // 
            // lstInnersCadastrados
            // 
            this.lstInnersCadastrados.FormattingEnabled = true;
            this.lstInnersCadastrados.HorizontalScrollbar = true;
            this.lstInnersCadastrados.Location = new System.Drawing.Point(6, 19);
            this.lstInnersCadastrados.Name = "lstInnersCadastrados";
            this.lstInnersCadastrados.Size = new System.Drawing.Size(659, 17);
            this.lstInnersCadastrados.TabIndex = 5;
            // 
            // btnPararMaquina
            // 
            this.btnPararMaquina.Location = new System.Drawing.Point(361, 6);
            this.btnPararMaquina.Name = "btnPararMaquina";
            this.btnPararMaquina.Size = new System.Drawing.Size(260, 77);
            this.btnPararMaquina.TabIndex = 46;
            this.btnPararMaquina.Text = "Parar Catraca";
            this.btnPararMaquina.UseVisualStyleBackColor = true;
            this.btnPararMaquina.Click += new System.EventHandler(this.btnPararMaquina_Click);
            // 
            // btnIniciarMaquina
            // 
            this.btnIniciarMaquina.Location = new System.Drawing.Point(72, 6);
            this.btnIniciarMaquina.Name = "btnIniciarMaquina";
            this.btnIniciarMaquina.Size = new System.Drawing.Size(272, 77);
            this.btnIniciarMaquina.TabIndex = 45;
            this.btnIniciarMaquina.Text = "Iniciar Catraca";
            this.btnIniciarMaquina.UseVisualStyleBackColor = true;
            this.btnIniciarMaquina.Click += new System.EventHandler(this.btnIniciarMaquina_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(719, 396);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Manutenção";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(713, 390);
            this.splitContainer1.SplitterDistance = 338;
            this.splitContainer1.TabIndex = 63;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblManu2);
            this.groupBox2.Controls.Add(this.lblEstimado);
            this.groupBox2.Controls.Add(this.btnParar);
            this.groupBox2.Controls.Add(this.lblQtdcatraca);
            this.groupBox2.Controls.Add(this.lblQtdenviar);
            this.groupBox2.Controls.Add(this.lblQtderros);
            this.groupBox2.Controls.Add(this.lblQtdenviados);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lblUsuarioscatraca);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ltbUsuarioscatraca);
            this.groupBox2.Controls.Add(this.ltbEnviar);
            this.groupBox2.Controls.Add(this.lblManutencao);
            this.groupBox2.Controls.Add(this.btnEnviar_dig);
            this.groupBox2.Controls.Add(this.btnReceber_dig);
            this.groupBox2.Controls.Add(this.ltbErros);
            this.groupBox2.Controls.Add(this.lblErros);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lbEnviando);
            this.groupBox2.Controls.Add(this.ltbEnviados);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(338, 390);
            this.groupBox2.TabIndex = 65;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Receber e Enviar Digitais";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // lblManu2
            // 
            this.lblManu2.Location = new System.Drawing.Point(201, 360);
            this.lblManu2.Name = "lblManu2";
            this.lblManu2.Size = new System.Drawing.Size(0, 13);
            this.lblManu2.TabIndex = 76;
            // 
            // lblEstimado
            // 
            this.lblEstimado.Location = new System.Drawing.Point(307, 360);
            this.lblEstimado.Name = "lblEstimado";
            this.lblEstimado.Size = new System.Drawing.Size(0, 13);
            this.lblEstimado.TabIndex = 75;
            // 
            // btnParar
            // 
            this.btnParar.Enabled = false;
            this.btnParar.Location = new System.Drawing.Point(220, 19);
            this.btnParar.Name = "btnParar";
            this.btnParar.Size = new System.Drawing.Size(100, 23);
            this.btnParar.TabIndex = 74;
            this.btnParar.Text = "Interromper Envio";
            this.btnParar.UseVisualStyleBackColor = true;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);
            // 
            // lblQtdcatraca
            // 
            this.lblQtdcatraca.Location = new System.Drawing.Point(6, 308);
            this.lblQtdcatraca.Name = "lblQtdcatraca";
            this.lblQtdcatraca.Size = new System.Drawing.Size(25, 13);
            this.lblQtdcatraca.TabIndex = 73;
            // 
            // lblQtdenviar
            // 
            this.lblQtdenviar.Location = new System.Drawing.Point(93, 311);
            this.lblQtdenviar.Name = "lblQtdenviar";
            this.lblQtdenviar.Size = new System.Drawing.Size(15, 13);
            this.lblQtdenviar.TabIndex = 72;
            // 
            // lblQtderros
            // 
            this.lblQtderros.Location = new System.Drawing.Point(264, 311);
            this.lblQtderros.Name = "lblQtderros";
            this.lblQtderros.Size = new System.Drawing.Size(71, 16);
            this.lblQtderros.TabIndex = 70;
            // 
            // lblQtdenviados
            // 
            this.lblQtdenviados.Location = new System.Drawing.Point(178, 311);
            this.lblQtdenviados.Name = "lblQtdenviados";
            this.lblQtdenviados.Size = new System.Drawing.Size(26, 16);
            this.lblQtdenviados.TabIndex = 71;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(108, 291);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 69;
            // 
            // lblUsuarioscatraca
            // 
            this.lblUsuarioscatraca.AutoSize = true;
            this.lblUsuarioscatraca.Location = new System.Drawing.Point(5, 51);
            this.lblUsuarioscatraca.Name = "lblUsuarioscatraca";
            this.lblUsuarioscatraca.Size = new System.Drawing.Size(61, 13);
            this.lblUsuarioscatraca.TabIndex = 68;
            this.lblUsuarioscatraca.Text = "Na Catraca";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "Para Enviar";
            // 
            // ltbUsuarioscatraca
            // 
            this.ltbUsuarioscatraca.FormattingEnabled = true;
            this.ltbUsuarioscatraca.Location = new System.Drawing.Point(8, 67);
            this.ltbUsuarioscatraca.Name = "ltbUsuarioscatraca";
            this.ltbUsuarioscatraca.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ltbUsuarioscatraca.Size = new System.Drawing.Size(79, 238);
            this.ltbUsuarioscatraca.TabIndex = 66;
            this.ltbUsuarioscatraca.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ltbUsuarioscatraca_MouseDown);
            // 
            // ltbEnviar
            // 
            this.ltbEnviar.FormattingEnabled = true;
            this.ltbEnviar.Location = new System.Drawing.Point(93, 67);
            this.ltbEnviar.Name = "ltbEnviar";
            this.ltbEnviar.Size = new System.Drawing.Size(79, 238);
            this.ltbEnviar.TabIndex = 62;
            // 
            // lblManutencao
            // 
            this.lblManutencao.Location = new System.Drawing.Point(24, 360);
            this.lblManutencao.Name = "lblManutencao";
            this.lblManutencao.Size = new System.Drawing.Size(0, 13);
            this.lblManutencao.TabIndex = 65;
            // 
            // btnEnviar_dig
            // 
            this.btnEnviar_dig.Enabled = false;
            this.btnEnviar_dig.Location = new System.Drawing.Point(114, 19);
            this.btnEnviar_dig.Name = "btnEnviar_dig";
            this.btnEnviar_dig.Size = new System.Drawing.Size(100, 23);
            this.btnEnviar_dig.TabIndex = 56;
            this.btnEnviar_dig.Text = "Enviar Digtais";
            this.btnEnviar_dig.UseVisualStyleBackColor = true;
            this.btnEnviar_dig.Click += new System.EventHandler(this.btnEnviar_dig_Click);
            // 
            // btnReceber_dig
            // 
            this.btnReceber_dig.Location = new System.Drawing.Point(8, 19);
            this.btnReceber_dig.Name = "btnReceber_dig";
            this.btnReceber_dig.Size = new System.Drawing.Size(100, 23);
            this.btnReceber_dig.TabIndex = 59;
            this.btnReceber_dig.Text = "Receber Digitais";
            this.btnReceber_dig.UseVisualStyleBackColor = true;
            this.btnReceber_dig.Click += new System.EventHandler(this.btnReceber_dig_Click);
            // 
            // ltbErros
            // 
            this.ltbErros.FormattingEnabled = true;
            this.ltbErros.Location = new System.Drawing.Point(264, 67);
            this.ltbErros.Name = "ltbErros";
            this.ltbErros.Size = new System.Drawing.Size(71, 238);
            this.ltbErros.TabIndex = 61;
            // 
            // lblErros
            // 
            this.lblErros.AutoSize = true;
            this.lblErros.Location = new System.Drawing.Point(300, 51);
            this.lblErros.Name = "lblErros";
            this.lblErros.Size = new System.Drawing.Size(31, 13);
            this.lblErros.TabIndex = 60;
            this.lblErros.Text = "Erros";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 63;
            this.label1.Text = "Enviados";
            // 
            // lbEnviando
            // 
            this.lbEnviando.Location = new System.Drawing.Point(24, 337);
            this.lbEnviando.Name = "lbEnviando";
            this.lbEnviando.Size = new System.Drawing.Size(0, 13);
            this.lbEnviando.TabIndex = 58;
            // 
            // ltbEnviados
            // 
            this.ltbEnviados.FormattingEnabled = true;
            this.ltbEnviados.Location = new System.Drawing.Point(178, 67);
            this.ltbEnviados.Name = "ltbEnviados";
            this.ltbEnviados.Size = new System.Drawing.Size(80, 238);
            this.ltbEnviados.TabIndex = 57;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFora_digitais);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnManutenção);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 390);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Na Catraca";
            // 
            // btnFora_digitais
            // 
            this.btnFora_digitais.Location = new System.Drawing.Point(290, 51);
            this.btnFora_digitais.Name = "btnFora_digitais";
            this.btnFora_digitais.Size = new System.Drawing.Size(75, 22);
            this.btnFora_digitais.TabIndex = 8;
            this.btnFora_digitais.Text = "Fora";
            this.btnFora_digitais.UseVisualStyleBackColor = true;
            this.btnFora_digitais.Click += new System.EventHandler(this.btnFora_digitais_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(9, 186);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(356, 186);
            this.listBox1.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 155);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Status: Aguardando";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 142);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "label9";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "label4";
            // 
            // btnManutenção
            // 
            this.btnManutenção.Location = new System.Drawing.Point(290, 19);
            this.btnManutenção.Name = "btnManutenção";
            this.btnManutenção.Size = new System.Drawing.Size(75, 23);
            this.btnManutenção.TabIndex = 0;
            this.btnManutenção.Text = "Rodar CoD";
            this.btnManutenção.UseVisualStyleBackColor = true;
            this.btnManutenção.Click += new System.EventHandler(this.btnManutenção_Click);
            // 
            // FrmOnline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 444);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOnline";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle Catraca";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainBIO_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmOnline_FormClosed);
            this.Load += new System.EventHandler(this.FrmOnline_Load);
            this.Shown += new System.EventHandler(this.MainBIO_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.gbCadastro.ResumeLayout(false);
            this.gbCadastro.PerformLayout();
            this.gbMonitoracao.ResumeLayout(false);
            this.gbMonitoracao.PerformLayout();
            this.gbCadastrados.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.OpenFileDialog ofgBase;
        private System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel lblEmExec1;
        public System.Windows.Forms.ToolStripStatusLabel lblDados;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.ListBox lstUsuarios;
        private System.Windows.Forms.GroupBox gbCadastro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdLimpar;
        public System.Windows.Forms.ListBox lstBilhetes;
        private System.Windows.Forms.GroupBox gbMonitoracao;
        public System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.Button cmdEntrada;
        public System.Windows.Forms.Button cmdSair;
        private System.Windows.Forms.GroupBox gbCadastrados;
        public System.Windows.Forms.ListBox lstInnersCadastrados;
        public System.Windows.Forms.Button btnPararMaquina;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox ltbErros;
        private System.Windows.Forms.Label lblErros;
        private System.Windows.Forms.Button btnReceber_dig;
        private System.Windows.Forms.Label lbEnviando;
        private System.Windows.Forms.ListBox ltbEnviados;
        private System.Windows.Forms.Button btnEnviar_dig;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox ltbEnviar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label lblManutencao;
        private System.Windows.Forms.Label lblUsuarioscatraca;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ListBox ltbUsuarioscatraca;
        private System.Windows.Forms.Label lblQtdcatraca;
        private System.Windows.Forms.Label lblQtdenviar;
        private System.Windows.Forms.Label lblQtderros;
        private System.Windows.Forms.Label lblQtdenviados;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnParar;
        private System.Windows.Forms.Label lblEstimado;
        public System.Windows.Forms.Label lblEstado;
        public System.Windows.Forms.Button btnIniciarMaquina;
        private System.Windows.Forms.Button btnManutenção;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox listBox1;
        public System.Windows.Forms.Label lblManu2;
        private System.Windows.Forms.Button btnFora_digitais;
    }
}