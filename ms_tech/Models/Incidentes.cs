namespace ms_tech.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Incidentes
    {
        public Incidentes()
        {
            IncidentesEstados = new HashSet<IncidentesEstados>();
        }

        [Key]
        public int IdIncidente { get; set; }

        public int IdUsuario { get; set; }

        public int IdCliente { get; set; }

        public int IdProblema { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        public int IdPrioridad { get; set; }

        public byte? Calificacion { get; set; }

        [StringLength(500)]
        public string Comentario { get; set; }

        public virtual Clientes Clientes { get; set; }

        public virtual Problemas Problemas { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual ICollection<IncidentesEstados> IncidentesEstados { get; set; }

        public virtual Prioridades Prioridades { get; set; }
    }
}
