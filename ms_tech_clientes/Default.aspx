<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ms_tech_clientes.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login de Cliente</title>
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
                        <a href="#" class="navbar-brand">MS-Tech</a>
                    </div>
                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav">
                            <li class="nav navbar-nav navbar">
                                <a href="#" class="nav navbar-nav navbar">Consulta de Incidentes</a>
                            </li>
                        </ul>
                        <ul class="nav navbar-nav">
                            <li class="nav navbar-nav navbar">
                                <a href="#" class="nav navbar-nav navbar">Soporte Técnico</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <h2>Login de Clientes</h2>
            <div class="row">
                <div class="col-md-8">
                    <section id="loginForm">
                        <div class="form-horizontal">

                            <h4>Use su Email y Contraseña para ingresar al sistema</h4>
                            <hr />

                            <div class="form-group">
                                <label class="control-label col-md-2">E-Mail</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="Email" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-2">Password</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-offset-2 col-md-10">
                                    <asp:Button ID="btnAceptar" runat="server" Text="Ingresar" OnClick="btnAceptar_Click"  CssClass="btn btn-default"/>
                                    <div class="text-danger">
                                        <br />
                                        <asp:Label ID="lblMsj" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                    </section>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
