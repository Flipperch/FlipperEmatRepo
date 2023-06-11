using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;

//Refer�ncias Nitgen
//using NBioBSPCOMLib;
using NITGEN.SDK.NBioBSP;

//Refer�ncias Projeto
using EmatWinFormsNetFramework13032.Digitais.COM;
using EmatWinFormsNetFramework13032.Digitais.Entity;
using System.Configuration;

//Refer�ncia EasyInner
namespace EmatWinFormsNetFramework13032.Digitais.FrmOnline
{   
  
    public class FrmOnlineController
    {

        public static int continua { get; set; }

        //Catraca
        public static bool LiberaEntrada = false;
        public static bool LiberaSaida = false;
        public static bool LiberaEntradaInvertida = false;
        public static bool LiberaSaidaInvertida = false;

        //Teclado
        public static string ultCartao;
        public static int intTentativas;

        private static Boolean InnerNetAcesso;
        private static byte InnerAcessoBio;


        //Templates online
        private string usr;
        private int numtpl;
         
        #region Propriedades

        #region UiBIO
        private static FrmOnline _UiMainOnline;
        public static FrmOnline UiBIO
        {
            get { return _UiMainOnline; }
            set { _UiMainOnline = value; }
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

        #region M�todos Auxiliares

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
            UiBIO.Timeout = (int)EasyInner.RetornarSegundosSys() + 7;
        }
        #endregion

        #region ValidaNumeroUsuario
        private static bool ValidaNumeroUsuario(ref string NumUsuario)
        {
            int saida;

            //Testa se � um N�mero
            if (int.TryParse(NumUsuario, out saida))
            {
                //Preenche com 0 a esquerda caso n�o tenha 10 N�meros
                while (NumUsuario.Length < 10)
                {
                    NumUsuario = "0" + NumUsuario;
                }
                return true;
            }
            else
                return false;
        }
        #endregion

        #region TratarRetornoErro
        //***********************************************************************************
        //Tratamento de erros
        //***********************************************************************************
        private static void TratarRetornoErro(int Ret)
        {            
            switch ((Enumeradores.RetornoBIO)Ret)
            {
                case Enumeradores.RetornoBIO.FALHA_NA_COMUNICACAO:
                    MessageBox.Show("Erro: Falha na comunica��o com o Inner BIO.", "Erro");
                    break;

                case Enumeradores.RetornoBIO.PROCESSANDO_ULTIMO_COMANDO:
                    MessageBox.Show("Aten��o: Ainda processando �ltimo Comando.", "Erro");
                    break;

                case Enumeradores.RetornoBIO.FALHA_NA_COMUNICACAO_COM_PLACA_BIO:
                    MessageBox.Show("Erro: Falha na comunica��o com a placa BIO.", "Erro");
                    break;

                case Enumeradores.RetornoBIO.INNER_BIO_NAO_ESTA_EM_MODO_MASTER:
                    MessageBox.Show("Erro: Inner BIO n�o esta em modo MASTER.", "Erro");
                    break;

                case Enumeradores.RetornoBIO.USUARIO_JA_CADASTRADO_NO_BANCO_DE_DADOS_INNER_BIO:
                    MessageBox.Show("Erro: Usu�rio ja cadastrado no banco de dados do Inner BIO.", "Erro");
                    break;

                case Enumeradores.RetornoBIO.USUARIO_NAO_CADASTRADO_NO_BANCO_DE_DADOS_INNER_BIO:
                    MessageBox.Show("Erro: Usu�rio n�o cadastrado no banco de dados Inner BIO.", "Erro");
                    break;

                case Enumeradores.RetornoBIO.BASE_DE_DADOS_DE_USUARIOS_ESTA_CHEIA:
                    MessageBox.Show("Erro: Base de dados de Usu�rios esta cheia.", "Erro");
                    break;

                case Enumeradores.RetornoBIO.ERRO_NO_SEGUNDO_DEDO_DO_USUARIO:
                    MessageBox.Show("Erro: Erro no segundo dedo do Usu�rio.", "Erro");
                    break;

                case Enumeradores.RetornoBIO.SOLICITACAO_PARA_INNER_BIO_INVALIDA:
                    MessageBox.Show("Erro: Solicita��o para Inner BIO Inv�lida.", "Erro");
                    break;

                default:
                    MessageBox.Show("Erro: Mensagem Indefinida", "Erro");
                    break;
            }
        }

        #endregion
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! - Configura��o Inner!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        #region AdicionarInnerOnline
        //***********************************************************************************
        //Adiciona o Inner em mem�ria
        //***********************************************************************************
        internal static void AdicionarInnerOnline(FrmOnline UiMainOnline)
        {
            UiBIO = UiMainOnline;

            //Cria um novo Objeto Inner.
            Inner objInner = new Inner();
            //objInner.Biometrico = UiMainOnline.ckbBIO.Checked;
            objInner.Biometrico = true;

            //Inicializa o Inner com as op��es selecionadas pelo usu�rio..

            //Se catraca
            //if (0 != (byte)Enumeradores.Acionamento.Acionamento_Coletor)
            //{
                objInner.Catraca = true;
            //}
            //else
            //{
            //    objInner.Catraca = false;
            //}

            //Campo obrigat�rio
            if (0 == -1)
            {
                //MessageBox.Show("Favor selecionar um tipo de leitor !", "Aten��o");
                return;
            }

            ////Se Catraca
            //if ((0 == (byte)Enumeradores.Acionamento.Acionamento_Coletor) || ((UiMainOnline.optDireita.Checked) || (UiMainOnline.optEsquerda.Checked)))
            //{
            //}
            //else
            //{
            //    MessageBox.Show("Favor informar o lado de instala��o da catraca !", "Aten��o");
            //    return;
            //}
            
            //Seta nas configura��es do Inner os dados informados em tela
            objInner.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            objInner.Numero = 1;
            objInner.QtdDigitos = 8;
            objInner.Teclado = true;
            objInner.Lista = false;
            objInner.ListaBio = false;
            objInner.PadraoCartao = 0;
            objInner.Identificacao = 1;
            objInner.Verificacao = 0;
            objInner.DoisLeitores = true;
            objInner.VariacaoInner = 0;
            
            //Testa se existe algum inner com o mesmo nome na lista da Janela
            //foreach (Inner inner in UiMainOnline.LstInners)
            //{
            //    if (inner.Numero == objInner.Numero)
            //    {
            //        MessageBox.Show("N�mero de Inner j� cadastrado em Mem�ria.", "Aten��o");
            //        return;
            //    }
            //}

            //Adiciona o Inner a mem�ria 
            UiMainOnline.LstInners.Add(objInner);
        }
        #endregion
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        #region AtualizaListaInnerOnline
        //***********************************************************************************
        //Atualiza lista de Inner's
        //***********************************************************************************
        internal static void AtualizaListaInnerOnline(FrmOnline UiMainOnline)
        {
            //Limpa a lista de Inner em mem�ria..
            UiMainOnline.lstInnersCadastrados.Items.Clear();

            //Adiciona os inners novamente em mem�ria..
            foreach (Inner objInner in UiMainOnline.LstInners)
            {
                UiMainOnline.lstInnersCadastrados.Items.Add(objInner);
            }
        }
        #endregion

        #region RemoverInnerLista
        //***********************************************************************************
        //Remove Inner da mem�ria
        //***********************************************************************************
        internal static void RemoverInnerLista(FrmOnline UiMainOnline)
        {
            UiBIO = UiMainOnline;

            if (UiBIO.lstInnersCadastrados.SelectedItem != null)
            {
                foreach (Inner inner in UiBIO.LstInners)
                {
                    if (inner.Numero == ((Inner)UiBIO.lstInnersCadastrados.SelectedItem).Numero)
                    {
                        //Remove
                        UiBIO.LstInners.Remove(inner);
                        UiBIO.lstInnersCadastrados.Items.Remove(UiBIO.lstInnersCadastrados.SelectedItem);
                        
                        //Atualiza lista
                        if (UiMainOnline.lstInnersCadastrados.Items.Count > 0)
                        {
                            UiMainOnline.lstInnersCadastrados.SelectedIndex = UiMainOnline.lstInnersCadastrados.Items.Count - 1;
                        }
                        MessageBox.Show("Inner removido da mem�ria");
                        break;
                    }
                }
            }
            else
            {
                //Campo obrigat�rio
                MessageBox.Show("� necess�rio selecionar um Inner para remover da mem�ria!");
            }
        }
        #endregion

        #region DefineValoresParaConfigurarLeitores
        //***********************************************************************************
        //CONFIGURA��O LEITORES
        //De acordo com o lado da catraca, coletor ou se � dois leitores
        //***********************************************************************************
        private static void DefineValoresParaConfigurarLeitores(Inner innerAtual)
        {
            //Configura��o Catraca Esquerda ou Direita
            //define os valores para configurar os leitores de acordo com o tipo de inner
            //if (innerAtual.DoisLeitores) 
            //{
            //    if (UiBIO.optDireita.Checked) 
            //    {
            //      //Direita Selecionado
            //      innerAtual.ValorLeitor1 = Convert.ToByte(Enumeradores.Operacao.SOMENTE_ENTRADA);
            //      innerAtual.ValorLeitor2 = Convert.ToByte(Enumeradores.Operacao.SOMENTE_SAIDA);
            //    }
            //    else
            //    {
            //      //Esquerda Selecionado
            //      innerAtual.ValorLeitor1 = Convert.ToByte(Enumeradores.Operacao.SOMENTE_SAIDA);
            //      innerAtual.ValorLeitor2 = Convert.ToByte(Enumeradores.Operacao.SOMENTE_ENTRADA);
            //    }
            //}
            //else
            //{
                //if (UiBIO.optDireita.Checked)
                //{
                //  //Direita Selecionado
                  //innerAtual.ValorLeitor1 = Convert.ToByte(Enumeradores.Operacao.ENTRADA_E_SAIDA);
                //}
                //else
                //{
                  //Esquerda Selecionado
                    innerAtual.ValorLeitor1 = Convert.ToByte(Enumeradores.Operacao.ENTRADA_E_SAIDA_INVERTIDAS);
            //    }

                    innerAtual.ValorLeitor2 = Convert.ToByte(Enumeradores.Operacao.ENTRADA_E_SAIDA_INVERTIDAS);
            
            //}
            
        }
        #endregion

        #region MontaConfiguracaoInner
        //***********************************************************************************
        //MONTAR CONFIGURA��O
        //Envia as configura��es, teclado, lista, biometria...
        //***********************************************************************************
        private static void MontaConfiguracaoInner(Inner innerAtual, Enumeradores.modoComunicacao modo)
        {
            ////Antes de realizar a configura��o precisa definir o Padr�o do cart�o 
            //if (UiBIO.cboPadraoCartaoOnline.SelectedIndex == 0)
            //{
            //    EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_TOPDATA);
            //}
            //else
            //{
                EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_LIVRE);
            //}
            
            //Define Modo de comunica��o
            if (modo == Enumeradores.modoComunicacao.MODO_OFF_LINE)
            {
                //Configura��es para Modo Offline.
                //Prepara o Inner para trabalhar no modo Off-Line, por�m essa fun��o
                //ainda n�o envia essa informa��o para o equipamento.
                EasyInner.ConfigurarInnerOffLine();
            }
            else
            {
                //Configura��es para Modo Online.
                //Prepara o Inner para trabalhar no modo On-Line, por�m essa fun��o
                //ainda n�o envia essa informa��o para o equipamento.
                EasyInner.ConfigurarInnerOnLine();
            }

