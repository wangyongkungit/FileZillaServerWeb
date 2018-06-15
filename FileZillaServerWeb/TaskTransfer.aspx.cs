using FileZillaServerBLL;
using FileZillaServerModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class TaskTransfer : System.Web.UI.Page
    {
        EmployeeProportionBLL epBll = new EmployeeProportionBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hidAmount.Value = Request.QueryString["amount"].ToString();
                lblAmount.Text = Request.QueryString["amount"].ToString();
                LoadCanTransferEmp();
            }
        }

        /// <summary>
        /// 加载当前分部领导手下的员工
        /// </summary>
        private void LoadCanTransferEmp()
        {
            string parentEmployeeID = Convert.ToString(Request.QueryString["parentEmployeeID"]);
            DataTable dtCanTransferEmp = epBll.GetEmployeeCanTransfer(parentEmployeeID).Tables[0];
            ddlCanTransferEmp.DataSource = dtCanTransferEmp;
            ddlCanTransferEmp.DataValueField = "eID";
            ddlCanTransferEmp.DataTextField = "name";
            ddlCanTransferEmp.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // 声明变量
            EmployeeAccountBLL eaBll = new EmployeeAccountBLL();
            // 任务ID
            string prjID = Request.QueryString["prjID"].ToString();
            // 当前分部领导 empId
            string parentEmployeeID = Convert.ToString(Request.QueryString["parentEmployeeID"]);
            // 需要转移对象的 empId
            string employeeID = ddlCanTransferEmp.SelectedValue;

            // 更新部门领导账户，按照配置比例计算出金额后累加到分部领导账户
            EmployeeProportion proportion = new EmployeeProportionBLL().GetModelList(" employeeId = '" + parentEmployeeID + "'").FirstOrDefault();
            decimal amountToLeader = Convert.ToDecimal(Request.QueryString["amount"]);
            EmployeeAccount empAcctParent = new EmployeeAccount();
            empAcctParent = eaBll.GetModelList(" employeeId = '" + parentEmployeeID + "'").FirstOrDefault();
            empAcctParent.AMOUNT += amountToLeader * proportion.PROPORTION;
            empAcctParent.LASTUPDATEDATE = DateTime.Now;
            eaBll.Update(empAcctParent);

            // 更新任务完成人
            ProjectSharing ps = new ProjectSharing();
            ps = new ProjectSharingBLL().GetModelList(" projectId = '" + prjID + "' AND FInishedperson = '" + parentEmployeeID + "'").FirstOrDefault();
            ps.FINISHEDPERSON = employeeID;
            new ProjectSharingBLL().Update(ps);

            //// 转移到的员工，需要待任务完成后，再计入账户
            //EmployeeAccount empAcctTransferTo = new EmployeeAccount();
            //empAcctTransferTo = eaBll.GetModelList(" employeeID = '" + employeeID + "'").FirstOrDefault();
            //empAcctTransferTo.AMOUNT += Convert.ToDecimal(txtAmount.Text.Trim());
            //empAcctTransferTo.LASTUPDATEDATE = DateTime.Now;
            //eaBll.Update(empAcctTransferTo);
        }
    }
}