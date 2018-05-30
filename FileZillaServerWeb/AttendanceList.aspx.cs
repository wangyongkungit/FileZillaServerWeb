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
    public partial class AttendanceList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "考勤列表";
            if (!IsPostBack)
            {
                GridViewDataBind();
            }
        }

        AttendanceBLL aBll = new AttendanceBLL();

        /// <summary>
        /// 绑定GridView
        /// </summary>
        private void GridViewDataBind()
        {
            //员工姓名
            string name = txtEmployeeName.Text.Trim();
            //员工编号
            string employeeNo = txtEmployeeNo.Text.Trim();
            string where = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                where += " AND name like '%" + name + "%'";
            }
            if (!string.IsNullOrEmpty(employeeNo))
            {
                where += " AND employeeNo like '%" + employeeNo + "%'";
            }
            DataSet ds = aBll.GetListUnionEmp(where);
            DataTable dt = ds.Tables[0];
            gvData.DataSource = dt;
            gvData.DataBind();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridViewDataBind();
        }

        /// <summary>
        /// 页索引发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            GridViewDataBind();
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
    }
}