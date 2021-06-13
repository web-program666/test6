using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WebApplication1.人脸识别
{
    public partial class savePic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)  
        {
            string state = Request["state"];
            string cardId = Request["cardId"];//用户的卡号
            string pic_base64 = Request["picture"];
            if (state == "0") {
                faceContrast(cardId, pic_base64, state);  //注册状态，仅需保存照片
                setCookie(cardId);
                Response.Write("1");   //人脸录入成功
                Response.End();
            }
            if (state == "1")    //人脸对比状态
            {
                faceContrast(cardId,pic_base64,state);
                //Response.Write("人脸上传成功！");
                try
                {
                    //用户卡号+状态，用作照片名
                    string result = demo(@"C:\Users\Lenovo\Desktop\WebPic\" + cardId + "0.png", @"C:\Users\Lenovo\Desktop\WebPic\" + cardId + "1.png");//返回json格式
                    JObject returnJson = (JObject)JsonConvert.DeserializeObject(result);
                    if ("0".Equals(returnJson["error_code"].ToString()) && "SUCCESS".Equals(returnJson["error_msg"].ToString()))
                    {
                        JObject resultJson = (JObject)JsonConvert.DeserializeObject(returnJson["result"].ToString());
                        if (Convert.ToDecimal(resultJson["score"].ToString()) > 90) //相似率超过90%即为同一人
                        {
                            result = "1";//同一人
                        }
                        else
                        {
                            result = "-1"; //不是同一人
                        }
                        //result = resultJson.ToString();   //返回原json
                    }
                    else
                    {
                        result = "识别失败："+returnJson["error_msg"].ToString();  //返回错误原因
                        //result = returnJson.ToString();   //返回原json
                    }
                    Response.Write(result);
                }
                catch
                {
                    string result = "人脸不存在！";
                    Response.Write(result);
                }

                Response.End();
            }
            
        }
        public void faceContrast(string cardId, string pic_base64,string state)
        {
            //pic_base64.Replace(" ", "+");//ajax在传输过程中将字符串+号全部转换为空格，需要转换回来
            //string[] str = pic_base64.Split(',');    //创建数组
            byte[] imageBytes = Convert.FromBase64String(pic_base64);
            MemoryStream memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
            memoryStream.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream);
            image.Save(@"C:\Users\Lenovo\Desktop\WebPic\" + cardId+state+ ".png");   //保存照片
        }
        public static string ReadImg(string img)
        {
            return Convert.ToBase64String(File.ReadAllBytes(img));
        }
        public static string demo(string Url1, string Url2)
        {
            var API_KEY = "XteMhlIhTDUXHYD4c70soa1q"; //百度注册账号的apikey
            var SECRET_KEY = "IEG7TSHDNxouaGrVbgupkbLz8o0zoGrn"; //百度注册账号的secretkey
            var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
            var faces = new JArray
    {
        new JObject
        {
            {"image", ReadImg(Url1)},
            {"image_type", "BASE64"},
            {"face_type", "LIVE"},
            {"quality_control", "LOW"},
            {"liveness_control", "NONE"},
        },
        new JObject
        {
            {"image", ReadImg(Url2)},
            {"image_type", "BASE64"},
            {"face_type", "LIVE"},
            {"quality_control", "LOW"},
            {"liveness_control", "NONE"},
        }
    };

            var result = client.Match(faces);
            if (File.Exists(Url2) == true)
            {
                File.Delete(Url2);   //删除用于验证的图片
            }
            return result.ToString();
        }
        public void setCookie(string name)
        {
            HttpCookie newCookie = new HttpCookie("cardId2");
            newCookie.Value = name;
            Response.AppendCookie(newCookie);
        }
    }
}