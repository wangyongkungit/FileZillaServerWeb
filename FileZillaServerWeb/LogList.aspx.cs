using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FileZillaServerBLL;
using System.Data;
using System.Configuration;

namespace FileZillaServerWeb
{
    public partial class LogList : WebPageHelper
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "日志查看";
            if (!IsPostBack)
            {
                ValidatePermission(Request.Url.LocalPath);
                RepeaterDataBind();
            }
        }

        private void RepeaterDataBind()
        {
            Dictionary<string, string> dicCondition = new Dictionary<string, string>();
            string employeeNO = txtEmployeeNo.Text.Trim();
            string operateDateStart = txtOperateDateStart.Text.Trim();
            string operateDateEnd = txtOperateDateEnd.Text.Trim();
            if (!string.IsNullOrEmpty(employeeNO))
            {
                dicCondition.Add("employeeNO", employeeNO);
            }
            if (!string.IsNullOrEmpty(operateDateStart))
            {
                dicCondition.Add("operateDateStart", operateDateStart);
            }
            if (!string.IsNullOrEmpty(operateDateEnd))
            {
                dicCondition.Add("operateDateEnd", operateDateEnd);
            }
            if (!string.IsNullOrEmpty(ddlOperateType.SelectedValue))
            {
                dicCondition.Add("operateType", ddlOperateType.SelectedValue);
            }
            SystemLogBLLex slBll = new SystemLogBLLex();
            AspNetPager.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            int totalAmount = 0;
            DataTable dt = slBll.GetSystemLog(dicCondition, AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out totalAmount);
            AspNetPager.RecordCount = totalAmount;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RepeaterDataBind();
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            RepeaterDataBind();
        }
    }
}