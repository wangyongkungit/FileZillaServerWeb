using FileZillaServerBLL;
using FileZillaServerModel;
using FileZillaServerModel.Interface;
using FileZillaServerProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yiliangyijia.Comm;

namespace FileZillaServerWeb
{
    public partial class CustomerServiceTrends : WebPageHelper
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidatePermission(Request.Url.LocalPath);
                LoadData();
            }
        }

        private void LoadData()
        {
            int totalRowsCount = 0;
            UserProfile user = UserProfile.GetInstance();
            string employeeID = user.ID;
            StringBuilder sbWhere = new StringBuilder();
            if (!user.Role.Any(item => item.RoleName.Contains("管理员")))
            {
                sbWhere.AppendFormat(" employeeID = '{0}' AND type = 2 ", employeeID);
            }
            else
            {
                sbWhere.Append(" type = 2 ");
            }
            AspNetPager1.PageSize = 20;
            List<TaskTrend> lstTrend = new TaskTrendBLL().GetModelListPage(sbWhere.ToString(), AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out totalRowsCount);
            AspNetPager1.RecordCount = totalRowsCount;
            lblRecordCount.Text = totalRowsCount.ToString();
            List<TaskTrendInterface> lstResult = new List<TaskTrendInterface>();
            foreach (var item in lstTrend)
            {
                TaskTrendInterface taskTrendInterface = new TaskTrendInterface();
                taskTrendInterface.ID = item.ID;
                taskTrendInterface.ProjectId = item.PROJECTID;
                taskTrendInterface.CreateDate = item.CREATEDATE ?? DateTime.Now;
                taskTrendInterface.FriendlyDate = DateTimeHelper.ChangeTime(item.CREATEDATE ?? DateTime.MinValue);
                if (item.DESCRIPTION.IndexOf("客户ID：") > -1)
                {
                    string wangwangId = this.MidStrEx(item.DESCRIPTION, "客户ID：", "Store ID：").Trim();
                    if (!string.IsNullOrEmpty(item.DESCRIPTION))
                    {
                        taskTrendInterface.TrendContent = item.DESCRIPTION.Replace(wangwangId, "<a href=\"http://www.taobao.com/webww/ww.php?ver=3&amp;touid=" + wangwangId
                            + "&amp;siteid=cntaobao&amp;status=2&amp;charset=utf-8\" target=\"_blank\" class=\"awwm\">" +
                                            "<img border=\"0\" src = \"http://amos.alicdn.com/online.aw?v=2&amp;uid=" + wangwangId + "&amp;site=cntaobao&amp;s=2&amp;charset=utf-8\">" +
                                            wangwangId + "</a> ").Replace("客户ID：", string.Empty);
                    }
                    else
                    {
                        taskTrendInterface.TrendContent = string.Empty;
                    }
                }
                else
                {
                    taskTrendInterface.TrendContent = item.DESCRIPTION;
                }
                taskTrendInterface.ReadStatus = item.READSTATUS;
                lstResult.Add(taskTrendInterface);
            }
            gvTrend.DataSource = lstResult;
            gvTrend.DataBind();
        }

        protected void gvTrend_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "markRead")
            {
                try
                {
                    string ID = e.CommandArgument.ToString();
                    TaskTrend trend = new TaskTrendBLL().GetModel(ID);
                    trend.READSTATUS = true;
                    new TaskTrendBLL().Update(trend);
                    LoadData();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('成功标记为已读！');", true);
                }
                catch
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('标记已读失败！');", true);
                }
            }
        }

        #region 页码事件
        /// <summary>
        /// 页索引发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 页码跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGoPage_Click(object sender, EventArgs e)
        {
            try
            {
                int pageindex = int.Parse(tb_pageindex.Text);
                AspNetPager1.CurrentPageIndex = pageindex;
            }
            catch (FormatException)
            {
                lbl_error.Text = "输入的页索引格式不正确";
            }
        }
        #endregion

        public string MidStrEx(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            if (string.IsNullOrEmpty(sourse))
            {
                return string.Empty;
            }
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}