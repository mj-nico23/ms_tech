using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ms_tech.Models;

namespace ms_tech.ViewModels
{
    public class SolucionesViewModel
    {
        [Key]
        public int IdSolucion { get; set; }

        public int IdProblema { get; set; }
        public int IdProducto { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Solución")]
        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        [Display(Name = "Fecha de Creación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Fecha de Modificación")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime FechaModificacion { get; set; }

        public virtual Problemas Problemas { get; set; }
        public virtual Productos Productos { get; set; }

    }
}