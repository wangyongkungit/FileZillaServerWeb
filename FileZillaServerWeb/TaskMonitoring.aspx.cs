using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class TaskMonitoring : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridViewDataBind();
            }
        }

        TaskRemindingBLL trBll = new TaskRemindingBLL();

        protected void GridViewDataBind()
        {
            string employeeNo = txtEmployeeNo.Text.Trim();
            string taskType = ddlTaskType.SelectedValue;
            string taskName = txtTaskName.Text.Trim();
            string isFinished = ddlIsFinished.SelectedValue;
            AspNetPager.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            int totalAmount = 0;
            DataTable dt = trBll.GetTaskList(employeeNo, taskName, isFinished, taskType, AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out totalAmount);
            AspNetPager.RecordCount = totalAmount;
            gvData.DataSource = dt;
            gvData.DataBind();
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                if (drv != null)
                {
                    //任务类型为售后时，drv["EXPIREDATE"]是为null的，因此这里要判断
                    if (drv["EXPIREDATE"] != DBNull.Value)
                    {
                        DateTime dtExpire = Convert.ToDateTime(drv["EXPIREDATE"]);
                        string[] strArr = Convert.ToString(ConfigurationManager.AppSettings["countDownInterval"]).Split(',').ToArray();
                        int[] intArr = strArr.Select(i => Convert.ToInt32(i)).ToArray();

                        string[] colorArr = Convert.ToString(ConfigurationManager.AppSettings["countDownColor"]).Split(',');

                        for (int i = 0; i < intArr.Length; i++)
                        {
                            if (DateTime.Now.AddHours(intArr[i]) > dtExpire && e.Row.Cells[3].Text == "未完成")
                            {
                                e.Row.Cells[2].Style.Add("color", colorArr[i]);
                                e.Row.Cells[2].Style.Add("font-weight", "bold");
                                StringBuilder sbTip = new StringBuilder();
                                if (DateTime.Now < dtExpire)
                                {
                                    TimeSpan ts = dtExpire - DateTime.Now;
                                    sbTip.AppendFormat("（{0}小时后到期）", Math.Floor(ts.TotalHours));
                                }
                                else if (dtExpire < DateTime.Now)
                                {
                                    TimeSpan ts = DateTime.Now - dtExpire;
                                    //e.Row.Cells[2].Text += "（已逾期" + Math.Floor( ts.TotalHours) + "小时）";
                                    e.Row.Cells[2].Text += string.Format("（已逾期{0}）", Common.TransformTimeSpan(ts));
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            GridViewDataBind();
        }

        /// <summary>
        /// 查询按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridViewDataBind();
        }
    }
}