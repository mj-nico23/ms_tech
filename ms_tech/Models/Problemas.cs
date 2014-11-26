namespace ms_tech.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Problemas
    {
        public Problemas()
        {
            Incidentes = new HashSet<Incidentes>();
            Soluciones = new HashSet<Soluciones>();
        }

        [Key]
        public int IdProblema { get; set; }

        public int IdProducto { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaCreacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaModificacion { get; set; }

        public virtual ICollection<Incidentes> Incidentes { get; set; }

        public virtual Productos Productos { get; set; }

        public virtual ICollection<Soluciones> Soluciones { get; set; }
    }
}
