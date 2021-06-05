using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BANK
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn;
                conn = new SqlConnection(@"server =.; user id = sa; password =123456; database = Web");
                conn.Open();
                var name = Request["adminName"];
                var password = Request["password2"];
                var confirm = Request["confirmText"];
                SqlCommand cmd = new SqlCommand("select * from verify", conn);
                SqlDataReader myReader = cmd.ExecuteReader();
                var dd = Judge(myReader, confirm.ToString());
                myReader.Close();
                if (dd)
                {
                    try
                    {
                        var sql = "INSERT INTO administrator(userName,password) VALUES ('" + name.ToString() + "','" + password + "')";
                        SqlCommand cmdSelect = new SqlCommand(sql, conn);
                        cmdSelect.ExecuteNonQuery();
                        Response.Write("1");
                    }
                    catch
                    {
                        Response.Write("用户名已注册！");
                    }

                }
                else
                {
                    Response.Write("验证信息错误！");
                }

            }
            catch
            {
                Response.Write("数据库连接有误！");
            }
            
           
        }

        private bool Judge(SqlDataReader myReader,string confirm )
        {
            while (myReader.Read())
            {
                if (myReader["verifyInfo"].ToString().Trim() == confirm.ToString())
                    return true;
            }
            return false;
        }

        
    }
}