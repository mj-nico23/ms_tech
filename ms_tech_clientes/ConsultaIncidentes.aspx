<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaIncidentes.aspx.cs" Inherits="ms_tech_clientes.ConsultaIncidentes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consulta de Incidente</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-2.1.3.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="navbar navbar-inverse navbar-fixed-top">
                <div class="container">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a href="ConsultaIncidentes.aspx" class="navbar-brand">MS-Tech</a>
                    </div>
                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav"> 
                            <li class="nav navbar-nav navbar">
                                <a href="ConsultaIncidentes.aspx" class="nav navbar-nav navbar">Consulta de Incidentes</a>
                            </li>
                        </ul>
                        <ul class="nav navbar-nav">
                            <li class="nav navbar-nav navbar">
                                <a href="Soporte.aspx" class="nav navbar-nav navbar">Soporte Técnico</a>
                            </li>
                        </ul>
                        <div class="navbar-right">
                            <ul class="nav navbar-nav navbar-right">
                                <li class="dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><%=Session["LogOn"].ToString() %> <span class="caret"></span></a>
                                    <ul class="dropdown-menu" role="menu">
                                        <li><a href="#" class="nav navbar-nav navbar">Cambiar Contraseña</a></li>
                                        <li><a href="FinSession.aspx" class="nav navbar-nav navbar">Cerrar Sesión</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <br />
            <br />
            <div class="container body-content">
                <h2>Consulta de Incidentes</h2>
                <p>
                    <a href="Soporte.aspx">Crear Nuevo Incidente</a>
                </p>

                <asp:GridView ID="gvIncidentes" runat="server" AutoGenerateColumns="false" DataKeyNames="IdIncidente" OnRowCommand="gvIncidentes_RowCommand" CssClass="table" BorderWidth="0">
                    <RowStyle BorderWidth="0" />
                    <HeaderStyle BorderWidth="0" />
                    <Columns>
                        <asp:BoundField DataField="Producto" HeaderText="Producto" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" />
                        <asp:BoundField DataField="Problema" HeaderText="Problema" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" />
                        <asp:TemplateField ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkVer" runat="server" CommandArgument='<%#Eval("IdIncidente") %>' CommandName="VIEW">Ver</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <hr />
                <footer>
                    <p>© 2015 - MS-Tech</p>
                </footer>
            </div>
        </div>
    </form>
</body>
</html>
