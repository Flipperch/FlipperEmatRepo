using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Classes
{
    public class Uf
    {
        private int codigo;
        private string nome;
        private string sigla;        
        private Pais pais;

        public Uf()
        {
            this.Pais = new Pais();
        }

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Sigla { get => sigla; set => sigla = value; }
        public Pais Pais { get => pais; set => pais = value; }

        #region Atributos

        #endregion

    }
}