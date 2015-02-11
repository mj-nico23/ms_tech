using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ms_tech_clientes
{
    public partial class CrearIncidente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["p"] == null && Request.QueryString["p"].ToString().Trim() == "")
                {
                    divDetalle.Visible = false;
                    divError.Visible = true;
                    return;
                }

                lblIdProblema.Text = Request.QueryString["p"].ToString().Trim();

                using (DataTable dt = Productos.RecuperarProblemasProducto(lblIdProblema.Text))
                {
                    if (dt.Rows.Count == 0)
                    {
                        divDetalle.Visible = false;
                        divError.Visible = true;
                        return;
                    }
                    lblProblema.Text = dt.Rows[0]["problema"].ToString().Trim();
                    lblProducto.Text = dt.Rows[0]["producto"].ToString().Trim();
                    lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDescripcion.Focus();
                }
                
            }
        }

        protected void btnCrearIncidente_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Trim() == "")
            {
                lblMsj.Text = "Debe ingresar una descripción del incidente.";
                return;
            }

            Incidentes1.CrearIncidente(lblIdProblema.Text, txtDescripcion.Text, Session["Id"].ToString());
            Response.Redirect("ConsultaIncidentes.aspx");
        }
    }
}