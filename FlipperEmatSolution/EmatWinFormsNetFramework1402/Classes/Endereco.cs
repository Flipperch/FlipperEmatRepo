using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Classes
{
    class Endereco
    {
        private string cep;
        private string logradouro;
        private int numero;
        private string bairro;
        private string complemento;       
        private Cidade cidade;
        public Endereco()
        {
            this.Cidade = new Cidade();
        }

        public string Cep { get => cep; set => cep = value; }
        public string Logradouro { get => logradouro; set => logradouro = value; }
        public int Numero { get => numero; set => numero = value; }
        public string Bairro { get => bairro; set => bairro = value; }
        public string Complemento { get => complemento; set => complemento = value; }
        public Cidade Cidade { get => cidade; set => cidade = value; }
    }
}
