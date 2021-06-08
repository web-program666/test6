using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dotnetApp;

namespace gestion_de_la_bibliothèque
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn;
            conn = new SqlConnection(@"server =.;database=library; user id = sa; password = 110158;");
            conn.Open();
            //if (conn.State == ConnectionState.Open)
            //{
            //    Response.Write("数据插入成功！");
            //}
            var iname = Request.Form["iname"];
            var ipwd = Request.Form["ipwd"];
            Response.Write(iname);

            string sql = string.Format("select * from admin where AdminName='{0}'", iname);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (cmd.ExecuteReader() == null)
            {
                Response.Write("不存在该用户！");
            }

            //SqlHelper.ConnectName = "CC";
            //var dt = SqlHelper.getDataTable("select * from admin order by AdminID");

            //foreach (DataRow dr1 in dt.Rows)
            //{
            //    Response.Write(dr1["AdminName"]);

            //    if (dr1["AdminName"].ToString() == "CC")
            //    {
            //        Response.Write("存在该用户！");
            //        //if (ipwd == dr1["AdminPwd"].ToString())
            //        //{
            //        //    Response.Write("登陆成功！");
            //        //}
            //        //else
            //        //{
            //        //    Response.Write("密码输入错误！");
            //        //}

            //    }
            //    else
            //    {
            //        Response.Write("不存在此用户！");
            //    }
            //}
        }
       
        
    }
}