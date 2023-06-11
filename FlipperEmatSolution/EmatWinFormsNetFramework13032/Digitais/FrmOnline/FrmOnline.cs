using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using EmatWinFormsNetFramework13032.Digitais.Entity;
using EmatWinFormsNetFramework13032.Digitais.COM;
using System.Threading;

using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;

namespace EmatWinFormsNetFramework13032.Digitais.FrmOnline
    
{
    public partial class FrmOnline : Form
    {
        public int veri = 0;

        static bool aberto = false;
        private int Digital = 0;        

        public string dig_1 = "";
        public string dig_2 = "";

        public string dig_ = "";

        public int continua = 1;
        public int enviado = 0;

        public List<string> lista = new List<string>();

        public List<string> lista_dig = new List<string>();

        public List<string> lista_dig_cadastrar= new List<string>();
        public List<int> lista_dig_excluir = new List<int>();

        public List<string> lista_dig_cadastro_ok = new List<string>();


        #region Propriedades

        private int ListIndex = -1;

        #region Template
        private byte[] _template;
        public byte[] Template
        {
            get { return _template; }
            set { _template = value; }
        }
        #endregion

        #region TemplateLeitor
        private byte[] _templateLeitor;
        public byte[] TemplateLeitor
        {
            get { return _templateLeitor; }
            set { _templateLeitor = value; }
        }
        #endregion

        #region UiMainBIO
        private static FrmOnline _uiMainBIO;
        public static FrmOnline UiMainOnline
        {
            get { return _uiMainBIO; }
            set { _uiMainBIO = value; }
        }
        #endregion

        #region Timeout
        private int _timeout;
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        #endregion

        #region LstUsuarios
        private List<Usuario> _lstUsuarios = new List<Usuario>();
        public List<Usuario> LstUsuarios
        {
            get { return _lstUsuarios; }
            set { _lstUsuarios = value; }
        }

        #endregion

        #region Parar
        private bool _ativa;
        public bool Ativa
        {
            get { return _ativa; }
            set { _ativa = value; }
        }

        #endregion

        #region LstInners
        private List<Inner> _lstInners = new List<Inner>();
        public List<Inner> LstInners
        {
            get { return _lstInners; }
            set { _lstInners = value; }
        }
        #endregion

        #region FechouMaquina
        private bool _fechouMaquina;
        public bool FechouMaquina
        {
            get { return _fechouMaquina; }
            set { _fechouMaquina = value; }
        }
        #endregion

        #endregion

        #region Métodos

        #region Métodos Internos da Form

        #region FrmOnline

        private static Form FPai;

        public FrmOnline(Form pai)
        {
            if (!aberto)
            {
                InitializeComponent();
                FPai = pai;
                Template = new byte[844];
                TemplateLeitor = new byte[404];
                MdiParent = pai;
                aberto = true;
                Show();
            }
            else
            {
                Dispose();
            }
        }
        #endregion

        #region GetInstance
        public static FrmOnline GetInstance()
        {
            if (UiMainOnline == null)
            {
                UiMainOnline = (FrmOnline)FPai.ActiveMdiChild;
            }
            return UiMainOnline;
        }
        #endregion

        #region MainBIO_Load
        private void MainBIO_Load(object sender, EventArgs e)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();


        }
        #endregion

        #region MainBIO_Shown
        private void MainBIO_Shown(object sender, EventArgs e)
        {
            //Recupera a Instancia da Form.
            GetInstance();
            //cboEquipamento.SelectedIndex = 1;

            //Adiciona o Inner a lista em memória..
            FrmOnlineController.AdicionarInnerOnline(UiMainOnline);

            //Atualiza a lista exibida..
            FrmOnlineController.AtualizaListaInnerOnline(UiMainOnline);

            //Verifica se esta configurado com servidor,
            //caso afirmativo, inicia rotina automatica para ligar catraca.
            Configurações.CSconnexoes_txt conf = new Configurações.CSconnexoes_txt();
            conf.get_configuracoes();

            if (conf.modo == "servidor")
            {
                btnIniciarMaquina.PerformClick();
            }

        }
        #endregion

        #region LiberaCatraca

        private bool LiberaCatraca()
        {
            if (UiMainOnline.lstInnersCadastrados.SelectedIndex == -1)
                UiMainOnline.lstInnersCadastrados.SelectedIndex = 0;

            foreach (Inner inner in UiMainOnline.LstInners)
            {
                if (inner.Numero == ((Inner)UiMainOnline.lstInnersCadastrados.SelectedItem).Numero)
                {
                    inner.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                }
            }
            return true;
        }
        #endregion

        #endregion

        #endregion

        #region Eventos

        #region btnAdicionarUsuarioInnerOnline_Click
        private void btnAdicionarUsuarioInnerOnline_Click(object sender, EventArgs e)
        {
            //Adiciona o Inner a lista em memória..
            FrmOnlineController.AdicionarInnerOnline(UiMainOnline);

            //Atualiza a lista exibida..
            FrmOnlineController.AtualizaListaInnerOnline(UiMainOnline);
        }
        #endregion

        #region btnIniciarMaquina_Click
        private void btnIniciarMaquina_Click(object sender, EventArgs e)
        {
            btnPararMaquina.Enabled = true;
            btnIniciarMaquina.Enabled = false;
            Application.DoEvents();
            FrmOnlineController.IniciarMaquina(UiMainOnline);
        }
        #endregion

        #region btnPararMaquina_Click
        private void btnPararMaquina_Click(object sender, EventArgs e)
        {
            FrmOnlineController.PararMaquina(UiMainOnline);

            btnIniciarMaquina.Enabled = true;

            //Joga os Inners para estado de Reconexão
            foreach (Inner inner in UiMainOnline.LstInners)
            {
                inner.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }

            cmdEntrada.Enabled = false;
            btnPararMaquina.Enabled = false;
            cmdSair.Enabled = false;
        }
        #endregion

        #region MainBIO_FormClosing
        private void MainBIO_FormClosing(object sender, FormClosingEventArgs e)
        {
            FrmOnlineController.PararMaquina(UiMainOnline);

            //Apaga o Instance unico da frmMainBio.
            UiMainOnline = null;
        }
        #endregion

        #region btnRemoverInnerLista_Click
        private void btnRemoverInnerLista_Click(object sender, EventArgs e)
        {
            FrmOnlineController.RemoverInnerLista(UiMainOnline);
        }
        #endregion

