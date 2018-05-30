using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using FileZillaServerCommonHelper;

namespace FileZillaServerCommonHelper
{
    public static class DingTalkHelper
    {
        //public static string GetAccessToken(HttpRequest.HttpHelper.HttpResult result)
        //{
           
        //    if (result!=null)
        //    {
        //        StreamReader myStreamReader = new StreamReader(result.Result, System.Text.Encoding.GetEncoding("utf-8"));
        //        string retString = myStreamReader.ReadToEnd();

        //        M_AccessToken oat = Newtonsoft.Json.JsonConvert.DeserializeObject< M_AccessToken>(retString);

        //        if (oat!=null)
        //        {
        //           if(oat.errcode==0)
        //            {
        //                return oat.access_token;
        //            }
        //        }
        //    }
        //    return "";
        //}
        //public static string GetAccessToken(string url)
        //{

        //    if (url != "")
        //    {
        //        try
        //        {
        //            HttpRequest.HttpHelper.HttpResult result = HttpRequest.HttpHelper.Get(url);
        //            M_AccessToken oat = Newtonsoft.Json.JsonConvert.DeserializeObject<M_AccessToken>(result.ToStringResult());

        //            if (oat != null)
        //            {
        //                if (oat.errcode == 0)
        //                {
        //                    return oat.access_token;
        //                }
        //            }
        //        }
        //        catch(Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //    return "";
        //}

        //public static string CreateDept(string url,string param)
        //{

        //    if (url != "")
        //    {
        //        try
        //        {
        //            HttpRequest.HttpHelper.HttpResult result = HttpRequest.HttpHelper.Post(url, param, "application/json");
        //            DDResult oat = Newtonsoft.Json.JsonConvert.DeserializeObject<DDResult>(result.ToStringResult());

        //            if (oat != null)
        //            {
        //                if (oat.errcode == 0)
        //                {
        //                    return "0";
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //    return "1";
        //}


        public static string GetAccessToken()
        {
            string apiUrl = "https://oapi.dingtalk.com/gettoken?corpid=ding08a708c5272bc85d35c2f4657eb6378f&corpsecret=o4Ivoh4T7MfhOGf2wlIZmzrUih03dDw2OcvekuZOGUohFj-CvlyOej2DZHRx_-By";
            //object result = WebServiceHelper.InvokeWebService(apiUrl, string.Empty, null);
            //return result.ToString();

            string result = WebServiceHelper.HttpGet(apiUrl, null);
            return result;
        }
    }
    //{"access_token":"","errcode":0,"errmsg":"ok"}
    public class M_AccessToken
    {
        public string access_token { get; set; }
        public int errcode { get; set; }

        public string errmsg { get; set; }


    }
    public class DDResult
    {
       
        public int errcode { get; set; }
        public string errmsg { get; set; }


    }
}
