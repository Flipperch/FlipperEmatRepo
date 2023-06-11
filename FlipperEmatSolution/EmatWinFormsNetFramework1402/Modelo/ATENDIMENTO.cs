namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ATENDIMENTO")]
    public partial class ATENDIMENTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ATENDIMENTO()
        {
            ATENDIMENTO_ALUNO = new HashSet<ATENDIMENTO_ALUNO>();
        }

        [Key]
        public short CODIGO { get; set; }

        [Required]
        [StringLength(50)]
        public string NOME { get; set; }

        public byte COD_DISCIPLINA { get; set; }

        public bool MENCAO { get; set; }

        public bool ATIVO { get; set; }

        public byte? ORDEM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATENDIMENTO_ALUNO> ATENDIMENTO_ALUNO { get; set; }

        public virtual DISCIPLINA DISCIPLINA { get; set; }
    }
}
