namespace ms_tech.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Soluciones
    {
        [Key]
        public int IdSolucion { get; set; }

        public int IdProblema { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaCreacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaModificacion { get; set; }

        public virtual Problemas Problemas { get; set; }
    }
}
