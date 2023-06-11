namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ATENDIMENTO_ALUNO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ATENDIMENTO_ALUNO()
        {
            MEDIA = new HashSet<MEDIA>();
        }

        [Key]
        public int CODIGO { get; set; }

        public int COD_DISCIPLINA_ALUNO { get; set; }

        public short COD_ATENDIMENTO { get; set; }

        public short COD_PROFESSOR { get; set; }

        public DateTime DT_ATENDIMENTO { get; set; }

        public short? COD_PROFESSOR_MODIFICACAO { get; set; }

        public DateTime? DT_MODIFICACAO { get; set; }

        [StringLength(20)]
        public string MODULO { get; set; }

        public virtual ATENDIMENTO ATENDIMENTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEDIA> MEDIA { get; set; }

        public virtual NOTA NOTA { get; set; }

        public virtual DISCIPLINA_ALUNO DISCIPLINA_ALUNO { get; set; }

        public virtual PROFESSOR PROFESSOR { get; set; }

        public virtual PROFESSOR PROFESSOR1 { get; set; }
    }
}
