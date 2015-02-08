using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ms_tech.Models;

namespace ms_tech.ViewModels
{
    public class EstadisticasViewModel
    {
        public int TipoReporte { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string Orden { get; set; }
        public string TipoOrden { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public int IdClienteTipo { get; set; }
        public int IdUsuarioTipo { get; set; }

    }
}