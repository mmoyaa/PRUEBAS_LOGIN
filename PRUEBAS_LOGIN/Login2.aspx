<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login2.aspx.cs" Inherits="PRUEBAS_LOGIN.WebForm1" %>

<!DOCTYPE html>

<html">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    
        <div>
    <form id="formulario_login" runat="server">
        <div class="form-control">
            <div class="col-md-6 text-center mb-5">
                <asp:Label cass="h3" ID="lblBienvenida" runat="server" Text="Bienvenido"></asp:Label>
                <asp:TextBox ID="tbUsuario" CssClass="form-control" runat="server" placeholder="Nombre usuario"></asp:TextBox>         
            </div>
            <div>
               
                <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                <asp:TextBox ID="tbPassword" CssClass="form-control" runat="server" placeholder="Password"></asp:TextBox>  
            </div>
            <div>
                <asp:Button ID="btnIngresar" CssClass="btn btn-primary btn-dark" runat="server" Text="Ingresar"/>  
            </div>
        </div>
       
    </form>
    </div>
  

</body>
</html>
