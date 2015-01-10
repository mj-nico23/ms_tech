using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ms_tech.ViewModels
{
    public class EstadisticasViewModel
    {
        public int TipoReporte { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string Orden { get; set; }
        public string TipoOrden { get; set; }
    }
}