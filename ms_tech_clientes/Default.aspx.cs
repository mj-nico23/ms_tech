using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ms_tech_clientes
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Email.Text.Trim() == "" || Password.Text == "")
            {
                lblMsj.Text = "Complete los datos de ingreso.";
                return;
            }

            string msj="";
            if (Cliente.ValidarUsuario(Email.Text, Password.Text, out msj))
            {
                Session["LogOn"] = msj;
                Response.Redirect("ConsultaIncidentes.aspx");
            }
            else
            {
                lblMsj.Text = msj;
            }
        }
    }
}