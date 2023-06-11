//******************************************************************************
//A Topdata Sistemas de Automa��o Ltda n�o se responsabiliza por qualquer
//tipo de dano que este software possa causar, este exemplo deve ser utilizado
//apenas para demonstrar a comunica��o com os equipamentos da linha
//inner e n�o deve ser alterado, por este motivo ele n�o deve ser incluso em
//suas aplica��es comerciais.
//
//Desenvolvido em C#.
//                                           Topdata Sistemas de Automa��o Ltda.
//******************************************************************************


//******************************************************************************
//Comunica��o com a DLL "EasyInner.dll"
//Todos os m�todos est�o descritos no manual do desenvolvedor que acompanha
//a instala��o da SDK
//******************************************************************************

using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EmatWinFormsNetFramework13032.Digitais.COM
{
    /// <summary>
    /// DLL que realiza troca de mensagens com Inners TOPDATA
    /// </summary>
    class EasyInner
    {
        #region M�todos da EasyInner


        #region RetornarSegundosSys

        [DllImport("Kernel32", CallingConvention = CallingConvention.Winapi)]
        private static extern int GetTickCount();
        
        /// <summary>
        /// M�todo que retorna os segundos do Sistema.
        /// </summary>
        /// <returns></returns>
        public static long RetornarSegundosSys()
        {
            return (long)(GetTickCount() / 1000);
        }
        #endregion

        #region COMANDOS DIRETOS
        
        #region DefinirTipoConexao
        /// <summary>
        /// Define qual ser� o tipo de conex�o(meio de comunica��o) que ser� utilizada pela dll para
        ///comunicar com os Inners. Essa fun��o dever� ser chamada antes de iniciar o processo de comunica��o
        ///e antes da fun��o AbrirPortaComunicacao.
        /// </summary>
        /// <param name="Tipo"> 0 - Comunica��o serial, RS-232/485. 
                            /// 1 - Comunica��o TCP/IP com porta vari�vel.
                            /// 2 - Comunica��o TCP/IP com porta fixa (Default).
                            /// 3 - Comunica��o via modem.
                            /// 4 � Comunica��o via TopPendrive.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirTipoConexao(byte Tipo);
        #endregion

        #region AbrirPortaComunicacao
        /// <summary>
        /// Abre a porta de comunica��o desejada, essa fun��o dever� ser chamada antes de iniciar
        ///qualquer processo de transmiss�o ou recep��o de dados com o Inner.Esta fun��o deve ser chamada apenas uma vez e no in�cio da comunica��o, e n�o deve ser chamada
        ///para cada Inner.
        /// </summary>
        /// <param name="Porta">N�mero da porta serial ou TCP/IP.
        ///- Para comunica��o TCP/IP o valor padr�o da porta � 3570 (Default).
        ///- Para comunica��o Serial/Modem o valor padr�o da porta � 1, COM 1(Default).
        ///- Para a comunica��o TopPendrive o valor � 3 (Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte AbrirPortaComunicacao(int Porta);
        #endregion

        #region FecharPortaComunicacao
        /// <summary>
        /// Fecha a porta de comunica��o previamente aberta, seja ela serial, Modem ou TCP/IP.
        ///Em modo Off-Line normalmente � chamada ap�s enviar/receber todos os dados do Inner.
        ///Em modo On-Line � chamada somente no ecerramento do software do software.
        /// </summary>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern void FecharPortaComunicacao();
        #endregion

        #region DefinirPadraoCartao
        /// <summary>
        /// Define qual padr�o de cart�o ser� utilizado pelos Inners, padr�o Topdata ou padr�o livre. O
        ///padr�o Topdata de cart�o est� descrito no manual dos equipamentos e � utilizado somente com os
        ///Inners em modo Off-Line. No padr�o livre todos os d�gitos do cart�o s�o considerados como matr�cula,
        ///ele pode ser utilizado no modo On Line ou no modo Off Line.
        ///Ao chamar essa fun��o, a quantidade de d�gitos � setada para 14.
        /// </summary>
        /// <param name="Padrao">0 - Padr�o Topdata. 
                              ///1 - Padr�o livre (Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirPadraoCartao(byte Padrao);
        #endregion

        #region AcionarRele1
        /// <summary>
        /// Aciona(atraca) o rele 1 do Inner. Este comando n�o deve ser utilizado nas catracas.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte AcionarRele1(int Inner);
        #endregion

        #region AcionarRele2
        /// <summary>
        /// Aciona(atraca) o rele 2 do Inner. Este comando n�o deve ser utilizado nas catracas.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte AcionarRele2(int Inner);
        #endregion 

        #region ManterRele1Acionado
        /// <summary>
        /// Mant�m acionado(atracado) o rele 1 do Inner at� que o comando DesabilitarRele1 seja
        ///enviado. Este comando n�o deve ser utilizado nas catracas.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ManterRele1Acionado(int Inner);
        #endregion
        
        #region ManterRele2Acionado
        /// <summary>
        /// Mant�m acionado(atracado) o rele 2 do Inner at� que o comando DesabilitarRele2 seja
        ///enviado. Este comando n�o deve ser utilizado nas catracas.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ManterRele2Acionado(int Inner);
        #endregion 
        
        #region DesabilitarRele1
        /// <summary>
        /// Desaciona(desatraca) o rele 1 previamente acionado com o comando
        ///ManterRele1Acionado. Este comando n�o deve ser utilizado nas catracas.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DesabilitarRele1(int Inner);
        #endregion
        
        #region DesabilitarRele2
        /// <summary>
        /// Desaciona(desatraca) o rele 2 previamente acionado com o comando
        ///ManterRele2Acionado. Este comando n�o deve ser utilizado nas catracas.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DesabilitarRele2(int Inner);
        #endregion
        
        #region AcionarBipCurto
        /// <summary>
        /// Faz com que o Inner emita um bip curto(aviso sonoro).
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte AcionarBipCurto(int Inner);
        #endregion
        
        #region AcionarBipLongo
        /// <summary>
        /// Faz com que o Inner emita um bip longo(aviso sonoro).
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte AcionarBipLongo(int Inner);
        #endregion
        
        #region Ping
        /// <summary>
        /// Testa a comunica��o com o Inner, tamb�m utilizado para efetuar a conex�o com o Inner.
        ///Para efetuar a conex�o com o Inner, essa fun��o deve ser executada em um loop at� retornar 0(zero),
        ///executado com sucesso.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte Ping(int Inner);
        #endregion

        #region PingOnLine
        /// <summary>
        /// Testa comunica��o com o Inner e mant�m o Inner em OnLine quando a mudan�a autom�tica
        ///est� configurada. Especialmente indicada para a verifica��o da conex�o em comunica��o TCP/IP.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte PingOnLine(int Inner);
        #endregion

        #region LigarBackLite
        /// <summary>
        /// Liga a luz emitida pelo display do Inner. Essa fun��o n�o deve ser utilizada nas catracas.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LigarBackLite(int Inner);
        #endregion
        
        #region DesligarBackLite
        /// <summary>
        /// Desliga a luz emitida pelo display do Inner. Essa fun��o n�o deve ser utilizada nas catracas.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DesligarBackLite(int Inner);
        #endregion
        
        #region LigarBipIntermitente
        /// <summary>
        /// Faz com que o Inner acione o bip de forma intermitente, ou seja, o Inner ir� emitir um aviso
        ///sonoro repetidamente. Essa fun��o n�o deve ser utilizada nas catracas.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LigarBipIntermitente(int Inner);
        #endregion
        
        #region DesligarBipIntermitente
        /// <summary>
        /// Faz com que o Inner desabilite o bip acionado pela fun��o LigarBipIntermitente. Essa
        ///fun��o n�o deve ser utilizada nas catracas.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DesligarBipIntermitente(int Inner);
        #endregion
        
        #region LiberarCatracaEntrada
        /// <summary>
        /// Libera a catraca no sentido de entrada padr�o do Inner, para o usu�rio poder efetuar o giro
        ///na catraca. Em modo On-Line, na fun��o ReceberDadosOnLine o valor retornado no par�metro
        ///�Complemento� ser� do tipo entrada.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LiberarCatracaEntrada(int Inner);
        #endregion
        
        #region LiberarCatracaSaida
        /// <summary>
        /// Libera a catraca no sentido de sa�da padr�o do Inner, para o usu�rio poder efetuar o giro na
        ///catraca. Em modo On-Line, na fun��o ReceberDadosOnLine o valor retornado no par�metro
        ///�Complemento� ser� do tipo sa�da. 
        /// Essa fun��o deve ser utilizada somente com Inners Catraca.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LiberarCatracaSaida(int Inner);
        #endregion
        
        #region LiberarCatracaEntradaInvertida
        /// <summary>
        /// Libera a catraca no sentido contr�rio a entrada padr�o do Inner, para o usu�rio poder efetuar
        ///o giro na catraca. Em modo On-Line, na fun��o ReceberDadosOnLine o valor retornado no par�metro
        ///�Complemento� ser� do tipo sa�da.
        /// Essa fun��o deve ser utilizada somente com Inners Catraca.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LiberarCatracaEntradaInvertida(int Inner);
        #endregion
        
        #region LiberarCatracaSaidaInvertida
        /// <summary>
        /// Libera a catraca no sentido contr�rio a sa�da padr�o do Inner, para o usu�rio poder efetuar o
        ///giro na catraca. Em modo On-Line, na fun��o ReceberDadosOnLine o valor retornado no par�metro
        ///�Complemento� ser� do tipo entrada.
        ///Essa fun��o deve ser utilizada somente com Inners Catraca.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LiberarCatracaSaidaInvertida(int Inner);
        #endregion
        
        
        #region LiberarCatracaDoisSentidos
        /// <summary>
        /// Libera a catraca para o usu�rio pode efetuar o giro na catraca em ambos os sentidos. Em
        ///modo On-Line, na fun��o ReceberDadosOnLine o valor retornado no par�metro �Complemento� ser�
        ///do tipo entrada ou sa�da, dependendo do sentido em que o usu�rio passar pela catraca.
        ///Essa fun��o deve ser utilizada somente com Inners Catraca.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LiberarCatracaDoisSentidos(int Inner);
        #endregion

        #region LevantarParaOnLine
        /// <summary>
        /// Ap�s o Inner cair para Off-Line e recuperar a conex�o, este comando j� inicializa o Inner para
        ///On-Line sem a necessidade de enviar o comando de configura��es novamente.
        ///Para esta fun��o funcionar corretamente, � necess�rio que o Inner tenha sido configurado com a fun��o
        ///EnviarConfiguracoesMudancaAutomaticaOnLineOffLine previamente.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LevantarParaOnLine(int Inner);
        #endregion
        
        
        #region ReceberVersaoFirmware
        /// <summary>
        /// Solicita a vers�o do firmware do Inner e dados como o Idioma, se � uma vers�o especial.
        /// </summary>
        /// <param name="Inner">N�mero do Inner desejado.</param>
        /// <param name="Linha">01 � Inner Plus.
        ///02 � Inner Disk.
        ///03 � Inner Verid.
        ///06 � Inner Bio.
        ///07 � Inner NET.</param>
        /// <param name="Variacao">Depende da vers�o, existe somente em vers�es customizadas.</param>
        /// <param name="VersaoAlta">00 a 99.</param>
        /// <param name="VersaoBaixa">00 a 99.</param>
        /// <param name="VersaoSufixo">Indica o idioma do firmware:
        ///01 � Portugu�s.
        ///02 � Espanhol.
        ///03 � Ingl�s.
        ///04 � Franc�s.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberVersaoFirmware(int Inner, ref byte Linha, ref short Variacao, ref byte VersaoAlta, ref byte VersaoBaixa, ref byte VersaoSufixo, ref byte Ruf);

        #endregion 
        
        #endregion

        #region FUN��ES DE CONFIGURA��ES GERAIS DOS INNERS

        #region ConfigurarInnerOffLine
        /// <summary>
        /// Prepara o Inner para trabalhar no modo Off-Line, por�m essa fun��o ainda n�o envia essa
        ///informa��o para o equipamento.
        /// </summary>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarInnerOffLine();
        #endregion

        #region ConfigurarInnerOnLine
        /// <summary>
        /// Prepara o Inner para trabalhar no modo On-Line, por�m essa fun��o ainda n�o envia essa
        ///informa��o para o equipamento.
        /// </summary>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarInnerOnLine();
        #endregion

        #region HabilitarTeclado
        /// <summary>
        /// Permite que os dados sejam inseridos no Inner atrav�s do teclado do equipamento.
        ///Habilitando o par�metro ecoar, o teclado ir� ecoar asteriscos no display do Inner.
        /// </summary>
        /// <param name="Habilita">0 - Desabilita o teclado (Default).
                                ///1 - Habilita o teclado.</param>
        /// <param name="Ecoar">0 � Ecoa o que � digitado no display do Inner (Default).
                             ///1 � Ecoa asteriscos no display do Inner.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte HabilitarTeclado(byte Habilita, byte Ecoar);
        #endregion

        #region ConfigurarAcionamento1
        /// <summary>
        /// Configura como ir� funcionar o acionamento(rele) 1 do Inner, e por quanto tempo ele ser� acionado.
        /// </summary>
        /// <param name="Funcao">  0 � N�o utilizado(Default).
                                ///1 � Aciona ao registrar uma entrada ou sa�da.
                                ///2 � Aciona ao registrar uma entrada.
                                ///3 � Aciona ao registrar uma sa�da.
                                ///4 � Est� conectado a uma sirene(Ver as fun��es de sirene).
                                ///5 � Utilizado para a revista de usu�rios(Ver a fun��o DefinirPorcentagemRevista).
                                ///6 � Catraca com a sa�da liberada.
                                ///7 � Catraca com a entrada liberada
                                ///8 � Catraca liberada nos dois sentidos.
                                ///9 � Catraca liberada nos dois sentidos e a marca��o(registro) � gerada de acordo com o sentido do giro.</param>
        /// <param name="Tempo">0 a 50 segundos.
                             ///0(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarAcionamento1(byte Funcao, byte Tempo);
        #endregion

        #region ConfigurarAcionamento2
        /// <summary>
        /// Configura como ir� funcionar o acionamento(rele) 2 do Inner, e por quanto tempo ele ser� acionado.
        /// </summary>
        /// <param name="Funcao">  0 � N�o utilizado(Default).
                                ///1 � Aciona ao registrar uma entrada ou sa�da.
                                ///2 � Aciona ao registrar uma entrada.
                                ///3 � Aciona ao registrar uma sa�da.
                                ///4 � Est� conectado a uma sirene(Ver as fun��es de sirene).
                                ///5 � Utilizado para a revista de usu�rios(Ver a fun��o DefinirPorcentagemRevista).
                                ///6 � Catraca com a sa�da liberada.
                                ///7 � Catraca com a entrada liberada
                                ///8 � Catraca liberada nos dois sentidos.
                                ///9 � Catraca liberada nos dois sentidos e a marca��o(registro) � gerada de acordo com o sentido do giro.</param>
        /// <param name="Tempo">0 a 50 segundos.
                             ///0(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarAcionamento2(byte Funcao, byte Tempo);
        #endregion

        #region ConfigurarTipoLeitor
        /// <summary>
        /// Configura o tipo do leitor que o Inner est� utilizando, se � um leitor de c�digo de barras,
        /// magn�tico ou proximidade.
        /// </summary>
        /// <param name="Tipo">0 � Leitor de c�digo de barras(Default).
                            ///1 � Leitor Magn�tico.
                            ///2 � Leitor proximidade AbaTrack2.
                            ///3 � Leitor proximidade Wiegand e Wiegand Facility Code.
                            ///4 � Leitor proximidade Smart Card.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarTipoLeitor(byte Tipo);
        #endregion

        #region ConfigurarLeitor1
        /// <summary>
        /// Configura as opera��es que o leitor ir� executar. Se ir� registrar os dados somente como
        ///entrada independente do sentido em que o cart�o for passado, somente como sa�da ou como entrada e sa�da.
        /// </summary>
        /// <param name="Operacao">0 � Leitor desativado(Default).
                                ///1 � Somente para entrada.
                                ///2 � Somente para sa�da.
                                ///3 � Entrada e sa�da.
                                ///4 � Entrada e sa�da invertidas.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarLeitor1(byte Operacao);
        #endregion

        #region ConfigurarLeitor2
        /// <summary>
        /// Configura as opera��es que o leitor ir� executar. Se ir� registrar os dados somente como
        ///entrada independente do sentido em que o cart�o for passado, somente como sa�da ou como entrada e sa�da.
        /// </summary>
        /// <param name="Operacao">0 � Leitor desativado(Default).
                                ///1 � Somente para entrada.
                                ///2 � Somente para sa�da.
                                ///3 � Entrada e sa�da.
                                ///4 � Entrada e sa�da invertidas.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarLeitor2(byte Operacao);
        #endregion

        #region DefinirCodigoEmpresa
        /// <summary>
        /// Define o c�digo da empresa utilizado nos cart�es, v�lido somente quando se est� utilizando
        ///o padr�o Topdata de cart�o.
        /// </summary>
        /// <param name="Codigo">0 a 999.
                              ///0(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirCodigoEmpresa(int Codigo);
        #endregion

        #region DefinirNivelAcesso
        /// <summary>
        /// Define o n�vel de acesso aceito por este Inner, deve ser utilizado somente nos Inners que
        ///est�o configurados para utilizar cart�es no padr�o Topdata.
        /// </summary>
        /// <param name="Nivel">0 a 9.
                             ///0(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirNivelAcesso(byte Nivel);
        #endregion

        #region UtilizarSenhaAcesso
        /// <summary>
        /// Configura o Inner para solicitar a senha de acesso cadastrada no cart�o do usu�rio, essa
        ///op��o � v�lida somente para Inners que estejam configurados para utilizar o padr�o Topdata de cart�o.
        /// </summary>
        /// <param name="Utiliza">0 � N�o solicita a senha de acesso(Default).
                               ///1 � Solicita a senha de acesso.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte UtilizarSenhaAcesso(byte Utiliza);
        #endregion

        #region DefinirTipoListaAcesso
        /// <summary>
        /// Define qual tipo de lista(controle) de acesso o Inner vai utilizar. Ap�s habilitar a lista de
        ///acesso � necess�rio preencher a lista e os hor�rios de acesso, verificar os as fun��es de �Hor�rios de
        ///Acesso� e as fun��es da �Lista de Acesso�.
        /// </summary>
        /// <param name="Tipo">0 � N�o utilizar a lista de acesso.
                            ///1 � Utilizar lista branca(cart�es fora da lista tem o acesso negado).
                            ///2 � Utilizar lista negra(bloqueia apenas os cart�es da lista).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirTipoListaAcesso(byte Tipo);
        #endregion

        #region DefinirQuantidadeDigitosCartao
        /// <summary>
        /// Define a quantidade de d�gitos dos cart�es a serem lidos pelo Inner.
        /// </summary>
        /// <param name="Quantidade">1 a 16 d�gitos.
                                 ///14(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirQuantidadeDigitosCartao(byte Quantidade);
        #endregion

        #region AvisarQuandoMemoriaCheia
        /// <summary>
        /// Configura o Inner para avisar quando a mem�ria que armazena os bilhetes Off-Line estiver 50% cheia.
        /// </summary>
        /// <param name="Avisa">0 � Desabilita o aviso de mem�ria cheia(Default).
                             ///1 � Habilita o aviso de mem�ria cheia.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte AvisarQuandoMemoriaCheia(byte Avisa);
        #endregion

        #region DefinirPorcentagemRevista
        /// <summary>
        /// Define a porcentagem de cart�es que ser�o selecionados para a revista ao passarem pela
        ///sa�da do Inner. Quando um cart�o � selecionado o Inner emite um aviso sonoro, uma mensagem no
        ///display e aciona o acionamento(rele) que esteja habilitado para a revista(ver as fun��es
        ///ConfigurarAcionamento1 e ConfigurarAcionamento2).
        /// </summary>
        /// <param name="Porcentagem">0 a 100.
                                   ///0(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirPorcentagemRevista(byte Porcentagem);
        #endregion

        #region RegistrarAcessoNegado
        /// <summary>
        /// Configura o Inner para registrar as tentativa de acesso negado. O Inner ir� rgistrar apenas os
        ///acessos negados em rela��o a lista de acesso configurada para o modo Off-Line, ver as fun��es
        ///DefinirTipoListaAcesso e ColetarBilhete.
        /// </summary>
        /// <param name="TipoRegistro">0 � N�o registrar o acesso negado.
                                    ///1 � Apenas o acesso negado.
                                    ///2 � Falha na valida��o da digital.
                                    ///3 � Acesso negado e falha na valida��o da digital.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte RegistrarAcessoNegado(byte TipoRegistro);
        #endregion

        #region CartaoMasterLiberaAcesso
        /// <summary>
        /// Configura o Inner para permitir que o cart�o master(cart�o mestre) libere o acesso para
        ///cart�es que est�o bloqueados pela lista de acesso. O cart�o mestre do Inner deve ser informado atrav�s
        ///da fun��o DefinirNumeroCartaoMaster.
        /// </summary>
        /// <param name="Libera">0 � N�o libera o acesso(Default).
                              ///1 � Libera o acesso.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte CartaoMasterLiberaAcesso(byte Libera);
        #endregion

        #region DefinirLogicaRele
        /// <summary>
        /// OBSOLETA:
        /// Define o l�gica que o Inner ir� utilizar nos reles(acionamentos), se os reles ficar�o
        ///normalmente abertos(NA), ou seja, os reles ficar�o desacionados, ou se os reles ficar�o normalmente
        ///fechados(NF), ou seja, os reles ficar�o acionados.
        ///� altamente recomend�vel n�o alterar esses valores, a n�o ser que seja necess�rio. Essa fun��o �
        ///obsoleta e n�o deve ser utilizada.
        /// </summary>
        /// <param name="Logica">0 � Normalmente aberto.
                              ///1 � Normalmente fechado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirLogicaRele(byte Logica);
        #endregion

        #region DesabilitarBloqueioCatracaMicroSwitch
        /// <summary>
        /// OBSOLETA:
        /// Fun��o obsoleta, n�o � mais utilizada. Essa fun��o era utilizada por catracas muito antigas,
        ///ela configurava a catraca para n�o bloquear automaticamente a passagem for�ada pela catraca.
        /// </summary>
        /// <param name="Desabilita">0 � Habilita o bloqueio(Default).
                                  ///1 � Desabilita o bloqueio autom�tico.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DesabilitarBloqueioCatracaMicroSwitch(byte Desabilita);
        #endregion

        #region DefinirFuncaoDefaultLeitoresProximidade
        /// <summary>
        /// Define qual ser� o tipo do registro realizado pelo Inner ao aproximar um cart�o do tipo
        ///proximidade no leitor do Inner, sem que o usu�rio tenha pressionado a tecla entrada, sa�da ou fun��o.
        /// </summary>
        /// <param name="Funcao">0 � Desablitado(Default).
                              ///1 a 9 � Registrar como uma fun��o do teclado do Inner.
                              ///10 � Registrar sempre como entrada.
                              ///11 � Registrar sempre como sa�da.
                              ///12 � Libera a catraca nos dois sentidos e registra o bilhete conforme o sentido giro.
        /// </param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirFuncaoDefaultLeitoresProximidade(byte Funcao);
        #endregion

        #region DefinirNumeroCartaoMaster
        /// <summary>
        /// Configura qual ser� o n�mero do cart�o master que o Inner ir� aceitar. V�lido somente para
        ///o padr�o livre de cart�o. Para o padr�o Topdata o n�mero do master sempre � 0.
        /// </summary>
        /// <param name="Master">0 a 99999999999999 (M�ximo de 14 d�gitos)
                              ///0(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirNumeroCartaoMaster(string Master);
        #endregion

        #region DefinirFormasPictogramasMillenium
        /// <summary>
        /// OBSOLETA:
        /// Fun��o obsoleta, n�o deve ser mais utilizada. Essa fun��o configura a forma dos led�s dos
        ///pictogramas laterais da antiga catraca millenium.
        /// </summary>
        /// <param name="Forma">Valor Lado1 Lado2
                            ///0 Seta Seta
                            ///1 Seta Negado
                            ///2 Negado Seta
                            ///3 Negado Negado
                            ///4 Seta Apagado
                            ///5 Apagado Seta
                            ///6 Negado Apagado
                            ///7 Apagado Negado
                            ///8 Apagado Apagado</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirFormasPictogramasMillenium(byte Forma);
        #endregion

        #region DesabilitarBipCatraca
        /// <summary>
        /// Desabilita o bip cont�nuo utilizada pela catraca para avisar que algu�m est� for�ando a
        ///passagem pelo equipamento.
        /// </summary>
        /// <param name="Desabilita">0 � N�o desabilita o bip(Default).
                                  ///1 � Desabilita bip.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DesabilitarBipCatraca(byte Desabilita);
        #endregion

        #region DefinirEventoSensor
        /// <summary>
        /// Altera a forma de como os eventos dos sensores do Inner dever�o ser disparados. N�o �
        ///acomselh�vel alterar estes valores, a n�o ser que seja extramamente necess�rio. N�o funciona nas catracas.
        /// </summary>
        /// <param name="Sensor">1 a 3, n�mero do sensor a ser configurado.</param>
        /// <param name="Evento">0 � n�o gera evento(Default).
                              ///1 � gera evento de subida (0->1).
                              ///2 � gera evento de descida (1->0).
                              ///3 � ambos.
                              ///4 � subida acionando bip.
                              ///5 � descida acionando bip.</param>
        /// <param name="Tempo">1 a 50, tempo para acionar o bip ap�s o evento ocorrer.
                             ///0(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirEventoSensor(byte Sensor, byte Evento, byte Tempo);
        #endregion

        #region PermitirCadastroInnerBioVerid
        /// <summary>
        /// Permite que os cadastros de novos usu�rio sejam realizados pelo menu do cart�o master,
        ///apenas para Inners da linha Bio e Verid.
        /// </summary>
        /// <param name="Permite">0 � N�o permitir cadastro(Default).
                               ///1 � Permitir cadastro.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte PermitirCadastroInnerBioVerid(byte Permite);
        #endregion

        #region ReceberDataHoraDadosOnLine
        /// <summary>
        /// Configura o Inner para enviar as informa��es de data/hora nos bilhete on line, esses dados
        ///ser�o retornados nos par�metros da fun��o ReceberDadosOnLine.
        /// </summary>
        /// <param name="Recebe">0 � N�o receber a data/hora do bilhete(Default).
                              ///1 � Recebe a data/hora do bilhete.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberDataHoraDadosOnLine(byte Recebe);
        #endregion

        #region InserirQuantidadeDigitoVariavel
        /// <summary>
        /// Configura o Inner para ler cart�o que possam variar de 1 d�gito at� 16 d�gitos. Para habilitar a
        ///quantidade de d�gitos desejada basta chamar a fun��o passando o n�mero de d�gitos que o Inner dever� suportar.
        /// </summary>
        /// <param name="Digito">0 � Desabilita a leitura de cart�es com quantidade de d�gitos diferentes(Default).
                              ///1 a 16 � Quantidade de d�gitos a ser lida.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte InserirQuantidadeDigitoVariavel(byte Digito);
        #endregion

        #region ConfigurarWiegandDoisLeitores
        /// <summary>
        /// Habilita os leitores wiegand para o primeiro leitor e o segundo leitor do Inner, e configura se o
        ///segundo leitor ir� exibir as mensagens configuradas.
        /// </summary>
        /// <param name="Habilita">0 � N�o habilita o segundo leitor como wiegand(Default).
                                ///1 � Habilita o segundo leitor como wiegand.</param>
        /// <param name="ExibirMensagem">0 � N�o exibe mensagem segundo leitor(Default).
                                      ///1 � Exibe mensagem segundo leitor.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarWiegandDoisLeitores(byte Habilita, byte ExibirMensagem);
        #endregion

        #region DefinirFuncaoDefaultSensorBiometria
        /// <summary>
        /// Configura o tipo de registro que ser� associado a uma marca��o, quando for inserido o dedo
        ///no Inner bio sem que o usu�rio tenha definido se � um entrada, sa�da, fun��o, etc.
        /// </summary>
        /// <param name="Funcao">0 � desabilitada(Default).
                              ///1 a 9 � fun��es de 1 a 9.
                              ///10 � entrada.
                              ///11 � sa�da.
                              ///12 � libera catraca para os dois lados e registra bilhete conforme o giro.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirFuncaoDefaultSensorBiometria(byte Funcao);
        #endregion

        #region EnviarConfiguracoes
        /// <summary>
        /// Envia o buffer interno da dll que cont�m todas as configura��es das fun��es anteriores para
        ///o Inner, ap�s o envio esse buffer � limpo sendo necess�rio chamar novamentes as fun��es acima para
        ///reconfigur�-lo.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarConfiguracoes(int Inner);
        #endregion

        #endregion

        #region FUN��ES PARA MANIPULAR DATA/HORA INNER

        #region EnviarRelogio
        /// <summary>
        /// Configura o rel�gio(data/hora) do Inner.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Dia">1 a 31.</param>
        /// <param name="Mes">1 a 12.</param>
        /// <param name="Ano">0 a 99</param>
        /// <param name="Hora">0 a 23</param>
        /// <param name="Minuto">0 a 59</param>
        /// <param name="Segundo">0 a 59</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarRelogio(int Inner, byte Dia, byte Mes, byte Ano, byte Hora, byte Minuto, byte Segundo);
        #endregion

        #region ReceberRelogio
        /// <summary>
        /// Solicita a data/hora atualmente configurada no Inner. Os dados s�o retornados por refer�ncia
        ///nos par�metros da fun��o.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Dia">1 a 31.</param>
        /// <param name="Mes">1 a 12.</param>
        /// <param name="Ano">0 a 99</param>
        /// <param name="Hora">0 a 23</param>
        /// <param name="Minuto">0 a 59</param>
        /// <param name="Segundo">0 a 59</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberRelogio(int Inner, ref byte Dia, ref byte Mes, ref byte Ano, ref byte Hora, ref byte Minuto, ref byte Segundo);
        #endregion

        #region EnviarHorarioVerao
        /// <summary>
        /// Configura a data/hora de in�cio e fim do hor�rio de ver�o.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="DiaInicio">1 a 31</param>
        /// <param name="MesInicio">1 a 12</param>
        /// <param name="AnoInicio">0 a 99</param>
        /// <param name="HoraInicio">0 a 23</param>
        /// <param name="MinutoInicio">0 a 59</param>
        /// <param name="DiaFim">1 a 31</param>
        /// <param name="MesFim">1 a 12</param>
        /// <param name="AnoFim">0 a 99</param>
        /// <param name="HoraFim">0 a 23</param>
        /// <param name="MinutoFim">0 a 59</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarHorarioVerao(int Inner, byte DiaInicio, byte MesInicio, byte AnoInicio, byte HoraInicio, byte MinutoInicio, byte DiaFim, byte MesFim, byte AnoFim, byte HoraFim, byte MinutoFim);
        #endregion

        #endregion

        #region FUN��ES PARA MANIPULAR HOR�RIOS DE ACESSO

        #region ApagarHorariosAcesso
        /// <summary>
        /// Apaga o buffer com a lista de hor�rios de acesso e envia automaticamente para o Inner.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ApagarHorariosAcesso(int Inner);
        #endregion

        #region InserirHorarioAcesso
        /// <summary>
        /// Insere no buffer da dll um hor�rio de acesso. O Inner possui uma tabela de 100 hor�rios de
        ///acesso, para cada hor�rio � poss�vel definir 4 faixas de acesso para cada dia da semana.
        /// </summary>
        /// <param name="Horario">1 a 100 � N�mero da tabela de hor�rios.</param>
        /// <param name="DiaSemana">1 a 7 � Dia da semana a qual pertence a faixa de hor�rio.</param>
        /// <param name="FaixaDia">1 a 4 � Para cada dia da semana existem 4 faixas de hor�rio.</param>
        /// <param name="Hora">0 a 23</param>
        /// <param name="Minuto">0 a 59</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte InserirHorarioAcesso(byte Horario, byte DiaSemana, byte FaixaDia, byte Hora, byte Minuto);
        #endregion

        #region EnviarHorariosAcesso
        /// <summary>
        /// Envia para o Inner o buffer com a lista de hor�rios de acesso, ap�s executar o comando o
        ///buffer � limpo pela dll automaticamente.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarHorariosAcesso(int Inner);
        #endregion

        #endregion

        #region FUN��ES PARA MANIPULAR LISTA DE ACESSO

        #region ApagarListaAcesso
        /// <summary>
        /// Limpar o buffer com a lista de usu�rios cadastrados e envia automaticamente para o Inner.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ApagarListaAcesso(int Inner);
        #endregion

        #region InserirUsuarioListaAcesso
        /// <summary>
        /// Insere no buffer da dll um usu�rio da lista e a qual hor�rio de acesso ele est� associado. Os
        ///hor�rios j� dever�o ter sido cadastrados atrav�s das fun��es InserirHorarioAcesso e enviados atrav�s
        ///da fun��o EnviarHorariosAcesso para a lista ter o efeito correto.
        /// </summary>
        /// <param name="Cartao">1 a ... � Dependo do padr�o de cart�o definido e da quantidade de d�gitos definda.</param>
        /// <param name="Horario">1 a 100 � N�mero do hor�rio j� cadastrado no Inner.
                               ///101 � Acesso sempre liberado para o usu�rio.
                               ///102 � Acesso sempre negado para o usu�rio.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte InserirUsuarioListaAcesso(string Cartao, byte Horario);
        #endregion

        #region EnviarListaAcesso
        /// <summary>
        /// Envia o buffer com os usu�rios da lista de acesso para o Inner, ap�s executar o comando o
        ///buffer � limpo pela dll automaticamente.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarListaAcesso(int Inner);
        #endregion

        #endregion

        #region FUN��ES PARA MANIPULAR AS MENSAGENS ONLINE DO INNER

        #region EnviarMensagemPadraoOnLine
        /// <summary>
        /// Envia para o Inner a mensagem padr�o(fixa) que ser� sempre exibida pelo Inner. Essa
        ///mensagem � exibida enquanto o Inner estiver ocioso. Caso a mensagem passe de 32 caracteres a DLL
        ///ir� utilizar os primeiros 32 caracteres.
        ///O Inner n�o aceita caracteres com acentua��o, padr�o UNICODE ou padr�o ANSI.
        ///O Inner aceita apenas os caracteres do padr�o ASCII.
        /// </summary>
        /// <param name="Inner">   1 a 32 � Para comunica��o serial.
                                ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                                ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="ExibirData">0 � N�o exibe a data/hora na linha superior do display.
                                  ///1 � Exibe a data/hora na linha superior do display.</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja
                                ///exibindo a data/hora o tamanho da mensagem passa a ser
                                ///16 ao inv�s de 32. Caso seja passado uma string vazia o
                                ///Inner exibir� a mensagem em branco no display.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarMensagemPadraoOnLine(int Inner, byte ExibirData, string Mensagem);
        #endregion

        #region EnviarMensagemTemporariaOnLine
        /// <summary>
        /// Envia para o Inner uma mensagem tempor�ria. Caso a mensagem passe de 32 caracteres a
        ///DLL ir� utilizar os primeiros 32 caracteres.
        ///O Inner n�o aceita caracteres com acentua��o, padr�o UNICODE ou padr�o ANSI.
        ///O Inner aceita apenas os caracteres do padr�o ASCII.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="ExibirData">0 � N�o exibe a data/hora na linha superior do display.
                                  ///1 � Exibe a data/hora na linha superior do display.</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja
                                ///exibindo a data/hora o tamanho da mensagem passa a ser
                                ///16 ao inv�s de 32. Caso seja passado uma string vazia o
                                ///Inner exibir� a mensagem em branco no display.</param>
        /// <param name="Tempo">Tempo, em segundos, que a mensagem ficar� na display. 1 a 50.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarMensagemTemporariaOnLine(int Inner, byte ExibirData, string Mensagem, byte Tempo);
        #endregion

        #endregion

        #region FUN��ES PARA MANIPULAR AS MENSAGENS OFFLINE DO INNER

        #region DefinirMensagemEntradaOffLine
        /// <summary>
        /// Configura a mensagem a ser exibida quando o usu�rio passar o cart�o no sentido de entrada
        ///do Inner. Caso a mensagem passe de 32 caracteres a DLL ir� utilizar os primeiros 32 caracteres.
        ///O Inner n�o aceita caracteres com acentua��o, padr�o UNICODE ou padr�o ANSI.
        ///O Inner aceita apenas os caracteres do padr�o ASCII.
        /// </summary>
        /// <param name="ExibirData">0 � N�o exibe a data/hora na linha superior do display.
                                  ///1 � Exibe a data/hora na linha superior do display(Default).</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja
                                ///exibindo a data/hora o tamanho da mensagem passa a ser
                                ///16 ao inv�s de 32. Caso seja passado uma string vazia o
                                ///Inner exibir� a mensagem em branco no display.
                                ///�Entrada OK�(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirMensagemEntradaOffLine(byte ExibirData, string Mensagem);
        #endregion

        #region DefinirMensagemSaidaOffLine
        /// <summary>
        /// Configura a mensagem a ser exibida quando o usu�rio passar o cart�o no sentido de sa�da
        ///do Inner. Caso a mensagem passe de 32 caracteres a DLL ir� utilizar os primeiros 32 caracteres.
        ///O Inner n�o aceita caracteres com acentua��o, padr�o UNICODE ou padr�o ANSI.
        ///O Inner aceita apenas os caracteres do padr�o ASCII.
        /// </summary>
        /// <param name="ExibirData">0 � N�o exibe a data/hora na linha superior do display.
                                  ///1 � Exibe a data/hora na linha superior do display(Default).</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja
                                ///exibindo a data/hora o tamanho da mensagem passa a ser
                                ///16 ao inv�s de 32. Caso seja passado uma string vazia o
                                ///Inner exibir� a mensagem em branco no display.
                                ///�Saida OK� (Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirMensagemSaidaOffLine(byte ExibirData, string Mensagem);
        #endregion

        #region DefinirMensagemPadraoOffLine
        /// <summary>
        /// Configura a mensagem a ser exibida pelo Inner quando ele estiver ocioso. Caso a
        ///mensagem passe de 32 caracteres a DLL ir� utilizar os primeiros 32 caracteres.
        ///O Inner n�o aceita caracteres com acentua��o, padr�o UNICODE ou padr�o ANSI.
        ///O Inner aceita apenas os caracteres do padr�o ASCII.
        /// </summary>
        /// <param name="ExibirData">0 � N�o exibe a data/hora na linha superior do display.
                                  ///1 � Exibe a data/hora na linha superior do display.</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja
                                ///exibindo a data/hora o tamanho da mensagem passa a ser
                                ///16 ao inv�s de 32. Caso seja passado uma string vazia o
                                ///Inner exibir� a mensagem em branco no display.
                                ///�Passe o cartao� (Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirMensagemPadraoOffLine(byte ExibirData, string Mensagem);
        #endregion

        #region DefinirMensagemFuncaoOffLine
        /// <summary>
        /// Configura a mensagem a ser exibida quando o usu�rio passar o cart�o utilizando uma das
        ///fun��es do Inner(de 0 a 9) e a habilita ou desabilita essas fun��es. Caso a mensagem passe de 32
        ///caracteres a DLL ir� utilizar os primeiros 32 caracteres.
        ///O Inner n�o aceita caracteres com acentua��o, padr�o UNICODE ou padr�o ANSI.
        ///O Inner aceita apenas os caracteres do padr�o ASCII.
        /// </summary>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja
                                ///exibindo a data/hora o tamanho da mensagem passa a ser
                                ///16 ao inv�s de 32. Caso seja passado uma string vazia o
                                ///Inner n�o exibir� a mensagem no display.</param>
        /// <param name="Funcao">0 a 9.</param>
        /// <param name="Habilitada">0 � Desabilita a fun��o do Inner(Default).
                                  ///1 � Habilita a fun��o do Inner.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirMensagemFuncaoOffLine(string Mensagem, byte Funcao, byte Habilitada);
        #endregion

        #region HabilitarScoreMensagemOffLine
        /// <summary>
        /// Configura se a mensagem de entrada/sa�da ir� exibir o score da digital no display do Inner Bio.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Tipo">0 � Entrada.
                            ///1 � Sa�da</param>
        /// <param name="Habilitar">0 � Desabilita a exibi��o do score.
                                 ///1 � Habilita a exibi��o do score.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte HabilitarScoreMensagemOffLine(int Inner, byte Tipo, byte Habilitar);
        #endregion

        #region EnviarMensagensOffLine
        /// <summary>
        /// Envia o buffer com todas as mensagens off line configuradas anteriormente, para o Inner.
        ///Ap�s executar a fun��o, o buffer com as mensagens � limpo automaticamente pela dll.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarMensagensOffLine(int Inner);
        #endregion

        #region ApagarMensagensOffLine
        /// <summary>
        /// Limpa o buffer com as mensagens, setando com as mensagens default do Inner, e envia o
        ///buffer para o Inner automaticamente.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ApagarMensagensOffLine(int Inner);
        #endregion

        #region DefinirConfiguracoesFuncoes
        /// <summary>
        /// Configura a rea��o do Inner para cada fun��o de forma individual, ou seja, se ao utilizar a
        ///fun��o 0 o Inner vai acionar o rele 1 e solicitar biometria, por exemplo.
        ///As configura��es ficam armazenadas em um buffer interno da dll e ser�o enviados somente ap�s a
        ///chamada a fun��o EnviarConfiguracoesFuncoes.
        /// </summary>
        /// <param name="Funcao">0 a 9.</param>
        /// <param name="Catraca">0 � N�o libera catraca(Default).
                               ///1 � Libera catraca no sentido de entrada.
                               ///2 � Libera catraca no sentido de sa�da.
                               ///3 � Libera catraca nos dois sentidos.</param>
        /// <param name="Rele1">0 � N�o aciona rele 1(Default).
                             ///1 � Aciona rele 1.</param>
        /// <param name="Rele2">0 � N�o aciona rele 2(Default).
                             ///1 � Aciona rele 2.</param>
        /// <param name="Lista">0 � N�o consulta lista para registrar a fun��o(Default).
                             ///1 � Consulta a lista, se o cart�o estiver com acesso
                             ///liberado registra a fun��o.</param>
        /// <param name="Biometria">0 � Registra a fun��o a partir da leitura do cart�o ou teclado. N�o faz verifica��o / identifica��o biom�trica(Default).
                                 ///1 - Faz verifica��o ou identifica��o biom�trica.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirConfiguracoesFuncoes(byte Funcao, byte Catraca, byte Rele1, byte Rele2, byte Lista, byte Biometria);
        #endregion

        #region HabilitarScoreFuncoes
        /// <summary>
        /// Configura se a fun��o ir� exibir o score da digital no display do Inner Bio.
        /// </summary>
        /// <param name="Funcao">0 a 9.</param>
        /// <param name="Score">0 � N�o exibe o score da digital(Default).
                             ///1 � Exibe o score da digital.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte HabilitarScoreFuncoes(int Funcao, byte Score);
        #endregion

        #region EnviarConfiguracoesFuncoes
        /// <summary>
        /// Envia o buffer com as configura��es de todas as fun��es para o Inner. Ap�s executar a
        ///fun��o, o buffer com as configura��es das fun��es � limpo automaticamente pela dll.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarConfiguracoesFuncoes(int Inner);
        #endregion

        #endregion

        #region FUN��ES PARA MANIPULAR OS HOR�RIOS DE TOQUE DA SIRENE DO INNER
      
        #region ApagarHorariosSirene
        /// <summary>
        /// Limpa o buffer com os hor�rios de sirene e o envia automaticamente para o Inner.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ApagarHorariosSirene(int Inner);
        #endregion

        #region InserirHorarioSirene
        /// <summary>
        /// Insere um hor�rio de toque de sirene e configura em quais dias da semana esses hor�rio
        ///ir�o tocar. � poss�vel inserir no m�ximo 100 hor�rios para a sirene.
        /// </summary>
        /// <param name="Hora">0 a 23</param>
        /// <param name="Minuto">0 a 59</param>
        /// <param name="Segunda">0 � Desabilita o toque nesse dia.
                               ///1 � Habilita o toque nesse dia.</param>
        /// <param name="Terca">0 � Desabilita o toque nesse dia.
                             ///1 � Habilita o toque nesse dia.</param>
        /// <param name="Quarta">0 � Desabilita o toque nesse dia.
                              ///1 � Habilita o toque nesse dia.</param>
        /// <param name="Quinta">0 � Desabilita o toque nesse dia.
                              ///1 � Habilita o toque nesse dia.</param>
        /// <param name="Sexta">0 � Desabilita o toque nesse dia.
                             ///1 � Habilita o toque nesse dia.</param>
        /// <param name="Sabado">0 � Desabilita o toque nesse dia.
                              ///1 � Habilita o toque nesse dia.</param>
        /// <param name="DomingoFeriado">0 � Desabilita o toque nesse dia.
                                      ///1 � Habilita o toque nesse dia.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte InserirHorarioSirene(byte Hora, byte Minuto, byte Segunda, byte Terca, byte Quarta, byte Quinta, byte Sexta, byte Sabado, byte DomingoFeriado);
        #endregion

        #region EnviarHorariosSirene
        /// <summary>
        /// Envia o buffer com os hor�rio de sirene cadastrados para o Inner. Ap�s executar a fun��o o
        ///buffer � limpo automaticamente pela dll.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarHorariosSirene(int Inner);
        #endregion

        #endregion

        #region FUN��O PARA RECEBER OS BILHETES OFFLINE DO INNER

        #region ColetarBilhete
        /// <summary>
        /// Coleta um bilhete Off-Line que est� armazenado na mem�ria do Inner, os dados do bilhete
        ///s�o retornados por refer�ncia nos par�metros da fun��o. Ates de chamar esta fun��o pela primeira vez �
        ///preciso chamar obrigatoriamente as fun��es DefinirPadraoCartao e DefinirQuantidadeDigitosCartao
        ///nessa ordem para que o n�mero do cart�o seja calculado de forma correta.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Tipo">Tipo da marca��o registrada.
                            ///0 a 9 � Fun��es registradas pelo cart�o.
                            ///10 � Entrada pelo cart�o.
                            ///11 � Sa�da pelo cart�o.
                            ///12 � Tentativa de entrada negada pelo cart�o.
                            ///13 � Tentativa de sa�da negada pelo cart�o.
                            ///100 a 109 � Fun��es registradas pelo teclado.
                            ///110 � Entrada pelo teclado.
                            ///111 � Sa�da pelo teclado.
                            ///112 � Tentativa de entrada negada pelo teclado.
                            ///113 � Tentativa de sa�da negada pelo teclado.</param>
        /// <param name="Dia">1 a 31</param>
        /// <param name="Mes">1 a 12</param>
        /// <param name="Ano">0 a 99</param>
        /// <param name="Hora">0 a 23</param>
        /// <param name="Minuto">0 a 59</param>
        /// <param name="Cartao">N�mero do cart�o do usu�rio.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte ColetarBilhete(int Inner, ref byte Tipo, ref byte Dia, ref byte Mes, ref byte Ano, ref byte Hora, ref byte Minuto, StringBuilder Cartao);
        #endregion

        #region ReceberQuantidadeBilhetes
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int ReceberQuantidadeBilhetes(int Inner, int[] QtdeBilhetes);
        #endregion

        #endregion

        #region FUN��ES PARA MANIPULAR OS DADOS ONLINE DO INNER

        #region EnviarFormasEntradasOnLine
        /// <summary>
        /// Configura as formas de entrada de dados do Inner no modo OnLine. Cada vez que alguma
        ///informa��o for recebida no modo OnLine atrav�s da fun��o ReceberDadosOnLine, a fun��o
        ///EnviarFormasEntradasOnLine dever� ser chamada novamente para reconfigurar o Inner.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="QtdeDigitosTeclado">0 a 20 d�gitos.</param>
        /// <param name="EcoTeclado">0 � para n�o
                                  ///1 � para sim
                                  ///2 � ecoar *</param>
        /// <param name="FormaEntrada">0 � n�o aceita entrada de dados
                                    ///1 � aceita teclado
                                    ///2 � aceita leitura no leitor 1
                                    ///3 � aceita leitura no leitor 2
                                    ///4 � teclado e leitor 1
                                    ///5 � teclado e leitor 2
                                    ///6 � leitor 1 e leitor 2
                                    ///7 � teclado, leitor 1 e leitor 2
                                    ///10 � teclado + verifica��o biom�trica
                                    ///11 � leitor1 + verifica��o biom�trica
                                    ///12 � teclado + leitor1 + verifica��o biom�trica
                                    ///13 � leitor1 com verifica��o biom�trica + leitor2 sem verifica��o biom�trica
                                    ///14 � leitor1 com verifica��o biom�trica + leitor2 sem verifica��o biom�trica + teclado sem verifica��o biom�trica
                                    ///100 � Leitor 1 + Identifica��o Biom�trica (sem Verifica��o)
                                    ///101 � Leitor 1 + Teclado + Identifica��o Biom�trica (sem Verifica��o)
                                    ///102 � Leitor 1 + Leitor 2 + Identifica��o Biom�trica (sem Verifica��o)
                                    ///103 � Leitor 1 + Leitor 2 + Teclado + Identifica��o Biom�trica (sem Verifica��o)
                                    ///104 � Leitor 1 invertido + Identifica��o Biom�trica (sem Verifica��o)
                                    ///105 � Leitor 1 invertido + Teclado + Identifica��o Biom�trica (sem Verifica��o)</param>
        /// <param name="TempoTeclado">1 a 50</param>
        /// <param name="PosicaoCursorTeclado">1 a 32</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarFormasEntradasOnLine(int Inner, byte QtdeDigitosTeclado, byte EcoTeclado, byte FormaEntrada, byte TempoTeclado, byte PosicaoCursorTeclado);
        #endregion

        #region ReceberDadosOnLine
        /// <summary>
        /// Coleta um bilhete OnLine, caso o usu�rio tenha passado ou digitado algum cart�o no Inner
        ///retorna as informa��es do cart�o nos par�metros da fun��o. Para que a data/hora do bilhete OnLine seja
        ///retornada, o Inner dever� ter sido previamente configurado atrav�s da fun��o
        ///ReceberDataHoraDadosOnLine.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Origem">Origem dos dados recebidos.
                                ///1 � via teclado
                                ///2 � via leitor 1
                                ///3 � via leitor 2
                                ///4 � sensor da catraca(obsoleto)
                                ///5 � fim do tempo de acionamento
                                ///6 � giro da catraca Topdata (sensor �tico)
                                ///7 � Urna (catraca Millenium)
                                ///8 � Evento no Sensor 1
                                ///9 � Evento no Sensor 2
                                ///10 � Evento no Sensor 3</param>
        /// <param name="Complemento">Informa��es adicionais sobre os dados recebidos.
                                    ///0 � sa�da (com cart�o)
                                    ///1 � entrada (com cart�o)
                                    ///35 � # via teclado (1� tecla)
                                    ///42 � * via teclado (1� tecla)
                                    ///65 � �Fun��o� via teclado
                                    ///66 � �Entrada� via teclado
                                    ///67 � �Sa�da� via teclado
                                    ///255 � Inseriu todos os d�gitos permitidos pelo teclado.
                                    ///Evento do Sensor
                                    ///0/1 � N�vel atual do sensor</param>
        /// <param name="Cartao">N�mero do cart�o recebido.</param>
        /// <param name="Dia">1 a 31.</param>
        /// <param name="Mes">1 a 12.</param>
        /// <param name="Ano">0 a 99</param>
        /// <param name="Hora">0 a 23</param>
        /// <param name="Minuto">0 a 59</param>
        /// <param name="Segundo">0 a 59</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberDadosOnLine(int Inner, ref byte Origem, ref byte Complemento, StringBuilder Cartao, ref byte Dia, ref byte Mes, ref byte Ano, ref byte Hora, ref byte Minuto, ref byte Segundo);
        #endregion

        #endregion

        #region FUN��O PARA LER OS STATUS DOS SENSORES DO INNER
        
        #region LerSensoresInner
        /// <summary>
        /// Recebe o status atual dos sensores do Inner. Essa fun��o dever� ser utilizada somente em
        ///casos muito espec�ficos, por exemplo, quando voc� possui um Inner Plus/NET conectado a um sensor
        ///de presen�a e deseja saber se existe alguma pessoa naquele local.
        ///Essa fun��o n�o deve ser utilizada em catracas.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="StatusSensor1">0 � em n�vel baixo (fechado)
                                     ///1 � em n�vel alto (aberto)</param>
        /// <param name="StatusSensor2">0 � em n�vel baixo (fechado)
                                     ///1 � em n�vel alto (aberto)</param>
        /// <param name="StatusSensor3">0 � em n�vel baixo (fechado)
                                     ///1 � em n�vel alto (aberto)</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LerSensoresInner(int Inner, ref byte StatusSensor1, ref byte StatusSensor2, ref byte StatusSensor3);
        #endregion

        #endregion

        #region FUN��ES PARA MANIPULAR AS MENSAGENS DA IMPRESSORA DO INNER
        
        #region EnviarMensagemImpressora00
        /// <summary>
        /// Envia a mensagem para a impressora com final terminado em 0x00.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Mensagem">Caracteres/n�meros que ser�o impressos na impressora. � poss�vel utilizar
                                ///caracteres especiais (avan�os de linha, etc), tudo depende do caracterset
                                ///que o modelo de impressora utilizado suporta.
                                ///O tamanho m�ximo da mensagem � de 254 caracteres.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarMensagemImpressora00(int Inner, string Mensagem);
        #endregion

        #region EnviarMensagemImpressoraFF
        /// <summary>
        /// Envia a mensagem para a impressora com final terminado em 0xFF.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Mensagem">Caracteres/n�meros que ser�o impressos na impressora. � poss�vel utilizar
                                ///caracteres especiais (avan�os de linha, etc), tudo depende do caracterset
                                ///que o modelo de impressora utilizado suporta.
                                ///O tamanho m�ximo da mensagem � de 254 caracteres.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarMensagemImpressoraFF(int Inner, string Mensagem);
        #endregion

        #endregion

        #region FUN��ES PARA CONFIGURAR A MUDAN�A AUTOM�TICA DE ON-LINE PARA OFF-LINE DO INNER

        #region HabilitarMudancaOnLineOffLine
        /// <summary>
        /// Habilita/Desabilita a mudan��o autom�tica do modo OffLine do Inner para OnLine e viceversa.
        ///Configura o tempo ap�s a comunica��o ser interrompida que est� mudan�a ir� ocorrer.
        /// </summary>
        /// <param name="Habilita">0 � Desabilita a mudan�a(Default).
                                ///1 � Habilita a mudan�a.
                                ///2 � Habilita a mudan�a autom�tica para o modo OnLine TCP com Ping,
                                ///onde o Inner precisa receber o comando PingOnLine para manter-se OnLine.</param>
        /// <param name="Tempo">Tempo em segundos para ocorrer a mudan�a.
                                ///1 a 50.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte HabilitarMudancaOnLineOffLine(byte Habilita, byte Tempo);
        #endregion

        #region DefinirEntradasMudancaOffLine
        /// <summary>
        /// Configura as formas de entradas de dados para quando o Inner mudar para o modo Off-Line.
        ///Para aplica��es com biometria verifique a pr�xima fun��o
        ///�DefinirEntradasMudan�aOffLineComBiometria�.
        /// </summary>
        /// <param name="Teclado">0 � N�o aceita dados pelo teclado.
                               ///1 � Aceita dados pelo teclado.</param>
        /// <param name="Leitor1">0 � desativado
                               ///1 � somente para entrada
                               ///2 � somente para sa�da
                               ///3 � entrada e sa�da
                               ///4 � sa�da e entrada</param>
        /// <param name="Leitor2">0 � desativado
                               ///1 � somente para entrada
                               ///2 � somente para sa�da
                               ///3 � entrada e sa�da
                               ///4 � sa�da e entrada</param>
        /// <param name="Catraca">0 � reservado para uso futuro.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirEntradasMudancaOffLine(byte Teclado, byte Leitor1, byte Leitor2, byte Catraca);
        #endregion

        #region DefinirEntradasMudancaOffLineComBiometria
        /// <summary>
        /// Configura as formas de entradas de dados para quando o Inner mudar para o modo Off-Line.
        ///Esse comando difere do anterior por permitir a configura��o de biometria. Atrav�s dessa fun��o o Inner
        ///pode ser configurado para trabalhar com verifica��o ou identifica��o biom�trica, quando ocorrer uma
        ///mudan�a autom�tica de On-Line para Off-Line.
        /// </summary>
        /// <param name="Teclado">0 � N�o aceita dados pelo teclado.
                               ///1 � Aceita dados pelo teclado.</param>
        /// <param name="Leitor1"> 0 � desativado
                                ///3 � entrada e sa�da
                                ///4 � sa�da e entrada (nesse caso for�a Leitor2 igual a zero)</param>
        /// <param name="Leitor2">0 � desativado
                               ///3 � entrada e sa�da</param>
        /// <param name="Verificacao">0 � desativada
                                   ///1 � ativada</param>
        /// <param name="Identificacao">0 � desativada
                                     ///1 � ativada</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirEntradasMudancaOffLineComBiometria(byte Teclado, byte Leitor1, byte Leitor2, byte Verificacao ,byte Identificacao);
        #endregion

        #region DefinirMensagemPadraoMudancaOffLine
        /// <summary>
        /// Configura a mensagem padr�o a ser exibida pelo Inner quando ele mudar para Off-line.
        /// </summary>
        /// <param name="ExibirData">0 � N�o exibe a data/hora na linha superior do display.
                                  ///1 � Exibe a data/hora na linha superior do display.</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja exibindo a data/hora o
                                ///tamanho da mensagem passa a ser 16 ao inv�s de 32. Caso seja passado
                                ///uma string vazia o Inner n�o exibir� a mensagem no display</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirMensagemPadraoMudancaOffLine(byte ExibirData, string Mensagem);
        #endregion

        #region DefinirMensagemPadraoMudancaOnLine
        /// <summary>
        /// Configura a mensagem padr�o exibido pelo Inner quando entrar para on line ap�s uma
        ///queda para off line.
        /// </summary>
        /// <param name="ExibirData">
                                    /// 0 � N�o exibe a data/hora na linha superior do display.
                                    ///1 � Exibe a data/hora na linha superior do display.</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja exibindo a data/hora o
                                ///tamanho da mensagem passa a ser 16 ao inv�s de 32. Caso seja passado
                                ///uma string vazia o Inner n�o exibir� a mensagem no display</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirMensagemPadraoMudancaOnLine(byte ExibirData, string Mensagem);
        #endregion

        #region DefinirEntradasMudancaOnLine
        /// <summary>
        /// Configura as formas de entrada dos dados quando o Inner voltar para o modo On Line ap�s
        ///uma queda para OffLine.
        /// </summary>
        /// <param name="Entrada">0 � N�o aceita entrada de dados.
                               ///1 � Aceita teclado.
                               ///2 � Aceita leitor 1.
                               ///3 � Aceita leitor 2.
                               ///4 � Teclado e leitor 1.
                               ///5 � Teclado e leitor 2.
                               ///6 � Leitor 1 e leitor 2.
                               ///7 � Teclado, leitor 1 e 2.
                               ///8 � Sensor da catraca.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirEntradasMudancaOnLine(byte Entrada);
        #endregion

        #region DefinirConfiguracaoTecladoOnLine
        /// <summary>
        /// Configura o teclado para quando o Inner voltar para OnLine ap�s uma queda para OffLine.
        /// </summary>
        /// <param name="Digitos">0 a 20 d�gitos.</param>
        /// <param name="EcoDisplay">0 � para n�o ecoar
                                 ///1 � para sim
                                 ///2 � ecoar '*'</param>
        /// <param name="Tempo">1 a 50.</param>
        /// <param name="PosicaoCursor">1 a 32.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirConfiguracaoTecladoOnLine(byte Digitos, byte EcoDisplay, byte Tempo, byte PosicaoCursor);
        #endregion

        #region EnviarConfiguracoesMudancaAutomaticaOnLineOffLine
        /// <summary>
        /// Envia o buffer com as configura��es de mudan�a autom�tica do modo OnLine para OffLine .
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarConfiguracoesMudancaAutomaticaOnLineOffLine(int Inner);
        #endregion

        #endregion

        #region FUN��ES ESPEC�FICAS DO INNER BIO

        #region SolicitarModeloBio
        /// <summary>
        /// Solicita o modelo do Inner bio. Para receber o resultado dessa opera��o voc� dever� chamar
        ///a fun��o ReceberModeloBio enquanto o retorno for processando a opera��o.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarModeloBio(int Inner);
        #endregion

        #region ReceberModeloBio
        /// <summary>
        /// Retorna o resultado do comando SolicitarModeloBio, o modelo do Inner Bio � retornado por
        ///refer�ncia no par�metro da fun��o.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 � O comando foi utilizado com o Inner em OffLine.
                              ///1 � O comando foi utilizado com o Inner em OnLine</param>
        /// <param name="Modelo">2 � Bio Light 100 usu�rios.
                              ///4 � Bio 1000/4000 usu�rios.
                              ///255 � Vers�o desconhecida.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberModeloBio(int Inner, byte OnLine, ref int Modelo);
        #endregion

        #region SolicitarVersaoBio
        /// <summary>
        /// Solicita a vers�o do firmware da placa do Inner Bio, a placa que armazena as digitais.
        /// </summary>
        /// <param name="Inner">
        /// 1 a 32 � Para comunica��o serial.
        /// 1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
        /// 1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarVersaoBio(int Inner);
        #endregion

        #region ReceberVersaoBio
        /// <summary>
        /// Retorna o resultado do comando SolicitarVersaoBio, a vers�o do Inner Bio � retornado por
        ///refer�ncia nos par�metros da fun��o.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
        ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
        ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 � O comando foi utilizado com o Inner em OffLine.
                              ///1 � O comando foi utilizado com o Inner em OnLine</param>
        /// <param name="VersaoAlta">Parte alta da vers�o.</param>
        /// <param name="VersaoBaixa">Parte baixa da vers�o.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberVersaoBio(int Inner, byte OnLine, ref int VersaoAlta, ref int VersaoBaixa);
        #endregion

        #region SolicitarQuantidadeUsuariosBio
        /// <summary>
        /// Solicita a quantidade de usu�rios cadastrados no Inner Bio.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarQuantidadeUsuariosBio(int Inner);
        #endregion

        #region ReceberQuantidadeUsuariosBio
        /// <summary>
        /// Retorna o resultado do comando SolicitarQuantidadeUsuariosBio, a quantidade de
        ///usu�rios cadastrados no Inner Bio � retornado por refer�ncia nos par�metros da fun��o.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 � O comando foi utilizado com o Inner em OffLine.
                              ///1 � O comando foi utilizado com o Inner em OnLine</param>
        /// <param name="Quantidade">Total de usu�rios cadastrados no Inner Bio.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberQuantidadeUsuariosBio(int Inner, byte OnLine, ref int Quantidade);
        #endregion

        #region SolicitarUsuarioCadastradoBio
        /// <summary>
        /// Solicita do Inner Bio, o template com as duas digitais do Usuario desejado.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
        ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
        ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Usuario">N�mero do cart�o do usu�rio cadastrado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarUsuarioCadastradoBio(int Inner, string Usuario);
        #endregion

        #region ReceberUsuarioCadastradoBio
        /// <summary>
        /// Retorna o resultado do comando SolicitarUsuarioCadastradoBio e o template com as duas
        ///digitais do usu�rio cadastrado no Inner Bio. O template � retornado por refer�ncia nos par�metros da
        ///fun��o.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 � O comando foi utilizado com o Inner em OffLine.
                              ///1 � O comando foi utilizado com o Inner em OnLine</param>
        /// <param name="Template">Cadastro do usu�rio com as duas digitais, o dado est� em bin�rio e n�o
                                ///deve ser alterado nunca. O tamanho do template � de 844 bytes.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberUsuarioCadastradoBio(int Inner, byte OnLine,  byte[] Template);
        #endregion

        #region SolicitarExclusaoUsuario
        /// <summary>
        /// Solicita para o Inner bio excluir o cadastro do usu�rio desejado. O Retorno da exclus�o �
        ///verificado atrav�s da fun��o UsuarioFoiExcluido
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Usuario">N�mero do usu�rio a ser exclu�do.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarExclusaoUsuario(int Inner, string Usuario);
        #endregion

        #region UsuarioFoiExcluido
        /// <summary>
        /// Retorna o resultado do comando SolicitarExclusaoUsuario, se o retorno da fun��o for igual
        ///a 0 � porque o usu�rio foi exclu�do com sucesso.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 � O comando foi utilizado com o Inner em OffLine.
                              ///1 � O comando foi utilizado com o Inner em OnLine.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte UsuarioFoiExcluido(int Inner, byte OnLine);
        #endregion

        #region InserirUsuarioLeitorBio
        /// <summary>
        /// Solicita para o Inner Bio inserir um usu�rio diretamente pelo leitor biom�trico. O leitor ir�
        ///acender a luz vermelho e ap�s o usu�rio inserir a digital, automaticamente o usu�rio ser� cadastrado no
        ///Inner bio com o n�mero do cart�o passado no par�metro Usu�rio.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Tipo">0 � para solicitar o primeiro template
                            ///1 � para solicitar o segundo template (mesmo dedo) e salvar.
                            ///2 � para solicitar o segundo template (outro dedo) e salvar.</param>
        /// <param name="Usuario">N�mero do cart�o que o usu�rio ter�.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte InserirUsuarioLeitorBio(int Inner, byte Tipo, string Usuario);
        #endregion

        #region ResultadoInsercaoUsuarioLeitorBio
        /// <summary>
        /// Retorna o resultado do comando InserirUsuarioLeitorBio. Se o retorno for igual a 0 �
        ///porque o usu�rio foi cadastrado com sucesso.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 � O comando foi utilizado com o Inner em OffLine.
                              ///1 � O comando foi utilizado com o Inner em OnLine.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoInsercaoUsuarioLeitorBio(int Inner, byte OnLine);
        #endregion

        #region FazerVerificacaoBiometricaBio
        /// <summary>
        /// Ao chamar esta fun��o o Inner ir� acender o leitor biom�trico e ir� solicitar para o usu�rio
        ///inserir o dedo, ap�s isso o Inner ir� compara a(s) digital(ais), associadas ao n�mero do cart�o passado
        ///nos par�metros, que est� armazenada na sua mem�ria com a digital inserida pelo usu�rio. O retorno
        ///desse processo � retornado na fun��o ResultadoVerificacaoBiometrica.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Usuario">N�mero do usu�rio cadastrado na mem�ria do Inner.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte FazerVerificacaoBiometricaBio(int Inner, string Usuario);
        #endregion

        #region ResultadoVerificacaoBiometrica
        /// <summary>
        /// Retorna o resultado do comando FazerVerificacaoBiometricaBio. Se o retorno for igual a 0
        ///� porque o usu�rio foi comparado com sucesso.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 � O comando foi utilizado com o Inner em OffLine.
                              ///1 � O comando foi utilizado com o Inner em OnLine.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoVerificacaoBiometrica(int Inner, byte OnLine);
        #endregion

        #region FazerIdentificacaoBiometricaBio
        /// <summary>
        /// Ao chamar esta fun��o o Inner ir� acender o leitor biom�trico e ir� solicitar para o usu�rio
        ///inserir o dedo, ap�s isso o Inner ir� comparar a digital com as digitais cadastradas no banco de dados do
        ///equipamento. O resultado dessa opera��o � retornada atrav�s da fun��o
        ///ResultadoIdentificacaoBiometrica.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte FazerIdentificacaoBiometricaBio(int Inner);
        #endregion

        #region ResultadoIdentificacaoBiometrica
        /// <summary>
        ///Retorna o resultado do comando InserirUsuarioLeitorBio. Se o retorno for igual a 0 �
        ///porque o usu�rio foi Identificado com sucesso, o n�mero do cart�o do usu�rio ser� retornado por
        ///refer�ncia no par�metro da fun��o.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 � O comando foi utilizado com o Inner em OffLine.
                              ///1 � O comando foi utilizado com o Inner em OnLine.</param>
        /// <param name="Usuario">N�mero do usu�rio cadastrado no Inner bio que possui a
                               ///mesma digital do dedo inserido no leitor biom�trico.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoIdentificacaoBiometrica(int Inner, byte OnLine,  byte[] Usuario);
        #endregion

        #region SolicitarTemplateLeitor
        /// <summary>
        /// Solicita diretamente do Inner bio um template com apenas uma digital, ao executar essa
        ///fun��o o leitor biom�trico do Inner bio ir� acender e a digital que for inserida ser� enviada diretamente
        ///para a aplica��o, a digital n�o ficar� armazenada no banco de dado do equipamento.
        ///Para receber a digital � necess�rio chamar a fun��o ReceberTemplateLeitor.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarTemplateLeitor(int Inner);
        #endregion

        #region ReceberTemplateLeitor
        /// <summary>
        /// Retorna o resultado do comando SolicitarTemplateLeitor. Se o retorno for igual a 0 �
        ///porque o template foi recebido com sucesso. O template ser� retornado por refer�ncia no par�metro da fun��o.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 � O comando foi utilizado com o Inner em OffLine.
                              ///1 � O comando foi utilizado com o Inner em OnLine.</param>
        /// <param name="Template">Digital recebida pelo leitor biom�trico. � um array de bytes e seu conte�do
                                ///n�o deve ser alterado nunca, seu tamanho � de 404 bytes.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberTemplateLeitor(int Inner, byte OnLine,  byte[] Template);
        #endregion

        #region ConfigurarBio
        /// <summary>
        /// Habilita/Desabilita a identifica��o biom�trica e/ou a verifica��o biom�trica do Inner bio. O
        ///resultado da configura��o deve ser obtivo atrav�s do comando ResultadoConfiguracaoBio.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="HabilitaIdentificacao">0 � Desabilita.
                                             ///1 � Habilita.</param>
        /// <param name="HabilitaVerificacao">0 � Desabilita.
                                           ///1 � Habilita.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarBio(int Inner, byte HabilitaIdentificacao, byte HabilitaVerificacao);
        #endregion

        #region ResultadoConfiguracaoBio
        /// <summary>
        /// Retorna o resultado da configura��o do Inner Bio, fun��o ConfigurarBio. Se o retorno for
        ///igual a 0 � poque o Inner bio foi configurado com sucesso.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 � O comando foi utilizado com o Inner em OffLine.
                              ///1 � O comando foi utilizado com o Inner em OnLine.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoConfiguracaoBio(int Inner, byte OnLine);
        #endregion

        #region EnviarUsuarioBio
        /// <summary>
        /// Envia um template com duas digitais para o Inner Bio cadastrar no seu banco de dados. O
        ///resultado do cadastro deve ser verificado no retorno da fun��o UsuarioFoiEnviado.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Template">O cadastro do usu�rio j� contendo as duas digitais e o n�mero do usu�rio.
                                ///� um array de bytes com o tamanho de 844 bytes.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarUsuarioBio(int Inner,  byte[] Template);
        #endregion

        #region UsuarioFoiEnviado
        /// <summary>
        /// Retorna o resultado do cadastro do Template no Inner Bio, atrav�s da fun��o
        ///EnviarUsuarioBio. Se o retorno for igual a 0 � porque o template foi cadastrado com sucesso.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 � O comando foi utilizado com o Inner em OffLine.
                              ///1 � O comando foi utilizado com o Inner em OnLine.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte UsuarioFoiEnviado(int Inner, byte OnLine);
        #endregion

        #region CompararDigitalLeitor
        /// <summary>
        /// Ao executar essa fun��o o Inner bio ir� acender o leitor biom�trico solicitando a digital do
        ///usu�rio, na sequ�ncia ir� comparar a digital inserida pelo usu�rio com a digital enviada pela fun��o no
        ///par�metro Template. O resultado da compara��o � retornado pela fun��o
        ///ResultadoComparacaoDigitalLeitor.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Template">Digital a ser comparada. Array de bytes de 404 bytes.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte CompararDigitalLeitor(int Inner,  byte[] Template);
        #endregion

        #region ResultadoComparacaoDigitalLeitor
        /// <summary>
        /// Retorna o resultado da compara��o da digital do usu�rio com o template enviado para o
        ///Inner Bio, atrav�s da fun��o CompararDigitalLeitor. Se o retorno for igual a 0 � poque a digital inserida
        ///� a mesma da enviada.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 � O comando foi utilizado com o Inner em OffLine.
                              ///1 � O comando foi utilizado com o Inner em OnLine.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoComparacaoDigitalLeitor(int Inner, byte OnLine);
        #endregion

        #region IncluirUsuarioSemDigitalBio
        /// <summary>
        /// Insere o n�mero do cart�o na lista de usu�rios sem digital do Inner bio. Este n�mero ficara
        ///armazenado em um buffer interno dentro da dll e somente ser� enviado para o Inner ap�s a chamada a
        ///fun��o EnviarListaUsuariosSemDigitalBio. O n�mero m�ximo de d�gitos para o cart�o � 10, caso os
        ///cart�es tenham mais de 10 d�gitos, utilizar os 10 d�gitos menos significativos do cart�o.
        /// </summary>
        /// <param name="Cartao">1 a 9999999999 � N�mero do cart�o do usu�rio.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte IncluirUsuarioSemDigitalBio(string Cartao);
        #endregion

        #region EnviarListaUsuariosSemDigitalBio
        /// <summary>
        /// Envia o buffer com a lista de usu�rios sem digital para o Inner. Ap�s a execu��o do
        ///comando, o buffer � limpo pela dll.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarListaUsuariosSemDigitalBio(int Inner);
        #endregion

        #region SetarBioLight
        /// <summary>
        /// Define que o Inner utilizado no momento � um Inner bio light ao inv�s de um Inner bio
        ///1000/4000. Essa fun��o dever� ser chamada sempre que necess�rio antes das fun��es
        ///SolicitarUsuarioCadastradoBio, SolicitarExclusaoUsuario, InserirUsuarioLeitorBio e
        ///FazerVerificacaoBiometricaBio.
        /// </summary>
        /// <param name="Light">1 � � um Inner bio light
                             ///0 � � um Inner bio 1000/4000(valor default)</param>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern void SetarBioLight(int Light);
        #endregion

        #endregion

        #region CONFIGURA��ES DE AJUSTES BIOM�TRICOS

        #region ConfigurarAjustesSensibilidadeBio
        /// <summary>
        /// Configura a quantidade de ganho, brilho e contraste que o Inner ir� utilizar para ler a digital do usu�rio.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Ganho">1, 2, 4 ou 8.
                             ///4(Valor default).</param>
        /// <param name="Brilho">0 a 100.
                              ///20(Valor default).</param>
        /// <param name="Contraste">0 a 100.
                                 ///20(Valor default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarAjustesSensibilidadeBio(int Inner, byte Ganho, byte Brilho, byte Contraste);
        #endregion

        #region ConfigurarAjustesQualidadeBio
        /// <summary>
        /// Configura o n�vel da qualidade da digital que o Inner Bio ir� utilizar para registrar a digital na
        ///base de dados e para utilizar na verifica��o biom�trica do cart�o.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Registro">30 a 100.
                                ///40(Valor default).</param>
        /// <param name="Verificacao">10 a 100.
                                   ///30(Valor default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarAjustesQualidadeBio(int Inner, byte Registro, byte Verificacao);
        #endregion

        #region ConfigurarAjustesSegurancaBio
        /// <summary>
        /// Configura o n�vel de seguran�a utilizados na Identifica��o e na Verifica��o biom�trica.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Identificacao">6 a 9.
                                     ///8(Valor default).</param>
        /// <param name="Verificacao">1 a 9.
                                   ///5(Valor default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarAjustesSegurancaBio(int Inner, byte Identificacao, byte Verificacao);
        #endregion

        #region ConfigurarCapturaAdaptativaBio
        /// <summary>
        /// Habilita a captura adaptativa da digital, � poss�vel especificar quantas tentativas o Inner Bio
        ///dever� realizar na captura da digital e por quanto tempo ficar� esperando o usu�rio inserir a digital.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Capturar">0 � Desabilita a captura adaptativa(Default).
                                ///1 � Habilita a captura adaptativa.</param>
        /// <param name="Total">1 a 10.
                             ///5(Valor Default).</param>
        /// <param name="Tempo">1 a 7.
                             ///5(Valor default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarCapturaAdaptativaBio(int Inner, byte Capturar, byte Total, byte Tempo);
        #endregion

        #region ConfigurarFiltroBio
        /// <summary>
        /// Habilita/Desabilita o filtro da digital latente pelo Inner Bio.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Habilitar">0 � Desabilita o filtro da digital latente(Default).
                                 ///1 � Habilita o filtro da digital latente.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarFiltroBio(int Inner, byte Habilitar);
        #endregion

        #region EnviarAjustesBio
        /// <summary>
        /// Envia o buffer com as configurac�es feitas pelas fun��es acima para o Inner.
        /// Ap�s o envio o buffer � limpo pela DLL.
        ///Para confirmar realmente se o Inner recebeu os dados com sucesso,
        /// � necess�rio verificar com a fun��o ResultadoEnvioAjustesBio.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarAjustesBio(int Inner);
        #endregion

        #endregion

        #region FUN��ES PARA RECEBER TODOS OS USU�RIOS CADASTRADOS NO INNER BIO

        #region InicializarColetaListaUsuariosBio
        /// <summary>
        /// Prepara a dll para iniciar a coleta dos usu�rios do Inner bio, essa fun��o deve ser chamada
        ///obrigatoriamente no in�cio do processo.
        /// </summary>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern void InicializarColetaListaUsuariosBio();
        #endregion

        #region SolicitarListaUsuariosBio
        /// <summary>
        /// Solicita o pacote(a parte) atual da lista de usu�rios do Inner bio.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarListaUsuariosBio(int Inner);
        #endregion

        #region ReceberPacoteListaUsuariosBio
        /// <summary>
        /// Receber o pacote solicitado pela fun��o SolicitarListaUsuariosBio.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberPacoteListaUsuariosBio(int Inner);
        #endregion

        #region ReceberUsuarioLista
        /// <summary>
        /// Recebe um usu�rio por vez do pacote recebido anteriormente. O n�mero do usu�rio �
        ///retornado pelo par�metro da fun��o.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Usuario">N�mero do usu�rio cadastrado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberUsuarioLista(int Inner,  StringBuilder Usuario);
        #endregion

        #region TemProximoUsuario
        /// <summary>
        /// Retorna 1 se ainda existe usu�rios no pacote recebido da lista.
        /// </summary>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int TemProximoUsuario();
        #endregion

        #region TemProximoPacote
        /// <summary>
        /// Retorna 1 se ainda existe mais pacotes da lista de usu�rios, a ser recebido do Inner Bio.
        /// </summary>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int TemProximoPacote();
        #endregion

        #endregion

        #region FUN��ES DO INNER COM MODEM

        #region EnviarStringInicializacaoModem
        /// <summary>
        /// Envia uma string para o modem executar. A string deve conter um comando AT v�lido do modem utilizado.
        /// </summary>
        /// <param name="Str">String v�lida de um comando AT.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int EnviarStringInicializacaoModem(string Str);
        #endregion

        #region LerByteModem
        /// <summary>
        /// Verifica o retorno ap�s enviar um comando AT para ser executado.
        /// </summary>
        /// <returns>-1 � Processando.
                   ///1, 5, 10 ou 12 � Modem conectado.
                   ///3 � Sem resposta do modem.
                   ///6 � Sem tom de discagem.
                   ///7 � Ocupado.</returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int LerByteModem();
        #endregion

        #region ConectarModem
        /// <summary>
        /// Configura, disca e efetua a conex�o com o modem e o Inner desejado.
        /// </summary>
        /// <param name="Porta">N�mero da porta serial do modem.</param>
        /// <param name="Str">String de inicializa��o do modem, com os comandos AT.</param>
        /// <param name="Tom">Se 0 discagem por pulso.
                           ///Se 1 discagem por tom.</param>
        /// <param name="Telefone">String com o n�mero do telefone/ramal a ser discado.</param>
        /// <param name="Inner">N�mero do Inner que est� conectado no modem.</param>
        /// <returns>-3 � Telefone inv�lido.
                  ///-2 � String de inicializa��o inv�lida.
                  ///-1 � Processando.
                  ///1, 5, 10 ou 12 � Modem conectado.
                  ///3 � Sem resposta do modem.
                  ///6 � Sem tom de discagem.
                  ///7 � Ocupado.</returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int ConectarModem(int Porta, string Str, int Tom, string Telefone, int Inner);
        #endregion

        #endregion

        #region INNER PADR�O - PRIMEIRA GERA��O DE INNERS

        #region SetarInnerOld
        /// <summary>
        /// Configura a DLL para comunicar com o Inner padr�o, ao chamar essa fun��o ele ir� afetar o
        ///grupo de fun��es referentes a �Lista de acesso�, �Hor�rios de acesso�, �Hor�rios de sirene� e a �Leitura
        ///dos sensores�, para que estas fun��es funcionem no Inner padr�o a SetarInnerOld deve ser chamada.
        ///Ap�s comunicar com o Inner padr�o, a fun��o deve ser chamada desabilitando a comunica��o com este tipo de Inner.
        ///As demais fun��es funcionam normalmente com este tipo de Inner.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Old">0 � N�o est� utilizando o Inner Padr�o.
                           ///1 � Est� utilizando o Inner Padr�o.</param>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern void SetarInnerOld(int Inner,int Old);
        #endregion

        #endregion

        #region FUN��ES DO INNER VERID

        #region IncluirUsuarioVerid
        /// <summary>
        /// Envia o template do usu�rio para o Inner Verid. As digitais ficar�o armazenadas na base de
        ///dados do Inner Verid. O resultado da inclus�o deve ser conferido atrav�s da fun��o
        ///ResultadoInclusaoUsuarioVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Template">Array de bytes contendo toda as informa��es das digitais do usu�rio.
                                ///Estes dados devem ter sido coletados de um outro Inner Verid previamente e 
                                ///n�o dever� ser alterado.
                                ///Este tamplate dever� ser o completo, com duas digitais.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte IncluirUsuarioVerid(int Inner,  byte[] Template);
        #endregion

        #region ResultadoInclusaoUsuarioVerid
        /// <summary>
        /// Retorna se a fun��o IncluirUsuarioVerid foi processada corretamente pelo Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoInclusaoUsuarioVerid(int Inner);
        #endregion

        #region CompararTemplateVerid
        /// <summary>
        /// Envia o template de apenas uma digital para o Inner iniciar a compara��o, o Inner Verid ir�
        ///comparar a digial enviada com a digital inserida pelo usu�rio.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Template">Array de bytes contendo toda as informa��es das digitais do usu�rio.
                                ///Estes dados devem ter sido coletados de um outro Inner Verid previamente
                                ///e n�o dever� ser alterado.
                                ///Este template n�o � o completo, ele dever� conter somente uma digital.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte CompararTemplateVerid(int Inner,  byte[] Template);
        #endregion

        #region ResultadoComparacaoTemplateVerid
        /// <summary>
        /// Retorna o resultado da compara��o da digital enviada pela fun��o CompararTemplateVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoComparacaoTemplateVerid(int Inner);
        #endregion

        #region CriarUsuarioLeitorVerid
        /// <summary>
        /// Cria o template com apenas 1 digital, ao executar esta fun��o o Inner Verid ir� solicitar a
        ///digital do usu�rio e ir� disponibilizar para o software. A digital coletada com esta fun��o dever� ser
        ///utilizada com a fun��o CompararTemplateVerid. O resultado e a digital dever�o ser capturados com a
        ///fun��o ResultadoInclusaoUsuarioLeitorVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte CriarUsuarioLeitorVerid(int Inner);
        #endregion

        #region ResultadoInclusaoUsuarioLeitorVerid
        /// <summary>
        /// Retorna o template com apenas uma digital, solicitado atrav�s da fun��o
        ///CriarUsuarioLeitorVerid e retorna se a solicita��o foi processada com sucesso pelo Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Template">Array de bytes contendo toda as informa��es das digitais do usu�rio.
                                ///Estes dados devem ter sido coletados de um outro Inner Verid 
                                /// previamente e n�o dever� ser alterado.
                                ///Este template n�o � o completo, ele dever� conter somente uma digital.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoInclusaoUsuarioLeitorVerid(int Inner,  byte[] Template);
        #endregion

        #region SolicitarTotalUsuariosVerid
        /// <summary>
        /// Solicita para o Inner Verid o total de usu�rios cadastrados no equipamento.Ap�s executar
        ///essa fun��o o software dever� esperar pelo menos 75 milisegundos.
        ///Os dados s�o retornados na fun��o ReceberTotalUsuariosVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Modo">0 � Receber somente PIN, cart�o.(Somente para Verid Plus).
                            ///1 � Receber PIN mais as digitais.(Todos os Verid�s).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarTotalUsuariosVerid(int Inner, byte Modo);
        #endregion

        #region ReceberTotalUsuariosVerid
        /// <summary>
        /// Retorna a quantidade de usu�rios cadastrados no Inner Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Total">Total de usu�rios cadastrados.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberTotalUsuariosVerid(int Inner, ref int Total);
        #endregion

        #region LocalizarPrimeiroUsuarioVerid
        /// <summary>
        /// Solicita o primeiro/pr�ximo usu�rio cadastrado no Verid, a fun��o
        ///LocalizarPrimeiroUsuarioVerid dever� ser chamada somente no in�cio da busca 
        /// dos usu�rios no banco de dados do Inner Verid.
        /// Ap�s executar essa fun��o o software dever� esperar pelo menos 75 milisegundos.
        ///Os dados s�o retornados na fun��o ReceberUsuarioVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Modo">0 � Receber somente PIN, cart�o.(Somente para Verid Plus).
                            ///1 � Receber PIN mais as digitais.(Todos os Verid�s).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LocalizarPrimeiroUsuarioVerid(int Inner, byte Modo);
        #endregion

        #region LocalizarProximoUsuarioVerid
        /// <summary>
        /// Solicita o primeiro/pr�ximo usu�rio cadastrado no Verid, a fun��o
        ///LocalizarPrimeiroUsuarioVerid dever� ser chamada somente no in�cio da busca 
        /// dos usu�rios no banco de dados do Inner Verid.
        /// Ap�s executar essa fun��o o software dever� esperar pelo menos 75 milisegundos.
        ///Os dados s�o retornados na fun��o ReceberUsuarioVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Modo">0 � Receber somente PIN, cart�o.(Somente para Verid Plus).
                            ///1 � Receber PIN mais as digitais.(Todos os Verid�s).</param> 
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LocalizarProximoUsuarioVerid(int Inner, byte Modo);
        #endregion

        #region LocalizarUsuarioVerid
        /// <summary>
        /// Localiza o usu�rio com o n�mero do cart�o desejado no banco de dados do Inner Verid. Ap�s
        ///executar essa fun��o o software dever� esperar pelo menos 75 milisegundos.
        ///Os dados s�o retornados na fun��o ReceberUsuarioVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Modo">0 � Receber somente PIN, cart�o.(Somente para Verid Plus).
                            ///1 � Receber PIN mais as digitais.(Todos os Verid�s).</param>
        /// <param name="Digitos">Quantidade de d�gitos do cart�o.
                               ///1 a 16.</param>
        /// <param name="Cartao">N�mero do cart�o desejado.</param>
        /// <returns>128 � Cart�o inv�lido.
                  ///129 � Modo de opera��o inv�lido.
                  ///130 � Quantidade de d�gitos inv�lida.</returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte LocalizarUsuarioVerid(int Inner, byte Modo, byte Digitos, string Cartao);
        #endregion

        #region ReceberUsuarioVerid
        /// <summary>
        /// Retorna o usu�rio cadastrado no banco de dados do Inner Verid. A fun��o ir� retornar a
        ///quantidade de d�gitos do cart�o, o n�mero do cart�o e o template completo do usu�rio.
        ///O Verid pode retornar um usu�rio repetido, quando isso acontecer � necess�rio executar a fun��o
        ///LocalizarPrimeiroUsuarioVerid e iniciar a coleta de usu�rios novamente a partir do in�cio. Isso
        ///acontece pois o Inner Verid pode se perder na lista de usu�rios.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Modo">0 � Receber somente PIN, cart�o.(Somente para Verid Plus).
                            ///1 � Receber PIN mais as digitais.(Todos os Verid�s).</param>
        /// <param name="Digitos">Quantidade de d�gitos do cart�o.
                               ///1 a 16.</param>
        /// <param name="Template">Array de bytes contendo toda as informa��es das digitais do usu�rio.
                                ///Estes dados devem ter sido coletados de um outro 
                                ///Inner Verid previamente  n�o dever� ser alterado.
                                ///Este tamplate dever� ser o completo, com duas digitais.</param>
        /// <param name="Cartao">N�mero do cart�o desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberUsuarioVerid(int Inner, byte Modo, ref byte Digitos,  byte[] Template, string Cartao);
        #endregion

        #region ApagarUsuarioVerid
        /// <summary>
        /// Descri��o: Solicita a exclus�o do usu�rio na base de dados do Inner Verid. O retorno da exclus�o dever�
        ///ser verificado atrav�s da fun��o ResultadoExclusaoUsuarioVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Cartao">N�mero do cart�o desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte ApagarUsuarioVerid(int Inner, string Cartao);
        #endregion

        #region ResultadoExclusaoUsuarioVerid
        /// <summary>
        /// Retorna o resultado da exclus�o de um usu�rio do Inner Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoExclusaoUsuarioVerid(int Inner);
        #endregion

        #region ApagarTodosUsuariosVerid
        /// <summary>
        /// Solicita a exclus�o de todos os usu�rios cadastrados no Inner Verid. � necess�rio possuir a
        ///senha de administrador cadastrada diretamente na placa Verid. O Retorno da exclus�o dever� ser
        ///verificado atrav�s da fun��o ResultadoExclusaoTodosUsuariosVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="SenhaAdm">Senha do administrador da placa Verid.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte ApagarTodosUsuariosVerid(int Inner, string SenhaAdm);
        #endregion

        #region ResultadoExclusaoTodosUsuariosVerid
        /// <summary>
        /// Retorna se todos os usu�rio foram exclu�dos do banco de dados do Inner Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoExclusaoTodosUsuariosVerid(int Inner);
        #endregion

        #region CompararPINVerid
        /// <summary>
        /// Compara a digital cadastrada para o n�mero do cart�o desejado com a digital inserida pelo
        ///leitor biom�trico do Inner Verid. O resultado da opera��o � retornado atrav�s da fun��o
        ///ResultadoComparacaoPINVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Cartao">N�mero do cart�o desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte CompararPINVerid(int Inner, string Cartao);
        #endregion

        #region ResultadoComparacaoPINVerid
        /// <summary>
        /// Retorna o resultado da compara��o do usu�rio cadastrado com a digital inserida pelo leitor
        ///biom�trico do Inner Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoComparacaoPINVerid(int Inner);
        #endregion

        #region ConfigurarRedeVerid
        /// <summary>
        /// Configura como o Inner Verid ir� se comportar em uma rede de Verid�s. Se o Inner dever�
        ///enviar/receber as digitais cadadastradas para os demais Verid�s
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <param name="Envia">0 � Desabilita o envio das digitais.
                             ///1 � Habilita o envio das digitais.</param>
        /// <param name="Recebe">0 � Desabilita a recep��o das digitais.
                              ///1 � Habilita a recep��o das digitais.</param>
        /// <param name="BroadCast">0 � Desabilita o broadcast das digitais.
                                 ///1 � Habilita o broadcast das digitais.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarRedeVerid(int Inner, byte Envia, byte Recebe, byte BroadCast);
        #endregion

        #region ResultadoConfiguracaoRedeVerid
        /// <summary>
        /// Retorna se o Inner Verid aceitou as configura��es da rede Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 � Para comunica��o serial.
                             ///1 a 99 � Para comunica��o TCP/IP com porta vari�vel.
                             ///1 a ... � Para comunica��o TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoConfiguracaoRedeVerid(int Inner);
        #endregion

        #endregion

        #region URNA / DIVERSOS
        #region DevolverCartao
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Inner"></param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DevolverCartao(int Inner);
        #endregion

        #region EngolirCartao
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Inner"></param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EngolirCartao(int Inner);
        #endregion

        #region ConfigurarLeitorProximidadeHIDAbaTrack2

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarLeitorProximidadeHIDAbaTrack2();
        #endregion

        #region ConfigurarLeitorProximidadeMotorolaAbaTrack2
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarLeitorProximidadeMotorolaAbaTrack2();
        #endregion
        
        #region ConfigurarLeitorProximidadeWiegand
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarLeitorProximidadeWiegand();
        #endregion

        #region ConfigurarLeitorProximidadeSmartCard
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarLeitorProximidadeSmartCard();
        #endregion

        #region ConfigurarLeitorProximidadeAcura
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarLeitorProximidadeAcura();
        #endregion

        #region ConfigurarLeitorProximidadeWiegandFacilityCode
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarLeitorProximidadeWiegandFacilityCode();
        #endregion

        #region ConfigurarLeitorProximidadeSmartCardAcura
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarLeitorProximidadeSmartCardAcura();
        #endregion

        #region ResultadoEnvioAjustesBio
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoEnvioAjustesBio(int Inner, byte OnLine);
        #endregion

        #endregion

        #endregion
    }
}