        #region FrmOnline_Load
        private void FrmOnline_Load(object sender, EventArgs e)
        {


            LstUsuarios = new List<Usuario>();
            LstInners = new List<Inner>();

            //this.cboPadraoCartaoOnline.Items.Add("Topdata");
            //this.cboPadraoCartaoOnline.Items.Add("Livre");
            //this.cboPadraoCartaoOnline.SelectedIndex = 1;

            //this.cboTipoConexaoOnline.Items.Add("Serial");
            //this.cboTipoConexaoOnline.Items.Add("TCP/IP porta variável");
            //this.cboTipoConexaoOnline.Items.Add("TCP/IP porta fixa");
            //this.cboTipoConexaoOnline.SelectedIndex = 2;

            //this.cboTipoLeitor.Items.Clear();
            //this.cboTipoLeitor.Items.Add("Código Barras");
            //this.cboTipoLeitor.Items.Add("Magnético");
            //this.cboTipoLeitor.Items.Add("Proximidade Abatrack");
            //this.cboTipoLeitor.Items.Add("Proximidade Wiegand");
            //this.cboTipoLeitor.Items.Add("Proximidade Smart Card");
            //this.cboTipoLeitor.SelectedIndex = 0;

            //this.cboEquipamento.Items.Clear();
            //this.cboEquipamento.Items.Add("Não utilizado(Coletor)");
            //this.cboEquipamento.Items.Add("Catraca Entrada/Saída");
            //this.cboEquipamento.Items.Add("Catraca Entrada");
            //this.cboEquipamento.Items.Add("Catraca Saída");
            //this.cboEquipamento.Items.Add("Catraca Saída Liberada");
            //this.cboEquipamento.Items.Add("Catraca Entrada Liberada");
            //this.cboEquipamento.Items.Add("Catraca Liberada 2 Sentidos");
            //this.cboEquipamento.Items.Add("Catraca Liberada 2 Sentidos(Sentido Giro)");
            //this.cboEquipamento.Items.Add("Catraca com Urna");
            //this.cboEquipamento.SelectedIndex = 0;

            UiMainBIO1 = this;
            btnPararMaquina.Enabled = false;


            


        }
        #endregion

