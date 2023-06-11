namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRINT_LOG
    {
        [Key]
        public int ID_PRINT_LOG { get; set; }

        public DateTime? DATETIME { get; set; }

        public int? N_MAT { get; set; }

        [StringLength(50)]
        public string REPORT_NAME { get; set; }

        public int? ID_USER { get; set; }

        [StringLength(300)]
        public string PARAMETERS { get; set; }
    }
}
