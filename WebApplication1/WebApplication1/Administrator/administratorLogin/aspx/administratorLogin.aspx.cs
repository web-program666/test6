using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1.Administrator.administratorLogin
{
    public partial class administratorLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn;
            conn = new SqlConnection(@"server =.; user id = sa; password =123456; database = Web");
            string name = Request["name"];
            int password =Convert.ToInt32(Request["password"].ToString());//将密码转换成int
            string sql = string.Format("SELECT password FROM administrator where userName ='{0}'", name);//对于是字符串类型的{}一定要加单引号，使其sql识别为字符串
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            if (cmd.ExecuteScalar() == null)
            {
                Response.Write(-1);    //账户不存在
                Response.End();
            }
            else
            {
                string password_sql = cmd.ExecuteScalar().ToString();
                if (password == Convert.ToInt32(password_sql))
                {
                    setCookie(name);
                    Response.Write(1);   //账号密码正确，登录成功
                    Response.End();
                }
                if (password != Convert.ToInt32(password_sql))
                {
                    Response.Write(0);   //密码错误
                    Response.End();
                }
            }
            conn.Close();
        }
        public void setCookie(string name)
        {
            HttpCookie newCookie = new HttpCookie("name");
            newCookie.Value = name;
            Response.AppendCookie(newCookie);
        }
    }
}