            //Verificar
            //Acionamentos 1 e 2
            //Configura como ir� funcionar o acionamento(rele) 1 e 2 do Inner, e por
            //quanto tempo ele ser� acionado.
            //switch (0)
            //{
            //    //Coletor
            //    case (byte)Enumeradores.Acionamento.Acionamento_Coletor:
            //        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA_OU_SAIDA, 3);
            //        EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA_OU_SAIDA, 2);
            //        break;

            //    //Catraca
            //    case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_E_Saida:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_SAIDA_LIBERADA,5);
                    //EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA_OU_SAIDA, 5);
            //        break;
            //    case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada:
            //        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA, 5);
            //        EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
            //        break;
            //    case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Saida:
            //        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_SAIDA, 5);
            //        EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
            //        break;
            //    case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Urna:
            //        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA_OU_SAIDA, 5);
            //        EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_SAIDA, 5);
            //        break;
            //    case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Saida_Liberada:
            //        //Se Esquerda Selecionado - Inverte c�digo
            //        if ((0 != (byte)Enumeradores.Acionamento.Acionamento_Coletor) )
            //        {
            //            EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_ENTRADA_LIBERADA, 5);
            //        }
            //        else
            //        {
            //            EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_SAIDA_LIBERADA, 5);
            //        }
            //        EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
            //        break;
            //    case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_Liberada:
            //        //Se Esquerda Selecionado - Inverte c�digo
            //        if ((0 != (byte)Enumeradores.Acionamento.Acionamento_Coletor) )
            //        {
            //            EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_SAIDA_LIBERADA, 5);
            //        }
            //        else
            //        {
            //            EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_ENTRADA_LIBERADA, 5);
            //        }
            //        EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
            //        break;
            //    case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Liberada_2_Sentidos:
            //        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_LIBERADA_DOIS_SENTIDOS, 5);
            //        EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
            //        break;
            //    case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Sentido_Giro:
            //        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_LIBERADA_DOIS_SENTIDOS_MARCACAO_REGISTRO, 5);
            //        EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
            //        break;
            //}

            //Configurar tipo do leitor
            //switch (innerAtual.PadraoCartao)
            //{
                ////leitor barras
                //case (byte)Enumeradores.TipoLeitor.CODIGO_DE_BARRAS:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.CODIGO_DE_BARRAS);
            
                    EasyInner.DefinirQuantidadeDigitosCartao(Convert.ToByte(innerAtual.QtdDigitos));
                //    break;
                ////leitor magn�tico
                //case (byte)Enumeradores.TipoLeitor.MAGNETICO:
                //    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.MAGNETICO);
                //    EasyInner.DefinirQuantidadeDigitosCartao(Convert.ToByte(innerAtual.QtdDigitos));
                //    break;
                ////leitor proximidade abatrack
                //case (byte)Enumeradores.TipoLeitor.PROXIMIDADE_ABATRACK2:
                //    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.PROXIMIDADE_ABATRACK2);
                //    EasyInner.DefinirQuantidadeDigitosCartao(Convert.ToByte(innerAtual.QtdDigitos));
                //    break;
                ////leitor wiegand - 6 d�gitos
                //case (byte)Enumeradores.TipoLeitor.WIEGAND:
                //    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.WIEGAND);
                //    EasyInner.DefinirQuantidadeDigitosCartao(6);
                //    break;
                ////leitor smart (abatrack 10 d�gitos)
                //case 4:
                //    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.PROXIMIDADE_ABATRACK2);
                //    EasyInner.DefinirQuantidadeDigitosCartao(10);
                //    break;
                ////leitor wiegand com facility code - 10 d�gitos
                //case 33:
                //    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.WIEGAND);
                //    EasyInner.DefinirQuantidadeDigitosCartao(10);
                //    break;
                ////valor inv�lido - configura como barras
                //default:
                //    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.CODIGO_DE_BARRAS);
                //    EasyInner.DefinirQuantidadeDigitosCartao(Convert.ToByte(innerAtual.QtdDigitos));
                //    break;
            //}

            //Habilitar teclado
            EasyInner.HabilitarTeclado((byte)(innerAtual.Teclado ? Enumeradores.Opcao.SIM : Enumeradores.Opcao.NAO), 0);

            // define os valores para configurar os leitores de acordo com o tipo de inner
            DefineValoresParaConfigurarLeitores(innerAtual);
            EasyInner.ConfigurarLeitor1(innerAtual.ValorLeitor1);
            EasyInner.ConfigurarLeitor2(innerAtual.ValorLeitor2);

            //Box = Configura equipamentos com dois leitores
            if (innerAtual.DoisLeitores)
            {
                // exibe mensagens do segundo leitor
                EasyInner.ConfigurarWiegandDoisLeitores(0, (byte)Enumeradores.Opcao.SIM);
            }

            //Registra acesso negado
            EasyInner.RegistrarAcessoNegado(1);

            //Catraca
            //Define qual ser� o tipo do registro realizado pelo Inner ao aproximar um
            //cart�o do tipo proximidade no leitor do Inner, sem que o usu�rio tenha
            //pressionado a tecla entrada, sa�da ou fun��o.
            if ((0 == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_E_Saida) || (0 == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Liberada_2_Sentidos) || (0 == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Sentido_Giro))
            {
                EasyInner.DefinirFuncaoDefaultLeitoresProximidade(12); // 12 � Libera a catraca nos dois sentidos e registra o bilhete conforme o sentido giro.
            }
            else
            {
                if ((0 == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada) || (0 == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Saida_Liberada))
                {
                    //if (UiBIO.optDireita.Checked)
                    //{
                    //    EasyInner.DefinirFuncaoDefaultLeitoresProximidade(10);  // 10 � Registrar sempre como entrada.
                    //}
                    //else
                    //{
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(11);  // Inverte o sentido de entrada.
                    //}

                }
                else
                {
                    //if (UiBIO.optDireita.Checked)
                    //{
                        //EasyInner.DefinirFuncaoDefaultLeitoresProximidade(11);  // 10 � Registrar sempre como entrada.
                    //}
                    //else
                    //{
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(10);  // Inverte o sentido de entrada.
                    //}
                }
            }

            //Configura o tipo de registro que ser� associado a uma marca��o
            if (innerAtual.Biometrico)
            {
                EasyInner.DefinirFuncaoDefaultSensorBiometria(10);
            }
            else
            {
                EasyInner.DefinirFuncaoDefaultSensorBiometria(0);
            }

            if (innerAtual.QtdDigitos <= 14)
            {
                //Configura para receber o horario dos dados quando Online.
                EasyInner.ReceberDataHoraDadosOnLine(Convert.ToByte(Enumeradores.Opcao.SIM));
            }
        }

        #endregion

        #region InverterString
        private static string InverterString(string str) 
        { 
            int tamanho = str.Length; 
            char[] caracteres = new char[tamanho]; 
            
            for (int i = 0; i < tamanho; i++) 
            { 
                caracteres[i] = str[tamanho - 1 - i]; 
            } 

            return new string(caracteres);
        }
        #endregion

        #region BinarioParaDecimal
        private static int BinarioParaDecimal(string valorBinario)
        {
            int expoente = 0;
            int numero; 
            int soma = 0;
            string numeroInvertido = InverterString(valorBinario);

            for(int i = 0; i < numeroInvertido.Length; i++)
            {
                //pega d�gito por d�gito do n�mero digitado
                numero = Convert.ToInt32(numeroInvertido.Substring(i,1)); 
                //multiplica o d�gito por 2 elevado ao expoente, e armazena o resultado em soma
                soma += numero * (int)Math.Pow(2,expoente);
                // incrementa          
                expoente++;
            }
            return soma;
        }
        #endregion

        #region ConfiguraEntradasMudancaOnLine
        //***********************************************************************************
        //Define Mudan�as OnLine
        //Fun��o que configura BIT a BIT, Ver no manual Anexo III
        //***********************************************************************************
        private static int ConfiguraEntradasMudancaOnLine(Inner InnerAtual)
        {
            string Configuracao;

            //Habilita Teclado
            Configuracao = (InnerAtual.Teclado ? 1 : 0).ToString();

            if (!InnerAtual.Biometrico)
            {
                //Dois leitores
                if (InnerAtual.DoisLeitores)
                    Configuracao = "010" + //Leitor 2 s� saida
                                   "001" + //Leitor 1 s� entrada
                                   Configuracao;
                else //Apenas um leitores
                    Configuracao = "000" + //Leitor 2 Desativado
                                   "011" + //Leitor 1 configurado para Entrada e Sa�da
                                   Configuracao;

                Configuracao = "1" + // Habilitado
                               Configuracao;
            }
            else //Com Biometria 
            {
                Configuracao = "0" + //Bit Fixo
                               "1" + //Habilitado
                               InnerAtual.Identificacao + //Identifica��o
                               InnerAtual.Verificacao + //Verifica��o
                               "0" + //Bit fixo 
                               (InnerAtual.DoisLeitores ? "11" : "10") + // 11 -> habilita leitor 1 e 2, 10 -> habilita apenas leitor 1
                               Configuracao;
            }

            //Converte Bin�rio para Decimal
            return BinarioParaDecimal(Configuracao);

        }
        #endregion

