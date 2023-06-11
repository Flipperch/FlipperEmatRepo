using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Cryptography;

//Referências Nitgen
//using NBioBSPCOMLib;
using NITGEN.SDK.NBioBSP;

//Referências Projeto
using EmatWinFormsNetFramework13032.Digitais.COM;
using EmatWinFormsNetFramework13032.Digitais.Entity;

using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace EmatWinFormsNetFramework13032.Digitais.FrmOnline
{
    public class CSdigital
    {   
        

        #region Propriedades

        #region UiBIO
        private static FRdigital _uiMainBio;
        public static FRdigital UiBIO
        {
            get { return _uiMainBio; }
            set { _uiMainBio = value; }
        }
        #endregion

        Nitgen _objNitgen = null;

        public Nitgen objNitgen
        {
            get { return _objNitgen; }
            set
            {
                if (_objNitgen == null)
                    _objNitgen = value;
            }
        }

        #region Dedo1
        private static StringBuilder _dedo1;

        public static StringBuilder Dedo1
        {
            get { return CSdigital._dedo1; }
            set { CSdigital._dedo1 = value; }
        }
        #endregion

        #region UltimoSegundo
        private static int _ultimoSegundo;

        public static int UltimoSegundo
        {
            get { return _ultimoSegundo; }
            set { _ultimoSegundo = value; }
        }

        #endregion

        #endregion

        ////

        //#region Metodos de Configuração

        //private static void CapturaVersaoPlaca()
        //{
        //    //***********************************************************************************
        //    //Solicita dados do Firmware
        //    //Retorna o resultado
        //    //***********************************************************************************
        //    int ret2 = 0;

        //    byte Linha = 0;
        //    short Variacao = 0;
        //    byte VersaoAlta = 0;
        //    byte VersaoBaixa = 0;
        //    byte VersaoSufixo = 0;
        //    byte Ruf = 0;

        //    //Solicita a versão do firmware do Inner e dados como o Idioma, se é uma versão especial.
        //    ret2 = EasyInner.ReceberVersaoFirmware(int.Parse(UiBIO.txtNumInner.Text), ref Linha, ref Variacao, ref VersaoAlta, ref VersaoBaixa, ref VersaoSufixo, ref Ruf);


        //    //Se selecionado Biometria, valida se o equipamento é compatível
        //    // if (((Linha != 6) && (Linha != 14)) || ((Linha == 14) && (InnerAcessoBio == 0)))
        //    if (((Linha != 6) && (Linha != 14)) || ((Linha == 14)))
        //    {
        //        MessageBox.Show("Equipamento não compatível com Biometria.", "Atenção");
        //    }
        //}

        //#region MontarConfiguracao
        ////***********************************************************************************
        ////MONTAR CONFIGURAÇÕES
        ////Esta rotina monta o buffer para enviar a configuração do Inner
        ////***********************************************************************************
        //internal static int MontarConfiguracao(FRdigital UiMainBIO)
        //{
        //    //Atribuição de Variáveis
        //    int Ret = -1;
        //    UiBIO = UiMainBIO;

        //    //Mensagens de status
        //    //UiMainBIO.lblEmExec1.Text = "Configurando...";
        //    //UiMainBIO.listBox1.Items.Clear();
        //    //UiMainBIO.listBox1.Items.Add("Configurando...");
        //    Application.DoEvents();

        //    //Definição da EasyInner
        //    //EasyInner.DefinirPadraoCartao((byte)UiBIO.cboPadraoCartao.SelectedIndex);
        //    //EasyInner.HabilitarTeclado((byte)Enumeradores.Habilita.HABILITA, (byte)Enumeradores.Ecoar.ECOA_DIGITADO);
        //    //EasyInner.DefinirQuantidadeDigitosCartao((byte)UiBIO.txtQtdeDigitos.Value);

        //    //Configurar tipo do leitor
        //    //EasyInner.ConfigurarTipoLeitor((byte)UiBIO.cboTipoLeitor.SelectedIndex);

        //    //Definição da EasyInner
        //    EasyInner.ConfigurarLeitor1((byte)Enumeradores.ConfiguracaoLeitor.ENTRADA_SAIDA);
        //    EasyInner.ConfigurarAcionamento1((byte)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA_OU_SAIDA, 5);

        //    CapturaVersaoPlaca();

        //    //Envia as configurações para o Inner..
        //    //Ret = EasyInner.EnviarConfiguracoes(System.Convert.ToInt32(UiBIO.txtNumInner.Text));

        //    //Testa retorno do Envio de Configurações..
        //    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return 1;
        //    }

        //}
        //#endregion

        //#region ConfigurarInner
        ////***********************************************************************************
        ////Configuração do Inner
        ////***********************************************************************************
        //internal static void ConfigurarInner(FRdigital UiMainBIO)
        //{
        //    //Declaração de variáveis..
        //    byte Identificacao;
        //    byte Verificacao;
        //    int Ret = -1;
        //    UiBIO = UiMainBIO;
        //    int Mensagem;

        //    Mensagem = 0;

        //    //Campo obrigatório
        //    if (UiMainBIO.cboTipoLeitor.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("Favor selecionar um tipo de leitor !", "Atenção");
        //        UiBIO.cboTipoLeitor.Focus();
        //        return;
        //    }

        //    //Altera o Cursor para modo ampulheta..
        //    UiBIO.Cursor = Cursors.WaitCursor;

        //    //Verifica se estão checados os campos validação e Identificação..

        //    if (UiMainBIO.chkHabIdentificacao.Checked)
        //    {
        //        Identificacao = 1;
        //    }
        //    else
        //    {
        //        Identificacao = 0;
        //    }

        //    if (UiMainBIO.chkHabVerificacao.Checked)
        //    {
        //        Verificacao = 1;
        //    }
        //    else
        //    {
        //        Verificacao = 0;
        //    }

        //    //Mensagens de status
        //    UiMainBIO.lblEmExec1.Text = "Enviando Configuração...";
        //    UiMainBIO.listBox1.Items.Clear();
        //    UiMainBIO.listBox1.Items.Add("Enviando Configuração...");
        //    Application.DoEvents();

        //    //Tenta realizar a conexão com o Inner, caso tenha sucesso envia as configurações..
        //    if (Conectar(UiMainBIO))
        //    {
        //        //Configuração INNER
        //        Mensagem = CSdigital.MontarConfiguracao(UiBIO);

        //        //Configuração INNER BIO
        //        if (Identificacao == 1 || Verificacao == 1)
        //        {
        //            Mensagem = CSdigital.ConfigurarValidacaoIdentificacao(UiBIO, Verificacao, Identificacao, Ret);
        //        }
        //    }

        //    //Se Configuração ok
        //    if (Mensagem == 0)
        //    {
        //        System.Windows.Forms.MessageBox.Show("Configuração enviada com sucesso!", "Atenção.");
        //        UiMainBIO.lblEmExec1.Text = "Envio de configurações OK..";

        //        UiMainBIO.listBox1.Items.Clear();
        //        UiMainBIO.listBox1.Items.Add("Envio de configurações OK..");
        //        Application.DoEvents();
        //    }
        //    else
        //    {
        //        //Se erro de Configuração
        //        if (Mensagem == 1)
        //        {
        //            System.Windows.Forms.MessageBox.Show("Erro ao enviar a configuração!");
        //            UiMainBIO.lblEmExec1.Text = "Erro ao configurar o inner..";

        //            UiMainBIO.listBox1.Items.Clear();
        //            UiMainBIO.listBox1.Items.Add("Erro ao configurar o inner..");
        //            Application.DoEvents();
        //        } //Se erro de Configuração da Biometria
        //        else
        //        {
        //            System.Windows.Forms.MessageBox.Show("Erro ao configurar o Inner bio!");
        //            UiMainBIO.lblEmExec1.Text = "Erro ao configurar o inner bio..";
        //        }
        //    }

        //    //Fecha a comunicação com o Inner..
        //    EasyInner.FecharPortaComunicacao();

        //    //Altera o Cursor para modo Default..
        //    UiBIO.Cursor = Cursors.Default;
        //}
        //#endregion

        //#region ConfigurarValidacaoIdentificacao
        ////***********************************************************************************
        ////Configurar Validação Identificação
        ////Método que configura biometria e retorna se foi configurado com sucesso
        ////***********************************************************************************
        //internal static int ConfigurarValidacaoIdentificacao(FRdigital UiMainBIO, byte Verificacao, byte Identificacao, int Ret)
        //{
        //    //Seta o cursor para Ampulheta..
        //    UiMainBIO.Cursor = Cursors.WaitCursor;

        //    //Mensagens de Status
        //    UiMainBIO.listBox1.Items.Clear();
        //    UiMainBIO.listBox1.Items.Add("Enviando Configuração Bio...");
        //    Application.DoEvents();
        //    UiMainBIO.lblEmExec1.Text = "Enviando Configurações...";

        //    //Envia comando de Configuração..
        //    Ret = EasyInner.ConfigurarBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text), Identificacao, Verificacao);

        //    //Testa retorno do comando..
        //    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
        //    {
        //        SetarTimeoutBio();

        //        //Espera resposta do comando..
        //        do
        //        {
        //            Pausa(1);
        //            Ret = EasyInner.ResultadoConfiguracaoBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text), 0);
        //        }
        //        while (EsperaRespostaBio(Ret));
        //    }

        //    //Testa resultado da resposta..
        //    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return 2;
        //    }

        //    //Limpa Lista
        //    UiMainBIO.listBox1.Items.Clear();

        //    //Seta o cursor para Default..
        //    UiMainBIO.Cursor = Cursors.Default;

        //}
        //#endregion

        //#region ReceberModeloBio
        ////***********************************************************************************
        ////Receber Modelo Bio
        ////O modelo do Inner Bio é retornado.
        ////***********************************************************************************
        //internal static void ReceberModeloBio(FRdigital UiMainBIO)
        //{
        //    UiBIO = UiMainBIO;

        //    int Modelo = 0;
        //    int Ret = -1;

        //    //Seta o cursor para Ampulheta..
        //    UiMainBIO.Cursor = Cursors.WaitCursor;

        //    //Mensagem de Status
        //    UiMainBIO.listBox1.Items.Clear();
        //    UiMainBIO.listBox1.Items.Add("Recebendo Modelo Bio...");
        //    Application.DoEvents();

        //    //Conecta com o Inner..
        //    if (Conectar(UiMainBIO))
        //    {
        //        //Envia comando solicitando modelo do BIO...
        //        Ret = EasyInner.SolicitarModeloBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text));

        //        //Testa retorno do Comando..
        //        if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
        //        {
        //            SetarTimeoutBio();

        //            do
        //            {
        //                Pausa(1);

        //                //Envia solicitação de resposta..
        //                Ret = EasyInner.ReceberModeloBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text), 0, ref Modelo);
        //            }
        //            while (EsperaRespostaBio(Ret));

        //        }

        //        //Testa solicitação de Resposta..
        //        if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
        //        {
        //            switch (Modelo)
        //            {
        //                case 2:
        //                    System.Windows.Forms.MessageBox.Show("Modelo do bio: Light usuários (FIM10).");
        //                    break;
        //                case 4:
        //                    System.Windows.Forms.MessageBox.Show("Modelo do bio: 1000/4000 usuários (FIM01).");
        //                    break;
        //                case 51:
        //                    System.Windows.Forms.MessageBox.Show("Modelo do bio: 1000/4000 usuários (FIM2030).");
        //                    break;
        //                case 52:
        //                    System.Windows.Forms.MessageBox.Show("Modelo do bio: 1000/4000 usuários (FIM2040).");
        //                    break;
        //                case 48:
        //                    System.Windows.Forms.MessageBox.Show("Modelo do bio: Light 100 usuários (FIM3030).");
        //                    break;
        //                case 64:
        //                    System.Windows.Forms.MessageBox.Show("Modelo do bio: Light 100 usuários (FIM3040).");
        //                    break;
        //                case 80:
        //                    System.Windows.Forms.MessageBox.Show("Modelo do bio: 1000/4000 usuários FIM5060.");
        //                    break;
        //                case 82:
        //                    System.Windows.Forms.MessageBox.Show("Modelo do bio: 1000/4000 usuários FIM5260.");
        //                    break;
        //                case 83:
        //                    System.Windows.Forms.MessageBox.Show("Modelo do bio: Light 100 usuários FIM5360.");
        //                    break;
        //                case 255:
        //                    System.Windows.Forms.MessageBox.Show("Modelo do bio: Desconhecido");
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            System.Windows.Forms.MessageBox.Show("Erro ao solicitar o modelo do bio!");
        //        }
        //    }

        //    //Fecha Porta de Comunicação..
        //    EasyInner.FecharPortaComunicacao();
        //    UiMainBIO.listBox1.Items.Clear();
        //    UiMainBIO.Cursor = Cursors.Default;
        //}
        //#endregion

        //#region ReceberVersaoBIO
        ////***********************************************************************************
        ////Receber Versão Bio
        ////A versão do Inner Bio é retornado.
        ////***********************************************************************************
        //internal static void ReceberVersaoBIO(FRdigital UiMainBIO)
        //{
        //    UiBIO = UiMainBIO;

        //    int Ret = -1;
        //    int VersaoAlta = 0;
        //    int VersaoBaixa = 0;

        //    //Seta o cursor para Ampulheta..
        //    UiMainBIO.Cursor = Cursors.WaitCursor;

        //    //Mensagem de Status
        //    UiMainBIO.listBox1.Items.Clear();
        //    UiMainBIO.listBox1.Items.Add("Recebendo Versão Bio...");
        //    Application.DoEvents();

        //    //Conecta com o Inner..
        //    if (Conectar(UiMainBIO))
        //    {
        //        //Envia Comando solicitando versão..
        //        Ret = EasyInner.SolicitarVersaoBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text));

        //        //Testa Retorno do comando..
        //        if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
        //        {
        //            SetarTimeoutBio();

        //            do
        //            {
        //                Pausa(1);
        //                //Envia solicitação da resposta do comando..
        //                Ret = EasyInner.ReceberVersaoBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text), 0, ref VersaoAlta, ref VersaoBaixa);
        //            }
        //            while (EsperaRespostaBio(Ret));
        //        }

        //        //Testa retorno da resposta..
        //        if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
        //        {
        //            System.Windows.Forms.MessageBox.Show("A versão do inner bio é " + VersaoAlta + "." + VersaoBaixa);

        //        }
        //        else
        //        {
        //            System.Windows.Forms.MessageBox.Show("Erro ao receber a versão do Bio!");
        //        }
        //    }

        //    //Fecha a porta de comunicação com o InnerBIO..
        //    EasyInner.FecharPortaComunicacao();
        //    UiMainBIO.listBox1.Items.Clear();
        //    UiMainBIO.Cursor = Cursors.Default;
        //}
        //#endregion

        //#endregion

        ////

        ////

        //#region Métodos de Manutenção de Usuários

        //#region AdicionarUsuarioAMemoria
        ////***********************************************************************************
        ////Adicionar Usuário na Memória
        ////Cadastra a 1º e 2º digital do usuário na memória do Inner Bio.
        ////***********************************************************************************
        //internal static void AdicionarUsuarioAMemoria(FRdigital UiMainBIO)
        //{
        //    UiBIO = UiMainBIO;
        //    String Usuario = "";

        //    //Mensagem de Status
        //    UiMainBIO.lblManutencao.RightToLeft = RightToLeft.No;
        //    UiMainBIO.lblManutencao.Items.Add("Cadastrando Usuário " + UiMainBIO.txtNumUsuario.Value.ToString());
        //    Application.DoEvents();

        //    UiMainBIO.Cursor = Cursors.WaitCursor;

        //    //Se Conectar Bio
        //    if (Conectar(UiMainBIO))
        //    {

        //        //Define que o Inner utilizado no momento é um Inner bio light ao invés de 
        //        //um Inner bio 1000/4000.
        //        if (BioLight(UiMainBIO))
        //        {
        //            EasyInner.SetarBioLight(1);
        //        }

        //        Usuario = UiMainBIO.txtNumUsuario.Value.ToString();

        //        //Inserção da primeira digital
        //        MessageBox.Show("Posicione a primeira digital");
        //        if (!inserirTemplateUsr(UiMainBIO, Usuario, 0))
        //        {
        //            UiMainBIO.Cursor = Cursors.Default;
        //            UiMainBIO.lblManutencao.Items.Clear();
        //            return;
        //        }
        //        Thread.Sleep(20);

        //        //Inserção da segunda digital
        //        MessageBox.Show("Posicione a segunda digital");
        //        if (!inserirTemplateUsr(UiMainBIO, Usuario, 2))
        //        {
        //            UiMainBIO.Cursor = Cursors.Default;
        //            UiMainBIO.lblManutencao.Items.Clear();
        //            return;
        //        }

        //        //Mensagem Status
        //        MessageBox.Show("Usuário cadastrado!");
        //    }

        //    //Fecha a Porta de Comunicação com o Inner
        //    EasyInner.FecharPortaComunicacao();
        //    UiMainBIO.lblManutencao.Items.Clear();
        //    UiMainBIO.Cursor = Cursors.Default;
        //}
        //#endregion

        //#region RemoverUsuarioDaMemoria
        ////***********************************************************************************
        ////Remover Usuário da Memória
        ////Verifica se o usuário existe, se sim, exclui e retorna.
        ////***********************************************************************************
        //internal static void RemoverUsuarioDaMemoria(FRdigital UiMainBIO)
        //{
        //    String Usuario = "";
        //    int Ret = -1;
        //    UiBIO = UiMainBIO;

        //    //Mensagem de Status
        //    UiMainBIO.lblManutencao.Items.Clear();
        //    UiMainBIO.lblManutencao.Items.Add("Excluindo Usuário " + UiMainBIO.txtNumUsuario.Value.ToString());
        //    UiMainBIO.lblManutencao.RightToLeft = RightToLeft.No;
        //    Application.DoEvents();

        //    UiMainBIO.Cursor = Cursors.WaitCursor;

        //    //Se conectar Bio
        //    if (Conectar(UiMainBIO))
        //    {
        //        //Seta modelo da placa
        //        bool placalight = BioLight(UiMainBIO);

        //        //Verifica se é light
        //        if (placalight)
        //            //Caso sim
        //            Usuario = StringOfChar("0", 8 - UiMainBIO.txtNumUsuario.Value.ToString().Length) + UiMainBIO.txtNumUsuario.Value.ToString() + StringOfChar("0", 8);
        //        else
        //            //Senão
        //            Usuario = StringOfChar("0", 10 - (UiMainBIO.txtNumUsuario.Value.ToString().Length)) + UiMainBIO.txtNumUsuario.Value.ToString();

        //        //Solicita para o Inner bio excluir o cadastro do usuário desejado.
        //        Ret = EasyInner.SolicitarExclusaoUsuario((int)UiMainBIO.txtNumInner.Value, Usuario);

        //        SetarTimeoutBio();
        //        do
        //        {
        //            Thread.Sleep(20);
        //            if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
        //            {
        //                Thread.Sleep(10);
        //                SetarTimeoutBio();
        //                do
        //                {
        //                    //Verifica se foi excluído
        //                    Ret = EasyInner.UsuarioFoiExcluido((int)UiMainBIO.txtNumInner.Value, 0);
        //                    if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
        //                    {
        //                        //Caso sim
        //                        MessageBox.Show("Usuário " + UiMainBIO.txtNumUsuario.Value.ToString() + " excluído com sucesso!");
        //                        break;
        //                    }
        //                    else if (Ret == (byte)Enumeradores.Retorno.RET_USU_NAO_CADAST)
        //                    {
        //                        //Senão
        //                        MessageBox.Show("Usuário não cadastrado!");
        //                        break;
        //                    }
        //                } while (EsperaRespostaBio(Ret));
        //                break;
        //            }
        //        } while (EsperaRespostaBio(Ret));
        //    }

        //    //Fecha porta de comunicação
        //    EasyInner.FecharPortaComunicacao();
        //    UiMainBIO.lblManutencao.Items.Clear();
        //    UiMainBIO.Cursor = Cursors.Default;
        //}
        //#endregion

        #region Conexao_MDB
        //***********************************************************************************
        //Método responsável em efetuar a conexão com o banco de dados
        //arquivo BaseBio\UsuarioBio.MDB
        //***********************************************************************************
        internal static Boolean ConectarBD()
        {
            ////Identifica o caminho da pasta do C#
            //String path = Environment.CurrentDirectory;
            //int pos = path.Length - 1;
            //int tot = 0;
            //int i = 0;
            //for (i = pos; i > 0; i--)
            //{
            //    if (path.Substring(i, 1) == @"\")
            //    {
            //        tot++;
            //    }
            //    if (tot == 5) break;
            //}

            //path = path.Substring(0, i);
            //path += @"\BaseBio\UsuarioBio.mdb";

            ////Define string de conexão - Provedor + fonte de dados (caminho do banco de dados e seu nome)
            //string strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path;

            ////Cria a conexão com o banco de dados
            //con = new OleDbConnection(strConnection);

            //Abre a conexão            
            try
            {
                SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

                sql_bd.Command.Parameters.Clear();
                sql_bd.Command.CommandText =
                    @"SELECT ID_CARTAO FROM ACESSO_CATRACA";               

                sql_bd.Connection.Open();
                return true;
            }
            catch
            {
                MessageBox.Show("Não foi possível conectar na base de dados!");
                return false;
            }
        }
        #endregion

        #region EnviarUsuarioPC_InnerBIO
        //***********************************************************************************
        //Enviar Usuários PC para o Inner Bio
        //Consulta todos os usuários cadastrados no banco de dados do computador (.MDB)
        //Envia um template com duas digitais de cada usuário para o Inner Bio cadastrar 
        //no seu banco de dados.
        //***********************************************************************************
        internal static void EnviarUsuarioPC_InnerBIO(FRdigital UiMainBIO)
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

                sql_bd.Command.Parameters.AddWithValue("@id_cartao", UiMainBIO.edCartao.Text);

                sql_bd.Connection.Open();
                sql_bd.Fill();
                UiMainBIO.Cursor = Cursors.WaitCursor;

                //Mensagem de Status                    
                UiMainBIO.lblManutencao.Text = "";
                UiMainBIO.lblManutencao.Text = "Iniciando comunicação...";
                UiMainBIO.lblManutencao.RightToLeft = RightToLeft.No;
                Application.DoEvents();

                int iJaCadast = 0;
                int iOk = 0;
                int iFalha = 0;

                //Se conectar Bio
                if (Conectar(UiMainBIO))
                {
                    UiMainBIO.lblManutencao.Text = "";
                    UiMainBIO.lblManutencao.Text = "Verificando tipo da placa FIM...";
                    UiMainBIO.lblManutencao.RightToLeft = RightToLeft.No;

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
                    UiMainBIO.lblManutencao.RightToLeft = RightToLeft.Yes;

                    string s = ("ENVIADOS: " + iOk) + (", JÁ CADASTRADOS: " + iJaCadast) + (", FALHARAM : " + iFalha) + "!";

                    UiMainBIO.lblManutencao.Text = s;

                    //Fecha Porta de Comunicação
                    EasyInner.FecharPortaComunicacao();
                }

                UiMainBIO.CarregaGrid();
                UiMainBIO.Cursor = Cursors.Default;

            }
            catch
            {
            }
            finally
            {
                
            }
           
            
        }
        #endregion

        //#region ReceberQtdUsuariosBIO
        ////***********************************************************************************
        ////Receber Usuários cadastrados no Inner Bio
        ////Retorna a quantidade de usuários cadastrados.
        ////***********************************************************************************
        //internal static void ReceberQtdUsuariosBIO(FRdigital UiMainBIO)
        //{
        //    UiBIO = UiMainBIO;
        //    int Quantidade = 0;
        //    int Ret = -1;

        //    //Mensagem Status
        //    UiMainBIO.lblManutencao.Items.Clear();
        //    UiMainBIO.lblManutencao.Items.Add("Recebendo quantidade de usuários cadastrados");
        //    UiMainBIO.lblManutencao.RightToLeft = RightToLeft.No;

        //    Application.DoEvents();

        //    UiMainBIO.Cursor = Cursors.WaitCursor;

        //    if (Conectar(UiMainBIO))
        //    {
        //        //Solicita a quantidade de usuários cadastrados no Inner Bio.
        //        Ret = EasyInner.SolicitarQuantidadeUsuariosBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text));
        //        Thread.Sleep(50);

        //        if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
        //        {
        //            Ret = (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO;
        //            SetarTimeoutBio();
        //            do
        //            {
        //                //Retorna a quantidade de usuários cadastrados no Inner Bio
        //                Ret = EasyInner.ReceberQuantidadeUsuariosBio((int)UiMainBIO.txtNumInner.Value, 0, ref Quantidade);

        //                if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
        //                {
        //                    MessageBox.Show("Quantidade total de usuários: " + Quantidade, "");
        //                    break;
        //                }
        //            } while (EsperaRespostaBio(Ret));
        //        }
        //    }

        //    //Fecha porta comunicação
        //    EasyInner.FecharPortaComunicacao();
        //    UiMainBIO.Cursor = Cursors.Default;
        //    Application.DoEvents();

        //}
        //#endregion

        //#region CadastrarUsuarioNoLeitorBIO
        ////***********************************************************************************
        ////Cadastrar Novo Usuário no Leitor Bio
        ////Cadastro das digitais do usuário (templates)
        ////***********************************************************************************
        //internal static void CadastrarUsuarioNoLeitorBIO(FRdigital UiMainBIO)
        //{
        //    //Declaração/Atribuição de variáveis..
        //    int Ret = -1;
        //    UiBIO = UiMainBIO;
        //    Usuario usuario = new Usuario();

        //    //Seta o cursor do Mouse para Ampulheta..
        //    UiMainBIO.Cursor = Cursors.WaitCursor;

        //    //Campo obrigatório
        //    if (UiMainBIO.lstUsuarios.SelectedItem == null)
        //    {
        //        MessageBox.Show("É necessário selecionar um usuário em memória para cadastrar no InnerBIO (Necessita número Usuário)!");
        //        UiMainBIO.Cursor = Cursors.Default;
        //        return;
        //    }
        //    else
        //    {
        //        usuario = ((Usuario)UiMainBIO.lstUsuarios.SelectedItem);
        //    }
        //    UiMainBIO.lblEmExec1.Text = "Tentando Conectar...";

        //    //Conecta com o Inner.
        //    if (Conectar(UiMainBIO))
        //    {
        //        UiMainBIO.lblEmExec1.Text = "Coletando Templates...";

        //        //Solicita Posicionamento do Dedo..
        //        MessageBox.Show("Coloque o dedo no leitor para realizar a captura do Template A.");

        //        //Solicita a primeira digital do usuario
        //        Ret = EasyInner.InserirUsuarioLeitorBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text), 0, usuario.NroUsuario);

        //        //Testa o Retorno do Comando..
        //        if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
        //        {
        //            //Seta Timeouto no InnerBIO
        //            SetarTimeoutBio();

        //            //Aguarda resposta do InnerBIO..
        //            do
        //            {
        //                Pausa(1);
        //                Ret = EasyInner.ResultadoInsercaoUsuarioLeitorBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text), 0);
        //            }
        //            while (EsperaRespostaBio(Ret));
        //        }

        //        //Testa Retorno da Resposta..
        //        if (Ret != (int)Enumeradores.Retorno.RET_COMANDO_OK)
        //        {
        //            TratarRetornoErro(Ret);
        //            UiMainBIO.Cursor = Cursors.Default;
        //            return;
        //        }

        //        //Solicita a segunda digital, testando se é a mesma ou se é uma digital diferente..
        //        //Caso seja a mesma digital..
        //        if (UiMainBIO.rdbMesmoDedo.Checked)
        //        {
        //            MessageBox.Show("Coloque o dedo no leitor para realizar a captura do Template A.");
        //            Ret = EasyInner.InserirUsuarioLeitorBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text), 1, usuario.NroUsuario);

        //        } //Caso seja outro template..
        //        else
        //        {
        //            MessageBox.Show("Coloque o dedo no leitor para realizar a captura do Template B.");
        //            Ret = EasyInner.InserirUsuarioLeitorBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text), 2, usuario.NroUsuario);
        //        }

        //        //Testa Retorno do Comando..
        //        if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
        //        {
        //            SetarTimeoutBio();

        //            //Aguarda Reposta do Comando..
        //            do
        //            {
        //                Pausa(1);
        //                Ret = EasyInner.ResultadoInsercaoUsuarioLeitorBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text), 0);
        //            }
        //            while (EsperaRespostaBio(Ret));
        //        }

        //        //Testa retorno da Resposta..
        //        if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
        //        {
        //            System.Windows.Forms.MessageBox.Show("Usuário inserido pelo leitor com sucesso!", "");
        //        }
        //        else
        //        {
        //            TratarRetornoErro(Ret);

        //            //Muda o cursor para default..
        //            UiMainBIO.Cursor = Cursors.Default;

        //            return;
        //        }
        //    }

        //    //Fecha a porta de comunicação com o InnerBIO
        //    EasyInner.FecharPortaComunicacao();

        //    //Seta o Cursor para Default.
        //    UiMainBIO.Cursor = Cursors.Default;
        //    UiMainBIO.lblEmExec1.Text = "Templates coletados com sucesso!";
        //}
        //#endregion

        //#region ReceberUsuariosBIO_PC
        ////***********************************************************************************
        ////Receber Usuários cadastrados no Inner Bio
        ////Retorna todos os usuários cadastrados
        ////***********************************************************************************
        //internal static void ReceberUsuariosBIO_PC(FRdigital UiMainBIO)
        //{
        //    UiBIO = UiMainBIO;

        //    StringBuilder Usuario;
        //    int nPacote = 0;
        //    int nUsuario = 0;
        //    int Ret = -1;

        //    //Atribui a janela principal a variável da controller..
        //    UiBIO = UiMainBIO;

        //    UiMainBIO.lblManutencao.Items.Clear();
        //    UiMainBIO.lblManutencao.Items.Add("Recebendo todos os Usuários...");
        //    UiMainBIO.lblManutencao.RightToLeft = RightToLeft.No;
        //    Application.DoEvents();
        //    UiMainBIO.Cursor = Cursors.WaitCursor;
        //    UiMainBIO.lstUsuarios.Items.Clear();

        //    if (Conectar(UiMainBIO))
        //    {
        //        //Limpa lista de usuários

        //        //Inicia coleta usuários
        //        EasyInner.InicializarColetaListaUsuariosBio();

        //        while (EasyInner.TemProximoPacote() == 1)
        //        {
        //            SetarTimeoutBio();
        //            do
        //            {
        //                Thread.Sleep(20);

        //                //Solicita uma parte(pacote) da lista de usuarios do bio
        //                Ret = EasyInner.SolicitarListaUsuariosBio((int)UiMainBIO.txtNumInner.Value);
        //                UiMainBIO.lblManutencao.Items.Clear();
        //                UiMainBIO.lblManutencao.Items.Add("Solicitando pacote...");
        //                UiMainBIO.lblManutencao.RightToLeft = RightToLeft.No;
        //                Application.DoEvents();
        //            } while (EsperaRespostaBio(Ret));//se ainda estava

        //            if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
        //            {
        //                //Recebe uma parte da lista com os usuarios
        //                SetarTimeoutBio();
        //                do
        //                {
        //                    Thread.Sleep(20);
        //                    Ret = EasyInner.ReceberPacoteListaUsuariosBio((int)UiMainBIO.txtNumInner.Value);
        //                    UiMainBIO.lblManutencao.Items.Clear();
        //                    UiMainBIO.lblManutencao.Items.Add("Recebendo pacote: " + System.Convert.ToInt32(nPacote + 1));
        //                    UiMainBIO.lblManutencao.RightToLeft = RightToLeft.No;
        //                    Application.DoEvents();

        //                } while (EsperaRespostaBio(Ret));

        //                if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
        //                {
        //                    nPacote++;
        //                    UiMainBIO.lblManutencao.Items.Clear();
        //                    UiMainBIO.lblManutencao.Items.Add("Recebeu pacote: " + System.Convert.ToInt32(nPacote));
        //                    UiMainBIO.lblManutencao.RightToLeft = RightToLeft.No;
        //                    Application.DoEvents();
        //                }
        //                else
        //                {
        //                    continue;
        //                }

        //                UiMainBIO.lstUsuarios.Items.Clear();
        //                Thread.Sleep(50);

        //                //Verifica se existe um usuario
        //                while (EasyInner.TemProximoUsuario() == 1)
        //                {
        //                    Usuario = null;
        //                    Usuario = new StringBuilder();

        //                    //Pede um usuario da lista
        //                    Ret = EasyInner.ReceberUsuarioLista((int)UiMainBIO.txtNumInner.Value, Usuario);

        //                    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
        //                    {
        //                        nUsuario++;
        //                        //Insere o usuario no listbox
        //                        UiMainBIO.lstUsuarios.Items.Add(Usuario);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Console.Beep();
        //                UiMainBIO.lblManutencao.RightToLeft = RightToLeft.No;
        //                UiMainBIO.lblManutencao.Items.Clear();

        //                //Em caso de erro
        //                switch (Ret)
        //                {
        //                    case (byte)Enumeradores.Retorno.RET_COMANDO_ERRO:
        //                        UiMainBIO.lblManutencao.Items.Add("Erro ao abrir porta de comunicação");
        //                        break;
        //                    case (byte)Enumeradores.Retorno.RET_PORTA_NAOABERTA:
        //                        UiMainBIO.lblManutencao.Items.Add("Porta não aberta");
        //                        break;
        //                    case (byte)Enumeradores.Retorno.RET_PORTA_JAABERTA:
        //                        UiMainBIO.lblManutencao.Items.Add("Porta já aberta");
        //                        break;
        //                    case (byte)Enumeradores.Retorno.RET_DLL_INNER2K_NAO_ENCONTRADA:
        //                        UiMainBIO.lblManutencao.Items.Add("DLL Inner2k não encontrada");
        //                        break;
        //                    case (byte)Enumeradores.Retorno.RET_DLL_INNERTCP_NAO_ENCONTRADA:
        //                        UiMainBIO.lblManutencao.Items.Add("DLL InnerTCP não encontrada");
        //                        break;
        //                    case (byte)Enumeradores.Retorno.RET_DLL_INNERTCP2_NAO_ENCONTRADA:
        //                        UiMainBIO.lblManutencao.Items.Add("DLL InnerTCP2 não encontrada");
        //                        break;
        //                    case (byte)Enumeradores.Retorno.RET_ERRO_GPF:
        //                        UiMainBIO.lblManutencao.Items.Add("Ocorreu um erro dentro da DLL");
        //                        break;
        //                    case (byte)Enumeradores.Retorno.RET_TIPO_CONEXAO_INVALIDA:
        //                        UiMainBIO.lblManutencao.Items.Add("Tipo de conexão inválida");
        //                        break;
        //                    default:
        //                        UiMainBIO.lblManutencao.Items.Add("Erro " + Ret.ToString());
        //                        break;
        //                }
        //            }
        //            Application.DoEvents();
        //        }
        //    }
        //    Console.Beep();
        //    UiMainBIO.lblTotalBIO.Text = "Recebeu " + nUsuario.ToString() + " usuários";
        //    EasyInner.FecharPortaComunicacao();

        //    UiMainBIO.Cursor = Cursors.Default;
        //}
        //#endregion

        //#region ReceberTemplateDeUsuarios
        ////***********************************************************************************
        ////Gravar Base
        ////Grava os usuários na base de dados do computador (.mdb)
        ////***********************************************************************************
        //internal static void GravarBase(FRdigital UiMainBIO)
        //{
        //    bool placaLight = false;
        //    int i;
        //    int y = 0;
        //    int j = -1;
        //    int ret1 = -1;
        //    int ret2 = -1;
        //    byte[] Template = new byte[844];
        //    Nitgen objNitgen;
        //    StringBuilder strBuilder;
        //    StringBuilder Template1;
        //    StringBuilder Template2;
        //    byte[] pTeste;

        //    UiBIO = UiMainBIO;

        //    //Se a lista de usuários estiver vazia finaliza
        //    if (UiMainBIO.lstUsuarios.Items.Count == 0)
        //        return;

        //    UiMainBIO.lblManutencao.Items.Clear();

        //    //Mensagem Status
        //    UiMainBIO.lblManutencao.Items.Add("Gravando na base de dados...");
        //    UiMainBIO.lblManutencao.RightToLeft = RightToLeft.No;
        //    Application.DoEvents();

        //    UiMainBIO.Cursor = Cursors.WaitCursor;

        //    //Se conectar
        //    if (Conectar(UiMainBIO))
        //    {
        //        placaLight = BioLight(UiMainBIO);

        //        //Para cada usuário da lista
        //        for (i = 0; i <= UiMainBIO.lstUsuarios.Items.Count - 1; i++)
        //        {
        //            //Conecta na base
        //            if (ConectarBD())
        //            {
        //                try
        //                {

        //                    //Define a instrução SQL
        //                    string strSql = "Select top 1 Cartao from UsuariosBio where CLng(Cartao) = " + int.Parse(UiMainBIO.lstUsuarios.Items[i].ToString());

        //                    //Cria o objeto command para executar a instrução sql
        //                    OleDbCommand cmd = new OleDbCommand(strSql, con);

        //                    cmd.CommandType = System.Data.CommandType.Text;
        //                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);

        //                    //Cria um objeto datatable
        //                    System.Data.DataTable Usuario = new System.Data.DataTable();
        //                    da.Fill(Usuario);

        //                    cmd.Dispose();
        //                    cmd = null;

        //                    //Verifica se já existe no banco de dados
        //                    if (Usuario.Rows.Count == 0)
        //                    {
        //                        SetarTimeoutBio();
        //                        do
        //                        {
        //                            //Senão existe

        //                            Thread.Sleep(20);

        //                            if (placaLight)
        //                                EasyInner.SetarBioLight(1);

        //                            //Solicita os dados do usuário cadastrados no leitor
        //                            ret1 = (byte)EasyInner.SolicitarUsuarioCadastradoBio((int)UiMainBIO.txtNumInner.Value, Convert.ToInt32(UiMainBIO.lstUsuarios.Items[i].ToString()).ToString());

        //                            if (ret1 == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
        //                            {
        //                                SetarTimeoutBio();
        //                                do
        //                                {
        //                                    Thread.Sleep(20);

        //                                    //Recebe os dados do usuário cadastrados no leitor
        //                                    ret2 = (byte)EasyInner.ReceberUsuarioCadastradoBio((int)UiMainBIO.txtNumInner.Value, 0, Template);

        //                                    //Se retornado com sucesso
        //                                    if (ret2 == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
        //                                    {
        //                                        //Inicia processo de gravação

        //                                        if (placaLight)
        //                                        {
        //                                            pTeste = new byte[404];
        //                                            j = 27;
        //                                            for (y = 0; y <= 403; y++)
        //                                            {
        //                                                pTeste[y] = Template[j];
        //                                                j++;
        //                                            }

        //                                            objNitgen = new Nitgen();
        //                                            objNitgen.objFPData.Import(1, 0, 1, 6, 400, pTeste, null);
        //                                            objNitgen.objFPData.Export(objNitgen.objFPData.FIR, 7);
        //                                            strBuilder = new StringBuilder();

        //                                            foreach (byte b in (byte[])objNitgen.objFPData.get_FPData(objNitgen.objFPData.get_FingerID(0), 0))
        //                                            {
        //                                                strBuilder.Append(b.ToString("x").PadLeft(2, '0'));
        //                                            }
        //                                            objNitgen = null;
        //                                            Template1 = new StringBuilder();
        //                                            Template1 = strBuilder;
        //                                            strBuilder = null;

        //                                            for (y = 0; y <= 403; y++)
        //                                            {
        //                                                pTeste[y] = 0;
        //                                            }

        //                                            j = 427;
        //                                            for (y = 0; y <= 403; y++)
        //                                            {
        //                                                pTeste[y] = Template[j];
        //                                                j++;
        //                                            }

        //                                            objNitgen = new Nitgen();
        //                                            objNitgen.objFPData.Import(1, 0, 1, 6, 400, pTeste, null);
        //                                            objNitgen.objFPData.Export(objNitgen.objFPData.FIR, 7);
        //                                            strBuilder = new StringBuilder();

        //                                            foreach (byte b in (byte[])objNitgen.objFPData.get_FPData(objNitgen.objFPData.get_FingerID(0), 0))
        //                                            {
        //                                                strBuilder.Append(b.ToString("x").PadLeft(2, '0'));
        //                                            }

        //                                            Template2 = new StringBuilder();
        //                                            Template2 = strBuilder;
        //                                            strBuilder = null;
        //                                            objNitgen = null;
        //                                        }
        //                                        else
        //                                        {
        //                                            pTeste = new byte[404];

        //                                            j = 28;
        //                                            for (y = 0; y <= 403; y++)
        //                                            {
        //                                                pTeste[y] = Template[j];
        //                                                j++;
        //                                            }

        //                                            strBuilder = new StringBuilder();

        //                                            foreach (byte b in (byte[])pTeste)
        //                                            {
        //                                                strBuilder.Append(b.ToString("x").PadLeft(2, '0'));
        //                                            }

        //                                            Template1 = strBuilder;
        //                                            strBuilder = null;

        //                                            for (y = 0; y <= 403; y++)
        //                                            {
        //                                                pTeste[y] = 0;
        //                                            }

        //                                            for (y = 0; y <= 403; y++)
        //                                            {
        //                                                pTeste[y] = Template[y + 432];
        //                                            }

        //                                            strBuilder = new StringBuilder();

        //                                            foreach (byte b in (byte[])pTeste)
        //                                            {
        //                                                strBuilder.Append(b.ToString("x").PadLeft(2, '0'));
        //                                            }

        //                                            Template2 = strBuilder;
        //                                            strBuilder = null;
        //                                        }

        //                                        strSql = "INSERT INTO UsuariosBio(Cartao, Template1, Template2, DataHoraCadastro)";
        //                                        strSql += " VALUES('" + System.Convert.ToInt64(UiMainBIO.lstUsuarios.Items[i].ToString()) + "', '" + Template1 + "', '" + Template2 + "', Now)";

        //                                        cmd = new OleDbCommand(strSql, con);

        //                                        try
        //                                        {
        //                                            //Executa a instrução SQL
        //                                            cmd.ExecuteNonQuery();
        //                                        }
        //                                        catch (OleDbException ex)
        //                                        {
        //                                            MessageBox.Show("Error: " + ex.Message);
        //                                        }
        //                                        finally
        //                                        {
        //                                            //Fecha a conexão 
        //                                            con.Close();
        //                                            con.Dispose();
        //                                            con = null;
        //                                            cmd.Dispose();
        //                                            cmd = null;
        //                                            Template1 = null;
        //                                            Template2 = null;
        //                                        }

        //                                        UiMainBIO.CarregaGrid();
        //                                    }
        //                                } while (EsperaRespostaBio(ret2));//se ainda estava
        //                            }
        //                            Thread.Sleep(10);
        //                        } while (EsperaRespostaBio(ret1));//se ainda estava
        //                    }
        //                }
        //                catch
        //                {
        //                }
        //            }
        //        }
        //        UiMainBIO.CarregaGrid();
        //        UiMainBIO.Cursor = Cursors.Default;
        //        Application.DoEvents();
        //    }
        //}
        //#endregion

        //#region GravaLogUsuariosErro
        ////***********************************************************************************
        ////Log de erros lista de usuários
        ////***********************************************************************************
        //private static void GravaLogUsuariosErro(List<UsuarioBIO> lstUsuariosErros)
        //{
        //    try
        //    {
        //        foreach (UsuarioBIO usuarioBio in lstUsuariosErros)
        //        {
        //            //Caso Não exista o Temp.. Cria..
        //            if (!Directory.Exists("c:\\temp"))
        //            {
        //                Directory.CreateDirectory("c:\\temp");
        //            }

        //            FileStream arquivo;
        //            if (!File.Exists("c:\\temp\\Log_ERRO.txt"))
        //            {
        //                arquivo = File.Create("c:\\temp\\Log_ERRO.txt");
        //            }
        //            else
        //            {
        //                arquivo = File.Open("c:\\temp\\Log_ERRO.txt", FileMode.Append);
        //            }

        //            //Grava no arquivo de log, a data..e o método que foi executado..
        //            StreamWriter writer = new StreamWriter(arquivo);

        //            writer.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        //            writer.WriteLine("Template de Usuário Lançou erro durante Conversão.");
        //            writer.WriteLine("Codigo Usuário: " + usuarioBio.Codigo.ToString());
        //            writer.WriteLine("Cartão Usuário: " + usuarioBio.Cartao.ToString());
        //            if (usuarioBio.Template404_1 != null)
        //                writer.WriteLine("Template404_1 : " + usuarioBio.Template404_1.ToString());
        //            else
        //                writer.WriteLine("Template404_1 : NULO!");
        //            if (usuarioBio.Template404_2 != null)
        //                writer.WriteLine("Template404_2 : " + usuarioBio.Template404_2.ToString());
        //            else
        //                writer.WriteLine("Template404_2 : NULO!");
        //            writer.WriteLine("            ---------------------------");

        //            if (usuarioBio.Template400_1 != null)
        //                writer.WriteLine("Template400_1 : " + usuarioBio.Template400_1.ToString());
        //            else
        //                writer.WriteLine("Template400_1 : NULO!");
        //            if (usuarioBio.Template400_2 != null)
        //                writer.WriteLine("Template400_2 : " + usuarioBio.Template400_2.ToString());
        //            else
        //                writer.WriteLine("Template400_2 : NULO!");
        //            writer.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

        //            //Usuario que não foi convertido
        //            writer.Close();
        //            arquivo.Close();

        //            Thread.Sleep(200);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        //#endregion

        //#region GravaLogUsuariosErro
        ////***********************************************************************************
        ////Log de erros usuário
        ////***********************************************************************************
        //private static void GravaLogUsuario(UsuarioBIO usuarioBio)
        //{
        //    try
        //    {
        //        {
        //            //Caso Não exista o Temp.. Cria..
        //            if (!Directory.Exists("c:\\temp"))
        //            {
        //                Directory.CreateDirectory("c:\\temp");
        //            }

        //            FileStream arquivo;
        //            if (!File.Exists("c:\\temp\\Log.txt"))
        //            {
        //                arquivo = File.Create("c:\\temp\\Log.txt");
        //            }
        //            else
        //            {
        //                arquivo = File.Open("c:\\temp\\Log.txt", FileMode.Append);
        //            }

        //            //Grava no arquivo de log, a data..e o método que foi executado..
        //            StreamWriter writer = new StreamWriter(arquivo);

        //            writer.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        //            writer.WriteLine("Template de Usuário Convertido.");
        //            writer.WriteLine("Codigo Usuário: " + usuarioBio.Codigo.ToString());
        //            writer.WriteLine("Cartão Usuário: " + usuarioBio.Cartao.ToString());
        //            if (usuarioBio.Template404_1 != null)
        //                writer.WriteLine("Template404_1 : " + usuarioBio.Template404_1.ToString());
        //            else
        //                writer.WriteLine("Template404_1 : NULO!");
        //            if (usuarioBio.Template404_2 != null)
        //                writer.WriteLine("Template404_2 : " + usuarioBio.Template404_2.ToString());
        //            else
        //                writer.WriteLine("Template404_2 : NULO!");
        //            writer.WriteLine("            ---------------------------");

        //            if (usuarioBio.Template400_1 != null)
        //                writer.WriteLine("Template400_1 : " + usuarioBio.Template400_1.ToString());
        //            else
        //                writer.WriteLine("Template400_1 : NULO!");
        //            if (usuarioBio.Template400_2 != null)
        //                writer.WriteLine("Template400_2 : " + usuarioBio.Template400_2.ToString());
        //            else
        //                writer.WriteLine("Template400_2 : NULO!");
        //            writer.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

        //            //Usuario que não foi convertido
        //            writer.Close();
        //            arquivo.Close();

        //            Thread.Sleep(5);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        //#endregion

        //#region ExcluirUsuario
        ////***********************************************************************************
        ////APAGA O CARTÃO 'Usuário'
        ////***********************************************************************************
        //internal static void ExcluirUsuario(FRdigital UiMainBIO)
        //{
        //    //Consulta se o usuário existe
        //    if ((UiMainBIO.dataGridView1.Rows.Count - 1) == 0)
        //        return;

        //    //Usuário encontrado
        //    DialogResult result1 = MessageBox.Show("Deseja realmente apagar o cartão '" + UiMainBIO.dataGridView1.SelectedCells[0].Value + "'?", "Exclusão", MessageBoxButtons.YesNo);

        //    if (result1 == DialogResult.No)
        //    {
        //        return;
        //    }

        //    //Conecta na base
        //    if (ConectarBD())
        //    {
        //        try
        //        {

        //            //Define a instrução SQL
        //            string strSql = "DELETE FROM UsuariosBio WHERE Cartao = '" + UiMainBIO.dataGridView1.SelectedCells[0].Value + "'";

        //            //Cria o comando que inicia a instrução SQL para exclusão
        //            OleDbCommand cmdExcluir = new OleDbCommand(strSql, con);

        //            try
        //            {
        //                //Executa a instrução SQL
        //                cmdExcluir.ExecuteNonQuery();

        //                MessageBox.Show("Dados excluídos com sucesso!");
        //            }
        //            catch (OleDbException ex)
        //            {
        //                MessageBox.Show("Error: " + ex.Message);
        //            }
        //            finally
        //            {
        //                //Fecha a conexão 
        //                con.Close();
        //                con.Dispose();
        //                con = null;
        //            }

        //            UiMainBIO.CarregaGrid();
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
        //#endregion

        #region AbrirDispositivo
        //***********************************************************************************
        //INICIO (Hamster)
        //***********************************************************************************
        internal static void AbrirDispositivo(FRdigital UiMainBIO)
        {
            short iDeviceID = NBioAPI.Type.DEVICE_ID.AUTO;

            //Seleciona dispositivo
            if (UiMainBIO.list_dispositivos.SelectedIndex > 0)
                iDeviceID = (short)(UiMainBIO.m_DeviceInfoEx[UiMainBIO.list_dispositivos.SelectedIndex - 1].NameID + (UiMainBIO.m_DeviceInfoEx[UiMainBIO.list_dispositivos.SelectedIndex - 1].Instance << 8));
            else
                iDeviceID = NBioAPI.Type.DEVICE_ID.AUTO;

            //Fecha dispositivo
            UiMainBIO.m_NBioAPI.CloseDevice(UiMainBIO.m_OpenedDeviceID);

            //Abre dispositivo
            uint ret = UiMainBIO.m_NBioAPI.OpenDevice(iDeviceID);
            if (ret == NBioAPI.Error.NONE)
            {
                UiMainBIO.m_OpenedDeviceID = iDeviceID;
                //UiMainBIO.btCapturar.Enabled = true;
                MessageBox.Show("Hamster pronto, informe o número do cartão");
                //UiMainBIO.edCartao.Focus();
            }
            else
            {
                UiMainBIO.m_NBioAPI.CloseDevice(iDeviceID);
            }

        }
        #endregion

        #region CapturaTemplate
        //***********************************************************************************
        //CAPTURA TEMPLATE
        //Cadastra as digitais do novo usuário
        //***********************************************************************************
        internal static void CapturaTemplate(FRdigital UiMainBIO)
        {
            int fLinha = 0;

            try
            {
                //Senão inicia preparação leitura dedos
                if (Dedo1 == null)
                {
                    MessageBox.Show("Preparando a leitura do dedo 1");
                    fLinha = 1;
                }
                else
                {
                    MessageBox.Show("Preparando a leitura do dedo 2");
                    fLinha = 2;
                }
                //}

                //Conexão com a classe NBioAPI
                NBioAPI m_NBioAPI = new NBioAPI();
                NBioAPI.Export.EXPORT_DATA exportData2030;
                NBioAPI.Export.EXPORT_AUDIT_DATA m_ExportAuditData = new NBioAPI.Export.EXPORT_AUDIT_DATA();
                NBioAPI.Export m_Export = new NBioAPI.Export(m_NBioAPI);

                //Criar uma estrutura de FIR
                NBioAPI.Type.HFIR capturedFIRAmostra1;

                //Estrutura para Audit Fir
                NBioAPI.Type.HFIR capturedFIRAudit1 = new NBioAPI.Type.HFIR();
                NBioAPI.Type.HFIR capturedFIRAudit2 = new NBioAPI.Type.HFIR();

                //Selecionar as opções para captura
                NBioAPI.Type.WINDOW_OPTION winOpt = new NBioAPI.Type.WINDOW_OPTION();
                winOpt.WindowStyle = NBioAPI.Type.WINDOW_STYLE.POPUP;

                //Realizar a captura
                uint ret = UiMainBIO.m_NBioAPI.Capture(NBioAPI.Type.FIR_PURPOSE.VERIFY, out capturedFIRAmostra1, 10000, capturedFIRAudit1, winOpt);

                //Verifica se teve erro captura
                if (ret != NBioAPI.Error.NONE)
                {
                    MessageBox.Show("Falha ao capturar!");
                    return;
                }

                //Exporta o primeiro Template
                ret = m_Export.NBioBSPToImage(capturedFIRAudit1, out m_ExportAuditData);
                ret = m_Export.NBioBSPToFDx(capturedFIRAmostra1, out exportData2030, NBioAPI.Type.MINCONV_DATA_TYPE.MINCONV_TYPE_FIM01_HV);

                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < exportData2030.FingerData.Length; i++)
                {
                    foreach (byte b in exportData2030.FingerData[i].Template[0].Data)
                    {
                        strBuilder.Append(b.ToString("x").PadLeft(2, '0'));
                    }
                }

                //Verifica captura dedo 1
                if (fLinha == 1)
                {
                    Dedo1 = strBuilder;
                    MessageBox.Show("Dedo 1 capturado com sucesso!!!");
                }
                else
                {
                    MessageBox.Show("Dedo 2 capturado com sucesso!!!");

                    string d1 = Dedo1.ToString();
                    string d2 = strBuilder.ToString();

                    SqlQuery sql_bd = new SqlQuery(Conexoes.GetSqlConnection());

                    sql_bd.Command.CommandText = @"UPDATE ACESSO_CATRACA SET TEMPLATE1=@template1,
                                                                             TEMPLATE2=@template2,
                                                                             DT_CAD=@dt_cad
                                                                             WHERE ID_CARTAO=@id_cartao";

                    sql_bd.Command.Parameters.AddWithValue("@id_cartao", System.Convert.ToInt64(UiMainBIO.edCartao.Text));
                    sql_bd.Command.Parameters.AddWithValue("@template1", d1);
                    sql_bd.Command.Parameters.AddWithValue("@template2", d2);
                    sql_bd.Command.Parameters.AddWithValue("@dt_cad", DateTime.Now);

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
                    catch (OleDbException ex)
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

                //UiMainBIO.CarregaGrid();

                //Se capturou dedo 2 finaliza
                if (fLinha == 2)
                {
                    
                    UiMainBIO.btCapturar.Enabled = false;                    
                    UiMainBIO.btIniciar.Enabled = false;
                    Dedo1 = null;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }
            

                
        }
        #endregion

        //#region InserirUsuarioSemDigital
        ////***********************************************************************************
        ////Inserir usuário sem digital no leitor
        ////***********************************************************************************
        //internal static void InserirUsuarioSemDigital(FRdigital UiMainBIO)
        //{
        //    int Ret = -1;

        //    UiMainBIO.Cursor = Cursors.WaitCursor;
        //    Ret = EasyInner.IncluirUsuarioSemDigitalBio(UiMainBIO.txtUsuario.Value.ToString());

        //    if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
        //        MessageBox.Show("Usuário inserido com sucesso!");

        //    UiMainBIO.Cursor = Cursors.Default;
        //}
        //#endregion

        //#region EnviarListaUsuariosSemDigital
        ////***********************************************************************************
        ////Envio Lista de Usuários sem digital
        ////***********************************************************************************
        //internal static void EnviarListaUsuariosSemDigitail(FRdigital UiMainBIO)
        //{
        //    int Ret = -1;
        //    UiMainBIO.Cursor = Cursors.WaitCursor;
        //    if (Conectar(UiMainBIO))
        //    {
        //        Ret = EasyInner.EnviarListaUsuariosSemDigitalBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text));

        //        if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
        //            MessageBox.Show("Lista de usuários sem digital enviada com sucesso!");
        //    }
        //    EasyInner.FecharPortaComunicacao();
        //    UiMainBIO.Cursor = Cursors.Default;
        //    Application.DoEvents();
        //}
        //#endregion

        //#endregion

        ////

        //#region Métodos Auxiliares

        #region Conectar
        //***********************************************************************************
        //CONECTAR
        //Rotina responsável por efetuar a conexão com o Inner
        //***********************************************************************************
        private static bool Conectar(FRdigital UiMainBIO)
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
                    System.Windows.Forms.MessageBox.Show("Conectou ao Inner!", "");
                    Application.DoEvents();
                    return true;
                }
                else
                {
                    //Exibe mensagem de erro para o Usuário..  
                    UiMainBIO.lblManutencao.Text = "Erro ao conectar com o Inner!";
                    Application.DoEvents();
                    return false;
                }
            }
            else
            {                
                UiMainBIO.lblManutencao.Text = "Não conectou ao Inner!";
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

        //#region ValidaNumeroUsuario
        ////***********************************************************************************
        ////Preenche com 0 a esquerda caso não tenha 10 Números
        ////***********************************************************************************
        //private static bool ValidaNumeroUsuario(ref string NumUsuario)
        //{
        //    int saida;

        //    //Testa se é um Número
        //    if (int.TryParse(NumUsuario, out saida))
        //    {
        //        while (NumUsuario.Length < 10)
        //        {
        //            NumUsuario = "0" + NumUsuario;
        //        }
        //        return true;
        //    }
        //    else
        //        return false;
        //}
        //#endregion

        //#region TratarRetornoErro
        ////***********************************************************************************
        ////Apresenta a mensagem de acordo com o erro retornado
        ////***********************************************************************************
        //private static void TratarRetornoErro(int Ret)
        //{
        //    switch ((Enumeradores.RetornoBIO)Ret)
        //    {
        //        case Enumeradores.RetornoBIO.FALHA_NA_COMUNICACAO:
        //            MessageBox.Show("Erro: Falha na comunicação com o Inner BIO.", "Erro");
        //            break;
        //        case Enumeradores.RetornoBIO.PROCESSANDO_ULTIMO_COMANDO:
        //            MessageBox.Show("Atenção: Ainda processando último Comando.", "Erro");
        //            break;
        //        case Enumeradores.RetornoBIO.FALHA_NA_COMUNICACAO_COM_PLACA_BIO:
        //            MessageBox.Show("Erro: Falha na comunicação com a placa BIO.", "Erro");
        //            break;
        //        case Enumeradores.RetornoBIO.INNER_BIO_NAO_ESTA_EM_MODO_MASTER:
        //            MessageBox.Show("Erro: Inner BIO não esta em moddo MASTER.", "Erro");
        //            break;
        //        case Enumeradores.RetornoBIO.USUARIO_JA_CADASTRADO_NO_BANCO_DE_DADOS_INNER_BIO:
        //            MessageBox.Show("Erro: Usuário ja cadastrado no banco de dados do Inner BIO.", "Erro");
        //            break;
        //        case Enumeradores.RetornoBIO.USUARIO_NAO_CADASTRADO_NO_BANCO_DE_DADOS_INNER_BIO:
        //            MessageBox.Show("Erro: Usuário não cadastrado no banco de dados Inner BIO.", "Erro");
        //            break;
        //        case Enumeradores.RetornoBIO.BASE_DE_DADOS_DE_USUARIOS_ESTA_CHEIA:
        //            MessageBox.Show("Erro: Base de dados de Usuários esta cheia.", "Erro");
        //            break;
        //        case Enumeradores.RetornoBIO.ERRO_NO_SEGUNDO_DEDO_DO_USUARIO:
        //            MessageBox.Show("Erro: Erro no segundo dedo do Usuário.", "Erro");
        //            break;
        //        case Enumeradores.RetornoBIO.SOLICITACAO_PARA_INNER_BIO_INVALIDA:
        //            MessageBox.Show("Erro: Solicitação para Inner BIO Inválida.", "Erro");
        //            break;
        //        default:
        //            MessageBox.Show("Erro: Mensagem Indefinida", "Erro");
        //            break;
        //    }
        //}

        //#endregion

        //#region RemoverInnerLista
        ////***********************************************************************************
        ////Remove da memória inner selecionado
        ////***********************************************************************************
        //internal static void RemoverInnerLista(FRdigital UiMainBIO)
        //{
        //    UiBIO = UiMainBIO;

        //    if (UiBIO.lstInnersCadastrados.SelectedItem != null)
        //    {
        //        foreach (Inner inner in UiBIO.LstInners)
        //        {
        //            if (inner.Numero == ((Inner)UiBIO.lstInnersCadastrados.SelectedItem).Numero)
        //            {
        //                UiBIO.LstInners.Remove(inner);
        //                UiBIO.lstInnersCadastrados.Items.Remove(UiBIO.lstInnersCadastrados.SelectedItem);
        //                if (UiMainBIO.lstInnersCadastrados.Items.Count > 0)
        //                {
        //                    UiMainBIO.lstInnersCadastrados.SelectedIndex = UiMainBIO.lstInnersCadastrados.Items.Count - 1;
        //                }
        //                MessageBox.Show("Inner removido da memória!");
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("É necessário selecionar um Inner para remover da memória!");
        //    }
        //}
        //#endregion

        #region BioLight
        //***********************************************************************************
        //Retorna o modelo BioLight
        //***********************************************************************************
        private static bool BioLight(FRdigital UiMainBIO)
        {
            bool placaLight = false;
            int Ret1 = -1;
            int modelo = 0;

            CSconf_catraca cscatraca = new CSconf_catraca();

            //Solicita Modelo
            Ret1 = EasyInner.SolicitarModeloBio(cscatraca.def_NumInner);
            SetarTimeoutBio();
            UiMainBIO.lblManutencao.Text = "";
            do
            {
                if (Ret1 == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Thread.Sleep(5);

                    //Recebe Modelo
                    Ret1 = EasyInner.ReceberModeloBio(cscatraca.def_NumInner, 0, ref modelo);
                    if (Ret1 == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        UiMainBIO.lblManutencao.RightToLeft = RightToLeft.No;
                        switch (modelo)
                        {
                            case 2:
                                UiMainBIO.lblManutencao.Text = "Modelo do bio: Light usuários  := FIM10).";
                                placaLight = true;
                                break;
                            case 4: UiMainBIO.lblManutencao.Text = "Modelo do bio: 1000/4000 usuários  := FIM01).";
                                break;
                            case 51: UiMainBIO.lblManutencao.Text = "Modelo do bio: 1000/4000 usuários  := FIM2030).";
                                break;
                            case 52: UiMainBIO.lblManutencao.Text = "Modelo do bio: 1000/4000 usuários  := FIM2040).";
                                break;
                            case 48:
                                UiMainBIO.lblManutencao.Text = "Modelo do bio: Light 100 usuários  := FIM3030).";
                                placaLight = true;
                                break;
                            case 64:
                                UiMainBIO.lblManutencao.Text = "Modelo do bio: Light 100 usuários  := FIM3040).";
                                placaLight = true;
                                break;
                            case 80: UiMainBIO.lblManutencao.Text = "Modelo do bio: 1000/4000 usuários FIM5060.";
                                break;
                            case 82: UiMainBIO.lblManutencao.Text = "Modelo do bio: 1000/4000 usuários FIM5260.";
                                break;
                            case 83:
                                UiMainBIO.lblManutencao.Text = "Modelo do bio: Light 100 usuários FIM5360.";
                                placaLight = true;
                                break;
                            case 255: UiMainBIO.lblManutencao.Text = "Modelo do bio: Desconhecido";
                                break;
                        }
                    }
                }
            } while (EsperaRespostaBio(Ret1));
            return placaLight;
        }
        #endregion

        //#region inserirTemplateUsr
        ////***********************************************************************************
        ////O usuário será cadastrado no Inner bio com o número do cartão
        ////Retorna o resultado
        ////***********************************************************************************
        //private static bool inserirTemplateUsr(FRdigital UiMainBIO, String Usr, int numTpl)
        //{
        //    bool Retorno = false;
        //    int Ret = -1;

        //    //Solicita inserção
        //    Ret = EasyInner.InserirUsuarioLeitorBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text), (byte)numTpl, Usr);

        //    Thread.Sleep(1000);
        //    SetarTimeoutBio();

        //    do
        //    {
        //        Thread.Sleep(20);
        //        //Retorna resultado inserção
        //        Ret = EasyInner.ResultadoInsercaoUsuarioLeitorBio(System.Convert.ToInt32(UiMainBIO.txtNumInner.Text), 0);

        //    } while (EsperaRespostaBio(Ret));

        //    if (numTpl == 0)
        //    {
        //        numTpl++;
        //    }

        //    UiMainBIO.lblManutencao.Items.Clear();

        //    //Resultado do cadastro
        //    switch (Ret)
        //    {
        //        case (byte)Enumeradores.Retorno.RET_COMANDO_OK:
        //            Retorno = true;
        //            MessageBox.Show("Digital " + numTpl + " capturada com sucesso.");
        //            break;
        //        case (byte)Enumeradores.Retorno.RET_BIO_USR_JA_CADASTRADO:
        //            MessageBox.Show("Usuário já existe.");
        //            UiMainBIO.lblManutencao.Items.Add("Selecione um Comando");
        //            break;
        //        case (byte)Enumeradores.Retorno.RET_BIO_BASE_CHEIA:
        //            MessageBox.Show("Não é possível incluir novo usuário, a base está cheia.");
        //            UiMainBIO.lblManutencao.Items.Add("Selecione um Comando");
        //            break;
        //        case (byte)Enumeradores.Retorno.RET_BIO_FALHA_COMUNICACAO:
        //            MessageBox.Show("Houve falha de comunicação, favor repetir o comando.");
        //            UiMainBIO.lblManutencao.Items.Add("Selecione um Comando");
        //            break;
        //        case (byte)Enumeradores.Retorno.RET_DIG_NAO_CONFERE:
        //            MessageBox.Show("As digitais não conferem");
        //            UiMainBIO.lblManutencao.Items.Add("Selecione um Comando");
        //            break;
        //    }

        //    return (Retorno);
        //}
        //#endregion

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

        //#endregion
    }


}
