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
    public partial class SalaryDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "工资详细";
            if (!IsPostBack)
            {
                DateTime dtNow = DateTime.Now;
                DateTime dt = new DateTime(dtNow.Year, dtNow.Month - 1, 1);
                txtMonthDate.Text = dt.ToString("yyyy-MM");
                Bind();
            }
        }

        SalaryBLL sBll = new SalaryBLL();

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void Bind()
        {
            string name = txtName.Text.Trim();
            string empNo = txtEmployeeNo.Text.Trim();
            string monthDate = txtMonthDate.Text.Trim();
            DataTable dt = sBll.GetListUnionEmp(name, empNo, monthDate).Tables[0];
            gvData.DataSource = dt;
            gvData.DataBind();
            gvData.Attributes.Add("style", "table-layout:fixed");//固定编辑状态时的样式
        }

        /// <summary>
        /// 查询按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Bind();
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvData.EditIndex = e.NewEditIndex;
            Bind();
        }

        protected void gvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtJbgz = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtBaseSalary");
            TextBox txtAgeWage = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtAgeWage");
            TextBox txtZsbt = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtZsbt");
            TextBox txtHsbt = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtHsbt");
            TextBox txtQtsr = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtQtsr");

            TextBox txtGrsb = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtGrsb");
            TextBox txtQysb = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtQysb");
            TextBox txtGrgjj = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtGrgjj");
            TextBox txtQygjj = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtQygjj");
            TextBox txtPieceWage = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtPieceWage");

            TextBox txtPiecePenalty = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtPiecePenalty");
            TextBox txtFullAttend = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtFullAttend");
            TextBox txtAttendancePenalty = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtAttendancePenalty");

            string jbgz = txtJbgz.Text.Trim();
            string ageWage = txtAgeWage.Text.Trim();
            string zsbt = txtZsbt.Text.Trim();
            string hsbt = txtHsbt.Text.Trim();
            string qtsr = txtQtsr.Text.Trim();
            string grsb = txtGrsb.Text.Trim();
            string qysb = txtQysb.Text.Trim();
            string grgjj = txtGrgjj.Text.Trim();
            string qygjj = txtQygjj.Text.Trim();
            string pieceWage = txtPieceWage.Text.Trim();
            string piecePenalty = txtPiecePenalty.Text.Trim();
            string fullAttend = txtFullAttend.Text.Trim();
            string attendancePenalty = txtAttendancePenalty.Text.Trim();

            string id = gvData.DataKeys[e.RowIndex].Value.ToString();

            Salary sal = sBll.GetModel(id);
            sal.BASESALARY = Convert.ToDecimal(jbgz);
            sal.AGEWAGE = Convert.ToDecimal(ageWage);
            sal.ACCOMMODATION_ALLOWANCE = Convert.ToDecimal(zsbt);
            sal.MEAL_ALLOWANCE = Convert.ToDecimal(hsbt);
            sal.OTHERWAGE = Convert.ToDecimal(qtsr);
            sal.SOCIALSECURITY_INDIVIDUAL = Convert.ToDecimal(grsb);
            sal.SOCIALSECURITY_COMPANY = Convert.ToDecimal(qysb);
            sal.HOUSINGPROVIDENTFUND_INDIVIDUAL = Convert.ToDecimal(grgjj);
            sal.HOUSINGPROVIDENTFUND_COMPANY = Convert.ToDecimal(qygjj);
            sal.PIECEWAGE = Convert.ToDecimal(pieceWage);
            sal.PIECEPENALTY = Convert.ToDecimal(piecePenalty);
            sal.FULLATTEND = Convert.ToDecimal(fullAttend);
            sal.ATTENDANCEPENALTY = Convert.ToDecimal(attendancePenalty);

            bool flag = sBll.Update(sal);
            if (flag)
            {
                gvData.EditIndex = -1;
                Bind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), string.Empty, "alert('更新成功！')", true);
            }
            else
            {
                gvData.EditIndex = -1;
                Bind();
                ClientScript.RegisterClientScriptBlock(this.GetType(), string.Empty, "alert('更新失败！')", true);
            }
        }

        protected void gvData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvData.EditIndex = -1;
            Bind();
        }
    }
}