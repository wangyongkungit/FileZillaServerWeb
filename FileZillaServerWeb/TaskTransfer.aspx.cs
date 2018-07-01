using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using FileZillaServerModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yiliangyijia.Comm;

namespace FileZillaServerWeb
{
    public partial class TaskTransfer : WebPageHelper
    {
        EmployeeProportionBLL epBll = new EmployeeProportionBLL();
        TransactionDetailsBLL tdBll = new TransactionDetailsBLL();
        ProjectSharingBLL psBll = new ProjectSharingBLL();
        EmployeeBLL empBll = new EmployeeBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string prjID = Request.QueryString["prjID"].ToString();
                decimal amount = Convert.ToDecimal(Request.QueryString["amount"]);
                string parentEmployeeID = Convert.ToString(Request.QueryString["parentEmployeeID"]);
                string where = " AND employeeId = '" + parentEmployeeID + "'";

                decimal proportion = 0m;
                ProjectProportion projectProportion = new ProjectProportionBLL().GetModelList(" projectId = '" + prjID + "'").FirstOrDefault();
                if (projectProportion != null)
                {
                    proportion = projectProportion.PROPORTION ?? 0m;
                }
                else
                {
                    EmployeeProportion empPro = epBll.GetModelList(where).FirstOrDefault();
                    proportion = empPro?.PROPORTION ?? 0m;
                }
                hidAmount.Value = lblAmount.Text = (amount * proportion).ToString();
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
            ddlCanTransferEmp.DataTextField = "empNoAndName";
            ddlCanTransferEmp.DataBind();
            ddlCanTransferEmp.Items.Insert(0, new ListItem("-请选择-", string.Empty));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlCanTransferEmp.SelectedValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择需要转移到的员工！');", true);
                return;
            }
            try
            {
                if (Convert.ToDecimal(txtProportion.Text.Trim()) > 100)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('比例不能超过100%！');", true);
                    return;
                }
                if (Convert.ToDecimal(txtAmount.Text.Trim()) > Convert.ToDecimal(hidAmount.Value))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('不能超过原始金额！');", true);
                    return;
                }
            }
            catch (Exception ex0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('数值不正确！');", true);
                return;
            }
            // 声明变量
            EmployeeAccountBLL eaBll = new EmployeeAccountBLL();
            // 任务ID
            string prjID = Request.QueryString["prjID"].ToString();
            // 当前分部领导 empId
            string parentEmployeeID = Convert.ToString(Request.QueryString["parentEmployeeID"]);
            // 需要转移对象的 empId
            string employeeID = ddlCanTransferEmp.SelectedValue;

            // 移动目录
            Employee empParent = empBll.GetModel(parentEmployeeID);
            string parentEmpNo = empParent.EMPLOYEENO;
            Employee empToTransfer = empBll.GetModel(employeeID);
            string transferToEmpNo = empToTransfer.EMPLOYEENO;

            FileCategoryBLL fcBll = new FileCategoryBLL();
            int errCode = 0;
            string returnFolderName = string.Empty;
            string taskRootFolder = string.Empty;
            string taskFolderWithoutEmpNo = string.Empty;
            fcBll.GetFilePathByProjectId(prjID, string.Empty, string.Empty, false, out returnFolderName, out taskRootFolder, out taskFolderWithoutEmpNo, out errCode);
            string sourceDirectory = string.Format(taskFolderWithoutEmpNo, parentEmpNo);
            string destinctDirectory = string.Format(taskFolderWithoutEmpNo, transferToEmpNo);

            try
            {
                Directory.Move(sourceDirectory, destinctDirectory);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine("|" + sourceDirectory + "|" + destinctDirectory);
                LogHelper.WriteLine(ex.Message + ex.StackTrace);
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('目录移动失败！');", true);
                return;
            }

            // 更新部门领导账户，按照配置比例计算出金额后累加到分部领导账户
            decimal proportion = 0;
            ProjectProportion projectProportion = new ProjectProportionBLL().GetModelList(" projectId = '" + prjID + "'").FirstOrDefault();
            if (projectProportion != null)
            {
                proportion = projectProportion.PROPORTION ?? 0m;
            }
            else
            {
                EmployeeProportion empProportion = new EmployeeProportionBLL().GetModelList(" AND employeeId = '" + parentEmployeeID + "'").FirstOrDefault();
                proportion = empProportion.PROPORTION ?? 0m;
            }
            decimal amountToLeader = Convert.ToDecimal(Request.QueryString["amount"]);
            EmployeeAccount empAcctParent = new EmployeeAccount();
            empAcctParent = eaBll.GetModelList(" employeeId = '" + parentEmployeeID + "'").FirstOrDefault();
            empAcctParent.AMOUNT += (amountToLeader * proportion);
            empAcctParent.LASTUPDATEDATE = DateTime.Now;
            eaBll.Update(empAcctParent);

            // 添加一条交易记录
            TransactionDetails transactionDetails = new TransactionDetails();
            transactionDetails.ID = Guid.NewGuid().ToString();
            transactionDetails.TRANSACTIONAMOUNT = amountToLeader * proportion;
            transactionDetails.TRANSACTIONDESCRIPTION = "分部领导提成";
            transactionDetails.TRANSACTIONTYPE = 6;
            transactionDetails.TRANSACTIONDATE = DateTime.Now;
            transactionDetails.PLANDATE = DateTimeHelper.GetFirstDateOfCurrentMonth();
            transactionDetails.EMPLOYEEID = parentEmployeeID;
            transactionDetails.PROJECTID = prjID;
            transactionDetails.ISDELETED = false;
            tdBll.Add(transactionDetails);

            //// 转移到的员工  先计入一条状态为已删除的数据，后续待任务完成时再调整：需要待任务完成后，再计入账户
            transactionDetails = new TransactionDetails();
            transactionDetails.ID = Guid.NewGuid().ToString();
            transactionDetails.TRANSACTIONAMOUNT = Convert.ToDecimal(txtAmount.Text.Trim());
            transactionDetails.TRANSACTIONDESCRIPTION = "项目提成（暂存）";
            transactionDetails.TRANSACTIONTYPE = 7;
            transactionDetails.TRANSACTIONDATE = DateTime.Now;
            transactionDetails.PLANDATE = DateTimeHelper.GetFirstDateOfCurrentMonth();
            transactionDetails.EMPLOYEEID = employeeID;
            transactionDetails.PROJECTID = prjID;
            transactionDetails.ISDELETED = true;
            tdBll.Add(transactionDetails);

            //EmployeeAccount empAcctTransferTo = new EmployeeAccount();
            //empAcctTransferTo = eaBll.GetModelList(" employeeID = '" + employeeID + "'").FirstOrDefault();
            //empAcctTransferTo.AMOUNT += Convert.ToDecimal(txtAmount.Text.Trim());
            //empAcctTransferTo.LASTUPDATEDATE = DateTime.Now;
            //eaBll.Update(empAcctTransferTo);

            // 更新任务完成人
            ProjectSharing ps = new ProjectSharing();
            ps = psBll.GetModelList(" projectId = '" + prjID + "' AND FInishedperson = '" + parentEmployeeID + "'").FirstOrDefault();
            ps.FINISHEDPERSON = employeeID;
            if (psBll.Update(ps))
            {
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('转移成功！');window.top.location.href='employeeHome.aspx';", true);
                return;
            }

            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('更新完成人失败！');window.top.location.href='employeeHome.aspx';", true);
            return;
        }
    }
}