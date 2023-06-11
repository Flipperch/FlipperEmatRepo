using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EmatWinFormsNetFramework13032.Digitais.COM;
using EmatWinFormsNetFramework13032.Digitais.Entity;
using System.Windows.Forms;



namespace EmatWinFormsNetFramework13032.Digitais.Controle_Catraca
{
    partial class FrmOffLineController
    {
        //private static Boolean InnerNetAcesso;
        //private static byte InnerAcessoBio;

        #region Linha
        private static byte _linha;
        public static byte Linha
        {
            get { return _linha; }
            set { _linha = value; }
        }
        #endregion

        //#region UiBIO
        //private static FrmOnline _UiMainOffline;
        //public static FrmOnline UiBIO
        //{
        //    get { return _UiMainOffline; }
        //    set { _UiMainOffline = value; }
        //}
        //#endregion

        #region Metodos

        #region Enviar
        //***********************************************************************************
        //COMANDO ENVIAR
        //Envia as configurações, relogio, mensagem, horarios, lista de acesso, horario
        //da sirene, lista dos inners.
        //***********************************************************************************
        internal static void Enviar(frConfCatraca UiMainOffline)
        {
            int Ret = -1;

            //Campo obrigatório
            if (UiMainOffline.cboPadraoCartao.SelectedIndex == -1)
            {
                MessageBox.Show("Favor selecionar um tipo de leitor !", "Atenção");
                return;
            }

            //Se catraca deve informar o lado que está instalada
            if ((UiMainOffline.cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Coletor) || ((UiMainOffline.optDireita.Checked) || (UiMainOffline.optEsquerda.Checked)))
            {
            }
            else
            {
                MessageBox.Show("Favor informar o lado de instalação da catraca !", "Atenção");
                return;
            }

            //Mensagem Status
            UiMainOffline.lblEnvia.Text = "Conectando com o inner...";
            UiMainOffline.btnEnviar.Enabled = false;
            UiMainOffline.btnReceber.Enabled = false;
            Application.DoEvents();
            UiMainOffline.lblVersao.Text = "";
            Linha = 0;

            //Define a versão do equipamento
            if (!DefineVersao(UiMainOffline))
            {
                UiMainOffline.lblEnvia.Text = "Erro ao conectar no inner!";
                EasyInner.FecharPortaComunicacao();
                UiMainOffline.btnEnviar.Enabled = true;
                UiMainOffline.btnReceber.Enabled = true;
                Application.DoEvents();
                return;
            }

            //Se selecionado Biometria, valida se o equipamento é compatível
            if (UiMainOffline.chkBio.Checked)
            {
                //if ((((Linha != 6) && (Linha != 14)) || ((Linha == 14) && (InnerAcessoBio == 0))))
                if ((((Linha != 6) && (Linha != 14)) || ((Linha == 14))))
                {
                    MessageBox.Show("Equipamento não compatível com Biometria.", "Atenção");
                }
            }

            //Mensagem Status
            UiMainOffline.lblEnvia.Text = "Enviando configurações...";
            System.Threading.Thread.Sleep(100);
            Application.DoEvents();

            //Chama rotina que monta as configurações
            MontarConfiguracao(UiMainOffline);

            //Configura o tipo de registro que será associado a uma marcação, quando for
            //inserido o dedo no Inner bio sem que o usuário tenha definido se é uma entrada,
            //saída, função...
            if (UiMainOffline.chkBio.Checked)
                //Entrada
                EasyInner.DefinirFuncaoDefaultSensorBiometria(10);
            else
                //Desativa
                EasyInner.DefinirFuncaoDefaultSensorBiometria(0);

            //Envia buffer com as configurações, buffer interno da dll que contém todas as
            //configurações das funções anteriores para o Inner, após o envio esse buffer
            //é limpo sendo necessário chamar novamente as funções acima para reconfigurá-lo.
            Ret = EasyInner.EnviarConfiguracoes(int.Parse(UiMainOffline.txtNumInner.Text)); //(nº do Inner)
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                UiMainOffline.lblEnvia.Text = "Configurações enviadas com sucesso!";
            }
            else
            {
                UiMainOffline.lblEnvia.Text = "Erro ao enviar configurações!";
                UiMainOffline.btnEnviar.Enabled = true;
                UiMainOffline.btnReceber.Enabled = true;
                return;
            }

            Application.DoEvents();

            //Envia relógio
            //Configura o relógio(data/hora) do Inner.
            if (UiMainOffline.chkRelogio.Checked)
            {
                //Mensagem Status
                UiMainOffline.lblEnvia.Text = "Enviando relógio...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);

                //Formato o ano, pega apenas os dois ultimos digitos
                int Ano = int.Parse(System.DateTime.Now.ToString("yy"));
                int Mes = System.DateTime.Now.Month;
                int Dia = System.DateTime.Now.Day;
                int Hora = System.DateTime.Now.Hour;
                int Minuto = System.DateTime.Now.Minute;
                int Segundo = System.DateTime.Now.Second;

                //Envia relogio
                Ret = EasyInner.EnviarRelogio(int.Parse(UiMainOffline.txtNumInner.Text), (byte)Dia, (byte)Mes, (byte)Ano, (byte)Hora, (byte)Minuto, (byte)Segundo);

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    UiMainOffline.lblEnvia.Text = "Relógio enviado com sucesso!";
                }
                else
                {
                    UiMainOffline.lblEnvia.Text = "Erro ao enviar relógio!";
                    UiMainOffline.btnEnviar.Enabled = true;
                    UiMainOffline.btnReceber.Enabled = true;
                    return;
                }
                Application.DoEvents();
            }

