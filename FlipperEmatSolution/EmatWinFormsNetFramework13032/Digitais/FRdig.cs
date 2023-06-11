using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Resources;


using EmatWinFormsNetFramework13032.Digitais.COM;
using EmatWinFormsNetFramework13032.Digitais.Entity;
using System.Threading;
//Referências Projeto

using EmatWinFormsNetFramework13032.Digitais.FrmOnline;

using NITGEN.SDK.NBioBSP;
using EmatWinFormsNetFramework13032.Properties;

namespace EmatWinFormsNetFramework13032.Digitais
{
    public partial class FRdig : Form
    {
        NBioAPI m_NBioAPI;

        NBioAPI.Type.WINDOW_OPTION m_WinOption; 

        public ComboBox list_dispositivos = new ComboBox();
        //public NBioAPI m_NBioAPI;
        public short m_OpenedDeviceID;
        public NBioAPI.Type.HFIR m_hNewFIR;
        public NBioAPI.Type.HFIR m_hNewFIR2;
        public NBioAPI.Type.FIR m_biFIR;
        public NBioAPI.Type.FIR_TEXTENCODE m_textFIR;
        
        public NBioAPI.Type.DEVICE_INFO_EX[] m_DeviceInfoEx;
        //public NBioAPI.Type.WINDOW_OPTION m_WinOption;

        string d1 = "";
        int cadastrado = 0;
        int excluido = 0;

        public int dig_qualidade;

        public string ReturnValue1;
        public string ReturnValue2;

        public FRdig()
        {
            InitializeComponent();

            m_NBioAPI = new NBioAPI();

            m_WinOption = new NBioAPI.Type.WINDOW_OPTION();
            m_WinOption.Option2 = new NBioAPI.Type.WINDOW_OPTION_2();
        }

        #region Propriedades

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
        private static FRdig _uiMainBIO;
        public static FRdig UiMainBIO
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

