namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LOG_CATRACA
    {
        [Key]
        public int CODIGO { get; set; }

        public int COD_CARTAO_ACESSO { get; set; }

        public DateTime DATA_HORA { get; set; }

        public bool? SENTIDO { get; set; }

        public virtual CARTAO_ACESSO CARTAO_ACESSO { get; set; }
    }
}
