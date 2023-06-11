namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ERROR_LOG
    {
        [Key]
        public int ID_ERROR_LOG { get; set; }

        public DateTime? DATETIME { get; set; }

        [StringLength(50)]
        public string THREAD { get; set; }

        [StringLength(50)]
        public string TYPE { get; set; }

        [StringLength(300)]
        public string SOURCE { get; set; }

        public string MESSAGE { get; set; }

        public string EXCEPTION { get; set; }
    }
}
