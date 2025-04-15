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
    public partial class WebForm1 : System.Web.UI.Page
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
                rptProducts.DataSource = dt;
                rptProducts.DataBind();
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            var btn = (System.Web.UI.WebControls.Button)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            var txtQty = (System.Web.UI.WebControls.TextBox)item.FindControl("txtQty");

            int productId = int.Parse(btn.CommandArgument);
            int quantity = int.TryParse(txtQty.Text, out int q) ? q : 1;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Check if item already in cart
                string checkQuery = "SELECT quantity FROM cart WHERE customer_name = 'Customer' AND product_id = @product_id";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@product_id", productId);
                conn.Open(); 
                object result = checkCmd.ExecuteScalar();

                if (result != null)
                {
                    // Update quantity
                    int existingQty = Convert.ToInt32(result);
                    string updateQuery = "UPDATE cart SET quantity = @qty WHERE customer_name = 'Customer' AND product_id = @product_id";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@qty", existingQty + quantity);
                    updateCmd.Parameters.AddWithValue("@product_id", productId);
                    updateCmd.ExecuteNonQuery();
                }
                else
                {
                    // Insert new item
                    string insertQuery = "INSERT INTO cart (customer_name, product_id, quantity) VALUES (@name, @product_id, @quantity)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@name", "Customer");
                    insertCmd.Parameters.AddWithValue("@product_id", productId);
                    insertCmd.Parameters.AddWithValue("@quantity", quantity);
                    insertCmd.ExecuteNonQuery();
                }
            }

            LoadProducts();
        }
    }
}