using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Enumeradores
{
    public enum EstadoCivil
    {
        [Description("SOLTEIRO(A)")]
        SOLTEIRO = 1,
        [Description("CASADO(A)")]
        CASADO = 2,
        [Description("SEPARADO(A)")]
        SEPARADO = 3,
        [Description("VIÚVO(A)")]
        VIUVO = 4,
        [Description("UNIÃO ESTÁVEL")]
        UNIAO = 5,
        [Description("DIVORCIADO(A)")]
        DIVORCIADO = 6
    }
}
