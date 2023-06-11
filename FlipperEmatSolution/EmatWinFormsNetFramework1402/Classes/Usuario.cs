using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using EmatWinFormsNetFramework1402.Enumeradores;

namespace EmatWinFormsNetFramework1402.Classes
{
    public class Usuario : Pessoa
    {
        private int codigo;
        private string login;
        private string senha;
        private Enumeradores.NivelAcesso nivelAcesso;

        public int Codigo { get => codigo; set => codigo = value; }
        public string Login { get => login; set => login = value; }
        public string Senha { get => senha; set => senha = value; }
        public Enumeradores.NivelAcesso NivelAcesso { get => nivelAcesso; set => nivelAcesso = value; }
    }

}
