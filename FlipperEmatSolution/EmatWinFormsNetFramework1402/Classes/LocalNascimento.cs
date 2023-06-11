using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Classes
{
    public class LocalNascimento
    {
        private Cidade cidade;

        public LocalNascimento()
        {
            this.Cidade = new Cidade();
        }

        public Cidade Cidade { get => cidade; set => cidade = value; }
    }
}
