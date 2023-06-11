namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DEFICIENCIA_USUARIO
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short COD_USUARIO { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short COD_DEFICIENCIA { get; set; }

        public string OBS { get; set; }

        public virtual DEFICIENCIA DEFICIENCIA { get; set; }

        public virtual USUARIO USUARIO { get; set; }
    }
}
