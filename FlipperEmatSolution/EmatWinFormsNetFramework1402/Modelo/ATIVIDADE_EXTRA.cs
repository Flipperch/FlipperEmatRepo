namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ATIVIDADE_EXTRA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int COD_ENSINO_ALUNO { get; set; }

        public short COD_USUARIO { get; set; }

        public DateTime DT_ATIVIDADE_EXTRA { get; set; }

        public virtual ENSINO_ALUNO ENSINO_ALUNO { get; set; }

        public virtual USUARIO USUARIO { get; set; }
    }
}
