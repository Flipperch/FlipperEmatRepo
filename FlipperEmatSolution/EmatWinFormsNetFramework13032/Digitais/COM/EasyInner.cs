//******************************************************************************
//A Topdata Sistemas de Automação Ltda não se responsabiliza por qualquer
//tipo de dano que este software possa causar, este exemplo deve ser utilizado
//apenas para demonstrar a comunicação com os equipamentos da linha
//inner e não deve ser alterado, por este motivo ele não deve ser incluso em
//suas aplicações comerciais.
//
//Desenvolvido em C#.
//                                           Topdata Sistemas de Automação Ltda.
//******************************************************************************


//******************************************************************************
//Comunicação com a DLL "EasyInner.dll"
//Todos os métodos estão descritos no manual do desenvolvedor que acompanha
//a instalação da SDK
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
        #region Métodos da EasyInner


        #region RetornarSegundosSys

        [DllImport("Kernel32", CallingConvention = CallingConvention.Winapi)]
        private static extern int GetTickCount();
        
        /// <summary>
        /// Método que retorna os segundos do Sistema.
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
        /// Define qual será o tipo de conexão(meio de comunicação) que será utilizada pela dll para
        ///comunicar com os Inners. Essa função deverá ser chamada antes de iniciar o processo de comunicação
        ///e antes da função AbrirPortaComunicacao.
        /// </summary>
        /// <param name="Tipo"> 0 - Comunicação serial, RS-232/485. 
                            /// 1 - Comunicação TCP/IP com porta variável.
                            /// 2 - Comunicação TCP/IP com porta fixa (Default).
                            /// 3 - Comunicação via modem.
                            /// 4 – Comunicação via TopPendrive.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirTipoConexao(byte Tipo);
        #endregion

        #region AbrirPortaComunicacao
        /// <summary>
        /// Abre a porta de comunicação desejada, essa função deverá ser chamada antes de iniciar
        ///qualquer processo de transmissão ou recepção de dados com o Inner.Esta função deve ser chamada apenas uma vez e no início da comunicação, e não deve ser chamada
        ///para cada Inner.
        /// </summary>
        /// <param name="Porta">Número da porta serial ou TCP/IP.
        ///- Para comunicação TCP/IP o valor padrão da porta é 3570 (Default).
        ///- Para comunicação Serial/Modem o valor padrão da porta é 1, COM 1(Default).
        ///- Para a comunicação TopPendrive o valor é 3 (Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte AbrirPortaComunicacao(int Porta);
        #endregion

        #region FecharPortaComunicacao
        /// <summary>
        /// Fecha a porta de comunicação previamente aberta, seja ela serial, Modem ou TCP/IP.
        ///Em modo Off-Line normalmente é chamada após enviar/receber todos os dados do Inner.
        ///Em modo On-Line é chamada somente no ecerramento do software do software.
        /// </summary>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern void FecharPortaComunicacao();
        #endregion

        #region DefinirPadraoCartao
        /// <summary>
        /// Define qual padrão de cartão será utilizado pelos Inners, padrão Topdata ou padrão livre. O
        ///padrão Topdata de cartão está descrito no manual dos equipamentos e é utilizado somente com os
        ///Inners em modo Off-Line. No padrão livre todos os dígitos do cartão são considerados como matrícula,
        ///ele pode ser utilizado no modo On Line ou no modo Off Line.
        ///Ao chamar essa função, a quantidade de dígitos é setada para 14.
        /// </summary>
        /// <param name="Padrao">0 - Padrão Topdata. 
                              ///1 - Padrão livre (Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirPadraoCartao(byte Padrao);
        #endregion

        #region AcionarRele1
        /// <summary>
        /// Aciona(atraca) o rele 1 do Inner. Este comando não deve ser utilizado nas catracas.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte AcionarRele1(int Inner);
        #endregion

        #region AcionarRele2
        /// <summary>
        /// Aciona(atraca) o rele 2 do Inner. Este comando não deve ser utilizado nas catracas.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte AcionarRele2(int Inner);
        #endregion 

        #region ManterRele1Acionado
        /// <summary>
        /// Mantém acionado(atracado) o rele 1 do Inner até que o comando DesabilitarRele1 seja
        ///enviado. Este comando não deve ser utilizado nas catracas.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ManterRele1Acionado(int Inner);
        #endregion
        
        #region ManterRele2Acionado
        /// <summary>
        /// Mantém acionado(atracado) o rele 2 do Inner até que o comando DesabilitarRele2 seja
        ///enviado. Este comando não deve ser utilizado nas catracas.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ManterRele2Acionado(int Inner);
        #endregion 
        
        #region DesabilitarRele1
        /// <summary>
        /// Desaciona(desatraca) o rele 1 previamente acionado com o comando
        ///ManterRele1Acionado. Este comando não deve ser utilizado nas catracas.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DesabilitarRele1(int Inner);
        #endregion
        
        #region DesabilitarRele2
        /// <summary>
        /// Desaciona(desatraca) o rele 2 previamente acionado com o comando
        ///ManterRele2Acionado. Este comando não deve ser utilizado nas catracas.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DesabilitarRele2(int Inner);
        #endregion
        
        #region AcionarBipCurto
        /// <summary>
        /// Faz com que o Inner emita um bip curto(aviso sonoro).
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte AcionarBipCurto(int Inner);
        #endregion
        
        #region AcionarBipLongo
        /// <summary>
        /// Faz com que o Inner emita um bip longo(aviso sonoro).
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte AcionarBipLongo(int Inner);
        #endregion
        
        #region Ping
        /// <summary>
        /// Testa a comunicação com o Inner, também utilizado para efetuar a conexão com o Inner.
        ///Para efetuar a conexão com o Inner, essa função deve ser executada em um loop até retornar 0(zero),
        ///executado com sucesso.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte Ping(int Inner);
        #endregion

        #region PingOnLine
        /// <summary>
        /// Testa comunicação com o Inner e mantém o Inner em OnLine quando a mudança automática
        ///está configurada. Especialmente indicada para a verificação da conexão em comunicação TCP/IP.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte PingOnLine(int Inner);
        #endregion

        #region LigarBackLite
        /// <summary>
        /// Liga a luz emitida pelo display do Inner. Essa função não deve ser utilizada nas catracas.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LigarBackLite(int Inner);
        #endregion
        
        #region DesligarBackLite
        /// <summary>
        /// Desliga a luz emitida pelo display do Inner. Essa função não deve ser utilizada nas catracas.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DesligarBackLite(int Inner);
        #endregion
        
        #region LigarBipIntermitente
        /// <summary>
        /// Faz com que o Inner acione o bip de forma intermitente, ou seja, o Inner irá emitir um aviso
        ///sonoro repetidamente. Essa função não deve ser utilizada nas catracas.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LigarBipIntermitente(int Inner);
        #endregion
        
        #region DesligarBipIntermitente
        /// <summary>
        /// Faz com que o Inner desabilite o bip acionado pela função LigarBipIntermitente. Essa
        ///função não deve ser utilizada nas catracas.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DesligarBipIntermitente(int Inner);
        #endregion
        
        #region LiberarCatracaEntrada
        /// <summary>
        /// Libera a catraca no sentido de entrada padrão do Inner, para o usuário poder efetuar o giro
        ///na catraca. Em modo On-Line, na função ReceberDadosOnLine o valor retornado no parâmetro
        ///“Complemento” será do tipo entrada.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LiberarCatracaEntrada(int Inner);
        #endregion
        
        #region LiberarCatracaSaida
        /// <summary>
        /// Libera a catraca no sentido de saída padrão do Inner, para o usuário poder efetuar o giro na
        ///catraca. Em modo On-Line, na função ReceberDadosOnLine o valor retornado no parâmetro
        ///“Complemento” será do tipo saída. 
        /// Essa função deve ser utilizada somente com Inners Catraca.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LiberarCatracaSaida(int Inner);
        #endregion
        
        #region LiberarCatracaEntradaInvertida
        /// <summary>
        /// Libera a catraca no sentido contrário a entrada padrão do Inner, para o usuário poder efetuar
        ///o giro na catraca. Em modo On-Line, na função ReceberDadosOnLine o valor retornado no parâmetro
        ///“Complemento” será do tipo saída.
        /// Essa função deve ser utilizada somente com Inners Catraca.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LiberarCatracaEntradaInvertida(int Inner);
        #endregion
        
        #region LiberarCatracaSaidaInvertida
        /// <summary>
        /// Libera a catraca no sentido contrário a saída padrão do Inner, para o usuário poder efetuar o
        ///giro na catraca. Em modo On-Line, na função ReceberDadosOnLine o valor retornado no parâmetro
        ///“Complemento” será do tipo entrada.
        ///Essa função deve ser utilizada somente com Inners Catraca.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LiberarCatracaSaidaInvertida(int Inner);
        #endregion
        
        
        #region LiberarCatracaDoisSentidos
        /// <summary>
        /// Libera a catraca para o usuário pode efetuar o giro na catraca em ambos os sentidos. Em
        ///modo On-Line, na função ReceberDadosOnLine o valor retornado no parâmetro “Complemento” será
        ///do tipo entrada ou saída, dependendo do sentido em que o usuário passar pela catraca.
        ///Essa função deve ser utilizada somente com Inners Catraca.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LiberarCatracaDoisSentidos(int Inner);
        #endregion

        #region LevantarParaOnLine
        /// <summary>
        /// Após o Inner cair para Off-Line e recuperar a conexão, este comando já inicializa o Inner para
        ///On-Line sem a necessidade de enviar o comando de configurações novamente.
        ///Para esta função funcionar corretamente, é necessário que o Inner tenha sido configurado com a função
        ///EnviarConfiguracoesMudancaAutomaticaOnLineOffLine previamente.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LevantarParaOnLine(int Inner);
        #endregion
        
        
        #region ReceberVersaoFirmware
        /// <summary>
        /// Solicita a versão do firmware do Inner e dados como o Idioma, se é uma versão especial.
        /// </summary>
        /// <param name="Inner">Número do Inner desejado.</param>
        /// <param name="Linha">01 – Inner Plus.
        ///02 – Inner Disk.
        ///03 – Inner Verid.
        ///06 – Inner Bio.
        ///07 – Inner NET.</param>
        /// <param name="Variacao">Depende da versão, existe somente em versões customizadas.</param>
        /// <param name="VersaoAlta">00 a 99.</param>
        /// <param name="VersaoBaixa">00 a 99.</param>
        /// <param name="VersaoSufixo">Indica o idioma do firmware:
        ///01 – Português.
        ///02 – Espanhol.
        ///03 – Inglês.
        ///04 – Francês.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberVersaoFirmware(int Inner, ref byte Linha, ref short Variacao, ref byte VersaoAlta, ref byte VersaoBaixa, ref byte VersaoSufixo, ref byte Ruf);

        #endregion 
        
        #endregion

        #region FUNÇÕES DE CONFIGURAÇÕES GERAIS DOS INNERS

        #region ConfigurarInnerOffLine
        /// <summary>
        /// Prepara o Inner para trabalhar no modo Off-Line, porém essa função ainda não envia essa
        ///informação para o equipamento.
        /// </summary>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarInnerOffLine();
        #endregion

        #region ConfigurarInnerOnLine
        /// <summary>
        /// Prepara o Inner para trabalhar no modo On-Line, porém essa função ainda não envia essa
        ///informação para o equipamento.
        /// </summary>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarInnerOnLine();
        #endregion

        #region HabilitarTeclado
        /// <summary>
        /// Permite que os dados sejam inseridos no Inner através do teclado do equipamento.
        ///Habilitando o parâmetro ecoar, o teclado irá ecoar asteriscos no display do Inner.
        /// </summary>
        /// <param name="Habilita">0 - Desabilita o teclado (Default).
                                ///1 - Habilita o teclado.</param>
        /// <param name="Ecoar">0 – Ecoa o que é digitado no display do Inner (Default).
                             ///1 – Ecoa asteriscos no display do Inner.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte HabilitarTeclado(byte Habilita, byte Ecoar);
        #endregion

        #region ConfigurarAcionamento1
        /// <summary>
        /// Configura como irá funcionar o acionamento(rele) 1 do Inner, e por quanto tempo ele será acionado.
        /// </summary>
        /// <param name="Funcao">  0 – Não utilizado(Default).
                                ///1 – Aciona ao registrar uma entrada ou saída.
                                ///2 – Aciona ao registrar uma entrada.
                                ///3 – Aciona ao registrar uma saída.
                                ///4 – Está conectado a uma sirene(Ver as funções de sirene).
                                ///5 – Utilizado para a revista de usuários(Ver a função DefinirPorcentagemRevista).
                                ///6 – Catraca com a saída liberada.
                                ///7 – Catraca com a entrada liberada
                                ///8 – Catraca liberada nos dois sentidos.
                                ///9 – Catraca liberada nos dois sentidos e a marcação(registro) é gerada de acordo com o sentido do giro.</param>
        /// <param name="Tempo">0 a 50 segundos.
                             ///0(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarAcionamento1(byte Funcao, byte Tempo);
        #endregion

        #region ConfigurarAcionamento2
        /// <summary>
        /// Configura como irá funcionar o acionamento(rele) 2 do Inner, e por quanto tempo ele será acionado.
        /// </summary>
        /// <param name="Funcao">  0 – Não utilizado(Default).
                                ///1 – Aciona ao registrar uma entrada ou saída.
                                ///2 – Aciona ao registrar uma entrada.
                                ///3 – Aciona ao registrar uma saída.
                                ///4 – Está conectado a uma sirene(Ver as funções de sirene).
                                ///5 – Utilizado para a revista de usuários(Ver a função DefinirPorcentagemRevista).
                                ///6 – Catraca com a saída liberada.
                                ///7 – Catraca com a entrada liberada
                                ///8 – Catraca liberada nos dois sentidos.
                                ///9 – Catraca liberada nos dois sentidos e a marcação(registro) é gerada de acordo com o sentido do giro.</param>
        /// <param name="Tempo">0 a 50 segundos.
                             ///0(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarAcionamento2(byte Funcao, byte Tempo);
        #endregion

        #region ConfigurarTipoLeitor
        /// <summary>
        /// Configura o tipo do leitor que o Inner está utilizando, se é um leitor de código de barras,
        /// magnético ou proximidade.
        /// </summary>
        /// <param name="Tipo">0 – Leitor de código de barras(Default).
                            ///1 – Leitor Magnético.
                            ///2 – Leitor proximidade AbaTrack2.
                            ///3 – Leitor proximidade Wiegand e Wiegand Facility Code.
                            ///4 – Leitor proximidade Smart Card.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarTipoLeitor(byte Tipo);
        #endregion

        #region ConfigurarLeitor1
        /// <summary>
        /// Configura as operações que o leitor irá executar. Se irá registrar os dados somente como
        ///entrada independente do sentido em que o cartão for passado, somente como saída ou como entrada e saída.
        /// </summary>
        /// <param name="Operacao">0 – Leitor desativado(Default).
                                ///1 – Somente para entrada.
                                ///2 – Somente para saída.
                                ///3 – Entrada e saída.
                                ///4 – Entrada e saída invertidas.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarLeitor1(byte Operacao);
        #endregion

        #region ConfigurarLeitor2
        /// <summary>
        /// Configura as operações que o leitor irá executar. Se irá registrar os dados somente como
        ///entrada independente do sentido em que o cartão for passado, somente como saída ou como entrada e saída.
        /// </summary>
        /// <param name="Operacao">0 – Leitor desativado(Default).
                                ///1 – Somente para entrada.
                                ///2 – Somente para saída.
                                ///3 – Entrada e saída.
                                ///4 – Entrada e saída invertidas.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarLeitor2(byte Operacao);
        #endregion

        #region DefinirCodigoEmpresa
        /// <summary>
        /// Define o código da empresa utilizado nos cartões, válido somente quando se está utilizando
        ///o padrão Topdata de cartão.
        /// </summary>
        /// <param name="Codigo">0 a 999.
                              ///0(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirCodigoEmpresa(int Codigo);
        #endregion

        #region DefinirNivelAcesso
        /// <summary>
        /// Define o nível de acesso aceito por este Inner, deve ser utilizado somente nos Inners que
        ///estão configurados para utilizar cartões no padrão Topdata.
        /// </summary>
        /// <param name="Nivel">0 a 9.
                             ///0(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirNivelAcesso(byte Nivel);
        #endregion

        #region UtilizarSenhaAcesso
        /// <summary>
        /// Configura o Inner para solicitar a senha de acesso cadastrada no cartão do usuário, essa
        ///opção é válida somente para Inners que estejam configurados para utilizar o padrão Topdata de cartão.
        /// </summary>
        /// <param name="Utiliza">0 – Não solicita a senha de acesso(Default).
                               ///1 – Solicita a senha de acesso.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte UtilizarSenhaAcesso(byte Utiliza);
        #endregion

        #region DefinirTipoListaAcesso
        /// <summary>
        /// Define qual tipo de lista(controle) de acesso o Inner vai utilizar. Após habilitar a lista de
        ///acesso é necessário preencher a lista e os horários de acesso, verificar os as funções de “Horários de
        ///Acesso” e as funções da “Lista de Acesso”.
        /// </summary>
        /// <param name="Tipo">0 – Não utilizar a lista de acesso.
                            ///1 – Utilizar lista branca(cartões fora da lista tem o acesso negado).
                            ///2 – Utilizar lista negra(bloqueia apenas os cartões da lista).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirTipoListaAcesso(byte Tipo);
        #endregion

        #region DefinirQuantidadeDigitosCartao
        /// <summary>
        /// Define a quantidade de dígitos dos cartões a serem lidos pelo Inner.
        /// </summary>
        /// <param name="Quantidade">1 a 16 dígitos.
                                 ///14(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirQuantidadeDigitosCartao(byte Quantidade);
        #endregion

        #region AvisarQuandoMemoriaCheia
        /// <summary>
        /// Configura o Inner para avisar quando a memória que armazena os bilhetes Off-Line estiver 50% cheia.
        /// </summary>
        /// <param name="Avisa">0 – Desabilita o aviso de memória cheia(Default).
                             ///1 – Habilita o aviso de memória cheia.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte AvisarQuandoMemoriaCheia(byte Avisa);
        #endregion

        #region DefinirPorcentagemRevista
        /// <summary>
        /// Define a porcentagem de cartões que serão selecionados para a revista ao passarem pela
        ///saída do Inner. Quando um cartão é selecionado o Inner emite um aviso sonoro, uma mensagem no
        ///display e aciona o acionamento(rele) que esteja habilitado para a revista(ver as funções
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
        /// Configura o Inner para registrar as tentativa de acesso negado. O Inner irá rgistrar apenas os
        ///acessos negados em relação a lista de acesso configurada para o modo Off-Line, ver as funções
        ///DefinirTipoListaAcesso e ColetarBilhete.
        /// </summary>
        /// <param name="TipoRegistro">0 – Não registrar o acesso negado.
                                    ///1 – Apenas o acesso negado.
                                    ///2 – Falha na validação da digital.
                                    ///3 – Acesso negado e falha na validação da digital.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte RegistrarAcessoNegado(byte TipoRegistro);
        #endregion

        #region CartaoMasterLiberaAcesso
        /// <summary>
        /// Configura o Inner para permitir que o cartão master(cartão mestre) libere o acesso para
        ///cartões que estão bloqueados pela lista de acesso. O cartão mestre do Inner deve ser informado através
        ///da função DefinirNumeroCartaoMaster.
        /// </summary>
        /// <param name="Libera">0 – Não libera o acesso(Default).
                              ///1 – Libera o acesso.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte CartaoMasterLiberaAcesso(byte Libera);
        #endregion

        #region DefinirLogicaRele
        /// <summary>
        /// OBSOLETA:
        /// Define o lógica que o Inner irá utilizar nos reles(acionamentos), se os reles ficarão
        ///normalmente abertos(NA), ou seja, os reles ficarão desacionados, ou se os reles ficarão normalmente
        ///fechados(NF), ou seja, os reles ficarão acionados.
        ///É altamente recomendável não alterar esses valores, a não ser que seja necessário. Essa função é
        ///obsoleta e não deve ser utilizada.
        /// </summary>
        /// <param name="Logica">0 – Normalmente aberto.
                              ///1 – Normalmente fechado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirLogicaRele(byte Logica);
        #endregion

        #region DesabilitarBloqueioCatracaMicroSwitch
        /// <summary>
        /// OBSOLETA:
        /// Função obsoleta, não é mais utilizada. Essa função era utilizada por catracas muito antigas,
        ///ela configurava a catraca para não bloquear automaticamente a passagem forçada pela catraca.
        /// </summary>
        /// <param name="Desabilita">0 – Habilita o bloqueio(Default).
                                  ///1 – Desabilita o bloqueio automático.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DesabilitarBloqueioCatracaMicroSwitch(byte Desabilita);
        #endregion

        #region DefinirFuncaoDefaultLeitoresProximidade
        /// <summary>
        /// Define qual será o tipo do registro realizado pelo Inner ao aproximar um cartão do tipo
        ///proximidade no leitor do Inner, sem que o usuário tenha pressionado a tecla entrada, saída ou função.
        /// </summary>
        /// <param name="Funcao">0 – Desablitado(Default).
                              ///1 a 9 – Registrar como uma função do teclado do Inner.
                              ///10 – Registrar sempre como entrada.
                              ///11 – Registrar sempre como saída.
                              ///12 – Libera a catraca nos dois sentidos e registra o bilhete conforme o sentido giro.
        /// </param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirFuncaoDefaultLeitoresProximidade(byte Funcao);
        #endregion

        #region DefinirNumeroCartaoMaster
        /// <summary>
        /// Configura qual será o número do cartão master que o Inner irá aceitar. Válido somente para
        ///o padrão livre de cartão. Para o padrão Topdata o número do master sempre é 0.
        /// </summary>
        /// <param name="Master">0 a 99999999999999 (Máximo de 14 dígitos)
                              ///0(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirNumeroCartaoMaster(string Master);
        #endregion

        #region DefinirFormasPictogramasMillenium
        /// <summary>
        /// OBSOLETA:
        /// Função obsoleta, não deve ser mais utilizada. Essa função configura a forma dos led’s dos
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
        /// Desabilita o bip contínuo utilizada pela catraca para avisar que alguém está forçando a
        ///passagem pelo equipamento.
        /// </summary>
        /// <param name="Desabilita">0 – Não desabilita o bip(Default).
                                  ///1 – Desabilita bip.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DesabilitarBipCatraca(byte Desabilita);
        #endregion

        #region DefinirEventoSensor
        /// <summary>
        /// Altera a forma de como os eventos dos sensores do Inner deverão ser disparados. Não é
        ///acomselhável alterar estes valores, a não ser que seja extramamente necessário. Não funciona nas catracas.
        /// </summary>
        /// <param name="Sensor">1 a 3, número do sensor a ser configurado.</param>
        /// <param name="Evento">0 – não gera evento(Default).
                              ///1 – gera evento de subida (0->1).
                              ///2 – gera evento de descida (1->0).
                              ///3 – ambos.
                              ///4 – subida acionando bip.
                              ///5 – descida acionando bip.</param>
        /// <param name="Tempo">1 a 50, tempo para acionar o bip após o evento ocorrer.
                             ///0(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirEventoSensor(byte Sensor, byte Evento, byte Tempo);
        #endregion

        #region PermitirCadastroInnerBioVerid
        /// <summary>
        /// Permite que os cadastros de novos usuário sejam realizados pelo menu do cartão master,
        ///apenas para Inners da linha Bio e Verid.
        /// </summary>
        /// <param name="Permite">0 – Não permitir cadastro(Default).
                               ///1 – Permitir cadastro.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte PermitirCadastroInnerBioVerid(byte Permite);
        #endregion

        #region ReceberDataHoraDadosOnLine
        /// <summary>
        /// Configura o Inner para enviar as informações de data/hora nos bilhete on line, esses dados
        ///serão retornados nos parâmetros da função ReceberDadosOnLine.
        /// </summary>
        /// <param name="Recebe">0 – Não receber a data/hora do bilhete(Default).
                              ///1 – Recebe a data/hora do bilhete.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberDataHoraDadosOnLine(byte Recebe);
        #endregion

        #region InserirQuantidadeDigitoVariavel
        /// <summary>
        /// Configura o Inner para ler cartão que possam variar de 1 dígito até 16 dígitos. Para habilitar a
        ///quantidade de dígitos desejada basta chamar a função passando o número de dígitos que o Inner deverá suportar.
        /// </summary>
        /// <param name="Digito">0 – Desabilita a leitura de cartões com quantidade de dígitos diferentes(Default).
                              ///1 a 16 – Quantidade de dígitos a ser lida.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte InserirQuantidadeDigitoVariavel(byte Digito);
        #endregion

        #region ConfigurarWiegandDoisLeitores
        /// <summary>
        /// Habilita os leitores wiegand para o primeiro leitor e o segundo leitor do Inner, e configura se o
        ///segundo leitor irá exibir as mensagens configuradas.
        /// </summary>
        /// <param name="Habilita">0 – Não habilita o segundo leitor como wiegand(Default).
                                ///1 – Habilita o segundo leitor como wiegand.</param>
        /// <param name="ExibirMensagem">0 – Não exibe mensagem segundo leitor(Default).
                                      ///1 – Exibe mensagem segundo leitor.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarWiegandDoisLeitores(byte Habilita, byte ExibirMensagem);
        #endregion

        #region DefinirFuncaoDefaultSensorBiometria
        /// <summary>
        /// Configura o tipo de registro que será associado a uma marcação, quando for inserido o dedo
        ///no Inner bio sem que o usuário tenha definido se é um entrada, saída, função, etc.
        /// </summary>
        /// <param name="Funcao">0 – desabilitada(Default).
                              ///1 a 9 – funções de 1 a 9.
                              ///10 – entrada.
                              ///11 – saída.
                              ///12 – libera catraca para os dois lados e registra bilhete conforme o giro.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirFuncaoDefaultSensorBiometria(byte Funcao);
        #endregion

        #region EnviarConfiguracoes
        /// <summary>
        /// Envia o buffer interno da dll que contém todas as configurações das funções anteriores para
        ///o Inner, após o envio esse buffer é limpo sendo necessário chamar novamentes as funções acima para
        ///reconfigurá-lo.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarConfiguracoes(int Inner);
        #endregion

        #endregion

        #region FUNÇÕES PARA MANIPULAR DATA/HORA INNER

        #region EnviarRelogio
        /// <summary>
        /// Configura o relógio(data/hora) do Inner.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
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
        /// Solicita a data/hora atualmente configurada no Inner. Os dados são retornados por referência
        ///nos parâmetros da função.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
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
        /// Configura a data/hora de início e fim do horário de verão.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
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

        #region FUNÇÕES PARA MANIPULAR HORÁRIOS DE ACESSO

        #region ApagarHorariosAcesso
        /// <summary>
        /// Apaga o buffer com a lista de horários de acesso e envia automaticamente para o Inner.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ApagarHorariosAcesso(int Inner);
        #endregion

        #region InserirHorarioAcesso
        /// <summary>
        /// Insere no buffer da dll um horário de acesso. O Inner possui uma tabela de 100 horários de
        ///acesso, para cada horário é possível definir 4 faixas de acesso para cada dia da semana.
        /// </summary>
        /// <param name="Horario">1 a 100 – Número da tabela de horários.</param>
        /// <param name="DiaSemana">1 a 7 – Dia da semana a qual pertence a faixa de horário.</param>
        /// <param name="FaixaDia">1 a 4 – Para cada dia da semana existem 4 faixas de horário.</param>
        /// <param name="Hora">0 a 23</param>
        /// <param name="Minuto">0 a 59</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte InserirHorarioAcesso(byte Horario, byte DiaSemana, byte FaixaDia, byte Hora, byte Minuto);
        #endregion

        #region EnviarHorariosAcesso
        /// <summary>
        /// Envia para o Inner o buffer com a lista de horários de acesso, após executar o comando o
        ///buffer é limpo pela dll automaticamente.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarHorariosAcesso(int Inner);
        #endregion

        #endregion

        #region FUNÇÕES PARA MANIPULAR LISTA DE ACESSO

        #region ApagarListaAcesso
        /// <summary>
        /// Limpar o buffer com a lista de usuários cadastrados e envia automaticamente para o Inner.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ApagarListaAcesso(int Inner);
        #endregion

        #region InserirUsuarioListaAcesso
        /// <summary>
        /// Insere no buffer da dll um usuário da lista e a qual horário de acesso ele está associado. Os
        ///horários já deverão ter sido cadastrados através das funções InserirHorarioAcesso e enviados através
        ///da função EnviarHorariosAcesso para a lista ter o efeito correto.
        /// </summary>
        /// <param name="Cartao">1 a ... – Dependo do padrão de cartão definido e da quantidade de dígitos definda.</param>
        /// <param name="Horario">1 a 100 – Número do horário já cadastrado no Inner.
                               ///101 – Acesso sempre liberado para o usuário.
                               ///102 – Acesso sempre negado para o usuário.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte InserirUsuarioListaAcesso(string Cartao, byte Horario);
        #endregion

        #region EnviarListaAcesso
        /// <summary>
        /// Envia o buffer com os usuários da lista de acesso para o Inner, após executar o comando o
        ///buffer é limpo pela dll automaticamente.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarListaAcesso(int Inner);
        #endregion

        #endregion

        #region FUNÇÕES PARA MANIPULAR AS MENSAGENS ONLINE DO INNER

        #region EnviarMensagemPadraoOnLine
        /// <summary>
        /// Envia para o Inner a mensagem padrão(fixa) que será sempre exibida pelo Inner. Essa
        ///mensagem é exibida enquanto o Inner estiver ocioso. Caso a mensagem passe de 32 caracteres a DLL
        ///irá utilizar os primeiros 32 caracteres.
        ///O Inner não aceita caracteres com acentuação, padrão UNICODE ou padrão ANSI.
        ///O Inner aceita apenas os caracteres do padrão ASCII.
        /// </summary>
        /// <param name="Inner">   1 a 32 – Para comunicação serial.
                                ///1 a 99 – Para comunicação TCP/IP com porta variável.
                                ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="ExibirData">0 – Não exibe a data/hora na linha superior do display.
                                  ///1 – Exibe a data/hora na linha superior do display.</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja
                                ///exibindo a data/hora o tamanho da mensagem passa a ser
                                ///16 ao invés de 32. Caso seja passado uma string vazia o
                                ///Inner exibirá a mensagem em branco no display.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarMensagemPadraoOnLine(int Inner, byte ExibirData, string Mensagem);
        #endregion

        #region EnviarMensagemTemporariaOnLine
        /// <summary>
        /// Envia para o Inner uma mensagem temporária. Caso a mensagem passe de 32 caracteres a
        ///DLL irá utilizar os primeiros 32 caracteres.
        ///O Inner não aceita caracteres com acentuação, padrão UNICODE ou padrão ANSI.
        ///O Inner aceita apenas os caracteres do padrão ASCII.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="ExibirData">0 – Não exibe a data/hora na linha superior do display.
                                  ///1 – Exibe a data/hora na linha superior do display.</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja
                                ///exibindo a data/hora o tamanho da mensagem passa a ser
                                ///16 ao invés de 32. Caso seja passado uma string vazia o
                                ///Inner exibirá a mensagem em branco no display.</param>
        /// <param name="Tempo">Tempo, em segundos, que a mensagem ficará na display. 1 a 50.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarMensagemTemporariaOnLine(int Inner, byte ExibirData, string Mensagem, byte Tempo);
        #endregion

        #endregion

        #region FUNÇÕES PARA MANIPULAR AS MENSAGENS OFFLINE DO INNER

        #region DefinirMensagemEntradaOffLine
        /// <summary>
        /// Configura a mensagem a ser exibida quando o usuário passar o cartão no sentido de entrada
        ///do Inner. Caso a mensagem passe de 32 caracteres a DLL irá utilizar os primeiros 32 caracteres.
        ///O Inner não aceita caracteres com acentuação, padrão UNICODE ou padrão ANSI.
        ///O Inner aceita apenas os caracteres do padrão ASCII.
        /// </summary>
        /// <param name="ExibirData">0 – Não exibe a data/hora na linha superior do display.
                                  ///1 – Exibe a data/hora na linha superior do display(Default).</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja
                                ///exibindo a data/hora o tamanho da mensagem passa a ser
                                ///16 ao invés de 32. Caso seja passado uma string vazia o
                                ///Inner exibirá a mensagem em branco no display.
                                ///“Entrada OK”(Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirMensagemEntradaOffLine(byte ExibirData, string Mensagem);
        #endregion

        #region DefinirMensagemSaidaOffLine
        /// <summary>
        /// Configura a mensagem a ser exibida quando o usuário passar o cartão no sentido de saída
        ///do Inner. Caso a mensagem passe de 32 caracteres a DLL irá utilizar os primeiros 32 caracteres.
        ///O Inner não aceita caracteres com acentuação, padrão UNICODE ou padrão ANSI.
        ///O Inner aceita apenas os caracteres do padrão ASCII.
        /// </summary>
        /// <param name="ExibirData">0 – Não exibe a data/hora na linha superior do display.
                                  ///1 – Exibe a data/hora na linha superior do display(Default).</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja
                                ///exibindo a data/hora o tamanho da mensagem passa a ser
                                ///16 ao invés de 32. Caso seja passado uma string vazia o
                                ///Inner exibirá a mensagem em branco no display.
                                ///“Saida OK” (Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirMensagemSaidaOffLine(byte ExibirData, string Mensagem);
        #endregion

        #region DefinirMensagemPadraoOffLine
        /// <summary>
        /// Configura a mensagem a ser exibida pelo Inner quando ele estiver ocioso. Caso a
        ///mensagem passe de 32 caracteres a DLL irá utilizar os primeiros 32 caracteres.
        ///O Inner não aceita caracteres com acentuação, padrão UNICODE ou padrão ANSI.
        ///O Inner aceita apenas os caracteres do padrão ASCII.
        /// </summary>
        /// <param name="ExibirData">0 – Não exibe a data/hora na linha superior do display.
                                  ///1 – Exibe a data/hora na linha superior do display.</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja
                                ///exibindo a data/hora o tamanho da mensagem passa a ser
                                ///16 ao invés de 32. Caso seja passado uma string vazia o
                                ///Inner exibirá a mensagem em branco no display.
                                ///“Passe o cartao” (Default).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirMensagemPadraoOffLine(byte ExibirData, string Mensagem);
        #endregion

        #region DefinirMensagemFuncaoOffLine
        /// <summary>
        /// Configura a mensagem a ser exibida quando o usuário passar o cartão utilizando uma das
        ///funções do Inner(de 0 a 9) e a habilita ou desabilita essas funções. Caso a mensagem passe de 32
        ///caracteres a DLL irá utilizar os primeiros 32 caracteres.
        ///O Inner não aceita caracteres com acentuação, padrão UNICODE ou padrão ANSI.
        ///O Inner aceita apenas os caracteres do padrão ASCII.
        /// </summary>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja
                                ///exibindo a data/hora o tamanho da mensagem passa a ser
                                ///16 ao invés de 32. Caso seja passado uma string vazia o
                                ///Inner não exibirá a mensagem no display.</param>
        /// <param name="Funcao">0 a 9.</param>
        /// <param name="Habilitada">0 – Desabilita a função do Inner(Default).
                                  ///1 – Habilita a função do Inner.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirMensagemFuncaoOffLine(string Mensagem, byte Funcao, byte Habilitada);
        #endregion

        #region HabilitarScoreMensagemOffLine
        /// <summary>
        /// Configura se a mensagem de entrada/saída irá exibir o score da digital no display do Inner Bio.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Tipo">0 – Entrada.
                            ///1 – Saída</param>
        /// <param name="Habilitar">0 – Desabilita a exibição do score.
                                 ///1 – Habilita a exibição do score.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte HabilitarScoreMensagemOffLine(int Inner, byte Tipo, byte Habilitar);
        #endregion

        #region EnviarMensagensOffLine
        /// <summary>
        /// Envia o buffer com todas as mensagens off line configuradas anteriormente, para o Inner.
        ///Após executar a função, o buffer com as mensagens é limpo automaticamente pela dll.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarMensagensOffLine(int Inner);
        #endregion

        #region ApagarMensagensOffLine
        /// <summary>
        /// Limpa o buffer com as mensagens, setando com as mensagens default do Inner, e envia o
        ///buffer para o Inner automaticamente.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ApagarMensagensOffLine(int Inner);
        #endregion

        #region DefinirConfiguracoesFuncoes
        /// <summary>
        /// Configura a reação do Inner para cada função de forma individual, ou seja, se ao utilizar a
        ///função 0 o Inner vai acionar o rele 1 e solicitar biometria, por exemplo.
        ///As configurações ficam armazenadas em um buffer interno da dll e serão enviados somente após a
        ///chamada a função EnviarConfiguracoesFuncoes.
        /// </summary>
        /// <param name="Funcao">0 a 9.</param>
        /// <param name="Catraca">0 – Não libera catraca(Default).
                               ///1 – Libera catraca no sentido de entrada.
                               ///2 – Libera catraca no sentido de saída.
                               ///3 – Libera catraca nos dois sentidos.</param>
        /// <param name="Rele1">0 – Não aciona rele 1(Default).
                             ///1 – Aciona rele 1.</param>
        /// <param name="Rele2">0 – Não aciona rele 2(Default).
                             ///1 – Aciona rele 2.</param>
        /// <param name="Lista">0 – Não consulta lista para registrar a função(Default).
                             ///1 – Consulta a lista, se o cartão estiver com acesso
                             ///liberado registra a função.</param>
        /// <param name="Biometria">0 – Registra a função a partir da leitura do cartão ou teclado. Não faz verificação / identificação biométrica(Default).
                                 ///1 - Faz verificação ou identificação biométrica.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirConfiguracoesFuncoes(byte Funcao, byte Catraca, byte Rele1, byte Rele2, byte Lista, byte Biometria);
        #endregion

        #region HabilitarScoreFuncoes
        /// <summary>
        /// Configura se a função irá exibir o score da digital no display do Inner Bio.
        /// </summary>
        /// <param name="Funcao">0 a 9.</param>
        /// <param name="Score">0 – Não exibe o score da digital(Default).
                             ///1 – Exibe o score da digital.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte HabilitarScoreFuncoes(int Funcao, byte Score);
        #endregion

        #region EnviarConfiguracoesFuncoes
        /// <summary>
        /// Envia o buffer com as configurações de todas as funções para o Inner. Após executar a
        ///função, o buffer com as configurações das funções é limpo automaticamente pela dll.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarConfiguracoesFuncoes(int Inner);
        #endregion

        #endregion

        #region FUNÇÕES PARA MANIPULAR OS HORÁRIOS DE TOQUE DA SIRENE DO INNER
      
        #region ApagarHorariosSirene
        /// <summary>
        /// Limpa o buffer com os horários de sirene e o envia automaticamente para o Inner.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ApagarHorariosSirene(int Inner);
        #endregion

        #region InserirHorarioSirene
        /// <summary>
        /// Insere um horário de toque de sirene e configura em quais dias da semana esses horário
        ///irão tocar. É possível inserir no máximo 100 horários para a sirene.
        /// </summary>
        /// <param name="Hora">0 a 23</param>
        /// <param name="Minuto">0 a 59</param>
        /// <param name="Segunda">0 – Desabilita o toque nesse dia.
                               ///1 – Habilita o toque nesse dia.</param>
        /// <param name="Terca">0 – Desabilita o toque nesse dia.
                             ///1 – Habilita o toque nesse dia.</param>
        /// <param name="Quarta">0 – Desabilita o toque nesse dia.
                              ///1 – Habilita o toque nesse dia.</param>
        /// <param name="Quinta">0 – Desabilita o toque nesse dia.
                              ///1 – Habilita o toque nesse dia.</param>
        /// <param name="Sexta">0 – Desabilita o toque nesse dia.
                             ///1 – Habilita o toque nesse dia.</param>
        /// <param name="Sabado">0 – Desabilita o toque nesse dia.
                              ///1 – Habilita o toque nesse dia.</param>
        /// <param name="DomingoFeriado">0 – Desabilita o toque nesse dia.
                                      ///1 – Habilita o toque nesse dia.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte InserirHorarioSirene(byte Hora, byte Minuto, byte Segunda, byte Terca, byte Quarta, byte Quinta, byte Sexta, byte Sabado, byte DomingoFeriado);
        #endregion

        #region EnviarHorariosSirene
        /// <summary>
        /// Envia o buffer com os horário de sirene cadastrados para o Inner. Após executar a função o
        ///buffer é limpo automaticamente pela dll.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarHorariosSirene(int Inner);
        #endregion

        #endregion

        #region FUNÇÃO PARA RECEBER OS BILHETES OFFLINE DO INNER

        #region ColetarBilhete
        /// <summary>
        /// Coleta um bilhete Off-Line que está armazenado na memória do Inner, os dados do bilhete
        ///são retornados por referência nos parâmetros da função. Ates de chamar esta função pela primeira vez é
        ///preciso chamar obrigatoriamente as funções DefinirPadraoCartao e DefinirQuantidadeDigitosCartao
        ///nessa ordem para que o número do cartão seja calculado de forma correta.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Tipo">Tipo da marcação registrada.
                            ///0 a 9 – Funções registradas pelo cartão.
                            ///10 – Entrada pelo cartão.
                            ///11 – Saída pelo cartão.
                            ///12 – Tentativa de entrada negada pelo cartão.
                            ///13 – Tentativa de saída negada pelo cartão.
                            ///100 a 109 – Funções registradas pelo teclado.
                            ///110 – Entrada pelo teclado.
                            ///111 – Saída pelo teclado.
                            ///112 – Tentativa de entrada negada pelo teclado.
                            ///113 – Tentativa de saída negada pelo teclado.</param>
        /// <param name="Dia">1 a 31</param>
        /// <param name="Mes">1 a 12</param>
        /// <param name="Ano">0 a 99</param>
        /// <param name="Hora">0 a 23</param>
        /// <param name="Minuto">0 a 59</param>
        /// <param name="Cartao">Número do cartão do usuário.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte ColetarBilhete(int Inner, ref byte Tipo, ref byte Dia, ref byte Mes, ref byte Ano, ref byte Hora, ref byte Minuto, StringBuilder Cartao);
        #endregion

        #region ReceberQuantidadeBilhetes
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int ReceberQuantidadeBilhetes(int Inner, int[] QtdeBilhetes);
        #endregion

        #endregion

        #region FUNÇÕES PARA MANIPULAR OS DADOS ONLINE DO INNER

        #region EnviarFormasEntradasOnLine
        /// <summary>
        /// Configura as formas de entrada de dados do Inner no modo OnLine. Cada vez que alguma
        ///informação for recebida no modo OnLine através da função ReceberDadosOnLine, a função
        ///EnviarFormasEntradasOnLine deverá ser chamada novamente para reconfigurar o Inner.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="QtdeDigitosTeclado">0 a 20 dígitos.</param>
        /// <param name="EcoTeclado">0 – para não
                                  ///1 – para sim
                                  ///2 – ecoar *</param>
        /// <param name="FormaEntrada">0 – não aceita entrada de dados
                                    ///1 – aceita teclado
                                    ///2 – aceita leitura no leitor 1
                                    ///3 – aceita leitura no leitor 2
                                    ///4 – teclado e leitor 1
                                    ///5 – teclado e leitor 2
                                    ///6 – leitor 1 e leitor 2
                                    ///7 – teclado, leitor 1 e leitor 2
                                    ///10 – teclado + verificação biométrica
                                    ///11 – leitor1 + verificação biométrica
                                    ///12 – teclado + leitor1 + verificação biométrica
                                    ///13 – leitor1 com verificação biométrica + leitor2 sem verificação biométrica
                                    ///14 – leitor1 com verificação biométrica + leitor2 sem verificação biométrica + teclado sem verificação biométrica
                                    ///100 – Leitor 1 + Identificação Biométrica (sem Verificação)
                                    ///101 – Leitor 1 + Teclado + Identificação Biométrica (sem Verificação)
                                    ///102 – Leitor 1 + Leitor 2 + Identificação Biométrica (sem Verificação)
                                    ///103 – Leitor 1 + Leitor 2 + Teclado + Identificação Biométrica (sem Verificação)
                                    ///104 – Leitor 1 invertido + Identificação Biométrica (sem Verificação)
                                    ///105 – Leitor 1 invertido + Teclado + Identificação Biométrica (sem Verificação)</param>
        /// <param name="TempoTeclado">1 a 50</param>
        /// <param name="PosicaoCursorTeclado">1 a 32</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarFormasEntradasOnLine(int Inner, byte QtdeDigitosTeclado, byte EcoTeclado, byte FormaEntrada, byte TempoTeclado, byte PosicaoCursorTeclado);
        #endregion

        #region ReceberDadosOnLine
        /// <summary>
        /// Coleta um bilhete OnLine, caso o usuário tenha passado ou digitado algum cartão no Inner
        ///retorna as informações do cartão nos parâmetros da função. Para que a data/hora do bilhete OnLine seja
        ///retornada, o Inner deverá ter sido previamente configurado através da função
        ///ReceberDataHoraDadosOnLine.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Origem">Origem dos dados recebidos.
                                ///1 – via teclado
                                ///2 – via leitor 1
                                ///3 – via leitor 2
                                ///4 – sensor da catraca(obsoleto)
                                ///5 – fim do tempo de acionamento
                                ///6 – giro da catraca Topdata (sensor ótico)
                                ///7 – Urna (catraca Millenium)
                                ///8 – Evento no Sensor 1
                                ///9 – Evento no Sensor 2
                                ///10 – Evento no Sensor 3</param>
        /// <param name="Complemento">Informações adicionais sobre os dados recebidos.
                                    ///0 – saída (com cartão)
                                    ///1 – entrada (com cartão)
                                    ///35 – # via teclado (1° tecla)
                                    ///42 – * via teclado (1° tecla)
                                    ///65 – “Função” via teclado
                                    ///66 – “Entrada” via teclado
                                    ///67 – “Saída” via teclado
                                    ///255 – Inseriu todos os dígitos permitidos pelo teclado.
                                    ///Evento do Sensor
                                    ///0/1 – Nível atual do sensor</param>
        /// <param name="Cartao">Número do cartão recebido.</param>
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

        #region FUNÇÃO PARA LER OS STATUS DOS SENSORES DO INNER
        
        #region LerSensoresInner
        /// <summary>
        /// Recebe o status atual dos sensores do Inner. Essa função deverá ser utilizada somente em
        ///casos muito específicos, por exemplo, quando você possui um Inner Plus/NET conectado a um sensor
        ///de presença e deseja saber se existe alguma pessoa naquele local.
        ///Essa função não deve ser utilizada em catracas.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="StatusSensor1">0 – em nível baixo (fechado)
                                     ///1 – em nível alto (aberto)</param>
        /// <param name="StatusSensor2">0 – em nível baixo (fechado)
                                     ///1 – em nível alto (aberto)</param>
        /// <param name="StatusSensor3">0 – em nível baixo (fechado)
                                     ///1 – em nível alto (aberto)</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LerSensoresInner(int Inner, ref byte StatusSensor1, ref byte StatusSensor2, ref byte StatusSensor3);
        #endregion

        #endregion

        #region FUNÇÕES PARA MANIPULAR AS MENSAGENS DA IMPRESSORA DO INNER
        
        #region EnviarMensagemImpressora00
        /// <summary>
        /// Envia a mensagem para a impressora com final terminado em 0x00.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Mensagem">Caracteres/números que serão impressos na impressora. É possível utilizar
                                ///caracteres especiais (avanços de linha, etc), tudo depende do caracterset
                                ///que o modelo de impressora utilizado suporta.
                                ///O tamanho máximo da mensagem é de 254 caracteres.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarMensagemImpressora00(int Inner, string Mensagem);
        #endregion

        #region EnviarMensagemImpressoraFF
        /// <summary>
        /// Envia a mensagem para a impressora com final terminado em 0xFF.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Mensagem">Caracteres/números que serão impressos na impressora. É possível utilizar
                                ///caracteres especiais (avanços de linha, etc), tudo depende do caracterset
                                ///que o modelo de impressora utilizado suporta.
                                ///O tamanho máximo da mensagem é de 254 caracteres.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarMensagemImpressoraFF(int Inner, string Mensagem);
        #endregion

        #endregion

        #region FUNÇÕES PARA CONFIGURAR A MUDANÇA AUTOMÁTICA DE ON-LINE PARA OFF-LINE DO INNER

        #region HabilitarMudancaOnLineOffLine
        /// <summary>
        /// Habilita/Desabilita a mudanção automática do modo OffLine do Inner para OnLine e viceversa.
        ///Configura o tempo após a comunicação ser interrompida que está mudança irá ocorrer.
        /// </summary>
        /// <param name="Habilita">0 – Desabilita a mudança(Default).
                                ///1 – Habilita a mudança.
                                ///2 – Habilita a mudança automática para o modo OnLine TCP com Ping,
                                ///onde o Inner precisa receber o comando PingOnLine para manter-se OnLine.</param>
        /// <param name="Tempo">Tempo em segundos para ocorrer a mudança.
                                ///1 a 50.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte HabilitarMudancaOnLineOffLine(byte Habilita, byte Tempo);
        #endregion

        #region DefinirEntradasMudancaOffLine
        /// <summary>
        /// Configura as formas de entradas de dados para quando o Inner mudar para o modo Off-Line.
        ///Para aplicações com biometria verifique a próxima função
        ///“DefinirEntradasMudançaOffLineComBiometria”.
        /// </summary>
        /// <param name="Teclado">0 – Não aceita dados pelo teclado.
                               ///1 – Aceita dados pelo teclado.</param>
        /// <param name="Leitor1">0 – desativado
                               ///1 – somente para entrada
                               ///2 – somente para saída
                               ///3 – entrada e saída
                               ///4 – saída e entrada</param>
        /// <param name="Leitor2">0 – desativado
                               ///1 – somente para entrada
                               ///2 – somente para saída
                               ///3 – entrada e saída
                               ///4 – saída e entrada</param>
        /// <param name="Catraca">0 – reservado para uso futuro.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirEntradasMudancaOffLine(byte Teclado, byte Leitor1, byte Leitor2, byte Catraca);
        #endregion

        #region DefinirEntradasMudancaOffLineComBiometria
        /// <summary>
        /// Configura as formas de entradas de dados para quando o Inner mudar para o modo Off-Line.
        ///Esse comando difere do anterior por permitir a configuração de biometria. Através dessa função o Inner
        ///pode ser configurado para trabalhar com verificação ou identificação biométrica, quando ocorrer uma
        ///mudança automática de On-Line para Off-Line.
        /// </summary>
        /// <param name="Teclado">0 – Não aceita dados pelo teclado.
                               ///1 – Aceita dados pelo teclado.</param>
        /// <param name="Leitor1"> 0 – desativado
                                ///3 – entrada e saída
                                ///4 – saída e entrada (nesse caso força Leitor2 igual a zero)</param>
        /// <param name="Leitor2">0 – desativado
                               ///3 – entrada e saída</param>
        /// <param name="Verificacao">0 – desativada
                                   ///1 – ativada</param>
        /// <param name="Identificacao">0 – desativada
                                     ///1 – ativada</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirEntradasMudancaOffLineComBiometria(byte Teclado, byte Leitor1, byte Leitor2, byte Verificacao ,byte Identificacao);
        #endregion

        #region DefinirMensagemPadraoMudancaOffLine
        /// <summary>
        /// Configura a mensagem padrão a ser exibida pelo Inner quando ele mudar para Off-line.
        /// </summary>
        /// <param name="ExibirData">0 – Não exibe a data/hora na linha superior do display.
                                  ///1 – Exibe a data/hora na linha superior do display.</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja exibindo a data/hora o
                                ///tamanho da mensagem passa a ser 16 ao invés de 32. Caso seja passado
                                ///uma string vazia o Inner não exibirá a mensagem no display</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirMensagemPadraoMudancaOffLine(byte ExibirData, string Mensagem);
        #endregion

        #region DefinirMensagemPadraoMudancaOnLine
        /// <summary>
        /// Configura a mensagem padrão exibido pelo Inner quando entrar para on line após uma
        ///queda para off line.
        /// </summary>
        /// <param name="ExibirData">
                                    /// 0 – Não exibe a data/hora na linha superior do display.
                                    ///1 – Exibe a data/hora na linha superior do display.</param>
        /// <param name="Mensagem">String com a mensagem a ser exibida. Caso esteja exibindo a data/hora o
                                ///tamanho da mensagem passa a ser 16 ao invés de 32. Caso seja passado
                                ///uma string vazia o Inner não exibirá a mensagem no display</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirMensagemPadraoMudancaOnLine(byte ExibirData, string Mensagem);
        #endregion

        #region DefinirEntradasMudancaOnLine
        /// <summary>
        /// Configura as formas de entrada dos dados quando o Inner voltar para o modo On Line após
        ///uma queda para OffLine.
        /// </summary>
        /// <param name="Entrada">0 – Não aceita entrada de dados.
                               ///1 – Aceita teclado.
                               ///2 – Aceita leitor 1.
                               ///3 – Aceita leitor 2.
                               ///4 – Teclado e leitor 1.
                               ///5 – Teclado e leitor 2.
                               ///6 – Leitor 1 e leitor 2.
                               ///7 – Teclado, leitor 1 e 2.
                               ///8 – Sensor da catraca.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirEntradasMudancaOnLine(byte Entrada);
        #endregion

        #region DefinirConfiguracaoTecladoOnLine
        /// <summary>
        /// Configura o teclado para quando o Inner voltar para OnLine após uma queda para OffLine.
        /// </summary>
        /// <param name="Digitos">0 a 20 dígitos.</param>
        /// <param name="EcoDisplay">0 – para não ecoar
                                 ///1 – para sim
                                 ///2 – ecoar '*'</param>
        /// <param name="Tempo">1 a 50.</param>
        /// <param name="PosicaoCursor">1 a 32.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte DefinirConfiguracaoTecladoOnLine(byte Digitos, byte EcoDisplay, byte Tempo, byte PosicaoCursor);
        #endregion

        #region EnviarConfiguracoesMudancaAutomaticaOnLineOffLine
        /// <summary>
        /// Envia o buffer com as configurações de mudança automática do modo OnLine para OffLine .
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarConfiguracoesMudancaAutomaticaOnLineOffLine(int Inner);
        #endregion

        #endregion

        #region FUNÇÕES ESPECÍFICAS DO INNER BIO

        #region SolicitarModeloBio
        /// <summary>
        /// Solicita o modelo do Inner bio. Para receber o resultado dessa operação você deverá chamar
        ///a função ReceberModeloBio enquanto o retorno for processando a operação.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarModeloBio(int Inner);
        #endregion

        #region ReceberModeloBio
        /// <summary>
        /// Retorna o resultado do comando SolicitarModeloBio, o modelo do Inner Bio é retornado por
        ///referência no parâmetro da função.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 – O comando foi utilizado com o Inner em OffLine.
                              ///1 – O comando foi utilizado com o Inner em OnLine</param>
        /// <param name="Modelo">2 – Bio Light 100 usuários.
                              ///4 – Bio 1000/4000 usuários.
                              ///255 – Versão desconhecida.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberModeloBio(int Inner, byte OnLine, ref int Modelo);
        #endregion

        #region SolicitarVersaoBio
        /// <summary>
        /// Solicita a versão do firmware da placa do Inner Bio, a placa que armazena as digitais.
        /// </summary>
        /// <param name="Inner">
        /// 1 a 32 – Para comunicação serial.
        /// 1 a 99 – Para comunicação TCP/IP com porta variável.
        /// 1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarVersaoBio(int Inner);
        #endregion

        #region ReceberVersaoBio
        /// <summary>
        /// Retorna o resultado do comando SolicitarVersaoBio, a versão do Inner Bio é retornado por
        ///referência nos parâmetros da função.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
        ///1 a 99 – Para comunicação TCP/IP com porta variável.
        ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 – O comando foi utilizado com o Inner em OffLine.
                              ///1 – O comando foi utilizado com o Inner em OnLine</param>
        /// <param name="VersaoAlta">Parte alta da versão.</param>
        /// <param name="VersaoBaixa">Parte baixa da versão.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberVersaoBio(int Inner, byte OnLine, ref int VersaoAlta, ref int VersaoBaixa);
        #endregion

        #region SolicitarQuantidadeUsuariosBio
        /// <summary>
        /// Solicita a quantidade de usuários cadastrados no Inner Bio.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarQuantidadeUsuariosBio(int Inner);
        #endregion

        #region ReceberQuantidadeUsuariosBio
        /// <summary>
        /// Retorna o resultado do comando SolicitarQuantidadeUsuariosBio, a quantidade de
        ///usuários cadastrados no Inner Bio é retornado por referência nos parâmetros da função.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 – O comando foi utilizado com o Inner em OffLine.
                              ///1 – O comando foi utilizado com o Inner em OnLine</param>
        /// <param name="Quantidade">Total de usuários cadastrados no Inner Bio.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberQuantidadeUsuariosBio(int Inner, byte OnLine, ref int Quantidade);
        #endregion

        #region SolicitarUsuarioCadastradoBio
        /// <summary>
        /// Solicita do Inner Bio, o template com as duas digitais do Usuario desejado.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
        ///1 a 99 – Para comunicação TCP/IP com porta variável.
        ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Usuario">Número do cartão do usuário cadastrado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarUsuarioCadastradoBio(int Inner, string Usuario);
        #endregion

        #region ReceberUsuarioCadastradoBio
        /// <summary>
        /// Retorna o resultado do comando SolicitarUsuarioCadastradoBio e o template com as duas
        ///digitais do usuário cadastrado no Inner Bio. O template é retornado por referência nos parâmetros da
        ///função.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 – O comando foi utilizado com o Inner em OffLine.
                              ///1 – O comando foi utilizado com o Inner em OnLine</param>
        /// <param name="Template">Cadastro do usuário com as duas digitais, o dado está em binário e não
                                ///deve ser alterado nunca. O tamanho do template é de 844 bytes.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberUsuarioCadastradoBio(int Inner, byte OnLine,  byte[] Template);
        #endregion

        #region SolicitarExclusaoUsuario
        /// <summary>
        /// Solicita para o Inner bio excluir o cadastro do usuário desejado. O Retorno da exclusão é
        ///verificado através da função UsuarioFoiExcluido
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Usuario">Número do usuário a ser excluído.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarExclusaoUsuario(int Inner, string Usuario);
        #endregion

        #region UsuarioFoiExcluido
        /// <summary>
        /// Retorna o resultado do comando SolicitarExclusaoUsuario, se o retorno da função for igual
        ///a 0 é porque o usuário foi excluído com sucesso.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 – O comando foi utilizado com o Inner em OffLine.
                              ///1 – O comando foi utilizado com o Inner em OnLine.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte UsuarioFoiExcluido(int Inner, byte OnLine);
        #endregion

        #region InserirUsuarioLeitorBio
        /// <summary>
        /// Solicita para o Inner Bio inserir um usuário diretamente pelo leitor biométrico. O leitor irá
        ///acender a luz vermelho e após o usuário inserir a digital, automaticamente o usuário será cadastrado no
        ///Inner bio com o número do cartão passado no parâmetro Usuário.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Tipo">0 – para solicitar o primeiro template
                            ///1 – para solicitar o segundo template (mesmo dedo) e salvar.
                            ///2 – para solicitar o segundo template (outro dedo) e salvar.</param>
        /// <param name="Usuario">Número do cartão que o usuário terá.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte InserirUsuarioLeitorBio(int Inner, byte Tipo, string Usuario);
        #endregion

        #region ResultadoInsercaoUsuarioLeitorBio
        /// <summary>
        /// Retorna o resultado do comando InserirUsuarioLeitorBio. Se o retorno for igual a 0 é
        ///porque o usuário foi cadastrado com sucesso.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 – O comando foi utilizado com o Inner em OffLine.
                              ///1 – O comando foi utilizado com o Inner em OnLine.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoInsercaoUsuarioLeitorBio(int Inner, byte OnLine);
        #endregion

        #region FazerVerificacaoBiometricaBio
        /// <summary>
        /// Ao chamar esta função o Inner irá acender o leitor biométrico e irá solicitar para o usuário
        ///inserir o dedo, após isso o Inner irá compara a(s) digital(ais), associadas ao número do cartão passado
        ///nos parâmetros, que está armazenada na sua memória com a digital inserida pelo usuário. O retorno
        ///desse processo é retornado na função ResultadoVerificacaoBiometrica.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Usuario">Número do usuário cadastrado na memória do Inner.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte FazerVerificacaoBiometricaBio(int Inner, string Usuario);
        #endregion

        #region ResultadoVerificacaoBiometrica
        /// <summary>
        /// Retorna o resultado do comando FazerVerificacaoBiometricaBio. Se o retorno for igual a 0
        ///é porque o usuário foi comparado com sucesso.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 – O comando foi utilizado com o Inner em OffLine.
                              ///1 – O comando foi utilizado com o Inner em OnLine.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoVerificacaoBiometrica(int Inner, byte OnLine);
        #endregion

        #region FazerIdentificacaoBiometricaBio
        /// <summary>
        /// Ao chamar esta função o Inner irá acender o leitor biométrico e irá solicitar para o usuário
        ///inserir o dedo, após isso o Inner irá comparar a digital com as digitais cadastradas no banco de dados do
        ///equipamento. O resultado dessa operação é retornada através da função
        ///ResultadoIdentificacaoBiometrica.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte FazerIdentificacaoBiometricaBio(int Inner);
        #endregion

        #region ResultadoIdentificacaoBiometrica
        /// <summary>
        ///Retorna o resultado do comando InserirUsuarioLeitorBio. Se o retorno for igual a 0 é
        ///porque o usuário foi Identificado com sucesso, o número do cartão do usuário será retornado por
        ///referência no parâmetro da função.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 – O comando foi utilizado com o Inner em OffLine.
                              ///1 – O comando foi utilizado com o Inner em OnLine.</param>
        /// <param name="Usuario">Número do usuário cadastrado no Inner bio que possui a
                               ///mesma digital do dedo inserido no leitor biométrico.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoIdentificacaoBiometrica(int Inner, byte OnLine,  byte[] Usuario);
        #endregion

        #region SolicitarTemplateLeitor
        /// <summary>
        /// Solicita diretamente do Inner bio um template com apenas uma digital, ao executar essa
        ///função o leitor biométrico do Inner bio irá acender e a digital que for inserida será enviada diretamente
        ///para a aplicação, a digital não ficará armazenada no banco de dado do equipamento.
        ///Para receber a digital é necessário chamar a função ReceberTemplateLeitor.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarTemplateLeitor(int Inner);
        #endregion

        #region ReceberTemplateLeitor
        /// <summary>
        /// Retorna o resultado do comando SolicitarTemplateLeitor. Se o retorno for igual a 0 é
        ///porque o template foi recebido com sucesso. O template será retornado por referência no parâmetro da função.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 – O comando foi utilizado com o Inner em OffLine.
                              ///1 – O comando foi utilizado com o Inner em OnLine.</param>
        /// <param name="Template">Digital recebida pelo leitor biométrico. É um array de bytes e seu conteúdo
                                ///não deve ser alterado nunca, seu tamanho é de 404 bytes.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberTemplateLeitor(int Inner, byte OnLine,  byte[] Template);
        #endregion

        #region ConfigurarBio
        /// <summary>
        /// Habilita/Desabilita a identificação biométrica e/ou a verificação biométrica do Inner bio. O
        ///resultado da configuração deve ser obtivo através do comando ResultadoConfiguracaoBio.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="HabilitaIdentificacao">0 – Desabilita.
                                             ///1 – Habilita.</param>
        /// <param name="HabilitaVerificacao">0 – Desabilita.
                                           ///1 – Habilita.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarBio(int Inner, byte HabilitaIdentificacao, byte HabilitaVerificacao);
        #endregion

        #region ResultadoConfiguracaoBio
        /// <summary>
        /// Retorna o resultado da configuração do Inner Bio, função ConfigurarBio. Se o retorno for
        ///igual a 0 é poque o Inner bio foi configurado com sucesso.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 – O comando foi utilizado com o Inner em OffLine.
                              ///1 – O comando foi utilizado com o Inner em OnLine.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoConfiguracaoBio(int Inner, byte OnLine);
        #endregion

        #region EnviarUsuarioBio
        /// <summary>
        /// Envia um template com duas digitais para o Inner Bio cadastrar no seu banco de dados. O
        ///resultado do cadastro deve ser verificado no retorno da função UsuarioFoiEnviado.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Template">O cadastro do usuário já contendo as duas digitais e o número do usuário.
                                ///É um array de bytes com o tamanho de 844 bytes.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarUsuarioBio(int Inner,  byte[] Template);
        #endregion

        #region UsuarioFoiEnviado
        /// <summary>
        /// Retorna o resultado do cadastro do Template no Inner Bio, através da função
        ///EnviarUsuarioBio. Se o retorno for igual a 0 é porque o template foi cadastrado com sucesso.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 – O comando foi utilizado com o Inner em OffLine.
                              ///1 – O comando foi utilizado com o Inner em OnLine.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte UsuarioFoiEnviado(int Inner, byte OnLine);
        #endregion

        #region CompararDigitalLeitor
        /// <summary>
        /// Ao executar essa função o Inner bio irá acender o leitor biométrico solicitando a digital do
        ///usuário, na sequência irá comparar a digital inserida pelo usuário com a digital enviada pela função no
        ///parâmetro Template. O resultado da comparação é retornado pela função
        ///ResultadoComparacaoDigitalLeitor.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Template">Digital a ser comparada. Array de bytes de 404 bytes.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte CompararDigitalLeitor(int Inner,  byte[] Template);
        #endregion

        #region ResultadoComparacaoDigitalLeitor
        /// <summary>
        /// Retorna o resultado da comparação da digital do usuário com o template enviado para o
        ///Inner Bio, através da função CompararDigitalLeitor. Se o retorno for igual a 0 é poque a digital inserida
        ///é a mesma da enviada.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="OnLine">0 – O comando foi utilizado com o Inner em OffLine.
                              ///1 – O comando foi utilizado com o Inner em OnLine.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoComparacaoDigitalLeitor(int Inner, byte OnLine);
        #endregion

        #region IncluirUsuarioSemDigitalBio
        /// <summary>
        /// Insere o número do cartão na lista de usuários sem digital do Inner bio. Este número ficara
        ///armazenado em um buffer interno dentro da dll e somente será enviado para o Inner após a chamada a
        ///função EnviarListaUsuariosSemDigitalBio. O número máximo de dígitos para o cartão é 10, caso os
        ///cartões tenham mais de 10 dígitos, utilizar os 10 dígitos menos significativos do cartão.
        /// </summary>
        /// <param name="Cartao">1 a 9999999999 – Número do cartão do usuário.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte IncluirUsuarioSemDigitalBio(string Cartao);
        #endregion

        #region EnviarListaUsuariosSemDigitalBio
        /// <summary>
        /// Envia o buffer com a lista de usuários sem digital para o Inner. Após a execução do
        ///comando, o buffer é limpo pela dll.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarListaUsuariosSemDigitalBio(int Inner);
        #endregion

        #region SetarBioLight
        /// <summary>
        /// Define que o Inner utilizado no momento é um Inner bio light ao invés de um Inner bio
        ///1000/4000. Essa função deverá ser chamada sempre que necessário antes das funções
        ///SolicitarUsuarioCadastradoBio, SolicitarExclusaoUsuario, InserirUsuarioLeitorBio e
        ///FazerVerificacaoBiometricaBio.
        /// </summary>
        /// <param name="Light">1 – É um Inner bio light
                             ///0 – É um Inner bio 1000/4000(valor default)</param>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern void SetarBioLight(int Light);
        #endregion

        #endregion

        #region CONFIGURAÇÕES DE AJUSTES BIOMÉTRICOS

        #region ConfigurarAjustesSensibilidadeBio
        /// <summary>
        /// Configura a quantidade de ganho, brilho e contraste que o Inner irá utilizar para ler a digital do usuário.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
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
        /// Configura o nível da qualidade da digital que o Inner Bio irá utilizar para registrar a digital na
        ///base de dados e para utilizar na verificação biométrica do cartão.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
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
        /// Configura o nível de segurança utilizados na Identificação e na Verificação biométrica.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
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
        /// Habilita a captura adaptativa da digital, é possível especificar quantas tentativas o Inner Bio
        ///deverá realizar na captura da digital e por quanto tempo ficará esperando o usuário inserir a digital.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Capturar">0 – Desabilita a captura adaptativa(Default).
                                ///1 – Habilita a captura adaptativa.</param>
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
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Habilitar">0 – Desabilita o filtro da digital latente(Default).
                                 ///1 – Habilita o filtro da digital latente.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarFiltroBio(int Inner, byte Habilitar);
        #endregion

        #region EnviarAjustesBio
        /// <summary>
        /// Envia o buffer com as configuracões feitas pelas funções acima para o Inner.
        /// Após o envio o buffer é limpo pela DLL.
        ///Para confirmar realmente se o Inner recebeu os dados com sucesso,
        /// é necessário verificar com a função ResultadoEnvioAjustesBio.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte EnviarAjustesBio(int Inner);
        #endregion

        #endregion

        #region FUNÇÕES PARA RECEBER TODOS OS USUÁRIOS CADASTRADOS NO INNER BIO

        #region InicializarColetaListaUsuariosBio
        /// <summary>
        /// Prepara a dll para iniciar a coleta dos usuários do Inner bio, essa função deve ser chamada
        ///obrigatoriamente no início do processo.
        /// </summary>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern void InicializarColetaListaUsuariosBio();
        #endregion

        #region SolicitarListaUsuariosBio
        /// <summary>
        /// Solicita o pacote(a parte) atual da lista de usuários do Inner bio.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarListaUsuariosBio(int Inner);
        #endregion

        #region ReceberPacoteListaUsuariosBio
        /// <summary>
        /// Receber o pacote solicitado pela função SolicitarListaUsuariosBio.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberPacoteListaUsuariosBio(int Inner);
        #endregion

        #region ReceberUsuarioLista
        /// <summary>
        /// Recebe um usuário por vez do pacote recebido anteriormente. O número do usuário é
        ///retornado pelo parâmetro da função.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Usuario">Número do usuário cadastrado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberUsuarioLista(int Inner,  StringBuilder Usuario);
        #endregion

        #region TemProximoUsuario
        /// <summary>
        /// Retorna 1 se ainda existe usuários no pacote recebido da lista.
        /// </summary>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int TemProximoUsuario();
        #endregion

        #region TemProximoPacote
        /// <summary>
        /// Retorna 1 se ainda existe mais pacotes da lista de usuários, a ser recebido do Inner Bio.
        /// </summary>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int TemProximoPacote();
        #endregion

        #endregion

        #region FUNÇÕES DO INNER COM MODEM

        #region EnviarStringInicializacaoModem
        /// <summary>
        /// Envia uma string para o modem executar. A string deve conter um comando AT válido do modem utilizado.
        /// </summary>
        /// <param name="Str">String válida de um comando AT.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int EnviarStringInicializacaoModem(string Str);
        #endregion

        #region LerByteModem
        /// <summary>
        /// Verifica o retorno após enviar um comando AT para ser executado.
        /// </summary>
        /// <returns>-1 – Processando.
                   ///1, 5, 10 ou 12 – Modem conectado.
                   ///3 – Sem resposta do modem.
                   ///6 – Sem tom de discagem.
                   ///7 – Ocupado.</returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int LerByteModem();
        #endregion

        #region ConectarModem
        /// <summary>
        /// Configura, disca e efetua a conexão com o modem e o Inner desejado.
        /// </summary>
        /// <param name="Porta">Número da porta serial do modem.</param>
        /// <param name="Str">String de inicialização do modem, com os comandos AT.</param>
        /// <param name="Tom">Se 0 discagem por pulso.
                           ///Se 1 discagem por tom.</param>
        /// <param name="Telefone">String com o número do telefone/ramal a ser discado.</param>
        /// <param name="Inner">Número do Inner que está conectado no modem.</param>
        /// <returns>-3 – Telefone inválido.
                  ///-2 – String de inicialização inválida.
                  ///-1 – Processando.
                  ///1, 5, 10 ou 12 – Modem conectado.
                  ///3 – Sem resposta do modem.
                  ///6 – Sem tom de discagem.
                  ///7 – Ocupado.</returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int ConectarModem(int Porta, string Str, int Tom, string Telefone, int Inner);
        #endregion

        #endregion

        #region INNER PADRÃO - PRIMEIRA GERAÇÃO DE INNERS

        #region SetarInnerOld
        /// <summary>
        /// Configura a DLL para comunicar com o Inner padrão, ao chamar essa função ele irá afetar o
        ///grupo de funções referentes a “Lista de acesso”, “Horários de acesso”, “Horários de sirene” e a “Leitura
        ///dos sensores”, para que estas funções funcionem no Inner padrão a SetarInnerOld deve ser chamada.
        ///Após comunicar com o Inner padrão, a função deve ser chamada desabilitando a comunicação com este tipo de Inner.
        ///As demais funções funcionam normalmente com este tipo de Inner.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Old">0 – Não está utilizando o Inner Padrão.
                           ///1 – Está utilizando o Inner Padrão.</param>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern void SetarInnerOld(int Inner,int Old);
        #endregion

        #endregion

        #region FUNÇÕES DO INNER VERID

        #region IncluirUsuarioVerid
        /// <summary>
        /// Envia o template do usuário para o Inner Verid. As digitais ficarão armazenadas na base de
        ///dados do Inner Verid. O resultado da inclusão deve ser conferido através da função
        ///ResultadoInclusaoUsuarioVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Template">Array de bytes contendo toda as informações das digitais do usuário.
                                ///Estes dados devem ter sido coletados de um outro Inner Verid previamente e 
                                ///não deverá ser alterado.
                                ///Este tamplate deverá ser o completo, com duas digitais.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte IncluirUsuarioVerid(int Inner,  byte[] Template);
        #endregion

        #region ResultadoInclusaoUsuarioVerid
        /// <summary>
        /// Retorna se a função IncluirUsuarioVerid foi processada corretamente pelo Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoInclusaoUsuarioVerid(int Inner);
        #endregion

        #region CompararTemplateVerid
        /// <summary>
        /// Envia o template de apenas uma digital para o Inner iniciar a comparação, o Inner Verid irá
        ///comparar a digial enviada com a digital inserida pelo usuário.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Template">Array de bytes contendo toda as informações das digitais do usuário.
                                ///Estes dados devem ter sido coletados de um outro Inner Verid previamente
                                ///e não deverá ser alterado.
                                ///Este template não é o completo, ele deverá conter somente uma digital.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte CompararTemplateVerid(int Inner,  byte[] Template);
        #endregion

        #region ResultadoComparacaoTemplateVerid
        /// <summary>
        /// Retorna o resultado da comparação da digital enviada pela função CompararTemplateVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoComparacaoTemplateVerid(int Inner);
        #endregion

        #region CriarUsuarioLeitorVerid
        /// <summary>
        /// Cria o template com apenas 1 digital, ao executar esta função o Inner Verid irá solicitar a
        ///digital do usuário e irá disponibilizar para o software. A digital coletada com esta função deverá ser
        ///utilizada com a função CompararTemplateVerid. O resultado e a digital deverão ser capturados com a
        ///função ResultadoInclusaoUsuarioLeitorVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte CriarUsuarioLeitorVerid(int Inner);
        #endregion

        #region ResultadoInclusaoUsuarioLeitorVerid
        /// <summary>
        /// Retorna o template com apenas uma digital, solicitado através da função
        ///CriarUsuarioLeitorVerid e retorna se a solicitação foi processada com sucesso pelo Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Template">Array de bytes contendo toda as informações das digitais do usuário.
                                ///Estes dados devem ter sido coletados de um outro Inner Verid 
                                /// previamente e não deverá ser alterado.
                                ///Este template não é o completo, ele deverá conter somente uma digital.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoInclusaoUsuarioLeitorVerid(int Inner,  byte[] Template);
        #endregion

        #region SolicitarTotalUsuariosVerid
        /// <summary>
        /// Solicita para o Inner Verid o total de usuários cadastrados no equipamento.Após executar
        ///essa função o software deverá esperar pelo menos 75 milisegundos.
        ///Os dados são retornados na função ReceberTotalUsuariosVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Modo">0 – Receber somente PIN, cartão.(Somente para Verid Plus).
                            ///1 – Receber PIN mais as digitais.(Todos os Verid’s).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte SolicitarTotalUsuariosVerid(int Inner, byte Modo);
        #endregion

        #region ReceberTotalUsuariosVerid
        /// <summary>
        /// Retorna a quantidade de usuários cadastrados no Inner Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Total">Total de usuários cadastrados.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberTotalUsuariosVerid(int Inner, ref int Total);
        #endregion

        #region LocalizarPrimeiroUsuarioVerid
        /// <summary>
        /// Solicita o primeiro/próximo usuário cadastrado no Verid, a função
        ///LocalizarPrimeiroUsuarioVerid deverá ser chamada somente no início da busca 
        /// dos usuários no banco de dados do Inner Verid.
        /// Após executar essa função o software deverá esperar pelo menos 75 milisegundos.
        ///Os dados são retornados na função ReceberUsuarioVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Modo">0 – Receber somente PIN, cartão.(Somente para Verid Plus).
                            ///1 – Receber PIN mais as digitais.(Todos os Verid’s).</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LocalizarPrimeiroUsuarioVerid(int Inner, byte Modo);
        #endregion

        #region LocalizarProximoUsuarioVerid
        /// <summary>
        /// Solicita o primeiro/próximo usuário cadastrado no Verid, a função
        ///LocalizarPrimeiroUsuarioVerid deverá ser chamada somente no início da busca 
        /// dos usuários no banco de dados do Inner Verid.
        /// Após executar essa função o software deverá esperar pelo menos 75 milisegundos.
        ///Os dados são retornados na função ReceberUsuarioVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Modo">0 – Receber somente PIN, cartão.(Somente para Verid Plus).
                            ///1 – Receber PIN mais as digitais.(Todos os Verid’s).</param> 
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte LocalizarProximoUsuarioVerid(int Inner, byte Modo);
        #endregion

        #region LocalizarUsuarioVerid
        /// <summary>
        /// Localiza o usuário com o número do cartão desejado no banco de dados do Inner Verid. Após
        ///executar essa função o software deverá esperar pelo menos 75 milisegundos.
        ///Os dados são retornados na função ReceberUsuarioVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Modo">0 – Receber somente PIN, cartão.(Somente para Verid Plus).
                            ///1 – Receber PIN mais as digitais.(Todos os Verid’s).</param>
        /// <param name="Digitos">Quantidade de dígitos do cartão.
                               ///1 a 16.</param>
        /// <param name="Cartao">Número do cartão desejado.</param>
        /// <returns>128 – Cartão inválido.
                  ///129 – Modo de operação inválido.
                  ///130 – Quantidade de dígitos inválida.</returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte LocalizarUsuarioVerid(int Inner, byte Modo, byte Digitos, string Cartao);
        #endregion

        #region ReceberUsuarioVerid
        /// <summary>
        /// Retorna o usuário cadastrado no banco de dados do Inner Verid. A função irá retornar a
        ///quantidade de dígitos do cartão, o número do cartão e o template completo do usuário.
        ///O Verid pode retornar um usuário repetido, quando isso acontecer é necessário executar a função
        ///LocalizarPrimeiroUsuarioVerid e iniciar a coleta de usuários novamente a partir do início. Isso
        ///acontece pois o Inner Verid pode se perder na lista de usuários.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Modo">0 – Receber somente PIN, cartão.(Somente para Verid Plus).
                            ///1 – Receber PIN mais as digitais.(Todos os Verid’s).</param>
        /// <param name="Digitos">Quantidade de dígitos do cartão.
                               ///1 a 16.</param>
        /// <param name="Template">Array de bytes contendo toda as informações das digitais do usuário.
                                ///Estes dados devem ter sido coletados de um outro 
                                ///Inner Verid previamente  não deverá ser alterado.
                                ///Este tamplate deverá ser o completo, com duas digitais.</param>
        /// <param name="Cartao">Número do cartão desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte ReceberUsuarioVerid(int Inner, byte Modo, ref byte Digitos,  byte[] Template, string Cartao);
        #endregion

        #region ApagarUsuarioVerid
        /// <summary>
        /// Descrição: Solicita a exclusão do usuário na base de dados do Inner Verid. O retorno da exclusão deverá
        ///ser verificado através da função ResultadoExclusaoUsuarioVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Cartao">Número do cartão desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte ApagarUsuarioVerid(int Inner, string Cartao);
        #endregion

        #region ResultadoExclusaoUsuarioVerid
        /// <summary>
        /// Retorna o resultado da exclusão de um usuário do Inner Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoExclusaoUsuarioVerid(int Inner);
        #endregion

        #region ApagarTodosUsuariosVerid
        /// <summary>
        /// Solicita a exclusão de todos os usuários cadastrados no Inner Verid. É necessário possuir a
        ///senha de administrador cadastrada diretamente na placa Verid. O Retorno da exclusão deverá ser
        ///verificado através da função ResultadoExclusaoTodosUsuariosVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="SenhaAdm">Senha do administrador da placa Verid.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte ApagarTodosUsuariosVerid(int Inner, string SenhaAdm);
        #endregion

        #region ResultadoExclusaoTodosUsuariosVerid
        /// <summary>
        /// Retorna se todos os usuário foram excluídos do banco de dados do Inner Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoExclusaoTodosUsuariosVerid(int Inner);
        #endregion

        #region CompararPINVerid
        /// <summary>
        /// Compara a digital cadastrada para o número do cartão desejado com a digital inserida pelo
        ///leitor biométrico do Inner Verid. O resultado da operação é retornado através da função
        ///ResultadoComparacaoPINVerid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Cartao">Número do cartão desejado.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern byte CompararPINVerid(int Inner, string Cartao);
        #endregion

        #region ResultadoComparacaoPINVerid
        /// <summary>
        /// Retorna o resultado da comparação do usuário cadastrado com a digital inserida pelo leitor
        ///biométrico do Inner Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ResultadoComparacaoPINVerid(int Inner);
        #endregion

        #region ConfigurarRedeVerid
        /// <summary>
        /// Configura como o Inner Verid irá se comportar em uma rede de Verid’s. Se o Inner deverá
        ///enviar/receber as digitais cadadastradas para os demais Verid’s
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
        /// <param name="Envia">0 – Desabilita o envio das digitais.
                             ///1 – Habilita o envio das digitais.</param>
        /// <param name="Recebe">0 – Desabilita a recepção das digitais.
                              ///1 – Habilita a recepção das digitais.</param>
        /// <param name="BroadCast">0 – Desabilita o broadcast das digitais.
                                 ///1 – Habilita o broadcast das digitais.</param>
        /// <returns></returns>
        [DllImport("EasyInner.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern byte ConfigurarRedeVerid(int Inner, byte Envia, byte Recebe, byte BroadCast);
        #endregion

        #region ResultadoConfiguracaoRedeVerid
        /// <summary>
        /// Retorna se o Inner Verid aceitou as configurações da rede Verid.
        /// </summary>
        /// <param name="Inner">1 a 32 – Para comunicação serial.
                             ///1 a 99 – Para comunicação TCP/IP com porta variável.
                             ///1 a ... – Para comunicação TCP/IP porta fixa.</param>
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
