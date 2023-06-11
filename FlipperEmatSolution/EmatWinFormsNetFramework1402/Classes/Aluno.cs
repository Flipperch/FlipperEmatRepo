using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace EmatWinFormsNetFramework1402.Classes
{
    /// <summary>
    /// Classe Aluno: Representa um Aluno e trás as informações de cadastro d associações ao aluno do CEEJA.
    /// Criada por Felipe A. Chagas
    /// </summary>
    public class Aluno : Pessoa
    {
        public int NMatricula { get; set; } //
        public DateTime DtMatricula { get; set; }
        public string Cpf { get; set; } //
        public string Ra { get; set; } //
        public string UfRg { get; set; } //
        public string OrgaoRg { get; set; } 
        public string DtRg { get; set; }
        public Enumeradores.Sexo Sexo { get; set; } //
        public string NomeMae { get; set; } //
        public string NomePai { get; set; } //
        public Enumeradores.EstadoCivil EstadoCivil { get; set; } //
        public Enumeradores.CorOrigemEtnica CorOrigemEtnica { get; set; } //
        public string Telefone { get; set; } 
        public string Celular { get; set; }
        public string TermoMatricula { get; set; }
        public string Email { get; set; } //
        public bool Concluinte { get; set; }
        public string Observacao { get; set; }
        public bool ApresentouHistorico { get; set; }
        public bool ApresentouCertificado { get; set; }
        public string NomeSocial { get; set; } //
        public LocalNascimento LocalNascimento { get; set; } //
        public List<EnsinoAluno> ListaEnsinoAluno { get; set; }
        public FotoAluno FotoDoAluno { get; set; }
        public Usuario Usuario { get; set; }
        internal Emprego Emprego { get; set; }
        internal Endereco Endereco { get; set; } //
        public EnsinoAluno EnsinoAlunoAtual { get; set; }


        public Aluno()
        {
            this.LocalNascimento = new LocalNascimento();
            this.Endereco = new Endereco();
            this.Emprego = new Emprego();
            this.ListaEnsinoAluno = new List<EnsinoAluno>();
        }

        #region Métodos

        public void GravarAluno()
        {
            try
            {
                DAO.AlunoDAO.Gravar(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InserirEnsinoAluno(Enumeradores.Ensino ensino, string dtInicioEnsinoAluno)
        {
            try
            {
                EnsinoAluno.InserirEnsinoAluno(new EnsinoAluno
                {
                    Ensino = ensino,
                    DtInicio = dtInicioEnsinoAluno,
                    //DtTermino = null, //TODO: TESTAR verificar se é necessário
                    Atual = true,
                    Aluno = this
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static EnsinoAluno GetEnsinoAlunoAtual(Aluno aluno)
        {
            foreach (EnsinoAluno ensinoAluno in aluno.ListaEnsinoAluno)
            {
                if (ensinoAluno.Atual)
                    return ensinoAluno;
            }
            return null;
        }

        public static DisciplinaAluno GetDisciplinaAlunoAtual(Aluno aluno)
        {
            foreach (DisciplinaAluno disciplinaAluno in GetEnsinoAlunoAtual(aluno).ListaDisciplinaAluno)
            {
                if (disciplinaAluno.Atual == true)
                    return disciplinaAluno;
            }
            return null;
        }

        #region Entity

        //public static Modelo.ENSINO_ALUNO GetEnsinoAlunoAtual(int n_mat)
        //{
        //    using (var context = new Modelo.Modelo())
        //    {
        //        //var ensino_aluno = context.ENSINO_ALUNO.Where(a => a.N_MAT == n_mat);
        //        return context.ENSINO_ALUNO.Where(a => a.N_MAT == n_mat && a.ATUAL == true).FirstOrDefault();
        //    }
        //}

        #endregion



        public static void setEnsinoAlunoAtual(Aluno aluno, Enumeradores.Ensino ensino, bool cadastro_aluno = false)
        {
            for (int i = 0; i < aluno.ListaEnsinoAluno.Count; i++)
            {
                if (aluno.ListaEnsinoAluno[i].Ensino == ensino)
                {
                    aluno.ListaEnsinoAluno[i].Atual = true;
                    // Atualizando EnsinoAluno
                    if(!cadastro_aluno) DAO.EnsinoAlunoDAO.Gravar(aluno.ListaEnsinoAluno[i]);
                }
                    
                else
                {
                    // Atualizando EnsinoAluno
                    aluno.ListaEnsinoAluno[i].Atual = false;
                    if (!cadastro_aluno) DAO.EnsinoAlunoDAO.Gravar(aluno.ListaEnsinoAluno[i]);
                }
                

            }
        }
        public static void setDisciplinaAlunoAtual(Aluno aluno, Disciplina disciplina, bool cadastro_aluno = true)
        {
            foreach (DisciplinaAluno disciplinaAluno in GetEnsinoAlunoAtual(aluno).ListaDisciplinaAluno)
            {
                if (disciplinaAluno.Disciplina.Codigo == disciplina.Codigo)
                {
                    disciplinaAluno.Atual = true;
                    //Atualizar Disciplina Aluno
                    if (!cadastro_aluno) DAO.DisciplinaAlunoDAO.Gravar(disciplinaAluno);
                }                    
                else
                {
                    if(disciplinaAluno.Atual)
                    {
                        disciplinaAluno.Atual = false;
                        //Atualizar Disciplina Aluno
                        if (!cadastro_aluno) DAO.DisciplinaAlunoDAO.Gravar(disciplinaAluno);
                    }                    
                }

            }
        }
        #endregion

        public static int QuantidadeAlunos()
        {
            var retorno = 0;

           
            return retorno;
        }
    }
}