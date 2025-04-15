using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecommerce
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadOrders();
        }

        void LoadOrders()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT 
                        o.customer_email,
                        MAX(o.customer_name) AS customer_name,
                        MAX(o.customer_phone) AS customer_phone,
                        MAX(o.shipping_address) AS shipping_address,
                        STRING_AGG(p.name + '|' + CAST(o.quantity AS NVARCHAR), ';') AS products,
                        SUM(o.total_price) AS total_price,
                        MIN(o.order_date) AS first_order,
                        MAX(o.order_date) AS last_order
                    FROM orders o
                    JOIN products p ON o.product_id = p.product_id
                    GROUP BY o.customer_email
                    ORDER BY last_order DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rptOrders.DataSource = dt;
                rptOrders.DataBind();
            }
        }

        // Format product list as <li>Product (Qty)</li>
        public string FormatProductList(string raw)
        {
            string[] items = raw.Split(';');
            string html = "";
            foreach (string item in items)
            {
                if (item.Contains("|"))
                {
                    string[] parts = item.Split('|');
                    html += $"<li>{parts[0]} (Qty: {parts[1]})</li>";
                }
            }
            return html;
        }
        protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "CompleteOrder")
            {
                string email = e.CommandArgument.ToString();

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // 1. Get products and quantities for that customer
                    string getOrders = @"SELECT product_id, quantity FROM orders WHERE customer_email = @email";
                    SqlCommand getCmd = new SqlCommand(getOrders, conn);
                    getCmd.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = getCmd.ExecuteReader();

                    var productsToUpdate = new List<(int productId, int qty)>();
                    while (reader.Read())
                    {
                        productsToUpdate.Add((reader.GetInt32(0), reader.GetInt32(1)));
                    }
                    reader.Close();

                    // 2. Update inventory for each product
                    foreach (var item in productsToUpdate)
                    {
                        SqlCommand updateQty = new SqlCommand(
                            "UPDATE products SET quantity = quantity - @qty WHERE product_id = @pid", conn);
                        updateQty.Parameters.AddWithValue("@qty", item.qty);
                        updateQty.Parameters.AddWithValue("@pid", item.productId);
                        updateQty.ExecuteNonQuery();
                    }

                    // 3. Delete all orders for the customer
                    SqlCommand deleteOrders = new SqlCommand(
                        "DELETE FROM orders WHERE customer_email = @email", conn);
                    deleteOrders.Parameters.AddWithValue("@email", email);
                    deleteOrders.ExecuteNonQuery();
                }

                LoadOrders(); // Refresh view
            }
        }
    }
}