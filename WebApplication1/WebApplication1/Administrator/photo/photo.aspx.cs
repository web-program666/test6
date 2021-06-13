using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace WebApplication1.Administrator.photo
{
    public partial class photo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var cardId = Request["cardId"];
            try
            {
                Response.Write(get_base64(cardId));
            }
            catch
            {
                Response.Write("-1");//未找到图片
            }
            Response.End();
        }
        public string get_base64(string cardId)   //将图片转换为base64
        {
            //根据图片文件的路径使用文件流打开，并保存为byte[] 
            FileStream photo = new FileStream(@"C:\Users\Lenovo\Desktop\WebPic\" + cardId + "0.png", FileMode.Open);
            byte[] byData = new byte[photo.Length];
            photo.Read(byData, 0, byData.Length);
            photo.Close();
            var phoBase64 = Convert.ToBase64String(byData);//括号里类型必须为byte[]，才能转换
            return phoBase64;
        }
    }
   
}