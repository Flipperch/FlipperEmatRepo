namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USUARIO")]
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            ALUNO = new HashSet<ALUNO>();
            ATIVIDADE_EXTRA = new HashSet<ATIVIDADE_EXTRA>();
            HISTORICO_ESCOLAR = new HashSet<HISTORICO_ESCOLAR>();
            HISTORICO_ESCOLAR1 = new HashSet<HISTORICO_ESCOLAR>();
            HISTORICO_ESCOLAR2 = new HashSet<HISTORICO_ESCOLAR>();
            LOG_ACESSO = new HashSet<LOG_ACESSO>();
            MOVIMENTACAO = new HashSet<MOVIMENTACAO>();
            REMATRICULA = new HashSet<REMATRICULA>();
            DEFICIENCIA_USUARIO = new HashSet<DEFICIENCIA_USUARIO>();
            MEDIA = new HashSet<MEDIA>();
            MEDIA1 = new HashSet<MEDIA>();
        }

        [Key]
        public short CODIGO { get; set; }

        [StringLength(100)]
        public string NOME { get; set; }

        [StringLength(20)]
        public string NOME_ACESSO { get; set; }

        [StringLength(20)]
        public string SENHA { get; set; }

        [StringLength(20)]
        public string RG { get; set; }

        public byte? NIVEL_ACESSO { get; set; }

        public bool ATIVO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ALUNO> ALUNO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATIVIDADE_EXTRA> ATIVIDADE_EXTRA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HISTORICO_ESCOLAR> HISTORICO_ESCOLAR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HISTORICO_ESCOLAR> HISTORICO_ESCOLAR1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HISTORICO_ESCOLAR> HISTORICO_ESCOLAR2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOG_ACESSO> LOG_ACESSO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MOVIMENTACAO> MOVIMENTACAO { get; set; }

        public virtual PROFESSOR PROFESSOR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REMATRICULA> REMATRICULA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEFICIENCIA_USUARIO> DEFICIENCIA_USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEDIA> MEDIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEDIA> MEDIA1 { get; set; }
    }
}
