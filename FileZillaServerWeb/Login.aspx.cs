using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using FileZillaServerProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FileZillaServerModel;
using FileZillaServerDAL;
using System.Runtime.InteropServices;
using System.Text;
using System.Net;

namespace FileZillaServerWeb
{
    /// <summary>
    /// 描    述：登录
    /// 作    者：Yongkun Wang
    /// 创建时间：2016-03-02
    /// 修改历史：2017-03-02 Yongkun Wang 创建
    /// </summary>
    public partial class Login : Page
    {
        EmployeeBLL empBll = new EmployeeBLL();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region 登录
        /// <summary>
        /// 登录按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LogIn();
        }

        /// <summary>
        /// 登录
        /// </summary>
        protected void LogIn()
        {
            try
            {
                //new Mac().GetMac();
                string userName = txtUserName.Text.Trim();//用户名
                string passWord = txtPassword.Text.Trim();//密码
                //用户名和密码均不为空
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(passWord))
                {
                    //加密密码
                    passWord = AES.Encrypt(passWord, "0512000000000512");
                }
                UserProfile user0 = UserProfile.GetUserProfileByUserIDNew(userName, passWord);
                if (user0 != null)
                {
                    //user0 = UserProfile.GetUserProfileByUserID(userName);
                    UserProfile.instance = user0;

                    GetClientIP();
                    string Mac = string.Empty;
                    string userIP = string.Empty;
                    GetMac(out Mac, out userIP);

                    string physicalAddress = Common.GetMacByIPConfig().Count > 0 ? Common.GetMacByIPConfig()[0] : null;
                    SystemLog log = new SystemLog();
                    log.ID = Guid.NewGuid().ToString();
                    log.EMPLOYEEID = user0.ID;
                    log.OPERATETYPE = null;
                    log.OPERATECONTENT = "登录";
                    log.CREATEDATE = DateTime.Now;
                    log.IPADDRESS = userIP;
                    log.PHYSICALADDRESS = Mac;
                    SystemLogDAL slogDal = new SystemLogDAL();
                    slogDal.Add(log);
                    Response.Redirect("~/WebPageRedirect.aspx", true);       //原来用重定向非常耗时 注意哦
                    Response.End();
                    //ExecuteScript("window.location.href='WebPageRedirect.aspx';");
                }
                else
                {
                    Alert("用户名或密码不正确，或者您不具备访问任何模块的权限！");
                    return;
                }
                ////获得用户dt
                //System.Data.DataTable dt = UserProfile.GetUserProfileByUserIDandPwd(userName, passWord);
                //UserProfile user = null;
                //if (dt != null)
                //{
                //    user = UserProfile.GetUserProfileByUserID(userName);
                //    UserProfile.instance = user;

                //    GetClientIP();
                //    string Mac = string.Empty;
                //    string userIP = string.Empty;
                //    GetMac(out Mac, out userIP);

                //    string physicalAddress = Common.GetMacByIPConfig().Count > 0 ? Common.GetMacByIPConfig()[0] : null;
                //    SystemLog log = new SystemLog();
                //    log.ID = Guid.NewGuid().ToString();
                //    log.EMPLOYEEID = user.ID;
                //    log.OPERATETYPE = null;
                //    log.OPERATECONTENT = "登录";
                //    log.CREATEDATE = DateTime.Now;
                //    log.IPADDRESS = userIP;
                //    log.PHYSICALADDRESS = Mac;
                //    SystemLogDAL slogDal = new SystemLogDAL();
                //    slogDal.Add(log);
                //    Response.Redirect("/WebPageRedirect.aspx");
                //}
                //else
                //{
                //    Alert("用户名或密码不正确，或者您不具备访问任何模块的权限！");
                //    return;
                //}
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine("登录异常。" + ex.Message + "\r\n" + ex.StackTrace);
                Alert("登录出现异常！");
                return;
            }
        }
        #endregion

        #region 辅助方法
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

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
            if (result == "::1")
            {
                result = "127.0.0.1";
            }
            return result;
        }

        private string GetBrowserId()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.Browser.Id;
            }
            return result;
        }
 
        //获取MAC地址
        private void GetMac(out string Mac0, out string userIP)
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
                //因为也获取不到客户端的MAC，所以就把这段注释了吧，不然还占用资源
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
        #endregion

        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="message"></param>
        public void Alert(string message)
        {
            message = message.Replace("'", "''");
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "')", true);
        }

        /// <summary>
        /// 执行JavaScript脚本
        /// </summary>
        /// <param name="script"></param>
        public void ExecuteScript(string script)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}