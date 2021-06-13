using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BANK.setInformation.aspx
{
    public partial class setInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var requese = Request["msg"];
            string cardID = Request["cardId"];  //设置cookie！！！！
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
            if (requese == "0")         //如果是请求为“0”，那么从数据中获取用户信息并返回给前端
            {
                var sql = "select* from card C JOIN [user] U ON C.cardId=U.cardId where C.cardId=@cardId";
                SqlCommand cmdSelect = new SqlCommand(sql, conn);
                cmdSelect.Parameters.AddWithValue("@cardId", cardID);
                SqlDataAdapter adpt = new SqlDataAdapter(cmdSelect);
                DataTable infoTable = new DataTable();
                adpt.Fill(infoTable);//数据库中已有卡号
                string[] columnsNames = GetColumnsByDataTable(infoTable);  //得到表的列名
                string[] values = new string[columnsNames.Length];
                for (int i = 0; i < columnsNames.Length; i++)
                {
                    values[i] = infoTable.Rows[0][i].ToString();
                }
                string str = GetJsonString(columnsNames, values);
                Response.Write(str);
            }
            else   //如果是请求不为“0”，那么从前端获取数据并更新对应的数据库内容
            {
                var name1 = Request["name"].ToString();
                var id = Request["ID"].ToString();
                var cardId1 = Request["cardId"].ToString();
                var balance1 = Request["balance"].ToString();
                var password1 = Request["password"].ToString();
                var Email1 = Request["Email"].ToString();
                var phone1 = Request["phone"].ToString();
                var state = Request["state"].ToString();
                string sql1 = "update [card] set balance='" + balance1 + "', password='" + password1 + "',state='" + state + "' where cardId='" + cardID + "'";
                string sql2 = "update [user] set name='" + name1 + "',ID='" + id + "',Email='" + Email1 + "',phone='" + phone1 + "' where  cardId='" + cardID + "'";
                //注意参数的书写格式，易错
                SqlTransaction myTran;
                myTran = conn.BeginTransaction();   //创建事务
                SqlCommand myComm = new SqlCommand();
                myComm.Connection = conn;
                myComm.Transaction = myTran;

                try
                {
                    myComm.CommandText = sql1;
                    myComm.ExecuteNonQuery();
                    myComm.CommandText = sql2;
                    myComm.ExecuteNonQuery();
                    myTran.Commit();              //事务提交

                    EMAIL.SendEmail(Email1, "银行修改信息", "您刚刚已在本行柜台进行了信息修改 "); //注意文件引用路径是否正确
                    Response.Write("修改成功!");
                }
                catch (Exception ex)
                {
                    myTran.Rollback();            //事务回滚
                    Response.Write("事务操作出错，已回滚\r\n" + ex.Message.ToString());
                }
            }
            Response.End();

        }

        //DataTable 获取列名
        public static string[] GetColumnsByDataTable(DataTable dt)
        {
            string[] strColumns = null;


            if (dt.Columns.Count > 0)
            {
                int columnNum = 0;
                columnNum = dt.Columns.Count;
                strColumns = new string[columnNum];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strColumns[i] = dt.Columns[i].ColumnName;
                }
            }


            return strColumns;
        }

        //将两个数组转换成JSON字符串
        public string GetJsonString(string[] arrKey, string[] arrValue)
        {
            string JsonString = "{";
            for (int i = 0; i < arrValue.Length; i++)
            {
                JsonString += "\"" + arrKey[i] + "\"";
                JsonString += ":" + "\"" + arrValue[i] + "\"";
                if (i < arrValue.Length - 1)
                    JsonString += ",";
            }
            JsonString += "}";
            return JsonString;
        }
    }
}