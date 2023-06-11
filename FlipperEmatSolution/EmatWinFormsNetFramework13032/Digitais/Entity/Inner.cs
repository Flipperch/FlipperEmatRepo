using System;
using System.Collections.Generic;
using System.Text;

namespace EmatWinFormsNetFramework13032.Digitais.Entity
{
    public class Inner
    {
        #region Propriedades

        #region Numero
        private int _numero;
        public int Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        #endregion

        private Enumeradores.modoComunicacao modoComunicacao;

        public Enumeradores.modoComunicacao ModoComunicacao
        {
            get { return modoComunicacao; }
            set { modoComunicacao = value; }
        }

        #region EstadoAtual
        private Enumeradores.EstadosInner _estadoAtual;
        public Enumeradores.EstadosInner EstadoAtual
        {
            get { return _estadoAtual; }
            set { _estadoAtual = value; }
        }
        #endregion

        #region EstadoTeclado
        private Enumeradores.EstadosTeclado _estadoTeclado;
        public Enumeradores.EstadosTeclado EstadoTeclado
        {
            get { return _estadoTeclado; }
            set { _estadoTeclado = value; }
        }
        #endregion

        #region EstadoSolicitacaoPingOnLine
        private Enumeradores.EstadosInner _estadoSolicitacaoPingOnLine;
        public Enumeradores.EstadosInner EstadoSolicitacaoPingOnLine
        {
            get { return _estadoSolicitacaoPingOnLine; }
            set { _estadoSolicitacaoPingOnLine = value; }
        }
        #endregion

        #region TempoColeta
        private long _tempoColeta;
        public long TempoColeta
        {
            get { return _tempoColeta; }
            set { _tempoColeta = value; }
        }
        #endregion

        #region Catraca 
        private bool _catraca;
        public bool Catraca
        {
            get { return _catraca; }
            set { _catraca = value; }
        }
        #endregion

        #region Biometrico
        private bool _biometrico;
        public bool Biometrico
        {
            get { return _biometrico; }
            set { _biometrico = value; }
        }
        #endregion

        #region Urna
        private bool _urna;
        public bool Urna
        {
            get { return _urna; }
            set { _urna = value; }
        }
        #endregion

        #region QtdDigitos
        private int _qtdDigitos;
        public int QtdDigitos
        {
            get { return _qtdDigitos; }
            set { _qtdDigitos = value; }
        }
        #endregion

        #region CntDoEvents
        private int _cntDoEvents;

        public int CntDoEvents
        {
            get { return _cntDoEvents; }
            set { _cntDoEvents = value; }
        }
        #endregion

        #region TipoLeitor
        private int _tipoLeitor;

        public int TipoLeitor
        {
            get { return _tipoLeitor; }
            set { _tipoLeitor = value; }
        }
        #endregion

        #region AcionarReles
        private int _AcionarReles = 0;
        /// <summary>
        /// Indica se o Inner deve acionar reles ap�s a valida��o do usu�rio.
        /// </summary>
        public int AcionarReles
        {
            get { return _AcionarReles; }
            set { _AcionarReles = value; }
        }
        #endregion

        #region Box
        private bool _box = false;
        /// <summary>
        /// Flag que indica se o Inner � ou n�o uma catraca box, que permite utiliza��o de dois leitores.
        /// </summary>
        public bool BOX
        {
            get { return _box; }
            set { _box = value; }
        }
        #endregion

        #region FInvertido
        private string _fInvertido = "N";
        /// <summary>
        /// Flag que indica se � ou n�o uma catraca invertida "a esquerda de quem entra" (Entrada/Saida, Saida/Entrada).
        /// </summary>
        public string FInvertido
        {
            get { return _fInvertido; }
            set { _fInvertido = value; }
        }
        #endregion

        #region ValorLeitor1
        private byte _valorLeitor1;
        /// <summary>
        /// Armazena o valor a ser configurado para o leitor 1
        /// </summary>
        public byte ValorLeitor1
        {
            get { return _valorLeitor1; }
            set { _valorLeitor1 = value; }
        }
        #endregion

        #region ValorLeitor2
        private byte _valorLeitor2;
        /// <summary>
        /// Armazena o valor a ser configurado para o leitor 2
        /// </summary>
        public byte ValorLeitor2
        {
            get { return _valorLeitor2; }
            set { _valorLeitor2 = value; }
        }
        #endregion

        #region Teclado
        private bool _teclado;
        public bool Teclado
        {
            get { return _teclado; }
            set { _teclado = value; }
        }
        #endregion

        #region PadraoCartao
        private int _padraoCartao;
        public int PadraoCartao
        {
            get { return _padraoCartao; }
            set { _padraoCartao = value; }
        }
        #endregion

        #region Verifica��o
        private byte _verificacao;
        public byte Verificacao
        {
            get { return _verificacao; }
            set { _verificacao = value; }
        }
        #endregion

        #region Identifica��o
        private byte _identificacao;
        public byte Identificacao
        {
            get { return _identificacao; }
            set { _identificacao = value; }
        }
        #endregion

