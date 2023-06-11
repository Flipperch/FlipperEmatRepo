namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UF")]
    public partial class UF
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UF()
        {
            CIDADE = new HashSet<CIDADE>();
        }

        [Key]
        public short CODIGO { get; set; }

        [Required]
        public string NOME { get; set; }

        [StringLength(2)]
        public string SIGLA { get; set; }

        public byte COD_PAIS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CIDADE> CIDADE { get; set; }

        public virtual PAIS PAIS { get; set; }
    }
}
