using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1.User.tradingInquiry.aspx
{
    public partial class tradingInquiry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn;
            conn = new SqlConnection(@"server =.; user id = sa; password =123456; database = Web");
            int cardId = int.Parse(Request["cardId"]);
            conn.Open();
            var sql = "SELECT exchangeTime,exchangeMoney,cardId2 from exchange where cardId=" +cardId;
            SqlDataAdapter adpt = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "1");
            DataTable dt = ds.Tables["1"];
            var str = "[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i != 0)
                {
                    str = str + ",";
                }
                str = str + "{";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j != 0)
                    {
                        str = str + ",";
                    }
                    str = str + "\"" + dt.Columns[j].ColumnName.ToString() + "\"" + ":" + "\"" + dt.Rows[i][j].ToString() + "\"";
                }
                str = str + "}";
            }
            str = str + "]";
            Response.Write(str);//返回json的字符串形式
            Response.End();
            conn.Close();


        }
    }
}