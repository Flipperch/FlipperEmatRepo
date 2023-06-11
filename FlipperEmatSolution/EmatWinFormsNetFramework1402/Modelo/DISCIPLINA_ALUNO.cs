namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DISCIPLINA_ALUNO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DISCIPLINA_ALUNO()
        {
            ATENDIMENTO_ALUNO = new HashSet<ATENDIMENTO_ALUNO>();
            MEDIA = new HashSet<MEDIA>();
        }

        [Key]
        public int CODIGO { get; set; }

        public int? COD_ENSINO_ALUNO { get; set; }

        public byte? COD_DISCIPLINA { get; set; }

        public bool? ATUAL { get; set; }

        public bool? CONCLUIDA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATENDIMENTO_ALUNO> ATENDIMENTO_ALUNO { get; set; }

        public virtual DISCIPLINA DISCIPLINA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEDIA> MEDIA { get; set; }

        public virtual ENSINO_ALUNO ENSINO_ALUNO { get; set; }
    }
}
