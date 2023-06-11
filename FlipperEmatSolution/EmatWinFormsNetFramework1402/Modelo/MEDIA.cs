namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MEDIA
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int COD_DISCIPLINA_ALUNO { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string VALOR { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime DT_MEDIA { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short COD_USUARIO { get; set; }

        public short? COD_USUARIO_MODIFICACAO { get; set; }

        public DateTime? DT_MODIFICACAO { get; set; }

        public int? COD_ATENDIMENTO_ALUNO { get; set; }

        public int? COD_CIDADE { get; set; }

        public string INSTITUICAO { get; set; }

        public virtual ATENDIMENTO_ALUNO ATENDIMENTO_ALUNO { get; set; }

        public virtual DISCIPLINA_ALUNO DISCIPLINA_ALUNO { get; set; }

        public virtual USUARIO USUARIO { get; set; }

        public virtual USUARIO USUARIO1 { get; set; }
    }
}
