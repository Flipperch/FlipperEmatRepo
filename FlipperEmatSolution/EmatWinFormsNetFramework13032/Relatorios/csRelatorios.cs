using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

//using Microsoft.Reporting.WebForms;

using Microsoft.Reporting.WinForms;

using System.IO;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;

using EmatWinFormsNetFramework13032.Relatorios.sorocaba.crystal;
using EmatWinFormsNetFramework13032.Properties;


namespace EmatWinFormsNetFramework13032.Relatorios
{
    public class csRelatorios
    {
        public DateTime data_relatorio_ { get; set; }

        Alunos.csAlunos cs_alunos = new Alunos.csAlunos();
        Usuarios_Grupos.csUsuarios cs_usuarios = new Usuarios_Grupos.csUsuarios();
        Notas.csAtendimentos cs_atendimentos = new Notas.csAtendimentos();
        Notas.csDisciplinas cs_disciplina = new Notas.csDisciplinas();
        Notas.csEnsinos cs_ensinos = new Notas.csEnsinos();

        SqlConnection sql_conn = new SqlConnection(Conexoes.SqlConnectionString);
        SqlCommand sql_comm = new SqlCommand();

        public string nome_arquivo;

        public string aluno;
        public string ra;
        public string rg;
        public string sigla;
        public string nnmat;
        public string sexo;
        public string ensino;
        public string mae;
        public string pais;
        public string estado;
        public string cidade;
        public string raca;

        public string user_cad;

        public string est_civil;

        public string dat_nasc;

        public string bairro;
        public string res_end;
        public string res_pais;
        public string res_estado;
        public string res_cidade;
        public string cep;

        public int posicao_etiqueta;


        public string ano_con;

        public string Serie_ant;
        public string Estab_ant;
        public string Ano_ant;
        public string Cidade_ant;
        public string Uf_ant;
        public string Diretor;
        public string Rg_diretor;
        public string Secretario;
        public string Rg_secretario;
        public string dt_liv;
        public string Livro;
        public string Pag;
        public string Termo;
        public string dt_doc;
        public string Obs;
        public string res_numero;

        public string dat_con;

        public string pos_def;
        public string q_def;

        public string tel;
        public string cel;

        public string dat_mat;

        #region Dados relatorio Nota

        public List<string> list_disciplina = new List<string>();
        public List<string> list_dat_ini = new List<string>();
        public List<string> list_media = new List<string>();
        public List<string> list_dat_fin = new List<string>();
        public List<string> list_orientador = new List<string>();

        #endregion

        #region Professores

        public List<string> orie_list = new List<string>();

        #endregion

        #region OrientaçãoIni

        public List<string> dt_ini_list = new List<string>();

        #endregion

        public List<string> dis_list = new List<string>();
        public List<string> ins_list = new List<string>();
        public List<string> mu_list = new List<string>();
        public List<string> uf_list = new List<string>();
        public List<string> not_list = new List<string>();
        public List<string> dt_list = new List<string>();

        public string mat_adi = "";
        public string mat_adi_1 = "";
        public string mat_adi_2 = "";
        public string mat_adi_3 = "";

        //etiqueta
        public List<string> lista_n_mat = new List<string>();
        public List<string> lista_rg = new List<string>();
        public List<string> lista_aluno = new List<string>();
        public List<string> lista_ensino = new List<string>();

        public DataTable dt = new DataTable();

        Configurações.CSconnexoes_txt conf = new Configurações.CSconnexoes_txt();

        

        public ReportDataSource requerimento_1()
        {
            ReportDataSource repor = new ReportDataSource();

            ReportViewer reportviewer = new ReportViewer();

            reportviewer.ProcessingMode = ProcessingMode.Local;
            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.requerimento.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            listreportparameter.Add(new ReportParameter("n_mat", nnmat));
            listreportparameter.Add(new ReportParameter("nome", aluno));
            listreportparameter.Add(new ReportParameter("rg", rg));
            listreportparameter.Add(new ReportParameter("ra", ra));
            listreportparameter.Add(new ReportParameter("dat_mat", dat_mat));
            listreportparameter.Add(new ReportParameter("sigla", sigla));
            listreportparameter.Add(new ReportParameter("termo", Termo));
            //nascimento
            listreportparameter.Add(new ReportParameter("dat_nasc", dat_nasc));
            listreportparameter.Add(new ReportParameter("municipio", cidade));
            listreportparameter.Add(new ReportParameter("estado", estado));
            listreportparameter.Add(new ReportParameter("pais", pais));
            //endereço
            listreportparameter.Add(new ReportParameter("res_end", res_end));
            listreportparameter.Add(new ReportParameter("res_bairro", bairro));
            listreportparameter.Add(new ReportParameter("res_municipio", res_cidade));
            listreportparameter.Add(new ReportParameter("res_estado", res_estado));
            listreportparameter.Add(new ReportParameter("res_cep", cep));
            listreportparameter.Add(new ReportParameter("res_numero", res_numero));
            // pessoal
            listreportparameter.Add(new ReportParameter("est_civil", est_civil));
            listreportparameter.Add(new ReportParameter("sexo", sexo));
            listreportparameter.Add(new ReportParameter("raca", raca));
            listreportparameter.Add(new ReportParameter("mae", mae));
            listreportparameter.Add(new ReportParameter("cel", cel));
            listreportparameter.Add(new ReportParameter("tel", tel));
            listreportparameter.Add(new ReportParameter("def", pos_def));
            listreportparameter.Add(new ReportParameter("q_def", q_def));
            listreportparameter.Add(new ReportParameter("ensino", ensino));
            listreportparameter.Add(new ReportParameter("dat_cidade", "Sorocaba, " + DateTime.Now.ToShortDateString()));
            listreportparameter.Add(new ReportParameter("mat_por", "Matrícula Efetuada Por: " + user_cad));

            listreportparameter.Add(new ReportParameter("data", DateTime.Now.ToString("dd/MM/yyyy")));

            conf.get_configuracoes();

            try
            {
                reportviewer.LocalReport.Refresh();

                reportviewer.LocalReport.EnableExternalImages = true;

                if (File.Exists(conf.caminho_fotos + nnmat + ".png"))
                {
                    string imagePath = new Uri(conf.caminho_fotos + nnmat + ".png").AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("Path", imagePath);
                    reportviewer.LocalReport.SetParameters(parameter);
                }
                else if (File.Exists(conf.caminho_fotos + nnmat + ".jpg"))
                {
                    string imagePath = new Uri(conf.caminho_fotos + nnmat + ".jpg").AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("Path", imagePath);
                    reportviewer.LocalReport.SetParameters(parameter);
                }

                reportviewer.LocalReport.SetParameters(listreportparameter);



                repor.Value = reportviewer.DataBindings;

                //Warning[] warnings;
                //string[] streamids;
                //string mimeType;
                //string encoding;
                //string extension;

                //byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                //
                //FileStream filestreamPDF = null;
                //string nomeArquivoPDF = Path.GetTempPath() + "requerimento_" + nnmat + ".pdf";
                //
                //filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
                //filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
                //filestreamPDF.Close();
                //
                //nome_arquivo = nomeArquivoPDF;
                //
                //Process.Start(nomeArquivoPDF);


            }

            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }


            return repor;
        }

        //HISTORICO E CERTIFICADO
        public void gera_historicoM()
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.historicoM.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            string certi = "O Diretor do C. E. E. J. A. PROF. NORBERTO SOARES RAMOS,  CERTIFICA, nos termos do Inciso do VII, Artigo 24 da Lei Federal 9394/96, que " + aluno + ", RG " + rg + ", Concluiu o Ensino Médio - Modalidade Educação de Jovens e Adultos, Atendimento Individualizado e Presença Flexível, no ano de " + ano_con + ".";

            listreportparameter.Add(new ReportParameter("nome", aluno));
            listreportparameter.Add(new ReportParameter("rg", rg));
            listreportparameter.Add(new ReportParameter("ra", ra));
            listreportparameter.Add(new ReportParameter("municipio", cidade));
            listreportparameter.Add(new ReportParameter("mae", mae));
            listreportparameter.Add(new ReportParameter("estado", estado));
            listreportparameter.Add(new ReportParameter("pais", pais));
            listreportparameter.Add(new ReportParameter("certi", certi));
            listreportparameter.Add(new ReportParameter("data", DateTime.Now.ToString("dd/MM/yyyy")));
            listreportparameter.Add(new ReportParameter("serie_ant", Serie_ant));
            listreportparameter.Add(new ReportParameter("ins_ant", Estab_ant));
            listreportparameter.Add(new ReportParameter("ano_ant", Ano_ant));
            listreportparameter.Add(new ReportParameter("mu_ant", Cidade_ant));
            listreportparameter.Add(new ReportParameter("uf_ant", Uf_ant));
            listreportparameter.Add(new ReportParameter("diretor", Diretor));
            listreportparameter.Add(new ReportParameter("rg_diretor", Rg_diretor));
            listreportparameter.Add(new ReportParameter("secretario", Secretario));
            listreportparameter.Add(new ReportParameter("rg_secretario", Rg_secretario));
            listreportparameter.Add(new ReportParameter("ini_curso", "O Aluno Iniciou o Curso em " + DateTime.Parse(dat_mat).ToString("dd/MM/yyyy")));
            listreportparameter.Add(new ReportParameter("dat_nasc", dat_nasc));
            listreportparameter.Add(new ReportParameter("dat_doc", dt_doc));
            listreportparameter.Add(new ReportParameter("obs", Obs));
            listreportparameter.Add(new ReportParameter("nu_mat", nnmat));

            #region preencher historico

            for (int i = 0; i < ins_list.Count; i++)
            {
                listreportparameter.Add(new ReportParameter("dis_" + (i + 1), dis_list[i]));
                listreportparameter.Add(new ReportParameter("ins_" + (i + 1), ins_list[i]));
                listreportparameter.Add(new ReportParameter("mu_" + (i + 1), mu_list[i]));
                listreportparameter.Add(new ReportParameter("uf_" + (i + 1), uf_list[i]));
                listreportparameter.Add(new ReportParameter("not_" + (i + 1), not_list[i]));
                listreportparameter.Add(new ReportParameter("dt_" + (i + 1), dt_list[i]));
            }

            #endregion

