using FileZillaServerProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace FileZillaServerWeb
{
    public class WebPageHelper : Page
    {
        public WebPageHelper() { }

        public void ValidatePermission(string requestUrl)
        {
            string rawUrl = requestUrl.TrimStart('/');
            UserProfile user = UserProfile.GetInstance();
            bool flag = false;
            for (int i = 0; i < user.Menu.Count; i++)
            {
                if (user.Menu[i].Path.Contains(rawUrl))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                Response.Redirect("Tip.html", true);
                Response.End();
            }
        }

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