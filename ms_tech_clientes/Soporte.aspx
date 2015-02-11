<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Soporte.aspx.cs" Inherits="ms_tech_clientes.Soporte" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Soporte Técnico</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-2.1.3.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
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
                                    <li><a href="CambiarContrasena.aspx" class="nav navbar-nav navbar">Cambiar Contraseña</a></li>
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
        <div class="container">
            <div class="form-group">
                <div style="text-align: center; width: 100%;">
                    <div style="text-align: left; width: 100%; display: inline-block">
                        <h2>Soporte Técnico</h2>
                    </div>
                    <br />
                    <div class="col-md-4">
                        <div style="text-align: left; width: 80%; display: inline-block">
                            <h3>Selección de Producto</h3>

                            <asp:TreeView ID="tree" runat="server" ForeColor="Black" SelectedNodeStyle-Font-Bold="true" OnSelectedNodeChanged="tree_SelectedNodeChanged"></asp:TreeView>
                        </div>
                    </div>
                    <div class="col-md-8" style="text-align: left;" runat="server" visible="false" id="divSoluciones">
                        <h3>Problemas Frecuentes</h3>
                        <h2>
                            <asp:Label ID="lblProducto" runat="server" Text="" CssClass="label label-default"></asp:Label></h2>
                        <br />
                        <p>
                            <asp:Label ID="lblProblema" runat="server" Text="Posibles Soluciones" Font-Bold="true"></asp:Label><br />
                            <asp:Label ID="lblSolucion" runat="server" Text=""></asp:Label>
                        </p>
                        <asp:Button ID="btnCrearIncidente" runat="server" Text="Crear Incidente" CssClass="btn btn-default" OnClick="btnCrearIncidente_Click"/>
                    </div>

                </div>
            </div>
        </div>
        <div style="align-content: center; text-align: center">
            <div style="text-align: left; width: 80%; display: inline-block">
                <hr />
                <footer>
                    <p>© 2015 - MS-Tech</p>
                </footer>
            </div>
        </div>
    </form>
</body>
</html>
