using EmatWinFormsNetFramework1402.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Relatorios.Geral
{
    public class Relatorios
    {
        public static rptListaAlunosDisciplina RptListaAlunosDisciplina(List<DadosAlunoParaImpressao.Linha> listaAlunos, string disciplina)
        {
            rptListaAlunosDisciplina rptListaAlunosDisciplina = new rptListaAlunosDisciplina();

            DataTable dt = new DataTable("ALUNO");
            dt.Columns.Add("NOME");
            dt.Columns.Add("RA");
            dt.Columns.Add("N_MAT");
            dt.Columns.Add("ENSINO");
            dt.Columns.Add("CELULAR");
            dt.Columns.Add("TELEFONE");
            dt.Columns.Add("ULTIMA_PRESENCA");

            foreach (var aluno in listaAlunos)
            {
                DataRow dr = dt.NewRow();
                dr["NOME"] = aluno.Nome;
                dr["RA"] = aluno.Ra;
                dr["N_MAT"] = aluno.Nmat;
                dr["ENSINO"] = (Enumeradores.Ensino)aluno.EnsinoAluno.COD_ENSINO;
                dr["CELULAR"] = aluno.Celular;
                dr["TELEFONE"] = aluno.Telefone;
                dr["ULTIMA_PRESENCA"] = aluno.UltimoAtendimentoAluno.DT_ATENDIMENTO;

                dt.Rows.Add(dr);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            rptListaAlunosDisciplina.SetDataSource(ds);
            rptListaAlunosDisciplina.SetParameterValue("DATA_IMPRESSAO", "DATA DE IMPRESSÃO: " + DateTime.Now.ToString("dd/MM/yyyy"));
            rptListaAlunosDisciplina.SetParameterValue("DISCIPLINA", "DISCIPLINA: " + disciplina);

            return rptListaAlunosDisciplina;
        }

        public static rptListaAlunosInativacao rptListaAlunosInativacao(List<DadosAlunoParaImpressao.Linha> listaAlunos, string data_corte)
        {
            rptListaAlunosInativacao rptListaAlunosInativacao = new rptListaAlunosInativacao();

            DataTable dt = new DataTable("ALUNO");
            dt.Columns.Add("N_MAT");
            dt.Columns.Add("NOME");
            dt.Columns.Add("RG");
            dt.Columns.Add("RA");
            dt.Columns.Add("ENSINO");
            dt.Columns.Add("DISCIPLINA");
            dt.Columns.Add("ULTIMA_PRESENCA");

            foreach (var aluno in listaAlunos)
            {
                DataRow dr = dt.NewRow();
                dr["N_MAT"] = aluno.Nmat;
                dr["NOME"] = aluno.Nome;
                dr["RG"] = aluno.Rg;
                dr["RA"] = aluno.Ra;
                dr["ENSINO"] = (Enumeradores.Ensino)aluno.EnsinoAluno.COD_ENSINO;
                dr["DISCIPLINA"] = aluno.DisciplinaAluno.DISCIPLINA.NOME;
                dr["ULTIMA_PRESENCA"] = aluno.UltimoAtendimentoAluno.DT_ATENDIMENTO;

                dt.Rows.Add(dr);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            rptListaAlunosInativacao.SetDataSource(ds);
            rptListaAlunosInativacao.SetParameterValue("DATA_IMPRESSAO", "DATA DE IMPRESSÃO: " + DateTime.Now.ToString("dd/MM/yyyy"));
            rptListaAlunosInativacao.SetParameterValue("DATA_CORTE", "ALUNOS ATIVOS COM ÚLTIMA PRESENÇA EM: " + data_corte);

            return rptListaAlunosInativacao;
        }
    }
}
