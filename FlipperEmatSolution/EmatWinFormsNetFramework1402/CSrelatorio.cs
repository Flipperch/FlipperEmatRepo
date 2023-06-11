using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Microsoft.Reporting.WebForms;
using System.IO;
using System.Diagnostics;

namespace E_Matricula.Relatorios
{
    public class CSrelatorio
    {
        public string aluno;
        public string ra;
        public string rg;
        public string nnmat;
        public string sexo;
        public string ensino;
        public string mae;
        public string pais;
        public string estado;
        public string cidade;
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

        public List<string> ins_list = new List<string>();
        public List<string> mu_list = new List<string>();
        public List<string> uf_list = new List<string>();
        public List<string> not_list = new List<string>();
        public List<string> dt_list = new List<string>();

        public DataTable dt = new DataTable();       

        public void gera_carteirinha()
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.carteirinha.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            listreportparameter.Add(new ReportParameter("Nome", aluno));
            listreportparameter.Add(new ReportParameter("RA", ra));
            listreportparameter.Add(new ReportParameter("RG", rg));
            

            reportviewer.LocalReport.SetParameters(listreportparameter);

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "RA_" + nnmat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void gera_etiqueta()
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.carteirinha.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            listreportparameter.Add(new ReportParameter("Nome", aluno));
            listreportparameter.Add(new ReportParameter("RA", ra));
            listreportparameter.Add(new ReportParameter("RG", rg));

            reportviewer.LocalReport.SetParameters(listreportparameter);

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytePDF = reportviewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream filestreamPDF = null;
            string nomeArquivoPDF = Path.GetTempPath() + "Etiqueta_" + nnmat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void gera_declaracao()
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.declaracao.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            string matri_sexo = "matriculado";

            if (sexo != "")
            {
                if (sexo == "FEMININO")
                {
                    matri_sexo = "matriculada";
                }
            }

            string declara = "                                A Direção do Centro Estadual de Educação para Jovens e Adultos \"Prof. Norberto Soares Ramos\", declara que: " + aluno + ", RG: " + rg + ", está " + matri_sexo + " sob o Nº " + nnmat + ", cursando o ensino " + ensino + ", modalidade de presença flexível. O horário de funcionamento desta Escola é das 08h00 às 22h00, de segunda a sexta-feira.";

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
            string nomeArquivoPDF = Path.GetTempPath() + "Declaração_" + nnmat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }

        public void gera_historicoM()
        {
            ReportViewer reportviewer = new ReportViewer();
            reportviewer.ProcessingMode = ProcessingMode.Local;

            reportviewer.LocalReport.ReportEmbeddedResource = "E_Matricula.Relatorios.historicoM.rdlc";

            List<ReportParameter> listreportparameter = new List<ReportParameter>();

            string certi = "O Diretor do C. E. E. J. A. PROF. NORBERTO SOARES RAMOS,  CERTIFICA, nos termos do Inciso do VII, Artigo 24 da Lei Federal 9394/96, que " + aluno + ", RG " + rg + ", Concluio o Ensino Médio - Modalidade Educação de Jovens e Adultos, Atendimento Individualizado e Flexível, no ano de " + ano_con+".";

            listreportparameter.Add(new ReportParameter("nome",aluno));
            listreportparameter.Add(new ReportParameter("rg",rg));
            listreportparameter.Add(new ReportParameter("ra",ra));
            listreportparameter.Add(new ReportParameter("municipio",cidade));
            listreportparameter.Add(new ReportParameter("mae",mae));
            listreportparameter.Add(new ReportParameter("estado",estado));
            listreportparameter.Add(new ReportParameter("pais", pais));
            listreportparameter.Add(new ReportParameter("certi", certi));
            listreportparameter.Add(new ReportParameter("data", DateTime.Now.ToString("dd/MM/yyyy")));
            listreportparameter.Add(new ReportParameter("serie_ant", Serie_ant));
            listreportparameter.Add(new ReportParameter("ins_ant", Estab_ant));
            listreportparameter.Add(new ReportParameter("ano_ant", Ano_ant));
            listreportparameter.Add(new ReportParameter("mu_ant", Cidade_ant));
            listreportparameter.Add(new ReportParameter("uf_ant", Uf_ant));
            //listreportparameter.Add(new ReportParameter("RG", Diretor));
            //listreportparameter.Add(new ReportParameter("RG", Rg_diretor));
            //listreportparameter.Add(new ReportParameter("RG", Secretario));
            //listreportparameter.Add(new ReportParameter("RG", Rg_secretario));
            //listreportparameter.Add(new ReportParameter("RG", dt_liv));
            //listreportparameter.Add(new ReportParameter("RG", Livro));
            //listreportparameter.Add(new ReportParameter("RG", Pag));
            //listreportparameter.Add(new ReportParameter("RG", Termo));
            //listreportparameter.Add(new ReportParameter("RG", dt_doc));
            //listreportparameter.Add(new ReportParameter("RG", Obs));

            #region preencher historico



            for (int i = 0; i < 12;i++)
            {
                listreportparameter.Add(new ReportParameter("ins_" + (i+1), ins_list[i]));
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

            string certi = "O Diretor do C. E. E. J. A. PROF. NORBERTO SOARES RAMOS,  CERTIFICA, nos termos do Inciso do VII, Artigo 24 da Lei Federal 9394/96, que " + aluno + ", RG " + rg + ", Concluio o Ensino Médio - Modalidade Educação de Jovens e Adultos, Atendimento Individualizado e Flexível, no ano de " + ano_con + ".";

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
            //listreportparameter.Add(new ReportParameter("RG", Diretor));
            //listreportparameter.Add(new ReportParameter("RG", Rg_diretor));
            //listreportparameter.Add(new ReportParameter("RG", Secretario));
            //listreportparameter.Add(new ReportParameter("RG", Rg_secretario));
            //listreportparameter.Add(new ReportParameter("RG", dt_liv));
            //listreportparameter.Add(new ReportParameter("RG", Livro));
            //listreportparameter.Add(new ReportParameter("RG", Pag));
            //listreportparameter.Add(new ReportParameter("RG", Termo));
            //listreportparameter.Add(new ReportParameter("RG", dt_doc));
            //listreportparameter.Add(new ReportParameter("RG", Obs));

            #region preencher historico



            for (int i = 0; i < 12; i++)
            {
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
            string nomeArquivoPDF = Path.GetTempPath() + "Hist_Fundamental_" + nnmat + ".pdf";

            filestreamPDF = new FileStream(nomeArquivoPDF, FileMode.Create);
            filestreamPDF.Write(bytePDF, 0, bytePDF.Length);
            filestreamPDF.Close();
            Process.Start(nomeArquivoPDF);
        }
    }
}
