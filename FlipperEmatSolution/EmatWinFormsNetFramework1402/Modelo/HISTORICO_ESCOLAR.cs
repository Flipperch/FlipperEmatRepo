namespace EmatWinFormsNetFramework1402.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HISTORICO_ESCOLAR
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int COD_ENSINO_ALUNO { get; set; }

        public string OBS { get; set; }

        public short COD_USUARIO_DIRETOR { get; set; }

        public short COD_USUARIO_SECRETARIO { get; set; }

        public short COD_USUARIO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DT_LIVRO { get; set; }

        public string LIVRO { get; set; }

        public string PAGINA { get; set; }

        public string TERMO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DT_DOCUMENTO { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DT_CONCLUSAO { get; set; }

        public string SERIE_ANTERIOR { get; set; }

        public string INSTITUICAO_ANTERIOR { get; set; }

        public int? ANO_ANTERIOR { get; set; }

        public short? COD_CIDADE_ANTERIOR { get; set; }

        public string FUNDAMENTACAO { get; set; }

        public string GDAE { get; set; }

        public bool? SEGUNDA_VIA { get; set; }

        public virtual CIDADE CIDADE { get; set; }

        public virtual ENSINO_ALUNO ENSINO_ALUNO { get; set; }

        public virtual USUARIO USUARIO { get; set; }

        public virtual USUARIO USUARIO1 { get; set; }

        public virtual USUARIO USUARIO2 { get; set; }
    }
}