        #region DefineVersao
        //***********************************************************************************
        //DEFINEVERSAO
        //Esta rotina � respons�vel por identificar a vers�o do inner
        //***********************************************************************************
        private static void DefineVersao(Inner InnerAtual, FrmOnline UiMainOnline)
        {
              byte Linha = 0;
              short Variacao = 0;
              byte VersaoAlta  = 0;
              byte VersaoBaixa = 0;
              byte VersaoSufixo = 0;
              byte Ruf = 0;
              int Modelo = 0;
              int VersaoAltaBio = 0;
              int VersaoBaixaBio = 0;

              //Solicita a vers�o do firmware do Inner e dados como o Idioma, se � uma vers�o especial.
              int Ret = EasyInner.ReceberVersaoFirmware(InnerAtual.Numero, ref Linha, ref Variacao, ref VersaoAlta, ref VersaoBaixa, ref VersaoSufixo, ref Ruf); 


              if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
              {
                    //Define a linha do Inner
                    switch (Linha)
                    {
                        case 1:
                            InnerAtual.LinhaInner = "Inner Plus";
                            break;

                        case 2:
                            InnerAtual.LinhaInner = "Inner Disk";
                            break;

                        case 3:
                            InnerAtual.LinhaInner = "Inner Verid";
                            break;

                        case 6:
                            InnerAtual.LinhaInner = "Inner Bio";
                            break;

                        case 7:
                            InnerAtual.LinhaInner = "Inner NET";
                            break;

                        case 14:
                            InnerAtual.LinhaInner = "Inner Acesso";
                            InnerNetAcesso = true;
                            break;
                    }

                  InnerAtual.VariacaoInner = Variacao;
                  InnerAtual.VersaoInner = VersaoAlta.ToString() + '.' + VersaoBaixa + '.' + VersaoSufixo;

                  //Se selecionado Biometria, valida se o equipamento � compat�vel
                  if (InnerAtual.Biometrico)
                  {
                      //if ((((Linha != 6) && (Linha != 14)) || ((Linha == 14) && (InnerAcessoBio == 0))))
                      //if ((((Linha != 6) && (Linha != 14)) || ((Linha == 14))))
                      //{
                        // MessageBox.Show("Equipamento " + InnerAtual.Numero + " n�o compat�vel com Biometria.", "Aten��o");
                      //}
                  }

                  //Se for biometria
                  if (InnerAtual.Biometrico)
                  {
                      //Solicita o modelo do Inner bio.
                      Ret = EasyInner.SolicitarModeloBio(InnerAtual.Numero);

                      do
                      {
                          Thread.Sleep(1);
                          
                          //Retorna o resultado do comando SolicitarModeloBio, o modelo
                          //do Inner Bio � retornado por refer�ncia no par�metro da fun��o.
                          Ret = EasyInner.ReceberModeloBio(InnerAtual.Numero, 0, ref Modelo);
                      }
                      while (Ret == 128);

                      //Define o modelo do Inner Bio
                      switch (Modelo)
                      {
                            case 1:
                                InnerAtual.ModeloBioInner = "Modelo do bio: Light 100 usu�rios FIM10";
                                break;
                            case 4:
                                InnerAtual.ModeloBioInner = "Modelo do bio: 1000/4000 usu�rios FIM01";
                                break;
                            case 51:
                                InnerAtual.ModeloBioInner = "Modelo do bio: 1000/4000 usu�rios FIM2030";
                                break;
                            case 52:
                                InnerAtual.ModeloBioInner = "Modelo do bio: 1000/4000 usu�rios FIM2040";
                                break;
                            case 48:
                                InnerAtual.ModeloBioInner = "Modelo do bio: Light 100 usu�rios FIM3030";
                                break;
                            case 64:
                                InnerAtual.ModeloBioInner = "Modelo do bio: Light 100 usu�rios FIM3040";
                                break;
                            case 80:
                                InnerAtual.ModeloBioInner = "Modelo do bio: 1000/4000 usu�rios FIM5060";
                                break;
                            case 82:
                                InnerAtual.ModeloBioInner = "Modelo do bio: 1000/4000 usu�rios FIM5260";
                                break;
                            case 83:
                                InnerAtual.ModeloBioInner = "Modelo do bio: Light 100 usu�rios FIM5360";
                                break;
                            case 255:
                                InnerAtual.ModeloBioInner = "Modelo do bio: Desconhecido";
                                break;
                      }

                      //Solicita a vers�o do Inner bio.
                      Ret = EasyInner.SolicitarVersaoBio(InnerAtual.Numero);

                      do
                      {
                          Thread.Sleep(1);

                          //Retorna o resultado do comando SolicitarVersaoBio, a vers�o
                          //do Inner Bio � retornado por refer�ncia nos par�metros da
                          //fun��o.
                          Ret = EasyInner.ReceberVersaoBio(InnerAtual.Numero, 0, ref VersaoAltaBio, ref VersaoBaixaBio);
                      }
                      while (Ret == 128);
                  }
                  InnerAtual.VersaoBio = VersaoAltaBio + "." + VersaoBaixaBio;
              }

            FrmOnlineController.AtualizaListaInnerOnline(UiMainOnline);
        }
        #endregion

        #region MontarHorarios
        //***********************************************************************************
        //MONTAR HORARIOS
        //Insere no buffer da dll um hor�rio de acesso. O Inner possui uma tabela de
        //100 hor�rios de acesso, para cada hor�rio � poss�vel definir 4 faixas de acesso
        //para cada dia da semana.
        //Tabela de horarios numero 1
        //***********************************************************************************
        private static void MontarHorarios(Inner InnerAtual)
        {
            EasyInner.ApagarHorariosAcesso(InnerAtual.Numero);
            
            //Insere no buffer da DLL horario de acesso
            //(Segunda - dia da semana = 1)
            EasyInner.InserirHorarioAcesso(1, 1, 1, 8, 0); //(1 - n� da tabela horario, 1 - dia da semana, 1 - faixa de horario, 8 - hora, 0 - minuto)
            EasyInner.InserirHorarioAcesso(1, 1, 2, 12, 0);
            EasyInner.InserirHorarioAcesso(1, 1, 3, 13, 0);
            EasyInner.InserirHorarioAcesso(1, 1, 4, 18, 0);

            //(Ter�a - dia da semana = 2)
            EasyInner.InserirHorarioAcesso(1, 2, 1, 8, 0);
            EasyInner.InserirHorarioAcesso(1, 2, 2, 12, 0);
            EasyInner.InserirHorarioAcesso(1, 2, 3, 13, 0);
            EasyInner.InserirHorarioAcesso(1, 2, 4, 18, 0);

            //(Quarta - dia da semana = 3)
            EasyInner.InserirHorarioAcesso(1, 3, 1, 8, 0);
            EasyInner.InserirHorarioAcesso(1, 3, 2, 12, 0);
            EasyInner.InserirHorarioAcesso(1, 3, 3, 13, 0);
            EasyInner.InserirHorarioAcesso(1, 3, 4, 18, 0);

            //(Quinta - dia da semana = 4)
            EasyInner.InserirHorarioAcesso(1, 4, 1, 8, 0);
            EasyInner.InserirHorarioAcesso(1, 4, 2, 12, 0);
            EasyInner.InserirHorarioAcesso(1, 4, 3, 13, 0);
            EasyInner.InserirHorarioAcesso(1, 4, 4, 18, 0);

            //(Sexta - dia da semana = 5
            EasyInner.InserirHorarioAcesso(1, 5, 1, 8, 0);
            EasyInner.InserirHorarioAcesso(1, 5, 2, 12, 0);
            EasyInner.InserirHorarioAcesso(1, 5, 3, 13, 0);
            EasyInner.InserirHorarioAcesso(1, 5, 4, 18, 0);

            //(Sabado - dia da semana = 6)
            //(Domingo/Feriado - dia da semana = 7)
            EasyInner.EnviarHorariosAcesso(InnerAtual.Numero);

        }
        #endregion

        #region MontarListaTopdata
        //***********************************************************************************
        //MONTAR LISTA TOPDATA
        //Monta o buffer para enviar a lista nos inners da linha Inner, cart�o padr�o Topdata
        //***********************************************************************************
        private static void MontarListaTopdata(Inner InnerAtual)
        {
            EasyInner.ApagarListaAcesso(InnerAtual.Numero);

            //Insere usuario da lista no buffer da DLL
            EasyInner.InserirUsuarioListaAcesso("100", 1);
            EasyInner.InserirUsuarioListaAcesso("1", 101);
            EasyInner.InserirUsuarioListaAcesso("2", 102);

            EasyInner.EnviarListaAcesso(InnerAtual.Numero);
        }
        #endregion
        
        #region MontarListaLivre
        //***********************************************************************************
        //MONTAR LISTA LIVRE
        //Monta o buffer para enviar a lista nos inners da linha Inner, cart�o padr�o livre 14 d�gitos
        //***********************************************************************************
        private static void MontarListaLivre(Inner InnerAtual)
        {
            EasyInner.ApagarListaAcesso(InnerAtual.Numero);

            //Insere usuario da lista no buffer da DLL
            EasyInner.InserirUsuarioListaAcesso("1", 1); //(1 - depende do padrao do cartao, 1 - n� do horario ja cadastrado)
            EasyInner.InserirUsuarioListaAcesso("2", 1);
            EasyInner.InserirUsuarioListaAcesso("3", 1);
            EasyInner.InserirUsuarioListaAcesso("100", 1);
            EasyInner.InserirUsuarioListaAcesso("100001", 1);
            EasyInner.InserirUsuarioListaAcesso("99999999999999", 101);
            EasyInner.InserirUsuarioListaAcesso("1234", 102);
            EasyInner.InserirUsuarioListaAcesso("1000", 101);
            EasyInner.InserirUsuarioListaAcesso("666", 101);
            EasyInner.InserirUsuarioListaAcesso("999", 101);
            EasyInner.InserirUsuarioListaAcesso("00000000000011", 102);
            
            EasyInner.EnviarListaAcesso(InnerAtual.Numero);
        }
        #endregion

        #region MontarBufferListaSemDigital
        //***********************************************************************************
        //Monta o buffer usu�rios sem digital
        //***********************************************************************************
        private static void MontarBufferListaSemDigital(Inner InnerAtual)
        {
            EasyInner.IncluirUsuarioSemDigitalBio("999");
            EasyInner.IncluirUsuarioSemDigitalBio("1000");
            EasyInner.IncluirUsuarioSemDigitalBio("1");
            EasyInner.IncluirUsuarioSemDigitalBio("666");
            EasyInner.IncluirUsuarioSemDigitalBio("3007");
        }
        #endregion

        #endregion

        #region Maquina de Estados

