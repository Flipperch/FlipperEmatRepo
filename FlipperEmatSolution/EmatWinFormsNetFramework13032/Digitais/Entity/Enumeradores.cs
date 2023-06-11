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

using System;
using System.Text;
using System.Collections.Generic;

namespace EmatWinFormsNetFramework13032.Digitais.Entity
{
    public class Enumeradores
    {
        #region Habilita
        public enum Habilita
        { 
            DESABILITA = 0,
            HABILITA = 1
        }
        #endregion

        #region Ecoar
        public enum Ecoar
        {
            //Constantes de Eco de digitos no teclado
            ECOA_DIGITADO = 0,
            ECOA_ASTERISCO = 1
        }
        #endregion

        #region TipoConexao
        public enum TipoConexao
        {
            SERIAL_RS232_485 = 0,
            TCP_IP_COM_PORTA_VARIAVEL = 1,
            TCP_IP_COM_PORTA_FIXA = 2,
            MODEM = 3,
            TOP_PENDRIVE = 4
        }
        #endregion

        #region TipoLeitor
        public enum TipoLeitor
        {
            //Constantes de Tipo de Leitor..
            CODIGO_DE_BARRAS = 0,
            MAGNETICO = 1,
            PROXIMIDADE_ABATRACK2 = 2,
            WIEGAND = 3,
            PROXIMIDADE_SMART_CARD = 4
        }
        #endregion

        #region Operacao
        public enum Operacao
        {
            //Constantes de Operação
            DESATIVADO = 0,
            SOMENTE_ENTRADA = 1,
            SOMENTE_SAIDA = 2,
            ENTRADA_E_SAIDA = 3,
            ENTRADA_E_SAIDA_INVERTIDAS = 4
        }
        #endregion

        #region Opcao
        public enum Opcao
        {
            //Constantes de Opção
            NAO = 0,
            SIM = 1
        }
        #endregion

        #region ConfiguracaoLeitor
        public enum ConfiguracaoLeitor
        {
            //Constantes de Configuração de Leitor
            DESATIVADO = 0,
            ENTRADA = 1,
            SAIDA = 2,
            ENTRADA_SAIDA =  3,
            ENTRADA_SAIDA_INVERTIDA = 4
        }
        #endregion

        #region FuncaoAcionamento
        public enum FuncaoAcionamento
        { 
            NAO_UTILIZADO = 0,
            ACIONA_REGISTRO_ENTRADA_OU_SAIDA = 1,
            ACIONA_REGISTRO_ENTRADA = 2,
            ACIONA_REGISTRO_SAIDA = 3,
            CONECTADO_SIRENE = 4,
            REVISTA_USUARIOS = 5,
            CATRACA_SAIDA_LIBERADA = 6,
            CATRACA_ENTRADA_LIBERADA = 7,
            CATRACA_LIBERADA_DOIS_SENTIDOS = 8,
            CATRACA_LIBERADA_DOIS_SENTIDOS_MARCACAO_REGISTRO = 9
        }
        #endregion

        #region RetornoBIO
        public enum RetornoBIO
        { 
            //Constantes retorno Bio
            FALHA_NA_COMUNICACAO = 1,
            PROCESSANDO_ULTIMO_COMANDO = 128,
            FALHA_NA_COMUNICACAO_COM_PLACA_BIO = 129,
            INNER_BIO_NAO_ESTA_EM_MODO_MASTER = 130,
            USUARIO_JA_CADASTRADO_NO_BANCO_DE_DADOS_INNER_BIO = 131,
            USUARIO_NAO_CADASTRADO_NO_BANCO_DE_DADOS_INNER_BIO = 132,
            BASE_DE_DADOS_DE_USUARIOS_ESTA_CHEIA = 133,
            ERRO_NO_SEGUNDO_DEDO_DO_USUARIO = 134,
            SOLICITACAO_PARA_INNER_BIO_INVALIDA = 135
        }
        #endregion

