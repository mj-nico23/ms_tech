using System;
using System.Data;

namespace ms_tech_clientes
{
    public partial class Incidente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["incidente"] != null)
            {
                string nroIncidente = Request.QueryString["incidente"].ToString();

                lblNroIncidente.Text = nroIncidente;

                using (DataTable dt = Incidentes1.RecuperarIncidente(nroIncidente))
                {
                    if (dt.Rows.Count > 0)
                    {
                        lblCliente.Text = dt.Rows[0]["Cliente"].ToString().Trim();
                        lblProducto.Text = dt.Rows[0]["Producto"].ToString().Trim();
                        lblProblema.Text = dt.Rows[0]["Problema"].ToString().Trim();
                        lblFecha.Text = dt.Rows[0]["FechaMostrar"].ToString().Trim();
                        lblDesc.Text = dt.Rows[0]["descripcion"].ToString().Trim();
                        lblEstado.Text = dt.Rows[0]["Estado"].ToString().Trim();
                    }
                }
            }
            else
            {
                throw new Exception("Error de datos...");
            }
        }
    }
}