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
    public partial class TaskTransfer : System.Web.UI.Page
    {
        EmployeeProportionBLL epBll = new EmployeeProportionBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hidAmount.Value = Request.QueryString["amount"].ToString();
                lblAmount.Text = Request.QueryString["amount"].ToString();
                LoadCanTransferEmp();
            }
        }

        private void LoadCanTransferEmp()
        {
            string parentEmployeeID = Convert.ToString(Request.QueryString["parentEmployeeID"]);
            DataTable dtCanTransferEmp = epBll.GetEmployeeCanTransfer(parentEmployeeID).Tables[0];
            ddlCanTransferEmp.DataSource = dtCanTransferEmp;
            ddlCanTransferEmp.DataValueField = "eID";
            ddlCanTransferEmp.DataTextField = "name";
            ddlCanTransferEmp.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string prjID = Request.QueryString["prjID"].ToString();
            string employeeID = ddlCanTransferEmp.SelectedValue;
            EmployeeAccount empAcct = new EmployeeAccount();
            EmployeeAccountBLL eaBll = new EmployeeAccountBLL();
            empAcct = eaBll.GetModelList(" employeeID = '" + employeeID + "'").FirstOrDefault();
            empAcct.AMOUNT += Convert.ToDecimal(txtAmount.Text.Trim());
            empAcct.LASTUPDATEDATE = DateTime.Now;
            eaBll.Update(empAcct);
        }
    }
}