        #region cboTipoConexaoOnline_SelectedIndexChanged
        private void cboTipoConexaoOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboTipoConexaoOnline.SelectedIndex == 0)
            //{
            //    txtPortaOnline.Value = 1;
            //}
            //else
            //{
            //txtPortaOnline.Value = 3570;
            //}
        }
        #endregion

        #region ckbBIO_Click
        private void ckbBIO_Click(object sender, EventArgs e)
        {
            //if (ckbBIO.Checked)
            //{
            //    ckbVerificacao.Enabled = true;
            //    ckbIdentificacao.Enabled = true;
            //    ckbListaBio.Enabled = true;
            //}
            //else
            //{
            //    ckbVerificacao.Enabled = false;
            //    ckbIdentificacao.Enabled = false;
            //    ckbListaBio.Enabled = false;
            //    ckbVerificacao.Checked = false;
            //    ckbIdentificacao.Checked = false;
            //    ckbListaBio.Checked = false;
            //}
        }
        #endregion

        #region cboPadraoCartao_SelectedIndexChanged
        private void cboPadraoCartao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ckbDoisLeitores.Enabled = (!(0 == (int)Enumeradores.TipoLeitor.CODIGO_DE_BARRAS) && !(0 == (int)Enumeradores.TipoLeitor.MAGNETICO));
            //ckbDoisLeitores.Checked = false;            
        }
        #endregion

        #region cmdLimpar_Click
        private void cmdLimpar_Click(object sender, EventArgs e)
        {
            lstBilhetes.Items.Clear();
        }
        #endregion

        #region cmdEntrada_Click
        private void cmdEntrada_Click(object sender, EventArgs e)
        {
            Inner inner = new Inner();
            if (inner.Catraca)
            {
                cmdEntrada.Enabled = false;
                cmdSair.Enabled = false;
                FrmOnlineController.HABILITA_LADO_CATRACA(UiMainOnline, "Entrada");
                inner.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                cmdEntrada.Enabled = true;
                cmdSair.Enabled = true;
            }
            else
            {
                cmdEntrada.Enabled = false;
                cmdSair.Enabled = false;
                EasyInner.AcionarBipCurto(1);
                EasyInner.AcionarRele1(1);
                cmdEntrada.Enabled = true;
                cmdSair.Enabled = true;
            }
        }
        #endregion

        #region lstInnersCadastrados_Click
        private void lstInnersCadastrados_Click(object sender, EventArgs e)
        {
            ListIndex = lstInnersCadastrados.SelectedIndex;
        }
        #endregion

        #region cmdSair_Click
        private void cmdSair_Click(object sender, EventArgs e)
        {
            Inner inner = new Inner();
            if (inner.Catraca)
            {
                cmdEntrada.Enabled = false;
                cmdSair.Enabled = false;
                FrmOnlineController.HABILITA_LADO_CATRACA(UiMainOnline, "Saida");
                inner.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                cmdEntrada.Enabled = true;
                cmdSair.Enabled = true;
            }
            else
            {
                cmdEntrada.Enabled = false;
                cmdSair.Enabled = false;
                EasyInner.AcionarBipCurto(1);                
                EasyInner.AcionarRele2(1);
                cmdEntrada.Enabled = true;
                cmdSair.Enabled = true;
            }
        }
        #endregion

        private void ckbListaBio_Click(object sender, EventArgs e)
        {
            //if (ckbListaBio.Checked)
            //    ckbVerificacao.Checked = true;
        }

        #endregion

        private void ckbVerificacao_Click(object sender, EventArgs e)
        {
            //if (!ckbVerificacao.Checked)
            //   ckbListaBio.Checked = false;
        }

        private void FrmOnline_FormClosed(object sender, FormClosedEventArgs e)
        {
            aberto = false;
        }

        //------Envio Digital Catraca---------

        #region Propriedades1

        #region Template1
        private byte[] _template1;
        public byte[] Template1
        {
            get { return _template1; }
            set { _template1 = value; }
        }
        #endregion

        #region TemplateLeitor1
        private byte[] _templateLeitor1;
        public byte[] TemplateLeitor1
        {
            get { return _templateLeitor1; }
            set { _templateLeitor1 = value; }
        }
        #endregion

        #region UiMainBIO1
        private static FrmOnline _uiMainBIO1;
        public static FrmOnline UiMainBIO1
        {
            get { return _uiMainBIO1; }
            set { _uiMainBIO1 = value; }
        }
        #endregion

        #region Timeout1
        private int _timeout1;
        public int Timeout1
        {
            get { return _timeout1; }
            set { _timeout1 = value; }
        }

        #endregion

        #region LstUsuarios1
        private List<Usuario> _lstUsuarios1 = new List<Usuario>();
        public List<Usuario> LstUsuarios1
        {
            get { return _lstUsuarios1; }
            set { _lstUsuarios1 = value; }
        }

        #endregion

        #region Parar1
        private bool _ativa1;
        public bool Ativa1
        {
            get { return _ativa1; }
            set { _ativa1 = value; }
        }

        #endregion

        #region LstInners1
        private List<Inner> _lstInners1 = new List<Inner>();
        public List<Inner> LstInners1
        {
            get { return _lstInners1; }
            set { _lstInners1 = value; }
        }
        #endregion

        #region FechouMaquina1
        private bool _fechouMaquina1;
        public bool FechouMaquina1
        {
            get { return _fechouMaquina1; }
            set { _fechouMaquina1 = value; }
        }
        #endregion

        

        #endregion

        #region Dedo1
        private static StringBuilder _dedo1;

        public static StringBuilder Dedo1
        {
            get { return _dedo1; }
            set { _dedo1 = value; }
        }
        #endregion

        #region UiBIO1
        private static FrmOnline _uiMainBio1;
        public static FrmOnline UiBIO1
        {
            get { return _uiMainBio1; }
            set { _uiMainBio1 = value; }
        }
        #endregion        

        #region CONEXAÕ INNER

        #region Conectar
        //***********************************************************************************
        //CONECTAR
        //Rotina responsável por efetuar a conexão com o Inner
        //***********************************************************************************
        private static bool Conectar(FrmOnline UiMainBIO)
        {
            CSconf_catraca cscatraca = new CSconf_catraca();

            int Fim;
            int Ret = -1;
            Application.DoEvents();
            //Define o tipo de conexão selecionada no Combo..
            EasyInner.DefinirTipoConexao((byte)cscatraca.def_DefinirTipoConexao);

            //Fecha as conexões caso esteja aberta..
            EasyInner.FecharPortaComunicacao();

            //Abre a porta de Conexão conforme a Porta Indicada..
            Ret = EasyInner.AbrirPortaComunicacao(cscatraca.def_Porta);

            //Tenta Realizar a Conexão
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                //Registra o tempo fim de conexão (tempo atual +15)
                Fim = (int)EasyInner.RetornarSegundosSys() + 15;

                //Realiza loop enquanto o tempo fim for menor que o tempo atual, e o comando retornado diferente de OK.
                do
                {
                    //Realiza Pausa, libera Thread de Interface Grafica.
                    Pausa(1);

                    //Tenta abrir a conexão com inner utilizando Ping.
                    Ret = EasyInner.Ping(cscatraca.def_NumInner);
                }
                while ((EasyInner.RetornarSegundosSys() <= Fim) && (Ret != (int)Enumeradores.Retorno.RET_COMANDO_OK));

                //Caso o retorno seja OK.. volta a função chamadora..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {

                    return true;
                }
                else
                {

                    return false;
                }
            }
            else
            {

                return false;
            }
        }
        #endregion

        #region Pausa
        private static void Pausa(int Tempo)
        {
            System.Threading.Thread.Sleep(Tempo);
            Application.DoEvents();
        }
        #endregion

        #region BioLight
        //***********************************************************************************
        //Retorna o modelo BioLight
        //***********************************************************************************
        private static bool BioLight(FrmOnline UiMainBIO)
        {
            bool placaLight = false;
            int Ret1 = -1;
            int modelo = 0;

            CSconf_catraca cscatraca = new CSconf_catraca();

            //Solicita Modelo
            Ret1 = EasyInner.SolicitarModeloBio(cscatraca.def_NumInner);
            SetarTimeoutBio();
            //UiMainBIO.tssl2.Text = "";
            do
            {
                if (Ret1 == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Thread.Sleep(5);

                    //Recebe Modelo
                    Ret1 = EasyInner.ReceberModeloBio(cscatraca.def_NumInner, 0, ref modelo);
                    if (Ret1 == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        //UiMainBIO.tssl2.RightToLeft = RightToLeft.No;
                        switch (modelo)
                        {
                            case 2:
                                //UiMainBIO.tssl2.Text = "Modelo do bio: Light usuários  := FIM10).";
                                placaLight = true;
                                break;
                            case 4: //UiMainBIO.tssl2.Text = "Modelo do bio: 1000/4000 usuários  := FIM01).";
                                break;
                            case 51: //UiMainBIO.tssl2.Text = "Modelo do bio: 1000/4000 usuários  := FIM2030).";
                                break;
                            case 52: //UiMainBIO.tssl2.Text = "Modelo do bio: 1000/4000 usuários  := FIM2040).";
                                break;
                            case 48:
                                //UiMainBIO.tssl2.Text = "Modelo do bio: Light 100 usuários  := FIM3030).";
                                placaLight = true;
                                break;
                            case 64:
                                //UiMainBIO.tssl2.Text = "Modelo do bio: Light 100 usuários  := FIM3040).";
                                placaLight = true;
                                break;
                            case 80: //UiMainBIO.tssl2.Text = "Modelo do bio: 1000/4000 usuários FIM5060.";
                                break;
                            case 82: //UiMainBIO.tssl2.Text = "Modelo do bio: 1000/4000 usuários FIM5260.";
                                break;
                            case 83:
                                //UiMainBIO.tssl2.Text = "Modelo do bio: Light 100 usuários FIM5360.";
                                placaLight = true;
                                break;
                            case 255: //UiMainBIO.tssl2.Text = "Modelo do bio: Desconhecido";
                                break;
                        }
                    }
                }
            } while (EsperaRespostaBio(Ret1));
            return placaLight;
        }
        #endregion

        #region SetarTimeoutBio
        private static void SetarTimeoutBio()
        {
            UiBIO1.Timeout1 = 0;
            UiBIO1.Timeout1 = (int)EasyInner.RetornarSegundosSys() + 7;
        }
        #endregion

        #region EsperaRespostaBio
        private static bool EsperaRespostaBio(int Ret)
        {
            Pausa(1);
            return ((Ret != (int)Enumeradores.Retorno.RET_COMANDO_OK) && ((int)EasyInner.RetornarSegundosSys() <= UiBIO1.Timeout1));
        }
        #endregion

        #region CompletaString
        //***********************************************************************************
        //Completa string de acordo com os parâmetros enviados
        //***********************************************************************************
        private static String CompletaString(String var1, int Len, String complemento)
        {
            while (var1.ToString().Length < Len)
            {
                var1 = complemento + var1;
            }
            return (var1);
        }
        #endregion

        #region StringOfChar
        private static String StringOfChar(String Ch, int Count)
        {
            int i = 0;
            String Retorno = "";

            for (i = 0; i <= Count - 1; i++)
            {
                Retorno = Retorno + Ch;
            }

            return (Retorno);
        }
        #endregion

        private void FRdig_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        #endregion

        public void verifica_campo()
        {
            SqlQuery sql_ver = new SqlQuery(Conexoes.GetSqlConnection());

            sql_ver.Command.Parameters.Clear();
            sql_ver.Command.CommandText =
                @"SELECT * FROM DIG_A_ENVIAR";

            sql_ver.Connection.Open();

            SqlDataReader reader = sql_ver.Command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    dig_1 = reader["DIG_1"].ToString();
                    dig_2 = reader["DIG_2"].ToString();
                }
                reader.Close();

            }
            catch
            {

            }
            finally
            {
                sql_ver.Connection.Close();

            }

        }

        public void envio()
        {
            if (dig_1 != "")
            {
                dig_ = dig_1;
                dig_1 = "";
            }
            else
            {
                dig_ = dig_2;
                dig_2 = "";
            }


            //enviar_digital();


        }

        //public void enviar_digital()
        //{

        //    CSconf_catraca cscatraca = new CSconf_catraca();

        //    bool placaLight = false;
        //    int Ret = -1;

        //    UiBIO1 = UiMainBIO1;

        //    try
        //    {
        //        SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

        //        sql_bd.Command.Parameters.Clear();
        //        sql_bd.Command.CommandText =
        //            @"SELECT * FROM ACESSO_CATRACA WHERE ID_CARTAO=@id_cartao";

        //        //sql_bd.Command.Parameters.AddWithValue("@id_cartao", CScad_digital.cad_card);

        //        sql_bd.Command.Parameters.AddWithValue("@id_cartao", dig_);


        //        sql_bd.Connection.Open();
        //        sql_bd.Fill();
        //        //UiMainBIO.Cursor = Cursors.WaitCursor;

        //        //Mensagem de Status                    
        //        //UiMainBIO.tssl2.Text = "";
        //        //UiMainBIO.tssl2.Text = "Iniciando comunicação...";
        //        //UiMainBIO.tssl2.RightToLeft = RightToLeft.No;
        //        Application.DoEvents();

        //        int iJaCadast = 0;
        //        int iOk = 0;
        //        int iFalha = 0;

        //        //Se conectar Bio
        //        if (Conectar(UiMainBIO1))
        //        {
        //            //UiMainBIO.tssl2.Text = "";
        //            //UiMainBIO.tssl2.Text = "Verificando tipo da placa FIM...";
        //            //UiMainBIO.tssl2.RightToLeft = RightToLeft.No;

        //            placaLight = BioLight(UiMainBIO1);

        //            //Se a base estiver vazia retorna
        //            if (sql_bd.Table.Rows.Count == 0)
        //            {
        //                sql_bd.Table.Dispose();
        //                sql_bd.Table = null;
        //                sql_bd.Connection.Dispose();
        //                return;
        //            }
        //            else
        //            {

        //                //Senão consulta os usuários cadastrados
        //                int i = 0;
        //                int j = 0;
        //                int k = 0;
        //                int x = 0;
        //                string TemplateA = "";
        //                string TemplateB = "";
        //                string cartao = "";
        //                byte[] linha;
        //                byte[] tempConv;
        //                Nitgen objNitgen;

        //                //Percorre todos os usuários cadastrados
        //                for (x = 0; x <= sql_bd.Table.Rows.Count - 1; x++)
        //                {
        //                    cartao = sql_bd.Table.Rows[x][0].ToString();
        //                    TemplateA = sql_bd.Table.Rows[x][2].ToString();
        //                    TemplateB = sql_bd.Table.Rows[x][3].ToString();
        //                    linha = new byte[844];
        //                    tempConv = new byte[404];

        //                    //limpa linha
        //                    for (i = 0; i <= 843; i++)
        //                    {
        //                        linha[i] = 0;
        //                    }

        //                    //Template A
        //                    i = 1;
        //                    if (!placaLight)
        //                    {

        //                        //Rotinas que interpretam a digital cadastrada

        //                        cartao = CompletaString(cartao, 10, "0");

        //                        for (j = 0; j < cartao.Length; j++)
        //                        {
        //                            linha[i] = Convert.ToByte(Convert.ToInt32(cartao.Substring(j, 1)) + 48);
        //                            i++;
        //                        }

        //                        i = 28;
        //                        k = 0;

        //                        for (j = 0; j < 807; j += 2)
        //                        {
        //                            tempConv[k] = Convert.ToByte(Convert.ToUInt32(TemplateA.Substring(j, 2), 16));
        //                            k++;
        //                        }

        //                        for (j = 0; j <= 403; j++)
        //                        {
        //                            linha[i] = tempConv[j];
        //                            i++;
        //                        }

        //                    } //placa light
        //                    else
        //                    {
        //                        //Rotinas que interpretam a digital cadastrada

        //                        cartao = CompletaString(cartao, 8, "0");

        //                        for (j = 0; j < cartao.Length; j++)
        //                        {
        //                            linha[i] = Convert.ToByte(Convert.ToInt32(cartao.Substring(j, 1)) + 48);
        //                            i++;
        //                        }

        //                        i = 27;
        //                        k = 0;

        //                        for (j = 0; j < 807; j += 2)
        //                        {
        //                            tempConv[k] = Convert.ToByte(Convert.ToUInt32(TemplateA.Substring(j, 2), 16));
        //                            k++;
        //                        }

        //                        objNitgen = new Nitgen();
        //                        objNitgen.biFIR = tempConv;
        //                        objNitgen.objFPData.Import(1, 0, 1, 7, 404, tempConv, null);
        //                        objNitgen.objFPData.Export(objNitgen.objFPData.FIR, 6);

        //                        foreach (byte b in (byte[])objNitgen.objFPData.get_FPData(objNitgen.objFPData.get_FingerID(0), 0))
        //                        {
        //                            linha[i] = b;
        //                            i++;
        //                        }

        //                        objNitgen = null;
        //                    }

        //                    //Template B
        //                    if (!placaLight)
        //                    {
        //                        i = 432;
        //                        k = 0;

        //                        //Rotinas que interpretam a digital cadastrada

        //                        for (j = 0; j <= 403; j++)
        //                        {
        //                            tempConv[j] = 0;
        //                        }

        //                        for (j = 0; j < 807; j += 2)
        //                        {
        //                            tempConv[k] = Convert.ToByte(Convert.ToUInt32(TemplateB.Substring(j, 2), 16));
        //                            k++;
        //                        }

        //                        for (j = 0; j <= 403; j++)
        //                        {
        //                            linha[i] = tempConv[j];
        //                            i++;
        //                        }

        //                    } //placa light
        //                    else
        //                    {
        //                        i = 427;
        //                        k = 0;

        //                        //Rotinas que interpretam a digital cadastrada
        //                        for (j = 0; j <= tempConv.Length - 1; j++)
        //                        {
        //                            tempConv[j] = 0;
        //                        }

        //                        for (j = 0; j < 807; j += 2)
        //                        {
        //                            tempConv[k] = Convert.ToByte(Convert.ToUInt32(TemplateB.Substring(j, 2), 16));
        //                            k++;
        //                        }

        //                        objNitgen = new Nitgen();
        //                        objNitgen.objFPData.Import(1, 0, 1, 7, 404, tempConv, null);
        //                        objNitgen.objFPData.Export(objNitgen.objFPData.FIR, 6);

        //                        foreach (byte b in (byte[])objNitgen.objFPData.get_FPData(objNitgen.objFPData.get_FingerID(0), 0))
        //                        {
        //                            linha[i] = b;
        //                            i++;
        //                        }
        //                    }

        //                    if (!placaLight)
        //                    {
        //                        linha[836] = Convert.ToByte(System.Convert.ToInt32(System.DateTime.Now.Year / 100));
        //                        linha[837] = Convert.ToByte(System.DateTime.Now.Year % 100);
        //                        linha[838] = Convert.ToByte(System.DateTime.Now.Month);
        //                        linha[839] = Convert.ToByte(System.DateTime.Now.Day);
        //                        linha[840] = Convert.ToByte(System.DateTime.Now.Hour);
        //                        linha[841] = Convert.ToByte(System.DateTime.Now.Minute);
        //                        linha[842] = Convert.ToByte(System.DateTime.Now.Second);
        //                        linha[843] = 0;
        //                    }
        //                    else
        //                    {
        //                        EasyInner.SetarBioLight(1);
        //                    }

        //                    //Envio do template para cadastro no Inner Bio
        //                    Ret = (byte)EasyInner.EnviarUsuarioBio(cscatraca.def_NumInner, linha);
        //                    if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
        //                    {

        //                        //Verifica se o template foi cadastrado com sucesso
        //                        Ret = (byte)EasyInner.UsuarioFoiEnviado(cscatraca.def_NumInner, 0);

        //                        if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
        //                            iOk++;
        //                        else if (Ret == (byte)Enumeradores.Retorno.RET_BIO_USR_JA_CADASTRADO)
        //                            iJaCadast++;
        //                        else if (Ret != (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO)
        //                            iFalha++;
        //                    }
        //                }
        //            }

        //            //Mensagens de Status
        //            //UiMainBIO.tssl2.RightToLeft = RightToLeft.Yes;

        //            //string s = ("ENVIADOS: " + iOk) + (", JÁ CADASTRADOS: " + iJaCadast) + (", FALHARAM : " + iFalha) + "!";


        //            //if (iOk == 1)
        //            //{
        //            //    UiMainBIO.tssl2.Text = "Digital enviada!";
        //            //}
        //            //if (iJaCadast == 1)
        //            //{
        //            //    UiMainBIO.tssl2.Text = "Digital já existe!";
        //            //}
        //            //if (iFalha == 1)
        //            //{
        //            //    UiMainBIO.tssl2.Text = "Digital não enviada!";
        //            //}


        //            //Fecha Porta de Comunicação
        //            EasyInner.FecharPortaComunicacao();
        //        }

        //        //UiMainBIO.CarregaGrid();



        //        UiMainBIO1.Cursor = Cursors.Default;

        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {

        //    }
        //}

        public void exclui_dig_enviada()
        {
            SqlQuery sql_ver = new SqlQuery(Conexoes.GetSqlConnection());

            sql_ver.Command.Parameters.Clear();
            sql_ver.Command.CommandText =
                @"DELETE FROM DIG_A_ENVIAR";

            sql_ver.Connection.Open();

            try
            {
                sql_ver.Command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sql_ver.Connection.Close();

            }
        }       
        
        public void get_digs()
        {
            lista_dig.Clear();
            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            sql_bd.Command.Parameters.Clear();
            sql_bd.Command.CommandText =
                @"SELECT ID_CARTAO, TEMPLATE1 FROM ACESSO_CATRACA";

            sql_bd.Connection.Open();

            SqlDataReader reader = sql_bd.Command.ExecuteReader();

            while(reader.Read())
            {
                if(reader["TEMPLATE1"].ToString() != string.Empty)
                {
                    lista_dig.Add(reader["ID_CARTAO"].ToString());
                }
            }

            reader.Close();
            sql_bd.Connection.Close();
            

            
        }

        //Em uso.        

        public void manutencao_dig()
        {
            
            CSdig_lista.usuarios_catraca.Clear();
            CSreceber_dig_catraca.ReceberUsuariosBIO_PC(this);
            comparar();
            excluir_digital();
            enviar_digital();
            
            //E COMPARA LISTA DE DIG A ENVIAR COM LISTA DE USERS CATRACA, CASO COTENHA, ALTERA BD PARA ENVIADO
            atualizar_bd();
            lbEnviando.Text = "Envio Concluido";
            
        }

        public void comparar()
        {
            string erro_id = "";
            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            sql_bd.Command.Parameters.Clear();
            sql_bd.Command.CommandText =
                @"SELECT * FROM ACESSO_CATRACA";

            sql_bd.Connection.Open();

            SqlDataReader reader = sql_bd.Command.ExecuteReader();

            try
            {
                lista_dig_cadastrar.Clear();

                while (reader.Read())
                {   //ATIVO ?
                    if (reader["ATIVO"] != DBNull.Value)
                    {
                        string a = (Convert.ToInt32(reader["ID_CARTAO"])).ToString("D10");
                        erro_id = a;
                        if (reader["ATIVO"].ToString() == "1")
                        {   //NÃO ENVIADO ?
                            if (reader["ENVIADO"].ToString() == "0")
                            {   //ADD PARA CADASTRAR    
                                lista_dig_cadastrar.Add(a);
                                //TEM NA CATRACA ? AQUI ACONTECE UMA "ATUALIZAÇÃO DE DIGITAIS" ELA ESTA NO BD COMO "ENVIADO=0", POREM CONSTA NA CATRACA
                                //PORTANTO DEVE SER EXCLUIDA PELO "IF" ABAIXO ANTES DE SER ENVIADA A CATRACA.
                                if (CSdig_lista.usuarios_catraca.Contains(a))
                                {                                    
                                    lista_dig_excluir.Add(Convert.ToInt32(a)); //SE SIM, EXCLUI DA CATRACA PARA ENVIAR NOVAMENTE.
                                }
                            }
                        }
                        else
                        {
                            if (CSdig_lista.usuarios_catraca.Contains(a))
                            {
                                lista_dig_excluir.Add(Convert.ToInt32(a)); //SE SIM, EXCLUI DA CATRACA PARA ENVIAR NOVAMENTE.
                            } //EXCLUI DA CATRACA DIGITAIS NÃO ATIVAS.
                        }
                    }
                }
                //MessageBox.Show("enviar " + lista_dig_cadastrar.Count);
            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
                ltbErros.Items.Add(erro_id);
                //MessageBox.Show(ex.Message);
                //MessageBox.Show(erro_id);
            }
            finally
            {
                sql_bd.Command.Parameters.Clear();
                sql_bd.Connection.Close();
            }

        }

        public void excluir_digital()
        {
            String Usuario = "";
            int Ret = -1;
            UiBIO1 = UiMainBIO1;

            if (Conectar(UiMainBIO1))
            {
                for (int z = 0; z < lista_dig_excluir.Count; z++)
                {

                    //Mensagem de Status

                    UiMainBIO1.lblManutencao.Text = "Excluindo Usuário " + lista_dig_excluir[z].ToString();

                    Application.DoEvents();

                    //Seta modelo da placa

                    bool placalight = BioLight(UiMainBIO1);

                    //Verifica se é light
                    if (placalight)
                        //Caso sim
                        Usuario = StringOfChar("0", 8 - lista_dig_excluir[z].ToString().Length) + lista_dig_excluir[z].ToString() + StringOfChar("0", 8);
                    else
                        //Senão
                        Usuario = StringOfChar("0", 10 - (lista_dig_excluir[z].ToString().Length)) + lista_dig_excluir[z].ToString();

                    //Solicita para o Inner bio excluir o cadastro do usuário desejado.
                    Ret = EasyInner.SolicitarExclusaoUsuario(1, Usuario);

                    SetarTimeoutBio();
                    do
                    {
                        Thread.Sleep(20);
                        if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                        {
                            Thread.Sleep(10);
                            SetarTimeoutBio();
                            do
                            {
                                //Verifica se foi excluído
                                Ret = EasyInner.UsuarioFoiExcluido(1, 0);
                                if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                                {
                                    //Caso sim

                                    lblManu2.Text = "Usuário " + lista_dig_excluir[z].ToString() + " excluído com sucesso!";
                                    break;
                                }
                                else if (Ret == (byte)Enumeradores.Retorno.RET_USU_NAO_CADAST)
                                {
                                    //Senão
                                    //MessageBox.Show("Usuário não cadastrado!");
                                    break;
                                }
                            } while (EsperaRespostaBio(Ret));
                            break;
                        }
                    } while (EsperaRespostaBio(Ret));
                }

                //Fecha porta de comunicação
                EasyInner.FecharPortaComunicacao();

                UiMainBIO1.lblManutencao.Text = "";
                lblManu2.Text = "";


            }
                
           
        }

        public void enviar_digital()
        {
            Cursor.Current = Cursors.WaitCursor;

            continua = 1;
            string a = "";

            CSconf_catraca cscatraca = new CSconf_catraca();
                
            bool placaLight = false;
            int Ret = -1;

            //Contadores
            int erros_enviar = 0;
            int sucesso_enviar = 0;

            UiBIO1 = UiMainBIO1;

            if (Conectar(UiMainBIO1))
            {
                #region Inicio Conectar

                for (int z = 0; z < lista_dig_cadastrar.Count; z++)
                {
                    listBox1.Items.Add("Status: Inicio for - " +lista_dig_cadastrar[z]+ " - " + DateTime.Now);
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    enviado = 0;
                    
                    if (continua == 1)
                    {
                        try
                        {
                            listBox1.Items.Add("Status: Banco - " + DateTime.Now);
                            a = lista_dig_cadastrar[z];
                            lbEnviando.Text = "Enviando: " + lista_dig_cadastrar[z];
                            
                            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());
                            
                            sql_bd.Command.Parameters.Clear();
                            sql_bd.Command.CommandText =
                                @"SELECT * FROM ACESSO_CATRACA WHERE ID_CARTAO=@id_cartao";
                            
                            //sql_bd.Command.Parameters.AddWithValue("@id_cartao", CScad_digital.cad_card);
                            
                            sql_bd.Command.Parameters.AddWithValue("@id_cartao", Convert.ToInt32(lista_dig_cadastrar[z]));
                            listBox1.Items.Add("Status: COnvertido - " + Convert.ToInt32(lista_dig_cadastrar[z]) + " - " + DateTime.Now);
                            
                            sql_bd.Connection.Open();
                            sql_bd.Fill();
                            
                            //Mensagem de Status 
                            //UiMainBIO.tssl2.Text = "";
                            //UiMainBIO.tssl2.Text = "Iniciando comunicação...";
                            //UiMainBIO.tssl2.RightToLeft = RightToLeft.No;
                            Application.DoEvents();
                            
                            int iJaCadast = 0;
                            int iOk = 0;
                            int iFalha = 0;
                            
                            listBox1.Items.Add("Status: Conectar - " + DateTime.Now);
                            
                            //Se conectar Bio
                        
                            //UiMainBIO.tssl2.Text = "";
                            //UiMainBIO.tssl2.Text = "Verificando tipo da placa FIM...";
                            //UiMainBIO.tssl2.RightToLeft = RightToLeft.No;

                            placaLight = BioLight(UiMainBIO1);

                            //Se a base estiver vazia retorna
                            if (sql_bd.Table.Rows.Count == 0)
                            {
                                listBox1.Items.Add("Status: Base Vazia - " + DateTime.Now);
                                sql_bd.Table.Dispose();
                                sql_bd.Table = null;
                                sql_bd.Connection.Dispose();
                                return;
                            }
                            else
                            {
                                //Senão consulta os usuários cadastrados
                                int i = 0;
                                int j = 0;
                                int k = 0;
                                int x = 0;
                                string TemplateA = "";
                                string TemplateB = "";
                                string cartao = "";
                                byte[] linha;
                                byte[] tempConv;
                                Nitgen objNitgen;

                                //Percorre todos os usuários cadastrados
                                for (x = 0; x <= sql_bd.Table.Rows.Count - 1; x++)
                                {
                                    cartao = sql_bd.Table.Rows[x][0].ToString();
                                    label4.Text = sql_bd.Table.Rows[x][0].ToString();
                                    TemplateA = sql_bd.Table.Rows[x][2].ToString();
                                    label5.Text= sql_bd.Table.Rows[x][2].ToString();
                                    TemplateB = sql_bd.Table.Rows[x][3].ToString();
                                    label6.Text = sql_bd.Table.Rows[x][3].ToString();
                                    linha = new byte[844];
                                    tempConv = new byte[404];

                                    //limpa linha
                                    for (i = 0; i <= 843; i++)
                                    {
                                        linha[i] = 0;
                                    }
                                    
                                    listBox1.Items.Add("Status: Template A - " + DateTime.Now);
                                    #region Template A
                                    //Template A
                                    i = 1;
                                    if (!placaLight)
                                    {
                                        label7.Text = "Não Light";

                                        //Rotinas que interpretam a digital cadastrada

                                        cartao = CompletaString(cartao, 10, "0");

                                        for (j = 0; j < cartao.Length; j++)
                                        {
                                            linha[i] = Convert.ToByte(Convert.ToInt32(cartao.Substring(j, 1)) + 48);
                                            i++;
                                        }

                                        i = 28;
                                        k = 0;

                                        for (j = 0; j < 807; j += 2)
                                        {
                                            tempConv[k] = Convert.ToByte(Convert.ToUInt32(TemplateA.Substring(j, 2), 16));
                                            k++;
                                        }

                                        for (j = 0; j <= 403; j++)
                                        {
                                            linha[i] = tempConv[j];
                                            i++;
                                        }

                                    } //placa light
                                    else
                                    {
                                        label7.Text = "Light";
                                        //Rotinas que interpretam a digital cadastrada

                                        cartao = CompletaString(cartao, 8, "0");

                                        for (j = 0; j < cartao.Length; j++)
                                        {
                                            linha[i] = Convert.ToByte(Convert.ToInt32(cartao.Substring(j, 1)) + 48);
                                            i++;
                                        }

                                        i = 27;
                                        k = 0;

                                        for (j = 0; j < 807; j += 2)
                                        {
                                            tempConv[k] = Convert.ToByte(Convert.ToUInt32(TemplateA.Substring(j, 2), 16));
                                            k++;
                                        }

                                        objNitgen = new Nitgen();
                                        objNitgen.biFIR = tempConv;
                                        objNitgen.objFPData.Import(1, 0, 1, 7, 404, tempConv, null);
                                        objNitgen.objFPData.Export(objNitgen.objFPData.FIR, 6);

                                        foreach (byte b in (byte[])objNitgen.objFPData.get_FPData(objNitgen.objFPData.get_FingerID(0), 0))
                                        {
                                            linha[i] = b;
                                            i++;
                                        }

                                        objNitgen = null;
                                    }

                                    #endregion
                                    
                                    listBox1.Items.Add("Status: Template B - " + DateTime.Now);
                                    #region Template B
                                    //Template B
                                    if (!placaLight)
                                    {
                                        i = 432;
                                        k = 0;

                                        //Rotinas que interpretam a digital cadastrada

                                        for (j = 0; j <= 403; j++)
                                        {
                                            tempConv[j] = 0;
                                        }

                                        for (j = 0; j < 807; j += 2)
                                        {
                                            tempConv[k] = Convert.ToByte(Convert.ToUInt32(TemplateB.Substring(j, 2), 16));
                                            k++;
                                        }

                                        for (j = 0; j <= 403; j++)
                                        {
                                            linha[i] = tempConv[j];
                                            i++;
                                        }

                                    } //placa light
                                    else
                                    {
                                        i = 427;
                                        k = 0;

                                        //Rotinas que interpretam a digital cadastrada
                                        for (j = 0; j <= tempConv.Length - 1; j++)
                                        {
                                            tempConv[j] = 0;
                                        }

                                        for (j = 0; j < 807; j += 2)
                                        {
                                            tempConv[k] = Convert.ToByte(Convert.ToUInt32(TemplateB.Substring(j, 2), 16));
                                            k++;
                                        }

                                        objNitgen = new Nitgen();
                                        objNitgen.objFPData.Import(1, 0, 1, 7, 404, tempConv, null);
                                        objNitgen.objFPData.Export(objNitgen.objFPData.FIR, 6);

                                        foreach (byte b in (byte[])objNitgen.objFPData.get_FPData(objNitgen.objFPData.get_FingerID(0), 0))
                                        {
                                            linha[i] = b;
                                            i++;
                                        }
                                    }
                                    #endregion

                                    if (!placaLight)
                                    {
                                        linha[836] = Convert.ToByte(System.Convert.ToInt32(System.DateTime.Now.Year / 100));
                                        linha[837] = Convert.ToByte(System.DateTime.Now.Year % 100);
                                        linha[838] = Convert.ToByte(System.DateTime.Now.Month);
                                        linha[839] = Convert.ToByte(System.DateTime.Now.Day);
                                        linha[840] = Convert.ToByte(System.DateTime.Now.Hour);
                                        linha[841] = Convert.ToByte(System.DateTime.Now.Minute);
                                        linha[842] = Convert.ToByte(System.DateTime.Now.Second);
                                        linha[843] = 0;
                                    }
                                    else
                                    {
                                        EasyInner.SetarBioLight(1);
                                    }

                                    //Envio do template para cadastro no Inner Bio
                                    listBox1.Items.Add("Status: Enviando - " + DateTime.Now);
                                    


                                    Ret = (byte)EasyInner.EnviarUsuarioBio(cscatraca.def_NumInner, linha);
                                    if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                                    {
                                        //Verifica se o template foi cadastrado com sucesso
                                        Ret = (byte)EasyInner.UsuarioFoiEnviado(cscatraca.def_NumInner, 0);

                                        if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                                        {
                                            iOk++;
                                            ltbEnviados.Items.Add(lista_dig_cadastrar[z]);
                                            lblQtdenviados.Text = (sucesso_enviar = sucesso_enviar + 1).ToString();
                                            enviado = 1;
                                            lista_dig_cadastro_ok.Add(lista_dig_cadastrar[z]);
                                            listBox1.Items.Add("Status: Template Enviada - " + DateTime.Now);
                                        }

                                        else if (Ret == (byte)Enumeradores.Retorno.RET_BIO_USR_JA_CADASTRADO)
                                        {
                                            iJaCadast++;
                                            listBox1.Items.Add("Status: Template Já Cadsatrada - " + DateTime.Now);
                                        }
                                        else if (Ret != (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO)
                                        {
                                            iFalha++;
                                            listBox1.Items.Add("Status: Falha no envio - " + DateTime.Now);
                                        }
                                    }
                                //Mensagens de Status
                                //UiMainBIO.tssl2.RightToLeft = RightToLeft.Yes;
                                
                                //string s = ("ENVIADOS: " + iOk) + (", JÁ CADASTRADOS: " + iJaCadast) + (", FALHARAM : " + iFalha) + "!";
                                
                                
                                //if (iOk == 1)
                                //{
                                //    UiMainBIO.tssl2.Text = "Digital enviada!";
                                //}
                                //if (iJaCadast == 1)
                                //{
                                //    UiMainBIO.tssl2.Text = "Digital já existe!";
                                //}
                                //if (iFalha == 1)
                                //{
                                //    UiMainBIO.tssl2.Text = "Digital não enviada!";
                                //}
                                
                                //Fecha Porta de Comunicação
                                
                                } //Final for                                
                            } //Final Else
                        } //Final Try
                        catch (Exception ex)
                        {
                            Error_Log.csControle_erros.exibir_erro(ex.Message);
                            ltbErros.Items.Add(a);
                            lblQtderros.Text = (erros_enviar + 1).ToString();
                            enviado = 0;
                        }
                        finally
                        {

                        }
                    }
                    else
                    { 
                        break;
                    }

                    watch.Stop();
                    
                    long elapsedTime = watch.ElapsedMilliseconds;
                    string s = "Tempo restante:" + (elapsedTime / 1000*(lista_dig_cadastrar.Count-z))/60 + " minutos";
                    
                    if(enviado == 1)
                    {
                        lblEstimado.Text = s;
                    }
                    
                    listBox1.Items.Add("Status: Fim for - " + lista_dig_cadastrar[z] + " - " + DateTime.Now);
                }

                #endregion FIm Conectar

                EasyInner.FecharPortaComunicacao();
                Thread.Sleep(2000);
            }
            else
            {
                listBox1.Items.Add("Status: Não conectado - " + DateTime.Now);
            }
            
            //UiMainBIO.CarregaGrid();

            Cursor.Current = Cursors.Default;
        }

        public void atualizar_bd()
        {
            for (int z = 0; z < lista_dig_cadastro_ok.Count; z++)
            {
                SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

                sql_bd.Command.CommandText = @"UPDATE ACESSO_CATRACA SET ENVIADO=@enviado WHERE ID_CARTAO=@id_cartao";

                sql_bd.Command.Parameters.AddWithValue("@id_cartao", System.Convert.ToInt64(lista_dig_cadastro_ok[z]));

                sql_bd.Command.Parameters.AddWithValue("@enviado", 1);

                try
                {
                    if (sql_bd.Connection.State != ConnectionState.Closed)
                    {
                        sql_bd.Connection.Close();
                    }
                    sql_bd.Connection.Open();

                    //Executa a instrução SQL
                    sql_bd.Command.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    //Fecha a conexão 
                    sql_bd.Command.Parameters.Clear();
                    sql_bd.Connection.Close();
                }
            }
        }

        public void sair_da_escola()
        {
            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            sql_bd.Command.CommandText = @"UPDATE ACESSO_CATRACA SET ESTADO = 0";
            
            try
            {
                if (sql_bd.Connection.State != ConnectionState.Closed)
                {
                    sql_bd.Connection.Close();
                }
                sql_bd.Connection.Open();

                //Executa a instrução SQL
                sql_bd.Command.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                //Fecha a conexão 
                sql_bd.Command.Parameters.Clear();
                sql_bd.Connection.Close();
            }
        }        

        private void btnReceber_dig_Click(object sender, EventArgs e)
        {

            ltbEnviados.Items.Clear();
            ltbEnviar.Items.Clear();
            ltbErros.Items.Clear();
            ltbUsuarioscatraca.Items.Clear();
            


            Cursor.Current = Cursors.WaitCursor;
            CSdig_lista.usuarios_catraca.Clear();
            CSreceber_dig_catraca.ReceberUsuariosBIO_PC(this);
            ltbUsuarioscatraca.DataSource = CSdig_lista.usuarios_catraca;            
            comparar();
            ltbEnviar.DataSource = lista_dig_cadastrar;            
            lblQtdcatraca.Text = ltbUsuarioscatraca.Items.Count.ToString();
            lblQtdenviar.Text = ltbEnviar.Items.Count.ToString();
            btnEnviar_dig.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnEnviar_dig_Click(object sender, EventArgs e)
        {
            
            btnReceber_dig.Enabled = false;
            btnEnviar_dig.Enabled = false;
            btnParar.Enabled = true;
            excluir_digital();
            enviar_digital();
            atualizar_bd();
            btnReceber_dig.Enabled = true;
            
        }        

        private void btnParar_Click(object sender, EventArgs e)
        {
            continua = 0;
            
        }

        private void btnManutenção_Click(object sender, EventArgs e)
        {
            adquirir_numat_e_data();

            ////CSdig_lista.usuarios_catraca.Clear();
            ////CSreceber_dig_catraca.ReceberUsuariosBIO_PC(this);

            ////List<string> lista_manutencao = new List<string>();
            ////List<DateTime> lista_manutencao2 = new List<DateTime>();

            //#region SELECT

            ////SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            ////sql_bd.Command.Parameters.Clear();
            ////sql_bd.Command.CommandText =
            ////    @"SELECT * FROM ACESSO_CATRACA WHERE TEMPLATE1 IS NOT NULL";

            ////sql_bd.Connection.Open();

            ////SqlDataReader reader = sql_bd.Command.ExecuteReader();

            ////MessageBox.Show(CSdig_lista.usuarios_catraca.Count.ToString());

            ////try
            ////{
                
            ////    while (reader.Read())
            ////    {
            ////        ////if (reader["TEMPLATE1"].ToString() != string.Empty)
            ////        ////{
                        
            ////        //    //if (reader["ENVIADO"].ToString() == "1")
            ////        //    //{

            ////        //        if (!CSdig_lista.usuarios_catraca.Contains((Convert.ToInt32(reader["ID_CARTAO"])).ToString("D10")))
            ////                //{
            ////                    lista_manutencao.Add(reader["ID_CARTAO"].ToString());
            ////                //}                           
            ////            //}
                        
            ////        //}
            ////    }
            ////    MessageBox.Show(lista_manutencao.Count.ToString());

            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show(ex.Message);
            ////}
            ////finally
            ////{
            ////    sql_bd.Command.Parameters.Clear();
            ////    sql_bd.Connection.Close();
            ////}

            //#endregion

            //#region UPDATE

            ////for (int z = 0; z < lista_manutencao.Count; z++)
            ////{
            //SqlQuery sql_bd1 = new SqlQuery(Conexoes.GetSqlConnection());


            //sql_bd1.Command.CommandText = @"UPDATE ACESSO_CATRACA SET ULTIMO_ACESSO = NULL WHERE TEMPLATE1 IS NOT NULL";

            ////sql_bd1.Command.Parameters.AddWithValue("@id_cartao", System.Convert.ToInt64(lista_manutencao[z]));
            ////sql_bd1.Command.Parameters.AddWithValue("@ultimo", 0);
            ////sql_bd1.Command.Parameters.AddWithValue("@ativo", 1);



            //try
            //{
            //    if (sql_bd1.Connection.State != ConnectionState.Closed)
            //    {
            //        sql_bd1.Connection.Close();
            //    }
            //    sql_bd1.Connection.Open();

            //    //Executa a instrução SQL
            //    sql_bd1.Command.ExecuteNonQuery();

            //}
            //catch (SqlException ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}
            //finally
            //{
            //    //Fecha a conexão 
            //    sql_bd1.Command.Parameters.Clear();
            //    sql_bd1.Connection.Close();

            //}
            ////}

            //#endregion

            //#region UPDATE

            ////for (int z = 0; z < CSdig_lista.usuarios_catraca.Count; z++)
            ////{
            ////    SqlQuery sql_bd1 = new SqlQuery(Conexoes.GetSqlConnection());


            ////    sql_bd1.Command.CommandText = @"UPDATE ACESSO_CATRACA SET ENVIADO=@enviado, ATIVO=@ativo WHERE ID_CARTAO=@id_cartao";

            ////    sql_bd1.Command.Parameters.AddWithValue("@id_cartao", System.Convert.ToInt64(CSdig_lista.usuarios_catraca[z]));
            ////    sql_bd1.Command.Parameters.AddWithValue("@enviado", 1);
            ////    sql_bd1.Command.Parameters.AddWithValue("@ativo", 1);            
                

            ////    try
            ////    {
            ////        if (sql_bd1.Connection.State != ConnectionState.Closed)
            ////        {
            ////            sql_bd1.Connection.Close();
            ////        }
            ////        sql_bd1.Connection.Open();

            ////        //Executa a instrução SQL
            ////        sql_bd1.Command.ExecuteNonQuery();

            ////    }
            ////    catch (SqlException ex)
            ////    {
            ////        MessageBox.Show("Error: " + ex.Message);
            ////    }
            ////    finally
            ////    {
            ////        //Fecha a conexão 
            ////        sql_bd1.Command.Parameters.Clear();
            ////        sql_bd1.Connection.Close();

            ////    }
            ////}

            //#endregion
        }

        private void ltbUsuarioscatraca_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (object row in ltbUsuarioscatraca.SelectedItems)
                    {
                        sb.Append(row.ToString());
                        sb.AppendLine();
                    }
                    sb.Remove(sb.Length - 1, 1); // Just to avoid copying last empty row
                    Clipboard.SetData(System.Windows.Forms.DataFormats.Text, sb.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnFora_digitais_Click(object sender, EventArgs e)
        {
            sair_da_escola();
        }

        private void adquirir_numat_e_data()
        {
            List<string> n_mat_list = new List<string>();

            List<string> dat_nasc_list = new List<string>();

            List<string> nv_id_cart_list = new List<string>();

            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

            sql_bd.Command.Parameters.Clear();
            sql_bd.Command.CommandText =
                @"SELECT N_MAT, DAT_NASC FROM ALUNOS";

            sql_bd.Connection.Open();

            SqlDataReader reader = sql_bd.Command.ExecuteReader();

            while (reader.Read())
            {
                n_mat_list.Add(reader["N_MAT"].ToString());
                dat_nasc_list.Add(DateTime.Parse(reader["DAT_NASC"].ToString()).ToString("yy"));
                nv_id_cart_list.Add(reader["N_MAT"].ToString() + DateTime.Parse(reader["DAT_NASC"].ToString()).ToString("yy"));
            }

            for (int z = 0; z < nv_id_cart_list.Count; z++)
            {

                SqlQuery sql_bd1 = new SqlQuery(Conexoes.GetSqlConnection());


                sql_bd1.Command.CommandText = @"UPDATE ACESSO_CATRACA SET ID_CARTAO=@id_cartao_1 WHERE ID_CARTAO=@id_cartao";

                sql_bd1.Command.Parameters.AddWithValue("@id_cartao", n_mat_list[z].ToString());
                sql_bd1.Command.Parameters.AddWithValue("@id_cartao_1", nv_id_cart_list[z].ToString());

                if (sql_bd1.Connection.State != ConnectionState.Closed)                    
                {
                    sql_bd1.Connection.Close();
                }
                sql_bd1.Connection.Open();

                //Executa a instrução SQL
                sql_bd1.Command.ExecuteNonQuery();

                //Fecha a conexão 
                sql_bd1.Command.Parameters.Clear();
                sql_bd1.Connection.Close();                
            }
            

            MessageBox.Show("Adquiridos " + n_mat_list.Count.ToString() + " Números e " + dat_nasc_list.Count.ToString() + " Datas.");

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

    }
}