namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LOCAL_NASCIMENTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int N_MAT { get; set; }

        public short? COD_CIDADE { get; set; }

        public virtual ALUNO ALUNO { get; set; }

        public virtual CIDADE CIDADE { get; set; }
    }
}
