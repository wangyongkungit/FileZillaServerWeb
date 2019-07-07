using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using FileZillaServerDAL;
using FileZillaServerModel;
using FileZillaServerProfile;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class employeeHome : WebPageHelper
    {
        #region 公共变量声明
        FileZillaServerBLL.EmployeeDominationBLL eDal = new FileZillaServerBLL.EmployeeDominationBLL();
        TaskAssignConfigDetailsBLL tacdBll = new TaskAssignConfigDetailsBLL();
        EmployeeAccountBLL empAcctBll = new EmployeeAccountBLL();
        CerficateBLL cBll = new CerficateBLL();
        ProjectDAL prjDal = new ProjectDAL();
        TaskTrendBLL ttBll = new TaskTrendBLL();
        TaskRemindingBLL trBll = new TaskRemindingBLL();
        WithdrawDetailsBLL wdBll = new WithdrawDetailsBLL();
        EmployeeProportionBLL epBll = new EmployeeProportionBLL();
        protected string EmployeeID
        {
            get
            {
                return (string)ViewState["EmployeeID"];
            }
            set
            {
                ViewState["EmployeeID"] = value;
            }
        }
        protected string UserName
        {
            get
            {
                return (string)ViewState["UserName"];
            }
            set
            {
                ViewState["UserName"] = value;
            }
        }
        protected string EmployeeNo
        {
            get
            {
                return (string)ViewState["EmployeeNo"];
            }
            set
            {
                ViewState["EmployeeNo"] = value;
            }
        }
        protected bool IsBranchLeader
        {
            get
            {
                return Convert.ToBoolean(ViewState["IsBranchLeader"]);
            }
            set
            {
                ViewState["IsBranchLeader"] = value;
            }
        }
        protected bool IsExternal
        {
            get
            {
                return Convert.ToBoolean(ViewState["IsExternal"]);
            }
            set
            {
                ViewState["IsExternal"] = value;
            }
        }
        protected List<Cerficate> lstCerficate
        {
            get
            {
                return (List<Cerficate>)ViewState["lstCerficate"];
            }
            set
            {
                ViewState["lstCerficate"] = value;
            }
        }
        protected List<string> lstProjectID
        {
            get
            {
                return (List<string>)ViewState["lstProjectID"];
            }
            set
            {
                ViewState["lstProjectID"] = value;
            }
        }
        protected string projectIdNeed
        {
            get
            {
                return (string)ViewState["projectIdNeed"];
            }
            set
            {
                ViewState["projectIdNeed"] = value;
            }
        }
        protected List<TransactionDetails> lstTcje
        {
            get
            {
                return (List<TransactionDetails>)ViewState["tcje"];
            }
            set
            {
                ViewState["tcje"] = value;
            }
        }
        protected List<ProjectProportion> lstPrjProportion
        {
            get
            {
                return (List<ProjectProportion>)ViewState["lstPrjProportion"];
            }
            set
            {
                ViewState["lstPrjProportion"] = value;
            }
        }
        protected EmployeeProportion empProportion
        {
            get
            {
                return (EmployeeProportion)ViewState["empProportion"];
            }
            set
            {
                ViewState["empProportion"] = value;
            }
        }
        protected DateTime? toRegularDate
        {
            get
            {
                return (DateTime)ViewState["toRegularDate"];
            }
            set
            {
                ViewState["toRegularDate"] = value;
            }
        }
        //protected List<string> xData = new List<string>();
        //protected List<int> yData = new List<int>();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "我的主页";
            if (!IsPostBack)
            {
                string employeeId = GetQueryString("employeeID"); // Request.QueryString["employeeID"];
                LoadTvEmployee();
                LoadSpecialties();
                LoadCerficate();
                LoadDataNeedReload();
            }
        }

        protected void LoadDataNeedReload()
        {
            //LoadPieChart();
            LoadProject();
        }

        private void LoadTvEmployee()
        {
            string employeeID = GetQueryString("employeeID"); // Request.QueryString["employeeID"];
            string employeeWhere = string.Empty;
            UserProfile user = UserProfile.GetInstance();
            EmployeeID = user.ID;
            UserName = user.Name;
            EmployeeNo = user.EmployeeNO;
            IsBranchLeader = user.isBranchLeader;
            IsExternal = user.isExternal;
            toRegularDate = user.toRegularDate;
            //if (string.IsNullOrEmpty(employeeID))
            //{
            //    bool isAdmin = user.Role.Any(item => item.RoleName == "超级管理员");
            //    employeeID = user.ID;
            //}
            if (string.IsNullOrEmpty(employeeID))
            {
                employeeID = user.ID;
            }
            employeeWhere = " e.ID = '" + employeeID + "'";
            DataTable dtEmployee = eDal.GetListDistinctParentEmpID(employeeWhere).Tables[0];
            DataTable dtEmployeeChild = eDal.GetListChildEmployee(string.Empty).Tables[0];
            DataTable dtExtenInfo = tacdBll.GetEmpNoSpecicaltyNameAndTaskAcountByEmpID().Tables[0];
            if (dtEmployee != null && dtEmployee.Rows.Count > 0)
            {
                UserName = dtEmployee.AsEnumerable().Where(item => Convert.ToString(item["parentEmployeeID"]) == employeeID).Select(item => Convert.ToString(item["name"])).FirstOrDefault() ?? UserName;
                EmployeeID = employeeID;
                EmployeeNo = dtEmployee.AsEnumerable().Where(item => Convert.ToString(item["parentEmployeeID"]) == employeeID).Select(item => Convert.ToString(item["EMPLOYEENO"])).FirstOrDefault() ?? EmployeeNo;
                toRegularDate = dtEmployee.AsEnumerable().Where(item => Convert.ToString(item["parentEmployeeID"]) == employeeID).Select(item => Convert.ToDateTime(item["toRegularDate"]))?.FirstOrDefault() ?? toRegularDate;
            }
            else if (dtEmployeeChild != null && dtEmployeeChild.Rows.Count > 0)
            {
                UserName = dtEmployeeChild.AsEnumerable().Where(item => Convert.ToString(item["childEmployeeId"]) == employeeID).Select(item => Convert.ToString(item["name"])).FirstOrDefault() ?? UserName;
                EmployeeID = employeeID;
                EmployeeNo = dtEmployeeChild.AsEnumerable().Where(item => Convert.ToString(item["childEmployeeId"]) == employeeID).Select(item => Convert.ToString(item["EMPLOYEENO"])).FirstOrDefault() ?? EmployeeNo;
                toRegularDate = dtEmployeeChild.AsEnumerable().Where(item => Convert.ToString(item["childEmployeeId"]) == employeeID).Select(item => Convert.ToDateTime(item["toRegularDate"]))?.FirstOrDefault() ?? toRegularDate;
            }
            var drEmployeeChilds = dtEmployeeChild.AsEnumerable().Where(item => 1 == 1).ToList();
            TreeNode tn = new TreeNode();
            tn.Text = "返回";
            tn.Value = user.ID;
            foreach (DataRow dr in dtEmployee.Rows)
            {
                TreeNode tr = new TreeNode();
                tr.Text = dr["EmployeeNO"].ToString();
                string empID = dr["parentEmployeeID"].ToString();
                tr.Value = empID;
                StringBuilder sbExtenInfo = new StringBuilder();
                sbExtenInfo.AppendFormat("{0}", dtExtenInfo.AsEnumerable().Where(item => (string)item["EmployeeID"] == empID)?.Select(result => result["employeeName"]?.ToString())?.FirstOrDefault());
                sbExtenInfo.AppendFormat("{0}", dtExtenInfo.AsEnumerable().Where(item => (string)item["EmployeeID"] == empID)?.Select(result => result["specialtyName"]?.ToString())?.FirstOrDefault());
                sbExtenInfo.AppendFormat("({0},", dtExtenInfo.AsEnumerable().Where(item => (string)item["EmployeeID"] == empID)?.Select(result => result["taskInProgress"]?.ToString())?.FirstOrDefault());
                sbExtenInfo.AppendFormat("{0})", dtExtenInfo.AsEnumerable().Where(item => (string)item["EmployeeID"] == empID)?.Select(result => result["modifyTaskInProgress"]?.ToString())?.FirstOrDefault());
                tr.Text += sbExtenInfo.ToString();
                LoadChildNode(dr["parentEmployeeID"].ToString(), tr, drEmployeeChilds);
                tn.ChildNodes.Add(tr);
            }
            tn.Expanded = true;
            tvEmployees.Nodes.Add(tn);
        }

        public void LoadChildNode(string pid, TreeNode tr, List<DataRow> drEmployeeChild)
        {
            List<DataRow> lstDatarow = drEmployeeChild.AsEnumerable().Where(item => Convert.ToString(item["parentEmployeeID"]) == pid).ToList();     //查询子节点数据
            DataTable dtExtenInfo = tacdBll.GetEmpNoSpecicaltyNameAndTaskAcountByEmpID().Tables[0];
            foreach (var dr in lstDatarow)   //循环绑定节点并查询子节点
            {
                TreeNode td = new TreeNode();
                string empId = dr["childEmployeeId"].ToString();
                td.Value = empId;
                td.Text = dr["EMPLOYEENO"].ToString();

                StringBuilder sbExtenInfo = new StringBuilder();
                sbExtenInfo.AppendFormat("{0}", dtExtenInfo.AsEnumerable().Where(item => (string)item["EmployeeID"] == empId)?.Select(result => result["employeeName"]?.ToString())?.FirstOrDefault());
                sbExtenInfo.AppendFormat("{0}", dtExtenInfo.AsEnumerable().Where(item => (string)item["EmployeeID"] == empId)?.Select(result => result["specialtyName"]?.ToString())?.FirstOrDefault());
                sbExtenInfo.AppendFormat("({0},", dtExtenInfo.AsEnumerable().Where(item => (string)item["EmployeeID"] == empId)?.Select(result => result["taskInProgress"]?.ToString())?.FirstOrDefault());
                sbExtenInfo.AppendFormat("{0})", dtExtenInfo.AsEnumerable().Where(item => (string)item["EmployeeID"] == empId)?.Select(result => result["modifyTaskInProgress"]?.ToString())?.FirstOrDefault());
                td.Text += sbExtenInfo.ToString();
                LoadChildNode(dr["childEmployeeId"].ToString(), td, drEmployeeChild);   //递归绑定子节点
                tr.ChildNodes.Add(td);
            }
        }

        protected void tvEmployees_SelectedNodeChanged(object sender, EventArgs e)
        {
            string employeeID = tvEmployees.SelectedValue;
            //Response.Redirect("EmployeeHome.aspx?employeeID=" + employeeID);
            string userId = UserProfile.GetInstance().ID;
            Response.Redirect("~/h/" + (employeeID == userId ? string.Empty : employeeID));
            //ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location.href='/h/"+ (employeeID == userId ? string.Empty : employeeID) + "';", true);
        }

        private void LoadSpecialties()
        {
            string employeeID = Convert.ToString(EmployeeID ?? string.Empty);
            DataTable dt = tacdBll.GetTaskAssignDetails(employeeID, "0").Tables[0];
            StringBuilder sbSkill = new StringBuilder();

            if (dt != null && dt.Rows.Count > 0)
            {
                dt.AsEnumerable().Where(item => item.Field<Int64>("available") == 1).ToList().ForEach(item => sbSkill.Append("<span class=\"skillSpan\">" + item.Field<string>("specialtyName") + "</span>"));
            }
            divMySkills.InnerHtml = sbSkill.ToString();
        }

        protected void LoadCerficate()
        {
            Cerficate certificate = cBll.GetModelList(" employeeID = '" + EmployeeID + "' AND isMain = 1 ").FirstOrDefault();
            //if (cerficate != null)
            //{
            //    imgCerficate.Src = "/FileOperation/ImageCompression.ashx?empID=" + EmployeeID + ""; // Convert.ToString(ConfigurationManager.AppSettings["fileSavePath"]) + "/" + cerficate.FILEPATH;
            //}
            if (certificate != null)
            {
                imgCerficate.ImageUrl = Path.Combine(ConfigurationManager.AppSettings["thumbnailsPath"].ToString(), certificate.FILEPATH); // "/FileOperation/ImageCompression.ashx?empID=" + EmployeeID;
            }
        }

        protected void LoadProject()
        {
            int totalRowsCount = 0;
            AspNetPager1.PageSize = 10;
            StringBuilder sbWhere = new StringBuilder();
            string anyCondition = txtAnyCondition.Text.Trim();
            if (!string.IsNullOrEmpty(anyCondition))
            {
                sbWhere.Append(anyCondition);
            }
            DataTable dtProject = prjDal.GetProjectForEmployeeHome(Convert.ToString(EmployeeID ?? string.Empty), sbWhere.ToString(), AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out totalRowsCount);
            if (dtProject != null && dtProject.Rows.Count > 0)
            {
                StringBuilder sbPrjId = new StringBuilder();
                dtProject.AsEnumerable().Select(item => (string)item["prjID"]).ToList().ForEach(item => sbPrjId.AppendFormat("'{0}',", item));
                projectIdNeed = sbPrjId.ToString().TrimEnd(',');
                lstTcje = new TransactionDetailsBLL().GetModelList(" AND employeeId = '" + Convert.ToString(EmployeeID ?? string.Empty) + "' AND TRANSACTIONTYPE = 7 AND PROJECTID IN (" + projectIdNeed + ") ");
                lstPrjProportion = new ProjectProportionBLL().GetModelList(" projectId IN (" + projectIdNeed + ")");
            }

            empProportion = epBll.GetModelList(" AND EMPLOYEEID = '" + Convert.ToString(EmployeeID ?? string.Empty) + "'").FirstOrDefault();

            if (string.IsNullOrEmpty(sbWhere.ToString()))
            {
                lblFinishedTaskCount.Text = string.Format("{0}", totalRowsCount);
            }
            AspNetPager1.RecordCount = totalRowsCount;
            gvProject.DataSource = dtProject;
            gvProject.DataBind();
            gvProject.Columns[1].Visible = IsExternal;
        }

        #region Some Events
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

        #region Repeat Databound，计算提成金额、任务剩余时间和修改剩余时间
        protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region 设置任务状态样式
                Label lblTaskStatus = e.Row.FindControl("lblTaskStatus") as Label;
                if (lblTaskStatus != null && !string.IsNullOrEmpty(lblTaskStatus.Text.Trim()))
                {
                    if (lblTaskStatus.Text.Trim() == "暂停")
                    {
                        lblTaskStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                    }
                }
                #endregion

                #region 计算提成金额
                #region 预计提成
                string prjId = gvProject.DataKeys[e.Row.RowIndex].Values[0].ToString();
                decimal proportion = 0m;
                // 先看此项目是否单独设置了提成
                ProjectProportion projectProportion = lstPrjProportion?.Where(item => item.PROJECTID == prjId).FirstOrDefault();  // new ProjectProportionBLL().GetModelList(" projectId = '" + prjId + "'").FirstOrDefault();
                // 如果设置了单独提成，则采用单独设置的比例
                if (projectProportion != null)
                {
                    proportion = projectProportion.PROPORTION ?? 0m;
                }
                // 如果未设置，则采用默认提成
                else if (empProportion != null)
                {
                    proportion = empProportion?.PROPORTION ?? 0m;
                }
                Label lblExpectAmount = e.Row.FindControl("lblExpectAmount") as Label;
                HiddenField hidOrderAmount = e.Row.FindControl("hidOrderAmount") as HiddenField;
                decimal orderAmount = Convert.ToDecimal(hidOrderAmount.Value);
                //Project prj = new ProjectBLL().GetModel(prjId);
                lblExpectAmount.Text = (orderAmount * Convert.ToDecimal(proportion)).ToString();
                //2019-03-26，未转正期间的任务隐藏预计提成
                Project project = new ProjectBLL().GetModel(prjId);
                if (project.CREATEDATE != null && toRegularDate != null)
                {
                    if (project.CREATEDATE <= toRegularDate || toRegularDate == Convert.ToDateTime("2000/1/1 00:00:00"))
                    {
                        lblExpectAmount.Text = "--";
                    }
                }
                #endregion

                #region 实际提成
                //decimal tcje = new TransactionDetailsBLL().GetModelList(" AND employeeId = '" + Convert.ToString(EmployeeID ?? string.Empty) + "' AND TRANSACTIONTYPE = 7 AND PROJECTID = '" + prjId + "' ").Sum(item => item.TRANSACTIONAMOUNT) ?? 0m;
                Label lblProportionAmount = e.Row.FindControl("lblProportionAmount") as Label;
                //lblProportionAmount.Text = tcje.ToString();
                if (lstTcje != null && lstTcje.Count() > 0)
                {
                    decimal transactionAmount = lstTcje.Where(item => item.PROJECTID == prjId && item.EMPLOYEEID == EmployeeID).Sum(item => item.TRANSACTIONAMOUNT) ?? 0m;
                    lblProportionAmount.Text = transactionAmount.ToString();
                }
                else
                {
                    lblProportionAmount.Text = "0";
                }
                #endregion
                #endregion

                #region 计算任务剩余时间
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
                        lblTimeRemain.Text = string.Format("逾期{0}", Common.TransformTimeSpan(ts));
                        lblTimeRemain.ForeColor = System.Drawing.Color.Red;
                    }
                }
                #endregion

                #region 计算修改剩余时间
                string projectId = gvProject.DataKeys[e.Row.RowIndex].Values[0].ToString();
                Label lblModifyTaskTimeRemain = e.Row.FindControl("lblModifyTaskTimeRemain") as Label;
                DataTable dt = new FileCategoryBLL().GetExpireDateByProjectId(projectId).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Button btnSetModifyTasksFinished = e.Row.FindControl("btnSetModifyTasksFinished") as Button;
                    btnSetModifyTasksFinished.Visible = true;
                    string folderName = Convert.ToString(dt.Rows[0]["folderName"]);
                    btnSetModifyTasksFinished.CommandArgument = projectId + "|" + folderName;
                    if (folderName.Contains("修改"))
                    {
                        string strExpireDate = Convert.ToString(dt.Rows[0]["expireDate"]);

                        DateTime dtExpire = Convert.ToDateTime(strExpireDate);
                        if (DateTime.Now < dtExpire)
                        {
                            TimeSpan ts = dtExpire - DateTime.Now;
                            //设置提醒label文本
                            lblModifyTaskTimeRemain.Text = string.Format("{0}剩余{1}小时", folderName, Math.Floor(ts.TotalHours));
                            if (ts.TotalHours <= 3)
                            {
                                lblModifyTaskTimeRemain.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");//不足3小时，红色
                                lblModifyTaskTimeRemain.Font.Bold = true;
                            }
                            else if (ts.TotalHours <= 6)
                            {
                                lblModifyTaskTimeRemain.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF8800");//不足6小时，橙色
                                lblModifyTaskTimeRemain.Font.Bold = true;
                            }
                            else if (ts.TotalHours <= 12)
                            {
                                lblModifyTaskTimeRemain.ForeColor = System.Drawing.ColorTranslator.FromHtml("#EEEE00");//不足12小时，黄色
                                lblModifyTaskTimeRemain.Font.Bold = true;
                            }
                            else if (ts.TotalHours <= 24)
                            {
                                lblModifyTaskTimeRemain.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF77FF");//不足24小时，洋红色
                            }
                            else if (ts.TotalHours <= 48)
                            {
                                lblModifyTaskTimeRemain.ForeColor = System.Drawing.ColorTranslator.FromHtml("#57C600");//48小时以上，酸橙色(浅绿)
                            }
                            else
                            {
                                lblModifyTaskTimeRemain.Text = string.Format("{0}剩余&gt;2天", folderName);
                            }
                        }
                        else if (dtExpire < DateTime.Now)
                        {
                            TimeSpan ts = DateTime.Now - dtExpire;
                            lblModifyTaskTimeRemain.Text = string.Format("{0}逾期{1}", folderName, Common.TransformTimeSpan(ts));
                            lblModifyTaskTimeRemain.ForeColor = System.Drawing.Color.Red;
                        }
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

        #region GridView Row Command，将任务或修改任务置为完成
        protected void gvProject_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // 将任务置为完成
            if (e.CommandName == "setFinished")
            {
                string prjId = e.CommandArgument.ToString();
                Project prj = new ProjectBLL().GetModel(prjId);
                prj.ISFINISHED = 1;
                new ProjectBLL().Update(prj);
                LoadProject();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('设置成功！');", true);
            }
            // 将修改任务置为完成
            else if (e.CommandName == "setModifyFinished")
            {
                ProjectBLL pBll = new ProjectBLL();
                string[] args = e.CommandArgument.ToString().Split('|');
                string projectID = args[0];
                string modfItem = args[1];
                Project project = pBll.GetModel(projectID);
                // 查询修改任务的完成稿是否存在
                bool isExistFinalModifyScript = pBll.IsExistFinalModifyScript(projectID, modfItem);
                if (!isExistFinalModifyScript)
                {
                    bool addFlag = pBll.AddProjectModify(projectID, modfItem, 1, 1, DateTime.Now);
                    if (addFlag)
                    {
                        LogHelper.WriteLine(string.Format("【Success】ID【{0}】任务修改记录的完成稿【{1}】创建成功", projectID, modfItem));
                        if (!trBll.IsExist(project.ENTERINGPERSON, project.TASKNO, modfItem, "1", "1"))
                        {
                            int addTaskRemindingFlag = trBll.Add(EmployeeNo, project.ENTERINGPERSON, project.TASKNO, modfItem, "0", DateTime.Now.ToString(), null, "1", "1", "1");
                            if (addTaskRemindingFlag > 0)
                            {
                                LoadProject();
                                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('设置成功！');", true);
                                LogHelper.WriteLine(string.Format("【任务监控】售后完成任务【{0}】-【{1}】的提醒添加成功", project.TASKNO, modfItem));
                                return;
                            }
                        }
                    }
                    else
                    {
                        LogHelper.WriteLine(string.Format("【任务监控】ID【{0}】任务修改记录的完成稿【{1}】创建失败", projectID, modfItem));
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('设置失败！');", true);
                        return;
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('设置失败！修改完成稿已存在。');", true);
                    return;
                }
            }
        }
        #endregion
        #endregion

        public string GetQueryString(string key, string defaultValue = "")
        {
            if (Page.RouteData.Values.Keys.Contains(key) && Page.RouteData.Values[key] != null)
                return Page.RouteData.Values[key].ToString();
            else if (!string.IsNullOrWhiteSpace(Request.QueryString[key]))
                return Request.QueryString[key];
            else
                return defaultValue;
        }
    }
}