namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ENSINO_ALUNO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ENSINO_ALUNO()
        {
            DISCIPLINA_ALUNO = new HashSet<DISCIPLINA_ALUNO>();
            MOVIMENTACAO = new HashSet<MOVIMENTACAO>();
            REMATRICULA = new HashSet<REMATRICULA>();
        }

        [Key]
        public int CODIGO { get; set; }

        public int N_MAT { get; set; }

        public byte COD_ENSINO { get; set; }

        public bool ATUAL { get; set; }

        public DateTime DT_INICIO { get; set; }

        public DateTime? DT_TERMINO { get; set; }

        public virtual ALUNO ALUNO { get; set; }

        public virtual ATIVIDADE_EXTRA ATIVIDADE_EXTRA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DISCIPLINA_ALUNO> DISCIPLINA_ALUNO { get; set; }

        public virtual HISTORICO_ESCOLAR HISTORICO_ESCOLAR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOVIMENTACAO> MOVIMENTACAO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REMATRICULA> REMATRICULA { get; set; }
    }
}
