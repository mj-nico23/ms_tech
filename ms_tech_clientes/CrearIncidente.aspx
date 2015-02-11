<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearIncidente.aspx.cs" Inherits="ms_tech_clientes.CrearIncidente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crear Incidente</title>
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
        <div class="container body-content">
            <h2>Crear Incidente</h2>

            <div id="divDetalle" style="text-align: left; width: 80%; display: inline-block" runat="server">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label class="control-label col-md-2" for="IdProducto">Producto</label>
                        <div class="col-md-10">
                            <asp:Label ID="lblProducto" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-md-2" for="IdProblema">Problema</label>
                        <div class="col-md-10">
                            <asp:Label ID="lblProblema" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                            <div style="display: none">
                                <asp:Label ID="lblIdProblema" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-md-2" for="IdProblema">Fecha</label>
                        <div class="col-md-10">
                            <asp:Label ID="lblFecha" runat="server" Text="Label" CssClass="form-control"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-md-2" for="Descripcion">Descripcion</label>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="5" Columns="55" MaxLength="500" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button ID="btnCrearIncidente" OnClick="btnCrearIncidente_Click" runat="server" Text="Crear Incidente" CssClass="btn btn-default" />
                            <div class="text-danger">
                                <br />
                                <asp:Label ID="lblMsj" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <footer>
                    <p>© 2015 - MS-Tech</p>
                </footer>
            </div>

            <div id="divError" style="text-align: left; width: 80%; display: inline-block" runat="server" visible="false">
                <br />
                <span>Error en los parámetros de ingeso a la página
                </span>
                <hr />
                <footer>
                    <p>© 2015 - MS-Tech</p>
                </footer>
            </div>
        </div>
    </form>
</body>
</html>
