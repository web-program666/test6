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
    public partial class transfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var password = Request["password"].ToString();
            var transferAmount = Convert.ToDouble(Request["transferAmount"]);
            var cardID2 = Request["cardID2"];//收款方卡号
            var email = Request["email"].ToString();
            var cardID1 = Request["cardID1"];//汇款方卡号
            SqlConnection conn = new SqlConnection("server=.;user id=sa;password=123456;Database=Web");
            conn.Open();//连接数据库
            var sql = "select C.balance,C.password,U.Email,U.name from card C JOIN [user] U ON C.cardId=U.cardId where C.cardId=@cardId";
            SqlCommand cmdSelect = new SqlCommand(sql, conn);
            cmdSelect.Parameters.AddWithValue("@cardId", cardID1);
            SqlDataAdapter adpt = new SqlDataAdapter(cmdSelect);
            DataTable remitter = new DataTable();
            adpt.Fill(remitter);//汇款方

            var sql2 = "select C.cardId,C.balance,C.state,U.Email,U.name from card C JOIN [user] U ON C.cardId=U.cardId where C.cardId=@cardId2";
            SqlCommand cmdSelect2 = new SqlCommand(sql2, conn);
            cmdSelect2.Parameters.AddWithValue("@cardId2", cardID2);
            SqlDataAdapter adpt2 = new SqlDataAdapter(cmdSelect2);
            DataTable payee = new DataTable();
            adpt2.Fill(payee);//收款方

            if (remitter.Rows.Count != 0)
            {
                if (remitter.Rows[0]["password"].ToString() != password)
                    Response.Write("密码错误");
                else if (Convert.ToDouble(remitter.Rows[0]["balance"]) - transferAmount < 0)
                {
                    Response.Write("金额不足"+ "\r\n余额为："+Convert.ToDouble(remitter.Rows[0]["balance"])+"元");
                }
                else
                {
                    if (payee.Rows.Count == 0)
                    {
                        Response.Write("收款方卡号不存在，请检查是否输入有误！");
                    }
                    else if (payee.Rows[0]["state"].ToString() != "1")
                    {
                        Response.Write("收款方银行卡目前已停用或冻结！");
                    }
                    else
                    {
                        SqlCommand cmdUpdate1 = new SqlCommand("update card set balance=@money1 where cardId=@ID1", conn);
                        cmdUpdate1.Parameters.AddWithValue("@ID1", cardID1);
                        var a = Convert.ToDouble(remitter.Rows[0]["balance"]) - transferAmount;
                        cmdUpdate1.Parameters.AddWithValue("@money1", a);
                        cmdUpdate1.ExecuteNonQuery();
                        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        var sql1 = "INSERT INTO exchange(cardId,exchangeTime,exchangeMoney,cardId2) VALUES ('" + cardID1 + "','" + time + "','" + -transferAmount + "','" + cardID2 + "')";
                        SqlCommand cmdInsert = new SqlCommand(sql1, conn);
                        cmdInsert.ExecuteNonQuery();
                        SqlCommand cmdUpdate2 = new SqlCommand("update card set balance=@money2 where cardId=@ID2", conn);
                        cmdUpdate2.Parameters.AddWithValue("@ID2", cardID2);
                        cmdUpdate2.Parameters.AddWithValue("@money2", Convert.ToDouble(payee.Rows[0]["balance"]) + transferAmount);
                        cmdUpdate2.ExecuteNonQuery();
                        if (email == "yes")
                        {
                           EMAIL.SendEmail(payee.Rows[0]["email"].ToString(), "转账记录", "转账记录：" + remitter.Rows[0]["name"].ToString() + "向你转账" + transferAmount + "元");                          
                           EMAIL.SendEmail(remitter.Rows[0]["email"].ToString(), "转账记录", "您已向：" + payee.Rows[0]["name"].ToString() + "转账" + transferAmount + "元" + "\r\n余额为：" + a + "元");

                        }
          
                Response.Write("转账成功!\r\n 汇款方：" + remitter.Rows[0]["name"].ToString() + "\r\n收款方:" + payee.Rows[0]["name"].ToString() + "\r\n转账金额为:" + transferAmount + "元" + "\r\n余额为：" + a + "元"+"\r\n转账时间为："+time);

                    }
                }

            }
            Response.End();
        }
    }
}