        #region EnviarFormasEntradasOnLine
        public enum EnviarFormasEntradasOnLine
        {
            EntradasON_NAO_ACEITA_ENTRADA_DADOS = 0,
            EntradasON_ACEITA_TECLADO = 1,
            EntradasON_ACEITA_LEITURA_LEITOR1 = 2,
            EntradasON_ACEITA_LEITURA_LEITOR2 = 3,
            EntradasON_TECLADO_E_LEITOR1 = 4,
            EntradasON_TECLADO_E_LEITOR2 = 5,
            EntradasON_LEITOR1_E_LEITOR2 = 6,
            EntradasON_TECLADO_E_LEITOR1_E_LEITOR2 = 7,
            EntradasON_TECLADO_E_VERIF_BIOMETRICA = 10,
            EntradasON_LEITOR1_E_VERIF_BIOMETRICA = 11,
            EntradasON_TECLADO_E_LEITOR1_E_VERIF_BIOMETRICA = 12,
            EntradasON_LEITOR1_COM_VERI_BIO_E_LEITOR2_SEM_VERI_BIO = 13,
            EntradasON_LEITOR1_COM_VERI_BIO_E_LEITOR2_SEM_VERI_BIO_E_TECLADO_SEM_VERI_BIO = 14,
            EntradasON_LEITOR1_E_IDENTIFICACAO_BIO = 100,
            EntradasON_LEITOR1_E_TECLADO_E_IDENTIFICACAO_BIO = 101,
            EntradasON_LEITOR1_E_LEITOR2_E_IDENTIFICACAO_BIO = 102,
            EntradasON_LEITOR1_E_LEITOR2_E_TECLADO_E_IDENTIFICACAO_BIO = 103,
            EntradasON_LEITOR1_INVERTIDO_E_IDENTIFICACAO_BIO = 104,
            EntradasON_LEITOR1_INVERTIDO_E_TECLADO_E_IDENTIFICACAO_BIO = 105
        }
        #endregion        
        
        #region EstadosTeclado
        public enum EstadosTeclado
        {
          //Enumeradores Estados Teclado
          TECLADO_EM_BRANCO,
          AGUARDANDO_TECLADO
        }
        #endregion

        #region EstadosInner
        public enum EstadosInner
        {
            //Enumeradores Estados Inner
            ESTADO_CONECTAR,
            ESTADO_ENVIAR_CFG_OFFLINE,
            ESTADO_COLETAR_BILHETES,
            ESTADO_ENVIAR_CFG_ONLINE,
            ESTADO_ENVIAR_DATA_HORA,
            ESTADO_ENVIAR_MSG_PADRAO,
            ESTADO_CONFIGURAR_ENTRADAS_ONLINE,
            ESTADO_POLLING,
            ESTADO_LIBERAR_CATRACA,
            ESTADO_ENVIAR_BIPCURTO,
            ESTADO_MONITORA_GIRO_CATRACA,
            PING_ONLINE,
            ESTADO_RECONECTAR,
            AGUARDA_TEMPO_MENSAGEM,
            ESTADO_DEFINICAO_TECLADO,
            ESTADO_AGUARDA_DEFINICAO_TECLADO,
            ESTADO_ENVIA_MSG_URNA,
            ESTADO_MONITORA_URNA,
            RECONECTANDO_SERIAL
        }
        #endregion

        #region Modo de comunicacao
        public enum modoComunicacao
        {
            //Constantes de Modo
            MODO_OFF_LINE = 0,
            MODO_ON_LINE = 1
        }
        #endregion

        #region Retorno
        public enum Retorno
        {
            //Constantes de Retorno
            RET_COMANDO_OK = 0,
            RET_COMANDO_ERRO = 1,
            RET_PORTA_NAO_ABERTA = 2,
            RET_PORTA_JA_ABERTA = 3,
            RET_DLL_INNER2K_NAO_ENCONTRADA = 4,
            RET_DLL_INNERTCP_NAO_ENCONTRADA = 5,
            RET_DLL_INNERTCP2_NAO_ENCONTRADA = 6,
            RET_ERRO_GPF = 8,
            RET_TIPO_CONEXAO_INVALIDA = 9,
            RET_PORTA_NAOABERTA = 2,
            RET_PORTA_JAABERTA = 3,
            RET_BIO_USR_JA_CADASTRADO = 131,
            RET_USU_NAO_CADAST = 132,
            RET_BIO_BASE_CHEIA = 133,
            RET_BIO_PROCESSANDO = 128,
            RET_BIO_FALHA_COMUNICACAO = 129,
            RET_DIG_NAO_CONFERE = 134
            
        }
        #endregion

        #region PadraoCartao
        public enum PadraoCartao
        {
            //Constantes de Tipo de Cartão..
            PADRAO_TOPDATA = 0,
            PADRAO_LIVRE = 1
        }
        #endregion

