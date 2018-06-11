using FileZillaServerBLL;
using FileZillaServerCommonHelper;
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
    public partial class TransactionRecords : System.Web.UI.Page
    {
        EmployeeBLL eBll = new EmployeeBLL();
        TransactionDetailsBLL tdBll = new TransactionDetailsBLL();
        EmployeeAccountBLL empAcctBll = new EmployeeAccountBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTransaction();
                DropDownListDataBind(ddlTransacType, ConfigTypeName.奖励与处罚类型, "-请选择-");
            }
        }

        private void LoadTransaction()
        {
            int totalRowsCount = 0;
            string employeeId = UserProfile.GetInstance()?.ID;
            string transacType = ddlTransacType.SelectedValue;
            string amountFrom = txtAmountFrom.Text.Trim();
            string amountTo = txtAmountTo.Text.Trim();
            string dateFrom = txtDateFrom.Text.Trim();
            string dateTo = txtDateTo.Text.Trim();
            string taskNo = txtTaskNo.Text.Trim();
            Dictionary<string, string> dicCondition = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(transacType))
            {
                dicCondition.Add("transacType", transacType);
            }
            if (!string.IsNullOrEmpty(amountFrom))
            {
                dicCondition.Add("amountFrom", amountFrom);
            }
            if (!string.IsNullOrEmpty(amountTo))
            {
                dicCondition.Add("amountTo", amountTo);
            }
            if (!string.IsNullOrEmpty(dateFrom))
            {
                dicCondition.Add("dateFrom", dateFrom);
            }
            if (!string.IsNullOrEmpty(dateTo))
            {
                dicCondition.Add("dateTo", dateTo);
            }
            dicCondition.Add("employeeId", employeeId);
            DataTable dt = tdBll.GetListJoinEmpAndPrj(dicCondition, AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out totalRowsCount).Tables[0];
            gvTransaction.DataSource = dt;
            gvTransaction.DataBind();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}