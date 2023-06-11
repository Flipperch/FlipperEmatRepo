using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using System.IO;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;

using EmatWinFormsNetFramework1402.Properties;
using EmatWinFormsNetFramework1402.Utils;

namespace EmatWinFormsNetFramework1402.Relatorios.Geral
{
    public class csRelatorios
    {
        public string nome_arquivo;        
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
       
        public void limpar_temp(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        
        #region Relatorios Gerais

        public lista_inativacao gera_crystal_lista_inativacao(DataTable dt, string data, DateTime dtRelatorio, IEmatriculaSettings settings)
        {
            lista_inativacao retorno = new lista_inativacao();

            retorno.SetDataSource(dt);

            retorno.SetParameterValue("data", data);
            retorno.SetParameterValue("data_relatorio", data_relatorio(dtRelatorio, settings));

            return retorno;
        }

        public static relatorio_secretaria gera_crystal_relatorio_secretaria(
            Utils.csDetalhesAluno.AlunosNoSistema alunosNoSistema,
            Utils.csDetalhesAluno.Matriculas matriculas,
            Utils.csDetalhesAluno.Rematriculas rematriculas, 
            Utils.csDetalhesAluno.HistoricoDeSituacoes historicoDeSituacoes,  
            Utils.csDetalhesAluno.ConclusoesDeEnsino conclusoesDeEnsino,
            List<bool> list_sup_secoes,            
            DataTable dt,
            string periodo_alteracoes,
            DateTime dtRelatorio, IEmatriculaSettings settings)
        {
            relatorio_secretaria retorno = new relatorio_secretaria();

            #region Usuários

            //retorno.SetDataSource(dt);

            retorno.Subreports[0].SetDataSource(dt);

            #endregion

            #region Alterações no Sistema

            retorno.SetParameterValue("periodo_alteracoes", periodo_alteracoes);

            retorno.SetParameterValue("mat_fundamental_manha", matriculas.fundamentalManha);
            retorno.SetParameterValue("mat_fundamental_tarde", matriculas.fundamentalTarde);
            retorno.SetParameterValue("mat_fundamental_noite", matriculas.fundamentalNoite);
            retorno.SetParameterValue("mat_fundamental_total", matriculas.fundamentalTotal);

            retorno.SetParameterValue("mat_medio_manha", matriculas.medioManha);
            retorno.SetParameterValue("mat_medio_tarde", matriculas.medioTarde);
            retorno.SetParameterValue("mat_medio_noite", matriculas.medioNoite);
            retorno.SetParameterValue("mat_medio_total", matriculas.medioTotal);

            retorno.SetParameterValue("mat_manha_total", matriculas.fundamentalManha + matriculas.medioManha);
            retorno.SetParameterValue("mat_tarde_total", matriculas.fundamentalTarde + matriculas.medioTarde);
            retorno.SetParameterValue("mat_noite_total", matriculas.fundamentalNoite + matriculas.medioNoite);
            retorno.SetParameterValue("mat_total", matriculas.fundamentalTotal + matriculas.medioTotal);

            retorno.SetParameterValue("rem_fundamental", rematriculas.fundamental);
            retorno.SetParameterValue("rem_medio", rematriculas.medio);
            retorno.SetParameterValue("rem_total", rematriculas.total);

            retorno.SetParameterValue("n_freq_fundamental", historicoDeSituacoes.n_frequetando_fundamental);
            retorno.SetParameterValue("n_freq_medio", historicoDeSituacoes.n_frequetando_medio);
            retorno.SetParameterValue("n_freq_total", historicoDeSituacoes.n_frequetando_total);

            retorno.SetParameterValue("can_fundamental", historicoDeSituacoes.cancelamento_fundamental);
            retorno.SetParameterValue("can_medio", historicoDeSituacoes.cancelamento_medio);
            retorno.SetParameterValue("can_total", historicoDeSituacoes.cancelamento_total);

            retorno.SetParameterValue("tran_fundamental", historicoDeSituacoes.transferencia_fundamental);
            retorno.SetParameterValue("tran_medio", historicoDeSituacoes.transferencia_medio);
            retorno.SetParameterValue("tran_total", historicoDeSituacoes.transferencia_total);

            retorno.SetParameterValue("con_fundamental", conclusoesDeEnsino.fundamental);
            retorno.SetParameterValue("con_medio", conclusoesDeEnsino.medio);
            retorno.SetParameterValue("con_total", conclusoesDeEnsino.total);

            #endregion

            #region Alunos do Sistema
                       
            //Ativo
            retorno.SetParameterValue("atv_fun", alunosNoSistema.atv_fun);
            retorno.SetParameterValue("atv_med", alunosNoSistema.atv_med);
            retorno.SetParameterValue("qtd_ativos", alunosNoSistema.qtd_ativos);

            //Inativo
            retorno.SetParameterValue("ina_fun", alunosNoSistema.ina_fun);
            retorno.SetParameterValue("ina_med", alunosNoSistema.ina_med);
            retorno.SetParameterValue("qtd_inativos", alunosNoSistema.qtd_inativos);

            retorno.SetParameterValue("qtd_concluintes", alunosNoSistema.qtd_concluintes);
            retorno.SetParameterValue("qtd_n_frequetando", alunosNoSistema.qtd_n_frequentando);
            retorno.SetParameterValue("qtd_cancelado", alunosNoSistema.qtd_cancelado);
            retorno.SetParameterValue("qtd_transferidos", alunosNoSistema.qtd_transferidos);


            #endregion

            #region Gráfico

            #endregion

            retorno.SetParameterValue("data_relatorio", data_relatorio(dtRelatorio, settings));

            #region Suprimir Seções

            retorno.sec_alteracoes.SectionFormat.EnableSuppress = list_sup_secoes[0];
            retorno.sec_alunos.SectionFormat.EnableSuppress = list_sup_secoes[1];
            retorno.sec_usuarios.SectionFormat.EnableSuppress = list_sup_secoes[2];
            retorno.sec_graficos.SectionFormat.EnableSuppress = list_sup_secoes[3];

            #endregion

            return retorno;
        }

        public relatorio_atendimentos gera_crystal_relatorio_atendimentos(List<bool> list_sup_secoes, string periodo_alteracoes, DataTable dt_atend, DataTable dt_disc, DateTime dtRelatorio, IEmatriculaSettings settings)
        {
            relatorio_atendimentos retorno = new relatorio_atendimentos();

            //DataSorce Primeiro

            retorno.Subreports["relatorio_qtd_atendimentos.rpt"].SetDataSource(dt_atend);
            retorno.Subreports["relatorio_qtd_alunos_disciplina.rpt"].SetDataSource(dt_disc);

            #region Atendimentos

            retorno.SetParameterValue("periodo_alteracoes", periodo_alteracoes);

            int atend_fun = 0;
            int atend_med = 0;

            for (int i = 0; i < dt_atend.Rows.Count; i++)
            {
                atend_fun = atend_fun + Convert.ToInt32(dt_atend.Rows[i][1].ToString());
                atend_med = atend_med + Convert.ToInt32(dt_atend.Rows[i][2].ToString());
            }

            retorno.SetParameterValue("atend_fun", atend_fun);
            retorno.SetParameterValue("atend_med", atend_med);
            retorno.SetParameterValue("atend_tot", atend_fun + atend_med);

            #endregion

            #region Disciplinas

            int disc_fun_atv = 0;
            int disc_fun_ina = 0;
            int disc_med_atv = 0;
            int disc_med_ina = 0;

            for (int i = 0; i < dt_disc.Rows.Count; i++)
            {
                disc_fun_atv = disc_fun_atv + Convert.ToInt32(dt_disc.Rows[i][1].ToString());
                disc_fun_ina = disc_fun_ina + Convert.ToInt32(dt_disc.Rows[i][2].ToString());
                disc_med_atv = disc_med_atv + Convert.ToInt32(dt_disc.Rows[i][3].ToString());
                disc_med_ina = disc_med_ina + Convert.ToInt32(dt_disc.Rows[i][4].ToString());
            }

            //Fundamental
            retorno.SetParameterValue("disc_fun_atv", disc_fun_atv);
            retorno.SetParameterValue("disc_fun_ina", disc_fun_ina);            

            //Médio
            retorno.SetParameterValue("disc_med_atv", disc_med_atv);
            retorno.SetParameterValue("disc_med_ina", disc_med_ina);

            //Total
            retorno.SetParameterValue("disc_fun", disc_fun_atv + disc_med_atv); //Soma do Ativo
            retorno.SetParameterValue("disc_med", disc_fun_ina + disc_med_ina); //Soma do Inativo        

            #endregion

            #region Gráfico

            #endregion

            retorno.SetParameterValue("data_relatorio", data_relatorio(dtRelatorio, settings));

            #region Suprimir Seções

            retorno.sec_atendimentos.SectionFormat.EnableSuppress = list_sup_secoes[0];
            retorno.sec_totaisAtendimentos.SectionFormat.EnableSuppress = list_sup_secoes[0];
            retorno.sec_disciplinas.SectionFormat.EnableSuppress = list_sup_secoes[1];
            retorno.sec_totaisDisciplinas.SectionFormat.EnableSuppress = list_sup_secoes[1];
            retorno.sec_graficos.SectionFormat.EnableSuppress = list_sup_secoes[2];

            #endregion

            return retorno;
        }

        public detalhes_disciplinas gera_crystal_detalhes_disciplinas(DateTime dt_inicial, DateTime dt_final)
        {
            detalhes_disciplinas retorno = new detalhes_disciplinas();

            //#region Obter Dados

            

            //DataTable dt = new DataTable();
            //dt.Columns.Add("DISCIPLINA");

            //dt.Columns.Add("INI_FUN");
            //dt.Columns.Add("INI_MED");

            //dt.Columns.Add("RET_FUN");
            //dt.Columns.Add("RET_MED");

            //dt.Columns.Add("AVA_FUN");
            //dt.Columns.Add("AVA_MED");

            //dt.Columns.Add("ATE_FUN");
            //dt.Columns.Add("ATE_MED");

            //dt.Columns.Add("CON_FUN");
            //dt.Columns.Add("CON_MED");

            //dt.Columns.Add("TOT_FUN");
            //dt.Columns.Add("TOT_MED");

            //for(int i = 0; i < listTipoAtendimentos.Count; i++)
            //{
            //    int qtd_fun = 0;
            //    int qtd_med = 0;
            //    string disciplina = "";

                
            //}

            

            //#endregion


            //for ()
            //{

            //}

            //retorno.SetDataSource(dt);

            return retorno;
        }

        #endregion

        //TODO==> csRelatorios PASSAR Formato de data_relatorio para appConfig/EmatriculaSettings...
        //Verificar o que realmente esse método está fazendo... Separar.. (ele testa e aplica formato)
        ///Funções
        //Primeira caixa alta.
        public  static string data_relatorio(DateTime data_relatorio_, IEmatriculaSettings settings)
        {
            string strdata = "";

            if (data_relatorio_.ToString("dd/MM/yyyy") == "01/01/0001")
                data_relatorio_ = DateTime.Now;

            if(settings.Ceeja.ToLower() == "americana")
            {
                strdata = data_relatorio_.ToString("'Americana,' dd 'de' MMMM 'de' yyyy");
            }
            else if (settings.Ceeja.ToLower() == "sorocaba")
            {
                strdata = data_relatorio_.ToString("'Sorocaba,' dd 'de' MMMM 'de' yyyy");
            }                
            return strdata;
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
            DataTable tb = new DataTable();

            tb.Columns.Add("data");
            tb.Columns.Add("item");
            tb.Columns.Add("quem");

            //Matricula
            tb.Rows.Add();
            //tb.Rows[0][0] = cs_alunos.dat_mat;
            tb.Rows[0][1] = "MATRICULADO";
            //Classes.Usuario objUsuario = new Classes.Usuario(cs_alunos.id_usuario_cad);
            //tb.Rows[0][2] = objUsuario.Nome;

            //Atendimentos
            //DataTable tab_atendimento = cs_atendimentos.buscarAtendimento_com(n_mat, 0);
            
            //List<Classes.AtendimentoAluno> listaDeAtendimentos = DAO.AtendimentoDAO.ExibirTodos(Convert.ToInt32(n_mat));

           //for (int i = 0; i < listaDeAtendimentos.Count; i++)
           //{
           //    //table.Columns.Add("id_atendimento");             //0
           //    //table.Columns.Add("id_tipo_atendimento");        //1
           //    //table.Columns.Add("data_atendimento");           //2 
           //    //table.Columns.Add("modulo");                     //3
           //    //table.Columns.Add("id_user_lanc");               //4
           //    //table.Columns.Add("id_disciplina"); 
           //
           //    tb.Rows.Add();
           //    tb.Rows[i + tb.Rows.Count][0] = listaDeAtendimentos[i].DtDoAtendimento;
           //    tb.Rows[i + tb.Rows.Count][1] = listaDeAtendimentos[i].NomeDoTipoDeAtendimento;
           //    tb.Rows[i + tb.Rows.Count][2] = listaDeAtendimentos[i].ProfessorAtribuiuAtendimento.Nome;
           //}

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