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

using EmatWinFormsNetFramework13032.Digitais.Entity;
using System.Threading.Tasks;

using NITGEN.SDK.NBioBSP;

namespace EmatWinFormsNetFramework13032.Digitais.FrmOnline
{
    
    public partial class FRdigital : Form
    {
        public ComboBox list_dispositivos = new ComboBox();
        public NBioAPI m_NBioAPI;
        public short m_OpenedDeviceID;
        public NBioAPI.Type.HFIR m_hNewFIR;
        public NBioAPI.Type.FIR m_biFIR;
        public NBioAPI.Type.FIR_TEXTENCODE m_textFIR;
        public NBioAPI.Type.DEVICE_INFO_EX[] m_DeviceInfoEx;
        public NBioAPI.Type.WINDOW_OPTION m_WinOption;

        public FRdigital()
        {
            InitializeComponent();
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
        private static FRdigital _uiMainBIO;
        public static FRdigital UiMainBIO
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

        #region Métodos

        #region Métodos Internos da Form

        #region FrmBIO
        private static Form FPai;
        static bool aberto = false;
        public FRdigital(Form pai)
        {
            if (!aberto)
            {
                InitializeComponent();
                FPai = pai;
                InicializaCombos();

                Template = new byte[844];
                TemplateLeitor = new byte[404];

                m_NBioAPI = new NBioAPI();

                m_OpenedDeviceID = NBioAPI.Type.DEVICE_ID.NONE;
                m_hNewFIR = null;

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
        public static FRdigital GetInstance()
        {
            if (UiMainBIO == null)
            {
                UiMainBIO = (FRdigital)FPai.ActiveMdiChild;
            }
            return UiMainBIO;
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
            
            btIniciar.PerformClick();
        }
        #endregion

        #endregion

        #region InicializaCombos
        private void InicializaCombos()
        {
            //this.cboTipoConexao.Items.Add("Serial");
            //this.cboTipoConexao.Items.Add("TCP/IP porta variável");
            //this.cboTipoConexao.Items.Add("TCP/IP porta fixa");
            //this.cboTipoConexao.SelectedIndex = 2;

            //this.cboTipoConexaoOnline.Items.Add("Serial");
            //this.cboTipoConexaoOnline.Items.Add("TCP/IP porta variável");
            //this.cboTipoConexaoOnline.Items.Add("TCP/IP porta fixa");
            //this.cboTipoConexaoOnline.SelectedIndex = 2;

            //this.cboPadraoCartao.Items.Add("Topdata");
            //this.cboPadraoCartao.Items.Add("Livre");
            //this.cboPadraoCartao.SelectedIndex = 1;

            //this.cboPadraoCartaoOnline.Items.Add("Topdata");
            //this.cboPadraoCartaoOnline.Items.Add("Livre");
            //this.cboPadraoCartaoOnline.SelectedIndex = 1;

            //this.cboTipoLeitor.Items.Add("Código Barras");
            //this.cboTipoLeitor.Items.Add("Magnético");
            //this.cboTipoLeitor.Items.Add("Proximidade Abatrack");
            //this.cboTipoLeitor.Items.Add("Proximidade Wiegand");
            //this.cboTipoLeitor.SelectedIndex = 0;
        }
        #endregion

        #endregion

        public void CarregaGrid()
        {
            //Conecta na base
            if (CSdigital.ConectarBD())
            {
                try
                {
                    ////Define a instrução SQL
                    //string strSql = "SELECT Cartao FROM UsuariosBio";
                    ///Cria o objeto command para executar a instrução sql
                    //OleDbCommand cmd = new OleDbCommand(strSql, CSdigital.ConexaoBD);
                    ////Define o tipo do comando 
                    //cmd.CommandType = CommandType.Text;
                    ////Cria um dataadapter
                    //OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    ////Cria um objeto datatable
                    //DataTable Usuario = new DataTable();
                    ////Preenche o datatable via dataadapter
                    //da.Fill(Usuario);
                    ////Atribui o datatable ao datagridview para exibir o resultado
                    //dataGridView1.DataSource = Usuario;
                    //CSdigital.ConexaoBD.Dispose();
                    //cmd.Dispose();
                    //da.Dispose();
                    //Usuario.Dispose();
                    //CSdigital.ConexaoBD = null;
                    //cmd = null;
                    //da = null;
                    //Usuario = null;

                    SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());
                    sql_bd.Command.Parameters.Clear();
                    sql_bd.Command.CommandText =
                        @"SELECT ID_CARTAO FROM ACESSO_CATRACA";

                    sql_bd.Fill();
                    //dataGridView1.DataSource = sql_bd.Table;
                    sql_bd.Connection.Dispose();
                    sql_bd.Command.Dispose();
                    sql_bd.Table.Dispose();

                    
                    sql_bd.Command = null;
                    sql_bd.Table = null;                    

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro:" + ex.Message);
                }
            }

        }

        private void btIniciar_Click(object sender, EventArgs e)
        {
            if (list_dispositivos.SelectedIndex == -1)
            {
                MessageBox.Show("Não foi selecionado nenhum dispositivo");
                return;
            }
            CSdigital.AbrirDispositivo(this);
        }

        private void btCapturar_Click(object sender, EventArgs e)
        {
            CSdigital.CapturaTemplate(UiMainBIO);
        }

        private void cboDispositivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_dispositivos.Text != "")
            {
                btIniciar.Enabled = true;
                btCapturar.Enabled = true;
            }
        }

        private void cmdEnviarInner_Click(object sender, EventArgs e)
        {
            CSdigital.EnviarUsuarioPC_InnerBIO(this);
        }

        private void FrmBIO_FormClosed(object sender, FormClosedEventArgs e)
        {
            aberto = false;
        }

        private void FRdigital_Load(object sender, EventArgs e)
        {
            try
            {
                NBioAPI.Type.VERSION version;

                version = new NBioAPI.Type.VERSION();
                m_NBioAPI.GetVersion(out version);

                // Enumerate Device
                int i;
                uint nNumDevice;
                short[] nDeviceID;
                uint ret = m_NBioAPI.EnumerateDevice(out nNumDevice, out nDeviceID, out m_DeviceInfoEx);

                for (i = 0; i < nNumDevice; i++)
                {
                    list_dispositivos.Items.Add(m_DeviceInfoEx[i].Name + " (ID:" + m_DeviceInfoEx[i].Instance.ToString("D2") + ")");
                    //cboDispositivos.Items.Add(m_DeviceInfoEx[i].Name + " (ID:" + m_DeviceInfoEx[i].Instance.ToString("D2") + ")");
                }

                //cboDispositivos.SelectedIndex = -1;
                list_dispositivos.SelectedIndex = 0;
                
                

            }
            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
                MessageBox.Show("Para funcionamento do Hamster deverá instalar o pacote da Nitgen, maiores detalhes acesse o arquivo leiame contido na instalação do SDK (Menu Iniciar/Programas/SDK EasyInner/Manuais)");
            }

            LstUsuarios = new List<Usuario>();
            LstInners = new List<Inner>();

            //CarregaGrid();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            edCartao.Text = textBox1.Text;
        }
    }
}
