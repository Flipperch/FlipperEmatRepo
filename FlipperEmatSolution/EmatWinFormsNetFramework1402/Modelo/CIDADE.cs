namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CIDADE")]
    public partial class CIDADE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CIDADE()
        {
            HISTORICO_ESCOLAR = new HashSet<HISTORICO_ESCOLAR>();
            ENDERECO_ALUNO = new HashSet<ENDERECO_ALUNO>();
            EMPREGO_ALUNO = new HashSet<EMPREGO_ALUNO>();
            LOCAL_NASCIMENTO = new HashSet<LOCAL_NASCIMENTO>();
        }

        [Key]
        public short CODIGO { get; set; }

        [Required]
        public string NOME { get; set; }

        public short COD_UF { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HISTORICO_ESCOLAR> HISTORICO_ESCOLAR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENDERECO_ALUNO> ENDERECO_ALUNO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPREGO_ALUNO> EMPREGO_ALUNO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOCAL_NASCIMENTO> LOCAL_NASCIMENTO { get; set; }

        public virtual UF UF { get; set; }
    }
}
