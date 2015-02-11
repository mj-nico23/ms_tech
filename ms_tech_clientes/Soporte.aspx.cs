using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ms_tech_clientes
{
    public partial class Soporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarTree();
        }

        private void CargarTree()
        {
            using (DataTable dtProductos = Productos.RecuperarProductos())
            {
                for (int i = 0; i < dtProductos.Rows.Count; i++)
                {
                    TreeNode node = new TreeNode(dtProductos.Rows[i]["nombre"].ToString(), dtProductos.Rows[i]["idProducto"].ToString());

                    using (DataTable dtProblemas = Productos.RecuperarProblemas(dtProductos.Rows[i]["idProducto"].ToString()))
                    {
                        for (int x = 0; x < dtProblemas.Rows.Count; x++)
                        {
                            TreeNode c1 = new TreeNode(dtProblemas.Rows[x]["nombre"].ToString(), dtProblemas.Rows[x]["idProblema"].ToString());
                            node.ChildNodes.Add(c1);
                        }
                    }

                    node.SelectAction = TreeNodeSelectAction.Expand;
                    tree.Nodes.Add(node);
                }

            }

        }

        protected void tree_SelectedNodeChanged(object sender, EventArgs e)
        {
            divSoluciones.Visible = false;
            if (tree.SelectedNode.ChildNodes.Count == 0)
            {
                lblProducto.Text = tree.SelectedNode.Text;
                lblSolucion.Text = Productos.RecuperarSolucion(tree.SelectedNode.Value);
                divSoluciones.Visible = true;
            }
        }

        protected void btnCrearIncidente_Click(object sender, EventArgs e)
        {
            Response.Redirect("CrearIncidente.aspx?p=" + tree.SelectedNode.Value);
        }
    }
}