        #region DataConnection
        private OleDbConnection _dataConnection;
        public OleDbConnection DataConnection
        {
            get { return _dataConnection; }
            set { _dataConnection = value; }
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

        #region UiBIO
        private static FRdig _uiMainBio;
        public static FRdig UiBIO
        {
            get { return _uiMainBio; }
            set { _uiMainBio = value; }
        }
        #endregion        

        private void FRdig_Load(object sender, EventArgs e)
        {

            if (CScad_digital.usuario == 1)
            {
                lblNumat.Text = "Nº de Matrícula: " + CScad_digital.cad_card;
                lblNome.Text = "Nome: " + CScad_digital.nome_cad_card;
            }
            else
            {
                lblNumat.Text = "Nº de Matrícula: " + CScad_digital.cad_card;
                lblNome.Text = "Nome: " + CScad_digital.nome_cad_card;
            }


            if (Digitais.CScad_digital.existente == "sim")
            {
                object digi_imagem = Resources.ResourceManager.GetObject("Digital_certo");
                ptbDigi_status.Image = digi_imagem as Image;
                btObterdigital.Enabled = false;
                btnGravar.Enabled = true;
                btnGravar.Text = "Fechar";
                
                btnVerificardig.Enabled = false;
                // --------------

                btnExcluirdig.Enabled = true;

            }
            else
            {
                object digi_imagem = Resources.ResourceManager.GetObject("Digital_errado");
                ptbDigi_status.Image = digi_imagem as Image;
            }
           

        }

        public void iniciar_hamster()
        {           

            Template = new byte[844];
            TemplateLeitor = new byte[404];

            m_NBioAPI = new NBioAPI();

            UiMainBIO = this;

            NBioAPI.Type.VERSION version;

            version = new NBioAPI.Type.VERSION();
            m_NBioAPI.GetVersion(out version);

            m_NBioAPI.OpenDevice(NBioAPI.Type.DEVICE_ID.AUTO);
           
        }

        private void btObterdigital_Click(object sender, EventArgs e)
        {
            if (d1 == "")
            {
                //label2.Text = "Insira o dedo no leitor";
                //label2.ForeColor = Color.Red;

                obter_dig();
            }
            else
            {
                if(MessageBox.Show("Digital já recolhida! Deseja obter novamente ?", "Obter digital",MessageBoxButtons.YesNo) == DialogResult.Yes)                
                {
                    obter_dig();
                    btnGravar.Enabled = false;
                }
                else
                {

                }
            }

            
            
        }

        public uint MyCaptureCallback(ref NBioAPI.Type.WINDOW_CALLBACK_PARAM_0 cbParam0, IntPtr userParam)
        {
            NBioAPI.Type.WINDOW_CALLBACK_PARAM_EX ParamEx = (NBioAPI.Type.WINDOW_CALLBACK_PARAM_EX)System.Runtime.InteropServices.Marshal.PtrToStructure(cbParam0.ParamEx, typeof(NBioAPI.Type.WINDOW_CALLBACK_PARAM_EX));
            string szQuality = cbParam0.Quality.ToString();

            if (labelImgQuality.InvokeRequired)
            {
                labelImgQuality.Invoke(new MethodInvoker(delegate()
                {
                    labelImgQuality.Text = szQuality;
                    dig_qualidade = Convert.ToInt32(szQuality);
                }
                   ));
            }
            else
                labelImgQuality.Text = szQuality;
                dig_qualidade = Convert.ToInt32(szQuality);

            return 0;
        }

        public void obter_dig()
        {          

            iniciar_hamster();			

            //Conexão com a classe NBioAPI
            NBioAPI m_NBioAPI = new NBioAPI();

            //Criar uma estrutura de FIR
            NBioAPI.Type.HFIR capturedFIRAmostra;
            

            NBioAPI.Export.EXPORT_DATA exportData2030;
            NBioAPI.Export.EXPORT_AUDIT_DATA m_ExportAuditData = new NBioAPI.Export.EXPORT_AUDIT_DATA();
            NBioAPI.Export m_Export = new NBioAPI.Export(m_NBioAPI);

            ////Estrutura para Audit Fir
            NBioAPI.Type.HFIR capturedFIRAudit = new NBioAPI.Type.HFIR();
            NBioAPI.Type.FIR_PAYLOAD payload_1 = new NBioAPI.Type.FIR_PAYLOAD();

            //Selecionar as opções para captura

            m_WinOption = new NBioAPI.Type.WINDOW_OPTION();
            m_WinOption.WindowStyle = NBioAPI.Type.WINDOW_STYLE.POPUP;

            // Callback funtion
            m_WinOption.CaptureCallBackInfo = new NBioAPI.Type.CALLBACK_INFO_0();
            m_WinOption.CaptureCallBackInfo.CallBackFunction = new NBioAPI.Type.WINDOW_CALLBACK_0(MyCaptureCallback);

            //m_WinOption.FingerWnd = pictureExtWnd.Handle;


            

            //Realizar a captura
            //uint ret = UiMainBIO.m_NBioAPI.Capture(NBioAPI.Type.FIR_PURPOSE.VERIFY, out capturedFIRAmostra, 10000, capturedFIRAudit, m_WinOption);
            uint ret = UiMainBIO.m_NBioAPI.Capture(out capturedFIRAmostra, NBioAPI.Type.TIMEOUT.DEFAULT, m_WinOption);
            //uint ret = UiMainBIO.m_NBioAPI.Enroll(out capturedFIRAmostra, payload_1);

            

            m_hNewFIR2 = capturedFIRAmostra;

            //Verifica se teve erro captura
                if (ret != NBioAPI.Error.NONE)
            {
                tssl1.Text = ("Falha ao capturar!");
                return;
            }
            else
            {
                if (dig_qualidade != null)
                {
                    if (dig_qualidade > 85)
                    {
                        tssl1.Text = ("Digital Capturada! Verificar.");
                        btnVerificardig.Enabled = true;
                        label1.ForeColor = Color.Black;
                        labelImgQuality.ForeColor = Color.Black;
                        //label2.Text = "Capturado";
                        //label2.ForeColor = Color.Red;
                    }
                    else
                    {
                        MessageBox.Show("Qualidade de Captura Ruim. Favor refazer.");
                        label1.ForeColor = Color.Red;
                        labelImgQuality.ForeColor = Color.Red;
                        btnVerificardig.Enabled = false;
                        btnGravar.Enabled = false;
                        return;
                    }
                }
            }
            //Exporta o primeiro Template
            ret = m_Export.NBioBSPToImage(capturedFIRAudit, out m_ExportAuditData);
            ret = m_Export.NBioBSPToFDx(capturedFIRAmostra, out exportData2030, NBioAPI.Type.MINCONV_DATA_TYPE.MINCONV_TYPE_FIM01_HV);

            string template_catraca = "";
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < exportData2030.FingerData.Length; i++)
            {
                
                foreach (byte b in exportData2030.FingerData[i].Template[0].Data)
                {
                    strBuilder.Append(b.ToString("x").PadLeft(2, '0'));
                    template_catraca += b.ToString();
                    int a = template_catraca.Length;
                }
                Dedo1 = strBuilder;
                string teste = Dedo1.ToString();

                //StringBuilder teste = new StringBuilder();

                string templ = "";
                int o = 0;
                for (int d = 0; d < 404; d++)
                {
                    int inteiro = Convert.ToInt32(teste.Substring(o, 2), 16);                    
                    templ += inteiro.ToString();
                    int a = templ.Length;
                    o = o + 2;
                    
                }
                
            }

            d1 = Dedo1.ToString();

            m_NBioAPI.CloseDevice(NBioAPI.Type.DEVICE_ID.AUTO);
        }