            reportviewer.LocalReport.SetParameters(listreportparameter);


            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);



            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "Hist_Médio_" + nnmat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void gera_historicoF()
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.historicoF.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            string certi = "O Diretor do C. E. E. J. A. PROF. NORBERTO SOARES RAMOS,  CERTIFICA, nos termos do Inciso do VII, Artigo 24 da Lei Federal 9394/96, que " + aluno + ", RG " + rg + ", Concluiu o Ensino Fundamental - Modalidade Educação de Jovens e Adultos, Atendimento Individualizado e Presença Flexível, no ano de " + ano_con + ".";

            listreportparameter.Add(new ReportParameter("nome", aluno));
            listreportparameter.Add(new ReportParameter("rg", rg));
            listreportparameter.Add(new ReportParameter("ra", ra));
            listreportparameter.Add(new ReportParameter("municipio", cidade));
            listreportparameter.Add(new ReportParameter("mae", mae));
            listreportparameter.Add(new ReportParameter("estado", estado));
            listreportparameter.Add(new ReportParameter("pais", pais));
            listreportparameter.Add(new ReportParameter("certi", certi));
            listreportparameter.Add(new ReportParameter("data", DateTime.Now.ToString("dd/MM/yyyy")));
            listreportparameter.Add(new ReportParameter("serie_ant", Serie_ant));
            listreportparameter.Add(new ReportParameter("ins_ant", Estab_ant));
            listreportparameter.Add(new ReportParameter("ano_ant", Ano_ant));
            listreportparameter.Add(new ReportParameter("mu_ant", Cidade_ant));
            listreportparameter.Add(new ReportParameter("uf_ant", Uf_ant));
            listreportparameter.Add(new ReportParameter("diretor", Diretor));
            listreportparameter.Add(new ReportParameter("rg_diretor", Rg_diretor));
            listreportparameter.Add(new ReportParameter("secretario", Secretario));
            listreportparameter.Add(new ReportParameter("rg_secretario", Rg_secretario));
            listreportparameter.Add(new ReportParameter("ini_curso", "O Aluno Iniciou o Curso em " + DateTime.Parse(dat_mat).ToString("dd/MM/yyyy")));
            listreportparameter.Add(new ReportParameter("dat_nasc", dat_nasc));

            listreportparameter.Add(new ReportParameter("dat_doc", dt_doc));
            listreportparameter.Add(new ReportParameter("obs", Obs));
            listreportparameter.Add(new ReportParameter("nu_mat", nnmat));

            #region preencher historico



            for (int i = 0; i < dis_list.Count; i++)
            {
                listreportparameter.Add(new ReportParameter("dis_" + (i + 1), dis_list[i]));
                listreportparameter.Add(new ReportParameter("ins_" + (i + 1), ins_list[i]));
                listreportparameter.Add(new ReportParameter("mu_" + (i + 1), mu_list[i]));
                listreportparameter.Add(new ReportParameter("uf_" + (i + 1), uf_list[i]));
                listreportparameter.Add(new ReportParameter("not_" + (i + 1), not_list[i]));
                listreportparameter.Add(new ReportParameter("dt_" + (i + 1), dt_list[i]));
            }

            //Fixo Fundamentao - ALTERAR



            //int a = listreportparameter.IndexOf();



            //listreportparameter.Add(new ReportParameter("dis_1", ""));

            #endregion



            reportviewer.LocalReport.SetParameters(listreportparameter);


            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);



            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "Hist_Fundamental_" + nnmat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void gera_certi_M()
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.certificado_M.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            string certi1 = "O Diretor da Escola C.E.E.J.A.. \" Prof. Norberto Soares Ramos \", nos termos do inciso VII, artigo 24 Lei Federal 9394/96, confere a,";

            string certi2 = "RG:" + rg + ", nascido(a) em " + dat_nasc + ", natural do Município de " + cidade;
            string certi3 = "Estado de " + estado + ", o presente CERTIFICADO de conclusão do Ensino Médio - Modalidade de Jovens e";
            string certi4 = "Adultos, Atendimento Individualizado e Presença Flexível, em " + dat_con + ".";
            string certi5 = "Sorocaba, " + dt_doc + ".";
            string nome_escola = "Rua Assis Machado, 920 - Vila Assis - Sorocaba - Cep.: 18020-258 - Fone (15) 3221-6689";
            string ato = "ATO DE CRIAÇÃO: DECRETO Nº 26.149 DE 31/10/1986";

            listreportparameter.Add(new ReportParameter("nome", aluno));
            listreportparameter.Add(new ReportParameter("certi1", certi1));
            listreportparameter.Add(new ReportParameter("certi2", certi2));
            listreportparameter.Add(new ReportParameter("certi3", certi3));
            listreportparameter.Add(new ReportParameter("certi4", certi4));
            listreportparameter.Add(new ReportParameter("certi5", certi5));
            listreportparameter.Add(new ReportParameter("ato", ato));
            listreportparameter.Add(new ReportParameter("nome_escola", nome_escola));
            listreportparameter.Add(new ReportParameter("diretor", Diretor));
            listreportparameter.Add(new ReportParameter("rg_diretor", Rg_diretor));
            listreportparameter.Add(new ReportParameter("secretario", Secretario));
            listreportparameter.Add(new ReportParameter("rg_secretario", Rg_secretario));

            reportviewer.LocalReport.SetParameters(listreportparameter);


            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);



            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "Certificado_Médio_" + nnmat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void gera_certi_F()
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.certificado_F.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            string certi1 = "O Diretor da Escola C.E.E.J.A.. \" Prof. Norberto Soares Ramos \", nos termos do inciso VII, artigo 24 Lei Federal 9394/96, confere a,";

            string certi2 = "RG:" + rg + ", nascido(a) em " + dat_nasc + ", natural do Município de " + cidade;
            string certi3 = "Estado de " + estado + ", o presente CERTIFICADO de conclusão do Ensino Fundamental - Modalidade de Jovens e";
            string certi4 = "Adultos, Atendimento Individualizado e Presença Flexível, em " + dat_con + ".";
            string certi5 = "Sorocaba, " + dt_doc + ".";
            string nome_escola = "Rua Assis Machado, 920 - Vila Assis - Sorocaba - Cep.: 18020-258 - Fone (15) 3221-6689";
            string ato = "ATO DE CRIAÇÃO: DECRETO Nº 26.149 DE 31/10/1986";

            listreportparameter.Add(new ReportParameter("nome", aluno));
            listreportparameter.Add(new ReportParameter("certi1", certi1));
            listreportparameter.Add(new ReportParameter("certi2", certi2));
            listreportparameter.Add(new ReportParameter("certi3", certi3));
            listreportparameter.Add(new ReportParameter("certi4", certi4));
            listreportparameter.Add(new ReportParameter("certi5", certi5));
            listreportparameter.Add(new ReportParameter("ato", ato));
            listreportparameter.Add(new ReportParameter("nome_escola", nome_escola));
            listreportparameter.Add(new ReportParameter("diretor", Diretor));
            listreportparameter.Add(new ReportParameter("rg_diretor", Rg_diretor));
            listreportparameter.Add(new ReportParameter("secretario", Secretario));
            listreportparameter.Add(new ReportParameter("rg_secretario", Rg_secretario));

            reportviewer.LocalReport.SetParameters(listreportparameter);


            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);



            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "Certificado_Médio_" + nnmat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();

            Process.Start(nomeArquivoPDF);
        }

        public void gera_not_M()
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.notas_m.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();


            listreportparameter.Add(new ReportParameter("nome", aluno));
            listreportparameter.Add(new ReportParameter("rg", rg));
            listreportparameter.Add(new ReportParameter("data", DateTime.Now.ToString("dd/MM/yyyy")));
            listreportparameter.Add(new ReportParameter("nu_mat", nnmat));

            #region preencher notas

            for (int i = 0; i < list_disciplina.Count; i++)
            {
                listreportparameter.Add(new ReportParameter("dis_" + (i + 1), list_disciplina[i]));
                listreportparameter.Add(new ReportParameter("dt_ini_" + (i + 1), list_dat_ini[i]));
                listreportparameter.Add(new ReportParameter("not_" + (i + 1), list_media[i]));
                listreportparameter.Add(new ReportParameter("dt_" + (i + 1), list_dat_fin[i]));
                listreportparameter.Add(new ReportParameter("orie_" + (i + 1), list_orientador[i]));
            }

            #endregion

            reportviewer.LocalReport.SetParameters(listreportparameter);


            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);



            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "NOtas_Médio_" + nnmat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void gera_not_F()
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.notas_f.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();


            listreportparameter.Add(new ReportParameter("nome", aluno));
            listreportparameter.Add(new ReportParameter("rg", rg));
            listreportparameter.Add(new ReportParameter("data", DateTime.Now.ToString("dd/MM/yyyy")));
            listreportparameter.Add(new ReportParameter("nu_mat", nnmat));

            for (int i = 0; i < list_disciplina.Count; i++)
            {
                listreportparameter.Add(new ReportParameter("dis_" + (i + 1), list_disciplina[i]));
                listreportparameter.Add(new ReportParameter("dt_ini_" + (i + 1), list_dat_ini[i]));
                listreportparameter.Add(new ReportParameter("not_" + (i + 1), list_media[i]));
                listreportparameter.Add(new ReportParameter("dt_" + (i + 1), list_dat_fin[i]));
                listreportparameter.Add(new ReportParameter("orie_" + (i + 1), list_orientador[i]));
            }

            reportviewer.LocalReport.SetParameters(listreportparameter);


            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);



            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "Notas_Fundamental_" + nnmat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void gera_relatorio(DataTable table)
        {
            //Relatório Atendimento diário por disciplina e por período
            //n_mat, nome, ensino, disciplina, orientador, data_atendimento, tipo_atendimnento  



            //Relatório de evasão;
            //n_mat, nome, ensino, disciplina atual, 

            //Relatório de Matricula mensal
            //n_mat, nome, data_mat

            //Disciplinas Concluidas
            //n_mat, nome, média_final, diciplina

            //Concluintes
            //Mensal 
            //n_mat, nome, status_concluido




            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.relatorio_filtro.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            //listreportparameter.Add(new ReportParameter("titulo_lista", "LISTA DE ALUNOS " + ativados_inativados));


            ReportDataSource rds = new ReportDataSource("data_2", table);

            reportviewer.LocalReport.DataSources.Clear();
            reportviewer.LocalReport.DataSources.Add(rds);

            //Popular uma lista no relatorio para apresentar os numeros a serem alterados. (impressão)

            //reportviewer.LocalReport.EnableExternalImages = true;

            //Configurações.CSconnexoes_txt conf = new Configurações.CSconnexoes_txt();
            //conf.get_configuracoes();

            reportviewer.LocalReport.SetParameters(listreportparameter);

            reportviewer.LocalReport.Refresh();


            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "lista_alteracoes" + nnmat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void limpar_temp(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);

            }
        }

        #region Relatorio Geral

        public void gera_relatorio_geral(string dat_ini, string dat_fin, string qtd_total, string qtd_evadidos, bool total)
        {
            Notas.csAtendimentos cs_atendimentos = new Notas.csAtendimentos();

            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.relatorio_geral.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            string certi = "";

            listreportparameter.Add(new ReportParameter("qtd_total", qtd_total));
            listreportparameter.Add(new ReportParameter("data_gerado", DateTime.Now.ToString()));

            //Definir datas
            string data = "";
            string data_dados = "";



            if (dat_ini != "")
            {
                if (dat_fin != "")
                {
                    data = "Dados entre " + dat_ini + " Até " + dat_fin;
                    data_dados = dat_ini + "' AND '" + dat_fin;
                }
                else
                {
                    data = "Dados de " + dat_ini;
                    data_dados = dat_ini + "' AND '" + dat_ini;
                }

                listreportparameter.Add(new ReportParameter("data", data));
            }
            else
            {
                listreportparameter.Add(new ReportParameter("data", "GERAL"));
            }

            listreportparameter.Add(new ReportParameter("qtd_evadidos", qtd_evadidos));


            #region Paramentros

            #region qtd atend - OK
            string por_fun_qtd_atend = cs_atendimentos.qtd_atendimentos(1, data_dados, total, 0).ToString();
            string por_med_qtd_atend = cs_atendimentos.qtd_atendimentos(1, data_dados, total, 1).ToString();
            string his_fun_qtd_atend = cs_atendimentos.qtd_atendimentos(4, data_dados, total, 0).ToString();
            string his_med_qtd_atend = cs_atendimentos.qtd_atendimentos(4, data_dados, total, 1).ToString();
            string geo_fun_qtd_atend = cs_atendimentos.qtd_atendimentos(12, data_dados, total, 0).ToString();
            string geo_med_qtd_atend = cs_atendimentos.qtd_atendimentos(12, data_dados, total, 1).ToString();
            string mat_fun_qtd_atend = cs_atendimentos.qtd_atendimentos(2, data_dados, total, 0).ToString();
            string mat_med_qtd_atend = cs_atendimentos.qtd_atendimentos(2, data_dados, total, 1).ToString();
            string ing_fun_qtd_atend = cs_atendimentos.qtd_atendimentos(7, data_dados, total, 0).ToString();
            string ing_med_qtd_atend = cs_atendimentos.qtd_atendimentos(7, data_dados, total, 1).ToString();
            string art_fun_qtd_atend = cs_atendimentos.qtd_atendimentos(8, data_dados, total, 0).ToString();
            string art_med_qtd_atend = cs_atendimentos.qtd_atendimentos(8, data_dados, total, 1).ToString();
            string cie_qtd_atend = cs_atendimentos.qtd_atendimentos(11, data_dados, total, 0).ToString();
            string fis_qtd_atend = cs_atendimentos.qtd_atendimentos(5, data_dados, total, 1).ToString();
            string qui_qtd_atend = cs_atendimentos.qtd_atendimentos(3, data_dados, total, 1).ToString();
            string bio_qtd_atend = cs_atendimentos.qtd_atendimentos(6, data_dados, total, 1).ToString();
            string fil_qtd_atend = cs_atendimentos.qtd_atendimentos(9, data_dados, total, 1).ToString();
            string soc_qtd_atend = cs_atendimentos.qtd_atendimentos(10, data_dados, total, 1).ToString();
            //subtotais
            string por_total_qtd_atend = cs_atendimentos.qtd_atendimentos(1, data_dados, total).ToString();
            string his_total_qtd_atend = cs_atendimentos.qtd_atendimentos(4, data_dados, total).ToString();
            string geo_total_qtd_atend = cs_atendimentos.qtd_atendimentos(12, data_dados, total).ToString();
            string mat_total_qtd_atend = cs_atendimentos.qtd_atendimentos(2, data_dados, total).ToString();
            string ing_total_qtd_atend = cs_atendimentos.qtd_atendimentos(7, data_dados, total).ToString();
            string art_total_qtd_atend = cs_atendimentos.qtd_atendimentos(8, data_dados, total).ToString();
            //Total
            string total_qtd_atend = cs_atendimentos.qtd_atendimentos(9, data_dados, total).ToString();
            #endregion

            #region Concluintes
            string por_fun_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 1, 0).ToString();
            string por_med_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 1, 1).ToString();
            string his_fun_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 4, 0).ToString();
            string his_med_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 4, 1).ToString();
            string geo_fun_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 12, 0).ToString();
            string geo_med_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 12, 1).ToString();
            string mat_fun_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 2, 0).ToString();
            string mat_med_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 2, 1).ToString();
            string ing_fun_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 7, 0).ToString();
            string ing_med_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 7, 1).ToString();
            string art_fun_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 8, 0).ToString();
            string art_med_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 8, 1).ToString();
            string cie_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 11, 0).ToString();
            string fis_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 5, 1).ToString();
            string qui_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 3, 1).ToString();
            string bio_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 6, 1).ToString();
            string fil_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 9, 1).ToString();
            string soc_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 10, 1).ToString();
            //subtotais                                                                                 
            string por_total_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 1).ToString();
            string his_total_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 4).ToString();
            string geo_total_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 12).ToString();
            string mat_total_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 2).ToString();
            string ing_total_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 7).ToString();
            string art_total_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total, 8).ToString();
            //Total
            string total_con = cs_atendimentos.qtd_alunos_ini_ret_con("ENCERRADO", data_dados, total).ToString();
            #endregion

            #region Iniciantes
            string por_fun_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 1, 0).ToString();
            string por_med_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 1, 1).ToString();
            string his_fun_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 4, 0).ToString();
            string his_med_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 4, 1).ToString();
            string geo_fun_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 12, 0).ToString();
            string geo_med_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 12, 1).ToString();
            string mat_fun_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 2, 0).ToString();
            string mat_med_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 2, 1).ToString();
            string ing_fun_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 7, 0).ToString();
            string ing_med_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 7, 1).ToString();
            string art_fun_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 8, 0).ToString();
            string art_med_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 8, 1).ToString();
            string cie_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 11, 0).ToString();
            string fis_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 5, 1).ToString();
            string qui_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 3, 1).ToString();
            string bio_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 6, 1).ToString();
            string fil_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 9, 1).ToString();
            string soc_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 10, 1).ToString();
            //Subtotais                                                                                          
            string por_total_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 1).ToString();
            string his_total_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 4).ToString();
            string geo_total_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 12).ToString();
            string mat_total_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 2).ToString();
            string ing_total_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 7).ToString();
            string art_total_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total, 8).ToString();
            //Total
            string total_ini = cs_atendimentos.qtd_alunos_ini_ret_con("ORIENTAÇÃO INICIAL", data_dados, total).ToString();
            #endregion

            #region Retorno
            string por_fun_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 1, 0).ToString();
            string por_med_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 1, 1).ToString();
            string his_fun_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 4, 0).ToString();
            string his_med_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 4, 1).ToString();
            string geo_fun_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 12, 0).ToString();
            string geo_med_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 12, 1).ToString();
            string mat_fun_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 2, 0).ToString();
            string mat_med_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 2, 1).ToString();
            string ing_fun_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 7, 0).ToString();
            string ing_med_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 7, 1).ToString();
            string art_fun_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 8, 0).ToString();
            string art_med_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 8, 1).ToString();
            string cie_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 11, 0).ToString();
            string fis_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 5, 1).ToString();
            string qui_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 3, 1).ToString();
            string bio_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 6, 1).ToString();
            string fil_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 9, 1).ToString();
            string soc_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 10, 1).ToString();
            //Subtotais                                                                               
            string por_total_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 1).ToString();
            string his_total_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 4).ToString();
            string geo_total_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 12).ToString();
            string mat_total_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 2).ToString();
            string ing_total_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 7).ToString();
            string art_total_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total, 8).ToString();
            //Total
            string total_ret = cs_atendimentos.qtd_alunos_ini_ret_con("RETORNO", data_dados, total).ToString();
            #endregion

            #region Total Disciplina
            string por_fun_total = cs_atendimentos.qtd_alunos("FUNDAMENTAL", 1).ToString();
            string por_med_total = cs_atendimentos.qtd_alunos("MÉDIO", 1).ToString();
            string his_fun_total = cs_atendimentos.qtd_alunos("FUNDAMENTAL", 4).ToString();
            string his_med_total = cs_atendimentos.qtd_alunos("MÉDIO", 4).ToString();
            string geo_fun_total = cs_atendimentos.qtd_alunos("FUNDAMENTAL", 12).ToString();
            string geo_med_total = cs_atendimentos.qtd_alunos("MÉDIO", 12).ToString();
            string mat_fun_total = cs_atendimentos.qtd_alunos("FUNDAMENTAL", 2).ToString();
            string mat_med_total = cs_atendimentos.qtd_alunos("MÉDIO", 2).ToString();
            string ing_fun_total = cs_atendimentos.qtd_alunos("FUNDAMENTAL", 7).ToString();
            string ing_med_total = cs_atendimentos.qtd_alunos("MÉDIO", 7).ToString();
            string art_fun_total = cs_atendimentos.qtd_alunos("FUNDAMENTAL", 8).ToString();
            string art_med_total = cs_atendimentos.qtd_alunos("MÉDIO", 8).ToString();
            string cie_total = cs_atendimentos.qtd_alunos("FUNDAMENTAL", 11).ToString();
            string fis_total = cs_atendimentos.qtd_alunos("MÉDIO", 5).ToString();
            string qui_total = cs_atendimentos.qtd_alunos("MÉDIO", 3).ToString();
            string bio_total = cs_atendimentos.qtd_alunos("MÉDIO", 6).ToString();
            string fil_total = cs_atendimentos.qtd_alunos("MÉDIO", 9).ToString();
            string soc_total = cs_atendimentos.qtd_alunos("MÉDIO", 10).ToString();
            //Subtotal Disciplina
            string por_total = cs_atendimentos.qtd_alunos("AMBOS", 1).ToString();
            string his_total = cs_atendimentos.qtd_alunos("AMBOS", 4).ToString();
            string geo_total = cs_atendimentos.qtd_alunos("AMBOS", 12).ToString();
            string mat_total = cs_atendimentos.qtd_alunos("AMBOS", 2).ToString();
            string ing_total = cs_atendimentos.qtd_alunos("AMBOS", 7).ToString();
            string art_total = cs_atendimentos.qtd_alunos("AMBOS", 8).ToString();
            //Total
            string total_discs = cs_atendimentos.qtd_alunos("AMBOS", 0).ToString();
            #endregion

            #endregion

            //total disciplina
            listreportparameter.Add(new ReportParameter("por_fun_total", por_fun_total));
            listreportparameter.Add(new ReportParameter("por_med_total", por_med_total));
            listreportparameter.Add(new ReportParameter("his_fun_total", his_fun_total));
            listreportparameter.Add(new ReportParameter("his_med_total", his_med_total));
            listreportparameter.Add(new ReportParameter("geo_fun_total", geo_fun_total));
            listreportparameter.Add(new ReportParameter("geo_med_total", geo_med_total));
            listreportparameter.Add(new ReportParameter("mat_fun_total", mat_fun_total));
            listreportparameter.Add(new ReportParameter("mat_med_total", mat_med_total));
            listreportparameter.Add(new ReportParameter("ing_fun_total", ing_fun_total));
            listreportparameter.Add(new ReportParameter("ing_med_total", ing_med_total));
            listreportparameter.Add(new ReportParameter("art_fun_total", art_fun_total));
            listreportparameter.Add(new ReportParameter("art_med_total", art_med_total));
            listreportparameter.Add(new ReportParameter("cie_total", cie_total));
            listreportparameter.Add(new ReportParameter("fis_total", fis_total));
            listreportparameter.Add(new ReportParameter("qui_total", qui_total));
            listreportparameter.Add(new ReportParameter("bio_total", bio_total));
            listreportparameter.Add(new ReportParameter("fil_total", fil_total));
            listreportparameter.Add(new ReportParameter("soc_total", soc_total));
            //Sub
            listreportparameter.Add(new ReportParameter("por_total", por_total));
            listreportparameter.Add(new ReportParameter("his_total", his_total));
            listreportparameter.Add(new ReportParameter("geo_total", geo_total));
            listreportparameter.Add(new ReportParameter("mat_total", mat_total));
            listreportparameter.Add(new ReportParameter("ing_total", ing_total));
            listreportparameter.Add(new ReportParameter("art_total", art_total));
            //Total
            listreportparameter.Add(new ReportParameter("total_discs", total_discs));

            //concluinte
            listreportparameter.Add(new ReportParameter("por_fun_con", por_fun_con));
            listreportparameter.Add(new ReportParameter("por_med_con", por_med_con));
            listreportparameter.Add(new ReportParameter("his_fun_con", his_fun_con));
            listreportparameter.Add(new ReportParameter("his_med_con", his_med_con));
            listreportparameter.Add(new ReportParameter("geo_fun_con", geo_fun_con));
            listreportparameter.Add(new ReportParameter("geo_med_con", geo_med_con));
            listreportparameter.Add(new ReportParameter("mat_fun_con", mat_fun_con));
            listreportparameter.Add(new ReportParameter("mat_med_con", mat_med_con));
            listreportparameter.Add(new ReportParameter("ing_fun_con", ing_fun_con));
            listreportparameter.Add(new ReportParameter("ing_med_con", ing_med_con));
            listreportparameter.Add(new ReportParameter("art_fun_con", art_fun_con));
            listreportparameter.Add(new ReportParameter("art_med_con", art_med_con));
            listreportparameter.Add(new ReportParameter("cie_con", cie_con));
            listreportparameter.Add(new ReportParameter("fis_con", fis_con));
            listreportparameter.Add(new ReportParameter("qui_con", qui_con));
            listreportparameter.Add(new ReportParameter("bio_con", bio_con));
            listreportparameter.Add(new ReportParameter("fil_con", fil_con));
            listreportparameter.Add(new ReportParameter("soc_con", soc_con));
            //Sub
            listreportparameter.Add(new ReportParameter("por_total_con", por_total_con));
            listreportparameter.Add(new ReportParameter("his_total_con", his_total_con));
            listreportparameter.Add(new ReportParameter("geo_total_con", geo_total_con));
            listreportparameter.Add(new ReportParameter("mat_total_con", mat_total_con));
            listreportparameter.Add(new ReportParameter("ing_total_con", ing_total_con));
            listreportparameter.Add(new ReportParameter("art_total_con", art_total_con));
            //Total
            listreportparameter.Add(new ReportParameter("total_con", total_con));

            ////ini
            listreportparameter.Add(new ReportParameter("por_fun_ini", por_fun_ini));
            listreportparameter.Add(new ReportParameter("por_med_ini", por_med_ini));
            listreportparameter.Add(new ReportParameter("his_fun_ini", his_fun_ini));
            listreportparameter.Add(new ReportParameter("his_med_ini", his_med_ini));
            listreportparameter.Add(new ReportParameter("geo_fun_ini", geo_fun_ini));
            listreportparameter.Add(new ReportParameter("geo_med_ini", geo_med_ini));
            listreportparameter.Add(new ReportParameter("mat_fun_ini", mat_fun_ini));
            listreportparameter.Add(new ReportParameter("mat_med_ini", mat_med_ini));
            listreportparameter.Add(new ReportParameter("ing_fun_ini", ing_fun_ini));
            listreportparameter.Add(new ReportParameter("ing_med_ini", ing_med_ini));
            listreportparameter.Add(new ReportParameter("art_fun_ini", art_fun_ini));
            listreportparameter.Add(new ReportParameter("art_med_ini", art_med_ini));
            listreportparameter.Add(new ReportParameter("cie_ini", cie_ini));
            listreportparameter.Add(new ReportParameter("fis_ini", fis_ini));
            listreportparameter.Add(new ReportParameter("qui_ini", qui_ini));
            listreportparameter.Add(new ReportParameter("bio_ini", bio_ini));
            listreportparameter.Add(new ReportParameter("fil_ini", fil_ini));
            listreportparameter.Add(new ReportParameter("soc_ini", soc_ini));
            //Sub
            listreportparameter.Add(new ReportParameter("por_total_ini", por_total_ini));
            listreportparameter.Add(new ReportParameter("his_total_ini", his_total_ini));
            listreportparameter.Add(new ReportParameter("geo_total_ini", geo_total_ini));
            listreportparameter.Add(new ReportParameter("mat_total_ini", mat_total_ini));
            listreportparameter.Add(new ReportParameter("ing_total_ini", ing_total_ini));
            listreportparameter.Add(new ReportParameter("art_total_ini", art_total_ini));
            //Total
            listreportparameter.Add(new ReportParameter("total_ini", total_ini));


            ////retorno
            listreportparameter.Add(new ReportParameter("por_fun_ret", por_fun_ret));
            listreportparameter.Add(new ReportParameter("por_med_ret", por_med_ret));
            listreportparameter.Add(new ReportParameter("his_fun_ret", his_fun_ret));
            listreportparameter.Add(new ReportParameter("his_med_ret", his_med_ret));
            listreportparameter.Add(new ReportParameter("geo_fun_ret", geo_fun_ret));
            listreportparameter.Add(new ReportParameter("geo_med_ret", geo_med_ret));
            listreportparameter.Add(new ReportParameter("mat_fun_ret", mat_fun_ret));
            listreportparameter.Add(new ReportParameter("mat_med_ret", mat_med_ret));
            listreportparameter.Add(new ReportParameter("ing_fun_ret", ing_fun_ret));
            listreportparameter.Add(new ReportParameter("ing_med_ret", ing_med_ret));
            listreportparameter.Add(new ReportParameter("art_fun_ret", art_fun_ret));
            listreportparameter.Add(new ReportParameter("art_med_ret", art_med_ret));
            listreportparameter.Add(new ReportParameter("cie_ret", cie_ret));
            listreportparameter.Add(new ReportParameter("fis_ret", fis_ret));
            listreportparameter.Add(new ReportParameter("qui_ret", qui_ret));
            listreportparameter.Add(new ReportParameter("bio_ret", bio_ret));
            listreportparameter.Add(new ReportParameter("fil_ret", fil_ret));
            listreportparameter.Add(new ReportParameter("soc_ret", soc_ret));
            //Sub
            listreportparameter.Add(new ReportParameter("por_total_ret", por_total_ret));
            listreportparameter.Add(new ReportParameter("his_total_ret", his_total_ret));
            listreportparameter.Add(new ReportParameter("geo_total_ret", geo_total_ret));
            listreportparameter.Add(new ReportParameter("mat_total_ret", mat_total_ret));
            listreportparameter.Add(new ReportParameter("ing_total_ret", ing_total_ret));
            listreportparameter.Add(new ReportParameter("art_total_ret", art_total_ret));
            //Total
            listreportparameter.Add(new ReportParameter("total_ret", total_ret));

            ////qt_atend
            listreportparameter.Add(new ReportParameter("por_fun_qtd_atend", por_fun_qtd_atend));
            listreportparameter.Add(new ReportParameter("por_med_qtd_atend", por_med_qtd_atend));
            listreportparameter.Add(new ReportParameter("his_fun_qtd_atend", his_fun_qtd_atend));
            listreportparameter.Add(new ReportParameter("his_med_qtd_atend", his_med_qtd_atend));
            listreportparameter.Add(new ReportParameter("geo_fun_qtd_atend", geo_fun_qtd_atend));
            listreportparameter.Add(new ReportParameter("geo_med_qtd_atend", geo_med_qtd_atend));
            listreportparameter.Add(new ReportParameter("mat_fun_qtd_atend", mat_fun_qtd_atend));
            listreportparameter.Add(new ReportParameter("mat_med_qtd_atend", mat_med_qtd_atend));
            listreportparameter.Add(new ReportParameter("ing_fun_qtd_atend", ing_fun_qtd_atend));
            listreportparameter.Add(new ReportParameter("ing_med_qtd_atend", ing_med_qtd_atend));
            listreportparameter.Add(new ReportParameter("art_fun_qtd_atend", art_fun_qtd_atend));
            listreportparameter.Add(new ReportParameter("art_med_qtd_atend", art_med_qtd_atend));
            listreportparameter.Add(new ReportParameter("cie_qtd_atend", cie_qtd_atend));
            listreportparameter.Add(new ReportParameter("fis_qtd_atend", fis_qtd_atend));
            listreportparameter.Add(new ReportParameter("qui_qtd_atend", qui_qtd_atend));
            listreportparameter.Add(new ReportParameter("bio_qtd_atend", bio_qtd_atend));
            listreportparameter.Add(new ReportParameter("fil_qtd_atend", fil_qtd_atend));
            listreportparameter.Add(new ReportParameter("soc_qtd_atend", soc_qtd_atend));
            //Sub
            listreportparameter.Add(new ReportParameter("por_total_qtd_atend", por_total_qtd_atend));
            listreportparameter.Add(new ReportParameter("his_total_qtd_atend", his_total_qtd_atend));
            listreportparameter.Add(new ReportParameter("geo_total_qtd_atend", geo_total_qtd_atend));
            listreportparameter.Add(new ReportParameter("mat_total_qtd_atend", mat_total_qtd_atend));
            listreportparameter.Add(new ReportParameter("ing_total_qtd_atend", ing_total_qtd_atend));
            listreportparameter.Add(new ReportParameter("art_total_qtd_atend", art_total_qtd_atend));
            //Total
            listreportparameter.Add(new ReportParameter("total_qtd_atend", total_qtd_atend));

            //listreportparameter.Add(new ReportParameter("ra", ra));


            #region preencher lista de disciplinas

            //for (int i = 0; i < 12; i++)
            //{
            //    listreportparameter.Add(new ReportParameter("disciplina_" + (i + 1), ins_list[i]));
            //    listreportparameter.Add(new ReportParameter("_" + (i + 1), mu_list[i]));
            //    listreportparameter.Add(new ReportParameter("uf_" + (i + 1), uf_list[i]));
            //    listreportparameter.Add(new ReportParameter("not_" + (i + 1), not_list[i]));
            //    listreportparameter.Add(new ReportParameter("dt_" + (i + 1), dt_list[i]));
            //}

            #endregion



            reportviewer.LocalReport.SetParameters(listreportparameter);


            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);



            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "Hist_Médio_" + nnmat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        //public List<string> definir_data()
        //{
        //    //bool
        //    //lista - true 

        //    List<string> data_where = new List<string>();

        //    //Periodo
        //    string data_filtro_ini = "";
        //    string data_filtro_fin = "";

        //    data_filtro_ini = dtpAtend_ini.Value.ToString("dd/MM/yyyy");
        //    data_filtro_fin = dtpAtend_fin.Value.ToString("dd/MM/yyyy");

        //    if (!lista)
        //    {
        //        if (!dtpAtend_fin.Checked)
        //        {
        //            data_where.Add(data_filtro_ini + "' AND '" + data_filtro_ini);
        //        }
        //        else
        //        {
        //            data_where.Add(data_filtro_ini + "' AND '" + data_filtro_fin);
        //        }
        //    }
        //    else
        //    {
        //        int data_count = (DateTime.Parse(data_filtro_fin).Day - DateTime.Parse(data_filtro_ini).Day) + 1;

        //        DateTime data = DateTime.Parse(data_filtro_ini);


        //        for (int i = 0; i < data_count; i++)
        //        {
        //            data_where.Add(data.ToString("dd/MM/yyyy") + "' AND '" + data.ToString("dd/MM/yyyy"));
        //            data = data.AddDays(1);
        //        }
        //    }
        //    return data_where;
        //}

        //public List<string> definir_periodo(bool lista, List<string> data)
        //{
        //    string a = "";
        //    List<string> periodo_where = new List<string>();

        //    //Periodo            
        //    if (cmbSel_periodo.Text == "MANHÃ")
        //    {
        //        //MANHÃ 
        //        for (int i = 0; i < data.Count; i++)
        //        {
        //            a = data[i].Replace("' AND", " 08:00:00' AND");
        //            periodo_where.Add("DATA_ATENDIMENTO between '" + a + " 11:59:59'");
        //        }
        //    }
        //    else if (cmbSel_periodo.Text == "TARDE")
        //    {
        //        //TARDE 
        //        for (int i = 0; i < data.Count; i++)
        //        {
        //            a = data[i].Replace("' AND", " 12:00:00' AND");
        //            periodo_where.Add("DATA_ATENDIMENTO between '" + a + " 17:59:59'");
        //        }
        //    }
        //    else if (cmbSel_periodo.Text == "NOITE")
        //    {
        //        //NOITE 
        //        for (int i = 0; i < data.Count; i++)
        //        {
        //            a = data[i].Replace("' AND", " 18:00:00' AND");
        //            periodo_where.Add("DATA_ATENDIMENTO between '" + a + " 21:59:59'");
        //        }
        //    }
        //    else
        //    {
        //        //TODOS
        //        periodo_where.Add("DATA_ATENDIMENTO between '" + data[0] + " 21:59:59'");
        //    }

        //    return periodo_where;

        //}

        #endregion

        #region Novo Passaporte notas



        #endregion

        #region Lista Alterações

        public void gera_lista_inativacao(DataTable table)
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.lista_inativacao.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            listreportparameter.Add(new ReportParameter("titulo_lista", "LISTA DE ALUNOS INATIVADOS"));
            listreportparameter.Add(new ReportParameter("data_impr", DateTime.Now.ToString()));



            ReportDataSource rds = new ReportDataSource("data", table);

            reportviewer.LocalReport.DataSources.Clear();
            reportviewer.LocalReport.DataSources.Add(rds);

            //Popular uma lista no relatorio para apresentar os numeros a serem alterados. (impressão)

            //reportviewer.LocalReport.EnableExternalImages = true;

            //Configurações.CSconnexoes_txt conf = new Configurações.CSconnexoes_txt();
            //conf.get_configuracoes();

            reportviewer.LocalReport.SetParameters(listreportparameter);

            reportviewer.LocalReport.Refresh();


            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "lista_alteracoes" + nnmat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        #endregion

        //Sorocaba 
        #region Relatórios Sorocaba ReportViewer

        public void gera_ra_sorocaba(string n_mat, string aluno, string ra, string rg)
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.ra_sorocaba.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            listreportparameter.Add(new ReportParameter("Nome", "Nome: " + aluno));
            
            listreportparameter.Add(new ReportParameter("RG", "RG: " + rg));
            listreportparameter.Add(new ReportParameter("validade", "Validade:"));
            listreportparameter.Add(new ReportParameter("n_mat", "Nº de Matricula: " + n_mat));

            reportviewer.LocalReport.EnableExternalImages = true;

            conf.get_configuracoes();

            if (File.Exists(conf.caminho_fotos + n_mat + ".png"))
            {
                string imagePath = new Uri(conf.caminho_fotos + n_mat + ".png").AbsoluteUri;
                //string imagePath = new Uri(@"D:\Arquivos\Documents\FOTOS\" + n_mat + ".png").AbsoluteUri;

                ReportParameter parameter = new ReportParameter("path", imagePath);
                reportviewer.LocalReport.SetParameters(parameter);
            }
            else if (File.Exists(conf.caminho_fotos + n_mat + ".jpg"))
            {
                string imagePath = new Uri(conf.caminho_fotos + n_mat + ".jpg").AbsoluteUri;
                ReportParameter parameter = new ReportParameter("path", imagePath);
                reportviewer.LocalReport.SetParameters(parameter);
            }

            //if (File.Exists(conf.caminho_barcodes + "\\bc_" + n_mat + ".png"))
            //{
            //    string imagePath = new Uri(conf.caminho_barcodes + "\\bc_" + n_mat + ".png").AbsoluteUri;
            //    ReportParameter parameter = new ReportParameter("path_bc", imagePath);
            //    reportviewer.LocalReport.SetParameters(parameter);
            //}


            reportviewer.LocalReport.SetParameters(listreportparameter);
            reportviewer.LocalReport.Refresh();

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "RA_" + n_mat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void gera_etiqueta_sorocaba(List<Alunos.csAlunos> list_)
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.etiqueta2.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            conf.get_configuracoes();

            for (int i = 0; i < list_.Count; i++)
            {
                if (list_[i].n_mat != string.Empty)
                {
                    listreportparameter.Add(new ReportParameter("nome_" + (i + 1), "Nome: " + list_[i].nome));
                    listreportparameter.Add(new ReportParameter("rg_" + (i + 1), "RG: " + list_[i].rg));
                    listreportparameter.Add(new ReportParameter("ensino_" + (i + 1), "Ensino: " + cs_disciplina.troca_ensino_id_por_nome(list_[i].id_ensino_atual)));
                    listreportparameter.Add(new ReportParameter("n_mat_" + (i + 1), "Nº de Matricula: " + list_[i].n_mat));

                    reportviewer.LocalReport.EnableExternalImages = true;

                    if (File.Exists(conf.caminho_fotos + list_[i].n_mat + ".png"))
                    {
                        string imagePath = new Uri(conf.caminho_fotos + list_[i].n_mat + ".png").AbsoluteUri;
                        ReportParameter parameter = new ReportParameter("path_" + (i + 1), imagePath);
                        reportviewer.LocalReport.SetParameters(parameter);
                    }
                    else if (File.Exists(conf.caminho_fotos + list_[i].n_mat + ".jpg"))
                    {
                        string imagePath = new Uri(conf.caminho_fotos + list_[i].n_mat + ".jpg").AbsoluteUri;
                        ReportParameter parameter = new ReportParameter("path_" + (i + 1), imagePath);
                        reportviewer.LocalReport.SetParameters(parameter);
                    }
                }
            }



            reportviewer.LocalReport.SetParameters(listreportparameter);
            reportviewer.LocalReport.Refresh();

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "Etiqueta.pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void gera_declaracao_sorocaba(string n_mat, string sexo, string aluno, string rg, int id_ensino)
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.declaracao_sorocaba.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            string matri_sexo = "matriculado";

            if (sexo != "")
            {
                if (sexo == "FEMININO")
                {
                    matri_sexo = "matriculada";
                }
            }

            string declara = "                                A Direção do Centro Estadual de Educação para Jovens e Adultos \"Prof. Norberto Soares Ramos\", declara que: " + aluno + ", RG: " + rg + ", está " + matri_sexo + " sob o Nº " + n_mat + ", cursando o ensino " + primeira_caixa_alta(cs_disciplina.troca_ensino_id_por_nome(id_ensino)) + ", modalidade de presença flexível. O horário de funcionamento desta Escola é das 08h00 às 22h00, de segunda a sexta-feira.";

            listreportparameter.Add(new ReportParameter("declar", declara));
            listreportparameter.Add(new ReportParameter("data", "Sorocaba, " + DateTime.Now.ToLongDateString()));

            reportviewer.LocalReport.SetParameters(listreportparameter);


            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "Declaração_" + n_mat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void gera_requerimento_sorocaba(List<Alunos.csAlunos> list_)
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.requerimento_sorocaba.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            listreportparameter.Add(new ReportParameter("n_mat", list_[0].n_mat));
            listreportparameter.Add(new ReportParameter("nome", list_[0].nome));
            listreportparameter.Add(new ReportParameter("rg", list_[0].rg));
            listreportparameter.Add(new ReportParameter("ra", list_[0].ra));
            listreportparameter.Add(new ReportParameter("dat_mat", list_[0].dat_mat.ToString()));
            listreportparameter.Add(new ReportParameter("sigla", list_[0].uf_rg));
            listreportparameter.Add(new ReportParameter("termo", list_[0].termo_mat));
            //nascimento
            listreportparameter.Add(new ReportParameter("dat_nasc", list_[0].dat_nasc));
            listreportparameter.Add(new ReportParameter("municipio", list_[0].nasc_cidade));
            listreportparameter.Add(new ReportParameter("estado", list_[0].nasc_uf));
            listreportparameter.Add(new ReportParameter("pais", list_[0].nasc_pais));
            //endereço
            listreportparameter.Add(new ReportParameter("res_end", list_[0].res_endereco));
            listreportparameter.Add(new ReportParameter("res_bairro", list_[0].res_bairro));
            listreportparameter.Add(new ReportParameter("res_municipio", list_[0].res_cidade));
            listreportparameter.Add(new ReportParameter("res_estado", list_[0].res_uf));
            listreportparameter.Add(new ReportParameter("res_cep", list_[0].res_cep));
            listreportparameter.Add(new ReportParameter("res_numero", list_[0].res_numero));
            // pessoal
            listreportparameter.Add(new ReportParameter("est_civil", list_[0].estado_civil));
            listreportparameter.Add(new ReportParameter("sexo", list_[0].sexo));
            listreportparameter.Add(new ReportParameter("raca", cs_alunos.troca_raca_id(list_[0].id_raca)));
            listreportparameter.Add(new ReportParameter("mae", list_[0].nome_mae));
            listreportparameter.Add(new ReportParameter("cel", list_[0].res_celular));
            listreportparameter.Add(new ReportParameter("tel", list_[0].res_telefone));
            if (list_[0].port_nec == true) listreportparameter.Add(new ReportParameter("def", "SIM"));
            else listreportparameter.Add(new ReportParameter("def", "NÃO"));
            listreportparameter.Add(new ReportParameter("q_def", list_[0].nec));
            listreportparameter.Add(new ReportParameter("ensino", cs_disciplina.troca_ensino_id_por_nome(list_[0].id_ensino_atual)));
            listreportparameter.Add(new ReportParameter("dat_cidade", "Sorocaba, " + DateTime.Now.ToShortDateString()));
            listreportparameter.Add(new ReportParameter("mat_por", "Matrícula Efetuada Por: " + user_cad));

            listreportparameter.Add(new ReportParameter("data", DateTime.Now.ToString("dd/MM/yyyy")));

            conf.get_configuracoes();

            try
            {
                reportviewer.LocalReport.Refresh();

                reportviewer.LocalReport.EnableExternalImages = true;

                if (File.Exists(conf.caminho_fotos + list_[0].n_mat + ".png"))
                {
                    string imagePath = new Uri(conf.caminho_fotos + list_[0].n_mat + ".png").AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("Path", imagePath);
                    reportviewer.LocalReport.SetParameters(parameter);
                }
                else if (File.Exists(conf.caminho_fotos + list_[0].n_mat + ".jpg"))
                {
                    string imagePath = new Uri(conf.caminho_fotos + list_[0].n_mat + ".jpg").AbsoluteUri;
                    ReportParameter parameter = new ReportParameter("Path", imagePath);
                    reportviewer.LocalReport.SetParameters(parameter);
                }

                reportviewer.LocalReport.SetParameters(listreportparameter);

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                FileStream filestreamPDF = null;
                string nomeArquivoPDF = Path.GetTempPath() + "requerimento_" + list_[0].n_mat + ".pdf";

                filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
                filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
                filestreamPDF.Close();

                nome_arquivo = nomeArquivoPDF;

                Process.Start(nomeArquivoPDF);


            }

            catch (Exception ex)
            {
                Error_Log.csControle_erros.exibir_erro(ex.Message);
            }

        }

        public void gera_passaporte_sorocaba(string n_mat, string aluno, string rg, string dat_mat, string termo, string tel, string cel, int id_ensino)
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.passaporte.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();


            listreportparameter.Add(new ReportParameter("titulo_passaporte", "ENSINO " + cs_disciplina.troca_ensino_id_por_nome(id_ensino)));
            listreportparameter.Add(new ReportParameter("nmat", "N MAT.: " + n_mat));
            listreportparameter.Add(new ReportParameter("nome", "NOME:" + aluno));
            listreportparameter.Add(new ReportParameter("rg", "RG:" + rg));
            listreportparameter.Add(new ReportParameter("termo", "TERMO:" + termo));
            listreportparameter.Add(new ReportParameter("tel", "TEL.:" + tel));
            listreportparameter.Add(new ReportParameter("cel", "CEL.:" + cel));
            listreportparameter.Add(new ReportParameter("dat_mat", "DATA MATRÍCULA:" + DateTime.Parse(dat_mat).ToShortDateString()));

            reportviewer.LocalReport.EnableExternalImages = true;

            conf.get_configuracoes();

            if (File.Exists(conf.caminho_fotos + n_mat + ".png"))
            {
                string imagePath = new Uri(conf.caminho_fotos + n_mat + ".png").AbsoluteUri;
                ReportParameter parameter = new ReportParameter("path_foto", imagePath);
                reportviewer.LocalReport.SetParameters(parameter);
            }
            else if (File.Exists(conf.caminho_barcodes + n_mat + ".jpg"))
            {
                string imagePath = new Uri(conf.caminho_barcodes + n_mat + ".jpg").AbsoluteUri;
                ReportParameter parameter = new ReportParameter("path_foto", imagePath);
                reportviewer.LocalReport.SetParameters(parameter);
            }



            reportviewer.LocalReport.SetParameters(listreportparameter);

            reportviewer.LocalReport.Refresh();

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "passaporte_" + n_mat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void gera_pass_m_sorocaba(string n_mat, string aluno, string rg, string dat_mat, string termo, string tel, string cel)
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.pass_m.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            listreportparameter.Add(new ReportParameter("n_mat", n_mat));
            listreportparameter.Add(new ReportParameter("nome", aluno));
            listreportparameter.Add(new ReportParameter("rg", rg));
            listreportparameter.Add(new ReportParameter("termo", termo));
            listreportparameter.Add(new ReportParameter("tel", tel));
            listreportparameter.Add(new ReportParameter("cel", cel));

            string dia = DateTime.Parse(dat_mat).Day.ToString();
            string mes = DateTime.Parse(dat_mat).Month.ToString();
            string ano = DateTime.Parse(dat_mat).Year.ToString();

            listreportparameter.Add(new ReportParameter("dia", dia));
            listreportparameter.Add(new ReportParameter("mes", mes));
            listreportparameter.Add(new ReportParameter("ano", ano));

            reportviewer.LocalReport.EnableExternalImages = true;

            Configurações.CSconnexoes_txt conf = new Configurações.CSconnexoes_txt();
            conf.get_configuracoes();


            if (File.Exists(conf.caminho_fotos + n_mat + ".png"))
            {
                string imagePath = new Uri(conf.caminho_fotos + n_mat + ".png").AbsoluteUri;
                ReportParameter parameter = new ReportParameter("Path", imagePath);
                reportviewer.LocalReport.SetParameters(parameter);
            }
            else if (File.Exists(conf.caminho_fotos + n_mat + ".jpg"))
            {
                string imagePath = new Uri(conf.caminho_fotos + n_mat + ".jpg").AbsoluteUri;
                ReportParameter parameter = new ReportParameter("Path", imagePath);
                reportviewer.LocalReport.SetParameters(parameter);
            }



            reportviewer.LocalReport.SetParameters(listreportparameter);

            reportviewer.LocalReport.Refresh();

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "pass_m_" + n_mat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void gera_pass_f_sorocaba(string n_mat, string aluno, string rg, string dat_mat, string termo, string tel, string cel)
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.pass_f.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            listreportparameter.Add(new ReportParameter("n_mat", n_mat));
            listreportparameter.Add(new ReportParameter("nome", aluno));
            listreportparameter.Add(new ReportParameter("rg", rg));
            listreportparameter.Add(new ReportParameter("termo", Termo));
            listreportparameter.Add(new ReportParameter("tel", tel));
            listreportparameter.Add(new ReportParameter("cel", cel));


            string dia = DateTime.Parse(dat_mat).Day.ToString();
            string mes = DateTime.Parse(dat_mat).Month.ToString();
            string ano = DateTime.Parse(dat_mat).Year.ToString();

            listreportparameter.Add(new ReportParameter("dia", dia));
            listreportparameter.Add(new ReportParameter("mes", mes));
            listreportparameter.Add(new ReportParameter("ano", ano));

            reportviewer.LocalReport.EnableExternalImages = true;

            if (File.Exists(conf.caminho_fotos + n_mat + ".png"))
            {
                string imagePath = new Uri(conf.caminho_fotos + n_mat + ".png").AbsoluteUri;
                ReportParameter parameter = new ReportParameter("Path", imagePath);
                reportviewer.LocalReport.SetParameters(parameter);
            }
            else if (File.Exists(conf.caminho_fotos + n_mat + ".jpg"))
            {
                string imagePath = new Uri(conf.caminho_fotos + n_mat + ".jpg").AbsoluteUri;
                ReportParameter parameter = new ReportParameter("Path", imagePath);
                reportviewer.LocalReport.SetParameters(parameter);
            }

            reportviewer.LocalReport.SetParameters(listreportparameter);

            reportviewer.LocalReport.Refresh();

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "pass_f_" + n_mat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        #endregion

        ///Sorocaba - CrystalReport
        //Relatórios Sorocaba
        public ra_sorocaba gera_crystal_ra_sorocaba(string n_mat_pesquisa_)
        {
            ra_sorocaba cr = new ra_sorocaba();

            #region Foto
            
            string imgpath = "";

            conf.get_configuracoes();

            if (File.Exists(conf.caminho_fotos + n_mat_pesquisa_ + ".png"))
            {
                string imagePath = System.IO.Path.GetFullPath(conf.caminho_fotos + n_mat_pesquisa_ + ".png");
                imgpath = imagePath;
            }
            else if (File.Exists(conf.caminho_fotos + n_mat_pesquisa_ + ".jpg"))
            {
                string imagePath = System.IO.Path.GetFullPath(conf.caminho_fotos + n_mat_pesquisa_ + ".jpg");
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

            #endregion

            #region Preencher Dados Aluno

            DataTable dtAluno = new DataTable();

            SqlDataAdapter daAluno = new SqlDataAdapter("SELECT * FROM ALUNOS WHERE N_MAT='" + n_mat_pesquisa_ + "'", sql_conn);

            daAluno.Fill(dtAluno);
            daAluno.FillSchema(dtAluno, SchemaType.Source);

            DataSet dsaluno = new DataSet();

            dsaluno.Tables.Add(dtAluno);
            dsaluno.Tables.Add(dtFoto);

            cr.SetDataSource(dsaluno);

            #endregion

            cr.SetParameterValue("DATA_IMPRESSAO", data_relatorio());

            return cr;
        }

        public requerimento_sorocaba gera_crystal_requerimento_sorocaba(string n_mat_pesquisa_)
        {
            requerimento_sorocaba cr = new requerimento_sorocaba();

            #region Foto

            string imgpath = "";

            conf.get_configuracoes();
            
            if (File.Exists(conf.caminho_fotos + n_mat_pesquisa_ + ".png"))
            {
                string imagePath = System.IO.Path.GetFullPath(conf.caminho_fotos + n_mat_pesquisa_ + ".png");
                //foto = Image.FromFile(imagePath);
                imgpath = imagePath;
            }
            else if (File.Exists(conf.caminho_fotos + n_mat_pesquisa_ + ".jpg"))
            {
                string imagePath = System.IO.Path.GetFullPath(conf.caminho_fotos + n_mat_pesquisa_ + ".jpg");
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

            #endregion

            #region Preencher Dados Aluno

            DataTable dtAluno = new DataTable();            

            SqlDataAdapter daAluno = new SqlDataAdapter("SELECT * FROM ALUNOS WHERE N_MAT='" + n_mat_pesquisa_ + "'", sql_conn);

            daAluno.Fill(dtAluno);
            daAluno.FillSchema(dtAluno, SchemaType.Source);

            DataSet dsaluno = new DataSet();

            dsaluno.Tables.Add(dtAluno);
            dsaluno.Tables.Add(dtFoto);

            cr.SetDataSource(dsaluno);

            #endregion
            
            int id_raca_;
            if (int.TryParse(dtAluno.Rows[0]["ID_RACA"].ToString(), out id_raca_))
            {
                cr.SetParameterValue("RACA", cs_alunos.troca_raca_id(id_raca_));
            }
            else
            {
                cr.SetParameterValue("RACA", "NÃO DECLARADA");
            }

            string contato_ = "";
            string tel_ = format_telefone(dtAluno.Rows[0]["RES_TELEFONE"].ToString());
            string cel_ = format_telefone(dtAluno.Rows[0]["RES_CELULAR"].ToString());

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

            cr.SetParameterValue("contato", contato_);

            string user_cad = "";

            if (dtAluno.Rows[0]["ID_USUARIO_CAD"] != DBNull.Value)
            {
                user_cad = cs_usuarios.troca_id_por_nome(Convert.ToInt32(dtAluno.Rows[0]["ID_USUARIO_CAD"].ToString()));
            }
            

            cr.SetParameterValue("MATRICULADO_POR", user_cad);
            
            cr.SetParameterValue("DATA_IMPRESSAO", data_relatorio());

            return cr;
        }

        public declaracao_conclusao_sorocaba gera_crystal_declaracao_conclusao_sorocaba()
        {
            declaracao_conclusao_sorocaba cr = new declaracao_conclusao_sorocaba();

            return cr;
        }

        public declaracao_matricula_sorocaba gera_crystal_declaracao_matricula_sorocaba()
        {
            declaracao_matricula_sorocaba cr = new declaracao_matricula_sorocaba();

            return cr;
        }

        public historico_m_sorocaba gera_crystal_historico_m_sorocaba(List<Alunos.csAlunos> list_aluno, List<Notas.csNotas> list_notas)
        {
            historico_m_sorocaba cr = new historico_m_sorocaba();

            #region Preencher Dados Histórico
            DataTable dt = new DataTable();
            dt.Columns.Add("dis");
            dt.Columns.Add("ins");
            dt.Columns.Add("mu");
            dt.Columns.Add("uf");
            dt.Columns.Add("dt");
            dt.Columns.Add("not");

            for (int i = 0; i < ins_list.Count; i++)
            {
                dt.Rows.Add();

                dt.Rows[i]["dis"] = dis_list[i];
                dt.Rows[i]["ins"] = ins_list[i];
                dt.Rows[i]["mu"] = mu_list[i];
                dt.Rows[i]["uf"] = uf_list[i];
                dt.Rows[i]["dt"] = dt_list[i];
                dt.Rows[i]["not"] = not_list[i];
            }
            cr.SetDataSource(dt);
            #endregion

            #region Preencher Dados Alunos
            cr.SetParameterValue("n_mat", list_aluno[0].n_mat);
            cr.SetParameterValue("nome", list_aluno[0].nome);
            cr.SetParameterValue("mae", list_aluno[0].nome_mae);
            cr.SetParameterValue("rg", list_aluno[0].rg);
            cr.SetParameterValue("ra", list_aluno[0].ra);
            cr.SetParameterValue("nasc_cidade", list_aluno[0].nasc_cidade);
            cr.SetParameterValue("nasc_estado", list_aluno[0].nasc_uf);
            cr.SetParameterValue("nasc_pais", list_aluno[0].nasc_pais);
            cr.SetParameterValue("dat_nasc", list_aluno[0].dat_nasc);
            cr.SetParameterValue("ini_curso", "O Aluno inicio ocurso em " + cs_ensinos.ent_ensino(list_aluno[0].n_mat, 2).ToString("dd/MM/yyyy"));
            cr.SetParameterValue("data_relatorio", DateTime.Now.ToString("dd/MM/yyyy"));

            cr.SetParameterValue("obs", list_notas[0].obs_1);
            cr.SetParameterValue("secretario", list_notas[0].secretario);
            cr.SetParameterValue("rg_secretario", list_notas[0].rg_secretario);
            cr.SetParameterValue("diretor", list_notas[0].diretor);
            cr.SetParameterValue("rg_diretor", list_notas[0].rg_diretor);

            cr.SetParameterValue("serie_anterior", list_notas[0].serie_ant);
            cr.SetParameterValue("instituicao_anterior", list_notas[0].ins_ant);
            cr.SetParameterValue("ano_anterior", list_notas[0].ano_ant.ToString());
            cr.SetParameterValue("municipio_anterior", list_notas[0].mu_ant);
            cr.SetParameterValue("uf_anterior", list_notas[0].uf_ant);

            string certi = "O Diretor do C. E. E. J. A. PROF. NORBERTO SOARES RAMOS,  CERTIFICA, nos termos do Inciso do VII, Artigo 24 da Lei Federal 9394/96, que " + list_aluno[0].nome + ", RG " + list_aluno[0].rg + ", Concluiu o Ensino Médio - Modalidade Educação de Jovens e Adultos, Atendimento Individualizado e Presença Flexível, no ano de " + list_notas[0].ano_con + ".";
            cr.SetParameterValue("uf_anterior", certi);

            #endregion

            return cr;
        }

        public historico_f_sorocaba gera_crystal_historico_f_sorocaba(List<Alunos.csAlunos> list_aluno, List<Notas.csNotas> list_notas)
        {
            historico_f_sorocaba cr = new historico_f_sorocaba();

            #region Preencher Dados Histórico
            DataTable dt = new DataTable();
            dt.Columns.Add("dis");
            dt.Columns.Add("ins");
            dt.Columns.Add("mu");
            dt.Columns.Add("uf");
            dt.Columns.Add("dt");
            dt.Columns.Add("not");

            for (int i = 0; i < ins_list.Count; i++)
            {
                dt.Rows.Add();

                dt.Rows[i]["dis"] = dis_list[i];
                dt.Rows[i]["ins"] = ins_list[i];
                dt.Rows[i]["mu"] = mu_list[i];
                dt.Rows[i]["uf"] = uf_list[i];
                dt.Rows[i]["dt"] = dt_list[i];
                dt.Rows[i]["not"] = not_list[i];
            }
            cr.SetDataSource(dt);
            #endregion

            #region preencher Dados Aluno
            cr.SetParameterValue("n_mat", list_aluno[0].n_mat);
            cr.SetParameterValue("nome", list_aluno[0].nome);            
            cr.SetParameterValue("mae", list_aluno[0].nome_mae);
            cr.SetParameterValue("rg", list_aluno[0].rg);
            cr.SetParameterValue("ra", list_aluno[0].ra);
            cr.SetParameterValue("nasc_cidade", list_aluno[0].nasc_cidade);
            cr.SetParameterValue("nasc_estado", list_aluno[0].nasc_uf);
            cr.SetParameterValue("nasc_pais", list_aluno[0].nasc_pais);
            cr.SetParameterValue("dat_nasc", list_aluno[0].dat_nasc);
            cr.SetParameterValue("ini_curso", "O Aluno inicio ocurso em " + cs_ensinos.ent_ensino(list_aluno[0].n_mat, 1).ToString("dd/MM/yyyy"));
            cr.SetParameterValue("data_relatorio", DateTime.Now.ToString("dd/MM/yyyy"));

            cr.SetParameterValue("obs", list_notas[0].obs_1);
            cr.SetParameterValue("secretario", list_notas[0].secretario);
            cr.SetParameterValue("rg_secretario", list_notas[0].rg_secretario);
            cr.SetParameterValue("diretor", list_notas[0].diretor);
            cr.SetParameterValue("rg_diretor", list_notas[0].rg_diretor);

            #endregion

            return cr;
        }
        






         
        ///Americana - crystal-report
        //Relatórios Americana
        

        public Relatorios.americana.crystal.declaracao_conclusao_americana gera_crystal_declaracao_conclusao_americana(string nome, string rg, int id_ensino)
        {
            Relatorios.americana.crystal.declaracao_conclusao_americana cr = new Relatorios.americana.crystal.declaracao_conclusao_americana();

            string ensino = "";
            string ensino_seguinte = "";

            if (id_ensino == 1)
            {
                // ATUAL: "Fundamental"
                ensino = "Fundamental";
                ensino_seguinte = "Médio";
            }
            else if (id_ensino == 2)
            {
                // ATUAL: "Médio"
                ensino = "Médio";
                ensino_seguinte = "Superior";
            }

            cr.SetParameterValue("nome", nome);
            cr.SetParameterValue("rg", rg);
            cr.SetParameterValue("ensino_atual", ensino);
            cr.SetParameterValue("ensino_seguinte", ensino_seguinte);
            cr.SetParameterValue("data_relatorio", data_relatorio());

            return cr;
        }

        public Relatorios.americana.crystal.atestado_eliminacao_americana gera_crystal_atestado_eli_americana(string n_mat, string nome, string rg, int id_ensino, List<Notas.csNotas> list_disc_eliminadas)
        {
            //Tabela de disciplinas eliminadas
            DataTable table = new DataTable();

            table.Columns.Add("N_DISCIPLINA");
            table.Columns.Add("DATA_FIM");
            table.Columns.Add("MEDIA");

            for (int i = 0; i < list_disc_eliminadas.Count; i++)
            {
                table.Rows.Add();
                table.Rows[i][0] = cs_disciplina.troca_disciplina_id_por_nome(list_disc_eliminadas[i].id_disciplina);
                table.Rows[i][1] = list_disc_eliminadas[i].dat_fin;
                table.Rows[i][2] = list_disc_eliminadas[i].media;
            }

            string ensino_ = "";

            if (id_ensino == 1)
            {
                // ATUAL: "Fundamental"
                ensino_ = "Fundamental";
            }
            else
            {
                // ATUAL: "Médio"
                ensino_ = "Médio";
            }

            americana.crystal.atestado_eliminacao_americana cr = new americana.crystal.atestado_eliminacao_americana();

            cr.SetParameterValue("nome", remove_espaco(nome));
            cr.SetParameterValue("rg", rg);
            cr.SetParameterValue("ensino_atual", primeira_caixa_alta(ensino_));

            return cr;
        }

        public Relatorios.americana.crystal.declaracao_matricula_americana gera_crystal_declaracao_matricula_americana(string n_mat, string nome, string rg, int id_ensino)
        {
            string ensino_ = "";
            if (id_ensino == 1)
            {
                ensino_ = "Fundamental";
            }
            else
            {
                ensino_ = "Médio";
            }

            americana.crystal.declaracao_matricula_americana cr = new americana.crystal.declaracao_matricula_americana();

            cr.SetParameterValue("nome", remove_espaco(nome));
            cr.SetParameterValue("rg", rg);
            cr.SetParameterValue("ensino_atual", primeira_caixa_alta(ensino_));
            cr.SetParameterValue("data_relatorio", data_relatorio());

            return cr;
        }

        public Relatorios.americana.crystal.declaracao_comparecimento_americana gera_crystal_declaracao_comparecimento_americana(string n_mat, string nome, string rg, int id_ensino)
        {
            americana.crystal.declaracao_comparecimento_americana cr = new americana.crystal.declaracao_comparecimento_americana();

            string periodo = "";
            TimeSpan manha = TimeSpan.Parse("12:00");
            TimeSpan tarde = TimeSpan.Parse("18:00");
            TimeSpan noite = TimeSpan.Parse("22:00");

            TimeSpan horaAgora = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));

            if (horaAgora < manha) periodo = "manhã";
            else if (horaAgora > manha && horaAgora < noite) periodo = "tarde";
            else periodo = "noite";

            cr.SetParameterValue("nome", nome);
            cr.SetParameterValue("rg", rg);
            cr.SetParameterValue("periodo", periodo);
            cr.SetParameterValue("data_relatorio", data_relatorio());

            return cr;

        }

        public Relatorios.americana.crystal.cartao_americana gera_crystal_cartao_americana(List<string> list_)
        {
            americana.crystal.cartao_americana cr = new americana.crystal.cartao_americana();

            Endereços.csEnderecos cs_enderecos = new Endereços.csEnderecos();

            if (list_ != null)
            {
                sql_comm.Connection = sql_conn;

                string a = "";

                for (int i = 0; i < list_.Count; i++)
                {
                    a += list_[i] + "','";
                }

                a += "')";

                sql_comm.CommandText = @"SELECT N_MAT, ALUNO, DAT_MAT, ENSINO FROM ALUNOS WHERE N_MAT IN ('" + a;
            }

            SqlDataAdapter da = new SqlDataAdapter(sql_comm.CommandText, sql_conn);

            DataSet ds = new DataSet1();

            da.Fill(ds.Tables["ALUNOS"]);
            cr.SetDataSource(ds.Tables["ALUNOS"]);

            return cr;
        }

        public Relatorios.americana.crystal.certificado_americana gera_crystal_certif_americana(List<Alunos.csAlunos> list_aluno, List<Notas.csNotas> list_notas, string ensino)
        {
            americana.crystal.certificado_americana cr = new americana.crystal.certificado_americana();

            cr.SetParameterValue("nome", list_aluno[0].nome);
            cr.SetParameterValue("rg", list_aluno[0].rg);
            cr.SetParameterValue("nasc_cidade", list_aluno[0].nasc_cidade);
            cr.SetParameterValue("data_nasc", list_aluno[0].dat_nasc);

            cr.SetParameterValue("ensino", ensino); //ensino atual do certificado

            cr.SetParameterValue("data_relatorio", data_relatorio());


            cr.SetParameterValue("diretor", list_notas[0].diretor);
            cr.SetParameterValue("rg_diretor", list_notas[0].rg_diretor);
            cr.SetParameterValue("secretario", list_notas[0].secretario);
            cr.SetParameterValue("rg_secretario", list_notas[0].rg_secretario);

            return cr;

        }

        public Relatorios.americana.crystal.declaracao_eliminacao_americana gera_crystal_declaracao_elimi_americana(string n_mat, string nome, string rg, int id_ensino, List<Notas.csNotas> list_disc_eliminadas)
        {
            americana.crystal.declaracao_eliminacao_americana cr = new americana.crystal.declaracao_eliminacao_americana();

            string ensino_ = "";
            string disc_elimi = "";

            if (id_ensino == 1)
            {
                // ATUAL: "Fundamental"
                ensino_ = "Fundamental";
            }
            else
            {
                // ATUAL: "Médio"
                ensino_ = "Médio";
            }

            for (int i = 0; i < list_disc_eliminadas.Count; i++)
            {
                if (i == (list_disc_eliminadas.Count - 1))
                {
                    disc_elimi += " e ";
                }

                disc_elimi += cs_disciplina.troca_disciplina_id_por_nome(list_disc_eliminadas[i].id_disciplina);
            }

            disc_elimi += ".";

            cr.SetParameterValue("nome", nome);
            cr.SetParameterValue("rg", rg);
            cr.SetParameterValue("ensino", "Ensino " + primeira_caixa_alta(ensino_));
            cr.SetParameterValue("disc_eliminadas", disc_elimi);
            cr.SetParameterValue("data_relatorio", data_relatorio());

            return cr;
        }

        public Relatorios.americana.crystal.etiqueta_passaporte_americana gera_crystal_etiqueta_passaporte_americana(List<string> list_)
        {
            americana.crystal.etiqueta_passaporte_americana cr = new americana.crystal.etiqueta_passaporte_americana();

            Endereços.csEnderecos cs_enderecos = new Endereços.csEnderecos();

            if (list_ != null)
            {
                sql_comm.Connection = sql_conn;

                string a = "";

                for (int i = 0; i < list_.Count; i++)
                {
                    a += list_[i] + "','";
                }

                a += "')";

                sql_comm.CommandText = @"SELECT N_MAT, ALUNO, RG, UF_RG, FORMAT(DAT_MAT,'dd/MM/yyyy') as DAT_MAT, ENSINO, FORMAT(DAT_NASC,'dd/MM/yyyy') as DAT_NASC, RES_TELEFONE, RES_CELULAR, OBS_PASSAPORTE FROM ALUNOS WHERE N_MAT IN ('" + a;
            }

            SqlDataAdapter da = new SqlDataAdapter(sql_comm.CommandText, sql_conn);

            DataSet ds = new DataSet1();

            da.Fill(ds.Tables["ALUNOS"]);
            cr.SetDataSource(ds.Tables["ALUNOS"]);

            return cr;
        }

        public Relatorios.americana.crystal.historico_f_americana gera_crystal_historico_f_americana(List<Alunos.csAlunos> list_aluno, List<Notas.csNotas> list_notas)
        {
            americana.crystal.historico_f_americana cr = new americana.crystal.historico_f_americana();

            #region Preencher Dados Histórico
            DataTable dt = new DataTable();
            dt.Columns.Add("dis");
            dt.Columns.Add("ins");
            dt.Columns.Add("mu");
            dt.Columns.Add("uf");
            dt.Columns.Add("dt");
            dt.Columns.Add("not");

            for (int i = 0; i < ins_list.Count; i++)
            {
                dt.Rows.Add();

                dt.Rows[i]["dis"] = dis_list[i];
                dt.Rows[i]["ins"] = ins_list[i];
                dt.Rows[i]["mu"] = mu_list[i];
                dt.Rows[i]["uf"] = uf_list[i];
                dt.Rows[i]["dt"] = dt_list[i];
                dt.Rows[i]["not"] = not_list[i];
            }
            cr.SetDataSource(dt);
            #endregion

            #region preencher Dados Aluno
            cr.SetParameterValue("n_mat", list_aluno[0].n_mat);
            cr.SetParameterValue("nome", list_aluno[0].nome);
            cr.SetParameterValue("rg", list_aluno[0].rg);
            cr.SetParameterValue("ra", list_aluno[0].ra);
            cr.SetParameterValue("nasc_cidade", list_aluno[0].nasc_cidade);
            cr.SetParameterValue("nasc_estado", list_aluno[0].nasc_uf);
            cr.SetParameterValue("nasc_pais", list_aluno[0].nasc_pais);
            cr.SetParameterValue("dat_nasc", list_aluno[0].dat_nasc);
            cr.SetParameterValue("ini_curso", "O Aluno inicio ocurso em " + list_aluno[0].dat_mat.ToString("dd/MM/yyyy"));
            cr.SetParameterValue("data_relatorio", DateTime.Now.ToString("dd/MM/yyyy"));

            cr.SetParameterValue("obs", list_notas[0].obs_1);
            cr.SetParameterValue("secretario", list_notas[0].secretario);
            cr.SetParameterValue("rg_secretario", list_notas[0].rg_secretario);
            cr.SetParameterValue("diretor", list_notas[0].diretor);
            cr.SetParameterValue("rg_diretor", list_notas[0].rg_diretor);

            #endregion
            return cr;
        }

        public Relatorios.americana.crystal.historico_m_americana gera_crystal_historico_m_americana(List<Alunos.csAlunos> list_aluno, List<Notas.csNotas> list_notas)
        {
            americana.crystal.historico_m_americana cr = new americana.crystal.historico_m_americana();

            #region Preencher Dados Histórico
            DataTable dt = new DataTable();
            dt.Columns.Add("dis");
            dt.Columns.Add("ins");
            dt.Columns.Add("mu");
            dt.Columns.Add("uf");
            dt.Columns.Add("dt");
            dt.Columns.Add("not");

            for (int i = 0; i < ins_list.Count; i++)
            {
                dt.Rows.Add();

                dt.Rows[i]["dis"] = dis_list[i];
                dt.Rows[i]["ins"] = ins_list[i];
                dt.Rows[i]["mu"] = mu_list[i];
                dt.Rows[i]["uf"] = uf_list[i];
                dt.Rows[i]["dt"] = dt_list[i];
                dt.Rows[i]["not"] = not_list[i];
            }
            cr.SetDataSource(dt);
            #endregion

            #region Preencher Dados Alunos
            cr.SetParameterValue("n_mat", list_aluno[0].n_mat);
            cr.SetParameterValue("nome", list_aluno[0].nome);
            cr.SetParameterValue("rg", list_aluno[0].rg);
            cr.SetParameterValue("ra", list_aluno[0].ra);
            cr.SetParameterValue("nasc_cidade", list_aluno[0].nasc_cidade);
            cr.SetParameterValue("nasc_estado", list_aluno[0].nasc_uf);
            cr.SetParameterValue("nasc_pais", list_aluno[0].nasc_pais);
            cr.SetParameterValue("dat_nasc", list_aluno[0].dat_nasc);
            cr.SetParameterValue("ini_curso", "O Aluno inicio ocurso em " + list_aluno[0].dat_mat.ToString("dd/MM/yyyy"));
            cr.SetParameterValue("data_relatorio", DateTime.Now.ToString("dd/MM/yyyy"));

            cr.SetParameterValue("obs", list_notas[0].obs_1);
            cr.SetParameterValue("secretario", list_notas[0].secretario);
            cr.SetParameterValue("rg_secretario", list_notas[0].rg_secretario);
            cr.SetParameterValue("diretor", list_notas[0].diretor);
            cr.SetParameterValue("rg_diretor", list_notas[0].rg_diretor);

           
            cr.SetParameterValue("instituicao_anterior", list_notas[0].ins_ant);
            cr.SetParameterValue("ano_anterior", list_notas[0].ano_ant.ToString());
            cr.SetParameterValue("municipio_anterior", list_notas[0].mu_ant);
            cr.SetParameterValue("uf_anterior", list_notas[0].uf_ant);            

            #endregion

            return cr;
        }

        public Relatorios.americana.crystal.requerimento_matricula_americana gera_crystal_requerimento_americana(string n_mat_pesquisa_)
        {
            americana.crystal.requerimento_matricula_americana cr = new americana.crystal.requerimento_matricula_americana();

            #region Preencher Dados Aluno

            DataTable dtAluno = new DataTable();

            SqlDataAdapter daAluno = new SqlDataAdapter("SELECT * FROM ALUNOS WHERE N_MAT='"+n_mat_pesquisa_+"'", sql_conn);



            daAluno.Fill(dtAluno);
            daAluno.FillSchema(dtAluno, SchemaType.Source);


            cr.SetDataSource(dtAluno);


            #endregion

            //cr.SetParameterValue("n_mat", dados_aluno[0].n_mat);
            //cr.SetParameterValue("nome", dados_aluno[0].nome);
            //if (dados_aluno[0].dat_rg != "") cr.SetParameterValue("rg_dat_exp", DateTime.Parse(dados_aluno[0].dat_rg).ToString("dd/MM/yyyy"));
            //cr.SetParameterValue("rg_local", dados_aluno[0].ufrg);
            //cr.SetParameterValue("rg_orgao_exp", dados_aluno[0].orgao);
            //cr.SetParameterValue("rg", dados_aluno[0].rg);
            //cr.SetParameterValue("ra", dados_aluno[0].ra);
            //cr.SetParameterValue("data_mat", DateTime.Parse(dados_aluno[0].dat_mat).ToString("dd/MM/yyyy"));
            //cr.SetParameterValue("sigla", dados_aluno[0].ufrg);
            //cr.SetParameterValue("termo", dados_aluno[0].termo_mat);
            //cr.SetParameterValue("ensino", "ENSINO " + cs_disciplina.troca_ensino_id_por_nome(dados_aluno[0].id_ensino_atual));
            //cr.SetParameterValue("informacoes", dados_aluno[0].obs_passaporte);
            //
            ////nascimento
            //cr.SetParameterValue("dat_nasc", DateTime.Parse(dados_aluno[0].dat_nasc).ToString("dd/MM/yyyy"));
            //cr.SetParameterValue("municipio", dados_aluno[0].nasc_cidade);
            //cr.SetParameterValue("estado", dados_aluno[0].nasc_uf);
            //cr.SetParameterValue("pais", dados_aluno[0].nasc_pais);
            ////endereço
            //cr.SetParameterValue("res_end", dados_aluno[0].res_endereco + ", " + dados_aluno[0].res_numero);
            //cr.SetParameterValue("res_bairro", dados_aluno[0].res_bairro);
            //cr.SetParameterValue("res_municipio", dados_aluno[0].res_cidade);
            //cr.SetParameterValue("res_estado", dados_aluno[0].res_uf);
            //cr.SetParameterValue("res_cep", dados_aluno[0].res_cep);
            //cr.SetParameterValue("contato", "Cel: " + format_telefone(dados_aluno[0].res_celular) + " - Tel: " + format_telefone(dados_aluno[0].res_telefone));
            //
            //// pessoal
            //cr.SetParameterValue("est_civil", dados_aluno[0].estado_civil);
            //cr.SetParameterValue("sexo", dados_aluno[0].sexo);
            //cr.SetParameterValue("raca", cs_alunos.troca_raca_id(dados_aluno[0].id_raca));
            //cr.SetParameterValue("mae", dados_aluno[0].nome_mae);
            //cr.SetParameterValue("pai", dados_aluno[0].nome_pai);
            //cr.SetParameterValue("cel", dados_aluno[0].res_celular);
            //cr.SetParameterValue("tel", dados_aluno[0].res_telefone);
            //cr.SetParameterValue("e_mail", dados_aluno[0].e_mail);
            ////cr.SetParameterValue("def", dados_aluno[0].port_nec));
            //cr.SetParameterValue("q_def", dados_aluno[0].nec);
            //cr.SetParameterValue("mat_por", "Matrícula Efetuada Por: " + user_cad);
            ////trabalho
            //cr.SetParameterValue("trab_local", dados_aluno[0].trabalho);
            //cr.SetParameterValue("trab_endereco", dados_aluno[0].trab_endereco);
            //cr.SetParameterValue("trab_bairro", dados_aluno[0].trab_bairro);
            //cr.SetParameterValue("trab_cidade", dados_aluno[0].trab_cidade);
            //cr.SetParameterValue("trab_estado", dados_aluno[0].trab_estado);
            //cr.SetParameterValue("trab_cep", dados_aluno[0].trab_cep);
            //cr.SetParameterValue("trab_contato", format_telefone(dados_aluno[0].trab_telefone));
            //


            int id_raca_;
            if (int.TryParse(dtAluno.Rows[0]["ID_RACA"].ToString(),out id_raca_))
            {
                cr.SetParameterValue("raca", cs_alunos.troca_raca_id(id_raca_));
            }
            else
            {
                cr.SetParameterValue("raca", "NÃO DECLARADA");
            }

            string contato_ = "";
            string tel_ = format_telefone(dtAluno.Rows[0]["RES_TELEFONE"].ToString());
            string cel_ = format_telefone(dtAluno.Rows[0]["RES_CELULAR"].ToString());

            if(tel_ != string.Empty)
            {
                contato_ += "Tel. " + tel_;
            }

            if(cel_ != string.Empty)
            {
                if (contato_ == string.Empty)
                    contato_ += "Cel. " + cel_;
                else
                    contato_ += " - Cel. " + cel_;
            }

            cr.SetParameterValue("contato", contato_);

            cr.SetParameterValue("contato_trab", format_telefone(dtAluno.Rows[0]["TRAB_TELEFONE"].ToString()));

            cr.SetParameterValue("ensino", "ENSINO " + dtAluno.Rows[0]["ENSINO"].ToString());
            cr.SetParameterValue("data_relatorio", data_relatorio());

            return cr;
        }
        
        ///Funções
        //Primeira caixa alta.
        public string data_relatorio()
        {
            string data = "";
            if(Configurações.csEscola.cidade.ToLower() == "americana")
            {
                data = data_relatorio_.ToString("'Americana,' dd 'de' MMMM 'de' yyyy");

            }
            else if (Configurações.csEscola.cidade.ToLower() == "sorocaba")
            {
                data = data_relatorio_.ToString("'Sorocaba,' dd 'de' MMMM 'de' yyyy");
            }
                
            return data;
        }

        public string primeira_caixa_alta(string palavra)
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

        public string remove_espaco(string palavra)
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
                    a += aa[i].Remove(0, 1).ToUpper();
                }

            }

            return a;
        }

        public string format_telefone(string telefone)
        {
            string a = "";

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



            return a;
        }

        //Relatorio acompanhamento aluno
        public DataTable tab_acomp_aluno(string n_mat)
        {
            //Aprender a usar DataRow = new row

            DataTable tb = new DataTable();

            tb.Columns.Add("data");
            tb.Columns.Add("item");
            tb.Columns.Add("quem");

            //Matricula
            tb.Rows.Add();
            tb.Rows[0][0] = cs_alunos.dat_mat__(nnmat);
            tb.Rows[0][1] = "MATRÍCULADO";
            tb.Rows[0][2] = cs_usuarios.troca_id_por_nome(cs_alunos.id_user_cad(nnmat));

            //Atendimentos
            DataTable tab_atendimento = cs_atendimentos.buscarAtendimento_com(n_mat, 0);
            for (int i = 0; i < tab_atendimento.Rows.Count; i++)
            {
                //table.Columns.Add("id_atendimento");             //0
                //table.Columns.Add("id_tipo_atendimento");        //1
                //table.Columns.Add("data_atendimento");           //2 
                //table.Columns.Add("modulo");                     //3
                //table.Columns.Add("id_user_lanc");               //4
                //table.Columns.Add("id_disciplina"); 

                tb.Rows.Add();
                tb.Rows[i + tb.Rows.Count][0] = tab_atendimento.Rows[i][2];
                tb.Rows[i + tb.Rows.Count][1] = cs_atendimentos.troca_id_nome_tipo_atendimento(Convert.ToInt32(tab_atendimento.Rows[i][1]));
                tb.Rows[i + tb.Rows.Count][2] = cs_usuarios.troca_id_por_nome(Convert.ToInt32(tab_atendimento.Rows[i][4]));
            }

            //Rematriculas
            List<string> list_rematriculas = new List<string>();
            for (int i = 0; i < list_rematriculas.Count; i++)
            {
                tb.Rows.Add();
                tb.Rows[i + tb.Rows.Count][0] = list_rematriculas[i];
                tb.Rows[i + tb.Rows.Count][1] = "REMATRÍCULA";
            }

            return tb;
        }
    }
}


