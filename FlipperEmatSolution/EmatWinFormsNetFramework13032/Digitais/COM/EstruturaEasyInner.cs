using System;
using System.Collections.Generic;
using System.Text;

namespace EmatWinFormsNetFramework13032.Digitais.COM
{
    class EstruturaEasyInner
    {
        #region COMANDOS DIRETOS

        //#region DefinirTipoConexao
        //#region AbrirPortaComunicacao
        //#region FecharPortaComunicacao
        //#region DefinirPadraoCartao
        //#region AcionarRele1
        //#region AcionarRele2
        //#region EnviarComandoAcessoNegado
        //#region ManterRele1Acionado
        //#region ManterRele2Acionado
        //#region DesabilitarRele1
        //#region DesabilitarRele2
        //#region AcionarBipCurto
        //#region AcionarBipLongo
        //#region Ping
        //#region PingOnLine
        //#region ResetarModoOnLine
        //#region LigarBackLite
        //#region DesligarBackLite
        //#region LigarBipIntermitente
        //#region DesligarBipIntermitente
        //#region LiberarCatracaEntrada
        //#region LiberarCatracaSaida
        //#region LiberarCatracaEntradaInvertida
        //#region LiberarCatracaSaidaInvertida
        //#region LiberarCatracaDoisSentidos
        //#region LevantarParaOnLine
        //#region ReceberVersaoFirmware
       #endregion

        #region FUNÇÕES DE CONFIGURAÇÕES GERAIS DOS INNERS

        //#region ConfigurarInnerOffLine
        //#region ConfigurarInnerOnLine
        //#region HabilitarTeclado
        //#region ConfigurarAcionamento1
        //#region ConfigurarAcionamento2
        //#region ConfigurarTipoLeitor
        //#region ConfigurarLeitor1
        //#region ConfigurarLeitor2
        //#region DefinirCodigoEmpresa
        //#region DefinirNivelAcesso
        //#region UtilizarSenhaAcesso
        //#region DefinirTipoListaAcesso
        //#region DefinirQuantidadeDigitosCartao
        //#region AvisarQuandoMemoriaCheia
        //#region DefinirPorcentagemRevista
        //#region RegistrarAcessoNegado
        //#region CartaoMasterLiberaAcesso
        //#region DefinirLogicaRele
        //#region DesabilitarBloqueioCatracaMicroSwitch
        //#region DefinirFuncaoDefaultLeitoresProximidade
        //#region DefinirNumeroCartaoMaster
        //#region DefinirFormasPictogramasMillenium
        //#region DesabilitarBipCatraca
        //#region DefinirEventoSensor
        //#region PermitirCadastroInnerBioVerid
        //#region ReceberDataHoraDadosOnLine
        //#region InserirQuantidadeDigitoVariavel
        //#region ConfigurarWiegandDoisLeitores
        //#region DefinirFuncaoDefaultSensorBiometria
        //#region EnviarConfiguracoes
        #endregion

        #region FUNÇÕES PARA MANIPULAR DATA/HORA INNER

        //#region EnviarRelogio
        //#region ReceberRelogio
        //#region EnviarHorarioVerao
        #endregion

        #region FUNÇÕES PARA MANIPULAR HORÁRIOS DE ACESSO

        //#region ApagarHorariosAcesso
        //#region InserirHorarioAcesso
        //#region EnviarHorariosAcesso
        #endregion

        #region FUNÇÕES PARA MANIPULAR LISTA DE ACESSO

        //#region ApagarListaAcesso
        //#region InserirUsuarioListaAcesso
        //#region EnviarListaAcesso
        #endregion

        #region FUNÇÕES PARA MANIPULAR AS MENSAGENS ONLINE DO INNER

        //#region EnviarMensagemPadraoOnLine
        //#region EnviarMensagemTemporariaOnLine
        #endregion

        #region FUNÇÕES PARA MANIPULAR AS MENSAGENS OFFLINE DO INNER

        //#region DefinirMensagemEntradaOffLine
        //#region DefinirMensagemSaidaOffLine
        //#region DefinirMensagemPadraoOffLine
        //#region DefinirMensagemFuncaoOffLine
        //#region HabilitarScoreMensagemOffLine
        //#region EnviarMensagensOffLine
        //#region ApagarMensagensOffLine
        //#region DefinirConfiguracoesFuncoes
        //#region HabilitarScoreFuncoes
        //#region EnviarConfiguracoesFuncoes

        #endregion

        #region FUNÇÕES PARA MANIPULAR OS HORÁRIOS DE TOQUE DA SIRENE DO INNER
      
        //#region ApagarHorariosSirene
        //#region InserirHorarioSirene
        //#region EnviarHorariosSirene
        #endregion

        #region FUNÇÃO PARA RECEBER OS BILHETES OFFLINE DO INNER
        //#region ColetarBilhete
        #endregion

        #region FUNÇÕES PARA MANIPULAR OS DADOS ONLINE DO INNER

        //#region EnviarFormasEntradasOnLine
        //#region ReceberDadosOnLine
        #endregion

        #region FUNÇÃO PARA LER OS STATUS DOS SENSORES DO INNER
        //#region LerSensoresInner
        #endregion

