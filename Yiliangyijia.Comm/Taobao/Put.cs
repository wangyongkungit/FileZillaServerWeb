using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Yiliangyijia.Comm.Taobao
{
    public class Get
    {
        public string GetResponseData(string requesturl,string timestr,string requestId,string Authorization)
        {
            string resp = string.Empty;
            HttpWebRequest request;
            try
            {
                string lowerurl = System.Web.HttpUtility.UrlEncode(requesturl).ToLower();
                request = (HttpWebRequest)WebRequest.Create(requesturl);
                request.Method = "GET";
                request.Headers.Add("x-jsb-sdk-req-timestamp", timestr);
                request.Headers.Add("x-jsb-sdk-req-uuid", requestId);
                request.Headers.Add(HttpRequestHeader.Authorization, Authorization); // 设置鉴权信息头部
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(response.ToString());
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8")))
                    {
                        resp = reader.ReadToEnd().ToString();
                    }
                }
                // Console.WriteLine("调用地址{0},结果{1}", requesturl, resp);
            }
            catch (Exception ex)
            {
                resp = ex.ToString();
            }
            return resp;
        }
    }
}
