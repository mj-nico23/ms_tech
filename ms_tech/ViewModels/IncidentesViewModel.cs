using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ms_tech.Models;

namespace ms_tech.ViewModels
{
    public class IncidentesViewModel
    {
        [Key]
        public int IdIncidente { get; set; }

        public int IdUsuario { get; set; }

        public int IdCliente { get; set; }

        public int IdProducto { get; set; }
        public int IdProblema { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        public byte Prioridad { get; set; }

        public virtual Clientes Clientes { get; set; }

        public virtual Productos Productos { get; set; }
        public virtual Problemas Problemas { get; set; }

    }
}