        public void verifcar_dig()
        {
            int confere = 0;
            // Check exist enrolled fingerprint
            if (m_hNewFIR2 == null)
            {
                MessageBox.Show("Falha na Leitura!");
                return;
            }

            uint ret;
            bool result;

            // Verify
            ret = m_NBioAPI.Verify(m_hNewFIR2, out result, null);

            if (ret != NBioAPI.Error.NONE)
            {
                //DisplayErrorMsg("Verify Function Failed! - ", ret);
                return;
            }

            // Check result of verify
            if (result)
            {
                tssl1.Text = "Digital confere! Gravar";
                confere = 1;
            }
            else
            {
                tssl1.Text = "Digital não confere! Verificar";
                confere = 0;
            }

            if (confere == 1)
            {
                btnGravar.Enabled = true;
            }
            else
            {
                
            }
        }

        private void btnAceitar_Click(object sender, EventArgs e)
        {
            if(btnGravar.Text == "3 - Gravar")
            {
                cadastra_dig();

                if (cadastrado == 1)
                {
                    object digi_imagem = Resources.ResourceManager.GetObject("Digital_certo");
                    ptbDigi_status.Image = digi_imagem as Image;
                    tssl1.Text = "Digital Cadastrada!";
                    tssl1.ForeColor = Color.Red;
                    btObterdigital.Enabled = false;
                    btnVerificardig.Enabled = false;
                    btnGravar.Text = "Fechar";
                }
                else
                {
                    MessageBox.Show("Falha na Gravação! Tente novamente.");
                }
            }
            else
            {
                this.Close();
            }
            
        }

        public void cadastra_dig()
        {
            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());


            sql_bd.Command.CommandText = @"UPDATE ACESSO_CATRACA SET TEMPLATE1=@template1, TEMPLATE2=@template2,                                                                            
                                                                             DT_CAD=@dt_cad,                                                                             
                                                                             ENVIADO=@enviado,
                                                                             ATIVO=@ativo
                                                                             WHERE ID_CARTAO=@id_cartao";

            sql_bd.Command.Parameters.AddWithValue("@id_cartao", System.Convert.ToInt64(CScad_digital.cad_card));
            sql_bd.Command.Parameters.AddWithValue("@template1", d1);
            sql_bd.Command.Parameters.AddWithValue("@template2", d1);
            sql_bd.Command.Parameters.AddWithValue("@dt_cad", DateTime.Now);
            
