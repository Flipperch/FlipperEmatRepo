using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Relatorios.Geral
{
    public class DadosAlunoParaImpressao
    {
        public class Linha
        {

            public int Nmat { get; set; }
            public string Nome { get; set; }
            public string Rg { get; set; }
            public string Ra { get; set; }
            public string Celular { get; set; }
            public string Telefone { get; set; }
            public string Email { get; set; }
            public Modelo.ENSINO_ALUNO EnsinoAluno { get; set; }
            public Modelo.DISCIPLINA_ALUNO DisciplinaAluno { get; set; }
            public Modelo.ATENDIMENTO_ALUNO UltimoAtendimentoAluno { get; set; }
        }
    }
}
