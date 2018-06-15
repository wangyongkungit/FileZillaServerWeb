using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using FileZillaServerModel;
using FileZillaServerProfile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb.Finance
{
    public partial class payoff : System.Web.UI.Page
    {
        EmployeeBLL eBll = new EmployeeBLL();
        TransactionDetailsBLL tdBll = new TransactionDetailsBLL();
        EmployeeAccountBLL empAcctBll = new EmployeeAccountBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = "工资发放";
                LoadEmployee();
                DropDownListDataBind(ddlTransacType, ConfigTypeName.奖励与处罚类型, "-请选择-");
                ddlTransacType.Enabled = false;
                ddlTransacType.SelectedValue = "3";
            }
            txtTransacDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// dropdownlist加载员工编号和姓名信息
        /// </summary>
        protected void LoadEmployee()
        {
            try
            {
                DataTable dtEmp = eBll.GetListUnionNoAndNameForDemonation(string.Empty, "EMPLOYEENO").Tables[0];
                ddlEmployeeName.DataSource = dtEmp;
                ddlEmployeeName.DataTextField = "NOANDNAME";
                ddlEmployeeName.DataValueField = "ID";
                ddlEmployeeName.DataBind();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message);
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('程序异常！');", true);
            }
        }

        private void LoadTransaction()
        {
            try
            {
                int totalRowsCount = 0;
                string employeeId = UserProfile.GetInstance()?.ID;
                string transacType = ddlTransacType.SelectedValue;
                Dictionary<string, string> dicCondition = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(transacType))
                {
                    dicCondition.Add("transacType", transacType);
                }
                //dicCondition.Add("employeeId", employeeId);
                DataTable dt = tdBll.GetListJoinEmpAndPrj(dicCondition, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out totalRowsCount).Tables[0];
                gvTransaction.DataSource = dt;
                gvTransaction.DataBind();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message);
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('程序异常！');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeAccount empAcct = empAcctBll.GetModelList(" employeeID = '" + ddlEmployeeName.SelectedValue + "'").FirstOrDefault();
                txtCanPayoff.Text = empAcct?.AMOUNT.ToString();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message);
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('程序异常！');", true);
            }
        }

        #region Dropdownlist Databind
        /// <summary>
        /// 绑定DropDownList的方法
        /// </summary>
        /// <param name="dropDownList"></param>
        /// <param name="configTypeName"></param>
        /// <param name="tipString"></param>
        protected void DropDownListDataBind(DropDownList dropDownList, ConfigTypeName configTypeName, string tipString)
        {
            try
            {
                //DataTable dt = new ConfigureBLL().GetConfig(configTypeName.ToString());
                DataTable dtConfig = dtConfig = new ConfigureBLL().GetConfig();
                DataTable dtNew = dtConfig.Clone();
                DataRow[] drArray = dtConfig.Select("configtypename='" + configTypeName + "'");
                for (int i = 0; i < drArray.Length; i++)
                {
                    dtNew.ImportRow(drArray[i]);
                }
                dropDownList.DataSource = dtNew;
                dropDownList.DataTextField = "configvalue";
                dropDownList.DataValueField = "configkey";
                dropDownList.DataBind();
                dtNew = null;
                if (configTypeName != ConfigTypeName.奖励与处罚类型)
                {
                    dropDownList.Items.Insert(0, new ListItem(tipString, string.Empty));
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + ex.StackTrace);
            }
        }
        #endregion

        protected void btnPayoff_Click(object sender, EventArgs e)
        {
            try
            {
                decimal payOffAmount = Convert.ToDecimal(txtAmount.Text.Trim());
                // 员工账户表修改
                EmployeeAccount empAcct = empAcctBll.GetModelList(" employeeID = '" + ddlEmployeeName.SelectedValue + "'").FirstOrDefault();
                empAcct.AMOUNT -= payOffAmount;
                empAcct.PAIDAMOUNT += payOffAmount;
                if (!empAcctBll.Update(empAcct))
                {
                    LogHelper.WriteLine(ddlEmployeeName.SelectedItem.Text + "账户修改失败");
                }

                // 交易记录表对应记录也需要修改
                TransactionDetails transac = new TransactionDetails();
                transac.ID = Guid.NewGuid().ToString();
                transac.TRANSACTIONAMOUNT = payOffAmount;
                transac.TRANSACTIONDATE = Convert.ToDateTime(txtTransacDate.Text);
                transac.TRANSACTIONTYPE = Convert.ToInt32(ddlTransacType.SelectedValue);
                transac.EMPLOYEEID = ddlEmployeeName.SelectedValue;
                transac.CREATEDATE = DateTime.Now;
                if (!tdBll.Add(transac))
                {
                    LogHelper.WriteLine(ddlEmployeeName.SelectedItem.Text + "交易记录添加失败");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message);
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('程序异常！" + ex.Message + "');", true);
            }
        }

        protected void gvTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                string ID = e.CommandArgument.ToString();
                // 删除交易记录
                if (tdBll.Delete(ID))
                {
                    // 员工账户对应记录也需要修改
                    TransactionDetails transac = new TransactionDetails();
                    transac = tdBll.GetModel(ID);
                    string employeeID = transac.EMPLOYEEID;
                    decimal amount = transac.TRANSACTIONAMOUNT ?? 0m;
                    EmployeeAccount empAcct = empAcctBll.GetModelList(" employeeID = '" + employeeID + "'").FirstOrDefault();
                    if (empAcct != null)
                    {
                        // 账户余额再减去刚刚删除的交易记录的金额
                        empAcct.AMOUNT -= transac.TRANSACTIONAMOUNT;
                        if (empAcctBll.Update(empAcct))
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('操作成功！');", true);
                        }
                    }
                    LoadTransaction();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('操作失败！');", true);
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
            LoadTransaction();
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
    }
}