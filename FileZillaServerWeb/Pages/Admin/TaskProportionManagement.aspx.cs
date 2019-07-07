using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FileZillaServerBLL;
using System.Data;
using FileZillaServerModel;
using FileZillaServerCommonHelper;

namespace FileZillaServerWeb
{
    public partial class TaskProportionManagement : WebPageHelper
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "任务分成管理";//设置网页标题
            if (!IsPostBack)
            {
                RepeaterDataBind();
            }
        }

        ProjectSharingBLL psBll = new ProjectSharingBLL();

        /// <summary>
        /// Repeater数据绑定
        /// </summary>
        private void RepeaterDataBind()
        {
            string projectID = Request.QueryString["projectID"];
            string where = string.Format(" projectid = '{0}'", projectID);
            DataTable dt = psBll.GetListInnerJoinEmployee(where).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                lblTaskNo.Text = dt.Rows[0]["TASKNO"].ToString();
                gvData.DataSource = dt;
                gvData.DataBind();
            }
        }

        /// <summary>
        /// 给相应完成人的比例赋值（数据库中存储的是小数，需要转换成百分比）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1 && e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    TextBox textBox = e.Row.FindControl("txtProportion") as TextBox;
                    if (textBox != null)
                    {
                        if (textBox.Text != string.Empty)
                        {
                            textBox.Text = (Convert.ToDouble(textBox.Text) * 100).ToString();
                            //textBox.Attributes["contentEditable"] = "false";
                        }
                    }

                    //System.Web.UI.HtmlControls.HtmlInputText textBox = e.Row.FindControl("txtProportion") as System.Web.UI.HtmlControls.HtmlInputText;
                    //if (textBox != null)
                    //{
                    //    if (textBox.Value != string.Empty)
                    //    {
                    //        textBox.Value = (Convert.ToDouble(textBox.Value) * 100).ToString();
                    //        //textBox.Attributes["contentEditable"] = "false";
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// 确定按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            try
            {
                int rowsCount = gvData.Rows.Count;
                GridViewRow gridRow;
                TextBox textBox;
                //System.Web.UI.HtmlControls.HtmlInputText textBox;
                for (int i = 0; i < rowsCount; i++)
                {
                    gridRow = gvData.Rows[i];
                    string id = gvData.DataKeys[i].Value.ToString();
                    textBox = (TextBox)gridRow.FindControl("txtProportion");
                    if (textBox != null)
                    {
                        LogHelper.WriteLine("txtId: " + textBox.Attributes["name"] + "   TextBox.Text:   " + textBox.Text);

                        string textValue = Request.Form["ctl00$ContentPlaceHolder1$gvData$ctl0" + (i+2) + "$txtProportion"];
                        //double proportion = Convert.ToInt16(textBox.Text.Trim()) * 0.01;
                        double proportion = Convert.ToInt16(textValue) * 0.01;
                        ProjectSharing model = psBll.GetModel(id);
                        model.PROPORTION = proportion;
                        bool update = psBll.Update(model);
                        if (!update)
                        {
                            LogHelper.WriteLine("任务比例设置失败！");
                            Alert("设置失败！");
                            break;
                        }
                    }

                    //HtmlControls.HtmlInputText


                    //gridRow = gvData.Rows[i];
                    //string id = gvData.DataKeys[i].Value.ToString();
                    //textBox = (System.Web.UI.HtmlControls.HtmlInputText)gridRow.FindControl("txtProportion");
                    //if (textBox != null)
                    //{
                    //    LogHelper.WriteLine("txtId: " + textBox.ID + " " + textBox.Value);

                    //    //string textValue = Request.Form["ctl00$ContentPlaceHolder1$gvData$ctl0" + (i) + "$txtProportion"];
                    //    double proportion = Convert.ToInt16(textBox.Value.Trim()) * 0.01;
                    //    //double proportion = Convert.ToInt16(textValue) * 0.01;
                    //    ProjectSharing model = psBll.GetModel(id);
                    //    model.PROPORTION = proportion;
                    //    bool update = psBll.Update(model);
                    //    if (!update)
                    //    {
                    //        LogHelper.WriteLine("任务比例设置失败！");
                    //        Alert("设置失败！");
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        RepeaterDataBind();
                    //        Alert("设置成功！");
                    //    }
                    //}
                }
                RepeaterDataBind();
                Alert("设置成功！");
            }
            catch (Exception ex)
            {
                Alert("出错了！");
                LogHelper.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// GridView RowCommand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "del")
                {
                    string psID = e.CommandArgument.ToString();
                    bool flag = psBll.Delete(psID);
                    if (flag)
                    {
                        RepeaterDataBind();
                        Alert("删除成功！");
                        return;
                    }
                    else
                    {
                        Alert("删除成功！");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + ex.StackTrace);
            }
        }
    }
}