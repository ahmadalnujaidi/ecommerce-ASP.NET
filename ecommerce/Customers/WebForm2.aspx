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
        Name: <asp:TextBox ID="txtName" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="required name" ControlToValidate="txtName" ForeColor="Red"></asp:RequiredFieldValidator>
        <br /><br />
        
        Email: <asp:TextBox ID="txtEmail" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required email" ControlToValidate="txtEmail" ForeColor="Red"></asp:RequiredFieldValidator>
        <br /><br />
        
        Phone: <asp:TextBox ID="txtPhone" runat="server" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Phone number must be 10 digits and start with 05" ControlToValidate="txtPhone" ForeColor="Red" ValidationExpression="^05[0-9]{8}$"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="required phone number" ControlToValidate="txtPhone"></asp:RequiredFieldValidator>
        <br /><br />
        

        Shipping Address: <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="3" Columns="40" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="required address" ControlToValidate="txtAddress" ForeColor="Red"></asp:RequiredFieldValidator>
        <br /><br />
        
        <asp:Button ID="btnCheckout" runat="server" Text="Place Order" OnClick="btnCheckout_Click" CssClass="btn" />
    </p>
</asp:Content>
