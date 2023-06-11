namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("REMATRICULA")]
    public partial class REMATRICULA
    {
        [Key]
        public int CODIGO { get; set; }

        public DateTime DT_REMATRICULA { get; set; }

        public short COD_USUARIO { get; set; }

        public int COD_ENSINO_ALUNO { get; set; }

        public virtual ENSINO_ALUNO ENSINO_ALUNO { get; set; }

        public virtual USUARIO USUARIO { get; set; }
    }
}
