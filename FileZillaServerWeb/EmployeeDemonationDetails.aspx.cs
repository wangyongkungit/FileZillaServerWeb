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
    public partial class EmployeeDemonationDetails : System.Web.UI.Page
    {
        EmployeeBLL empBll = new EmployeeBLL();
        EmployeeDominationBLL edBll = new EmployeeDominationBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllEmployee();
            }
        }

        private void LoadAllEmployee()
        {
            DataTable dtEmployee = empBll.GetListUnionNoAndNameForDemonation(string.Empty, "employeeNo").Tables[0];
            cblEmployeeDemonation.DataSource = dtEmployee;
            cblEmployeeDemonation.DataTextField = "noAndName";
            cblEmployeeDemonation.DataValueField = "ID";
            cblEmployeeDemonation.DataBind();
        }

        private void LoadEmployeeDemonation()
        {
            string employeeID = Request.QueryString["employeeID"];

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string parentEmployeeID = Request.QueryString["employeeID"];
            foreach (ListItem item in cblEmployeeDemonation.Items)
            {

                string childEmployeeID = item.Value;
                bool exist = edBll.ExistsByParentIDAndChildID(parentEmployeeID, childEmployeeID);
                // 如果存在记录，即父级员工已经具备管理当前子员工的权限
                if (exist)
                {
                    // 如果没被选中，则说明取消了父级员工管理该子员工的权限
                    if (!item.Selected)
                    {
                        bool delFlag = edBll.DeleteByParentIDAndChildID(parentEmployeeID, childEmployeeID);
                    }
                }
                // 如果当前父级员工不具备管理当前子员工的权限
                else
                {
                    // 如果是选中的，说明需要赋予父级员工管理当前子员工的权限
                    if (item.Selected)
                    {
                        FileZillaServerModel.EmployeeDomination empDemonation = new FileZillaServerModel.EmployeeDomination();
                        empDemonation.ID = Guid.NewGuid().ToString();
                        empDemonation.PARENTEMPLOYEEID = parentEmployeeID;
                        empDemonation.CHILDEMPLOYEEID = childEmployeeID;
                        bool addFlag = edBll.Add(empDemonation);
                    }
                }
            }
            LoadAllEmployee();
        }

        protected void cblEmployeeDemonation_DataBound(object sender, EventArgs e)
        {
            string parentEmployeeID = Request.QueryString["employeeID"];
            CheckBoxList cbList = sender as CheckBoxList;
            foreach (ListItem item in cbList.Items)
            {
                string childlEmployeeID = item.Value;
                bool exist = edBll.ExistsByParentIDAndChildID(parentEmployeeID, childlEmployeeID);
                item.Selected = exist;
                if (exist)
                {
                    item.Attributes.CssStyle.Add(HtmlTextWriterStyle.Color, "#66cc00");
                }
                if (childlEmployeeID == parentEmployeeID)
                {
                    item.Enabled = false;
                }
            }
        }
    }
}