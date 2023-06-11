using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;


using EmatWinFormsNetFramework13032.Digitais.Entity;
using EmatWinFormsNetFramework13032.Digitais.COM;
using System.Threading;

namespace EmatWinFormsNetFramework13032.Digitais.FrmOnline
{
    public class CSreceber_dig_catraca
    {

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
            UiBIO.Timeout1 = 0;
            UiBIO.Timeout1 = (int)EasyInner.RetornarSegundosSys() + 7;
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

        #region UiBIO
        private static FrmOnline _uiMainBio;
        public static FrmOnline UiBIO
        {
            get { return _uiMainBio; }
            set { _uiMainBio = value; }
        }
        #endregion

        public static void ReceberUsuariosBIO_PC(FrmOnline UiMainBIO)
        {
            UiBIO = UiMainBIO;

            StringBuilder Usuario;
            int nPacote = 0;
            int nUsuario = 0;
            int Ret = -1;

            //Atribui a janela principal a variável da controller..
            UiBIO = UiMainBIO;

            
            UiMainBIO.lblManutencao.Text = "Recebendo todos os Usuários...";
            
            Application.DoEvents();
            UiMainBIO.Cursor = Cursors.WaitCursor;

            if (Conectar(UiMainBIO))
            {
            
                //Limpa lista de usuários
                UiMainBIO.lstUsuarios.Items.Clear();

                //Inicia coleta usuários
                EasyInner.InicializarColetaListaUsuariosBio();

                while (EasyInner.TemProximoPacote() == 1)
                {
                    SetarTimeoutBio();
                    do
                    {
                        Thread.Sleep(20);

                        //Solicita uma parte(pacote) da lista de usuarios do bio
                        Ret = EasyInner.SolicitarListaUsuariosBio(1);
                        
                        UiMainBIO.ltbEnviar.Text="Solicitando pacote...";
                        
                        Application.DoEvents();

                    } while (EsperaRespostaBio(Ret));//se ainda estava

                    if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        //Recebe uma parte da lista com os usuarios
                        SetarTimeoutBio();
                        do
                        {
                            Thread.Sleep(500);
                            Ret = EasyInner.ReceberPacoteListaUsuariosBio(1);

                            UiMainBIO.lblManutencao.Text = "Recebendo pacote: " + System.Convert.ToInt32(nPacote + 1);
                            
                            Application.DoEvents();

                        } while (EsperaRespostaBio(Ret));

                        if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                        {
                            nPacote++;

                            UiMainBIO.lblManutencao.Text="Recebeu pacote: " + System.Convert.ToInt32(nPacote);
                           
                            Application.DoEvents();
                        }
                        else
                        {
                            continue;
                        }

                        
                        Thread.Sleep(50);

                        //Verifica se existe um usuario
                        while (EasyInner.TemProximoUsuario() == 1)
                        {
                            Usuario = null;
                            Usuario = new StringBuilder();

                            //Pede um usuario da lista
                            Ret = EasyInner.ReceberUsuarioLista(1, Usuario);

                            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                            {
                                nUsuario++;
                                //Insere o usuario no listbox
                                UiMainBIO.lstUsuarios.Items.Add(Usuario);
                                CSdig_lista.usuarios_catraca.Add(Usuario.ToString());
                            }
                        }

                        
                    }
                    else
                    {
                        
                      
                        //Em caso de erro
                        switch (Ret)
                        {
                            case (byte)Enumeradores.Retorno.RET_COMANDO_ERRO:
                                UiMainBIO.ltbEnviar.Items.Add("Erro ao abrir porta de comunicação");
                                break;
                            case (byte)Enumeradores.Retorno.RET_PORTA_NAOABERTA:
                                UiMainBIO.ltbEnviar.Items.Add("Porta não aberta");
                                break;
                            case (byte)Enumeradores.Retorno.RET_PORTA_JAABERTA:
                                UiMainBIO.ltbEnviar.Items.Add("Porta já aberta");
                                break;
                            case (byte)Enumeradores.Retorno.RET_DLL_INNER2K_NAO_ENCONTRADA:
                                UiMainBIO.ltbEnviar.Items.Add("DLL Inner2k não encontrada");
                                break;
                            case (byte)Enumeradores.Retorno.RET_DLL_INNERTCP_NAO_ENCONTRADA:
                                UiMainBIO.ltbEnviar.Items.Add("DLL InnerTCP não encontrada");
                                break;
                            case (byte)Enumeradores.Retorno.RET_DLL_INNERTCP2_NAO_ENCONTRADA:
                                UiMainBIO.ltbEnviar.Items.Add("DLL InnerTCP2 não encontrada");
                                break;
                            case (byte)Enumeradores.Retorno.RET_ERRO_GPF:
                                UiMainBIO.ltbEnviar.Items.Add("Ocorreu um erro dentro da DLL");
                                break;
                            case (byte)Enumeradores.Retorno.RET_TIPO_CONEXAO_INVALIDA:
                                UiMainBIO.ltbEnviar.Items.Add("Tipo de conexão inválida");
                                break;
                            default:
                                UiMainBIO.ltbEnviar.Items.Add("Erro " + Ret.ToString());
                                break;
                        }
                    }
                    Application.DoEvents();

                    
                }
            }

            UiMainBIO.lblManutencao.Text = "Recebeu " + nUsuario.ToString() + " usuários";
            EasyInner.FecharPortaComunicacao();

            UiMainBIO.Cursor = Cursors.Default;
        }
    }
}
