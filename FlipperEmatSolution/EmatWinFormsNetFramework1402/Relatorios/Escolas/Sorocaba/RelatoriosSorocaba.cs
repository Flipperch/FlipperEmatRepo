using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using EmatWinFormsNetFramework1402.Classes;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.Relatorios.Escolas.Sorocaba
{
    class RelatoriosSorocaba
    {
        public static atestado_eliminacao_sorocaba gera_crystal_atestado_eliminacao_sorocaba(Classes.Aluno objAluno, DateTime dtRelatorio)
        {
            atestado_eliminacao_sorocaba cr = new atestado_eliminacao_sorocaba();
            try
            {
                #region Preencher Dados Eliminações
                DataTable dt = new DataTable();
                dt.Columns.Add("dis");
                dt.Columns.Add("ins");
                dt.Columns.Add("mu");
                dt.Columns.Add("uf");
                dt.Columns.Add("dt");
                dt.Columns.Add("not");

                foreach (Classes.DisciplinaAluno disciplinaAluno in Classes.Aluno.GetEnsinoAlunoAtual(objAluno).ListaDisciplinaAluno)
                {
                    if (disciplinaAluno.Concluida)
                    {
                        if (disciplinaAluno.Media != null)
                        {
                            DataRow dr;
                            dr = dt.NewRow();
                            dr["dis"] = disciplinaAluno.Disciplina.NomeHistorico;
                            dr["ins"] = disciplinaAluno.Media.Instituicao;
                            dr["mu"] = disciplinaAluno.Media.Cidade.Nome;
                            dr["uf"] = disciplinaAluno.Media.Cidade.Uf.Sigla;
                            dr["dt"] = DateTime.Parse(disciplinaAluno.Media.DtMedia).ToString("dd/MM/yyyy");
                            dr["not"] = disciplinaAluno.Media.Valor;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                cr.SetDataSource(dt);
                #endregion

                #region preencher Dados Aluno
                cr.SetParameterValue("n_mat", objAluno.NMatricula);
                cr.SetParameterValue("nome", objAluno.Nome);
                cr.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
                cr.SetParameterValue("ensino", "ENSINO " + Classes.Aluno.GetEnsinoAlunoAtual(objAluno).Ensino);
                cr.SetParameterValue("nasc_cidade", objAluno.LocalNascimento.Cidade.Nome);
                cr.SetParameterValue("nasc_estado", objAluno.LocalNascimento.Cidade.Uf.Nome);
                cr.SetParameterValue("dat_nasc", objAluno.DtNascimento);
                cr.SetParameterValue("data_relatorio", data_relatorio(dtRelatorio));

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cr;
        }       

        public static declaracao_etec_sorocaba gera_crystal_declaracao_etec_sorocaba(Classes.Aluno objAluno, DateTime dtRelatorio)
        {
            declaracao_etec_sorocaba cr = new declaracao_etec_sorocaba();
            try
            {
                cr.SetParameterValue("n_mat", objAluno.NMatricula);
                cr.SetParameterValue("nome", objAluno.Nome);
                cr.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
                cr.SetParameterValue("ensino_atual", "ENSINO " + Classes.Aluno.GetEnsinoAlunoAtual(objAluno).Ensino);
                cr.SetParameterValue("termo", objAluno.TermoMatricula);
                cr.SetParameterValue("dat_matricula", objAluno.DtMatricula.ToString("dd/MM/yyyy"));
                if (Classes.Aluno.GetEnsinoAlunoAtual(objAluno).ListaRematricula.Count > 0)
                {
                    cr.SetParameterValue("ultima_rematricula", " e rematrícula em " + Classes.Aluno.GetEnsinoAlunoAtual(objAluno).ListaRematricula[0].Data.ToString("dd/MM/yyyy"));
                }
                else cr.SetParameterValue("ultima_rematricula", "");
                cr.SetParameterValue("data_relatorio", data_relatorio(dtRelatorio));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return cr;
        }

        private class frequencias
        {
            public string mes { get; set; }
            public List<string> list_frequencia_ { get; set; }
            public string frequencia { get; set; }
        }

        public static declaracao_inss_sorocaba gera_crystal_declaracao_inss_sorocaba(
            Classes.Aluno objAluno, DateTime dat_ini, DateTime dat_fim, DateTime dtRelatorio)
        {
            declaracao_inss_sorocaba cr = new declaracao_inss_sorocaba();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            #region Frequencia
            //Montar tabela de frequencia
            List<Classes.AtendimentoAluno> list_ = new List<Classes.AtendimentoAluno>();

            foreach (Classes.DisciplinaAluno disciplinaAluno in Classes.Aluno.GetEnsinoAlunoAtual(objAluno).ListaDisciplinaAluno)
            {
                foreach (Classes.AtendimentoAluno atendimentoAluno in disciplinaAluno.ListaAtendimentoAluno)
                {
                    if (atendimentoAluno.DtDoAtendimento >= dat_ini && atendimentoAluno.DtDoAtendimento <= dat_fim)
                    {
                        list_.Add(atendimentoAluno);
                    }
                }
            }

            List<frequencias> list_frequencias = new List<frequencias>();

            for (int i = 0; i < list_.Count; i++)
            {
                //Verificar se MÊS existe na lista => foi incluído.
                if (list_frequencias.Exists(x => x.mes == list_[i].DtDoAtendimento.ToString("MMMM/yy")))
                {
                    //Loop percorre para encontrar posição.
                    for (int z = 0; z < list_frequencias.Count; z++)
                    {
                        //Loop verificação para encontrar posição.
                        if (list_frequencias[z].mes == list_[i].DtDoAtendimento.ToString("MMMM/yy"))
                        {

                            //Verificar se dia não existe na lista de frequencia.
                            if (!list_frequencias[z].list_frequencia_.Exists(x => x == list_[i].DtDoAtendimento.ToString("dd")))
                            {
                                list_frequencias[z].list_frequencia_.Add(list_[i].DtDoAtendimento.ToString("dd"));
                            }

                        }
                    }
                }
                else
                {
                    frequencias obj_frequencia = new frequencias();
                    obj_frequencia.mes = list_[i].DtDoAtendimento.ToString("MMMM/yy");
                    obj_frequencia.list_frequencia_ = new List<string>();
                    obj_frequencia.list_frequencia_.Add(list_[i].DtDoAtendimento.ToString("dd"));
                    list_frequencias.Add(obj_frequencia);
                }
            }

            string sequencia_frequencia = "";
            for (int i = 0; i < list_frequencias.Count; i++)
            {
                for (int z = 0; z < list_frequencias[i].list_frequencia_.Count; z++)
                {
                    list_frequencias[i].frequencia += list_frequencias[i].list_frequencia_[z];

                    if (z == list_frequencias[i].list_frequencia_.Count - 2)
                        list_frequencias[i].frequencia += " e ";
                    else
                    {
                        if (z != list_frequencias[i].list_frequencia_.Count - 1)
                            list_frequencias[i].frequencia += ", ";
                    }
                }

            }

            DataTable dt = new DataTable();
            dt.TableName = "FREQUENCIAS";
            dt.Columns.Add("MES");
            dt.Columns.Add("FREQUENCIA");

            for (int i = 0; i < list_frequencias.Count; i++)
            {
                dt.Rows.Add();
                dt.Rows[i]["MES"] = list_frequencias[i].mes;
                dt.Rows[i]["FREQUENCIA"] = list_frequencias[i].frequencia;
            }

            cr.SetDataSource(dt);

            #endregion

            cr.SetParameterValue("n_mat", objAluno.NMatricula);
            cr.SetParameterValue("nome", objAluno.Nome);
            cr.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
            cr.SetParameterValue("ensino_atual", "ENSINO " + Classes.Aluno.GetEnsinoAlunoAtual(objAluno).Ensino);
            cr.SetParameterValue("data_matricula", objAluno.DtMatricula);
            cr.SetParameterValue("dat_ini", dat_ini.ToString("MMMM/yyyy"));

            List<DisciplinaAluno> listEncerradas = Classes.Aluno.GetEnsinoAlunoAtual(objAluno).ListaDisciplinaAluno.Where(x => x.Concluida == true).ToList();
            List<DisciplinaAluno> listFazer = Classes.Aluno.GetEnsinoAlunoAtual(objAluno).ListaDisciplinaAluno.Where(x => x.Concluida == false).ToList();

            string sequencia_disciplinas_encerradas = String.Join(", ", listEncerradas.Select(x => x.Disciplina.Nome));
            string sequencia_disciplinas_restantes = String.Join(", ", listFazer.Select(x => x.Disciplina.Nome));

            cr.SetParameterValue("sequencia_disciplinas_eliminadas", sequencia_disciplinas_encerradas);
            cr.SetParameterValue("sequencia_disciplinas_restantes",  sequencia_disciplinas_restantes);
            cr.SetParameterValue("data_relatorio", data_relatorio(dtRelatorio));

            return cr;
        }

        public static declaracao_conclusao_sorocaba gera_crystal_declaracao_conclusao_sorocaba(Classes.Aluno objAluno, DateTime dtRelatorio, EnsinoAluno ensinoSelecionado)
        {
            declaracao_conclusao_sorocaba cr = new declaracao_conclusao_sorocaba();
            try
            {
                cr.SetParameterValue("n_mat", objAluno.NMatricula);
                cr.SetParameterValue("nome", objAluno.Nome);
                cr.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
                cr.SetParameterValue("ensinoSelecionado", "ENSINO " + ensinoSelecionado.Ensino);
                cr.SetParameterValue("data_relatorio", data_relatorio(dtRelatorio));

                if (DateTime.TryParse(ensinoSelecionado.DtTermino, out DateTime dtConclusao))
                {
                    cr.SetParameterValue("dat_con", dtConclusao.ToString("dd/MM/yyyy"));
                }
                else
                {
                    List<AtendimentoAluno> listaAtendimentoAluno = new List<AtendimentoAluno>();
                    string ultimaMedia = "----";

                    foreach (DisciplinaAluno disciplinaAluno in ensinoSelecionado.ListaDisciplinaAluno)
                    {
                        foreach(AtendimentoAluno atendimentoAluno in disciplinaAluno.ListaAtendimentoAluno)
                        {
                            listaAtendimentoAluno.Add(atendimentoAluno);
                        }
                    }

                    ultimaMedia = listaAtendimentoAluno.OrderByDescending(x => x.DtDoAtendimento).First().DtDoAtendimento.ToString("dd/MM/yyyy");
                                        
                    cr.SetParameterValue("dat_con", ultimaMedia);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return cr;
        }

        public static declaracao_matricula_sorocaba gera_crystal_declaracao_matricula_sorocaba(Classes.Aluno objAluno, DateTime dtRelatorio)
        {
            declaracao_matricula_sorocaba cr = new declaracao_matricula_sorocaba();
            try
            {
                cr.SetParameterValue("n_mat", objAluno.NMatricula);
                cr.SetParameterValue("nome", objAluno.Nome);
                cr.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
                cr.SetParameterValue("ensino_atual", "ENSINO " + Classes.Aluno.GetEnsinoAlunoAtual(objAluno).Ensino.ToString());
                cr.SetParameterValue("data_relatorio", data_relatorio(dtRelatorio));
                return cr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            
        }

        public static historico_m_sorocaba gera_crystal_historico_m_sorocaba(Classes.Aluno objAluno)
        {
            historico_m_sorocaba cr = new historico_m_sorocaba();
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
                    if (ensinoAluno.Ensino == Enumeradores.Ensino.MÉDIO)
                    {
                        List<DisciplinaAluno> listaDisciplinaOrdenada = new List<DisciplinaAluno>();
                        listaDisciplinaOrdenada = ensinoAluno.ListaDisciplinaAluno.OrderBy(x => x.Disciplina.Ordem).ToList();
                        foreach (DisciplinaAluno disciplinaAluno in listaDisciplinaOrdenada)
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
                                dt.Rows[dt.Rows.Count - 1]["ins"] = "------";
                                dt.Rows[dt.Rows.Count - 1]["mu"] = "------";
                                dt.Rows[dt.Rows.Count - 1]["uf"] = "------";
                                dt.Rows[dt.Rows.Count - 1]["dt"] = "------";
                                dt.Rows[dt.Rows.Count - 1]["not"] = "------";
                            }
                        }
                    }
                }
                cr.SetDataSource(dt);
                #endregion
                #region Preencher Dados Alunos
                cr.SetParameterValue("n_mat", objAluno.NMatricula);
                cr.SetParameterValue("nome", objAluno.Nome);
                cr.SetParameterValue("mae", objAluno.NomeMae);
                cr.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
                cr.SetParameterValue("ra", objAluno.Ra);
                cr.SetParameterValue("nasc_cidade", objAluno.LocalNascimento.Cidade.Nome);
                cr.SetParameterValue("nasc_estado", objAluno.LocalNascimento.Cidade.Uf.Nome);
                cr.SetParameterValue("nasc_pais", objAluno.LocalNascimento.Cidade.Uf.Pais.Nome);
                DateTime dat_;
                if (DateTime.TryParse(objAluno.DtNascimento, out dat_))
                {
                    cr.SetParameterValue("dat_nasc", dat_.ToString("dd/MM/yyyy"));
                }
                else
                {
                    cr.SetParameterValue("dat_nasc", DateTime.Now.ToString("dd/MM/yyyy"));
                }
                #endregion
                #region Prencher dados do Historico Escolar
                foreach (EnsinoAluno ensinoAluno in objAluno.ListaEnsinoAluno)
                {
                    if (ensinoAluno.Ensino == Enumeradores.Ensino.MÉDIO)
                    {
                        DateTime.TryParse(ensinoAluno.DtInicio, out DateTime dtInicioEnsino);
                        cr.SetParameterValue("ini_curso", "O Aluno iniciou o curso em " + dtInicioEnsino.ToString("dd/MM/yyyy"));

                        cr.SetParameterValue("diretor", ensinoAluno.HistoricoEscolar.Diretor.Nome);
                        cr.SetParameterValue("rg_diretor", ensinoAluno.HistoricoEscolar.Diretor.Rg);
                        cr.SetParameterValue("secretario", ensinoAluno.HistoricoEscolar.Secretario.Nome);
                        cr.SetParameterValue("rg_secretario", ensinoAluno.HistoricoEscolar.Secretario.Rg);
                        cr.SetParameterValue("gdae", ensinoAluno.HistoricoEscolar.Gdae);
                        //cr.SetParameterValue("fundamentacao", ensinoAluno.HistoricoEscolar.Fundamentacao);
                        cr.SetParameterValue("ano_anterior", ensinoAluno.HistoricoEscolar.AnoAnterior);
                        cr.SetParameterValue("instituicao_anterior", ensinoAluno.HistoricoEscolar.InstituicaoAnterior);
                        if (ensinoAluno.HistoricoEscolar.CidadeAnterior != null)
                        {
                            cr.SetParameterValue("municipio_anterior", ensinoAluno.HistoricoEscolar.CidadeAnterior.Nome);
                            cr.SetParameterValue("uf_anterior", ensinoAluno.HistoricoEscolar.CidadeAnterior.Uf.Sigla);
                        }
                        else
                        {
                            cr.SetParameterValue("municipio_anterior", string.Empty);
                            cr.SetParameterValue("uf_anterior", string.Empty);
                        }

                        cr.SetParameterValue("serie_anterior", ensinoAluno.HistoricoEscolar.SerieAnterior);

                        DateTime dtConc = DateTime.Now;
                        DateTime.TryParse(ensinoAluno.HistoricoEscolar.DtConclusao, out dtConc);
                        
                        cr.SetParameterValue("obs", ensinoAluno.HistoricoEscolar.Observacao);
                        string certi = "O Diretor do C. E. E. J. A. PROF. NORBERTO SOARES RAMOS,  " +
                            "CERTIFICA, nos termos do Inciso do VII, Artigo 24 da Lei Federal 9394/96, que " + objAluno.Nome +
                            ", RG " + objAluno.Rg + ", Concluiu o Ensino Médio - Modalidade " +
                            "Educação de Jovens e Adultos, Atendimento Individualizado e Presença Flexível, no ano de " + dtConc.ToString("yyyy") + ".";
                        cr.SetParameterValue("certi", certi);

                        if (ensinoAluno.HistoricoEscolar.SegundaVia)
                            cr.SetParameterValue("2via", "Sim");
                        else
                            cr.SetParameterValue("2via", "Não");

                    }
                }
                cr.SetParameterValue("data_relatorio", DateTime.Now.ToString("dd/MM/yyyy"));

                
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return cr;
        }

        public static historico_f_sorocaba gera_crystal_historico_f_sorocaba(Classes.Aluno objAluno)
        {
            historico_f_sorocaba cr = new historico_f_sorocaba();
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
                        List<DisciplinaAluno> listaDisciplinaOrdenada = new List<DisciplinaAluno>();
                        listaDisciplinaOrdenada = ensinoAluno.ListaDisciplinaAluno.OrderBy(x => x.Disciplina.Ordem).ToList();
                        foreach (DisciplinaAluno disciplinaAluno in listaDisciplinaOrdenada)
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
                                dt.Rows[dt.Rows.Count - 1]["ins"] = "------";
                                dt.Rows[dt.Rows.Count - 1]["mu"] = "------";
                                dt.Rows[dt.Rows.Count - 1]["uf"] = "------";
                                dt.Rows[dt.Rows.Count - 1]["dt"] = "------";
                                dt.Rows[dt.Rows.Count - 1]["not"] = "------";
                            }

                            if (disciplinaAluno.Disciplina.Nome == "PORTUGUÊS/LITERATURA")
                            {
                                dt.Rows[dt.Rows.Count - 1]["dis"] = "PORTUGUÊS";
                            }
                        }
                    }
                }
                cr.SetDataSource(dt);
                #endregion

                #region preencher Dados Aluno
                cr.SetParameterValue("n_mat", objAluno.NMatricula);
                cr.SetParameterValue("nome", objAluno.Nome);
                cr.SetParameterValue("mae", objAluno.NomeMae);
                cr.SetParameterValue("rg", objAluno.Rg + "/" + objAluno.UfRg);
                cr.SetParameterValue("ra", objAluno.Ra);
                cr.SetParameterValue("nasc_cidade", objAluno.LocalNascimento.Cidade.Nome);
                cr.SetParameterValue("nasc_estado", objAluno.LocalNascimento.Cidade.Uf.Nome);
                cr.SetParameterValue("nasc_pais", objAluno.LocalNascimento.Cidade.Uf.Pais.Nome);
                DateTime dat_;
                if (DateTime.TryParse(objAluno.DtNascimento, out dat_))
                {
                    cr.SetParameterValue("dat_nasc", dat_.ToString("dd/MM/yyyy"));
                }
                else
                {
                    cr.SetParameterValue("dat_nasc", DateTime.Now.ToString("dd/MM/yyyy"));
                }
                #endregion

                #region Preencher Dados Histórico
                cr.SetParameterValue("data_relatorio", DateTime.Now.ToString("dd/MM/yyyy"));
                foreach (EnsinoAluno ensinoAluno in objAluno.ListaEnsinoAluno)
                {
                    if (ensinoAluno.Ensino == Enumeradores.Ensino.FUNDAMENTAL)
                    {
                        DateTime.TryParse(ensinoAluno.DtInicio, out DateTime dtInicioEnsino);
                        cr.SetParameterValue("ini_curso", "O Aluno iniciou o curso em " + dtInicioEnsino.ToString("dd/MM/yyyy"));


                        cr.SetParameterValue("diretor", ensinoAluno.HistoricoEscolar.Diretor.Nome);
                        cr.SetParameterValue("rg_diretor", ensinoAluno.HistoricoEscolar.Diretor.Rg);
                        cr.SetParameterValue("secretario", ensinoAluno.HistoricoEscolar.Secretario.Nome);
                        cr.SetParameterValue("rg_secretario", ensinoAluno.HistoricoEscolar.Secretario.Rg);
                        cr.SetParameterValue("gdae", ensinoAluno.HistoricoEscolar.Gdae);
                        //cr.SetParameterValue("fundamentacao", ensinoAluno.HistoricoEscolar.Fundamentacao);


                        cr.SetParameterValue("obs", ensinoAluno.HistoricoEscolar.Observacao);

                        DateTime dtConc = DateTime.Now;
                        DateTime.TryParse(ensinoAluno.HistoricoEscolar.DtConclusao, out dtConc);

                        string certi = "O Diretor do C. E. E. J. A. PROF. NORBERTO SOARES RAMOS,  CERTIFICA, " +
                            "nos termos do Inciso do VII, Artigo 24 da Lei Federal 9394/96, que " + objAluno.Nome +
                            ", RG " + objAluno.Rg + ", Concluiu o Ensino Fundamental - Modalidade Educação de Jovens" +
                            " e Adultos, Atendimento Individualizado e Presença Flexível, no ano de " +
                            dtConc.ToString("yyyy") + ".";
                        cr.SetParameterValue("certi", certi);

                        if (ensinoAluno.HistoricoEscolar.SegundaVia)
                            cr.SetParameterValue("2via", "Sim");
                        else
                            cr.SetParameterValue("2via", "Não");
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return cr;
        }

        public static ra_sorocaba gera_crystal_ra_sorocaba(Classes.Aluno objAluno)
        {
            string imgpath = "";
            ra_sorocaba cr = new ra_sorocaba();
            try
            {
                #region Foto

                
                if (objAluno.FotoDoAluno.Caminho != null)
                {
                    string imagePath = System.IO.Path.GetFullPath(objAluno.FotoDoAluno.Caminho);
                    imgpath = imagePath;
                }

                DataTable dtFoto = new DataTable();
                dtFoto.TableName = "FOTOS";

                if (imgpath != string.Empty)
                {
                    dtFoto.Columns.Add("FOTO", System.Type.GetType("System.Byte[]"));
                    FileStream fs = new FileStream(imgpath, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);

                    DataRow row = dtFoto.NewRow();
                    row[0] = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length));
                    dtFoto.Rows.Add(row);
                    br = null;
                    fs.Close();
                }

                DataSet dsaluno = new DataSet();
                dsaluno.Tables.Add(dtFoto);
                cr.SetDataSource(dsaluno);

                #endregion

                #region Preencher Dados Aluno

                cr.SetParameterValue("n_mat", objAluno.NMatricula);
                cr.SetParameterValue("nome", objAluno.Nome);
                cr.SetParameterValue("rg", objAluno.Rg);
                cr.SetParameterValue("ra", objAluno.Ra);
                cr.SetParameterValue("ensino", Aluno.GetEnsinoAlunoAtual(objAluno).Ensino.ToString());

                cr.SetParameterValue("VALIDADE", "Validade: dez/" + DateTime.Now.ToString("yyyy"));

                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }

            
            return cr;
        }

        public static rptRequerimentoMatricula rptRequerimentoMatricula(Classes.Aluno objAluno, DateTime dtRelatorio, string cidade, string uf)
        {
            rptRequerimentoMatricula rptRequerimentoMatricula = new rptRequerimentoMatricula();
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            #region Foto
            string imgpath = "";
            
            if (objAluno.FotoDoAluno.Caminho != null)
            {
                string imagePath = System.IO.Path.GetFullPath(objAluno.FotoDoAluno.Caminho);
                //foto = Image.FromFile(imagePath);
                imgpath = imagePath;
            }

            DataTable dtFoto = new DataTable();
            dtFoto.TableName = "FOTOS";

            if (imgpath != string.Empty)
            {
                dtFoto.Columns.Add("FOTO", System.Type.GetType("System.Byte[]"));
                FileStream fs = new FileStream(imgpath, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);

                DataRow row = dtFoto.NewRow();
                row[0] = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length));
                dtFoto.Rows.Add(row);
                br = null;
                fs.Close();
            }

            DataSet dsaluno = new DataSet();
            dsaluno.Tables.Add(dtFoto);
            rptRequerimentoMatricula.SetDataSource(dsaluno);

            #endregion
            #region limpar campos
            rptRequerimentoMatricula.SetParameterValue("nMatricula", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("nome", string.Empty);
            //rptRequerimentoMatricula.SetParameterValue("nomeSocial", string.Empty);
            //rptRequerimentoMatricula.SetParameterValue("telefone", string.Empty);
            //rptRequerimentoMatricula.SetParameterValue("celular", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("rg", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("ufRg", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("ra", string.Empty);
            //rptRequerimentoMatricula.SetParameterValue("cpf", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("ensino", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("dataMatricula", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("termo", string.Empty);
            //rptRequerimentoMatricula.SetParameterValue("observacao", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("sexo", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("estadoCivil", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("cor", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("mae", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("dtNascimento", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("localNascimento", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("ufNascimento", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("paisNascimento", string.Empty);

            rptRequerimentoMatricula.SetParameterValue("cidade", string.Empty);
            rptRequerimentoMatricula.SetParameterValue("bairro", string.Empty);
            
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
                //if (objAluno.NomeSocial != string.Empty)
                    //rptRequerimentoMatricula.SetParameterValue("nomeSocial", " / " + objAluno.NomeSocial);
                rptRequerimentoMatricula.SetParameterValue("rg", objAluno.Rg);
                rptRequerimentoMatricula.SetParameterValue("ufRg", objAluno.UfRg);
                rptRequerimentoMatricula.SetParameterValue("ra", objAluno.Ra);
                //rptRequerimentoMatricula.SetParameterValue("cpf", objAluno.Cpf);
                rptRequerimentoMatricula.SetParameterValue("ensino", primeira_caixa_alta(Classes.Aluno.GetEnsinoAlunoAtual(objAluno).Ensino.ToString()));
                rptRequerimentoMatricula.SetParameterValue("dataMatricula", objAluno.DtMatricula.ToString("dd/MM/yyyy"));
                rptRequerimentoMatricula.SetParameterValue("termo", objAluno.TermoMatricula);
                //rptRequerimentoMatricula.SetParameterValue("observacao", objAluno.Observacao);
                rptRequerimentoMatricula.SetParameterValue("sexo", Enum.GetName(typeof(Enumeradores.Sexo), objAluno.Sexo));
                rptRequerimentoMatricula.SetParameterValue("estadoCivil", Enum.GetName(typeof(Enumeradores.EstadoCivil), objAluno.EstadoCivil));
                rptRequerimentoMatricula.SetParameterValue("cor", Enum.GetName(typeof(Enumeradores.CorOrigemEtnica), objAluno.CorOrigemEtnica));
                rptRequerimentoMatricula.SetParameterValue("mae", objAluno.NomeMae);
                rptRequerimentoMatricula.SetParameterValue("dtNascimento", objAluno.DtNascimento);
                if (objAluno.LocalNascimento != null)
                {
                    rptRequerimentoMatricula.SetParameterValue("localNascimento", objAluno.LocalNascimento.Cidade.Nome);
                    if (!String.IsNullOrWhiteSpace(objAluno.LocalNascimento.Cidade.Uf.Sigla))
                        rptRequerimentoMatricula.SetParameterValue("ufNascimento", objAluno.LocalNascimento.Cidade.Uf.Sigla);
                    else
                        rptRequerimentoMatricula.SetParameterValue("ufNascimento", objAluno.LocalNascimento.Cidade.Uf.Nome);
                    rptRequerimentoMatricula.SetParameterValue("paisNascimento", objAluno.LocalNascimento.Cidade.Uf.Pais.Nome);
                }
                if (objAluno.Endereco != null)
                {
                    if (objAluno.Endereco.Cidade.Nome != null)
                    {
                        rptRequerimentoMatricula.SetParameterValue("cidade", cidade );//objAluno.Endereco.Cidade.Nome
                        rptRequerimentoMatricula.SetParameterValue("bairro", objAluno.Endereco.Bairro);//objAluno.Endereco.Cidade.Uf.Sigla
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
                
                string contato_ = "";
                string tel_ = format_telefone(objAluno.Telefone);
                string cel_ = format_telefone(objAluno.Celular);
                if (tel_ != string.Empty)
                {
                    contato_ += "Tel. " + tel_;
                }
                if (cel_ != string.Empty)
                {
                    if (contato_ == string.Empty)
                        contato_ += "Cel. " + cel_;
                    else
                        contato_ += " - Cel. " + cel_;
                }
                rptRequerimentoMatricula.SetParameterValue("contato", contato_);
                rptRequerimentoMatricula.SetParameterValue("usuario", objAluno.Usuario.Nome);
                rptRequerimentoMatricula.SetParameterValue("dataRelatorio", data_relatorio(dtRelatorio));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rptRequerimentoMatricula;
        }
        
        /// <summary>
        /// Método para gerar etiquetas de prontuário. Layout 50,8mm x 101,6mm
        /// </summary>
        /// <param name="listaDeAlunos"></param>
        /// <returns></returns>
        public static rptEtiquetaProntuario50x101Carta GerarEtiquetaProntuario50x101Carta(List<Classes.Aluno> listaDeAlunos, IEmatriculaSettings settings)
        {
            try
            {
                rptEtiquetaProntuario50x101Carta rptEtiquetaProntuario50X101Carta = new rptEtiquetaProntuario50x101Carta();

                List<BinaryReader> lista_binarys = new List<BinaryReader>();

                DataTable dtEtiqueta = new DataTable();
                dtEtiqueta.TableName = "ETIQUETA";

                dtEtiqueta.Columns.Add("FOTO", Type.GetType("System.Byte[]"));
                dtEtiqueta.Columns.Add("N_MAT");
                dtEtiqueta.Columns.Add("RG");
                dtEtiqueta.Columns.Add("NOME");
                dtEtiqueta.Columns.Add("ENSINO");
                dtEtiqueta.Columns.Add("DAT_MAT");

                int z = 0;
                foreach (Aluno aluno in listaDeAlunos)
                {
                    dtEtiqueta.Rows.Add();
                    
                    dtEtiqueta.Rows[z]["NOME"] = "Nome: " + aluno.Nome;
                    dtEtiqueta.Rows[z]["RG"] = "RG: " + aluno.Rg;
                    dtEtiqueta.Rows[z]["ENSINO"] = "Ensino: " + Aluno.GetEnsinoAlunoAtual(aluno).Ensino;
                    dtEtiqueta.Rows[z]["N_MAT"] = "Nº de Matrícula: " + aluno.NMatricula;
                    dtEtiqueta.Rows[z]["DAT_MAT"] = "Data Matrícula: " + Aluno.GetEnsinoAlunoAtual(aluno).DtInicio;

                    aluno.FotoDoAluno = new FotoAluno(aluno.NMatricula, settings);

                    if (aluno.FotoDoAluno != null)
                    {
                        if (File.Exists(aluno.FotoDoAluno.Caminho))
                        {
                            FileStream fs = new FileStream(aluno.FotoDoAluno.Caminho, FileMode.Open);
                            BinaryReader br = new BinaryReader(fs);
                            dtEtiqueta.Rows[z]["FOTO"] = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length));
                            br = null;
                            fs.Close();
                        }
                    }

                    z++;
                 }

                DataSet dsEtiqueta = new DataSet();
                dsEtiqueta.Tables.Add(dtEtiqueta);
                rptEtiquetaProntuario50X101Carta.SetDataSource(dsEtiqueta);
                return rptEtiquetaProntuario50X101Carta;
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorHandleService.ExibirMsgBoxError(ex.ToString());
                throw;
            }
        }

        #region Métodos Auxiliares
        private static string primeira_caixa_alta(string palavra)
        {
            string a = "";
            try
            {
                if (palavra != null)
                {
                    string aaa = palavra.Replace("  ", string.Empty);

                    

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
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return a;
        }
        public static string data_relatorio(DateTime data_relatorio_)
        {
            string strdata = "";
            try
            {

                if (data_relatorio_.ToString("dd/MM/yyyy") == "01/01/0001")
                    data_relatorio_ = DateTime.Now;

                strdata = data_relatorio_.ToString("'Sorocaba,' dd 'de' MMMM 'de' yyyy");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strdata;
        }
        public static string format_telefone(string telefone)
        {
            string a = "";
            try
            {
                a = telefone;
                if (telefone.Length == 11)
                {
                    a = "(" + telefone.Substring(0, 2) + ")";
                    a += telefone.Substring(2, 5) + "-";
                    a += telefone.Substring(7);

                }
                if (telefone.Length == 10)
                {
                    a = "(" + telefone.Substring(0, 2) + ")";
                    a += telefone.Substring(2, 4) + "-";
                    a += telefone.Substring(6);
                }
                if (telefone.Length == 8)
                {
                    a = telefone.Substring(0, 4) + "-";
                    a += telefone.Substring(5);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return a;
        }
        #endregion
    }
}
