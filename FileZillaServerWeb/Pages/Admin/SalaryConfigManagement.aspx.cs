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
    public partial class SalaryConfigManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "工资基本参数配置";
            if (!IsPostBack)
            {
                Bind();
            }
        }

        SalaryConfigBLL scBll = new SalaryConfigBLL();

        /// <summary>
        /// 绑定GridView数据
        /// </summary>
        protected void Bind()
        {
            DataTable dt = scBll.GetListUnionEmp(string.Empty).Tables[0];
            gvData.DataSource = dt;
            gvData.DataBind();
            gvData.Attributes.Add("style", "table-layout:fixed");//固定编辑状态时的样式
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvData.EditIndex = e.NewEditIndex;
            Bind();
        }

        /// <summary>
        /// RowUpdating事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtBaseSalary = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtBaseSalary");
            TextBox txtCommission = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtCommission");
            TextBox txtAgeWage = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtAgeWage");
            TextBox txtZsbt = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtZsbt");
            TextBox txtHsbt = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtZsbt");
            TextBox txtQtsr = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtQtsr");
            TextBox txtGrsb = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtGrsb");
            TextBox txtQysb = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtQysb");
            TextBox txtGrgjj = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtGrgjj");
            TextBox txtQygjj = (TextBox)gvData.Rows[e.RowIndex].FindControl("txtQygjj");

            string baseSalary = txtBaseSalary.Text.Trim();                  //基本工资
            string commission = txtCommission.Text.Trim();                  //提成比例
            string ageWage = txtAgeWage.Text.Trim();                        //工龄工资
            string accommodationAlowance = txtZsbt.Text.Trim();             //住宿补贴
            string mealAllowance = txtHsbt.Text.Trim();                     //伙食补贴
            string otherWage = txtQtsr.Text.Trim();                         //其他收入
            string socialsecurityIndividual = txtGrsb.Text.Trim();          //个人社保
            string socialsecurityCompany = txtQysb.Text.Trim();             //企业社保
            string housingprovidentfundIndividual = txtGrgjj.Text.Trim();   //个人公积金
            string housingprovidentfundCompany = txtQygjj.Text.Trim();      //企业公积金

            string id = gvData.DataKeys[e.RowIndex].Value.ToString();
            SalaryConfig salary = scBll.GetModel(id);
            salary.BASESALARY = Convert.ToDecimal(baseSalary);
            salary.COMMISSION = Convert.ToDecimal(commission);
            salary.AGEWAGE = Convert.ToDecimal(ageWage);
            salary.ACCOMMODATION_ALLOWANCE = Convert.ToDecimal(accommodationAlowance);
            salary.OTHERWAGE = Convert.ToDecimal(otherWage);
            salary.SOCIALSECURITY_INDIVIDUAL = Convert.ToDecimal(socialsecurityIndividual);
            salary.SOCIALSECURITY_COMPANY = Convert.ToDecimal(socialsecurityCompany);
            salary.HOUSINGPROVIDENTFUND_INDIVIDUAL = Convert.ToDecimal(housingprovidentfundIndividual);
            salary.HOUSINGPROVIDENTFUND_COMPANY = Convert.ToDecimal(housingprovidentfundCompany);
            bool flag = scBll.Update(salary);
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