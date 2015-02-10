<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Incidente.aspx.cs" Inherits="ms_tech_clientes.Incidente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Incidente</title>
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
                    <a href="#" class="navbar-brand">MS-Tech</a>

                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <div style="text-align: center; width: 100%;">
            <div id="divDetalle" style="text-align: left; width: 80%; display: inline-block" runat="server">
                <h2>Consulta de Incidentes</h2>
                <h3>Incidente Nro.
                <asp:Label ID="lblNroIncidente" runat="server" Text=""></asp:Label></h3>
                <hr />
                <dl class="dl-horizontal">
                    <dt>Cliente
                    </dt>
                    <dd>
                        <asp:Label ID="lblCliente" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Producto
                    </dt>
                    <dd>
                        <asp:Label ID="lblProducto" runat="server" Text=""></asp:Label>
                    </dd>
                    <dt>Problema
                    </dt>

                    <dd>
                        <asp:Label ID="lblProblema" runat="server" Text=""></asp:Label>
                    </dd>

                    <dt>Fecha
                    </dt>

                    <dd>
                        <asp:Label ID="lblFecha" runat="server" Text=""></asp:Label>
                    </dd>

                    <dt>Descripción
                    </dt>

                    <dd>
                        <asp:Label ID="lblDesc" runat="server" Text=""></asp:Label>
                    </dd>


                    <dt>Fecha Actualización
                    </dt>

                    <dd>
                        <asp:Label ID="lblFechaAct" runat="server" Text=""></asp:Label>
                    </dd>

                    <dt>Estado
                    </dt>

                    <dd>
                        <asp:Label ID="lblEstado" runat="server" Text=""></asp:Label>
                    </dd>

                </dl>

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
