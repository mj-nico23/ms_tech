namespace ms_tech.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clientes
    {
        public Clientes()
        {
            Incidentes = new HashSet<Incidentes>();
        }

        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Mail { get; set; }

        public int IdClienteTipo { get; set; }

        [StringLength(256)]
        public string Password { get; set; }

        public bool Activo { get; set; }

        [StringLength(250)]
        public string Direccion { get; set; }

        [StringLength(50)]
        public string Telefono { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaCreacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaModificacion { get; set; }

        public virtual ClientesTipos ClientesTipos { get; set; }

        public virtual ICollection<Incidentes> Incidentes { get; set; }
    }
}