        #region FUNÇÕES PARA MANIPULAR AS MENSAGENS DA IMPRESSORA DO INNER

        //#region EnviarMensagemImpressora00
        //#region EnviarMensagemImpressoraFF
        #endregion

        #region FUNÇÕES PARA CONFIGURAR A MUDANÇA AUTOMÁTICA DE ON-LINE PARA OFF-LINE DO INNER

        //#region HabilitarMudancaOnLineOffLine
        //#region DefinirEntradasMudancaOffLine
        //#region DefinirEntradasMudancaOffLineComBiometria
        //#region DefinirMensagemPadraoMudancaOffLine
        //#region DefinirMensagemPadraoMudancaOnLine
        //#region DefinirEntradasMudancaOnLine
        //#region DefinirConfiguracaoTecladoOnLine
        //#region EnviarConfiguracoesMudancaAutomaticaOnLineOffLine
        #endregion

        #region FUNÇÕES ESPECÍFICAS DO INNER BIO

        //#region SolicitarModeloBio
        //#region ReceberModeloBio
        //#region SolicitarVersaoBio
        //#region ReceberVersaoBio
        //#region SolicitarQuantidadeUsuariosBio
        //#region ReceberQuantidadeUsuariosBio
        //#region SolicitarUsuarioCadastradoBio
        //#region ReceberUsuarioCadastradoBio
        //#region SolicitarExclusaoUsuario
        //#region UsuarioFoiExcluido
        //#region InserirUsuarioLeitorBio
        //#region ResultadoInsercaoUsuarioLeitorBio
        //#region FazerVerificacaoBiometricaBio
        //#region ResultadoVerificacaoBiometrica
        //#region FazerIdentificacaoBiometricaBio
        //#region ResultadoIdentificacaoBiometrica
        //#region SolicitarTemplateLeitor
        //#region ReceberTemplateLeitor
        //#region ConfigurarBio
        //#region ResultadoConfiguracaoBio
        //#region EnviarUsuarioBio
        //#region UsuarioFoiEnviado
        //#region CompararDigitalLeitor
        //#region ResultadoComparacaoDigitalLeitor
        //#region IncluirUsuarioSemDigitalBio
        //#region EnviarListaUsuariosSemDigitalBio
        //#region SetarBioLight
        #endregion

        #region CONFIGURAÇÕES DE AJUSTES BIOMÉTRICOS

        //#region ConfigurarAjustesSensibilidadeBio
        //#region ConfigurarAjustesQualidadeBio
        //#region ConfigurarAjustesSegurancaBio
        //#region ConfigurarCapturaAdaptativaBio
        //#region ConfigurarFiltroBio
        //#region EnviarAjustesBio
        #endregion

        #region FUNÇÕES PARA RECEBER TODOS OS USUÁRIOS CADASTRADOS NO INNER BIO

        //#region InicializarColetaListaUsuariosBio
        //#region SolicitarListaUsuariosBio
        //#region ReceberPacoteListaUsuariosBio
        //#region ReceberUsuarioLista
        //#region TemProximoUsuario
        //#region TemProximoPacote
        #endregion

        #region FUNÇÕES DO INNER COM MODEM

        //#region EnviarStringInicializacaoModem
        //#region LerByteModem
        //#region ConectarModem
        #endregion

        #region INNER PADRÃO - PRIMEIRA GERAÇÃO DE INNERS
        //#region SetarInnerOld
        #endregion

        #region FUNÇÕES DO INNER VERID
        //#region IncluirUsuarioVerid
        //#region ResultadoInclusaoUsuarioVerid
        //#region CompararTemplateVerid
        //#region ResultadoComparacaoTemplateVerid
        //#region CriarUsuarioLeitorVerid
        //#region ResultadoInclusaoUsuarioLeitorVerid
        //#region SolicitarTotalUsuariosVerid
        //#region ReceberTotalUsuariosVerid
        //#region LocalizarPrimeiroUsuarioVerid
        //#region LocalizarProximoUsuarioVerid
        //#region LocalizarUsuarioVerid
        //#region ReceberUsuarioVerid
        //#region ApagarUsuarioVerid
        //#region ResultadoExclusaoUsuarioVerid
        //#region ApagarTodosUsuariosVerid
        //#region ResultadoExclusaoTodosUsuariosVerid
        //#region CompararPINVerid
        //#region ResultadoComparacaoPINVerid
        //#region ConfigurarRedeVerid
        //#region ResultadoConfiguracaoRedeVerid
        #endregion

        #region DevolverCartao
        //#region EngolirCartao
        //#region ConfigurarLeitorProximidadeHIDAbaTrack2
        //#region ConfigurarLeitorProximidadeMotorolaAbaTrack2
        //#region ConfigurarLeitorProximidadeWiegand
        //#region ConfigurarLeitorProximidadeSmartCard
        //#region ConfigurarLeitorProximidadeAcura
        //#region ConfigurarLeitorProximidadeWiegandFacilityCode
        //#region ConfigurarLeitorProximidadeSmartCardAcura
        //#region ResultadoEnvioAjustesBio
        #endregion
    }
}
