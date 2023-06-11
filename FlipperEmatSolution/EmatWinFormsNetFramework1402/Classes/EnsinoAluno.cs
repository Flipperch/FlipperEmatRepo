using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EmatWinFormsNetFramework1402.Enumeradores;

namespace EmatWinFormsNetFramework1402.Classes
{
    public class EnsinoAluno
    {
        private int codigo;
        private Enumeradores.Ensino ensino;
        private bool atual;
        private string dtInicio;
        private string dtTermino;
        private HistoricoEscolar historicoEscolar;
        private List<DisciplinaAluno> listaDisciplinaAluno;
        private List<Movimentacao> listaMovimentacao;
        private List<Rematricula> listaRematricula;
        private AtividadeExtra atividadeExtra;
        //Não carrega no DAO
        private Aluno aluno;       

        public EnsinoAluno()
        {
            //HistoricoEscolar = new HistoricoEscolar();
            ListaDisciplinaAluno = new List<DisciplinaAluno>();
            ListaMovimentacao = new List<Movimentacao>();
            listaRematricula = new List<Rematricula>();
            atividadeExtra = new AtividadeExtra();            
        }

        public int Codigo { get => codigo; set => codigo = value; }
        public Enumeradores.Ensino Ensino { get => ensino; set => ensino = value; }
        public bool Atual { get => atual; set => atual = value; }
        public string DtInicio { get => dtInicio; set => dtInicio = value; }
        public string DtTermino { get => dtTermino; set => dtTermino = value; }
        public List<DisciplinaAluno> ListaDisciplinaAluno { get => listaDisciplinaAluno; set => listaDisciplinaAluno = value; }
        public List<Rematricula> ListaRematricula { get => listaRematricula; set => listaRematricula = value; }
        public Aluno Aluno { get => aluno; set => aluno = value; }
        internal HistoricoEscolar HistoricoEscolar { get => historicoEscolar; set => historicoEscolar = value; }
        internal List<Movimentacao> ListaMovimentacao { get => listaMovimentacao; set => listaMovimentacao = value; }
        internal AtividadeExtra AtividadeExtra { get => atividadeExtra; set => atividadeExtra = value; }

        public void InserirMovimentacaoNoEnsinoAluno(SituacaoAluno situacaoAluno, string motivo, DateTime dataMovimentacao)
        {
            try
            {
                //Verificar mudanças no ensino
                //TODO:: Irá Remover as alterações de Ativo e Concluinte (Aluno)
                //CÓDIGOS COMENTADOS POIS EDIÇÃO DOS DADOS DOALUNO NÃO SERÃO NECESSÁRIOS POR ESTE MÉTODO DE CRIAÇÃO DE MOVIMENTAÇÃO
                if (situacaoAluno == SituacaoAluno.ATIVO)
                {
                    //aluno.Ativo = true;
                    //aluno.Concluinte = false;
                    //DAO.AlunoDAO.Gravar(aluno);

                }
                else if (situacaoAluno == SituacaoAluno.CONCLUINTE) //Conclui o ensinoAluno
                {
                    //if (Ensino == Ensino.MÉDIO)
                    //{
                    //    aluno.Ativo = false;
                    //    aluno.Concluinte = true;
                    //    DAO.AlunoDAO.Gravar(aluno);
                    //}

                    DtTermino = dataMovimentacao.ToString();
                    DAO.EnsinoAlunoDAO.Gravar(this);
                }
                else if (situacaoAluno == Enumeradores.SituacaoAluno.FALECIDO ||
                    situacaoAluno == Enumeradores.SituacaoAluno.NÃO_COMPARECIMENTO ||
                    situacaoAluno == Enumeradores.SituacaoAluno.TRANSFERIDO)
                {
                    //aluno.Ativo = false;
                    //aluno.Concluinte = false;
                    //DAO.AlunoDAO.Gravar(aluno);
                }

                //DAO.EnsinoAlunoDAO.Gravar(this);

                //Inserir nova movimentação no ensinoAluno
                Movimentacao.InserirMovimentacao(
                    new Movimentacao
                    {
                        SituacaoAluno = situacaoAluno,
                        EnsinoAluno = this,
                        Usuario = Utils.csUsuarioLogado.usuario,
                        DtMovimentacao = dataMovimentacao,
                        Motivo = motivo
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static void InserirEnsinoAluno(EnsinoAluno ensinoAluno)
        {
            try
            {
                //Grava ensinoAluno no banco
                DAO.EnsinoAlunoDAO.Gravar(ensinoAluno);
                //Adiciona ensinoAluno na lista de Ensinos do Aluno
                ensinoAluno.aluno.ListaEnsinoAluno.Add(ensinoAluno);
                //Inserir movimentação de matrícula
                ensinoAluno.InserirMovimentacaoNoEnsinoAluno(SituacaoAluno.ATIVO, "MATRÍCULA", DateTime.Now);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public static void AtualizarEnsinoAluno(EnsinoAluno ensinoAluno, string dtInicio, string dtTermino, bool atual)
        {
            try
            {
                //TODO:: Verificar como irá se comportar na troca de ensinos (atual)
                ensinoAluno.DtInicio = dtInicio;
                ensinoAluno.DtTermino = dtTermino;
                ensinoAluno.Atual = atual;

                //Grava ensinoAluno no banco
                DAO.EnsinoAlunoDAO.Gravar(ensinoAluno);
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
