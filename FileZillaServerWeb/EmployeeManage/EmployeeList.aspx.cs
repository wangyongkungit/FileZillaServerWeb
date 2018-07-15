using FileZillaServerBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb.EmployeeManage
{
    public partial class EmployeeList : System.Web.UI.Page
    {
        EmployeeBLL eBll = new EmployeeBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataLoad();
            }
            //设置网页标题
            Page.Title = "员工列表";
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        private void DataLoad()
        {
            //获取列表并根据employeeNo排序
            StringBuilder sbWhere = new StringBuilder();
            string empNo = txtEmployeeNo.Text.Trim();
            string name = txtEmployeeName.Text.Trim();
            string mobilePhone = txtMobilePhone.Text.Trim();
            if (!string.IsNullOrEmpty(empNo))
            {
                sbWhere.AppendFormat(" AND EMPLOYEENO LIKE '%{0}%'", empNo);
            }
            if (!string.IsNullOrEmpty(name))
            {
                sbWhere.AppendFormat(" AND NAME LIKE '%{0}%'", name);
            }
            if (!string.IsNullOrEmpty(mobilePhone))
            {
                sbWhere.AppendFormat(" AND MOBILEPHONE LIKE '%{0}%'", mobilePhone);
            }

            DataSet ds = eBll.GetList(sbWhere.ToString(), "EMPLOYEENO");
            rptData.DataSource = ds.Tables[0];
            rptData.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataLoad();
        }
    }
}