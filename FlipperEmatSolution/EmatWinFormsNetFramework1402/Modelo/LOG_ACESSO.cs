namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LOG_ACESSO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CODIGO { get; set; }

        public short COD_USUARIO { get; set; }

        public bool TIPO { get; set; }

        public DateTime DATA_HORA_ACESSO { get; set; }

        public virtual USUARIO USUARIO { get; set; }
    }
}
