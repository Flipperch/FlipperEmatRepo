using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Classes
{
    public class Cidade
    {
        private Int16 codigo;
        private string nome;
        private Uf uf;

        public Cidade()
        {
            this.Uf = new Uf();
        }

        public Int16 Codigo { get => codigo; set => codigo = value; }
        public string Nome { get => nome; set => nome = value; }
        public Uf Uf { get => uf; set => uf = value; }
    }
}
