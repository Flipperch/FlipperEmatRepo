namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ALUNO")]
    public partial class ALUNO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ALUNO()
        {
            DEFICIENCIA_ALUNO = new HashSet<DEFICIENCIA_ALUNO>();
            ENSINO_ALUNO = new HashSet<ENSINO_ALUNO>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int N_MAT { get; set; }

        public DateTime? DT_MAT { get; set; }

        public string CPF { get; set; }

        public string RA { get; set; }

        public string RG { get; set; }

        public string UF_RG { get; set; }

        public string ORGAO_RG { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DT_RG { get; set; }

        public string NOME { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DT_NASCIMENTO { get; set; }

        public int? SEXO { get; set; }

        public string NOME_MAE { get; set; }

        public string NOME_PAI { get; set; }

        public int? ESTADO_CIVIL { get; set; }

        public int? COR_ORIGEM_ETNICA { get; set; }

        public string TELEFONE { get; set; }

        public string CELULAR { get; set; }

        public string TERMO_MAT { get; set; }

        public string E_MAIL { get; set; }

        public bool ATIVO { get; set; }

        public bool? CONCLUINTE { get; set; }

        public string OBS_PASSAPORTE { get; set; }

        public bool? APRESENTOU_CERTIDAO { get; set; }

        public bool? APRESENTOU_HISTORICO { get; set; }

        public string NOME_SOCIAL { get; set; }

        public short? COD_USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEFICIENCIA_ALUNO> DEFICIENCIA_ALUNO { get; set; }

        public virtual ENDERECO_ALUNO ENDERECO_ALUNO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ENSINO_ALUNO> ENSINO_ALUNO { get; set; }

        public virtual EMPREGO_ALUNO EMPREGO_ALUNO { get; set; }

        public virtual LOCAL_NASCIMENTO LOCAL_NASCIMENTO { get; set; }

        public virtual USUARIO USUARIO { get; set; }
    }
}
