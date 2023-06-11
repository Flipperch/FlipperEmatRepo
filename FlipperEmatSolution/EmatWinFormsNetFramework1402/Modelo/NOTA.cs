namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NOTA")]
    public partial class NOTA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int COD_ATENDIMENTO_ALUNO { get; set; }

        [Column("NOTA")]
        public double NOTA1 { get; set; }

        public virtual ATENDIMENTO_ALUNO ATENDIMENTO_ALUNO { get; set; }
    }
}
