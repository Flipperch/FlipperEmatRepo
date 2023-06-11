using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace EmatWinFormsNetFramework1402.Classes
{
    public abstract class Pessoa
    {
        private string nome; //
        private string rg; //
        private string dtNascimento; //
        private bool ativo;
        private List<DeficienciaPessoa> listaDeficienciaPessoa;

        public string Nome { get => nome; set => nome = value; }
        public string Rg { get => rg; set => rg = value; }
        public string DtNascimento { get => dtNascimento; set => dtNascimento = value; }
        public bool Ativo { get => ativo; set => ativo = value; }
        internal List<DeficienciaPessoa> ListaDeficienciaPessoa { get => listaDeficienciaPessoa; set => listaDeficienciaPessoa = value; }
    }

    
}
