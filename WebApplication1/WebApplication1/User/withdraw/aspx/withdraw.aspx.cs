using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BANK
{
    public partial class test : System.Web.UI.Page
    {
        string str;
        protected void Page_Load(object sender, EventArgs e)
        {
            //获得前端输入数据       
            var password = Request["password"];
            var withdrawalAmount = Convert.ToDouble(Request["withdraw"]);
            var email = Request["email"];
            string cardID = Request["cardId"].ToString();//待解决自动记录已登录的用户卡号？？？？
            try
            {
                SqlConnection conn = new SqlConnection("server=.;user id=sa;password=123456;Database=Web");
                conn.Open();//连接数据库
                //从数据库获取用户余额、卡密码、邮箱地址信息
                SqlCommand cmd = new SqlCommand("select C.balance,C.password,U.Email from card C JOIN [user] U ON C.cardId=U.cardId where C.cardId=@cardId", conn);
                SqlParameter parameter = new SqlParameter("@cardId", SqlDbType.VarChar);
                parameter.Value = cardID;
                cmd.Parameters.Add(parameter);//添加参数 
                SqlDataReader myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    if (myReader["password"].ToString() != password.ToString())
                    {
                        Response.Write("'密码错误!");
                        break;
                    }
                    else if (Convert.ToInt32(myReader["balance"]) < withdrawalAmount)
                    {
                        Response.Write("金额不足!");
                        break;
                    }
                    else
                    {
                        var a = ((float)Convert.ToDouble(myReader["balance"]) - withdrawalAmount);
                        str = myReader["Email"].ToString();
                        if (!myReader.IsClosed) myReader.Close();
                        SqlCommand cmdUpdate = new SqlCommand("update card set balance=@money where cardId=cardID", conn);
                        cmdUpdate.Parameters.AddWithValue("@money", a);
                        cmdUpdate.ExecuteNonQuery();
                        //取款后同时插入一天流水记录
                        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        var sql1 = "INSERT INTO exchange(cardId,exchangeTime,exchangeMoney) VALUES ('" + cardID + "','" + time + "','" + -withdrawalAmount + "')";
                        SqlCommand cmdInsert = new SqlCommand(sql1, conn);
                        cmdInsert.ExecuteNonQuery();
                        if (email == "yes")
                            EMAIL.SendEmail(str, "银行取款", "您已在本行取款" + withdrawalAmount + "元，剩余金额为" + a + "元");
                        Response.Write("取款成功");
                        break;
                    }
                }
                if (!myReader.IsClosed) myReader.Close();
            }
            catch
            {
                Response.Write("数据库连接错误！");
            }
            Response.End();
        }





    }
}