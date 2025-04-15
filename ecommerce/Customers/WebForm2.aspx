<%@ Page Title="" Language="C#" MasterPageFile="~/Customers/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="ecommerce.Customers.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Your Cart</h3>

<asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" DataKeyNames="product_id"
    OnRowDeleting="gvCart_RowDeleting">
    <Columns>
        <asp:BoundField DataField="name" HeaderText="Product" />
        <asp:BoundField DataField="description" HeaderText="Description" />
        <asp:BoundField DataField="quantity" HeaderText="Quantity" />
        <asp:BoundField DataField="price" HeaderText="Unit Price" />
        <asp:BoundField DataField="total" HeaderText="Total Price" />
        <asp:CommandField ShowDeleteButton="True" />
    </Columns>
</asp:GridView>
    <h4>Total: $<asp:Literal ID="litCartTotal" runat="server" /></h4>

    <h4>Checkout Information</h4>
    <p>
        Name: <asp:TextBox ID="txtName" runat="server" /><br /><br />
        Email: <asp:TextBox ID="txtEmail" runat="server" /><br /><br />
        Phone: <asp:TextBox ID="txtPhone" runat="server" /><br /><br />
        Shipping Address: <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="3" Columns="40" /><br /><br />
        <asp:Button ID="btnCheckout" runat="server" Text="Place Order" OnClick="btnCheckout_Click" CssClass="btn" />
    </p>
</asp:Content>
