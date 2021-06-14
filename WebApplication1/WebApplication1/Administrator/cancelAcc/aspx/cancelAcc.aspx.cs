using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace cancellation
{
    public partial class cancelAcc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取前端的值
            var Id = Request["card"];
            var pass = Request["password"];
            var path = @"C:\Users\Lenovo\Desktop\WebPic\" + Id + "0.png";

            //建立数据库连接
            SqlConnection conn = new SqlConnection(@"server =.; user id = sa; password =123456; database = Web");
            conn.Open();

            //判断密码是否正确
            var sql = "select password from card where cardId ='" + Id + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader myReader = cmd.ExecuteReader();
            //myReader.Read();
            if (myReader.Read())
            {
                if (myReader["password"].ToString() != pass.ToString())
                {
                    Response.Write("密码错误");
                }
                else
                {
                    myReader.Close();

                    //获取销户人的邮箱
                    var sql3 = "select Email from [user] where cardId='" + Id + "'";
                    SqlCommand cmd3 = new SqlCommand(sql3, conn);
                    SqlDataReader myReader1 = cmd3.ExecuteReader();
                    myReader1.Read();
                    var mail = myReader1["Email"].ToString();
                    myReader1.Close();

                    //在数据库中消除销户人的个人信息和卡信息
                    var sql1 = "delete [user] where cardId='" + Id + "'";
                    var sql2 = "delete [card] where cardId='" + Id + "'";
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    cmd1.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    cmd2.ExecuteNonQuery();
                    if (File.Exists(path) == true)
                    {
                        File.Delete(path);   //删除用户图片
                    }
                    Response.Write("您已在本行成功销户!相关邮件已发送至您的邮箱");
                    SendEmail(mail, "销户", "您已成功在本行销户!");
                }

                if (!myReader.IsClosed)
                    myReader.Close();

                conn.Close();
               
            }
            else
            {
                Response.Write("账户不存在！");
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