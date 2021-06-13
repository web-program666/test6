using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication1.Administrator.cardIdInput
{
    public partial class cardIdInput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //数据连接
            SqlConnection conn = new SqlConnection("server=.;user id=sa;password=123456;Database=Web");
            string cardId = Request["cardId"];//接收用户输入的卡号
            //string cardId = "1";
            //int password = 111111;
            string sql = string.Format("SELECT password FROM [card] where cardId='{0}'", cardId);//根据用户输入的卡号找出对应密码
            SqlCommand cmd = new SqlCommand(sql, conn);//SqlCommand对象用于只与SQL Server建立连接后执行特定的语句,比如INSERT,DELETE,UPDATE等等这样的命令.
            conn.Open();     //打开连接
            if (cmd.ExecuteScalar() == null)//用于查询
            {
                Response.Write("-1");//账户不存在
                Response.End();
            }
            else
            {
                Response.Write("1");//登录成功
                setCookie(cardId);
                Response.End();

            }
        }
        public void setCookie(string name)
        {
            HttpCookie newCookie = new HttpCookie("cardId2");
            newCookie.Value = name;
            Response.AppendCookie(newCookie);
        }
    }
}