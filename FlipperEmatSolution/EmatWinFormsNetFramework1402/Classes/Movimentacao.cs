using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EmatWinFormsNetFramework1402.Enumeradores;

namespace EmatWinFormsNetFramework1402.Classes
{
    /// <summary>
    /// Classe Inativação: Representa cada movimentacao que o aluno recebe informando a data, motivo e etc.
    /// Criada por Felipe A. Chagas
    /// </summary>
    public class Movimentacao
    {
        private int codigo;
        private DateTime dtMovimentacao; 
        private string motivo; //Observação no ato da inativação      
        private Usuario usuario;   
        private SituacaoAluno situacaoAluno;
        private EnsinoAluno ensinoAluno;

        public int Codigo { get => codigo; set => codigo = value; }
        public DateTime DtMovimentacao { get => dtMovimentacao; set => dtMovimentacao = value; }
        public string Motivo { get => motivo; set => motivo = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
        public Enumeradores.SituacaoAluno SituacaoAluno { get => situacaoAluno; set => situacaoAluno = value; }
        public EnsinoAluno EnsinoAluno { get => ensinoAluno; set => ensinoAluno = value; }

        public static void InserirMovimentacao(Movimentacao movimentacao)
        {
            try
            {
                //Gravar movimentação no banco
                DAO.MovimentacaoDAO.InserirMovimentacao(movimentacao);
                //Adicionar na lista de movimentações do ensinoAluno
                movimentacao.ensinoAluno.ListaMovimentacao.Add(movimentacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

      
