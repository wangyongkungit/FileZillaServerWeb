using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using FileZillaServerDAL;
using FileZillaServerModel;
using FileZillaServerProfile;
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
    public partial class EmployeeHome : System.Web.UI.Page
    {
        FileZillaServerBLL.EmployeeDominationBLL eDal = new FileZillaServerBLL.EmployeeDominationBLL();
        TaskAssignConfigDetailsBLL tacdBll = new TaskAssignConfigDetailsBLL();
        EmployeeAccountBLL empAcctBll = new EmployeeAccountBLL();
        CerficateBLL cBll = new CerficateBLL();
        ProjectDAL prjDal = new ProjectDAL();
        TaskTrendBLL ttBll = new TaskTrendBLL();
        WithdrawDetailsBLL wdBll = new WithdrawDetailsBLL();
        protected static string EmployeeID { get; set; }
        protected static string UserName { get; set; }
        protected static string EmployeeNo { get; set; }
        protected static List<Cerficate> lstCerficate { get; set; }
        protected static List<string> xData = new List<string>();
        protected static List<int> yData = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "我的主页";
            if (!IsPostBack)
            {
                string employeeId = Request.QueryString["employeeID"];
                LoadTvEmployee();
                LoadSpecialties();
                LoadCerficate();
                LoadBalance();
                LoadTaskTrend();
                LoadDataNeedReload();
            }
        }

        protected void LoadDataNeedReload()
        {
            LoadPieChart();
            LoadProject();
        }

        private void LoadTvEmployee()
        {
            string employeeID = Request.QueryString["employeeID"];
            string employeeWhere = string.Empty;
            UserProfile user = UserProfile.GetInstance();
            EmployeeID = user.ID;
            UserName = user.Name;
            EmployeeNo = user.EmployeeNO;
            //if (string.IsNullOrEmpty(employeeID))
            //{
            //    bool isAdmin = user.Role.Any(item => item.RoleName == "超级管理员");
            //    employeeID = user.ID;
            //}
            employeeID = employeeID ?? user.ID;
            employeeWhere = " e.ID = '" + employeeID + "'";
            DataTable dtEmployee = eDal.GetListDistinctParentEmpID(employeeWhere).Tables[0];
            DataTable dtEmployeeChild = eDal.GetListChildEmployee(string.Empty).Tables[0];
            if (dtEmployee != null && dtEmployee.Rows.Count > 0)
            {
                UserName = dtEmployee.AsEnumerable().Where(item => Convert.ToString(item["parentEmployeeID"]) == employeeID).Select(item => Convert.ToString(item["name"])).FirstOrDefault() ?? UserName;
                EmployeeID = employeeID;
                EmployeeNo = dtEmployee.AsEnumerable().Where(item => Convert.ToString(item["parentEmployeeID"]) == employeeID).Select(item => Convert.ToString(item["EMPLOYEENO"])).FirstOrDefault() ?? EmployeeNo;
            }
            else if (dtEmployeeChild != null && dtEmployeeChild.Rows.Count > 0)
            {
                UserName = dtEmployeeChild.AsEnumerable().Where(item => Convert.ToString(item["childEmployeeId"]) == employeeID).Select(item => Convert.ToString(item["name"])).FirstOrDefault() ?? UserName;
                EmployeeID = employeeID;
                EmployeeNo = dtEmployeeChild.AsEnumerable().Where(item => Convert.ToString(item["childEmployeeId"]) == employeeID).Select(item => Convert.ToString(item["EMPLOYEENO"])).FirstOrDefault() ?? EmployeeNo;
            }
            var drEmployeeChilds = dtEmployeeChild.AsEnumerable().Where(item => 1 == 1).ToList();
            TreeNode tn = new TreeNode();
            tn.Text = "返回";
            tn.Value = user.ID;
            foreach (DataRow dr in dtEmployee.Rows)
            {
                TreeNode tr = new TreeNode();
                tr.Text = dr["EmployeeNO"].ToString();
                tr.Value = dr["parentEmployeeID"].ToString();
                LoadChildNode(dr["parentEmployeeID"].ToString(), tr, drEmployeeChilds);
                tn.ChildNodes.Add(tr);
            }
            tn.Expanded = true;
            tvEmployees.Nodes.Add(tn);
        }

        public void LoadChildNode(string pid, TreeNode tr, List<DataRow> drEmployeeChild)
        {
            List<DataRow> lstDatarow = drEmployeeChild.AsEnumerable().Where(item => Convert.ToString(item["parentEmployeeID"]) == pid).ToList();     //查询子节点数据
            foreach (var dr in lstDatarow)   //循环绑定节点并查询子节点
            {
                TreeNode td = new TreeNode();
                td.Value = dr["childEmployeeId"].ToString();
                td.Text = dr["EMPLOYEENO"].ToString();

                LoadChildNode(dr["childEmployeeId"].ToString(), td, drEmployeeChild);   //递归绑定子节点
                tr.ChildNodes.Add(td);
            }
        }

        protected void tvEmployees_SelectedNodeChanged(object sender, EventArgs e)
        {
            string employeeID = tvEmployees.SelectedValue;
            Response.Redirect("EmployeeHome.aspx?employeeID=" + employeeID);
        }

        private void LoadSpecialties()
        {
            string employeeID = EmployeeID;
            DataTable dt = tacdBll.GetTaskAssignDetails(employeeID, "0").Tables[0];
            StringBuilder sbSkill = new StringBuilder();

            if (dt != null && dt.Rows.Count > 0)
            {
                dt.AsEnumerable().Where(item => item.Field<Int64>("available") == 1).ToList().ForEach(item => sbSkill.Append("<span class=\"skillSpan\">" + item.Field<string>("specialtyName") + "</span>"));
            }
            //lblMySkills.Text = sbSkill.ToString().TrimEnd('，');
            //lblMySkills.ToolTip = sbSkill.ToString().TrimEnd('，');
            divMySkills.InnerHtml = sbSkill.ToString();
        }

        protected void LoadCerficate()
        {
            Cerficate cerficate = cBll.GetModelList(" employeeID = '" + EmployeeID + "' AND isMain = 1 ").FirstOrDefault();
            if (cerficate != null)
            {
                imgCerficate.Src = Convert.ToString(ConfigurationManager.AppSettings["fileSavePath"]) + "/" + cerficate.FILEPATH;
            }
        }

        protected void LoadPieChart()
        {
            //EmployeeAccount empAcct = empAcctBll.GetModelList(" employeeID = '" + EmployeeID + "'").FirstOrDefault();

            //xData = new List<string>() { "剩余", "已发", "奖罚", "其他" };
            //yData = new List<int>() { 10, 20, 30, 40 };
            //ChartMoney.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
            //ChartMoney.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            //ChartMoney.Series[0].Points.DataBindXY(xData, yData);
        }

        public static void LoadEmployeeAccount()
        {

        }

        protected void LoadTaskTrend()
        {
            List<TaskTrend> lstTrend = ttBll.GetTop10ModelList(" employeeID = '" + EmployeeID + "'");
            gvTaskTrend.DataSource = lstTrend;
            gvTaskTrend.DataBind();
        }

        protected void LoadProject()
        {
            int totalRowsCount = 0;
            AspNetPager1.PageSize = 10;// Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            StringBuilder sbWhere = new StringBuilder();
            string anyCondition = txtAnyCondition.Text.Trim();
            if(!string.IsNullOrEmpty(anyCondition))
            {
                sbWhere.Append(anyCondition);
            }
            DataTable dtProject = prjDal.GetProjectForEmployeeHome(EmployeeID, sbWhere.ToString(), AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out totalRowsCount);

            if (string.IsNullOrEmpty(sbWhere.ToString()))
            {
                lblFinishedTaskCount.Text = string.Format("{0}", totalRowsCount);
            }
            AspNetPager1.RecordCount = totalRowsCount;
            gvProject.DataSource = dtProject;
            gvProject.DataBind();
        }

        private void LoadBalance()
        {
            EmployeeAccount empAcct = empAcctBll.GetModelList(" employeeID = '" + EmployeeID + "'").FirstOrDefault();
            decimal withdraw = wdBll.GetModelList(" employeeID = '" + EmployeeID + "' and isconfirmed = 0").Sum(item => item.WITHDRAWAMOUNT) ?? 0m;
            lblCanWithdrawAmount.Text = (empAcct.AMOUNT - withdraw).ToString();
        }

        #region 页码事件
        /// <summary>
        /// 页索引发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            LoadDataNeedReload();
        }

        ///// <summary>
        ///// 页码跳转
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnGoPage_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int pageindex = int.Parse(tb_pageindex.Text);
        //        AspNetPager1.CurrentPageIndex = pageindex;
        //    }
        //    catch (FormatException)
        //    {
        //        lbl_error.Text = "输入的页索引格式不正确";
        //    }
        //}
        #endregion

        #region Repeat Databound
        protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region 计算剩余时间
                bool isFinished = Convert.ToInt32(gvProject.DataKeys[e.Row.RowIndex].Values[1]) == 1;
                Label lblTimeRemain = e.Row.FindControl("lblTimeRemain") as Label;
                //未完成的，才显示剩余时间
                if (lblTimeRemain != null && !isFinished)
                {
                    HiddenField hidExpireDate = e.Row.FindControl("hidExpireDate") as HiddenField;
                    DateTime dtExpire = Convert.ToDateTime(hidExpireDate.Value);
                    if (DateTime.Now < dtExpire)
                    {
                        TimeSpan ts = dtExpire - DateTime.Now;

                        //设置提醒label文本
                        lblTimeRemain.Text = string.Format("{0}小时", Math.Floor(ts.TotalHours));
                        if (ts.TotalHours <= 3)
                        {
                            lblTimeRemain.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");//不足3小时，红色
                            lblTimeRemain.Font.Bold = true;
                        }
                        else if (ts.TotalHours <= 6)
                        {
                            lblTimeRemain.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF8800");//不足6小时，橙色
                            lblTimeRemain.Font.Bold = true;
                        }
                        else if (ts.TotalHours <= 12)
                        {
                            lblTimeRemain.ForeColor = System.Drawing.ColorTranslator.FromHtml("#EEEE00");//不足12小时，黄色
                            lblTimeRemain.Font.Bold = true;
                        }
                        else if (ts.TotalHours <= 24)
                        {
                            lblTimeRemain.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF77FF");//不足24小时，洋红色
                        }
                        else if (ts.TotalHours <= 48)
                        {
                            lblTimeRemain.ForeColor = System.Drawing.ColorTranslator.FromHtml("#57C600");//48小时以上，酸橙色(浅绿)
                        }
                        else
                        {
                            lblTimeRemain.Text = "&gt;2天";
                        }
                    }
                    else if (dtExpire < DateTime.Now)
                    {
                        TimeSpan ts = DateTime.Now - dtExpire;
                        //e.Row.Cells[2].Text += "（已逾期" + Math.Floor( ts.TotalHours) + "小时）";
                        lblTimeRemain.Text += string.Format("逾期{0}", Common.TransformTimeSpan(ts));
                        lblTimeRemain.ForeColor = System.Drawing.Color.Red;
                    }
                }
                #endregion
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProject();
        }
    }
}