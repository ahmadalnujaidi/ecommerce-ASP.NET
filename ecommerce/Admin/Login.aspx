<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ecommerce.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding:50px; width:300px; margin:auto;">
            <h2>Login</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
            <br />
            Username: <asp:TextBox ID="txtUsername" runat="server" /><br /><br />
            Password: <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" /><br /><br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
        </div>
    </form>
</body>
</html>