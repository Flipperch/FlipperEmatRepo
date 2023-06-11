namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CARTAO_ACESSO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CARTAO_ACESSO()
        {
            LOG_CATRACA = new HashSet<LOG_CATRACA>();
        }

        [Key]
        public int CODIGO { get; set; }

        public int? ALUNO { get; set; }

        public int? PROFESSOR { get; set; }

        public bool ESTADO { get; set; }

        public bool IMPRESSO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOG_CATRACA> LOG_CATRACA { get; set; }
    }
}
