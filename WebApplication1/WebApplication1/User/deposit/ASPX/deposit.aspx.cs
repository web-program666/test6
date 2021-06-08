using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Deposit
{
    public partial class deposit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var amount = Convert.ToDouble(Request["depo_amount"]);
                var pass = Request["password"];
                var mail = Request["email"];
                var cardId =Request["cardId"];

                SqlConnection conn = new SqlConnection(@"server=.;database=Web;Integrated Security = true");
                conn.Open();

                SqlCommand cmd1 = new SqlCommand("select C.balance,C.password,U.Email from card C JOIN [user] U ON C.cardId=U.cardId where C.cardId=@cardId", conn);
                SqlParameter parameter = new SqlParameter("@cardId", SqlDbType.VarChar);
                parameter.Value = cardId;
                cmd1.Parameters.AddWithValue("@cardId", cardId);
                SqlDataReader myReader = cmd1.ExecuteReader();

                while (myReader.Read())
                {
                    if (myReader["password"].ToString() != pass.ToString())
                    {
                        Response.Write("'密码错误!");
                    }
                    else
                    {
                        var emailTo = myReader["Email"].ToString();
                        var a = Convert.ToDouble(myReader["balance"]);
                        var ba = a + amount;
                        string strcommand = "update card set balance=@ba where cardID=@Id";

                        if (!myReader.IsClosed) myReader.Close();

                        SqlCommand cmd = new SqlCommand(strcommand, conn);
                        cmd.Parameters.AddWithValue("@ba", ba);
                        cmd.Parameters.AddWithValue("@Id", cardId);
                        cmd.ExecuteNonQuery();

                        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        var sql = "INSERT INTO exchange(cardId,exchangeTime,exchangeMoney) VALUES ('" + cardId + "','" + time + "','" + amount + "')";
                        SqlCommand cmdInsert = new SqlCommand(sql, conn);
                        cmdInsert.ExecuteNonQuery();

                        if (mail == "yes")
                        {
                            SendEmail(emailTo, "银行存款", "您已在本行存款" + amount + "元，剩余金额为" + ba + "元");
                        }
                        Response.Write("存款成功");
                        break;
                    }
                }
                if (!myReader.IsClosed) 
                    myReader.Close();

                conn.Close();
            }
            catch
            {
                Response.Write("数据库连接有误！");
            }
            Response.End();
        }

        public static bool SendEmail(string mailTo, string mailSubject, string mailContent)
        {
            // 设置发送方的邮件信息,例如使用网易的smtp
            string smtpServer = "smtp.qq.com"; //SMTP服务器
            string mailFrom = "2652620859@qq.com"; //登陆用户名
            string userPassword = "zteqjyuesrtodifa";//登陆密码-新版之后的QQ邮箱都是使用授权码,需要到邮箱-设置-账户里面找到-生成授权码-复制进来

            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;//由于使用了授权码必须设置该属性为true
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpServer; //指定SMTP服务器
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码

            // 发送邮件设置       
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(mailFrom, mailTo); // 发送人和收件人
            mailMessage.Subject = mailSubject;//主题
            mailMessage.Body = mailContent;//内容
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = System.Net.Mail.MailPriority.Low;//优先级

            try
            {
                smtpClient.Send(mailMessage); // 发送邮件
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }


}