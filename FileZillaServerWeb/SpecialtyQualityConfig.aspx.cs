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
    public partial class SpecialtyQualityConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSpecialtyQuality();
                //LoadSpecialtyMinor();
                LoadEmpTargetAmountAndTimeMultiple();
            }
        }

        ConfigureBLL cBll = new ConfigureBLL();
        TaskAssignConfigBLL tacBll = new TaskAssignConfigBLL();
        TaskAssignConfigDetailsBLL tacdBll = new TaskAssignConfigDetailsBLL();
        
        //protected void LoadSpecialty()
        //{
        //    DataTable dt = tacdBll.GetSpecialtyConfig(Convert.ToString(Request.QueryString["employeeID"]));
        //    gvSpcQlt.DataSource = dt;
        //    gvSpcQlt.DataBind();
        //}

        protected void LoadSpecialtyQuality()
        {
            DataTable dt = tacdBll.GetTaskAssignDetails(Convert.ToString(Request.QueryString["employeeID"]), "0").Tables[0];
            gvSpcQlt.DataSource = dt;
            gvSpcQlt.DataKeyNames = new string[] { "ID", "SpecialtyKey" };
            gvSpcQlt.DataBind();
            gvSpcQlt.Attributes.Add("style", "table-layout:fixed");//固定编辑状态时的样式
            // type 为 0，是从管理界面打开的，允许修改质量分；为 1 时，是从员工主页过来的，只允许启用、禁用专业，不允许修改质量分
            if (Request.QueryString["type"] == "1")
            {
                gvSpcQlt.Columns[2].Visible = false;
                cblst.Visible = false;
                divTimeMultiple.Visible = false;
            }
        }

        protected void LoadEmpTargetAmountAndTimeMultiple()
        {
            DataTable dtTargetAmount = tacBll.GetList(" employeeID = '" + Convert.ToString(Request.QueryString["employeeID"]) + "'").Tables[0];
            if (dtTargetAmount != null && dtTargetAmount.Rows.Count > 0)
            {
                txtTargetAmount.Text = Convert.ToString(dtTargetAmount.AsEnumerable().Select(row => row.Field<decimal?>("TargetAmount")).FirstOrDefault());
                txtTimeMultiple.Text = Convert.ToString(dtTargetAmount.AsEnumerable().Select(row => row.Field<int?>("timeMultiple")).FirstOrDefault());
            }
        }

        protected void LoadSpecialtyMinor()
        {
            DataTable dt = tacdBll.GetTaskAssignDetails(Convert.ToString(Request.QueryString["employeeID"]), "1").Tables[0];
            cblstSpecialtyMinor.DataSource = dt;
            cblstSpecialtyMinor.DataTextField = "specialtyName";
            cblstSpecialtyMinor.DataValueField = "specialtyKey";
            cblstSpecialtyMinor.DataBind();
        }

        protected DataTable LoadEmpSpecialtyQualityDetails()
        {
            DataTable dt = tacdBll.GetList("employeeID = '" + Convert.ToString(Request.QueryString["employeeID"]) + "' and specialtyType = 0").Tables[0];
            return dt;
        }

        protected void gvSpcQlt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkSpc = (CheckBox)e.Row.FindControl("chkSpecialtyName");
                string specialtyKey = gvSpcQlt.DataKeys[e.Row.RowIndex]["SpecialtyKey"].ToString();

                DataTable dt = LoadEmpSpecialtyQualityDetails();
                DataRow[] drs = dt.Select("specialtyCategory = '" + specialtyKey + "'");
                if (drs != null && drs.Length > 0)
                {
                    chkSpc.Checked = Convert.ToString(drs[0]["AVAILABLE"]) == "1";
                }
                Label lblSpecialtyName = (Label)e.Row.FindControl("lblSpecialtyName");
                lblSpecialtyName.ForeColor = chkSpc.Checked ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            }
        }

        protected void gvSpcQlt_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSpcQlt.EditIndex = e.NewEditIndex;
            LoadSpecialtyQuality();
        }

        protected void gvSpcQlt_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ID = gvSpcQlt.DataKeys[e.RowIndex]["ID"].ToString();
            string specialtyKey = gvSpcQlt.DataKeys[e.RowIndex]["SpecialtyKey"].ToString();
            CheckBox chkAvailable = (CheckBox)gvSpcQlt.Rows[e.RowIndex].FindControl("chkSpecialtyName");
            int available = chkAvailable.Checked ? 1 : 0;
            TextBox txtScore = (TextBox)gvSpcQlt.Rows[e.RowIndex].FindControl("txtQualityScore");
            string strScore = txtScore.Text.Trim();
            decimal? score = !string.IsNullOrEmpty(strScore) ? Convert.ToDecimal(strScore) : 0;
            //TextBox txtTimeMultiple = (TextBox)gvSpcQlt.Rows[e.RowIndex].FindControl("txtTimeMultiple");
            //string strTimeMultiple = txtTimeMultiple.Text.Trim();
            //int? timeMultiple = !string.IsNullOrEmpty(strTimeMultiple) ? Convert.ToInt32(strTimeMultiple) : 1;
            TaskAssignConfigDetails tacdModel = new TaskAssignConfigDetails();
            tacdModel.ID = ID;
            tacdModel.EMPLOYEEID = Request.QueryString["employeeID"].ToString();
            tacdModel.SPECIALTYCATEGORY = specialtyKey;
            tacdModel.QUALITYSCORE = score;
            tacdModel.AVAILABLE = available;
            //tacdModel.TIMEMULTIPLE = timeMultiple;
            tacdModel.SPECIALTYTYPE = 0;
            if (string.IsNullOrEmpty(ID))
            {
                tacdModel.ID = Guid.NewGuid().ToString();
                tacdBll.Add(tacdModel);
            }
            else
            {
                tacdBll.Update(tacdModel);
            }
            gvSpcQlt.EditIndex = -1;
            LoadSpecialtyQuality();
        }

        protected void gvSpcQlt_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSpcQlt.EditIndex = -1;
            LoadSpecialtyQuality();
        }

        protected void btnSaveTargetAmount_Click(object sender, EventArgs e)
        {
            #region Fold
            //string employeeID = Request.QueryString["employeeID"];
            //foreach (ListItem item in cblstSpecialtyMinor.Items)
            //{
            //    string specialtyKey = item.Value;
            //    DataTable dt = tacdBll.GetTaskAssignDetails(Convert.ToString(Request.QueryString["employeeID"]), "1").Tables[0];
            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        DataRow[] dr = dt.Select("specialtyKey = '" + specialtyKey + "'");
            //        if (dr.Length > 0)
            //        {
            //            string ID = Convert.ToString(dr[0]["ID"]);
            //            TaskAssignConfigDetails tacdModel = new TaskAssignConfigDetails();
            //            tacdModel.ID = ID;
            //            tacdModel.EMPLOYEEID = employeeID;
            //            tacdModel.SPECIALTYCATEGORY = specialtyKey;
            //            tacdModel.AVAILABLE = item.Selected ? 1 : 0;
            //            tacdModel.SPECIALTYTYPE = 1;
            //            if (string.IsNullOrEmpty(ID))
            //            {
            //                tacdModel.ID = Guid.NewGuid().ToString();
            //                tacdBll.Add(tacdModel);
            //            }
            //            else
            //            {
            //                tacdBll.Update(tacdModel);
            //            }
            //        }
            //    }
            //}
            #endregion
            string employeeID = Convert.ToString(Request.QueryString["employeeID"]);
            DataTable dt = tacBll.GetList("employeeID = '" + employeeID + "'").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                string ID = Convert.ToString(dt.Rows[0]["ID"]);
                TaskAssignConfig tacModel = new TaskAssignConfig();
                tacModel = tacBll.GetModel(ID);
                tacModel.TARGETAMOUNT = Convert.ToDecimal(txtTargetAmount.Text.Trim());
                tacBll.Update(tacModel);
                LoadEmpTargetAmountAndTimeMultiple();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('修改成功');", true);
            }
        }

        protected void cblstSpecialtyMinor_DataBound(object sender, EventArgs e)
        {
            DataTable dt = tacdBll.GetTaskAssignDetails(Convert.ToString(Request.QueryString["employeeID"]), "1").Tables[0];
            CheckBoxList cblst = sender as CheckBoxList;
            foreach (ListItem item in cblst.Items)
            {
                string itemValue = item.Value;
                DataRow[] dr = dt.Select("specialtyKey = '" + itemValue + "'");
                if (dr.Length > 0)
                {
                    item.Selected = dr[0]["AVAILABLE"].ToString() == "1";
                }
            }
        }

        protected void btnSaveTimeMultiple_Click(object sender, EventArgs e)
        {
            string employeeID = Convert.ToString(Request.QueryString["employeeID"]);
            DataTable dt = tacBll.GetList("employeeID = '" + employeeID + "'").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                string ID = Convert.ToString(dt.Rows[0]["ID"]);
                TaskAssignConfig tacModel = new TaskAssignConfig();
                tacModel = tacBll.GetModel(ID);
                tacModel.TIMEMULTIPLE = Convert.ToInt32(txtTimeMultiple.Text.Trim());
                tacBll.Update(tacModel);
                LoadEmpTargetAmountAndTimeMultiple();
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('修改成功');", true);
            }
        }
    }
}