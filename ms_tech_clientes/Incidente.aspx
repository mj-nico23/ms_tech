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
        <div>
            <h2>Incidente Nro.
                <asp:Label ID="lblNroIncidente" runat="server" Text=""></asp:Label></h2>
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


                <dt>Estado
                </dt>

                <dd>
                    <asp:Label ID="lblEstado" runat="server" Text=""></asp:Label>
                </dd>

            </dl>

        </div>
    </form>
</body>
</html>