        #region MaquinaOnline
        //***********************************************************************************
        //FUNCIONAMENTO DA M�QUINA DE ESTADOS
        //M�TODO RESPONS�VEL EM EXECUTAR OS PROCEDIMENTOS DO MODO ONLINE
        //A M�quina de Estados nada mais � do que uma rotina que fica em loop testando
        //uma vari�vel que chamamos de Estado. Dependendo do estado atual, executamos
        //alguns procedimentos e em seguida alteramos o estado que ser� verificado pela
        //m�quina de estados novamente no pr�ximo passo do loop.
        //***********************************************************************************
        private static void MaquinaOnline(FrmOnline UiMainOnline)
        {
            try
            {
                //Inicializa Vari�veis
                UiBIO = UiMainOnline;
                int Ret2 = -1;

                //Testa se existe Inners em mem�ria para ativar a maquina de estados..
                if (UiMainOnline.LstInners.Count < 1)
                {
                    MessageBox.Show("N�o existem Inners cadastrados em mem�ria.", "Aten��o");
                    
                    //UiMainOnline.btnAdicionarUsuarioInnerOnline.Enabled = true;
                    //UiMainOnline.btnRemoverInnerLista.Enabled = true;
                    return;
                }

                //Define o tipo de conex�o conforme selecionado em Combo (padr�o Porta Fixa)
                EasyInner.DefinirTipoConexao(2);

                //Fecha as conex�es caso esteja aberta..
                EasyInner.FecharPortaComunicacao();

                //Abre a porta de comunica��o com o Inner..
                Ret2 = EasyInner.AbrirPortaComunicacao(3570);

                var query = from r in UiMainOnline.LstInners
                            where r.Biometrico == true
                            //orderby r.EstadoAtual descending
                            select r;

                //Tenta realizar a conex�o com o Inner, caso tenha sucesso envia configura��es..
                if (Ret2 == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Enquanto a vari�vel Estiver Selecionada para prosseguir a maquina, executa o processo..
                    while (UiMainOnline.Ativa)
                    {
                        //Para cada inner da Lista de Inners cadastrados na UI.
                        foreach (Inner InnerAtual in UiMainOnline.LstInners)
                        {
                            //Verifica o Estado do Inner Atual..
                            switch (InnerAtual.EstadoAtual)
                            {
                                case Enumeradores.EstadosInner.ESTADO_CONECTAR:
                                    PASSO_ESTADO_CONECTAR(UiMainOnline, InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_CFG_OFFLINE:
                                    PASSO_ESTADO_ENVIAR_CFG_OFFLINE(UiMainOnline, InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_COLETAR_BILHETES:
                                    PASSO_ESTADO_COLETAR_BILHETES(UiMainOnline, InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_CFG_ONLINE:
                                    PASSO_ESTADO_ENVIAR_CFG_ONLINE(UiMainOnline, InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_DATA_HORA:
                                    PASSO_ESTADO_ENVIAR_DATA_HORA(UiMainOnline, InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_PADRAO:
                                    PASSO_ENVIAR_MENSAGEM_PADRAO(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_CONFIGURAR_ENTRADAS_ONLINE:
                                    PASSO_ESTADO_CONFIGURAR_ENTRADAS_ONLINE(UiMainOnline, InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_POLLING:
                                    PASSO_ESTADO_POLLING(UiMainOnline, InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA:
                                    PASSO_LIBERA_GIRO_CATRACA(UiMainOnline, InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_MONITORA_GIRO_CATRACA:
                                    PASSO_MONITORA_GIRO_CATRACA(UiMainOnline, InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.PING_ONLINE:
                                    PASSO_ESTADO_ENVIA_PING_ONLINE(UiMainOnline, InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_RECONECTAR:
                                    PASSO_ESTADO_RECONECTAR(UiMainOnline, InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.AGUARDA_TEMPO_MENSAGEM:
                                    PASSO_AGUARDA_TEMPO_MENSAGEM(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_DEFINICAO_TECLADO:
                                    PASSO_ESTADO_DEFINICAO_TECLADO(UiMainOnline, InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_AGUARDA_DEFINICAO_TECLADO:
                                    PASSO_ESTADO_AGUARDA_DEFINICAO_TECLADO(UiMainOnline, InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIA_MSG_URNA:
                                    PASSO_ESTADO_ENVIA_MSG_URNA(InnerAtual);
                                    break;
                                case Enumeradores.EstadosInner.ESTADO_MONITORA_URNA:
                                    PASSO_ESTADO_MONITORA_URNA(UiMainOnline, InnerAtual);
                                    break;
                                
                                #endregion
                            }
                            if (InnerAtual.CntDoEvents++ > 10)
                            {
                                InnerAtual.CntDoEvents = 0;
                                Application.DoEvents();
                                System.Threading.Thread.Sleep((int)1 / UiMainOnline.LstInners.Count);
                            }

                        }
                    }
                    Thread.EndCriticalRegion();
                }
                else
                {
                    MessageBox.Show("Erro ao tentar abrir a porta de comunica��o.", "Aten��o");
                }
                //Fecha Porta de Comunica��o.
                UiMainOnline.FechouMaquina = true;
                EasyInner.FecharPortaComunicacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }
        }

        #region Passos Da Maquina de Estados

        #region PASSO_ESTADO_CONECTAR
        //***********************************************************************************
        //CONECTAR
        //Inicia a conex�o com o Inner
        //Pr�ximo passo: ESTADO_ENVIAR_CFG_OFFLINE
        //***********************************************************************************
        private static void PASSO_ESTADO_CONECTAR(FrmOnline UiMainOnline, Inner InnerAtual)
        {
            try
            {
                //Seta tempo inicial ping online
                InnerAtual.TempoInicialPingOnLine = DateTime.Now;

                //Mensagem Status
                UiMainOnline.lblStatus.Text = "Inner " + InnerAtual.Numero + " Conectando...";

                //Caso o Inner esteja Realizando Ping, vai para o passo de Configura��o.
                if (EasyInner.Ping(InnerAtual.Numero) == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    System.DateTime Data;
                    Data = System.DateTime.Now;
                    
                    //Testa a conex�o, tenta enviar um rel�gio para o Inner.
                    int retorno = EasyInner.EnviarRelogio(InnerAtual.Numero,
                                                                (byte)Data.Day,
                                                                (byte)Data.Month,
                                                                System.Convert.ToByte(Data.Year.ToString().Substring(2, 2)),
                                                                (byte)Data.Hour,
                                                                (byte)Data.Minute,
                                                                (byte)Data.Second);
                    
                    if (retorno == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        //caso consiga o Inner vai para o Passo de Configura��o OFFLINE, posteriormente para coleta de Bilhetes.
                        InnerAtual.CountTentativasEnvioComando = 0;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_CFG_OFFLINE;

                        UiMainOnline.btnPararMaquina.Enabled = true;

                    }
                    else
                    {
                        //caso ele n�o consiga, tentar� enviar tr�s vezes, se n�o conseguir volta para o passo Reconectar
                        if (InnerAtual.CountTentativasEnvioComando >= 3)
                        {
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                        }
                        
                        //Adiciona 1 contador de tentativas
                        InnerAtual.CountTentativasEnvioComando++;
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        #endregion

        #region ESTADO_ENVIAR_CFG_OFFLINE
        //***********************************************************************************
        //CFG_OFFLINE
        //Configura modo Offline
        //Pr�ximo passo: ESTADO_COLETAR_BILHETES
        //***********************************************************************************
        private static void PASSO_ESTADO_ENVIAR_CFG_OFFLINE(FrmOnline UiMainOnline, Inner InnerAtual)
        {
            int ret = 0;
            try
            {
                #region Realiza as configura��es Offline do Inner Atual.

                //Preenche os campos de configura��o do Inner
                MontaConfiguracaoInner(InnerAtual, Enumeradores.modoComunicacao.MODO_OFF_LINE);
                
                //Mensagem Entrada e Saida Offline Liberado!
                EasyInner.DefinirMensagemEntradaOffLine(1, "Entrada liberada.");
                EasyInner.DefinirMensagemSaidaOffLine(1, "Saida liberada.");                
                EasyInner.DefinirMensagemPadraoOffLine(1, "Modo OffLine");

                //Envia mensagens definidas
                EasyInner.EnviarMensagensOffLine(InnerAtual.Numero);

                //Define Lista e hor�rios offline
                if (InnerAtual.Lista)
                {
                    MontarHorarios(InnerAtual);

                    ////Define a Lista de verifica��o
                    //if (UiBIO.cboPadraoCartaoOnline.SelectedIndex == 0)
                    //{
                    //    MontarListaTopdata(InnerAtual);
                    //}
                    //else
                    //{
                        MontarListaLivre(InnerAtual);
                    //}

                    //Define qual tipo de lista(controle) de acesso o Inner vai utilizar.
                    //Utilizar lista branca (cart�es fora da lista tem o acesso negado).
                    EasyInner.DefinirTipoListaAcesso(1);
                }
                else
                {
                    //N�o utilizar a lista de acesso.
                    EasyInner.DefinirTipoListaAcesso(0);
                }

                if (InnerAtual.ListaBio){
                    Application.DoEvents();
                    //Chama rotina que monta o buffer de cartoes que nao irao precisar da digital
                    MontarBufferListaSemDigital(InnerAtual);
                    //Envia o buffer com a lista de usuarios sem digital
                    EasyInner.EnviarListaUsuariosSemDigitalBio(InnerAtual.Numero);
                }

                do
                {
                    //Envia o comando de configura��o
                    ret = EasyInner.EnviarConfiguracoes(InnerAtual.Numero);
                    InnerAtual.CountTentativasEnvioComando++;
                    Application.DoEvents();
                    Thread.Sleep(200);
                } while ((ret != (int)Enumeradores.Retorno.RET_COMANDO_OK) && (InnerAtual.CountTentativasEnvioComando < 300));

                //Configura a mudan�a autom�tica
                //Habilita/Desabilita a mudan�a autom�tica do modo OffLine do Inner para
                //OnLine e vice-versa.
                //Habilita a mudan�a Offline
                string TipoComunicacao = "2";
                int Posicao = TipoComunicacao.IndexOf("TCP");
                if (Posicao != -1)
                {
                    //Habilita a mudan�a Offline
                    EasyInner.HabilitarMudancaOnLineOffLine(2, 10);
                }
                else
                {
                    //Habilita a mudan�a Offline
                    EasyInner.HabilitarMudancaOnLineOffLine(1, 10);
                }

                //Configura o teclado para quando o Inner voltar para OnLine ap�s uma queda
                //para OffLine.
                EasyInner.DefinirConfiguracaoTecladoOnLine(6, 0, 5, 17);

                //Define Mudan�as OnLine
                //Fun��o que configura BIT a BIT, Ver no manual Anexo III
                EasyInner.DefinirEntradasMudancaOnLine((byte)ConfiguraEntradasMudancaOnLine(InnerAtual));

                if (InnerAtual.Biometrico)
                {
                    // Configura entradas mudan�a OffLine com Biometria 
                    EasyInner.DefinirEntradasMudancaOffLineComBiometria((byte)(InnerAtual.Teclado ? Enumeradores.Opcao.SIM : Enumeradores.Opcao.NAO),
                                                                        3,(byte)(InnerAtual.DoisLeitores ? 3 : 0),InnerAtual.Verificacao,InnerAtual.Identificacao);
                }
                else
                {
                    // Configura entradas mudan�a OffLine               
                    EasyInner.DefinirEntradasMudancaOffLine((byte)(InnerAtual.Teclado ? Enumeradores.Opcao.SIM : Enumeradores.Opcao.NAO),
                        (byte)(InnerAtual.DoisLeitores ? 1 : 3),
                        (byte)(InnerAtual.DoisLeitores ? 2 : 0),
                        0);
                }

                //Define mensagem de Altera��o Online -> Offline.
                EasyInner.DefinirMensagemPadraoMudancaOffLine(1, " Modo OffLine");

                //Define mensagem de Altera��o OffLine -> OnLine.
                EasyInner.DefinirMensagemPadraoMudancaOnLine(1, "Modo Online");

                //Envia Configura��es.
                EasyInner.EnviarConfiguracoesMudancaAutomaticaOnLineOffLine(InnerAtual.Numero);
                
		        //Testa o retorno do envio das configura��es Off Line
                if (ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Define o modelo do equipamento
                    DefineVersao(InnerAtual, UiMainOnline);

                    //Zera contador
                    InnerAtual.CountTentativasEnvioComando = 0;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_COLETAR_BILHETES;
                    
                    //Adiciona 3 segundos tempo de coleta
                    InnerAtual.TempoColeta = (int)System.DateTime.Now.Second + 3;
                }
                else
                {
		            //caso ele n�o consiga, tentar� enviar tr�s vezes, se n�o conseguir volta para o passo Reconectar
                    if (InnerAtual.CountTentativasEnvioComando >= 3)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 contador de tentativas
                    InnerAtual.CountTentativasEnvioComando++;
                }

                #endregion
            }
            catch (Exception ex)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        #endregion

        #region PASSO_AGUARDA_TEMPO_MENSAGEM
        //***********************************************************************************
        //TEMPO_MENSAGEM
        //Mant�m a mensagem no display por 2 segundos.
        //Pr�ximo passo: ESTADO_ENVIAR_MSG_PADRAO
        //***********************************************************************************
        private static void PASSO_AGUARDA_TEMPO_MENSAGEM(Inner innerAtual)
        {
            try
            {
                //Ap�s passar os 2 segundos volta para o passo enviar mensagem padr�o
                TimeSpan tempo = DateTime.Now - innerAtual.TempoInicialMensagem;
                if (tempo.Seconds >= 2)
                {
                    innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_PADRAO;
                }
            }
            catch (Exception ex)
            {
                innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        #endregion

        #region PASSO_ESTADO_COLETAR_BILHETES
        //***********************************************************************************
        //COLETAR_BILHETES
        //Efetua a coleta dos bilhetes no modo Off-line
        //Pr�ximo passo: ESTADO_ENVIAR_CFG_ONLINE
        //***********************************************************************************
        private static void PASSO_ESTADO_COLETAR_BILHETES(FrmOnline UiMainOnline, Inner InnerAtual)
        {
            ColetarBilhetes(UiMainOnline, InnerAtual);
        }
        #endregion

        #region ColetarBilhetes
        //***********************************************************************************
        //COLETAR_BILHETES
        //Efetua a coleta dos bilhetes no modo Off-line
        //Pr�ximo passo: ESTADO_ENVIAR_CFG_ONLINE
        //***********************************************************************************
        //private static void ColetarBilhetesInnerAcesso(FrmOnline UiMainOnline, Inner InnerAtual)
        //{
        //    try
        //    {
        //        //Exibe no rodap� da janela o estado da maquina.
        //        UiMainOnline.lblStatus.Text = "Inner " + InnerAtual.Numero + " Coletando Bilhetes...";

        //        //Declara��o de Vari�veis...
        //        StringBuilder Cartao = new StringBuilder();
        //        byte Dia = 0;
        //        byte Mes = 0;
        //        byte Ano = 0;
        //        byte Hora = 0;
        //        byte Minuto = 0;
        //        string strCartao = string.Empty;
        //        int Ret = -1;
        //        int Count = 0;
        //        byte Tipo = 0;
        //        int tamCartao = 0;

        //        int nBilhetes; 
        //        int QtdeBilhetes;
        //        int[] receber = new int[2];

        //        Thread.BeginCriticalRegion();

        //        //Envia o Comando Receber Dados Online..

        //        //Zera a contagem de Ping Online.
        //        InnerAtual.CntDoEvents = 0;
        //        InnerAtual.CountPingFail = 0;
        //        InnerAtual.CountRepeatPingOnline = 0;

        //        nBilhetes = 0;
        //        QtdeBilhetes = 0;
        //        Ret = EasyInner.ReceberQuantidadeBilhetes(InnerAtual.Numero, receber);
        //        QtdeBilhetes = receber[0];

        //        do
        //        {
        //         if (QtdeBilhetes > 0)
        //         {
        //          do
        //          {
        //            //Envia o Comando de Coleta de Bilhetes..
        //            Ret = EasyInner.ColetarBilhete(InnerAtual.Numero, ref Tipo, ref Dia, ref Mes, ref Ano, ref Hora, ref Minuto, Cartao);

        //            //Caso exista bilhete a coletar..
        //            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
        //            {
        //                strCartao = "";

        //                //Recebe hora atual para inicio do PingOnline
        //                InnerAtual.TempoInicialPingOnLine = DateTime.Now;

        //                // Antes de realizar a configura��o precisa definir o Padr�o do cart�o 
        //                if (UiBIO.cboPadraoCartaoOnline.SelectedIndex == 0)
        //                {
        //                    tamCartao = Cartao.Length;
        //                }
        //                else
        //                {
        //                    tamCartao = Cartao.Length - 1;
        //                }

        //                //Atribui o nro do Cart�o..
        //                for (Count = 0; Count < tamCartao; Count++)
        //                {
        //                    strCartao += System.Convert.ToString(System.Convert.ToChar(Cartao[Count]));
        //                }

        //                //Adiciona a lista de bilhetes o N�mero bilhete coletado..
        //                UiMainOnline.lstBilhetes.Items.Add("Marca��es Offline. Inner:" +
        //                    InnerAtual.Numero + "  Tipo:" +
        //                    Tipo + "  Cart�o:" +
        //                    strCartao + "  Data:" +
        //                    Dia.ToString("00") + "/" +
        //                    Mes.ToString("00") + "/" +
        //                    Ano + "  Hora:" +
        //                    Hora.ToString("00") + ":" +
        //                    Minuto.ToString("00"));

        //                    nBilhetes++;
        //                    QtdeBilhetes--;
        //                 }

        //            } while (QtdeBilhetes > 0);

        //            Ret = EasyInner.ReceberQuantidadeBilhetes(InnerAtual.Numero, receber);
        //            QtdeBilhetes = receber[0];
        //        }
        //    } while (QtdeBilhetes > 0);

        //    Cartao = null;
        //    } catch (Exception ex) {
        //            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
        //    }

        //    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_CFG_ONLINE;
        //}
        #endregion

        #region ColetarBilhetes
        //***********************************************************************************
        //COLETAR_BILHETES
        //Efetua a coleta dos bilhetes no modo Off-line
        //Pr�ximo passo: ESTADO_ENVIAR_CFG_ONLINE
        //***********************************************************************************
        private static void ColetarBilhetes(FrmOnline UiMainOnline, Inner InnerAtual)
        {
            try
            {
                //Exibe no rodap� da janela o estado da maquina.
                UiMainOnline.lblStatus.Text = "Inner " + InnerAtual.Numero + " Coletando Bilhetes...";

                //Declara��o de Vari�veis...
                StringBuilder Cartao = new StringBuilder();
                byte Dia = 0;
                byte Mes = 0;
                byte Ano = 0;
                byte Hora = 0;
                byte Minuto = 0;
                string strCartao = string.Empty;
                int Ret = -1;
                int Count = 0;
                byte Tipo = 0;
                int tamCartao = 0;

                Thread.BeginCriticalRegion();

                //Envia o Comando Receber Dados Online..

                //Zera a contagem de Ping Online.
                InnerAtual.CntDoEvents = 0;
                InnerAtual.CountPingFail = 0;
                InnerAtual.CountRepeatPingOnline = 0;

                //Envia o Comando de Coleta de Bilhetes..
                Ret = EasyInner.ColetarBilhete(InnerAtual.Numero, ref Tipo, ref Dia, ref Mes, ref Ano, ref Hora, ref Minuto, Cartao);

                //Caso exista bilhete a coletar..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    strCartao = "";

                    //Recebe hora atual para inicio do PingOnline
                    InnerAtual.TempoInicialPingOnLine = DateTime.Now;

                    //// Antes de realizar a configura��o precisa definir o Padr�o do cart�o 
                    //if (UiBIO.cboPadraoCartaoOnline.SelectedIndex == 0)
                    //{
                    //    tamCartao = Cartao.Length;
                    //}
                    //else
                    //{
                        tamCartao = Cartao.Length - 1;
                    //}

                    //Atribui o nro do Cart�o..
                    for (Count = 0; Count < tamCartao; Count++)
                    {
                        strCartao += System.Convert.ToString(System.Convert.ToChar(Cartao[Count]));
                    }

                    //Adiciona a lista de bilhetes o N�mero bilhete coletado..
                    UiMainOnline.lstBilhetes.Items.Add("Marca��es Offline. Inner:" +
                        InnerAtual.Numero + "  Tipo:" +
                        Tipo + "  Cart�o:" +
                        strCartao + "  Data:" +
                        Dia.ToString("00") + "/" +
                        Mes.ToString("00") + "/" +
                        Ano + "  Hora:" +
                        Hora.ToString("00") + ":" +
                        Minuto.ToString("00"));

                    //Adiciona 3 segundos tempo de coleta
                    InnerAtual.TempoColeta = (int)System.DateTime.Now.Second + 3;
                    InnerAtual.CountPingFail = 0;
                }
                else
                {
                    if ((int)System.DateTime.Now.Second >= (InnerAtual.TempoColeta % 60))
                    {
                        //Zera contador de tentativas
                        InnerAtual.CountTentativasEnvioComando = 0;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_CFG_ONLINE;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        #endregion

        //***********************************************************************************
        //MaximoNumeroTentativas
        //Verifica se a quantidade maxima de tentativas de um envio de comando
        //Ocorreu, caso tenha ocorrido retorna TRUE, sen�o FALSE..
        //***********************************************************************************

        #region MaximoNumeroTentativas
        private static Boolean MaximoNumeroTentativas()
        {
            //Incrementa o n�mero de tentativas..
            intTentativas = intTentativas + 1;

            //Verifica se o n�mero de tentativas � maior do que 3..
            //MAXIMO_TENTATIVAS_COMUNICACAO
            if (intTentativas > 3)
            {
                return true; //Retorna TRUE
            }
            else
            {
                return false; //Retorna FALSE
            }
        }
        #endregion

        #region HABILITA_LADO_CATRACA
        //***********************************************************************************
        //HABILITA_LADO_CATRACA
        //De acordo com o que foi informado (Esquerda ou Direita)
        //***********************************************************************************
        public static void HABILITA_LADO_CATRACA(FrmOnline UiMainOnline, string lado)
        {
            if (lado == "Entrada")
            {            
                LiberaEntrada = false;
                LiberaEntradaInvertida = true;               
            }

            if (lado == "Saida")
            {
                LiberaSaida = false;
                LiberaSaidaInvertida = true;               
            }

            if (lado == "dois")
            {
                ////sa�da
                //if (UiMainOnline.optDireita.Checked)
                //{
                //    LiberaSaida = true;
                //    LiberaSaidaInvertida = false;
                //}
                //else
                //{
                LiberaEntrada = false;
                LiberaEntradaInvertida = false;
                LiberaSaidaInvertida = false;
                LiberaSaida = false;
                //}
            }
        }
        #endregion

        //------ PASSO ----

        #region PASSO_ESTADO_ENVIAR_CFG_ONLINE
        //***********************************************************************************
        //CFG_ONLINE
        //Configura modo On-line
        //Pr�ximo passo: ESTADO_ENVIAR_DATA_HORA
        //***********************************************************************************
        private static void PASSO_ESTADO_ENVIAR_CFG_ONLINE(FrmOnline UiMainOnline, Inner InnerAtual)
        {
            try
            {
                #region Realiza as configura��es Online do Inner atual.
                Thread.BeginCriticalRegion();

                //Monta configura��o modo Online
                MontaConfiguracaoInner(InnerAtual, Enumeradores.modoComunicacao.MODO_ON_LINE);

                //Envia as configura��es ao Inner Atual.
                int ret = EasyInner.EnviarConfiguracoes(InnerAtual.Numero);
                if (ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Caso consiga enviar as configura��es, passa para o passo Enviar Data Hora
                    InnerAtual.CountTentativasEnvioComando = 0;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_DATA_HORA;
                    Thread.EndCriticalRegion();
                }
                else
                {
                    //caso ele n�o consiga, tentar� enviar tr�s vezes, se n�o conseguir volta para o passo Reconectar
                    if (InnerAtual.CountTentativasEnvioComando >= 3)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 ao contador de tentativas
                    InnerAtual.CountTentativasEnvioComando++;
                }

                
                

                #endregion
            }
            catch (Exception ex)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }

        }
        #endregion        

        #region ESTADO_ENVIAR_DATA_HORA
        //***********************************************************************************
        //DATA_HORA
        //Envia ao Inner data e hora atual
        //Pr�ximo passo: ESTADO_ENVIAR_MSG_PADRAO
        //***********************************************************************************
        private static void PASSO_ESTADO_ENVIAR_DATA_HORA(FrmOnline UiMainOnline, Inner InnerAtual)
        {
            try
            {
                //Exibe estado do Inner no Rodap� da Janela
                UiMainOnline.lblStatus.Text = "Inner " + InnerAtual.Numero + " Enviando data e hora...";

                //Declara��o de Vari�veis..
                int Ret = -1;
                System.DateTime Data;
                Data = System.DateTime.Now;

                //Envia Comando de Rel�gio ao Inner Atual..
                Ret = EasyInner.EnviarRelogio(
                    InnerAtual.Numero,
                    (byte)Data.Day,
                    (byte)Data.Month,
                    System.Convert.ToByte(Data.Year.ToString().Substring(2, 2)),
                    (byte)Data.Hour,
                    (byte)Data.Minute,
                    (byte)Data.Second);

                //Testa o Retorno do comando de Envio de Rel�gio..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Vai para o passo de Envio de Msg Padr�o..
                    InnerAtual.CountTentativasEnvioComando = 0;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_PADRAO;
                }
                else
                {
                    //caso ele n�o consiga, tentar� enviar tr�s vezes, se n�o conseguir volta para o passo Reconectar
                    if (InnerAtual.CountTentativasEnvioComando >= 3)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 ao contador de tentativas
                    InnerAtual.CountTentativasEnvioComando++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        #endregion

        //------ PASSO ----

        #region PASSO_ESTADO_CONFIGURAR_ENTRADAS_ONLINE
        //***********************************************************************************
        //CONFIGURAR_ENTRADAS_ONLINE
        //Prepara��o configura��o online para entrar em modo Polling
        //Pr�ximo passo: ESTADO_POLLING
        //***********************************************************************************
        private static void PASSO_ESTADO_CONFIGURAR_ENTRADAS_ONLINE(FrmOnline UiMainOnline, Inner InnerAtual)
        {
            try
            {
                //Exibe estado do Inner no Rodap� da Janela
                UiMainOnline.lblStatus.Text = "Inner " + InnerAtual.Numero + " Configurando Entradas Online...";

                //Declara��o de vari�veis..
                int Ret = -1;

                //Converte Bin�rio para Decimal
                int ValorDecimal = ConfiguraEntradasMudancaOnLine(InnerAtual); //Ver no manual Anexo III

                Ret = EasyInner.EnviarFormasEntradasOnLine(InnerAtual.Numero,      //Numero do Inner..
                                                           (byte)InnerAtual.QtdDigitos,  //Qtd Digitos Teclado..
                                                           1,                      //Eco do Teclado no Display..
                                                           (byte)ValorDecimal,     //Valor decimal resultante da convers�o Bin�rio para Decimal
                                                           15,                     //Tempo teclado..
                                                           17);                    //Posi��o do Cursor no Teclado..

 
                //Testa o retorno do comando..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Vai para o Estado De Polling.
                    InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                    InnerAtual.CountTentativasEnvioComando = 0;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_POLLING;

                    if (InnerAtual.Catraca)
                    {
                        UiMainOnline.cmdEntrada.Text = "Entrada";
                        UiMainOnline.cmdSair.Text = "Sa�da";
                        UiMainOnline.cmdEntrada.Enabled = true;
                        UiMainOnline.cmdSair.Enabled = true;
                    }
                    else
                    {
                        UiMainOnline.cmdEntrada.Text = "Porta 1";
                        UiMainOnline.cmdSair.Text = "Porta 2";
                        UiMainOnline.cmdEntrada.Enabled = true;
                        UiMainOnline.cmdSair.Enabled = true;
                    }
                }
                else
                {
                    //caso ele n�o consiga, tentar� enviar tr�s vezes, se n�o conseguir volta para o passo Reconectar
                    if (InnerAtual.CountTentativasEnvioComando >= 3)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 ao contador de tentativas
                    InnerAtual.CountTentativasEnvioComando++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        #endregion

        //***********************************************************************************
        //DEFINICAO_TECLADO
        //Mostra mensagem para que seja informado se � entrada ou sa�da
        //Este estado configura a mensagem padr�o que ser� exibida no dispositivo em seu
        //funcionamento Online utilizando o m�todo EnviarMensagemPadraoOnline.
        //O passo posterior a este estado � o passo de configura��o de entradas online,
        //ou em caso de erro pode retornar para o estado de conex�o ap�s alcan�ar o
        //n�mero m�ximo de tentativas.
        //Pr�ximo passo: ESTADO_POLLING
        //******************************************************************************
        #region PASSO_ESTADO_DEFINICAO_TECLADO
        private static void PASSO_ESTADO_DEFINICAO_TECLADO(FrmOnline UiMainOnline, Inner InnerAtual)
        {
           int Ret = -1;
            
           //Envia mensagem Padr�o Online..
           Ret = EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "ENTRADA OU SAIDA?");
           Ret = EasyInner.EnviarFormasEntradasOnLine(InnerAtual.Numero,
                        0, //Quantidade de Digitos do Teclado.. (N�o aceita digita��o num�rica)
                        0, //0 � n�o ecoa
                        (int)Enumeradores.EnviarFormasEntradasOnLine.EntradasON_ACEITA_TECLADO,
                        10, // Tempo de entrada do Teclado (10s).
                        32);//Posi��o do Cursor (32 fica fora..)

           //Se Retorno OK, vai para proximo estado..
           if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
           {
             intTentativas = 0;
             InnerAtual.EstadoTeclado = Enumeradores.EstadosTeclado.AGUARDANDO_TECLADO;
             InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_AGUARDA_DEFINICAO_TECLADO;
           }
           else
           {
             //Caso o retorno n�o for OK, tenta novamente at� 3x..
             if (MaximoNumeroTentativas() == true)
             {
                 InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
             }
           }
        }
        #endregion
        
        //***********************************************************************************
        //PASSO_ESTADO_AGUARDAR_DEFINICAO_TECLADO
        //Aguarda a resposta do teclado (Entrada, Saida, anula ou confirma)
        //Proximo estado: ESTADO_POLLING
        //***********************************************************************************
        #region PASSO_ESTADO_AGUARDA_DEFINICAO_TECLADO
        private static void PASSO_ESTADO_AGUARDA_DEFINICAO_TECLADO(FrmOnline UiMainOnline, Inner InnerAtual)
        {
            byte Origem = 0;
            byte Complemento = 0;
            StringBuilder Cartao = new StringBuilder();
            byte Dia = 0;
            byte Mes = 0;
            byte Ano = 0;
            byte Hora = 0;
            byte Minuto = 0;
            byte Segundo = 0;
            int Ret = -1;

            //Exibe estado do Inner no Rodap� da Janela
            UiMainOnline.lblStatus.Text = "Inner " + InnerAtual.Numero + " Estado Aguarda Defini��o teclado...";

            //Atribui Temporizador
            InnerAtual.Temporizador = DateTime.Now;

            try
            {
                Thread.BeginCriticalRegion();

                //Envia o Comando Receber Dados Online..
                Ret = EasyInner.ReceberDadosOnLine(InnerAtual.Numero,
                    ref Origem, ref Complemento, Cartao, ref Dia, ref Mes, ref Ano, ref Hora,
                    ref Minuto, ref Segundo);
                
                //Testa o Retorno do Comando..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Se est� aguardando retorno (entrada ou sa�da)
                    if (InnerAtual.EstadoTeclado == Enumeradores.EstadosTeclado.AGUARDANDO_TECLADO)
                    {
                        //****************************************************
                        //Entrada, sa�da liberada, confirma, anula ou fun��o tratar mensagem
                        //66 - "Entrada" via teclado
                        //67 - "Sa�da" via teclado
                        //35 - "Confirma" via teclado
                        //42 - "Anula" via teclado
                        //65 - "Fun��o" via teclado
                        if (Convert.ToInt16(Complemento.ToString()) == (int)Enumeradores.Origem.TECLA_ENTRADA) //entrada
                        {
                            EasyInner.AcionarBipCurto(InnerAtual.Numero);
                            HABILITA_LADO_CATRACA(UiMainOnline, "Entrada");
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                        }
                        else if (Convert.ToInt16(Complemento.ToString()) == (int)Enumeradores.Origem.TECLA_SAIDA)   //sa�da
                        {
                            EasyInner.AcionarBipCurto(InnerAtual.Numero);
                            HABILITA_LADO_CATRACA(UiMainOnline, "Saida");
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                        }
                        else if (Convert.ToInt16(Complemento.ToString()) == (int)Enumeradores.Origem.TECLA_CONFIRMA) //confirma
                        {
                            EasyInner.AcionarBipCurto(InnerAtual.Numero);
                            Ret = EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "LIBERADO DOIS   SENTIDOS.");
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                        }
                        else if (Convert.ToInt16(Complemento.ToString()) == (int)Enumeradores.Origem.TECLA_ANULA) //anula
                        {
                            //Desliga Led Verde
                            EasyInner.LigarBackLite(InnerAtual.Numero);
                            InnerAtual.TempoInicialMensagem = DateTime.Now;
                            InnerAtual.CountTentativasEnvioComando = 0;
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_PADRAO;
                        }
                        else if (Convert.ToInt16(Complemento.ToString()) == (int)Enumeradores.Origem.TECLA_FUNCAO) //fun��o
                        {
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_DEFINICAO_TECLADO;
                        }
                        InnerAtual.EstadoTeclado = Enumeradores.EstadosTeclado.TECLADO_EM_BRANCO;
                    }
                }
                else
                {
                    //Se passar 3 segundos sem receber nada, passa para o estado enviar ping on line, para manter o equipamento em on line.
                    TimeSpan tempo = DateTime.Now - InnerAtual.TempoInicialPingOnLine;
                    if (tempo.Seconds >= 3)
                    {
                        InnerAtual.EstadoSolicitacaoPingOnLine = InnerAtual.EstadoAtual;
                        InnerAtual.CountTentativasEnvioComando = 0;
                        InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.PING_ONLINE;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        #endregion

        // Polling !!!

        #region PASSO_ESTADO_POLLING
        //***********************************************************************************
        //POLLING
        //� onde funciona todo o processo do modo online
        //Passagem de cart�o, catraca, urna, mensagens...
        //***********************************************************************************
        private static void PASSO_ESTADO_POLLING(FrmOnline UiMainOnline, Inner InnerAtual)
        {
            try
            {
                //Exibe estado do Inner no Rodap� da Janela
                UiMainOnline.lblStatus.Text = "Inner " + InnerAtual.Numero + " Estado de Polling...";

                //Declara��o de Vari�veis..
                byte Origem = 0;
                byte Complemento = 0;
                StringBuilder Cartao = new StringBuilder();
                byte Dia = 0;
                byte Mes = 0;
                byte Ano = 0;
                byte Hora = 0;
                byte Minuto = 0;
                byte Segundo = 0;
                string strCartao = string.Empty;
                int Ret = -1;
                int Count = 0;
                string NumCartao = string.Empty;
                string Bilhete;

                Thread.BeginCriticalRegion();
                //Envia o Comando Receber Dados Online..
                Ret = EasyInner.ReceberDadosOnLine(InnerAtual.Numero,
                    ref Origem, ref Complemento, Cartao, ref Dia, ref Mes, ref Ano, ref Hora,
                    ref Minuto, ref Segundo);

                //Atribui Temporizador
                InnerAtual.Temporizador = DateTime.Now;

                #region ENVIAR DIGITAIS PARA A CATRACA
                //-----------------------------------------------------
                //ENVIAR DIGITAIS PARA A CATRACA----------------------- 
                //-----------------------------------------------------

                TimeSpan hora_cad = new TimeSpan(00, 30, 00);
                TimeSpan hora_cad_pos = new TimeSpan(00, 33, 00);
                TimeSpan hora_agora = DateTime.Now.TimeOfDay;

                if (hora_agora > hora_cad && hora_agora < hora_cad_pos)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    UiMainOnline.lblEstado.Text = "Enviando Digitais";
                    UiMainOnline.manutencao_dig();
                    UiMainOnline.lblEstado.Text = "Digitais J� enviadas.";
                    Cursor.Current = Cursors.Default;

                    UiMainOnline.btnPararMaquina.PerformClick();
                    Thread.Sleep(5000);
                    UiMainOnline.btnIniciarMaquina.PerformClick();
                }

                //-----------------------------------------------------
                //-----------------------------------------------------
                #endregion

                #region SAIR DA ESCOLA
                //-----------------------------------------------------
                //ENVIAR DIGITAIS PARA FORA----------------------- 
                //-----------------------------------------------------

                TimeSpan hora_cad_2 = new TimeSpan(00, 10, 00);
                TimeSpan hora_cad_pos_2 = new TimeSpan(00, 13, 00);
                TimeSpan hora_agora_2 = DateTime.Now.TimeOfDay;

                if (hora_agora_2 > hora_cad_2 && hora_agora_2 < hora_cad_pos_2)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    UiMainOnline.lblEstado.Text = "Enviando Alunos para Fora";
                    UiMainOnline.sair_da_escola();
                    UiMainOnline.lblEstado.Text = "Alunos Enviados para Fora.";
                    Cursor.Current = Cursors.Default;

                    UiMainOnline.btnPararMaquina.PerformClick();
                    Thread.Sleep(5000);
                    UiMainOnline.btnIniciarMaquina.PerformClick();
                }

                //-----------------------------------------------------
                //-----------------------------------------------------
                #endregion

                //Testa o Retorno do Comando..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Teste se a origem � Fim de Acionamento, Fun��o, Anula ou Giro de Catraca..
                    //Caso seja alguma destas origens, retorna para a maquina de estados.
                    if (Complemento == (int)Enumeradores.Origem.FIM_TEMPO_ACIONAMENTO
                        || Complemento == (int)Enumeradores.Origem.GIRO_DA_CATRACA_TOPDATA
                        || Complemento == (int)Enumeradores.Origem.TECLA_FUNCAO
                        || Complemento == (int)Enumeradores.Origem.TECLA_ANULA
                        || ((Cartao.Length == 0) && !(InnerAtual.EstadoTeclado == Enumeradores.EstadosTeclado.AGUARDANDO_TECLADO)))                   
                    {
                        //Zera contador de tentativas
                        InnerAtual.CountTentativasEnvioComando = 0;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_PADRAO;
                        return;
                    }

                    //Validar os dados aqui
                    strCartao = "";
                    int tam = 0;

                    if (InnerAtual.QtdDigitos > 14)
                    {
                        tam = Cartao.Length - 1;
                    }
                    else
                    {
                        tam = Cartao.Length;
                    }

                    int cartaoo;

                    if (int.TryParse(Cartao.ToString(), out cartaoo) == true)
                    {
                        strCartao = cartaoo.ToString();

                    }
                    
                    NumCartao = "";
                    
                    //Padr�o Livre
                    NumCartao = strCartao;
                    
                    Bilhete = "Marca��es Online. Inner:" +
                        InnerAtual.Numero.ToString() +
                        "  Origem:" + Origem.ToString() +
                        "  Complemento:" + Complemento.ToString() +
                        "  Cart�o:" + NumCartao; 

                    //Se Quantidade de d�gitos informado for maior que 14 n�o deve mostrar data e hora
                    if (InnerAtual.QtdDigitos <= 14)
                    {
                        Bilhete = Bilhete +
                                  "  Data:" + Dia.ToString("00") + "/" +
                                  Mes.ToString("00") + "/" +
                                  Ano.ToString() +
                                  "  Hora:" + Hora.ToString("00") + ":" +
                                  Minuto.ToString("00") + ":" +
                                  Segundo.ToString("00");
                    }

                    //Adiciona bilhete coletado na Lista
                    UiMainOnline.lstBilhetes.Items.Add(Bilhete);
                    
                    if (strCartao != "220")
                    {
                        CSacesso_catraca cs = new CSacesso_catraca();

                        cs.id_buscar = strCartao;

                        cs.get_cartao(); //Verifiva se existe o numeor na tabela Acesso_catraca

                        //cs.verifica_presen�a(); //Verifica presen�a do Aluno
                    
                        if (CScard_catraca.presenca == "liberar") //Libera numero existente
                        {
                            CScard_catraca.id_card = strCartao;

                            if (CScard_catraca.estado_retorno == "0")
                            {
                                HABILITA_LADO_CATRACA(UiMainOnline, "Entrada");
                                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                                CScard_catraca.estado = "1";
                            }
                            else
                            {
                                HABILITA_LADO_CATRACA(UiMainOnline, "Entrada");
                                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                                CScard_catraca.estado = "1";
                            }
                        }
                        else if(CScard_catraca.presenca == "n_liberar") //Bloqueia numero n�o existente
                        {
                            
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_PADRAO;
                        }
                    }
                    else
                    {
                        // Cart�o 220 - MASTER
                        HABILITA_LADO_CATRACA(UiMainOnline, "dois");
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                        UiMainOnline.lstBilhetes.Items.Add("Master Passado");                        
                    }
                }
                else
                {
                    //Se passar 3 segundos sem receber nada, passa para o estado enviar ping on line, para manter o equipamento em on line.
                    TimeSpan tempo = DateTime.Now - InnerAtual.TempoInicialPingOnLine;
                    if (tempo.Seconds >= 3)
                    {
                        InnerAtual.EstadoSolicitacaoPingOnLine = InnerAtual.EstadoAtual;
                        InnerAtual.CountTentativasEnvioComando = 0;
                        InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.PING_ONLINE;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        #endregion

        #region PASSO_ENVIAR_MENSAGEM_PADRAO
        //***********************************************************************************
        //MENSAGEM_PADRAO
        //Envia mensagem padr�o modo Online
        //Pr�ximo passo: ESTADO_CONFIGURAR_ENTRADAS_ONLINE
        //***********************************************************************************
        private static void PASSO_ENVIAR_MENSAGEM_PADRAO(Inner innerAtual)
        {
            try
            {
                if(CScard_catraca.presenca == "n_liberar")
                {
                    //Testa o Retorno do comando de Envio de Mensagem Padr�o On Line
                    if (EasyInner.EnviarMensagemPadraoOnLine(innerAtual.Numero, 1, " N�m N�o Existe ") == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        //Muda o passo para configura��o de entradas Online.
                        innerAtual.CountTentativasEnvioComando = 0;
                        innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONFIGURAR_ENTRADAS_ONLINE;

                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                    {
                        //caso ele n�o consiga, tentar� enviar tr�s vezes, se n�o conseguir volta para o passo Reconectar
                        if (innerAtual.CountTentativasEnvioComando >= 3)
                        {
                            innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                        }
                        //Adiciona 1 ao contador de tentativas
                        innerAtual.CountTentativasEnvioComando++;
                    }                    
                }


                //Testa o Retorno do comando de Envio de Mensagem Padr�o On Line
                if (EasyInner.EnviarMensagemPadraoOnLine(innerAtual.Numero,1, " ") == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Muda o passo para configura��o de entradas Online.
                    innerAtual.CountTentativasEnvioComando = 0;
                    innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONFIGURAR_ENTRADAS_ONLINE;
                }
                else
                {
                    //caso ele n�o consiga, tentar� enviar tr�s vezes, se n�o conseguir volta para o passo Reconectar
                    if (innerAtual.CountTentativasEnvioComando >= 3)
                    {
                        innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 ao contador de tentativas
                    innerAtual.CountTentativasEnvioComando++;
                }
            }
            catch (Exception ex)
            {
                innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        #endregion

        #region PASSO_ENVIAR_MSG_URNA
        //***********************************************************************************
        //MSG_URNA
        //Envia mensagem padr�o estado Urna
        //Pr�ximo passo: ESTADO_MONITORA_URNA
        //***********************************************************************************
        private static void PASSO_ESTADO_ENVIA_MSG_URNA(Inner innerAtual)
        {
            try
            {
                //Testa o Retorno do comando de Envio de Mensagem padr�o Urna    
                if (EasyInner.EnviarMensagemPadraoOnLine(innerAtual.Numero, 0, " DEPOSITE O       CARTAO") == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    EasyInner.AcionarRele2(innerAtual.Numero);
                    innerAtual.CountTentativasEnvioComando = 0;
                    innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_MONITORA_URNA;
                }
                else
                {
                    //caso ele n�o consiga, tentar� enviar tr�s vezes, se n�o conseguir volta para o passo Reconectar
                    if (innerAtual.CountTentativasEnvioComando >= 3)
                    {
                        innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 ao contador de tentativas
                    innerAtual.CountTentativasEnvioComando++;
                }
            }
            catch (Exception ex)
            {
                innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        #endregion

        #region ESTADO_LIBERA_GIRO_CATRACA
        //***********************************************************************************
        //LIBERA_GIRO_CATRACA
        //Libera a catraca de acordo com o lado informado
        //Pr�ximo Passo: ESTADO_MONITORA_GIRO_CATRACA
        //***********************************************************************************
        private static void PASSO_LIBERA_GIRO_CATRACA(FrmOnline UiMainOnline, Inner InnerAtual)
        {

            string id_carta = Convert.ToInt32(CScard_catraca.id_card).ToString("D8");


            try
            {
                //Exibe estado do Inner no Rodap� da Janela
                UiMainOnline.lblStatus.Text = "Inner " + InnerAtual.Numero + " Libera Giro da Catraca...";

                //Declara��o de Vari�veis..
                int Ret = -1;

                //Envia comando de liberar a catraca para Entrada.
                if (LiberaEntrada)
                {
                    EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "    " + id_carta + "    ENTRADA LIBERADA");
                    LiberaEntrada = false;
                    Ret = EasyInner.LiberarCatracaEntrada(InnerAtual.Numero);
                }
                else
                {
                    if (LiberaEntradaInvertida)
                    {
                        EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "    " + id_carta + "    ENTRADA LIBERADA");
                        LiberaEntradaInvertida = false;
                        Ret = EasyInner.LiberarCatracaEntradaInvertida(InnerAtual.Numero);
                    }
                    else
                    {
                        //Envia comando de liberar a catraca para Sa�da.
                        if (LiberaSaida)
                        {
                            EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "    " + id_carta + "    SAIDA LIBERADA");
                            LiberaSaida = false;
                            Ret = EasyInner.LiberarCatracaSaida(InnerAtual.Numero);
                        }
                        else
                        {
                            if (LiberaSaidaInvertida)
                            {
                                EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "    " + id_carta + "    SAIDA LIBERADA");
                                LiberaSaidaInvertida = false;
                                Ret = EasyInner.LiberarCatracaSaidaInvertida(InnerAtual.Numero);
                            }
                            else
                            {
                                EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "ACESSO LIBERADO");
                                Ret = EasyInner.LiberarCatracaDoisSentidos(InnerAtual.Numero);
                            }
                        }
                    }
                }
        
                //Testa Retorno do comando..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    EasyInner.AcionarBipCurto(InnerAtual.Numero);
                    InnerAtual.CountPingFail = 0;
                    InnerAtual.CountTentativasEnvioComando = 0;
                    InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_MONITORA_GIRO_CATRACA;
                }
                else
                {
                    //Se o retorno for diferente de 0 tenta liberar a catraca 3 vezes, caso n�o consiga enviar o comando volta para o passo reconectar.
                    if (InnerAtual.CountTentativasEnvioComando >= 3)
                    {
                        InnerAtual.CountTentativasEnvioComando = 0;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 ao contador de tentativas
                    InnerAtual.CountTentativasEnvioComando++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        #endregion

        #region ESTADO_MONITORA_GIRO_CATRACA
        //***********************************************************************************
        //MONITORA_GIRO_CATRACA
        //Verifica se a catraca foi girada ou n�o e caso sim para qual lado.
        //Pr�ximo Passo: ESTADO_ENVIAR_MSG_PADRAO
        //***********************************************************************************

        
        private static void PASSO_MONITORA_GIRO_CATRACA(FrmOnline UiMainOnline, Inner InnerAtual)
        {

            try
            {
                //Exibe estado do giro
                UiMainOnline.lblDados.Text = "Monitorando Giro de Catraca!";

                //Exibe estado do Inner no Rodap� da Janela
                UiMainOnline.lblStatus.Text = "Inner " + InnerAtual.Numero + " Monitora Giro da Catraca...";

                //Declara��o de Vari�veis..
                Bilhete Bilhetes = new Bilhete();
                Bilhetes.Origem = 0;
                Bilhetes.Complemento = 0;
                Bilhetes.Cartao = null;
                StringBuilder Cartao = new StringBuilder();
                Bilhetes.Dia = 0;
                Bilhetes.Mes = 0;
                Bilhetes.Ano = 0;
                Bilhetes.Hora = 0;
                Bilhetes.Minuto = 0;
                Bilhetes.Segundo = 0;
                string strCartao = string.Empty;
                int Ret = -1;

                //Monitora o giro da catraca..
                Ret = EasyInner.ReceberDadosOnLine(InnerAtual.Numero,
                    ref Bilhetes.Origem, ref Bilhetes.Complemento, Cartao, ref Bilhetes.Dia, ref Bilhetes.Mes, ref Bilhetes.Ano, ref Bilhetes.Hora,
                    ref Bilhetes.Minuto, ref Bilhetes.Segundo);

                //Testa o retorno do comando..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {

                    //Testa se girou o n�o a catraca..
                    if (Bilhetes.Origem == (int)Enumeradores.Origem.FIM_TEMPO_ACIONAMENTO)
                    {
                        UiMainOnline.lblDados.Text = "N�o girou a catraca!";
                    }
                    else if (Bilhetes.Origem == (int)Enumeradores.Origem.GIRO_DA_CATRACA_TOPDATA)
                    {
                        UiMainOnline.lblDados.Text = "Girou a catraca para " + (Bilhetes.Complemento - Convert.ToInt16(true) == 0 ? "entrada." : "sa�da.").ToString();
                        CSacesso_catraca cs = new CSacesso_catraca();
                        cs.altera_estado();
                        cs.registra_passagem_estado();

                    }

                    UiMainOnline.cmdEntrada.Enabled = true;
                    UiMainOnline.cmdSair.Enabled = true;

                    //Vai para o estado de Envio de Msg Padr�o..
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_PADRAO;
                }
                else
                {
                    //Caso o tempo que estiver monitorando o giro chegue a 3 segundos,
                    //dever� enviar o ping on line para manter o equipamento em modo on line
                    TimeSpan tempo = DateTime.Now - InnerAtual.TempoInicialPingOnLine;
                    if (tempo.Seconds >= 3)
                    {
                        InnerAtual.EstadoSolicitacaoPingOnLine = InnerAtual.EstadoAtual;
                        InnerAtual.CountTentativasEnvioComando = 0;
                        InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.PING_ONLINE;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                            
            }
        }
        #endregion

        #region ESTADO_MONITORA_URNA
        //***********************************************************************************
        //MONITORA_URNA
        //Monitora o dep�sito do cart�o na Urna
        //Pr�ximo passo: ESTADO_LIBERAR_CATRACA
        //***********************************************************************************
        private static void PASSO_ESTADO_MONITORA_URNA(FrmOnline UiMainOnline, Inner InnerAtual)
        {
            try
            {
                //Exibe estado do giro
                UiMainOnline.lblDados.Text = "Monitorando Giro de Catraca!";
                
                //Exibe estado do Inner no Rodap� da Janela
                UiMainOnline.lblStatus.Text = "Inner " + InnerAtual.Numero + " Monitora Giro da Catraca...";

                //Declara��o de Vari�veis..
                Bilhete Bilhetes = new Bilhete();
                Bilhetes.Origem = 0;
                Bilhetes.Complemento = 0;
                Bilhetes.Cartao = null;
                StringBuilder Cartao = new StringBuilder();
                Bilhetes.Dia = 0;
                Bilhetes.Mes = 0;
                Bilhetes.Ano = 0;
                Bilhetes.Hora = 0;
                Bilhetes.Minuto = 0;
                Bilhetes.Segundo = 0;
                string strCartao = string.Empty;
                int Ret = -1;

                //Monitora o giro da catraca..
                Ret = EasyInner.ReceberDadosOnLine(InnerAtual.Numero,
                    ref Bilhetes.Origem, ref Bilhetes.Complemento, Cartao, ref Bilhetes.Dia, ref Bilhetes.Mes, ref Bilhetes.Ano, ref Bilhetes.Hora,
                    ref Bilhetes.Minuto, ref Bilhetes.Segundo);

                //Testa o retorno do comando..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Testa se a urna recolheu o cart�o
                    if (Bilhetes.Origem == (int)Enumeradores.Origem.URNA)
                    {
                        UiMainOnline.lblDados.Text = "URNA RECOLHEU CART�O";
                        
                        //Vai para o estado de Envio de Msg Padr�o..
                        LiberaSaida = true;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                    }
                    //Sen�o depositou o cart�o mostra mensagem e bloqueia o acesso
                    else if (Bilhetes.Origem == (int)Enumeradores.Origem.FIM_TEMPO_ACIONAMENTO)
                    {
                        UiMainOnline.lblDados.Text = "N�O DEPOSITOU CART�O";
                        EasyInner.AcionarBipLongo(InnerAtual.Numero);
                        EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "     ACESSO          NEGADO");
                        
                        //Vai para o estado de Envio de Msg Padr�o..
                        InnerAtual.TempoInicialMensagem = DateTime.Now;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.AGUARDA_TEMPO_MENSAGEM;
                    }
                }
                else
                {
                    //Caso o tempo que estiver monitorando o giro chegue a 3 segundos,
                    //dever� enviar o ping on line para manter o equipamento em modo on line
                    TimeSpan tempo = DateTime.Now - InnerAtual.TempoInicialPingOnLine;
                    if (tempo.Seconds >= 3)
                    {
                        InnerAtual.EstadoSolicitacaoPingOnLine = InnerAtual.EstadoAtual;
                        InnerAtual.CountTentativasEnvioComando = 0;
                        InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.PING_ONLINE;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);

            }
        }
        #endregion
        
        #region PASSO_ESTADO_ENVIA_PING_ONLINE
        //***********************************************************************************
        //ENVIA_PING_ONLINE
        //Testa comunica��o com o Inner e mant�m o Inner em OnLine quando a mudan�a
        //autom�tica est� configurada. Especialmente indicada para a verifica��o da
        //conex�o em comunica��o TCP/IP.
        //Pr�ximo Passo: RETORNA M�TODO QUE O ACIONOU
        //***********************************************************************************
        private static void PASSO_ESTADO_ENVIA_PING_ONLINE(FrmOnline UiMainOnline, Inner InnerAtual)
        {
            try
            {
                //Exibe estado do Inner no Rodap� da Janela
                UiMainOnline.lblStatus.Text = "Inner " + InnerAtual.Numero + " PING ONLINE...";

                //Envia o comando de PING ON LINE, se o retorno for OK volta para o estado onde chamou o m�todo
                int retorno = EasyInner.PingOnLine(InnerAtual.Numero);
                if (retorno == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    InnerAtual.EstadoAtual = InnerAtual.EstadoSolicitacaoPingOnLine;
                    InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                }
                else
                {
                    //caso ele n�o consiga, tentar� enviar tr�s vezes, se n�o conseguir volta para o passo Reconectar
                    if (InnerAtual.CountTentativasEnvioComando >= 3)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 ao contador de tentativas
                    InnerAtual.CountTentativasEnvioComando++;
                }
            }
            catch (Exception ex)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
         
        #endregion

        #region PASSO_ESTADO_RECONECTAR
        //***********************************************************************************
        //RECONECTAR
        //Se a conex�o cair tenta conectar novamente
        //Pr�ximo Passo: ESTADO_ENVIAR_CFG_OFFLINE
        //***********************************************************************************
        private static void PASSO_ESTADO_RECONECTAR(FrmOnline UiMainOnline, Inner InnerAtual)
        {
            try
            {
                //Verifica tempo
                TimeSpan tempo = DateTime.Now - InnerAtual.TempoInicialPingOnLine;
                if (tempo.Seconds < 10)
                {
                    return;
                }
                InnerAtual.TempoInicialPingOnLine = DateTime.Now;

                UiMainOnline.lblStatus.Text = "Inner " + InnerAtual.Numero + " Reconectando...";

                //Realiza Ping de DLL.
                int retorno = EasyInner.Ping(InnerAtual.Numero);
                if (retorno == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Caso o Inner esteja Realizando Ping, vai para o passo de Configura��o.
                    System.DateTime Data;
                    Data = System.DateTime.Now;
		            //Testa o comando de envio de rel�gio para o Inner
                    if (EasyInner.EnviarRelogio(InnerAtual.Numero,
                                                (byte)Data.Day,
                                                (byte)Data.Month,
                                                System.Convert.ToByte(Data.Year.ToString().Substring(2, 2)),
                                                (byte)Data.Hour,
                                                (byte)Data.Minute,
                                                (byte)Data.Second) == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        //Zera as vari�veis de controle da maquina de estados.
                        InnerAtual.CountTentativasEnvioComando = 0;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_CFG_OFFLINE;
                    }
                    else
                    {
			            //caso ele n�o consiga, tentar� enviar tr�s vezes, se n�o conseguir volta para o passo Reconectar
                        if (InnerAtual.CountTentativasEnvioComando >= 3)
                        {
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                        }
                        InnerAtual.CountTentativasEnvioComando++;
                    }
                }
                InnerAtual.CountRepeatPingOnline = 0;
            }
            catch (Exception ex)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        

        }
        #endregion

        #endregion

        #endregion

        #region IniciarMaquina
        internal static void IniciarMaquina(FrmOnline UiMainOnline)
        {
            try
            {
                UiMainOnline.Ativa = true;

                //UiMainOnline.btnAdicionarUsuarioInnerOnline.Enabled = false;
                //UiMainOnline.btnRemoverInnerLista.Enabled = false;

                FrmOnlineController.MaquinaOnline(UiMainOnline);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }
        }
        #endregion

        #region PararMaquina
        internal static void PararMaquina(FrmOnline UiMainOnline)
        {
            try
            {
                //Altera o flag da maquina de estados
                UiMainOnline.Ativa = false;

                //Libera bot�es..
                //UiMainOnline.btnAdicionarUsuarioInnerOnline.Enabled = true;
                //UiMainOnline.btnRemoverInnerLista.Enabled = true;

                //Exibe no rodap� o Fim da execu��o..
                UiMainOnline.lblStatus.Text = "Terminada execu��o Online...";

                //Fecha a porta da Easy Inner.
                EasyInner.FecharPortaComunicacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }
        }
        #endregion

        
    }
}
