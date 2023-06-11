using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Classes
{
    public class Rematricula
    {
        private int codigo;
        private DateTime data;
        private Usuario usuario;
        private EnsinoAluno ensinoAluno;

        public int Codigo { get => codigo; set => codigo = value; }
        public DateTime Data { get => data; set => data = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
        public EnsinoAluno EnsinoAluno { get => ensinoAluno; set => ensinoAluno = value; }
    }
}