        #region DoisLeitores
        private bool _doisLeitores;
        public bool DoisLeitores
        {
            get { return _doisLeitores; }
            set { _doisLeitores = value; }
        }
        #endregion

        private int _countReconexao;

        public int CountReconexao
        {
            get { return _countReconexao; }
            set { _countReconexao = value; }
        }
	

        private int _countPingFail;

        public int CountPingFail
        {
            get { return _countPingFail; }
            set { _countPingFail = value; }
        }

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        private int _countRepeatPingOnline;
        public int CountRepeatPingOnline
        {
            get { return _countRepeatPingOnline; }
            set { _countRepeatPingOnline = value; }
        }

        private bool _flagMensagemTemporizada;
        public bool FlagMensagemTemporizada
        {
            get { return _flagMensagemTemporizada; }
            set { _flagMensagemTemporizada = value; }
        }

        private DateTime _teporizador;
        public DateTime Temporizador
        {
            get { return _teporizador; }
            set { _teporizador = value; }
        }

        private DateTime _tempoInicialMensagem;
        public DateTime TempoInicialMensagem
        {
            get { return _tempoInicialMensagem; }
            set { _tempoInicialMensagem = value; }
        }        
        
        private DateTime _temporizadorErroComSerial;
        /// <summary>
        /// Temporizador respos�vel por testar quando a conex�o serial esta com Erro, recebe Datetime.now quando receber um erro do comando Receber
        /// Dados Online..
        /// </summary>
        public DateTime TemporizadorErroComSerial
        {
           get { return _temporizadorErroComSerial;}
           set { _temporizadorErroComSerial = value;}
        }

        #region CountTentativasEnvioComando
        private int _countTentativasEnvioComando;
        public int CountTentativasEnvioComando
        {
            get { return _countTentativasEnvioComando; }
            set { _countTentativasEnvioComando = value; }
        }
        #endregion

        #region TempoInicialPingOnLine
        private DateTime _tempoInicialPingOnLine;
        public DateTime TempoInicialPingOnLine
        {
            get { return _tempoInicialPingOnLine; }
            set { _tempoInicialPingOnLine = value; }
        }
        #endregion

        #region LinhaInner
        private string _linhaInner;
        public string LinhaInner
        {
            get { return _linhaInner; }
            set { _linhaInner = value; }
        }
        #endregion

        #region VariacaoInner
        private short _variacaoInner;
        public short VariacaoInner
        {
            get { return _variacaoInner; }
            set { _variacaoInner = value; }
        }
        #endregion

        #region VersaoInner
        private string _versaoInner;
        public string VersaoInner
        {
            get { return _versaoInner; }
            set { _versaoInner = value; }
        }
        #endregion 

        #region ModeloBioInner
        private string _modeloBioInner;
        public string ModeloBioInner
        {
            get { return _modeloBioInner; }
            set { _modeloBioInner = value; }
        }
        #endregion

        #region VersaoBio
        private string _versaoBio;
        public string VersaoBio
        {
            get { return _versaoBio; }
            set { _versaoBio = value; }
        }
        #endregion

        #region Lista
        private bool _lista;
        public bool Lista
        {
            get { return _lista; }
            set { _lista = value; }
        }
        #endregion

        #region ListaBio
        private bool _listaBio;
        public bool ListaBio
        {
            get { return _listaBio; }
            set { _listaBio = value; }
        }
        #endregion

        #endregion

        #region M�todos

        #region ToString
        public override string ToString()
        {
            //return base.ToString();
            return "N�mero Inner: " + this.Numero.ToString() +
                   " | N�mero de D�gitos: " + this.QtdDigitos.ToString() +
                   " | Catraca: " + this.Catraca.ToString() +
                   " | Biometria: " + this.Biometrico.ToString() +
                   " | Verifica��o: " + (this.Verificacao == 0 ? "False" : "True") +
                   " | Identifica��o: " + (this.Identificacao == 0 ? "False" : "True") +
                   " | Cart�o: " + (this.PadraoCartao == 0 ? "Barras" : (this.PadraoCartao == 1 ? "Magn�tico" : (this.PadraoCartao == 2 ? "Abatrack" : this.PadraoCartao == 3 ? "Wiegand" : "Smart Card"))) +
                   " | " + (this.DoisLeitores ? "Dois Leitores" : "Um Leitor") +
                   " | Lista: " + this.Lista.ToString() +
                   (this.LinhaInner != null ? " | Linha: " + this.LinhaInner : "") +
                   (this.VariacaoInner != 0 ? " | Varia��o: " + this.VariacaoInner : "") +
                   (this.VersaoInner != null ? " | Vers�o : " + this.VersaoInner : "") +
                   (this.Biometrico ? (this.ModeloBioInner != null ? " | " + this.ModeloBioInner : "") : "") +
                   (this.Biometrico ? (this.VersaoBio != null ? " | Vers�o Bio: " + this.VersaoBio : "") : "");

        }
        #endregion

        #endregion
    }
}
