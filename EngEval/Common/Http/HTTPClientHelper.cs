using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace Http
{
    public class HTTPClientHelper
    {

        public static string FormatParams(Dictionary<string, string> parameters)
        {
            return JsonConvert.SerializeObject(parameters);
        }

        // Post请求

        public static string PostResponse(string url, Dictionary<string, string> parameters, out string statusCode)

        {
            string postData = FormatParams(parameters);
            string result = string.Empty;

            //设置Http的正文
            HttpContent httpContent = new StringContent(postData);
            //设置Http的内容标头
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //设置Http的内容标头的字符
            httpContent.Headers.ContentType.CharSet = "utf-8";
            using (HttpClient httpClient = new HttpClient())
            {

                //异步Post
                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

                //输出Http响应状态码
                statusCode = response.StatusCode.ToString();

                //确保Http响应成功
                if (response.IsSuccessStatusCode)
                {
                    //异步读取json
                    result = response.Content.ReadAsStringAsync().Result;
                }
            }
            return result;
        }

    }
}