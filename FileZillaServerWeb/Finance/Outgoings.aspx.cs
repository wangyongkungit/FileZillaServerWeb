using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using FileZillaServerModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yiliangyijia.Comm;

namespace FileZillaServerWeb.Finance
{
    public partial class Outgoings : System.Web.UI.Page
    {
        EmployeeBLL eBll = new EmployeeBLL();
        TransactionDetailsBLL tdBll = new TransactionDetailsBLL();
        EmployeeAccountBLL empAcctBll = new EmployeeAccountBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtTransacDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtPlanDate.Text = DateTimeHelper.GetFirstDateOfCurrentMonth().ToString("yyyy-MM");
                LoadEmployee();
                LoadTransaction();
                DropDownListDataBind(ddlTransacType, ConfigTypeName.奖励与处罚类型, "-请选择-");
            }
        }

        /// <summary>
        /// dropdownlist加载员工编号和姓名信息
        /// </summary>
        protected void LoadEmployee()
        {
            DataTable dtEmp = eBll.GetListUnionNoAndNameForDemonation(string.Empty, "EMPLOYEENO").Tables[0];
            ddlEmployeeName.DataSource = dtEmp;
            ddlEmployeeName.DataTextField = "NOANDNAME";
            ddlEmployeeName.DataValueField = "ID";
            ddlEmployeeName.DataBind();
        }

        /// <summary>
        /// 加载交易信息
        /// </summary>
        private void LoadTransaction()
        {
            int sumAmount = 0;
            int totalRowsCount = 0;
            DataTable dtExport;
            Dictionary<string, bool> dicSelectFlag = new Dictionary<string, bool>();
            string employeeID = ddlEmployeeName.SelectedValue;
            Dictionary<string, string> dicCondition = new Dictionary<string, string>();
            string transacType = ddlTransacType.SelectedValue;
            if (!string.IsNullOrEmpty(transacType))
            {
                dicCondition.Add("transacType", transacType);
            }
            dicCondition.Add("employeeId", employeeID);
            AspNetPager1.PageSize = 10;
            DataTable dtTransac = tdBll.GetListJoinEmpAndPrj(dicCondition, dicSelectFlag, string.Empty, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out totalRowsCount, out sumAmount, out dtExport).Tables[0];
            AspNetPager1.RecordCount = totalRowsCount; Page.Title += totalRowsCount;
            gvTransaction.DataSource = dtTransac;
            gvTransaction.DataBind();
        }

        /// <summary>
        /// 查询按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlEmployeeName.SelectedValue))
            {
                LoadTransaction();
            }
        }

        /// <summary>
        /// 添加按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAmount.Text.Trim()))
            {
                AddTransaction();
                LoadTransaction();
            }
        }

        /// <summary>
        /// 添加一条交易记录
        /// </summary>
        private void AddTransaction()
        {
            string employeeID = ddlEmployeeName.SelectedValue;

            // 首先添加交易记录
            TransactionDetails transac = new TransactionDetails();
            transac.ID = Guid.NewGuid().ToString();
            transac.TRANSACTIONAMOUNT = Convert.ToDecimal(txtAmount.Text.Trim());
            transac.TRANSACTIONTYPE = Convert.ToInt32(ddlTransacType.SelectedValue);
            transac.TRANSACTIONDESCRIPTION = txtDescription.Text.Trim();
            transac.PROJECTID = new ProjectBLL().GetPrjIDByTaskNo(txtTaskNo.Text.Trim());
            transac.EMPLOYEEID = employeeID;
            DateTime dtTransacDate = DateTime.Now;
            string strTransacDate = txtTransacDate.Text.Trim();
            if (!string.IsNullOrEmpty(strTransacDate))
            {
                dtTransacDate = Convert.ToDateTime(strTransacDate);
            }
            transac.TRANSACTIONDATE = dtTransacDate;
            transac.PLANDATE = Convert.ToDateTime(string.Format("{0}-01 00:00:00", txtPlanDate.Text.Trim()));
            transac.CREATEDATE = DateTime.Now;
            if (tdBll.Add(transac))
            {
                // 操作相应的员工账户
                EmployeeAccount empAcct = empAcctBll.GetModelList(" employeeID = '" + employeeID + "'").FirstOrDefault();
                if (empAcct != null)
                {
                    // 账户余额加上
                    empAcct.AMOUNT += transac.TRANSACTIONAMOUNT;
                    // 如果是3（即工资发放），则需要在已发项目上加上金额
                    if (ddlTransacType.SelectedValue == "3")
                    {
                        empAcct.PAIDAMOUNT += Math.Abs(transac.TRANSACTIONAMOUNT ?? 0m); // 员工账户中的已发金额加上此次发放的金额
                    }
                    if (empAcctBll.Update(empAcct))
                    {
                        txtAmount.Text = txtTransacDate.Text = string.Empty;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加成功！');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加失败！');", true);
                }
            }
            else
            {

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
                if (configTypeName == ConfigTypeName.奖励与处罚类型)
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

        /// <summary>
        /// GridView Row Command 事件，删除一行记录，并更改员工账户余额中的记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del")
            {
                string ID = e.CommandArgument.ToString();
                TransactionDetails transactionDetails = tdBll.GetModel(ID);
                transactionDetails.ISDELETED = true;
                // 删除交易记录
                if (tdBll.Update(transactionDetails))
                {
                    // 员工账户对应记录也需要修改
                    //TransactionDetails transac = new TransactionDetails();
                    //transac = tdBll.GetModel(ID);
                    string employeeID = transactionDetails.EMPLOYEEID;
                    decimal amount = transactionDetails.TRANSACTIONAMOUNT ?? 0m;
                    EmployeeAccount empAcct = empAcctBll.GetModelList(" employeeID = '" + employeeID + "'").FirstOrDefault();
                    if (empAcct != null)
                    {
                        // 账户余额再减去刚刚删除的交易记录的金额
                        empAcct.AMOUNT -= transactionDetails.TRANSACTIONAMOUNT;
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

        protected void gvTransaction_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string tdID = gvTransaction.DataKeys[e.RowIndex].Value.ToString();
            TextBox txtPlanDate = gvTransaction.Rows[e.RowIndex].FindControl("txtPlanDate") as TextBox;
            string strPlanDate = string.Format("{0}-01 00:00:00", txtPlanDate.Text.Trim());
            DateTime dtPlanDate = Convert.ToDateTime(strPlanDate);
            TransactionDetails transac = tdBll.GetModel(tdID);
            transac.PLANDATE = dtPlanDate;
            if (tdBll.Update(transac))
            {
                gvTransaction.EditIndex = -1;
                LoadTransaction();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('更新计划时间成功！');", true);
                return;
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('更新计划时间失败！');", true);
                return;
            }
        }

        protected void gvTransaction_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTransaction.EditIndex = e.NewEditIndex;
            LoadTransaction();
        }

        protected void gvTransaction_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTransaction.EditIndex = -1;
            LoadTransaction();
        }
    }
}