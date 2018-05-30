using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FileZillaServerCommonHelper
{
    public class Mac : System.Web.UI.Page
    {
         [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        protected void Page_Load(object sender, EventArgs e)
        {
            GetMac();  
        }
        //获取客户端IP
        private string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }
 
        //获取MAC地址
        public void GetMac()
        {
            // 在此处放置用户代码以初始化页面
            try
            {
                string userip = Request.UserHostAddress;
                string strClientIP = Request.UserHostAddress.ToString().Trim();
                Int32 ldest = inet_addr(strClientIP); //目的地的ip 
                Int32 lhost = inet_addr("");   //本地服务器的ip 
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");
                //if (mac_src == "0")
                //{
                //    //if (userip == "127.0.0.1")
                //    //    Response.Write("正在访问Localhost!");
                //    //else
                //    //    Response.Write("欢迎来自IP为" + userip + "的朋友！" + "");
                //}
                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }
                string mac_dest = "";
                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }
                //Response.Write("欢迎来自IP为" + userip + "" + ",MAC地址为" + mac_dest + "的朋友！"
                // + "");
                //return mac_dest;
            }
            catch (Exception err)
            {
                Response.Write(err.Message);
            }

        }

        //获取MAC地址
        public void GetMac2(out string Mac0, out string userIP)
        {
            Mac0 = string.Empty;
            userIP = string.Empty;
            // 在此处放置用户代码以初始化页面
            try
            {
                string userip = Request.UserHostAddress;
                string strClientIP = Request.UserHostAddress.ToString().Trim();
                Int32 ldest = inet_addr(strClientIP); //目的地的ip 
                Int32 lhost = inet_addr("");   //本地服务器的ip 
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");
                if (mac_src == "0")
                {
                    if (userip == "127.0.0.1")
                    {
                    }
                    else
                    {
                    }
                    userIP = userip;
                }
                //while (mac_src.Length < 12)
                //{
                //    mac_src = mac_src.Insert(0, "0");
                //}
                //string mac_dest = "";
                //for (int i = 0; i < 11; i++)
                //{
                //    if (0 == (i % 2))
                //    {
                //        if (i == 10)
                //        {
                //            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                //        }
                //        else
                //        {
                //            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                //        }
                //    }
                //}
                //Mac0 = mac_dest;


                // StringBuilder strReturn = new StringBuilder();  

                // System.Net.IPHostEntry Tempaddr = (System.Net.IPHostEntry)Dns.GetHostByName(Dns.GetHostName());
                // System.Net.IPAddress[] TempAd = Tempaddr.AddressList;
                // Int32 remote = (int)TempAd[0].Address;
                // Int64 macinfo1 = new Int64();
                // Int32 length = 6;
                // SendARP(remote, 0, ref macinfo1, ref length);

                // string temp = System.Convert.ToString(macinfo1, 16).PadLeft(12, '0').ToUpper();

                // int x = 12;
                // for (int i = 0; i < 6; i++)
                // {
                //     if (i == 5) { strReturn.Append(temp.Substring(x - 2, 2)); }
                //     else { strReturn.Append(temp.Substring(x - 2, 2) + ":"); }
                //     x -= 2;
                // }

                //Mac0=  strReturn.ToString();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine("获取IP地址失败。\r\n" + ex.Message + ex.StackTrace);
            }
        }
    }
}
