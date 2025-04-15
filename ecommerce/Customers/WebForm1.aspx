<%@ Page Title="" Language="C#" MasterPageFile="~/Customers/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ecommerce.Customers.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <asp:Repeater ID="rptProducts" runat="server">
    <ItemTemplate>
        <div class="product-card">
            <h4><%# Eval("name") %></h4>
            <p><%# Eval("description") %></p>
            <p><strong>Price:</strong> $<%# Eval("price") %></p>
            Quantity:
            <asp:TextBox ID="txtQty" runat="server" Text="1" Width="40" />
            <br />
            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CommandArgument='<%# Eval("product_id") %>' OnClick="btnAddToCart_Click" />
        </div>
    </ItemTemplate>
</asp:Repeater>
    <div style="clear: both;"></div>
</asp:Content>