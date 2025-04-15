using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecommerce.Admin
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadProducts();
        }

        void LoadProducts()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM products", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO products (name, description, quantity, price) VALUES (@n, @d, @q, @p)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@n", txtName.Text);
                cmd.Parameters.AddWithValue("@d", txtDesc.Text);
                cmd.Parameters.AddWithValue("@q", int.Parse(txtQty.Text));
                cmd.Parameters.AddWithValue("@p", decimal.Parse(txtPrice.Text));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            LoadProducts();
        }

        protected void gvProducts_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            gvProducts.EditIndex = e.NewEditIndex;
            LoadProducts();
        }

        protected void gvProducts_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvProducts.Rows[e.RowIndex];
            int id = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value);
            string name = ((System.Web.UI.WebControls.TextBox)row.Cells[1].Controls[0]).Text;
            string desc = ((System.Web.UI.WebControls.TextBox)row.Cells[2].Controls[0]).Text;
            int qty = int.Parse(((System.Web.UI.WebControls.TextBox)row.Cells[3].Controls[0]).Text);
            decimal price = decimal.Parse(((System.Web.UI.WebControls.TextBox)row.Cells[4].Controls[0]).Text);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE products SET name=@n, description=@d, quantity=@q, price=@p WHERE product_id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@d", desc);
                cmd.Parameters.AddWithValue("@q", qty);
                cmd.Parameters.AddWithValue("@p", price);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            gvProducts.EditIndex = -1;
            LoadProducts();
        }

        protected void gvProducts_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            gvProducts.EditIndex = -1;
            LoadProducts();
        }

        protected void gvProducts_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM products WHERE product_id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadProducts();
        }
    }
}