using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmatWinFormsNetFramework1402.Classes
{
    /// <summary>
    /// Classe de histórico escolar: Representa um histórico escolar que pode ser atribuído à um aluno
    /// </summary>
    class HistoricoEscolar
    {
        private string observacao;
       
        private string dtDocumento;
        private string dtConclusao;
        private string serieAnterior;
        private string instituicaoAnterior;
        private int anoAnterior;
        private string fundamentacao;
        private string gdae;
        private Cidade cidadeAnterior;
        private Usuario diretor;
        private Usuario secretario;
        private Usuario usuario;
        private Boolean segundaVia;

        public HistoricoEscolar()
        {
            cidadeAnterior = new Cidade();
            usuario = new Usuario();
            diretor = new Usuario();
            secretario = new Usuario();
        }

        public string Observacao { get => observacao; set => observacao = value; }       
        public string DtDocumento { get => dtDocumento; set => dtDocumento = value; }
        public string DtConclusao { get => dtConclusao; set => dtConclusao = value; }
        public string SerieAnterior { get => serieAnterior; set => serieAnterior = value; }
        public string InstituicaoAnterior { get => instituicaoAnterior; set => instituicaoAnterior = value; }
        public int AnoAnterior { get => anoAnterior; set => anoAnterior = value; }
        public string Fundamentacao { get => fundamentacao; set => fundamentacao = value; }
        public string Gdae { get => gdae; set => gdae = value; }
        public Usuario Diretor { get => diretor; set => diretor = value; }
        public Usuario Secretario { get => secretario; set => secretario = value; }
        public Usuario Usuario { get => usuario; set => usuario = value; }
        public Cidade CidadeAnterior { get => cidadeAnterior; set => cidadeAnterior = value; }
        public bool SegundaVia { get => segundaVia; set => segundaVia = value; }
    }
}
