namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEFICIENCIA")]
    public partial class DEFICIENCIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DEFICIENCIA()
        {
            DEFICIENCIA_ALUNO = new HashSet<DEFICIENCIA_ALUNO>();
            DEFICIENCIA_USUARIO = new HashSet<DEFICIENCIA_USUARIO>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CODIGO { get; set; }

        public int NOME { get; set; }

        public byte TIPO_DEFICIENCIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEFICIENCIA_ALUNO> DEFICIENCIA_ALUNO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEFICIENCIA_USUARIO> DEFICIENCIA_USUARIO { get; set; }
    }
}