            //Envia o buffer com todas as mensagens off line configuradas anteriormente,
            //para o Inner.
            if (UiMainOffline.chkMensagem.Checked)
            {
                //Mensagem Status
                UiMainOffline.lblEnvia.Text = "Enviando mensagem...";
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);

                //Chama rotina de envio de mensagem
                MontarMensagem();

                //Envia Buffer com todas as mensagens offline
                Ret = EasyInner.EnviarMensagensOffLine(int.Parse(UiMainOffline.txtNumInner.Text));

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    UiMainOffline.lblEnvia.Text = "Mensagem enviada com sucesso!";
                }
                else
                {
                    UiMainOffline.lblEnvia.Text = "Erro ao enviar Mensagem!";
                    UiMainOffline.btnEnviar.Enabled = true;
                    UiMainOffline.btnReceber.Enabled = true;
                    return;
                }
                Application.DoEvents();
            }

            //Envia o buffer com os horário de sirene cadastrados para o Inner.
            if (UiMainOffline.chkSirene.Checked)
            {
                //Mensagem Status
                UiMainOffline.lblEnvia.Text = "Enviando horários sirene...";
                System.Threading.Thread.Sleep(100);

                //Chama rotina que monta os horarios
                MontarHorariosSirene();
                Application.DoEvents();
                Ret = EasyInner.EnviarHorariosSirene(int.Parse(UiMainOffline.txtNumInner.Text));

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    UiMainOffline.lblEnvia.Text = "Horários da Sirene enviados com sucesso!";
                }
                else
                {
                    UiMainOffline.lblEnvia.Text = "Erro ao enviar os horários da sirene!";
                    UiMainOffline.btnEnviar.Enabled = true;
                    UiMainOffline.btnReceber.Enabled = true;
                    return;
                }
                Application.DoEvents();
            }

            //Envia para o Inner o buffer com a lista de horários de acesso, após executar
            //o comando o buffer é limpo tomaticamente pela dll
            if (UiMainOffline.chkHorarios.Checked)
            {
                //Mensagem Status
                UiMainOffline.lblEnvia.Text = "Enviando horários...";

                //chama a rotina que monta horarios
                MontarHorarios();
                Application.DoEvents();
                System.Threading.Thread.Sleep(100);

                //Envia buffer com lista de horarios de acesso
                Ret = EasyInner.EnviarHorariosAcesso(int.Parse(UiMainOffline.txtNumInner.Text));

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    UiMainOffline.lblEnvia.Text = "Horários enviados com sucesso!";
                }
                else
                {
                    UiMainOffline.lblEnvia.Text = "Erro ao enviar os horários!";
                    UiMainOffline.btnEnviar.Enabled = true;
                    UiMainOffline.btnReceber.Enabled = true;
                    return;
                }
                Application.DoEvents();
            }
            else
            {
                do
                { 
                    System.Threading.Thread.Sleep(10);

                    //Apaga o buffer com a lista de horários de acesso e envia
                    //automaticamente para o Inner.
                    Ret = EasyInner.ApagarHorariosAcesso(int.Parse(UiMainOffline.txtNumInner.Text));

                } while (Ret != (int)Enumeradores.Retorno.RET_COMANDO_OK);
            }

            //Envia lista
            if (UiMainOffline.chkLista.Checked)
            {
                //Mensagem Status
                UiMainOffline.lblEnvia.Text = "Enviando lista...";
                System.Threading.Thread.Sleep(100);

                //Verifica qual lista enviar
                if (UiMainOffline.rdbPadraoTopdata.Checked)
                {
                    //Chama rotina que monta lista do tipo TOPDATA
                    MontarListaTopdata();
                }
                else
                {
                    if (UiMainOffline.rdbPadraoLivre.Checked)
                    {
                        //Chama rotina que monta lista do tipo LIVRE
                        MontarListaLivre(UiMainOffline);
                    }
                }
                Application.DoEvents();
                //Envia o Buffer com os usuarios da lista

                EasyInner.InserirUsuarioListaAcesso("7540091",101);
                EasyInner.EnviarListaAcesso(1);
                //Ret = EasyInner.EnviarListaAcesso(int.Parse(UiMainOffline.txtNumInner.Text));

                //if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                //{
                //    UiMainOffline.lblEnvia.Text = "Lista enviada com sucesso!";
                //}
                //else
                //{
                //    UiMainOffline.lblEnvia.Text = "Erro ao enviar lista!";
                //    UiMainOffline.btnEnviar.Enabled = true;
                //    UiMainOffline.btnReceber.Enabled = true;
                //    return;
                //}
                Application.DoEvents();
            }

            //Equipamento Biométrico
            //if ((Linha == 6 || (Linha == 14 && InnerAcessoBio == 1)) && (UiMainOffline.chkBio.Checked))
            if ((Linha == 6 || (Linha == 14)) && (UiMainOffline.chkBio.Checked))
            {
                //Habilita/Desabilita a identificação biométrica e/ou a verificação
                //biométrica do Inner bio.
                EasyInner.ConfigurarBio(int.Parse(UiMainOffline.txtNumInner.Text), (byte)(UiMainOffline.chkIdentificacao.Checked ? 1 : 0), (byte)(UiMainOffline.chkVerificacao.Checked ? 1 : 0));

                //Retorna o resultado da configuração do Inner Bio, função ConfigurarBio.
                //Se o retorno for igual a 0 é porque o Inner bio foi configurado com
                //sucesso.
                Ret = EasyInner.ResultadoConfiguracaoBio(int.Parse(UiMainOffline.txtNumInner.Text), 0);
            }

            //Envia lista biometrica
            if (UiMainOffline.chkListaBio.Checked)
            {
                //Mensagem Status
                UiMainOffline.lblEnvia.Text = "Enviando lista do InnerBio...";
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();

                //Chama rotina que monta o buffer de cartoes que nao irao precisar da digital
                MontarBufferListaSemDigital();

                Application.DoEvents();
                //Envia o buffer com a lista de usuarios sem digital
                Ret = EasyInner.EnviarListaUsuariosSemDigitalBio(int.Parse(UiMainOffline.txtNumInner.Text));

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    UiMainOffline.lblEnvia.Text = "Lista do InnerBio enviada com sucesso!";
                }
                else
                {
                    UiMainOffline.lblEnvia.Text = "Erro ao enviar lista do InnerBio!";
                    UiMainOffline.btnEnviar.Enabled = true;
                    UiMainOffline.btnReceber.Enabled = true;
                    return;
                }
                Application.DoEvents();
            }
            else
            {
                EasyInner.EnviarListaUsuariosSemDigitalBio(int.Parse(UiMainOffline.txtNumInner.Text));
            }

            UiMainOffline.btnEnviar.Enabled = true;
            UiMainOffline.btnReceber.Enabled = true;

            //Após procedimentos, fecha porta de comunicação
            EasyInner.FecharPortaComunicacao();
        }
        #endregion

        #region Receber
        //***********************************************************************************
        //RECEBER
        //Esta rotina é responsável por efetuar a coleta dos bilhetes, verificando
        //qual o padrão do cartão
        //***********************************************************************************
        internal static void Receber(frConfCatraca UiMainOffline)
        {
            //Campo obrigatório
            if (UiMainOffline.cboPadraoCartao.SelectedIndex == -1)
            {
                MessageBox.Show("Favor selecionar um tipo de leitor !", "Atenção");
                return;
            }

            //Desabilita os botões durante a coleta
            UiMainOffline.btnEnviar.Enabled = false;
            UiMainOffline.btnReceber.Enabled = false;

            //Define qual será o tipo de conexão(meio de comunicação) que será utilizada
            //pela dll para comunicar com os Inners. Essa função deverá ser chamada antes
            //de iniciar o processo de comunicação e antes da função AbrirPortaComunicacao.
            EasyInner.DefinirTipoConexao((byte)UiMainOffline.cboTipoConexao.SelectedIndex);

            //Define qual padrão de cartão será utilizado pelos Inners
            //padrão Topdata ou padrão livre.
            if (UiMainOffline.rdbPadraoTopdata.Checked)
            {
                EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_TOPDATA);
            }
            else
            {
                if (UiMainOffline.rdbPadraoLivre.Checked)
                {
                    EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_LIVRE);
                }
            }

            //Mensagem Status
            UiMainOffline.lblBilhetes.Text = "Coletando bilhetes...";
            Application.DoEvents();

            //Chama rotina que realiza a coleta dos bilhetes offline
            //if (InnerNetAcesso)
            //{
            //    ColetarBilhetesInnerAcesso(UiMainOffline);
            //}
            //else
            //{
            ColetarBilhetes(UiMainOffline);
            //}

            //Após realizar a coleta, habilita novamente os botões
            UiMainOffline.btnEnviar.Enabled = true;
            UiMainOffline.btnReceber.Enabled = true;
        }

        #endregion

        #endregion

        #region Metodos Auxiliares

        #region DefineVersao
        //***********************************************************************************
        //DEFINEVERSAO
        //Esta rotina é responsável por identificar a versão do inner
        //***********************************************************************************
        private static bool DefineVersao(frConfCatraca UiMainOffline)
        {
            //Declaração de variáveis
            short VariacaoInner = 0;
            byte VersaoAlta = 0;
            byte VersaoBaixa = 0;
            byte VersaoSufixo = 0;
            byte Linha_ = 0;
            byte Ruf = 0;
            string LinhaInner = "";
            string VersaoInner = "";
            byte Ret2 = 0;
            int Modelo = 0;
            string ModeloBioInner = "";
            int VersaoAltaBio = 0;
            int VersaoBaixaBio = 0;
            string VersaoBio = "";
            string StrVersao = "";

            Application.DoEvents();

            //Chama rotina que realiza a conexão
            if (Conectar(UiMainOffline))
            {
                Ret2 = 255;

                while (Ret2 != 0)
                {
                    //Solicita a versão do firmware do Inner e dados como o Idioma, se é
                    //uma versão especial.
                    Ret2 = EasyInner.ReceberVersaoFirmware(int.Parse(UiMainOffline.txtNumInner.Text), ref Linha_, ref VariacaoInner, ref VersaoAlta, ref VersaoBaixa, ref VersaoSufixo, ref Ruf);
                    System.Threading.Thread.Sleep(100);
                    Linha = Linha_;
                }

                if (Ret2 == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Define a linha do Inner
                    switch (Linha)
                    {
                        case 1:
                            LinhaInner = "Inner Plus";
                            break;
                        case 2:
                            LinhaInner = "Inner Disk";
                            break;
                        case 3:
                            LinhaInner = "Inner Verid";
                            break;
                        case 6:
                            LinhaInner = "Inner Bio";
                            break;
                        case 7:
                            LinhaInner = "Inner NET";
                            break;
                        //case 14:
                        //    LinhaInner = "Inner Acesso";
                        //    InnerNetAcesso = true;
                        //    break;
                    }

                    VersaoInner = VersaoAlta.ToString() + "." + VersaoBaixa.ToString() + "." + VersaoSufixo.ToString();
                    StrVersao = LinhaInner;

                    if (VariacaoInner > 0)
                        StrVersao = StrVersao + " - Variação: " + VariacaoInner.ToString();

                    StrVersao = StrVersao + " - Versão: " + VersaoInner;

                    //Se for biometria
                    //if ((Linha == 6 || (Linha == 14 && InnerAcessoBio == 1)) && (UiMainOffline.chkBio.Checked))
                    if ((Linha == 6 || (Linha == 14)) && (UiMainOffline.chkBio.Checked))
                    {
                        //Solicita o modelo do Inner bio.
                        EasyInner.SolicitarModeloBio(int.Parse(UiMainOffline.txtNumInner.Text));
                        Ret2 = 128;

                        while (Ret2 == 128)
                        {
                            //Retorna o resultado do comando SolicitarModeloBio, o modelo
                            //do Inner Bio é retornado por referência no parâmetro da função.
                            Ret2 = EasyInner.ReceberModeloBio(int.Parse(UiMainOffline.txtNumInner.Text), 0, ref Modelo);
                            System.Threading.Thread.Sleep(100);
                        }

                        //Define o modelo do Inner Bio
                        switch (Modelo)
                        {
                            case 1:
                                ModeloBioInner = "Modelo: Light 100 usuários FIM10";
                                break;
                            case 4:
                                ModeloBioInner = "Modelo: 1000/4000 usuários FIM01";
                                break;
                            case 51:
                                ModeloBioInner = "Modelo: 1000/4000 usuários FIM2030";
                                break;
                            case 52:
                                ModeloBioInner = "Modelo: 1000/4000 usuários FIM2040";
                                break;
                            case 48:
                                ModeloBioInner = "Modelo: Light 100 usuários FIM3030";
                                break;
                            case 64:
                                ModeloBioInner = "Modelo: Light 100 usuários FIM3040";
                                break;
                            case 80:
                                ModeloBioInner = "Modelo: 1000/4000 usuários FIM5060";
                                break;
                            case 82:
                                ModeloBioInner = "Modelo: 1000/4000 usuários FIM5260";
                                break;
                            case 83:
                                ModeloBioInner = "Modelo: Light 100 usuários FIM5360";
                                break;
                            case 255:
                                ModeloBioInner = "Modelo: Desconhecido";
                                break;
                        }

                        //Solicita a versão do Inner bio.
                        Ret2 = EasyInner.SolicitarVersaoBio(int.Parse(UiMainOffline.txtNumInner.Text));
                        Ret2 = 128;

                        while (Ret2 == 128)
                        {
                            //Retorna o resultado do comando SolicitarVersaoBio, a versão
                            //do Inner Bio é retornado por referência nos parâmetros da
                            //função.
                            Ret2 = EasyInner.ReceberVersaoBio(System.Convert.ToInt32(UiMainOffline.txtNumInner.Text), 0, ref VersaoAltaBio, ref VersaoBaixaBio);
                            System.Threading.Thread.Sleep(100);
                        }

                        VersaoBio = VersaoAltaBio.ToString() + "." + VersaoBaixaBio.ToString();
                        StrVersao = StrVersao + " - " + ModeloBioInner + " -> " + VersaoBio;
                    }

                    UiMainOffline.lblVersao.Text = StrVersao;
                    Application.DoEvents();
                    return true;
                }
                else
                {
                    //Mensagens Status
                    UiMainOffline.lblEnvia.Text = "Erro ao conectar no inner!";
                    EasyInner.FecharPortaComunicacao();
                    Application.DoEvents();
                    return false;
                }
            }
            else
            {
                UiMainOffline.lblEnvia.Text = "Erro ao conectar no inner!";
                EasyInner.FecharPortaComunicacao();
                Application.DoEvents();
                return false;
            }

        }
        #endregion

        #region Conectar
        //***********************************************************************************
        //CONECTAR
        //Rotina responsável por efetuar a conexão com o Inner
        //***********************************************************************************
        private static bool Conectar(frConfCatraca UiMainOffline)
        {
            int Fim = 0;
            int Ret = -1;

            //Define qual será o tipo de conexão(meio de comunicação) que será utilizada
            //pela dll para comunicar com os Inners. Essa função deverá ser chamada antes
            //de iniciar o processo de comunicação e antes da função AbrirPortaComunicacao.
            EasyInner.DefinirTipoConexao((byte)UiMainOffline.cboTipoConexao.SelectedIndex);

            //Define qual padrão de cartão será utilizado pelos Inners
            //padrão Topdata ou padrão livre.
            if (UiMainOffline.rdbPadraoTopdata.Checked)
            {
                EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_TOPDATA);
            }
            else
            {
                if (UiMainOffline.rdbPadraoLivre.Checked)
                {
                    EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_LIVRE);
                }
            }

            //Fecha a porta de comunicação previamente aberta, seja ela serial, Modem ou
            //TCP/IP.
            EasyInner.FecharPortaComunicacao();

            //Abre a porta de comunicação desejada, essa função deverá ser chamada antes
            //de iniciar qualquer processo de transmissão ou recepção de dados com o Inner.
            Ret = EasyInner.AbrirPortaComunicacao(int.Parse(UiMainOffline.txtPorta.Text));

            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                Fim = (int)EasyInner.RetornarSegundosSys() + 15;
                do
                {
                    System.Threading.Thread.Sleep(100);
                    //Testa a comunicação com o Inner, também utilizado para efetuar a conexão
                    //com o Inner. Para efetuar a conexão com o Inner, essa função deve ser
                    //executada em um loop até retornar 0(zero), executado com sucesso.
                    Ret = EasyInner.Ping(int.Parse(UiMainOffline.txtNumInner.Text));
                } while (((int)EasyInner.RetornarSegundosSys() <= Fim) && (Ret != (int)Enumeradores.Retorno.RET_COMANDO_OK));

                return (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK);
            }
            else
            {
                return (false);
            }
        }
        #endregion

        #region MontarConfiguracao
        //***********************************************************************************
        //MONTAR CONFIGURAÇÕES
        //Esta rotina monta o buffer para enviar a configuração do Inner
        //***********************************************************************************
        private static void MontarConfiguracao(frConfCatraca UiMainOffline)
        {
            //Antes de realizar a configuração precisa definir o Padrão do cartão
            //Topdata ou padrão livre.
            if (UiMainOffline.rdbPadraoTopdata.Checked)
            {
                EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_TOPDATA);
            }
            else
            {
                if (UiMainOffline.rdbPadraoLivre.Checked)
                {
                    EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_LIVRE);
                }
            }

            //Modo de comunicação
            //Configurações para Modo Offline.
            //Prepara o Inner para trabalhar no modo Off-Line, porém essa função ainda
            //não envia essa informação para o equipamento.
            EasyInner.ConfigurarInnerOffLine();

            //Verificar
            //Acionamentos 1 e 2
            //Configura como irá funcionar o acionamento(rele) 1 e 2 do Inner, e por
            //quanto tempo ele será acionado.
            switch (UiMainOffline.cboEquipamento.SelectedIndex)
            {
                //Coletor
                case (byte)Enumeradores.Acionamento.Acionamento_Coletor:
                    if (UiMainOffline.chkSirene.Checked)
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CONECTADO_SIRENE, 5);
                        EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA_OU_SAIDA, 5);
                        EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA_OU_SAIDA, 3);
                    }
                    break;

                //Catraca
                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_E_Saida:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA_OU_SAIDA, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.CONECTADO_SIRENE, 5);
                    break;
                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.CONECTADO_SIRENE, 5);
                    break;
                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Urna:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA_OU_SAIDA, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_SAIDA, 5);
                    break;
                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Saida:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_SAIDA, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.CONECTADO_SIRENE, 5);
                    break;
                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Saida_Liberada:
                    //Se Esquerda Selecionado - Inverte código
                    if ((UiMainOffline.cboEquipamento.SelectedIndex != (byte)Enumeradores.Acionamento.Acionamento_Coletor) && (UiMainOffline.optEsquerda.Checked))
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_ENTRADA_LIBERADA, 5);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_SAIDA_LIBERADA, 5);
                    }
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.CONECTADO_SIRENE, 5);
                    break;
                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_Liberada:
                    //Se Esquerda Selecionado - Inverte código
                    if ((UiMainOffline.cboEquipamento.SelectedIndex != (byte)Enumeradores.Acionamento.Acionamento_Coletor) && (UiMainOffline.optEsquerda.Checked))
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_SAIDA_LIBERADA, 5);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_ENTRADA_LIBERADA, 5);
                    }
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.CONECTADO_SIRENE, 5);
                    break;
                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Liberada_2_Sentidos:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_LIBERADA_DOIS_SENTIDOS, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.CONECTADO_SIRENE, 5);
                    break;
                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Sentido_Giro:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_LIBERADA_DOIS_SENTIDOS_MARCACAO_REGISTRO, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.CONECTADO_SIRENE, 5);
                    break;
            }

            //Configura o tipo do leitor que o Inner está utilizando, se é um leitor
            //de código de barras, magnético ou proximidade.
            switch (UiMainOffline.cboPadraoCartao.SelectedIndex)
            {
                //leitor barras
                case (byte)Enumeradores.TipoLeitor.CODIGO_DE_BARRAS:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.CODIGO_DE_BARRAS);
                    break;

                //leitor magnético
                case (byte)Enumeradores.TipoLeitor.MAGNETICO:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.MAGNETICO);
                    break;

                //leitor proximidade abatrack
                case (byte)Enumeradores.TipoLeitor.PROXIMIDADE_ABATRACK2:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.PROXIMIDADE_ABATRACK2);
                    break;

                //leitor wiegand - 6 dígitos
                case (byte)Enumeradores.TipoLeitor.WIEGAND:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.WIEGAND);
                    break;
            }

            //Define a quantidade de dígitos dos cartões a serem lidos pelo Inner.
            EasyInner.DefinirQuantidadeDigitosCartao((byte)int.Parse(UiMainOffline.txtDigitos.Text));

            //Habilitar teclado
            //Permite que os dados sejam inseridos no Inner através do teclado do
            //equipamento. Habilitando o parâmetro ecoar, o teclado irá ecoar asteriscos
            //no display do Inner.
            EasyInner.HabilitarTeclado((byte)(UiMainOffline.chkTeclado.Checked ? 1 : 0), 0);

            //ConfigurarLeitor: Configura as operações que o leitor irá executar. Se irá
            //registrar os dados somente como entrada independente do sentido em que o
            //cartão for passado, somente como saída ou como entrada e saída.
            if (UiMainOffline.chkDoisLeitores.Checked)
            {
                //Configuração Catraca Esquerda ou Direita
                if (UiMainOffline.optDireita.Checked)
                {
                    //Direita Selecionado
                    EasyInner.ConfigurarLeitor1((byte)Enumeradores.LeitorEntrada.LEITOR1_SOMENTE_ENTRADA);
                    EasyInner.ConfigurarLeitor2((byte)Enumeradores.LeitorEntrada.LEITOR2_SOMENTE_SAIDA);
                }
                else
                {
                    //Esquerda Selecionado
                    EasyInner.ConfigurarLeitor1((byte)Enumeradores.LeitorEntrada.LEITOR1_SOMENTE_SAIDA);
                    EasyInner.ConfigurarLeitor2((byte)Enumeradores.LeitorEntrada.LEITOR2_SOMENTE_ENTRADA);
                }

                //Habilita os leitores wiegand para o primeiro leitor e o segundo leitor
                //do Inner, e configura se o segundo leitor irá exibir as mensagens
                //configuradas.
                EasyInner.ConfigurarWiegandDoisLeitores(0, 1);
            }
            else
            {
                if (UiMainOffline.optDireita.Checked)
                {
                    //Direita Selecionado
                    EasyInner.ConfigurarLeitor1((byte)Enumeradores.LeitorEntrada.LEITOR1_ENTRADA_SAIDA);
                }
                else
                {
                    //Esquerda Selecionado
                    EasyInner.ConfigurarLeitor1((byte)Enumeradores.LeitorEntrada.LEITOR1_SAIDA_ENTRADA);
                }

                EasyInner.ConfigurarLeitor2((byte)Enumeradores.LeitorEntrada.LEITOR1_DESABILITADO);
            }

            //Define qual tipo de lista(controle) de acesso o Inner vai utilizar
            if (UiMainOffline.chkLista.Checked)
                EasyInner.DefinirTipoListaAcesso(1);
            else
                EasyInner.DefinirTipoListaAcesso(0);

            //Configura o Inner para registrar as tentativas de acesso negado.
            EasyInner.RegistrarAcessoNegado(1);

            //Catraca
            //Define qual será o tipo do registro realizado pelo Inner ao aproximar um
            //cartão do tipo proximidade no leitor do Inner, sem que o usuário tenha
            //pressionado a tecla entrada, saída ou função.
            if ((UiMainOffline.cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_E_Saida) || (UiMainOffline.cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Liberada_2_Sentidos) || (UiMainOffline.cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Sentido_Giro))
            {
                EasyInner.DefinirFuncaoDefaultLeitoresProximidade(12); // 12 – Libera a catraca nos dois sentidos e registra o bilhete conforme o sentido giro.
            }
            else
            {
                if ((UiMainOffline.cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada) || (UiMainOffline.cboEquipamento.SelectedIndex == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Saida_Liberada))
                {
                    if (UiMainOffline.optDireita.Checked)
                    {
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(10);  // 10 – Registrar sempre como entrada.
                    }
                    else
                    {
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(11);  // Inverte o sentido de entrada.
                    }

                }
                else
                {
                    if (UiMainOffline.optDireita.Checked)
                    {
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(11);  // 10 – Registrar sempre como entrada.
                    }
                    else
                    {
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(10);  // Inverte o sentido de entrada.
                    }
                }
            }

            //Configura o tipo de registro que será associado a uma marcação, quando
            //for inserido o dedo no Inner bio sem que o usuário tenha definido se é um
            //entrada, saída, função, etc.
            EasyInner.DefinirFuncaoDefaultSensorBiometria(0);
        }
        #endregion

        #region ColetarBilhetes
        //***********************************************************************************
        //COLETAR BILHETES
        //Esta rotina efetua a coleta de bilhetes que foram registrados em offline
        //***********************************************************************************
        private static void ColetarBilhetesInnerAcesso(frConfCatraca UiMainOffline)
        {

            //Declaração das variáveis
            Bilhete Bilhete;
            Bilhete = new Bilhete();
            Bilhete.Ano = 0;
            Bilhete.Cartao = null;
            Bilhete.Cartao = new StringBuilder();
            Bilhete.Dia = 0;
            Bilhete.Hora = 0;
            Bilhete.Mes = 0;
            Bilhete.Minuto = 0;
            Bilhete.Tipo = 0;

            int Fim = 0;
            int Ret = -1;
            String strCartao = "";
            int Count;
            int nBilhetes = 0;
            int QtdeBilhetes;
            int[] receber = new int[2];

            //Verifica conexao
            if (Conectar(UiMainOffline))
            {
                nBilhetes = 0;
                QtdeBilhetes = 0;
                Ret = EasyInner.ReceberQuantidadeBilhetes(int.Parse(UiMainOffline.txtNumInner.Text), receber);
                QtdeBilhetes = receber[0];

                UiMainOffline.lblBilhetes.Text = "Foram coletados 0 bilhete(s)!";

                do
                {
                    if (QtdeBilhetes > 0)
                    {
                        do
                        {
                            System.Threading.Thread.Sleep(100);

                            //Coleta um bilhete Off-Line que está armazenado na memória do Inner
                            Ret = EasyInner.ColetarBilhete(int.Parse(UiMainOffline.txtNumInner.Text), ref Bilhete.Tipo, ref Bilhete.Dia, ref Bilhete.Mes, ref Bilhete.Ano, ref Bilhete.Hora, ref Bilhete.Minuto, Bilhete.Cartao);

                            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                            {
                                strCartao = "";

                                //Atribui o nro do Cartão..
                                for (Count = 0; Count < 16; Count++)
                                {
                                    strCartao += System.Convert.ToString(System.Convert.ToChar(Bilhete.Cartao[Count]));
                                }

                                //Armazena os dados do bilhete no list, pode ser utilizado com
                                //banco de dados ou outro meio de armazenamento compatível
                                UiMainOffline.lstBilhetes.Items.Add("Tipo:" + Bilhete.Tipo + "  Cartão:" + strCartao + "  Data:" + Bilhete.Dia.ToString("00") + "/" + Bilhete.Mes.ToString("00") + "/" + Bilhete.Ano.ToString("00") + "  Hora:" + Bilhete.Hora.ToString("00") + ":" + Bilhete.Minuto.ToString("00"));
                                Fim = (int)EasyInner.RetornarSegundosSys() + 15;
                                nBilhetes++;
                                QtdeBilhetes--;
                                Application.DoEvents();
                            }
                        } while (QtdeBilhetes > 0);

                        UiMainOffline.lblBilhetes.Text = "Foram coletados " + nBilhetes + " bilhete(s) offline !";
                        Ret = EasyInner.ReceberQuantidadeBilhetes(int.Parse(UiMainOffline.txtNumInner.Text), receber);
                        QtdeBilhetes = receber[0];
                    }
                } while (QtdeBilhetes > 0);

            }
            else
            {
                UiMainOffline.lblBilhetes.Text = "Erro ao conectar no inner!";
            }
            Application.DoEvents();
        }
        #endregion

        #region ColetarBilhetes
        //***********************************************************************************
        //COLETAR BILHETES
        //Esta rotina efetua a coleta de bilhetes que foram registrados em offline
        //***********************************************************************************
        private static void ColetarBilhetes(frConfCatraca UiMainOffline)
        {

            //Declaração das variáveis
            Bilhete Bilhete;
            Bilhete = new Bilhete();
            Bilhete.Ano = 0;
            Bilhete.Cartao = null;
            Bilhete.Cartao = new StringBuilder();
            Bilhete.Dia = 0;
            Bilhete.Hora = 0;
            Bilhete.Mes = 0;
            Bilhete.Minuto = 0;
            Bilhete.Tipo = 0;

            int Fim = 0;
            int Ret = -1;
            String strCartao = "";
            int Count;
            int nBilhetes = 0;

            //Verifica conexao
            if (Conectar(UiMainOffline))
            {
                //Contador tempo de coleta
                Fim = (int)EasyInner.RetornarSegundosSys() + 15;
                do
                {
                    System.Threading.Thread.Sleep(100);

                    //Coleta um bilhete Off-Line que está armazenado na memória do Inner
                    Ret = EasyInner.ColetarBilhete(int.Parse(UiMainOffline.txtNumInner.Text), ref Bilhete.Tipo, ref Bilhete.Dia, ref Bilhete.Mes, ref Bilhete.Ano, ref Bilhete.Hora, ref Bilhete.Minuto, Bilhete.Cartao);

                    if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        strCartao = "";

                        //Atribui o nro do Cartão..
                        for (Count = 0; Count < 16; Count++)
                        {
                            strCartao += System.Convert.ToString(System.Convert.ToChar(Bilhete.Cartao[Count]));
                        }

                        //Armazena os dados do bilhete no list, pode ser utilizado com
                        //banco de dados ou outro meio de armazenamento compatível
                        UiMainOffline.lstBilhetes.Items.Add("Tipo:" + Bilhete.Tipo + "  Cartão:" + strCartao + "  Data:" + Bilhete.Dia.ToString("00") + "/" + Bilhete.Mes.ToString("00") + "/" + Bilhete.Ano.ToString("00") + "  Hora:" + Bilhete.Hora.ToString("00") + ":" + Bilhete.Minuto.ToString("00"));
                        Fim = (int)EasyInner.RetornarSegundosSys() + 15;
                        nBilhetes++;
                        Application.DoEvents();
                    }
                } while ((Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK) || ((int)EasyInner.RetornarSegundosSys() <= Fim));

                EasyInner.FecharPortaComunicacao();

                //Mensagens Status
                UiMainOffline.lblBilhetes.Text = "Foram coletados " + nBilhetes + " bilhete(s) offline !";
            }
            else
            {
                UiMainOffline.lblBilhetes.Text = "Erro ao conectar no inner!";
            }
            Application.DoEvents();
        }
        #endregion

        #region MontarBufferListaSemDigital
        //***********************************************************************************
        //APENAS PARA O INNER BIO
        //Monta o buffer da lista de cartões dos usuários sem digital no Inner bio
        //***********************************************************************************
        private static void MontarBufferListaSemDigital()
        {
            EasyInner.IncluirUsuarioSemDigitalBio("999");
            EasyInner.IncluirUsuarioSemDigitalBio("1000");
            EasyInner.IncluirUsuarioSemDigitalBio("1");
            EasyInner.IncluirUsuarioSemDigitalBio("666");
            EasyInner.IncluirUsuarioSemDigitalBio("3007");
        }
        #endregion

        #region MontarHorarios
        //***********************************************************************************
        //MONTAR HORARIOS
        //Monta o buffer para enviar os horários de acesso
        //Tabela de horarios numero 1
        //***********************************************************************************
        private static void MontarHorarios()
        {
            //Insere no buffer da DLL horario de acesso
            //(Segunda - dia da semana = 1)
            EasyInner.InserirHorarioAcesso(1, 1, 1, 8, 0); //(1 - nº da tabela horario, 1 - dia da semana, 1 - faixa de horario, 8 - hora, 0 - minuto)
            EasyInner.InserirHorarioAcesso(1, 1, 2, 12, 0);
            EasyInner.InserirHorarioAcesso(1, 1, 3, 13, 0);
            EasyInner.InserirHorarioAcesso(1, 1, 4, 18, 0);

            //(Terça - dia da semana = 2)
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
        }
        #endregion

        #region MontarMensagem
        //***********************************************************************************
        //MONTAR MENSAGEM
        //Esta rotina é responsável por montar o buffer para o envio de mensagens
        //***********************************************************************************
        private static void MontarMensagem()
        {
            EasyInner.DefinirMensagemEntradaOffLine(1, "ENTRADA OFF LINE"); //(1 - Exibe data/hora, string com a mensagem a ser exibida no momento da entrada)
            EasyInner.DefinirMensagemSaidaOffLine(1, "SAIDA   OFF LINE"); //(1 - Exibe data/hora, string com a mensagem a ser exibida no momento da saida)
            EasyInner.DefinirMensagemPadraoOffLine(1, "    OFF LINE    "); //(1 - Exibe data/hora, string com a mensagem a ser exibida quando o Inner estiver ocioso)
            EasyInner.DefinirMensagemFuncaoOffLine("    FUNCAO 0    ", 0, 1); //(String com mensagem, nº da função, habilita função)
            EasyInner.DefinirMensagemFuncaoOffLine("    FUNCAO 1    ", 1, 0); //(String com mensagem, nº da função, desabilita função)
            EasyInner.DefinirMensagemFuncaoOffLine("    FUNCAO 2    ", 2, 1); //(String com mensagem, nº da função, habilita função)
            EasyInner.DefinirMensagemFuncaoOffLine("    FUNCAO 3    ", 3, 0); //(String com mensagem, nº da função, desabilita função)
            EasyInner.DefinirMensagemFuncaoOffLine("    FUNCAO 4    ", 4, 1); //(String com mensagem, nº da função, habilita função)
            EasyInner.DefinirMensagemFuncaoOffLine("    FUNCAO 5    ", 5, 0); //(String com mensagem, nº da função, desabilita função)
            EasyInner.DefinirMensagemFuncaoOffLine("    FUNCAO 6    ", 6, 1); //(String com mensagem, nº da função, habilita função)
            EasyInner.DefinirMensagemFuncaoOffLine("    FUNCAO 7    ", 7, 0); //(String com mensagem, nº da função, desabilita função)
            EasyInner.DefinirMensagemFuncaoOffLine("    FUNCAO 8    ", 8, 1); //(String com mensagem, nº da função, habilita função)
            EasyInner.DefinirMensagemFuncaoOffLine("    FUNCAO 9    ", 9, 0); //(String com mensagem, nº da função, desabilita função)
        }

        #endregion

        #region MontarHorariosSirene
        //***********************************************************************************
        //MONTAR HORARIO SIRENE
        //Esta rotina monta os horários de toque da sirene e quais dias da semana irão tocar
        //***********************************************************************************
        private static void MontarHorariosSirene()
        {
            EasyInner.InserirHorarioSirene(8, 0, 1, 1, 1, 1, 1, 1, 1);   //( Hora, Minuto, Segunda, Terca, Quarta, Quinta, Sexta, Sabado,DomingoFeriado)
            EasyInner.InserirHorarioSirene(15, 34, 1, 1, 1, 1, 1, 1, 1); //( Hora, Minuto, Segunda, Terca, Quarta, Quinta, Sexta, Sabado,DomingoFeriado)
            EasyInner.InserirHorarioSirene(15, 35, 1, 1, 1, 1, 1, 1, 1); //( Hora, Minuto, Segunda, Terca, Quarta, Quinta, Sexta, Sabado,DomingoFeriado)
            EasyInner.InserirHorarioSirene(15, 36, 1, 1, 1, 1, 1, 1, 1); //( Hora, Minuto, Segunda, Terca, Quarta, Quinta, Sexta, Sabado,DomingoFeriado)
            EasyInner.InserirHorarioSirene(18, 0, 1, 1, 1, 1, 1, 1, 1);  //( Hora, Minuto, Segunda, Terca, Quarta, Quinta, Sexta, Sabado,DomingoFeriado)
        }
        #endregion

        #region MontarListaTopdata
        //***********************************************************************************
        //MONTAR LISTA TOPDATA
        //Monta o buffer para enviar a lista nos inners da linha Inner, cartão padrão Topdata
        //***********************************************************************************
        private static void MontarListaTopdata()
        {

            //Define qual padrao o Inner vai usar
            EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_TOPDATA);

            //Insere usuario da lista no buffer da DLL
            EasyInner.InserirUsuarioListaAcesso("100", 1);
            EasyInner.InserirUsuarioListaAcesso("1", 101);
            EasyInner.InserirUsuarioListaAcesso("2", 102);
        }
        #endregion

        #region MontarListaLivre
        //***********************************************************************************
        //MONTAR LISTA LIVRE
        //Monta o buffer para enviar a lista nos inners da linha Inner, cartão padrão livre 14 dígitos
        //***********************************************************************************
        private static void MontarListaLivre(frConfCatraca UiMainOffline)
        {
            //Define qual padrao o Inner vai usar
            EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_LIVRE); //(1 - Padrao Livre(Default))

            //Quantidade de digitos que o cartao usará
            EasyInner.DefinirQuantidadeDigitosCartao((byte)int.Parse(UiMainOffline.txtDigitos.Text)); //(qtde de digitos)

            //Insere usuario da lista no buffer da DLL
            EasyInner.InserirUsuarioListaAcesso("1", 1); //(1 - depende do padrao do cartao, 1 - nº do horario ja cadastrado)
            EasyInner.ApagarListaAcesso(1);
            //EasyInner.InserirUsuarioListaAcesso("2", 1);
            //EasyInner.InserirUsuarioListaAcesso("3", 1);
            //EasyInner.InserirUsuarioListaAcesso("100", 1);
            //EasyInner.InserirUsuarioListaAcesso("100001", 1);
            //EasyInner.InserirUsuarioListaAcesso("99999999999999", 101);
            //EasyInner.InserirUsuarioListaAcesso("1234", 102);
            //EasyInner.InserirUsuarioListaAcesso("1000", 101);
            //EasyInner.InserirUsuarioListaAcesso("666", 101);
            //EasyInner.InserirUsuarioListaAcesso("999", 101);
            //EasyInner.InserirUsuarioListaAcesso("00000000000011", 101);

            //15MIL
            //for (int i = 1;i<=14563;i++) {
            //  String c = String.Format("%010d",i);
            //    EasyInner.InserirUsuarioListaAcesso(c,101);
            //}
        }
        #endregion

        #endregion
    }
}
