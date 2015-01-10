namespace ms_tech.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Usuarios
    {
        public Usuarios()
        {
            Incidentes = new HashSet<Incidentes>();
        }

        [Key]
        public int IdUsuario { get; set; }

        public int IdUsuarioTipo { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Apellido { get; set; }

        public bool Activo { get; set; }

        [StringLength(256)]
        public string Password { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaCreacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime FechaModificacion { get; set; }

        public virtual ICollection<Incidentes> Incidentes { get; set; }

        public virtual UsuariosTipos UsuariosTipos { get; set; }

        public int ObtenerId(string userEmail)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Usuarios.Where(a => a.Email.Equals(userEmail)).FirstOrDefault();
            if (user != null)
            {
                return user.IdUsuario;
            }
            return 0;
        }

        public Usuarios Obtener(string userEmail)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var user = db.Usuarios.Where(a => a.Email.Equals(userEmail)).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
