using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Classes
{
    public class DisciplinaAluno
    {
        private int codigo;
        private bool atual;
        private bool concluida;        
        private List<AtendimentoAluno> listaAtendimentoAluno;        
        private Disciplina disciplina;        
        private Media media;

        //Não carrega no DAO
        private EnsinoAluno ensinoAluno;
        public DisciplinaAluno()
        {
            listaAtendimentoAluno = new List<AtendimentoAluno>();
            disciplina = new Disciplina();
            media = new Media();            
        }

        public int Codigo { get => codigo; set => codigo = value; }
        public bool Atual { get => atual; set => atual = value; }
        public bool Concluida { get => concluida; set => concluida = value; }
        public List<AtendimentoAluno> ListaAtendimentoAluno { get => listaAtendimentoAluno; set => listaAtendimentoAluno = value; }
        public Disciplina Disciplina { get => disciplina; set => disciplina = value; }
        public EnsinoAluno EnsinoAluno { get => ensinoAluno; set => ensinoAluno = value; }
        internal Media Media { get => media; set => media = value; }
    }
}
