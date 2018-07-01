using FileZillaServerBLL;
using FileZillaServerModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class EmployeeDefaultProportionConfig : System.Web.UI.Page
    {
        ConfigureBLL configBll = new ConfigureBLL();
        EmployeeProportionBLL epBll = new EmployeeProportionBLL();
        EmployeeBLL eBll = new EmployeeBLL();
        DataTable dtAllEmployeeProportion = new DataTable();
        DataTable dtManager = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "任务默认提成比例设置";
            dtAllEmployeeProportion = epBll.GetList(string.Empty).Tables[0];
            if (!IsPostBack)
            {
                LoadDefaultProportion();
            }
        }

        protected void LoadDefaultProportion()
        {
            DataTable dt = epBll.GetListJoinEmployee(string.Empty).Tables[0];
            dtManager = eBll.GetList(" AND isBranchLeader = 1 ", "EMPLOYEENO").Tables[0];
            gvEmployeeProportion.DataSource = dt;
            gvEmployeeProportion.DataBind();
        }

        protected void gvEmployeeProportion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string employeeID = gvEmployeeProportion.DataKeys[e.Row.RowIndex].Value.ToString();
                DropDownList ddlManager = e.Row.FindControl("ddlManager") as DropDownList;
                ddlManager.DataSource = dtManager.AsEnumerable().Where(item => item.Field<string>("ID") != employeeID).CopyToDataTable();
                ddlManager.DataTextField = "employeeNo";
                ddlManager.DataValueField = "ID";
                ddlManager.DataBind();
                ddlManager.Items.Insert(0, (new ListItem("--", string.Empty)));
                ddlManager.SelectedValue = dtAllEmployeeProportion.AsEnumerable().Where(item => item["employeeID"].ToString() == employeeID).Select(item => Convert.ToString(item["parentEmployeeID"])).FirstOrDefault();
            }
        }

        protected void gvEmployeeProportion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string employeeID = gvEmployeeProportion.DataKeys[e.RowIndex].Value.ToString();
            TextBox txtProportion = gvEmployeeProportion.Rows[e.RowIndex].FindControl("txtProportion") as TextBox;
            string strProportion = txtProportion.Text.Trim();
            decimal dProportion = !string.IsNullOrEmpty(strProportion) ? Convert.ToDecimal(strProportion) : 0m;
            DropDownList ddlManager = gvEmployeeProportion.Rows[e.RowIndex].FindControl("ddlManager") as DropDownList;
            string parentEmployeeID = ddlManager.SelectedValue;
            DataTable dtEmpProportion = epBll.GetList(" AND employeeID = '" + employeeID + "'").Tables[0];
            if (dtEmpProportion != null && dtEmpProportion.Rows.Count > 0)
            {
                EmployeeProportion empProportion = new EmployeeProportion();
                empProportion.ID = dtEmpProportion.AsEnumerable().FirstOrDefault()["ID"].ToString();
                empProportion.EMPLOYEEID = employeeID;
                empProportion.PROPORTION = dProportion;
                empProportion.PARENTEMPLOYEEID = parentEmployeeID == string.Empty ? null : parentEmployeeID;
                //empProportion.ISBRANCHLEADER = string.IsNullOrEmpty(parentEmployeeID);
                epBll.Update(empProportion);
            }
            else
            {
                EmployeeProportion empProportion = new EmployeeProportion();
                empProportion.ID = Guid.NewGuid().ToString();
                empProportion.EMPLOYEEID = employeeID;
                empProportion.PROPORTION = dProportion;
                empProportion.PARENTEMPLOYEEID = parentEmployeeID == string.Empty ? null : parentEmployeeID;
                //empProportion.ISBRANCHLEADER = string.IsNullOrEmpty(parentEmployeeID);
                epBll.Add(empProportion);
            }
            gvEmployeeProportion.EditIndex = -1;
            dtAllEmployeeProportion = epBll.GetList(string.Empty).Tables[0];
            LoadDefaultProportion();
        }

        protected void gvEmployeeProportion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployeeProportion.EditIndex = e.NewEditIndex;
            LoadDefaultProportion();
        }

        protected void gvEmployeeProportion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployeeProportion.EditIndex = -1;
            LoadDefaultProportion();
        }
    }
}