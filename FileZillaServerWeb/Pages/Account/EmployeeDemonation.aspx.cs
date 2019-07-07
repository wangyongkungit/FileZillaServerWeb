using FileZillaServerBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class EmployeeDemonation : System.Web.UI.Page
    {
        EmployeeBLL empBll = new EmployeeBLL();


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "员工权限配置";
            if (!IsPostBack)
            {
                LoadEmployee();
            }
        }

        private void LoadEmployee()
        {
            DataTable dtEmployee = empBll.GetList(string.Empty, "employeeNo").Tables[0];
            gvEmployee.DataSource = dtEmployee;
            gvEmployee.DataBind();
        }
    }
}