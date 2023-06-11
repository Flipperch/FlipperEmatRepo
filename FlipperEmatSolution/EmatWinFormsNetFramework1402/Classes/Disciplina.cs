using System.Collections.Generic;

namespace EmatWinFormsNetFramework1402.Classes
{
    /// <summary>
    /// Classe de Disciplina: Representa uma disciplina que pode ser atribuída para o aluno.
    /// Criado por Felipe A. Chagas
    /// </summary>
    public class Disciplina
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string NomeHistorico { get; set; }
        public int Capacidade { get; set; }
        public int Ordem { get; set; }
        public bool BloqAtribuicao { get; set; }
        public List<Atendimento> ListaAtendimento { get; set; }
        public string Horario { get; set; }
    }
}
