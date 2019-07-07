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
    public partial class rwmb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }
        
        ConfigureBLL cBll = new ConfigureBLL();

        private void Bind()
        {
            DataTable dt = cBll.GetTaskObjectiveValue();
            gvData.DataSource = dt;
            gvData.DataBind();
        }

        protected void gvData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvData.EditIndex = e.NewEditIndex;
            Bind();
        }

        protected void gvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtObjectiveValue = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtObjectiveValue");
            TextBox txtD_value = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtD_value");
            int objectiveValue = Convert.ToInt16(txtObjectiveValue.Text.Trim());
            int d_value = Convert.ToInt16(txtD_value.Text.Trim());
            string id = gvData.DataKeys[e.RowIndex].Value.ToString();
            bool flag = cBll.UpdateObjectiveValue(objectiveValue, d_value, id);
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

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvData.EditIndex = -1;
            Bind();
        }
    }
}