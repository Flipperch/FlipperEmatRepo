using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Classes
{
    public class Atendimento
    {
        private int codigo;
        private string nome;
        private bool mencao;
        private bool ativo;
        private Disciplina disciplina;
        public int Ordem { get; set; }

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nome { get => nome; set => nome = value; }
        public bool Mencao { get => mencao; set => mencao = value; }
        public bool Ativo { get => ativo; set => ativo = value; }
        public Disciplina Disciplina { get => disciplina; set => disciplina = value; }
    }
}
