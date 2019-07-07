using FileZillaServerBLL;
using FileZillaServerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class Withdraw : System.Web.UI.Page
    {
        EmployeeAccountBLL eaBll = new EmployeeAccountBLL();
        WithdrawDetailsBLL wdBll = new WithdrawDetailsBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string employeeID = Request.QueryString["employeeID"];
                hidEmployeeID.Value = employeeID;
                EmployeeAccount empAcct = eaBll.GetModelList(" employeeID = '" + employeeID + "'").FirstOrDefault();
                if (empAcct != null)
                {
                    // 员工已经申请但尚未通过审核的金额
                    decimal withdrawAmount = wdBll.GetModelList(" employeeID = '" + employeeID + "' and isconfirmed = 0").Sum(item => item.WITHDRAWAMOUNT) ?? 0m;
                    // 可提现金额，为员工账户剩余金额 减去 上面获取到的金额
                    lblCanWithdrawAmount.Text = (empAcct.AMOUNT - withdrawAmount).ToString();
                }
                else
                {
                    lblCanWithdrawAmount.Text = "数据异常，请联系管理员！";
                    lblCanWithdrawAmount.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnWithdrwa_Click(object sender, EventArgs e)
        {

        }
    }
}