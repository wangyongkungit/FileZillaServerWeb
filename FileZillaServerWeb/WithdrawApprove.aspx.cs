using FileZillaServerBLL;
using FileZillaServerModel;
using FileZillaServerProfile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class WithdrawApprove : System.Web.UI.Page
    {
        EmployeeAccountBLL empAcctBll = new EmployeeAccountBLL();
        WithdrawDetailsBLL wBll = new WithdrawDetailsBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "提现审批";
            if (!IsPostBack)
            {
                LoadWithdraw();
            }
        }

        private void LoadWithdraw()
        {
            string where = "";
            if (!string.IsNullOrEmpty(Request.QueryString["employeeID"]))
            {
                where += " employeeID = '" + Request.QueryString["employeeID"] + "'";
            }
            DataTable dtWithdraw = wBll.GetListUnionEmployee(where).Tables[0];
            gvWithdrawApprove.DataSource = dtWithdraw;
            gvWithdrawApprove.DataBind();
        }

        protected void gvWithdrawApprove_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Confirm")
            {
                string[] strArgs = e.CommandArgument.ToString().Split('|');
                string withdrawID = strArgs[0];
                string employeeID = strArgs[1];
                WithdrawDetails withdrawDetails = wBll.GetModel(withdrawID);
                withdrawDetails.ISCONFIRMED = true;
                withdrawDetails.OPERATEPERSON = UserProfile.GetInstance().ID;
                if (wBll.Update(withdrawDetails))
                {
                    EmployeeAccount empAcct02 = empAcctBll.GetModelList(" employeeID = '" + employeeID + "'").FirstOrDefault();
                    if (empAcct02 != null)
                    {
                        if (empAcct02.AMOUNT >= withdrawDetails.WITHDRAWAMOUNT)
                        {
                            // 余额减去
                            empAcct02.AMOUNT -= withdrawDetails.WITHDRAWAMOUNT;
                            // 已发加上
                            empAcct02.PAIDAMOUNT += withdrawDetails.WITHDRAWAMOUNT;
                            if (empAcctBll.Update(empAcct02))
                            {
                                LoadWithdraw();
                                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('操作成功！');", true);
                            }
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('操作失败！code: 4001');", true);
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('操作失败！');", true);
                }
            }
        }
    }
}