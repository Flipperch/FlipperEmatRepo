using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmatWinFormsNetFramework1402.Enumeradores;

namespace EmatWinFormsNetFramework1402.Classes
{
    class Deficiencia
    {
        private int codigo;
        private string nome;
        private Enumeradores.TipoDeficiencia tipoDeficiencia;

        public int Codigo { get => codigo; set => codigo = value; }
        public string Nome { get => nome; set => nome = value; }
        public Enumeradores.TipoDeficiencia TipoDeficiencia { get => tipoDeficiencia; set => tipoDeficiencia = value; }
    }
}
