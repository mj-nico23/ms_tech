using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ms_tech_clientes
{
    public partial class ConsultaIncidentes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LogOn"] == null || Session["LogOn"].ToString() == "")
                {
                    Response.Redirect("Default.aspx");
                }

                gvIncidentes.DataSource = Incidentes1.RecuperarIncidentes(Session["LogOn"].ToString());
                gvIncidentes.DataBind();
            }
        }

        protected void gvIncidentes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VIEW")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string dealId = lnkView.CommandArgument;
                Response.Redirect("Incidente.aspx?incidente=" + dealId);
            }
        }

       
    }
}