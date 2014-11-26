namespace ms_tech.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Estados
    {
        public Estados()
        {
            IncidentesEstados = new HashSet<IncidentesEstados>();
        }

        [Key]
        public int IdEstado { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public virtual ICollection<IncidentesEstados> IncidentesEstados { get; set; }
    }
}
