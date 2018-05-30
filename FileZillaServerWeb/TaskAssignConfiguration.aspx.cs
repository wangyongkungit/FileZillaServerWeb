using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FileZillaServerBLL;
using System.Data;
using FileZillaServerModel;
using FileZillaServerCommonHelper;
using System.Text;

namespace FileZillaServerWeb
{
    public partial class TaskAssignConfiguration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "任务分配参数配置";
            if (!IsPostBack)
            {
                LoadEmployee();
                LoadTaskAssignList();
                LoadRightdownPercent();
                LoadWeightsConfig();
            }
        }

        EmployeeBLL eBll = new EmployeeBLL();
        TaskAssignConfigBLL taBll = new TaskAssignConfigBLL();
        TaskAssignConfigDetailsBLL tacdBll = new TaskAssignConfigDetailsBLL();
        RightDownBLL rBll = new RightDownBLL();
        WeightsConfigBLL wcBll = new WeightsConfigBLL();

        protected void LoadEmployee()
        {
            DataTable dtEmp = eBll.GetListUnionNoAndName(string.Empty, "EMPLOYEENO").Tables[0];
            ddlEmployeeName.DataSource = dtEmp;
            ddlEmployeeName.DataTextField = "NOANDNAME";
            ddlEmployeeName.DataValueField = "ID";
            ddlEmployeeName.DataBind();
        }

        protected void LoadTaskAssignList()
        {
            DataTable dtTaskAssignList = taBll.GetListJoinEmp(string.Empty).Tables[0];
            gvEmpTaskAssignConfig.DataSource = dtTaskAssignList;
            gvEmpTaskAssignConfig.DataBind();
        }

        protected void LoadRightdownPercent()
        {
            DataTable dtRightdown = rBll.GetList(string.Empty).Tables[0];
            gvRightdown.DataSource = dtRightdown;
            gvRightdown.DataKeyNames = new string[] { "ID" };
            gvRightdown.DataBind();
        }

        protected void LoadWeightsConfig()
        {
            DataTable dtWeights = wcBll.GetList(string.Empty).Tables[0];
            gvWeights.DataSource = dtWeights;
            gvWeights.DataKeyNames = new string[] { "ID" };
            gvWeights.DataBind();
        }

        protected void LoadGridView()
        {
            DataTable dt = null;
        }

        protected void btnAddToList_Click(object sender, EventArgs e)
        {
            string employeeID = ddlEmployeeName.SelectedValue;
            TaskAssignConfig taskAssign = new TaskAssignConfig();
            taskAssign.ID = Guid.NewGuid().ToString();
            taskAssign.EMPLOYEEID = employeeID;
            taskAssign.AVAILABLE = 1;
            if (!taBll.ExistsByEmployeeID(employeeID))
            {
                if (!taBll.Add(taskAssign))
                {
                    LogHelper.WriteLine("添加员工到专业分配表失败");
                }
            }
            LoadEmployee();
            LoadTaskAssignList();
        }

        protected void gvEmpTaskAssignConfig_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string taskAssignID = gvEmpTaskAssignConfig.DataKeys[e.Row.RowIndex].Value.ToString();
                TaskAssignConfig taskAssign = new TaskAssignConfig();
                taskAssign = taBll.GetModel(taskAssignID);
                // 如果显示隐藏的复选框没选中，默认不显示禁用的数据
                if (!cbxShowVisible.Checked && taskAssign.AVAILABLE == 0)
                {
                    e.Row.Visible = false;
                }

                HiddenField hidEmpID = e.Row.FindControl("hidEmpID") as HiddenField;
                DataTable dt = tacdBll.GetTaskAssignDetails(hidEmpID.Value, "0").Tables[0];
                StringBuilder sbTip = new StringBuilder();

                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.AsEnumerable().Where(item => item.Field<Int64>("available") == 1).ToList().ForEach(item => sbTip.Append(item.Field<string>("specialtyName") + "：" + string.Format("{0:0.##}", item.Field<decimal>("QUALITYSCORE")) + "，"));
                }
                sbTip.Append("<br/>预期目标：" + string.Format("{0:0.##}", taskAssign.TARGETAMOUNT) + "，工时倍数：" + taskAssign.TIMEMULTIPLE + "<br/>");
                Label lblTip = e.Row.FindControl("lblTip") as Label;
                if (lblTip != null)
                {
                    lblTip.Text = sbTip.ToString();
                }
            }
        }

        protected void gvEmpTaskAssignConfig_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {            
            string taskAssignID = gvEmpTaskAssignConfig.DataKeys[e.RowIndex].Value.ToString();
            TaskAssignConfig taskAssign = new TaskAssignConfig();
            taskAssign = taBll.GetModel(taskAssignID);
            taskAssign.AVAILABLE = taskAssign.AVAILABLE == 0 ? 1 : 0;
            if (taBll.Update(taskAssign))
            {
                LoadTaskAssignList();
            }
        }

        protected void gvRightdown_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = gvRightdown.DataKeys[e.RowIndex]["ID"].ToString();
            TextBox txtFromvalue = (TextBox)gvRightdown.Rows[e.RowIndex].FindControl("txtFromvalue");
            string strFromvalue = txtFromvalue.Text.Trim();
            int fromValue = Convert.ToInt32(strFromvalue);
            TextBox txtTovalue = (TextBox)gvRightdown.Rows[e.RowIndex].FindControl("txtTovalue");
            string strTovalue = txtTovalue.Text.Trim();
            int toValue = Convert.ToInt32(strTovalue);
            TextBox txtRightdownPercent = (TextBox)gvRightdown.Rows[e.RowIndex].FindControl("txtRightdownPercent");
            string strRightdownPercent = txtRightdownPercent.Text.Trim();
            decimal rightdownPercent = Convert.ToDecimal(strRightdownPercent) * 0.01M;
            RightDown rightDown = new RightDown();
            rightDown.ID = ID;
            rightDown.FROMVALUE = fromValue;
            rightDown.TOVALUE = toValue;
            rightDown.RIGHTDOWNPERCENT = rightdownPercent;
            rBll.Update(rightDown);
            gvRightdown.EditIndex = -1;
            LoadRightdownPercent();
        }

        protected void gvRightdown_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvRightdown.EditIndex = e.NewEditIndex;
            LoadRightdownPercent();
        }

        protected void gvRightdown_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRightdown.EditIndex = -1;
            LoadRightdownPercent();
        }

        protected void btnSaveRightdown_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFromvalue02.Text.Trim()) || string.IsNullOrEmpty(txtTovalue02.Text.Trim()))
            {
                return;
            }
            int fromValue = Convert.ToInt32(txtFromvalue02.Text.Trim());
            int toValue = Convert.ToInt32(txtTovalue02.Text.Trim());
            decimal rightDownPercent = Convert.ToDecimal("0." + txtRightdownPercent02.Text.Trim());
            RightDown rightDown = new RightDown();
            rightDown.ID = Guid.NewGuid().ToString();
            rightDown.FROMVALUE = fromValue;
            rightDown.TOVALUE = toValue;
            rightDown.RIGHTDOWNPERCENT = rightDownPercent;
            if (rBll.Add(rightDown))
            {
                gvRightdown.EditIndex = -1;
                LoadRightdownPercent();
            }
        }

        protected void gvRightdown_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Del"))
            {
                string ID = e.CommandArgument.ToString();
                rBll.Delete(ID);
                txtFromvalue02.Text = string.Empty;
                txtTovalue02.Text = string.Empty;
                txtRightdownPercent02.Text = string.Empty;
                gvRightdown.EditIndex = -1;
                LoadRightdownPercent();
            }
        }

        #region Weights
        protected void gvWeights_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = gvWeights.DataKeys[e.RowIndex]["ID"].ToString();
            TextBox txtItemValue = (TextBox)gvWeights.Rows[e.RowIndex].FindControl("txtItemValue");
            string strItemValue = txtItemValue.Text.Trim();
            decimal itemValue = Convert.ToDecimal(strItemValue) * 0.01M;

            WeightsConfig weightConfig = new WeightsConfig();
            weightConfig = wcBll.GetModel(ID);
            weightConfig.ITEMVALUE = itemValue;
            wcBll.Update(weightConfig);
            gvWeights.EditIndex = -1;
            LoadWeightsConfig();
        }

        protected void gvWeights_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvWeights.EditIndex = e.NewEditIndex;
            LoadWeightsConfig();
        }

        protected void gvWeights_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvWeights.EditIndex = -1;
            LoadWeightsConfig();
        }

        protected void gvWeights_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// 显示禁用的复选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxShowVisible_CheckedChanged(object sender, EventArgs e)
        {
            LoadTaskAssignList();
        }
    }
}