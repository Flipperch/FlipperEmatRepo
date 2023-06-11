namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MOVIMENTACAO")]
    public partial class MOVIMENTACAO
    {
        [Key]
        public int CODIGO { get; set; }

        public byte COD_SITUACAO { get; set; }

        public int COD_ENSINO_ALUNO { get; set; }

        public short COD_USUARIO { get; set; }

        public DateTime DT_MOVIMENTACAO { get; set; }

        public string MOTIVO { get; set; }

        public virtual ENSINO_ALUNO ENSINO_ALUNO { get; set; }

        public virtual USUARIO USUARIO { get; set; }
    }
}
