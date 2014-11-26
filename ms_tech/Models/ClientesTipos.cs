namespace ms_tech.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ClientesTipos
    {
        public ClientesTipos()
        {
            Clientes = new HashSet<Clientes>();
        }

        [Key]
        public int IdClienteTipo { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public virtual ICollection<Clientes> Clientes { get; set; }
    }
}
