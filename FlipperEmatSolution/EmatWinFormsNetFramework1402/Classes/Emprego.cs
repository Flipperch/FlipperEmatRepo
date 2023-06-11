using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Classes
{
    public class Emprego
    {
        private string nomeEmpresa;        
        private string telefone;
        private Cidade cidade;

        public Emprego()
        {
            this.Cidade = new Cidade();
        }

        public string NomeEmpresa { get => nomeEmpresa; set => nomeEmpresa = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        
        public Cidade Cidade { get => cidade; set => cidade = value; }
    }
}
