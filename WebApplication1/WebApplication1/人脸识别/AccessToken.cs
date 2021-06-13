using System;
using System.Collections.Generic;
using System.Net.Http;

namespace WebApplication1.人脸识别
{
    public class AccessToken
    {

        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static String clientId = "XteMhlIhTDUXHYD4c70soa1q";
        // 百度云中开通对应服务应用的 Secret Key
        private static String clientSecret = "IEG7TSHDNxouaGrVbgupkbLz8o0zoGrn";

        public static String getAccessToken()
        {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            return result;
        }
    }
}