<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ecommerce.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .order-card {
            background: #fff;
            border: 1px solid #ddd;
            border-radius: 10px;
            padding: 20px;
            margin-bottom: 20px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }

        .order-header {
            font-size: 1.1em;
            font-weight: bold;
            margin-bottom: 10px;
            color: #333;
        }

        .order-details p {
            margin: 4px 0;
        }

        .products-list {
            margin-top: 10px;
            padding-left: 15px;
        }
    </style>

    <h3>Customer Orders</h3>

    <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand">
    <ItemTemplate>
        <div class="order-card">
            <div class="order-header">👤 <%# Eval("customer_name") %> | <%# Eval("customer_email") %></div>
            <div class="order-details">
                <p><strong>Phone:</strong> <%# Eval("customer_phone") %></p>
                <p><strong>Address:</strong> <%# Eval("shipping_address") %></p>
                <p><strong>Order Period:</strong> <%# Eval("first_order", "{0:yyyy-MM-dd HH:mm}") %> → <%# Eval("last_order", "{0:yyyy-MM-dd HH:mm}") %></p>
                <p><strong>Total Price:</strong> $<%# Eval("total_price", "{0:F2}") %></p>
                <p><strong>Products:</strong></p>
                <ul class="products-list">
                    <%# FormatProductList(Eval("products").ToString()) %>
                </ul>

                <asp:Button ID="btnComplete" runat="server" Text="✅ Complete Order"
                    CommandName="CompleteOrder"
                    CommandArgument='<%# Eval("customer_email") %>' CssClass="btn" />
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
</asp:Content>
