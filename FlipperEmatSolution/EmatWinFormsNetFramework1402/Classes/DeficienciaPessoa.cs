using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Classes
{
    /// <summary>
    /// Classe Tipo de Deficiencia: Representa uma opção de deficiencia que pode ser atribuida ao aluno.
    /// Criada por Felipe A. Chagas
    /// </summary>
    class DeficienciaPessoa
    {
        private Deficiencia deficiencia;
        private string observacao;

        public string Observacao { get => observacao; set => observacao = value; }
        internal Deficiencia Deficiencia { get => deficiencia; set => deficiencia = value; }
    }
}
