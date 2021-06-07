using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1.User.perInfo.aspx
{
    public partial class userInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cardId = Request["cardId"].ToString();
            try
            {
                SqlConnection conn = new SqlConnection(@"server =.; user id = sa; password =123456; database = Web");
                conn.Open();
                string sql = string.Format("SELECT * FROM [user] where cardId={0}",cardId);
                try {
                    SqlDataAdapter adpt = new SqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    adpt.Fill(ds, "1");
                    DataTable dt = ds.Tables["1"];
                    string str = Json(dt);
                    Response.Write(str); //返回json;
                  
                }
                catch
                {
                    Response.Write(-2);//数据库语句执行错误
                }
            }
            catch
            {
                Response.Write(-1);  //数据库连接有误
            }
            Response.End();
        }
        public string Json(DataTable dt)   //转换成json的字符串形式
        {
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
            return str;
        }
    }
}