            sql_bd.Command.Parameters.AddWithValue("@enviado", 0);
            sql_bd.Command.Parameters.AddWithValue("@ativo", 1);

            try
            {
                if (sql_bd.Connection.State != ConnectionState.Closed)
                {
                    sql_bd.Connection.Close();
                }
                sql_bd.Connection.Open();

                //Executa a instrução SQL
                sql_bd.Command.ExecuteNonQuery();

                cadastrado = 1;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                //Fecha a conexão 
                sql_bd.Connection.Close();
                sql_bd.Connection.Dispose();
                sql_bd.Command.Dispose();
                sql_bd.Command = null;
            }
        }        

        public void enviar_digital()
        {

            CSconf_catraca cscatraca = new CSconf_catraca();

            bool placaLight = false;
            int Ret = -1;

            UiBIO = UiMainBIO;

            try
            {
                SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

                sql_bd.Command.Parameters.Clear();
                sql_bd.Command.CommandText =
                    @"SELECT * FROM ACESSO_CATRACA WHERE ID_CARTAO=@id_cartao";

                //sql_bd.Command.Parameters.AddWithValue("@id_cartao", CScad_digital.cad_card);

                sql_bd.Command.Parameters.AddWithValue("@id_cartao", CScad_digital.cad_card);


                sql_bd.Connection.Open();
                sql_bd.Fill();
                UiMainBIO.Cursor = Cursors.WaitCursor;

                //Mensagem de Status                    
                UiMainBIO.lblNumat.Text = "";
                UiMainBIO.lblNumat.Text = "Iniciando comunicação...";
                UiMainBIO.lblNumat.RightToLeft = RightToLeft.No;
                Application.DoEvents();

                int iJaCadast = 0;
                int iOk = 0;
                int iFalha = 0;

                //Se conectar Bio
                if (Conectar(UiMainBIO))
                {
                    UiMainBIO.lblNumat.Text = "";
                    UiMainBIO.lblNumat.Text = "Verificando tipo da placa FIM...";
                    UiMainBIO.lblNumat.RightToLeft = RightToLeft.No;

                    placaLight = BioLight(UiMainBIO);

                    //Se a base estiver vazia retorna
                    if (sql_bd.Table.Rows.Count == 0)
                    {
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
                            TemplateA = sql_bd.Table.Rows[x][2].ToString();
                            TemplateB = sql_bd.Table.Rows[x][3].ToString();
                            linha = new byte[844];
                            tempConv = new byte[404];

                            //limpa linha
                            for (i = 0; i <= 843; i++)
                            {
                                linha[i] = 0;
                            }

                            //Template A
                            i = 1;
                            if (!placaLight)
                            {

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
                            Ret = (byte)EasyInner.EnviarUsuarioBio(cscatraca.def_NumInner, linha);
                            if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                            {

                                //Verifica se o template foi cadastrado com sucesso
                                Ret = (byte)EasyInner.UsuarioFoiEnviado(cscatraca.def_NumInner, 0);

                                if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                                    iOk++;
                                else if (Ret == (byte)Enumeradores.Retorno.RET_BIO_USR_JA_CADASTRADO)
                                    iJaCadast++;
                                else if (Ret != (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO)
                                    iFalha++;
                            }
                        }
                    }

                    //Mensagens de Status
                    UiMainBIO.lblNumat.RightToLeft = RightToLeft.Yes;

                    //string s = ("ENVIADOS: " + iOk) + (", JÁ CADASTRADOS: " + iJaCadast) + (", FALHARAM : " + iFalha) + "!";


                    if (iOk == 1)
                    {
                        UiMainBIO.lblNumat.Text = "Digital enviada!";
                    }
                    if (iJaCadast == 1)
                    {
                        UiMainBIO.lblNumat.Text = "Digital já existe!";
                    }
                    if (iFalha == 1)
                    {
                        UiMainBIO.lblNumat.Text = "Digital não enviada!";
                    }


                    //Fecha Porta de Comunicação
                    EasyInner.FecharPortaComunicacao();
                }

                //UiMainBIO.CarregaGrid();
                UiMainBIO.Cursor = Cursors.Default;

            }
            catch
            {
            }
            finally
            {

            }
        }

        private void btnVerificardig_Click(object sender, EventArgs e)
        {
            verifcar_dig();
        }

        private void btnExcluirdig_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Excluir a digital ? ", "Excluir digital", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                excluir_digital();               
            }
                  
        }

        public void excluir_digital()
        {
            SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());


            sql_bd.Command.CommandText = @"UPDATE ACESSO_CATRACA SET TEMPLATE1=@template1,                                                                             
                                                                             DT_CAD=@dt_cad,
                                                                             ULTIMO_ACESSO=@ultimo,
                                                                             ENVIADO=@enviado,
                                                                             ATIVO=@ativo
                                                                             WHERE ID_CARTAO=@id_cartao";

            sql_bd.Command.Parameters.AddWithValue("@id_cartao", System.Convert.ToInt64(CScad_digital.cad_card));
            sql_bd.Command.Parameters.AddWithValue("@template1", string.Empty);
            sql_bd.Command.Parameters.AddWithValue("@dt_cad", DateTime.Now);
            sql_bd.Command.Parameters.AddWithValue("@ultimo", DateTime.Now);
            sql_bd.Command.Parameters.AddWithValue("@ativo", 0);
            sql_bd.Command.Parameters.AddWithValue("@enviado", 0);

            try
            {
                if (sql_bd.Connection.State != ConnectionState.Closed)
                {
                    sql_bd.Connection.Close();
                }
                sql_bd.Connection.Open();

                //Executa a instrução SQL
                sql_bd.Command.ExecuteNonQuery();

                excluido = 1;

                btnGravar.Text = "3 - Gravar";
                btnGravar.Enabled = false;


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                //Fecha a conexão 
                sql_bd.Connection.Close();
                sql_bd.Connection.Dispose();
                sql_bd.Command.Dispose();
                sql_bd.Command = null;
                btObterdigital.Enabled = true;
                object digi_errrado = Resources.ResourceManager.GetObject("Digital_errado");
                ptbDigi_status.Image = digi_errrado as Image;
            }
        }

        #region CONEXAÕ INNER

        #region Conectar
        //***********************************************************************************
        //CONECTAR
        //Rotina responsável por efetuar a conexão com o Inner
        //***********************************************************************************
        private static bool Conectar(FRdig UiMainBIO)
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
                    UiMainBIO.lblNumat.Text = "Conectou ao Inner!";
                    Application.DoEvents();
                    return true;
                }
                else
                {
                    //Exibe mensagem de erro para o Usuário..  
                    UiMainBIO.lblNumat.Text = "Erro ao conectar com o Inner!";
                    Application.DoEvents();
                    return false;
                }
            }
            else
            {
                UiMainBIO.lblNumat.Text = "Não conectou ao Inner!";
                Application.DoEvents();
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
        private static bool BioLight(FRdig UiMainBIO)
        {
            bool placaLight = false;
            int Ret1 = -1;
            int modelo = 0;

            CSconf_catraca cscatraca = new CSconf_catraca();

            //Solicita Modelo
            Ret1 = EasyInner.SolicitarModeloBio(cscatraca.def_NumInner);
            SetarTimeoutBio();
            UiMainBIO.lblNumat.Text = "";
            do
            {
                if (Ret1 == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Thread.Sleep(5);

                    //Recebe Modelo
                    Ret1 = EasyInner.ReceberModeloBio(cscatraca.def_NumInner, 0, ref modelo);
                    if (Ret1 == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        UiMainBIO.lblNumat.RightToLeft = RightToLeft.No;
                        switch (modelo)
                        {
                            case 2:
                                UiMainBIO.lblNumat.Text = "Modelo do bio: Light usuários  := FIM10).";
                                placaLight = true;
                                break;
                            case 4: UiMainBIO.lblNumat.Text = "Modelo do bio: 1000/4000 usuários  := FIM01).";
                                break;
                            case 51: UiMainBIO.lblNumat.Text = "Modelo do bio: 1000/4000 usuários  := FIM2030).";
                                break;
                            case 52: UiMainBIO.lblNumat.Text = "Modelo do bio: 1000/4000 usuários  := FIM2040).";
                                break;
                            case 48:
                                UiMainBIO.lblNumat.Text = "Modelo do bio: Light 100 usuários  := FIM3030).";
                                placaLight = true;
                                break;
                            case 64:
                                UiMainBIO.lblNumat.Text = "Modelo do bio: Light 100 usuários  := FIM3040).";
                                placaLight = true;
                                break;
                            case 80: UiMainBIO.lblNumat.Text = "Modelo do bio: 1000/4000 usuários FIM5060.";
                                break;
                            case 82: UiMainBIO.lblNumat.Text = "Modelo do bio: 1000/4000 usuários FIM5260.";
                                break;
                            case 83:
                                UiMainBIO.lblNumat.Text = "Modelo do bio: Light 100 usuários FIM5360.";
                                placaLight = true;
                                break;
                            case 255: UiMainBIO.lblNumat.Text = "Modelo do bio: Desconhecido";
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
            UiBIO.Timeout = 0;
            UiBIO.Timeout = (int)EasyInner.RetornarSegundosSys() + 7;
        }
        #endregion

        #region EsperaRespostaBio
        private static bool EsperaRespostaBio(int Ret)
        {
            Pausa(1);
            return ((Ret != (int)Enumeradores.Retorno.RET_COMANDO_OK) && ((int)EasyInner.RetornarSegundosSys() <= UiBIO.Timeout));
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

        //Backup
//        public void obter_dig()
//        {


//            int fLinha = 0;

//            try
//            {
//                //Senão inicia preparação leitura dedos
//                if (Dedo1 == null)
//                {
//                    tssl1.Text = "Preparando a leitura do dedo 1";
//                    fLinha = 1;
//                }
//                else
//                {
//                    tssl1.Text = "Preparando a leitura do dedo 2";
//                    fLinha = 2;
//                }
//                //}

//                //Conexão com a classe NBioAPI
//                NBioAPI m_NBioAPI = new NBioAPI();


//                NBioAPI.Export.EXPORT_DATA exportData2030;
//                NBioAPI.Export.EXPORT_AUDIT_DATA m_ExportAuditData = new NBioAPI.Export.EXPORT_AUDIT_DATA();
//                NBioAPI.Export m_Export = new NBioAPI.Export(m_NBioAPI);

//                //Criar uma estrutura de FIR
//                NBioAPI.Type.HFIR capturedFIRAmostra1;

//                ////Estrutura para Audit Fir
//                NBioAPI.Type.HFIR capturedFIRAudit1 = new NBioAPI.Type.HFIR();
//                NBioAPI.Type.HFIR capturedFIRAudit2 = new NBioAPI.Type.HFIR();

//                //Sele  cionar as opções para captura

//                m_WinOption = new NBioAPI.Type.WINDOW_OPTION();
//                m_WinOption.WindowStyle = NBioAPI.Type.WINDOW_STYLE.INVISIBLE;
//                m_WinOption.FingerWnd = ptbDigital.Handle;

//                //NBioAPI.Type.WINDOW_OPTION winOpt = new NBioAPI.Type.WINDOW_OPTION();
//                //winOpt.WindowStyle = NBioAPI.Type.WINDOW_STYLE.INVISIBLE;

//                //Realizar a captura

//                //m_NBioAPI.Capture(out hCapturedFIR, NBioAPI.Type.TIMEOUT.DEFAULT, m_WinOption);
//                uint ret = UiMainBIO.m_NBioAPI.Capture(NBioAPI.Type.FIR_PURPOSE.VERIFY, out capturedFIRAmostra1, 10000, capturedFIRAudit1, m_WinOption);



//                //-------------------------
//                //NBioAPI.Type.FIR_PAYLOAD myPayload = new NBioAPI.Type.FIR_PAYLOAD();
//                ////myPayload.Data = textPayload.Text;

//                //uint ret = m_NBioAPI.Enroll(ref m_hNewFIR, out m_hNewFIR, myPayload, NBioAPI.Type.TIMEOUT.DEFAULT, null, null);

//                //-------------------------


//                //Verifica se teve erro captura
//                if (ret != NBioAPI.Error.NONE)
//                {
//                    tssl1.Text = ("Falha ao capturar!");
//                    return;
//                }

//                //Exporta o primeiro Template
//                ret = m_Export.NBioBSPToImage(capturedFIRAudit1, out m_ExportAuditData);
//                ret = m_Export.NBioBSPToFDx(capturedFIRAmostra1, out exportData2030, NBioAPI.Type.MINCONV_DATA_TYPE.MINCONV_TYPE_FIM01_HV);


//                //m_NBioAPI.GetFIRFromHandle(m_hNewFIR, out m_biFIR);

//                //m_NBioAPI.GetTextFIRFromHandle(m_hNewFIR, out m_textFIR, true);

//                //string zz = m_textFIR.TextFIR;


//                StringBuilder strBuilder = new StringBuilder();
//                for (int i = 0; i < exportData2030.FingerData.Length; i++)
//                {
//                    foreach (byte b in exportData2030.FingerData[i].Template[0].Data)
//                    {
//                        strBuilder.Append(b.ToString("x").PadLeft(2, '0'));
//                    }
//                }

//                //Verifica captura dedo 1
//                if (fLinha == 1)
//                {
//                    Dedo1 = strBuilder;
//                    tssl1.Text = "Dedo 1 capturado com sucesso!";
//                    ReturnValue1 = "ok";
//                    btObterdigital.Text = "Capturar 2º Digital";
//                }
//                else
//                {
//                    tssl1.Text = "Dedo 2 capturado com sucesso!";
//                    ReturnValue2 = "ok";

//                    enviar = 1;

//                    string d1 = Dedo1.ToString();
//                    string d2 = strBuilder.ToString();
//                    //string d2 = m_textFIR.TextFIR;

//                    SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());


//                    sql_bd.Command.CommandText = @"UPDATE ACESSO_CATRACA SET TEMPLATE1=@template1,
//                                                                             TEMPLATE2=@template2,
//                                                                             DT_CAD=@dt_cad,
//                                                                             ULTIMO_ACESSO=@ultimo
//                                                                             WHERE ID_CARTAO=@id_cartao";

//                    sql_bd.Command.Parameters.AddWithValue("@id_cartao", System.Convert.ToInt64(CScad_digital.cad_card));
//                    sql_bd.Command.Parameters.AddWithValue("@template1", d1);
//                    sql_bd.Command.Parameters.AddWithValue("@template2", d2);
//                    sql_bd.Command.Parameters.AddWithValue("@dt_cad", DateTime.Now);
//                    sql_bd.Command.Parameters.AddWithValue("@ultimo", DateTime.Now);

//                    try
//                    {
//                        if (sql_bd.Connection.State != ConnectionState.Closed)
//                        {
//                            sql_bd.Connection.Close();
//                        }
//                        sql_bd.Connection.Open();

//                        //Executa a instrução SQL
//                        sql_bd.Command.ExecuteNonQuery();


//                    }
//                    catch (SqlException ex)
//                    {
//                        MessageBox.Show("Error: " + ex.Message);
//                    }
//                    finally
//                    {
//                        //Fecha a conexão 
//                        sql_bd.Connection.Close();
//                        sql_bd.Connection.Dispose();
//                        sql_bd.Command.Dispose();
//                        sql_bd.Command = null;
//                    }
//                }

//                //UiMainBIO.CarregaGrid();

//                //Se capturou dedo 2 finaliza
//                if (fLinha == 2)
//                {

//                    //btObterdigital.Enabled = false;

//                    Dedo1 = null;

//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Erro:" + ex.Message);
//            }
//        }

    }
}
