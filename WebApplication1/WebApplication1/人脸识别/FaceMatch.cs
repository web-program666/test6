using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace WebApplication1.人脸识别
{
    public class FaceMatch
    {
        // 人脸对比
        public static string faceMatch()
        {
            string token =AccessToken.getAccessToken();
            string host = "https://aip.baidubce.com/rest/2.0/face/v3/match?access_token=" + token;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            
            string imageReg= Convert.ToBase64String(File.ReadAllBytes(@"D:\Saved Pictures\1.jpg")); ;
            string imageUse= Convert.ToBase64String(File.ReadAllBytes(@"D:\Saved Pictures\2.jpg")); ;
            request.Method = "post";
            request.KeepAlive = true;
            String str = "[{\"image\":\"" + imageReg + "\",\"image_type\":\"BASE64\",\"face_type\":\"IDCARD\",\"quality_control\":\"LOW\",\"liveness_control\":\"HIGH\"},{\"image\":\"" + imageUse + "\",\"image_type\":\"BASE64\",\"face_type\":\"LIVE\",\"quality_control\":\"LOW\",\"liveness_control\":\"HIGH\"}]";
            byte[] buffer = encoding.GetBytes(str);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                string result = reader.ReadToEnd();
                Console.WriteLine("人脸对比:");
                Console.WriteLine(result);
                return result;
            }
        }
}