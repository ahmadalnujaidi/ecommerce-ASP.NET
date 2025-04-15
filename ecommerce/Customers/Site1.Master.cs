using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ecommerce.Customers
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(quantity), 0) FROM cart WHERE customer_name = 'Customer'", conn);
                    litCartCount.Text = cmd.ExecuteScalar().ToString();
                }
            }
        }
    }
}