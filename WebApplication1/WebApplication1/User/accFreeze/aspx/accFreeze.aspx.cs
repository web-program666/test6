using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1.User.accFreeze.aspx
{
    public partial class accFreeze : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cardId = Request["cardId"].ToString();
            try
            {
                SqlConnection conn = new SqlConnection(@"server =.; user id = sa; password =123456; database = Web");
                conn.Open();
                string sql = string.Format("UPDATE card SET state=0 where cardId={0}", cardId);//变成冻结状态
                try
                {
                    SqlCommand cmdSelect = new SqlCommand(sql, conn);
                    int i=cmdSelect.ExecuteNonQuery();
                    if (i == 1) Response.Write("1");//修改成功
                    else Response.Write("0");  //修改失败
                }
                catch
                {
                    Response.Write("-2");//数据库语句执行错误
                }
            }
            catch
            {
                Response.Write("-1");  //数据库连接有误
            }
            Response.End();
        }
    }
}