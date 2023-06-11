using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using EmatWinFormsNetFramework1402.Classes;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.Relatorios.Escolas.Votorantim
{
    class RelatoriosVotorantim
    {
        //TODO::IMPORTANTE... REF Conf/appConfig RELATÓRIOS CRIAR MÉTODO DENTRO DAS CLASSES DE RELATORIOS DAS OPÇÕES DE ACORDO COM A UNIDADE 
        //ASSIM REMOVENDO MAIS ESSAS COSTUMIZAÇÕES DO CÓDIGO FONTE DO FORMULÁRIO

        //TODO:ARRUMAR CLASSE PARA QUE CASO ALGUM MÉTODO ENTRE NO CATCH, RETORNE NULL E NÃO ABRA A JANELA

        public static rptPassaporte rptPassaporte(Aluno objAluno)
        {
            rptPassaporte rptPassaporte = new rptPassaporte();
            try
            {
                rptPassaporte.SetParameterValue("nMatricula", objAluno.NMatricula);
                rptPassaporte.SetParameterValue("nome", objAluno.Nome);
                rptPassaporte.SetParameterValue("telefone", objAluno.Telefone);
                rptPassaporte.SetParameterValue("celular", objAluno.Celular);
                rptPassaporte.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
                rptPassaporte.SetParameterValue("ra", objAluno.Ra);
                rptPassaporte.SetParameterValue("cidade", primeira_caixa_alta(objAluno.Endereco.Cidade.Nome));
                rptPassaporte.SetParameterValue("ensino", primeira_caixa_alta(Classes.Aluno.GetEnsinoAlunoAtual(objAluno).Ensino.ToString()));
                rptPassaporte.SetParameterValue("dtMatricula", objAluno.DtMatricula.ToString("dd/MM/yyyy"));
                rptPassaporte.SetParameterValue("termo", objAluno.TermoMatricula);
                rptPassaporte.SetParameterValue("observacao", objAluno.Observacao);


                if (objAluno.NomeSocial != string.Empty)
                {
                    rptPassaporte.SetParameterValue("nome", objAluno.NomeSocial);
                    rptPassaporte.SetParameterValue("observacao", objAluno.Nome);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);

            }
            return rptPassaporte;
        }

        public static rptDeclaracaoMatricula rptDeclaracaoMatricula(Aluno objAluno)
        {
            rptDeclaracaoMatricula rptDeclaracaoMatricula = new rptDeclaracaoMatricula();

            try
            {
                rptDeclaracaoMatricula.SetParameterValue("nome", objAluno.Nome);
                rptDeclaracaoMatricula.SetParameterValue("n_mat", objAluno.NMatricula);
                rptDeclaracaoMatricula.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
                rptDeclaracaoMatricula.SetParameterValue("ensino_atual", primeira_caixa_alta(Aluno.GetEnsinoAlunoAtual(objAluno).Ensino.ToString()));
                rptDeclaracaoMatricula.SetParameterValue("data_relatorio", data_relatorio(DateTime.Now));

                var ultimaPresenca = AtendimentoAluno.GetUltimaPresença(objAluno);
                if (ultimaPresenca != null)
                {
                    rptDeclaracaoMatricula.SetParameterValue("ultimaPresenca",
                        ultimaPresenca.DtDoAtendimento.ToString("dd/MM/yyyy"));
                }
                else
                {
                    rptDeclaracaoMatricula.SetParameterValue("ultimaPresenca",
                        objAluno.DtMatricula.ToString("dd/MM/yyyy"));
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }
            return rptDeclaracaoMatricula;
        }

        public static rptAtestadoEliminacao rptAtestadoEliminacao(Aluno objAluno, EnsinoAluno ensinoAluno, DateTime dtRelatorio)
        {
            rptAtestadoEliminacao rptAtestadoEliminacao = new rptAtestadoEliminacao();

            #region Preencher Dados Eliminações
            DataTable dt = new DataTable();
            dt.Columns.Add("dis");
            dt.Columns.Add("ins");
            dt.Columns.Add("mu");
            dt.Columns.Add("uf");
            dt.Columns.Add("dt");
            dt.Columns.Add("not");


            foreach (DisciplinaAluno disciplinaAluno in ensinoAluno.ListaDisciplinaAluno)
            {
                if (disciplinaAluno.Concluida)
                {
                    if (disciplinaAluno.Media != null)
                    {
                        DataRow dr;
                        dr = dt.NewRow();
                        dr["dis"] = disciplinaAluno.Disciplina.Nome;
                        dr["ins"] = disciplinaAluno.Media.Instituicao;
                        dr["mu"] = disciplinaAluno.Media.Cidade.Nome;
                        dr["uf"] = disciplinaAluno.Media.Cidade.Uf.Sigla;
                        dr["dt"] = DateTime.Parse(disciplinaAluno.Media.DtMedia).ToString("dd/MM/yyyy");
                        dr["not"] = disciplinaAluno.Media.Valor;
                        dt.Rows.Add(dr);
                    }
                }
            }
            rptAtestadoEliminacao.SetDataSource(dt);
            #endregion

            rptAtestadoEliminacao.SetParameterValue("nome", objAluno.Nome);
            rptAtestadoEliminacao.SetParameterValue("n_mat", objAluno.NMatricula);
            rptAtestadoEliminacao.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
            rptAtestadoEliminacao.SetParameterValue("dtNascimento", DateTime.Parse(objAluno.DtNascimento).ToString("dd/MM/yyyy"));
            if (objAluno.LocalNascimento != null)
                rptAtestadoEliminacao.SetParameterValue("localNascimento", objAluno.LocalNascimento.Cidade.Nome);
            else
                rptAtestadoEliminacao.SetParameterValue("localNascimento", "");
            rptAtestadoEliminacao.SetParameterValue("ensinoAluno", primeira_caixa_alta(ensinoAluno.Ensino.ToString()));
            rptAtestadoEliminacao.SetParameterValue("ra", objAluno.Ra);
            rptAtestadoEliminacao.SetParameterValue("dtRelatorio", data_relatorio(dtRelatorio));


            return rptAtestadoEliminacao;
        }

        public static rptDeclaracaoConclusao rptDeclaracaoConclusao(Aluno objAluno, EnsinoAluno ensinoSelecionado, DateTime dtRelatorio)
        {
            rptDeclaracaoConclusao rptDeclaracaoConclusao = new rptDeclaracaoConclusao();
            try
            {

                rptDeclaracaoConclusao.SetParameterValue("nome", objAluno.Nome);
                rptDeclaracaoConclusao.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
                rptDeclaracaoConclusao.SetParameterValue("ensino_selecionado", ensinoSelecionado.Ensino.ToString());
                rptDeclaracaoConclusao.SetParameterValue("dtRelatorio", data_relatorio(dtRelatorio));

                List<AtendimentoAluno> ListaGeralAtendimentoAluno = new List<AtendimentoAluno>();

                if (DateTime.TryParse(ensinoSelecionado.DtTermino, out DateTime dtConclusao))
                {
                    rptDeclaracaoConclusao.SetParameterValue("dataConclusao", dtConclusao.ToString("dd/MM/yyyy"));
                }
                else
                {
                    List<DisciplinaAluno> listaDisciplinaAluno = new List<DisciplinaAluno>();
                    string ultimaMedia = "";

                    foreach (DisciplinaAluno disciplinaAluno in ensinoSelecionado.ListaDisciplinaAluno)
                    {
                        if (disciplinaAluno.ListaAtendimentoAluno != null)
                        {
                            foreach (AtendimentoAluno atendimentoAluno in disciplinaAluno.ListaAtendimentoAluno)
                            {
                                ListaGeralAtendimentoAluno.Add(atendimentoAluno);
                            }
                        }

                        //if (DateTime.TryParse(disciplinaAluno.DtInicio, out DateTime data))
                        //{
                        //    listaDisciplinaAluno.Add(disciplinaAluno);
                        //}
                    }

                    if (ListaGeralAtendimentoAluno.Count > 0)
                    {

                        ultimaMedia = ListaGeralAtendimentoAluno.
                            OrderBy(x => x.DtDoAtendimento).
                            Select(x => x.DtDoAtendimento.ToString("dd/MM/yyyy")).
                            LastOrDefault();
                    }

                    rptDeclaracaoConclusao.SetParameterValue("dataConclusao", ultimaMedia);




                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

            return rptDeclaracaoConclusao;
        }

        public static rptDeclaracaoInss rptDeclaracaoInss(Aluno objAluno)
        {
            //TODO:: FAZER RELATORIO INSS PARA PROXIMA ATUALIZAÇÃO URGENTE
            rptDeclaracaoInss rptDeclaracaoInss = new rptDeclaracaoInss();
            try
            {
                rptDeclaracaoInss.SetParameterValue("nMatricula", objAluno.NMatricula);
                rptDeclaracaoInss.SetParameterValue("nome", objAluno.Nome);
                rptDeclaracaoInss.SetParameterValue("telefone", objAluno.Telefone);
                rptDeclaracaoInss.SetParameterValue("celular", objAluno.Celular);
                rptDeclaracaoInss.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
                rptDeclaracaoInss.SetParameterValue("ra", objAluno.Ra);
                rptDeclaracaoInss.SetParameterValue("cidade", primeira_caixa_alta(objAluno.Endereco.Cidade.Nome));
                rptDeclaracaoInss.SetParameterValue("ensino", primeira_caixa_alta(objAluno.ListaEnsinoAluno.FirstOrDefault(en => en.Atual).Ensino.ToString()));
                rptDeclaracaoInss.SetParameterValue("dtMatricula", objAluno.DtMatricula);
                rptDeclaracaoInss.SetParameterValue("termo", objAluno.TermoMatricula);
                rptDeclaracaoInss.SetParameterValue("observacao", objAluno.Observacao);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

            return rptDeclaracaoInss;
        }

        public static rptDeclaracaoEliminacao rptDeclaracaoEliminacao(Aluno objAluno, DateTime dtRelatorio)
        {
            rptDeclaracaoEliminacao rptDeclaracaoEliminacao = new rptDeclaracaoEliminacao();

            try
            {

                rptDeclaracaoEliminacao.SetParameterValue("nMatricula", objAluno.NMatricula);
                rptDeclaracaoEliminacao.SetParameterValue("nome", objAluno.Nome);
                rptDeclaracaoEliminacao.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
                rptDeclaracaoEliminacao.SetParameterValue("ensino_atual", primeira_caixa_alta(Classes.Aluno.GetEnsinoAlunoAtual(objAluno).Ensino.ToString()));
                rptDeclaracaoEliminacao.SetParameterValue("dtRelatorio", data_relatorio(dtRelatorio));

                List<DisciplinaAluno> listaDisciplinasEliminadas = new List<DisciplinaAluno>();
                List<DisciplinaAluno> listaDisciplinasRestantes = new List<DisciplinaAluno>();

                foreach (Classes.DisciplinaAluno disciplinaAluno in Classes.Aluno.GetEnsinoAlunoAtual(objAluno).ListaDisciplinaAluno)
                {
                    if (disciplinaAluno.Concluida)
                    {
                        if (disciplinaAluno.Media != null)
                        {
                            listaDisciplinasEliminadas.Add(disciplinaAluno);
                        }
                    }
                    else
                    {
                        listaDisciplinasRestantes.Add(disciplinaAluno);
                    }
                }

                string sequenciaDisciplinasEliminadas = "";
                if (listaDisciplinasEliminadas.Count == 1)
                {
                    sequenciaDisciplinasEliminadas = listaDisciplinasEliminadas.Select(x => x.Disciplina.Nome).LastOrDefault();
                }
                else
                {
                    sequenciaDisciplinasEliminadas = String.Join(", ", listaDisciplinasEliminadas.Select(x => x.Disciplina.Nome).ToArray(), 0,
                        listaDisciplinasEliminadas.Count - 1) + " e " + listaDisciplinasEliminadas.Select(x => x.Disciplina.Nome).LastOrDefault();
                }


                string sequenciaDisciplinasRestantes = "";
                if (listaDisciplinasRestantes.Count == 1)
                {
                    sequenciaDisciplinasRestantes = listaDisciplinasRestantes.Select(x => x.Disciplina.Nome).LastOrDefault();
                }
                else
                {
                    sequenciaDisciplinasRestantes = String.Join(", ", listaDisciplinasRestantes.Select(x => x.Disciplina.Nome).ToArray(), 0,
                        listaDisciplinasRestantes.Count - 1) + " e " + listaDisciplinasRestantes.Select(x => x.Disciplina.Nome).LastOrDefault();
                }

                rptDeclaracaoEliminacao.SetParameterValue("sequencia_disciplinas_eliminadas", sequenciaDisciplinasEliminadas);
                rptDeclaracaoEliminacao.SetParameterValue("sequencia_disciplinas_restantes", sequenciaDisciplinasRestantes);

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

            return rptDeclaracaoEliminacao;
        }

        public static rptRequerimentoMatricula rptRequerimentoMatricula(Aluno objAluno, IEmatriculaSettings settings)
        {
            rptRequerimentoMatricula rptRequerimentoMatricula = new rptRequerimentoMatricula();

            #region FOTO
            DataTable dtAluno = new DataTable("ETIQUETA");
            DataSet dsaluno = new DataSet();
            dtAluno.Columns.Add("FOTO", System.Type.GetType("System.Byte[]"));
            FotoAluno FotoDoAluno = new FotoAluno(objAluno.NMatricula, settings);
            
            if (FotoDoAluno != null)
            {
                if (File.Exists(objAluno.FotoDoAluno.Caminho))
                {
                    DataRow row = dtAluno.NewRow();
                    FileStream fs = new FileStream(FotoDoAluno.Caminho, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    row["FOTO"] = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length));
                    br = null;
                    fs.Close();
                    dtAluno.Rows.Add(row);
                    dsaluno.Tables.Add(dtAluno);
                }
            }
            rptRequerimentoMatricula.SetDataSource(dsaluno);
            #endregion

            #region limpar campos
            rptRequerimentoMatricula.SetParameterValue("nMatricula", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("nome", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("nomeSocial", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("telefone", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("celular", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("rg", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("ufRg", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("ra", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("cpf", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("ensino", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("dataMatricula", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("ano", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("observacao", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("sexo", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("estadoCivil", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("cor", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("nomeMae", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("dtNascimento", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("nascimentoCidade", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("nascimentoUfSigla", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("pais", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("cidade", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("bairro", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("pais", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("uf", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("cep", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("complemento", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("logradouro", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("numero", string.Empty);
            #endregion

            try
            {
                rptRequerimentoMatricula.SetParameterValue("nMatricula", objAluno.NMatricula);
                rptRequerimentoMatricula.SetParameterValue("nome", objAluno.Nome);
                if (objAluno.NomeSocial != string.Empty)
                    rptRequerimentoMatricula.SetParameterValue("nomeSocial", " / " + objAluno.NomeSocial);
                rptRequerimentoMatricula.SetParameterValue("telefone", objAluno.Telefone);
                rptRequerimentoMatricula.SetParameterValue("celular", objAluno.Celular);
                rptRequerimentoMatricula.SetParameterValue("rg", objAluno.Rg);
                rptRequerimentoMatricula.SetParameterValue("ufRg", objAluno.UfRg);
                rptRequerimentoMatricula.SetParameterValue("ra", objAluno.Ra);
                rptRequerimentoMatricula.SetParameterValue("cpf", objAluno.Cpf);
                rptRequerimentoMatricula.SetParameterValue("ensino", primeira_caixa_alta(Classes.Aluno.GetEnsinoAlunoAtual(objAluno).Ensino.ToString()));
                rptRequerimentoMatricula.SetParameterValue("dataMatricula", objAluno.DtMatricula.ToString("dd/MM/yyyy"));
                rptRequerimentoMatricula.SetParameterValue("ano", objAluno.TermoMatricula);
                rptRequerimentoMatricula.SetParameterValue("observacao", objAluno.Observacao);
                rptRequerimentoMatricula.SetParameterValue("sexo", Enum.GetName(typeof(Enumeradores.Sexo), objAluno.Sexo));
                rptRequerimentoMatricula.SetParameterValue("estadoCivil", Enum.GetName(typeof(Enumeradores.EstadoCivil), objAluno.EstadoCivil));
                rptRequerimentoMatricula.SetParameterValue("cor", Enum.GetName(typeof(Enumeradores.CorOrigemEtnica), objAluno.CorOrigemEtnica));
                rptRequerimentoMatricula.SetParameterValue("nomeMae", objAluno.NomeMae);
                rptRequerimentoMatricula.SetParameterValue("dtNascimento", objAluno.DtNascimento);
                if (objAluno.LocalNascimento != null)
                {
                    rptRequerimentoMatricula.SetParameterValue("nascimentoCidade", objAluno.LocalNascimento.Cidade.Nome);
                    if (!String.IsNullOrWhiteSpace(objAluno.LocalNascimento.Cidade.Uf.Sigla))
                        rptRequerimentoMatricula.SetParameterValue("nascimentoUfSigla", objAluno.LocalNascimento.Cidade.Uf.Sigla);
                    else
                        rptRequerimentoMatricula.SetParameterValue("nascimentoUfSigla", objAluno.LocalNascimento.Cidade.Uf.Nome);
                    rptRequerimentoMatricula.SetParameterValue("pais", objAluno.LocalNascimento.Cidade.Uf.Pais.Nome);
                }

                if (objAluno.Endereco != null)
                {
                    if (objAluno.Endereco.Cidade.Nome != null)
                    {
                        rptRequerimentoMatricula.SetParameterValue("cidade", objAluno.Endereco.Cidade.Nome);
                        rptRequerimentoMatricula.SetParameterValue("bairro", objAluno.Endereco.Bairro);
                        if (!String.IsNullOrWhiteSpace(objAluno.Endereco.Cidade.Uf.Sigla))
                            rptRequerimentoMatricula.SetParameterValue("uf", objAluno.Endereco.Cidade.Uf.Sigla);
                        else
                            rptRequerimentoMatricula.SetParameterValue("uf", objAluno.Endereco.Cidade.Uf.Nome);
                        rptRequerimentoMatricula.SetParameterValue("cep", objAluno.Endereco.Cep);
                        rptRequerimentoMatricula.SetParameterValue("complemento", objAluno.Endereco.Complemento);
                        rptRequerimentoMatricula.SetParameterValue("logradouro", objAluno.Endereco.Logradouro);
                        rptRequerimentoMatricula.SetParameterValue("numero", objAluno.Endereco.Numero);
                    }
                }

                //Eliminações
                rptRequerimentoMatricula.SetParameterValue("portugues", "(   ) Português");
                rptRequerimentoMatricula.SetParameterValue("matematica", "(   ) Matemática");
                rptRequerimentoMatricula.SetParameterValue("historia", "(   ) História");
                rptRequerimentoMatricula.SetParameterValue("geografia", "(   ) Geografia");
                rptRequerimentoMatricula.SetParameterValue("ciencias", "(   ) Ciências");
                rptRequerimentoMatricula.SetParameterValue("arte", "(   ) Arte");
                rptRequerimentoMatricula.SetParameterValue("ingles", "(   ) Inglês");
                rptRequerimentoMatricula.SetParameterValue("quimica", "(   ) Química");
                rptRequerimentoMatricula.SetParameterValue("biologia", "(   ) Biologia");
                rptRequerimentoMatricula.SetParameterValue("fisica", "(   ) Física");
                rptRequerimentoMatricula.SetParameterValue("sociologia", "(   ) Sociologia");
                rptRequerimentoMatricula.SetParameterValue("filosofia", "(   ) Filosofia");

                rptRequerimentoMatricula.SetParameterValue("dtEnsinoFundamental", "__________");

                foreach (var ensinoAluno in objAluno.ListaEnsinoAluno)
                {
                    if (ensinoAluno.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    {
                        if (ensinoAluno.DtTermino != string.Empty)
                        {
                            rptRequerimentoMatricula.SetParameterValue("dtEnsinoFundamental",
                                DateTime.Parse(ensinoAluno.DtTermino).ToString("dd/MM/yyyy"));
                        }
                    }
                }
                rptRequerimentoMatricula.SetParameterValue("dataRelatorio", data_relatorio(DateTime.Now));

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

            return rptRequerimentoMatricula;
        }

        public static rptHistoricoEscolarFundamental rptHistoricoEscolarFundamental(Aluno objAluno)
        {
            rptHistoricoEscolarFundamental rptHistoricoEscolarFundamental = new rptHistoricoEscolarFundamental();
            try
            {
                #region Preencher Dados Médias
                DataTable dt = new DataTable();
                dt.Columns.Add("dis");
                dt.Columns.Add("ins");
                dt.Columns.Add("mu");
                dt.Columns.Add("uf");
                dt.Columns.Add("dt");
                dt.Columns.Add("not");

                foreach (EnsinoAluno ensinoAluno in objAluno.ListaEnsinoAluno)
                {
                    if (ensinoAluno.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    {
                        foreach (DisciplinaAluno disciplinaAluno in ensinoAluno.ListaDisciplinaAluno.OrderBy(d => d.Disciplina.Ordem))
                        {
                            dt.Rows.Add();
                            dt.Rows[dt.Rows.Count - 1]["dis"] = disciplinaAluno.Disciplina.NomeHistorico;
                            if (disciplinaAluno.Media != null)
                            {
                                dt.Rows[dt.Rows.Count - 1]["ins"] = disciplinaAluno.Media.Instituicao;
                                dt.Rows[dt.Rows.Count - 1]["mu"] = disciplinaAluno.Media.Cidade.Nome;
                                dt.Rows[dt.Rows.Count - 1]["uf"] = disciplinaAluno.Media.Cidade.Uf.Sigla;
                                dt.Rows[dt.Rows.Count - 1]["dt"] = DateTime.Parse(disciplinaAluno.Media.DtMedia).ToString("dd/MM/yyyy");
                                dt.Rows[dt.Rows.Count - 1]["not"] = disciplinaAluno.Media.Valor;
                            }
                            else
                            {
                                dt.Rows[dt.Rows.Count - 1]["ins"] = "----";
                                dt.Rows[dt.Rows.Count - 1]["mu"] = "----";
                                dt.Rows[dt.Rows.Count - 1]["uf"] = "----";
                                dt.Rows[dt.Rows.Count - 1]["dt"] = "----";
                                dt.Rows[dt.Rows.Count - 1]["not"] = "----";
                            }
                        }
                        break;
                    }
                }
                rptHistoricoEscolarFundamental.SetDataSource(dt);
                #endregion

                rptHistoricoEscolarFundamental.SetParameterValue("nome", objAluno.Nome);
                rptHistoricoEscolarFundamental.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
                rptHistoricoEscolarFundamental.SetParameterValue("data_relatorio", DateTime.Now.ToString("dd/MM/yyyy"));
                rptHistoricoEscolarFundamental.SetParameterValue("nasc_estado", objAluno.LocalNascimento.Cidade.Uf.Nome);
                rptHistoricoEscolarFundamental.SetParameterValue("nasc_cidade", objAluno.LocalNascimento.Cidade.Nome);
                rptHistoricoEscolarFundamental.SetParameterValue("nasc_pais", objAluno.LocalNascimento.Cidade.Uf.Pais.Nome);
                rptHistoricoEscolarFundamental.SetParameterValue("ra", objAluno.Ra);
                rptHistoricoEscolarFundamental.SetParameterValue("dat_nasc", DateTime.Parse(objAluno.DtNascimento).ToString("dd/MM/yyyy"));
                rptHistoricoEscolarFundamental.SetParameterValue("n_mat", objAluno.NMatricula);
                rptHistoricoEscolarFundamental.SetParameterValue("mae", objAluno.NomeMae);

                foreach (EnsinoAluno ensinoAluno in objAluno.ListaEnsinoAluno)
                {
                    if (ensinoAluno.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    {
                        rptHistoricoEscolarFundamental.SetParameterValue("diretor", ensinoAluno.HistoricoEscolar.Diretor.Nome);
                        rptHistoricoEscolarFundamental.SetParameterValue("rg_diretor", ensinoAluno.HistoricoEscolar.Diretor.Rg);
                        rptHistoricoEscolarFundamental.SetParameterValue("secretario", ensinoAluno.HistoricoEscolar.Secretario.Nome);
                        rptHistoricoEscolarFundamental.SetParameterValue("rg_secretario", ensinoAluno.HistoricoEscolar.Secretario.Rg);
                        rptHistoricoEscolarFundamental.SetParameterValue("gdae", ensinoAluno.HistoricoEscolar.Gdae);
                        rptHistoricoEscolarFundamental.SetParameterValue("fundamentacao", ensinoAluno.HistoricoEscolar.Fundamentacao);
                        rptHistoricoEscolarFundamental.SetParameterValue("obs", ensinoAluno.HistoricoEscolar.Observacao);
                        if (ensinoAluno.HistoricoEscolar.SegundaVia) rptHistoricoEscolarFundamental.SetParameterValue("2via", "sim");
                        else rptHistoricoEscolarFundamental.SetParameterValue("2via", "não");
                        DateTime dtConclusao = DateTime.Now;
                        DateTime.TryParse(ensinoAluno.HistoricoEscolar.DtConclusao, out dtConclusao);

                        rptHistoricoEscolarFundamental.SetParameterValue("certi",
                    "O Diretor do CEEJA PROFESSORA MERTILA LARCHER DE MORAES, CERTIFICA," +
                    " nos termos inciso VII, Art. 24 da Lei Federal 9394/96, que " + objAluno.Nome + " RG: " + objAluno.Rg + "/" + objAluno.UfRg + " concluiu o Ensino Fundamental - " +
                    "Educação de Jovens e Adultos, Atendimento Individualizado e Presença Flexivel, no ano de " + dtConclusao.ToString("yyyy"));
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
            }

            return rptHistoricoEscolarFundamental;
        }

        public static rptHistoricoEscolarMedio rptHistoricoEscolarMedio(Aluno objAluno)
        {
            rptHistoricoEscolarMedio rptHistoricoEscolarMedio = new rptHistoricoEscolarMedio();

            try
            {
                #region Preencher Dados Médias
                DataTable dt = new DataTable("LISTA_MEDIA_HISTORICO_ESCOLAR");
                dt.Columns.Add("dis");
                dt.Columns.Add("ins");
                dt.Columns.Add("mu");
                dt.Columns.Add("uf");
                dt.Columns.Add("dt");
                dt.Columns.Add("not");

                //Definindo Ensino Aluno do Histórico Escolas
                EnsinoAluno ensinoAluno = new EnsinoAluno();
                foreach (EnsinoAluno _ensinoAluno in objAluno.ListaEnsinoAluno)
                {
                    if (_ensinoAluno.Ensino == Enumeradores.Ensino.MÉDIO)
                    {
                        ensinoAluno = _ensinoAluno;
                        //break;
                    }
                }

                //Dados Médias das Disciplinas
                foreach (DisciplinaAluno disciplinaAluno in ensinoAluno.ListaDisciplinaAluno.OrderBy(d => d.Disciplina.Ordem))
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["dis"] = disciplinaAluno.Disciplina.NomeHistorico;
                    if (disciplinaAluno.Media != null)
                    {
                        dt.Rows[dt.Rows.Count - 1]["ins"] = disciplinaAluno.Media.Instituicao;
                        dt.Rows[dt.Rows.Count - 1]["mu"] = disciplinaAluno.Media.Cidade.Nome;
                        dt.Rows[dt.Rows.Count - 1]["uf"] = disciplinaAluno.Media.Cidade.Uf.Sigla;
                        dt.Rows[dt.Rows.Count - 1]["dt"] = DateTime.Parse(disciplinaAluno.Media.DtMedia).ToString("dd/MM/yyyy");
                        dt.Rows[dt.Rows.Count - 1]["not"] = disciplinaAluno.Media.Valor;
                    }
                    else
                    {
                        dt.Rows[dt.Rows.Count - 1]["ins"] = "----";
                        dt.Rows[dt.Rows.Count - 1]["mu"] = "----";
                        dt.Rows[dt.Rows.Count - 1]["uf"] = "----";
                        dt.Rows[dt.Rows.Count - 1]["dt"] = "----";
                        dt.Rows[dt.Rows.Count - 1]["not"] = "----";
                    }
                }


                rptHistoricoEscolarMedio.SetDataSource(dt);
                #endregion

                rptHistoricoEscolarMedio.SetParameterValue("nome", objAluno.Nome);
                rptHistoricoEscolarMedio.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
                rptHistoricoEscolarMedio.SetParameterValue("data_relatorio", DateTime.Now.ToString("dd/MM/yyyy"));
                rptHistoricoEscolarMedio.SetParameterValue("nasc_estado", objAluno.LocalNascimento.Cidade.Uf.Nome);
                rptHistoricoEscolarMedio.SetParameterValue("nasc_cidade", objAluno.LocalNascimento.Cidade.Nome);
                rptHistoricoEscolarMedio.SetParameterValue("nasc_pais", objAluno.LocalNascimento.Cidade.Uf.Pais.Nome);
                rptHistoricoEscolarMedio.SetParameterValue("ra", objAluno.Ra);
                DateTime dtNascimento = DateTime.Now;
                DateTime.TryParse(objAluno.DtNascimento, out dtNascimento);
                rptHistoricoEscolarMedio.SetParameterValue("dat_nasc", dtNascimento.ToString("dd/MM/yyyy"));
                rptHistoricoEscolarMedio.SetParameterValue("n_mat", objAluno.NMatricula);
                rptHistoricoEscolarMedio.SetParameterValue("mae", objAluno.NomeMae);

                //Dados Obrigatórios Histórico Escolar
                rptHistoricoEscolarMedio.SetParameterValue("diretor", ensinoAluno.HistoricoEscolar.Diretor.Nome);
                rptHistoricoEscolarMedio.SetParameterValue("rg_diretor", ensinoAluno.HistoricoEscolar.Diretor.Rg);
                rptHistoricoEscolarMedio.SetParameterValue("secretario", ensinoAluno.HistoricoEscolar.Secretario.Nome);
                rptHistoricoEscolarMedio.SetParameterValue("rg_secretario", ensinoAluno.HistoricoEscolar.Secretario.Rg);
                rptHistoricoEscolarMedio.SetParameterValue("gdae", ensinoAluno.HistoricoEscolar.Gdae);
                rptHistoricoEscolarMedio.SetParameterValue("fundamentacao", ensinoAluno.HistoricoEscolar.Fundamentacao);

                //Dados Opcionais Histórico Escolar
                if (ensinoAluno.HistoricoEscolar.AnoAnterior > 0)
                {
                    rptHistoricoEscolarMedio.SetParameterValue("ano_anterior", ensinoAluno.HistoricoEscolar.AnoAnterior);
                }
                else
                {
                    rptHistoricoEscolarMedio.SetParameterValue("ano_anterior", "");

                }

                if (ensinoAluno.HistoricoEscolar.SerieAnterior != null)
                {
                    rptHistoricoEscolarMedio.SetParameterValue("serie_anterior", ensinoAluno.HistoricoEscolar.SerieAnterior);
                }
                else
                {
                    rptHistoricoEscolarMedio.SetParameterValue("serie_anterior", "");
                }

                if (ensinoAluno.HistoricoEscolar.InstituicaoAnterior != null)
                {
                    rptHistoricoEscolarMedio.SetParameterValue("instituicao_anterior", ensinoAluno.HistoricoEscolar.InstituicaoAnterior);
                }
                else
                {
                    rptHistoricoEscolarMedio.SetParameterValue("instituicao_anterior", "");
                }

                if (ensinoAluno.HistoricoEscolar.CidadeAnterior.Nome != null && ensinoAluno.HistoricoEscolar.CidadeAnterior.Uf.Nome != null)
                {
                    rptHistoricoEscolarMedio.SetParameterValue("municipio_anterior", ensinoAluno.HistoricoEscolar.CidadeAnterior.Nome);
                    rptHistoricoEscolarMedio.SetParameterValue("uf_anterior", ensinoAluno.HistoricoEscolar.CidadeAnterior.Uf.Sigla);

                }
                else
                {
                    rptHistoricoEscolarMedio.SetParameterValue("municipio_anterior", "");
                    rptHistoricoEscolarMedio.SetParameterValue("uf_anterior", "");

                }


                rptHistoricoEscolarMedio.SetParameterValue("obs", ensinoAluno.HistoricoEscolar.Observacao);

                if (ensinoAluno.HistoricoEscolar.SegundaVia)
                {
                    rptHistoricoEscolarMedio.SetParameterValue("2via", "sim");
                }
                else
                {
                    rptHistoricoEscolarMedio.SetParameterValue("2via", "não");
                }

                DateTime dtConclusao = DateTime.Now;
                DateTime.TryParse(ensinoAluno.HistoricoEscolar.DtConclusao, out dtConclusao);


                rptHistoricoEscolarMedio.SetParameterValue("certi",
            "O Diretor do CEEJA PROFESSORA MERTILA LARCHER DE MORAES, CERTIFICA," +
            " nos termos inciso VII, Art. 24 da Lei Federal 9394/96, que " + objAluno.Nome + " RG: " + objAluno.Rg + "/" + objAluno.UfRg + " concluiu o Ensino Médio - " +
            "Educação de Jovens e Adultos, Atendimento Individualizado e Presença Flexível, no ano de " + dtConclusao.ToString("yyyy"));

            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                ErrorLog.ErrorHandleService.ExibirMsgBoxError("Verificar todos os dados necessários para emitir o Histórico Escolar");
            }

            return rptHistoricoEscolarMedio;
        }

        public static rptRaLote rptRaVotorantimLote(List<Aluno> listaDeAlunos, IEmatriculaSettings settings)
        {
            rptRaLote cr = new rptRaLote();
            try
            {
                List<BinaryReader> lista_binarys = new List<BinaryReader>();

                DataTable dtAluno = new DataTable();
                dtAluno.TableName = "RA_CONTINUO";

                DataSet dsaluno = new DataSet();

                //TODO:: Verificar sé é possivel criar um objeto a partir do dataset pronto

                dtAluno.Columns.Add("nome");
                dtAluno.Columns.Add("rg");
                dtAluno.Columns.Add("ensino");
                dtAluno.Columns.Add("n_mat");
                dtAluno.Columns.Add("validade");

                dtAluno.Columns.Add("foto", System.Type.GetType("System.Byte[]"));
                dtAluno.Columns.Add("barcode", System.Type.GetType("System.Byte[]"));

                foreach (var aluno in listaDeAlunos)
                {
                    #region DADOS

                    DataRow row = dtAluno.NewRow();
                    row["nome"] = aluno.Nome;
                    row["rg"] = aluno.Rg;
                    row["ensino"] = Aluno.GetEnsinoAlunoAtual(aluno).Ensino;
                    row["n_mat"] = aluno.NMatricula;
                    row["validade"] = "Validade: " + DateTime.Now.ToString("MMM") + "/" + (DateTime.Now.Year + 1);

                    #endregion

                    #region FOTO

                    FotoAluno FotoDoAluno = new FotoAluno(aluno.NMatricula, settings);

                    if (FotoDoAluno != null)
                    {
                        if (File.Exists(FotoDoAluno.Caminho))
                        {
                            FileStream fs = new FileStream(FotoDoAluno.Caminho, FileMode.Open);
                            BinaryReader br = new BinaryReader(fs);
                            row["foto"] = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length));
                            br = null;
                            fs.Close();
                        }
                    }

                    #endregion

                    #region BARCODE

                    if (File.Exists(settings.DiretorioBarcode + @"/" + aluno.NMatricula + ".png"))
                    {
                        string imagePath = Path.GetFullPath(settings.DiretorioBarcode + @"/" + aluno.NMatricula + ".png");

                        if (imagePath != string.Empty) //Verificar se dah pra eliminar essa verificação
                        {
                            FileStream fs = new FileStream(imagePath, FileMode.Open);
                            BinaryReader br = new BinaryReader(fs);
                            row["barcode"] = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length));
                            br = null;
                            fs.Close();
                        }
                    }
                    else
                    {
                        //TODO - Feature Removida - Barcode
                        
                        //Gerar Barcode
                        //Utils.geraBarcode.SalvarBarcode(aluno.NMatricula, settings);

                        string imagePath = Path.GetFullPath(settings.DiretorioBarcode + @"/" + aluno.NMatricula + ".png");
                        //imgpath = imagePath; //Verificar se é gerado a cada foreach

                        if (imagePath != string.Empty) //Verificar se dah pra eliminar essa verificação
                        {
                            FileStream fs = new FileStream(imagePath, FileMode.Open);
                            BinaryReader br = new BinaryReader(fs);
                            row["barcode"] = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length));
                            br = null;
                            fs.Close();
                        }
                    }

                    #endregion

                    dtAluno.Rows.Add(row);
                }

                dsaluno.Tables.Add(dtAluno);

                cr.SetDataSource(dsaluno);
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.Message);
                return null; //VERIFICAR MÉTODO RETORNANDO NULL (É UMA SOLUÇÃO?)
            }
            return cr;
        }

        #region Métodos Auxiliares
        private static string primeira_caixa_alta(string palavra)
        {
            try
            {
                if (palavra != null)
                {
                    string aaa = palavra.Replace("  ", string.Empty);

                    string a = "";

                    string[] aa = aaa.Split(' ');

                    for (int i = 0; i < aa.Length; i++)
                    {
                        if (aa[i] != string.Empty)
                        {
                            if (a != string.Empty) a += " ";

                            a += aa[i].Substring(0, 1).ToUpper();
                            a += aa[i].Remove(0, 1).ToLower();
                        }
                    }
                    return a;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static string data_relatorio(DateTime data_relatorio_)
        {
            try
            {
                string strdata = "";

                if (data_relatorio_.ToString("dd/MM/yyyy") == "01/01/0001")
                    data_relatorio_ = DateTime.Now;

                strdata = data_relatorio_.ToString("'Votorantim,' dd 'de' MMMM 'de' yyyy");

                return strdata;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion
    }
}