        #region EntradasMudancaOnline
        public enum EntradasMudancaOnline
        {
            //Constantes do Método EntradasMudancaOnline
            NAO_ACEITA_ENTRADA_DADOS = 0,
            ACEITA_TECLADO = 1,
            ACEITA_LEITURA_LEITOR1 = 2,
            ACEITA_LEITURA_LEITOR2 = 3,
            TECLADO_E_LEITOR1 = 4,
            TECLADO_E_LEITOR2 = 5,
            LEITOR1_E_LEITOR2 = 6,
            TECLADO_LEITOR1_LEITOR2 = 7,
            TECLADO_E_VERI_BIOMETRICA = 10,
            LEITOR1_E_VERI_BIOMETRICA = 11,
            TECLADO_E_LEITOR1_E_VERI_BIOMETRICA = 12,
            LEITOR1_E_VERI_BIOMETRICA_LEITOR2_SEM_VERI_BIOMETRICA = 13,
            LEITOR1_E_VERI_BIOMETRICA_LEITOR2_SEM_VERI_BIOMETRICA_E_TECLADO_SEM_VERI_BIOMETRICA = 14,
            LEITOR1_E_IDENTIFICACAO_BIOMETRICA = 100,
            LEITOR1_E_TECLADO_IDENTIFICACAO_BIOMETRICA = 101,
            LEITOR1_E_LEITOR2_E_IDENTIFICACAO_BIOMETRICA = 102,
            LEITOR1_E_LEITOR2_E_TECLADO_E_IDENTIFICACAO_BIOMETRICA = 103,
            LEITOR1_INVERTIDO_IDENTIFICACAO_BIOMETRICA = 104,
            LEITOR1_INVERTIDO_E_TECLADO_E_IDENTIFICACAO_BIOMETRICA = 105
        }
        #endregion

        #region Origem
        public enum Origem
        {
            //Constantes de Origem do Método Receber Dados Online
            VIA_TECLADO = 1,
            VIA_LEITOR1 = 2,
            VIA_LEITOR2 = 3,
            SENSOR_DA_CATRACA_OBSOLETO_ = 4,
            FIM_TEMPO_ACIONAMENTO = 5,
            GIRO_DA_CATRACA_TOPDATA = 6,
            URNA = 7,
            EVENTO_SENSOR1 = 8,
            EVENTO_SENSOR2 = 9,
            EVENTO_SENSOR3 = 10,
            SENSOR_BIOMETRICO = 12,
            TECLA_FUNCAO = 65,
            TECLA_ANULA = 42,
            TECLA_ENTRADA = 66,
            TECLA_SAIDA = 67,
            TECLA_CONFIRMA = 35,
            PRESENCA_DEDO = 37

        }
        #endregion

        #region MudancaOnlineOffline
        public enum MudancaOnlineOffline
        { 
            DESABILITA = 0,
            SEM_PING_ONLINE = 1,
            COM_PING_ONLINE = 2
        }
        #endregion

        #region LeitorEntrada
        public enum LeitorEntrada
        {
            LEITOR1_DESABILITADO = 0,
            LEITOR1_SOMENTE_ENTRADA = 1,
            LEITOR1_SOMENTE_SAIDA = 2,
            LEITOR1_ENTRADA_SAIDA = 3,
            LEITOR1_SAIDA_ENTRADA = 4,
            LEITOR2_SOMENTE_ENTRADA = 1,
            LEITOR2_SOMENTE_SAIDA = 2,
            LEITOR2_ENTRADA_SAIDA = 3,
            LEITOR2_SAIDA_ENTRADA = 4
        }
        #endregion

        #region Acionamento
        public enum Acionamento
        {
            //Constantes de Função de Acionamento..
            Acionamento_Coletor = 0,
            Acionamento_Catraca_Entrada_E_Saida = 1,
            Acionamento_Catraca_Entrada = 2,
            Acionamento_Catraca_Saida = 3,
            Acionamento_Catraca_Saida_Liberada = 4,
            Acionamento_Catraca_Entrada_Liberada = 5,
            Acionamento_Catraca_Liberada_2_Sentidos = 6,
            Acionamento_Catraca_Sentido_Giro = 7,
            Acionamento_Catraca_Urna = 8
        }
        #endregion
    }
}
