using FileZillaServerBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            DataSet ds = eBll.GetList(string.Empty, "EMPLOYEENO");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rptData.DataSource = ds.Tables[0];
                rptData.DataBind();
            }
        }
    }
}