namespace ms_tech.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IncidentesEstados
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdEstado { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdIncidente { get; set; }

        public int IdUsuario { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaActualizacion { get; set; }

        [Required]
        [StringLength(500)]
        public string Observacion { get; set; }

        public bool Finalizado { get; set; }

        public virtual Estados Estados { get; set; }

        public virtual Incidentes Incidentes { get; set; }
    }
}
