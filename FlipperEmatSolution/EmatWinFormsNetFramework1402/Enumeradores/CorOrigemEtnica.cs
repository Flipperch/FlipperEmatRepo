using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Enumeradores
{
    public enum CorOrigemEtnica
    {
        [Description("BRANCA")]
        BRANCA = 1,
        [Description("NEGRA")]
        NEGRA = 2,
        [Description("AMARELA")]
        AMARELA = 3,
        [Description("PARDA")]
        PARDA = 4,
        [Description("INDÍGINA")]
        INDÍGINA = 5,
        [Description("NÃO INFORMADO")]
        NÃO = 6,
    }
}
