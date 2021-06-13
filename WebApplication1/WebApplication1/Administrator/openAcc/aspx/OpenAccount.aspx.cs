using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BANK.openAccount
{
    public partial class OpenAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["WEB"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();//连接数据库
            }
            catch (Exception ex)
            {
                Response.Write("数据库连接有误！！！\r\n" + ex.Message.ToString());
                Response.End();
            }
           

            if (Request["msg"].ToString() == "0")   //获取卡号
            {
                //从数据库中获得已有卡号
                var ID = Request["identityID"].ToString();
                SqlCommand cmdSelect = new SqlCommand("select cardId,ID from [user]", conn);
                SqlDataAdapter adpt = new SqlDataAdapter(cmdSelect);
                DataTable cardIDTable = new DataTable();
                adpt.Fill(cardIDTable);//数据库中已有卡号
                string[] arrCardID = cardIDTable.AsEnumerable().Select(d => d.Field<string>("cardId")).ToArray();//将DataTable的一列转换为数组
                string[] arrID = cardIDTable.AsEnumerable().Select(d => d.Field<string>("ID")).ToArray();//将DataTable的一列转换为数组
                if (isInArray(arrID, ID))
                {
                    Response.Write("ID：" + ID + "已存在卡号，请重新输入ID号");
                    Response.End();
                }
                else
                {
                    //随机生成一个可供选择的卡号数组（六位数），没有包含数据库中已有的卡号
                    string[] ran = new string[8]; ;
                    for (int i = 0; i < 8; i++)
                    {
                        string cardString = GetRandomString(6);
                        bool b1 = isInArray(ran, cardString);
                        bool b2 = isInArray(arrCardID, cardString);
                        if (!(b1 || b2))     //生成的随机数既不能是已生成的，也不能是在数据库中有的
                        {
                            ran[i] = cardString;
                        }
                        else
                            i -= 1;//重复
                    }
                    string jsonString = GetJsonString(ran);
                    Response.Write(jsonString);
                    Response.End();
                }          
            }
            else            //提交办理开户
            {
                var name = Request["name"].ToString();
                var identityID = Request["identityID"].ToString();
                var Email = Request["Email"].ToString();
                var phone = Request["phone"].ToString();
                var selCardID = Request["selCardID"].ToString();
                var confirmPassword = Request["confirmPassword"].ToString();
                var money = Request["money"].ToString();
                var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "insert into [user](cardId,name,ID,Email,phone) values('" + selCardID + "','" + name + "','" + identityID + "','" + Email + "','" + phone + "')";
                string sql1 = "insert into [card](cardId,balance,password,state,openTime) values('" + selCardID + "','" + money + "','" + confirmPassword + "','" + "1" + "','" + time + "')";
                var sql2 = "INSERT INTO exchange(cardId,exchangeTime,exchangeMoney) VALUES ('" + selCardID + "','" + time + "','" + money + "')";

                SqlTransaction myTran;
                myTran = conn.BeginTransaction();   //创建事务
                SqlCommand myComm = new SqlCommand();
                myComm.Connection = conn;
                myComm.Transaction = myTran;
                try
                {
                    myComm.CommandText = sql1;
                    myComm.ExecuteNonQuery();
                    myComm.CommandText = sql;
                    myComm.ExecuteNonQuery();
                    myComm.CommandText = sql2;
                    myComm.ExecuteNonQuery();
                    myTran.Commit();              //事务提交
                    EMAIL.SendEmail(Email, "银行开户", "您刚刚已在本行办理了银行开户\r\n卡号为：" + selCardID + "\r\n余额为："+ money+"元"+"\r\n卡密码为："+confirmPassword+"\r\n开户时间为："+time);
                    Response.Write("办理开户成功，用户卡号为:  " + selCardID);
                }
                catch (Exception ex)
                {
                    myTran.Rollback();            //事务回滚
                    Response.Write("事务操作出错，已回滚\r\n" + ex.Message.ToString());
                }
                Response.End();
            }
        }

        //获得一个六位数的随机数
        public string GetRandomString(int iLength)
        {
            string buffer = "0123456789";// 随机字符中也可以为汉字（任何）
            StringBuilder sb = new StringBuilder();
            Random r = new Random();
            int range = buffer.Length;
            for (int i = 0; i < iLength; i++)
            {
                sb.Append(buffer.Substring(r.Next(range), 1));
            }
            return sb.ToString();
        }
        //将一个数组转换成JSON字符串
        public string GetJsonString(string[] arr)
        {
            string JsonString = "{";
            for (int i = 0; i < arr.Length; i++)
            {
                JsonString += "\"" + i.ToString() + "\"";
                JsonString += ":" + "\"" + arr[i] + "\"";
                if (i < arr.Length - 1)
                    JsonString += ",";
            }
            JsonString += "}";
            return JsonString;
        }
        //判断一个元素是否在数组中
        public bool isInArray(string[] arr, string str)
        {
            foreach (string value in arr)
            {
                if (value == str)
                    return true;
            }
            return false;
        }
    }

}