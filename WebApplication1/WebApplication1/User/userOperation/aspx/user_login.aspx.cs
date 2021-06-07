using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 大作业
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //数据连接
            SqlConnection conn = new SqlConnection("server=.;user id=sa;password=123456;Database=Web");
            string cardId = Request["cardId"];//接收用户输入的卡号
            int password = Convert.ToInt32(Request["password"].ToString());//将密码转化为int型
            string sql=string.Format("SELECT password FROM [card] where cardId='{0}'", cardId);//根据用户输入的卡号找出对应密码
            SqlCommand cmd = new SqlCommand(sql, conn);//SqlCommand对象用于只与SQL Server建立连接后执行特定的语句,比如INSERT,DELETE,UPDATE等等这样的命令.
            conn.Open();     //打开连接
            if (cmd.ExecuteScalar() == null)//用于查询
            {
                Response.Write("-1");//账户不存在
                Response.End();
            }
            else
            {
                string password_sql = cmd.ExecuteScalar().ToString();//将数据库中密码赋值给password_sql
                string sql1 = string.Format("SELECT state FROM [card] where cardId='{0}'", cardId);//根据用户输入的卡号找出对应卡的状态
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                string state_sql = cmd1.ExecuteScalar().ToString();
                if (Convert.ToInt32(state_sql) == 0)
                {
                    Response.Write("-2");//账户处于冻结状态，禁止登录
                    Response.End();
                }
                else
                {
                    if (password == Convert.ToInt32(password_sql))
                    {
                        Response.Write("1");//账号密码正确，登录成功
                                            //Response.Redirect("../html/index.html");
                        Response.End();
                    }
                    if (password != Convert.ToInt32(password_sql))
                    {
                        Response.Write("0");//密码错误
                        Response.End();
                    }
                }
            }
            conn.Close();
        }
    }
}