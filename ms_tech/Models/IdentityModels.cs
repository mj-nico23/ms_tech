using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ms_tech.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ms_tech.Models.Usuarios> Usuarios { get; set; }

        public System.Data.Entity.DbSet<ms_tech.Models.UsuariosTipos> UsuariosTipos { get; set; }

        public System.Data.Entity.DbSet<ms_tech.Models.Clientes> Clientes { get; set; }

        public System.Data.Entity.DbSet<ms_tech.Models.ClientesTipos> ClientesTipos { get; set; }

        public System.Data.Entity.DbSet<ms_tech.Models.Productos> Productos { get; set; }

        public System.Data.Entity.DbSet<ms_tech.Models.Problemas> Problemas { get; set; }

        public System.Data.Entity.DbSet<ms_tech.Models.Soluciones> Soluciones { get; set; }

        public System.Data.Entity.DbSet<ms_tech.Models.Incidentes> Incidentes { get; set; }

        public System.Data.Entity.DbSet<ms_tech.Models.Prioridades> Prioridades { get; set; }
    }
}