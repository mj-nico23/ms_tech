using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ms_tech.ViewModels
{
    public class CambiarPasswordViewModel
    {
        [Key]
        public int IdUsuario { get; set; }

        [Display(Name = "Contraseña Anterior")]
        public string OldPassword { get; set; }

        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }
    }
}