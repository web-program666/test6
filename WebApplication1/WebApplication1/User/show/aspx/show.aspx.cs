using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 余额查询
{
    public partial class show : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var cardId =Request["cardId"];
            SqlConnection conn = new SqlConnection(@"server=.;database=Web;Integrated Security = true");
            conn.Open();

            SqlCommand cmd = new SqlCommand("select balance from card where cardId=@Id", conn);
            SqlParameter parameter = new SqlParameter("@Id", SqlDbType.VarChar);
            parameter.Value = cardId;
            cmd.Parameters.AddWithValue("@Id", cardId);
            SqlDataReader myReader = cmd.ExecuteReader();

            myReader.Read();
            Response.Write(myReader["balance"]);
            myReader.Close();

            conn.Close();
            Response.End();
        }
    }
}