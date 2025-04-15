<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="ecommerce.Admin.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .section {
            margin-bottom: 40px;
        }

        .section h3 {
            border-bottom: 2px solid #ccc;
            padding-bottom: 5px;
            color: #333;
        }

        .form-row {
            margin-bottom: 10px;
        }

        .form-row label {
            display: inline-block;
            width: 100px;
        }

        .form-row input {
            width: 250px;
            padding: 5px;
        }

        .grid {
            margin-top: 20px;
            border-collapse: collapse;
            width: 100%;
        }

        .grid th, .grid td {
            border: 1px solid #ccc;
            padding: 8px;
            text-align: left;
        }

        .grid th {
            background-color: #f4f4f4;
        }

        .grid tr:hover {
            background-color: #f9f9f9;
        }

        .btn {
            padding: 6px 12px;
            margin-top: 10px;
            background-color: #007bff;
            color: white;
            border: none;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #0056b3;
        }
    </style>

    <div class="section">
        <h3>Stock Management</h3>
        <asp:GridView ID="gvProducts" CssClass="grid" runat="server" AutoGenerateColumns="False" DataKeyNames="product_id"
            OnRowEditing="gvProducts_RowEditing" OnRowUpdating="gvProducts_RowUpdating"
            OnRowCancelingEdit="gvProducts_RowCancelingEdit" OnRowDeleting="gvProducts_RowDeleting">
            <Columns>
                <asp:BoundField DataField="product_id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="name" HeaderText="Name" />
                <asp:BoundField DataField="description" HeaderText="Description" />
                <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="price" HeaderText="Price" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>

    <div class="section">
        <h3>Add New Product</h3>
        <div class="form-row">
            <label>Name:</label>
            <asp:TextBox ID="txtName" runat="server" />
        </div>
        <div class="form-row">
            <label>Description:</label>
            <asp:TextBox ID="txtDesc" runat="server" />
        </div>
        <div class="form-row">
            <label>Quantity:</label>
            <asp:TextBox ID="txtQty" runat="server" />
        </div>
        <div class="form-row">
            <label>Price:</label>
            <asp:TextBox ID="txtPrice" runat="server" />
        </div>
        <asp:Button ID="btnAdd" runat="server" Text="Add Product" CssClass="btn" OnClick="btnAdd_Click" />
    </div>
</asp:Content>
