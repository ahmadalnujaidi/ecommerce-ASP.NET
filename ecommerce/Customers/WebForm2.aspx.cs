using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecommerce.Customers
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadCart();
        }

        void LoadCart()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT c.product_id, p.name, p.description, c.quantity, p.price, 
                        (c.quantity * p.price) as total
                        FROM cart c 
                        JOIN products p ON c.product_id = p.product_id
                        WHERE c.customer_name = 'Customer'";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvCart.DataSource = dt;
                gvCart.DataBind();

                // calculate total
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                    total += Convert.ToDecimal(row["total"]);
                litCartTotal.Text = total.ToString("F2");
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Load cart again
                string getCartQuery = @"SELECT c.product_id, c.quantity, (c.quantity * p.price) AS total 
                        FROM cart c
                        JOIN products p ON c.product_id = p.product_id
                        WHERE c.customer_name = 'Customer'";
                SqlCommand cmdCart = new SqlCommand(getCartQuery, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmdCart);
                DataTable dtCart = new DataTable();
                da.Fill(dtCart);

                foreach (DataRow row in dtCart.Rows)
                {
                    SqlCommand insert = new SqlCommand(@"INSERT INTO orders 
                        (product_id, customer_name, customer_email, customer_phone, shipping_address, quantity, total_price)
                        VALUES (@pid, @name, @email, @phone, @address, @qty, @total)", conn);

                    insert.Parameters.AddWithValue("@pid", row["product_id"]);
                    insert.Parameters.AddWithValue("@name", txtName.Text);
                    insert.Parameters.AddWithValue("@email", txtEmail.Text);
                    insert.Parameters.AddWithValue("@phone", txtPhone.Text);
                    insert.Parameters.AddWithValue("@address", txtAddress.Text);
                    insert.Parameters.AddWithValue("@qty", row["quantity"]);
                    insert.Parameters.AddWithValue("@total", row["total"]);
                    insert.ExecuteNonQuery();
                }

                // Clear cart
                SqlCommand clear = new SqlCommand("DELETE FROM cart WHERE customer_name = 'Customer'", conn);
                clear.ExecuteNonQuery();
            }


            Response.Write("<script>alert('Order placed successfully!'); window.location='Webform1.aspx';</script>");
        }
        protected void gvCart_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int productId = Convert.ToInt32(gvCart.DataKeys[e.RowIndex].Value);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM cart WHERE customer_name = 'Customer' AND product_id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", productId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadCart();
        }
    }
}