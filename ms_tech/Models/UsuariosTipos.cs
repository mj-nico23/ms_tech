namespace ms_tech.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UsuariosTipos
    {
        public UsuariosTipos()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        [Key]
        public int IdUsuarioTipo { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public bool Activo { get; set; }

        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
